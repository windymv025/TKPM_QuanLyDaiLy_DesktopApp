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
using System.Windows.Media.Imaging;

namespace QuanLyDaiLyMVVM.ViewModel
{
    public class CapNhatSanPhamViewModel: BaseViewModel
    {
        private HinhAnhSanPham _HinhAnhHienThi;
        public HinhAnhSanPham HinhAnhHienThi { get => _HinhAnhHienThi; set { _HinhAnhHienThi = value; OnPropertyChanged(); } }

        private string _Ten;
        public string Ten { get => _Ten; set { _Ten = value; OnPropertyChanged(); } }

        private string _GiaNhap;
        public string GiaNhap { get => _GiaNhap; set { _GiaNhap = value; OnPropertyChanged(); } }

        private string _GiaBan;
        public string GiaBan { get => _GiaBan; set { _GiaBan = value; OnPropertyChanged(); } }

        private string _SoLuong;
        public string SoLuong { get => _SoLuong; set { _SoLuong = value; OnPropertyChanged(); } }

        private string _HinhAnh;
        public string HinhAnh { get => _HinhAnh; set { _HinhAnh = value; OnPropertyChanged(); } }

        private string _MoTa;
        public string MoTa { get => _MoTa; set { _MoTa = value; OnPropertyChanged(); } }

        private SanPham _SanPham;
        public SanPham SanPham { get => _SanPham; set { _SanPham = value; OnPropertyChanged(); } }

        private NguonNhap _NguonNhap;
        public NguonNhap NguonNhap { get => _NguonNhap; set { _NguonNhap = value; OnPropertyChanged(); } }

        private LoaiSanPham _LoaiSanPham;
        public LoaiSanPham LoaiSanPham { get => _LoaiSanPham; set { _LoaiSanPham = value; OnPropertyChanged(); } }

        private DonViTinh _DonViTinh;
        public DonViTinh DonViTinh { get => _DonViTinh; set { _DonViTinh = value; OnPropertyChanged(); } }

        private SanPhamHienThi _SanPhamHienThi;
        public SanPhamHienThi SanPhamHienThi { get => _SanPhamHienThi; set { _SanPhamHienThi = value; OnPropertyChanged(); } }

        private ObservableCollection<HinhAnhSanPham> _HinhAnhSanPhams;
        public ObservableCollection<HinhAnhSanPham> HinhAnhSanPhams { get => _HinhAnhSanPhams; set { _HinhAnhSanPhams = value; OnPropertyChanged(); } }

        private List<HinhAnhSanPham> ListHinhAnhBiXoa;

        private ObservableCollection<LoaiSanPham> loaiSanPhams;
        public ObservableCollection<LoaiSanPham> LoaiSanPhams { get => loaiSanPhams; set { loaiSanPhams = value; OnPropertyChanged(); } }
        
        private ObservableCollection<NguonNhap> nguonNhaps;
        public ObservableCollection<NguonNhap> NguonNhaps  { get => nguonNhaps; set { nguonNhaps = value; OnPropertyChanged(); } }

        private ObservableCollection<DonViTinh> _DonViTinhs;
        public ObservableCollection<DonViTinh> DonViTinhs { get => _DonViTinhs; set { _DonViTinhs = value; OnPropertyChanged(); } }

        public ICommand ThemDonViCommand { get; set; }
        public ICommand ThemNguonNhapCommand { get; set; }
        public ICommand ThemLoaiSanPhamCommand { get; set; }
        public ICommand ThoatCommand { get; set; }
        public ICommand ThayDoiDuLieuCommand { get; set; }
        public ICommand ThayDoiDuLieuSoCommand { get; set; }
        public ICommand LuuThayDoiCommand { get; set; }
        public ICommand DuyetFileCommand { get; set; }
        public ICommand PrevImageCommand { get; set; }
        public ICommand NextImageCommand { get; set; }
        public ICommand ThemAnhSPCommand { get; set; }
        public ICommand XoaHinhAnhSanPhamCommand { get; set; }
        public ICommand XoaCommand { get; set; }

