using Microsoft.Win32;
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
    /// Interaction logic for ScreenThemDaiLy.xaml
    /// </summary>
    public partial class ScreenThemDaiLy : Window
    {
        string[] loai = { "1", "2" };
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
            cbb_loaidaily.ItemsSource = loai;
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

        }

        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
