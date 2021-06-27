using QuanLyDaiLyMVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDaiLyMVVM.Model
{
    public class SanPhamTop : BaseViewModel
    {
        private int _ID;
        public int ID { get => _ID; set { _ID = value; OnPropertyChanged(); } }

        private string _SoLuong;
        public string SoLuong { get => _SoLuong; set { _SoLuong = value; OnPropertyChanged(); } }

        private int _SoLuongNum;
        public int SoLuongNum { get => _SoLuongNum; set { _SoLuongNum = value; OnPropertyChanged(); } }

        private string _HinhAnh;
        public string HinhAnh { get => _HinhAnh; set { _HinhAnh = value; OnPropertyChanged(); } }

        private string _Ten;
        public string Ten { get => _Ten; set { _Ten = value; OnPropertyChanged(); } }
    }
}
