using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClenoviSDSM.Shared.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;


namespace ClenoviSDSM.Server.Repositories
{
    public interface IAccountRepository
    {
        public Task<TokenModel> GetAuthenticationToken(User user);
        public Task<TokenModel> ActivateTokenUsingRefreshToken(TokenModel tokenModel);
    }
}
