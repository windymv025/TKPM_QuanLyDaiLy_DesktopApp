using QuanLyDaiLyMVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using LiveCharts;
using LiveCharts.Wpf;
using OfficeOpenXml;
using Microsoft.Win32;
using OfficeOpenXml.Style;
using System.IO;

namespace QuanLyDaiLyMVVM.ViewModel
{
    public class BaoCaoViewModel : BaseViewModel
    {
        Func<ChartPoint, string> PointLabel => point => $"({point.Participation:P2})";
        private ObservableCollection<DoanhSo> _DoanhSoHienThi;
        public ObservableCollection<DoanhSo> DoanhSoHienThi { get => _DoanhSoHienThi; set { _DoanhSoHienThi = value;OnPropertyChanged(); } }
        private ObservableCollection<CongNo> _CongNoHienThi;
        public ObservableCollection<CongNo> CongNoHienThi { get => _CongNoHienThi; set { _CongNoHienThi = value; OnPropertyChanged(); } }

        private SeriesCollection _SeriesCollection_DoanhSo;
        public SeriesCollection SeriesCollection_DoanhSo { get=>_SeriesCollection_DoanhSo; set { _SeriesCollection_DoanhSo = value; OnPropertyChanged(); } }

        private int _Year;
        public int Year { get => _Year; set { _Year = value; OnPropertyChanged(); } }
        private decimal _TongDoanhSo;
        public decimal TongDoanhSo { get => _TongDoanhSo; set { _TongDoanhSo = value; OnPropertyChanged(); } }
        private decimal _TongCongNo;
        public decimal TongCongNo { get => _TongCongNo; set { _TongCongNo = value; OnPropertyChanged(); } }
        
        private ObservableCollection<int> _ListYear;
        public ObservableCollection<int> ListYear { get => _ListYear; set { _ListYear = value; OnPropertyChanged(); } }
        public List<string> Labels { get; set; } = new List<string>();

        public ICommand CBBChangedCommand { get; set; }
        private SeriesCollection _SeriesCollection_DoanhSoCot;
        public SeriesCollection SeriesCollection_DoanhSoCot { get => _SeriesCollection_DoanhSoCot; set { _SeriesCollection_DoanhSoCot = value; OnPropertyChanged(); } }
        private SeriesCollection _SeriesCollection_CongNoCot;
        public SeriesCollection SeriesCollection_CongNoCot { get => _SeriesCollection_CongNoCot; set { _SeriesCollection_CongNoCot = value; OnPropertyChanged(); } }

        public ICommand ExportDoanhThuCommand { get; set; }
        public ICommand ExportCongNoCommand { get; set; }

