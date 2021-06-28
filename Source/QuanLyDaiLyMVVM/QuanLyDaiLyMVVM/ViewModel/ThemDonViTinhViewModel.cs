using QuanLyDaiLyMVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDaiLyMVVM.ViewModel
{
    public class ThemDonViTinhViewModel: BaseViewModel
    {
        private DonViTinh _DonViTinh;
        public DonViTinh DonViTinh { get => _DonViTinh; set { _DonViTinh = value; OnPropertyChanged(); } }

        public ThemDonViTinhViewModel()
        {
            DonViTinh = new DonViTinh();
        }
    }
}
