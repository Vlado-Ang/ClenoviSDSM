using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClenoviSDSM.Shared.Models;

namespace ClenoviSDSM.Client.Services
{
    interface IExportExcelService
    {
        public byte[] GenerateExcelWorkbook(List<ClenExcel> clenovi);
    }
}
