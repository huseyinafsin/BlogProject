using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Helpers
{
    public class FileHelperManager
    {
        private static readonly string RootDirectory = Environment.CurrentDirectory + "\\wwwroot";

        public static string Upload(IFormFile file, string rootPath)
        {
            if (file.Length > 0)
            {
                if (!Directory.Exists(rootPath))
                {

                    Directory.CreateDirectory(rootPath);
                }
                string extension = Path.GetExtension(file.FileName);
                string guid = GuidHelper.CreateGuid();
                string filePath = rootPath + "/" + guid + extension;

                try
                {
                    using (FileStream fileStream = File.Create(filePath))
                    {
                        file.CopyTo(fileStream);
                        fileStream.Flush();
                        return filePath;
                    }
                }
                catch (Exception e)
                {
                }


            }
            return null;


        }

        public static bool Delete(string filePath)
        {
            var path = $"{RootDirectory}\\{filePath.Replace("/", "\\")}";

            if (File.Exists(path))
            {
                File.Delete(path);
                return true;
            }

            return false;
        }

        public static string Update(IFormFile file, string filePath, string rootPath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            return Upload(file, rootPath);
        }
    }
}
