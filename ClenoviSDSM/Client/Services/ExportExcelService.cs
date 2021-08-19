using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClenoviSDSM.Shared.Models;
using OfficeOpenXml;
using System.Globalization;

namespace ClenoviSDSM.Client.Services
{
    public class ExportExcelService : IExportExcelService
    {
        public byte[] GenerateExcelWorkbook(List<ClenExcel> clenoviExcel)
        {
            var stream = new MemoryStream();
            using (var package = new ExcelPackage(stream))
            {
                var workSheet = package.Workbook.Worksheets.Add("Clenovi");

                workSheet.Cells.LoadFromCollection(clenoviExcel, true);
                workSheet.Column(1).Width = 15;
                workSheet.Column(2).Width = 18;
                workSheet.Column(3).Width = 16;
                workSheet.Column(4).Width = 17;
                workSheet.Column(5).Width = 15;
                workSheet.Column(6).Width = 13;
                workSheet.Column(7).Width = 35;
                workSheet.Column(8).Width = 25;
                workSheet.Column(9).Width = 70;
                workSheet.Column(10).Width = 25;
                workSheet.Column(11).Width = 70;
                workSheet.Column(12).Width = 35;
                workSheet.Column(13).Width = 17;

                workSheet.Column(5).Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
                workSheet.Column(9).Style.WrapText = true;

                return package.GetAsByteArray();
            }
        }
    }
}
