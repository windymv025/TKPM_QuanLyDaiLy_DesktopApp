using QuanLyDaiLyMVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyDaiLyMVVM.ViewModel
{
    public class NhanVienViewModel: BaseViewModel
    {
        private ObservableCollection<NhanVienVaTaiKhoan> _List;
        public ObservableCollection<NhanVienVaTaiKhoan> List { get => _List; set { _List = value; OnPropertyChanged(); } }
        private int _Id;
        public int Id { get => _Id; set { _Id = value; OnPropertyChanged(); } }
        private string _Ten;
        public string Ten { get => _Ten; set { _Ten = value; OnPropertyChanged(); } }
        private string _DienThoai;
        public string DienThoai { get => _DienThoai; set { _DienThoai = value; OnPropertyChanged(); } }
        private string _DiaChi;
        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }
        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }
        private string _TenDangNhap;
        public string TenDangNhap { get => _TenDangNhap; set { _TenDangNhap = value; OnPropertyChanged(); } }
        private string _MatKhau;
        public string MatKhau { get => _MatKhau; set { _MatKhau = value; OnPropertyChanged(); } }
        private int _VaiTro; //1- admin, 2-staff
        public int VaiTro { get => _VaiTro; set { _VaiTro = value; OnPropertyChanged(); } }
        private ObservableCollection<int> _ListVaiTro;
        public ObservableCollection<int> ListVaiTro { get => _ListVaiTro; set { _ListVaiTro = value; OnPropertyChanged(); } }

        private NhanVienVaTaiKhoan _SelectedItem;
        public NhanVienVaTaiKhoan SelectedItem { get => _SelectedItem; set {
                _SelectedItem = value; OnPropertyChanged();
                if(SelectedItem != null)
                {
                    Id = SelectedItem.Id;
                    Ten = SelectedItem.Ten;
                    DienThoai = SelectedItem.DienThoai;
                    DiaChi = SelectedItem.DiaChi;
                    Email = SelectedItem.Email;
                    VaiTro = SelectedItem.VaiTro;
                    TenDangNhap = SelectedItem.TenDangNhap;
                    MatKhau = SelectedItem.MatKhau;
                }
            } 
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand TextChangedPhoneCommand { get; set; }


        public NhanVienViewModel()
        {
            ListVaiTro = new ObservableCollection<int>() { 1, 2 };
            List = new ObservableCollection<NhanVienVaTaiKhoan>();
            using (var db = new DBQuanLyCacDaiLyEntities())
            {
                var nhanvien = db.NhanViens.Where(x=>x.isRemove == false).ToList();
                var taikhoan = db.TaiKhoans.ToList();
                var list = (from nv in nhanvien
                            join tk in taikhoan on nv.Id equals tk.Id
                            select new
                            {
                                Id = nv.Id,
                                Ten = nv.Ten,
                                DienThoai = nv.DienThoai,
                                DiaChi = nv.DiaChi,
                                Email =nv.Email,
                                VaiTro = nv.VaiTro,
                                TenDangNhap = tk.TenDangNhap,
                                MatKhau = tk.MatKhau
                            });
                foreach(var nv in list)
                {
                    List.Add(new NhanVienVaTaiKhoan()
                    {
                        Id = nv.Id,
                        Ten = nv.Ten,
                        DienThoai = nv.DienThoai,
                        DiaChi = nv.DiaChi,
                        Email = nv.Email,
                        VaiTro = nv.VaiTro,
                        TenDangNhap = nv.TenDangNhap,
                        MatKhau = nv.MatKhau
                    });
                }
                       
            }

            AddCommand = new RelayCommand<NhanVienVaTaiKhoanWindow>((p) => {
                if (p == null || string.IsNullOrEmpty(p.ten.Text.Trim()) || string.IsNullOrEmpty(p.phone.Text.Trim()) || string.IsNullOrEmpty(p.address.Text.Trim()) || string.IsNullOrEmpty(p.role.Text.Trim()) || string.IsNullOrEmpty(p.username.Text.Trim()))
                {
                    return false;
                }
                else
                {
                    using(var db = new DBQuanLyCacDaiLyEntities())
                    {
                        if(db.TaiKhoans.Where(x=>x.TenDangNhap == TenDangNhap).Count() > 0)
                        {
                            return false;
                        }
                        else
                        {
                            if(db.NhanViens.Where(x=>x.Ten == Ten && x.DienThoai == DienThoai).Count() > 0)
                            {
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        }
                    }
                }
            }, (p) => {
                using (var db = new DBQuanLyCacDaiLyEntities())
                {
                    var nv = new NhanVien()
                    {
                        Ten = Ten,
                        DienThoai = DienThoai,
                        DiaChi = DiaChi,
                        Email = Email,
                        VaiTro = VaiTro,
                        HinhAnh = null,
                        isRemove = false
                    };
                    db.NhanViens.Add(nv);
                    db.SaveChanges();

                    var tk = new TaiKhoan()
                    {
                        //Id = db.NhanViens.Where(x => x.Ten == Ten && x.DienThoai == DienThoai).FirstOrDefault().Id,
                        Id = nv.Id,
                        TenDangNhap = TenDangNhap,
                        MatKhau = LoginViewModel.MD5Hash(LoginViewModel.Base64Encode(MatKhau))
                    };

                    db.TaiKhoans.Add(tk);
                    db.SaveChanges();
                    
                    List.Add(new NhanVienVaTaiKhoan() {
                        Id = Id,
                        Ten = Ten,
                        DienThoai = DienThoai,
                        DiaChi = DiaChi,
                        Email = Email,
                        VaiTro = VaiTro,
                        TenDangNhap = TenDangNhap,
                        MatKhau = LoginViewModel.MD5Hash(LoginViewModel.Base64Encode(MatKhau))
                    });
                }

                Ten = null;
                DienThoai = null;
                DiaChi = null;
                Email = null;
                VaiTro = 2;
                TenDangNhap = null;
                MatKhau = null;
                p.FloatingPasswordBox.Password = null;
            });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { if (p == null) return false; else return true; }, (p) => { MatKhau = p.Password; });
            TextChangedPhoneCommand = new RelayCommand<TextBox>((p) => { if (p == null) return false; else return true; }, (p) => { 
                p.Text = Regex.Replace(p.Text, "[^0-9]+", "");
            });

            EditCommand = new RelayCommand<NhanVienVaTaiKhoanWindow>((p) =>
            {
                if (p == null || string.IsNullOrEmpty(p.ten.Text.Trim()) || string.IsNullOrEmpty(p.phone.Text.Trim()) || string.IsNullOrEmpty(p.address.Text.Trim()) || string.IsNullOrEmpty(p.role.Text.Trim()) || string.IsNullOrEmpty(p.username.Text.Trim()) || SelectedItem == null)
                {
                    return false;
                }
                else
                {
                    using (var db = new DBQuanLyCacDaiLyEntities())
                    {
                        if (db.TaiKhoans.Where(x => x.TenDangNhap == TenDangNhap).Count() > 0)
                        {
                            if (db.TaiKhoans.Where(x => x.TenDangNhap == TenDangNhap).FirstOrDefault().Id == SelectedItem.Id)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                }
            }, (p) =>
            {
                using (var db = new DBQuanLyCacDaiLyEntities())
                {
                    var item = db.NhanViens.Where(x => x.Id == SelectedItem.Id).FirstOrDefault();
                    item.Ten = Ten;
                    item.DienThoai = DienThoai;
                    item.DiaChi = DiaChi;
                    item.Email = Email;
                    item.VaiTro = VaiTro;
                    
                    db.SaveChanges();

                    var tk = db.TaiKhoans.Where(x => x.Id == SelectedItem.Id).FirstOrDefault();
                    tk.TenDangNhap = TenDangNhap;
                    tk.MatKhau = LoginViewModel.MD5Hash(LoginViewModel.Base64Encode(MatKhau));
                    db.SaveChanges();

                    SelectedItem.Ten = Ten;
                    SelectedItem.DienThoai = DienThoai;
                    SelectedItem.DiaChi = DiaChi;
                    SelectedItem.Email = Email;
                    SelectedItem.VaiTro = VaiTro;
                    SelectedItem.TenDangNhap = TenDangNhap;
                    SelectedItem.MatKhau = LoginViewModel.MD5Hash(LoginViewModel.Base64Encode(MatKhau));
                }
            });

            DeleteCommand = new RelayCommand<NhanVienVaTaiKhoanWindow>((p) =>
            {
                if (SelectedItem == null)
                {
                    return false;
                }
                else
                {
                    if(SelectedItem.Id == LoginViewModel.IdUser)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
            }, (p) =>
            {
                using (var db = new DBQuanLyCacDaiLyEntities())
                {
                    var item = db.NhanViens.Where(x => x.Id == SelectedItem.Id).FirstOrDefault();
                    item.isRemove = true;
                    db.SaveChanges();

                    List.Remove(SelectedItem);

                    SelectedItem = null;
                    Ten = null;
                    DienThoai = null;
                    DiaChi = null;
                    Email = null;
                    VaiTro = 2;
                    TenDangNhap = null;
                    MatKhau = null;
                    p.FloatingPasswordBox.Password = null;
                }
            });

        }
    }
}
