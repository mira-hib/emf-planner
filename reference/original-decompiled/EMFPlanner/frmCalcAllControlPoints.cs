using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[DesignerGenerated]
public class frmCalcAllControlPoints : Form
{
	private IContainer components;

	[field: AccessedThroughProperty("DGV1")]
	internal virtual DataGridView DGV1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdCancel")]
	internal virtual Button cmdCancel
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdCancel_Click;
			Button button = field;
			if (button != null)
			{
				button.Click -= value2;
			}
			field = value;
			button = field;
			if (button != null)
			{
				button.Click += value2;
			}
		}
	}

	public frmCalcAllControlPoints()
	{
		Load += frmCalcAllControlPoints_Load;
		InitializeComponent();
	}

	[DebuggerNonUserCode]
	protected override void Dispose(bool disposing)
	{
		try
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
		}
		finally
		{
			base.Dispose(disposing);
		}
	}

	[System.Diagnostics.DebuggerStepThrough]
	private void InitializeComponent()
	{
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		this.DGV1 = new System.Windows.Forms.DataGridView();
		this.cmdCancel = new System.Windows.Forms.Button();
		((System.ComponentModel.ISupportInitialize)this.DGV1).BeginInit();
		base.SuspendLayout();
		this.DGV1.AllowUserToAddRows = false;
		this.DGV1.AllowUserToDeleteRows = false;
		this.DGV1.AllowUserToResizeColumns = false;
		this.DGV1.AllowUserToResizeRows = false;
		this.DGV1.BackgroundColor = System.Drawing.SystemColors.Menu;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.DGV1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.DGV1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGV1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.DGV1.Location = new System.Drawing.Point(0, 0);
		this.DGV1.Name = "DGV1";
		this.DGV1.ReadOnly = true;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.DGV1.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
		this.DGV1.RowHeadersVisible = false;
		this.DGV1.RowHeadersWidth = 55;
		this.DGV1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.DGV1.RowTemplate.Height = 18;
		this.DGV1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
		this.DGV1.Size = new System.Drawing.Size(1260, 742);
		this.DGV1.TabIndex = 7;
		this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cmdCancel.Location = new System.Drawing.Point(566, 5000);
		this.cmdCancel.Name = "cmdCancel";
		this.cmdCancel.Size = new System.Drawing.Size(58, 26);
		this.cmdCancel.TabIndex = 13;
		this.cmdCancel.Text = "cancel";
		this.cmdCancel.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.cmdCancel;
		base.ClientSize = new System.Drawing.Size(1260, 742);
		base.Controls.Add(this.cmdCancel);
		base.Controls.Add(this.DGV1);
		base.KeyPreview = true;
		base.MinimizeBox = false;
		base.Name = "frmCalcAllControlPoints";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Control point calculation results";
		((System.ComponentModel.ISupportInitialize)this.DGV1).EndInit();
		base.ResumeLayout(false);
	}

	private void frmCalcAllControlPoints_Load(object sender, EventArgs e)
	{
		Module1.SetDoubleBuffered(DGV1);
		FillDGV1();
	}

	public void FillDGV1()
	{
		DGV1.Rows.Clear();
		RepWordAlias.ResKB[] RKB = default;
		if (!RepWordAlias.CalcKT(ref RKB))
		{
			return;
		}
		Font font = new Font(DGV1.DefaultCellStyle.Font.Name, DGV1.DefaultCellStyle.Font.Size, FontStyle.Bold);
		DGV1.ColumnHeadersDefaultCellStyle.Font = font;
		checked
		{
			DGV1.RowCount = Public2.PRJ.TX.GetUpperBound(0) + 9;
			DGV1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			DGV1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			DGV1.ColumnCount = RKB.GetUpperBound(0) + 3;
			DGV1.Columns[0].Width = 30;
			DGV1.Columns[1].HeaderText = "Radiating Source / Control Point";
			DGV1.Columns[1].Frozen = true;
			DGV1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			DGV1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
			DGV1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
			DGV1.Rows[0].Frozen = true;
			DGV1.Rows[1].Frozen = true;
			DGV1.Rows[2].Frozen = true;
			DGV1.Rows[3].Frozen = true;
			DGV1.Rows[4].Frozen = true;
			DGV1.Rows[5].Frozen = true;
			DGV1[1, 0].Value = "Location";
			DGV1[1, 1].Value = "Height (m)";
			DGV1[1, 2].Value = "Elevation (m)";
			DGV1[1, 3].Value = "Measured E-field strength (V/m)";
			DGV1[1, 4].Value = "Calculated E-field strength (V/m)";
			DGV1[1, 5].Value = "Simultaneous exposure (%)";
			DGV1[1, 6].Value = "Total E-field strength (V/m)";
			DGV1[1, 7].Value = "Total eq. plane-wave power density (W/m²)";
			int upperBound = Public2.PRJ.TX.GetUpperBound(0);
			for (int i = 0; i <= upperBound; i++)
			{
				DGV1[0, i + 8].Value = Strings.Format(i + 1, "000").ToString();
				DGV1[1, i + 8].Value = "E-field strength for " + Public2.PRJ.TX[i].Name + " (V/m)";
			}
			int upperBound2 = RKB.GetUpperBound(0);
			for (int j = 0; j <= upperBound2; j++)
			{
				DGV1.Columns[j + 2].HeaderText = RKB[j].PointName;
				DGV1.Columns[j + 2].Width = 50;
				DGV1.Columns[j + 2].SortMode = DataGridViewColumnSortMode.NotSortable;
				DGV1[j + 2, 0].Value = RKB[j].PointLocation;
				DGV1[j + 2, 1].Value = Conversions.ToString(Math.Round(RKB[j].PointH, 1)) + " m";
				DGV1[j + 2, 2].Value = Strings.Format(RKB[j].PointElev, "0.0") + " m";
				DGV1[j + 2, 3].Value = Module1.SuperFormat(RKB[j].MeasuredE, 6);
				DGV1[j + 2, 4].Value = Module1.SuperFormat(RKB[j].CalculatedEsum, 6);
				DGV1[j + 2, 5].Value = Module1.SuperFormat(RKB[j].KB * 100f, 6);
				if (RKB[j].KB > 1f)
				{
					DGV1[j + 2, 5].Style.ForeColor = Color.Red;
					DGV1[j + 2, 5].Style.Font = font;
				}
				DGV1[j + 2, 6].Value = Module1.SuperFormat(RKB[j].TotalEsum, 6);
				DGV1[j + 2, 7].Value = Module1.SuperFormat(RKB[j].TotalPPEsum / 100f, 6);
				int upperBound3 = Public2.PRJ.TX.GetUpperBound(0);
				for (int k = 0; k <= upperBound3; k++)
				{
					DGV1[j + 2, k + 8].Value = Module1.SuperFormat(RKB[j].V[k], 6);
				}
			}
			int upperBound4 = Public2.PRJ.TX.GetUpperBound(0);
			for (int l = 0; l <= upperBound4; l++)
			{
				DGV1.Rows[l + 8].DefaultCellStyle.BackColor = Public2.PRJ.TX[l].Color;
			}
			DGV1.CurrentCell = DGV1[1, 1];
			DGV1.ClearSelection();
		}
	}

	private void cmdCancel_Click(object sender, EventArgs e)
	{
		Close();
	}
}
