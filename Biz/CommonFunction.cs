using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;

namespace Biz
{
    public class CommonFunction
    {
        //移动文件
        public static void Move(string oldPath, string newPath, string fileName)
        {
            newPath = ((!File.Exists(newPath + fileName)) ? (newPath + fileName) : (newPath + DateTime.Now.ToString("yyyyMMddHHmmssfff") + "_" + fileName));
            File.Move(oldPath, newPath);
        }

        //创建目录
        public static void CreateDirIfNotExist(string dir)
        {
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }


        //获取目录下所有文件
        public static void GetAllFiles(string path, List<string> list, bool isRecursive)
        {
            DirectoryInfo d = new DirectoryInfo(path);
            string[] fileList = Directory.GetFiles(path);
            FileInfo[] files = d.GetFiles();
            DirectoryInfo[] directs = d.GetDirectories();
            string[] array = fileList;
            foreach (string f in array)
            {
                list.Add(f);
            }
            if (isRecursive)
            {
                DirectoryInfo[] array2 = directs;
                foreach (DirectoryInfo dd in array2)
                {
                    GetAllFiles(dd.FullName, list, isRecursive: true);
                }
            }
        }

        public string FormatDate(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return date;
            }
            string o = string.Empty;
            string year = date.Substring(date.LastIndexOf(".") + 1);
            string month = date.Substring(date.IndexOf(".") + 1, date.LastIndexOf(".") - date.IndexOf(".") - 1);
            string day = date.Substring(0, date.IndexOf("."));
            return year + "-" + month + "-" + day;
        }

