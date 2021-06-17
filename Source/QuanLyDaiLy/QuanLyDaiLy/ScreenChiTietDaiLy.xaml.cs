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
            this.Title = daiLy.Ten;
        }
    }
}
