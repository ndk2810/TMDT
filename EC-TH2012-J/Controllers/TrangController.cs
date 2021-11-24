using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using EC_TH2012_J.Models;
using System.Net;

namespace EC_TH2012_J.Controllers
{
    [AuthLog(Roles = "Quản trị viên,Nhân viên")]
    public class TrangController : Controller
    {
        //
        // GET: /Trang/
        public ActionResult Index()
        {
            return View();
        }
        //chuyen trang
        public ActionResult ChangePage(string id)
        {
            return RedirectToAction("Index", "Home");
        }
        public ActionResult EditTrang(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrangModel lm = new TrangModel();
            Trang sp = lm.FindById(id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            return View(sp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTrang([Bind(Include = "MaTrang,TenTrang,ThuTu")] Trang loai)
        {
            TrangModel spm = new TrangModel();
            if (ModelState.IsValid)
            {
                spm.EditTrang(loai);
                return RedirectToAction("Index");
            }
            return View(loai);
        }

        public ActionResult DeleteTrang(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrangModel spm = new TrangModel();
            if (spm.FindById(id) == null)
            {
                return HttpNotFound();
            }
            spm.DeleteTrang(id);
            return TimTrang(null, null);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ThemTrang([Bind(Include = "TenTrang,ThuTu")] Trang loai)
        {
            TrangModel spm = new TrangModel();
            if (ModelState.IsValid && spm.KiemTraTen(loai.TenTrang))
            {
                string maloai = spm.ThemTrang(loai);
                return View("Index");
            }
            return View("Index", loai);
        }

        [HttpPost]
        public ActionResult MultibleDel(List<string> lstdel)
        {
            if (lstdel == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            foreach (var item in lstdel)
            {
                TrangModel spm = new TrangModel();
                if (spm.FindById(item) == null)
                {
                    return HttpNotFound();
                }
                spm.DeleteTrang(item);
            }
            return TimTrang(null, null);
        }

        public ActionResult TimTrang(string key, int? page)
        {
            TrangModel spm = new TrangModel();
            ViewBag.key = key;
            return PhanTrangSP(spm.SearchByName(key), page, null);
        }

        public ActionResult PhanTrangSP(IQueryable<Trang> lst, int? page, int? pagesize)
        {
            int pageSize = (pagesize ?? 10);
            int pageNumber = (page ?? 1);
            return PartialView("TrangPartial", lst.OrderBy(m => m.TenTrang).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult kiemtra(string key)
        {
            TrangModel spm = new TrangModel();
            if (spm.KiemTraTen(key))
                return Json(true, JsonRequestBehavior.AllowGet);
            return Json(false, JsonRequestBehavior.AllowGet);
        }

    }
}