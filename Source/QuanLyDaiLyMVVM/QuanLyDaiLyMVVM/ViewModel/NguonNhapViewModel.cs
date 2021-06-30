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
    public class NguonNhapViewModel : BaseViewModel
    {
        private string _Ten;
        public string Ten { get => _Ten; set { _Ten = value; OnPropertyChanged(); } }

        private string _DiaChi;
        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }

        private string _SoDienThoai;
        public string SoDienThoai { get => _SoDienThoai; set { _SoDienThoai = value; OnPropertyChanged(); } }

        private string _HinhAnh;
        public string HinhAnh { get => _HinhAnh; set { _HinhAnh = value; OnPropertyChanged(); } }

        private NguonNhap _SelectNguonNhap;
        public NguonNhap SelectNguonNhap { get => _SelectNguonNhap; set { _SelectNguonNhap = value; OnPropertyChanged(); } }

        private ObservableCollection<NguonNhap> _NguonNhaps;
        public ObservableCollection<NguonNhap> NguonNhaps { get => _NguonNhaps; set { _NguonNhaps = value; OnPropertyChanged(); } }

        public ICommand SelectionChangedCommand { get; set; }
        public ICommand ThayDoiDuLieuSoCommand { get; set; }
        public ICommand DuyetFileCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public NguonNhapViewModel()
        {
            loadData();
            SelectionChangedCommand = new RelayCommand<ListView>((p) => {
                return true;
            }, (p) => {
                if (SelectNguonNhap == null)
                    return;

                Ten = SelectNguonNhap.Ten;
                SoDienThoai = SelectNguonNhap.SoDienThoai;
                DiaChi = SelectNguonNhap.DiaChi;
                HinhAnh = SelectNguonNhap.HinhAnh;
            });

            ThayDoiDuLieuSoCommand = new RelayCommand<TextBox>((p) => {
                return true;
            }, (p) => {
                int caretIndex = p.CaretIndex;

                p.Text = Regex.Replace(p.Text, "[^0-9]+", "");
                p.CaretIndex = caretIndex;
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

            AddCommand = new RelayCommand<Button>((p) => { return true; }, (p) => {
                MessageBoxResult result = MessageBox.Show("Bạn có muốn lưu?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (string.IsNullOrEmpty(HinhAnh))
                    {
                        HinhAnh = Path.GetFullPath("Assets/image_not_available.png");
                    }
                    if (string.IsNullOrEmpty(Ten)
                        || string.IsNullOrEmpty(DiaChi)
                        || string.IsNullOrEmpty(SoDienThoai)
                        || SoDienThoai.Length < 10)
                    {
                        MessageBox.Show("Bạn nhập thiếu thông tin nguồn nhập.", "Error Systerm", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }


                    var nguonNhap = new NguonNhap
                    {
                        HinhAnh = ImageDatabase.savingImageToDatabase(HinhAnh, ImageDatabase.PATH_PRODUCT),
                        Ten = Ten,
                        SoDienThoai = SoDienThoai,
                        DiaChi = DiaChi
                    };


                    using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
                    {
                        db.NguonNhaps.Add(nguonNhap);
                        db.SaveChanges();
                    }
                    nguonNhap.HinhAnh = Path.GetFullPath(nguonNhap.HinhAnh);
                    NguonNhaps.Add(nguonNhap);
                    //MessageBox.Show(nguonNhap.Id + ""); 
                    HinhAnh = "Assets/image_not_available.png";
                    Ten = "";
                    SoDienThoai = "";
                    DiaChi = "";
                }
            });

            RefreshCommand = new RelayCommand<Button>((p) => { return true; }, (p) => {
                HinhAnh = "Assets/image_not_available.png";
                Ten = "";
                SoDienThoai = "";
                DiaChi = "";
                
            });

            DeleteCommand = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                using (var db = new DBQuanLyCacDaiLyEntities())
                {
                    var nguonNhap = db.NguonNhaps.Where(nn => nn.Id == SelectNguonNhap.Id).FirstOrDefault();
                    if (nguonNhap.SanPhams.Count() == 0)
                    {
                        db.NguonNhaps.Remove(nguonNhap);
                        db.SaveChanges();
                        NguonNhaps.Remove(SelectNguonNhap);

                        HinhAnh = "Assets/image_not_available.png";
                        Ten = "";
                        SoDienThoai = "";
                        DiaChi = "";
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa nguồn nhập này do ràng buộc khóa ngoại.", "Error Systerm", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                
                
            });

            EditCommand = new RelayCommand<ListView>((p) => { return true; }, (p) =>
            {
                if (SelectNguonNhap == null)
                    return;
                MessageBoxResult result = MessageBox.Show("Bạn có muốn lưu?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (string.IsNullOrEmpty(HinhAnh))
                    {
                        HinhAnh = Path.GetFullPath("Assets/image_not_available.png");
                    }
                    if (string.IsNullOrEmpty(Ten)
                        || string.IsNullOrEmpty(DiaChi)
                        || string.IsNullOrEmpty(SoDienThoai)
                        || SoDienThoai.Length < 10)
                    {
                        MessageBox.Show("Bạn nhập thiếu thông tin nguồn nhập.", "Error Systerm", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
                    {
                        var nguonNhap = db.NguonNhaps.Where(nn => nn.Id == SelectNguonNhap.Id).FirstOrDefault();
                        if (nguonNhap.HinhAnh == null || HinhAnh != Path.GetFullPath(nguonNhap.HinhAnh))
                        {
                            nguonNhap.HinhAnh = ImageDatabase.savingImageToDatabase(HinhAnh, ImageDatabase.PATH_PRODUCT);
                        }
                            nguonNhap.Ten = Ten;
                        nguonNhap.SoDienThoai = SoDienThoai;
                        nguonNhap.DiaChi = DiaChi;

                        db.SaveChanges();
                    }
                    
                    SelectNguonNhap.HinhAnh = HinhAnh;
                    SelectNguonNhap.Ten = Ten;
                    SelectNguonNhap.SoDienThoai = SoDienThoai;
                    SelectNguonNhap.DiaChi = DiaChi;
                    
                }
            });

        }
        public void loadData()
        {
            using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                NguonNhaps = new ObservableCollection<NguonNhap>(db.NguonNhaps);
                foreach (var i in NguonNhaps)
                {
                    if (i.HinhAnh == null)
                    {
                        i.HinhAnh = Path.GetFullPath("Assets/image_not_available.png");
                    }
                    else
                        i.HinhAnh = Path.GetFullPath(i.HinhAnh);
                }
            }
            HinhAnh = "Assets/image_not_available.png";
        }
    }
}
