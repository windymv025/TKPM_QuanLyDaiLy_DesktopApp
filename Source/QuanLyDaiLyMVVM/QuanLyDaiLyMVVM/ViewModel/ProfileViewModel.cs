using QuanLyDaiLyMVVM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyDaiLyMVVM.ViewModel
{
    public class ProfileViewModel: BaseViewModel
    {
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

        public ProfileViewModel()
        {
            using (var db = new DBQuanLyCacDaiLyEntities())
            {
                NhanVien = db.NhanViens.Where(x => x.Id == LoginViewModel.IdUser).FirstOrDefault();
            }
                        
            ShowDoiMatKhauCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { DoiMatKhauWindow wd = new DoiMatKhauWindow(); wd.ShowDialog(); });
            ShowDSNhanVienCommand = new RelayCommand<NhanVienWindow>((p) => {
                if (p == null)
                {
                    return false;
                }
                else
                {
                    using (var db = new DBQuanLyCacDaiLyEntities())
                    {
                        var role = db.NhanViens.Where(x => x.Id == LoginViewModel.IdUser).FirstOrDefault().VaiTro;
                        if (role != 1)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }, (p) => {
                var wd = new NhanVienVaTaiKhoanWindow();
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
                if(!string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(NewPassword))
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
            CloseCommand = new RelayCommand<Window>((p) => { if (p == null) return false; return true; }, (p) => { p.Close(); });
        }
    }
}
