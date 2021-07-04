using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLyDaiLyMVVM.ViewModel;

namespace QuanLyDaiLyMVVM.Model
{
    public class SanPhamHienThi: BaseViewModel
    {
        private SanPham _SanPham;
        public SanPham SanPham { get=>_SanPham; set { _SanPham = value;  OnPropertyChanged(); } }

        private LoaiSanPham _LoaiSanPham;
        public LoaiSanPham LoaiSanPham { get => _LoaiSanPham; set { _LoaiSanPham = value; OnPropertyChanged(); } }

        private NguonNhap _NguonNhap;
        public NguonNhap NguonNhap { get => _NguonNhap; set { _NguonNhap = value; OnPropertyChanged(); } }

        private DonViTinh _DonViTinh;
        public DonViTinh DonViTinh { get => _DonViTinh; set { _DonViTinh = value; OnPropertyChanged(); } }
        
        private string _GiaNhap;
        public string GiaNhap { get => _GiaNhap; set { _GiaNhap = value; OnPropertyChanged(); } }

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
