using Biz;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
            for(var i = 12; i < 26; i++)
            {
                List<string> sqlArr = new List<string>();
                string fileName = "cpo2022050612" + i + ".csv";
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
                from sp_pack_orders spo,sp_pack_boxes spb where spo.id = spb.order_id and spo.order_key = '{0}' and spb.box_sort = {1}", "0000187552", i);
                DataTable d = cc.ExecuteToDataTable(sql);
                string orderId = d.Rows[0]["ORDER_ID"].ToString();
                string boxId = d.Rows[0]["BOX_ID"].ToString();

                string sku = "692137-0-1";
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

            //UpdateOriginQty();









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

                    sql2 = string.Format(@"select oms_no from oms_order_head where client_order_no = '{0}' and Customer_Po_No = '{1}'", dr[2].ToString(), dr[0].ToString());
                    DataTable dt = dB.ExecuteToDataTable(sql2);
                    if (dt.Rows.Count > 0)
                    {
                        var omsNo = dt.Rows[0][0].ToString();
                        sql2 = string.Format(@"SELECT receiptkey FROM WH25.RECEIPT R WHERE R.EXTERNRECEIPTKEY  = '{0}'", omsNo);

                        dt = cc.ExecuteToDataTable(sql2);
                        var receiptKey = dt.Rows[0][0].ToString();
                        //var date = FormatDate(dr[1].ToString());
                        sql2 = string.Format("update WH25.RECEIPTDETAIL set QTYEXPECTED ='{0}' WHERE RECEIPTKEY = '{1}' and sku='{2}'", dr[4].ToString(), receiptKey, dr[3].ToString());
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

        private void button3_Click(object sender, EventArgs e)
        {
            string fms = "Provider=MSDAORA.1;Password=stafms;User ID=stafms;Data Source=10g";
            EntDB cc = new EntDB(fms);
            List<string> aSql = new List<string>();
            string sqlid = "";
            string textId = "";
            string oms_no = "";

            string strYMD = DateTime.Now.AddDays(-150).ToString("yyyy-MM-dd");
            string strYMD1 = DateTime.Now.ToString("yyyyMMdd");
            string strYMD2 = DateTime.Now.ToString("yyyyMMddhhmmss");
            string s1 = strYMD2.Substring(2, 6);
            string s2 = strYMD2.Substring(8, 4);

            string sql = "select DISTINCT(f.shipment_fee_id) " +
            "from order_fee f where    f.CUSTOMER_ID ='25328026'   " +
            " and f.operator_rdc = 'STA LINGANG'";

            sql = string.Format(@"
select
--f.*

DISTINCT f.shipment_fee_id, f.sta_shipment_no,f.customer_po_no

from order_fee f
where 1 = 1
and f.CUSTOMER_ID = '25328026'
and f.operator_rdc = 'STA LINGANG'
and f.customer_po_no in (
            '5261834',
'5260395',
'5260885',
'5261059',
'5261336',
'5261601',
'5260786',
'5260578',
'5260918',
'5261174',
'5261832',
'5260838',
'5260787',
'5261590',
'5261278',
'5260980',
'5261175',
'5261734',
'5261692',
'5263168',
'5263457',
'5263284',
'5262610',
'5263377',
'5262819',
'5262762',
'5263163',
'5262648',
'5263081',
'5262090',
'5264272',
'5264747',
'5264852',
'5264791',
'5264919',
'5264249',
'5263561',
'5264620',
'5263826',
'5263636',
'5264769',
'5264343',
'5264132',
'5263827',
'5265254',
'5265098',
'5264977',
'5265200',
'5265089',
'5265110',
'5265111',
'5265194',
'5265241',
'5265050',
'5265138',
'5265218',
'5265211',
'5265478',
'5265453',
'5265353',
'5265310',
'5265379',
'5265315',
'5265462',
'5265456',
'5265486',
'5265375',
'5265423',
'5265408',
'5266676',
'5266451',
'5266127',
'5266767',
'5265783',
'5265833',
'5265656',
'5265712',
'5265912',
'5266919',
'5267455',
'5267688',
'5268112',
'5267014',
'5267156',
'5267448',
'5266908',
'5267600',
'5267863',
'5267966',
'5268270',
'5269532',
'5268979',
'5268827',
'5268636',
'5268553',
'5268700',
'5268922',
'5269116',
'5269332',
'5281576',
'5281831',
'5281772',
'5281893',
'5281596',
'5280963',
'5280914',
'5281168',
'5280956',
'5283547',
'5283141',
'5283444',
'5283556',
'5283156',
'5282147',
'5282837',
'5283443',
'5282257',
'5283202',
'5282790',
'5282071',
'5282973',
'5283327',
'5283474',
'5282292',
'5284042',
'5284224',
'5284696',
'5284638',
'5284134',
'5284144',
'5284780',
'5284653',
'5284812',
'5283967',
'5284848',
'5284501',
'5283711',
'5283817',
'5283894',
'5286041',
'5286149',
'5286135',
'5286077',
'5286072',
'5286122',
'5286143',
'5286091',
'5286037',
'5286105',
'5286148',
'5285933',
'5285979',
'5286016',
'5286095',
'5286062',
'5286106',
'5285794',
'5285138',
'5285010',
'5285692',
'5285615',
'5285042',
'5285432',
'5285139',
'5285164',
'5285551',
'5285716',
'5285241',
'5285336',
'5285839',
'5286191',
'5286308',
'5286174',
'5286297',
'5286301',
'5286193',
'5286263',
'5286235',
'5286251',
'5286252',
'5286292',
'5286192',
'5286434',
'5286565',
'5286538',
'5286447',
'5286498',
'5288519',
'5288557',
'5289924',
'5289535',
'5289369',
'5289841',
'5289900',
'5289118',
'5288831',
'5289280',
'5289117',
'5289519',
'5288750',
'5289005',
'5289004',
'5289116',
'5289305',
'5289834',
'5291609',
'5291694',
'5290847',
'5290578',
'5291361',
'5290822',
'5291227',
'5290914',
'5291612',
'5291408',
'5291514',
'5292193',
'5293138',
'5292618',
'5292024',
'5292984',
'5291862',
'5292718',
'5292986',
'5293122',
'5292511',
'5292303',
'5293217',
'5292530',
'5293945',
'5293879',
'5293889',
'5293333',
'5293956',
'5293413',
'5293995',
'5293785',
'5294034',
'5294073',
'5293860',
'5293787',
'5293939',
'5293996',
'5293394',
'5293806',
'5293577',
'5294074',
'5293555',
'5294283',
'5294096',
'5294136',
'5294152',
'5294122',
'5294194',
'5294203',
'5294177',
'5294254',
'5294165',
'5294251',
'5294359',
'5294477',
'5295484',
'5294756',
'5294755',
'5295449',
'5294631',
'5295243',
'5295288',
'5296614',
'5296871',
'5295681',
'5296791',
'5296554',
'5297067',
'5295840',
'5296630',
'5296546',
'5296962',
'5295805',
'5296209',
'5297023',
'5297020',
'5296493',
'5296072',
'5296382',
'5297942',
'5298368',
'5297140',
'5297711',
'5298394',
'5297700',
'5297529',
'5297920',
'5298036',
'5298220',
'5298278',
'5297739',
'5297517',
'5298145',
'5298299',
'5298369',
'5297837',
'5302299',
'5302504',
'5301950',
'5302143',
'5302273',
'5302317',
'5301947',
'5302137',
'5302332',
'5300036',
'5299596',
'5299282',
'5299287',
'5300045',
'5300239',
'5300359',
'5299860',
'5300012',
'5300210',
'5298678',
'5299390',
'5299761',
'5300158',
'5298469',
'5299900',
'5299762',
'5299983',
'5301722',
'5301234',
'5301705',
'5300706',
'5300791',
'5301712',
'5301876',
'5301529',
'5301421',
'5300458',
'5300554',
'5300630',
'5300799',
'5300895',
'5302708',
'5302738',
'5302846',
'5302669',
'5302767',
'5302905',
'5302914',
'5302969',
'5302798',
'5302674',
'5302745',
'5302613',
'5302532',
'5302488',
'5302577',
'5302291',
'5302594',
'5304381',
'5304063',
'5304686',
'5304237',
'5304452',
'5303269',
'5304666',
'5303511',
'5304525',
'5304578',
'5304132',
'5303682',
'5305343',
'5305527',
'5305254',
'5305738',
'5305828',
'5305403',
'5305669',
'5306039',
'5306041',
'5305743',
'5305821',
'5308155',
'5308259',
'5307904',
'5308103',
'5307693',
'5307800',
'5307158',
'5307566',
'5307438',
'5307946',
'5308043',
'5307421',
'5311687',
'5311696',
'5311684',
'5311787',
'5311708',
'5311697',
'5311719',
'5311673',
'5311692',
'5311742',
'5311737',
'5309539',
'5309596',
'5310013',
'5310008',
'5309194',
'5309085',
'5309791',
'5308987',
'5308988',
'5309335',
'5309444',
'5309481',
'5309173',
'5309708',
'5311652',
'5311394',
'5311599',
'5311467',
'5311535',
'5311526',
'5311584',
'5311485',
'5311630',
'5311650',
'5311380',
'5311532',
'5311436',
'5311565',
'5311395',
'5311616',
'5311511',
'5311651',
'5311639',
'5311291',
'5310075',
'5311073',
'5310190',
'5310524',
'5311091',
'5310392',
'5310563',
'5310880',
'5311276',
'5311065',
'5311198',
'5311068',
'5312649',
'5312977',
'5312556',
'5312863',
'5311978',
'5312045',
'5312141',
'5312770',
'5312974',
'5312188',
'5311882',
'5312756',
'5313267',
'5313546',
'5313687',
'5313741',
'5313796',
'5314290',
'5313950',
'5314054',
'5314309',
'5314332',
'5314031',
'5313420',
'5314095',
'5313702',
'5314202',
'5315610',
'5315480',
'5315251',
'5314453',
'5314609',
'5315659',
'5315174',
'5315650',
'5314770',
'5315692',
'5315552',
'5315521',
'5315346',
'5315242',
'5317186',
'5317366',
'5317365',
'5316775',
'5316868',
'5317141',
'5317178',
'5316448',
'5316450',
'5317255',
'5316449',
'5316461',
'5316575',
'5317266',
'5316568',
'5317005',
'5317204',
'5317130',
'5319075',
'5319106',
'5319263',
'5319127',
'5319218',
'5319187',
'5319241',
'5319094',
'5319064',
'5319145',
'5319081',
'5319149',
'5319048',
'5318811',
'5318960',
'5319037',
'5319047',
'5318742',
'5318894',
'5318908',
'5318912',
'5318838',
'5317891',
'5318657',
'5318534',
'5317994',
'5318007',
'5317585',
'5318267',
'5318612',
'5317696',
'5317884',
'5317927',
'5318281',
'5318656',
'5318122',
'5317949',
'5317709',
'5317492',
'5318025',
'5318026',
'5317566',
'5317726',
'5320091',
'5319490',
'5320246',
'5319486',
'5320213',
'5319416',
'5319402',
'5319787',
'5319601',
'5319384',
'5323059',
'5322871',
'5323075',
'5323039',
'5322872',
'5322948',
'5322986',
'5320427',
'5322771',
'5322145',
'5322688',
'5322425',
'5322677',
'5324005',
'5324450',
'5323267',
'5324215',
'5324546',
'5324470',
'5323800',
'5324124',
'5324390',
'5323715',
'5324021',
'5323439',
'5323864',
'5324186',
'5326269',
'5325702',
'5326069',
'5326155',
'5325301',
'5325250',
'5326058',
'5325243',
'5325890',
'5325585',
'5325994',
'5326211',
'5326100',
'5324835',
'5325586',
'5325420',
'5325311',
'5328039',
'5327824',
'5327841',
'5327996',
'5327862',
'5327956',
'5327922',
'5328028',
'5328047',
'5328052',
'5327951',
'5327981',
'5327771',
'5327924',
'5327851',
'5327838',
'5327954',
'5328011',
'5328062',
'5328143',
'5328222',
'5328252',
'5328124',
'5328105',
'5328140',
'5328214',
'5328205',
'5328242',
'5328181',
'5328290',
'5328089',
'5328153',
'5327629',
'5327333',
'5326688',
'5327720',
'5327729',
'5326798',
'5326918',
'5326992',
'5327616',
'5326986',
'5327542',
'5327630',
'5327046',
'5327669',
'5327344',
'5326539',
'5326686',
'5328478',
'5329463',
'5328322',
'5328544',
'5328735',
'5329482',
'5328444',
'5329492',
'5328455',
'5328673',
'5330101',
'5330652',
'5330716',
'5329708',
'5330403',
'5330542',
'5330200',
'5330055',
'5330331',
'5332067',
'5331279',
'5331281',
'5331932',
'5331441',
'5332115',
'5331359',
'5331762',
'5331734',
'5332120',
'5331044',
'5331669',
'5334183',
'5333126',
'5333242',
'5333812',
'5333335',
'5333127',
'5334142',
'5333921',
'5334028',
'5334298',
'5334288',
'5334184',
'5333503',
'5334320',
'5333886',
'5335036',
'5335161',
'5335232',
'5335443',
'5334965',
'5335375',
'5334463',
'5335043',
'5335351',
'5335488',
'5334776',
'5335212',
'5335509',
'5334725',
'5335088',
'5335572',
'5334869',
'5334760',
'5334615',
'5334561',
'5335480',
'5334926',
'5334908',
'5335684',
'5335674',
'5335673',
'5335589',
'5335701',
'5335614',
'5335625',
'5335619',
'5335640',
'5335613'

)");



            //customer_po_no sta_shipment_no
            DataTable dt = cc.ExecuteToDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    var sta_shipment_no = dt.Rows[i]["sta_shipment_no"].ToString();
                    var customer_po_no = dt.Rows[i]["customer_po_no"].ToString();
                    aSql.Clear();
                    Double VATRate = 1.09;//2018年5月变更为1.10// 20190408 更新为1.09 ,9% 税率
                    //取ID 的值
                    sqlid = "select t.LEGO_FEE_INVOICE_ID From lego_fee_invoice_id t ";
                    textId = Convert.ToString(cc.GetObject(sqlid));
                    oms_no = "LEGE" + textId;

                    //取计费后的汇总结果，装运运费及燃油附加费
                    string sqlFee = "select f.freight_fee,f.total_Fuel_Fee from shipment_fee f where  f.customer_id ='25328026'  and id ='" + dt.Rows[i]["shipment_fee_id"].ToString() + "'";
                    DataTable dtFee = cc.ExecuteToDataTable(sqlFee);
                    double TranFee = 0;
                    double FUEL_FEE = 0;
                    if (dtFee.Rows.Count > 0)
                    {
                        TranFee = Convert.ToDouble(dtFee.Rows[0]["freight_fee"].ToString()) * VATRate;
                        FUEL_FEE = Convert.ToDouble(dtFee.Rows[0]["total_Fuel_Fee"].ToString()) * VATRate;
                    }

                    double TotalFee = TranFee + FUEL_FEE;
                    string strid = textId;
                    string feedbackOrderData = "";

                    //头字符
                    feedbackOrderData += "UNA:+.? '";
                    feedbackOrderData += "\r\n";
                    //GSN号码需要修改为中国的
                    feedbackOrderData += "UNB+UNOC:3+TOLLCHINA:ZZ+5790000000708:14+" + s1 + ":" + s2 + "+" + strid + "++++++1'";
                    feedbackOrderData += "\r\n";

                    //正文部门
                    feedbackOrderData += "UNH+" + oms_no + "+INVOIC:D:96A:UN'";
                    feedbackOrderData += "\r\n";
                    feedbackOrderData += "BGM+381+" + strid + "+9'";
                    feedbackOrderData += "\r\n";
                    feedbackOrderData += "DTM+137:" + strYMD1 + ":102'";
                    feedbackOrderData += "\r\n";
                    feedbackOrderData += "NAD+IV+5790001087326::9'"; //接收方
                    feedbackOrderData += "\r\n";

                    feedbackOrderData += "NAD+II+TOLLCHINA::9'"; //这个号码需要修改为中国的
                    feedbackOrderData += "\r\n";

                    //feedbackOrderData += "RFF+VA+'";
                    //feedbackOrderData += "\r\n";
                    feedbackOrderData += "CUX+2:CNY:4'";
                    feedbackOrderData += "\r\n";
                    feedbackOrderData += "PAT+1'";
                    feedbackOrderData += "\r\n";
                    feedbackOrderData += "LIN+1'";
                    feedbackOrderData += "\r\n";
                    feedbackOrderData += "MOA+203:" + TotalFee.ToString() + "'";//总价格
                    feedbackOrderData += "\r\n";


                    feedbackOrderData += "RFF+SRN:" + customer_po_no + "'"; //这行SRN 号码：是否需要LEGO 提供的装运编号
                    feedbackOrderData += "\r\n";

                    feedbackOrderData += "ALC+C++++FRT:132:92'";//运输费（含税），影射到’FRT’字段 (field 7161) + 燃油附加部分
                    feedbackOrderData += "\r\n";
                    feedbackOrderData += "MOA+8:" + (TotalFee).ToString() + "'";
                    feedbackOrderData += "\r\n";
                    feedbackOrderData += "ALC+C++++BAF:132:92'";//燃油附加费（含税），影射到‘BAF’字段 (k 7161) -- 要求设置为0 。把燃油价格直接算成运费。
                    feedbackOrderData += "\r\n";
                    feedbackOrderData += "MOA+8:0'";
                    feedbackOrderData += "\r\n";

                    feedbackOrderData += "UNS+S'";
                    feedbackOrderData += "\r\n";
                    feedbackOrderData += "MOA+79:" + TotalFee.ToString() + "'";//总价格
                    feedbackOrderData += "\r\n";
                    feedbackOrderData += "MOA+86:" + TotalFee.ToString() + "'";//总价格
                    feedbackOrderData += "\r\n";
                    feedbackOrderData += "TAX+7+VAT+++:::0+Z'";
                    feedbackOrderData += "\r\n";

                    feedbackOrderData += "MOA+176:0'";
                    feedbackOrderData += "\r\n";
                    feedbackOrderData += "UNT+20+" + oms_no + "'";
                    feedbackOrderData += "\r\n";

                    //尾部统计
                    feedbackOrderData += "UNZ+1+" + strid + "'";
                    feedbackOrderData += "\r\n";
                    string FeedbackFileName = "LEGO_CreditNote_" + oms_no + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";
                    WriteFile(FeedbackFileName, "ASCII", feedbackOrderData);
                    string sqlupdate = "update order_fee set EXPORT_FLAG='C'  where   CUSTOMER_ID ='25328026' and sta_shipment_no='" + sta_shipment_no + "'";
                    string IDupdate = "update lego_fee_invoice_id set lego_fee_invoice_id=trim(to_char(lego_fee_invoice_id+1,'00000000')) ";
                    string logadd = "insert into  lego_fee_invoice_log(SHIPMENT_NUMBER,OTHER_FEERE_MARK,FLAG,FILENAME) values('" + sta_shipment_no + "','" + strid + "','C','" + FeedbackFileName + "')";
                    aSql.Add(sqlupdate);
                    aSql.Add(IDupdate);
                    aSql.Add(logadd);
                    cc.DoTran(aSql.ToArray());

                }
            }
        }
        private void WriteFile(string FeedbackFileName, string encodingName, string content)
        {

            string sCopyPath = "";
            string filePath = @"C:\Toll\PROJECTS\LEGOBMSEDI\Backup\" + DateTime.Today.ToString("yyyyMMdd") + @"\";
            sCopyPath = @"Y:\STANDAEDI\Feedback\LEGBMS\INVOICE\" + FeedbackFileName;

            if (!Directory.Exists(filePath)) { Directory.CreateDirectory(filePath); }
            StreamWriter sr = new StreamWriter(filePath + @"\" + FeedbackFileName, true, (encodingName.Length == 0) ? Encoding.Default : Encoding.GetEncoding(encodingName));
            sr.WriteLine(content);
            sr.Flush();
            sr.Close();
            sr = null;
            System.IO.File.Copy(filePath + @"\" + FeedbackFileName, sCopyPath);
        }
    }
}
