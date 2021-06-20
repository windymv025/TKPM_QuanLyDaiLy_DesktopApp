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
            getSanPhams();
            getLoaiSanPhams();
            getNguonNhaps();
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

        private void getSanPhams()
        {
            SanPhams = new List<SanPham>();
            SanPhams = SanPhamDAO.GetAllSanPham();

            var result = from sp in SanPhams
                         join lsp in LoaiSanPhams
                         on sp.IDLoaiSanPham equals lsp.ID
                         join nn in NguonNhaps
                         on sp.IDNguonNhap equals nn.ID
                         select new SanPhamHienThi
                         {
                             ID = sp.ID,
                             Ten = sp.Ten,
                             DonGiaNum = sp.DonGia,
                             HinhAnh = Path.GetFullPath(sp.HinhAnh),
                             MoTa = sp.MoTa,
                             SoLuongNum = sp.SoLuong,
                             HinhAnhLoai = Path.GetFullPath(lsp.HinhAnh),
                             HinhAnhNguon = Path.GetFullPath(nn.HinhAnh)
                         };

            SanPhamHienThis = new List<SanPhamHienThi>();
            foreach (var i in result)
            {
                i.DonGia = ConvertNumber.convertNumberDecimalToString(i.DonGiaNum);
                i.SoLuong = ConvertNumber.convertNumberToString(i.SoLuongNum);
                SanPhamHienThis.Add(i);
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
