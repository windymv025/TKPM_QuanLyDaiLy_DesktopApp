using QuanLyDaiLyMVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDaiLyMVVM.ViewModel
{
    public class CapNhatPhieuXuatHangViewModel: BaseViewModel
    {
        private string _TongTien;
        public string TongTien { get => _TongTien; set { _TongTien = value; OnPropertyChanged(); } }

        private string _SoLuong;
        public string SoLuong { get => _SoLuong; set { _SoLuong = value; OnPropertyChanged(); } }

        private string _GiaBan;
        public string GiaBan { get => _GiaBan; set { _GiaBan = value; OnPropertyChanged(); } }

        private DaiLy _DaiLy;
        public DaiLy DaiLy { get => _DaiLy; set { _DaiLy = value; OnPropertyChanged(); } }

        private PhieuXuatHang _PhieuXuatHang;
        public PhieuXuatHang PhieuXuatHang { get => _PhieuXuatHang; set { _PhieuXuatHang = value; OnPropertyChanged(); } }

        private SanPhamHienThi _SanPham;
        public SanPhamHienThi SanPham { get => _SanPham; set { _SanPham = value; OnPropertyChanged(); } }

        private SanPhamXuat _SanPhamXuat;
        public SanPhamXuat SanPhamXuat { get => _SanPhamXuat; set { _SanPhamXuat = value; OnPropertyChanged(); } }

        private PhieuXuatHangHienThi _PhieuXuatHangHienThi;
        public PhieuXuatHangHienThi PhieuXuatHangHienThi { get => _PhieuXuatHangHienThi; set { _PhieuXuatHangHienThi = value; OnPropertyChanged(); } }

        private ObservableCollection<ChiTietPhieuXuatHang> _ChiTietPhieuXuatHangs;
        public virtual ObservableCollection<ChiTietPhieuXuatHang> ChiTietPhieuXuatHangs { get => _ChiTietPhieuXuatHangs; set { _ChiTietPhieuXuatHangs = value; OnPropertyChanged(); } }
        
        private ObservableCollection<DaiLy> _DaiLys;
        public virtual ObservableCollection<DaiLy> DaiLys { get => _DaiLys; set { _DaiLys = value; OnPropertyChanged(); } }
        
        private ObservableCollection<SanPhamHienThi> _SanPhams;
        public ObservableCollection<SanPhamHienThi> SanPhams { get => _SanPhams; set { _SanPhams = value; OnPropertyChanged(); } }

        private ObservableCollection<SanPhamXuat> _SanPhamXuats;
        public ObservableCollection<SanPhamXuat> SanPhamXuats { get => _SanPhamXuats; set { _SanPhamXuats = value; OnPropertyChanged(); } }

        public CapNhatPhieuXuatHangViewModel()
        {
        }

        public CapNhatPhieuXuatHangViewModel(PhieuXuatHangHienThi phieuXuatHangHienThi)
        {
            loadData(phieuXuatHangHienThi);

        }

        private void loadData(PhieuXuatHangHienThi phieuXuatHangHienThi)
        {
            SanPham = new SanPhamHienThi();
            SoLuong = "0";
            GiaBan = "0";

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

                //Load san pham xuat
                SanPhamXuats = new ObservableCollection<SanPhamXuat>(
                    from i in db.ChiTietPhieuXuatHangs
                    where i.IdPhieuXuatHang == phieuXuatHangHienThi.PhieuDaiLy.Id
                    select new SanPhamXuat
                    {
                        SanPham = i.SanPham,
                        DonViTinh = i.SanPham.DonViTinh,
                        ChiTietPhieuXuatHang = i
                    });

                foreach(var sp in SanPhamXuats)
                {
                    sp.SoLuong = ConvertNumber.convertNumberToString(sp.ChiTietPhieuXuatHang.SoLuong);
                    sp.GiaBan = ConvertNumber.convertNumberDecimalToString(sp.ChiTietPhieuXuatHang.GiaBan);
                    sp.ThanhTienNum = sp.ChiTietPhieuXuatHang.GiaBan * sp.ChiTietPhieuXuatHang.SoLuong;
                    sp.ThanhTien = ConvertNumber.convertNumberDecimalToString(sp.ThanhTienNum);
                }
            }

            // Binding view
            DaiLy = DaiLys.Where(dl => dl.Id == phieuXuatHangHienThi.DaiLy.Id).FirstOrDefault();

            decimal tongtien = 0;

            int stt = 1;
            foreach(var i in SanPhamXuats)
            {
                tongtien += i.ThanhTienNum;
                i.STT = stt;
                stt++;
            }
            TongTien = ConvertNumber.convertNumberDecimalToString(tongtien);
        }
    }
}