        public static string ChooseFile()
        {
            string filePath = string.Empty;
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "所有文件(*.*)|*.*";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                filePath = fileDialog.FileName;
            }
            return filePath;
        }


        //修改文件后缀
        public static void ChangeFileNameFromTmpToTxt(string filePath, string fileName)
        {
            File.Move(filePath + "\\" + fileName, filePath + "\\" + fileName.Substring(0, fileName.Length - 4) + ".txt");
        }



        public static string SBCToDBC(string input)
        {
            char[] cc = input.ToCharArray();
            for (int i = 0; i < cc.Length; i++)
            {
                if (cc[i] == '\u3000')
                {
                    cc[i] = ' ';
                }
                else if (cc[i] > '\uff00' && cc[i] < '｟')
                {
                    cc[i] = (char)(cc[i] - 65248);
                }
            }
            return new string(cc);
        }


        #region   请求接口数据
        public static DataTable GetDataFromJDA(string client_c, string wh_id)
        {
            try
            {
                NameValueCollection VarPost = new NameValueCollection();
                VarPost.Clear();
                VarPost.Add("client_c", client_c);
                VarPost.Add("wh_id", wh_id);
                string stockJson = PostData("GetLSLStockData", VarPost);
                return ToDataTable(stockJson);
            }
            catch (Exception)
            {
                return null;
            }
        }
        private static string PostData(string actionName, NameValueCollection VarPost)
        {
            try
            {
                WebClient web = new WebClient();
                byte[] byRemoteInfo = web.UploadValues("http://10.205.200.28:9527/api/LONInvFeed/" + actionName, "POST", VarPost);
                return Encoding.UTF8.GetString(byRemoteInfo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static DataTable ToDataTable(string json)
        {
            DataTable dataTable = new DataTable();  //实例化
            DataTable result;
            try
            {
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                javaScriptSerializer.MaxJsonLength = Int32.MaxValue; //取得最大数值
                ArrayList arrayList = javaScriptSerializer.Deserialize<ArrayList>(json);
                if (arrayList.Count > 0)
                {
                    foreach (Dictionary<string, object> dictionary in arrayList)
                    {
                        if (dictionary.Keys.Count<string>() == 0)
                        {
                            result = dataTable;
                            return result;
                        }
                        if (dataTable.Columns.Count == 0)
                        {
                            foreach (string current in dictionary.Keys)
                            {
                                dataTable.Columns.Add(current, dictionary[current].GetType());
                            }
                        }
                        DataRow dataRow = dataTable.NewRow();
                        foreach (string current in dictionary.Keys)
                        {
                            dataRow[current] = dictionary[current];
                        }

                        dataTable.Rows.Add(dataRow); //循环添加行到DataTable中
                    }
                }
            }
            catch { }
            result = dataTable;
            return result;
        }
        #endregion

        public static DataSet ReadUperExcelFileToDataSet(string fileFullPath, int startLine, int columnCount, string sheetName)
        {
            string strConn = "Provider=Microsoft.Ace.OleDb.12.0;data source = " + fileFullPath + ";Extended Properties = 'Excel 12.0; HDR=NO; IMEX=1'";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[4]
            {
            null,
            null,
            null,
            "TABLE"
            });
            DataSet dsExcel = new DataSet();
            try
            {
                string strExcel = "select * from [" + sheetName + "$]";
                OleDbDataAdapter myCommand = new OleDbDataAdapter(strExcel, strConn);
                myCommand.Fill(dsExcel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            if (dsExcel.Tables[0].Rows.Count > startLine)
            {
                DataSet ds = new DataSet();
                ds.Tables.Add("FileTable");
                for (int k = 0; k < columnCount; k++)
                {
                    ds.Tables["FileTable"].Columns.Add("Column_" + k.ToString(), Type.GetType("System.String"));
                }
                for (int j = startLine; j < dsExcel.Tables[0].Rows.Count; j++)
                {
                    string allFields2 = "";
                    for (int i = 0; i < columnCount; i++)
                    {
                        allFields2 = ((i < dsExcel.Tables[0].Columns.Count) ? (allFields2 + dsExcel.Tables[0].Rows[j][i].ToString().Trim() + "+|+") : (allFields2 + "+|+"));
                    }
                    allFields2 = allFields2.Substring(0, allFields2.Length - 3);
                    if (allFields2.Replace("+|+", "").Trim().Length > 0)
                    {
                        DataRowCollection rows = ds.Tables["FileTable"].Rows;
                        object[] values = allFields2.Split(new string[1]
                        {
                        "+|+"
                        }, StringSplitOptions.None);
                        rows.Add(values);
                    }
                }
                return ds;
            }
            throw new Exception("第一个表格没有订单");
        }

        public static DataSet ReadFileBySplitterInOneLine(string fileFullPath, int startLineIndex, string splitter, int columnCount, string encodingName)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add("FileTable");
            for (int j = 0; j < columnCount; j++)
            {
                ds.Tables["FileTable"].Columns.Add("Column_" + j.ToString(), Type.GetType("System.String"));
            }
            StreamReader sr = new StreamReader(fileFullPath, (encodingName.Length == 0) ? Encoding.Default : Encoding.GetEncoding(encodingName));
            string lineText2 = "";
            for (int l = 0; l < startLineIndex; l++)
            {
                if (sr.ReadLine() == null)
                {
                    return ds;
                }
            }
            string text6 = sr.ReadToEnd();
            string text5 = text6.Replace("\r\n", "^_^");
            string text4 = text5.Replace("\n", " ");
            string text3 = text4.Replace("^_^", "\n");
            string[] array = text3.Split('\n');
            for (int k = 0; k < array.Length; k++)
            {
                lineText2 = array[k];
                string allFields2 = "";
                string[] lineArray = array[k].Split(new string[1]
                {
                splitter
                }, StringSplitOptions.None);
                for (int i = 0; i < columnCount; i++)
                {
                    allFields2 = ((lineArray.Length <= i) ? (allFields2 + "+|+") : (allFields2 + lineArray[i].Trim() + "+|+"));
                }
                allFields2 = allFields2.Substring(0, allFields2.Length - 3);
                if (allFields2.Replace("+|+", "").Trim().Length > 0)
                {
                    DataRowCollection rows = ds.Tables["FileTable"].Rows;
                    object[] values = allFields2.Split(new string[1]
                    {
                    "+|+"
                    }, StringSplitOptions.None);
                    rows.Add(values);
                }
            }
            sr.Close();
            sr.Dispose();
            return ds;
        }

        public static DataSet ReadExcelFileToDataSet(string fileFullPath, int startLine, int columnCount)
        {
            string ExcelTableName = "";
            string strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileFullPath + ";Extended Properties='Excel 8.0;HDR=NO;IMEX=1';";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[4]
            {
        null,
        null,
        null,
        "TABLE"
            });
            DataSet dsExcel = new DataSet();
            try
            {
                DataRow dr = schemaTable.Rows[0];
                ExcelTableName = dr["TABLE_NAME"].ToString().Trim();
                string strExcel = "select * from [" + ExcelTableName + "]";
                OleDbDataAdapter myCommand = new OleDbDataAdapter(strExcel, strConn);
                myCommand.Fill(dsExcel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conn.Close();
            }
            if (dsExcel.Tables[0].Rows.Count > startLine)
            {
                DataSet ds = new DataSet();
                ds.Tables.Add("FileTable");
                for (int k = 0; k < columnCount; k++)
                {
                    ds.Tables["FileTable"].Columns.Add("Column_" + k.ToString(), Type.GetType("System.String"));
                }
                for (int j = startLine; j < dsExcel.Tables[0].Rows.Count; j++)
                {
                    string allFields2 = "";
                    for (int i = 0; i < columnCount; i++)
                    {
                        allFields2 = ((i < dsExcel.Tables[0].Columns.Count) ? (allFields2 + dsExcel.Tables[0].Rows[j][i].ToString().Trim() + "+|+") : (allFields2 + "+|+"));
                    }
                    allFields2 = allFields2.Substring(0, allFields2.Length - 3);
                    if (allFields2.Replace("+|+", "").Trim().Length > 0)
                    {
                        DataRowCollection rows = ds.Tables["FileTable"].Rows;
                        object[] values = allFields2.Split(new string[1]
                        {
                    "+|+"
                        }, StringSplitOptions.None);
                        rows.Add(values);
                    }
                }
                return ds;
            }
            throw new Exception("第一个表格没有订单");
        }

        public static string ModelToJson<T>(T model)
        {
            return JsonConvert.SerializeObject(model,
                new JsonSerializerSettings
                {
                    ContractResolver = new SortContractResolver(),
                    StringEscapeHandling = StringEscapeHandling.Default
                });

            //NullValueHandling = NullValueHandling.Ignore    排除空值设置
        }
        public class SortContractResolver : DefaultContractResolver
        {
            protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
            {
                IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);

                return properties.OrderBy(x => x.PropertyName).ToList();
            }
        }

        public static string ModelToJsonA<T>(T model)
        {
            JsonSerializer serializer = new JsonSerializer();
            StringWriter sw = new StringWriter();
            serializer.Serialize(new JsonTextWriter(sw), model);
            return sw.GetStringBuilder().ToString();
        }

        public static string Post(string postdata, string url)
        {
            //var baseUrl = "http://120.79.157.84:30001";
            HttpWebResponse hw;
            string result = string.Empty;
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.ContentType = "application/json";
            


            var appkey = "11000";
            var format = "json";
            var timestamp = "12345672333";
            var method = "open.api.openCommon.queryPublicRoute";
            var sign = "CF529FFB8B49499EC30BE2C29C02AE67";
            var token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9";

            req.Headers.Add("appkey", appkey);
            req.Headers.Add("format", format);
            req.Headers.Add("timestamp", timestamp);
            req.Headers.Add("method", method);
            req.Headers.Add("sign", sign);
            req.Headers.Add("token", token);

            #region 添加Post 参数  
            byte[] data = Encoding.UTF8.GetBytes(postdata);
            req.ContentLength = data.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(data, 0, data.Length);//将post对象放入请求流中
                reqStream.Close();
            }
            #endregion
            try
            {
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream stream = resp.GetResponseStream();
                //获取响应内容  
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }
            }
            catch (WebException w)
            {
                hw = (HttpWebResponse)w.Response;
                StreamReader sr = new StreamReader(hw.GetResponseStream(), Encoding.UTF8, false);
                result = sr.ReadToEnd();
            }

            return result;
        }
    }
}
