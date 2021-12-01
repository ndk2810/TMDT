using EC_TH2012_J.Models;
using System;
using System.Web.Mvc;

namespace EC_TH2012_J.Controllers
{
    public class XuligiohangController : Controller
    {
        private static Entities db = new Entities();
        public ActionResult Addcart(string sp, int quantity)
        {
            try
            {
                var temp = db.SanPhams.Find(sp);
                int index = Kiemtratontai(sp);
                if(index == -1)
                {
                    Chitietgiohang tam = new Chitietgiohang();
                    tam.sanPham = temp;
                    tam.Soluong = quantity;
                    ManagerObject.getIntance().giohang.addCart(tam);
                }
                else
                {
                    ManagerObject.getIntance().giohang.getGiohang()[index].Soluong += quantity;
                }
                return PartialView("Addcart1", ManagerObject.getIntance().giohang);
            }
            catch (Exception e) {return Json("faill"); }
            
        }
        public int Kiemtratontai(string id)
        {
            for (int i = 0; i < ManagerObject.getIntance().giohang.getGiohang().Count; i++)
            {
                if (ManagerObject.getIntance().giohang.getGiohang()[i].sanPham.MaSP == id)
                    return i;
            }
            return -1;
        }
        // GET: Xuligiohang
        public ActionResult Xoagiohang(int index)
        {
            ManagerObject.getIntance().giohang.removeCart(index);
            return RedirectToAction("basicXuLiGiohang");
        }
        public ActionResult Thaydoisoluong(int index,string value)
        {
            ManagerObject.getIntance().giohang.Changequanlity(index, value);
            return RedirectToAction("basicXuLiGiohang");
        }
        
        public ActionResult basicXuLiGiohang()
        {
            return PartialView("basicXuLiGiohang", ManagerObject.getIntance().giohang);
        }
        public ActionResult UpdategiohangContent()
        {
            return PartialView("Addcart1", ManagerObject.getIntance().giohang);
        }
        public ActionResult CartTitle()
        {
            return PartialView("Addcart1",ManagerObject.getIntance().giohang);
        }
        public ActionResult CartOrder()
        {
            return PartialView("Ordercheckout", ManagerObject.getIntance().giohang);
        }
    }
}