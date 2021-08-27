using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using Scan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace APP_TAG_RETURN_REPORT
{
    public partial class Form1 : Form
    {
        OMS_DB.DataBaseAccessSoapClient conn = new OMS_DB.DataBaseAccessSoapClient();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var rdc = comboBox1.SelectedItem.ToString();
                var client = comboBox2.SelectedItem.ToString();
                var start = dateTimePicker1.Text.ToString();
                var end = dateTimePicker2.Text.ToString();
                if (!string.IsNullOrEmpty(rdc) && !string.IsNullOrEmpty(client) && !string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
                {
                    var sql = string.Format(@"SELECT OH.OMS_NO,OH.CLIENT_ORDER_NO,OH.ORDER_CREATION_DTE,OH.RECEIVE_PARTY_CODE,OH.RECEIVE_PARTY_NAME,OD.PRODUCT_NO,OD.PRODUCT_NAME,OD.LINE_ITEM_NO,OD.PO_ORDER_QTY,OD.PO_UOM,'1' AS QTY_BOX,'' AS CARTON_BOX_QTY,'' AS SCANNED_QTY FROM OMS_ORDER_HEAD OH,OMS_ORDER_DETAIL OD WHERE OH.OMS_NO = OD.OMS_NO AND OH.ORDER_TYPE = 'ISSUE' AND OH.CLIENT_C = '{0}' AND OH.OPERATING_WAREHOUSE_CODE = '{1}' AND TRUNC(OH.ORDER_CREATION_DTE) >= TRUNC(TO_DATE('{2}','yyyy-MM-dd hh24:mi:ss')) AND TRUNC(OH.ORDER_CREATION_DTE) <= TRUNC(TO_DATE('{3}','yyyy-MM-dd hh24:mi:ss')) AND OD.LINE_REMARKS = '按箱扫描' ORDER BY OH.ORDER_CREATION_DTE", client, rdc, start, end);
                    //测试
                    //sql = string.Format(@"SELECT OH.OMS_NO,OH.CLIENT_ORDER_NO,OH.ORDER_CREATION_DTE,OH.RECEIVE_PARTY_CODE,OH.RECEIVE_PARTY_NAME,OD.PRODUCT_NO,OD.PRODUCT_NAME,OD.LINE_ITEM_NO,OD.PO_ORDER_QTY,OD.PO_UOM,'1' AS QTY_BOX,'' AS CARTON_BOX_QTY,'' AS SCANNED_QTY FROM OMS_ORDER_HEAD OH,OMS_ORDER_DETAIL OD WHERE OH.OMS_NO = OD.OMS_NO AND OH.CLIENT_ORDER_NO  in ('0360557205','0360557394')");
                    DataTable dt = conn.GetDataSet(sql, new string[] { }).Tables[0];
                    if (dt.Rows.Count > 0)
                    {
                        var rows = dt.Select().ToList();
                        var groupRows = (from row in rows
                                         group row by new
                                         {
                                             omsNo = row["OMS_NO"].ToString(),
                                             clientOrderNo = row["CLIENT_ORDER_NO"].ToString()
                                         } into g
                                         select new
                                         {
                                             key = g.Key,
                                             LineRows = g.ToList()
                                         }).ToList();
                        var listRows = new List<DataRow>();
                        foreach (var row in groupRows)
                        {
                            var clientOrderNo = row.key.clientOrderNo;
                            ObSetRootA model = new ObSetRootA();
                            ObSetRootA res = PostBase.GetNeedScanInfo(clientOrderNo, 2, model, "Y");
                            if (res.result.d.results.Count > 0)
                            {
                                var param = res.result.d.results[0].ExOBQty.results;
                                CalcQty(param, row.LineRows, client);
                                foreach (var dr in row.LineRows)
                                {
                                    listRows.Add(dr);
                                }
                            }
                            else
                            {
                                throw new Exception(clientOrderNo + "请求扫描数据失败，请稍后再尝试导出~");
                            }
                        }
                        ExportExcel(listRows);
                    }
                    else
                    {
                        MessageBox.Show("暂无订单");
                    }
                }
                else
                {
                    MessageBox.Show("查询条件不可为空");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        public void CalcQty(List<ExOBQtyResultsItem> param, IEnumerable<DataRow> lineRows, string client)
        {
            foreach (var item in param)
            {
                var totalQty = item.TotalBoxQty.ToString();
                var poOrderQty = string.Empty;
                var sku = item.Material;
                var factor = GetPoUomPack(client, sku);
                var sameSkuRows = lineRows.Where(x => x["PRODUCT_NO"].ToString() == item.Material).ToList();
                foreach (var row in sameSkuRows)
                {
                    poOrderQty = Convert.ToString(Convert.ToDouble(row["PO_ORDER_QTY"].ToString()) / factor);
                    if (Convert.ToDouble(poOrderQty) <= Convert.ToDouble(totalQty))
                    {
                        row["CARTON_BOX_QTY"] = poOrderQty;
                        row["SCANNED_QTY"] = Convert.ToString(Convert.ToDouble(poOrderQty) - Convert.ToDouble(item.CurrentBoxQty.ToString()));
                        totalQty = Convert.ToString(Convert.ToDouble(totalQty) - Convert.ToDouble(poOrderQty));
                    }
                }
            }
        }

        public Double GetPoUomPack(string client, string sku) 
        {
            try
            {
                string sql = string.Format(@"SELECT CLIENT_UOM_FACTOR FROM OMS_PRODUCT WHERE CLIENT_C = '{0}' AND PRODUCT_NO = '{1}'", client, sku);
                DataTable dt = conn.GetDataSet(sql, new string[] { }).Tables[0];
                var factor = string.IsNullOrEmpty(dt.Rows[0]["CLIENT_UOM_FACTOR"].ToString()) ? 1 : Convert.ToDouble(dt.Rows[0]["CLIENT_UOM_FACTOR"].ToString());
                return factor;
            }
            catch(Exception ex)
            {
                throw new Exception("获取转换率出错");
            }
            
        }

        public void ExportExcel(List<DataRow> list)
        {
            if (list.Count > 0)
            {
                HSSFWorkbook hssfworkbook = new HSSFWorkbook();
                var sh_result = hssfworkbook.CreateSheet("Sheet1");
                var row_title0 = sh_result.CreateRow(0);
                sh_result.GetRow(0).CreateCell(0).SetCellValue("OMS号");
                sh_result.GetRow(0).CreateCell(1).SetCellValue("订单号");
                sh_result.GetRow(0).CreateCell(2).SetCellValue("订单创建日期");
                sh_result.GetRow(0).CreateCell(3).SetCellValue("收货人代码");
                sh_result.GetRow(0).CreateCell(4).SetCellValue("收货人名称");
                sh_result.GetRow(0).CreateCell(5).SetCellValue("产品代码");
                sh_result.GetRow(0).CreateCell(6).SetCellValue("产品名称");
                sh_result.GetRow(0).CreateCell(7).SetCellValue("客户子行号");
                sh_result.GetRow(0).CreateCell(8).SetCellValue("PO数量");
                sh_result.GetRow(0).CreateCell(9).SetCellValue("PO单位");
                sh_result.GetRow(0).CreateCell(10).SetCellValue("Qty/Shipping Box");
                sh_result.GetRow(0).CreateCell(11).SetCellValue("CartonBox");
                sh_result.GetRow(0).CreateCell(12).SetCellValue("Scanned QTY");

                for (int i = 0; i < list.Count; i++)
                {
                    //OH.OMS_NO,OH.CLIENT_ORDER_NO,OH.ORDER_CREATION_DTE,OH.RECEIVE_PARTY_CODE,OH.RECEIVE_PARTY_NAME,OD.PRODUCT_NO,OD.PRODUCT_NAME,OD.LINE_ITEM_NO,OD.PO_ORDER_QTY,OD.PO_UOM,'1' AS QTY_BOX,'' AS CARTON_BOX_QTY,'' AS SCANNED_QTY
                    var _row = sh_result.CreateRow(i + 1);
                    sh_result.GetRow(i + 1).CreateCell(0).SetCellValue(list[i]["OMS_NO"].ToString());
                    sh_result.GetRow(i + 1).CreateCell(1).SetCellValue(list[i]["CLIENT_ORDER_NO"].ToString());
                    sh_result.GetRow(i + 1).CreateCell(2).SetCellValue(list[i]["ORDER_CREATION_DTE"].ToString());
                    sh_result.GetRow(i + 1).CreateCell(3).SetCellValue(list[i]["RECEIVE_PARTY_CODE"].ToString());
                    sh_result.GetRow(i + 1).CreateCell(4).SetCellValue(list[i]["RECEIVE_PARTY_NAME"].ToString());
                    sh_result.GetRow(i + 1).CreateCell(5).SetCellValue(list[i]["PRODUCT_NO"].ToString());
                    sh_result.GetRow(i + 1).CreateCell(6).SetCellValue(list[i]["PRODUCT_NAME"].ToString());
                    sh_result.GetRow(i + 1).CreateCell(7).SetCellValue(list[i]["LINE_ITEM_NO"].ToString());
                    sh_result.GetRow(i + 1).CreateCell(8).SetCellValue(list[i]["PO_ORDER_QTY"].ToString());
                    sh_result.GetRow(i + 1).CreateCell(9).SetCellValue(list[i]["PO_UOM"].ToString());
                    sh_result.GetRow(i + 1).CreateCell(10).SetCellValue(list[i]["QTY_BOX"].ToString());
                    sh_result.GetRow(i + 1).CreateCell(11).SetCellValue(list[i]["CARTON_BOX_QTY"].ToString());
                    sh_result.GetRow(i + 1).CreateCell(12).SetCellValue(list[i]["SCANNED_QTY"].ToString());
                }

                AutoColumnWidth(sh_result, 7);
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "选择文件保存路径";
                string path = string.Empty;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    path = dialog.SelectedPath + @"\";
                    string foldPath = Path.Combine(path, "OSR扫描信息报告" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls");
                    FileStream file = new FileStream(foldPath, FileMode.Create);
                    hssfworkbook.Write(file);
                    file.Close();
                    MessageBox.Show("导出成功。");
                }
                else
                {
                    MessageBox.Show("请选择文件保存路径~");
                }
            }
            
        }
        private void AutoColumnWidth(ISheet sheet, int columns)
        {
            for (int i = 0; i < columns; i++)
            {
                int columnWidth = sheet.GetColumnWidth(i) / 256;
                for (int j = 0; j < sheet.LastRowNum; j++)
                {
                    IRow row = sheet.GetRow(j);
                    if (row == null)
                    {
                        continue;
                    }
                    ICell cell = row.GetCell(i);
                    if (cell == null)
                    {
                        continue;
                    }
                    int cellWidth = Encoding.UTF8.GetBytes(cell.ToString()).Length;
                    if (columnWidth <= cellWidth)
                    {
                        columnWidth = cellWidth + 2;
                    }
                }
                sheet.SetColumnWidth(i, columnWidth * 256);
            }
        }
    }
}
