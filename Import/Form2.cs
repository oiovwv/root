using Biz;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Import
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int a = 0;
            List<string> sqlArr = new List<string>();
            string aaaa = "Provider=MSDAORA.1;Password=sta_ilis;User ID=sta_ilis;Data Source=PROD";
            //aaaa = "Provider=MSDAORA.1;Password=stasoil;User ID=wh25;Data Source=prod";
            EntDB dB = new EntDB();
            EntDB cc = new EntDB(aaaa);
            string fileName = "CPO202108041172.CSV";
            fileName = fileName.ToUpper();
            string sssql = string.Format(@"SELECT OMS_NO 
                              FROM OMS_ORDER_HEAD S 
                             WHERE 1 = 1 
                               AND S.CLIENT_C = 'JOS'
                               AND S.ORIGIN_FILE_NAME = '{0}'
                             ORDER BY S.OMS_NO DESC", fileName);
            DataTable dtaa = dB.ExecuteToDataTable(sssql);
            sqlArr.Clear();
            foreach (DataRow dra in dtaa.Rows)
            {
                sssql = string.Format(@"INSERT INTO T_TMP_JOS(OMS_NO,CPO_FILENAME) VALUES('{0}','{1}')", dra["OMS_NO"].ToString(), fileName);
                sqlArr.Add(sssql);
            }
            a = cc.DoTran(sqlArr.ToArray());

            sqlArr.Clear();
            for (var i = 1; i < 2; i++)
            {
                string sql = string.Format(@"select spo.id as order_id,spb.id as box_id from sp_pack_orders spo,sp_pack_boxes spb where spo.id = spb.order_id and spo.order_key = '{0}' and spb.box_sort = {1}", "0000167688", i);
                DataTable d = cc.ExecuteToDataTable(sql);
                string orderId = d.Rows[0]["ORDER_ID"].ToString();
                string boxId = d.Rows[0]["BOX_ID"].ToString();
                for (var r = 0; r < 1; r++)
                {
                    string sku = "542045-0-8";
                    string ssql = string.Format(@"INSERT INTO SP_PACK_ITEMS(ID, ORDER_ID, BOX_ID, SKU, QTY, WHO_ADD) VALUES('{0}','{1}','{2}','{3}','{4}','{5}')", Guid.NewGuid(), orderId, boxId, sku, "1", "TEST11");
                    sqlArr.Add(ssql);
                }
                
            }
            a = cc.DoTran(sqlArr.ToArray());




            try
            {
                //后台手工更新JOS预期收货数量
                string filePath = CommonFunction.ChooseFile();
                DataSet ds = CommonFunction.ReadUperExcelFileToDataSet(filePath, 1, 50, "Sheet1");
                string sql2 = string.Empty;
                List<string> sqlList = new List<string>();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    sql2 = string.Format(@"select oms_no from oms_order_head where client_order_no = '{0}' and Customer_Po_No = '{1}'", dr[1].ToString(), dr[0].ToString());
                    DataTable dt = dB.ExecuteToDataTable(sql2);
                    if (dt.Rows.Count > 0)
                    {
                        var omsNo = dt.Rows[0][0].ToString();
                        sql2 = string.Format(@"SELECT receiptkey FROM WH25.RECEIPT R WHERE R.EXTERNRECEIPTKEY  = '{0}'", omsNo);
                        
                        dt = cc.ExecuteToDataTable(sql2);
                        var receiptKey = dt.Rows[0][0].ToString();
                        //var date = FormatDate(dr[1].ToString());
                        sql2 = string.Format("update WH25.RECEIPTDETAIL set QTYEXPECTED ='{0}' WHERE RECEIPTKEY = '{1}' and sku='{2}'", dr[3].ToString(), receiptKey,dr[2].ToString());
                        sqlList.Add(sql2);
                    }

                    //if (isExist(dr[0].ToString(), dr[1].ToString()))
                    //{

                    //}                       
                }

                var aaa = "";
                int res = cc.DoTran(sqlList.ToArray());
                if (res > 0)
                {
                    Console.WriteLine("导入完成");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("导入失败，原因：" + ex.Message);
            }
        }
        private string FormatDate(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return date;
            }
            string o = string.Empty;
            string year = date.Substring(date.LastIndexOf("-") + 1);
            string month = date.Substring(date.IndexOf("-") + 1, date.LastIndexOf("-") - date.IndexOf("-") - 1);
            string day = date.Substring(0, date.IndexOf("-"));
            return year + "/" + month + "/" + day;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string aaaa = "Provider=MSDAORA.1;Password=sta_ilis;User ID=sta_ilis;Data Source=PROD";
            //aaaa = "Provider=MSDAORA.1;Password=stasoil;User ID=wh25;Data Source=prod";
            EntDB dB = new EntDB();
            EntDB cc = new EntDB(aaaa);

            DataTable dt = CreateWmsData();
            List<string> sqlList = new List<string>();
            sqlList.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                string sql = string.Format(@"INSERT INTO SP_PACK_NZCL(SKU,UPC,SKU_NAME,CTN_QTY) VALUES('{0}','{0}','{0}','{1}')",dr["SKU"].ToString(),dr["CTN_QTY"].ToString());
                sqlList.Add(sql);
            }
            int a = cc.DoTran(sqlList.ToArray());
        }

        private DataTable CreateWmsData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SKU", Type.GetType("System.String"));           
            dt.Columns.Add("CTN_QTY", Type.GetType("System.String"));

            dt.Rows.Add(new object[] { "674620-0-1", "20"});
            dt.Rows.Add(new object[] { "674621-0-1", "20"});
            dt.Rows.Add(new object[] { "674622-0-1", "20" });
            dt.Rows.Add(new object[] { "674623-0-1", "20" });
            



            return dt;
        }
    }
}
