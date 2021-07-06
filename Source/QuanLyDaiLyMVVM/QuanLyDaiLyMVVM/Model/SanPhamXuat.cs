using QuanLyDaiLyMVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDaiLyMVVM.Model
{
    public class SanPhamXuat : BaseViewModel
    {
        private int _STT;
        public int STT { get => _STT; set { _STT = value; OnPropertyChanged(); } }

        private SanPham _SanPham;
        public SanPham SanPham { get => _SanPham; set { _SanPham = value; OnPropertyChanged(); } }

        private DonViTinh _DonViTinh;
        public DonViTinh DonViTinh { get => _DonViTinh; set { _DonViTinh = value; OnPropertyChanged(); } }

        private ChiTietPhieuXuatHang _ChiTietPhieuXuatHang;
        public ChiTietPhieuXuatHang ChiTietPhieuXuatHang { get => _ChiTietPhieuXuatHang; set { _ChiTietPhieuXuatHang = value; OnPropertyChanged(); } }

        private string _GiaBan;
        public string GiaBan { get => _GiaBan; set { _GiaBan = value; OnPropertyChanged(); } }

        private string _SoLuong;
        public string SoLuong { get => _SoLuong; set { _SoLuong = value; OnPropertyChanged(); } }

        private string _ThanhTien;
        public string ThanhTien { get => _ThanhTien; set { _ThanhTien = value; OnPropertyChanged(); } }

        private decimal _ThanhTienNum;
        public decimal ThanhTienNum { get => _ThanhTienNum; set { _ThanhTienNum = value; OnPropertyChanged(); } }
    }
}