        public BaoCaoViewModel()
        {
            Year = DateTime.Now.Year;
            var list = new List<int>();
            for(int i = 2010; i <= DateTime.Now.Year; i++)
            {
                list.Add(i);
            }
            ListYear = new ObservableCollection<int>(list);

            DrawChart();

            CBBChangedCommand = new RelayCommand<QuyDinhWindow>((p) => { return true; }, (p) => {
                DrawChart();
            });

            ExportDoanhThuCommand = new RelayCommand<QuyDinhWindow>((q) => { return true; }, (q) => {
                string filePath = "";
                // tạo SaveFileDialog để lưu file excel
                SaveFileDialog dialog = new SaveFileDialog();

                // chỉ lọc ra các file có định dạng Excel
                dialog.Filter = "Excel | *.xlsx | Excel 2003 | *.xls";

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

                    using (ExcelPackage p = new ExcelPackage())
                    {
                        // đặt tên người tạo file
                        p.Workbook.Properties.Author = "Pham Van That";

                        // đặt tiêu đề cho file
                        p.Workbook.Properties.Title = "Báo cáo thống kê";

                        //Tạo một sheet để làm việc trên đó
                        p.Workbook.Worksheets.Add("Doanh số bán hàng");

                        // lấy sheet vừa add ra để thao tác
                        ExcelWorksheet ws = p.Workbook.Worksheets[0];

                        // đặt tên cho sheet
                        ws.Name = "Doanh số bán hàng";
                        // fontsize mặc định cho cả sheet
                        ws.Cells.Style.Font.Size = 12;
                        // font family mặc định cho cả sheet
                        //ws.Cells.Style.Font.Name = "Calibri";
                        ws.Cells.Style.Font.Name = "Times New Roman";

                        // Tạo danh sách các column header
                        string[] arrColumnHeader = {"Tên đại lý", "Tổng tiền bán được", "Tỷ lệ"};

                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();

                        // merge các column lại từ column 1 đến số column header
                        // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                        ws.Cells[1, 1].Value = $"Thống kê doanh số bán hàng năm {Year}";
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
                        foreach (var item in DoanhSoHienThi)
                        {
                            // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            colIndex = 1;

                            // rowIndex tương ứng từng dòng dữ liệu
                            rowIndex++;

                            //gán giá trị cho từng cell                      
                            ws.Cells[rowIndex, colIndex++].Value = item.TenDaiLy;
                            ws.Cells[rowIndex, colIndex++].Value = item.TongTien;
                            ws.Cells[rowIndex, colIndex++].Value = $"{100 * Math.Round(item.Tyle, 2)}%";

                            // lưu ý phải .ToShortDateString để dữ liệu khi in ra Excel là ngày như ta vẫn thấy.Nếu không sẽ ra tổng số :v
                            //ws.Cells[rowIndex, colIndex++].Value = item.Birthday.ToShortDateString();

                        }
                        ws.Cells[++rowIndex, 1].Value = "Tổng doanh số bán hàng";
                        ws.Cells[rowIndex, 1].Style.Font.Bold = true;

                        ws.Cells[rowIndex, 2].Value = TongDoanhSo;
                        ws.Cells[rowIndex, 2].Style.Font.Bold = true;

                        ws.Cells[rowIndex, 3].Value = "100%";
                        ws.Cells[rowIndex, 3].Style.Font.Bold = true;

                        //Lưu file lại
                        Byte[] bin = p.GetAsByteArray();
                        File.WriteAllBytes(filePath, bin);
                    }
                    MessageBox.Show("Xuất excel thành công!");
                }
                catch
                {
                    MessageBox.Show("Có lỗi khi lưu file!");
                }
            });