        public CapNhatSanPhamViewModel()
        {
        }

        public CapNhatSanPhamViewModel(SanPhamHienThi sanPhamHienThi)
        {
            loadDataBinding(sanPhamHienThi);
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

            ThayDoiDuLieuCommand = new RelayCommand<Button>((p) => { return true; }, (p) => { p.IsEnabled = true; });
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

            PrevImageCommand = new RelayCommand<ListView>((p) => { return true; }, (p) => { 
                if(p.SelectedIndex > -1)
                {
                    if(p.SelectedIndex == 0 )
                    {
                        HinhAnhHienThi = HinhAnhSanPhams[HinhAnhSanPhams.Count - 1];
                    }
                    else
                    {
                        HinhAnhHienThi = HinhAnhSanPhams[p.SelectedIndex - 1];
                    }
                }
            });

            NextImageCommand = new RelayCommand<ListView>((p) => { return true; }, (p) => { 
                if(p.SelectedIndex > -1)
                {
                    if(p.SelectedIndex == HinhAnhSanPhams.Count - 1)
                    {
                        HinhAnhHienThi = HinhAnhSanPhams[0];
                    }
                    else
                    {
                        HinhAnhHienThi = HinhAnhSanPhams[p.SelectedIndex + 1];
                    }
                }
            });

            XoaHinhAnhSanPhamCommand = new RelayCommand<ListView>((p) => { return true; }, (p) => { 
                if(p.SelectedIndex > -1)
                {
                    ListHinhAnhBiXoa.Add(HinhAnhSanPhams[p.SelectedIndex]);
                    HinhAnhSanPhams.RemoveAt(p.SelectedIndex);
                    if(HinhAnhSanPhams.Count>0)
                    {
                        HinhAnhHienThi = HinhAnhSanPhams[0];
                    }
                    else
                    {
                        HinhAnhHienThi = new HinhAnhSanPham();
                        HinhAnhHienThi.HinhAnh = "Assets/image_not_available.png";
                    }
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
                    HinhAnh = img[0].ToString();
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
            #region LƯU CHỈNH SỬA
            LuuThayDoiCommand = new RelayCommand<Window>((p) => { return true; }, (p) =>
            {

                MessageBoxResult result = MessageBox.Show("Bạn có muốn lưu?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (HinhAnh != SanPham.HinhAnh)
                    {
                        HinhAnh = ImageDatabase.savingImageToDatabase(HinhAnh, ImageDatabase.PATH_PRODUCT);
                        SanPham.HinhAnh = HinhAnh;
                    }

                    SanPham.GiaBan = ConvertNumber.convertStringtoDecimal(GiaBan);
                    SanPham.GiaNhap = ConvertNumber.convertStringtoDecimal(GiaNhap);
                    SanPham.SoLuong = ConvertNumber.convertStringtoInt(SoLuong);
                    SanPham.IdDonViTinh = DonViTinh.Id;
                    SanPham.IdLoaiSanPham = LoaiSanPham.Id;
                    SanPham.IdNguonNhap = NguonNhap.Id;
                    SanPham.Ten = Ten;
                    SanPham.MoTa = MoTa;

                   

                    using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
                    {
                        foreach (var sp in db.SanPhams)
                        {
                            if (sp.Id == SanPham.Id)
                            {
                                sp.HinhAnh = HinhAnh;
                                sp.GiaBan = SanPham.GiaBan;
                                sp.GiaNhap = SanPham.GiaNhap;
                                sp.IdDonViTinh = DonViTinh.Id;
                                sp.IdLoaiSanPham = LoaiSanPham.Id;
                                sp.IdNguonNhap = NguonNhap.Id;
                                sp.Ten = Ten;
                                sp.MoTa = MoTa;
                                sp.SoLuong = SanPham.SoLuong;
                                break;
                            }
                        }


                        foreach (var i in ListHinhAnhBiXoa)
                        {
                            if (db.HinhAnhSanPhams.Where(ha => ha.Id == i.Id).Count() > 0)
                                db.HinhAnhSanPhams.Remove(db.HinhAnhSanPhams.Where(ha => ha.Id == i.Id).FirstOrDefault());
                        }

                        foreach (var i in HinhAnhSanPhams)
                        {
                            if (db.HinhAnhSanPhams.Where(ha => ha.Id == i.Id).Count() < 1)
                            {
                                i.HinhAnh = ImageDatabase.savingImageToDatabase(i.HinhAnh, ImageDatabase.PATH_PRODUCT);
                                db.HinhAnhSanPhams.Add(i);
                            }
                        }

                        db.SaveChanges();
                    }

                    SanPhamHienThi.SoLuong = SoLuong;
                    SanPhamHienThi.GiaNhap = GiaNhap;
                    SanPhamHienThi.GiaBan = GiaBan;
                    SanPhamHienThi.SanPham = SanPham;
                    SanPhamHienThi.SanPham.HinhAnh = Path.GetFullPath(SanPhamHienThi.SanPham.HinhAnh);

                    p.Close();
                }
            });
            #endregion

            XoaCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                SanPham.TrangThai = false;
                using (var db = new DBQuanLyCacDaiLyEntities())
                {
                    db.SanPhams.Where(sp => sp.Id == SanPham.Id).FirstOrDefault().TrangThai = false;
                    db.SaveChanges();
                }
                SanPhamHienThi.SanPham = SanPham;
                p.Close(); 
            });

            ThoatCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });
        }
        private void loadDataList()
        {
            ListHinhAnhBiXoa = new List<HinhAnhSanPham>();
            using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                LoaiSanPhams = new ObservableCollection<LoaiSanPham>(db.LoaiSanPhams);
                NguonNhaps = new ObservableCollection<NguonNhap>(db.NguonNhaps);
                DonViTinhs = new ObservableCollection<DonViTinh>(db.DonViTinhs);

                DonViTinh = DonViTinhs.Where(dv => dv.Id == SanPhamHienThi.DonViTinh.Id).FirstOrDefault();
                NguonNhap = NguonNhaps.Where(i => i.Id == SanPhamHienThi.NguonNhap.Id).FirstOrDefault();
                LoaiSanPham = LoaiSanPhams.Where(i => i.Id == SanPhamHienThi.LoaiSanPham.Id).FirstOrDefault();
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
        public void loadDataBinding(SanPhamHienThi sanPhamHienThi)
        {
            SanPhamHienThi = sanPhamHienThi;
            SanPham = SanPhamHienThi.SanPham;
            NguonNhap = SanPhamHienThi.NguonNhap;
            LoaiSanPham = SanPhamHienThi.LoaiSanPham;
            DonViTinh = SanPhamHienThi.DonViTinh;

            Ten = SanPham.Ten;
            GiaBan = SanPhamHienThi.GiaBan;
            GiaNhap = SanPhamHienThi.GiaNhap;
            SoLuong = SanPhamHienThi.SoLuong;
            MoTa = SanPham.MoTa;
            HinhAnh = SanPham.HinhAnh;

            using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                HinhAnhSanPhams = new ObservableCollection<HinhAnhSanPham>(
                    from ha in db.HinhAnhSanPhams
                    where ha.IdSanPham == SanPham.Id
                    select ha
                    );

            }
            foreach (var i in HinhAnhSanPhams)
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
            if (HinhAnhSanPhams.Count > 0)
                HinhAnhHienThi = HinhAnhSanPhams[0];
            else
                HinhAnhHienThi = new HinhAnhSanPham { HinhAnh = "Assets/image_not_available.png" };

        }
    }
}
