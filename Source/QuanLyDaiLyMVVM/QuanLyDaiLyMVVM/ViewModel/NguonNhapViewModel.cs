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
        private NguonNhap _NguonNhap;
        public NguonNhap NguonNhap { get => _NguonNhap; set { _NguonNhap = value; OnPropertyChanged(); } }

        private NguonNhap _SelectNguonNhap;
        public NguonNhap SelectNguonNhap { get => _SelectNguonNhap; set { _SelectNguonNhap = value; OnPropertyChanged(); } }

        private ObservableCollection<NguonNhap> _NguonNhaps;
        public ObservableCollection<NguonNhap> NguonNhaps { get => _NguonNhaps; set { _NguonNhaps = value; OnPropertyChanged(); } }

        public ICommand SelectionChangedCommand { get; set; }
        public ICommand ThayDoiDuLieuSoCommand { get; set; }
        public ICommand DuyetFileCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand RefreshCommand { get; set; }

        public NguonNhapViewModel()
        {
            loadData();
            SelectionChangedCommand = new RelayCommand<ListView>((p) => {
                return true;
            }, (p) => {
                NguonNhap = SelectNguonNhap;
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
                    NguonNhap.HinhAnh = img[0].ToString();
                }
            });

            AddCommand = new RelayCommand<Button>((p) => { return true; }, (p) => {
                MessageBoxResult result = MessageBox.Show("Bạn có muốn lưu?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (string.IsNullOrEmpty(NguonNhap.HinhAnh))
                    {
                        NguonNhap.HinhAnh = Path.GetFullPath("Assets/image_not_available.png");
                    }
                    if (string.IsNullOrEmpty(NguonNhap.Ten)
                        || string.IsNullOrEmpty(NguonNhap.DiaChi)
                        || string.IsNullOrEmpty(NguonNhap.SoDienThoai)
                        || NguonNhap.SoDienThoai.Length < 10)
                    {
                        MessageBox.Show("Bạn nhập thiếu thông tin nguồn nhập.", "Error Systerm", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }


                    var nguonNhap = new NguonNhap
                    {
                        HinhAnh = ImageDatabase.savingImageToDatabase(NguonNhap.HinhAnh, ImageDatabase.PATH_PRODUCT),
                        Ten = NguonNhap.Ten,
                        SoDienThoai = NguonNhap.SoDienThoai,
                        DiaChi = NguonNhap.DiaChi
                    };


                    using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
                    {
                        db.NguonNhaps.Add(nguonNhap);
                        db.SaveChanges();
                    }
                    nguonNhap.HinhAnh = Path.GetFullPath(nguonNhap.HinhAnh);
                    NguonNhaps.Add(nguonNhap);
                    MessageBox.Show(nguonNhap.Id + ""); 

                }
            });

            RefreshCommand = new RelayCommand<Button>((p) => { return true; }, (p) => {
                NguonNhap = new NguonNhap
                {
                    HinhAnh = "Assets/image_not_available.png"
                };
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
            NguonNhap = new NguonNhap
            {
                HinhAnh = "Assets/image_not_available.png"
            };
        }
    }
}
