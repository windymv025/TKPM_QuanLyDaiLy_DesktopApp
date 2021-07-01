using QuanLyDaiLyMVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QuanLyDaiLyMVVM.ViewModel
{
    public class QuyDinhViewModel : BaseViewModel
    {
        private ObservableCollection<QuyDinh> _List;
        public ObservableCollection<QuyDinh> List { get => _List; set { _List = value; OnPropertyChanged(); } }
        private string _TenQuyDinh;
        public string TenQuyDinh { get => _TenQuyDinh; set { _TenQuyDinh = value;OnPropertyChanged(); } }
        private double _GiaTri;
        public double GiaTri { get => _GiaTri; set { _GiaTri = value; OnPropertyChanged(); } }

        public string _KieuDuLieu;
        public string KieuDuLieu { get => _KieuDuLieu; set { _KieuDuLieu = value; OnPropertyChanged(); } }

        private QuyDinh _SelectedItem;
        public QuyDinh SelectedItem { get => _SelectedItem; 
            set
            {
                _SelectedItem = value; 
                OnPropertyChanged();
                if(SelectedItem != null)
                {
                    TenQuyDinh = SelectedItem.TenQuyDinh;
                    GiaTri = SelectedItem.GiaTri;
                    KieuDuLieu = SelectedItem.KieuDuLieu;
                }
            } 
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand DeleteCommand { get; set; }

        public QuyDinhViewModel()
        {
            using (var db = new DBQuanLyCacDaiLyEntities())
            {
                List = new ObservableCollection<QuyDinh>(db.QuyDinhs.Where(x=>x.TrangThai == true).ToList());
            }

            AddCommand = new RelayCommand<QuyDinhWindow>((p) => { 
                if(p != null && !string.IsNullOrEmpty(TenQuyDinh) && !string.IsNullOrEmpty(KieuDuLieu) && !string.IsNullOrEmpty(p.Value.Text.Trim()))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }, (p) => {
                using (var db = new DBQuanLyCacDaiLyEntities())
                {
                    var item = new QuyDinh() { TenQuyDinh = TenQuyDinh, GiaTri = GiaTri, KieuDuLieu = KieuDuLieu, TrangThai = true };
                    db.QuyDinhs.Add(item);
                    db.SaveChanges();
                    List.Add(item);
                }
            });
            EditCommand = new RelayCommand<QuyDinhWindow>((p) => {
                if (p != null && !string.IsNullOrEmpty(TenQuyDinh) && !string.IsNullOrEmpty(KieuDuLieu) && !string.IsNullOrEmpty(p.Value.Text.Trim()) && SelectedItem != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }, (p) => {
                using (var db = new DBQuanLyCacDaiLyEntities())
                {
                    var item = db.QuyDinhs.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                    item.TenQuyDinh = TenQuyDinh;
                    item.GiaTri = GiaTri;
                    item.KieuDuLieu = KieuDuLieu;
                    db.SaveChanges();

                    SelectedItem.TenQuyDinh = TenQuyDinh;
                    SelectedItem.GiaTri = GiaTri;
                    SelectedItem.KieuDuLieu = KieuDuLieu;
                }
            });
            DeleteCommand = new RelayCommand<QuyDinhWindow>((p) => { 
                if(SelectedItem == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }, (p) => {
                using (var db = new DBQuanLyCacDaiLyEntities())
                {
                    var item = db.QuyDinhs.Where(x => x.Id == SelectedItem.Id).SingleOrDefault();
                    item.TrangThai = false;
                    db.SaveChanges();

                    SelectedItem.TrangThai = false;

                    List = new ObservableCollection<QuyDinh>(db.QuyDinhs.Where(x => x.TrangThai == true).ToList());
                }
            });
        }
    }
}
