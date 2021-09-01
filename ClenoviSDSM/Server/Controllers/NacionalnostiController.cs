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
    public class NacionalnostiController : ControllerBase
    {
        private readonly IConfiguration _conf;
        public NacionalnostiController(IConfiguration configuration)
        {
            this._conf = configuration;
        }

        [HttpGet]
        [Route("api/GetNacionalnosti")]
        public async Task<IEnumerable<Nacionalnost>> GetNacionalnosti()
        {
            NacionalnostRepository repo = new NacionalnostRepository(_conf.GetConnectionString("dbClenovi"));

            IEnumerable<Nacionalnost> res = await repo.GetNacionalnosti();

            return res;
        }

        [HttpPost]
        [Route("api/UpdateNacionalnost")]
        public async Task UpdateNacionalnost(Nacionalnost rs)
        {
            NacionalnostRepository repo = new NacionalnostRepository(_conf.GetConnectionString("dbClenovi"));
            await repo.UpdateNacionalnost(rs);
        }

        [HttpPost]
        [Route("api/InsertNacionalnost")]
        public async Task InsertNacionalnost(Nacionalnost rs)
        {
            NacionalnostRepository repo = new NacionalnostRepository(_conf.GetConnectionString("dbClenovi"));
            await repo.InsertNacionalnost(rs);
        }

        [HttpDelete]
        [Route("api/DeleteNacionalnost/{id:int}")]
        public async Task DeleteNacionalnost(int id)
        {
            NacionalnostRepository repo = new NacionalnostRepository(_conf.GetConnectionString("dbClenovi"));
            await repo.DeleteNacionalnost(id);
        }
    }
}
