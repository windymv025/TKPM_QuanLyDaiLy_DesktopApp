﻿using QuanLyDaiLyMVVM.Model;
using QuanLyDaiLyMVVM.ViewModel;
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

namespace QuanLyDaiLyMVVM
{
    /// <summary>
    /// Interaction logic for CapNhatSanPhamWindow.xaml
    /// </summary>
    public partial class CapNhatSanPhamWindow : Window
    {
        CapNhatSanPhamViewModel viewModel;
        public CapNhatSanPhamWindow(SanPhamHienThi sanPhamHienThi)
        {
            InitializeComponent();
            viewModel = this.DataContext as CapNhatSanPhamViewModel;
            viewModel.loadDataBinding(sanPhamHienThi);
        }
    }
}