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
    public class StepeniObrazovanieController : ControllerBase
    {
        private readonly IConfiguration _conf;
        public StepeniObrazovanieController(IConfiguration configuration)
        {
            this._conf = configuration;
        }

        [HttpGet]
        [Route("api/GetStepeniObrazovanie")]
        public async Task<IEnumerable<StepenObrazovanie>> GetStepeniObrazovanie()
        {
            StepenObrazovanieRepository repo = new StepenObrazovanieRepository(_conf.GetConnectionString("dbClenovi"));

            IEnumerable<StepenObrazovanie> res = await repo.GetStepeniObrazovanie();

            return res;
        }

        [HttpPost]
        [Route("api/UpdateStepenObrazovanie")]
        public async Task UpdateStepenObrazovanie(StepenObrazovanie rs)
        {
            StepenObrazovanieRepository repo = new StepenObrazovanieRepository(_conf.GetConnectionString("dbClenovi"));
            await repo.UpdateStepenObrazovanie(rs);
        }

        [HttpPost]
        [Route("api/InsertStepenObrazovanie")]
        public async Task InsertStepenObrazovanie(StepenObrazovanie rs)
        {
            StepenObrazovanieRepository repo = new StepenObrazovanieRepository(_conf.GetConnectionString("dbClenovi"));
            await repo.InsertStepenObrazovanie(rs);
        }

        [HttpDelete]
        [Route("api/DeleteStepenObrazovanie/{id:int}")]
        public async Task DeleteStepenObrazovanie(int id)
        {
            StepenObrazovanieRepository repo = new StepenObrazovanieRepository(_conf.GetConnectionString("dbClenovi"));
            await repo.DeleteStepenObrazovanie(id);
        }
    }
}
