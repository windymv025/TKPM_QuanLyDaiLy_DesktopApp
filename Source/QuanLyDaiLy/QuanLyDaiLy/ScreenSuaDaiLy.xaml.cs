using AppQuanLyDaiLy.ViewModels;
using QuanLyDaiLy.Model;
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
using System.Windows.Shapes;

namespace QuanLyDaiLy
{
    /// <summary>
    /// Interaction logic for ScreenSuaDaiLy.xaml
    /// </summary>
    public partial class ScreenSuaDaiLy : Window
    {
        DaiLy daiLy;
        HomeViewModel viewModel = new HomeViewModel();
        public ScreenSuaDaiLy(DaiLy dl)
        {
            InitializeComponent();
            daiLy = dl;
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btn_OK_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = daiLy;
            cbb_loaidaily.ItemsSource = viewModel.getAllLoaiDaiLy();

            int index = 0;
            foreach (var item in viewModel.getAllLoaiDaiLy())
            {
                if(item.MaLoai == daiLy.LoaiDaiLy)
                {
                    cbb_loaidaily.SelectedIndex = index;
                }
                index++;
            }
        }
    }
}
