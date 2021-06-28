﻿using Microsoft.Win32;
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
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Data.Entity;

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
                    HinhAnh = Path.GetFullPath(SelectedItem.HinhAnh);
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
        private string _TextSearch;
        public string TextSearch { get => _TextSearch; set { _TextSearch = value; OnPropertyChanged(); } }


        public ICommand ShowAddCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand TextChangedSDTCommand { get; set; }
        public ICommand SearchTextChangedCommand { get; set; }
        public ICommand ExitThemDaiLyCommand { get; set; }

        public ICommand ShowAddLoaiDaiLyCommand { get; set; }
        public ICommand AddLoaiDaiLyCommand { get; set; }
        public ICommand TextChangedMaxMoneyCommand { get; set; }
        public ICommand ExitThemLoaiDaiLyCommand { get; set; }
        
        public ICommand ChooseImageCommand { get; set; }
        public ICommand ChooseImageUpdateCommand { get; set; }
        public ICommand ListSelectionChangedCommand { get; set; }

        public ICommand ExitUpdateCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }



        public DaiLyViewModel()
        {
            //load data
            using (var db = new DBQuanLyCacDaiLyEntities())
            {
                List = new ObservableCollection<DaiLy>(db.DaiLies.Where(x => x.IsRemove == false));
                LoaiDaiLy = new ObservableCollection<LoaiDaiLy>(db.LoaiDaiLies);
            }

            /*========================================================================================================*/
            #region THÊM ĐẠI LÝ
            //Show window
            ShowAddCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ThemDaiLyWindow wd = new ThemDaiLyWindow();
                wd.ShowDialog();
            });

            //Duyệt ảnh
            ChooseImageCommand = new RelayCommand<ThemDaiLyWindow>((p) => { return true; }, (p) =>
            {
                OpenFileDialog open = new OpenFileDialog();
                open.Multiselect = false;
                open.Filter = "Image Files(*.jpg; *.png; *.jpeg; *.gif; *.bmp)|*.jpg; *.png; *.jpeg; *.gif; *.bmp";
                bool? result = open.ShowDialog();
                if (result == true)
                {
                    var img = open.FileNames;
                    p.avatar_DaiLy.Source = new BitmapImage(new Uri(img[0].ToString()));
                    //HinhAnh = img[0].ToString();
                }
            });

            ChooseImageUpdateCommand = new RelayCommand<ThemDaiLyWindow>((p) => { return true; }, (p) =>
            {
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

            //text nhập toàn số
            TextChangedSDTCommand = new RelayCommand<ThemDaiLyWindow>((p) => { if (p == null) return false; else return true; }, (p) =>
            {
                p.DaiLy_textbox_phone.Text = Regex.Replace(p.DaiLy_textbox_phone.Text, "[^0-9]+", "");
            });

            //Thêm đại lý
            AddCommand = new RelayCommand<ThemDaiLyWindow>((p) => 
            {
                if (p == null)
                {
                    return false;
                }
                else
                {
                    if (string.IsNullOrEmpty(p.DaiLy_textbox_name.Text.Trim()) || string.IsNullOrEmpty(p.DaiLy_textbox_phone.Text.Trim()) || string.IsNullOrEmpty(p.DaiLy_textbox_address.Text.Trim()) || string.IsNullOrEmpty(p.DaiLy_textbox_district.Text.Trim()) || string.IsNullOrEmpty(p.DaiLy_textbox_email.Text.Trim()) || !Regex.IsMatch(p.DaiLy_textbox_email.Text.Trim().ToString(), "^[\\w-_\\.+]*[\\w-_\\.]\\@([\\w]+\\.)+[\\w]+[\\w]$") || p.cbb_loaidaily.SelectedIndex == -1 || p.cbb_loaidaily.Text.Trim() == "" || p.avatar_DaiLy.Source == null)
                    {
                        return false;
                    }
                    return true;
                }
            }, (p) =>
            {
                MessageBoxResult result = MessageBox.Show("Bạn có muốn lưu?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    var currentFolder = AppDomain.CurrentDomain.BaseDirectory.ToString();
                    string uriImage = currentFolder.ToString();
                    string file = p.avatar_DaiLy.Source.ToString().Substring(8);


                    //Lấy file ảnh copy vào Images của project
                    var info = new FileInfo(file);
                    var newName = $"{Guid.NewGuid()}{info.Extension}";
                    File.Copy(file, $"{uriImage}Images\\DaiLy\\{newName}");

                    var daiLy = new DaiLy();
                    daiLy.HinhAnh = $"Images/Daily/{newName}";

                    daiLy.Ten = p.DaiLy_textbox_name.Text.Trim();
                    daiLy.DienThoai = p.DaiLy_textbox_phone.Text.Trim();
                    daiLy.DiaChi = p.DaiLy_textbox_address.Text.Trim();
                    daiLy.NgayTiepNhan = p.DaiLy_Date.SelectedDate ?? DateTime.Now;
                    daiLy.Quan = p.DaiLy_textbox_district.Text.Trim();
                    daiLy.Email = p.DaiLy_textbox_email.Text.Trim();
                    daiLy.IdLoaiDaiLy = (p.cbb_loaidaily.SelectedItem as Model.LoaiDaiLy).Id;
                    //daiLy.LoaiDaiLy = LoaiDaiLy.Where(x => x.Id == (p.cbb_loaidaily.SelectedItem as Model.LoaiDaiLy).Id).SingleOrDefault();


                    using (var db = new DBQuanLyCacDaiLyEntities())
                    {
                        daiLy.LoaiDaiLy = db.LoaiDaiLies.Where(x => x.Id == daiLy.IdLoaiDaiLy).SingleOrDefault();
                        db.DaiLies.Add(daiLy);
                        db.SaveChanges();
                        List.Add(daiLy);
                    }

                    p.Close();
                }
            });

            ExitThemDaiLyCommand = new RelayCommand<Window>((p) => { if (p == null) return false; else return true; }, (p) =>
            {
                p.Close();
            });
            #endregion

            /*========================================================================================================*/
            #region LOẠI ĐẠI LÝ
            //show màn hình
            ShowAddLoaiDaiLyCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ThemLoaiDaiLyWindow wd = new ThemLoaiDaiLyWindow();
                wd.ShowDialog();
            });

            //text nhập toàn số
            TextChangedMaxMoneyCommand = new RelayCommand<ThemLoaiDaiLyWindow>((p) => { if (p == null) return false; else return true; }, (p) =>
            {
                p.LoaiDaiLy_textbox_SoTienNoToiDa.Text = Regex.Replace(p.LoaiDaiLy_textbox_SoTienNoToiDa.Text, "[^0-9]+", "");
            });

            //thêm
            AddLoaiDaiLyCommand = new RelayCommand<ThemLoaiDaiLyWindow>((p) => { if (p == null) return false; else return true; }, (p) =>
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
            ExitThemLoaiDaiLyCommand = new RelayCommand<ThemLoaiDaiLyWindow>((p) => { if (p == null) return false; else return true; }, (p) =>
            {
                p.Close();
            });
            #endregion


            #region CẬP NHẬT ĐẠI LÝ
            UpdateCommand = new RelayCommand<CapNhatDaiLyWindow>((p) => {
                if (p == null)
                {
                    return false;
                }
                else
                {
                    if (string.IsNullOrEmpty(p.DaiLy_textbox_name.Text.Trim()) || string.IsNullOrEmpty(p.DaiLy_textbox_phone.Text.Trim()) || string.IsNullOrEmpty(p.DaiLy_textbox_address.Text.Trim()) || string.IsNullOrEmpty(p.DaiLy_textbox_district.Text.Trim()) || string.IsNullOrEmpty(p.DaiLy_textbox_email.Text.Trim()) || !Regex.IsMatch(p.DaiLy_textbox_email.Text.Trim().ToString(), "^[\\w-_\\.+]*[\\w-_\\.]\\@([\\w]+\\.)+[\\w]+[\\w]$") || p.cbb_loaidaily.SelectedIndex == -1 || p.cbb_loaidaily.Text.Trim() == "" || p.avatar_DaiLy.Source == null)
                    {
                        return false;
                    }
                    return true;
                }
            }, (p) =>
            {
                using (var db = new DBQuanLyCacDaiLyEntities())
                {
                    var dl = db.DaiLies.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                    dl.Ten = Ten;
                    dl.DienThoai = DienThoai;
                    dl.DiaChi = DiaChi;
                    dl.NgayTiepNhan = NgayTiepNhan;
                    dl.Quan = Quan;
                    dl.Email = Email;

                    var currentFolder = AppDomain.CurrentDomain.BaseDirectory.ToString();
                    string uriImage = currentFolder.ToString();
                    //string file = p.avatar_DaiLy.Source.ToString().Substring(8);
                    string file = HinhAnh;
                    var info = new FileInfo(file);
                    var newName = $"{Guid.NewGuid()}{info.Extension}";
                    File.Copy(file, $"{uriImage}Images\\DaiLy\\{newName}");
                    HinhAnh = $"Images/DaiLy/{newName}";
                    dl.HinhAnh = HinhAnh;

                    dl.IdLoaiDaiLy = SelectedLoaiDaiLy.Id;

                    dl.LoaiDaiLy = db.LoaiDaiLies.Where(x => x.Id == dl.IdLoaiDaiLy).SingleOrDefault();

                    db.SaveChanges();

                    SelectedItem.Ten = Ten;
                    SelectedItem.DienThoai = DienThoai;
                    SelectedItem.DiaChi = DiaChi;
                    SelectedItem.NgayTiepNhan = NgayTiepNhan;
                    SelectedItem.Quan = Quan;
                    SelectedItem.Email = Email;

                    var info1 = new FileInfo(uriImage).ToString();


                    //SelectedItem.HinhAnh = $"{info1}{HinhAnh}";
                    SelectedItem.HinhAnh = Path.GetFullPath(HinhAnh);

                    SelectedItem.HinhAnh = HinhAnh;
                    SelectedItem.LoaiDaiLy = SelectedLoaiDaiLy;
                }
                p.Close();
            });
            ExitUpdateCommand = new RelayCommand<Window>((p) => { if (p == null) return false; else return true; }, (p) =>
            {
                p.Close();
            });
            #endregion


            ListSelectionChangedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CapNhatDaiLyWindow wd = new CapNhatDaiLyWindow();
                wd.DataContext = this;
                wd.ShowDialog();
            });

            DeleteCommand = new RelayCommand<Window>((p) => {
                if (SelectedItem == null || p == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }, (p) =>
            {
                using(var db = new DBQuanLyCacDaiLyEntities())
                {
                    var item = db.DaiLies.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                    item.IsRemove = true;
                    db.SaveChanges();
                    p.Close();
                    //List = new ObservableCollection<DaiLy>(db.DaiLies.Where(x => x.IsRemove == false).ToList());
                }
            });

            SearchTextChangedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                using (var db = new DBQuanLyCacDaiLyEntities())
                {
                    if (string.IsNullOrEmpty(TextSearch))
                    {
                        List = new ObservableCollection<DaiLy>(db.DaiLies.Where(x => x.IsRemove == false).ToList());
                    }
                    else
                    {
                        string sql = $"select* from DaiLy where freetext(Ten, N'{TextSearch}')";
                        List = new ObservableCollection<DaiLy>(db.DaiLies.SqlQuery(sql).ToList());
                    }
                }
            });
        }
    }
}
