using AppQuanLyDaiLy.ViewModels;
using Microsoft.Win32;
using QuanLyDaiLy.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLyDaiLy
{
    /// <summary>
    /// Interaction logic for ScreenThemDaiLy.xaml
    /// </summary>
    public partial class ScreenThemDaiLy : Window
    {
        HomeViewModel viewModel = new HomeViewModel();
        public ScreenThemDaiLy()
        {
            InitializeComponent();
        }

        private void cbb_loaidaily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            placeHolderCategory.Visibility = Visibility.Collapsed;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cbb_loaidaily.ItemsSource = viewModel.getAllLoaiDaiLy();
        }

        private void loadData()
        {
            cbb_loaidaily.ItemsSource = viewModel.getAllLoaiDaiLy();
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = false;
            open.Filter = "Image Files(*.jpg; *.png; *.jpeg; *.gif; *.bmp)|*.jpg; *.png; *.jpeg; *.gif; *.bmp";
            bool? result = open.ShowDialog();
            if (result == true)
            {
                var img = open.FileNames;
                ImageSource imageSource = new BitmapImage(new Uri(img[0].ToString()));
                avatar_DaiLy.Source = imageSource;
                avatar_DaiLy.Visibility = Visibility.Visible;
            }
        }

        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            DaiLy daiLy = new DaiLy();
            bool canSave = true;
            StringBuilder noti = new StringBuilder();
            if (DaiLy_textbox_name.Text.Trim().ToString().Equals(""))
            {
                canSave = false;
                noti.Append("Tên, ");
            }
            if (DaiLy_textbox_phone.Text.Trim().ToString().Equals(""))
            {
                canSave = false;
                noti.Append("Số điện thoại, ");
            }
            if (DaiLy_textbox_address.Text.Trim().ToString().Equals(""))
            {
                canSave = false;
                noti.Append("Địa chỉ, ");
            }
            if (DaiLy_textbox_Date.Text.Trim().ToString().Equals(""))
            {
                canSave = false;
                noti.Append("Ngày tiếp nhận, ");
            }
            if (DaiLy_textbox_district.Text.Trim().ToString().Equals(""))
            {
                canSave = false;
                noti.Append("Quận, ");
            }
            if (DaiLy_textbox_email.Text.Trim().ToString().Equals(""))
            {
                canSave = false;
                noti.Append("Email, ");
            }
            if(cbb_loaidaily.Text.Trim() == "" && cbb_loaidaily.SelectedIndex == -1)
            {
                canSave = false;
                noti.Append("Loại đại lý, ");
            }
            if(avatar_DaiLy.Source == null)
            {
                canSave = false;
                noti.Append("Hình ảnh, ");
            }

            if (noti.Length != 0)
            {
                noti.Remove(noti.ToString().LastIndexOf(","), 1);
            }

            if (canSave)
            {
                MessageBoxResult result = MessageBox.Show("Bạn có muốn lưu?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    var currentFolder = AppDomain.CurrentDomain.BaseDirectory.ToString();
                    string uriImage = currentFolder.ToString();
                    string file = avatar_DaiLy.Source.ToString().Substring(8);


                    //Lấy file ảnh copy vào Images của project
                    var info = new FileInfo(file);
                    var newName = $"{Guid.NewGuid()}{info.Extension}";
                    File.Copy(file, $"{uriImage}Images\\DaiLy\\{newName}");

                    daiLy.HinhAnh = $"Images/Daily/{newName}";

                    daiLy.Ten = DaiLy_textbox_name.Text.Trim();
                    daiLy.DienThoai = DaiLy_textbox_phone.Text.Trim();
                    daiLy.DiaChi = DaiLy_textbox_address.Text.Trim();
                    daiLy.NgayTiepNhan = DateTime.ParseExact(DaiLy_textbox_Date.Text.Trim(),"yyyy/MM/dd", CultureInfo.InvariantCulture);
                    daiLy.Quan = DaiLy_textbox_district.Text.Trim();
                    daiLy.Email = DaiLy_textbox_email.Text.Trim();
                    daiLy.LoaiDaiLy = (cbb_loaidaily.SelectedItem as LoaiDaiLy).MaLoai;

                    viewModel.AddDaiLy(daiLy);

                    this.Close();
                }
            }
            else
            {
                MessageBox.Show($"Thông tin {noti}của đại lý chưa hợp lệ!!!", "Thông tin không hợp lệ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DaiLy_textbox_phone_TextChanged(object sender, TextChangedEventArgs e)
        {
            DaiLy_textbox_phone.Text = Regex.Replace(DaiLy_textbox_phone.Text, "[^0-9]+", "");
        }

        private void btn_ThemLoaiDaiLy_Click(object sender, RoutedEventArgs e)
        {
            ScreenPopupThemLoaiDaiLy screenPopupThemLoaiDaiLy = new ScreenPopupThemLoaiDaiLy();
            screenPopupThemLoaiDaiLy.ShowDialog();
            loadData();
        }
    }
}
