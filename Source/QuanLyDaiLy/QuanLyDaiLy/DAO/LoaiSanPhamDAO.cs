using QuanLyDaiLy.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDaiLy.DAO
{
    public class LoaiSanPhamDAO
    {
        public static List<LoaiSanPham> GetAllLoaiSanPham()
        {
            List<LoaiSanPham> list = new List<LoaiSanPham>();
            using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                list = db.LoaiSanPhams.ToList();

                foreach (var i in list)
                {
                    i.HinhAnh = Path.GetFullPath(i.HinhAnh);
                }
            }

            return list;
        }
    }

}
