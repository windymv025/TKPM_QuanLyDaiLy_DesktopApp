using QuanLyDaiLyMVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyDaiLyMVVM.ViewModel
{
    public class ThemSanPhamViewModel: BaseViewModel
    {
        private SanPham _SanPham;
        public SanPham SanPham { get => _SanPham; set { _SanPham = value; OnPropertyChanged(); } }

        private ObservableCollection<HinhAnhSanPham> _HinhAnhSanPhams;
        public ObservableCollection<HinhAnhSanPham> HinhAnhSanPhams { get => _HinhAnhSanPhams; set { _HinhAnhSanPhams = value; OnPropertyChanged(); } }

        private ObservableCollection<LoaiSanPham> loaiSanPhams;
        public ObservableCollection<LoaiSanPham> LoaiSanPhams { get => loaiSanPhams; set { loaiSanPhams = value; OnPropertyChanged(); } }

        private ObservableCollection<NguonNhap> nguonNhaps;
        public ObservableCollection<NguonNhap> NguonNhaps { get => nguonNhaps; set { nguonNhaps = value; OnPropertyChanged(); } }

        private ObservableCollection<DonViTinh> _DonViTinhs;
        public ObservableCollection<DonViTinh> DonViTinhs { get => _DonViTinhs; set { _DonViTinhs = value; OnPropertyChanged(); } }

        public ICommand ThemDonViCommand { get; set; }
        public ICommand ThemNguonNhapCommand { get; set; }
        public ICommand ThemLoaiSanPhamCommand { get; set; }

        public ThemSanPhamViewModel()
        {
            SanPham = new SanPham { 
                HinhAnh = "Assets/image_not_available.png"
            };
            loadDataList();

            ThemDonViCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { new ThemDonViTinhWindow().ShowDialog(); });
            ThemNguonNhapCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { new ThemNguonNhapWindow().ShowDialog(); });
            ThemLoaiSanPhamCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { new ThemLoaiSanPhamWindow().ShowDialog(); });
        }

        private void loadDataList()
        {
            using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                LoaiSanPhams = new ObservableCollection<LoaiSanPham>(db.LoaiSanPhams);
                NguonNhaps = new ObservableCollection<NguonNhap>(db.NguonNhaps);
                DonViTinhs = new ObservableCollection<DonViTinh>(db.DonViTinhs);
            }

            foreach (var i in LoaiSanPhams)
            {
                if (i.HinhAnh != null)
                {
                    i.HinhAnh = Path.GetFullPath(i.HinhAnh);
                }
                else
                {
                    i.HinhAnh = Path.GetFullPath("Assets/image_not_available.png");
                }
            }

            foreach (var i in NguonNhaps)
            {
                if (i.HinhAnh != null)
                {
                    i.HinhAnh = Path.GetFullPath(i.HinhAnh);
                }
                else
                {
                    i.HinhAnh = Path.GetFullPath("Assets/image_not_available.png");
                }
            }


        }
    }
}
