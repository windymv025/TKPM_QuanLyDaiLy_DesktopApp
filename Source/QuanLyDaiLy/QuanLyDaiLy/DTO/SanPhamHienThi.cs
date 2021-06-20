using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDaiLy.DTO
{
    public class SanPhamHienThi
    {
        public int ID { get; set; }
        public string Ten { get; set; }
        public string MoTa { get; set; }
        public string HinhAnh { get; set; }
        public string HinhAnhLoai { get; set; }
        public string HinhAnhNguon { get; set; }
        public string DonGia { get; set; }
        public string SoLuong { get; set; }
        public int SoLuongNum { get; set; }
        public decimal DonGiaNum { get; set; }
    }
}
