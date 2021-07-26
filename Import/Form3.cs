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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            //dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(dataGridView1_CellEndEdit);
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                if (dataGridView1.Columns[i].DataPropertyName.Length <= 0)
                {
                    dataGridView1.Columns[i].DataPropertyName = dataGridView1.Columns[i].Name;
                }
            }
            while (dataGridView1.Rows.Count != 0)
            {
                dataGridView1.Rows.RemoveAt(0);
            }
        }

        private DataTable GetDt()
        {
            var dt = new DataTable();
            dt.Columns.Add("A", Type.GetType("System.String"));
            dt.Columns.Add("B", Type.GetType("System.String"));
            dt.Columns.Add("C", Type.GetType("System.String"));
            dt.Columns.Add("D", Type.GetType("System.String"));
            

            //dt.Rows.Add(new object[] { "OSRE080706600", "AA3094602DC", "540", "540", "540", "0", "0", "1" });
            //dt.Rows.Add(new object[] { "OSRE080689500", "AB3591101DC", "280", "280", "280", "0", "0", "2" });

            dt.Rows.Add(new object[] { "OSRE080706500", "A53775000F1", "1000", "" });
            dt.Rows.Add(new object[] { "OSRE080706500", "AB3591101DC", "280", "" });
            dt.Rows.Add(new object[] { "OSRE080706500", "A53775000F1", "1000", "" });
            dt.Rows.Add(new object[] { "OSRE080706500", "AB3591101DC", "280", "" });
            dt.Rows.Add(new object[] { "OSRE080706500", "A53775000F1", "1000", "" });
            dt.Rows.Add(new object[] { "OSRE080706500", "AB3591101DC", "280", "" });
            dt.Rows.Add(new object[] { "OSRE080706500", "A53775000F1", "1000", "" });
            dt.Rows.Add(new object[] { "OSRE080706500", "AB3591101DC", "280", "" });
            dt.Rows.Add(new object[] { "OSRE080706500", "A53775000F1", "1000", "" });
            dt.Rows.Add(new object[] { "OSRE080706500", "AB3591101DC", "280", "" });
            return dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetDt();
        }
    }
}
