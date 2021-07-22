using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biz;

namespace FileMove
{
    class Program
    {
        static void Main(string[] args)
        {
            string originFilesPath = ConfigurationManager.AppSettings["InPath"];
            string remoteFilePath = ConfigurationManager.AppSettings["OutPath"];
            var isAll = ConfigurationManager.AppSettings["AllFile"] == "0" ? true : false;
            List<string> files = new List<string>();
            CommonFunction.CreateDirIfNotExist(originFilesPath);
            CommonFunction.GetAllFiles(originFilesPath, files, isRecursive: isAll);
            foreach (string filePath in files)
            {
                string fileName = new FileInfo(filePath).Name;
                CommonFunction.Move(filePath, remoteFilePath, fileName);
            }
        }
    }
}
