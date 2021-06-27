using QuanLyDaiLyMVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDaiLyMVVM.ViewModel
{
    public class QuyDinhViewModel : BaseViewModel
    {
        private ObservableCollection<QuyDinh> _List;
        public ObservableCollection<QuyDinh> List { get => _List; set { _List = value; OnPropertyChanged(); } }
        private string _TenQuyDinh;
        public string TenQuyDinh { get => _TenQuyDinh; set { _TenQuyDinh = value;OnPropertyChanged(); } }
        private int _GiaTri;
        public int GiaTri { get => _GiaTri; set { _GiaTri = value; OnPropertyChanged(); } }

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
        public QuyDinhViewModel()
        {
            using (var db = new DBQuanLyCacDaiLyEntities())
            {
                List = new ObservableCollection<QuyDinh>(db.QuyDinhs);
            }
        }
    }
}
