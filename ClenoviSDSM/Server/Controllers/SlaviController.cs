using ClenoviSDSM.Shared.Models;
using ClenoviSDSM.Server.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClenoviSDSM.Server.Controllers
{
    [Authorize]
    [ApiController]
    public class SlaviController : ControllerBase
    {
        private readonly IConfiguration _conf;
        public SlaviController(IConfiguration configuration)
        {
            this._conf = configuration;
        }

        [HttpGet]
        [Route("api/GetSlavi")]
        public async Task<IEnumerable<Slava>> GetSlavi()
        {
            SlavaRepository repo = new SlavaRepository(_conf.GetConnectionString("dbClenovi"));

            IEnumerable<Slava> res = await repo.GetSlavi();

            return res;
        }

        [HttpPost]
        [Route("api/UpdateSlava")]
        public async Task UpdateSlava(Slava rs)
        {
            SlavaRepository repo = new SlavaRepository(_conf.GetConnectionString("dbClenovi"));
            await repo.UpdateSlava(rs);
        }

        [HttpPost]
        [Route("api/InsertSlava")]
        public async Task InsertSlava(Slava rs)
        {
            SlavaRepository repo = new SlavaRepository(_conf.GetConnectionString("dbClenovi"));
            await repo.InsertSlava(rs);
        }

        [HttpDelete]
        [Route("api/DeleteSlava/{id:int}")]
        public async Task DeleteSlava(int id)
        {
            SlavaRepository repo = new SlavaRepository(_conf.GetConnectionString("dbClenovi"));
            await repo.DeleteSlava(id);
        }
    }
}
