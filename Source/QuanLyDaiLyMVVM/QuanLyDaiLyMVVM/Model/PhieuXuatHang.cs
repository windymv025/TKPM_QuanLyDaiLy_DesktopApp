//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QuanLyDaiLyMVVM.Model
{
    using QuanLyDaiLyMVVM.ViewModel;
    using System;
    using System.Collections.Generic;
    
    public partial class PhieuXuatHang: BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PhieuXuatHang()
        {
            this.ChiTietPhieuXuatHangs = new HashSet<ChiTietPhieuXuatHang>();
        }

        private int _IdPhieuDaiLy;
        public int IdPhieuDaiLy { get => _IdPhieuDaiLy; set { _IdPhieuDaiLy = value; OnPropertyChanged(); } }

        private decimal _TongTien;
        public decimal TongTien { get => _TongTien; set { _TongTien = value; OnPropertyChanged(); } }

        private System.DateTime _NgayLapPhieu;
        public System.DateTime NgayLapPhieu { get => _NgayLapPhieu; set { _NgayLapPhieu = value; OnPropertyChanged(); } }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

        private ICollection<ChiTietPhieuXuatHang> _ChiTietPhieuXuatHangs;
        public virtual ICollection<ChiTietPhieuXuatHang> ChiTietPhieuXuatHangs { get => _ChiTietPhieuXuatHangs; set { _ChiTietPhieuXuatHangs = value; OnPropertyChanged(); } }

        private PhieuDaiLy _PhieuDaiLy;
        public virtual PhieuDaiLy PhieuDaiLy { get => _PhieuDaiLy; set { _PhieuDaiLy = value; OnPropertyChanged(); } }
    }
}
