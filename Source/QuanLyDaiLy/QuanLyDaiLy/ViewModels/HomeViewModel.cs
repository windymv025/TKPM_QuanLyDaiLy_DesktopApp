using QuanLyDaiLy.Model;
using System;
using System.Collections.Generic;
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
                result = db.DaiLies.ToList();
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
    }
}
