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
        private int loaiSapXep = -1;
        private bool IsSXTang = true;

        private string _SearchKeyword;
        public string SearchKeyword { get => _SearchKeyword; set { _SearchKeyword = value; OnPropertyChanged(); } }

        private ObservableCollection<SanPhamHienThi> _ListSanPhamHienThiTheotrang;
        public ObservableCollection<SanPhamHienThi> ListSanPhamHienThiTheotrang { get => _ListSanPhamHienThiTheotrang; set { _ListSanPhamHienThiTheotrang = value; OnPropertyChanged(); } }

        private ObservableCollection<SanPhamHienThi> _ListSanPham;
        public ObservableCollection<SanPhamHienThi> ListSanPham { get => _ListSanPham; set { _ListSanPham = value; OnPropertyChanged(); } }

        private ObservableCollection<SanPhamHienThi> _ListSanPhamBackup;
        public ObservableCollection<SanPhamHienThi> ListSanPhamBackup { get => _ListSanPham; set { _ListSanPham = value; OnPropertyChanged(); } }

        private ObservableCollection<SanPham> _SanPhams;
        public ObservableCollection<SanPham> SanPhams { get => _SanPhams; set { _SanPhams = value; OnPropertyChanged(); } }

        private ObservableCollection<LoaiSanPham> _LoaiSanPhams;
        public ObservableCollection<LoaiSanPham> LoaiSanPhams { get => _LoaiSanPhams; set { _LoaiSanPhams = value; OnPropertyChanged(); } }

        private ObservableCollection<NguonNhap> _NguonNhaps;
        public ObservableCollection<NguonNhap> NguonNhaps { get => _NguonNhaps; set { _NguonNhaps = value; OnPropertyChanged(); } }

        private ObservableCollection<DonViTinh> _DonViTinhs;
        public ObservableCollection<DonViTinh> DonViTinhs { get => _DonViTinhs; set { _DonViTinhs = value; OnPropertyChanged(); } }

        public ICommand PrevBtnCommand { get; set; }
        public ICommand NextBtnCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand ThemCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand ThayDoiLoaiSapXepCommand { get; set; }
        public ICommand ThayDoiSapXepCommand { get; set; }
        public ICommand SearchCommand { get; set; }

        private PagingInfo _PagingInfo;
        public PagingInfo PagingInfo { get => _PagingInfo; set { _PagingInfo = value; OnPropertyChanged(); } }

        public SanPhamViewModel()
        {
            loadData();
            PagingInfo = new PagingInfo(5, ListSanPham.Count);
            ListSanPhamHienThiTheotrang = loadPageHienThi(1);

            PrevBtnCommand = new RelayCommand<ListView>((p) => { return true; }, (p) => { loadPrevPage(p); });
            NextBtnCommand = new RelayCommand<ListView>((p) => { return true; }, (p) => { loadNextPage(p); });

            SelectionChangedCommand = new RelayCommand<ListView>((p) => { return true; }, (p) => { selectionChanged(p); });
            ThemCommand = new RelayCommand<Window>((p) => { return true; }, 
                (p) => {
                    var wd = new ThemSanPhamWindow();
                    var vm = wd.DataContext as ThemSanPhamViewModel;
                    wd.ShowDialog();
                    if (vm.IsSave)
                    {
                        loadData();
                        PagingInfo = new PagingInfo(5, ListSanPham.Count);
                        ListSanPhamHienThiTheotrang = loadPageHienThi(1);
                    }
                });
            RefreshCommand = new RelayCommand<SanPhamWindow>((p) => { return true; }, (p) =>
            {
                p.cbb_SapXepTheo.SelectedIndex = -1;
                p.SanPham_textbox_search.Text = "";
                p.cbb_KieuSapXep.SelectedIndex = 0;

                loadData();
                PagingInfo = new PagingInfo(5, ListSanPham.Count);
                ListSanPhamHienThiTheotrang = loadPageHienThi(1);
            });

            
            ThayDoiLoaiSapXepCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) =>
            {
                loaiSapXep = p.SelectedIndex;
                switch (loaiSapXep)
                {
                    case 0:
                        sapXepTheoTen();
                        break;

                    case 1:
                        sapXepTheoGiaBan();
                        break;

                    case 2:
                        sapXepTheoGiaNhap();
                        break;

                    case 3:
                        sapXepTheoSoLuong();
                        break;

                    default:
                        break;
                }
            });

            ThayDoiSapXepCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) =>
            {
                IsSXTang = !IsSXTang;
                switch (loaiSapXep)
                {
                    case 0:
                        sapXepTheoTen();
                        break;

                    case 1:
                        sapXepTheoGiaBan();
                        break;

                    case 2:
                        sapXepTheoGiaNhap();
                        break;

                    case 3:
                        sapXepTheoSoLuong();
                        break;

                    default:
                        break;
                }
            });

            SearchCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
                {
                    string sql = $"select* from SanPham where freetext((Ten, MoTa), N'%{SearchKeyword}%') AND TrangThai = 1";
                    SanPhams = new ObservableCollection<SanPham>(db.SanPhams.SqlQuery(sql));

                    foreach (var sp in SanPhams)
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

                    ListSanPham = new ObservableCollection<SanPhamHienThi>(
                        from sp in SanPhams
                        join lsp in LoaiSanPhams
                        on sp.IdLoaiSanPham equals lsp.Id
                        join nn in NguonNhaps
                        on sp.IdNguonNhap equals nn.Id
                        join dv in DonViTinhs
                        on sp.IdDonViTinh equals dv.Id
                        select new SanPhamHienThi
                        {
                            SanPham = sp,
                            LoaiSanPham = lsp,
                            NguonNhap = nn,
                            GiaBan = ConvertNumber.convertNumberDecimalToString(sp.GiaBan),
                            GiaNhap = ConvertNumber.convertNumberDecimalToString(sp.GiaNhap),
                            SoLuong = ConvertNumber.convertNumberToString(sp.SoLuong),
                            DonViTinh = dv
                        });

                    PagingInfo = new PagingInfo(5, ListSanPham.Count);
                    ListSanPhamHienThiTheotrang = loadPageHienThi(1);
                }
            });
        }

        private void sapXepTheoTen()
        {
            if (IsSXTang)
            {
                ListSanPham = new ObservableCollection<SanPhamHienThi>(
                    from p in ListSanPham
                    orderby p.SanPham.Ten ascending
                    select p);
            }
            else
            {
                ListSanPham = new ObservableCollection<SanPhamHienThi>(
                   from p in ListSanPham
                   orderby p.SanPham.Ten descending
                   select p);
            }
            ListSanPhamHienThiTheotrang = loadPageHienThi(1);
        }

        private void sapXepTheoGiaBan()
        {
            if (IsSXTang)
            {
                ListSanPham = new ObservableCollection<SanPhamHienThi>(
                    from p in ListSanPham
                    orderby p.SanPham.GiaBan ascending
                    select p);
            }
            else
            {
                ListSanPham = new ObservableCollection<SanPhamHienThi>(
                   from p in ListSanPham
                   orderby p.SanPham.GiaBan descending
                   select p);
            }
            ListSanPhamHienThiTheotrang = loadPageHienThi(1);
        }

        private void sapXepTheoGiaNhap()
        {
            if (IsSXTang)
            {
                ListSanPham = new ObservableCollection<SanPhamHienThi>(
                    from p in ListSanPham
                    orderby p.SanPham.GiaNhap ascending
                    select p);
            }
            else
            {
                ListSanPham = new ObservableCollection<SanPhamHienThi>(
                   from p in ListSanPham
                   orderby p.SanPham.GiaNhap descending
                   select p);
            }
            ListSanPhamHienThiTheotrang = loadPageHienThi(1);
        }

        private void sapXepTheoSoLuong()
        {
            if (IsSXTang)
            {
                ListSanPham = new ObservableCollection<SanPhamHienThi>(
                    from p in ListSanPham
                    orderby p.SanPham.SoLuong ascending
                    select p);
            }
            else
            {
                ListSanPham = new ObservableCollection<SanPhamHienThi>(
                   from p in ListSanPham
                   orderby p.SanPham.SoLuong descending
                   select p);
            }
            ListSanPhamHienThiTheotrang = loadPageHienThi(1);
        }

        private void selectionChanged(ListView p)
        {
            if(p.SelectedIndex > -1)
            {
                var wd = new CapNhatSanPhamWindow();
                var vm = new CapNhatSanPhamViewModel(ListSanPhamHienThiTheotrang[p.SelectedIndex]);
                wd.DataContext = vm;
                wd.ShowDialog();
                if (ListSanPham.Where(sp => sp.SanPham.TrangThai == false).Count() > 0)
                {
                    int currentPage = PagingInfo.CurrentPage;
                    ListSanPham.Remove(ListSanPham.Where(sp => sp.SanPham.TrangThai == false).First());
                    PagingInfo = new PagingInfo(5, ListSanPham.Count);
                    if (PagingInfo.TotalPage >= currentPage)
                    {
                        PagingInfo.CurrentPage = currentPage;
                    }
                    else
                    {
                        PagingInfo.CurrentPage = currentPage - 1;
                    }
                    ListSanPhamHienThiTheotrang = loadPageHienThi(PagingInfo.CurrentPage);

                }
            }
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
            SearchKeyword = "";
            using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                SanPhams = new ObservableCollection<SanPham>(db.SanPhams.Where(sp=> sp.TrangThai == true));
                LoaiSanPhams = new ObservableCollection<LoaiSanPham>(db.LoaiSanPhams);
                NguonNhaps = new ObservableCollection<NguonNhap>(db.NguonNhaps);
                DonViTinhs = new ObservableCollection<DonViTinh>(db.DonViTinhs);

                foreach(var sp in SanPhams)
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

                foreach (var l in LoaiSanPhams)
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

                foreach (var l in NguonNhaps)
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

            ListSanPham = new ObservableCollection<SanPhamHienThi>(
                from sp in SanPhams
                join lsp in LoaiSanPhams
                on sp.IdLoaiSanPham equals lsp.Id
                join nn in NguonNhaps
                on sp.IdNguonNhap equals nn.Id
                join dv in DonViTinhs
                on sp.IdDonViTinh equals dv.Id
                select new SanPhamHienThi
                {
                    SanPham = sp,
                    LoaiSanPham = lsp,
                    NguonNhap = nn,
                    GiaBan = ConvertNumber.convertNumberDecimalToString(sp.GiaBan),
                    GiaNhap = ConvertNumber.convertNumberDecimalToString(sp.GiaNhap),
                    SoLuong = ConvertNumber.convertNumberToString(sp.SoLuong),
                    DonViTinh = dv
                });

            ListSanPhamBackup = new ObservableCollection<SanPhamHienThi>(
                from sp in SanPhams
                join lsp in LoaiSanPhams
                on sp.IdLoaiSanPham equals lsp.Id
                join nn in NguonNhaps
                on sp.IdNguonNhap equals nn.Id
                join dv in DonViTinhs
                on sp.IdDonViTinh equals dv.Id
                select new SanPhamHienThi
                {
                    SanPham = sp,
                    LoaiSanPham = lsp,
                    NguonNhap = nn,
                    GiaBan = ConvertNumber.convertNumberDecimalToString(sp.GiaBan),
                    GiaNhap = ConvertNumber.convertNumberDecimalToString(sp.GiaNhap),
                    SoLuong = ConvertNumber.convertNumberToString(sp.SoLuong),
                    DonViTinh = dv
                });

        }
    }
}
