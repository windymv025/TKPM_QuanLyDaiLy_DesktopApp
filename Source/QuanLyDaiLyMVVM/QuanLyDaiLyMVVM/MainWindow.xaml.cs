using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyDaiLyMVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void bg_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_btn.IsChecked = false;
        }

        private void Image_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_btn.IsChecked = false;
        }

        private void ListViewItem_MouseEnter(object sender, MouseEventArgs e)
        {
            //Set tooltip
            if (Tg_btn.IsChecked == true)
            {
                tt_home.Visibility = Visibility.Collapsed;
                tt_daily.Visibility = Visibility.Collapsed;
                tt_sanpham.Visibility = Visibility.Collapsed;
                tt_phieuthutien.Visibility = Visibility.Collapsed;
                tt_phieuxuathang.Visibility = Visibility.Collapsed;
                tt_contact.Visibility = Visibility.Collapsed;
            }
            else
            {
                tt_home.Visibility = Visibility.Visible;
                tt_daily.Visibility = Visibility.Visible;
                tt_sanpham.Visibility = Visibility.Visible;
                tt_phieuthutien.Visibility = Visibility.Visible;
                tt_phieuxuathang.Visibility = Visibility.Visible;
                tt_contact.Visibility = Visibility.Visible;
            }
        }

        private void spDaiLyTop1_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void spDaiLyTop3_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void spDaiLyTop2_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void spSanPhamTop1_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void spSanPhamTop2_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void spSanPhamTop3_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void textbox_search_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Tg_btn_Unchecked(object sender, RoutedEventArgs e)
        {
            bg.Opacity = 1;
        }

        private void Tg_btn_Checked(object sender, RoutedEventArgs e)
        {
            bg.Opacity = 0.3;
        }
    }
}
