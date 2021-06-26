﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyDaiLyMVVM.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public bool Isloaded = false;
        public ICommand LoadedWindowCommand { get; set; }
        public ICommand DaiLyShowCommand { get; set; }
        public ICommand SanPhamShowCommand { get; set; }
        public ICommand PhieuThuTienShowCommand { get; set; }
        public ICommand PhieuXuatHangShowCommand { get; set; }
        public ICommand RevenueShowCommand { get; set; }
        public ICommand AccountShowCommand { get; set; }
        public ICommand ContactShowCommand { get; set; }

        public MainViewModel()
        {
            LoadedWindowCommand = new RelayCommand<Window>((p) => { return true; }, (p) => {
                Isloaded = true;
                if (p == null)
                    return;
                p.Hide();

                LoginWindow loginWindow = new LoginWindow();
                loginWindow.ShowDialog();

                if (loginWindow.DataContext == null)
                    return;
                var loginVM = loginWindow.DataContext as LoginViewModel;

                if (loginVM.IsLogin)
                {
                    p.Show();
                    //LoadallData();
                }
                else
                {
                    p.Close();
                }
            });

            DaiLyShowCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                DaiLyWindow wd = new DaiLyWindow();
                wd.ShowDialog();
            });

            SanPhamShowCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                SanPhamWindow wd = new SanPhamWindow();
                wd.ShowDialog();
            });

            PhieuThuTienShowCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                PhieuThuTienWindow wd = new PhieuThuTienWindow();
                wd.ShowDialog();
            });

            PhieuXuatHangShowCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                PhieuXuatHangWindow wd = new PhieuXuatHangWindow();
                wd.ShowDialog();
            });

            RevenueShowCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                BaoCaoWindow wd = new BaoCaoWindow();
                wd.ShowDialog();
            });

            AccountShowCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                NhanVienWindow wd = new NhanVienWindow();
                wd.ShowDialog();
            });

            ContactShowCommand = new RelayCommand<object>((p) => { return true; }, (p) => {
                LienHeWindow wd = new LienHeWindow();
                wd.ShowDialog();
            });
        }

        FrameworkElement GetWindowParent(object p, string name)
        {
            var parent = p as FrameworkElement;

            while (parent.Name != name && parent.Parent != null)
            {
                parent = parent.Parent as FrameworkElement;
            }

            return parent;
        }
    }
}
