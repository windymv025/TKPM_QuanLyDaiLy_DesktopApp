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
    
    public partial class DaiLy: BaseViewModel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DaiLy()
        {
            this.PhieuDaiLies = new HashSet<PhieuDaiLy>();
        }

        private int _Id;
        public int Id { get=>_Id; set { _Id = value; OnPropertyChanged(); } }
        private string _Ten;
        public string Ten { get => _Ten; set { _Ten = value; OnPropertyChanged(); } }
        private string _DienThoai;
        public string DienThoai { get => _DienThoai; set { _DienThoai = value; OnPropertyChanged(); } }
        private string _DiaChi;
        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }
        private System.DateTime _NgayTiepNhan;
        public System.DateTime NgayTiepNhan { get => _NgayTiepNhan; set { _NgayTiepNhan = value; OnPropertyChanged(); } }
        private string _Quan;
        public string Quan { get => _Quan; set { _Quan = value; OnPropertyChanged(); } }
        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }
        private int _IdLoaiDaiLy;
        public int IdLoaiDaiLy { get => _IdLoaiDaiLy; set { _IdLoaiDaiLy = value; OnPropertyChanged(); } }
        private bool _IsRemove;
        public bool IsRemove { get => _IsRemove; set { _IsRemove = value; OnPropertyChanged(); } }
        private string _HinhAnh;
        public string HinhAnh { get => _HinhAnh; set { _HinhAnh = value; OnPropertyChanged(); } }

        private LoaiDaiLy _LoaiDaiLy;
        public virtual LoaiDaiLy LoaiDaiLy { get=>_LoaiDaiLy; set { _LoaiDaiLy = value; OnPropertyChanged(); } }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        private ICollection<PhieuDaiLy> _PhieuDaiLies;
        public virtual ICollection<PhieuDaiLy> PhieuDaiLies { get=>_PhieuDaiLies; set { _PhieuDaiLies = value; OnPropertyChanged(); } }
    }
}
