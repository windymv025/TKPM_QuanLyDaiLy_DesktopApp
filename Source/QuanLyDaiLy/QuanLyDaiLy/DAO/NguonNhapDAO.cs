using QuanLyDaiLy.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDaiLy.DAO
{
    public class NguonNhapDAO
    {
        public static List<NguonNhap> GetAllNguonNhap()
        {
            var NguonNhaps = new List<NguonNhap>();
            using(DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                NguonNhaps = db.NguonNhaps.ToList();
            }

            return NguonNhaps;
        }
    }
}
