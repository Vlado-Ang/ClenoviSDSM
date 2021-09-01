using ClenoviSDSM.Shared.Models;
using ClenoviSDSM.Server.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;

namespace ClenoviSDSM.Server.Controllers
{
    [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _conf;
        private static string salt = "1q2W3e4R";

        public UserController(IConfiguration configuration)
        {
            this._conf = configuration;
        }

        [HttpGet]
        [Route("api/GetKorisnici")]
        public async Task<IEnumerable<User>> GetKorisnici()
        {
            UserRepository _userRepo = new UserRepository(_conf.GetConnectionString("dbClenovi"));
            IEnumerable<User> res = await _userRepo.GetKorisnici();

            return res;
        }

        [HttpPost]
        [Route("api/UpdateKorisnik")]
        public async Task UpdateKorisnik(User rs)
        {
            UserRepository repo = new UserRepository(_conf.GetConnectionString("dbClenovi"));
            await repo.UpdateKorisnik(rs);
        }

        [HttpPost]
        [Route("api/ChangePassword")]
        public async Task ChangePassword(User rs)
        {
            UserRepository repo = new UserRepository(_conf.GetConnectionString("dbClenovi"));
            string saltedPassword = rs.Username + "-" + rs.Password + "+" + salt;
            string passwordHash = MD5Hash(saltedPassword);

            rs.Password = passwordHash;
            await repo.ChangePassword(rs);
        }

        [HttpPost]
        [Route("api/InsertKorisnik")]
        public async Task InsertKorisnik(User rs)
        {
            UserRepository repo = new UserRepository(_conf.GetConnectionString("dbClenovi"));
            string saltedPassword = rs.Username + "-" + rs.Password + "+" + salt;
            string passwordHash = MD5Hash(saltedPassword);

            rs.Password = passwordHash;
            await repo.InsertKorisnik(rs);
        }

        [HttpDelete]
        [Route("api/DeleteKorisnik/{id:int}")]
        public async Task DeleteKorisnik(int id)
        {
            UserRepository repo = new UserRepository(_conf.GetConnectionString("dbClenovi"));
            await repo.DeleteKorisnik(id);
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
