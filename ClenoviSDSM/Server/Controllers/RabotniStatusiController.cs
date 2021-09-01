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
    public class RabotniStatusiController : ControllerBase
    {
        private readonly IConfiguration _conf;
        public RabotniStatusiController(IConfiguration configuration)
        {
            this._conf = configuration;
        }

        [HttpGet]
        [Route("api/GetRabotniStatusi")]
        public async Task<IEnumerable<RabotenStatus>> GetRabotniStatusi()
        {
            RabotenStatusRepository repo = new RabotenStatusRepository(_conf.GetConnectionString("dbClenovi"));

            IEnumerable<RabotenStatus> res = await repo.GetRabotniStatusi();

            return res;
        }

        [HttpPost]
        [Route("api/UpdateRabotenStatus")]
        public async Task UpdateRabotenStatus(RabotenStatus rs)
        {
            RabotenStatusRepository repo = new RabotenStatusRepository(_conf.GetConnectionString("dbClenovi"));
            await repo.UpdateRabotenStatus(rs);
        }

        [HttpPost]
        [Route("api/InsertRabotenStatus")]
        public async Task InsertRabotenStatus(RabotenStatus rs)
        {
            RabotenStatusRepository repo = new RabotenStatusRepository(_conf.GetConnectionString("dbClenovi"));
            await repo.InsertRabotenStatus(rs);
        }

        [HttpDelete]
        [Route("api/DeleteRabotenStatus/{id:int}")]
        public async Task DeleteRabotenStatus(int id)
        {
            RabotenStatusRepository repo = new RabotenStatusRepository(_conf.GetConnectionString("dbClenovi"));
            await repo.DeleteRabotenStatus(id);
        }
    }
}
