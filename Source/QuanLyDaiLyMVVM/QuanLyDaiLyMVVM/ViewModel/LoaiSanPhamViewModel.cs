using Microsoft.Win32;
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
using System.Windows.Input;

namespace QuanLyDaiLyMVVM.ViewModel
{
    public class LoaiSanPhamViewModel: BaseViewModel
    {
        private string _Ten;
        public string Ten { get => _Ten; set { _Ten = value; OnPropertyChanged(); } }

        private string _HinhAnh;
        public string HinhAnh { get => _HinhAnh; set { _HinhAnh = value; OnPropertyChanged(); } }

        private LoaiSanPham _LoaiSanPham;
        public LoaiSanPham LoaiSanPham { get => _LoaiSanPham; set { _LoaiSanPham = value; OnPropertyChanged(); } }

        private ObservableCollection<LoaiSanPham> _LoaiSanPhams;
        public ObservableCollection<LoaiSanPham> LoaiSanPhams { get => _LoaiSanPhams; set { _LoaiSanPhams = value; OnPropertyChanged(); } }

        public ICommand SelectionChangedCommand { get; set; }
        public ICommand DuyetFileCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public LoaiSanPhamViewModel()
        {
            loadData();
            SelectionChangedCommand = new RelayCommand<ListView>((p) => {
                return true;
            }, (p) => {
                if (LoaiSanPham == null)
                    return;

                Ten = LoaiSanPham.Ten;
                HinhAnh = LoaiSanPham.HinhAnh;
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
                    if (string.IsNullOrEmpty(Ten))
                    {
                        MessageBox.Show("Bạn nhập thiếu thông tin nguồn nhập.", "Error Systerm", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }


                    var loaiSanPham = new LoaiSanPham
                    {
                        HinhAnh = ImageDatabase.savingImageToDatabase(HinhAnh, ImageDatabase.PATH_PRODUCT),
                        Ten = Ten
                    };


                    using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
                    {
                        db.LoaiSanPhams.Add(loaiSanPham);
                        db.SaveChanges();
                    }
                    loaiSanPham.HinhAnh = Path.GetFullPath(loaiSanPham.HinhAnh);
                    LoaiSanPhams.Add(loaiSanPham);

                    HinhAnh = "Assets/image_not_available.png";
                    Ten = "";
                }
            });

            RefreshCommand = new RelayCommand<Button>((p) => { return true; }, (p) => {
                HinhAnh = "Assets/image_not_available.png";
                Ten = "";
            });

            DeleteCommand = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                using (var db = new DBQuanLyCacDaiLyEntities())
                {
                    var loaiSanPham = db.LoaiSanPhams.Where(nn => nn.Id == LoaiSanPham.Id).FirstOrDefault();
                    if (loaiSanPham.SanPhams.Count() == 0)
                    {
                        db.LoaiSanPhams.Remove(loaiSanPham);
                        db.SaveChanges();
                        LoaiSanPhams.Remove(LoaiSanPham);

                        HinhAnh = "Assets/image_not_available.png";
                        Ten = "";
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
                if (LoaiSanPham == null)
                    return;
                MessageBoxResult result = MessageBox.Show("Bạn có muốn lưu?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (string.IsNullOrEmpty(HinhAnh))
                    {
                        HinhAnh = Path.GetFullPath("Assets/image_not_available.png");
                    }
                    if (string.IsNullOrEmpty(Ten))
                    {
                        MessageBox.Show("Bạn nhập thiếu thông tin nguồn nhập.", "Error Systerm", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
                    {
                        var loaiSanPham = db.LoaiSanPhams.Where(nn => nn.Id == LoaiSanPham.Id).FirstOrDefault();
                        if (loaiSanPham.HinhAnh == null || HinhAnh != Path.GetFullPath(loaiSanPham.HinhAnh))
                        {
                            loaiSanPham.HinhAnh = ImageDatabase.savingImageToDatabase(HinhAnh, ImageDatabase.PATH_PRODUCT);
                        }
                        loaiSanPham.Ten = Ten;

                        db.SaveChanges();
                    }

                    LoaiSanPham.HinhAnh = HinhAnh;
                    LoaiSanPham.Ten = Ten;
                }
            });

        }
        public void loadData()
        {
            using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                LoaiSanPhams = new ObservableCollection<LoaiSanPham>(db.LoaiSanPhams);
                foreach (var i in LoaiSanPhams)
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
