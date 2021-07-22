using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Scan
{
    public partial class Form1 : Form
    {
        public string PCode = string.Empty;

        public string isPro = string.Empty;
        public Form1()
        {
            InitializeComponent();
        }
        public void GetEnv()
        {
            if (rdb1.Checked)
            {
                isPro = "N";
            }
            else if (rdb2.Checked)
            {
                isPro = "Y";
            }
            if (string.IsNullOrEmpty(isPro))
            {
                MessageBox.Show("请先选择运行环境~", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetEnv();
            if (!string.IsNullOrEmpty(isPro))
            {
                SP_AM_Traceability s_Traceability = new SP_AM_Traceability(PCode, isPro);
                s_Traceability.Owner = this;
                s_Traceability.ShowDialog();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GetEnv();
            if (!string.IsNullOrEmpty(isPro))
            {
                PCode = string.Empty;
                SP_Blocking_Checking sP_Blocking_Checking = new SP_Blocking_Checking(isPro);
                sP_Blocking_Checking.Owner = this;
                sP_Blocking_Checking.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            GetEnv();
            if (!string.IsNullOrEmpty(isPro))
            {
                OEM_Traceability oEM_Traceability = new OEM_Traceability(isPro);
                oEM_Traceability.Owner = this;
                oEM_Traceability.ShowDialog();
            }
        }
    }
}
