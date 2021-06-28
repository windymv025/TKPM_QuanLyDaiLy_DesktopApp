using QuanLyDaiLyMVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDaiLyMVVM.ViewModel
{
    public class ThemNguonNhapViewModel : BaseViewModel
    {
        private NguonNhap _NguonNhap;
        public NguonNhap NguonNhap { get => _NguonNhap; set { _NguonNhap = value; OnPropertyChanged(); } }

        public ThemNguonNhapViewModel()
        {
            NguonNhap = new NguonNhap
            {
                HinhAnh = "Assets/image_not_available.png"
            };
        }
    }
}
