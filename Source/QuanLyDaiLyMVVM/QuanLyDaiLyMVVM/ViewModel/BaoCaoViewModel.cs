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
        private ObservableCollection<int> _ListYear;
        public ObservableCollection<int> ListYear { get => _ListYear; set { _ListYear = value; OnPropertyChanged(); } }
        public List<string> Labels { get; set; } = new List<string>();

        public ICommand CBBChangedCommand { get; set; }
        private SeriesCollection _SeriesCollection_DoanhSoCot;
        public SeriesCollection SeriesCollection_DoanhSoCot { get => _SeriesCollection_DoanhSoCot; set { _SeriesCollection_DoanhSoCot = value; OnPropertyChanged(); } }
        private SeriesCollection _SeriesCollection_CongNoCot;
        public SeriesCollection SeriesCollection_CongNoCot { get => _SeriesCollection_CongNoCot; set { _SeriesCollection_CongNoCot = value; OnPropertyChanged(); } }

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
                                             where pdl.NgayLapPhieu.Year == Year
                                             select new
                                             {
                                                 iddl = dl.Id,
                                                 tendl = dl.Ten,
                                                 time = pdl.NgayLapPhieu,
                                                 idpdl = pdl.Id
                                             };
                                
                //Kết bảng đại lý, phiếu đại lý và phiếu xuất hàng
                var daily_phieudaily_phieuxuathang = from p in query_daily_phieudaily
                            join pxh in phieuxuathang on p.idpdl equals pxh.IdPhieuDaiLy
                            select new
                            {
                                IDDAILY = p.iddl,
                                TENDAILY = p.tendl,
                                TIEN = pxh.TongTien,
                                TIME = p.time
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
                                                         TIME = pdl.NgayLapPhieu
                                                     };
                var daily_phieudaily_phieuxuathang_noYear = from dl in daily
                                                            join pdl in phieudaily on dl.Id equals pdl.IdDaiLy
                                                            join pxh in phieuxuathang on pdl.Id equals pxh.IdPhieuDaiLy
                                                            select new
                                                            {
                                                                IDDAILY = dl.Id,
                                                                TENDAILY = dl.Ten,
                                                                TIEN = pxh.TongTien,
                                                                TIME = pdl.NgayLapPhieu
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
