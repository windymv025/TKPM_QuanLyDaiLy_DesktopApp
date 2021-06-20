using QuanLyDaiLy.DAO;
using QuanLyDaiLy.DTO;
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
        public SanPhamTop SanPhamTop1 { get; set; }
        public SanPhamTop SanPhamTop2 { get; set; }
        public SanPhamTop SanPhamTop3 { get; set; }

        public DaiLyTop DaiLyTop1 { get; set; }
        public DaiLyTop DaiLyTop2 { get; set; }
        public DaiLyTop DaiLyTop3 { get; set; }

        public HomeViewModel()
        {
            SanPhamTop1 = new SanPhamTop();
            SanPhamTop2 = new SanPhamTop();
            SanPhamTop3 = new SanPhamTop();

            DaiLyTop1 = new DaiLyTop();
            DaiLyTop2 = new DaiLyTop();
            DaiLyTop3 = new DaiLyTop();

            getTopSanPham();

            getTopDaiLy();
        }

        public void getTopDaiLy()
        {
            using (DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                var listDaiLy = (from pdl in db.PhieuDaiLies
                                 join pxh in db.PhieuXuatHangs
                                 on pdl.ID equals pxh.ID
                                 join dl in db.DaiLies
                                 on pdl.IDDaiLy equals dl.ID
                                 select new
                                 {
                                     ID = pdl.IDDaiLy,
                                     TongTien = pxh.TongTien
                                 }).GroupBy(t => t.ID);

                var listDaiLyTop = new List<DaiLyTop>();

                foreach (var group in listDaiLy)
                {
                    var dlt = new DaiLyTop { ID = group.Key };
                    dlt.TongTienXuatNum = 0;
                    foreach (var i in group)
                    {
                        dlt.TongTienXuatNum += i.TongTien;
                    }
                    dlt.TongTienXuat = ConvertNumber.convertNumberDecimalToString(dlt.TongTienXuatNum);
                    listDaiLyTop.Add(dlt);
                }


                for (int i = 0; i < listDaiLyTop.Count; i++)
                {
                    for (int j = i + 1; j < listDaiLyTop.Count; j++)
                    {
                        if (listDaiLyTop[i].TongTienXuatNum < listDaiLyTop[j].TongTienXuatNum)
                        {
                            var temp = listDaiLyTop[i];
                            listDaiLyTop[i] = listDaiLyTop[j];
                            listDaiLyTop[j] = temp;
                        }
                    }
                }


                foreach (var i in listDaiLyTop)
                {
                    foreach (var dl in db.DaiLies)
                    {
                        if (i.ID == dl.ID)
                        {
                            if (dl.HinhAnh != null)
                                i.HinhAnh = Path.GetFullPath(dl.HinhAnh);
                            else
                                i.HinhAnh = Path.GetFullPath("/Assets/image_not_available.png");
                            i.Ten = dl.Ten;
                        }
                    }
                }


                if (listDaiLyTop.Count >= 0)
                {
                    DaiLyTop1 = listDaiLyTop[0];
                }
                if (listDaiLyTop.Count >= 1)
                {
                    DaiLyTop2 = listDaiLyTop[1];
                }

                if (listDaiLyTop.Count >= 2)
                {
                    DaiLyTop3 = listDaiLyTop[2];
                }

            }
        }

        

        public void getTopSanPham()
        {
            using(DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                var listPhieuXuat = db.ChiTietPhieuXuatHangs.GroupBy(t=>t.IDSanPham);

                var listSanPhamTop = new List<SanPhamTop>();

                foreach (var group in listPhieuXuat)
                {
                    var spt = new SanPhamTop { ID = group.Key};
                    spt.SoLuongNum = 0;
                    foreach(var i in group)
                    {
                        spt.SoLuongNum += i.SoLuong;
                    }
                    spt.SoLuong = ConvertNumber.convertNumberToString(spt.SoLuongNum);
                    listSanPhamTop.Add(spt);
                }

                
                for(int i = 0; i < listSanPhamTop.Count; i++)
                {
                    for (int j = i + 1; j < listSanPhamTop.Count; j++)
                    {
                        if(listSanPhamTop[i].SoLuongNum < listSanPhamTop[j].SoLuongNum)
                        {
                            var temp = listSanPhamTop[i];
                            listSanPhamTop[i] = listSanPhamTop[j];
                            listSanPhamTop[j] = temp;
                        }
                    }
                }
                    

                foreach( var i in listSanPhamTop)
                {
                    foreach(var sp in db.SanPhams)
                    {
                        if(i.ID == sp.ID)
                        {
                            if (sp.HinhAnh != null)
                                i.HinhAnh = Path.GetFullPath(sp.HinhAnh);
                            else
                                i.HinhAnh = Path.GetFullPath("/Assets/image_not_available.png");

                            i.Ten = sp.Ten;
                        }
                    }
                }
                
                
                if(listSanPhamTop.Count >= 0)
                {
                    SanPhamTop1 = listSanPhamTop[0];
                }
                if (listSanPhamTop.Count >= 1)
                {
                    SanPhamTop2 = listSanPhamTop[1];
                }

                if (listSanPhamTop.Count >= 2)
                {
                    SanPhamTop3 = listSanPhamTop[2];
                }

            }
        }

        public DaiLy getDaiLyTheoTop(DaiLyTop daiLyTop)
        {
            DaiLy dl = new DaiLy();

            using(DBQuanLyCacDaiLyEntities db = new DBQuanLyCacDaiLyEntities())
            {
                dl = db.DaiLies.Where(d => d.ID == daiLyTop.ID).FirstOrDefault();
            }

            return dl;
        }
    }
}
