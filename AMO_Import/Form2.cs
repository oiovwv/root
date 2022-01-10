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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var client= this.comboBox1.SelectedItem == null ? string.Empty : this.comboBox1.SelectedItem.ToString();
            
            if (string.IsNullOrEmpty(client))
            {
                MessageBox.Show("请先选择客户代码~", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                Form1 form = (Form1)base.Owner;
                form.client = client;
                Close();
            }
        }
    }
}
