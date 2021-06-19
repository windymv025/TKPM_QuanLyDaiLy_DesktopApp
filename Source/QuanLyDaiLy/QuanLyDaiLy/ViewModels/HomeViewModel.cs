using QuanLyDaiLy.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppQuanLyDaiLy.ViewModels
{
    public class SanPhamTop
    {
        public int ID { get; set; }
        public string SoLuong { get; set; }
        public int SoLuongNum { get; set; }
        public string HinhAnh { get; set; }
        public string Ten { get; set; }
    }

    public class DaiLyTop
    {
        public int ID { get; set; }
        public decimal TongTienXuatNum { get; set; }
        public string TongTienXuat { get; set; }
        public string HinhAnh { get; set; }
        public string Ten { get; set; }
    }

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
                    dlt.TongTienXuat = convertNumberDecimalToString(dlt.TongTienXuatNum);
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
                    spt.SoLuong = convertNumberToString(spt.SoLuongNum);
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

        private string convertNumberToString(int num)
        {
            string temp = "";
            string result = "";
            string number = num.ToString();

            for(int i = number.Length - 1; i >= 0; i--)
            {
                temp += number[i];
                if ( i > 0 && number.Length - i > 2 && (number.Length - i) % 3 == 0) 
                {
                    temp += ".";
                }
            }

            for (int i = temp.Length - 1; i >= 0; i--)
            {
                result+= temp[i];
            }

                return result;
        }

        private string convertNumberDecimalToString(decimal num)
        {
            string temp = "";
            string result = "";
            string number = num.ToString().Split('.')[0];

            for (int i = number.Length - 1; i >= 0; i--)
            {
                temp += number[i];
                if (i > 0 && number.Length - i > 2 && (number.Length - i) % 3 == 0)
                {
                    temp += ".";
                }
            }

            for (int i = temp.Length - 1; i >= 0; i--)
            {
                result += temp[i];
            }

            result += "," + num.ToString().Split('.')[1] + " VND";

            return result;
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
