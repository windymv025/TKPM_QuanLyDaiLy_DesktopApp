using QuanLyDaiLyMVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDaiLyMVVM.Model
{
    public class PhieuXuatHangHienThi: BaseViewModel
    {
        private PhieuDaiLy _PhieuDaiLy;
        public PhieuDaiLy PhieuDaiLy { get => _PhieuDaiLy; set { _PhieuDaiLy = value; OnPropertyChanged(); } }
        
        private DaiLy _DaiLy;
        public DaiLy DaiLy { get => _DaiLy; set { _DaiLy = value; OnPropertyChanged(); } }
        
        private PhieuXuatHang _PhieuXuatHang;
        public PhieuXuatHang PhieuXuatHang { get => _PhieuXuatHang; set { _PhieuXuatHang = value; OnPropertyChanged(); } }

        private string _TongTien;
        public string TongTien { get => _TongTien; set { _TongTien = value; OnPropertyChanged(); } }
    }
}
