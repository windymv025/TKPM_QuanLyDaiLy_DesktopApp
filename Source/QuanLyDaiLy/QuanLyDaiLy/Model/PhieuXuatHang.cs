//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyDaiLy.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class PhieuXuatHang
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuXuatHang()
        {
            this.ChiTietPhieuXuatHangs = new HashSet<ChiTietPhieuXuatHang>();
        }
    
        public int ID { get; set; }
        public string DonViTinh { get; set; }
        public decimal TongTien { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChiTietPhieuXuatHang> ChiTietPhieuXuatHangs { get; set; }
        public virtual PhieuDaiLy PhieuDaiLy { get; set; }
    }
}
