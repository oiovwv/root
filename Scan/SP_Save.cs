using Scan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

public class SP_Save : Form
{
	private string isPro = string.Empty;

	private string clientOrderNo = string.Empty;

	private List<ExOBQtyResultsItem> param = null;

	private int totalQty = 0;

	private int currentQty = 0;

	private IContainer components = null;

	private Label label1;

	private TextBox txtPcode;

	private Button button1;

	private Button button2;

	private Label label3;

	private Button button3;

	private Label qty;

	private Label label2;

	private Label label4;

	private Label sku;

	private Label currentSkuQty;

	public SP_Save()
	{
		InitializeComponent();
	}

	public SP_Save(string pcode, string delivery, List<ExOBQtyResultsItem> list, string env)
	{
		InitializeComponent();
		sku.Text = string.Empty;
		isPro = env;
		txtPcode.Text = pcode;
		clientOrderNo = delivery;
		param = list;
		GetTotalQty(list);
	}

	private void button1_Click(object sender, EventArgs e)
	{
		Save("Finish");
		Finished();
	}

	private void txtPcode_KeyDown(object sender, KeyEventArgs e)
	{
		Keys keyCode = e.KeyCode;
		if (keyCode == Keys.Return)
		{
			Save("Finish");
			Finished();
		}
	}

	private void button3_Click(object sender, EventArgs e)
	{
		if (PostBase.ConfirmClose("确定重新扫描？将清除之前扫描记录并记录当前扫描PCode~"))
		{
			Save("Rescan");
			Finished();
		}
	}

	public string SBCToDBC(string input)
	{
		char[] cc = input.ToCharArray();
		for (int i = 0; i < cc.Length; i++)
		{
			if (cc[i] == '\u3000')
			{
				cc[i] = ' ';
			}
			else if (cc[i] > '\uff00' && cc[i] < '｟')
			{
				cc[i] = (char)(cc[i] - 65248);
			}
		}
		return new string(cc);
	}

	private void Save(string way)
	{
		if (!string.IsNullOrEmpty(txtPcode.Text))
		{
			string code = txtPcode.Text.Trim().ToUpper();
			string sCode = SBCToDBC(code);
			txtPcode.Text = sCode;
			Traceability traceability = new Traceability();
			traceability.Rescan = ((way == "Rescan") ? "X" : string.Empty);
			traceability.Delivery = clientOrderNo;
			traceability.Pcode = sCode;
			List<IxOBQtyItem> items = new List<IxOBQtyItem>();
			IxOBQtyItem item = new IxOBQtyItem();
			string material = sCode.Substring(3, sCode.IndexOf("W") - 4);
			item.Delivery = clientOrderNo;
			item.Material = material;
			item.TotalBoxQty = totalQty;
			item.CurrentBoxQty = currentQty;
			items.Add(item);
			List<ExSaveMsgItem> msgs = new List<ExSaveMsgItem>();
			ExSaveMsgItem _item = new ExSaveMsgItem();
			msgs.Add(_item);
			traceability.ExSaveMsg = msgs;
			traceability.IxOBQty = items;
			string json = PostBase.ModelToJson(traceability);
			string result = PostBase.GetApiResult(json, 3, isPro);
			if (result.IndexOf("出错") >= 0)
			{
				PostBase.MessageTips("请求出错，无数据返回~");
				return;
			}
			SaveRootA res = PostBase.ToObject<SaveRootA>(result);
			if (res.result.d.ExSaveMsg.results.Count <= 0)
			{
				return;
			}
			string type = res.result.d.ExSaveMsg.results[0].Type.ToString();
			string msg = res.result.d.ExSaveMsg.results[0].Message.ToString();
			if (type == "S")
			{
				PostBase.playSuccessTips();
				if (way == "Rescan")
				{
					ObSetRootA model = new ObSetRootA();
					ObSetRootA dataApi2 = PostBase.GetNeedScanInfo(clientOrderNo, 2, model, isPro);
					if (dataApi2.result.d.results.Count > 0)
					{
						string t = dataApi2.result.d.results[0].ExOBMsg.results[0].Type.ToString();
						string i = dataApi2.result.d.results[0].ExOBMsg.results[0].Message.ToString();
						List<ExOBQtyResultsItem> p = dataApi2.result.d.results[0].ExOBQty.results;
						if (t == "E" && p.Count > 1)
						{
							PostBase.MessageTips(i);
						}
						GetTotalQty(p);
						GetCurrentSkuRemainingQty(p, material, way);
					}
					else
					{
						PostBase.MessageTips("获取OB信息：请求成功，但无数据返回~");
					}
				}
				else
				{
					currentQty--;
					qty.Text = currentQty + "/" + totalQty;
					GetCurrentSkuRemainingQty(param, material, way);
				}
				sku.Text = material;
				txtPcode.Text = string.Empty;
			}
			else
			{
				PostBase.MessageTips(msg);
			}
		}
		else
		{
			MessageBox.Show("请先扫描PCode~");
		}
	}

