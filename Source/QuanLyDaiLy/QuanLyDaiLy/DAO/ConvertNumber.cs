using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDaiLy.DAO
{
    public class ConvertNumber
    {
        public static string convertNumberToString(int num)
        {
            string temp = "";
            string result = "";
            string number = num.ToString();

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

            return result;
        }

        public static string convertNumberDecimalToString(decimal num)
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

            result += "," + num.ToString().Split('.')[1];

            return result;
        }
    }
}
