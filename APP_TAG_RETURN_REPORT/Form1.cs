using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
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
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                dialog.Description = "请选择文件路径";
                string path = string.Empty;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    path = dialog.SelectedPath + @"\";
                }
                string sql = string.Format(@"");
                DataTable dt = conn.GetDataSet(sql,new string[] { }).Tables[0];
                if (dt.Rows.Count > 0)
                {
                    HSSFWorkbook hssfworkbook = new HSSFWorkbook();
                    var sh_result = hssfworkbook.CreateSheet("Sheet1");
                    var row_title0 = sh_result.CreateRow(0);
                    sh_result.GetRow(0).CreateCell(0).SetCellValue("承运商名称");
                    sh_result.GetRow(0).CreateCell(1).SetCellValue("司机名称");
                    sh_result.GetRow(0).CreateCell(2).SetCellValue("客户代码");
                    sh_result.GetRow(0).CreateCell(3).SetCellValue("装运编号");
                    sh_result.GetRow(0).CreateCell(4).SetCellValue("OMS号");
                    sh_result.GetRow(0).CreateCell(5).SetCellValue("订单号");
                    sh_result.GetRow(0).CreateCell(6).SetCellValue("收货人名称");
                    sh_result.GetRow(0).CreateCell(7).SetCellValue("收货人地址");
                    sh_result.GetRow(0).CreateCell(8).SetCellValue("终点城市");
                    sh_result.GetRow(0).CreateCell(9).SetCellValue("省份");
                    sh_result.GetRow(0).CreateCell(10).SetCellValue("发货数量");
                    sh_result.GetRow(0).CreateCell(11).SetCellValue("重量");
                    sh_result.GetRow(0).CreateCell(12).SetCellValue("体积");
                    sh_result.GetRow(0).CreateCell(13).SetCellValue("出库确认时间");
                    sh_result.GetRow(0).CreateCell(14).SetCellValue("实际交付日期");
                    sh_result.GetRow(0).CreateCell(15).SetCellValue("要求返单日期");
                    sh_result.GetRow(0).CreateCell(16).SetCellValue("实际返单时间");
                    sh_result.GetRow(0).CreateCell(17).SetCellValue("TAG卡号");
                    sh_result.GetRow(0).CreateCell(18).SetCellValue("TAG卡返回时间");
                    sh_result.GetRow(0).CreateCell(19).SetCellValue("经度");
                    sh_result.GetRow(0).CreateCell(20).SetCellValue("纬度");
                    sh_result.GetRow(0).CreateCell(21).SetCellValue("电量");

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        var _row = sh_result.CreateRow(i + 1);
                        sh_result.GetRow(i + 1).CreateCell(0).SetCellValue(dt.Rows[i]["CARRER_NAME"].ToString());
                        sh_result.GetRow(i + 1).CreateCell(1).SetCellValue(dt.Rows[i]["DRIVER_NAME"].ToString());
                        sh_result.GetRow(i + 1).CreateCell(2).SetCellValue(dt.Rows[i]["CLIENT_C"].ToString());
                        sh_result.GetRow(i + 1).CreateCell(3).SetCellValue(dt.Rows[i]["SHIPPING_NO"].ToString());
                        sh_result.GetRow(i + 1).CreateCell(4).SetCellValue(dt.Rows[i]["OMS_NO"].ToString());
                        sh_result.GetRow(i + 1).CreateCell(5).SetCellValue(dt.Rows[i]["CLIENT_ORDER_NO"].ToString());
                        sh_result.GetRow(i + 1).CreateCell(6).SetCellValue(dt.Rows[i]["CONSIGNEE_NAME"].ToString());
                        sh_result.GetRow(i + 1).CreateCell(7).SetCellValue(dt.Rows[i]["RECEIVE_PARTY_ADD"].ToString());
                        sh_result.GetRow(i + 1).CreateCell(8).SetCellValue(dt.Rows[i]["END_CITY"].ToString());
                        sh_result.GetRow(i + 1).CreateCell(9).SetCellValue(dt.Rows[i]["PROVINCE"].ToString());
                        sh_result.GetRow(i + 1).CreateCell(10).SetCellValue(dt.Rows[i]["ISSUE_QTY"].ToString());
                        sh_result.GetRow(i + 1).CreateCell(11).SetCellValue(dt.Rows[i]["ISSUE_WT"].ToString());
                        sh_result.GetRow(i + 1).CreateCell(12).SetCellValue(dt.Rows[i]["ISSUE_VOL"].ToString());
                        sh_result.GetRow(i + 1).CreateCell(13).SetCellValue(dt.Rows[i]["ISSUE_TIME"].ToString());
                        sh_result.GetRow(i + 1).CreateCell(14).SetCellValue(dt.Rows[i]["NOTICED_DELIVERY_DATE"].ToString());
                        sh_result.GetRow(i + 1).CreateCell(15).SetCellValue(dt.Rows[i]["PROPOSED_RETURN_DATE"].ToString());
                        sh_result.GetRow(i + 1).CreateCell(16).SetCellValue(dt.Rows[i]["RETURN_VOUCHER_TIME"].ToString());
                        sh_result.GetRow(i + 1).CreateCell(17).SetCellValue(dt.Rows[i]["IMEI"].ToString());
                        sh_result.GetRow(i + 1).CreateCell(18).SetCellValue(dt.Rows[i]["balance"].ToString());
                        sh_result.GetRow(i + 1).CreateCell(19).SetCellValue(dt.Rows[i]["balance"].ToString());
                        sh_result.GetRow(i + 1).CreateCell(20).SetCellValue(dt.Rows[i]["balance"].ToString());
                        sh_result.GetRow(i + 1).CreateCell(21).SetCellValue(dt.Rows[i]["balance"].ToString());
                    }


                    AutoColumnWidth(sh_result, 7);                    
                    string foldPath = Path.Combine(path, "回单信息" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls");
                    FileStream file = new FileStream(foldPath, FileMode.Create);
                    hssfworkbook.Write(file);
                    file.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
