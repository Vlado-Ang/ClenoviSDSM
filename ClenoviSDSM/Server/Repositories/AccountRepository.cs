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

namespace ClenoviSDSM.Server.Repositories
{
    public class AccountRepository :IAccountRepository
    {   
        private readonly TokenSettings _tokenSettings;
        public AccountRepository(IOptions<TokenSettings> tokenSettings)
        {
            _tokenSettings = tokenSettings.Value;
        }
        private List<User> Users = new List<User>
        {
            new User{
                Id = 1,
                FirstName = "Владимир",
                LastName = "Ангеловски",
                Username = "Admin",
                Password="1234",
                Roles = new List<Role>
                {
                    new Role 
                    {
                        RoleName= "Admin"
                    }
                }
            },
            new User{
                Id = 3,
                FirstName = "Test",
                LastName = "Viewer",
                Username = "Viewer",
                Password = "asdf",
                Roles = new List<Role>
                {
                    new Role
                    {
                        RoleName="Viewer"
                    }
                }
            },
            new User{
                Id = 2,
                FirstName = "Тест",
                LastName = "Едитор",
                Username = "Editor",
                Password = "asdf",
                Roles = new List<Role>
                {
                    new Role
                    {
                        RoleName="Editor"
                    }
                }
            }
        };


        public string GetAuthenticationToken(LoginModel loginModel)
        {
            User currentUser = Users.Where(_ => _.Username.ToLower() == loginModel.Username.ToLower() &&
            _.Password == loginModel.Password).FirstOrDefault();

            if (currentUser != null)
            {
                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Key));
                var credentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>();
                claims.Add(new Claim("username", loginModel.Username));
                claims.Add(new Claim("displayname", currentUser.FirstName + " " + currentUser.LastName));

                // Add roles as multiple claims
                foreach (var role in currentUser.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
                }

                var jwtToken = new JwtSecurityToken(
                    issuer: _tokenSettings.Issuer,
                    audience: _tokenSettings.Audience,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: credentials,
                    claims: claims.ToArray()
                );

                string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
                return token;
            }

            return string.Empty;
        }
    }
}
