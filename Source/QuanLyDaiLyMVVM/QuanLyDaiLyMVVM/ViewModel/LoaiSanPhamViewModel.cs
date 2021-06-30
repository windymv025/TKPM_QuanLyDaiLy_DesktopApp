using QuanLyDaiLyMVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDaiLyMVVM.ViewModel
{
    public class LoaiSanPhamViewModel: BaseViewModel
    {
        private LoaiSanPham _LoaiSanPham;
        public LoaiSanPham LoaiSanPham { get => _LoaiSanPham; set { _LoaiSanPham = value; OnPropertyChanged(); } }

        public LoaiSanPhamViewModel()
        {
            LoaiSanPham = new LoaiSanPham { 
                HinhAnh = "Assets/image_not_available.png"
            };

        }
    }
}
