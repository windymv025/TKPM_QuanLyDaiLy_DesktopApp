using QuanLyDaiLy.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDaiLy.DAO
{
    public class SanPhamDAO
    {
        public static List<SanPham> GetAllSanPham()
        {
            List<SanPham> list = new List<SanPham>();
            using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                list = db.SanPhams.ToList();

                foreach (var i in list)
                {
                    i.HinhAnh = Path.GetFullPath(i.HinhAnh);
                }
            }

            return list;
        }
    }
}
