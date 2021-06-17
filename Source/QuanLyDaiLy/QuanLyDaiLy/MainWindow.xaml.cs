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

namespace QuanLyDaiLy
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

        private void Tg_btn_Unchecked(object sender, RoutedEventArgs e)
        {
            bg.Opacity = 1;
            contact_screen.Opacity = 1;
        }

        private void Tg_btn_Checked(object sender, RoutedEventArgs e)
        {
            bg.Opacity = 0.3;
            contact_screen.Opacity = 0.3;
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

        private void Image_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bg_home.Visibility = Visibility.Visible;
            bg_daily.Visibility = Visibility.Collapsed;
            bg_sanpham.Visibility = Visibility.Collapsed;
            bg_phieuthutien.Visibility = Visibility.Collapsed;
            bg_phieuxuathang.Visibility = Visibility.Collapsed;
            contact_screen.Visibility = Visibility.Collapsed;
            Tg_btn.IsChecked = false;
        }

        private void img_daily_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bg_home.Visibility = Visibility.Collapsed;
            bg_daily.Visibility = Visibility.Visible;
            bg_sanpham.Visibility = Visibility.Collapsed;
            bg_phieuthutien.Visibility = Visibility.Collapsed;
            bg_phieuxuathang.Visibility = Visibility.Collapsed;
            contact_screen.Visibility = Visibility.Collapsed;
            Tg_btn.IsChecked = false;
        }

        private void img_sanpham_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bg_home.Visibility = Visibility.Collapsed;
            bg_daily.Visibility = Visibility.Collapsed;
            bg_sanpham.Visibility = Visibility.Visible;
            bg_phieuthutien.Visibility = Visibility.Collapsed;
            bg_phieuxuathang.Visibility = Visibility.Collapsed;
            contact_screen.Visibility = Visibility.Collapsed;
            Tg_btn.IsChecked = false;
        }

        private void img_phieuthutien_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bg_home.Visibility = Visibility.Collapsed;
            bg_daily.Visibility = Visibility.Collapsed;
            bg_sanpham.Visibility = Visibility.Collapsed;
            bg_phieuthutien.Visibility = Visibility.Visible;
            bg_phieuxuathang.Visibility = Visibility.Collapsed;
            contact_screen.Visibility = Visibility.Collapsed;
            Tg_btn.IsChecked = false;
        }

        private void img_phieuxuathang_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bg_home.Visibility = Visibility.Collapsed;
            bg_daily.Visibility = Visibility.Collapsed;
            bg_sanpham.Visibility = Visibility.Collapsed;
            bg_phieuthutien.Visibility = Visibility.Collapsed;
            bg_phieuxuathang.Visibility = Visibility.Visible;
            contact_screen.Visibility = Visibility.Collapsed;
            Tg_btn.IsChecked = false;
        }

        private void img_contact_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bg_home.Visibility = Visibility.Collapsed;
            bg_daily.Visibility = Visibility.Collapsed;
            bg_sanpham.Visibility = Visibility.Collapsed;
            bg_phieuthutien.Visibility = Visibility.Collapsed;
            bg_phieuxuathang.Visibility = Visibility.Collapsed;
            contact_screen.Visibility = Visibility.Visible;
            Tg_btn.IsChecked = false;
        }

        private void bg_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Tg_btn.IsChecked = false;
        }

        private void textbox_search_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DaiLy_button_Search_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void DaiLy_textbox_search_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DaiLy_lable_Them_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ScreenThemDaiLy screenThemDaiLy = new ScreenThemDaiLy();
            screenThemDaiLy.ShowDialog();
        }

        private void DaiLy_button_Them_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ScreenThemDaiLy screenThemDaiLy = new ScreenThemDaiLy();
            screenThemDaiLy.ShowDialog();
        }
    }
}