	private void GetTotalQty(List<ExOBQtyResultsItem> list)
	{
		currentQty = 0;
		totalQty = 0;
		foreach (ExOBQtyResultsItem o in list)
		{
			currentQty += o.CurrentBoxQty;
			totalQty += o.TotalBoxQty;
		}
		qty.Text = currentQty + "/" + totalQty;
	}

	private void GetCurrentSkuRemainingQty(List<ExOBQtyResultsItem> list, string currentSku, string way)
	{
		int remainingQty = 0;
		List<ExOBQtyResultsItem> currentSkuList = list.Where((ExOBQtyResultsItem x) => x.Material == currentSku).ToList();
		if (way == "Finish")
		{
			currentSkuList[0].CurrentBoxQty--;
		}
		foreach (ExOBQtyResultsItem item in currentSkuList)
		{
			remainingQty += item.CurrentBoxQty;
		}
		currentSkuQty.Text = remainingQty.ToString();
	}

	private void Finished()
	{
		if ((currentQty == 0) ? true : false)
		{
			MessageBox.Show(clientOrderNo + " Scanning finished");
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
		button1 = new System.Windows.Forms.Button();
		button2 = new System.Windows.Forms.Button();
		label3 = new System.Windows.Forms.Label();
		button3 = new System.Windows.Forms.Button();
		qty = new System.Windows.Forms.Label();
		label2 = new System.Windows.Forms.Label();
		label4 = new System.Windows.Forms.Label();
		sku = new System.Windows.Forms.Label();
		currentSkuQty = new System.Windows.Forms.Label();
		SuspendLayout();
		label1.AutoSize = true;
		label1.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		label1.Location = new System.Drawing.Point(12, 166);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(111, 20);
		label1.TabIndex = 0;
		label1.Text = "P.code/pall.No.";
		txtPcode.Location = new System.Drawing.Point(14, 201);
		txtPcode.Name = "txtPcode";
		txtPcode.Size = new System.Drawing.Size(336, 21);
		txtPcode.TabIndex = 1;
		txtPcode.KeyDown += new System.Windows.Forms.KeyEventHandler(txtPcode_KeyDown);
		button1.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		button1.Location = new System.Drawing.Point(136, 246);
		button1.Name = "button1";
		button1.Size = new System.Drawing.Size(90, 35);
		button1.TabIndex = 2;
		button1.Text = "Finish";
		button1.UseVisualStyleBackColor = true;
		button1.Click += new System.EventHandler(button1_Click);
		button2.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		button2.Location = new System.Drawing.Point(259, 246);
		button2.Name = "button2";
		button2.Size = new System.Drawing.Size(90, 35);
		button2.TabIndex = 3;
		button2.Text = "Back";
		button2.UseVisualStyleBackColor = true;
		label3.AutoSize = true;
		label3.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		label3.Location = new System.Drawing.Point(12, 76);
		label3.Name = "label3";
		label3.Size = new System.Drawing.Size(145, 20);
		label3.TabIndex = 5;
		label3.Text = "To be scanned total:";
		button3.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		button3.Location = new System.Drawing.Point(16, 246);
		button3.Name = "button3";
		button3.Size = new System.Drawing.Size(90, 35);
		button3.TabIndex = 10;
		button3.Text = "Rescan";
		button3.UseVisualStyleBackColor = true;
		button3.Click += new System.EventHandler(button3_Click);
		qty.AutoSize = true;
		qty.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		qty.Location = new System.Drawing.Point(255, 76);
		qty.Name = "qty";
		qty.Size = new System.Drawing.Size(0, 20);
		qty.TabIndex = 12;
		label2.AutoSize = true;
		label2.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		label2.Location = new System.Drawing.Point(12, 31);
		label2.Name = "label2";
		label2.Size = new System.Drawing.Size(98, 20);
		label2.TabIndex = 13;
		label2.Text = "Last scanned:";
		label4.AutoSize = true;
		label4.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		label4.Location = new System.Drawing.Point(12, 122);
		label4.Name = "label4";
		label4.Size = new System.Drawing.Size(140, 20);
		label4.TabIndex = 14;
		label4.Text = "To be scanned mat:";
		sku.AutoSize = true;
		sku.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		sku.Location = new System.Drawing.Point(116, 31);
		sku.Name = "sku";
		sku.Size = new System.Drawing.Size(0, 20);
		sku.TabIndex = 16;
		currentSkuQty.AutoSize = true;
		currentSkuQty.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		currentSkuQty.Location = new System.Drawing.Point(158, 122);
		currentSkuQty.Name = "currentSkuQty";
		currentSkuQty.Size = new System.Drawing.Size(0, 20);
		currentSkuQty.TabIndex = 18;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(364, 341);
		base.Controls.Add(currentSkuQty);
		base.Controls.Add(sku);
		base.Controls.Add(label4);
		base.Controls.Add(label2);
		base.Controls.Add(qty);
		base.Controls.Add(button3);
		base.Controls.Add(label3);
		base.Controls.Add(button2);
		base.Controls.Add(button1);
		base.Controls.Add(txtPcode);
		base.Controls.Add(label1);
		base.MaximizeBox = false;
		base.Name = "SP_Save";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		Text = "SP Save";
		ResumeLayout(false);
		PerformLayout();
	}
}
