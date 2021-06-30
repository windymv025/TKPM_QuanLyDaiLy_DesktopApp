using QuanLyDaiLyMVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyDaiLyMVVM.ViewModel
{
    public class DonViTinhViewModel: BaseViewModel
    {
        private string _Ten;
        public string Ten { get => _Ten; set { _Ten = value; OnPropertyChanged(); } }

        private DonViTinh _DonViTinh;
        public DonViTinh DonViTinh { get => _DonViTinh; set { _DonViTinh = value; OnPropertyChanged(); } }

        private ObservableCollection<DonViTinh> _DonViTinhs;
        public ObservableCollection<DonViTinh> DonViTinhs { get => _DonViTinhs; set { _DonViTinhs = value; OnPropertyChanged(); } }

        public ICommand SelectionChangedCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public DonViTinhViewModel()
        {
            loadData();
            SelectionChangedCommand = new RelayCommand<ListView>((p) => {
                return true;
            }, (p) => {
                if (DonViTinh == null)
                    return;

                Ten = DonViTinh.Ten;
            });

            AddCommand = new RelayCommand<Button>((p) => { return true; }, (p) => {
                MessageBoxResult result = MessageBox.Show("Bạn có muốn lưu?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    
                    if (string.IsNullOrEmpty(Ten))
                    {
                        MessageBox.Show("Bạn nhập thiếu thông tin nguồn nhập.", "Error Systerm", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }


                    var donVi = new DonViTinh
                    {
                        Ten = Ten
                    };


                    using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
                    {
                        db.DonViTinhs.Add(donVi);
                        db.SaveChanges();
                    }
                    DonViTinhs.Add(donVi);
                    Ten = "";
                }
            });

            RefreshCommand = new RelayCommand<Button>((p) => { return true; }, (p) => {
                Ten = "";
            });

            DeleteCommand = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                using (var db = new DBQuanLyCacDaiLyEntities())
                {
                    var dv = db.DonViTinhs.Where(nn => nn.Id == DonViTinh.Id).FirstOrDefault();
                    if (dv.SanPhams.Count() == 0)
                    {
                        db.DonViTinhs.Remove(dv);
                        db.SaveChanges();
                        DonViTinhs.Remove(DonViTinh);

                        Ten = "";
                    }
                    else
                    {
                        MessageBox.Show("Không thể xóa đơn vị tính này do ràng buộc khóa ngoại.", "Error Systerm", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }


            });

            EditCommand = new RelayCommand<ListView>((p) => { return true; }, (p) =>
            {
                if (DonViTinh == null)
                    return;
                MessageBoxResult result = MessageBox.Show("Bạn có muốn lưu?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (string.IsNullOrEmpty(Ten))
                    {
                        MessageBox.Show("Bạn nhập thiếu thông tin nguồn nhập.", "Error Systerm", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
                    {
                        var donVi = db.DonViTinhs.Where(nn => nn.Id == DonViTinh.Id).FirstOrDefault();
                        
                        donVi.Ten = Ten;

                        db.SaveChanges();
                    }

                    DonViTinh.Ten = Ten;
                }
            });

        }
        public void loadData()
        {
            using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                DonViTinhs = new ObservableCollection<DonViTinh>(db.DonViTinhs);
            }
        }
    }
}
