using Microsoft.Win32;
using QuanLyDaiLyMVVM.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace QuanLyDaiLyMVVM.ViewModel
{
    public class ProfileViewModel : BaseViewModel
    {
        private Visibility _Visibility;
        public Visibility Visibility { get => _Visibility; set { _Visibility = value; OnPropertyChanged(); } }

        private NhanVien _NhanVien;
        public NhanVien NhanVien { get => _NhanVien; set { _NhanVien = value; OnPropertyChanged(); } }

        private string _Password;
        public string Password { get => _Password; set { _Password = value; OnPropertyChanged(); } }

        private string _NewPassword;
        public string NewPassword { get => _NewPassword; set { _NewPassword = value; OnPropertyChanged(); } }

        public ICommand ShowDoiMatKhauCommand { get; set; }
        public ICommand ShowDSNhanVienCommand { get; set; }
        public ICommand DoiMatKhauCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand PasswordChangedCommand { get; set; }
        public ICommand PasswordChangedCommandNew { get; set; }
        public ICommand WindowLoaded { get; set; }
        public ICommand ChangeAVTCommand { get; set; }

        public ProfileViewModel()
        {
            using (var db = new DBQuanLyCacDaiLyEntities())
            {
                NhanVien = db.NhanViens.Where(x => x.Id == LoginViewModel.IdUser).FirstOrDefault();
            }
            
            if(NhanVien.HinhAnh != null)
            {
                NhanVien.HinhAnh = Path.GetFullPath(NhanVien.HinhAnh);
            }

            ShowDoiMatKhauCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { DoiMatKhauWindow wd = new DoiMatKhauWindow(); wd.ShowDialog(); });
            ShowDSNhanVienCommand = new RelayCommand<object>((p) => {
                return true;
            }, (p) => {
                var wd = new NhanVienVaTaiKhoanWindow();
                var vm = wd.DataContext as NhanVienViewModel;
                vm.Ten = null;
                vm.DienThoai = null;
                vm.DiaChi = null;
                vm.Email = null;
                vm.TenDangNhap = null;
                vm.MatKhau = null;
                vm.VaiTro = 1;
                wd.ShowDialog();
            });
            DoiMatKhauCommand = new RelayCommand<Window>((p) => {
                using (var db = new DBQuanLyCacDaiLyEntities())
                {
                    var passwordcsdl = db.TaiKhoans.Where(x => x.Id == LoginViewModel.IdUser).SingleOrDefault().MatKhau;
                    if (LoginViewModel.MD5Hash(LoginViewModel.Base64Encode(Password)) == passwordcsdl && !string.IsNullOrEmpty(NewPassword))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }, (p) => {
                if (!string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(NewPassword))
                {
                    using (var db = new DBQuanLyCacDaiLyEntities())
                    {
                        var acc = db.TaiKhoans.Where(x => x.Id == LoginViewModel.IdUser).SingleOrDefault();
                        acc.MatKhau = LoginViewModel.MD5Hash(LoginViewModel.Base64Encode(NewPassword));
                        db.SaveChanges();
                    }
                    MessageBox.Show("Đổi mật khẩu thành công");
                    p.Close();
                }
                else
                {
                    MessageBox.Show("Yêu cầu nhập đầy đủ thông tin.");
                }
            });
            PasswordChangedCommand = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { Password = p.Password; });
            PasswordChangedCommandNew = new RelayCommand<PasswordBox>((p) => { return true; }, (p) => { NewPassword = p.Password; });

            ChangeAVTCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                OpenFileDialog open = new OpenFileDialog();
                open.Multiselect = false;
                open.Filter = "Image Files(*.jpg; *.png; *.jpeg; *.gif; *.bmp)|*.jpg; *.png; *.jpeg; *.gif; *.bmp";
                bool? result = open.ShowDialog();
                if (result == true)
                {
                    var img = open.FileNames;
                    //p.imgEllipse.ImageSource = new BitmapImage(new Uri(img[0].ToString()));
                    NhanVien.HinhAnh = img[0].ToString();

                    var currentFolder = AppDomain.CurrentDomain.BaseDirectory.ToString();
                    string uriImage = currentFolder.ToString();
                    string file = NhanVien.HinhAnh;


                    //Lấy file ảnh copy vào Images của project
                    var info = new FileInfo(file);
                    var newName = $"{Guid.NewGuid()}{info.Extension}";
                    File.Copy(file, $"{uriImage}Images\\Staff\\{newName}");

                    using (var db = new DBQuanLyCacDaiLyEntities())
                    {
                        var item = db.NhanViens.Where(x => x.Id == LoginViewModel.IdUser).FirstOrDefault();
                        item.HinhAnh = $"Images/Staff/{newName}";
                        db.SaveChanges();
                    }
                }
            });
            CloseCommand = new RelayCommand<Window>((p) => { if (p == null) return false; return true; }, (p) => { p.Close(); });
        }
    }
}