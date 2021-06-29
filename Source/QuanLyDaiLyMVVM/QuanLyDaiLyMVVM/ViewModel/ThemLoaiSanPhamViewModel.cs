using QuanLyDaiLyMVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDaiLyMVVM.ViewModel
{
    public class ThemLoaiSanPhamViewModel: BaseViewModel
    {
        private LoaiSanPham _LoaiSanPham;
        public LoaiSanPham LoaiSanPham { get => _LoaiSanPham; set { _LoaiSanPham = value; OnPropertyChanged(); } }

        public ThemLoaiSanPhamViewModel()
        {
            LoaiSanPham = new LoaiSanPham { 
                HinhAnh = "Assets/image_not_available.png"
            };

        }
    }
}
