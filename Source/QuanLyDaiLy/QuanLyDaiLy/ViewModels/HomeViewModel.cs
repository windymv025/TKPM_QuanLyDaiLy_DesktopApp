using QuanLyDaiLy.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppQuanLyDaiLy.ViewModels
{
    public class HomeViewModel
    {
        public List<DaiLy> getAllDaiLy()
        {
            List<DaiLy> result = new List<DaiLy>();
            using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                string sql = $"select* from DaiLy where IsRemove = 0";
                result = db.DaiLies.SqlQuery(sql).ToList();
            }

            foreach (var item in result)
            {
                if (item.HinhAnh != null)
                {
                    item.HinhAnh = Path.GetFullPath(item.HinhAnh);
                }
                else
                {
                    item.HinhAnh = Path.GetFullPath("/Assets/image_not_available.png");
                }
            }

            return result;
        }

        public List<LoaiDaiLy> getAllLoaiDaiLy()
        {
            List<LoaiDaiLy> result = new List<LoaiDaiLy>();
            using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                result = db.LoaiDaiLies.ToList();
            }

            return result;
        }

        public void AddDaiLy(DaiLy daiLy)
        {
            using(DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                foreach (var item in db.DaiLies)
                {
                    if (item.Ten.Equals(daiLy.Ten))
                    {
                        return;
                    }
                }
                db.DaiLies.Add(daiLy);
                db.SaveChanges();
            }
        }

        public void AddLoaiDaiLy(LoaiDaiLy loaiDaiLy)
        {
            using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                db.LoaiDaiLies.Add(loaiDaiLy);
                db.SaveChanges();
            }
        }
    }
}
