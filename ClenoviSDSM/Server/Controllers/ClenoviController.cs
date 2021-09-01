using ClenoviSDSM.Shared.Models;
using ClenoviSDSM.Server.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClenoviSDSM.Server.Controllers
{
    [Authorize]
    [ApiController]
    public class ClenoviController : ControllerBase
    {
        private readonly IConfiguration _conf;

        public ClenoviController(IConfiguration configuration)
        {
            this._conf = configuration;
        }

        [HttpGet]
        [Route("api/GetClen/{id:int}")]
        public async Task<Clen> GetClen(int id)
        {
            ClenRepository repo = new ClenRepository(_conf.GetConnectionString("dbClenovi"));

            Clen res = await repo.GetClen(id);

            return res;
        }

        [HttpGet]
        [Route("api/GetClenovi")]
        public async Task<IEnumerable<Clen>> GetClenovi()
        {
            ClenRepository repo = new ClenRepository(_conf.GetConnectionString("dbClenovi"));

            IEnumerable<Clen> res = await repo.GetClenovi();

            return res;
        }

        [HttpGet]
        [Route("api/GetRodendeni/{denovi:int}")]
        public async Task<IEnumerable<Clen>> GetRodendeni(int denovi)
        {
            ClenRepository repo = new ClenRepository(_conf.GetConnectionString("dbClenovi"));

            IEnumerable<Clen> res = await repo.GetRodendeni(denovi);

            return res;
        }

        [HttpGet]
        [Route("api/GetClenoviExcel")]
        public async Task<IEnumerable<ClenExcel>> GetClenoviExcel()
        {
            ClenRepository repo = new ClenRepository(_conf.GetConnectionString("dbClenovi"));

            IEnumerable<ClenExcel> res = await repo.GetClenoviExcel();

            return res;
        }

        [HttpGet]
        [Route("api/GetOpstini")]
        public async Task<IEnumerable<string>> GetOpstini()
        {
            ClenRepository repo = new ClenRepository(_conf.GetConnectionString("dbClenovi"));

            IEnumerable<string> res = await repo.GetOpstini();

            return res;
        }

        [HttpPost]
        [Route("api/InsertClen")]
        public async Task InsertClen(Clen c)
        {
            ClenRepository repo = new ClenRepository(_conf.GetConnectionString("dbClenovi"));
            await repo.InsertClen(c);
        }

        [HttpPost]
        [Route("api/UpdateClen")]
        public async Task UpdateClen(Clen c)
        {
            ClenRepository repo = new ClenRepository(_conf.GetConnectionString("dbClenovi"));
            await repo.UpdateClen(c);
        }

        [HttpDelete]
        [Route("api/DeleteClen/{id:int}")]
        public async Task DeleteClen(int id)
        {
            ClenRepository repo = new ClenRepository(_conf.GetConnectionString("dbClenovi"));
            await repo.DeleteClen(id);
        }
    }
}
