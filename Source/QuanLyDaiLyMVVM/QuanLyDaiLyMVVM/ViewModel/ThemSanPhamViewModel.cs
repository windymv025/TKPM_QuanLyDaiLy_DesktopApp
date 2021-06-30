using Microsoft.Win32;
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
    public class ThemSanPhamViewModel: BaseViewModel
    {
        public bool IsSave = false;

        private string _GiaNhap;
        public string GiaNhap { get => _GiaNhap; set { _GiaNhap = value; OnPropertyChanged(); } }

        private string _GiaBan;
        public string GiaBan { get => _GiaBan; set { _GiaBan = value; OnPropertyChanged(); } }

        private string _SoLuong;
        public string SoLuong { get => _SoLuong; set { _SoLuong = value; OnPropertyChanged(); } }

        private SanPham _SanPham;
        public SanPham SanPham { get => _SanPham; set { _SanPham = value; OnPropertyChanged(); } }

        private NguonNhap _NguonNhap;
        public NguonNhap NguonNhap { get => _NguonNhap; set { _NguonNhap = value; OnPropertyChanged(); } }

        private LoaiSanPham _LoaiSanPham;
        public LoaiSanPham LoaiSanPham { get => _LoaiSanPham; set { _LoaiSanPham = value; OnPropertyChanged(); } }

        private DonViTinh _DonViTinh;
        public DonViTinh DonViTinh { get => _DonViTinh; set { _DonViTinh = value; OnPropertyChanged(); } }

        private ObservableCollection<HinhAnhSanPham> _HinhAnhSanPhams;
        public ObservableCollection<HinhAnhSanPham> HinhAnhSanPhams { get => _HinhAnhSanPhams; set { _HinhAnhSanPhams = value; OnPropertyChanged(); } }

        private ObservableCollection<LoaiSanPham> loaiSanPhams;
        public ObservableCollection<LoaiSanPham> LoaiSanPhams { get => loaiSanPhams; set { loaiSanPhams = value; OnPropertyChanged(); } }

        private ObservableCollection<NguonNhap> nguonNhaps;
        public ObservableCollection<NguonNhap> NguonNhaps { get => nguonNhaps; set { nguonNhaps = value; OnPropertyChanged(); } }

        private ObservableCollection<DonViTinh> _DonViTinhs;
        public ObservableCollection<DonViTinh> DonViTinhs { get => _DonViTinhs; set { _DonViTinhs = value; OnPropertyChanged(); } }

        public ICommand ThemDonViCommand { get; set; }
        public ICommand ThemNguonNhapCommand { get; set; }
        public ICommand ThemLoaiSanPhamCommand { get; set; }

        public ICommand ThoatCommand { get; set; }
        public ICommand ThayDoiDuLieuSoCommand { get; set; }
        public ICommand LuuCommand { get; set; }

        public ICommand DuyetFileCommand { get; set; }
        public ICommand ThemAnhSPCommand { get; set; }
        public ICommand XoaHinhAnhSanPhamCommand { get; set; }

        public ThemSanPhamViewModel()
        {
            SanPham = new SanPham { 
                HinhAnh = "Assets/image_not_available.png"
            };
            loadDataList();

            ThemDonViCommand = new RelayCommand<Button>((p) => { return true; }, (p) => {
                new DonViTinhWindow().ShowDialog();
                loadDataList();
            });
            ThemNguonNhapCommand = new RelayCommand<Button>((p) => { return true; }, (p) => {
                new NguonNhapWindow().ShowDialog();
                loadDataList();
            });
            ThemLoaiSanPhamCommand = new RelayCommand<Button>((p) => { return true; }, (p) => {
                new LoaiSanPhamWindow().ShowDialog();
                loadDataList();
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
            });

            #region Xử lý hình ảnh sản phẩm


            XoaHinhAnhSanPhamCommand = new RelayCommand<ListView>((p) => { return true; }, (p) => {
                if (p.SelectedIndex > -1)
                {
                    HinhAnhSanPhams.RemoveAt(p.SelectedIndex);
                }
            });

            DuyetFileCommand = new RelayCommand<Button>((p) => { return true; }, (p) => {
                OpenFileDialog open = new OpenFileDialog();
                open.Multiselect = false;
                open.Filter = "Image Files(*.jpg; *.png; *.jpeg; *.gif; *.bmp)|*.jpg; *.png; *.jpeg; *.gif; *.bmp";
                bool? result = open.ShowDialog();
                if (result == true)
                {
                    var img = open.FileNames;
                    SanPham.HinhAnh = img[0].ToString();
                }
            });

            ThemAnhSPCommand = new RelayCommand<Button>((p) => { return true; }, (p) => {
                OpenFileDialog open = new OpenFileDialog();
                open.Multiselect = true;
                open.Filter = "Image Files(*.jpg; *.png; *.jpeg; *.gif; *.bmp)|*.jpg; *.png; *.jpeg; *.gif; *.bmp";
                bool? result = open.ShowDialog();
                if (result == true)
                {
                    var img = open.FileNames;
                    foreach (var i in img)
                    {
                        var newImage = new HinhAnhSanPham
                        {
                            HinhAnh = i.ToString(),
                            IdSanPham = SanPham.Id
                        };
                        HinhAnhSanPhams.Add(newImage);
                    }
                }
            });
            #endregion

            //====================================================================================================
            #region Thêm sản phẩm
            LuuCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {
                MessageBoxResult result = MessageBox.Show("Bạn có muốn lưu?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                    if (string.IsNullOrEmpty(SanPham.HinhAnh)
                        || string.IsNullOrEmpty(SanPham.Ten)
                        || string.IsNullOrEmpty(GiaBan)
                        || string.IsNullOrEmpty(GiaNhap)
                        || string.IsNullOrEmpty(SoLuong)
                        || GiaNhap == "0"
                        || GiaBan == "0"
                        || DonViTinh == null
                        || LoaiSanPham == null
                        || NguonNhap == null
                        )
                    {
                        MessageBox.Show("Bạn nhập thiếu thông tin sản phẩm.", "Error Systerm", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }


                    SanPham.HinhAnh = ImageDatabase.savingImageToDatabase(SanPham.HinhAnh, ImageDatabase.PATH_PRODUCT);
                    SanPham.GiaBan = ConvertNumber.convertStringtoDecimal(GiaBan);
                    SanPham.GiaNhap = ConvertNumber.convertStringtoDecimal(GiaNhap);
                    SanPham.SoLuong = ConvertNumber.convertStringtoInt(SoLuong);
                    SanPham.IdDonViTinh = DonViTinh.Id;
                    SanPham.IdLoaiSanPham = LoaiSanPham.Id;
                    SanPham.IdNguonNhap = NguonNhap.Id;
                    SanPham.TrangThai = true;

                    if (HinhAnhSanPhams.Count > 0)
                        SanPham.HinhAnhSanPhams = HinhAnhSanPhams;

                    using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
                    {
                        db.SanPhams.Add(SanPham);
                        db.SaveChanges();
                    }

                    IsSave = true;
                    p.Close();
                }
            });
            #endregion

            ThoatCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });
        }

        private void loadDataList()
        {
            HinhAnhSanPhams = new ObservableCollection<HinhAnhSanPham>();
            using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                LoaiSanPhams = new ObservableCollection<LoaiSanPham>(db.LoaiSanPhams);
                NguonNhaps = new ObservableCollection<NguonNhap>(db.NguonNhaps);
                DonViTinhs = new ObservableCollection<DonViTinh>(db.DonViTinhs);
            }

            foreach (var i in LoaiSanPhams)
            {
                if (i.HinhAnh != null)
                {
                    i.HinhAnh = Path.GetFullPath(i.HinhAnh);
                }
                else
                {
                    i.HinhAnh = Path.GetFullPath("Assets/image_not_available.png");
                }
            }

            foreach (var i in NguonNhaps)
            {
                if (i.HinhAnh != null)
                {
                    i.HinhAnh = Path.GetFullPath(i.HinhAnh);
                }
                else
                {
                    i.HinhAnh = Path.GetFullPath("Assets/image_not_available.png");
                }
            }


        }
    }
}
