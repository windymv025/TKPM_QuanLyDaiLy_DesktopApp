using QuanLyDaiLyMVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDaiLyMVVM.Model
{
    public class PhieuThuTienHienThi: BaseViewModel
    {
        private PhieuDaiLy _PhieuDaiLy;
        public PhieuDaiLy PhieuDaiLy { get => _PhieuDaiLy; set { _PhieuDaiLy = value; OnPropertyChanged(); } }

        private DaiLy _DaiLy;
        public DaiLy DaiLy { get => _DaiLy; set { _DaiLy = value; OnPropertyChanged(); } }

        private PhieuThuTien _PhieuThuTien;
        public PhieuThuTien PhieuThuTien { get => _PhieuThuTien; set { _PhieuThuTien = value; OnPropertyChanged(); } }

        private string _SoTienThu;
        public string SoTienThu { get => _SoTienThu; set { _SoTienThu = value; OnPropertyChanged(); } }
    }
}
