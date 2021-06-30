using QuanLyDaiLyMVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDaiLyMVVM.Model
{
    public class DoanhSo : BaseViewModel
    {
        private int _IdDaiLy;
        public int IdDaiLy { get => _IdDaiLy; set { _IdDaiLy = value; OnPropertyChanged(); } }
        private string _TenDaiLy;
        public string TenDaiLy { get => _TenDaiLy; set { _TenDaiLy = value; OnPropertyChanged(); } }
        private decimal _TongTien;
        public decimal TongTien { get => _TongTien; set { _TongTien = value; OnPropertyChanged(); } }
        private double _Tyle;
        public double Tyle { get => _Tyle; set { _Tyle = value; OnPropertyChanged(); } }
    }
}
