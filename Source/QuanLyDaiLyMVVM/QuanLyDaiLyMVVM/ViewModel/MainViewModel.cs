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
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace QuanLyDaiLyMVVM.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public bool IsShow = false;
        private int loaiTimKiem = 0;

        private string _DoanhThu;
        public string DoanhThu { get => _DoanhThu; set { _DoanhThu = value; OnPropertyChanged(); } }
        
        private string _LoiNhuan;
        public string LoiNhuan { get => _LoiNhuan; set { _LoiNhuan = value; OnPropertyChanged(); } }

        private string _TongCongNo;
        public string TongCongNo { get => _TongCongNo; set { _TongCongNo = value; OnPropertyChanged(); } }

        private string _SanPhamTonKho;
        public string SanPhamTonKho { get => _SanPhamTonKho; set { _SanPhamTonKho = value; OnPropertyChanged(); } }

        private string _TongSoDaiLy; 
        public string TongSoDaiLy { get => _TongSoDaiLy; set { _TongSoDaiLy = value; OnPropertyChanged(); } }

        private DateTime _NgayBatDau;
        public DateTime NgayBatDau { get => _NgayBatDau; set { _NgayBatDau = value; OnPropertyChanged(); } }
        
        private DateTime _NgayKetThuc;
        public DateTime NgayKetThuc { get => _NgayKetThuc; set { _NgayKetThuc = value; OnPropertyChanged(); } }

        private SanPhamTop _SanPhamTop1;
        public SanPhamTop SanPhamTop1 { get => _SanPhamTop1; set { _SanPhamTop1 = value; OnPropertyChanged();} }

        private SanPhamTop _SanPhamTop2;
        public SanPhamTop SanPhamTop2 { get => _SanPhamTop2; set { _SanPhamTop2 = value; OnPropertyChanged(); } }

        private SanPhamTop _SanPhamTop3;
        public SanPhamTop SanPhamTop3 { get => _SanPhamTop3; set { _SanPhamTop3 = value; OnPropertyChanged(); } }

        private DaiLyTop _DaiLyTop1;
        public DaiLyTop DaiLyTop1 { get => _DaiLyTop1; set { _DaiLyTop1 = value; OnPropertyChanged(); } }

        private DaiLyTop _DaiLyTop2;
        public DaiLyTop DaiLyTop2 { get => _DaiLyTop2; set { _DaiLyTop2 = value; OnPropertyChanged(); } }

        private DaiLyTop _DaiLyTop3;
        public DaiLyTop DaiLyTop3 { get => _DaiLyTop3; set { _DaiLyTop3 = value; OnPropertyChanged(); } }

        private ObservableCollection<DaiLy> _ListDaiLy;
        public ObservableCollection<DaiLy> ListDaiLy { get => _ListDaiLy; set { _ListDaiLy = value; OnPropertyChanged(); } }

        private ObservableCollection<SanPham> _ListSanPham;
        public ObservableCollection<SanPham> ListSanPham { get => _ListSanPham; set { _ListSanPham = value; OnPropertyChanged(); } }
        
        private ObservableCollection<NguonNhap> _ListNguonNhap;
        public ObservableCollection<NguonNhap> ListNguonNhap { get => _ListNguonNhap; set { _ListNguonNhap = value; OnPropertyChanged(); } }

        public bool Isloaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand LogoutCommand { get; set; }
        public ICommand DaiLyShowCommand { get; set; }
        public ICommand SanPhamShowCommand { get; set; }
        public ICommand PhieuThuTienShowCommand { get; set; }
        public ICommand PhieuXuatHangShowCommand { get; set; }
        public ICommand RevenueShowCommand { get; set; }
        public ICommand AccountShowCommand { get; set; }
        public ICommand QuyDinhShowCommand { get; set; }
        public ICommand ContactShowCommand { get; set; }
        public ICommand NguonNhapShowCommand { get; set; }
        public ICommand LoaiSanPhamShowCommand { get; set; }
        public ICommand LocTheoNgayCommand { get; set; }
        public ICommand TimKiemCommand { get; set; }
        public ICommand SelectedSearchCommand { get; set; }
        public ICommand LoaiTimKiemCommand { get; set; }

        public ICommand SanPhamTop1_MouseDownCommand { get; set; }
        public ICommand SanPhamTop2_MouseDownCommand { get; set; }
        public ICommand SanPhamTop3_MouseDownCommand { get; set; }

        public ICommand DaiLyTop1_MouseDownCommand { get; set; }
        public ICommand DaiLyTop2_MouseDownCommand { get; set; }
        public ICommand DaiLyTop3_MouseDownCommand { get; set; }

        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                Isloaded = true;

                if (p == null)
                    return;
                p.Hide();

                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                if (loginWindow.DataContext == null)
                    return;
                var loginVM = loginWindow.DataContext as LoginViewModel;

                if (loginVM.IsLogin)
                {
                    p.Show();
                }
                else
                {
                    p.Close();
                }
            });
            loadAllData();

            LogoutCommand = new RelayCommand<MainWindow>((p) => { if (p == null) return false; else { return true; } }, (p) => {
                MainWindow mainWindow = new MainWindow();
                p.Hide();
                mainWindow.Show();
                p.Close();
            });

            DaiLyShowCommand = new RelayCommand<ToggleButton>((p) => { return true; }, (p) => {
                p.IsChecked = false;
                DaiLyWindow wd = new DaiLyWindow();
                wd.ShowDialog();
                loadAllData();
            });

            NguonNhapShowCommand = new RelayCommand<ToggleButton>((p) => { return true; }, (p) => {
                p.IsChecked = false;
                NguonNhapWindow wd = new NguonNhapWindow();
                wd.ShowDialog();
                loadAllData();
            });

            LoaiSanPhamShowCommand = new RelayCommand<ToggleButton>((p) => { return true; }, (p) => {
                p.IsChecked = false;
                LoaiSanPhamWindow wd = new LoaiSanPhamWindow();
                wd.ShowDialog();
                loadAllData();
            });

            SanPhamShowCommand = new RelayCommand<ToggleButton>((p) => { return true; }, (p) => {
                p.IsChecked = false;
                SanPhamWindow wd = new SanPhamWindow();
                wd.ShowDialog();
                loadAllData();
            });

            PhieuThuTienShowCommand = new RelayCommand<ToggleButton>((p) => { return true; }, (p) => {
                p.IsChecked = false;
                PhieuThuTienWindow wd = new PhieuThuTienWindow();
                wd.ShowDialog();
                loadAllData();
            });

            PhieuXuatHangShowCommand = new RelayCommand<ToggleButton>((p) => { return true; }, (p) => {
                p.IsChecked = false;
                PhieuXuatHangWindow wd = new PhieuXuatHangWindow();
                wd.ShowDialog();
                loadAllData();
            });

            RevenueShowCommand = new RelayCommand<ToggleButton>((p) => { return true; }, (p) => {
                p.IsChecked = false;
                BaoCaoWindow wd = new BaoCaoWindow();
                BaoCaoViewModel vm = new BaoCaoViewModel();
                wd.DataContext = vm;
                wd.ShowDialog();
                loadAllData();
            });

            AccountShowCommand = new RelayCommand<ToggleButton>((p) => { return true; }, (p) => {
                p.IsChecked = false;
                NhanVienWindow wd = new NhanVienWindow();
                ProfileViewModel vm = new ProfileViewModel();
                using (var db = new DBQuanLyCacDaiLyEntities())
                {
                    var role = db.NhanViens.Where(x => x.Id == LoginViewModel.IdUser).FirstOrDefault().VaiTro;
                    if (role != 1)
                    {
                        vm.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        vm.Visibility = Visibility.Visible;
                    }
                }
                wd.DataContext = vm;
                wd.ShowDialog();
            });

            QuyDinhShowCommand = new RelayCommand<ToggleButton>((p) => { return true; }, (p) => {
                p.IsChecked = false;
                QuyDinhWindow wd = new QuyDinhWindow();
                wd.ShowDialog();
                loadAllData();
            });

            ContactShowCommand = new RelayCommand<ToggleButton>((p) => { return true; }, (p) => {
                p.IsChecked = false;
                LienHeWindow wd = new LienHeWindow();
                wd.ShowDialog();
            });

            LocTheoNgayCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { 
                LocThongkeTheoNgay(); 
            });

            SanPhamTop1_MouseDownCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                SanPhamTop1_MouseDown(p);
            });

            SanPhamTop2_MouseDownCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                SanPhamTop2_MouseDown(p);
            });

            SanPhamTop3_MouseDownCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                SanPhamTop3_MouseDown(p);
            });

            DaiLyTop1_MouseDownCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                DaiLyTop1_MouseDown(p);
            });
            DaiLyTop2_MouseDownCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { 
                DaiLyTop2_MouseDown(p);
            });
            DaiLyTop3_MouseDownCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { 
                DaiLyTop3_MouseDown(p);
            });

            TimKiemCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) => {
                string keySearch = p.Text;
                string sql = "";
                using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
                {
                    switch (loaiTimKiem)
                    {
                        case 0:
                            sql = $"select* from DaiLy where freetext((Ten,DienThoai, DiaChi, Email, Quan), N'%{keySearch}%')  AND IsRemove = 0";
                            ListDaiLy = new ObservableCollection<DaiLy>(db.DaiLies.SqlQuery(sql));
                            p.ItemsSource = ListDaiLy;
                            break;
                        case 1:
                            sql = $"select* from SanPham where freetext((Ten, MoTa), N'%{keySearch}%') AND TrangThai = 1";
                            ListSanPham = new ObservableCollection<SanPham>(db.SanPhams.SqlQuery(sql));
                            p.ItemsSource = ListSanPham;
                            break;
                        case 2:
                            sql = $"select* from NguonNhap where freetext((Ten, DiaChi, SoDienThoai), N'%{keySearch}%')";
                            ListNguonNhap = new ObservableCollection<NguonNhap>(db.NguonNhaps.SqlQuery(sql));
                            p.ItemsSource = ListNguonNhap;
                            break;
                        default:
                            break;
                    }
                    loadHinhAnh();
                }
            });

            SelectedSearchCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) => {
                if (p.SelectedIndex > -1)
                {
                    using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
                    {
                        switch (loaiTimKiem)
                        {
                            case 0:
                                var daiLyWD = new CapNhatDaiLyWindow();
                                DaiLyViewModel vm = new DaiLyViewModel();

                                DaiLy dl = db.DaiLies.Where(d => d.Id == DaiLyTop1.ID && d.IsRemove == false).FirstOrDefault();
                                vm.Ten = dl.Ten;
                                vm.Quan = dl.Quan;
                                vm.DiaChi = dl.DiaChi;
                                vm.DienThoai = dl.DienThoai;
                                vm.Email = dl.Email;
                                vm.HinhAnh = Path.GetFullPath(dl.HinhAnh);
                                vm.NgayTiepNhan = dl.NgayTiepNhan;

                                vm.SelectedLoaiDaiLy = vm.LoaiDaiLy.Where(x => x.Id == (dl.IdLoaiDaiLy)).SingleOrDefault();
                                vm.SelectedItem = new DaiLy()
                                {
                                    Id = dl.Id,
                                    Ten = dl.Ten,
                                    DienThoai = dl.DienThoai,
                                    DiaChi = dl.DiaChi,
                                    NgayTiepNhan = dl.NgayTiepNhan,
                                    Quan = dl.Quan,
                                    Email = dl.Email,
                                    HinhAnh = Path.GetFullPath(dl.HinhAnh),
                                    IdLoaiDaiLy = dl.IdLoaiDaiLy
                                };
                                daiLyWD.DataContext = vm;
                                daiLyWD.ShowDialog();
                                loadAllData();
                                break;
                            case 1:
                                var sanPhamWD = new CapNhatSanPhamWindow();
                                SanPham sanPhamSeleted = ListSanPham.ToList()[p.SelectedIndex];
                                SanPhamHienThi sanPhamHienThi = new SanPhamHienThi();

                                var SanPham = db.SanPhams.Where(sp => sp.Id == sanPhamSeleted.Id).FirstOrDefault();
                                SanPham.HinhAnh = Path.GetFullPath(SanPham.HinhAnh);

                                sanPhamHienThi.SanPham = SanPham;

                                sanPhamHienThi.NguonNhap = SanPham.NguonNhap;
                                sanPhamHienThi.LoaiSanPham = SanPham.LoaiSanPham;
                                sanPhamHienThi.DonViTinh = SanPham.DonViTinh;

                                sanPhamHienThi.GiaBan = ConvertNumber.convertNumberDecimalToString(SanPham.GiaBan);
                                sanPhamHienThi.GiaNhap = ConvertNumber.convertNumberDecimalToString(SanPham.GiaNhap);
                                sanPhamHienThi.SoLuong = ConvertNumber.convertNumberDecimalToString(SanPham.SoLuong);

                                var sanPhamVM = new CapNhatSanPhamViewModel(sanPhamHienThi);
                                sanPhamWD.DataContext = sanPhamVM;
                                sanPhamWD.ShowDialog();
                                loadAllData();
                                break;
                            case 2:
                                var nguonNhapWD = new NguonNhapWindow();
                                var nguonNhapVM = new NguonNhapViewModel();
                                nguonNhapVM.SelectNguonNhap = nguonNhapVM.NguonNhaps.Where(nn => nn.Id == (p.SelectedItem as NguonNhap).Id).FirstOrDefault();

                                nguonNhapVM.HinhAnh = nguonNhapVM.SelectNguonNhap.HinhAnh;
                                nguonNhapVM.Ten = nguonNhapVM.SelectNguonNhap.Ten;
                                nguonNhapVM.SoDienThoai = nguonNhapVM.SelectNguonNhap.SoDienThoai;
                                nguonNhapVM.DiaChi = nguonNhapVM.SelectNguonNhap.DiaChi;

                                nguonNhapWD.DataContext = nguonNhapVM;

                                nguonNhapWD.ShowDialog();

                                loadAllData();
                                break;
                            default:
                                break;
                        }
                        p.Text = "";
                    }
                }
            });

            LoaiTimKiemCommand = new RelayCommand<MainWindow>((p) => { return true; }, (p) => {
                loaiTimKiem = p.cbb_loaiTimKiem.SelectedIndex;
                switch(loaiTimKiem)
                {
                    case 0:
                        p.textbox_search.ItemsSource = ListDaiLy;
                        break;
                    case 1:
                        p.textbox_search.ItemsSource = ListSanPham;
                        break;
                    case 2:
                        p.textbox_search.ItemsSource = ListNguonNhap;
                        break;
                    default:
                        break;
                }
            });

        }

        private void DaiLyTop1_MouseDown(Window p)
        {
            if (DaiLyTop1 == null)
                return;

            var wd = new CapNhatDaiLyWindow();
            DaiLyViewModel vm = new DaiLyViewModel();
            using(DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                DaiLy dl = db.DaiLies.Where(d => d.Id == DaiLyTop1.ID && d.IsRemove == false).FirstOrDefault();
                vm.Ten = dl.Ten;
                vm.Quan = dl.Quan;
                vm.DiaChi = dl.DiaChi;
                vm.DienThoai = dl.DienThoai;
                vm.Email = dl.Email;
                vm.HinhAnh = Path.GetFullPath(dl.HinhAnh);
                vm.NgayTiepNhan = dl.NgayTiepNhan;

                vm.SelectedLoaiDaiLy = vm.LoaiDaiLy.Where(x => x.Id == (dl.IdLoaiDaiLy)).SingleOrDefault();
                vm.SelectedItem = new DaiLy()
                {
                    Id = dl.Id,
                    Ten = dl.Ten,
                    DienThoai = dl.DienThoai,
                    DiaChi = dl.DiaChi,
                    NgayTiepNhan = dl.NgayTiepNhan,
                    Quan = dl.Quan,
                    Email = dl.Email,
                    HinhAnh = Path.GetFullPath(dl.HinhAnh),
                    IdLoaiDaiLy = dl.IdLoaiDaiLy
                };
            }
            wd.DataContext = vm;

            wd.ShowDialog();
            loadAllData();
        }

        private void DaiLyTop2_MouseDown(Window p)
        {
            if (DaiLyTop2 == null)
                return;

            var wd = new CapNhatDaiLyWindow();
            DaiLyViewModel vm = new DaiLyViewModel();
            using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                DaiLy dl = db.DaiLies.Where(d => d.Id == DaiLyTop2.ID && d.IsRemove == false).FirstOrDefault();
                vm.Ten = dl.Ten;
                vm.Quan = dl.Quan;
                vm.DiaChi = dl.DiaChi;
                vm.DienThoai = dl.DienThoai;
                vm.Email = dl.Email;
                vm.HinhAnh = Path.GetFullPath(dl.HinhAnh);
                vm.NgayTiepNhan = dl.NgayTiepNhan;

                vm.SelectedLoaiDaiLy = vm.LoaiDaiLy.Where(x => x.Id == (dl.IdLoaiDaiLy)).SingleOrDefault();
                vm.SelectedItem = new DaiLy()
                {
                    Id = dl.Id,
                    Ten = dl.Ten,
                    DienThoai = dl.DienThoai,
                    DiaChi = dl.DiaChi,
                    NgayTiepNhan = dl.NgayTiepNhan,
                    Quan = dl.Quan,
                    Email = dl.Email,
                    HinhAnh = Path.GetFullPath(dl.HinhAnh),
                    IdLoaiDaiLy = dl.IdLoaiDaiLy
                };
            }
            wd.DataContext = vm;

            wd.ShowDialog();
            loadAllData();
        }

        private void DaiLyTop3_MouseDown(Window p)
        {
            if (DaiLyTop3 == null)
                return;

            var wd = new CapNhatDaiLyWindow();
            DaiLyViewModel vm = new DaiLyViewModel();
            using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                DaiLy dl = db.DaiLies.Where(d => d.Id == DaiLyTop3.ID && d.IsRemove == false).FirstOrDefault();
                vm.Ten = dl.Ten;
                vm.Quan = dl.Quan;
                vm.DiaChi = dl.DiaChi;
                vm.DienThoai = dl.DienThoai;
                vm.Email = dl.Email;
                vm.HinhAnh = Path.GetFullPath(dl.HinhAnh);
                vm.NgayTiepNhan = dl.NgayTiepNhan;
                
                vm.SelectedLoaiDaiLy = vm.LoaiDaiLy.Where(x => x.Id == (dl.IdLoaiDaiLy)).SingleOrDefault();
                vm.SelectedItem = new DaiLy()
                {
                    Id = dl.Id,
                    Ten = dl.Ten,
                    DienThoai = dl.DienThoai,
                    DiaChi = dl.DiaChi,
                    NgayTiepNhan = dl.NgayTiepNhan,
                    Quan = dl.Quan,
                    Email = dl.Email,
                    HinhAnh = Path.GetFullPath(dl.HinhAnh),
                    IdLoaiDaiLy = dl.IdLoaiDaiLy
                };
            }
            wd.DataContext = vm;

            wd.ShowDialog();
            loadAllData();
        }

        private void SanPhamTop1_MouseDown(Window p)
        { 
            if (SanPhamTop1 == null)
                return;

            CapNhatSanPhamWindow wd = new CapNhatSanPhamWindow();
            SanPhamHienThi sanPhamHienThi = new SanPhamHienThi();
            using(DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                if (db.SanPhams.Where(sp => sp.Id == SanPhamTop1.ID && sp.TrangThai == true).Count() < 1)
                    return;

                var SanPham = db.SanPhams.Where(sp => sp.Id == SanPhamTop1.ID && sp.TrangThai == true).FirstOrDefault();
                if (SanPham.HinhAnh != null)
                    SanPham.HinhAnh = Path.GetFullPath(SanPham.HinhAnh);
                else
                    SanPham.HinhAnh = Path.GetFullPath("/Assets/image_not_available.png");

                sanPhamHienThi.SanPham = SanPham;

                sanPhamHienThi.NguonNhap = SanPham.NguonNhap;
                sanPhamHienThi.LoaiSanPham = SanPham.LoaiSanPham;
                sanPhamHienThi.DonViTinh = SanPham.DonViTinh;

                sanPhamHienThi.GiaBan = ConvertNumber.convertNumberDecimalToString(SanPham.GiaBan);
                sanPhamHienThi.GiaNhap = ConvertNumber.convertNumberDecimalToString(SanPham.GiaNhap);
                sanPhamHienThi.SoLuong = ConvertNumber.convertNumberDecimalToString(SanPham.SoLuong);
            }
            var vm = new CapNhatSanPhamViewModel(sanPhamHienThi);
            wd.DataContext = vm;
            wd.ShowDialog();
            loadAllData();
        }
        private void SanPhamTop2_MouseDown(Window p)
        {
            if (SanPhamTop2 == null)
                return;

            CapNhatSanPhamWindow wd = new CapNhatSanPhamWindow();
            SanPhamHienThi sanPhamHienThi = new SanPhamHienThi();
            using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                if (db.SanPhams.Where(sp => sp.Id == SanPhamTop2.ID && sp.TrangThai == true).Count() < 1)
                    return;

                var SanPham = db.SanPhams.Where(sp => sp.Id == SanPhamTop2.ID && sp.TrangThai == true).FirstOrDefault();

                if (SanPham.HinhAnh != null)
                    SanPham.HinhAnh = Path.GetFullPath(SanPham.HinhAnh);
                else
                    SanPham.HinhAnh = Path.GetFullPath("/Assets/image_not_available.png");

                sanPhamHienThi.SanPham = SanPham;

                sanPhamHienThi.NguonNhap = SanPham.NguonNhap;
                sanPhamHienThi.LoaiSanPham = SanPham.LoaiSanPham;
                sanPhamHienThi.DonViTinh = SanPham.DonViTinh;

                sanPhamHienThi.GiaBan = ConvertNumber.convertNumberDecimalToString(SanPham.GiaBan);
                sanPhamHienThi.GiaNhap = ConvertNumber.convertNumberDecimalToString(SanPham.GiaNhap);
                sanPhamHienThi.SoLuong = ConvertNumber.convertNumberDecimalToString(SanPham.SoLuong);
            }
            var vm = new CapNhatSanPhamViewModel(sanPhamHienThi);
            wd.DataContext = vm;
            wd.ShowDialog();
            loadAllData();
        }
        private void SanPhamTop3_MouseDown(Window p)
        {
            if (SanPhamTop3 == null)
                return;
            CapNhatSanPhamWindow wd = new CapNhatSanPhamWindow();
            SanPhamHienThi sanPhamHienThi = new SanPhamHienThi();
            using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                if (db.SanPhams.Where(sp => sp.Id == SanPhamTop3.ID && sp.TrangThai == true).Count() < 1)
                    return;

                var SanPham = db.SanPhams.Where(sp => sp.Id == SanPhamTop3.ID && sp.TrangThai == true).FirstOrDefault();
                if (SanPham.HinhAnh != null)
                    SanPham.HinhAnh = Path.GetFullPath(SanPham.HinhAnh);
                else
                    SanPham.HinhAnh = Path.GetFullPath("/Assets/image_not_available.png");

                sanPhamHienThi.SanPham = SanPham;

                sanPhamHienThi.NguonNhap = SanPham.NguonNhap;
                sanPhamHienThi.LoaiSanPham = SanPham.LoaiSanPham;
                sanPhamHienThi.DonViTinh = SanPham.DonViTinh;

                sanPhamHienThi.GiaBan = ConvertNumber.convertNumberDecimalToString(SanPham.GiaBan);
                sanPhamHienThi.GiaNhap = ConvertNumber.convertNumberDecimalToString(SanPham.GiaNhap);
                sanPhamHienThi.SoLuong = ConvertNumber.convertNumberDecimalToString(SanPham.SoLuong);
            }
            var vm = new CapNhatSanPhamViewModel(sanPhamHienThi);
            wd.DataContext = vm;
            wd.ShowDialog();
            loadAllData();

        }

        private void loadAllData()
        {
            khoiTaoThuocTinh();
            loadTopSanPham();
            loadTopDaiLy();
            LocThongkeTheoNgay();
        }

        private void loadTopSanPham()
        {
            var listSanPhamTop = new List<SanPhamTop>();
            using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                var listPhieuXuat = db.ChiTietPhieuXuatHangs.Where(ct => ct.SanPham.TrangThai == true).GroupBy(t => t.IdSanPham);

                foreach (var group in listPhieuXuat)
                {
                    var spt = new SanPhamTop { ID = group.Key };
                    spt.SoLuongNum = 0;
                    foreach (var i in group)
                    {
                        spt.SoLuongNum += i.SoLuong;
                    }
                    spt.SoLuong = ConvertNumber.convertNumberToString(spt.SoLuongNum);
                    listSanPhamTop.Add(spt);
                }


                for (int i = 0; i < listSanPhamTop.Count - 1; i++)
                {
                    for (int j = i + 1; j < listSanPhamTop.Count; j++)
                    {
                        if (listSanPhamTop[i].SoLuongNum < listSanPhamTop[j].SoLuongNum)
                        {
                            var temp = listSanPhamTop[i];
                            listSanPhamTop[i] = listSanPhamTop[j];
                            listSanPhamTop[j] = temp;
                        }
                    }
                }


                foreach (var i in listSanPhamTop)
                {
                    foreach (var sp in db.SanPhams)
                    {
                        if (i.ID == sp.Id)
                        {
                            if (sp.TrangThai)
                            {
                                if (sp.HinhAnh != null)
                                    i.HinhAnh = Path.GetFullPath(sp.HinhAnh);
                                else
                                    i.HinhAnh = Path.GetFullPath("/Assets/image_not_available.png");

                                i.Ten = sp.Ten;
                            }
                            else
                            {
                                listSanPhamTop.Remove(i);
                            }
                            break;
                        }
                    }
                }
            }
            if (listSanPhamTop.Count > 0)
            {
                SanPhamTop1 = listSanPhamTop[0];
            }
            if (listSanPhamTop.Count > 1)
            {
                SanPhamTop2 = listSanPhamTop[1];
            }

            if (listSanPhamTop.Count > 2)
            {
                SanPhamTop3 = listSanPhamTop[2];
            }
        }
 
        private void loadTopDaiLy()
        {
            using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                var listDaiLy = (from pdl in db.PhieuDaiLies
                                 join pxh in db.PhieuXuatHangs
                                 on pdl.Id equals pxh.IdPhieuDaiLy
                                 join dl in db.DaiLies
                                 on pdl.IdDaiLy equals dl.Id
                                 where dl.IsRemove == false
                                 select new
                                 {
                                     ID = pdl.IdDaiLy,
                                     TongTien = pxh.TongTien
                                 }).GroupBy(t => t.ID);

                var listDaiLyTop = new List<DaiLyTop>();

                foreach (var group in listDaiLy)
                {
                    var dlt = new DaiLyTop { ID = group.Key };
                    dlt.TongTienXuatNum = 0;
                    foreach (var i in group)
                    {
                        dlt.TongTienXuatNum += i.TongTien;
                    }
                    dlt.TongTienXuat = ConvertNumber.convertNumberDecimalToString(dlt.TongTienXuatNum);
                    listDaiLyTop.Add(dlt);
                }


                for (int i = 0; i < listDaiLyTop.Count; i++)
                {
                    for (int j = i + 1; j < listDaiLyTop.Count; j++)
                    {
                        if (listDaiLyTop[i].TongTienXuatNum < listDaiLyTop[j].TongTienXuatNum)
                        {
                            var temp = listDaiLyTop[i];
                            listDaiLyTop[i] = listDaiLyTop[j];
                            listDaiLyTop[j] = temp;
                        }
                    }
                }


                foreach (var i in listDaiLyTop)
                {
                    foreach (var dl in db.DaiLies)
                    {
                        if (i.ID == dl.Id)
                        {
                            if (dl.HinhAnh != null)
                                i.HinhAnh = Path.GetFullPath(dl.HinhAnh);
                            else
                                i.HinhAnh = Path.GetFullPath("/Assets/image_not_available.png");
                            i.Ten = dl.Ten;
                        }
                    }
                }


                if (listDaiLyTop.Count > 0)
                {
                    DaiLyTop1 = listDaiLyTop[0];
                }
                if (listDaiLyTop.Count > 1)
                {
                    DaiLyTop2 = listDaiLyTop[1];
                }

                if (listDaiLyTop.Count > 2)
                {
                    DaiLyTop3 = listDaiLyTop[2];
                }

            }
        }

        private void khoiTaoThuocTinh()
        {
            SanPhamTop1 = new SanPhamTop();
            SanPhamTop2 = new SanPhamTop();
            SanPhamTop3 = new SanPhamTop();

            DaiLyTop1 = new DaiLyTop();
            DaiLyTop2 = new DaiLyTop();
            DaiLyTop3 = new DaiLyTop();

            NgayKetThuc = DateTime.Now;

            using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                if (db.PhieuDaiLies.Count() > 0)
                {
                    NgayBatDau = (from i in db.PhieuXuatHangs
                                  orderby i.NgayLapPhieu ascending
                                  select i).FirstOrDefault().NgayLapPhieu;
                }
                else
                {
                    NgayBatDau = DateTime.MinValue;
                }

                ListDaiLy = new ObservableCollection<DaiLy>(db.DaiLies.Where(p => p.IsRemove == false));
                ListSanPham = new ObservableCollection<SanPham>(db.SanPhams.Where(p => p.TrangThai == true));
                ListNguonNhap = new ObservableCollection<NguonNhap>(db.NguonNhaps);
            }

            loadHinhAnh();
        }

        private void LocThongkeTheoNgay()
        {
            using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                int soLuongDaiLy = db.DaiLies.Where(dl => dl.IsRemove == false).Count();
                int sanPhamTonKho = 0;
                decimal doanhThu = 0;
                decimal tongTienThu = 0;
                decimal loiNhuan = 0;
                decimal tongCongNo = 0;

                foreach (var i in db.SanPhams.Where(sp => sp.TrangThai == true)) 
                {
                    sanPhamTonKho += i.SoLuong;
                }

                foreach (var i in db.PhieuXuatHangs.Where(p => p.NgayLapPhieu >= NgayBatDau && p.NgayLapPhieu <= NgayKetThuc))
                {
                    doanhThu += i.TongTien;
                }

                foreach (var i in db.PhieuThuTiens.Where(p => p.NgayThuTien >= NgayBatDau && p.NgayThuTien <= NgayKetThuc))
                {
                    tongTienThu += i.SoTienThu;
                }

                foreach (var i in db.ChiTietPhieuXuatHangs.Where(p => p.PhieuXuatHang.NgayLapPhieu >= NgayBatDau && p.PhieuXuatHang.NgayLapPhieu <= NgayKetThuc))
                {
                    loiNhuan += i.SoLuong * (i.GiaBan - i.SanPham.GiaNhap);
                }

                tongCongNo = doanhThu - tongTienThu;
                doanhThu -= tongCongNo;

                DoanhThu = ConvertNumber.convertNumberDecimalToString(doanhThu);
                TongCongNo = ConvertNumber.convertNumberDecimalToString(tongCongNo);
                LoiNhuan = ConvertNumber.convertNumberDecimalToString(loiNhuan);
                SanPhamTonKho = ConvertNumber.convertNumberToString(sanPhamTonKho);
                TongSoDaiLy = ConvertNumber.convertNumberToString(soLuongDaiLy);
            }
        }

        private void loadHinhAnh()
        {
            foreach (var i in ListDaiLy)
            {
                if (!string.IsNullOrEmpty(i.HinhAnh))
                {
                    i.HinhAnh = Path.GetFullPath(i.HinhAnh);
                }
                else
                {
                    i.HinhAnh = Path.GetFullPath("/Assets/image_not_available.png");
                }
            }
            foreach (var i in ListSanPham)
            {
                if (!string.IsNullOrEmpty(i.HinhAnh))
                {
                    i.HinhAnh = Path.GetFullPath(i.HinhAnh);
                }
                else
                {
                    i.HinhAnh = Path.GetFullPath("/Assets/image_not_available.png");
                }
            }
            foreach (var i in ListNguonNhap)
            {
                if (!string.IsNullOrEmpty(i.HinhAnh))
                {
                    i.HinhAnh = Path.GetFullPath(i.HinhAnh);
                }
                else
                {
                    i.HinhAnh = Path.GetFullPath("/Assets/image_not_available.png");
                }
            }
        }

        FrameworkElement GetWindowParent(object p, string name)
        {
            var parent = p as FrameworkElement;

            while (parent.Name != name && parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }

            return parent;
        }
    }
}
