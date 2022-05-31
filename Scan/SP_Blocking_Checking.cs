using Scan;
using Scan.Models;
using System;
using System.ComponentModel;
using System.Windows.Forms;

public class SP_Blocking_Checking : Form
{
	private string isPro = string.Empty;

	private IContainer components = null;

	private Label label1;

	private TextBox txtPcode;

	private Label label2;

	private TextBox textBox1;

	private Button btnExit;

	public SP_Blocking_Checking(string env)
	{
		isPro = env;
		InitializeComponent();
		txtPcode.Focus();
	}

	private void btnExit_Click(object sender, EventArgs e)
	{
		string tips = "确定退出吗？";
		if (PostBase.ConfirmClose(tips))
		{
			txtPcode.Text = string.Empty;
			Close();
		}
	}

	private void txtPcode_KeyDown(object sender, KeyEventArgs e)
	{
		Keys keyCode = e.KeyCode;
		if (keyCode == Keys.Return)
		{
			SPChecking();
		}
	}

	private void SPChecking()
	{
		string Pcode = txtPcode.Text.Trim();
		string result = string.Empty;
		if (!string.IsNullOrEmpty(Pcode))
		{
			//result = PostBase.GetApiResult(Pcode, "P", 1, isPro);
			result = PostBase.GetApiResultNew(Pcode, 1, isPro);
			if (!string.IsNullOrEmpty(result) && result.IndexOf("异常") < 0)
			{
				PcodeRootA res = PostBase.ToObject<PcodeRootA>(result);
				if (res.result == null)
				{
					MessageBox.Show("请求数据为null,请稍后再试");
				}
				else
				{
					if (res.result.d.results.Count > 0)
					{
						string type = res.result.d.results[0].ExPcodeMsg.results[0].Type.ToString();
						string msg = res.result.d.results[0].ExPcodeMsg.results[0].Message.ToString();
						if (type == "S" && msg == "OK")
						{
							Form1 form = (Form1)base.Owner;
							form.PCode = Pcode;
							MessageBox.Show("It's OK~");
							Close();
						}
						else
						{
							MessageBox.Show(msg);
						}
					}
					else
					{
						MessageBox.Show("请求成功，但无数据返回~");
					}
				}
			}
			else
			{
				MessageBox.Show("请求出现异常，稍后再试~");
			}
		}
		else
		{
			MessageBox.Show("请先扫描PCode~");
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
		txtPcode = new System.Windows.Forms.TextBox();
		label2 = new System.Windows.Forms.Label();
		textBox1 = new System.Windows.Forms.TextBox();
		btnExit = new System.Windows.Forms.Button();
		SuspendLayout();
		label1.AutoSize = true;
		label1.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		label1.Location = new System.Drawing.Point(12, 61);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(123, 20);
		label1.TabIndex = 0;
		label1.Text = "Production Code";
		txtPcode.Location = new System.Drawing.Point(12, 97);
		txtPcode.Name = "txtPcode";
		txtPcode.Size = new System.Drawing.Size(340, 21);
		txtPcode.TabIndex = 1;
		txtPcode.KeyDown += new System.Windows.Forms.KeyEventHandler(txtPcode_KeyDown);
		label2.AutoSize = true;
		label2.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		label2.Location = new System.Drawing.Point(14, 143);
		label2.Name = "label2";
		label2.Size = new System.Drawing.Size(59, 20);
		label2.TabIndex = 2;
		label2.Text = "Ship-to";
		textBox1.Location = new System.Drawing.Point(80, 143);
		textBox1.Name = "textBox1";
		textBox1.Size = new System.Drawing.Size(128, 21);
		textBox1.TabIndex = 3;
		btnExit.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		btnExit.Location = new System.Drawing.Point(262, 277);
		btnExit.Name = "btnExit";
		btnExit.Size = new System.Drawing.Size(90, 35);
		btnExit.TabIndex = 4;
		btnExit.Text = "EXIT";
		btnExit.UseVisualStyleBackColor = true;
		btnExit.Click += new System.EventHandler(btnExit_Click);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(364, 341);
		base.Controls.Add(btnExit);
		base.Controls.Add(textBox1);
		base.Controls.Add(label2);
		base.Controls.Add(txtPcode);
		base.Controls.Add(label1);
		base.MaximizeBox = false;
		base.Name = "SP_Blocking_Checking";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		Text = "SP Blocking Checking";
		ResumeLayout(false);
		PerformLayout();
	}
}
