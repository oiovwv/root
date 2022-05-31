using Biz;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Compression
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
				var isTempRun = ConfigurationManager.AppSettings["IsTempRun"].ToString() == "Y" ? true : false;
				var month = int.Parse(ConfigurationManager.AppSettings["Month"].ToString());

				if (isTempRun)
                {
					Run(month);
                }
                else
                {
					DateTime now = DateTime.Now;
					string today = now.ToString("yyyyMMdd");
					string firstDay = new DateTime(now.Year, now.Month, 1).ToString("yyyyMMdd");
					if (today == firstDay)
					{
						Run(month);
					}
					else
					{
						Console.WriteLine("今天不是第一天");
						Cfg.WriteLog(Cfg.LogRootPath, "今天不是第一天");
					}
				}
			}
			catch(Exception ex)
            {
				Cfg.WriteLog(Cfg.LogRootPath, ex.Message);
			}			
		}

		public static void Run(int month)
        {
			EntDB conn = new EntDB();
			string sql = $"SELECT DISTINCT CLIENT_C FROM EDI_FILE_CONFIG";
			DataTable dt = conn.ExecuteToDataTable(sql);
			string yearAndMonth = DateTime.Now.AddMonths(-month).ToString("yyyyMM");
			string year = yearAndMonth.Substring(0, 4);
			foreach (DataRow dr in dt.Rows)
			{
				string client = dr["CLIENT_C"].ToString();
				Cfg.WriteLog(Cfg.LogRootPath, "当前正在压缩的客户：" + client);
				Console.WriteLine("当前正在压缩的客户：" + client);
				string movePath = ConfigurationManager.AppSettings["MovePath"].ToString() + client + "\\";
				CommonFunction.CreateDirIfNotExist(movePath);
				startCompress(client, movePath, year, yearAndMonth);
				Console.WriteLine(client + "压缩完成");
				Cfg.WriteLog(Cfg.LogRootPath, client + "压缩完成");
			}
			Console.WriteLine("所有客户压缩完成");
			Cfg.WriteLog(Cfg.LogRootPath, "所有客户压缩完成");
		}


		public static void startCompress(string client, string remotePath, string year, string backupFolder)
		{
			string path = "Y:\\STANDAEDI\\OrderProcessing\\" + client + "\\Backup\\";
			string backupPath = path + year + "\\" + backupFolder;
			string zipName = backupFolder + ".zip";
			string zipPath = path + year + "\\" + zipName;
			if (Directory.Exists(backupPath) && CompressDirectory(backupPath, zipPath, 9, deleteDir: false))
			{
				CommonFunction.Move(path + year + "\\" + zipName, remotePath, zipName);
				//DeleteFolder(backupPath);
			}
		}
		public static bool CompressDirectory(string dirPath, string GzipFileName, int CompressionLevel, bool deleteDir)
		{
			bool res = false;
			if (GzipFileName == string.Empty)
			{
				GzipFileName = dirPath.Substring(dirPath.LastIndexOf("\\") - 6, 6);
				GzipFileName = dirPath.Substring(0, dirPath.LastIndexOf("\\") - 6) + GzipFileName + ".zip";
			}
			Dictionary<string, DateTime> fileList = GetAllFies(dirPath);
			if (fileList.Count > 0)
			{
				using (ZipOutputStream zipoutputstream = new ZipOutputStream(File.Create(GzipFileName)))
				{
					zipoutputstream.SetLevel(CompressionLevel);
					Crc32 crc = new Crc32();
					foreach (KeyValuePair<string, DateTime> item in fileList)
					{
						FileStream fs = File.OpenRead(item.Key.ToString());
						byte[] buffer = new byte[fs.Length];
						fs.Read(buffer, 0, buffer.Length);
						ZipEntry entry = new ZipEntry(item.Key.Substring(dirPath.Length));
						entry.DateTime = item.Value;
						entry.Size = fs.Length;
						fs.Close();
						crc.Reset();
						crc.Update(buffer);
						entry.Crc = crc.Value;
						zipoutputstream.PutNextEntry(entry);
						zipoutputstream.Write(buffer, 0, buffer.Length);
					}
					res = true;
				}
				if (deleteDir)
				{
					Directory.Delete(dirPath, recursive: true);
				}
			}
			return res;
		}
		public static void Decompress(string GzipFile, string targetPath)
		{
			if (!Directory.Exists(targetPath))
			{
				Directory.CreateDirectory(targetPath);
			}
			byte[] data = new byte[2048];
			int size2 = 2048;
			ZipEntry theEntry2 = null;
			using (ZipInputStream s = new ZipInputStream(File.OpenRead(GzipFile)))
			{
				while ((theEntry2 = s.GetNextEntry()) != null)
				{
					if (theEntry2.IsDirectory)
					{
						if (!Directory.Exists(targetPath + theEntry2.Name))
						{
							Directory.CreateDirectory(targetPath + theEntry2.Name);
						}
					}
					else if (theEntry2.Name != string.Empty)
					{
						if (theEntry2.Name.Contains("\\"))
						{
							string parentDirPath = theEntry2.Name.Remove(theEntry2.Name.LastIndexOf("\\") + 1);
							if (!Directory.Exists(parentDirPath))
							{
								Directory.CreateDirectory(targetPath + parentDirPath);
							}
						}
						using (FileStream streamWriter = File.Create(targetPath + theEntry2.Name))
						{
							while (true)
							{
								size2 = s.Read(data, 0, data.Length);
								if (size2 <= 0)
								{
									break;
								}
								streamWriter.Write(data, 0, size2);
							}
							streamWriter.Close();
						}
					}
				}
				s.Close();
			}
		}
		private static Dictionary<string, DateTime> GetAllFies(string dir)
		{
			Dictionary<string, DateTime> FilesList = new Dictionary<string, DateTime>();
			DirectoryInfo fileDire = new DirectoryInfo(dir);
			if (!fileDire.Exists)
			{
				throw new FileNotFoundException("目录:" + fileDire.FullName + "没有找到!");
			}
			GetAllDirFiles(fileDire, FilesList);
			GetAllDirsFiles(fileDire.GetDirectories(), FilesList);
			return FilesList;
		}
		private static void GetAllDirsFiles(DirectoryInfo[] dirs, Dictionary<string, DateTime> filesList)
		{
			foreach (DirectoryInfo dir in dirs)
			{
				FileInfo[] files = dir.GetFiles("*.*");
				foreach (FileInfo file in files)
				{
					filesList.Add(file.FullName, file.LastWriteTime);
				}
				GetAllDirsFiles(dir.GetDirectories(), filesList);
			}
		}
		private static void GetAllDirFiles(DirectoryInfo dir, Dictionary<string, DateTime> filesList)
		{
			FileInfo[] files = dir.GetFiles("*.*");
			foreach (FileInfo file in files)
			{
				filesList.Add(file.FullName, file.LastWriteTime);
			}
		}
		public static void DeleteFolder(string deleteDirectory)
		{
			if (!Directory.Exists(deleteDirectory))
			{
				return;
			}
			string[] fileSystemEntries = Directory.GetFileSystemEntries(deleteDirectory);
			foreach (string deleteFile in fileSystemEntries)
			{
				if (File.Exists(deleteFile))
				{
					File.Delete(deleteFile);
				}
				else
				{
					DeleteFolder(deleteFile);
				}
			}
			Directory.Delete(deleteDirectory);
		}
	}
}
