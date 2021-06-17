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
    /// Interaction logic for ScreenChiTietDaiLy.xaml
    /// </summary>
    public partial class ScreenChiTietDaiLy : Window
    {
        DaiLy daiLy;
        public ScreenChiTietDaiLy(DaiLy dl)
        {
            InitializeComponent();
            daiLy = dl;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = daiLy;
        }

        private void btn_Xoa_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Sua_Click(object sender, RoutedEventArgs e)
        {
            ScreenSuaDaiLy screenSuaDaiLy = new ScreenSuaDaiLy(daiLy);
            screenSuaDaiLy.ShowDialog();
        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
