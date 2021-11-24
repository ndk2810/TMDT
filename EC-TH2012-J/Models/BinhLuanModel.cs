using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace EC_TH2012_J.Models
{
    public class BinhLuanModel
    {
        public BinhLuan binhLuan;
        public AspNetUser nguoiMua;
        
        public BinhLuan Binhluanmoi()
        {
            using (Entities db = new Entities())
            {
                
                db.BinhLuans.AsNoTracking();
               var result = (from p in db.BinhLuans
                              join q in db.SanPhams on p.MaSP equals q.MaSP
                              select new { 
                                q.TenSP,
                                p.NgayDang,
                                p.HoTen,
                                p.NoiDung,
                                p.Parent, 
                              }).OrderByDescending(p => p.NgayDang).First();
               BinhLuan bl = new BinhLuan();
               if (result != null)
               {
                   //bl.SanPham = result.TenSP;
                   bl.HoTen = result.HoTen;
                   bl.NgayDang = result.NgayDang;
                   bl.NoiDung = result.NoiDung;
               }

               return bl;
            }
        }

    }
}