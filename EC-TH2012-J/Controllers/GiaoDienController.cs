using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EC_TH2012_J.Models;
using System.Net;
using System.IO;
using System.Data.Entity.Validation;
namespace EC_TH2012_J.Controllers
{

    public class GiaoDienController : Controller
    {
        //
        // GET: /GiaoDien/
        public ActionResult Header()
        {
            GiaoDienModel dd = new GiaoDienModel();
            List<GiaoDien> model = dd.GetDD().ToList();
            return View(model);
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên")]
        public ActionResult General()
        {
            GiaoDienModel dd = new GiaoDienModel();
            List<GiaoDien> model = dd.GetDD().ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult EditGiaoDien(List<GiaoDien> list)
        {
            if (ModelState.IsValid)
            {
                using (Entities db = new Entities())
                {
                    try
                    {
                        foreach (var i in list)
                        {
                            var c = db.GiaoDiens.Where(a => a.Id.Equals(i.Id)).FirstOrDefault();
                            if (c != null)
                            {
                                c.ThuocTinh = i.ThuocTinh;
                                c.GiaTri = i.GiaTri;
                            }
                        }
                        db.SaveChanges();
                    }
                    catch (DbEntityValidationException e)
                    {
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        throw;
                    }
                }
                ViewBag.Message = "Successfully Updated.";
                return View("General",list);
            }
            else
            {
                ViewBag.Message = "Failed ! Please try again.";
                return View("General", list);
            }
        }
        public ActionResult SlideShowView()
        {
            KhuyenMaiModel km = new KhuyenMaiModel();
            return PartialView("SlideShowView", km.TimKhuyenMai(null, null, null).Where(m => m.NgayBatDau <= DateTime.Today && m.NgayKetThuc >= DateTime.Today));
        }

        public ActionResult SlideShowSetting()
        {
            GiaoDienModel gd = new GiaoDienModel();
            List<Link> linklist = gd.GetSlideShow().ToList();
            return View(linklist);
        }

        public ActionResult SlideShow()
        {
            Link link = new Link();
            link.Group = "SlideShow";
            return View(link);
        }
        public bool UploadAnh(HttpPostedFileBase file, string tenfile)
        {
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                var name = Path.GetExtension(file.FileName);
                // extract only the filename
                if (!Path.GetExtension(file.FileName).Equals(".png"))
                {
                    return false;
                }
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/images/logo"), tenfile + ".png");
                file.SaveAs(path);
                return true;
            }
            // redirect back to the index action to show the form once again
            return false;
        }

        public bool DeleteAnh(string filename)
        {
            string fullPath = Request.MapPath("~/images/logo/" + filename);
            if (System.IO.File.Exists(fullPath))
            {
                System.IO.File.Delete(fullPath);
                return true;
            }
            return false;
        }

    }
}