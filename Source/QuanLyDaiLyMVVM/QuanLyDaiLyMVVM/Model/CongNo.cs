using QuanLyDaiLyMVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDaiLyMVVM.Model
{
    public class CongNo: BaseViewModel
    {
        private int _IdDaiLy;
        public int IdDaiLy { get => _IdDaiLy; set { _IdDaiLy = value; OnPropertyChanged(); } }
        private string _TenDaiLy;
        public string TenDaiLy { get => _TenDaiLy; set { _TenDaiLy = value; OnPropertyChanged(); } }
        private decimal _TienNo;
        public decimal TienNo { get => _TienNo; set { _TienNo = value; OnPropertyChanged(); } }
        private decimal _TienPhatSinh;
        public decimal TienPhatSinh { get => _TienPhatSinh; set { _TienPhatSinh = value; OnPropertyChanged(); } }
        private decimal _TongTienCanThu;
        public decimal TongTienCanThu { get => _TienNo + _TienPhatSinh; set { _TongTienCanThu = value; OnPropertyChanged(); } }
    }
}
