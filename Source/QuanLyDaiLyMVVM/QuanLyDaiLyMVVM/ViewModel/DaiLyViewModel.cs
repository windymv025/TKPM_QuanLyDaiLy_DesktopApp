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
                    SelectedLoaiDaiLy = LoaiDaiLy.Where(x => x.Id == (SelectedItem.IdLoaiDaiLy)).FirstOrDefault();
                }
            } 
        }

        private LoaiDaiLy _SelectedItemLoaiDaiLy;
        public LoaiDaiLy SelectedItemLoaiDaiLy { get => _SelectedItemLoaiDaiLy; set { 
                _SelectedItemLoaiDaiLy = value; OnPropertyChanged();
                if(SelectedItemLoaiDaiLy != null)
                {
                    TenLoaiDaiLy = SelectedItemLoaiDaiLy.Ten;
                    SoTienNoToiDa = SelectedItemLoaiDaiLy.SoTienNoToiDa;
                }
            }
        }

        private DateTime _NgayTiepNhan_Them;
        public DateTime NgayTiepNhan_Them { get => _NgayTiepNhan_Them; set { _NgayTiepNhan_Them = value; OnPropertyChanged(); } }

        private string _TenLoaiDaiLy;
        public string TenLoaiDaiLy { get => _TenLoaiDaiLy; set { _TenLoaiDaiLy = value; OnPropertyChanged(); } }
        private decimal _SoTienNoToiDa;
        public decimal SoTienNoToiDa { get => _SoTienNoToiDa; set { _SoTienNoToiDa = value; OnPropertyChanged(); } }

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
        public ICommand EditLoaiDaiLyCommand { get; set; }
        public ICommand DeleteLoaiDaiLyCommand { get; set; }
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
            NgayTiepNhan_Them = DateTime.Now;

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

                        double? count = db.QuyDinhs.Where(x => x.TenQuyDinh == "SO_LUONG_DAI_LY_TOI_DA_TRONG_MOT_QUAN").FirstOrDefault()?.GiaTri;
                        if(count == null)
                        {
                            count = 4;
                        }

                        if(count > db.DaiLies.Where(x=>x.Quan == p.DaiLy_textbox_district.Text.Trim()).Count())
                        {
                            db.DaiLies.Add(daiLy);
                            db.SaveChanges();
                            List.Add(daiLy);
                        }
                        else
                        {
                            MessageBox.Show("Số lượng đại lý trong một quận vượt quá giới hạn. Bạn có thể vào Quy định cấu hình lại mục SO_LUONG_DAI_LY_TOI_DA_TRONG_MOT_QUAN để sửa đổi nếu cần thiết");
                            return;
                        }
                        
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
                //p.LoaiDaiLy_textbox_SoTienNoToiDa.Text = Regex.Replace(p.LoaiDaiLy_textbox_SoTienNoToiDa.Text, "[^0-9]+", "");

                int caretIndex = p.LoaiDaiLy_textbox_SoTienNoToiDa.CaretIndex;
                p.LoaiDaiLy_textbox_SoTienNoToiDa.Text = Regex.Replace(p.LoaiDaiLy_textbox_SoTienNoToiDa.Text, "[^0-9.]+", "");
                string numberString = p.LoaiDaiLy_textbox_SoTienNoToiDa.Text;
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
                p.LoaiDaiLy_textbox_SoTienNoToiDa.Text = ConvertNumber.convertNumberDecimalToString(number);

                if (p.LoaiDaiLy_textbox_SoTienNoToiDa.Text.Length == numberString.Length + 1)
                {
                    caretIndex++;
                }
                if (p.LoaiDaiLy_textbox_SoTienNoToiDa.Text.Length == numberString.Length - 1)
                {
                    caretIndex--;
                }
                if (caretIndex < 0)
                {
                    caretIndex = 0;
                }
                p.LoaiDaiLy_textbox_SoTienNoToiDa.CaretIndex = caretIndex;
            });

            //thêm
            AddLoaiDaiLyCommand = new RelayCommand<ThemLoaiDaiLyWindow>((p) => {
                if (p == null)
                    return false;
                else
                {
                    using (var db = new DBQuanLyCacDaiLyEntities())
                    {
                        if (db.LoaiDaiLies.Where(x => x.Ten == TenLoaiDaiLy).Count() > 0 || string.IsNullOrEmpty(p.tenldl.Text.Trim()) || string.IsNullOrEmpty(p.LoaiDaiLy_textbox_SoTienNoToiDa.Text.Trim()))
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }

                    }
                }
            }, (p) =>
            {
                var category = new LoaiDaiLy() { Ten = TenLoaiDaiLy, SoTienNoToiDa = SoTienNoToiDa };
                using (var db = new DBQuanLyCacDaiLyEntities())
                {
                    double? count = count = db.QuyDinhs.Where(x => x.TenQuyDinh == "SO_LUONG_LOAI_DAI_LY").FirstOrDefault()?.GiaTri;
                    if(count == null)
                    {
                        count = 2;
                    }
                    
                    if ((int)count > db.LoaiDaiLies.Count())
                    {
                        db.LoaiDaiLies.Add(category);
                        db.SaveChanges();
                        LoaiDaiLy.Add(category);
                    }
                    else
                    {
                        MessageBox.Show("Vượt quá số lượng loại đại lý quy định");
                        return;
                    }
                }
                
            });
            EditLoaiDaiLyCommand = new RelayCommand<ThemLoaiDaiLyWindow>((p) => {
                if (p == null || SelectedItemLoaiDaiLy == null || string.IsNullOrEmpty(p.tenldl.Text.Trim()) || string.IsNullOrEmpty(p.LoaiDaiLy_textbox_SoTienNoToiDa.Text.Trim()))
                    return false;
                else
                {
                    using (var db = new DBQuanLyCacDaiLyEntities())
                    {
                        if (db.LoaiDaiLies.Where(x => x.Ten == TenLoaiDaiLy).Count() > 0)
                        {
                            if(TenLoaiDaiLy == SelectedItemLoaiDaiLy.Ten)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return true;
                        }

                    }
                }
            }, (p) =>
            {
                using (var db = new DBQuanLyCacDaiLyEntities())
                {
                    var item = db.LoaiDaiLies.Where(x => x.Id == SelectedItemLoaiDaiLy.Id).FirstOrDefault();
                    item.Ten = TenLoaiDaiLy;
                    item.SoTienNoToiDa = SoTienNoToiDa;
                    db.SaveChanges();
                }
                SelectedItemLoaiDaiLy.Ten = Ten;
                SelectedItemLoaiDaiLy.SoTienNoToiDa = SoTienNoToiDa;
            });
            DeleteLoaiDaiLyCommand = new RelayCommand<ThemLoaiDaiLyWindow>((p) => {
                if (p == null || SelectedItemLoaiDaiLy == null)
                    return false;
                else
                {
                    return true;
                }
            }, (p) =>
            {
                using (var db = new DBQuanLyCacDaiLyEntities())
                {
                    if (db.DaiLies.Where(x=>x.IdLoaiDaiLy == SelectedItemLoaiDaiLy.Id).Count() > 0)
                    {
                        MessageBox.Show("Có đại lý đang thuộc loại này nên không thể xóa.");
                        return;
                    }
                    else
                    {
                        db.LoaiDaiLies.Remove(db.LoaiDaiLies.Where(x=>x.Ten == SelectedItemLoaiDaiLy.Ten).FirstOrDefault());
                        db.SaveChanges();
                    }
                }
                LoaiDaiLy.Remove(SelectedItemLoaiDaiLy);
                SelectedItemLoaiDaiLy = null;
            });

            //Thoát
            ExitThemLoaiDaiLyCommand = new RelayCommand<ThemLoaiDaiLyWindow>((p) => { if (p == null) return false; else return true; }, (p) =>
            {
                p.Close();
            });
            #endregion


            #region CẬP NHẬT ĐẠI LÝ
            UpdateCommand = new RelayCommand<CapNhatDaiLyWindow>((p) => {
                if (p == null || SelectedItem == null)
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
                MessageBoxResult result = MessageBox.Show("Bạn có muốn lưu thay đổi?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    using (var db = new DBQuanLyCacDaiLyEntities())
                    {
                        double? count = db.QuyDinhs.Where(x => x.TenQuyDinh == "SO_LUONG_DAI_LY_TOI_DA_TRONG_MOT_QUAN").FirstOrDefault()?.GiaTri;
                        if (count == null)
                        {
                            count = 4;
                        }

                        if (count > db.DaiLies.Where(x => x.Quan == p.DaiLy_textbox_district.Text.Trim()).Count())
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

                            //SelectedItem.HinhAnh = HinhAnh;
                            SelectedItem.IdLoaiDaiLy = SelectedLoaiDaiLy.Id;
                            SelectedItem.LoaiDaiLy = SelectedLoaiDaiLy;
                        }
                        else
                        {
                            MessageBox.Show("Số lượng đại lý trong một quận vượt quá giới hạn. Bạn có thể vào Quy định cấu hình lại mục SO_LUONG_DAI_LY_TOI_DA_TRONG_MOT_QUAN để sửa đổi nếu cần thiết");
                        }

                    }
                    p.Close();
                }
                    
            });
            ExitUpdateCommand = new RelayCommand<Window>((p) => { if (p == null) return false; else return true; }, (p) =>
            {
                p.Close();
            });
            #endregion


            ListSelectionChangedCommand = new RelayCommand<object>((p) => { 
                if(SelectedItem == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }, (p) =>
            {
                SelectedLoaiDaiLy = LoaiDaiLy.Where(x => x.Id == (SelectedItem.IdLoaiDaiLy)).FirstOrDefault();
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
                MessageBoxResult result = MessageBox.Show("Bạn có muốn xóa đại lý?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    using (var db = new DBQuanLyCacDaiLyEntities())
                    {
                        var item = db.DaiLies.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                        item.IsRemove = true;
                        db.SaveChanges();
                        List.Remove(SelectedItem);
                        p.Close();
                    }
                }
            });

            SearchTextChangedCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                using (var db = new DBQuanLyCacDaiLyEntities())
                {
                    if (string.IsNullOrEmpty(TextSearch))
                    {
                        var List1 = new ObservableCollection<DaiLy>(db.DaiLies.Where(x => x.IsRemove == false).ToList());
                        List = List1;
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
