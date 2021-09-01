using ClenoviSDSM.Shared.Models;
using ClenoviSDSM.Server.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;

namespace ClenoviSDSM.Server.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _conf;
        private IAccountRepository _accountRepository;
        private static string salt = "1q2W3e4R";

        public AccountController(IConfiguration configuration, IAccountRepository accountRepository)
        {
            this._conf = configuration;
            _accountRepository = accountRepository;
        }

        [HttpPost]
        [Route("api/LoginUser")]
        public async Task<IActionResult> LoginUser(LoginModel model)
        {
            UserRepository _userRepo = new UserRepository(_conf.GetConnectionString("dbClenovi"));

            User user = await _userRepo.GetKorisnik(model.Username);
            TokenModel tm = new TokenModel();
            tm.Token = String.Empty;

            if (user != null)
            {
                string saltedPassword = model.Username + "-" + model.Password + "+" + salt;
                string passwordHash = MD5Hash(saltedPassword);

                if (user.Password != passwordHash)
                {
                    return NotFound();
                }

                tm = await _accountRepository.GetAuthenticationToken(user);
            }
            else
            {
                return NotFound();
            }
            return Ok(tm);
        }

        [HttpPost]
        [Route("api/ActivateAccessTokenByRefresh")]
        public async Task<IActionResult> ActivateAccessTokenByRefresh(TokenModel refreshToken)
        {
            var resultTokenModel = await _accountRepository.ActivateTokenUsingRefreshToken(refreshToken);
            if (refreshToken == null)
            {
                return NotFound();
            }
            return Ok(resultTokenModel);
        }

        public static string MD5Hash(string text)
        {
            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text  
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));

            //get hash result after compute it  
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits  
                //for each byte  
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

    }
}
