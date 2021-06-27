using MaterialDesignThemes.Wpf;
using QuanLyDaiLyMVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyDaiLyMVVM.ViewModel
{
    public class SanPhamViewModel: BaseViewModel
    {
        private ObservableCollection<SanPhamHienThi> _ListSanPhamHienThiTheotrang;
        public ObservableCollection<SanPhamHienThi> ListSanPhamHienThiTheotrang { get => _ListSanPhamHienThiTheotrang; set { _ListSanPhamHienThiTheotrang = value; OnPropertyChanged(); } }

        private ObservableCollection<SanPhamHienThi> _ListSanPham;
        public ObservableCollection<SanPhamHienThi> ListSanPham { get => _ListSanPham; set { _ListSanPham = value; OnPropertyChanged(); } }

        private ObservableCollection<SanPham> _SanPhams;
        public ObservableCollection<SanPham> SanPhams { get => _SanPhams; set { _SanPhams = value; OnPropertyChanged(); } }

        private ObservableCollection<LoaiSanPham> _LoaiSanPhams;
        public ObservableCollection<LoaiSanPham> LoaiSanPhams { get => _LoaiSanPhams; set { _LoaiSanPhams = value; OnPropertyChanged(); } }

        private ObservableCollection<NguonNhap> _NguonNhaps;
        public ObservableCollection<NguonNhap> NguonNhaps { get => _NguonNhaps; set { _NguonNhaps = value; OnPropertyChanged(); } }

        public ICommand PrevBtnCommand { get; set; }
        public ICommand NextBtnCommand { get; set; }

        private PagingInfo _PagingInfo;
        public PagingInfo PagingInfo { get => _PagingInfo; set { _PagingInfo = value; OnPropertyChanged(); } }

        public SanPhamViewModel()
        {
            loadData();
            PagingInfo = new PagingInfo(5, ListSanPham.Count);
            ListSanPhamHienThiTheotrang = loadPageHienThi(1);

            PrevBtnCommand = new RelayCommand<ListView>((p) => { return true; }, (p) => { loadPrevPage(p); });
            NextBtnCommand = new RelayCommand<ListView>((p) => { return true; }, (p) => { loadNextPage(p); });
        }

        private void loadNextPage(ListView p)
        {
            ListSanPhamHienThiTheotrang = loadPageHienThi(PagingInfo.CurrentPage + 1);
        }

        private void loadPrevPage(ListView p)
        {
            ListSanPhamHienThiTheotrang = loadPageHienThi(PagingInfo.CurrentPage - 1);
        }

        public ObservableCollection<SanPhamHienThi> loadPageHienThi(int pageNumber)
        {
            ObservableCollection<SanPhamHienThi> result = new ObservableCollection<SanPhamHienThi>();

            if (PagingInfo.TotalPage > 0)
            {
                if (pageNumber > PagingInfo.TotalPage)
                    pageNumber = 1;
                if (pageNumber < 1)
                    pageNumber = PagingInfo.TotalPage;

                PagingInfo.CurrentPage = pageNumber;

                result = new ObservableCollection<SanPhamHienThi>(ListSanPham.Skip((pageNumber - 1) * PagingInfo.ItemInPerPage).Take(PagingInfo.ItemInPerPage));
            }
            else
            {
                PagingInfo.CurrentPage = 0;
            }
            return result;
        }

        private void loadData()
        {
            using(DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                _SanPhams = new ObservableCollection<SanPham>(db.SanPhams);
                _LoaiSanPhams = new ObservableCollection<LoaiSanPham>(db.LoaiSanPhams);
                _NguonNhaps = new ObservableCollection<NguonNhap>(db.NguonNhaps);

                foreach(var sp in _SanPhams)
                {
                    if (sp.HinhAnh == null)
                    {
                        sp.HinhAnh = Path.GetFullPath("Assets/image_not_available.png");
                    }
                    else
                    {
                        sp.HinhAnh = Path.GetFullPath(sp.HinhAnh);
                    }

                }

                foreach (var l in _LoaiSanPhams)
                {
                    if (l.HinhAnh == null)
                    {
                        l.HinhAnh = Path.GetFullPath("Assets/image_not_available.png");
                    }
                    else
                    {
                        l.HinhAnh = Path.GetFullPath(l.HinhAnh);
                    }

                }

                foreach (var l in _NguonNhaps)
                {
                    if (l.HinhAnh == null)
                    {
                        l.HinhAnh = Path.GetFullPath("Assets/image_not_available.png");
                    }
                    else
                    {
                        l.HinhAnh = Path.GetFullPath(l.HinhAnh);
                    }

                }
            }

            _ListSanPham = new ObservableCollection<SanPhamHienThi>(
                from sp in _SanPhams
                join lsp in _LoaiSanPhams
                on sp.IdLoaiSanPham equals lsp.Id
                join nn in _NguonNhaps
                on sp.IdNguonNhap equals nn.Id
                select new SanPhamHienThi
                {
                    SanPham = sp,
                    LoaiSanPham = lsp,
                    NguonNhap = nn,
                    GiaBan = ConvertNumber.convertNumberDecimalToString(sp.GiaBan),
                    GiaNhap = ConvertNumber.convertNumberDecimalToString(sp.GiaNhap),
                    SoLuong = ConvertNumber.convertNumberToString(sp.SoLuong)
                });

        }
    }
}
