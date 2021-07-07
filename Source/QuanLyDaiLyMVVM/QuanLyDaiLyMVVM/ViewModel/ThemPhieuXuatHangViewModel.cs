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
    public class ThemPhieuXuatHangViewModel: BaseViewModel
    {
        private decimal tongtien = 0;

        private bool _IsSave;
        public bool IsSave { get => _IsSave; set { _IsSave = value; OnPropertyChanged(); } }

        private bool _IsChange;
        public bool IsChange { get => _IsChange; set { _IsChange = value; OnPropertyChanged(); } }

        private bool _IsAdd;
        public bool IsAdd { get => _IsAdd; set { _IsAdd = value; OnPropertyChanged(); } }

        private bool _IsRemove;
        public bool IsRemove { get => _IsRemove; set { _IsRemove = value; OnPropertyChanged(); } }

        private bool isChooseSPX = false;

        private bool isChooseDaiLy = false;

        private string _TongTien;
        public string TongTien { get => _TongTien; set { _TongTien = value; OnPropertyChanged(); } }

        private string _SoLuong;
        public string SoLuong { get => _SoLuong; set { _SoLuong = value; OnPropertyChanged(); } }

        private string _GiaBan;
        public string GiaBan { get => _GiaBan; set { _GiaBan = value; OnPropertyChanged(); } }

        private DateTime _NgayLapPhieu;
        public DateTime NgayLapPhieu { get => _NgayLapPhieu; set { _NgayLapPhieu = value; OnPropertyChanged(); } }


        //Binding combobox
        private DaiLy _DaiLy;
        public DaiLy DaiLy { get => _DaiLy; set { _DaiLy = value; OnPropertyChanged(); } }

        private ObservableCollection<DaiLy> _DaiLys;
        public virtual ObservableCollection<DaiLy> DaiLys { get => _DaiLys; set { _DaiLys = value; OnPropertyChanged(); } }

        private SanPhamHienThi _SanPham;
        public SanPhamHienThi SanPham { get => _SanPham; set { _SanPham = value; OnPropertyChanged(); } }

        private ObservableCollection<SanPhamHienThi> _SanPhams;
        public ObservableCollection<SanPhamHienThi> SanPhams { get => _SanPhams; set { _SanPhams = value; OnPropertyChanged(); } }

        private SanPhamXuat _SanPhamXuat;
        public SanPhamXuat SanPhamXuat { get => _SanPhamXuat; set { _SanPhamXuat = value; OnPropertyChanged(); } }

        private ObservableCollection<SanPhamXuat> _SanPhamXuats;
        public ObservableCollection<SanPhamXuat> SanPhamXuats { get => _SanPhamXuats; set { _SanPhamXuats = value; OnPropertyChanged(); } }

        public ICommand ThayDoiDuLieuSoCommand { get; set; }
        public ICommand ThayDoiDuLieuCommand { get; set; }
        public ICommand ThayDoiSanPhamCommand { get; set; }
        public ICommand ThayDoiDaiLyCommand { get; set; }
        public ICommand SelectionChangedCommand { get; set; }
        public ICommand RefreshCommand { get; set; }
        public ICommand LuuThayDoiCommand { get; set; }
        public ICommand ThoatCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand XuatFileExcelCommand { get; set; }

        public ThemPhieuXuatHangViewModel()
        {
            loadData();

            #region XỬ LÝ THAY ĐỔI DỮ LIỆU
            ThayDoiDuLieuCommand = new RelayCommand<Button>((p) =>
            {
                return true;
            }, (p) =>
            {
                if (isChooseSPX)
                    IsChange = true;
                IsAdd = true;

            });

            ThayDoiSanPhamCommand = new RelayCommand<ComboBox>((p) =>
            {
                return true;
            }, (p) =>
            {
                if (p.SelectedIndex > -1)
                    GiaBan = SanPham.GiaBan;
                else
                    GiaBan = "0";

            });

            ThayDoiDaiLyCommand = new RelayCommand<Button>((p) =>
            {
                return true;
            }, (p) =>
            {
                IsSave = true;
                isChooseDaiLy = true;
            });

            ThayDoiDuLieuSoCommand = new RelayCommand<TextBox>((p) => { return true; }, (p) => {
                int caretIndex = p.CaretIndex;
                p.Text = Regex.Replace(p.Text, "[^0-9.]+", "");
                string numberString = p.Text;
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
                p.Text = ConvertNumber.convertNumberDecimalToString(number);

                if (p.Text.Length == numberString.Length + 1)
                {
                    caretIndex++;
                }
                if (p.Text.Length == numberString.Length - 1)
                {
                    caretIndex--;
                }
                if (caretIndex < 0)
                {
                    caretIndex = 0;
                }
                p.CaretIndex = caretIndex;

                if (number > 0)
                {
                    IsAdd = true;
                    if (isChooseSPX)
                        IsChange = true;
                }
                else
                {
                    IsAdd = false;
                    IsChange = false;
                }
            });

            SelectionChangedCommand = new RelayCommand<ListView>((p) => { return true; }, (p) => {
                if (p.SelectedIndex > -1)
                {
                    SanPham = SanPhams.Where(sp => sp.SanPham.Id == SanPhamXuat.SanPham.Id).FirstOrDefault();
                    GiaBan = SanPhamXuat.GiaBan;
                    SoLuong = SanPhamXuat.SoLuong;

                    IsAdd = true;
                    IsChange = false;
                    IsRemove = true;
                    isChooseSPX = true;
                }
                else
                {
                    IsAdd = false;
                    IsChange = false;
                    IsRemove = false;
                    isChooseSPX = false;
                }
            });

            #endregion

            #region  XỬ LÝ NÚT NHẤN CHO TOÀN BỘ PHIẾU XUẤT HÀNG
            
            LuuThayDoiCommand = new RelayCommand<ThemPhieuXuatHangWindow>((p) => { return true; }, (p) =>
            {
                MessageBoxResult result = MessageBox.Show("Bạn có muốn lưu?", "Error System", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    if(SanPhamXuats.Count <= 0 || !isChooseDaiLy)
                    {
                        MessageBox.Show("Bạn cần cung cấp thông tin đại lý và sản phẩm cần xuất hợp lệ.", "Error System", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
                    {
                        PhieuXuatHang phieuXuatHang = new PhieuXuatHang
                        {
                            NgayLapPhieu = NgayLapPhieu,
                            TongTien = tongtien
                        };

                        PhieuDaiLy phieuDaiLy = new PhieuDaiLy
                        {
                            IdDaiLy = DaiLy.Id,
                            PhieuXuatHang = phieuXuatHang
                        };

                        db.PhieuDaiLies.Add(phieuDaiLy);

                        foreach(var i in SanPhamXuats)
                        {
                            var ctp = new ChiTietPhieuXuatHang
                            {
                               IdSanPham = i.SanPham.Id,
                               GiaBan = i.ChiTietPhieuXuatHang.GiaBan,
                               SoLuong = i.ChiTietPhieuXuatHang.SoLuong,
                               IdPhieuXuatHang = phieuDaiLy.Id
                            };

                            db.ChiTietPhieuXuatHangs.Add(ctp);
                        }

                        foreach (var i in SanPhams)
                        {
                            var sp = db.SanPhams.Where(d => d.Id == i.SanPham.Id).FirstOrDefault();
                            sp.SoLuong = i.SanPham.SoLuong;
                        }

                        db.SaveChanges();
                    }

                    p.Close();
                }
            });

            ThoatCommand = new RelayCommand<Window>((p) => { return true; }, (p) => { p.Close(); });

            XuatFileExcelCommand = new RelayCommand<Button>((p) => { return true; }, (p) => {
                ExportFileExcel();
            });
            #endregion

            #region XỬ LÝ NÚT NHẤN SẢN PHẨM
            RefreshCommand = new RelayCommand<ThemPhieuXuatHangWindow>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.cbb_DSSanPham.SelectedIndex = -1;
                p.GiaBanTxt.Text = "0";
                p.soLuongTxt.Text = "0";

            });

            AddCommand = new RelayCommand<ThemPhieuXuatHangWindow>((p) => { return true; }, (p) => {
                int soLuongXuat = ConvertNumber.convertStringtoInt(SoLuong);
                if (SanPham.SanPham.SoLuong < soLuongXuat || soLuongXuat == 0)
                {
                    MessageBox.Show($"Số lượng sản phẩm xuất không được phép nhỏ hơn hoặc vượt quá số lượng đang có ({SanPham.SanPham.SoLuong} {SanPham.DonViTinh.Ten}).", "System Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                bool isAdded = false;
                foreach (var sp in SanPhamXuats)
                {
                    if (sp.SanPham.Id == SanPham.SanPham.Id)
                    {
                        // Set lại số lượng sản phẩm
                        SanPham.SanPham.SoLuong -= soLuongXuat;
                        SanPham.SoLuong = ConvertNumber.convertNumberToString(SanPham.SanPham.SoLuong);
                        //Set lại tổng tiền
                        tongtien -= sp.ThanhTienNum;
                        IsSave = true;

                        //Set lại số lượng mới và giá bán mới
                        sp.ChiTietPhieuXuatHang.GiaBan = ConvertNumber.convertStringtoDecimal(GiaBan);
                        sp.ChiTietPhieuXuatHang.SoLuong += ConvertNumber.convertStringtoInt(SoLuong);
                        sp.SoLuong = ConvertNumber.convertNumberToString(sp.ChiTietPhieuXuatHang.SoLuong);
                        sp.GiaBan = GiaBan;

                        //tính toán lại thành tiền
                        sp.ThanhTienNum = sp.ChiTietPhieuXuatHang.GiaBan * sp.ChiTietPhieuXuatHang.SoLuong;
                        sp.ThanhTien = ConvertNumber.convertNumberDecimalToString(sp.ThanhTienNum);

                        //Tính lại tổng tiền.
                        tongtien += sp.ThanhTienNum;
                        TongTien = ConvertNumber.convertNumberDecimalToString(tongtien);

                        isAdded = true;
                        break;
                    }
                }

                if (!isAdded)
                {
                    ChiTietPhieuXuatHang chiTietPhieuXuatHang = new ChiTietPhieuXuatHang
                    {
                        IdSanPham = SanPham.SanPham.Id,
                        GiaBan = ConvertNumber.convertStringtoDecimal(GiaBan),
                        SoLuong = soLuongXuat
                    };
                    SanPhamXuat sanPhamXuat = new SanPhamXuat
                    {
                        SanPham = SanPham.SanPham,
                        DonViTinh = SanPham.DonViTinh,
                        GiaBan = GiaBan,
                        SoLuong = SoLuong,
                        STT = SanPhamXuats.Count + 1,
                        ChiTietPhieuXuatHang = chiTietPhieuXuatHang,
                        ThanhTienNum = soLuongXuat * chiTietPhieuXuatHang.GiaBan,
                    };
                    sanPhamXuat.ThanhTien = ConvertNumber.convertNumberDecimalToString(sanPhamXuat.ThanhTienNum);

                    SanPham.SanPham.SoLuong -= soLuongXuat;
                    SanPham.SoLuong = ConvertNumber.convertNumberToString(SanPham.SanPham.SoLuong);

                    tongtien += sanPhamXuat.ThanhTienNum;
                    TongTien = ConvertNumber.convertNumberDecimalToString(tongtien);

                    SanPhamXuats.Add(sanPhamXuat);
                }
                p.cbb_DSSanPham.SelectedIndex = -1;
                SoLuong = "0";
                GiaBan = "0";
                IsChange = false;
                IsAdd = false;
                IsRemove = false;
                IsSave = true;
            });

            DeleteCommand = new RelayCommand<ThemPhieuXuatHangWindow>((p) => { return true; }, (p) => {
                tongtien -= SanPhamXuat.ThanhTienNum;
                TongTien = ConvertNumber.convertNumberDecimalToString(tongtien);

                SanPham.SanPham.SoLuong += SanPhamXuat.ChiTietPhieuXuatHang.SoLuong;
                SanPham.SoLuong = ConvertNumber.convertNumberToString(SanPham.SanPham.SoLuong);

                SanPhamXuats.Remove(SanPhamXuat);

                p.cbb_DSSanPham.SelectedIndex = -1;
                SoLuong = "0";
                GiaBan = "0";
                IsChange = false;
                IsAdd = false;
                IsRemove = false;
                IsSave = true;

                int stt = 1;
                foreach (var sp in SanPhamXuats)
                {
                    sp.STT = stt++;
                }
            });

            EditCommand = new RelayCommand<ThemPhieuXuatHangWindow>((p) => { return true; }, (p) => {
                int soLuongXuat = ConvertNumber.convertStringtoInt(SoLuong);
                int soLuongCon = SanPham.SanPham.SoLuong + SanPhamXuat.ChiTietPhieuXuatHang.SoLuong;

                if (soLuongCon < soLuongXuat || soLuongXuat == 0)
                {
                    MessageBox.Show($"Số lượng sản phẩm xuất không được phép nhỏ hơn hoặc vượt quá số lượng đang có ({soLuongCon} {SanPham.DonViTinh.Ten}).", "System Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (p.lv_DanhSachSanPhamXuat.SelectedIndex > -1)
                {
                    SanPham.SanPham.SoLuong = SanPham.SanPham.SoLuong - soLuongXuat + SanPhamXuat.ChiTietPhieuXuatHang.SoLuong;
                    SanPham.SoLuong = ConvertNumber.convertNumberToString(SanPham.SanPham.SoLuong);

                    IsSave = true;

                    tongtien -= SanPhamXuat.ThanhTienNum;

                    SanPhamXuat.SoLuong = SoLuong;
                    SanPhamXuat.GiaBan = GiaBan;
                    SanPhamXuat.ChiTietPhieuXuatHang.GiaBan = ConvertNumber.convertStringtoDecimal(GiaBan);
                    SanPhamXuat.ChiTietPhieuXuatHang.SoLuong = ConvertNumber.convertStringtoInt(SoLuong);

                    SanPhamXuat.SanPham = SanPham.SanPham;
                    SanPhamXuat.ChiTietPhieuXuatHang.IdSanPham = SanPham.SanPham.Id;

                    SanPhamXuat.DonViTinh = SanPham.DonViTinh;
                    SanPhamXuat.ThanhTienNum = SanPhamXuat.ChiTietPhieuXuatHang.GiaBan * SanPhamXuat.ChiTietPhieuXuatHang.SoLuong;
                    SanPhamXuat.ThanhTien = ConvertNumber.convertNumberDecimalToString(SanPhamXuat.ThanhTienNum);

                    tongtien += SanPhamXuat.ThanhTienNum;
                    TongTien = ConvertNumber.convertNumberDecimalToString(tongtien);
                }
                IsChange = false;
            });
            #endregion
        }

        private void ExportFileExcel()
        {
            string filePath = "";
            string ngayLapPhieu = NgayLapPhieu.ToString("yyyy-MM-dd");
            // tạo SaveFileDialog để lưu file excel
            SaveFileDialog dialog = new SaveFileDialog();

            // chỉ lọc ra các file có định dạng Excel
            dialog.Filter = "Excel | *.xlsx | Excel 2003 | *.xls";
            dialog.FileName = $"Phiếu xuất hàng_{DaiLy.Ten}_{ngayLapPhieu}.xlsx";

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
                    ep.Workbook.Properties.Title = "Phiếu xuất hàng";

                    //Tạo một sheet để làm việc trên đó
                    ep.Workbook.Worksheets.Add("Phiếu xuất hàng");

                    // lấy sheet vừa add ra để thao tác
                    ExcelWorksheet ws = ep.Workbook.Worksheets[0];

                    // đặt tên cho sheet
                    ws.Name = "Phiếu xuất hàng";
                    // fontsize mặc định cho cả sheet
                    ws.Cells.Style.Font.Size = 12;
                    // font family mặc định cho cả sheet
                    //ws.Cells.Style.Font.Name = "Calibri";
                    ws.Cells.Style.Font.Name = "Times New Roman";

                    // Tạo danh sách các column header
                    string[] arrColumnHeader = { "STT", "Mặt hàng", "Đơn vị tính", "Số lượng", "Đơn giá", "Thành tiền" };

                    // lấy ra số lượng cột cần dùng dựa vào số lượng header
                    var countColHeader = arrColumnHeader.Count();

                    // merge các column lại từ column 1 đến số column header
                    // gán giá trị cho cell vừa merge là Danh sách phiếu thu tiền
                    ws.Cells[1, 1].Value = $"Phiếu xuất hàng";
                    ws.Cells[1, 1, 1, countColHeader].Merge = true;
                    // in đậm
                    ws.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;
                    // căn giữa
                    ws.Cells[1, 1, 1, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    ws.Cells[2, 1].Value = $"Đại lý: {DaiLy.Ten}";
                    ws.Cells[2, 1, 2, 3].Merge = true;
                    ws.Cells[2, 1].Style.Font.Bold = true;
                    // căn giữa
                    ws.Cells[2, 1, 2, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    ws.Cells[2, 5].Value = $"Ngày lập phiếu:";
                    // in đậm
                    ws.Cells[2, 5].Style.Font.Bold = true;
                    ws.Cells[2, 6].Value = NgayLapPhieu.ToShortDateString();
                    ws.Cells[2, 6].Style.Font.Bold = true;

                    int colIndex = 1;
                    int rowIndex = 3;

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
                    foreach (var item in SanPhamXuats)
                    {
                        // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                        colIndex = 1;

                        // rowIndex tương ứng từng dòng dữ liệu
                        rowIndex++;

                        //gán giá trị cho từng cell                      
                        ws.Cells[rowIndex, colIndex++].Value = item.STT;
                        ws.Cells[rowIndex, colIndex++].Value = item.SanPham.Ten;
                        ws.Cells[rowIndex, colIndex++].Value = item.DonViTinh.Ten;
                        ws.Cells[rowIndex, colIndex++].Value = item.ChiTietPhieuXuatHang.SoLuong;
                        ws.Cells[rowIndex, colIndex++].Value = item.ChiTietPhieuXuatHang.GiaBan;
                        ws.Cells[rowIndex, colIndex++].Value = item.ThanhTienNum;

                        // lưu ý phải .ToShortDateString để dữ liệu khi in ra Excel là ngày như ta vẫn thấy.Nếu không sẽ ra tổng số :v
                        //ws.Cells[rowIndex, colIndex++].Value = item.Birthday.ToShortDateString();

                    }

                    colIndex = 1;
                    rowIndex++;
                    // merge các column lại từ column 1 đến số column header
                    // gán giá trị cho cell vừa merge là Danh sách phiếu thu tiền
                    ws.Cells[rowIndex, colIndex++].Value = $"Tổng tiền: ";
                    ws.Cells[rowIndex, 1, rowIndex, 5].Merge = true;
                    // in đậm
                    ws.Cells[rowIndex, 1, rowIndex, 5].Style.Font.Bold = true;
                    // căn phải
                    ws.Cells[rowIndex, 1, rowIndex, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;

                    ws.Cells[rowIndex, 6].Value = $"{tongtien}";
                    // in đậm
                    ws.Cells[rowIndex, countColHeader].Style.Font.Bold = true;

                    //Lưu file lại
                    Byte[] bin = ep.GetAsByteArray();
                    File.WriteAllBytes(filePath, bin);
                }
                MessageBox.Show("Xuất excel thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi lưu file!" + "\n" + ex.Message);
            }
        }

        private void loadData()
        {
            SanPham = new SanPhamHienThi();
            DaiLy = new DaiLy();
            SanPhamXuat = new SanPhamXuat();
            NgayLapPhieu = DateTime.Today;

            SoLuong = "0";
            GiaBan = "0";
            IsChange = false;
            IsAdd = false;
            IsRemove = false;
            IsSave = false;

            SanPhamXuats = new ObservableCollection<SanPhamXuat>();

            using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                // Load list san pham
                var sanPhams = new ObservableCollection<SanPham>(db.SanPhams.Where(sp => sp.TrangThai == true));
                var loaiSanPhams = new ObservableCollection<LoaiSanPham>(db.LoaiSanPhams);
                var nguonNhaps = new ObservableCollection<NguonNhap>(db.NguonNhaps);
                var donViTinhs = new ObservableCollection<DonViTinh>(db.DonViTinhs);

                foreach (var sp in sanPhams)
                {
                    if (sp.HinhAnh == null)
                    {
                        sp.HinhAnh = Path.GetFullPath("Assets/image_not_available.png");
                    }
                    else
                    {
                        sp.HinhAnh = Path.GetFullPath(sp.HinhAnh);
                    }

                }

                foreach (var l in loaiSanPhams)
                {
                    if (l.HinhAnh == null)
                    {
                        l.HinhAnh = Path.GetFullPath("Assets/image_not_available.png");
                    }
                    else
                    {
                        l.HinhAnh = Path.GetFullPath(l.HinhAnh);
                    }

                }

                foreach (var l in nguonNhaps)
                {
                    if (l.HinhAnh == null)
                    {
                        l.HinhAnh = Path.GetFullPath("Assets/image_not_available.png");
                    }
                    else
                    {
                        l.HinhAnh = Path.GetFullPath(l.HinhAnh);
                    }

                }
                SanPhams = new ObservableCollection<SanPhamHienThi>(
                from sp in sanPhams
                join lsp in loaiSanPhams
                on sp.IdLoaiSanPham equals lsp.Id
                join nn in nguonNhaps
                on sp.IdNguonNhap equals nn.Id
                join dv in donViTinhs
                on sp.IdDonViTinh equals dv.Id
                select new SanPhamHienThi
                {
                    SanPham = sp,
                    LoaiSanPham = lsp,
                    NguonNhap = nn,
                    GiaBan = ConvertNumber.convertNumberDecimalToString(sp.GiaBan),
                    GiaNhap = ConvertNumber.convertNumberDecimalToString(sp.GiaNhap),
                    SoLuong = ConvertNumber.convertNumberToString(sp.SoLuong),
                    DonViTinh = dv
                });

                //Load dai ly
                DaiLys = new ObservableCollection<DaiLy>(db.DaiLies);
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
