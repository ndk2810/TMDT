
using EC_TH2012_J.Models;
using EC_TH2012_J.Models.DTO;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace EC_TH2012_J.Controllers
{
    public class ExportExcelController : Controller
    {
        private Entities db = new Entities();
        public ActionResult SanPhamView()
        {
            return View("SanPhamPartial");
        }

        public ActionResult ExportSPXls(string filename)
        {
            SanPhamModel sp = new SanPhamModel();
            List<SanPhamDTO> lstSanPham = sp.GetAllSP();
            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("DsSanPham");
                ws.Cells["A1"].LoadFromCollection(lstSanPham, true);
                // Load your collection "accounts"

                Byte[] fileBytes = pck.GetAsByteArray();
                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename="+filename+".xlsx");
                // Replace filename with your custom Excel-sheet name.

                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";
                StringWriter sw = new StringWriter();
                Response.BinaryWrite(fileBytes);
                Response.End();
            }

            return RedirectToAction("Index");
        }
    }
}