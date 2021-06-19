using AppQuanLyDaiLy.ViewModels;
using QuanLyDaiLy.Model;
using QuanLyDaiLy.ViewModels;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for ScreenPopupThemLoaiDaiLy.xaml
    /// </summary>
    public partial class ScreenPopupThemLoaiDaiLy : Window
    {
        DaiLyViewModel viewModel = new DaiLyViewModel();
        public ScreenPopupThemLoaiDaiLy()
        {
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {
            LoaiDaiLy loaiDaiLy = new LoaiDaiLy();
            bool canSave = true;
            string noti = "";
            if (LoaiDaiLy_textbox_SoTienNoToiDa.Text.Trim().Equals(""))
            {
                canSave = false;
                noti = "Nhập số tiền nợ tối đa cho loại đại lý mới.";
            }

            if (canSave)
            {
                MessageBoxResult result = MessageBox.Show($"Bạn có muốn lưu loại đại lý {DaiLyDAO.getAllLoaiDaiLy().Count + 1}?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                loaiDaiLy.MaLoai = DaiLyDAO.getAllLoaiDaiLy().Count + 1;
                loaiDaiLy.SoTienNoToiDa = decimal.Parse(LoaiDaiLy_textbox_SoTienNoToiDa.Text.Trim());

                viewModel.AddLoaiDaiLy(loaiDaiLy);
                this.Close();
            }
            else
            {
                MessageBox.Show(noti, "Thông tin không hợp lệ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LoaiDaiLy_textbox_SoTienNoToiDa_TextChanged(object sender, TextChangedEventArgs e)
        {
            LoaiDaiLy_textbox_SoTienNoToiDa.Text = Regex.Replace(LoaiDaiLy_textbox_SoTienNoToiDa.Text, "[^0-9]+", "");
        }
    }
}
