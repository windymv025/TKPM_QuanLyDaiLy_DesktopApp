using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDaiLy.DTO
{
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
}
