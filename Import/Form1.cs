using Biz;
using Biz.Models;
using Biz.SySWS;
using Newtonsoft.Json;
//using OSR_OrderTracking.Models;
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
using System.Threading;
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
            //SystemWSSoapClient c = new SystemWSSoapClient();
            ////var afsdgsd = c.checkUserLogin(new string[]{
            //        "lichengd",
            //        "121211",
            //        "STA TESTRDC",
            //        "10.250.133.119,192.168.0.103",
            //        "F4:4E:E3:31:BE:A1,00:05:9A:3C:7A:00"
            //        });
            //var sql = string.Format(@""); 


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
                //string filePathSku = CommonFunction.ChooseFile();
                //入库
                //ds = CommonFunction.ReadExcelFileToDataSet(filePathSku, 0, 40);
                //出库
                //ds = CommonFunction.ReadExcelFileToDataSet(filePathSku, 4, 30);
                //库存

                int no = 1;
                List<string> sqls = new List<string>();
                //var dt = GetTestData();
                //ds = CommonFunction.ReadExcelFileToDataSet(filePathSku, 4, 30);
                //
                var dt = CreateWmsDataA();
                //var dt = ds.Tables[0];
                foreach (DataRow dr in dt.Rows)
                {

                    //if (dr["Column_16"].ToString() == "BAG" && !string.IsNullOrEmpty(dr["Column_15"].ToString()) && dr["Column_15"].ToString() != "0")
                    //{
                    //    var sql = string.Format(@"update oms_product set product_class = '{0}' where client_c = 'LSL' and product_no = '{1}'", dr["Column_15"].ToString(), dr["Column_0"].ToString());
                    //    sqls.Add(sql);
                    //}
                    //var sql = string.Format(@"select count(*) from APP_SIGNIN_CUSTOMER where client_code = 'COL' and  customer_code = '{0}' ", dr[0].ToString());
                    ////var sql = string.Format(@"select count(*) from app_tags where imei = '{0}' ", dr[0].ToString());
                    //bool res = Convert.ToInt32(dB.GetObject(sql)) > 0;
                    //if (res)
                    //{
                    //    continue;
                    //}
                    //else
                    //{
                    //    //sql = string.Format(@"INSERT INTO app_tags(imei,deviceclass,devicestatus,belongtowho,devicetype,deleteflag,rdc_code,simno) values('{0}','1','1','卢魁','GT420D','N','STA SHANGHAI','121211SH')", dr[0].ToString());
                    //    sql = string.Format(@"INSERT INTO APP_SIGNIN_CUSTOMER(client_code,customer_code) values('COL','{0}')", dr[0].ToString());
                    //    //string sql = string.Format("insert into oms_osr_product_bu(bu,division,ag,description) values('{0}','{1}','{2}','{3}')", dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
                    //    //sql = string.Format(@"UPDATE OMS_PRODUCT SET REFERENCE01 = '{0}' WHERE CLIENT_C='JOS' AND PRODUCT_NO ='{1}'", dr[1].ToString(), dr[0].ToString());
                    //    //if (dB.Execute(sql))
                    //    //{
                    //    //    no++;
                    //    //}
                    //    //sqls.Add(sql);
                    //}
                    //入库
                    //string sql = string.Format("INSERT INTO SPDA_STORAGE_AMO (\r\nCLIENT_C,\r\nIDX,\r\nSTORAGE_DATE,\r\nSTORAGE_TYPE,\r\nPRODUCT_NO,\r\nPRODUCT_NAME,\r\nSPECIFICATIONS,\r\nCOMPANY,\r\nPRODUCT_REGISTRATION,\r\nBATCH_NUMBER,\r\nBATCH_DATE,\r\nEXPIRY_DATE,\r\nQTY,\r\nUNIT,\r\nSTORAGE_CONDITION,\r\nSTORAGE_NUMBER,\r\nPRODUCT_STATUS,\r\nREMARK,\r\nOPUSER,\r\nADD_DATE,\r\nTEMP1,TEMP2,TEMP3,TEMP4,TEMP5) \r\nVALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',{12},'{13}','{14}','{15}','{16}','{17}','{18}',{19},'{20}','{21}','{22}','{23}','{24}')", "AMO", "", dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), FormartDate(dr[9].ToString()), FormartDate(dr[10].ToString()), dr[11].ToString(), dr[12].ToString(), dr[13].ToString(), dr[14].ToString(), dr[16].ToString(), "", "", "sysdate", dr[15].ToString(), "", dr[8].ToString(), "", "");
                    //出库
                    //string sql = string.Format("INSERT INTO SPDA_OUTBOUND_AMO (\r\nCLIENT_C,\r\nIDX,\r\n\r\noutboun_type,\r\norder_no,\r\nPRODUCT_NO,\r\nPRODUCT_NAME,\r\nSPECIFICATIONS,\r\nCOMPANY,\r\nPRODUCT_REGISTRATION,\r\nBATCH_NUMBER,\r\noutbound_condition,\r\nUNIT,\r\nQTY,\r\ncliant_name,\r\naddress,\r\ncontacts,\r\nphone,\r\nREMARK,\r\nOPUSER,\r\nADD_DATE,\r\nchukuriqi,\r\nreceipt_party_no,\r\nTEMP1,TEMP2,TEMP3,TEMP4,TEMP5,TEMP6) \r\nVALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',{12},'{13}','{14}','{15}','{16}','{17}','{18}',{19},'{20}','{21}','{22}','{23}','{24}','{25}','{26}','{27}')", "AMO", "", dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[8].ToString(), dr[9].ToString(), dr[11].ToString(), dr[12].ToString(), dr[13].ToString(), dr[15].ToString(), dr[16].ToString(), dr[17].ToString(), dr[18].ToString(), "", "", "sysdate", dr[1].ToString(), dr[14].ToString(), "", "", dr[10].ToString(), "", "", "");
                    //库存
//                    var sql = string.Format(@"INSERT INTO {0}(CLIENT_C, 
//storage_date, 
//product_no, 
//product_name, 
//specifications, 
//company, 
//product_registration, 
//batch_number, 
//batch_date,
//expiry_date,
//qty,
//unit,
//storage_number,
//temp1,
//storage_condition,
//product_status) VALUES
//('{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', {11}, '{12}', '{13}', '{14}', '{15}','{16}')",
//                        "spda_stock","JNJ", "", dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), 
//                        dr[7].ToString(), dr[8].ToString(), dr[9].ToString(), dr[10].ToString(), dr[11].ToString(), dr[12].ToString(), dr[13].ToString(), dr[14].ToString(), dr[15].ToString());

                    var sql = string.Format(@"update oms_product set PERIOD_OF_VALIDITY = '{0}' where client_c = 'ZQL' and product_no = '{1}'", dr[1].ToString(), dr[0].ToString());
                    sqls.Add(sql);
                    //string sql = string.Format(@"INSERT INTO app_tags(imei,deviceclass,devicestatus,belongtowho,devicetype,deleteflag,rdc_code,simno) values('{0}','1','1','卢魁','GT420D','N','STA SHANGHAI','121211SH')",dr[0].ToString());

                }
                dB.DoTran(sqls.ToArray());
                string a = "asfa";
            }
            catch (Exception ex)
            {
                //throw new Exception(ex.Message);
            }
        }

        private DataTable CreateWmsDataA()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("sku", Type.GetType("System.String"));
            dt.Columns.Add("a", Type.GetType("System.String"));


            //dt.Rows.Add(new object[] { "OSRE080706600", "AA3094602DC", "540", "540", "540", "0", "0", "1" });
            //dt.Rows.Add(new object[] { "OSRE080689500", "AB3591101DC", "280", "280", "280", "0", "0", "2" });


            //dt.Rows.Add(new object[] { "收货人代码" });
            dt.Rows.Add(new object[] { "1710815", "912" });
            dt.Rows.Add(new object[] { "1710816", "912" });
            dt.Rows.Add(new object[] { "1710817", "912" });
            dt.Rows.Add(new object[] { "1710819", "912" });
            dt.Rows.Add(new object[] { "1710824", "912" });
            dt.Rows.Add(new object[] { "1710828", "1095" });
            dt.Rows.Add(new object[] { "1710829", "1095" });
            dt.Rows.Add(new object[] { "1710830", "1095" });
            dt.Rows.Add(new object[] { "1710833", "912" });
            dt.Rows.Add(new object[] { "1710835", "912" });
            dt.Rows.Add(new object[] { "1710836", "912" });
            dt.Rows.Add(new object[] { "1710846", "912" });
            dt.Rows.Add(new object[] { "1710853", "912" });
            dt.Rows.Add(new object[] { "1710860", "912" });
            dt.Rows.Add(new object[] { "1710924", "1095" });
            dt.Rows.Add(new object[] { "1710926", "1095" });
            dt.Rows.Add(new object[] { "1710928", "1095" });
            dt.Rows.Add(new object[] { "1710929", "1095" });
            dt.Rows.Add(new object[] { "1710934", "1095" });
            dt.Rows.Add(new object[] { "1710935", "1095" });
            dt.Rows.Add(new object[] { "1710936", "1095" });
            dt.Rows.Add(new object[] { "1710937", "1095" });
            dt.Rows.Add(new object[] { "1710940", "1095" });
            dt.Rows.Add(new object[] { "1710945", "912" });
            dt.Rows.Add(new object[] { "1710948", "1095" });
            dt.Rows.Add(new object[] { "1710950", "1095" });
            dt.Rows.Add(new object[] { "1710956", "1095" });
            dt.Rows.Add(new object[] { "1710958", "1095" });
            dt.Rows.Add(new object[] { "1710960", "1095" });
            dt.Rows.Add(new object[] { "1710961", "1095" });
            dt.Rows.Add(new object[] { "1710970", "1095" });
            dt.Rows.Add(new object[] { "1710976", "912" });
            dt.Rows.Add(new object[] { "1710977", "912" });
            dt.Rows.Add(new object[] { "1710984", "912" });
            dt.Rows.Add(new object[] { "2780157", "1185" });
            dt.Rows.Add(new object[] { "2780158", "1185" });
            dt.Rows.Add(new object[] { "2780160", "1185" });
            dt.Rows.Add(new object[] { "2780175", "1185" });
            dt.Rows.Add(new object[] { "2780178", "1185" });
            dt.Rows.Add(new object[] { "2780846", "1185" });
            dt.Rows.Add(new object[] { "338548", "1095" });
            dt.Rows.Add(new object[] { "344645", "1095" });
            dt.Rows.Add(new object[] { "344646", "1095" });
            dt.Rows.Add(new object[] { "344660", "1095" });
            dt.Rows.Add(new object[] { "344670", "730" });
            dt.Rows.Add(new object[] { "344674", "1095" });
            dt.Rows.Add(new object[] { "344675", "1095" });
            dt.Rows.Add(new object[] { "344677", "1095" });
            dt.Rows.Add(new object[] { "344678", "1095" });








            return dt;
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

        private void button4_Click(object sender, EventArgs e)
        {
            string aaaa = "Provider=MSDAORA.1;Password=sta_ilis;User ID=sta_ilis;Data Source=PROD";
            string sql = string.Format(@"
                select distinct s1.CONTAINERKEY,s1.F01,s1.F02,s1.F03,s1.F04,s1.F05,s1.F06,s1.F08,s1.F09,s1.F10,s1.F11,s1.F12,s1.F13,s1.F15,
                s1.F16,s1.F19,s1.F20,s6.order_key as orderkey from
                ST_CONTAINER  s1,  --查箱/柜
                ST_CONTAINERD s2, --- 查托盘
                SP_PALLETS s3,
                SP_PALLET_BOXES s4,
                SP_PACK_BOXES s5,
                sp_pack_orders s6
                where 1=1
                and s1.CONTAINERKEY = s2.CONTAINERKEY
                and s2.orderkey = s3.pallet_num
                and s3.id = s4.pallet_id
                and s4.box_id = s5.id
                and s5.order_id = s6.id
                and s1.CONTAINERKEY = 'ZCSU8755092'");
            EntDB cc = new EntDB(aaaa);
            var dt = cc.ExecuteToDataTable(sql);
            DataTable dtNew = dt.Clone();
            dtNew.Columns.Add("ORIGIN");
            dtNew.Columns.Add("EXTEND02");
            dtNew.Columns.Add("EXTERNALRECEIPTKEY2");
            dtNew.Columns.Add("SKU");
            dtNew.Columns.Add("ItemDesc");
            dtNew.Columns.Add("shpqty");
            dtNew.Columns.Add("cartons");
            dtNew.Columns.Add("CBMS");
            dtNew.Columns.Add("KGS");
            dtNew.Columns.Add("LOTTABLE09");
            dtNew.Columns.Add("invoiceNumber");
            dtNew.Columns.Add("RetailPrice");
            dtNew.Columns.Add("invoiceAmount");
            dtNew.Columns.Add("freightCharge");
            dtNew.Columns.Add("sum_shpqty");
            dtNew.Columns.Add("sum_CBMS");
            dtNew.Columns.Add("sum_kgs");
            var sqlA = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                var orderkey = dr["orderkey"].ToString();
                var sqlTemp = string.Format(@"
select 
--s8.orderkey,
s9.sku,
s11.STDGROSSWGT,
s11.STDCUBE,
wh25.f_split(s10.EXTEND03, '|+|', 4) as RetailPrice,
s13.lottable09,
s13.lottable10--,
--sum(a3.qty) as qty
from 
wh25.orders s8,
wh25.orderdetail s9,
wh25.St_Orderdetail_Extend s10,
wh25.sku s11,
wh25.pickdetail s12,
wh25.lotattribute s13--,
--sp_pack_orders a1,
--sp_pack_boxes a2,
--sp_pack_items a3
where s8.orderkey = s9.orderkey
and s9.orderkey = s10.orderkey
and s9.orderlinenumber = s10.orderlinenumber
and s9.sku = s11.sku
and s8.storerkey = s11.storerkey
and s9.orderkey = s12.orderkey
and s9.orderlinenumber = s12.orderlinenumber
and s9.sku = s12.sku
and s12.lot = s13.lot
--and s8.orderkey = a1.order_key
--and a1.id = a2.order_id
--and a2.id = a3.box_id
--and s9.sku = a3.sku
and s8.orderkey = '{0}'", orderkey);
                sqlA += sqlA.Length > 0 ? (" union " + sqlTemp) : sqlTemp;
                
                
            }
            var dtQty = cc.ExecuteToDataTable(sqlA);

            //foreach (DataRow dr1 in dtQty.Rows)
            //{
            //    DataRow row = dtNew.NewRow();
            //    var receiptKey = dr1["lottable10"].ToString();
            //    var sku = dr1["sku"].ToString();
            //    sql = string.Format(@"select b1.externalreceiptkey2,b3.extend02,b4.extend06 from 
            //            WH25.RECEIPT b1,WH25.RECEIPTDETAIL b2,WH25.ST_RECEIPT_EXTEND b3,WH25.ST_RECEIPTDETAIL_EXTEND b4
            //            where b1.receiptkey = b2.receiptkey
            //            and b1.receiptkey = b3.receiptkey
            //            and b2.receiptkey = b4.receiptkey
            //            and b2.receiptlinenumber = b4.receiptlinenumber
            //            and b1.receiptkey = '{0}'
            //            and b2.sku = '{1}'", receiptKey, sku);
            //    DataTable skuTable = cc.ExecuteToDataTable(sql);
            //    row["CONTAINERKEY"] = dr["CONTAINERKEY"].ToString();
            //    row["F01"] = dr["F01"].ToString();
            //    row["F02"] = dr["F02"].ToString();
            //    row["F03"] = dr["F03"].ToString();
            //    row["F05"] = dr["F05"].ToString();
            //    row["F06"] = dr["F06"].ToString();
            //    row["F08"] = dr["F08"].ToString();
            //    row["F09"] = dr["F09"].ToString();
            //    row["F10"] = dr["F10"].ToString();
            //    row["F11"] = dr["F11"].ToString();
            //    row["F12"] = dr["F12"].ToString();
            //    row["F13"] = dr["F13"].ToString();
            //    row["F15"] = dr["F15"].ToString();
            //    row["F16"] = dr["F16"].ToString();
            //    row["F19"] = dr["F19"].ToString();
            //    row["F20"] = dr["F20"].ToString();
            //    row["ORIGIN"] = "Shenzhen";
            //    row["EXTEND02"] = skuTable.Rows[0]["EXTEND02"].ToString();
            //    row["EXTERNALRECEIPTKEY2"] = skuTable.Rows[0]["EXTERNALRECEIPTKEY2"].ToString();
            //    row["SKU"] = dr1["sku"].ToString();
            //    row["ItemDesc"] = skuTable.Rows[0]["EXTEND06"].ToString();
            //    row["shpqty"] = dr1["qty"].ToString();
            //    row["cartons"] = "0";
            //    //CBMS
            //    //KGS
            //    row["LOTTABLE09"] = dr1["LOTTABLE09"].ToString();
            //    row["invoiceNumber"] = "IN0810240A";
            //    row["RetailPrice"] = dr1["RetailPrice"].ToString();
            //    //row["invoiceAmount"] = "0";
            //    row["freightCharge"] = "0.00";
            //    row["sum_shpqty"] = "";
            //    row["sum_CBMS"] = "";
            //    row["sum_kgs"] = "";
            //    dtNew.Rows.Add(row);
            //}
            //var a = dtQty.Columns.Count;
            //var fieldNames = new List<string>();
            //foreach(DataColumn column in dtQty.Columns)
            //{
            //    fieldNames.Add(column.ColumnName);
            //}
            //var dtRes = DistinctSomeColumn(dtQty, fieldNames.ToArray());
            var dtResClone = dtQty.Clone();
            dtResClone.Columns.Add("externalreceiptkey2", Type.GetType("System.String"));
            dtResClone.Columns.Add("extend02", Type.GetType("System.String"));
            dtResClone.Columns.Add("extend06", Type.GetType("System.String"));
            sqlA = string.Empty;
            foreach(DataRow row in dtQty.Rows)
            {
                var receiptKey = row[5].ToString();
                var sku = row[0].ToString();
                var sqlTemp2 = string.Format(@"select b1.receiptkey,b2.sku,b1.externalreceiptkey2, b3.extend02, b4.extend06 from
                        WH25.RECEIPT b1,WH25.RECEIPTDETAIL b2,WH25.ST_RECEIPT_EXTEND b3,WH25.ST_RECEIPTDETAIL_EXTEND b4
                     where b1.receiptkey = b2.receiptkey
                       and b1.receiptkey = b3.receiptkey
                      and b2.receiptkey = b4.receiptkey
                      and b2.receiptlinenumber = b4.receiptlinenumber
                      and b1.receiptkey = '{0}'
                      and b2.sku = '{1}'", receiptKey, sku);
                sqlA += sqlA.Length > 0 ? (" union " + sqlTemp2) : sqlTemp2;
            }
            var ddd = cc.ExecuteToDataTable(sqlA);
            foreach(DataRow dr1 in ddd.Rows)
            {
                DataRow[] drs = dtQty.Select("sku='" + dr1[1].ToString() + "' and lottable10 = '" + dr1[0].ToString() + "'");
                foreach(DataRow item in drs)
                {
//                    s9.sku,
//s11.STDGROSSWGT,
//s11.STDCUBE,
//wh25.f_split(s10.EXTEND03, '|+|', 4) as RetailPrice,
//s13.lottable09,
//s13.lottable10
                    DataRow newRow = dtResClone.NewRow();
                    newRow["sku"] = item[0].ToString();
                    newRow["STDGROSSWGT"] = item[1].ToString();
                    newRow["STDCUBE"] = item[2].ToString();
                    newRow["RetailPrice"] = item[3].ToString();
                    newRow["lottable09"] = item[4].ToString();
                    newRow["lottable10"] = item[5].ToString();
                    newRow["externalreceiptkey2"] = dr1[0].ToString();
                    newRow["extend02"] = dr1[2].ToString();
                    newRow["extend06"] = dr1[3].ToString();
                    dtResClone.Rows.Add(newRow);
                }

            }
            sql = string.Format(@"select spi.sku,
                               --spo.order_key, 
                               sum(spi.qty) as shpqty
                                  from sp_pack_items   spi,
                                       sp_pallet_boxes spb,
                                       sp_pallets      sp,
                                       st_containerd   sc--,
                                       --sp_pack_orders spo
                                 where 1 = 1
                                   and sc.orderkey = sp.pallet_num
                                   and sp.id = spb.pallet_id
                                   and spb.box_id = spi.box_id
                                   --and spo.id = spi.order_id
                                   and sc.containerkey = 'ZCSU8755092' 
                                   group by spi.sku");
            var qtyDt = cc.ExecuteToDataTable(sql);
            dtResClone.Columns.Add("qty", Type.GetType("System.String"));
            //double totalGt = qtyDt.AsEnumerable().Select(d => Convert.ToDouble(d.Field<string>("shpqty"))).Sum();
            List<string> skuList = new List<string>();

            foreach (DataRow row1 in qtyDt.Rows)
            {
                DataRow[] drSkuRows = dtResClone.Select("sku='" + row1[0].ToString() + "'");
                
                if (drSkuRows.Length == 1)
                {
                    
                    drSkuRows[0]["qty"] = row1[1].ToString();
                }
                else
                {
                    foreach (DataRow qtyRow in drSkuRows)
                    {
                        skuList.Add(qtyRow[0].ToString());
                    }
                }
                //foreach(DataRow qtyRow in drSkuRows)
                //{
                //    qtyRow["qty"] = row1[1].ToString();
                //}
            }

            
            foreach (DataRow drNew in dtNew.Rows)
            {

            }
            var a = "";
        }


        public string GetSql()
        {
            var res = string.Empty;
            return res;
        }


        public DataTable DistinctSomeColumn(DataTable sourceTable, string[] fieldName)
        {
            DataTable dt2 = sourceTable.Clone();
            DataView v1 = dt2.DefaultView;
            StringBuilder filter = new StringBuilder();
            foreach (DataRow row in sourceTable.Rows)
            {
                for (int i = 0; i < fieldName.Length; i++)
                {
                    filter.AppendFormat("{0}='{1}'", fieldName[i], row[fieldName[i]].ToString().TrimEnd());
                    if (i < fieldName.Length - 1)
                    {
                        filter.Append(" and ");
                    }
                }

                v1.RowFilter = filter.ToString();

                if (v1.Count > 0)
                {
                    filter = new StringBuilder();
                    continue;
                }
                dt2.Rows.Add(row.ItemArray);
                filter = new StringBuilder();
            }
            return dt2;
        }

        public void TestAAA()
        {
            string aaaa = "Provider=MSDAORA.1;Password=sta_ilis;User ID=sta_ilis;Data Source=PROD";
            EntDB cc = new EntDB(aaaa);
            var sql= string.Format(@" 
select 
distinct 
s1.CONTAINERKEY,
s1.F01,
s1.F02,
s1.F03,
s1.F04,
s1.F05,
s1.F06,
s1.F08,
s1.F09,
s1.F10,
s1.F11,
s1.F12,
s1.F13,
s1.F15,
s1.F16,
s1.F19,
s1.F20,
s6.order_key
from
ST_CONTAINER  s1,  --查箱/柜
ST_CONTAINERD s2, --- 查托盘
SP_PALLETS s3,
SP_PALLET_BOXES s4, --箱
SP_PACK_BOXES s5,  ---装箱
sp_pack_orders s6,  ---订单
sp_pack_items s7,---扫描明细
wh25.orders s8--,wh25.orderdetail s9,
--wh25.St_Orderdetail_Extend s10
where 1=1
and s1.CONTAINERKEY = s2.CONTAINERKEY
and s2.orderkey = s3.pallet_num
and s3.id = s4.pallet_id
and s4.box_id = s5.id
and s5.order_id = s6.id
and s5.id = s7.box_id
and s6.id = s7.order_id
and s4.box_id = s7.box_id
and s6.order_key = s8.orderkey
and s1.CONTAINERKEY = 'ZCSU8755092'");
            var orderDt = cc.ExecuteToDataTable(sql);
            sql = string.Format(@"select spi.sku, sum(spi.qty) as shpqty
                                  from sp_pack_items   spi,
                                       sp_pallet_boxes spb,
                                       sp_pallets      sp,
                                       st_containerd   sc
                                 where 1 = 1
                                   and sc.orderkey = sp.pallet_num
                                   and sp.id = spb.pallet_id
                                   and spb.box_id = spi.box_id
                                   and sc.containerkey = 'ZCSU8755092'
            group by spi.sku");
            var qtyDt = cc.ExecuteToDataTable(sql);
            var strSql = string.Empty;
            for(var i = 0; i < orderDt.Rows.Count; i++)
            {
                var orderkey = orderDt.Rows[i]["order_key"].ToString();
                var sqlTemp = string.Format(@"select distinct s8.orderkey,s9.sku from wh25.orders s8,wh25.orderdetail s9
                    where s8.orderkey = s9.orderkey and s8.orderkey = '{0}'", orderkey);
                strSql += strSql.Length > 0 ? (" union " + sqlTemp) : sqlTemp;
            }
            var skuDt = cc.ExecuteToDataTable(strSql);
            var dtSkuClone = skuDt.Clone();
            for(var r = 0; r < qtyDt.Rows.Count; r++)
            {
                var sku = qtyDt.Rows[r]["sku"].ToString();                
                DataRow[] rows = skuDt.Select("SKU = '" + sku + "'");
                foreach(DataRow dr in rows)
                {
                    DataRow row = dtSkuClone.NewRow();
                    row["orderkey"] = dr["orderkey"].ToString();
                    row["SKU"] = dr["SKU"].ToString();
                    dtSkuClone.Rows.Add(row);
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            TestAAA();

            string aaaa = "Provider=MSDAORA.1;Password=sta_ilis;User ID=sta_ilis;Data Source=PROD";
            string sql = string.Format(@"
                
select 
distinct 
s1.CONTAINERKEY,
s1.F01,
s1.F02,
s1.F03,
s1.F04,
s1.F05,
s1.F06,
s1.F08,
s1.F09,
s1.F10,
s1.F11,
s1.F12,
s1.F13,
s1.F15,
s1.F16,
s1.F19,
s1.F20,
s6.order_key,
s7.sku,
--wh25.f_split(s10.EXTEND03, '|+|', 4) as RetailPrice,
sum(s7.qty) as shpqty
from
ST_CONTAINER  s1,  --查箱/柜
ST_CONTAINERD s2, --- 查托盘
SP_PALLETS s3,
SP_PALLET_BOXES s4, --箱
SP_PACK_BOXES s5,  ---装箱
sp_pack_orders s6,  ---订单
sp_pack_items s7,---扫描明细
wh25.orders s8--,wh25.orderdetail s9--,
--wh25.St_Orderdetail_Extend s10
where 1=1
and s1.CONTAINERKEY = s2.CONTAINERKEY
and s2.orderkey = s3.pallet_num
and s3.id = s4.pallet_id
and s4.box_id = s5.id
and s5.order_id = s6.id
and s5.id = s7.box_id
and s6.id = s7.order_id
and s4.box_id = s7.box_id
and s6.order_key = s8.orderkey
--and s8.orderkey = s9.orderkey
/*and s6.order_key = s9.orderkey
and s9.orderkey = s10.orderkey
and s9.orderlinenumber = s10.orderlinenumber
and s7.sku = s9.sku*/
and s1.CONTAINERKEY = 'ZCSU8755092'
group by 
s1.CONTAINERKEY,
s1.F01,
s1.F02,
s1.F03,
s1.F04,
s1.F05,
s1.F06,
s1.F08,
s1.F09,
s1.F10,
s1.F11,
s1.F12,
s1.F13,
s1.F15,
s1.F16,
s1.F19,
s1.F20,
s6.order_key,
--s8.externorderkey,
s7.sku");
            EntDB cc = new EntDB(aaaa);
            var dt = cc.ExecuteToDataTable(sql);
            var dtNew = CloneDatatale(dt);

            sql = doSth(dt);
            var dtSku = cc.ExecuteToDataTable(sql);

        }

        public DataTable CloneDatatale(DataTable dt)
        {
            var dtNew = dt.Clone();
            dtNew.Columns.Add("ORIGIN");
            dtNew.Columns.Add("EXTEND02");
            dtNew.Columns.Add("EXTERNALRECEIPTKEY2");
            //dtNew.Columns.Add("SKU");
            dtNew.Columns.Add("ItemDesc");
            //dtNew.Columns.Add("shpqty");
            dtNew.Columns.Add("cartons");
            dtNew.Columns.Add("CBMS");
            dtNew.Columns.Add("KGS");
            dtNew.Columns.Add("LOTTABLE09");
            dtNew.Columns.Add("invoiceNumber");
            dtNew.Columns.Add("RetailPrice");
            dtNew.Columns.Add("invoiceAmount");
            dtNew.Columns.Add("freightCharge");
            dtNew.Columns.Add("sum_shpqty");
            dtNew.Columns.Add("sum_CBMS");
            dtNew.Columns.Add("sum_kgs");
            return dtNew;
        }

        public string doSth(DataTable dt)
        {
            var sql = string.Empty;
            foreach (DataRow dr in dt.Rows)
            {
                var orderkey = dr["order_key"].ToString();
                var sku = dr["sku"].ToString();
                var sqlTemp = string.Format(@"
select 
s11.STDGROSSWGT,
s11.STDCUBE,
wh25.f_split(s10.EXTEND03, '|+|', 4) as RetailPrice,
s13.lottable09,
s13.lottable10
from 
wh25.orders s8,
wh25.orderdetail s9,
wh25.St_Orderdetail_Extend s10,
wh25.sku s11,
wh25.pickdetail s12,
wh25.lotattribute s13
where s8.orderkey = s9.orderkey
and s9.orderkey = s10.orderkey
and s9.orderlinenumber = s10.orderlinenumber
and s9.sku = s11.sku
and s8.storerkey = s11.storerkey
and s9.orderkey = s12.orderkey
and s9.orderlinenumber = s12.orderlinenumber
and s9.sku = s12.sku
and s12.lot = s13.lot
and s12.sku = s13.sku
and s12.storerkey =s13.storerkey
and s8.orderkey = '{0}'
and s9.sku='{1}'", orderkey, sku);
                sql += sql.Length > 0 ? (" union " + sqlTemp) : sqlTemp;
                //var dtSku = cc.ExecuteToDataTable(sql);
                //foreach (DataRow dr1 in dtSku.Rows)
                //{
                //    DataRow row = dtNew.NewRow();
                //    var receiptKey = dr1["lottable10"].ToString();
                //    //var sku = dr1["sku"].ToString();
                //    sql = string.Format(@"select b1.externalreceiptkey2,b3.extend02,b4.extend06 from 
                //        WH25.RECEIPT b1,WH25.RECEIPTDETAIL b2,WH25.ST_RECEIPT_EXTEND b3,WH25.ST_RECEIPTDETAIL_EXTEND b4
                //        where b1.receiptkey = b2.receiptkey
                //        and b1.receiptkey = b3.receiptkey
                //        and b2.receiptkey = b4.receiptkey
                //        and b2.receiptlinenumber = b4.receiptlinenumber
                //        and b1.receiptkey = '{0}'
                //        and b2.sku = '{1}'", receiptKey, sku);
                //    DataTable skuTable = cc.ExecuteToDataTable(sql);
                //    row["CONTAINERKEY"] = dr["CONTAINERKEY"].ToString();
                //    row["F01"] = dr["F01"].ToString();
                //    row["F02"] = dr["F02"].ToString();
                //    row["F03"] = dr["F03"].ToString();
                //    row["F05"] = dr["F05"].ToString();
                //    row["F06"] = dr["F06"].ToString();
                //    row["F08"] = dr["F08"].ToString();
                //    row["F09"] = dr["F09"].ToString();
                //    row["F10"] = dr["F10"].ToString();
                //    row["F11"] = dr["F11"].ToString();
                //    row["F12"] = dr["F12"].ToString();
                //    row["F13"] = dr["F13"].ToString();
                //    row["F15"] = dr["F15"].ToString();
                //    row["F16"] = dr["F16"].ToString();
                //    row["F19"] = dr["F19"].ToString();
                //    row["F20"] = dr["F20"].ToString();
                //    row["ORIGIN"] = "Shenzhen";
                //    row["EXTEND02"] = skuTable.Rows[0]["EXTEND02"].ToString();
                //    row["EXTERNALRECEIPTKEY2"] = skuTable.Rows[0]["EXTERNALRECEIPTKEY2"].ToString();
                //    row["SKU"] = sku;
                //    row["ItemDesc"] = skuTable.Rows[0]["EXTEND06"].ToString();
                //    row["shpqty"] = dr["shpqty"].ToString();
                //    row["cartons"] = "0";
                //    //CBMS
                //    //KGS
                //    row["LOTTABLE09"] = dr1["LOTTABLE09"].ToString();
                //    row["invoiceNumber"] = "IN0810240A";
                //    row["RetailPrice"] = dr1["RetailPrice"].ToString();
                //    //row["invoiceAmount"] = "0";
                //    row["freightCharge"] = "0.00";
                //    row["sum_shpqty"] = "";
                //    row["sum_CBMS"] = "";
                //    row["sum_kgs"] = "";
                //    dtNew.Rows.Add(row);
                //}
            }
            return sql;
        } 

        public void Test(DataTable dt)
        {
            int sid = dt.Rows.Count % 1000 == 0 ? (dt.Rows.Count / 1000) : (dt.Rows.Count / 1000 + 1);
            for (int i = 1; i <= sid; i++)
            {
                ThreadParam threadParam = new ThreadParam();
                threadParam.startindex = Convert.ToInt32(i.ToString());
                threadParam.limitstep = Convert.ToInt32(sid.ToString());
                threadParam.data = dt;
                ThreadPool.QueueUserWorkItem(todo, threadParam);
            }

            //int rowCount = dt.Rows.Count;
            //int threadCount = dt.Rows.Count % 1000 == 0 ? (dt.Rows.Count / 1000) : (dt.Rows.Count / 1000 + 1);
            //for (int i = 0; i < threadCount; i++)
            //{
            //    int start = i * threadCount;
            //    int end = (i == threadCount - 1) ? rowCount - 1 : start + threadCount;
            //    Thread thread = new Thread(new ThreadStart(
            //        () => Run(start, end, dt)));

            //    thread.Start();
            //}

        }
        public void todo(object aa)
        {
            ThreadParam param = aa as ThreadParam;
            var startindex = param.startindex;
            var limitstep = param.limitstep;
            var dt = param.data;
            for (int i = (startindex > 1 ? ((startindex - 1) * 1000) : 0); i < (startindex == limitstep ? (dt.Rows.Count) : startindex * 1000); i++)
            {
                //todo数据操作
                var dtNew = doSth(dt);
                var aaaaa = string.Empty;
            }
            Thread.Sleep(2000);
        }


        public void Run(int start, int end, DataTable table)
        {
            for (int i = start; i <= end; i++)
            {
                var dataRow = table.Rows[i];

                dataRow["Type"] = 1;
                string email = dataRow["Email"].ToString();
                //......
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var url = "https://open.ky-express.com/sandbox/router/rest";
            var dto = new RequestOSRDto();
            List<string> waybillNumbers = new List<string>();
            waybillNumbers.Add("80009802184");
            dto.waybillNumbers = waybillNumbers;
            var str = CommonFunction.ModelToJsonA(dto);
            var res = CommonFunction.Post(str, url);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //var aaaaa = "22|+||+|687892-0-1|+|";
            //var sss = aaaaa.Split(new string[] { "|+|"}, StringSplitOptions.None);

            ////var path = CommonFunction.ChooseFile();
            ////var ds = CommonFunction.ReadUperExcelFileToDataSet(path, 1, 60, "Sheet1");

            //ExteriorRoute route = new ExteriorRoute();
            //route.waybillNumber = "KY5000020045120";
            //route.omsNo = "AAAAAA";            
            ////route.clientOrderNo = "TPA99971435201";
            ////route.ref_h_01 = "4545554";
            ////route.ref_h_02 = "asfasf";
            ////route.ref_h_03 = "5698awfa";
            ////route.ref_h_04 = "23511~~!!!!";
            ////route.ref_h_05 = "///////";
            //route.timestamp = DateTime.Now.ToString("yyyyMMddHH");
            ////var dt = CreateWmsData();
            //List<ExteriorRouteListItem> items = new List<ExteriorRouteListItem>();
            //var column = 39;
            ////for (var i = 0; i < 5; i++)
            ////{
            ////    ExteriorRouteListItem item = new ExteriorRouteListItem();
            ////    item.id = i + 1;
            ////    item.routeStep = ds.Tables[0].Rows[0][column + 1].ToString().Replace("'", "");
            ////    item.routeDescription = ds.Tables[0].Rows[0][column + 1].ToString().Replace("'", "");
            ////    item.uploadDate = ds.Tables[0].Rows[0][column].ToString().Replace("'", "");
            ////    //item.city = dr[4].ToString();
            ////    //item.address = dr[5].ToString();
            ////    //item.carInfo = dr[6].ToString();
            ////    //item.intransitInfo = dr[7].ToString();
            ////    //item.ref_d_01 = dr[8].ToString();
            ////    //item.ref_d_02 = dr[9].ToString();
            ////    //item.ref_d_03 = dr[10].ToString();
            ////    //item.ref_d_04 = dr[11].ToString();
            ////    //item.ref_d_05 = dr[12].ToString();


            ////    column += 2;
            ////    if (!string.IsNullOrEmpty(item.uploadDate))
            ////    {
            ////        items.Add(item);
            ////    }
            ////}
            //foreach (DataRow dr in dt.Rows)
            //{
            //    ExteriorRouteListItem item = new ExteriorRouteListItem();
            //    item.id = int.Parse(dr[0].ToString());
            //    item.routeStep = dr[1].ToString();
            //    item.routeDescription = dr[2].ToString();
            //    item.uploadDate = dr[3].ToString();
            //    item.city = dr[4].ToString();
            //    item.address = dr[5].ToString();
            //    item.carInfo = dr[6].ToString();
            //    item.intransitInfo = dr[7].ToString();
            //    item.ref_d_01 = dr[8].ToString();
            //    item.ref_d_02 = dr[9].ToString();
            //    item.ref_d_03 = dr[10].ToString();
            //    item.ref_d_04 = dr[11].ToString();
            //    item.ref_d_05 = dr[12].ToString();
            //    items.Add(item);
            //}
            //route.exteriorRouteList = items;
            //List<ExteriorRoute> dto = new List<ExteriorRoute>();
            //dto.Add(route);
            //var str = CommonFunction.ModelToJsonA(dto);
            //string appkey = "OSREdiUser";
            //string appsecret = "2021OSR12";
            //var timestamp = route.timestamp;
            //string myAccessToken = appkey + timestamp + appsecret;
            //var accesstoken = encryptMD5(myAccessToken);
            //var url = "http://wp.tgl.tollgroup.com/Search/UploadTrackRoute?appkey=" + appkey + "&appsecret=" + accesstoken;
            //url= "http://localhost:7812/Search/UploadTrackRoute?appkey=" + appkey + "&appsecret=" + accesstoken;
            ////var url=""
            //var res = CommonFunction.Post(str, url);
            //var aaa = "";

        }
        public string encryptMD5(string data)
        {
            using (MD5 mi = MD5.Create())
            {
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                //开始加密
                byte[] newBuffer = mi.ComputeHash(buffer);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < newBuffer.Length; i++)
                {
                    sb.Append(newBuffer[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
        private DataTable CreateWmsData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("routeStep", typeof(string));
            dt.Columns.Add("routeDescription", typeof(string));
            dt.Columns.Add("uploadDate", typeof(string));
            dt.Columns.Add("city", typeof(string));
            dt.Columns.Add("address", typeof(string));
            dt.Columns.Add("carInfo", typeof(string));
            dt.Columns.Add("intransitInfo", typeof(string));
            dt.Columns.Add("ref_d_01", typeof(string));
            dt.Columns.Add("ref_d_02", typeof(string));
            dt.Columns.Add("ref_d_03", typeof(string));
            dt.Columns.Add("ref_d_04", typeof(string));
            dt.Columns.Add("ref_d_05", typeof(string));



            //dt.Rows.Add(new object[] { "OSRE080706600", "AA3094602DC", "540", "540", "540", "0", "0", "1" });
            //dt.Rows.Add(new object[] { "OSRE080689500", "AB3591101DC", "280", "280", "280", "0", "0", "2" });

            dt.Rows.Add(new object[] { 100, "签收完毕", "快件已由李四签收", "2021-12-14 15:49:18", "深圳市", "花样年美年广场", "车牌4445", "空", "2021-12-16 00:00:00", "", "", "", "" });
            //dt.Rows.Add(new object[] { 4, "交接扫描（取）", "快件已装入客户处，准备送往跨越专车", "2019-05-07 20:24:14", "广州", "大沙地", "车牌4445", "中转", "dd1", "dd2", "dd3", "dd4", "dd5" });

            return dt;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            //var path = CommonFunction.ChooseFile();

            var sql = string.Format(@"
SELECT ORDERS.ORDERKEY,
       ORDERS.STORERKEY,
       ORDERS.EXTERNORDERKEY,
       ORDERS.CONSIGNEEKEY,
       ORDERS.C_COMPANY,
       ORDERS.C_ADDRESS1,
       ORDERS.EXTERNALORDERKEY2,
       ORDERDETAIL.EXTERNORDERKEY,
       ORDERDETAIL.EXTERNLINENO,
       ORDERDETAIL.SKU,
       ORDERDETAIL.SUSR1,
       ST_ORDERS_EXTEND.EXTEND01,
       ST_ORDERS_EXTEND.EXTEND03,
       ST_ORDERS_EXTEND.EXTEND04,
       ST_ORDERS_EXTEND.EXTEND07,
       ST_ORDERS_EXTEND.EXTEND08,
       ST_ORDERDETAIL_EXTEND.EXTEND01 as A,
       PICKDETAIL.QTY,
       PICKDETAIL.LOC,
       SKU.SUSR2,
       SKU.STDGROSSWGT,
       SKU.STDCUBE,
       SKU.BUSR4
  FROM WH25.ORDERS@wms                ORDERS,
       WH25.ORDERDETAIL@wms         ORDERDETAIL,
       WH25.ST_ORDERS_EXTEND@wms      ST_ORDERS_EXTEND,
       WH25.ST_ORDERDETAIL_EXTEND@wms ST_ORDERDETAIL_EXTEND,
       WH25.PICKDETAIL@wms            PICKDETAIL,
       WH25.SKU@wms                   SKU
 WHERE ORDERS.ORDERKEY = ORDERDETAIL.ORDERKEY
   AND ORDERS.ORDERKEY = ST_ORDERS_EXTEND.ORDERKEY
   AND ORDERDETAIL.ORDERKEY = ST_ORDERDETAIL_EXTEND.ORDERKEY
   AND ORDERDETAIL.ORDERLINENUMBER = ST_ORDERDETAIL_EXTEND.ORDERLINENUMBER
   AND ORDERDETAIL.ORDERKEY = PICKDETAIL.ORDERKEY
   AND ORDERDETAIL.ORDERLINENUMBER = PICKDETAIL.ORDERLINENUMBER
   AND PICKDETAIL.STORERKEY = SKU.STORERKEY
   AND PICKDETAIL.SKU = SKU.SKU
   AND ORDERS.STORERKEY = 'JOS'
   AND ORDERS.ORDERKEY >= '0000183950'
   and ORDERS.ORDERKEY <= '0000184160'
----0000183898 - 0000184672
 ORDER BY ORDERDETAIL.EXTERNORDERKEY ASC, SKU.BUSR3 ASC");
            var a = 0;
            var orderkey = string.Empty;
            //var ds = OpenCSV(path);
            EntDB dB = new EntDB();
            var dt = dB.ExecuteToDataTable(sql);
            try
            {
                for(var i = 0; i < dt.Rows.Count; i++)
                {
                    orderkey = dt.Rows[i]["ORDERKEY"].ToString();
                    var sssss = int.Parse(dt.Rows[i]["A"].ToString().Split(new string[] { "|+|" }, StringSplitOptions.None)[0]);
                    a++;
                }
            }
            catch(Exception ex)
            {
                var sss = "";
            }
            var ccccc = "";
        }

        public DataSet OpenCSV(string filePath)
        {

            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);

            //StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("gb2312"));
            //string fileContent = sr.ReadToEnd();
            //encoding = sr.CurrentEncoding;
            //记录每次读取的一行记录
            string strLine = "";
            //记录每行记录中的各字段内容
            string[] aryLine = null;
            string[] tableHead = null;
            //标示列数
            int columnCount = 0;
            //标示是否是读取的第一行
            bool IsFirst = true;
            //逐行读取CSV中的数据
            int a = 0;
            while ((strLine = sr.ReadLine()) != null)
            {
                //strLine = Common.ConvertStringUTF8(strLine, encoding);
                //strLine = Common.ConvertStringUTF8(strLine);

                if (IsFirst == true)
                {
                    tableHead = strLine.Split(',');
                    IsFirst = false;
                    columnCount = tableHead.Length;
                    //创建列
                    for (int i = 0; i < columnCount; i++)
                    {
                        DataColumn dc = new DataColumn("Column_" + i);
                        dt.Columns.Add(dc);
                    }
                }
                else
                {
                    aryLine = strLine.Split(',');
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        dr[j] = aryLine[j].Replace("\"", "");
                    }
                    dt.Rows.Add(dr);
                }
                a++;
            }
            //if (aryLine != null && aryLine.Length > 0)
            //{
            //    dt.DefaultView.Sort = tableHead[0] + " " + "asc";
            //}

            sr.Close();
            fs.Close();
            dt.TableName = "FileTable";
            ds.Tables.Add(dt.Copy());
            return ds;
        }
    }

    public class ThreadParam
    {
        public int startindex { get; set; }
        public int limitstep { get; set; }
        public DataTable data { get; set; }
    }


}
