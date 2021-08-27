using Biz;
using Biz.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Import
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        public DateTime GetLastWeekDay(int year, int month)
        {
              for (int i = 1; i< 7; i++)
              {
                  DateTime dtt = (new DateTime(year, month, 1)).AddMonths(1).AddDays(-i);
                  if (dtt.DayOfWeek == DayOfWeek.Saturday || dtt.DayOfWeek == DayOfWeek.Sunday)
                  {
                      continue;
                  }
                  else
                  {
                     return dtt;
                  }
             }
             throw new Exception("这个异常不会出现");
        }

        private bool getTimeSpan(string now, string startTime, string endTime)
        {
            //判断当前时间是否在工作时间段内
            string _strWorkingDayAM = "08:30";//工作时间上午08:30
            string _strWorkingDayPM = "17:30";
            TimeSpan dspWorkingDayAM = DateTime.Parse(_strWorkingDayAM).TimeOfDay;
            TimeSpan dspWorkingDayPM = DateTime.Parse(_strWorkingDayPM).TimeOfDay;

            //string time1 = "2017-2-17 8:10:00";
            DateTime t1 = Convert.ToDateTime(now);

            TimeSpan dspNow = t1.TimeOfDay;
            if (dspNow > dspWorkingDayAM && dspNow < dspWorkingDayPM)
            {
                return true;
            }
            return false;
        }

        private bool IsBetween(DateTime now, TimeSpan start, TimeSpan end)
        {
            var time = now.TimeOfDay;
            // Scenario 1: If the start time and the end time are in the same day.
            if (start <= end)
                return time >= start && time <= end;
            // Scenario 2: The start time and end time is on different days.
            return time >= start || time <= end;
        }

        void mmmy()
        {
            MessageBox.Show("哈哈");
        }

        private string GetIFNO()
        {
            return DateTime.Now.ToString("yyyyMMddhhmmssfff");
        }
        public void TestLSL()
        {
            
            var param = new ProductParamModel();
            var control = new CONTROLModel();
            control.IFNO = GetIFNO();
            control.SUSER = "";
            control.SDATE = DateTime.Now.Date.ToString("yyyyMMdd");
            control.STIME = DateTime.Now.ToLocalTime().ToString("HHmmss");
            control.KEYDATA = "";
            param.CONTROL = control;
            var list = new List<ProductItem>();
            var dt = CommonFunction.GetDataFromJDA("LSL", "CN009");
            var i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                var item = new ProductItem();
                item.S1 = dr[1].ToString();
                item.S2 = "0";
                item.S3 = dr[4].ToString();
                item.S4 = "9002";
                item.S5 = DateTime.Now.ToString("yyyyMMdd");
                list.Add(item);
                i++;
                if (i == 1)
                {
                    break;
                }
            }
            param.DATA = list;
            var ss=Request(param);
            
        }

        public string Request<T>(T param)
        {
            string ttt = JsonConvert.SerializeObject(param);
            LSLRMS.MiddleServicePortTypeClient service = new LSLRMS.MiddleServicePortTypeClient();
            var res = service.restMiddle(ttt);
            return res;
        }

        public DataTable GetTestData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SKU", Type.GetType("System.String"));
            dt.Columns.Add("A", Type.GetType("System.String"));
            

            //dt.Rows.Add(new object[] { "OSRE080706600", "AA3094602DC", "540", "540", "540", "0", "0", "1" });
            //dt.Rows.Add(new object[] { "OSRE080689500", "AB3591101DC", "280", "280", "280", "0", "0", "2" });

            dt.Rows.Add(new object[] { "674199-0-1", "999.022" });
            dt.Rows.Add(new object[] { "674248-0-1", "999.023" });
            dt.Rows.Add(new object[] { "674258-0-1", "999.024" });
            dt.Rows.Add(new object[] { "674389-0-1", "999.025" });
            dt.Rows.Add(new object[] { "674400-0-1", "999.026" });
            dt.Rows.Add(new object[] { "674431-0-1", "999.027" });
            dt.Rows.Add(new object[] { "674198-0-1", "999.028" });
            dt.Rows.Add(new object[] { "674243-0-1", "999.029" });
            dt.Rows.Add(new object[] { "674247-0-1", "999.030" });
            dt.Rows.Add(new object[] { "674257-0-1", "999.031" });
            dt.Rows.Add(new object[] { "674346-0-1", "999.032" });
            dt.Rows.Add(new object[] { "674347-0-1", "999.033" });
            dt.Rows.Add(new object[] { "674380-0-1", "999.034" });
            dt.Rows.Add(new object[] { "674396-0-1", "999.035" });
            dt.Rows.Add(new object[] { "674441-0-1", "999.036" });
            dt.Rows.Add(new object[] { "674815-0-1", "999.037" });
            dt.Rows.Add(new object[] { "674816-0-1", "999.038" });
            dt.Rows.Add(new object[] { "674817-0-1", "999.039" });
            dt.Rows.Add(new object[] { "674200-0-1", "999.040" });
            dt.Rows.Add(new object[] { "674393-0-1", "999.041" });
            dt.Rows.Add(new object[] { "674432-0-1", "999.042" });
            dt.Rows.Add(new object[] { "674427-0-1", "999.043" });
            dt.Rows.Add(new object[] { "674242-0-1", "999.044" });
            dt.Rows.Add(new object[] { "674821-0-1", "999.045" });

            return dt;
        }
        public void TestLSL1()
        {
            TestLSL();


            var lastWorkDay = GetLastWeekDay(DateTime.Now.Year, DateTime.Now.Month);
            var now = DateTime.Now;
            var isEqual = DateTime.Compare(Convert.ToDateTime(lastWorkDay.ToShortDateString()), Convert.ToDateTime(lastWorkDay.ToShortDateString()));
            if (isEqual == 0)
            {
                var startSpan = new TimeSpan(17, 00, 0);
                var endSpan = new TimeSpan(3, 0, 0);
                var isStop = IsBetween(now, startSpan, endSpan);
                if (!isStop)
                {
                    //上传订单
                    mmmy();
                }
            }
            else
            {
                //上传订单
                mmmy();
            }
            var txt= (File.ReadAllText("E:\\TollProject\\a.txt"));
            var aaa = JsonConvert.DeserializeObject<Root>(txt);
            Request(aaa);
        }
        public  string Jsonstr(String filePath)
        {
            string strData = "";
            try
            {
                string line;
                // 创建一个 StreamReader 的实例来读取文件 ,using 语句也能关闭 StreamReader
                using (System.IO.StreamReader sr = new System.IO.StreamReader(filePath))
                {
                    // 从文件读取并显示行，直到文件的末尾
                    while ((line = sr.ReadLine()) != null)
                    {
                        //Console.WriteLine(line);
                        strData = line;
                    }
                }
            }
            catch (Exception e)
            {
                // 向用户显示出错消息
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return strData;
        }

    

        private void button1_Click(object sender, EventArgs e)
        {

            //TestLSL1();




            //btnCreate_Click();
            //for (int i = 0; i < 10; i++)
            //{
            //    RNGCryptoServiceProvider csp = new RNGCryptoServiceProvider();
            //    byte[] byteCsp = new byte[10];
            //    csp.GetBytes(byteCsp);
            //    //var a = BitConverter.ToInt64(byteCsp, 6);
            //}


            //var aasfsf = Convert.ToDateTime("2021-Apr-21 20:00:00");

            EntDB dB = new EntDB();
            DataSet ds = new DataSet();
            //storage();
            try
            {
                string filePathSku = CommonFunction.ChooseFile();
                //入库
                //ds = CommonFunction.ReadExcelFileToDataSet(filePathSku, 3, 40);
                //出库
                //ds = CommonFunction.ReadExcelFileToDataSet(filePathSku, 4, 30);
                //库存
                
                int no = 1;
                var dt = GetTestData();
                ds = CommonFunction.ReadExcelFileToDataSet(filePathSku, 4, 30);
                dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {
                    //入库
                    //string sql = string.Format("INSERT INTO SPDA_STORAGE_AMO (\r\nCLIENT_C,\r\nIDX,\r\nSTORAGE_DATE,\r\nSTORAGE_TYPE,\r\nPRODUCT_NO,\r\nPRODUCT_NAME,\r\nSPECIFICATIONS,\r\nCOMPANY,\r\nPRODUCT_REGISTRATION,\r\nBATCH_NUMBER,\r\nBATCH_DATE,\r\nEXPIRY_DATE,\r\nQTY,\r\nUNIT,\r\nSTORAGE_CONDITION,\r\nSTORAGE_NUMBER,\r\nPRODUCT_STATUS,\r\nREMARK,\r\nOPUSER,\r\nADD_DATE,\r\nTEMP1,TEMP2,TEMP3,TEMP4,TEMP5) \r\nVALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',{12},'{13}','{14}','{15}','{16}','{17}','{18}',{19},'{20}','{21}','{22}','{23}','{24}')", "AMO", "", dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), FormartDate(dr[9].ToString()), FormartDate(dr[10].ToString()), dr[11].ToString(), dr[12].ToString(), dr[13].ToString(), dr[14].ToString(), dr[16].ToString(), "", "", "sysdate", dr[15].ToString(), "", dr[8].ToString(), "", "");
                    //出库
                    //string sql = string.Format("INSERT INTO SPDA_OUTBOUND_AMO (\r\nCLIENT_C,\r\nIDX,\r\n\r\noutboun_type,\r\norder_no,\r\nPRODUCT_NO,\r\nPRODUCT_NAME,\r\nSPECIFICATIONS,\r\nCOMPANY,\r\nPRODUCT_REGISTRATION,\r\nBATCH_NUMBER,\r\noutbound_condition,\r\nUNIT,\r\nQTY,\r\ncliant_name,\r\naddress,\r\ncontacts,\r\nphone,\r\nREMARK,\r\nOPUSER,\r\nADD_DATE,\r\nchukuriqi,\r\nreceipt_party_no,\r\nTEMP1,TEMP2,TEMP3,TEMP4,TEMP5,TEMP6) \r\nVALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',{12},'{13}','{14}','{15}','{16}','{17}','{18}',{19},'{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}')", "AMO", "", dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString(), dr[9].ToString(), dr[11].ToString(), dr[12].ToString(), dr[13].ToString(), dr[15].ToString(), dr[16].ToString(), dr[17].ToString(), dr[18].ToString(), "", "", "sysdate", dr[1].ToString(), dr[14].ToString(), "", "", dr[10].ToString(), "", "", "");
                    //库存
                    string sql = string.Format(@"INSERT INTO app_tags(imei,deviceclass,devicestatus,belongtowho,devicetype,deleteflag,rdc_code,simno) values('{0}','1','1','赵振文','GT420D','N','STA BEIJING','121211BJ')",dr[0].ToString());
                    //sql = string.Format(@"UPDATE OMS_PRODUCT SET REFERENCE01 = '{0}' WHERE CLIENT_C='JOS' AND PRODUCT_NO ='{1}'", dr[1].ToString(), dr[0].ToString());
                    if (dB.Execute(sql))
                    {
                        no++;
                    }
                }
                string a = "asfa";
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
            }
        }

        

        private static string FormartDate(string date)
        {
            if (string.IsNullOrEmpty(date.Trim()) || date.IndexOf("A") > 0 || date.IndexOf("/") < 0)
            {
                return date;
            }
            string year = date.Substring(date.LastIndexOf("/") + 1);
            string month = date.Substring(date.IndexOf("/") + 1, date.LastIndexOf("/") - date.IndexOf("/") - 1);
            string day = date.Substring(0, date.IndexOf("/"));
            return year + "-" + month + "-" + day;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            EntDB dB = new EntDB();
            string omsNos = string.Format("'{0}'", textOms.Text.ToString().Replace(",", "','"));
            string sql11 = string.Empty;

            List<string> sqls = new List<string>();
            sql11 = $"delete from oms_order_head where oms_no in ({omsNos})";
            sqls.Add(sql11);
            sql11 = $"delete from OMS_ORDER_DETAIL where oms_no in ({omsNos})";
            sqls.Add(sql11);
            sql11 = $"delete from EDI_ORDER_H where oms_no in ({omsNos})";
            sqls.Add(sql11);
            sql11 = $"delete from EDI_ORDER_D where oms_no in ({omsNos})";
            sqls.Add(sql11);
            sql11 = $"delete from EDI_ORDER_DUPLICATE_CHECK where oms_no in ({omsNos})";
            sqls.Add(sql11);
            sql11 = $"delete from EDI_ORDER_FILENAME_DUP_CHECK where oms_no in ({omsNos})";
            sqls.Add(sql11);
            sql11 = $"delete from OMS_CHECK_ORDER where oms_no in ({omsNos})";
            sqls.Add(sql11);
            if (rdb1.Checked)
            {
                sql11 = $"delete from ots_issue_hander where oms_no  in ({omsNos})";
            }
            else
            {
                sql11 = $"delete from ots_receipt_hander where oms_no  in ({omsNos})";
            }

            sqls.Add(sql11);
            sql11 = $"delete from OTS_ORDER_DETAIL where oms_no in ({omsNos})";
            sqls.Add(sql11);
            dB.DoTran(sqls.ToArray());
            string aaa = "";
        }

        public static System.Collections.Generic.Dictionary<string, HashSet<string>> Hashtable = new Dictionary<string, HashSet<string>>();

        public string Genchecknum(string num)
        {
            string result = string.Empty;
            try
            {
                if (num.Length == 22)
                {
                    int sum = 0;
                    for (int i = 0; i < 22; i++)
                    {
                        int dit = 0;
                        //is Odd
                        if (Convert.ToBoolean(i % 2))
                        {
                            dit = int.Parse(num.Substring(i, 1)) * 2;
                            if (dit > 9)
                                dit = dit - 9;
                        }
                        else
                        {
                            dit = int.Parse(num.Substring(i, 1));
                        }
                        sum = sum + dit;
                    }
                    double doublesum = Math.Ceiling(Convert.ToDouble(sum) / 10) * 10;
                    result = (doublesum - sum).ToString();
                }
            }
            catch
            {
            }
            return result;
        }

        public string GenRandom(string acct, string country, string type, string tranName)
        {
            string refnum = string.Empty;
            Random random = new Random();
            DateTime dt = DateTime.Now;
            string acctnum = string.Empty;
            string prefix = string.Empty;
            string fieldType = string.Empty;
            string binNum = string.Empty;
            try
            {
                prefix = "7";
                acctnum = binNum == null ? acct.Substring(0, 6) : binNum;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            string ID = dt.ToString("yyyyMMddHHmmss");
            {
                if (Hashtable.ContainsKey(ID))
                {
                    bool isnotunique = true;
                    int runnum = 0;
                    do
                    {
                        string ran = random.Next(99999).ToString("00000");
                        //refNum 由站位数字prefix + acct账号的前6位 + 年份的最后一位数字 + 当天是当年的第多少天 + 当前的HHmmss +  五位的随机数， 以及 校验生成的一位数字
                        refnum = prefix + acctnum + dt.Year.ToString().Substring(3, 1) + dt.DayOfYear.ToString("000") + dt.ToString("HHmmss") + ran;

                        string checknum = Genchecknum(refnum);
                        refnum = refnum + checknum;
                        //插入非重的Refnum到Hashtable中去，确保同一秒种生成的RefNum不出现重复
                        if (!Hashtable[ID].Contains(refnum))
                        {
                            isnotunique = false;
                            Hashtable[ID].Add(refnum);
                        }
                        //循环再获取新的Refnum
                        runnum++;
                        if (runnum > 50000)
                        {
                            dt = DateTime.Now;
                            ID = dt.ToString("yyyyMMddHHmmss");
                        }
                    } while (isnotunique);

                }
                else
                {
                    Hashtable.Clear();
                    string ran = random.Next(99999).ToString("00000");
                    refnum = prefix + acctnum + dt.Year.ToString().Substring(3, 1) + dt.DayOfYear.ToString("000") + dt.ToString("HHmmss") + ran;

                    string checknum = Genchecknum(refnum);
                    refnum = refnum + checknum;
                    HashSet<string> hs = new HashSet<string>();
                    hs.Add(refnum);
                    Hashtable.Add(ID, hs);
                }
            }
            return refnum;
        }


        private void btnCreate_Click()
        {
            this.UseWaitCursor = true;
            int[] a = getRandom(999999);
            List<string> sqls = new List<string>();
            EntDB dB = new EntDB();
            var n = 0;
            foreach (int c in a)
            {
                var idx = dB.GetTableIDX("SIGN_CODE");
                if (c != 0)
                {
                    var sql = string.Format(@"INSERT INTO TABLE_RANDOM_CODE(RANDOM_CODE,CODE_CLASS,IDX) VALUES('{0}','{1}','{2}')", c.ToString(), "SIGN_CODE", idx);
                    sqls.Add(sql);
                    n++;
                }
            }

            dB.DoTran(sqls.ToArray());


            Stopwatch sw = new Stopwatch();
            sw.Start();

            sw.Stop();
            this.UseWaitCursor = false;
            //if (list == null)
            //{
            //    return;
            //}

            //string strInfo = string.Format("成功生成{1}个随机字符串！用时：{0}毫秒！", sw.ElapsedMilliseconds, 999999);


            //DialogResult result = MessageBox.Show(strInfo, "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            //if (result == DialogResult.OK)
            //{
            //    StringBuilder str_content = new StringBuilder();
            //    foreach (string r in list)
            //    {
            //        str_content.Append(r   "\r\n");
            //    }

            //    string file_path = Application.StartupPath   "\\rand_"   DateTime.Now.ToFileTime().ToString()   ".txt";
            //    File.WriteAllText(file_path, str_content.ToString());
            //    Process.Start(file_path);
            //}
        }
        int[] getRandom(int n)
        {
            //if (n < 0) return null;
            int[] rs = new int[n];
            //Random rdm = new Random(unchecked((int)DateTime.Now.Ticks));


            //for (int i = 0; i < n; i++)
            //{
            //    rs[i] = rdm.Next(100000, 999999);
            //}
            for (int r = 0; r < n; r++)
            {
                int[] array = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                Random ran = new Random(Guid.NewGuid().GetHashCode());
                for (int i = 10; i > 1; i--)
                {
                    int index = ran.Next(i);
                    int tmp = array[index];
                    array[index] = array[i - 1];
                    array[i - 1] = tmp;
                }
                int result = 0;
                for (int i = 0; i < 6; i++)
                {
                    result = result * 10 + array[i];
                }
                if (result <= 100000)
                {
                    continue;
                }
                rs[r] = result;
            }


            return rs;
        }

        /// <summary>
        /// 随机排序
        /// </summary>
        /// <param name="charList"></param>
        /// <returns></returns>
        private List<string> SortByRandom(List<string> charList)
        {
            Random rand = new Random();
            for (int i = 0; i < charList.Count; i++)
            {
                int index = rand.Next(0, charList.Count);
                string temp = charList[i];
                charList[i] = charList[index];
                charList[index] = temp;
            }

            return charList;
        }

        private void ShowError(string strError)
        {
            MessageBox.Show(strError, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// 获取随机字符串
        /// </summary>
        /// <param name="len"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private List<string> GetRandString(int len, int count)
        {
            double max_value = Math.Pow(36, len);
            if (max_value > long.MaxValue)
            {
                ShowError(string.Format("Math.Pow(36, {0}) 超出 long最大值！", len));
                return null;
            }

            long all_count = (long)max_value;
            long stepLong = all_count / count;
            if (stepLong > int.MaxValue)
            {
                ShowError(string.Format("stepLong ({0}) 超出 int最大值！", stepLong));
                return null;
            }
            int step = (int)stepLong;
            if (step < 3)
            {
                ShowError("step 不能小于 3!");
                return null;
            }
            long begin = 0;
            List<string> list = new List<string>();
            Random rand = new Random();
            while (true)
            {
                long value = rand.Next(1, step) * begin;
                begin = step;
                list.Add(GetChart(len, value));
                if (list.Count == count)
                {
                    break;
                }
            }

            list = SortByRandom(list);

            return list;
        }

        //数字 字母
        private const string CHAR = "0123456789";

        /// <summary>
        /// 将数字转化成字符串
        /// </summary>
        /// <param name="len"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetChart(int len, long value)
        {
            StringBuilder str = new StringBuilder();
            while (true)
            {
                str.Append(CHAR[(int)(value % 36)]);
                value = value / 36;
                if (str.Length == len)
                {
                    break;
                }
            }

            return str.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            //EntDB dB = new EntDB();
            //try
            //{
            //    string filePath = CommonFunction.ChooseFile();
            //    DataSet ds = CommonFunction.ReadUperExcelFileToDataSet(filePath, 1, 50, "Sheet1");
            //    string sql2 = string.Empty;
            //    List<string> sqlList = new List<string>();
            //    foreach (DataRow dr in ds.Tables[0].Rows)
            //    {
            //        var vol = Convert.ToDouble(dr[4].ToString()) * 1000;
            //        sql2 = string.Format(@"update oms_product set product_ht = '{0}',product_wdt='{1}',product_len='{2}',volume='{3}' where client_c='{4}' and product_no = '{5}'",
            //                1,1, vol, vol, "LEG", dr[0].ToString());
            //        sqlList.Add(sql2);
            //        //if (isExist(dr[0].ToString(), dr[1].ToString()))
            //        //{
                        
            //        //}                       
            //    }

            //    var aaa = "";
            //    int res = dB.DoTran(sqlList.ToArray());
            //    if (res > 0)
            //    {
            //        Console.WriteLine("导入完成");
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("导入失败，原因：" + ex.Message);
            //}
        }
        private static bool isExist(string client_c, string sku)
        {
            EntDB dB = new EntDB();
            string sql = string.Format(@"SELECT COUNT(*) FROM oms_product WHERE CLIENT_C='{0}' and product_no='{1}'", client_c, sku);
            int count = Convert.ToInt32(dB.GetObject(sql));
            return count > 0;
        }
    }   
    
}
