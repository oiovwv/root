using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AMO_Import
{
    public partial class Form1 : Form
    {
        private static string client = "AMO";
        DataBaseAccessWB.DataBaseAccessSoapClient conn = new DataBaseAccessWB.DataBaseAccessSoapClient();
        public Form1()
        {
            InitializeComponent();
        }

        private void 粘贴新行ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataGirdViewCellPaste(dataGridView1);
        }

        private void DataGirdViewCellPaste(DataGridView dgv)
        {
            try
            {
                string PasteText = Clipboard.GetText();
                if (string.IsNullOrEmpty(PasteText))
                {
                    return;
                }
                string[] lines = PasteText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in lines)
                {
                    if (string.IsNullOrEmpty(line.Trim()))
                        continue;
                    string[] vals = line.Split('\t');
                    for (int i = 0; i < vals.Length; i++)
                    {
                        vals[i] = vals[i].Trim().Replace("'", "''");
                        if (i > 20)
                        {
                            vals[i] = "";
                        }
                    }
                    dgv.Rows.Add(vals);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("发生错误" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 粘贴新行ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DataGirdViewCellPaste(dataGridView2);
        }

        private void 粘贴新行ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DataGirdViewCellPaste(dataGridView3);
        }

        private void 粘贴新行ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            DataGirdViewCellPaste(dataGridView4);
        }

        private void 删除当天导入信息ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            //DeleteByTable();
        }

        public void DeleteByTable(string table, string client_c)
        {
            DialogResult a = MessageBox.Show("确定要删除当天导入信息吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (a == DialogResult.Yes)
            {
                //系统当前时间
                //string local = DateTime.Now.ToString();
                //2016-8-19 00:00:00
                DateTime local = DateTime.Now.Date;

                string sql = string.Format("delete from {0} where ADD_DATE>to_date('" + local + "','yyyy-MM-dd hh24:mi:ss') and CLIENT_C='{1}'", table, client_c);
                int count = conn.ExecuteNonQuery(sql, new string[] { });
                if (count > 0)
                {
                    MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        }
        public void ClearDatagridview(DataGridView dgv)
        {
            int j = dgv.Rows.Count;
            for (int i = 0; i < j; i++)
            {
                dgv.Rows.RemoveAt(0);
            }

        }

        private void 删除当天导入信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteByTable("SPDA_STORAGE_AMO", client);
        }

        private void 删除当天导入信息ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            DeleteByTable("SPDA_OUTBOUND_AMO", client);
        }

        private void 删除当天导入信息ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            DeleteByTable("SPDA_STOCK_AMO", client);
        }

        private void 清空列表ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearDatagridview(dataGridView1);
        }

        private void 清空列表ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ClearDatagridview(dataGridView2);
        }

        private void 清空列表ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ClearDatagridview(dataGridView3);
        }

        private void 清空列表ToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ClearDatagridview(dataGridView4);
        }

        private void 导入入库信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Update(dataGridView1, "IN", "ICheck");
        }
        private string FormartDate(string date)
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

        private void 导入出库信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Update(dataGridView2, "OUT", "OCheck");
        }

        private void 导入库存信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Update(dataGridView3, "STOCK", "SCheck");
        }

        private void 导入委托产品信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Update(dataGridView4, "PRODUCT", "PCheck");
        }


        private void Update(DataGridView dgv, string type, string checkColumn)
        {
            if (dgv.Rows.Count > 0)
            {
                for (int i = 0; i < dgv.Rows.Count; i++)
                {
                    DataGridViewRow dr = dgv.Rows[i];
                    try
                    {
                        string sql = string.Empty;
                        switch (type)
                        {
                            case "IN":
                                sql = string.Format("INSERT INTO SPDA_STORAGE_AMO (CLIENT_C,IDX,STORAGE_DATE,STORAGE_TYPE,PRODUCT_NO,PRODUCT_NAME,SPECIFICATIONS,COMPANY,PRODUCT_REGISTRATION,BATCH_NUMBER,TEMP3,BATCH_DATE,EXPIRY_DATE,QTY,UNIT,STORAGE_CONDITION,STORAGE_NUMBER,TEMP1,PRODUCT_STATUS,REMARK,OPUSER,ADD_DATE,TEMP2,TEMP4,TEMP5) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}',{12},'{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}',{21},'{22}','{23}','{24}')", client, "", dr.Cells["Column1"].Value.ToString(), dr.Cells["Column2"].Value.ToString(), dr.Cells["Column3"].Value.ToString(), dr.Cells["Column4"].Value.ToString(), dr.Cells["Column5"].Value.ToString(), dr.Cells["Column6"].Value.ToString(), dr.Cells["Column7"].Value.ToString(), dr.Cells["Column8"].Value.ToString(), dr.Cells["Column9"].Value.ToString(), dr.Cells["Column10"].Value.ToString(), dr.Cells["Column11"].Value.ToString(), dr.Cells["Column12"].Value.ToString(), dr.Cells["Column13"].Value.ToString(), dr.Cells["Column14"].Value.ToString(), dr.Cells["Column15"].Value.ToString(), dr.Cells["Column16"].Value.ToString(), dr.Cells["Column17"].Value.ToString(), dr.Cells["IRemark"].Value.ToString(), "FORM", "sysdate", "", "", "");
                                break;
                            case "OUT":
                                sql = string.Format("INSERT INTO SPDA_OUTBOUND_AMO (CLIENT_C,IDX,chukuriqi,outboun_type,order_no,PRODUCT_NO,PRODUCT_NAME,SPECIFICATIONS,COMPANY,PRODUCT_REGISTRATION,BATCH_NUMBER,TEMP3,outbound_condition,UNIT,QTY,receipt_party_no,cliant_name,address,contacts,phone,REMARK,OPUSER,ADD_DATE,TEMP1,TEMP2,TEMP4,TEMP5,TEMP6) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}',{22},'{23}','{24}','{25}','{26}','{27}')", client, "", dr.Cells["o1"].Value.ToString(), dr.Cells["o2"].Value.ToString(), dr.Cells["o3"].Value.ToString(), dr.Cells["o4"].Value.ToString(), dr.Cells["o5"].Value.ToString(), dr.Cells["o6"].Value.ToString(), dr.Cells["o7"].Value.ToString(), dr.Cells["o8"].Value.ToString(), dr.Cells["o9"].Value.ToString(), dr.Cells["o10"].Value.ToString(), dr.Cells["o11"].Value.ToString(), dr.Cells["o12"].Value.ToString(), dr.Cells["o13"].Value.ToString(), dr.Cells["o14"].Value.ToString(), dr.Cells["o15"].Value.ToString(), dr.Cells["o16"].Value.ToString(), dr.Cells["o17"].Value.ToString(), dr.Cells["o18"].Value.ToString(), dr.Cells["o19"].Value.ToString(), "FORM", "sysdate", "", "", "", "", "");
                                    break;
                            case "STOCK":
                                sql = string.Format("INSERT INTO  SPDA_STOCK_AMO (CLIENT_C,IDX,STORAGE_DATE,PRODUCT_NO,PRODUCT_NAME,SPECIFICATIONS,COMPANY,PRODUCT_REGISTRATION,BATCH_NUMBER,BATCH_DATE,EXPIRY_DATE,QTY,UNIT,STORAGE_NUMBER,TEMP1,STORAGE_CONDITION,PRODUCT_STATUS,REMARK,OPUSER,ADD_DATE,TEMP2,TEMP3,TEMP4,TEMP5) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}',{11},'{12}','{13}','{14}','{15}','{16}','{17}','{18}',{19},'{20}','{21}','{22}','{23}')", client, "", "", dr.Cells["s1"].Value.ToString(), dr.Cells["s2"].Value.ToString(), dr.Cells["s3"].Value.ToString(), dr.Cells["s4"].Value.ToString(), dr.Cells["s5"].Value.ToString(), dr.Cells["s6"].Value.ToString(), dr.Cells["s7"].Value.ToString().Replace("/", "-"), dr.Cells["s8"].Value.ToString().Replace("/", "-"), dr.Cells["s9"].Value.ToString(), dr.Cells["s10"].Value.ToString(), dr.Cells["s11"].Value.ToString(), dr.Cells["s12"].Value.ToString(), dr.Cells["s13"].Value.ToString(), dr.Cells["s14"].Value.ToString(), dr.Cells["s15"].Value.ToString(), "FORM", "sysdate", "", "", "", "");
                                break;
                            case "PRODUCT":
                                sql = string.Format("INSERT INTO DEPUTEPRODUCT_INFO_MANAGE (CLIENT_C,IDX,PRODUCT_NO,DEPUTE_PRODUCTNAME,SPECIFICATIONS,PRODUCT_REGISTRATION,APPROVAL_DATE,EXPIRY_DATE,COMPANY_QIYE,COMPANY_REGISTRATION,COMPANY,OUTBOUND_CONDITION,CANGCHUCANGKUTYPE,TEMP1,TEMP2,TEMP3,TEMP4,TEMP5) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}',{15},'{16}','{17}')", client, "", dr.Cells["p1"].Value.ToString(), dr.Cells["p2"].Value.ToString(), dr.Cells["p3"].Value.ToString(), dr.Cells["p4"].Value.ToString(), dr.Cells["p5"].Value.ToString(), dr.Cells["p6"].Value.ToString(), dr.Cells["p7"].Value.ToString(), dr.Cells["p8"].Value.ToString(), dr.Cells["p9"].Value.ToString(), dr.Cells["p10"].Value.ToString(), dr.Cells["p11"].Value.ToString(), dr.Cells["p12"].Value.ToString(), "FORM", "sysdate", "", "");
                                break;
                        }
                        conn.ExecuteNonQuery(sql, new string[] { });
                    }
                    catch (Exception ex)
                    {
                        dgv.Rows[i].Cells[checkColumn].Value = "导入时报错:" + ex.Message;
                        dgv.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        return;
                    }
                    dgv.Rows[i].Cells[checkColumn].Value = "导入成功";
                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.LawnGreen;
                }
            }
            else
            {
                MessageBox.Show("请选择要导入的信息！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void 清空库存信息ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult a = MessageBox.Show("确定要清空库存信息吗？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (a == DialogResult.Yes)
            {
                //系统当前时间
                //string local = DateTime.Now.ToString();
                //2016-8-19 00:00:00
                DateTime local = DateTime.Now.Date;

                string sql = string.Format("delete from {0} where CLIENT_C='{1}'", "SPDA_STOCK_AMO", client);
                int count = conn.ExecuteNonQuery(sql, new string[] { });
                if (count > 0)
                {
                    MessageBox.Show("删除成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("删除失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
