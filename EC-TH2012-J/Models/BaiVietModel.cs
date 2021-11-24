using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EC_TH2012_J.Models
{
    public class BaiVietModel
    {
        Entities db = new Entities();
        public IQueryable<BaiViet> SearchByName(string term)
        {
            IQueryable<BaiViet> lst;
            lst = db.BaiViets.Where(u => u.TieuDe.Contains(term));
            return lst ;
        }

        public IQueryable<BaiViet> AdvancedSearch(string term, string trang)
        {
            IQueryable<BaiViet> lst = db.BaiViets;
            if(!string.IsNullOrEmpty(term))
                lst = SearchByName(term);
            if(!string.IsNullOrEmpty(trang))
                lst = from p in lst where p.Trang.Equals(trang) select p;
            
            return lst;
        }
        public IQueryable<BaiViet> SearchByType(string term)
        {
            var splist = (from p in db.BaiViets where p.Trang.Equals(term) select p);
            return splist;
        }

        internal IQueryable<BaiViet> GetAll()
        {
            return db.BaiViets;
        }

        internal BaiViet FindById(string id)
        {
            return db.BaiViets.Find(id);
        }

        internal IQueryable<Trang> GetAllTrang()
        {
            return db.Trangs;
        }

        internal void EditBV(BaiViet BaiViet)
        {
            //MaSP,TenSP,LoaiSP,HangSX,XuatXu,GiaTien,MoTa,SoLuong,isnew,ishot
            BaiViet sp = db.BaiViets.Find(BaiViet.MaBV);
            sp.TieuDe = BaiViet.TieuDe;
            sp.NoiDung = BaiViet.NoiDung;
            sp.NgayTao = BaiViet.NgayTao;
            sp.Trang = BaiViet.Trang;
            db.Entry(sp).State = EntityState.Modified;
            db.SaveChanges();
        }

        internal void DeleteBV(string id)
        {
            BaiViet BaiViet = db.BaiViets.Find(id);
            db.BaiViets.Remove(BaiViet);
            db.SaveChanges();
        }

        internal string ThemBV(BaiViet BaiViet)
        {
            BaiViet.MaBV = TaoMa();
            BaiViet.TieuDe = BaiViet.TieuDe;
            BaiViet.NoiDung = BaiViet.NoiDung;
            BaiViet.NgayTao = DateTime.Now;
            BaiViet.Trang = BaiViet.Trang;
            db.BaiViets.Add(BaiViet);
            db.SaveChanges();
            return BaiViet.MaBV;
        }

        private string TaoMa()
        {
            string maID;
            Random rand = new Random();
            do
            {
                maID = "";
                for (int i = 0; i < 5; i++)
                {
                    maID += rand.Next(9);
                }
            }
            while (!KiemtraID(maID));
            return maID;
        }

        private bool KiemtraID(string maID)
        {
            using (Entities db = new Entities())
            {
                var temp = db.BaiViets.Find(maID);
                if (temp == null)
                    return true;
                return false;
            }
        }
        public BaiViet getBaiViet(string id)
        {
            var sp = (from p in db.BaiViets where (p.MaBV == id) select p).FirstOrDefault();
            return sp;
        }

    }
}