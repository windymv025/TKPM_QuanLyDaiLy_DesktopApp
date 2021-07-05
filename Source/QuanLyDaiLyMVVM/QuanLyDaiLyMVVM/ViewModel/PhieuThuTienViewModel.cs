using QuanLyDaiLyMVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyDaiLyMVVM.ViewModel
{
    public class PhieuThuTienViewModel : BaseViewModel
    {
        private DaiLy _DaiLy;
        public DaiLy DaiLy { get => _DaiLy; set { _DaiLy = value; OnPropertyChanged(); } }

        private PhieuThuTien _PhieuThuTien;
        public PhieuThuTien PhieuThuTien { get => _PhieuThuTien; set { _PhieuThuTien = value; OnPropertyChanged(); } }

        private string _SoTienThu;
        public string SoTienThu { get => _SoTienThu; set { _SoTienThu = value; OnPropertyChanged(); } }

        private System.DateTime _NgayLapPhieu;
        public System.DateTime NgayLapPhieu { get => _NgayLapPhieu; set { _NgayLapPhieu = value; OnPropertyChanged(); } }

        private PhieuThuTienHienThi _SelectedPhieuThuTien;
        public PhieuThuTienHienThi SelectedPhieuThuTien { get => _SelectedPhieuThuTien; set { _SelectedPhieuThuTien = value; OnPropertyChanged(); } }

        private ObservableCollection<PhieuThuTienHienThi> _PhieuThuTiens;
        public ObservableCollection<PhieuThuTienHienThi> PhieuThuTiens { get => _PhieuThuTiens; set { _PhieuThuTiens = value; OnPropertyChanged(); } }
        
        private ObservableCollection<DaiLy> _DaiLys;
        public ObservableCollection<DaiLy> DaiLys { get => _DaiLys; set { _DaiLys = value; OnPropertyChanged(); } }

        public ICommand RefreshCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }

        public PhieuThuTienViewModel()
        {
            loadData();
            RefreshCommand = new RelayCommand<PhieuXuatHangWindow>((p) => { return true; }, (p) =>
            {
                p.cbb_LoaiTimKiem.SelectedIndex = 0;
                p.cbb_SapXepTheo.SelectedIndex = -1;
                p.textbox_search.Text = "";
                p.datePicker_Search.SelectedDate = null;
                p.cbb_KieuSapXep.SelectedIndex = 0;
                
                loadData();
            });

            SelectionChangedCommand = new RelayCommand<ListView>((p) =>
            {
                return true;
            }, (p) =>
            {
                DaiLy = DaiLys.Where(dl => dl.Id == SelectedPhieuThuTien.DaiLy.Id).FirstOrDefault();
                SoTienThu = SelectedPhieuThuTien.SoTienThu;
                NgayLapPhieu = SelectedPhieuThuTien.PhieuDaiLy.NgayLapPhieu;
            });
        }

        private void loadData()
        {
            using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                DaiLys = new ObservableCollection<DaiLy>(db.DaiLies);

                PhieuThuTiens = new ObservableCollection<PhieuThuTienHienThi>(
                    from p in db.PhieuThuTiens
                    select new PhieuThuTienHienThi
                    {
                        PhieuDaiLy = p.PhieuDaiLy,
                        PhieuThuTien = p,
                        DaiLy = p.PhieuDaiLy.DaiLy
                    });

                foreach (var i in PhieuThuTiens)
                {
                    if (i.DaiLy.HinhAnh == null)
                    {
                        i.DaiLy.HinhAnh = Path.GetFullPath("Assets/image_not_available.png");
                    }
                    else
                    {
                        i.DaiLy.HinhAnh = Path.GetFullPath(i.DaiLy.HinhAnh);
                    }
                    i.SoTienThu = ConvertNumber.convertNumberDecimalToString(i.PhieuThuTien.SoTienThu);
                }


                foreach (var i in DaiLys)
                {
                    if (i.HinhAnh == null)
                    {
                        i.HinhAnh = Path.GetFullPath("Assets/image_not_available.png");
                    }
                    else
                    {
                        i.HinhAnh = Path.GetFullPath(i.HinhAnh);
                    }
                }
            }
           
        }

    }
}
