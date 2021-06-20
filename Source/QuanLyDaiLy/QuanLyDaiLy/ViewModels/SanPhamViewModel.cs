using QuanLyDaiLy.DAO;
using QuanLyDaiLy.DTO;
using QuanLyDaiLy.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDaiLy.ViewModels
{
     public class SanPhamViewModel
    {
        public List<SanPham> SanPhams { get; set; }
        public List<SanPhamHienThi> SanPhamHienThis { get; set; }

        public List<LoaiSanPham> LoaiSanPhams { get; set; }
        public List<NguonNhap> NguonNhaps { get; set; }
        public PagingInfo PagingInfo { get; set; }

        public SanPhamViewModel()
        {
            getLoaiSanPhams();
            getNguonNhaps();
            getSanPhams();

            PagingInfo = new PagingInfo(3, SanPhams.Count);
        }

        private void getNguonNhaps()
        {
            NguonNhaps = new List<NguonNhap>();
            NguonNhaps = NguonNhapDAO.GetAllNguonNhap();
        }

        private void getLoaiSanPhams()
        {
            LoaiSanPhams = new List<LoaiSanPham>();
            LoaiSanPhams = LoaiSanPhamDAO.GetAllLoaiSanPham();
        }

        public void getSanPhams()
        {
            SanPhams = new List<SanPham>();
            SanPhams = SanPhamDAO.GetAllSanPham();

            using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                var result = (from sp in db.SanPhams
                              select new SanPhamHienThi
                              {
                                  ID = sp.ID,
                                  Ten = sp.Ten,
                                  DonGiaNum = sp.DonGia,
                                  HinhAnh = sp.HinhAnh,
                                  MoTa = sp.MoTa,
                                  SoLuongNum = sp.SoLuong,
                                  HinhAnhLoai = sp.LoaiSanPham.HinhAnh,
                                  HinhAnhNguon = sp.NguonNhap.HinhAnh,
                                  DonGia = "",
                                  SoLuong = ""
                              }).ToList();


                SanPhamHienThis = new List<SanPhamHienThi>();
                foreach (var sp in result)
                {
                    if (sp.HinhAnh == null)
                    {
                        sp.HinhAnh = Path.GetFullPath("Assets/image_not_available.png");
                    }
                    else
                    {
                        sp.HinhAnh = Path.GetFullPath(sp.HinhAnh);
                    }

                    if(sp.HinhAnhLoai == null)
                    {
                        sp.HinhAnhLoai = Path.GetFullPath("Assets/image_not_available.png");

                    }
                    else
                    {
                        sp.HinhAnhLoai = Path.GetFullPath(sp.HinhAnhLoai);
                    }

                    if (sp.HinhAnhNguon == null)
                    {
                        sp.HinhAnhNguon = Path.GetFullPath("Assets/image_not_available.png");

                    }
                    else
                    {
                        sp.HinhAnhNguon = Path.GetFullPath(sp.HinhAnhNguon);
                    }
                    SanPhamHienThis.Add(new SanPhamHienThi
                    {
                        ID = sp.ID,
                        Ten = sp.Ten,
                        DonGiaNum = sp.DonGiaNum,
                        HinhAnh = sp.HinhAnh,
                        MoTa = sp.MoTa,
                        SoLuongNum = sp.SoLuongNum,
                        HinhAnhLoai = sp.HinhAnhLoai,
                        HinhAnhNguon = sp.HinhAnhNguon,
                        DonGia = ConvertNumber.convertNumberDecimalToString(sp.DonGiaNum),
                        SoLuong = ConvertNumber.convertNumberToString(sp.SoLuongNum)
                    });
                }
            }
        }

        public List<SanPhamHienThi> loadPageHienThi(int pageNumber)
        {
            List<SanPhamHienThi> result = new List<SanPhamHienThi>();

            if (PagingInfo.TotalPage > 0)
            {
                if (pageNumber > PagingInfo.TotalPage)
                    pageNumber = 1;
                if (pageNumber < 1)
                    pageNumber = PagingInfo.TotalPage;

                PagingInfo.CurrentPage = pageNumber;

                result = SanPhamHienThis.Skip((pageNumber - 1) * PagingInfo.ItemInPerPage).Take(PagingInfo.ItemInPerPage).ToList();
            }
            else
            {
                PagingInfo.CurrentPage = 0;
            }
            return result;
        }
    }
}
