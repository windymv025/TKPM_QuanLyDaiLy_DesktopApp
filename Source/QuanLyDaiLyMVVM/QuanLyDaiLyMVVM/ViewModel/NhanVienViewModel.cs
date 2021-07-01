using QuanLyDaiLyMVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        VaiTro = nv.VaiTro ?? 2,
                        TenDangNhap = nv.TenDangNhap,
                        MatKhau = nv.MatKhau
                    });
                }
                       
            }
        }
    }
}
