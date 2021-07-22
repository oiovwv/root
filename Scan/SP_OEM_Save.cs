using Scan.Models;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

public class SP_OEM_Save : Form
{
	private string isPro = string.Empty;

	private int currentRowIndex;

	private bool isRescan = false;

	private DataTable data;

	private IContainer components = null;

	private Label label1;

	private Label label2;

	private Label label3;

	private Label label4;

	private TextBox txtOrderNo;

	private TextBox txtCurrentSku;

	private TextBox txtLastBarcode;

	private TextBox txtBarcode;

	private DataGridView dgv;

	private DataGridViewTextBoxColumn Material;

	private DataGridViewTextBoxColumn TotalQty;

	private DataGridViewTextBoxColumn QtyScanned;

	private DataGridViewTextBoxColumn OpenQty;

	private DataGridViewTextBoxColumn SalesUnit;

	private DataGridViewTextBoxColumn Category;

	private DataGridViewTextBoxColumn ScanUnit;

	public SP_OEM_Save()
	{
		InitializeComponent();
	}

	public SP_OEM_Save(string orderNo, DataTable dt, string env)
	{
		InitializeComponent();
		isPro = env;
		for (int i = 0; i < dgv.ColumnCount; i++)
		{
			if (dgv.Columns[i].DataPropertyName.Length <= 0)
			{
				dgv.Columns[i].DataPropertyName = dgv.Columns[i].Name;
			}
		}
		while (dgv.Rows.Count != 0)
		{
			dgv.Rows.RemoveAt(0);
		}
		txtOrderNo.Text = orderNo;
		dgv.DataSource = dt;
		data = dt.Copy();
		DataGridViewButtonColumn btn = new DataGridViewButtonColumn
		{
			Name = "Rescan",
			HeaderText = "Rescan",
			DefaultCellStyle = 
			{
				NullValue = "Rescan"
			}
		};
		dgv.Columns.Add(btn);
	}

	private void doubleClick(object sender, DataGridViewCellEventArgs e)
	{
		string columnName = dgv.CurrentCell.OwningColumn.Name;
		if (e.ColumnIndex == 1 && columnName == "Material")
		{
			txtBarcode.Text = "";
			currentRowIndex = e.RowIndex;
			string sku = dgv.Rows[e.RowIndex].Cells[columnName].Value.ToString();
			txtCurrentSku.Text = sku;
			txtBarcode.Focus();
		}
	}

	private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
	{
		if (dgv.Columns[e.ColumnIndex].Name == "Rescan")
		{
			isRescan = PostBase.ConfirmClose("确定重新扫描？将清除之前所有扫描记录并记录当前扫描Barcode~");
			if (isRescan)
			{
				txtBarcode.Text = "";
				currentRowIndex = e.RowIndex;
				string sku = dgv.Rows[e.RowIndex].Cells["Material"].Value.ToString();
				txtCurrentSku.Text = sku;
				txtBarcode.Focus();
			}
		}
	}

	private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
	{
		Keys keyCode = e.KeyCode;
		if (keyCode == Keys.Return)
		{
			Save(currentRowIndex);
		}
	}

