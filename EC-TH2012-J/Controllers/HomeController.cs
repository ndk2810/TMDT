using EC_TH2012_J.Models;
using System.Collections.Generic;
using System.Linq;
using Stripe;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Web.Configuration;
using System;

namespace EC_TH2012_J.Controllers
{
    public class HomeController : MyBaseController
    {
        private static Entities db = new Entities();

        public static List<Thanhviennhom> Ds_Group;
        public ActionResult Index()
        {
            return View();
        }
        //Xac nhan mail
        public ActionResult Confirm()
        {
            return View();
        }


        //chuyen locale
        public ActionResult ChangeLanguage(string lang)
        {
            new SiteLanguages().SetLanguage(lang);
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Thongtinnhom()
        {
            if (Ds_Group == null)
            {
                
            }
            return View(Ds_Group);
        }
        
        public ActionResult Cart()
        {
            return View(ManagerObject.getIntance().giohang);
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên,Khách hàng")]
        //Đơn hàng
        public ActionResult Xemdonhang(string maKH)
        {
            List<DonhangKHModel> temp = new List<DonhangKHModel>();
            if (maKH.Length != 0)
            {
                DonhangKHModel dh = new DonhangKHModel();
                temp = dh.Xemdonhang(maKH);
            }
            return View(temp);
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên,Khách hàng")]
        public ActionResult Huydonhang(string maDH)
        {
            DonhangKHModel dh = new DonhangKHModel();
            dh.HuyDH(maDH);
            var donhang = dh.Xemdonhang(User.Identity.GetUserId());
            return View(donhang);
        }
        public ActionResult Checkout()
        {
            ViewBag.StripePublishKey = WebConfigurationManager.AppSettings["stripePublishableKey"];
;
            if (Request.IsAuthenticated)
            {
                DonhangKHModel dh = new DonhangKHModel();
                dh.nguoiMua = dh.Xemttnguoidung(User.Identity.GetUserId());
                Giohang giohang = ManagerObject.getIntance().giohang;
                ViewBag.GioHang = giohang;
                Donhangtongquan dhtq = new Donhangtongquan()
                {
                    buyer = dh.nguoiMua.HoTen,
                    seller = dh.nguoiMua.HoTen,
                    phoneNumber = dh.nguoiMua.PhoneNumber,
                    address = dh.nguoiMua.DiaChi
                };
                return View(dhtq);
            }
            else
            {
                return RedirectToAction("Authentication", "Account", new { returnUrl = "/Home/Checkout" });
            }
        }

        [AuthLog(Roles = "Quản trị viên,Nhân viên,Khách hàng")]
        [HttpPost]
        public ActionResult Checkout(Donhangtongquan dh)
        {
            if (User.Identity.IsAuthenticated)
            {
                DonhangKHModel dhmodel = new DonhangKHModel();
                dhmodel.Luudonhang(dh, User.Identity.GetUserId(), ManagerObject.getIntance().giohang);

                ManagerObject.getIntance().giohang.Cart.Clear();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return RedirectToAction("Checkout", "Home");
            }
        }

        public ActionResult MainMenu()
        {
            MainMenuModel mnmodel = new MainMenuModel();
            var menulist = mnmodel.GetMenuList();
            return PartialView("_MainMenuPartial", menulist);
        }

        public ActionResult SPNoiBat(int? skip)
        {
            SanPhamModel sp = new SanPhamModel();
            int skipnum = (skip ?? 0);
            IQueryable<SanPham> splist = sp.SPHot();
            splist = splist.OrderBy(r => r.MaSP).Skip(skipnum).Take(4);
            if (splist.Any())
                return PartialView("_ProductTabLoadMorePartial", splist);
            else
                return null;
        }

        public ActionResult SPMoiNhap(int? skip)
        {
            SanPhamModel sp = new SanPhamModel();
            int skipnum = (skip ?? 0);
            IQueryable<SanPham> splist = sp.SPMoiNhap();
            splist = splist.OrderBy(r => r.MaSP).Skip(skipnum).Take(4);
            if (splist.Any())
                return PartialView("_ProductTabLoadMorePartial", splist);
            else
                return null;
        }

        public ActionResult SPKhuyenMai(int? skip)
        {
            SanPhamModel sp = new SanPhamModel();
            int skipnum = (skip ?? 0);
            IQueryable<SanPham> splist = sp.SPKhuyenMai();
            splist = splist.OrderBy(r => r.MaSP).Skip(skipnum).Take(4);
            if (splist.Any())
                return PartialView("_ProductTabLoadMorePartial", splist);
            else
                return null;
        }

        public ActionResult SPBanChay()
        {
            SanPhamModel sp = new SanPhamModel();
            IQueryable<SanPham> splist = sp.SPBanChay(7);      
            if (splist.Any())
                return PartialView("_BestSellerPartial", splist.ToList());
            else
                return null;
        }
        public ActionResult SPMoiXem()
        {
            return PartialView("_RecentlyViewPartial", ManagerObject.getIntance().Laydanhsachsanphammoixem());
        }
        public ActionResult Charge()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Charge(string stripeToken, string stripeEmail)
        {
            var myCharge = new ChargeCreateOptions();

            // always set these properties
            myCharge.Amount = 1500000;
            myCharge.Currency = "vnd";

            myCharge.ReceiptEmail = stripeEmail;
            myCharge.Description = "Thanh toán guitar";
            myCharge.Source = stripeToken;
            myCharge.Capture = true;

            var chargeService = new ChargeService();
            Charge stripeCharge = chargeService.Create(myCharge);

            return View();
        }
    }
}