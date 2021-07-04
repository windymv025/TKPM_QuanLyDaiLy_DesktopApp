using QuanLyDaiLyMVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDaiLyMVVM.ViewModel
{
    public class CapNhatPhieuXuatHangViewModel: BaseViewModel
    {
        private string _TongTien;
        public string TongTien { get => _TongTien; set { _TongTien = value; OnPropertyChanged(); } }

        private string _SoLuong;
        public string SoLuong { get => _SoLuong; set { _SoLuong = value; OnPropertyChanged(); } }

        private string _GiaBan;
        public string GiaBan { get => _GiaBan; set { _GiaBan = value; OnPropertyChanged(); } }

        private DaiLy _DaiLy;
        public DaiLy DaiLy { get => _DaiLy; set { _DaiLy = value; OnPropertyChanged(); } }

        private PhieuXuatHang _PhieuXuatHang;
        public PhieuXuatHang PhieuXuatHang { get => _PhieuXuatHang; set { _PhieuXuatHang = value; OnPropertyChanged(); } }

        private SanPhamHienThi _SanPham;
        public SanPhamHienThi SanPham { get => _SanPham; set { _SanPham = value; OnPropertyChanged(); } }

        private SanPhamHienThi _SanPhamXuat;
        public SanPhamHienThi SanPhamXuat { get => _SanPhamXuat; set { _SanPhamXuat = value; OnPropertyChanged(); } }

        private PhieuXuatHangHienThi _PhieuXuatHangHienThi;
        public PhieuXuatHangHienThi PhieuXuatHangHienThi { get => _PhieuXuatHangHienThi; set { _PhieuXuatHangHienThi = value; OnPropertyChanged(); } }

        private ObservableCollection<ChiTietPhieuXuatHang> _ChiTietPhieuXuatHangs;
        public virtual ObservableCollection<ChiTietPhieuXuatHang> ChiTietPhieuXuatHangs { get => _ChiTietPhieuXuatHangs; set { _ChiTietPhieuXuatHangs = value; OnPropertyChanged(); } }
        
        private ObservableCollection<DaiLy> _DaiLys;
        public virtual ObservableCollection<DaiLy> DaiLys { get => _DaiLys; set { _DaiLys = value; OnPropertyChanged(); } }
        
        private ObservableCollection<SanPhamHienThi> _SanPhams;
        public ObservableCollection<SanPhamHienThi> SanPhams { get => _SanPhams; set { _SanPhams = value; OnPropertyChanged(); } }

        private ObservableCollection<SanPhamHienThi> _SanPhamXuats;
        public ObservableCollection<SanPhamHienThi> SanPhamXuats { get => _SanPhamXuats; set { _SanPhamXuats = value; OnPropertyChanged(); } }

        public CapNhatPhieuXuatHangViewModel()
        {
        }

        public CapNhatPhieuXuatHangViewModel(PhieuXuatHangHienThi phieuXuatHangHienThi)
        {
        }
    }
}