	private void Save(int rowIndex)
	{
		if (!string.IsNullOrEmpty(txtBarcode.Text.Trim()))
		{
			OEMTraceability o = new OEMTraceability();
			o.Delivery = txtOrderNo.Text.Trim();
			o.Rescan = (isRescan ? "X" : "");
			string sku = o.Material = txtCurrentSku.Text.Trim();
			o.AntiFakeNo = txtBarcode.Text.Trim();
			o.Category = dgv.Rows[rowIndex].Cells["Category"].Value.ToString();
			o.ScanUnit = dgv.Rows[rowIndex].Cells["ScanUnit"].Value.ToString();
			o.QtyScanned = 0;
			o.BaseUnit = "";
			List<ExSaveMsg_NonOEMItem> msgs = new List<ExSaveMsg_NonOEMItem>();
			ExSaveMsg_NonOEMItem _item = new ExSaveMsg_NonOEMItem();
			msgs.Add(_item);
			o.ExSaveMsg_NonOEM = msgs;
			string json = PostBase.ModelToJson(o);
			string result = PostBase.GetApiResult(json, 5, isPro);
			if (result.IndexOf("出错") >= 0)
			{
				PostBase.MessageTips("请求出错，无数据返回~");
				return;
			}
			NonOEMDRootA res = PostBase.ToObject<NonOEMDRootA>(result);
			if (res.result.d.ExSaveMsg_NonOEM.results.Count > 0)
			{
				if (isRescan)
				{
					foreach (DataGridViewRow dr in (IEnumerable)dgv.Rows)
					{
						dr.Cells["QtyScanned"].Value = "0.000";
						dr.Cells["OpenQty"].Value = dr.Cells["TotalQty"].Value.ToString();
					}
					isRescan = false;
				}
				string type = res.result.d.ExSaveMsg_NonOEM.results[0].Type;
				string msg = res.result.d.ExSaveMsg_NonOEM.results[0].Message;
				if (type == "S")
				{
					double openQty2 = double.Parse(dgv.Rows[rowIndex].Cells["OpenQty"].Value.ToString());
					double scannedQty2 = double.Parse(dgv.Rows[rowIndex].Cells["QtyScanned"].Value.ToString());
					DataGridViewCell dataGridViewCell = dgv.Rows[rowIndex].Cells["QtyScanned"];
					double num = scannedQty2 += 1.0;
					dataGridViewCell.Value = num.ToString("f3");
					DataGridViewCell dataGridViewCell2 = dgv.Rows[rowIndex].Cells["OpenQty"];
					num = (openQty2 -= 1.0);
					dataGridViewCell2.Value = num.ToString("f3");
					txtLastBarcode.Text = txtBarcode.Text;
				}
				MessageBox.Show(msg);
			}
		}
		else
		{
			MessageBox.Show("请先扫描Barcode~");
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		label1 = new System.Windows.Forms.Label();
		label2 = new System.Windows.Forms.Label();
		label3 = new System.Windows.Forms.Label();
		label4 = new System.Windows.Forms.Label();
		txtOrderNo = new System.Windows.Forms.TextBox();
		txtCurrentSku = new System.Windows.Forms.TextBox();
		txtLastBarcode = new System.Windows.Forms.TextBox();
		txtBarcode = new System.Windows.Forms.TextBox();
		dgv = new System.Windows.Forms.DataGridView();
		Material = new System.Windows.Forms.DataGridViewTextBoxColumn();
		TotalQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
		QtyScanned = new System.Windows.Forms.DataGridViewTextBoxColumn();
		OpenQty = new System.Windows.Forms.DataGridViewTextBoxColumn();
		SalesUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
		Category = new System.Windows.Forms.DataGridViewTextBoxColumn();
		ScanUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
		((System.ComponentModel.ISupportInitialize)dgv).BeginInit();
		SuspendLayout();
		label1.AutoSize = true;
		label1.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		label1.Location = new System.Drawing.Point(40, 35);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(138, 20);
		label1.TabIndex = 0;
		label1.Text = "Outbound Number";
		label2.AutoSize = true;
		label2.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		label2.Location = new System.Drawing.Point(40, 80);
		label2.Name = "label2";
		label2.Size = new System.Drawing.Size(186, 20);
		label2.TabIndex = 1;
		label2.Text = "Current Scanning Materail";
		label3.AutoSize = true;
		label3.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		label3.Location = new System.Drawing.Point(40, 125);
		label3.Name = "label3";
		label3.Size = new System.Drawing.Size(156, 20);
		label3.TabIndex = 2;
		label3.Text = "Last Scanned Barcode";
		label4.AutoSize = true;
		label4.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		label4.Location = new System.Drawing.Point(40, 170);
		label4.Name = "label4";
		label4.Size = new System.Drawing.Size(64, 20);
		label4.TabIndex = 3;
		label4.Text = "Barcode";
		txtOrderNo.Location = new System.Drawing.Point(264, 35);
		txtOrderNo.Name = "txtOrderNo";
		txtOrderNo.Size = new System.Drawing.Size(222, 21);
		txtOrderNo.TabIndex = 4;
		txtCurrentSku.Location = new System.Drawing.Point(264, 80);
		txtCurrentSku.Name = "txtCurrentSku";
		txtCurrentSku.Size = new System.Drawing.Size(222, 21);
		txtCurrentSku.TabIndex = 5;
		txtLastBarcode.Location = new System.Drawing.Point(264, 125);
		txtLastBarcode.Name = "txtLastBarcode";
		txtLastBarcode.Size = new System.Drawing.Size(222, 21);
		txtLastBarcode.TabIndex = 6;
		txtBarcode.Location = new System.Drawing.Point(264, 170);
		txtBarcode.Name = "txtBarcode";
		txtBarcode.Size = new System.Drawing.Size(222, 21);
		txtBarcode.TabIndex = 7;
		txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(txtBarcode_KeyDown);
		dgv.AllowUserToAddRows = false;
		dgv.Anchor = (System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right);
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle8.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		dgv.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
		dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		dgv.Columns.AddRange(Material, TotalQty, QtyScanned, OpenQty, SalesUnit, Category, ScanUnit);
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle7.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle7.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle7.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle7.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle7.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle7.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
		dgv.DefaultCellStyle = dataGridViewCellStyle7;
		dgv.Location = new System.Drawing.Point(12, 220);
		dgv.Name = "dgv";
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle6.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		dgv.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
		dgv.RowTemplate.Height = 23;
		dgv.Size = new System.Drawing.Size(979, 218);
		dgv.TabIndex = 8;
		dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(dgv_CellContentClick);
		dgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(doubleClick);
		Material.HeaderText = "Material No.";
		Material.Name = "Material";
		Material.Width = 130;
		TotalQty.HeaderText = "Qty";
		TotalQty.Name = "TotalQty";
		TotalQty.Width = 80;
		QtyScanned.HeaderText = "Scanned Records";
		QtyScanned.Name = "QtyScanned";
		QtyScanned.Width = 160;
		OpenQty.HeaderText = "Records to be Scanned";
		OpenQty.Name = "OpenQty";
		OpenQty.Width = 190;
		SalesUnit.HeaderText = "SalesUnit";
		SalesUnit.Name = "SalesUnit";
		SalesUnit.Width = 80;
		Category.HeaderText = "Traceability Category";
		Category.Name = "Category";
		Category.Width = 180;
		ScanUnit.HeaderText = "ScanUnit";
		ScanUnit.Name = "ScanUnit";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(1003, 450);
		base.Controls.Add(dgv);
		base.Controls.Add(txtBarcode);
		base.Controls.Add(txtLastBarcode);
		base.Controls.Add(txtCurrentSku);
		base.Controls.Add(txtOrderNo);
		base.Controls.Add(label4);
		base.Controls.Add(label3);
		base.Controls.Add(label2);
		base.Controls.Add(label1);
		base.MaximizeBox = false;
		base.Name = "SP_OEM_Save";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		Text = "SP OEM Save";
		((System.ComponentModel.ISupportInitialize)dgv).EndInit();
		ResumeLayout(false);
		PerformLayout();
	}
}
