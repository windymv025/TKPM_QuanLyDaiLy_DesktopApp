using Microsoft.Win32;
using QuanLyDaiLyMVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace QuanLyDaiLyMVVM.ViewModel
{
    public class DaiLyViewModel : BaseViewModel
    {
        private ObservableCollection<DaiLy> _List;
        public ObservableCollection<DaiLy> List { get => _List; set { _List = value; OnPropertyChanged(); } }
        private DaiLy _SelectedItem;
        public DaiLy SelectedItem { get => _SelectedItem; 
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if(SelectedItem != null)
                {
                    Ten = SelectedItem.Ten;
                    DienThoai = SelectedItem.DienThoai;
                    DiaChi = SelectedItem.DiaChi;
                    NgayTiepNhan = SelectedItem.NgayTiepNhan;
                    Quan = SelectedItem.Quan;
                    Email = SelectedItem.Email;
                    HinhAnh = SelectedItem.HinhAnh;
                    SelectedLoaiDaiLy = SelectedItem.LoaiDaiLy;
                }
            } 
        }

        private string _Ten;
        public string Ten { get => _Ten; set { _Ten = value; OnPropertyChanged(); } }
        private string _DienThoai;
        public string DienThoai { get => _DienThoai; set { _DienThoai = value; OnPropertyChanged(); } }
        private string _DiaChi;
        public string DiaChi { get => _DiaChi; set { _DiaChi = value; OnPropertyChanged(); } }
        private DateTime _NgayTiepNhan;
        public DateTime NgayTiepNhan { get => _NgayTiepNhan; set { _NgayTiepNhan = value; OnPropertyChanged(); } }
        private string _Quan;
        public string Quan { get => _Quan; set { _Quan = value; OnPropertyChanged(); } }
        private ObservableCollection<Model.LoaiDaiLy> _LoaiDaiLy;
        public ObservableCollection<Model.LoaiDaiLy> LoaiDaiLy { get => _LoaiDaiLy; set { _LoaiDaiLy = value; OnPropertyChanged(); } }
        private string _Email;
        public string Email { get => _Email; set { _Email = value; OnPropertyChanged(); } }
        private string _HinhAnh;
        public string HinhAnh { get => _HinhAnh; set { _HinhAnh = value; OnPropertyChanged(); } }

        private Model.LoaiDaiLy _SelectedLoaiDaiLy;
        public Model.LoaiDaiLy SelectedLoaiDaiLy { get => _SelectedLoaiDaiLy; set { _SelectedLoaiDaiLy = value; OnPropertyChanged(); } }


        public ICommand ShowAddCommand { get; set; }
        public ICommand AddCommand { get; set; }

        public ICommand ShowAddLoaiDaiLyCommand { get; set; }
        public ICommand AddLoaiDaiLyCommand { get; set; }
        public ICommand TextChangedMaxMoneyCommand { get; set; }
        public ICommand ExitThemDaiLyCommand { get; set; }
        
        public ICommand ChooseImageCommand { get; set; }
        public ICommand ListSelectionChangedCommand { get; set; }
        


        public DaiLyViewModel()
        {
            using (var db = new DBQuanLyCacDaiLyEntities())
            {
                List = new ObservableCollection<DaiLy>(db.DaiLies);
                LoaiDaiLy = new ObservableCollection<LoaiDaiLy>(db.LoaiDaiLies);
            }

            ShowAddCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ThemDaiLyWindow wd = new ThemDaiLyWindow();
                wd.ShowDialog();
            });

            /*========================================================================================================*/
            #region LOẠI ĐẠI LÝ
            //show màn hình
            ShowAddLoaiDaiLyCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ThemLoaiDaiLyWindow wd = new ThemLoaiDaiLyWindow();
                wd.ShowDialog();
            });

            //text nhập toàn số
            TextChangedMaxMoneyCommand = new RelayCommand<ThemLoaiDaiLyWindow>((p) => { return true; }, (p) =>
            {
                p.LoaiDaiLy_textbox_SoTienNoToiDa.Text = Regex.Replace(p.LoaiDaiLy_textbox_SoTienNoToiDa.Text, "[^0-9]+", "");
            });

            //thêm
            AddLoaiDaiLyCommand = new RelayCommand<ThemLoaiDaiLyWindow>((p) => { return true; }, (p) =>
            {
                var category = new LoaiDaiLy() { Ten = p.TenLoaiDaiLyMoi.Text.Trim(), SoTienNoToiDa = Decimal.Parse(p.LoaiDaiLy_textbox_SoTienNoToiDa.Text.Trim()) };
                using (var db = new DBQuanLyCacDaiLyEntities())
                {
                    if (db.LoaiDaiLies.Where(x => x.Ten == p.TenLoaiDaiLyMoi.Text.Trim()).Count() > 0 || string.IsNullOrEmpty(p.TenLoaiDaiLyMoi.Text.Trim()))
                    {
                        MessageBox.Show("Tên loại không phù hợp");
                        return;
                    }
                    db.LoaiDaiLies.Add(category);
                    db.SaveChanges();
                }
                LoaiDaiLy.Add(category);
                p.Close();
            });

            //Thoát
            ExitThemDaiLyCommand = new RelayCommand<ThemLoaiDaiLyWindow>((p) => { return true; }, (p) =>
            {
                p.Close();
            });
            #endregion



            ChooseImageCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Multiselect = false;
                open.Filter = "Image Files(*.jpg; *.png; *.jpeg; *.gif; *.bmp)|*.jpg; *.png; *.jpeg; *.gif; *.bmp";
                bool? result = open.ShowDialog();
                if (result == true)
                {
                    var img = open.FileNames;
                    //ImageSource imageSource = new BitmapImage(new Uri(img[0].ToString()));
                    //avatar_DaiLy.Source = imageSource;
                    //avatar_DaiLy.Visibility = Visibility.Visible;
                    HinhAnh = img[0].ToString();
                }
            });

            ListSelectionChangedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ThemDaiLyWindow wd = new ThemDaiLyWindow();
                wd.DataContext = this;
                wd.ShowDialog();
            });
        }
    }
}
