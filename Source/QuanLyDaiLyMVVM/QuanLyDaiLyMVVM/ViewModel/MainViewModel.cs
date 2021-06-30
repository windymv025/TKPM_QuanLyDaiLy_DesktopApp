using QuanLyDaiLyMVVM.Model;
using System;
using System.Collections.Generic;
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

        public bool Isloaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand DaiLyShowCommand { get; set; }
        public ICommand SanPhamShowCommand { get; set; }
        public ICommand PhieuThuTienShowCommand { get; set; }
        public ICommand PhieuXuatHangShowCommand { get; set; }
        public ICommand RevenueShowCommand { get; set; }
        public ICommand AccountShowCommand { get; set; }
        public ICommand QuyDinhShowCommand { get; set; }
        public ICommand ContactShowCommand { get; set; }
        


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


            DaiLyShowCommand = new RelayCommand<ToggleButton>((p) => { return true; }, (p) => {
                p.IsChecked = false;
                DaiLyWindow wd = new DaiLyWindow();
                wd.ShowDialog();
            });

            SanPhamShowCommand = new RelayCommand<ToggleButton>((p) => { return true; }, (p) => {
                p.IsChecked = false;
                //new CapNhatSanPhamWindow().ShowDialog();
                SanPhamWindow wd = new SanPhamWindow();
                wd.ShowDialog();
            });

            PhieuThuTienShowCommand = new RelayCommand<ToggleButton>((p) => { return true; }, (p) => {
                p.IsChecked = false;
                PhieuThuTienWindow wd = new PhieuThuTienWindow();
                wd.ShowDialog();
            });

            PhieuXuatHangShowCommand = new RelayCommand<ToggleButton>((p) => { return true; }, (p) => {
                p.IsChecked = false;
                PhieuXuatHangWindow wd = new PhieuXuatHangWindow();
                wd.ShowDialog();
            });

            RevenueShowCommand = new RelayCommand<ToggleButton>((p) => { return true; }, (p) => {
                p.IsChecked = false;
                BaoCaoWindow wd = new BaoCaoWindow();
                wd.ShowDialog();
            });

            AccountShowCommand = new RelayCommand<ToggleButton>((p) => { return true; }, (p) => {
                p.IsChecked = false;
                NhanVienWindow wd = new NhanVienWindow();
                wd.ShowDialog();
            });

            QuyDinhShowCommand = new RelayCommand<ToggleButton>((p) => { return true; }, (p) => {
                p.IsChecked = false;
                QuyDinhWindow wd = new QuyDinhWindow();
                wd.ShowDialog();
            });

            ContactShowCommand = new RelayCommand<ToggleButton>((p) => { return true; }, (p) => {
                p.IsChecked = false;
                LienHeWindow wd = new LienHeWindow();
                wd.ShowDialog();
            });

            SanPhamTop1_MouseDownCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { SanPhamTop1_MouseDown(p); });

            SanPhamTop2_MouseDownCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { SanPhamTop2_MouseDown(p); });

            SanPhamTop3_MouseDownCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { SanPhamTop3_MouseDown(p); });

            DaiLyTop1_MouseDownCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { DaiLyTop1_MouseDown(p); });
            DaiLyTop2_MouseDownCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { DaiLyTop2_MouseDown(p); });
            DaiLyTop3_MouseDownCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { DaiLyTop3_MouseDown(p); });

        }

        private void DaiLyTop1_MouseDown(Window p)
        {
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
            loadTopDaiLy();
        }

        private void DaiLyTop2_MouseDown(Window p)
        {
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
            loadTopDaiLy();
        }

        private void DaiLyTop3_MouseDown(Window p)
        {
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
            loadTopDaiLy();
        }

        private void SanPhamTop1_MouseDown(Button p)
        {
            CapNhatSanPhamWindow wd = new CapNhatSanPhamWindow();
            SanPhamHienThi sanPhamHienThi = new SanPhamHienThi();
            using(DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                var SanPham = db.SanPhams.Where(sp => sp.Id == SanPhamTop1.ID).FirstOrDefault();
                SanPham.HinhAnh = Path.GetFullPath(SanPham.HinhAnh);

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
            loadTopSanPham();
        }
        private void SanPhamTop2_MouseDown(Button p)
        {
            CapNhatSanPhamWindow wd = new CapNhatSanPhamWindow();
            SanPhamHienThi sanPhamHienThi = new SanPhamHienThi();
            using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                var SanPham = db.SanPhams.Where(sp => sp.Id == SanPhamTop2.ID).FirstOrDefault();
                SanPham.HinhAnh = Path.GetFullPath(SanPham.HinhAnh);

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
            loadTopSanPham();
        }
        private void SanPhamTop3_MouseDown(Button p)
        {
            CapNhatSanPhamWindow wd = new CapNhatSanPhamWindow();
            SanPhamHienThi sanPhamHienThi = new SanPhamHienThi();
            using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                var SanPham = db.SanPhams.Where(sp => sp.Id == SanPhamTop3.ID).FirstOrDefault();
                SanPham.HinhAnh = Path.GetFullPath(SanPham.HinhAnh);

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
            loadTopSanPham();

        }

        private void khoiTaoThuocTinh()
        {
            _SanPhamTop1 = new SanPhamTop();
            _SanPhamTop2 = new SanPhamTop();
            _SanPhamTop3 = new SanPhamTop();

            _DaiLyTop1 = new DaiLyTop();
            _DaiLyTop2 = new DaiLyTop();
            _DaiLyTop3 = new DaiLyTop();
        }

        private void loadAllData()
        {
            khoiTaoThuocTinh();
            loadTopSanPham();
            loadTopDaiLy();
        }

        private void loadTopSanPham()
        {
            var listSanPhamTop = new List<SanPhamTop>();
            using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                var listPhieuXuat = db.ChiTietPhieuXuatHangs.GroupBy(t => t.IdSanPham);

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


                for (int i = 0; i < listSanPhamTop.Count; i++)
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
            if (listSanPhamTop.Count >= 0)
            {
                SanPhamTop1 = listSanPhamTop[0];
            }
            if (listSanPhamTop.Count >= 1)
            {
                SanPhamTop2 = listSanPhamTop[1];
            }

            if (listSanPhamTop.Count >= 2)
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


                if (listDaiLyTop.Count >= 0)
                {
                    _DaiLyTop1 = listDaiLyTop[0];
                }
                if (listDaiLyTop.Count >= 1)
                {
                    _DaiLyTop2 = listDaiLyTop[1];
                }

                if (listDaiLyTop.Count >= 2)
                {
                    _DaiLyTop3 = listDaiLyTop[2];
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
