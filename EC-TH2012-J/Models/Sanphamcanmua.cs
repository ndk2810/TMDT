//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EC_TH2012_J.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sanphamcanmua
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sanphamcanmua()
        {
            this.DanhsachdangkisanphamNCCs = new HashSet<DanhsachdangkisanphamNCC>();
        }
    
        public int ID { get; set; }
        public string MaSP { get; set; }
        public Nullable<int> Soluong { get; set; }
        public Nullable<System.DateTime> Ngayketthuc { get; set; }
        public Nullable<System.DateTime> Ngaydang { get; set; }
        public string Mota { get; set; }
    
        public virtual SanPham SanPham { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DanhsachdangkisanphamNCC> DanhsachdangkisanphamNCCs { get; set; }
    }
}
