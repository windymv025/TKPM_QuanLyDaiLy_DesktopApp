using AppQuanLyDaiLy.ViewModels;
using QuanLyDaiLy.Model;
using QuanLyDaiLy.ViewModels;
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
        HomeViewModel viewModel;
        SanPhamViewModel sanPhamViewModel;
        public MainWindow()
        {
            InitializeComponent();
            viewModel = new HomeViewModel();
            sanPhamViewModel = new SanPhamViewModel();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            listDaiLy.ItemsSource = DaiLyDAO.getAllDaiLy();

            spSanPhamTop1.DataContext = viewModel.SanPhamTop1;
            spSanPhamTop2.DataContext = viewModel.SanPhamTop2;
            spSanPhamTop3.DataContext = viewModel.SanPhamTop3;

            spDaiLyTop1.DataContext = viewModel.DaiLyTop1;
            spDaiLyTop2.DataContext = viewModel.DaiLyTop2;
            spDaiLyTop3.DataContext = viewModel.DaiLyTop3;

            
        }

        private void loadDataDaiLy()
        {
            listDaiLy.ItemsSource = DaiLyDAO.getAllDaiLy();
        }

        private void loadDataSanPham(int page)
        {
            lvSanPham.ItemsSource = sanPhamViewModel.loadPageHienThi(page);
            currentPageTxt.Text = sanPhamViewModel.PagingInfo.CurrentPage.ToString();
            totalPageTxt.Text = sanPhamViewModel.PagingInfo.TotalPage.ToString();
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
            sanPhamViewModel.getSanPhams();
            loadDataSanPham(1);
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
            loadDataDaiLy();
        }

        private void DaiLy_button_Them_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ScreenThemDaiLy screenThemDaiLy = new ScreenThemDaiLy();
            screenThemDaiLy.ShowDialog();
        }

        private void listDaiLy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = listDaiLy.SelectedItem as DaiLy;

            ScreenChiTietDaiLy screenChiTietDaiLy = new ScreenChiTietDaiLy(item);
            screenChiTietDaiLy.ShowDialog();
        }

        private void spDaiLyTop2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var dl = viewModel.getDaiLyTheoTop(viewModel.DaiLyTop2);
            ScreenChiTietDaiLy screenChiTietDaiLy = new ScreenChiTietDaiLy(dl);
            screenChiTietDaiLy.ShowDialog();
        }

        private void spDaiLyTop1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var dl = viewModel.getDaiLyTheoTop(viewModel.DaiLyTop1);
            ScreenChiTietDaiLy screenChiTietDaiLy = new ScreenChiTietDaiLy(dl);
            screenChiTietDaiLy.ShowDialog();
        }

        private void spDaiLyTop3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var dl = viewModel.getDaiLyTheoTop(viewModel.DaiLyTop3);
            ScreenChiTietDaiLy screenChiTietDaiLy = new ScreenChiTietDaiLy(dl);
            screenChiTietDaiLy.ShowDialog();
        }

        private void spSanPhamTop2_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void spSanPhamTop1_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void spSanPhamTop3_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void SanPham_lable_Them_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void SanPham_button_Them_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void SanPham_textbox_search_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SanPham_btn_Search_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void SanPham_previous_button(object sender, MouseButtonEventArgs e)
        {
            loadDataSanPham(sanPhamViewModel.PagingInfo.CurrentPage - 1);
        }

        private void SanPham_next_button(object sender, MouseButtonEventArgs e)
        {
            loadDataSanPham(sanPhamViewModel.PagingInfo.CurrentPage + 1);
        }
    }
}
