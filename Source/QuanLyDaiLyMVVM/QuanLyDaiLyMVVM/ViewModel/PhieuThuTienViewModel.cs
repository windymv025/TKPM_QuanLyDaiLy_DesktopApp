using Microsoft.Win32;
using OfficeOpenXml;
using OfficeOpenXml.Style;
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
        private int loaiSapXep = -1;
        private bool IsSXTang = true;
        private bool IsNgayLapPhieu = false;

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

        private ObservableCollection<PhieuThuTienHienThi> _PhieuThuTienBackups;
        public ObservableCollection<PhieuThuTienHienThi> PhieuThuTienBackups { get => _PhieuThuTienBackups; set { _PhieuThuTienBackups = value; OnPropertyChanged(); } }
        
        private ObservableCollection<DaiLy> _DaiLys;
        public ObservableCollection<DaiLy> DaiLys { get => _DaiLys; set { _DaiLys = value; OnPropertyChanged(); } }

        public ICommand RefreshCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand ThayDoiDuLieuSoCommand { get; set; }
        public ICommand ThayDoiDuLieuCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand WindowLoadedCommand { get; set; }
        public ICommand XuatFileExcelCommand { get; set; }

        public ICommand ThayDoiLoaiTimKiemCommand { get; set; }
        public ICommand ThayDoiLoaiSapXepCommand { get; set; }
        public ICommand ThayDoiSapXepCommand { get; set; }
        public ICommand KeySearchCommand { get; set; }
        public ICommand DateSearchCommand { get; set; }

        public PhieuThuTienViewModel()
        {
            WindowLoadedCommand = new RelayCommand<PhieuThuTienWindow>((p) =>
            {
                if (p != null)
                    return true;
                else
                    return false;
            }, (p) =>
            {
                loadData();
                refreshView(p);
            });

            RefreshCommand = new RelayCommand<PhieuThuTienWindow>((p) =>
            {
                if (p != null)
                    return true;
                else
                    return false;
            }, (p) =>
            {
                refreshView(p);
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
                        decimal tongSoTienNo = tinhTongSoTienNoDaiLy(DaiLy);

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

                    refreshView(p);
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

                refreshView(p);
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

            ThayDoiLoaiTimKiemCommand = new RelayCommand<PhieuThuTienWindow>((p) => { return true; }, (p) =>
            {
                if (!IsNgayLapPhieu)
                {
                    p.txtSearch.Visibility = Visibility.Collapsed;
                    p.datePickerSearch.Visibility = Visibility.Visible;
                    IsNgayLapPhieu = true;
                }
                else
                {
                    p.datePickerSearch.Visibility = Visibility.Collapsed;
                    p.txtSearch.Visibility = Visibility.Visible;
                    IsNgayLapPhieu = false;
                }
            });

            ThayDoiLoaiSapXepCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) =>
            {
                loaiSapXep = p.SelectedIndex;
                switch (loaiSapXep)
                {
                    case 0:
                        sapXepTheoTenDaiLy();
                        break;

                    case 1:
                        sapXepTheoNgayThuTien();
                        break;

                    case 2:
                        sapXepTheoSoTienThu();
                        break;

                    default:
                        break;
                }
            });

            ThayDoiSapXepCommand = new RelayCommand<ComboBox>((p) => { return true; }, (p) =>
            {
                IsSXTang = !IsSXTang;
                switch (loaiSapXep)
                {
                    case 0:
                        sapXepTheoTenDaiLy();
                        break;

                    case 1:
                        sapXepTheoNgayThuTien();
                        break;

                    case 2:
                        sapXepTheoSoTienThu();
                        break;

                    default:
                        break;
                }
            });

            KeySearchCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) =>
            {
                string keySearch = p.Text.Trim();
                
                using(DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
                {
                    if (keySearch != "")
                    {
                        string sql = $"select* from DaiLy where freetext((Ten,DienThoai, DiaChi, Email, Quan), N'%{keySearch}%')";
                        var listDaiLy = db.DaiLies.SqlQuery(sql);
                        
                        PhieuThuTiens = new ObservableCollection<PhieuThuTienHienThi>(
                            PhieuThuTienBackups.Where(i => listDaiLy.Where(dl => dl.Id == i.DaiLy.Id).Count() > 0)
                            );
                    }
                }
            });

            DateSearchCommand = new RelayCommand<DatePicker>((p) => { return true; }, (p) =>
            {
                using(DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
                {
                    PhieuThuTiens = new ObservableCollection<PhieuThuTienHienThi>(
                        PhieuThuTienBackups.Where(i => i.PhieuThuTien.NgayThuTien.Date == p.SelectedDate)
                        );
                }
            });

            XuatFileExcelCommand = new RelayCommand<Button>((p) => { return true; }, (p) =>
            {
                string minDay = (from i in PhieuThuTiens
                                 orderby i.PhieuThuTien.NgayThuTien ascending
                                 select i).FirstOrDefault().PhieuThuTien.NgayThuTien.ToString("dd-MM-yyyy");
                string maxDay = (from i in PhieuThuTiens
                                 orderby i.PhieuThuTien.NgayThuTien descending
                                 select i).FirstOrDefault().PhieuThuTien.NgayThuTien.ToString("dd-MM-yyyy");
                string filePath = "";
                // tạo SaveFileDialog để lưu file excel
                SaveFileDialog dialog = new SaveFileDialog();

                // chỉ lọc ra các file có định dạng Excel
                dialog.Filter = "Excel | *.xlsx | Excel 2003 | *.xls";
                dialog.FileName = $"Danh Sach Phieu Thu Tien {minDay}_{maxDay}.xlsx";

                // Nếu mở file và chọn nơi lưu file thành công sẽ lưu đường dẫn lại dùng
                if (dialog.ShowDialog() == true)
                {
                    filePath = dialog.FileName;
                }

                // nếu đường dẫn null hoặc rỗng thì báo không hợp lệ và return hàm
                if (string.IsNullOrEmpty(filePath))
                {
                    MessageBox.Show("Đường dẫn báo cáo không hợp lệ");
                    return;
                }

                try
                {
                    // If you use EPPlus in a noncommercial context
                    // according to the Polyform Noncommercial license:
                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                    using (ExcelPackage ep = new ExcelPackage())
                    {
                        // đặt tên người tạo file
                        ep.Workbook.Properties.Author = "Pham Minh Vuong";

                        // đặt tiêu đề cho file
                        ep.Workbook.Properties.Title = "Danh sách phiếu thu tiền";

                        //Tạo một sheet để làm việc trên đó
                        ep.Workbook.Worksheets.Add("Danh sách phiếu thu tiền");

                        // lấy sheet vừa add ra để thao tác
                        ExcelWorksheet ws = ep.Workbook.Worksheets[0];

                        // đặt tên cho sheet
                        ws.Name = "Danh sách phiếu thu tiền";
                        // fontsize mặc định cho cả sheet
                        ws.Cells.Style.Font.Size = 12;
                        // font family mặc định cho cả sheet
                        //ws.Cells.Style.Font.Name = "Calibri";
                        ws.Cells.Style.Font.Name = "Times New Roman";

                        // Tạo danh sách các column header
                        string[] arrColumnHeader = { "Tên đại lý", "Điện thoại", "Email", "Địa chỉ", "Ngày thu tiền", "Số tiền thu" };

                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();

                        // merge các column lại từ column 1 đến số column header
                        // gán giá trị cho cell vừa merge là Danh sách phiếu thu tiền
                        ws.Cells[1, 1].Value = $"Danh sách phiếu thu tiền";
                        ws.Cells[1, 1, 1, countColHeader].Merge = true;
                        // in đậm
                        ws.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;
                        // căn giữa
                        ws.Cells[1, 1, 1, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                        int colIndex = 1;
                        int rowIndex = 2;

                        //tạo các header từ column header đã tạo từ bên trên
                        foreach (var item in arrColumnHeader)
                        {
                            var cell = ws.Cells[rowIndex, colIndex];

                            //set màu thành gray
                            var fill = cell.Style.Fill;
                            fill.PatternType = ExcelFillStyle.Solid;
                            fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

                            //căn chỉnh các border
                            var border = cell.Style.Border;
                            border.Bottom.Style =
                                border.Top.Style =
                                border.Left.Style =
                                border.Right.Style = ExcelBorderStyle.Thin;
                            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                            //gán giá trị
                            cell.Value = item;

                            colIndex++;
                        }

                        // lấy ra danh sách UserInfo từ ItemSource của DataGrid
                        //List<UserInfo> userList = dtgExcel.ItemsSource.Cast<UserInfo>().ToList();

                        // với mỗi item trong danh sách sẽ ghi trên 1 dòng
                        foreach (var item in PhieuThuTiens)
                        {
                            // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            colIndex = 1;

                            // rowIndex tương ứng từng dòng dữ liệu
                            rowIndex++;

                            //gán giá trị cho từng cell                      
                            ws.Cells[rowIndex, colIndex++].Value = item.DaiLy.Ten;
                            ws.Cells[rowIndex, colIndex++].Value = item.DaiLy.DienThoai;
                            ws.Cells[rowIndex, colIndex++].Value = item.DaiLy.Email;
                            ws.Cells[rowIndex, colIndex++].Value = item.DaiLy.DiaChi;
                            ws.Cells[rowIndex, colIndex++].Value = item.PhieuThuTien.NgayThuTien.ToShortDateString();
                            ws.Cells[rowIndex, colIndex++].Value = item.PhieuThuTien.SoTienThu;

                            // lưu ý phải .ToShortDateString để dữ liệu khi in ra Excel là ngày như ta vẫn thấy.Nếu không sẽ ra tổng số :v
                            //ws.Cells[rowIndex, colIndex++].Value = item.Birthday.ToShortDateString();

                        }

                        //Lưu file lại
                        Byte[] bin = ep.GetAsByteArray();
                        File.WriteAllBytes(filePath, bin);
                    }
                    MessageBox.Show("Xuất excel thành công!");
                }
                catch
                {
                    MessageBox.Show("Có lỗi khi lưu file!");
                }
            });

        }

        private decimal tinhTongSoTienNoDaiLy(DaiLy daiLy)
        {
            using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                decimal tongSoTienNo = 0;

                foreach (var i in db.PhieuXuatHangs.Where(px => px.PhieuDaiLy.IdDaiLy == daiLy.Id))
                {
                    tongSoTienNo += i.TongTien;
                }

                foreach (var i in db.PhieuThuTiens.Where(px => px.PhieuDaiLy.IdDaiLy == daiLy.Id))
                {
                    tongSoTienNo -= i.SoTienThu;
                }

                return tongSoTienNo;
            }
        }

        private void sapXepTheoTenDaiLy()
        {
            if (IsSXTang)
            {
                PhieuThuTiens = new ObservableCollection<PhieuThuTienHienThi>(
                 from i in PhieuThuTiens
                 orderby i.DaiLy.Ten ascending
                 select i);
            }
            else
            {
                PhieuThuTiens = new ObservableCollection<PhieuThuTienHienThi>(
                 from i in PhieuThuTiens
                 orderby i.DaiLy.Ten descending
                 select i);
            }
            
        }

        private void sapXepTheoNgayThuTien()
        {
            if (IsSXTang)
            {
                PhieuThuTiens = new ObservableCollection<PhieuThuTienHienThi>(
                 from i in PhieuThuTiens
                 orderby i.PhieuThuTien.NgayThuTien ascending
                 select i);
            }
            else
            {
                PhieuThuTiens = new ObservableCollection<PhieuThuTienHienThi>(
                 from i in PhieuThuTiens
                 orderby i.PhieuThuTien.NgayThuTien descending
                 select i);
            }
        }

        private void sapXepTheoSoTienThu()
        {
            if (IsSXTang)
            {
                PhieuThuTiens = new ObservableCollection<PhieuThuTienHienThi>(
                 from i in PhieuThuTiens
                 orderby i.PhieuThuTien.SoTienThu ascending
                 select i);
            }
            else
            {
                PhieuThuTiens = new ObservableCollection<PhieuThuTienHienThi>(
                 from i in PhieuThuTiens
                 orderby i.PhieuThuTien.SoTienThu descending
                 select i);
            }
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
            PhieuThuTienBackups = new ObservableCollection<PhieuThuTienHienThi>(PhieuThuTiens);
        }

        private void refreshView(PhieuThuTienWindow p)
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
        }
    }
}
