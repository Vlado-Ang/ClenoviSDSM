using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClenoviSDSM.Shared.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Dapper;

namespace ClenoviSDSM.Server.Repositories
{
    public class AccountRepository :IAccountRepository
    {   
        private readonly TokenSettings _tokenSettings;
        private readonly IConfiguration _conf;

        public AccountRepository(IOptions<TokenSettings> tokenSettings, IConfiguration configuration)
        {
            _tokenSettings = tokenSettings.Value;
            _conf = configuration;
        }

        public async Task<TokenModel> GetAuthenticationToken(User user)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim("username", user.Username));
            claims.Add(new Claim("displayname", user.FirstName + " " + user.LastName));
            claims.Add(new Claim(ClaimTypes.Role, user.RoleName));

            return await GetTokens(user, claims);
        }

        private async Task<TokenModel> GetTokens(User currentUser, List<Claim> userClaims)
        {
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Key));
            var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            userClaims.RemoveAll(_ => _.Type == "aud");

            var jwtToken = new JwtSecurityToken(
                issuer: _tokenSettings.Issuer,
                audience: _tokenSettings.Audience,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials,
                claims: userClaims.ToArray()
            );

            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            string refreshToken = GetRefreshToken();

            currentUser.RefreshToken = refreshToken;
            await UpdateRefreshToken(currentUser);

            return new TokenModel
            {
                Token = token,
                RefreshToken = refreshToken
            };
        }

        private string GetRefreshToken()
        {
            var key = new Byte[32];
            using (var refreshTokenGenerator = RandomNumberGenerator.Create())
            {
                refreshTokenGenerator.GetBytes(key);
                return Convert.ToBase64String(key);
            }
        }

        public async Task UpdateRefreshToken(User user)
        {
            using (SqliteConnection conn = new SqliteConnection(_conf.GetConnectionString("dbClenovi")))
            {
                DynamicParameters par = new DynamicParameters();
                par.Add("@Id", user.Id);
                par.Add("@RefreshToken", user.RefreshToken);

                await conn.OpenAsync();

                var res = await conn.ExecuteAsync(
                    "UPDATE tUsers SET  RefreshToken = @RefreshToken WHERE Id = @Id",
                    par,
                    commandType: System.Data.CommandType.Text
                );
            }
        }

        public async Task<TokenModel> ActivateTokenUsingRefreshToken(TokenModel tokenModel)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var claimsPrincipal = tokenHandler.ValidateToken(tokenModel.Token,
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = _tokenSettings.Issuer,
                ValidateAudience = true,
                ValidAudience = _tokenSettings.Audience,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Key)),
                ValidateLifetime = true
            }, out SecurityToken validatedToken);


            var jwtToken = validatedToken as JwtSecurityToken;

            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256))
            {
                return null;
            }

            var username = claimsPrincipal.Claims.Where(_ => _.Type == "username").Single().Value;
            if (string.IsNullOrEmpty(username))
            {
                return null;
            }

            UserRepository _userRepo = new UserRepository(_conf.GetConnectionString("dbClenovi"));

            User currentUser = await _userRepo.GetKorisnik(username);

                //_myWorldDbContext.User.Where(_ => _.Email == email && _.RefreshToken == tokenModel.RefreshToken).FirstOrDefault();
            if (currentUser == null || currentUser.RefreshToken != tokenModel.RefreshToken)
            {
                return null;
            }

            return await GetTokens(currentUser, jwtToken.Claims.ToList());
        }
    }
}
