using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using EMFPlanner.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[DesignerGenerated]
public class frmCalcOneControlPoint : Form
{
	private IContainer components;

	private float AnalisisHmin;

	private float AnalisisHmax;

	private float AnalisisStep;

	private bool GraphType;

	private float PointElevation;

	private short IndExclBLD;

	private short IndExclWall;

	private bool InBuilding;

	private float BldHeight;

	private float[] GR;

	private float[] GrEsum;

	[field: AccessedThroughProperty("SplitContainer1")]
	internal virtual SplitContainer SplitContainer1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("GroupBox3")]
	internal virtual GroupBox GroupBox3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("GroupBox1")]
	internal virtual GroupBox GroupBox1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("GroupBox2")]
	internal virtual GroupBox GroupBox2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdCANCEL")]
	internal virtual Button cmdCANCEL
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("DGV1")]
	internal virtual DataGridView DGV1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label5")]
	internal virtual Label Label5
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdRefresh")]
	internal virtual Button cmdRefresh
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdRefresh_Click;
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

	[field: AccessedThroughProperty("txtHmin")]
	internal virtual TextBox txtHmin
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			CancelEventHandler value2 = txtHmin_Validating;
			TextBox textBox = field;
			if (textBox != null)
			{
				textBox.Validating -= value2;
			}
			field = value;
			textBox = field;
			if (textBox != null)
			{
				textBox.Validating += value2;
			}
		}
	}

	[field: AccessedThroughProperty("txtStep")]
	internal virtual TextBox txtStep
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			CancelEventHandler value2 = txtHmax_txtStep;
			TextBox textBox = field;
			if (textBox != null)
			{
				textBox.Validating -= value2;
			}
			field = value;
			textBox = field;
			if (textBox != null)
			{
				textBox.Validating += value2;
			}
		}
	}

	[field: AccessedThroughProperty("Label6")]
	internal virtual Label Label6
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label2")]
	internal virtual Label Label2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label4")]
	internal virtual Label Label4
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label1")]
	internal virtual Label Label1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtHmax")]
	internal virtual TextBox txtHmax
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			CancelEventHandler value2 = txtHmax_Validating;
			TextBox textBox = field;
			if (textBox != null)
			{
				textBox.Validating -= value2;
			}
			field = value;
			textBox = field;
			if (textBox != null)
			{
				textBox.Validating += value2;
			}
		}
	}

	[field: AccessedThroughProperty("Label3")]
	internal virtual Label Label3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PB")]
	internal virtual PictureBox PB
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			PaintEventHandler value2 = PB_Paint;
			EventHandler value3 = PB_Resize;
			PictureBox pictureBox = field;
			if (pictureBox != null)
			{
				pictureBox.Paint -= value2;
				pictureBox.Resize -= value3;
			}
			field = value;
			pictureBox = field;
			if (pictureBox != null)
			{
				pictureBox.Paint += value2;
				pictureBox.Resize += value3;
			}
		}
	}

	[field: AccessedThroughProperty("lblElevation")]
	internal virtual Label lblElevation
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("rbTotalE")]
	internal virtual RadioButton rbTotalE
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = rbSimExposure_MouseClick;
			RadioButton radioButton = field;
			if (radioButton != null)
			{
				radioButton.MouseClick -= value2;
			}
			field = value;
			radioButton = field;
			if (radioButton != null)
			{
				radioButton.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("rbSimExposure")]
	internal virtual RadioButton rbSimExposure
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = rbSimExposure_MouseClick;
			RadioButton radioButton = field;
			if (radioButton != null)
			{
				radioButton.MouseClick -= value2;
			}
			field = value;
			radioButton = field;
			if (radioButton != null)
			{
				radioButton.MouseClick += value2;
			}
		}
	}

	public frmCalcOneControlPoint()
	{
		Load += frmCalcOneControlPoint_Load;
		AnalisisHmin = 0f;
		AnalisisHmax = 10f;
		AnalisisStep = 0.5f;
		GraphType = false;
		InBuilding = false;
		BldHeight = 0f;
		GR = new float[501];
		GrEsum = new float[501];
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
		this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
		this.GroupBox3 = new System.Windows.Forms.GroupBox();
		this.rbTotalE = new System.Windows.Forms.RadioButton();
		this.rbSimExposure = new System.Windows.Forms.RadioButton();
		this.PB = new System.Windows.Forms.PictureBox();
		this.cmdCANCEL = new System.Windows.Forms.Button();
		this.GroupBox1 = new System.Windows.Forms.GroupBox();
		this.lblElevation = new System.Windows.Forms.Label();
		this.Label5 = new System.Windows.Forms.Label();
		this.cmdRefresh = new System.Windows.Forms.Button();
		this.txtHmin = new System.Windows.Forms.TextBox();
		this.txtStep = new System.Windows.Forms.TextBox();
		this.Label6 = new System.Windows.Forms.Label();
		this.Label2 = new System.Windows.Forms.Label();
		this.Label4 = new System.Windows.Forms.Label();
		this.Label1 = new System.Windows.Forms.Label();
		this.txtHmax = new System.Windows.Forms.TextBox();
		this.Label3 = new System.Windows.Forms.Label();
		this.GroupBox2 = new System.Windows.Forms.GroupBox();
		this.DGV1 = new System.Windows.Forms.DataGridView();
		((System.ComponentModel.ISupportInitialize)this.SplitContainer1).BeginInit();
		this.SplitContainer1.Panel1.SuspendLayout();
		this.SplitContainer1.Panel2.SuspendLayout();
		this.SplitContainer1.SuspendLayout();
		this.GroupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.PB).BeginInit();
		this.GroupBox1.SuspendLayout();
		this.GroupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.DGV1).BeginInit();
		base.SuspendLayout();
		this.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
		this.SplitContainer1.Location = new System.Drawing.Point(0, 0);
		this.SplitContainer1.Name = "SplitContainer1";
		this.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.SplitContainer1.Panel1.Controls.Add(this.GroupBox3);
		this.SplitContainer1.Panel1.Controls.Add(this.GroupBox1);
		this.SplitContainer1.Panel2.Controls.Add(this.GroupBox2);
		this.SplitContainer1.Size = new System.Drawing.Size(1208, 745);
		this.SplitContainer1.SplitterDistance = 229;
		this.SplitContainer1.TabIndex = 0;
		this.GroupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GroupBox3.Controls.Add(this.rbTotalE);
		this.GroupBox3.Controls.Add(this.rbSimExposure);
		this.GroupBox3.Controls.Add(this.PB);
		this.GroupBox3.Controls.Add(this.cmdCANCEL);
		this.GroupBox3.Location = new System.Drawing.Point(225, 3);
		this.GroupBox3.Name = "GroupBox3";
		this.GroupBox3.Size = new System.Drawing.Size(980, 223);
		this.GroupBox3.TabIndex = 1;
		this.GroupBox3.TabStop = false;
		this.GroupBox3.Text = "Graph of simultaneous exposure distribution by height";
		this.rbTotalE.AutoSize = true;
		this.rbTotalE.Location = new System.Drawing.Point(178, 18);
		this.rbTotalE.Name = "rbTotalE";
		this.rbTotalE.Size = new System.Drawing.Size(151, 17);
		this.rbTotalE.TabIndex = 14;
		this.rbTotalE.Text = "Total E-field strength (V/m)";
		this.rbTotalE.UseVisualStyleBackColor = true;
		this.rbSimExposure.AutoSize = true;
		this.rbSimExposure.Checked = true;
		this.rbSimExposure.Location = new System.Drawing.Point(13, 18);
		this.rbSimExposure.Name = "rbSimExposure";
		this.rbSimExposure.Size = new System.Drawing.Size(151, 17);
		this.rbSimExposure.TabIndex = 13;
		this.rbSimExposure.TabStop = true;
		this.rbSimExposure.Text = "Simultaneous exposure (%)";
		this.rbSimExposure.UseVisualStyleBackColor = true;
		this.PB.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.PB.BackColor = System.Drawing.SystemColors.Window;
		this.PB.Location = new System.Drawing.Point(3, 41);
		this.PB.Name = "PB";
		this.PB.Size = new System.Drawing.Size(974, 176);
		this.PB.TabIndex = 12;
		this.PB.TabStop = false;
		this.cmdCANCEL.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdCANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cmdCANCEL.Location = new System.Drawing.Point(4883, 159);
		this.cmdCANCEL.Name = "cmdCANCEL";
		this.cmdCANCEL.Size = new System.Drawing.Size(88, 26);
		this.cmdCANCEL.TabIndex = 11;
		this.cmdCANCEL.TabStop = false;
		this.cmdCANCEL.Text = "Отмена";
		this.cmdCANCEL.UseVisualStyleBackColor = true;
		this.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.GroupBox1.Controls.Add(this.lblElevation);
		this.GroupBox1.Controls.Add(this.Label5);
		this.GroupBox1.Controls.Add(this.cmdRefresh);
		this.GroupBox1.Controls.Add(this.txtHmin);
		this.GroupBox1.Controls.Add(this.txtStep);
		this.GroupBox1.Controls.Add(this.Label6);
		this.GroupBox1.Controls.Add(this.Label2);
		this.GroupBox1.Controls.Add(this.Label4);
		this.GroupBox1.Controls.Add(this.Label1);
		this.GroupBox1.Controls.Add(this.txtHmax);
		this.GroupBox1.Controls.Add(this.Label3);
		this.GroupBox1.Location = new System.Drawing.Point(3, 3);
		this.GroupBox1.Name = "GroupBox1";
		this.GroupBox1.Size = new System.Drawing.Size(216, 223);
		this.GroupBox1.TabIndex = 0;
		this.GroupBox1.TabStop = false;
		this.GroupBox1.Text = "Hights";
		this.lblElevation.AutoSize = true;
		this.lblElevation.Location = new System.Drawing.Point(18, 26);
		this.lblElevation.Name = "lblElevation";
		this.lblElevation.Size = new System.Drawing.Size(127, 13);
		this.lblElevation.TabIndex = 21;
		this.lblElevation.Text = "Elevation at control point:";
		this.Label5.AutoSize = true;
		this.Label5.Location = new System.Drawing.Point(144, 110);
		this.Label5.Name = "Label5";
		this.Label5.Size = new System.Drawing.Size(15, 13);
		this.Label5.TabIndex = 20;
		this.Label5.Text = "m";
		this.cmdRefresh.Location = new System.Drawing.Point(80, 141);
		this.cmdRefresh.Name = "cmdRefresh";
		this.cmdRefresh.Size = new System.Drawing.Size(79, 26);
		this.cmdRefresh.TabIndex = 12;
		this.cmdRefresh.Text = "Refresh";
		this.cmdRefresh.UseVisualStyleBackColor = true;
		this.txtHmin.Location = new System.Drawing.Point(94, 55);
		this.txtHmin.Name = "txtHmin";
		this.txtHmin.Size = new System.Drawing.Size(44, 20);
		this.txtHmin.TabIndex = 15;
		this.txtHmin.Text = "0";
		this.txtHmin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtStep.Location = new System.Drawing.Point(94, 107);
		this.txtStep.Name = "txtStep";
		this.txtStep.Size = new System.Drawing.Size(44, 20);
		this.txtStep.TabIndex = 19;
		this.txtStep.Text = "1";
		this.txtStep.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.Label6.AutoSize = true;
		this.Label6.Location = new System.Drawing.Point(59, 110);
		this.Label6.Name = "Label6";
		this.Label6.Size = new System.Drawing.Size(29, 13);
		this.Label6.TabIndex = 18;
		this.Label6.Text = "Step";
		this.Label2.AutoSize = true;
		this.Label2.Location = new System.Drawing.Point(57, 58);
		this.Label2.Name = "Label2";
		this.Label2.Size = new System.Drawing.Size(31, 13);
		this.Label2.TabIndex = 13;
		this.Label2.Text = "Hmin";
		this.Label4.AutoSize = true;
		this.Label4.Location = new System.Drawing.Point(144, 58);
		this.Label4.Name = "Label4";
		this.Label4.Size = new System.Drawing.Size(15, 13);
		this.Label4.TabIndex = 17;
		this.Label4.Text = "m";
		this.Label1.AutoSize = true;
		this.Label1.Location = new System.Drawing.Point(54, 84);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(34, 13);
		this.Label1.TabIndex = 11;
		this.Label1.Text = "Hmax";
		this.txtHmax.Location = new System.Drawing.Point(94, 81);
		this.txtHmax.Name = "txtHmax";
		this.txtHmax.Size = new System.Drawing.Size(44, 20);
		this.txtHmax.TabIndex = 14;
		this.txtHmax.Text = "30";
		this.txtHmax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.Label3.AutoSize = true;
		this.Label3.Location = new System.Drawing.Point(144, 84);
		this.Label3.Name = "Label3";
		this.Label3.Size = new System.Drawing.Size(15, 13);
		this.Label3.TabIndex = 16;
		this.Label3.Text = "m";
		this.GroupBox2.Controls.Add(this.DGV1);
		this.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GroupBox2.Location = new System.Drawing.Point(0, 0);
		this.GroupBox2.Name = "GroupBox2";
		this.GroupBox2.Size = new System.Drawing.Size(1208, 512);
		this.GroupBox2.TabIndex = 1;
		this.GroupBox2.TabStop = false;
		this.GroupBox2.Text = "Table of distribution of simultaneous exposure and components by heights";
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
		this.DGV1.Location = new System.Drawing.Point(3, 16);
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
		this.DGV1.Size = new System.Drawing.Size(1202, 493);
		this.DGV1.TabIndex = 7;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.cmdCANCEL;
		base.ClientSize = new System.Drawing.Size(1208, 745);
		base.Controls.Add(this.SplitContainer1);
		base.KeyPreview = true;
		base.MinimizeBox = false;
		base.Name = "frmCalcOneControlPoint";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Control point calculation results";
		this.SplitContainer1.Panel1.ResumeLayout(false);
		this.SplitContainer1.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.SplitContainer1).EndInit();
		this.SplitContainer1.ResumeLayout(false);
		this.GroupBox3.ResumeLayout(false);
		this.GroupBox3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.PB).EndInit();
		this.GroupBox1.ResumeLayout(false);
		this.GroupBox1.PerformLayout();
		this.GroupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.DGV1).EndInit();
		base.ResumeLayout(false);
	}

	private void frmCalcOneControlPoint_Load(object sender, EventArgs e)
	{
		Module1.SetDoubleBuffered(DGV1);
		txtHmin.Text = Conversions.ToString(AnalisisHmin);
		txtHmax.Text = Conversions.ToString(AnalisisHmax);
		txtStep.Text = Conversions.ToString(AnalisisStep);
		if (GraphType)
		{
			rbSimExposure.Checked = false;
			rbTotalE.Checked = true;
		}
		else
		{
			rbSimExposure.Checked = true;
			rbTotalE.Checked = false;
		}
		PointElevation = Triangul.FindHeightTriang(Public2.PRJ.CPG[MyProject.Forms.Form1.UC_ControlPoints1.SelRowDGV1].CP[MyProject.Forms.Form1.UC_ControlPoints1.SelRowDGV2].CoordWM.Xwm, Public2.PRJ.CPG[MyProject.Forms.Form1.UC_ControlPoints1.SelRowDGV1].CP[MyProject.Forms.Form1.UC_ControlPoints1.SelRowDGV2].CoordWM.Ywm);
		IndExclBLD = -1;
		IndExclWall = -1;
		checked
		{
			if (Operators.CompareString(Public2.PRJ.CPG[MyProject.Forms.Form1.UC_ControlPoints1.SelRowDGV1].CP[MyProject.Forms.Form1.UC_ControlPoints1.SelRowDGV2].Location, "Building", TextCompare: false) == 0)
			{
				if (Information.IsNothing(Public2.PRJ.BLD))
				{
					Interaction.MsgBox(Localization.LocMSG(71) + ". " + Localization.LocMSG(72) + " " + Conversions.ToString(MyProject.Forms.Form1.UC_ControlPoints1.SelRowDGV2 + 1) + " " + Localization.LocMSG(73));
					Close();
					return;
				}
				bool flag = false;
				int upperBound = Public2.PRJ.BLD.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					if (Module1.IsPointInsidePolygonF(Public2.PRJ.CPG[MyProject.Forms.Form1.UC_ControlPoints1.SelRowDGV1].CP[MyProject.Forms.Form1.UC_ControlPoints1.SelRowDGV2].CoordWM, Public2.PRJ.BLD[i].CoordWM))
					{
						PointElevation = Public2.PRJ.BLD[i].Fundation;
						IndExclBLD = (short)i;
						BldHeight = Public2.PRJ.BLD[i].H;
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					Interaction.MsgBox(Localization.LocMSG(71) + ". " + Localization.LocMSG(72) + " " + Conversions.ToString(MyProject.Forms.Form1.UC_ControlPoints1.SelRowDGV2 + 1) + " " + Localization.LocMSG(74));
					Close();
					return;
				}
				InBuilding = true;
			}
			else if (Operators.CompareString(Public2.PRJ.CPG[MyProject.Forms.Form1.UC_ControlPoints1.SelRowDGV1].CP[MyProject.Forms.Form1.UC_ControlPoints1.SelRowDGV2].Location, "Window", TextCompare: false) == 0)
			{
				int upperBound2 = Public2.PRJ.BLD.GetUpperBound(0);
				for (int j = 0; j <= upperBound2; j++)
				{
					if (!Module1.IsPointInsidePolygonF(Public2.PRJ.CPG[MyProject.Forms.Form1.UC_ControlPoints1.SelRowDGV1].CP[MyProject.Forms.Form1.UC_ControlPoints1.SelRowDGV2].CoordWM, Public2.PRJ.BLD[j].CoordWM))
					{
						continue;
					}
					PointElevation = Public2.PRJ.BLD[j].Fundation;
					IndExclBLD = (short)j;
					double num = 99999.0;
					IndExclWall = -1;
					int num2 = Public2.PRJ.BLD[j].CoordWM.GetUpperBound(0) - 1;
					for (int k = 0; k <= num2; k++)
					{
						double num3 = Module1.DistPointToLine(Public2.PRJ.CPG[MyProject.Forms.Form1.UC_ControlPoints1.SelRowDGV1].CP[MyProject.Forms.Form1.UC_ControlPoints1.SelRowDGV2].CoordWM.Xwm, Public2.PRJ.CPG[MyProject.Forms.Form1.UC_ControlPoints1.SelRowDGV1].CP[MyProject.Forms.Form1.UC_ControlPoints1.SelRowDGV2].CoordWM.Ywm, Public2.PRJ.BLD[j].CoordWM[k].Xwm, Public2.PRJ.BLD[j].CoordWM[k].Ywm, Public2.PRJ.BLD[j].CoordWM[k + 1].Xwm, Public2.PRJ.BLD[j].CoordWM[k + 1].Ywm);
						if (num3 < num)
						{
							num = num3;
							IndExclWall = (short)k;
						}
					}
					break;
				}
			}
			else if (!Information.IsNothing(Public2.PRJ.BLD))
			{
				int upperBound3 = Public2.PRJ.BLD.GetUpperBound(0);
				for (int l = 0; l <= upperBound3; l++)
				{
					if (Module1.IsPointInsidePolygonF(Public2.PRJ.CPG[MyProject.Forms.Form1.UC_ControlPoints1.SelRowDGV1].CP[MyProject.Forms.Form1.UC_ControlPoints1.SelRowDGV2].CoordWM, Public2.PRJ.BLD[l].CoordWM))
					{
						Interaction.MsgBox(Localization.LocMSG(71) + ". " + Localization.LocMSG(72) + " " + Conversions.ToString(MyProject.Forms.Form1.UC_ControlPoints1.SelRowDGV2 + 1) + " " + Localization.LocMSG(75));
						Close();
						return;
					}
				}
			}
			lblElevation.Text = "Elevation at control point: " + Conversions.ToString(Math.Round(PointElevation, 1)) + " m";
			FillDGV1();
			CalcGraph();
			PB.Refresh();
		}
	}

	private void cmdRefresh_Click(object sender, EventArgs e)
	{
		FillDGV1();
		CalcGraph();
		PB.Refresh();
	}

	public void FillDGV1()
	{
		DGV1.Rows.Clear();
		checked
		{
			short num = (short)Math.Floor((AnalisisHmax - AnalisisHmin) / AnalisisStep);
			if (num < 1)
			{
				return;
			}
			float[,] array = new float[Public2.PRJ.TX.GetUpperBound(0) + 1, num + 1];
			float[] array2 = new float[num + 1];
			float[] array3 = new float[num + 1];
			float[] array4 = new float[num + 1];
			int num2 = num;
			float[] Value = default;
			for (int i = 0; i <= num2; i++)
			{
				float zt = AnalisisHmin + (float)i * AnalisisStep + PointElevation;
				bool inBuilding = false;
				if (InBuilding && AnalisisHmin + (float)i * AnalisisStep < BldHeight)
				{
					inBuilding = true;
				}
				array4[i] = CalcSZZ.CalcPointKB(inBuilding, 0f, IndExclBLD, IndExclWall, (float)Public2.PRJ.CPG[MyProject.Forms.Form1.UC_ControlPoints1.SelRowDGV1].CP[MyProject.Forms.Form1.UC_ControlPoints1.SelRowDGV2].CoordM.Xm, (float)Public2.PRJ.CPG[MyProject.Forms.Form1.UC_ControlPoints1.SelRowDGV1].CP[MyProject.Forms.Form1.UC_ControlPoints1.SelRowDGV2].CoordM.Ym, zt, ref Value, ref array3[i], ref array2[i]);
				int upperBound = Public2.PRJ.TX.GetUpperBound(0);
				for (int j = 0; j <= upperBound; j++)
				{
					array[j, i] = Value[j];
				}
			}
			DGV1.RowCount = Public2.PRJ.TX.GetUpperBound(0) + 4;
			DGV1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			DGV1.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
			DGV1.ColumnCount = num + 3;
			DGV1.Columns[0].Width = 30;
			DGV1.Columns[1].HeaderText = "Radiating Source / Hight (m)";
			DGV1.Columns[1].Frozen = true;
			DGV1.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
			DGV1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
			DGV1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
			DGV1.Rows[0].Frozen = true;
			DGV1.Rows[1].Frozen = true;
			DGV1.Rows[2].Frozen = true;
			DGV1[1, 0].Value = "Simultaneous exposure (%)";
			DGV1[1, 1].Value = "Total E-field strength (V/m)";
			DGV1[1, 2].Value = "Total eq. plane-wave power density (W/m²)";
			int upperBound2 = Public2.PRJ.TX.GetUpperBound(0);
			for (int k = 0; k <= upperBound2; k++)
			{
				DGV1[0, k + 3].Value = Strings.Format(k + 1, "000").ToString();
				DGV1[1, k + 3].Value = "E-field strength for " + Public2.PRJ.TX[k].Name + " (V/m)";
			}
			int num3 = num;
			for (int l = 0; l <= num3; l++)
			{
				float num4 = AnalisisHmin + (float)l * AnalisisStep;
				DGV1.Columns[l + 2].HeaderText = Conversions.ToString(Math.Round(num4, 1)) + " m";
				DGV1.Columns[l + 2].Width = 50;
				DGV1.Columns[l + 2].SortMode = DataGridViewColumnSortMode.NotSortable;
				DGV1[l + 2, 0].Value = Module1.SuperFormat(array4[l] * 100f, 6);
				DGV1[l + 2, 1].Value = Module1.SuperFormat(array2[l], 6);
				DGV1[l + 2, 2].Value = Module1.SuperFormat(array3[l] / 100f, 6);
				int upperBound3 = Public2.PRJ.TX.GetUpperBound(0);
				for (int m = 0; m <= upperBound3; m++)
				{
					DGV1[l + 2, m + 3].Value = Module1.SuperFormat(array[m, l], 6);
				}
			}
			float num5 = 0f;
			short num6 = 0;
			float num7 = 0f;
			short num8 = 0;
			float num9 = 0f;
			short num10 = 0;
			int num11 = num;
			for (int n = 0; n <= num11; n++)
			{
				if (array4[n] > num5)
				{
					num5 = array4[n];
					num6 = (short)n;
				}
				if (array3[n] > num7)
				{
					num7 = array3[n];
					num8 = (short)n;
				}
				if (array2[n] > num9)
				{
					num9 = array2[n];
					num10 = (short)n;
				}
			}
			float[] array5 = new float[Public2.PRJ.TX.GetUpperBound(0) + 1];
			short[] array6 = new short[Public2.PRJ.TX.GetUpperBound(0) + 1];
			int upperBound4 = Public2.PRJ.TX.GetUpperBound(0);
			for (int num12 = 0; num12 <= upperBound4; num12++)
			{
				int num13 = num;
				for (int num14 = 0; num14 <= num13; num14++)
				{
					if (array[num12, num14] > array5[num12])
					{
						array5[num12] = array[num12, num14];
						array6[num12] = (short)num14;
					}
				}
			}
			Font font = new Font(DGV1.DefaultCellStyle.Font.Name, DGV1.DefaultCellStyle.Font.Size, FontStyle.Bold);
			if (num5 > 0f)
			{
				DGV1[num6 + 2, 0].Style.Font = font;
			}
			if (num7 > 0f)
			{
				DGV1[num8 + 2, 1].Style.Font = font;
			}
			if (num9 > 0f)
			{
				DGV1[num10 + 2, 2].Style.Font = font;
			}
			int upperBound5 = Public2.PRJ.TX.GetUpperBound(0);
			for (int num15 = 0; num15 <= upperBound5; num15++)
			{
				if (array5[num15] > 0f)
				{
					DGV1[array6[num15] + 2, num15 + 3].Style.Font = font;
				}
			}
			int num16 = num;
			for (int num17 = 0; num17 <= num16; num17++)
			{
				if (array4[num17] >= 1f)
				{
					DGV1[num17 + 2, 0].Style.ForeColor = Color.Red;
				}
			}
			int upperBound6 = Public2.PRJ.TX.GetUpperBound(0);
			for (int num18 = 0; num18 <= upperBound6; num18++)
			{
				DGV1.Rows[num18 + 3].DefaultCellStyle.BackColor = Public2.PRJ.TX[num18].Color;
			}
			DGV1.CurrentCell = DGV1[1, 1];
			DGV1.ClearSelection();
		}
	}

	public void CalcGraph()
	{
		if (AnalisisHmax - AnalisisHmin <= 0f)
		{
			return;
		}
		float num = (AnalisisHmax - AnalisisHmin) / (float)GR.GetUpperBound(0);
		int upperBound = GR.GetUpperBound(0);
		float[] Value = default;
		float PPEsum = default;
		for (int i = 0; i <= upperBound; i = checked(i + 1))
		{
			float zt = AnalisisHmin + (float)i * num + PointElevation;
			bool inBuilding = false;
			if (InBuilding && AnalisisHmin + (float)i * num < BldHeight)
			{
				inBuilding = true;
			}
			GR[i] = 100f * CalcSZZ.CalcPointKB(inBuilding, 0f, IndExclBLD, IndExclWall, (float)Public2.PRJ.CPG[MyProject.Forms.Form1.UC_ControlPoints1.SelRowDGV1].CP[MyProject.Forms.Form1.UC_ControlPoints1.SelRowDGV2].CoordM.Xm, (float)Public2.PRJ.CPG[MyProject.Forms.Form1.UC_ControlPoints1.SelRowDGV1].CP[MyProject.Forms.Form1.UC_ControlPoints1.SelRowDGV2].CoordM.Ym, zt, ref Value, ref PPEsum, ref GrEsum[i]);
		}
	}

	private void PB_Paint(object sender, PaintEventArgs e)
	{
		short num = 14;
		short num2 = 15;
		short num3 = 35;
		short num4 = 45;
		SizeF sizeF = default;
		e.Graphics.Clear(Color.White);
		checked
		{
			e.Graphics.DrawLine(new Pen(Color.Black, 1f), num3, num, PB.Width - num4, num);
			e.Graphics.DrawLine(new Pen(Color.Black, 1f), num3, PB.Height - num2, PB.Width - num4, PB.Height - num2);
			e.Graphics.DrawLine(new Pen(Color.Black, 1f), num3, num, num3, PB.Height - num2);
			e.Graphics.DrawLine(new Pen(Color.Black, 1f), PB.Width - num4, num, PB.Width - num4, PB.Height - num2);
			float X;
			float dX;
			if (!GraphType)
			{
				float num5 = 0f;
				int upperBound = GR.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					if (GR[i] > num5)
					{
						num5 = GR[i];
					}
				}
				if (num5 == 0f)
				{
					num5 = 1f;
				}
				num5 = (float)((double)num5 * 1.05);
				float num6 = (float)((double)(PB.Width - num3 - num4) / (double)GR.GetUpperBound(0));
				float num7 = (float)(PB.Width - num3 - num4) / (AnalisisHmax - AnalisisHmin);
				float num8 = (float)(PB.Height - num2 - num) / num5;
				Pen pen = new Pen(Color.Gray, 1f);
				pen.DashStyle = DashStyle.Dash;
				float analisisHmin = AnalisisHmin;
				float analisisHmax = AnalisisHmax;
				short num9 = default;
				int N = num9;
				double num10 = default;
				dX = (float)num10;
				double num11 = default;
				X = (float)num11;
				Module1.RazmetkaOSI(analisisHmin, analisisHmax, 8, 20, ref N, ref dX, ref X);
				num11 = X;
				num10 = dX;
				num9 = (short)N;
				N = num9 - 1;
				for (int j = 0; j <= N; j++)
				{
					short num12 = (short)(int)Math.Round((double)num3 + (num11 - (double)AnalisisHmin) * (double)num7);
					e.Graphics.DrawLine(pen, num12, num, num12, PB.Height - num2);
					sizeF = e.Graphics.MeasureString(Conversions.ToString(num11), new Font("Arial", 7f, FontStyle.Regular));
					e.Graphics.DrawString(Conversions.ToString(num11), new Font("Arial", 7f, FontStyle.Regular), Brushes.Black, (float)num12 - sizeF.Width / 2f, PB.Height - 12);
					num11 += num10;
				}
				float xmax = num5;
				int N2 = num9;
				Module1.RazmetkaOSI2(0f, xmax, 4, 8, ref N2, ref num10, ref num11);
				num9 = (short)N2;
				N2 = num9 - 1;
				for (int k = 0; k <= N2; k++)
				{
					short num12 = (short)(int)Math.Round((double)(PB.Height - num2) - num11 * (double)num8);
					e.Graphics.DrawLine(pen, num3, num12, PB.Width - num4, num12);
					sizeF = e.Graphics.MeasureString(Conversions.ToString(num11), new Font("Arial", 7f, FontStyle.Regular));
					e.Graphics.DrawString(Conversions.ToString(num11), new Font("Arial", 7f, FontStyle.Regular), Brushes.Black, (float)num3 - sizeF.Width - 1f, (float)num12 - sizeF.Height / 2f);
					num11 += num10;
				}
				int upperBound2 = GR.GetUpperBound(0);
				for (int l = 1; l <= upperBound2; l++)
				{
					Point[] array = new Point[GR.GetUpperBound(0) + 1];
					int upperBound3 = GR.GetUpperBound(0);
					for (int m = 0; m <= upperBound3; m++)
					{
						array[m] = new Point((int)Math.Round((float)num3 + (float)m * num6), (int)Math.Round((float)(PB.Height - num2) - GR[m] * num8));
					}
					e.Graphics.DrawLines(new Pen(Brushes.Red, 3f), array);
				}
				e.Graphics.DrawString("Height\r\n    (m)", new Font("Arial", 7f, FontStyle.Regular), Brushes.Black, PB.Width - 40, PB.Height - 22);
				e.Graphics.DrawString("Simultaneous exposure (%)", new Font("Arial", 7f, FontStyle.Regular), Brushes.Black, 23f, 0f);
				return;
			}
			float num13 = 0f;
			int upperBound4 = GrEsum.GetUpperBound(0);
			for (int n = 0; n <= upperBound4; n++)
			{
				if (GrEsum[n] > num13)
				{
					num13 = GrEsum[n];
				}
			}
			if (num13 == 0f)
			{
				num13 = 1f;
			}
			num13 = (float)((double)num13 * 1.05);
			float num14 = (float)((double)(PB.Width - num3 - num4) / (double)GR.GetUpperBound(0));
			float num15 = (float)(PB.Width - num3 - num4) / (AnalisisHmax - AnalisisHmin);
			float num16 = (float)(PB.Height - num2 - num) / num13;
			Pen pen2 = new Pen(Color.Gray, 1f);
			pen2.DashStyle = DashStyle.Dash;
			float analisisHmin2 = AnalisisHmin;
			float analisisHmax2 = AnalisisHmax;
			short num17 = default;
			int N3 = num17;
			double num18 = default;
			X = (float)num18;
			double num19 = default;
			dX = (float)num19;
			Module1.RazmetkaOSI(analisisHmin2, analisisHmax2, 8, 20, ref N3, ref X, ref dX);
			num19 = dX;
			num18 = X;
			num17 = (short)N3;
			N3 = num17 - 1;
			for (int num20 = 0; num20 <= N3; num20++)
			{
				short num21 = (short)(int)Math.Round((double)num3 + (num19 - (double)AnalisisHmin) * (double)num15);
				e.Graphics.DrawLine(pen2, num21, num, num21, PB.Height - num2);
				sizeF = e.Graphics.MeasureString(Conversions.ToString(num19), new Font("Arial", 7f, FontStyle.Regular));
				e.Graphics.DrawString(Conversions.ToString(num19), new Font("Arial", 7f, FontStyle.Regular), Brushes.Black, (float)num21 - sizeF.Width / 2f, PB.Height - 12);
				num19 += num18;
			}
			float xmax2 = num13;
			int N4 = num17;
			Module1.RazmetkaOSI2(0f, xmax2, 4, 8, ref N4, ref num18, ref num19);
			num17 = (short)N4;
			N4 = num17 - 1;
			for (int num22 = 0; num22 <= N4; num22++)
			{
				short num21 = (short)(int)Math.Round((double)(PB.Height - num2) - num19 * (double)num16);
				e.Graphics.DrawLine(pen2, num3, num21, PB.Width - num4, num21);
				sizeF = e.Graphics.MeasureString(Conversions.ToString(num19), new Font("Arial", 7f, FontStyle.Regular));
				e.Graphics.DrawString(Conversions.ToString(num19), new Font("Arial", 7f, FontStyle.Regular), Brushes.Black, (float)num3 - sizeF.Width - 1f, (float)num21 - sizeF.Height / 2f);
				num19 += num18;
			}
			Pen pen3 = new Pen(Brushes.Red, 3f);
			pen3.LineJoin = LineJoin.Bevel;
			int upperBound5 = GrEsum.GetUpperBound(0);
			for (int num23 = 1; num23 <= upperBound5; num23++)
			{
				Point[] array2 = new Point[GrEsum.GetUpperBound(0) + 1];
				int upperBound6 = GrEsum.GetUpperBound(0);
				for (int num24 = 0; num24 <= upperBound6; num24++)
				{
					array2[num24] = new Point((int)Math.Round((float)num3 + (float)num24 * num14), (int)Math.Round((float)(PB.Height - num2) - GrEsum[num24] * num16));
				}
				e.Graphics.DrawLines(pen3, array2);
			}
			e.Graphics.DrawString("Height\r\n    (m)", new Font("Arial", 7f, FontStyle.Regular), Brushes.Black, PB.Width - 40, PB.Height - 22);
			e.Graphics.DrawString("Total E-field strength (V/m)", new Font("Arial", 7f, FontStyle.Regular), Brushes.Black, 23f, 0f);
		}
	}

	private void PB_Resize(object sender, EventArgs e)
	{
		PB.Refresh();
	}

	private void txtHmin_Validating(object sender, CancelEventArgs e)
	{
		TextBox textBox = txtHmin;
		string TextProperty = textBox.Text;
		ref float analisisHmin = ref AnalisisHmin;
		double DblVariable = analisisHmin;
		Module1.TextPropertyValidating(ref TextProperty, ref DblVariable, CheckLimits: true, 0f, 100f, IncludeMin: true, IncludeMax: true, Message: true);
		analisisHmin = (float)DblVariable;
		textBox.Text = TextProperty;
	}

	private void txtHmax_Validating(object sender, CancelEventArgs e)
	{
		TextBox textBox = txtHmax;
		string TextProperty = textBox.Text;
		ref float analisisHmax = ref AnalisisHmax;
		double DblVariable = analisisHmax;
		Module1.TextPropertyValidating(ref TextProperty, ref DblVariable, CheckLimits: true, 0f, 200f, IncludeMin: false, IncludeMax: true, Message: true);
		analisisHmax = (float)DblVariable;
		textBox.Text = TextProperty;
	}

	private void txtHmax_txtStep(object sender, CancelEventArgs e)
	{
		TextBox textBox = txtStep;
		string TextProperty = textBox.Text;
		ref float analisisStep = ref AnalisisStep;
		double DblVariable = analisisStep;
		Module1.TextPropertyValidating(ref TextProperty, ref DblVariable, CheckLimits: true, 0f, 10f, IncludeMin: false, IncludeMax: true, Message: true);
		analisisStep = (float)DblVariable;
		textBox.Text = TextProperty;
	}

	private void rbSimExposure_MouseClick(object sender, MouseEventArgs e)
	{
		GraphType = rbTotalE.Checked;
		PB.Refresh();
	}
}
