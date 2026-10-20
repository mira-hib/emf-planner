using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using EMFPlanner.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[DesignerGenerated]
public class UC_Antennas : UserControl
{
	private IContainer components;

	private bool FlagFillDGVisGoingOn;

	private bool FlagGroupChangeDGV1;

	[field: AccessedThroughProperty("GroupBox1")]
	internal virtual GroupBox GroupBox1
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
		[CompilerGenerated]
		set
		{
			DataGridViewCellEventHandler value2 = DGV1_CellEnter;
			EventHandler value3 = DGV1_SelectionChanged;
			DataGridViewCellEventHandler value4 = DGV1_CellValueChanged;
			DataGridView dataGridView = field;
			if (dataGridView != null)
			{
				dataGridView.CellEnter -= value2;
				dataGridView.SelectionChanged -= value3;
				dataGridView.CellValueChanged -= value4;
			}
			field = value;
			dataGridView = field;
			if (dataGridView != null)
			{
				dataGridView.CellEnter += value2;
				dataGridView.SelectionChanged += value3;
				dataGridView.CellValueChanged += value4;
			}
		}
	}

	[field: AccessedThroughProperty("cmdPosition")]
	internal virtual Button cmdPosition
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdPosition_Click;
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

	[field: AccessedThroughProperty("cmdMoveSelectedAntennasToCenterScreen")]
	internal virtual Button cmdMoveSelectedAntennasToCenterScreen
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdMoveSelectedAntennasToCenterScreen_Click;
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

	[field: AccessedThroughProperty("ToolTip1")]
	internal virtual ToolTip ToolTip1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdMoveToActive")]
	internal virtual Button cmdMoveToActive
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdMoveToActive_Click;
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

	[field: AccessedThroughProperty("cmdMoveToOrigin")]
	internal virtual Button cmdMoveToOrigin
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdMoveToOrigin_Click;
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

	[field: AccessedThroughProperty("Label1")]
	internal virtual Label Label1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("tbLengthAttractAntennas")]
	internal virtual TrackBar tbLengthAttractAntennas
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = tbLengthAttractAntennas_Scroll;
			TrackBar trackBar = field;
			if (trackBar != null)
			{
				trackBar.Scroll -= value2;
			}
			field = value;
			trackBar = field;
			if (trackBar != null)
			{
				trackBar.Scroll += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmdMoveOriginToActive")]
	internal virtual Button cmdMoveOriginToActive
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdMoveOriginToActive_Click;
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

	[field: AccessedThroughProperty("Column1")]
	internal virtual DataGridViewTextBoxColumn Column1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Column2")]
	internal virtual DataGridViewTextBoxColumn Column2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Column3")]
	internal virtual DataGridViewTextBoxColumn Column3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Column4")]
	internal virtual DataGridViewTextBoxColumn Column4
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Column5")]
	internal virtual DataGridViewTextBoxColumn Column5
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Column6")]
	internal virtual DataGridViewTextBoxColumn Column6
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Column7")]
	internal virtual DataGridViewTextBoxColumn Column7
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public UC_Antennas()
	{
		Load += UC_Antennas_Load;
		FlagFillDGVisGoingOn = false;
		FlagGroupChangeDGV1 = false;
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
		this.components = new System.ComponentModel.Container();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EMFPlanner.UC_Antennas));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		this.GroupBox1 = new System.Windows.Forms.GroupBox();
		this.cmdMoveOriginToActive = new System.Windows.Forms.Button();
		this.Label1 = new System.Windows.Forms.Label();
		this.tbLengthAttractAntennas = new System.Windows.Forms.TrackBar();
		this.cmdMoveToOrigin = new System.Windows.Forms.Button();
		this.cmdMoveToActive = new System.Windows.Forms.Button();
		this.cmdPosition = new System.Windows.Forms.Button();
		this.cmdMoveSelectedAntennasToCenterScreen = new System.Windows.Forms.Button();
		this.DGV1 = new System.Windows.Forms.DataGridView();
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.GroupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.tbLengthAttractAntennas).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.DGV1).BeginInit();
		base.SuspendLayout();
		this.GroupBox1.Controls.Add(this.cmdMoveOriginToActive);
		this.GroupBox1.Controls.Add(this.Label1);
		this.GroupBox1.Controls.Add(this.tbLengthAttractAntennas);
		this.GroupBox1.Controls.Add(this.cmdMoveToOrigin);
		this.GroupBox1.Controls.Add(this.cmdMoveToActive);
		this.GroupBox1.Controls.Add(this.cmdPosition);
		this.GroupBox1.Controls.Add(this.cmdMoveSelectedAntennasToCenterScreen);
		this.GroupBox1.Controls.Add(this.DGV1);
		this.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GroupBox1.Location = new System.Drawing.Point(0, 0);
		this.GroupBox1.Name = "GroupBox1";
		this.GroupBox1.Size = new System.Drawing.Size(500, 705);
		this.GroupBox1.TabIndex = 0;
		this.GroupBox1.TabStop = false;
		this.GroupBox1.Text = "Antenna Locations";
		this.cmdMoveOriginToActive.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdMoveOriginToActive.Image = (System.Drawing.Image)resources.GetObject("cmdMoveOriginToActive.Image");
		this.cmdMoveOriginToActive.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdMoveOriginToActive.Location = new System.Drawing.Point(112, 19);
		this.cmdMoveOriginToActive.Name = "cmdMoveOriginToActive";
		this.cmdMoveOriginToActive.Size = new System.Drawing.Size(26, 26);
		this.cmdMoveOriginToActive.TabIndex = 163;
		this.cmdMoveOriginToActive.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdMoveOriginToActive, "Move origin to current antenna point");
		this.cmdMoveOriginToActive.UseVisualStyleBackColor = true;
		this.Label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.Label1.AutoSize = true;
		this.Label1.Location = new System.Drawing.Point(6, 679);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(133, 13);
		this.Label1.TabIndex = 162;
		this.Label1.Text = "Maximum capture distance";
		this.tbLengthAttractAntennas.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tbLengthAttractAntennas.AutoSize = false;
		this.tbLengthAttractAntennas.Location = new System.Drawing.Point(153, 675);
		this.tbLengthAttractAntennas.Maximum = 30;
		this.tbLengthAttractAntennas.Minimum = 1;
		this.tbLengthAttractAntennas.Name = "tbLengthAttractAntennas";
		this.tbLengthAttractAntennas.Size = new System.Drawing.Size(341, 24);
		this.tbLengthAttractAntennas.TabIndex = 161;
		this.tbLengthAttractAntennas.TickStyle = System.Windows.Forms.TickStyle.None;
		this.tbLengthAttractAntennas.Value = 15;
		this.cmdMoveToOrigin.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdMoveToOrigin.Image = (System.Drawing.Image)resources.GetObject("cmdMoveToOrigin.Image");
		this.cmdMoveToOrigin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdMoveToOrigin.Location = new System.Drawing.Point(85, 19);
		this.cmdMoveToOrigin.Name = "cmdMoveToOrigin";
		this.cmdMoveToOrigin.Size = new System.Drawing.Size(26, 26);
		this.cmdMoveToOrigin.TabIndex = 160;
		this.cmdMoveToOrigin.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdMoveToOrigin, "Move all selected antennas to the origin");
		this.cmdMoveToOrigin.UseVisualStyleBackColor = true;
		this.cmdMoveToActive.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdMoveToActive.Image = (System.Drawing.Image)resources.GetObject("cmdMoveToActive.Image");
		this.cmdMoveToActive.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdMoveToActive.Location = new System.Drawing.Point(58, 19);
		this.cmdMoveToActive.Name = "cmdMoveToActive";
		this.cmdMoveToActive.Size = new System.Drawing.Size(26, 26);
		this.cmdMoveToActive.TabIndex = 159;
		this.cmdMoveToActive.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdMoveToActive, "Move all selected antennas to the current antenna point");
		this.cmdMoveToActive.UseVisualStyleBackColor = true;
		this.cmdPosition.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdPosition.Image = (System.Drawing.Image)resources.GetObject("cmdPosition.Image");
		this.cmdPosition.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdPosition.Location = new System.Drawing.Point(4, 19);
		this.cmdPosition.Name = "cmdPosition";
		this.cmdPosition.Size = new System.Drawing.Size(26, 26);
		this.cmdPosition.TabIndex = 156;
		this.cmdPosition.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdPosition, "Position the map to a point with a selected antenna");
		this.cmdPosition.UseVisualStyleBackColor = true;
		this.cmdMoveSelectedAntennasToCenterScreen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdMoveSelectedAntennasToCenterScreen.Image = (System.Drawing.Image)resources.GetObject("cmdMoveSelectedAntennasToCenterScreen.Image");
		this.cmdMoveSelectedAntennasToCenterScreen.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdMoveSelectedAntennasToCenterScreen.Location = new System.Drawing.Point(31, 19);
		this.cmdMoveSelectedAntennasToCenterScreen.Name = "cmdMoveSelectedAntennasToCenterScreen";
		this.cmdMoveSelectedAntennasToCenterScreen.Size = new System.Drawing.Size(26, 26);
		this.cmdMoveSelectedAntennasToCenterScreen.TabIndex = 155;
		this.cmdMoveSelectedAntennasToCenterScreen.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdMoveSelectedAntennasToCenterScreen, "Move all selected antennas to the screen center");
		this.cmdMoveSelectedAntennasToCenterScreen.UseVisualStyleBackColor = true;
		this.DGV1.AllowUserToAddRows = false;
		this.DGV1.AllowUserToDeleteRows = false;
		this.DGV1.AllowUserToOrderColumns = true;
		this.DGV1.AllowUserToResizeColumns = false;
		this.DGV1.AllowUserToResizeRows = false;
		this.DGV1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
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
		this.DGV1.Columns.AddRange(this.Column1, this.Column2, this.Column3, this.Column4, this.Column5, this.Column6, this.Column7);
		this.DGV1.Location = new System.Drawing.Point(3, 51);
		this.DGV1.Name = "DGV1";
		this.DGV1.RowHeadersWidth = 55;
		this.DGV1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.DGV1.RowTemplate.Height = 18;
		this.DGV1.Size = new System.Drawing.Size(492, 618);
		this.DGV1.TabIndex = 6;
		this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.ForeColor = System.Drawing.Color.FromArgb(70, 70, 70);
		this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
		this.Column1.HeaderText = "Source name";
		this.Column1.Name = "Column1";
		this.Column1.ReadOnly = true;
		this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(70, 70, 70);
		this.Column2.DefaultCellStyle = dataGridViewCellStyle3;
		this.Column2.HeaderText = "Latitude, deg";
		this.Column2.Name = "Column2";
		this.Column2.ReadOnly = true;
		this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column2.Width = 65;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(70, 70, 70);
		this.Column3.DefaultCellStyle = dataGridViewCellStyle4;
		this.Column3.HeaderText = "Longitude, deg.";
		this.Column3.Name = "Column3";
		this.Column3.ReadOnly = true;
		this.Column3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column3.Width = 65;
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Column4.DefaultCellStyle = dataGridViewCellStyle5;
		this.Column4.HeaderText = "Х, m";
		this.Column4.Name = "Column4";
		this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column4.Width = 45;
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Column5.DefaultCellStyle = dataGridViewCellStyle6;
		this.Column5.HeaderText = "Y, m";
		this.Column5.Name = "Column5";
		this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column5.Width = 45;
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle7.ForeColor = System.Drawing.Color.FromArgb(70, 70, 70);
		this.Column6.DefaultCellStyle = dataGridViewCellStyle7;
		this.Column6.HeaderText = "H, m";
		this.Column6.Name = "Column6";
		this.Column6.ReadOnly = true;
		this.Column6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column6.Width = 45;
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(70, 70, 70);
		this.Column7.DefaultCellStyle = dataGridViewCellStyle8;
		this.Column7.HeaderText = "Azimuth, deg.";
		this.Column7.Name = "Column7";
		this.Column7.ReadOnly = true;
		this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column7.Width = 45;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		base.Controls.Add(this.GroupBox1);
		this.MinimumSize = new System.Drawing.Size(300, 0);
		base.Name = "UC_Antennas";
		base.Size = new System.Drawing.Size(500, 705);
		this.GroupBox1.ResumeLayout(false);
		this.GroupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.tbLengthAttractAntennas).EndInit();
		((System.ComponentModel.ISupportInitialize)this.DGV1).EndInit();
		base.ResumeLayout(false);
	}

	private void UC_Antennas_Load(object sender, EventArgs e)
	{
		MyProject.Forms.Form1.DrawPIC();
		Module1.SetDoubleBuffered(DGV1);
		FillDGV1();
		if (!Information.IsNothing(Public2.PRJ.TX) && Form1_Public.IndexTX <= Public2.PRJ.TX.GetUpperBound(0))
		{
			DGV1.CurrentCell = DGV1[0, Form1_Public.IndexTX];
			DGV1.Rows[Form1_Public.IndexTX].Selected = true;
		}
		tbLengthAttractAntennas.Value = Public2.PRJ.SETT.LengthAttractAntennas;
	}

	public void FillDGV1()
	{
		FlagFillDGVisGoingOn = true;
		DGV1.Rows.Clear();
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.TX))
			{
				CalcCoords.ConvAllTxWebMercMetersToMeters();
				int upperBound = Public2.PRJ.TX.GetUpperBound(0);
				double LatWgs = default;
				double LonWgs = default;
				for (int i = 0; i <= upperBound; i++)
				{
					CalcCoords.ConvWebMercMetersToWGS84(Public2.PRJ.TX[i].CoordWM, ref LatWgs, ref LonWgs);
					DGV1.Rows.Add(Public2.PRJ.TX[i].Name + " (" + Public2.PRJ.TX[i].AntType + ")", CalcCoords.ConvLatToStr(LatWgs), CalcCoords.ConvLonToStr(LonWgs), Strings.Format(Public2.PRJ.TX[i].CoordM.Xm, "0.0"), Strings.Format(Public2.PRJ.TX[i].CoordM.Ym, "0.0"), Public2.PRJ.TX[i].H, Form1_Public.Azimuth360(Public2.PRJ.TX[i].Az));
				}
				int upperBound2 = Public2.PRJ.TX.GetUpperBound(0);
				for (int j = 0; j <= upperBound2; j++)
				{
					DGV1.Rows[j].HeaderCell.Value = Strings.Format(j + 1, "000").ToString();
					DGV1.Rows[j].DefaultCellStyle.BackColor = Public2.PRJ.TX[j].Color;
				}
				int upperBound3 = Public2.PRJ.TX.GetUpperBound(0);
				for (int k = 0; k <= upperBound3; k++)
				{
					Public2.PRJ.TX[k].Selected = false;
				}
				FlagFillDGVisGoingOn = false;
			}
		}
	}

	private void DGV1_CellEnter(object sender, DataGridViewCellEventArgs e)
	{
		if (!FlagFillDGVisGoingOn)
		{
			Form1_Public.IndexTX = e.RowIndex;
		}
	}

	private void DGV1_SelectionChanged(object sender, EventArgs e)
	{
		if (Information.IsNothing(Public2.PRJ.TX))
		{
			return;
		}
		checked
		{
			if (DGV1.SelectedRows.Count == 0)
			{
				int upperBound = Public2.PRJ.TX.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					Public2.PRJ.TX[i].Selected = false;
				}
				MyProject.Forms.Form1.PB.Refresh();
				return;
			}
			int upperBound2 = Public2.PRJ.TX.GetUpperBound(0);
			for (int j = 0; j <= upperBound2; j++)
			{
				Public2.PRJ.TX[j].Selected = false;
			}
			int num = DGV1.SelectedRows.Count - 1;
			for (int k = 0; k <= num; k++)
			{
				Public2.PRJ.TX[DGV1.SelectedRows[k].Index].Selected = true;
			}
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void DGV1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
		if (FlagFillDGVisGoingOn || FlagGroupChangeDGV1 || e.RowIndex < 0)
		{
			return;
		}
		checked
		{
			if (e.ColumnIndex == 3)
			{
				if (Versioned.IsNumeric(RuntimeHelpers.GetObjectValue(DGV1[e.ColumnIndex, e.RowIndex].Value)))
				{
					if (DGV1.SelectedCells.Count == 1)
					{
						Public2.PRJ.TX[e.RowIndex].CoordM.Xm = Conversions.ToDouble(DGV1[e.ColumnIndex, e.RowIndex].Value);
						DGV1[e.ColumnIndex, e.RowIndex].Value = Strings.Format(Public2.PRJ.TX[e.RowIndex].CoordM.Xm, "0.0").ToString();
						Public2.PRJ.TX[e.RowIndex].CoordWM = CalcCoords.ConvMetersToWebMercMeters(Public2.PRJ.TX[e.RowIndex].CoordM);
						double LatWgs = default;
						double LonWgs = default;
						CalcCoords.ConvWebMercMetersToWGS84(Public2.PRJ.TX[e.RowIndex].CoordWM, ref LatWgs, ref LonWgs);
						DGV1[1, e.RowIndex].Value = Strings.Format(LatWgs, "0.000000");
						DGV1[2, e.RowIndex].Value = Strings.Format(LonWgs, "0.000000");
					}
					else
					{
						if (DGV1.SelectedCells.Count <= 1)
						{
							return;
						}
						FlagGroupChangeDGV1 = true;
						string sTR = Conversions.ToString(DGV1[e.ColumnIndex, e.RowIndex].Value);
						int num = DGV1.SelectedCells.Count - 1;
						double LatWgs2 = default;
						double LonWgs2 = default;
						for (int i = 0; i <= num; i++)
						{
							if ((DGV1.SelectedCells[i].ColumnIndex == e.ColumnIndex) & (DGV1.SelectedCells[i].RowIndex <= Public2.PRJ.TX.GetUpperBound(0)))
							{
								Public2.PRJ.TX[DGV1.SelectedCells[i].RowIndex].CoordM.Xm = Module1.QSNG(sTR);
								DGV1[e.ColumnIndex, DGV1.SelectedCells[i].RowIndex].Value = Strings.Format(Module1.QSNG(sTR), "0.0").ToString();
								Public2.PRJ.TX[DGV1.SelectedCells[i].RowIndex].CoordWM = CalcCoords.ConvMetersToWebMercMeters(Public2.PRJ.TX[DGV1.SelectedCells[i].RowIndex].CoordM);
								CalcCoords.ConvWebMercMetersToWGS84(Public2.PRJ.TX[DGV1.SelectedCells[i].RowIndex].CoordWM, ref LatWgs2, ref LonWgs2);
								DGV1[1, DGV1.SelectedCells[i].RowIndex].Value = Strings.Format(LatWgs2, "0.000000").ToString();
								DGV1[2, DGV1.SelectedCells[i].RowIndex].Value = Strings.Format(LonWgs2, "0.000000").ToString();
							}
						}
						DGV1.ClearSelection();
						FlagGroupChangeDGV1 = false;
					}
				}
				else
				{
					DGV1[e.ColumnIndex, e.RowIndex].Value = Strings.Format(Public2.PRJ.TX[e.RowIndex].CoordM.Xm, "0.0").ToString();
				}
			}
			else
			{
				if (e.ColumnIndex != 4)
				{
					return;
				}
				if (Versioned.IsNumeric(RuntimeHelpers.GetObjectValue(DGV1[e.ColumnIndex, e.RowIndex].Value)))
				{
					if (DGV1.SelectedCells.Count == 1)
					{
						Public2.PRJ.TX[e.RowIndex].CoordM.Ym = Conversions.ToDouble(DGV1[e.ColumnIndex, e.RowIndex].Value);
						DGV1[e.ColumnIndex, e.RowIndex].Value = Strings.Format(Public2.PRJ.TX[e.RowIndex].CoordM.Ym, "0.0").ToString();
						Public2.PRJ.TX[e.RowIndex].CoordWM = CalcCoords.ConvMetersToWebMercMeters(Public2.PRJ.TX[e.RowIndex].CoordM);
						double LatWgs3 = default;
						double LonWgs3 = default;
						CalcCoords.ConvWebMercMetersToWGS84(Public2.PRJ.TX[e.RowIndex].CoordWM, ref LatWgs3, ref LonWgs3);
						DGV1[1, e.RowIndex].Value = Strings.Format(LatWgs3, "0.000000");
						DGV1[2, e.RowIndex].Value = Strings.Format(LonWgs3, "0.000000");
					}
					else
					{
						if (DGV1.SelectedCells.Count <= 1)
						{
							return;
						}
						FlagGroupChangeDGV1 = true;
						string sTR2 = Conversions.ToString(DGV1[e.ColumnIndex, e.RowIndex].Value);
						int num2 = DGV1.SelectedCells.Count - 1;
						double LatWgs4 = default;
						double LonWgs4 = default;
						for (int j = 0; j <= num2; j++)
						{
							if ((DGV1.SelectedCells[j].ColumnIndex == e.ColumnIndex) & (DGV1.SelectedCells[j].RowIndex <= Public2.PRJ.TX.GetUpperBound(0)))
							{
								Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].CoordM.Ym = Module1.QSNG(sTR2);
								DGV1[e.ColumnIndex, DGV1.SelectedCells[j].RowIndex].Value = Strings.Format(Module1.QSNG(sTR2), "0.0").ToString();
								Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].CoordWM = CalcCoords.ConvMetersToWebMercMeters(Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].CoordM);
								CalcCoords.ConvWebMercMetersToWGS84(Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].CoordWM, ref LatWgs4, ref LonWgs4);
								DGV1[1, DGV1.SelectedCells[j].RowIndex].Value = Strings.Format(LatWgs4, "0.000000").ToString();
								DGV1[2, DGV1.SelectedCells[j].RowIndex].Value = Strings.Format(LonWgs4, "0.000000").ToString();
							}
						}
						DGV1.ClearSelection();
						FlagGroupChangeDGV1 = false;
					}
				}
				else
				{
					DGV1[e.ColumnIndex, e.RowIndex].Value = Strings.Format(Public2.PRJ.TX[e.RowIndex].CoordM.Ym, "0.0").ToString();
				}
			}
		}
	}

	private void cmdMoveSelectedAntennasToCenterScreen_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		checked
		{
			if (Information.IsNothing(Public2.PRJ.TX) | (DGV1.SelectedRows.Count == 0))
			{
				Interaction.MsgBox(Localization.LocMSG(39), MsgBoxStyle.Critical);
			}
			else if (Interaction.MsgBox(Localization.LocMSG(43), MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
			{
				Public2.CoordWebMerc screenCenterWM = Form1_Public.GetScreenCenterWM();
				int num = DGV1.SelectedRows.Count - 1;
				for (int i = 0; i <= num; i++)
				{
					Public2.PRJ.TX[DGV1.SelectedRows[i].Index].CoordWM = screenCenterWM;
				}
				MyProject.Forms.Form1.PB.Refresh();
				FillDGV1();
			}
		}
	}

	private void cmdPosition_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (!Information.IsNothing(Public2.PRJ.TX) && DGV1.CurrentCell.RowIndex >= 0)
		{
			MyProject.Forms.Form1.MapPositionWM(Public2.PRJ.TX[DGV1.CurrentCell.RowIndex].CoordWM.Xwm, Public2.PRJ.TX[DGV1.CurrentCell.RowIndex].CoordWM.Ywm);
		}
	}

	private void cmdMoveToActive_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Information.IsNothing(Public2.PRJ.TX) || DGV1.CurrentCell.RowIndex < 0)
		{
			return;
		}
		checked
		{
			if (DGV1.SelectedRows.Count == 0)
			{
				Interaction.MsgBox(Localization.LocMSG(39), MsgBoxStyle.Critical);
			}
			else if (Interaction.MsgBox(Localization.LocMSG(40), MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
			{
				int num = DGV1.SelectedRows.Count - 1;
				for (int i = 0; i <= num; i++)
				{
					Public2.PRJ.TX[DGV1.SelectedRows[i].Index].CoordWM = Public2.PRJ.TX[DGV1.CurrentCell.RowIndex].CoordWM;
				}
				MyProject.Forms.Form1.PB.Refresh();
				FillDGV1();
			}
		}
	}

	private void cmdMoveToOrigin_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Information.IsNothing(Public2.PRJ.TX) || DGV1.CurrentCell.RowIndex < 0)
		{
			return;
		}
		checked
		{
			if (DGV1.SelectedRows.Count == 0)
			{
				Interaction.MsgBox(Localization.LocMSG(39), MsgBoxStyle.Critical);
			}
			else if (Interaction.MsgBox(Localization.LocMSG(41), MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
			{
				int num = DGV1.SelectedRows.Count - 1;
				for (int i = 0; i <= num; i++)
				{
					Public2.PRJ.TX[DGV1.SelectedRows[i].Index].CoordWM = Public2.PRJ.SETT.OriginCoordWM;
				}
				MyProject.Forms.Form1.PB.Refresh();
				FillDGV1();
			}
		}
	}

	private void CmdMoveOriginToActive_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Interaction.MsgBox(Localization.LocMSG(42), MsgBoxStyle.YesNo) == MsgBoxResult.Yes && !Information.IsNothing(Public2.PRJ.TX) && DGV1.CurrentCell.RowIndex >= 0)
		{
			Public2.PRJ.SETT.OriginCoordWM = Public2.PRJ.TX[DGV1.CurrentCell.RowIndex].CoordWM;
			CalcCoords.ConvWebMercMetersToWGS84(Public2.PRJ.SETT.OriginCoordWM, ref Public2.PRJ.SETT.OriginCoordWGS.Lat, ref Public2.PRJ.SETT.OriginCoordWGS.Lon);
			CalcCoords.ConvScaleWebMercMetersToMeters();
			CalcCoords.ConvAllTxWebMercMetersToMeters();
			CalcCoords.ConvAllControlPointsWebMercMetersToMeters();
			CalcCoords.ConvAllVertPlanesWebMercMetersToMeters();
			MyProject.Forms.Form1.PB.Refresh();
			FillDGV1();
		}
	}

	private void tbLengthAttractAntennas_Scroll(object sender, EventArgs e)
	{
		Public2.PRJ.SETT.LengthAttractAntennas = checked((byte)tbLengthAttractAntennas.Value);
	}
}
