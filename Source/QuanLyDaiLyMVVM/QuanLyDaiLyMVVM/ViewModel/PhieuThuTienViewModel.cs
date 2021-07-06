using QuanLyDaiLyMVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyDaiLyMVVM.ViewModel
{
    public class PhieuThuTienViewModel : BaseViewModel
    {
        private bool IsDaiLy = false;
        private bool IsSoTienThu = false;

        private DaiLy _DaiLy;
        public DaiLy DaiLy { get => _DaiLy; set { _DaiLy = value; OnPropertyChanged(); } }

        private PhieuThuTien _PhieuThuTien;
        public PhieuThuTien PhieuThuTien { get => _PhieuThuTien; set { _PhieuThuTien = value; OnPropertyChanged(); } }

        private string _SoTienThu;
        public string SoTienThu { get => _SoTienThu; set { _SoTienThu = value; OnPropertyChanged(); } }

        private System.DateTime _NgayThuTien;
        public System.DateTime NgayThuTien { get => _NgayThuTien; set { _NgayThuTien = value; OnPropertyChanged(); } }

        private PhieuThuTienHienThi _SelectedPhieuThuTien;
        public PhieuThuTienHienThi SelectedPhieuThuTien { get => _SelectedPhieuThuTien; set { _SelectedPhieuThuTien = value; OnPropertyChanged(); } }

        private ObservableCollection<PhieuThuTienHienThi> _PhieuThuTiens;
        public ObservableCollection<PhieuThuTienHienThi> PhieuThuTiens { get => _PhieuThuTiens; set { _PhieuThuTiens = value; OnPropertyChanged(); } }
        
        private ObservableCollection<DaiLy> _DaiLys;
        public ObservableCollection<DaiLy> DaiLys { get => _DaiLys; set { _DaiLys = value; OnPropertyChanged(); } }

        public ICommand RefreshCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand ThayDoiDuLieuSoCommand { get; set; }
        public ICommand ThayDoiDuLieuCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public PhieuThuTienViewModel()
        {
            loadData();
            RefreshCommand = new RelayCommand<PhieuThuTienWindow>((p) =>
            {
                if (p != null)
                    return true;
                else
                    return false;
            }, (p) =>
            {
                p.btnThem.IsEnabled = false;
                p.btnSua.IsEnabled = false;
                p.btnXoa.IsEnabled = false;

                p.cbb_LoaiTimKiem.SelectedIndex = 0;
                p.cbb_SapXepTheo.SelectedIndex = -1;
                p.textbox_search.Text = "";
                p.datePicker_Search.SelectedDate = null;
                p.cbb_KieuSapXep.SelectedIndex = 0;

                p.cbb_dsDaiLy.SelectedIndex = -1;
                p.lv_DanhSachPhieuThuTien.SelectedIndex = -1;
                p.SoTienThuTxt.Text = "0";

                NgayThuTien = DateTime.Now;
                loadData();
            });

            SelectionChangedCommand = new RelayCommand<PhieuThuTienWindow>((p) =>
            {
                return true;
            }, (p) =>
            {
                if (p.lv_DanhSachPhieuThuTien.SelectedIndex > -1)
                {
                    DaiLy = DaiLys.Where(dl => dl.Id == SelectedPhieuThuTien.DaiLy.Id).FirstOrDefault();
                    SoTienThu = SelectedPhieuThuTien.SoTienThu;
                    NgayThuTien = SelectedPhieuThuTien.PhieuThuTien.NgayThuTien;
                    p.btnThem.IsEnabled = true;
                    p.btnSua.IsEnabled = true;
                    p.btnXoa.IsEnabled = true;
                }
                else
                {
                    p.btnThem.IsEnabled = false;
                    p.btnSua.IsEnabled = false;
                    p.btnXoa.IsEnabled = false;
                }
            });

            ThayDoiDuLieuCommand = new RelayCommand<PhieuThuTienWindow>((p) =>
            {
                return true;
            }, (p) =>
            {
                if (p.cbb_dsDaiLy.SelectedIndex > -1)
                {
                    using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
                    {
                        decimal tongSoTienNo = 0;

                        foreach (var i in db.PhieuXuatHangs.Where(px => px.PhieuDaiLy.IdDaiLy == DaiLy.Id))
                        {
                            tongSoTienNo += i.TongTien;
                        }

                        foreach (var i in db.PhieuThuTiens.Where(px => px.PhieuDaiLy.IdDaiLy == DaiLy.Id))
                        {
                            tongSoTienNo -= i.SoTienThu;
                        }

                        SoTienThu = ConvertNumber.convertNumberDecimalToString(tongSoTienNo);
                        PhieuThuTien.SoTienThu = tongSoTienNo;
                    }
                        IsDaiLy = true;
                    if (IsSoTienThu && IsDaiLy)
                    {
                        p.btnThem.IsEnabled = true;
                    }
                }
                else
                {
                    IsDaiLy = false;
                }
            });

            ThayDoiDuLieuSoCommand = new RelayCommand<PhieuThuTienWindow>((p) => { return true; }, (p) => {
                int caretIndex = p.SoTienThuTxt.CaretIndex;
                p.SoTienThuTxt.Text = Regex.Replace(p.SoTienThuTxt.Text, "[^0-9.]+", "");
                string numberString = p.SoTienThuTxt.Text;
                string newNumStr = "";
                for (int i = 0; i < numberString.Length; i++)
                {
                    if (numberString[i] != '.')
                    {
                        newNumStr += numberString[i];
                    }
                }
                if (newNumStr == "")
                {
                    newNumStr = "0";
                }
                decimal number = decimal.Parse(newNumStr);
                PhieuThuTien.SoTienThu = number;
                p.SoTienThuTxt.Text = ConvertNumber.convertNumberDecimalToString(number);
                p.SoTienThuTxt.CaretIndex = caretIndex;

                if(number > 0)
                {
                    IsSoTienThu = true;
                    if(IsSoTienThu && IsDaiLy)
                    {
                        p.btnThem.IsEnabled = true;
                    }
                }
                else
                {
                    IsSoTienThu = false;
                }
            });

            AddCommand = new RelayCommand<PhieuThuTienWindow>((p) => { return true; }, (p) => {
                MessageBoxResult result = MessageBox.Show("Bạn có muốn lưu?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (SoTienThu == "0")
                    {
                        MessageBox.Show("Số tiền thu phải lớn hơn 0.", "Error Systerm", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
                    {
                        decimal tongSoTienNo = 0;
                        
                        foreach(var i in db.PhieuXuatHangs.Where(px => px.PhieuDaiLy.IdDaiLy == DaiLy.Id))
                        {
                            tongSoTienNo += i.TongTien;
                        }
                        
                        foreach(var i in db.PhieuThuTiens.Where(px => px.PhieuDaiLy.IdDaiLy == DaiLy.Id))
                        {
                            tongSoTienNo -= i.SoTienThu;
                        }

                        if(tongSoTienNo < PhieuThuTien.SoTienThu)
                        {
                            MessageBox.Show($"Số tiền thu không vượt quá số tiền nợ của đại lý ({ConvertNumber.convertNumberDecimalToString(tongSoTienNo)} VNĐ).", "Error Systerm", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        PhieuDaiLy phieuDaiLy = new PhieuDaiLy
                        {
                            IdDaiLy = DaiLy.Id
                        };

                        PhieuThuTien phieuThuTien = new PhieuThuTien
                        {
                            PhieuDaiLy = phieuDaiLy,
                            SoTienThu = PhieuThuTien.SoTienThu,
                            NgayThuTien = NgayThuTien
                        };

                        db.PhieuThuTiens.Add(phieuThuTien);
                        db.SaveChanges();

                        var phieuThuTienHienThi = new PhieuThuTienHienThi
                        {
                            DaiLy = DaiLy,
                            SoTienThu = SoTienThu,
                            PhieuThuTien = phieuThuTien,
                            PhieuDaiLy = phieuThuTien.PhieuDaiLy
                        };

                        PhieuThuTiens.Add(phieuThuTienHienThi);

                    }

                    p.btnThem.IsEnabled = false;
                    p.btnSua.IsEnabled = false;
                    p.btnXoa.IsEnabled = false;

                    p.cbb_LoaiTimKiem.SelectedIndex = 0;
                    p.cbb_SapXepTheo.SelectedIndex = -1;
                    p.textbox_search.Text = "";
                    p.datePicker_Search.SelectedDate = null;
                    p.cbb_KieuSapXep.SelectedIndex = 0;

                    p.cbb_dsDaiLy.SelectedIndex = -1;
                    p.lv_DanhSachPhieuThuTien.SelectedIndex = -1;
                    p.SoTienThuTxt.Text = "0";

                    NgayThuTien = DateTime.Now;
                }
            });

            DeleteCommand = new RelayCommand<PhieuThuTienWindow>((p) => { return true; }, (p) =>
            {
                using (var db = new DBQuanLyCacDaiLyEntities())
                {
                    db.PhieuThuTiens.Remove(db.PhieuThuTiens.Where(i => i.PhieuDaiLy.Id == SelectedPhieuThuTien.PhieuDaiLy.Id).FirstOrDefault());
                    db.PhieuDaiLies.Remove(db.PhieuDaiLies.Where(i => i.Id == SelectedPhieuThuTien.PhieuDaiLy.Id).FirstOrDefault());
                    db.SaveChanges();

                    PhieuThuTiens.Remove(SelectedPhieuThuTien);
                }

                p.btnThem.IsEnabled = false;
                p.btnSua.IsEnabled = false;
                p.btnXoa.IsEnabled = false;

                p.cbb_LoaiTimKiem.SelectedIndex = 0;
                p.cbb_SapXepTheo.SelectedIndex = -1;
                p.textbox_search.Text = "";
                p.datePicker_Search.SelectedDate = null;
                p.cbb_KieuSapXep.SelectedIndex = 0;

                p.cbb_dsDaiLy.SelectedIndex = -1;
                p.lv_DanhSachPhieuThuTien.SelectedIndex = -1;
                p.SoTienThuTxt.Text = "0";

                NgayThuTien = DateTime.Now;
            });

            EditCommand = new RelayCommand<ListView>((p) => { return true; }, (p) =>
            {
                if (SelectedPhieuThuTien == null)
                    return;
                MessageBoxResult result = MessageBox.Show("Bạn có muốn lưu?", "", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if (SoTienThu == "0")
                    {
                        MessageBox.Show("Số tiền thu phải lớn hơn 0.", "Error Systerm", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
                    {
                        decimal tongSoTienNo = 0;

                        foreach (var i in db.PhieuXuatHangs.Where(px => px.PhieuDaiLy.IdDaiLy == DaiLy.Id))
                        {
                            tongSoTienNo += i.TongTien;
                        }

                        foreach (var i in db.PhieuThuTiens.Where(px => px.PhieuDaiLy.IdDaiLy == DaiLy.Id))
                        {
                            tongSoTienNo -= i.SoTienThu;
                        }

                        if (tongSoTienNo < PhieuThuTien.SoTienThu)
                        {
                            MessageBox.Show($"Số tiền thu không vượt quá số tiền nợ của đại lý ({ConvertNumber.convertNumberDecimalToString(tongSoTienNo)} VNĐ).", 
                                "Error Systerm", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        var phieuThuTien = db.PhieuThuTiens.Where(i => i.PhieuDaiLy.Id == SelectedPhieuThuTien.PhieuDaiLy.Id).FirstOrDefault();

                        phieuThuTien.PhieuDaiLy.IdDaiLy = DaiLy.Id;
                        phieuThuTien.NgayThuTien = NgayThuTien;
                        phieuThuTien.SoTienThu = PhieuThuTien.SoTienThu;
                        
                        db.SaveChanges();
                    }
                    SelectedPhieuThuTien.PhieuDaiLy.IdDaiLy = DaiLy.Id;
                    SelectedPhieuThuTien.DaiLy = DaiLy;
                    SelectedPhieuThuTien.PhieuThuTien.NgayThuTien = NgayThuTien;
                    SelectedPhieuThuTien.PhieuThuTien.SoTienThu = PhieuThuTien.SoTienThu;
                    SelectedPhieuThuTien.SoTienThu = SoTienThu;
                }
            });
        }

        private void loadData()
        {
            DaiLy = new DaiLy();
            PhieuThuTien = new PhieuThuTien();
            SoTienThu = "0";
            NgayThuTien = DateTime.Now;
            SelectedPhieuThuTien = new PhieuThuTienHienThi();

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
