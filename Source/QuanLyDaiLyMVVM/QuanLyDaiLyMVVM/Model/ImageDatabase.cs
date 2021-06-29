using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyDaiLyMVVM.Model
{
    public class ImageDatabase
    {
        public const string PATH_PRODUCT = "Images/Product";
        public const string PATH_CATEGORY = "Images/Category";
        public const string PATH_ImportSource = "Images/ImportSource";
        public static string savingImageToDatabase(string pathImage, string pathDatabase)
        {
            string newPath = "";
            var currentFolder = AppDomain.CurrentDomain.BaseDirectory.ToString();
            string uriImage = currentFolder.ToString();
            string file = pathImage;


            //Lấy file ảnh copy vào Images của project
            var info = new FileInfo(file);
            var newName = $"{Guid.NewGuid()}{info.Extension}";
            File.Copy(file, $"{uriImage}{pathDatabase}\\{newName}");

            newPath = $"{pathDatabase}/{newName}";

            return newPath;
        }
    }
}
