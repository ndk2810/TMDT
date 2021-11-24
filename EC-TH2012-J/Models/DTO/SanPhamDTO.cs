using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EC_TH2012_J.Models.DTO
{
    public class SanPhamDTO
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public string LoaiSP { get; set; }
        public Nullable<int> SoLuotXemSP { get; set; }
        public string HangSX { get; set; }
        public string XuatXu { get; set; }
        public Nullable<decimal> GiaTien { get; set; }
        public Nullable<int> SoLuong { get; set; }
    }
}