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
    public class SanPhamDAO
    {
        public static List<SanPham> GetAllSanPham()
        {
            List<SanPham> list = new List<SanPham>();
            using(DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                list = db.SanPhams.ToList();

                foreach(var i in list)
                {
                    i.HinhAnh = Path.GetFullPath(i.HinhAnh);
                }
            }

            return list;
        }
    }
    public class LoaiSanPhamDAO
    {
        public static List<LoaiSanPham> GetAllLoaiSanPham()
        {
            List<LoaiSanPham> list = new List<LoaiSanPham>();
            using(DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                list = db.LoaiSanPhams.ToList();

                foreach(var i in list)
                {
                    i.HinhAnh = Path.GetFullPath(i.HinhAnh);
                }
            }

            return list;
        }
    }

    public class PagingInfo
    {
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int ItemInPerPage { get; set; }

        public PagingInfo(int numberOfItemInPerPage, int total)
        {
            if (total > 0)
            {
                CurrentPage = 1;
                ItemInPerPage = numberOfItemInPerPage;
                TotalPage = total / numberOfItemInPerPage +
                    ((total % numberOfItemInPerPage) == 0 ? 0 : 1);
            }
            else
            {
                CurrentPage = 0;
                TotalPage = 0;
            }

        }
    }

    public class SanPhamViewModel
    {
        public BindingList<SanPham> SanPhams { get; set; }
        public BindingList<LoaiSanPham> LoaiSanPhams { get; set; }
        public PagingInfo PagingInfo { get; set; }

        public SanPhamViewModel()
        {
            getSanPhams();
            getLoaiSanPhams();
        }

        private void getLoaiSanPhams()
        {
            LoaiSanPhams = new BindingList<LoaiSanPham>();
            var list = LoaiSanPhamDAO.GetAllLoaiSanPham();
            foreach (var i in list)
            {
                LoaiSanPhams.Add(i);
            }
        }

        private void getSanPhams()
        {
            SanPhams = new BindingList<SanPham>();
            var list = SanPhamDAO.GetAllSanPham();
            foreach(var i in list)
            {
                SanPhams.Add(i);
            }
        }

        public List<SanPham> loadPage(int pageNumber)
        {
            List<SanPham> result = new List<SanPham>();

            if (PagingInfo.TotalPage > 0)
            {
                if (pageNumber > PagingInfo.TotalPage)
                    pageNumber = 1;
                if (pageNumber < 1)
                    pageNumber = PagingInfo.TotalPage;

                PagingInfo.CurrentPage = pageNumber;

                result = SanPhams.Skip((pageNumber - 1) * PagingInfo.ItemInPerPage).Take(PagingInfo.ItemInPerPage).ToList();
            }
            else
            {
                PagingInfo.CurrentPage = 0;
            }
            return result;
        }
    }
}