            ExportCongNoCommand = new RelayCommand<QuyDinhWindow>((q) => { return true; }, (q) => {
                string filePath = "";
                // tạo SaveFileDialog để lưu file excel
                SaveFileDialog dialog = new SaveFileDialog();

                // chỉ lọc ra các file có định dạng Excel
                dialog.Filter = "Excel | *.xlsx | Excel 2003 | *.xls";

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

                    using (ExcelPackage p = new ExcelPackage())
                    {
                        // đặt tên người tạo file
                        p.Workbook.Properties.Author = "Pham Van That";

                        // đặt tiêu đề cho file
                        p.Workbook.Properties.Title = "Báo cáo thống kê";

                        //Tạo một sheet để làm việc trên đó
                        p.Workbook.Worksheets.Add("Công nợ");

                        // lấy sheet vừa add ra để thao tác
                        ExcelWorksheet ws = p.Workbook.Worksheets[0];

                        // đặt tên cho sheet
                        ws.Name = "Công nợ";
                        // fontsize mặc định cho cả sheet
                        ws.Cells.Style.Font.Size = 12;
                        // font family mặc định cho cả sheet
                        //ws.Cells.Style.Font.Name = "Calibri";
                        ws.Cells.Style.Font.Name = "Times New Roman";

                        // Tạo danh sách các column header
                        string[] arrColumnHeader = { "Tên đại lý", "Tổng tiền nợ" };

                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();

                        // merge các column lại từ column 1 đến số column header
                        // gán giá trị cho cell vừa merge là Thống kê thông tni User Kteam
                        ws.Cells[1, 1].Value = $"Thống kê công nợ";
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
                        foreach (var item in CongNoHienThi)
                        {
                            // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            colIndex = 1;

                            // rowIndex tương ứng từng dòng dữ liệu
                            rowIndex++;

                            //gán giá trị cho từng cell                      
                            ws.Cells[rowIndex, colIndex++].Value = item.TenDaiLy;
                            ws.Cells[rowIndex, colIndex++].Value = item.TienNo;

                            // lưu ý phải .ToShortDateString để dữ liệu khi in ra Excel là ngày như ta vẫn thấy.Nếu không sẽ ra tổng số :v
                            //ws.Cells[rowIndex, colIndex++].Value = item.Birthday.ToShortDateString();

                        }
                        ws.Cells[++rowIndex, 1].Value = "Tổng tiền nợ";
                        ws.Cells[rowIndex, 1].Style.Font.Bold = true;

                        ws.Cells[rowIndex, 2].Value = TongCongNo;
                        ws.Cells[rowIndex, 2].Style.Font.Bold = true;

                        //Lưu file lại
                        Byte[] bin = p.GetAsByteArray();
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

        private void DrawChart()
        {
            /* select dl.Id, dl.Ten, Sum(pxh.TongTien) 
             * from DaiLy as dl, PhieuDaiLy as pdl, PhieuXuatHang as pxh 
             * where dl.Id = pdl.IdDaiLy and pxh.IdPhieuDaiLy = pdl.Id and year(pdl.NgayLapPhieu) = '2020' and dl.IsRemove = 0 
             * group by dl.Id, dl.Ten
             */
            DoanhSoHienThi = new ObservableCollection<DoanhSo>();
            CongNoHienThi = new ObservableCollection<CongNo>();
            SeriesCollection_DoanhSo = new SeriesCollection();

            SeriesCollection_DoanhSoCot = new SeriesCollection();
            SeriesCollection_CongNoCot = new SeriesCollection();

            using (var db = new DBQuanLyCacDaiLyEntities())
            {
                var daily = db.DaiLies.Where(x => x.IsRemove == false).ToList();
                var phieudaily = db.PhieuDaiLies.ToList();
                var phieuxuathang = db.PhieuXuatHangs.ToList();
                var phieuthutien = db.PhieuThuTiens.ToList();

                //Kết bảng đại lý và phiếu đại lý
                var query_daily_phieudaily = from dl in daily
                                             join pdl in phieudaily on dl.Id equals pdl.IdDaiLy
                                             select new
                                             {
                                                 iddl = dl.Id,
                                                 tendl = dl.Ten,
                                                 idpdl = pdl.Id
                                             };
                                
                //Kết bảng đại lý, phiếu đại lý và phiếu xuất hàng
                var daily_phieudaily_phieuxuathang = from p in query_daily_phieudaily
                            join pxh in phieuxuathang on p.idpdl equals pxh.IdPhieuDaiLy
                            where pxh.NgayLapPhieu.Year == Year
                            select new
                            {
                                IDDAILY = p.iddl,
                                TENDAILY = p.tendl,
                                TIEN = pxh.TongTien,
                                TIME = pxh.NgayLapPhieu
                            };

                //Group by => số tiền mà các đại lý mua
                var kq = from p in daily_phieudaily_phieuxuathang
                         group p by new { p.IDDAILY, p.TENDAILY } into gr
                         let money = gr.Sum(x => x.TIEN)
                         select new
                         {
                             IDDAILY = gr.Key.IDDAILY,
                             TENDAILY = gr.Key.TENDAILY,
                             TIEN = money
                         };
                decimal sum = 0;
                sum = kq.ToList().Sum(x => x.TIEN);
                foreach (var item in kq)
                {
                    DoanhSoHienThi.Add(new DoanhSo()
                    {
                        IdDaiLy = item.IDDAILY,
                        TenDaiLy = item.TENDAILY,
                        TongTien = item.TIEN,
                        Tyle = (double)(item.TIEN / sum)
                    });
                }

                TongDoanhSo = sum;

                /*===========================================Công nợ===============================================*/
                //Kết bảng đại lý, phiếu đại lý và phiếu thu tiền
                var daily_phieudaily_thutien = from dl in daily
                                               join pdl in phieudaily on dl.Id equals pdl.IdDaiLy
                                               join ptt in phieuthutien on pdl.Id equals ptt.IdPhieuDaiLy
                                                     select new
                                                     {
                                                         IDDAILY = dl.Id,
                                                         TENDAILY = dl.Ten,
                                                         TIEN = ptt.SoTienThu,
                                                         TIME = ptt.NgayThuTien
                                                     };
                var daily_phieudaily_phieuxuathang_noYear = from dl in daily
                                                            join pdl in phieudaily on dl.Id equals pdl.IdDaiLy
                                                            join pxh in phieuxuathang on pdl.Id equals pxh.IdPhieuDaiLy
                                                            select new
                                                            {
                                                                IDDAILY = dl.Id,
                                                                TENDAILY = dl.Ten,
                                                                TIEN = pxh.TongTien,
                                                                TIME = pxh.NgayLapPhieu
                                                            };
                
                
                //Group by => số tiền mua sản phẩm
                var kq2 = from p in daily_phieudaily_phieuxuathang_noYear
                          group p by new { p.IDDAILY, p.TENDAILY } into gr
                          let money = gr.Sum(x => x.TIEN)
                          select new
                          {
                              IDDAILY = gr.Key.IDDAILY,
                              TENDAILY = gr.Key.TENDAILY,
                              TIEN = money,
                          };
                //Group by => số tiền thu
                var kq3 = from p in daily_phieudaily_thutien
                          group p by new { p.IDDAILY, p.TENDAILY } into gr
                          let money = gr.Sum(x => x.TIEN)
                          select new
                          {
                              IDDAILY = gr.Key.IDDAILY,
                              TENDAILY = gr.Key.TENDAILY,
                              TIEN = money,
                          };

                //Tìm số tiền nợ
                var rs = from r1 in kq2
                             join r2 in kq3 on r1.IDDAILY equals r2.IDDAILY into a
                             from b in a.DefaultIfEmpty()
                             select new
                             {
                                 IDDAILY = r1.IDDAILY,
                                 TENDAILY = r1.TENDAILY,
                                 TIENNO = r1.TIEN - (b?.TIEN ?? 0)
                             };

                var result = rs.Where(x => x.TIENNO != 0);
                

                foreach (var item in DoanhSoHienThi)
                {
                    PieSeries pieSeries = new PieSeries()
                    {
                        Title = item.TenDaiLy,
                        DataLabels = true,
                        LabelPoint = PointLabel,
                        Values = new ChartValues<decimal> { item.TongTien }
                    };
                    SeriesCollection_DoanhSo.Add(pieSeries);


                    ColumnSeries columnSeries = new ColumnSeries()
                    {
                        Values = new ChartValues<decimal> { item.TongTien },
                        Title = item.TenDaiLy,
                        ToolTip = item.TongTien,
                        DataLabels = true
                    };
                    SeriesCollection_DoanhSoCot.Add(columnSeries);
                }

                /*================================CÔNG NỢ========================*/
                
                foreach (var item in result)
                {
                    CongNoHienThi.Add(new CongNo()
                    {
                        IdDaiLy = item.IDDAILY,
                        TenDaiLy = item.TENDAILY,
                        TienNo = item.TIENNO,
                    });
                }

                TongCongNo = CongNoHienThi.Sum(x => x.TienNo);

                foreach(var item in CongNoHienThi)
                {
                    ColumnSeries columnSeries = new ColumnSeries()
                    {
                        Values = new ChartValues<decimal> { item.TienNo },
                        Title = item.TenDaiLy,
                        DataLabels = true
                    };
                    SeriesCollection_CongNoCot.Add(columnSeries);
                }
            }
        }
    }
}
