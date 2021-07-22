using Scan;
using Scan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

public class SP_AM_Traceability : Form
{
	private string isPro = string.Empty;

	private string PCode = string.Empty;

	private string type = string.Empty;

	private string msg = string.Empty;

	private List<ExOBQtyResultsItem> param = null;

	private string tip = "确定退出吗？将清除PCode记录~";

	private IContainer components = null;

	private Label label1;

	private TextBox txtClientOrderNo;

	private Button button1;

	private Label label2;

	public SP_AM_Traceability()
	{
		InitializeComponent();
	}

	public SP_AM_Traceability(string pcode, string env)
	{
		InitializeComponent();
		isPro = env;
		PCode = pcode;
		txtClientOrderNo.Focus();
	}

	private void button1_Click(object sender, EventArgs e)
	{
		ThisClosed();
	}

	private void txtClientOrderNo_KeyDown(object sender, KeyEventArgs e)
	{
		Keys keyCode = e.KeyCode;
		if (keyCode == Keys.Return)
		{
			ScanClientOrderNo();
		}
	}

	private void ScanClientOrderNo()
	{
		string clientOrderNo = txtClientOrderNo.Text.TrimStart('0');
		ObSetRootA model = new ObSetRootA();
		ObSetRootA res = PostBase.GetNeedScanInfo(clientOrderNo, 2, model, isPro);
		if (res.result.d.results.Count > 0)
		{
			type = res.result.d.results[0].ExOBMsg.results[0].Type.ToString();
			msg = res.result.d.results[0].ExOBMsg.results[0].Message.ToString();
			param = res.result.d.results[0].ExOBQty.results;
			if (type == "E")
			{
				if (msg.IndexOf("Scanning finished") > 0)
				{
					ShowScreen(param, clientOrderNo);
				}
				else
				{
					MessageBox.Show(msg);
				}
			}
			else
			{
				ShowScreen(param, clientOrderNo);
			}
		}
		else
		{
			MessageBox.Show("请求成功，但无数据返回~");
		}
	}

	public void ShowScreen(List<ExOBQtyResultsItem> list, string delivery)
	{
		SP_Save sP_Save = new SP_Save(PCode, delivery, list, isPro);
		sP_Save.Owner = this;
		sP_Save.ShowDialog();
	}

	private void ThisClosed()
	{
		if (PostBase.ConfirmClose(tip))
		{
			Form1 form = (Form1)base.Owner;
			form.PCode = string.Empty;
			Close();
		}
	}

	protected override void Dispose(bool disposing)
	{
		if (disposing && components != null)
		{
			components.Dispose();
		}
		base.Dispose(disposing);
	}

	private void InitializeComponent()
	{
		label1 = new System.Windows.Forms.Label();
		txtClientOrderNo = new System.Windows.Forms.TextBox();
		button1 = new System.Windows.Forms.Button();
		label2 = new System.Windows.Forms.Label();
		SuspendLayout();
		label1.AutoSize = true;
		label1.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		label1.Location = new System.Drawing.Point(12, 98);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(79, 20);
		label1.TabIndex = 0;
		label1.Text = "Outbound";
		txtClientOrderNo.Location = new System.Drawing.Point(16, 140);
		txtClientOrderNo.Name = "txtClientOrderNo";
		txtClientOrderNo.Size = new System.Drawing.Size(336, 21);
		txtClientOrderNo.TabIndex = 1;
		txtClientOrderNo.KeyDown += new System.Windows.Forms.KeyEventHandler(txtClientOrderNo_KeyDown);
		button1.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		button1.Location = new System.Drawing.Point(262, 261);
		button1.Name = "button1";
		button1.Size = new System.Drawing.Size(90, 35);
		button1.TabIndex = 2;
		button1.Text = "EXIT";
		button1.UseVisualStyleBackColor = true;
		button1.Click += new System.EventHandler(button1_Click);
		label2.AutoSize = true;
		label2.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		label2.Location = new System.Drawing.Point(12, 55);
		label2.Name = "label2";
		label2.Size = new System.Drawing.Size(242, 20);
		label2.TabIndex = 6;
		label2.Text = "SP Traceability Information Record";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(364, 341);
		base.Controls.Add(label2);
		base.Controls.Add(button1);
		base.Controls.Add(txtClientOrderNo);
		base.Controls.Add(label1);
		base.MaximizeBox = false;
		base.Name = "SP_AM_Traceability";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		Text = "SP AM Traceability";
		ResumeLayout(false);
		PerformLayout();
	}
}
