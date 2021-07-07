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
    public class PhieuXuatHangViewModel: BaseViewModel
    {
        private int loaiSapXep = -1;
        private bool IsSXTang = true;
        private bool IsNgayLapPhieu = false;

        private PhieuXuatHangHienThi _SelectPhieuXuatHang;
        public PhieuXuatHangHienThi SelectPhieuXuatHang { get => _SelectPhieuXuatHang; set { _SelectPhieuXuatHang = value; OnPropertyChanged(); } }

        private ObservableCollection<PhieuXuatHangHienThi> _PhieuXuatHangs;
        public ObservableCollection<PhieuXuatHangHienThi> PhieuXuatHangs { get => _PhieuXuatHangs; set { _PhieuXuatHangs = value; OnPropertyChanged(); } }
        
        private ObservableCollection<PhieuXuatHangHienThi> _PhieuXuatHangBackups;
        public ObservableCollection<PhieuXuatHangHienThi> PhieuXuatHangBackups { get => _PhieuXuatHangBackups; set { _PhieuXuatHangBackups = value; OnPropertyChanged(); } }

        private PagingInfo _PagingInfo;
        public PagingInfo PagingInfo { get => _PagingInfo; set { _PagingInfo = value; OnPropertyChanged(); } }

        private ObservableCollection<PhieuXuatHangHienThi> _ListHienThiTheotrang;
        public ObservableCollection<PhieuXuatHangHienThi> ListHienThiTheotrang { get => _ListHienThiTheotrang; set { _ListHienThiTheotrang = value; OnPropertyChanged(); } }

        public ICommand PrevBtnCommand { get; set; }
        public ICommand NextBtnCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand ThayDoiLoaiTimKiemCommand { get; set; }
        public ICommand ThayDoiLoaiSapXepCommand { get; set; }
        public ICommand ThayDoiSapXepCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand KeySearchCommand { get; set; }
        public ICommand DateSearchCommand { get; set; }

        public PhieuXuatHangViewModel()
        {
            loadData();
            PrevBtnCommand = new RelayCommand<ListView>((p) => { return true; }, (p) => { loadPrevPage(p); });
            NextBtnCommand = new RelayCommand<ListView>((p) => { return true; }, (p) => { loadNextPage(p); });

            RefreshCommand = new RelayCommand<PhieuXuatHangWindow>((p) => { return true; }, (p) =>
            {
                p.cbb_LoaiTimKiem.SelectedIndex = 0;
                p.cbb_SapXepTheo.SelectedIndex = -1;
                p.textbox_search.Text = "";
                p.datePicker_Search.SelectedDate = null;
                p.cbb_KieuSapXep.SelectedIndex = 0;
                p.lv_phieuXuatHang.SelectedIndex = -1;
                loadData();
            });

            ThayDoiLoaiTimKiemCommand = new RelayCommand<PhieuXuatHangWindow>((p) => { return true; }, (p) =>
            {
                if(!IsNgayLapPhieu)
                {
                    p.txtSearch.Visibility = Visibility.Collapsed;
                    p.datePickerSearch.Visibility = Visibility.Visible;
                    IsNgayLapPhieu = true;
                }
                else
                {
                    p.datePickerSearch.Visibility = Visibility.Collapsed;
                    p.txtSearch.Visibility = Visibility.Visible;
                    IsNgayLapPhieu = false;
                }
            });

            ThayDoiLoaiSapXepCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) =>
            {
                loaiSapXep = p.SelectedIndex;
                switch (loaiSapXep)
                {
                    case 0:
                        sapXepTheoTenDaiLy();
                        break;

                    case 1:
                        sapXepTheoNgayLapPhieu();
                        break;

                    case 2:
                        sapXepTheoTongTien();
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
                        sapXepTheoTenDaiLy();
                        break;

                    case 1:
                        sapXepTheoNgayLapPhieu();
                        break;

                    case 2:
                        sapXepTheoTongTien();
                        break;

                    default:
                        break;
                }
            });

            AddCommand = new RelayCommand<Button>((p) =>
            {
                return true;
            }, (p) =>
            {
                var wd = new ThemPhieuXuatHangWindow();
                var vm = new ThemPhieuXuatHangViewModel();

                wd.DataContext = vm;
                wd.ShowDialog();
                loadData();
            });

            SelectionChangedCommand = new RelayCommand<PhieuXuatHangWindow>((p) =>
            {
                return true;
            }, (p) =>
            {
                if (p.lv_phieuXuatHang.SelectedIndex > -1)
                {
                    var wd = new CapNhatPhieuXuatHangWindow();
                    var vm = new CapNhatPhieuXuatHangViewModel(SelectPhieuXuatHang);
                    wd.DataContext = vm;
                    wd.ShowDialog();

                    p.cbb_LoaiTimKiem.SelectedIndex = 0;
                    p.cbb_SapXepTheo.SelectedIndex = -1;
                    p.textbox_search.Text = "";
                    p.datePicker_Search.SelectedDate = null;
                    p.cbb_KieuSapXep.SelectedIndex = 0;
                    p.lv_phieuXuatHang.SelectedIndex = -1;
                    loadData();
                }
            });


            KeySearchCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                string keySearch = p.Text.Trim();

                using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
                {
                    if (keySearch != "")
                    {
                        string sql = $"select* from DaiLy where freetext((Ten,DienThoai, DiaChi, Email, Quan), N'%{keySearch}%')";
                        var listDaiLy = db.DaiLies.SqlQuery(sql);

                        PhieuXuatHangs = new ObservableCollection<PhieuXuatHangHienThi>(
                            PhieuXuatHangBackups.Where(i => listDaiLy.Where(dl => dl.Id == i.DaiLy.Id).Count() > 0)
                            );
                        PagingInfo = new PagingInfo(5, PhieuXuatHangs.Count);

                        ListHienThiTheotrang = loadPageHienThi(1);
                    }
                }
            });

            DateSearchCommand = new RelayCommand<DatePicker>((p) => { return true; }, (p) =>
            {
                using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
                {
                    PhieuXuatHangs = new ObservableCollection<PhieuXuatHangHienThi>(
                        _PhieuXuatHangBackups.Where(i => i.PhieuXuatHang.NgayLapPhieu.Date == p.SelectedDate)
                        );
                    PagingInfo = new PagingInfo(5, PhieuXuatHangs.Count);

                    ListHienThiTheotrang = loadPageHienThi(1);
                }
            });

        }

        private void sapXepTheoTongTien()
        {
            if (IsSXTang)
            {
                PhieuXuatHangs = new ObservableCollection<PhieuXuatHangHienThi>(
                    from p in PhieuXuatHangs
                    orderby p.PhieuXuatHang.TongTien ascending
                    select p);
            }
            else
            {
                PhieuXuatHangs = new ObservableCollection<PhieuXuatHangHienThi>(
                    from p in PhieuXuatHangs
                    orderby p.PhieuXuatHang.TongTien descending
                    select p);
            }
            ListHienThiTheotrang = loadPageHienThi(1);
        }

        private void sapXepTheoNgayLapPhieu()
        {
            if (IsSXTang)
            {
                PhieuXuatHangs = new ObservableCollection<PhieuXuatHangHienThi>(
                    from p in PhieuXuatHangs
                    orderby p.PhieuXuatHang.NgayLapPhieu ascending
                    select p);
            }
            else
            {
                PhieuXuatHangs = new ObservableCollection<PhieuXuatHangHienThi>(
                    from p in PhieuXuatHangs
                    orderby p.PhieuXuatHang.NgayLapPhieu descending
                    select p);
            }
            ListHienThiTheotrang = loadPageHienThi(1);
        }

        private void sapXepTheoTenDaiLy()
        {
            if (IsSXTang)
            {
                PhieuXuatHangs = new ObservableCollection<PhieuXuatHangHienThi>(
                    from p in PhieuXuatHangs
                    orderby p.DaiLy.Ten ascending
                    select p);
            }
            else
            {
                PhieuXuatHangs = new ObservableCollection<PhieuXuatHangHienThi>(
                    from p in PhieuXuatHangs
                    orderby p.DaiLy.Ten descending
                    select p);
            }
            ListHienThiTheotrang = loadPageHienThi(1);
        }

        private void loadData()
        {
            using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                PhieuXuatHangs = new ObservableCollection<PhieuXuatHangHienThi>(
                    from p in db.PhieuXuatHangs
                    select new PhieuXuatHangHienThi
                    {
                        PhieuDaiLy = p.PhieuDaiLy,
                        PhieuXuatHang = p,
                        DaiLy = p.PhieuDaiLy.DaiLy
                    });

                foreach (var i in PhieuXuatHangs)
                {
                    if (i.DaiLy.HinhAnh == null)
                    {
                        i.DaiLy.HinhAnh = Path.GetFullPath("Assets/image_not_available.png");
                    }
                    else
                    {
                        i.DaiLy.HinhAnh = Path.GetFullPath(i.DaiLy.HinhAnh);
                    }
                    i.TongTien = ConvertNumber.convertNumberDecimalToString(i.PhieuXuatHang.TongTien);
                }
            }
            PhieuXuatHangBackups = new ObservableCollection<PhieuXuatHangHienThi>(PhieuXuatHangs);
            PagingInfo = new PagingInfo(5, PhieuXuatHangs.Count);
            ListHienThiTheotrang = loadPageHienThi(1);
        }

        private void loadNextPage(ListView p)
        {
            ListHienThiTheotrang = loadPageHienThi(PagingInfo.CurrentPage + 1);
        }

        private void loadPrevPage(ListView p)
        {
            ListHienThiTheotrang = loadPageHienThi(PagingInfo.CurrentPage - 1);
        }

        public ObservableCollection<PhieuXuatHangHienThi> loadPageHienThi(int pageNumber)
        {
            ObservableCollection<PhieuXuatHangHienThi> result = new ObservableCollection<PhieuXuatHangHienThi>();

            if (PagingInfo.TotalPage > 0)
            {
                if (pageNumber > PagingInfo.TotalPage)
                    pageNumber = 1;
                if (pageNumber < 1)
                    pageNumber = PagingInfo.TotalPage;

                PagingInfo.CurrentPage = pageNumber;

                result = new ObservableCollection<PhieuXuatHangHienThi>(PhieuXuatHangs.Skip((pageNumber - 1) * PagingInfo.ItemInPerPage).Take(PagingInfo.ItemInPerPage));
            }
            else
            {
                PagingInfo.CurrentPage = 0;
            }
            return result;
        }
    }
}
