using QuanLyDaiLyMVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyDaiLyMVVM.ViewModel
{
    public class CapNhatPhieuXuatHangViewModel: BaseViewModel
    {
        private bool _IsSave;
        public bool IsSave { get => _IsSave; set { _IsSave = value; OnPropertyChanged(); } }
        
        private bool _IsChange;
        public bool IsChange { get => _IsChange; set { _IsChange = value; OnPropertyChanged(); } }
        
        private bool _IsAdd;
        public bool IsAdd { get => _IsAdd; set { _IsAdd = value; OnPropertyChanged(); } }
        
        private bool _IsRemove;
        public bool IsRemove { get => _IsRemove; set { _IsRemove = value; OnPropertyChanged(); } }

        private bool isChooseSPX = false;

        private string _TongTien;
        public string TongTien { get => _TongTien; set { _TongTien = value; OnPropertyChanged(); } }

        private string _SoLuong;
        public string SoLuong { get => _SoLuong; set { _SoLuong = value; OnPropertyChanged(); } }

        private string _GiaBan;
        public string GiaBan { get => _GiaBan; set { _GiaBan = value; OnPropertyChanged(); } }        

        private PhieuXuatHangHienThi _PhieuXuatHangHienThi;
        public PhieuXuatHangHienThi PhieuXuatHangHienThi { get => _PhieuXuatHangHienThi; set { _PhieuXuatHangHienThi = value; OnPropertyChanged(); } }

        //Binding combobox
        private DaiLy _DaiLy;
        public DaiLy DaiLy { get => _DaiLy; set { _DaiLy = value; OnPropertyChanged(); } }

        private ObservableCollection<DaiLy> _DaiLys;
        public virtual ObservableCollection<DaiLy> DaiLys { get => _DaiLys; set { _DaiLys = value; OnPropertyChanged(); } }

        private SanPhamHienThi _SanPham;
        public SanPhamHienThi SanPham { get => _SanPham; set { _SanPham = value; OnPropertyChanged(); } }

        private ObservableCollection<SanPhamHienThi> _SanPhams;
        public ObservableCollection<SanPhamHienThi> SanPhams { get => _SanPhams; set { _SanPhams = value; OnPropertyChanged(); } }

        private SanPhamXuat _SanPhamXuat;
        public SanPhamXuat SanPhamXuat { get => _SanPhamXuat; set { _SanPhamXuat = value; OnPropertyChanged(); } }

        private ObservableCollection<SanPhamXuat> _SanPhamXuats;
        public ObservableCollection<SanPhamXuat> SanPhamXuats { get => _SanPhamXuats; set { _SanPhamXuats = value; OnPropertyChanged(); } }

        public ICommand ThayDoiDuLieuSoCommand { get; set; }
        public ICommand ThayDoiDuLieuCommand { get; set; }
        public ICommand ThayDoiDaiLyCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand LuuThayDoiCommand { get; set; }
        public ICommand ThoatCommand { get; set; }
        public ICommand XoaCommand { get; set; }

        public CapNhatPhieuXuatHangViewModel()
        {
        }

        public CapNhatPhieuXuatHangViewModel(PhieuXuatHangHienThi phieuXuatHangHienThi)
        {
            loadData(phieuXuatHangHienThi);
            RefreshCommand = new RelayCommand<CapNhatPhieuXuatHangWindow>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.cbb_DSSanPham.SelectedIndex = -1;
                p.GiaBanTxt.Text = "0";
                p.soLuongTxt.Text = "0";

            });
            
            ThayDoiDuLieuCommand = new RelayCommand<Button>((p) =>
            {
                return true;
            }, (p) =>
            {
                if (isChooseSPX)
                    IsChange = true;
                IsAdd = true;

            });

            ThayDoiDaiLyCommand = new RelayCommand<Button>((p) =>
            {
                return true;
            }, (p) =>
            {
                IsSave = true;
            });

            ThayDoiDuLieuSoCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) => {
                int caretIndex = p.CaretIndex;
                p.Text = Regex.Replace(p.Text, "[^0-9.]+", "");
                string numberString = p.Text;
                string newNumStr = "";
                for (int i = 0; i < numberString.Length; i++)
                {
                    if (numberString[i] != '.')
                    {
                        newNumStr += numberString[i];
                    }
                }
                if (newNumStr == "")
                {
                    newNumStr = "0";
                }
                decimal number = decimal.Parse(newNumStr);
                p.Text = ConvertNumber.convertNumberDecimalToString(number);
                p.CaretIndex = caretIndex;

                if(number > 0)
                {
                    IsAdd = true;
                    if (isChooseSPX)
                        IsChange = true;
                }
                else
                {
                    IsAdd = false;
                    IsChange = false;
                }
            });

            SelectionChangedCommand = new RelayCommand<ListView>((p) => { return true; }, (p) => {
                if (p.SelectedIndex > -1)
                {
                    SanPham = SanPhams.Where(sp => sp.SanPham.Id == SanPhamXuat.SanPham.Id).FirstOrDefault();
                    GiaBan = SanPhamXuat.GiaBan;
                    SoLuong = SanPhamXuat.SoLuong;

                    IsAdd = true;
                    IsChange = false;
                    IsRemove = true;
                    isChooseSPX = true;
                }
                else
                {
                    IsAdd = false;
                    IsChange = false;
                    IsRemove = false;
                    isChooseSPX = false;
                }
            });

            XoaCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                
                p.Close();
            });

            LuuThayDoiCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {

                MessageBoxResult result = MessageBox.Show("Bạn có muốn lưu?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                   

                    p.Close();
                }
            });

            ThoatCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });
        }

        private void loadData(PhieuXuatHangHienThi phieuXuatHangHienThi)
        {
            SanPham = new SanPhamHienThi();
            SoLuong = "0";
            GiaBan = "0";
            IsChange = false;
            IsAdd = false;
            IsRemove = false;
            IsSave = false;

            PhieuXuatHangHienThi = phieuXuatHangHienThi;
            using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
               // Load list san pham
                var sanPhams = new ObservableCollection<SanPham>(db.SanPhams.Where(sp => sp.TrangThai == true));
                var loaiSanPhams = new ObservableCollection<LoaiSanPham>(db.LoaiSanPhams);
                var nguonNhaps = new ObservableCollection<NguonNhap>(db.NguonNhaps);
                var donViTinhs = new ObservableCollection<DonViTinh>(db.DonViTinhs);

                foreach (var sp in sanPhams)
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

                foreach (var l in loaiSanPhams)
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

                foreach (var l in nguonNhaps)
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
                SanPhams = new ObservableCollection<SanPhamHienThi>(
                from sp in sanPhams
                join lsp in loaiSanPhams
                on sp.IdLoaiSanPham equals lsp.Id
                join nn in nguonNhaps
                on sp.IdNguonNhap equals nn.Id
                join dv in donViTinhs
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

                //Load dai ly
                DaiLys = new ObservableCollection<DaiLy>(db.DaiLies);
                foreach (var i in DaiLys)
                {
                    if (i.HinhAnh == null)
                    {
                        i.HinhAnh = Path.GetFullPath("Assets/image_not_available.png");
                    }
                    else
                    {
                        i.HinhAnh = Path.GetFullPath(i.HinhAnh);
                    }
                }

                //Load san pham xuat
                SanPhamXuats = new ObservableCollection<SanPhamXuat>(
                    from i in db.ChiTietPhieuXuatHangs
                    where i.IdPhieuXuatHang == phieuXuatHangHienThi.PhieuDaiLy.Id
                    select new SanPhamXuat
                    {
                        SanPham = i.SanPham,
                        DonViTinh = i.SanPham.DonViTinh,
                        ChiTietPhieuXuatHang = i
                    });

                foreach(var sp in SanPhamXuats)
                {
                    sp.SoLuong = ConvertNumber.convertNumberToString(sp.ChiTietPhieuXuatHang.SoLuong);
                    sp.GiaBan = ConvertNumber.convertNumberDecimalToString(sp.ChiTietPhieuXuatHang.GiaBan);
                    sp.ThanhTienNum = sp.ChiTietPhieuXuatHang.GiaBan * sp.ChiTietPhieuXuatHang.SoLuong;
                    sp.ThanhTien = ConvertNumber.convertNumberDecimalToString(sp.ThanhTienNum);
                }
            }

            // Binding view
            DaiLy = DaiLys.Where(dl => dl.Id == phieuXuatHangHienThi.DaiLy.Id).FirstOrDefault();

            decimal tongtien = 0;

            int stt = 1;
            foreach(var i in SanPhamXuats)
            {
                tongtien += i.ThanhTienNum;
                i.STT = stt;
                stt++;
            }
            TongTien = ConvertNumber.convertNumberDecimalToString(tongtien);
        }
    }
}
