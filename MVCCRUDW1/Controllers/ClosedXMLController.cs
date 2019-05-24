using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCCRUDW1.Controllers
{
    public class ClosedXMLController : Controller
    {
        // GET: ClosedXML
        public ActionResult DataExport(IQueryable data, string Sheet)
        {
            //ClosedXML
            using (XLWorkbook wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add(Sheet, 1);
                ws.Cell(1, 1).InsertData(data);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    wb.SaveAs(memoryStream);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    return this.File(memoryStream.ToArray(), "application/vnd.ms-excel", Sheet + ".xlsx");
                }
            }
        }
    }
}