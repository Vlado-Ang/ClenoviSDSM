using Microsoft.AspNetCore.Mvc;
using System;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace ClenoviSDSM.Server.Controllers
{
    public class BaseController : ControllerBase
    {
        private readonly IConfiguration _conf;

        public BaseController(IConfiguration configuration)
        {
            this._conf = configuration;
        }

        protected string _connString
        {
            get { return _conf.GetConnectionString("dbClenovi");  }
        }
    }
}
