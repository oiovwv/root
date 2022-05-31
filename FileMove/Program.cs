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
            string remoteFilePath = ConfigurationManager.AppSettings["OutPath"] + DateTime.Now.ToString("yyyyMMdd") + "\\";
            //int days = System.Threading.Thread.CurrentThread.CurrentUICulture.Calendar.GetDaysInMonth(DateTime.Now.Year, DateTime.Now.Month);

            //int days = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            //var path = string.Empty;
            //for (var i = 1; i < days - 8; i++) 
            //{
            //    if (i >= 10)
            //    {
            //        path = remoteFilePath + "202204" + i;
            //    }
            //    else
            //    {
            //        path = remoteFilePath + "2022040" + i;
            //    }
                

            //    CommonFunction.CreateDirIfNotExist(path);
            //}


            var isAll = ConfigurationManager.AppSettings["AllFile"] == "0" ? true : false;
            List<string> files = new List<string>();    
            CommonFunction.CreateDirIfNotExist(originFilesPath);
            CommonFunction.CreateDirIfNotExist(remoteFilePath);
            CommonFunction.GetAllFiles(originFilesPath, files, isRecursive: isAll);
            foreach (string filePath in files)
            {
                string fileName = new FileInfo(filePath).Name;
                CommonFunction.Move(filePath, remoteFilePath, fileName);
            }
        }
    }
}
