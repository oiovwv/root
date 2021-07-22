using Scan.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

public class OEM_Traceability : Form
{
	private string isPro = string.Empty;

	private IContainer components = null;

	private Label label1;

	private TextBox txtOrderNo;

	public OEM_Traceability(string env)
	{
		InitializeComponent();
		isPro = env;
	}

	private void txtOrderNo_KeyDown(object sender, KeyEventArgs e)
	{
		Keys keyCode = e.KeyCode;
		if (keyCode == Keys.Return)
		{
			CheckClientOrderNo();
		}
	}

	private void CheckClientOrderNo()
	{
		string clientOrderNo = txtOrderNo.Text.TrimStart('0');
		OEMRootA model = new OEMRootA();
		OEMRootA res = PostBase.GetNeedScanInfo(clientOrderNo, 4, model, isPro);
		if (res.result.d.results.Count > 0)
		{
			string type = res.result.d.results[0].ExOBMsg_NonOEM.results[0].Type.ToString();
			string msg = res.result.d.results[0].ExOBMsg_NonOEM.results[0].Message.ToString();
			List<OEMResultsItem> param = res.result.d.results[0].ExOBQty_NonOEM.results;
			if (type == "E")
			{
				MessageBox.Show(msg);
				return;
			}
			DataTable dt = CreateDataTable(param);
			SP_OEM_Save sP_OEM_Save = new SP_OEM_Save(clientOrderNo, dt, isPro);
			sP_OEM_Save.Owner = this;
			sP_OEM_Save.ShowDialog();
		}
		else
		{
			MessageBox.Show("请求成功，但无数据返回~");
		}
	}

	private DataTable CreateDataTable(List<OEMResultsItem> list)
	{
		DataTable dt = new DataTable();
		dt.Columns.Add("Material", Type.GetType("System.String"));
		dt.Columns.Add("TotalQty", Type.GetType("System.String"));
		dt.Columns.Add("QtyScanned", Type.GetType("System.String"));
		dt.Columns.Add("OpenQty", Type.GetType("System.String"));
		dt.Columns.Add("SalesUnit", Type.GetType("System.String"));
		dt.Columns.Add("Category", Type.GetType("System.String"));
		dt.Columns.Add("ScanUnit", Type.GetType("System.String"));
		foreach (OEMResultsItem o in list)
		{
			DataRow dr = dt.NewRow();
			dr["Material"] = o.Material;
			dr["TotalQty"] = o.TotalQty;
			dr["QtyScanned"] = o.QtyScanned;
			dr["OpenQty"] = o.OpenQty;
			dr["SalesUnit"] = o.SalesUnit;
			dr["Category"] = o.Category;
			dr["ScanUnit"] = o.ScanUnit;
			dt.Rows.Add(dr);
		}
		return dt;
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
		txtOrderNo = new System.Windows.Forms.TextBox();
		SuspendLayout();
		label1.AutoSize = true;
		label1.Font = new System.Drawing.Font("微软雅黑", 10.5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 134);
		label1.Location = new System.Drawing.Point(16, 139);
		label1.Name = "label1";
		label1.Size = new System.Drawing.Size(138, 20);
		label1.TabIndex = 0;
		label1.Text = "Outbound Number";
		txtOrderNo.Location = new System.Drawing.Point(164, 139);
		txtOrderNo.Name = "txtOrderNo";
		txtOrderNo.Size = new System.Drawing.Size(166, 21);
		txtOrderNo.TabIndex = 1;
		txtOrderNo.KeyDown += new System.Windows.Forms.KeyEventHandler(txtOrderNo_KeyDown);
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 12f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.ClientSize = new System.Drawing.Size(364, 341);
		base.Controls.Add(txtOrderNo);
		base.Controls.Add(label1);
		base.MaximizeBox = false;
		base.Name = "OEM_Traceability";
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		Text = "Non-AM OEM Traceability";
		ResumeLayout(false);
		PerformLayout();
	}
}
