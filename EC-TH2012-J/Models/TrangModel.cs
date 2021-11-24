using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EC_TH2012_J.Models
{
    public class TrangModel
    {
        private Entities db;
        public TrangModel()
        {
            db = new Entities();
        }
        public IQueryable<Trang> GetTrang()
        {
            IQueryable<Trang> lst = db.Trangs.OrderBy(x => x.ThuTu);
            return lst;
        }
        public static List<Trang> GetAll()
        {
            Entities db = new Entities();
            List<Trang> listTrang = db.Trangs.OrderBy(x => x.ThuTu).ToList();
            return listTrang;
        }
        internal Trang FindById(string id)
        {
            return db.Trangs.Find(id);
        }

        internal void EditTrang(Trang loai)
        {
            Trang lsp = db.Trangs.Find(loai.MaTrang);
            lsp.TenTrang = loai.TenTrang;
            db.Entry(lsp).State = EntityState.Modified;
            db.SaveChanges();
        }

        internal void DeleteTrang(string id)
        {
            Trang loai = db.Trangs.Find(id);
            db.Trangs.Remove(loai);
            db.SaveChanges();
        }

        internal string ThemTrang(Trang loai)
        {
            loai.MaTrang = TaoMa();
            db.Trangs.Add(loai);
            db.SaveChanges();
            return loai.MaTrang;
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
            var temp = db.Trangs.Find(maID);
            if (temp == null)
                return true;
            return false;
        }

        internal IQueryable<Trang> SearchByName(string key)
        {
            if (string.IsNullOrEmpty(key))
                return db.Trangs;
            return db.Trangs.Where(u => u.TenTrang.Contains(key));
        }


        internal bool KiemTraTen(string p)
        {
            var temp = db.Trangs.Where(m=>m.TenTrang.Equals(p)).ToList();
            if (temp.Count == 0)
                return true;
            return false;
        }
    }
}