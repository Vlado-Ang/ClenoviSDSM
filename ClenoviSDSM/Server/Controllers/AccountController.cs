using ClenoviSDSM.Shared.Models;
using ClenoviSDSM.Server.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClenoviSDSM.Server.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _conf;
        private IAccountRepository _accountRepository;

        public AccountController(IConfiguration configuration, IAccountRepository accountRepository)
        {
            this._conf = configuration;
            _accountRepository = accountRepository;
        }

        [HttpPost]
        [Route("api/LoginUser")]
        public IActionResult LoginUser(LoginModel model)
        {
            var token = _accountRepository.GetAuthenticationToken(model);

            if (string.IsNullOrEmpty(token))
            {
                return NotFound();
            }
            return Ok(new { token });
        }

    }
}
