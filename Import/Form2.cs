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
        public static string aaaa = "Provider=MSDAORA.1;Password=sta_ilis;User ID=sta_ilis;Data Source=PROD";
        //public static string aaaa = "Provider=MSDAORA.1;Password=stasoil;User ID=wh25;Data Source=prod";//
        //
        EntDB dB = new EntDB();
        EntDB cc = new EntDB(aaaa);
        public Form2()
        {
            InitializeComponent();
        }
        //public DataSet GetManifestData(string ContainerNumber)
        //{
        //    string strSQL = "select CONTAINERKEY, " +
        //                   //"carrier, " +
        //                   "F01, " +
        //                   "F02, " +
        //                   "F03, " +
        //                   "F04, " +
        //                   "F05, " +
        //                   "F06, " +
        //                   "F08, " +
        //                   "F09, " +
        //                   "F10, " +
        //                   "F11, " +
        //                   "F12, " +
        //                   "F13, " +
        //                   "F15, " +
        //                   "F16, " +
        //                   "F19, " +
        //                   "F20, " +
        //                   "'Shenzhen' as ORIGIN, " +
        //                   "EXTEND02, " +
        //                   "EXTERNALRECEIPTKEY2, " +
        //                   "A.SKU, " +
        //                   "ItemDesc, " +
        //                   "to_char（ROUND(shpqty,2)) as shpqty, " +
        //                   "'0' as cartons, " +
        //                   "to_char(shpqty * STDCUBE / 1000000,'99990.9999') as CBMS, " +
        //                   "to_char(shpqty * STDGROSSWGT / 1000,'99990.9999') AS KGS, " +
        //                   "LOTTABLE09, " +
        //                   "'IN0810240A' as invoiceNumber, " +
        //                   "RetailPrice, " +
        //                   "to_char(ROUND(shpqty,2) * ROUND(RetailPrice,2),'99990.99') as invoiceAmount, " +
        //                   "'0.00' as freightCharge, " +
        //                   "'' as sum_shpqty, " +
        //                   "'' as sum_CBMS, " +
        //                   "'' as sum_kgs " +
        //              "from (SELECT distinct ST_CONTAINER.CONTAINERKEY, " +
        //                                    //"wh25.f_split(ST_ORDERS_EXTEND.EXTEND07, '|+|', 1) as carrier, " +
        //                                    "ST_CONTAINER.F01, " +
        //                                    "ST_CONTAINER.F02, " +
        //                                    "ST_CONTAINER.F03, " +
        //                                    "ST_CONTAINER.F04, " +
        //                                    "ST_CONTAINER.F05, " +
        //                                    "ST_CONTAINER.F06, " +
        //                                    "ST_CONTAINER.F08, " +
        //                                    "ST_CONTAINER.F09, " +
        //                                    "ST_CONTAINER.F10, " +
        //                                    "ST_CONTAINER.F11, " +
        //                                    "ST_CONTAINER.F12, " +
        //                                    "ST_CONTAINER.F13, " +
        //                                    "ST_CONTAINER.F15, " +
        //                                    "ST_CONTAINER.F16, " +
        //                                    "ST_CONTAINER.F19, " +
        //                                    "ST_CONTAINER.F20, " +
        //                                    "ST_RECEIPT_EXTEND.EXTEND02, " +
        //                                    "RECEIPT.EXTERNALRECEIPTKEY2, " +
        //                                    "ORDERDETAIL.SKU, " +
        //                                    "ST_RECEIPTDETAIL_EXTEND.EXTEND06 as ItemDesc, " +
        //                                    "SKU.STDGROSSWGT, " +
        //                                    "SKU.STDCUBE, " +
        //                                    "LOTATTRIBUTE.LOTTABLE09, " +
        //                                    "wh25.f_split(ST_ORDERDETAIL_EXTEND.EXTEND03, " +
        //                                                 "'|+|', " +
        //                                                 "4) as RetailPrice " +
        //                      "FROM STA_ILIS.ST_CONTAINER      ST_CONTAINER, " +
        //                           "STA_ILIS.ST_CONTAINERD     ST_CONTAINERD, " +
        //                           "STA_ILIS.SP_PALLETS SP, " +
        //                           "STA_ILIS.SP_PALLET_BOXES SPLB, " +
        //                           "STA_ILIS.SP_PACK_BOXES SPB, " +
        //                           "STA_ILIS.SP_PACK_ORDERS SPO, " +
        //                           "WH25.ORDERS                ORDERS, " +
        //                           "WH25.ST_ORDERS_EXTEND      ST_ORDERS_EXTEND, " +
        //                           "WH25.ORDERDETAIL           ORDERDETAIL, " +
        //                           "WH25.SKU                   SKU, " +
        //                           "WH25.ST_ORDERDETAIL_EXTEND ST_ORDERDETAIL_EXTEND, " +
        //                           "WH25.PICKDETAIL            PICKDETAIL, " +
        //                           "WH25.LOTATTRIBUTE          LOTATTRIBUTE, " +
        //                           "WH25.RECEIPT               RECEIPT, " +
        //                           "WH25.RECEIPTDETAIL         RECEIPTDETAIL, " +
        //                           "WH25.ST_RECEIPT_EXTEND     ST_RECEIPT_EXTEND, " +
        //                           "WH25.ST_RECEIPTDETAIL_EXTEND     ST_RECEIPTDETAIL_EXTEND " +
        //                     "WHERE ST_CONTAINER.CONTAINERKEY = ST_CONTAINERD.CONTAINERKEY " +
        //                       "AND ST_CONTAINERD.ORDERKEY=SP.PALLET_NUM " +
        //                       "AND SP.ID=SPLB.PALLET_ID " +
        //                       "AND SPLB.BOX_ID=SPB.ID " +
        //                       "AND SPB.ORDER_ID=SPO.ID " +
        //                       "AND SPO.ORDER_KEY=ORDERS.ORDERKEY " +
        //                       "AND ORDERS.ORDERKEY = ST_ORDERS_EXTEND.ORDERKEY " +
        //                       "AND ORDERS.ORDERKEY = ORDERDETAIL.ORDERKEY " +
        //                       "AND ORDERDETAIL.STORERKEY = SKU.STORERKEY " +
        //                       "AND ORDERDETAIL.SKU = SKU.SKU " +
        //                       "AND ORDERDETAIL.ORDERKEY = ST_ORDERDETAIL_EXTEND.ORDERKEY " +
        //                       "AND ORDERDETAIL.ORDERLINENUMBER = " +
        //                           "ST_ORDERDETAIL_EXTEND.ORDERLINENUMBER " +
        //                       "AND ORDERDETAIL.ORDERKEY = PICKDETAIL.ORDERKEY " +
        //                       "and LOTATTRIBUTE.lot  not in ('0000106724','0000106725','0000106726') " +
        //                       "AND ORDERDETAIL.ORDERLINENUMBER = PICKDETAIL.ORDERLINENUMBER " +
        //                       "AND PICKDETAIL.LOT = LOTATTRIBUTE.LOT " +
        //                       "AND RECEIPT.RECEIPTKEY = ST_RECEIPT_EXTEND.RECEIPTKEY  " +
        //                       "AND RECEIPT.RECEIPTKEY = RECEIPTDETAIL.RECEIPTKEY " +
        //                       "AND    RECEIPTDETAIL.RECEIPTKEY = ST_RECEIPTDETAIL_EXTEND.RECEIPTKEY  " +
        //                       "AND    RECEIPTDETAIL.RECEIPTLINENUMBER = ST_RECEIPTDETAIL_EXTEND.RECEIPTLINENUMBER " +
        //                       "AND PICKDETAIL.SKU=RECEIPTDETAIL.SKU " +
        //                       "AND RECEIPT.RECEIPTKEY=ST_RECEIPTDETAIL_EXTEND.RECEIPTKEY " +
        //                       "AND LOTATTRIBUTE.LOTTABLE10=RECEIPT.RECEIPTKEY " +
        //                       "AND ST_CONTAINER.CONTAINERKEY = '" + ContainerNumber + "' " +
        //                       "AND RECEIPT.WAREHOUSEREFERENCE not  LIKE '%JOS19%' " +
        //                     "ORDER BY ST_CONTAINER.CONTAINERKEY ASC) A, " +
        //                   "(select s.sku, sum(s.qty) as shpqty " +
        //                      "from sp_pack_items s " +
        //                     "where 1 = 1 " +
        //                       "and s.box_id in " +
        //                           "(select s.box_id " +
        //                              "from sp_pallet_boxes s " +
        //                             "where 1 = 1 " +
        //                               "and s.pallet_id IN " +
        //                                   "(select ID " +
        //                                      "from sp_pallets s " +
        //                                     "where 1 = 1 " +
        //                                       "and s.pallet_num IN " +
        //                                           "(select s.orderkey " +
        //                                              "from st_containerd s " +
        //                                             "where 1 = 1 " +
        //                                               "and containerkey = '" + ContainerNumber + "' " +
        //                                               "and s.orderkey like '%JPB%'))) " +
        //                     "group by s.sku) B " +
        //             "where A.SKU = B.SKU " +
        //             "order by a.sku";

        //    strSQL = @"select CONTAINERKEY,
        //                       F01,
        //                       F02,
        //                       F03,
        //                       F04,
        //                       F05,
        //                       F06,
        //                       F08,
        //                       F09,
        //                       F10,
        //                       F11,
        //                       F12,
        //                       F13,
        //                       F15,
        //                       F16,
        //                       F19,
        //                       F20,
        //                       'Shenzhen' as ORIGIN,
        //                       EXTEND02,
        //                       EXTERNALRECEIPTKEY2,
        //                       A.SKU,
        //                       ItemDesc,
        //                       to_char(ROUND(shpqty, 2)) as shpqty,
        //                       '0' as cartons,
        //                       to_char(shpqty * STDCUBE / 1000000, '99990.9999') as CBMS,
        //                       to_char(shpqty * STDGROSSWGT / 1000, '99990.9999') AS KGS,
        //                       LOTTABLE09,
        //                       'IN0810240A' as invoiceNumber,
        //                       RetailPrice,
        //                       to_char(ROUND(shpqty, 2) * ROUND(RetailPrice, 2), '99990.99') as invoiceAmount,
        //                       '0.00' as freightCharge,
        //                       '' as sum_shpqty,
        //                       '' as sum_CBMS,
        //                       '' as sum_kgs
        //                  from (SELECT distinct ST_CONTAINER.CONTAINERKEY,
        //                                        ST_CONTAINER.F01,
        //                                        ST_CONTAINER.F02,
        //                                        ST_CONTAINER.F03,
        //                                        ST_CONTAINER.F04,
        //                                        ST_CONTAINER.F05,
        //                                        ST_CONTAINER.F06,
        //                                        ST_CONTAINER.F08,
        //                                        ST_CONTAINER.F09,
        //                                        ST_CONTAINER.F10,
        //                                        ST_CONTAINER.F11,
        //                                        ST_CONTAINER.F12,
        //                                        ST_CONTAINER.F13,
        //                                        ST_CONTAINER.F15,
        //                                        ST_CONTAINER.F16,
        //                                        ST_CONTAINER.F19,
        //                                        ST_CONTAINER.F20,
        //                                        ST_RECEIPT_EXTEND.EXTEND02,
        //                                        RECEIPT.EXTERNALRECEIPTKEY2,
        //                                        ORDERDETAIL.SKU,
        //                                        ST_RECEIPTDETAIL_EXTEND.EXTEND06 as ItemDesc,
        //                                        SKU.STDGROSSWGT,
        //                                        SKU.STDCUBE,
        //                                        LOTATTRIBUTE.LOTTABLE09,
        //                                        wh25.f_split(ST_ORDERDETAIL_EXTEND.EXTEND03,
        //                                                     '|+|',
        //                                                     4) as RetailPrice
        //                          FROM STA_ILIS.ST_CONTAINER        ST_CONTAINER,
        //                               STA_ILIS.ST_CONTAINERD       ST_CONTAINERD,
        //                               STA_ILIS.SP_PALLETS          SP,
        //                               STA_ILIS.SP_PALLET_BOXES     SPLB,
        //                               STA_ILIS.SP_PACK_BOXES       SPB,
        //                               STA_ILIS.SP_PACK_ORDERS      SPO,
        //                               WH25.ORDERS                  ORDERS,
        //                               WH25.ST_ORDERS_EXTEND        ST_ORDERS_EXTEND,
        //                               WH25.ORDERDETAIL             ORDERDETAIL,
        //                               WH25.SKU                     SKU,
        //                               WH25.ST_ORDERDETAIL_EXTEND   ST_ORDERDETAIL_EXTEND,
        //                               WH25.PICKDETAIL              PICKDETAIL,
        //                               WH25.LOTATTRIBUTE            LOTATTRIBUTE,
        //                               WH25.RECEIPT                 RECEIPT,
        //                               WH25.RECEIPTDETAIL           RECEIPTDETAIL,
        //                               WH25.ST_RECEIPT_EXTEND       ST_RECEIPT_EXTEND,
        //                               WH25.ST_RECEIPTDETAIL_EXTEND ST_RECEIPTDETAIL_EXTEND
        //                         WHERE 1 = 1
        //                           and ST_CONTAINER.CONTAINERKEY = ST_CONTAINERD.CONTAINERKEY
        //                           AND ST_CONTAINERD.ORDERKEY = SP.PALLET_NUM
        //                           AND SP.ID = SPLB.PALLET_ID
        //                           AND SPLB.BOX_ID = SPB.ID
        //                           AND SPB.ORDER_ID = SPO.ID
        //                           AND SPO.ORDER_KEY = ORDERS.ORDERKEY
        //                           AND ORDERS.ORDERKEY = ST_ORDERS_EXTEND.ORDERKEY
        //                           AND ORDERS.ORDERKEY = ORDERDETAIL.ORDERKEY
        //                           AND ORDERDETAIL.STORERKEY = SKU.STORERKEY
        //                           AND ORDERDETAIL.SKU = SKU.SKU
        //                           AND ORDERDETAIL.ORDERKEY = ST_ORDERDETAIL_EXTEND.ORDERKEY
        //                           AND ORDERDETAIL.ORDERLINENUMBER =
        //                               ST_ORDERDETAIL_EXTEND.ORDERLINENUMBER
        //                           AND ORDERDETAIL.ORDERKEY = PICKDETAIL.ORDERKEY
        //                           AND ORDERDETAIL.ORDERLINENUMBER = PICKDETAIL.ORDERLINENUMBER
        //                           AND PICKDETAIL.LOT = LOTATTRIBUTE.LOT
        //                           AND RECEIPT.RECEIPTKEY = ST_RECEIPT_EXTEND.RECEIPTKEY
        //                           AND RECEIPT.RECEIPTKEY = RECEIPTDETAIL.RECEIPTKEY
        //                           AND RECEIPTDETAIL.RECEIPTKEY = ST_RECEIPTDETAIL_EXTEND.RECEIPTKEY
        //                           AND RECEIPTDETAIL.RECEIPTLINENUMBER =
        //                               ST_RECEIPTDETAIL_EXTEND.RECEIPTLINENUMBER
        //                           AND PICKDETAIL.SKU = RECEIPTDETAIL.SKU
        //                           AND RECEIPT.RECEIPTKEY = ST_RECEIPTDETAIL_EXTEND.RECEIPTKEY
        //                           AND LOTATTRIBUTE.LOTTABLE10 = RECEIPT.RECEIPTKEY
        //                           AND ST_CONTAINER.CONTAINERKEY = '" + ContainerNumber + "' ";
        //    strSQL += @" ORDER BY ST_CONTAINER.CONTAINERKEY ASC) A,
        //                       (select spi.sku, sum(spi.qty) as shpqty
        //                          from sp_pack_items   spi,
        //                               sp_pallet_boxes spb,
        //                               sp_pallets      sp,
        //                               st_containerd   sc
        //                         where 1 = 1
        //                           and sc.orderkey = sp.pallet_num
        //                           and sp.id = spb.pallet_id
        //                           and spb.box_id = spi.box_id
        //                           and sc.containerkey = '" + ContainerNumber + "' ";
        //    strSQL += @" group by spi.sku) B
        //                 where A.SKU = B.SKU
        //                 order by a.sku";

        //    DataSet dsRes = _Conn.ExecuteToDataSet(strSQL);
        //    dsRes.Tables[0].TableName = "Manifest";

        //    return dsRes;
        //}
        void InsertFileName()
        {
            List<string> sqlArr = new List<string>();
            string fileName = "cpo202110251183.csv";
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
            var a = cc.DoTran(sqlArr.ToArray());
            var ssss = "";
        }

        void InsertScanItem()
        {
            List<string> sqlArr = new List<string>();
            sqlArr.Clear();
            //i是箱编号
            var c = 28;
            for (var i = 1; i < 2; i++)
            {
                string sql = string.Format(@"select spo.id as order_id,spb.id as box_id 
                from sp_pack_orders spo,sp_pack_boxes spb where spo.id = spb.order_id and spo.order_key = '{0}' and spb.box_sort = {1}", "0000176509", i);
                DataTable d = cc.ExecuteToDataTable(sql);
                string orderId = d.Rows[0]["ORDER_ID"].ToString();
                string boxId = d.Rows[0]["BOX_ID"].ToString();

                string sku = "655033-0-1";
                for (var r = 0; r < 1; r++)
                {
                    string ssql = string.Format(@"INSERT INTO SP_PACK_ITEMS(ID, ORDER_ID, BOX_ID, SKU, QTY, WHO_ADD) VALUES('{0}','{1}','{2}','{3}','{4}','{5}')", Guid.NewGuid(), orderId, boxId, sku, "1", "TEST11");
                    sqlArr.Add(ssql);
                }
                c++;

            }
            var a = cc.DoTran(sqlArr.ToArray());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int a = 0;
            List<string> sqlArr = new List<string>();


            //InsertFileName();
            InsertScanItem();

            UpdateOriginQty();









        }
        void UpdateOriginQty()
        {
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
                        sql2 = string.Format("update WH25.RECEIPTDETAIL set QTYEXPECTED ='{0}' WHERE RECEIPTKEY = '{1}' and sku='{2}'", dr[3].ToString(), receiptKey, dr[2].ToString());
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
