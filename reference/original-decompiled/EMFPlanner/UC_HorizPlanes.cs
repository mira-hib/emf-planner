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
public class UC_HorizPlanes : UserControl
{
	private IContainer components;

	private bool FlagOK;

	private Color[] CLR;

	private readonly Image nullImage;

	[field: AccessedThroughProperty("GroupBox1")]
	internal virtual GroupBox GroupBox1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
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

	[field: AccessedThroughProperty("ToolTip1")]
	internal virtual ToolTip ToolTip1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdMoveHorizArea")]
	internal virtual Button cmdMoveHorizArea
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdMoveHorizArea_Click;
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

	[field: AccessedThroughProperty("cmdStart1")]
	internal virtual Button cmdStart1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdStart1_Click;
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

	[field: AccessedThroughProperty("cmdStart0")]
	internal virtual Button cmdStart0
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdStart0_Click;
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

	[field: AccessedThroughProperty("GroupBox3")]
	internal virtual GroupBox GroupBox3
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
			DataGridViewRowPrePaintEventHandler value2 = DGV1_RowPrePaint;
			DataGridViewCellMouseEventHandler value3 = DGV1_CellMouseClick;
			DataGridViewCellEventHandler value4 = DGV1_CellValueChanged;
			DataGridViewRowEventHandler value5 = DGV1_UserAddedRow;
			DataGridViewRowEventHandler value6 = DGV1_UserDeletedRow;
			EventHandler value7 = DGV1_CurrentCellDirtyStateChanged;
			DataGridView dataGridView = field;
			if (dataGridView != null)
			{
				dataGridView.RowPrePaint -= value2;
				dataGridView.CellMouseClick -= value3;
				dataGridView.CellValueChanged -= value4;
				dataGridView.UserAddedRow -= value5;
				dataGridView.UserDeletedRow -= value6;
				dataGridView.CurrentCellDirtyStateChanged -= value7;
			}
			field = value;
			dataGridView = field;
			if (dataGridView != null)
			{
				dataGridView.RowPrePaint += value2;
				dataGridView.CellMouseClick += value3;
				dataGridView.CellValueChanged += value4;
				dataGridView.UserAddedRow += value5;
				dataGridView.UserDeletedRow += value6;
				dataGridView.CurrentCellDirtyStateChanged += value7;
			}
		}
	}

	[field: AccessedThroughProperty("Button1")]
	internal virtual Button Button1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ilICO")]
	internal virtual ImageList ilICO
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdAutoFill1")]
	internal virtual Button cmdAutoFill1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdAutoFill1_Click;
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

	[field: AccessedThroughProperty("cmdSORT")]
	internal virtual Button cmdSORT
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdSORT_Click;
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

	[field: AccessedThroughProperty("cmdAutoFill2")]
	internal virtual Button cmdAutoFill2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdAutoFill2_Click;
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

	[field: AccessedThroughProperty("CD1")]
	internal virtual ColorDialog CD1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdDifferentLineTypes")]
	internal virtual Button cmdDifferentLineTypes
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdDifferentLineTypes_Click;
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

	[field: AccessedThroughProperty("cmdSameColor")]
	internal virtual Button cmdSameColor
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdSameColor_Click;
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

	[field: AccessedThroughProperty("chkCombineHorPlanes")]
	internal virtual CheckBox chkCombineHorPlanes
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkCombineHorPlanes_MouseClick;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("GroupBox4")]
	internal virtual GroupBox GroupBox4
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtLineWidthZO")]
	internal virtual NumericUpDown txtLineWidthZO
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = txtLineWidthZO_ValueChanged;
			NumericUpDown numericUpDown = field;
			if (numericUpDown != null)
			{
				numericUpDown.ValueChanged -= value2;
			}
			field = value;
			numericUpDown = field;
			if (numericUpDown != null)
			{
				numericUpDown.ValueChanged += value2;
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

	[field: AccessedThroughProperty("Label6")]
	internal virtual Label Label6
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtTransparFillZO")]
	internal virtual NumericUpDown txtTransparFillZO
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = txtTransparFillZO_ValueChanged;
			NumericUpDown numericUpDown = field;
			if (numericUpDown != null)
			{
				numericUpDown.ValueChanged -= value2;
			}
			field = value;
			numericUpDown = field;
			if (numericUpDown != null)
			{
				numericUpDown.ValueChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmdWord1")]
	internal virtual Button cmdWord1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdWord1_Click;
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

	[field: AccessedThroughProperty("cmdAutoFill3")]
	internal virtual Button cmdAutoFill3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdAutoFill3_Click;
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

	[field: AccessedThroughProperty("lblWarning")]
	internal virtual Label lblWarning
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = lblWarning_Click;
			Label label = field;
			if (label != null)
			{
				label.Click -= value2;
			}
			field = value;
			label = field;
			if (label != null)
			{
				label.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmdSize")]
	internal virtual Button cmdSize
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdSize_Click;
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

	[field: AccessedThroughProperty("cmdDifferentColors")]
	internal virtual Button cmdDifferentColors
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdDifferentColors_Click;
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

	[field: AccessedThroughProperty("chkCombineHorPlanesByColor")]
	internal virtual CheckBox chkCombineHorPlanesByColor
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkCombineHorPlanesByColor_MouseClick;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmdAutoFill4")]
	internal virtual Button cmdAutoFill4
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdAutoFill4_Click;
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

	[field: AccessedThroughProperty("cmdAddVertPlane")]
	internal virtual Button cmdAddVertPlane
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdAddVertPlane_Click;
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

	[field: AccessedThroughProperty("ContWord")]
	internal virtual ContextMenuStrip ContWord
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdHorizontalCutsА4_1")]
	internal virtual ToolStripMenuItem cmdHorizontalCutsА4_1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdHorizontalCutsА4_1_Click;
			ToolStripMenuItem toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			field = value;
			toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmdHorizontalCutsА3_1")]
	internal virtual ToolStripMenuItem cmdHorizontalCutsА3_1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdHorizontalCutsА3_1_Click;
			ToolStripMenuItem toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			field = value;
			toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	[field: AccessedThroughProperty("DataGridViewCheckBoxColumn1")]
	internal virtual DataGridViewCheckBoxColumn DataGridViewCheckBoxColumn1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("DataGridViewTextBoxColumn1")]
	internal virtual DataGridViewTextBoxColumn DataGridViewTextBoxColumn1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("DataGridViewTextBoxColumn2")]
	internal virtual DataGridViewTextBoxColumn DataGridViewTextBoxColumn2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Column7")]
	internal virtual DataGridViewImageColumn Column7
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

	[field: AccessedThroughProperty("chkLimit3")]
	internal virtual CheckBox chkLimit3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkLimit3_MouseClick;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("chkLimit2")]
	internal virtual CheckBox chkLimit2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkLimit2_MouseClick;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("chkLimit1")]
	internal virtual CheckBox chkLimit1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkLimit1_MouseClick;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("chkLimit0")]
	internal virtual CheckBox chkLimit0
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkLimit0_MouseClick;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmdCardinalPointsChart")]
	internal virtual ToolStripMenuItem cmdCardinalPointsChart
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdCardinalPointsChart_Click;
			ToolStripMenuItem toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click -= value2;
			}
			field = value;
			toolStripMenuItem = field;
			if (toolStripMenuItem != null)
			{
				toolStripMenuItem.Click += value2;
			}
		}
	}

	public UC_HorizPlanes()
	{
		Load += UC_HorizPlanes_Load;
		CLR = new Color[20]
		{
			Color.Red,
			Color.Yellow,
			Color.Green,
			Color.SaddleBrown,
			Color.Purple,
			Color.Aqua,
			Color.Chartreuse,
			Color.DarkOrange,
			Color.DeepPink,
			Color.Firebrick,
			Color.Khaki,
			Color.LightSeaGreen,
			Color.LightSkyBlue,
			Color.Olive,
			Color.Gold,
			Color.DarkKhaki,
			Color.Indigo,
			Color.MistyRose,
			Color.DarkSeaGreen,
			Color.DodgerBlue
		};
		nullImage = new Bitmap(1, 1);
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EMFPlanner.UC_HorizPlanes));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		this.GroupBox1 = new System.Windows.Forms.GroupBox();
		this.GroupBox2 = new System.Windows.Forms.GroupBox();
		this.chkLimit3 = new System.Windows.Forms.CheckBox();
		this.chkLimit2 = new System.Windows.Forms.CheckBox();
		this.chkLimit1 = new System.Windows.Forms.CheckBox();
		this.chkLimit0 = new System.Windows.Forms.CheckBox();
		this.cmdAddVertPlane = new System.Windows.Forms.Button();
		this.cmdAutoFill4 = new System.Windows.Forms.Button();
		this.cmdDifferentColors = new System.Windows.Forms.Button();
		this.cmdSize = new System.Windows.Forms.Button();
		this.cmdAutoFill3 = new System.Windows.Forms.Button();
		this.cmdWord1 = new System.Windows.Forms.Button();
		this.GroupBox4 = new System.Windows.Forms.GroupBox();
		this.txtLineWidthZO = new System.Windows.Forms.NumericUpDown();
		this.Label1 = new System.Windows.Forms.Label();
		this.Label6 = new System.Windows.Forms.Label();
		this.txtTransparFillZO = new System.Windows.Forms.NumericUpDown();
		this.cmdDifferentLineTypes = new System.Windows.Forms.Button();
		this.cmdSameColor = new System.Windows.Forms.Button();
		this.cmdAutoFill2 = new System.Windows.Forms.Button();
		this.cmdAutoFill1 = new System.Windows.Forms.Button();
		this.cmdSORT = new System.Windows.Forms.Button();
		this.GroupBox3 = new System.Windows.Forms.GroupBox();
		this.chkCombineHorPlanesByColor = new System.Windows.Forms.CheckBox();
		this.lblWarning = new System.Windows.Forms.Label();
		this.chkCombineHorPlanes = new System.Windows.Forms.CheckBox();
		this.DGV1 = new System.Windows.Forms.DataGridView();
		this.DataGridViewCheckBoxColumn1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.DataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.DataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column7 = new System.Windows.Forms.DataGridViewImageColumn();
		this.Button1 = new System.Windows.Forms.Button();
		this.cmdStart1 = new System.Windows.Forms.Button();
		this.cmdStart0 = new System.Windows.Forms.Button();
		this.cmdMoveHorizArea = new System.Windows.Forms.Button();
		this.cmdPosition = new System.Windows.Forms.Button();
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.ilICO = new System.Windows.Forms.ImageList(this.components);
		this.CD1 = new System.Windows.Forms.ColorDialog();
		this.ContWord = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.cmdHorizontalCutsА4_1 = new System.Windows.Forms.ToolStripMenuItem();
		this.cmdHorizontalCutsА3_1 = new System.Windows.Forms.ToolStripMenuItem();
		this.cmdCardinalPointsChart = new System.Windows.Forms.ToolStripMenuItem();
		this.GroupBox1.SuspendLayout();
		this.GroupBox2.SuspendLayout();
		this.GroupBox4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtLineWidthZO).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTransparFillZO).BeginInit();
		this.GroupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.DGV1).BeginInit();
		this.ContWord.SuspendLayout();
		base.SuspendLayout();
		this.GroupBox1.Controls.Add(this.GroupBox2);
		this.GroupBox1.Controls.Add(this.cmdAddVertPlane);
		this.GroupBox1.Controls.Add(this.cmdAutoFill4);
		this.GroupBox1.Controls.Add(this.cmdDifferentColors);
		this.GroupBox1.Controls.Add(this.cmdSize);
		this.GroupBox1.Controls.Add(this.cmdAutoFill3);
		this.GroupBox1.Controls.Add(this.cmdWord1);
		this.GroupBox1.Controls.Add(this.GroupBox4);
		this.GroupBox1.Controls.Add(this.cmdDifferentLineTypes);
		this.GroupBox1.Controls.Add(this.cmdSameColor);
		this.GroupBox1.Controls.Add(this.cmdAutoFill2);
		this.GroupBox1.Controls.Add(this.cmdAutoFill1);
		this.GroupBox1.Controls.Add(this.cmdSORT);
		this.GroupBox1.Controls.Add(this.GroupBox3);
		this.GroupBox1.Controls.Add(this.cmdStart1);
		this.GroupBox1.Controls.Add(this.cmdStart0);
		this.GroupBox1.Controls.Add(this.cmdMoveHorizArea);
		this.GroupBox1.Controls.Add(this.cmdPosition);
		this.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GroupBox1.Location = new System.Drawing.Point(0, 0);
		this.GroupBox1.Name = "GroupBox1";
		this.GroupBox1.Size = new System.Drawing.Size(350, 696);
		this.GroupBox1.TabIndex = 1;
		this.GroupBox1.TabStop = false;
		this.GroupBox1.Text = "Horizontal Cuts";
		this.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GroupBox2.Controls.Add(this.chkLimit3);
		this.GroupBox2.Controls.Add(this.chkLimit2);
		this.GroupBox2.Controls.Add(this.chkLimit1);
		this.GroupBox2.Controls.Add(this.chkLimit0);
		this.GroupBox2.Location = new System.Drawing.Point(3, 577);
		this.GroupBox2.Name = "GroupBox2";
		this.GroupBox2.Size = new System.Drawing.Size(344, 53);
		this.GroupBox2.TabIndex = 201;
		this.GroupBox2.TabStop = false;
		this.GroupBox2.Text = "Use EMF Limits for calculations";
		this.chkLimit3.AutoSize = true;
		this.chkLimit3.Location = new System.Drawing.Point(256, 23);
		this.chkLimit3.Name = "chkLimit3";
		this.chkLimit3.Size = new System.Drawing.Size(61, 17);
		this.chkLimit3.TabIndex = 3;
		this.chkLimit3.TabStop = false;
		this.chkLimit3.Text = "Limits 4";
		this.chkLimit3.UseVisualStyleBackColor = true;
		this.chkLimit2.AutoSize = true;
		this.chkLimit2.Location = new System.Drawing.Point(173, 23);
		this.chkLimit2.Name = "chkLimit2";
		this.chkLimit2.Size = new System.Drawing.Size(61, 17);
		this.chkLimit2.TabIndex = 2;
		this.chkLimit2.TabStop = false;
		this.chkLimit2.Text = "Limits 3";
		this.chkLimit2.UseVisualStyleBackColor = true;
		this.chkLimit1.AutoSize = true;
		this.chkLimit1.Location = new System.Drawing.Point(90, 23);
		this.chkLimit1.Name = "chkLimit1";
		this.chkLimit1.Size = new System.Drawing.Size(61, 17);
		this.chkLimit1.TabIndex = 1;
		this.chkLimit1.TabStop = false;
		this.chkLimit1.Text = "Limits 2";
		this.chkLimit1.UseVisualStyleBackColor = true;
		this.chkLimit0.AutoSize = true;
		this.chkLimit0.Location = new System.Drawing.Point(7, 23);
		this.chkLimit0.Name = "chkLimit0";
		this.chkLimit0.Size = new System.Drawing.Size(61, 17);
		this.chkLimit0.TabIndex = 0;
		this.chkLimit0.TabStop = false;
		this.chkLimit0.Text = "Limits 1";
		this.chkLimit0.UseVisualStyleBackColor = true;
		this.cmdAddVertPlane.BackColor = System.Drawing.SystemColors.Control;
		this.cmdAddVertPlane.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdAddVertPlane.Image = (System.Drawing.Image)resources.GetObject("cmdAddVertPlane.Image");
		this.cmdAddVertPlane.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdAddVertPlane.Location = new System.Drawing.Point(301, 23);
		this.cmdAddVertPlane.Name = "cmdAddVertPlane";
		this.cmdAddVertPlane.Size = new System.Drawing.Size(26, 26);
		this.cmdAddVertPlane.TabIndex = 197;
		this.cmdAddVertPlane.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdAddVertPlane, "Set vertical cut along the ruler (if the ruler is active)");
		this.cmdAddVertPlane.UseVisualStyleBackColor = false;
		this.cmdAutoFill4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdAutoFill4.Image = (System.Drawing.Image)resources.GetObject("cmdAutoFill4.Image");
		this.cmdAutoFill4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdAutoFill4.Location = new System.Drawing.Point(139, 23);
		this.cmdAutoFill4.Name = "cmdAutoFill4";
		this.cmdAutoFill4.Size = new System.Drawing.Size(26, 26);
		this.cmdAutoFill4.TabIndex = 196;
		this.cmdAutoFill4.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdAutoFill4, "Add to the list of horizontal cuts the heights of the maxima of vertical sections + addition (if the calculation has already been performed in vertical sections)");
		this.cmdAutoFill4.UseVisualStyleBackColor = true;
		this.cmdDifferentColors.BackColor = System.Drawing.SystemColors.Control;
		this.cmdDifferentColors.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdDifferentColors.Image = (System.Drawing.Image)resources.GetObject("cmdDifferentColors.Image");
		this.cmdDifferentColors.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdDifferentColors.Location = new System.Drawing.Point(247, 23);
		this.cmdDifferentColors.Name = "cmdDifferentColors";
		this.cmdDifferentColors.Size = new System.Drawing.Size(26, 26);
		this.cmdDifferentColors.TabIndex = 195;
		this.cmdDifferentColors.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdDifferentColors, "Set different colors for all cuts");
		this.cmdDifferentColors.UseVisualStyleBackColor = false;
		this.cmdSize.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdSize.Image = (System.Drawing.Image)resources.GetObject("cmdSize.Image");
		this.cmdSize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdSize.Location = new System.Drawing.Point(58, 23);
		this.cmdSize.Name = "cmdSize";
		this.cmdSize.Size = new System.Drawing.Size(26, 26);
		this.cmdSize.TabIndex = 194;
		this.cmdSize.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdSize, "Set calculation grid size relative to origin");
		this.cmdSize.UseVisualStyleBackColor = true;
		this.cmdAutoFill3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdAutoFill3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdAutoFill3.Location = new System.Drawing.Point(166, 23);
		this.cmdAutoFill3.Name = "cmdAutoFill3";
		this.cmdAutoFill3.Size = new System.Drawing.Size(26, 26);
		this.cmdAutoFill3.TabIndex = 192;
		this.cmdAutoFill3.TabStop = false;
		this.cmdAutoFill3.Text = "+";
		this.ToolTip1.SetToolTip(this.cmdAutoFill3, "Add cuts with heights in the specified range");
		this.cmdAutoFill3.UseVisualStyleBackColor = true;
		this.cmdWord1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdWord1.Image = (System.Drawing.Image)resources.GetObject("cmdWord1.Image");
		this.cmdWord1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdWord1.Location = new System.Drawing.Point(59, 50);
		this.cmdWord1.Name = "cmdWord1";
		this.cmdWord1.Size = new System.Drawing.Size(26, 26);
		this.cmdWord1.TabIndex = 191;
		this.cmdWord1.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdWord1, "Reports for horizontal cuts");
		this.cmdWord1.UseVisualStyleBackColor = true;
		this.GroupBox4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GroupBox4.Controls.Add(this.txtLineWidthZO);
		this.GroupBox4.Controls.Add(this.Label1);
		this.GroupBox4.Controls.Add(this.Label6);
		this.GroupBox4.Controls.Add(this.txtTransparFillZO);
		this.GroupBox4.Location = new System.Drawing.Point(4, 636);
		this.GroupBox4.Name = "GroupBox4";
		this.GroupBox4.Size = new System.Drawing.Size(342, 54);
		this.GroupBox4.TabIndex = 170;
		this.GroupBox4.TabStop = false;
		this.GroupBox4.Text = "Cuts visualization";
		this.txtLineWidthZO.Location = new System.Drawing.Point(98, 23);
		this.txtLineWidthZO.Maximum = new decimal(new int[4] { 10, 0, 0, 0 });
		this.txtLineWidthZO.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.txtLineWidthZO.Name = "txtLineWidthZO";
		this.txtLineWidthZO.Size = new System.Drawing.Size(45, 20);
		this.txtLineWidthZO.TabIndex = 3;
		this.txtLineWidthZO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtLineWidthZO.Value = new decimal(new int[4] { 2, 0, 0, 0 });
		this.Label1.AutoSize = true;
		this.Label1.Location = new System.Drawing.Point(12, 25);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(75, 13);
		this.Label1.TabIndex = 146;
		this.Label1.Text = "Line thickness";
		this.Label6.AutoSize = true;
		this.Label6.Location = new System.Drawing.Point(197, 25);
		this.Label6.Name = "Label6";
		this.Label6.Size = new System.Drawing.Size(72, 13);
		this.Label6.TabIndex = 164;
		this.Label6.Text = "Transparency";
		this.txtTransparFillZO.Location = new System.Drawing.Point(282, 23);
		this.txtTransparFillZO.Maximum = new decimal(new int[4] { 10, 0, 0, 0 });
		this.txtTransparFillZO.Name = "txtTransparFillZO";
		this.txtTransparFillZO.Size = new System.Drawing.Size(45, 20);
		this.txtTransparFillZO.TabIndex = 163;
		this.txtTransparFillZO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtTransparFillZO.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		this.cmdDifferentLineTypes.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdDifferentLineTypes.Image = (System.Drawing.Image)resources.GetObject("cmdDifferentLineTypes.Image");
		this.cmdDifferentLineTypes.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdDifferentLineTypes.Location = new System.Drawing.Point(274, 23);
		this.cmdDifferentLineTypes.Name = "cmdDifferentLineTypes";
		this.cmdDifferentLineTypes.Size = new System.Drawing.Size(26, 26);
		this.cmdDifferentLineTypes.TabIndex = 169;
		this.cmdDifferentLineTypes.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdDifferentLineTypes, "Set different line types for all cuts");
		this.cmdDifferentLineTypes.UseVisualStyleBackColor = true;
		this.cmdSameColor.BackColor = System.Drawing.SystemColors.Control;
		this.cmdSameColor.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdSameColor.Image = (System.Drawing.Image)resources.GetObject("cmdSameColor.Image");
		this.cmdSameColor.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdSameColor.Location = new System.Drawing.Point(220, 23);
		this.cmdSameColor.Name = "cmdSameColor";
		this.cmdSameColor.Size = new System.Drawing.Size(26, 26);
		this.cmdSameColor.TabIndex = 168;
		this.cmdSameColor.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdSameColor, "Set one color for the selected cuts (the color is taken first in the selected list)");
		this.cmdSameColor.UseVisualStyleBackColor = false;
		this.cmdAutoFill2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdAutoFill2.Image = (System.Drawing.Image)resources.GetObject("cmdAutoFill2.Image");
		this.cmdAutoFill2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdAutoFill2.Location = new System.Drawing.Point(112, 23);
		this.cmdAutoFill2.Name = "cmdAutoFill2";
		this.cmdAutoFill2.Size = new System.Drawing.Size(26, 26);
		this.cmdAutoFill2.TabIndex = 167;
		this.cmdAutoFill2.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdAutoFill2, "Add to the list of horizontal cuts the heights of the maxima of vertical sections (if a calculation has already been performed in vertical cuts)");
		this.cmdAutoFill2.UseVisualStyleBackColor = true;
		this.cmdAutoFill1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdAutoFill1.Image = (System.Drawing.Image)resources.GetObject("cmdAutoFill1.Image");
		this.cmdAutoFill1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdAutoFill1.Location = new System.Drawing.Point(85, 23);
		this.cmdAutoFill1.Name = "cmdAutoFill1";
		this.cmdAutoFill1.Size = new System.Drawing.Size(26, 26);
		this.cmdAutoFill1.TabIndex = 166;
		this.cmdAutoFill1.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdAutoFill1, "Fill in the list of horizontal cuts with the heights of antennas of active radiating sources");
		this.cmdAutoFill1.UseVisualStyleBackColor = true;
		this.cmdSORT.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdSORT.Image = (System.Drawing.Image)resources.GetObject("cmdSORT.Image");
		this.cmdSORT.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdSORT.Location = new System.Drawing.Point(193, 23);
		this.cmdSORT.Name = "cmdSORT";
		this.cmdSORT.Size = new System.Drawing.Size(26, 26);
		this.cmdSORT.TabIndex = 165;
		this.cmdSORT.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdSORT, "Sort the list of cuts by height");
		this.cmdSORT.UseVisualStyleBackColor = true;
		this.GroupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GroupBox3.Controls.Add(this.chkCombineHorPlanesByColor);
		this.GroupBox3.Controls.Add(this.lblWarning);
		this.GroupBox3.Controls.Add(this.chkCombineHorPlanes);
		this.GroupBox3.Controls.Add(this.DGV1);
		this.GroupBox3.Controls.Add(this.Button1);
		this.GroupBox3.Location = new System.Drawing.Point(3, 82);
		this.GroupBox3.Name = "GroupBox3";
		this.GroupBox3.Padding = new System.Windows.Forms.Padding(3, 5, 3, 3);
		this.GroupBox3.Size = new System.Drawing.Size(344, 489);
		this.GroupBox3.TabIndex = 164;
		this.GroupBox3.TabStop = false;
		this.GroupBox3.Text = "Parameters of horizontal cuts";
		this.chkCombineHorPlanesByColor.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.chkCombineHorPlanesByColor.AutoSize = true;
		this.chkCombineHorPlanesByColor.Location = new System.Drawing.Point(6, 467);
		this.chkCombineHorPlanesByColor.Name = "chkCombineHorPlanesByColor";
		this.chkCombineHorPlanesByColor.Size = new System.Drawing.Size(178, 17);
		this.chkCombineHorPlanesByColor.TabIndex = 148;
		this.chkCombineHorPlanesByColor.Text = "Merge cuts with the same colors";
		this.chkCombineHorPlanesByColor.UseVisualStyleBackColor = true;
		this.lblWarning.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.lblWarning.AutoSize = true;
		this.lblWarning.Cursor = System.Windows.Forms.Cursors.Hand;
		this.lblWarning.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, 204);
		this.lblWarning.ForeColor = System.Drawing.Color.Navy;
		this.lblWarning.Location = new System.Drawing.Point(317, 445);
		this.lblWarning.Name = "lblWarning";
		this.lblWarning.Size = new System.Drawing.Size(13, 13);
		this.lblWarning.TabIndex = 147;
		this.lblWarning.Text = "?";
		this.chkCombineHorPlanes.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.chkCombineHorPlanes.AutoSize = true;
		this.chkCombineHorPlanes.Location = new System.Drawing.Point(6, 445);
		this.chkCombineHorPlanes.Name = "chkCombineHorPlanes";
		this.chkCombineHorPlanes.Size = new System.Drawing.Size(290, 17);
		this.chkCombineHorPlanes.TabIndex = 3;
		this.chkCombineHorPlanes.Text = "Make a projection of the lower cuts onto the upper ones";
		this.chkCombineHorPlanes.UseVisualStyleBackColor = true;
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
		this.DGV1.Columns.AddRange(this.DataGridViewCheckBoxColumn1, this.DataGridViewTextBoxColumn1, this.DataGridViewTextBoxColumn2, this.Column7);
		this.DGV1.Location = new System.Drawing.Point(3, 18);
		this.DGV1.Name = "DGV1";
		this.DGV1.RowHeadersWidth = 25;
		this.DGV1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.DGV1.RowTemplate.Height = 18;
		this.DGV1.Size = new System.Drawing.Size(338, 413);
		this.DGV1.TabIndex = 2;
		this.DGV1.TabStop = false;
		this.DataGridViewCheckBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
		this.DataGridViewCheckBoxColumn1.HeaderText = "";
		this.DataGridViewCheckBoxColumn1.Name = "DataGridViewCheckBoxColumn1";
		this.DataGridViewCheckBoxColumn1.Width = 21;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle2.NullValue = "0";
		this.DataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle2;
		this.DataGridViewTextBoxColumn1.HeaderText = "Cut height, m";
		this.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1";
		this.DataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.DataGridViewTextBoxColumn1.Width = 70;
		this.DataGridViewTextBoxColumn2.HeaderText = "Line color";
		this.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2";
		this.DataGridViewTextBoxColumn2.ReadOnly = true;
		this.DataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.DataGridViewTextBoxColumn2.Width = 45;
		this.Column7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		this.Column7.DefaultCellStyle = dataGridViewCellStyle3;
		this.Column7.HeaderText = "Line type";
		this.Column7.Name = "Column7";
		this.Column7.ReadOnly = true;
		this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.Button1.Location = new System.Drawing.Point(0, 2000);
		this.Button1.Name = "Button1";
		this.Button1.Size = new System.Drawing.Size(51, 21);
		this.Button1.TabIndex = 1;
		this.Button1.Text = "refresh";
		this.Button1.UseVisualStyleBackColor = true;
		this.cmdStart1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdStart1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
		this.cmdStart1.Image = (System.Drawing.Image)resources.GetObject("cmdStart1.Image");
		this.cmdStart1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdStart1.Location = new System.Drawing.Point(32, 50);
		this.cmdStart1.Name = "cmdStart1";
		this.cmdStart1.Size = new System.Drawing.Size(26, 26);
		this.cmdStart1.TabIndex = 161;
		this.cmdStart1.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdStart1, "Calculation of horizontal cuts with grid 2");
		this.cmdStart1.UseVisualStyleBackColor = true;
		this.cmdStart0.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdStart0.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
		this.cmdStart0.Image = (System.Drawing.Image)resources.GetObject("cmdStart0.Image");
		this.cmdStart0.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdStart0.Location = new System.Drawing.Point(5, 50);
		this.cmdStart0.Name = "cmdStart0";
		this.cmdStart0.Size = new System.Drawing.Size(26, 26);
		this.cmdStart0.TabIndex = 160;
		this.cmdStart0.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdStart0, "Calculation of horizontal cuts with grid 1");
		this.cmdStart0.UseVisualStyleBackColor = true;
		this.cmdMoveHorizArea.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdMoveHorizArea.Image = (System.Drawing.Image)resources.GetObject("cmdMoveHorizArea.Image");
		this.cmdMoveHorizArea.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdMoveHorizArea.Location = new System.Drawing.Point(31, 23);
		this.cmdMoveHorizArea.Name = "cmdMoveHorizArea";
		this.cmdMoveHorizArea.Size = new System.Drawing.Size(26, 26);
		this.cmdMoveHorizArea.TabIndex = 159;
		this.cmdMoveHorizArea.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdMoveHorizArea, "Move the calculation grid to the center of the screen");
		this.cmdMoveHorizArea.UseVisualStyleBackColor = true;
		this.cmdPosition.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdPosition.Image = (System.Drawing.Image)resources.GetObject("cmdPosition.Image");
		this.cmdPosition.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdPosition.Location = new System.Drawing.Point(4, 23);
		this.cmdPosition.Name = "cmdPosition";
		this.cmdPosition.Size = new System.Drawing.Size(26, 26);
		this.cmdPosition.TabIndex = 156;
		this.cmdPosition.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdPosition, "Display the calculation grid in the center of the screen");
		this.cmdPosition.UseVisualStyleBackColor = true;
		this.ilICO.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ilICO.ImageStream");
		this.ilICO.TransparentColor = System.Drawing.Color.Transparent;
		this.ilICO.Images.SetKeyName(0, "0.bmp");
		this.ilICO.Images.SetKeyName(1, "1.bmp");
		this.ilICO.Images.SetKeyName(2, "2.bmp");
		this.ilICO.Images.SetKeyName(3, "3.bmp");
		this.ilICO.Images.SetKeyName(4, "4.bmp");
		this.ilICO.Images.SetKeyName(5, "5.bmp");
		this.ilICO.Images.SetKeyName(6, "6.bmp");
		this.ilICO.Images.SetKeyName(7, "7.bmp");
		this.ilICO.Images.SetKeyName(8, "8.bmp");
		this.ilICO.Images.SetKeyName(9, "9.bmp");
		this.ilICO.Images.SetKeyName(10, "10.bmp");
		this.ilICO.Images.SetKeyName(11, "11.bmp");
		this.ilICO.Images.SetKeyName(12, "12.bmp");
		this.ilICO.Images.SetKeyName(13, "13.bmp");
		this.ilICO.Images.SetKeyName(14, "14.bmp");
		this.ilICO.Images.SetKeyName(15, "15.bmp");
		this.ilICO.Images.SetKeyName(16, "16.bmp");
		this.ilICO.Images.SetKeyName(17, "17.bmp");
		this.ilICO.Images.SetKeyName(18, "18.bmp");
		this.ilICO.Images.SetKeyName(19, "19.bmp");
		this.ilICO.Images.SetKeyName(20, "20.bmp");
		this.ilICO.Images.SetKeyName(21, "21.bmp");
		this.ilICO.Images.SetKeyName(22, "22.bmp");
		this.ilICO.Images.SetKeyName(23, "23.bmp");
		this.ilICO.Images.SetKeyName(24, "24.bmp");
		this.ilICO.Images.SetKeyName(25, "25.bmp");
		this.ilICO.Images.SetKeyName(26, "26.bmp");
		this.ilICO.Images.SetKeyName(27, "27.bmp");
		this.ContWord.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.cmdHorizontalCutsА4_1, this.cmdHorizontalCutsА3_1, this.cmdCardinalPointsChart });
		this.ContWord.Name = "cmsDBW";
		this.ContWord.Size = new System.Drawing.Size(302, 70);
		this.cmdHorizontalCutsА4_1.Name = "cmdHorizontalCutsА4_1";
		this.cmdHorizontalCutsА4_1.Size = new System.Drawing.Size(301, 22);
		this.cmdHorizontalCutsА4_1.Text = "Horizontal cuts A4 {HorizontalCutsА4_1}";
		this.cmdHorizontalCutsА3_1.Name = "cmdHorizontalCutsА3_1";
		this.cmdHorizontalCutsА3_1.Size = new System.Drawing.Size(301, 22);
		this.cmdHorizontalCutsА3_1.Text = "Horizontal cuts A3 {HorizontalCutsА3_1}";
		this.cmdCardinalPointsChart.Name = "cmdCardinalPointsChart";
		this.cmdCardinalPointsChart.Size = new System.Drawing.Size(301, 22);
		this.cmdCardinalPointsChart.Text = "Cardinal points chart {CardinalPointsChart}";
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		base.Controls.Add(this.GroupBox1);
		this.MinimumSize = new System.Drawing.Size(250, 0);
		base.Name = "UC_HorizPlanes";
		base.Size = new System.Drawing.Size(350, 696);
		this.GroupBox1.ResumeLayout(false);
		this.GroupBox2.ResumeLayout(false);
		this.GroupBox2.PerformLayout();
		this.GroupBox4.ResumeLayout(false);
		this.GroupBox4.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtLineWidthZO).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTransparFillZO).EndInit();
		this.GroupBox3.ResumeLayout(false);
		this.GroupBox3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.DGV1).EndInit();
		this.ContWord.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void UC_HorizPlanes_Load(object sender, EventArgs e)
	{
		FillDGV1();
		ShowColomnsDGV1();
		Public2.PRJ.SETT.HorAreaCoordM[0] = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.SETT.HorAreaCoordWM[0]);
		Public2.PRJ.SETT.HorAreaCoordM[1] = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.SETT.HorAreaCoordWM[1]);
		FlagOK = false;
		chkCombineHorPlanes.Checked = Public2.PRJ.SETT.CombineHorPlanes;
		chkCombineHorPlanesByColor.Checked = Public2.PRJ.SETT.CombineHorPlanesByColor;
		txtTransparFillZO.Value = new decimal(Public2.PRJ.SETT.TransparFillZO);
		txtLineWidthZO.Value = new decimal(Public2.PRJ.SETT.LineWidthZO);
		FlagOK = true;
		chkLimit0.Checked = Public2.PRJ.SETT.HorizCutLimits[0];
		chkLimit1.Checked = Public2.PRJ.SETT.HorizCutLimits[1];
		chkLimit2.Checked = Public2.PRJ.SETT.HorizCutLimits[2];
		chkLimit3.Checked = Public2.PRJ.SETT.HorizCutLimits[3];
		chkLimit0.ForeColor = Public2.PRJ.SETT.LimitColor[0];
		chkLimit1.ForeColor = Public2.PRJ.SETT.LimitColor[1];
		chkLimit2.ForeColor = Public2.PRJ.SETT.LimitColor[2];
		chkLimit3.ForeColor = Public2.PRJ.SETT.LimitColor[3];
		MyProject.Forms.Form1.DrawPIC();
	}

	private void cmdPosition_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		double xwm = (Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm + Public2.PRJ.SETT.HorAreaCoordWM[1].Xwm) / 2.0;
		double ywm = (Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm + Public2.PRJ.SETT.HorAreaCoordWM[1].Ywm) / 2.0;
		MyProject.Forms.Form1.MapPositionWM(xwm, ywm);
	}

	private void cmdMoveHorizArea_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Interaction.MsgBox(Localization.LocMSG(59), MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
		{
			Public2.CoordWebMerc screenCenterWM = Form1_Public.GetScreenCenterWM();
			Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm = screenCenterWM.Xwm - 50.0;
			Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm = screenCenterWM.Ywm - 50.0;
			Public2.PRJ.SETT.HorAreaCoordWM[1].Xwm = screenCenterWM.Xwm + 50.0;
			Public2.PRJ.SETT.HorAreaCoordWM[1].Ywm = screenCenterWM.Ywm + 50.0;
			Public2.PRJ.SETT.HorAreaCoordM[0] = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.SETT.HorAreaCoordWM[0]);
			Public2.PRJ.SETT.HorAreaCoordM[1] = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.SETT.HorAreaCoordWM[1]);
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void cmdSize_Click(object sender, EventArgs e)
	{
		Form1_Public.AreaWidth1 = (float)Math.Round(Math.Abs(Public2.PRJ.SETT.HorAreaCoordM[0].Xm));
		Form1_Public.AreaWidth2 = (float)Math.Round(Math.Abs(Public2.PRJ.SETT.HorAreaCoordM[1].Xm));
		Form1_Public.AreaHeight1 = (float)Math.Round(Math.Abs(Public2.PRJ.SETT.HorAreaCoordM[1].Ym));
		Form1_Public.AreaHeight2 = (float)Math.Round(Math.Abs(Public2.PRJ.SETT.HorAreaCoordM[0].Ym));
		GroupBox1.Focus();
		MyProject.Forms.frmAreaSize.ShowDialog();
		if (Form1_Public.AreaSizeOK)
		{
			Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm = Public2.PRJ.SETT.OriginCoordWM.Xwm - (double)Form1_Public.AreaWidth1 / CalcCoords.CalcMashtWMtoMeters() - 0.1;
			Public2.PRJ.SETT.HorAreaCoordWM[1].Xwm = Public2.PRJ.SETT.OriginCoordWM.Xwm + (double)Form1_Public.AreaWidth2 / CalcCoords.CalcMashtWMtoMeters() + 0.1;
			Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm = Public2.PRJ.SETT.OriginCoordWM.Ywm - (double)Form1_Public.AreaHeight2 / CalcCoords.CalcMashtWMtoMeters() - 0.1;
			Public2.PRJ.SETT.HorAreaCoordWM[1].Ywm = Public2.PRJ.SETT.OriginCoordWM.Ywm + (double)Form1_Public.AreaHeight1 / CalcCoords.CalcMashtWMtoMeters() + 0.1;
			Public2.PRJ.SETT.HorAreaCoordM[0] = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.SETT.HorAreaCoordWM[0]);
			Public2.PRJ.SETT.HorAreaCoordM[1] = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.SETT.HorAreaCoordWM[1]);
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void cmdAutoFill1_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		DGV1.EndEdit();
		if (!CalcSZZ.CalcTmp() || Interaction.MsgBox(Localization.LocMSG(60), MsgBoxStyle.YesNo) != MsgBoxResult.Yes)
		{
			return;
		}
		if (Information.IsNothing(Public2.PRJ.TX))
		{
			DGV1.Rows.Clear();
			Public2.PRJ.HP = null;
			return;
		}
		float originHeihgt = Module1.GetOriginHeihgt();
		short num = -1;
		int upperBound = Public2.PRJ.TX.GetUpperBound(0);
		checked
		{
			float[] array = default;
			for (int i = 0; i <= upperBound; i++)
			{
				if (Public2.PRJ.TX[i].Active)
				{
					num++;
					array = (float[])Utils.CopyArray(array, new float[num + 1]);
					array[num] = (float)Math.Round(CalcSZZ.TMP[i].H - originHeihgt, 1);
				}
			}
			if (num == -1)
			{
				DGV1.Rows.Clear();
				Public2.PRJ.HP = null;
				return;
			}
			int num2 = array.GetUpperBound(0) - 1;
			for (int j = 0; j <= num2; j++)
			{
				int num3 = j + 1;
				int upperBound2 = array.GetUpperBound(0);
				for (int k = num3; k <= upperBound2; k++)
				{
					if (array[k] < array[j])
					{
						float num4 = array[j];
						array[j] = array[k];
						array[k] = num4;
					}
				}
			}
			num = 0;
			int upperBound3 = array.GetUpperBound(0);
			for (int l = 1; l <= upperBound3; l++)
			{
				if (array[l] > array[l - 1])
				{
					num++;
					array[num] = array[l];
				}
			}
			array = (float[])Utils.CopyArray(array, new float[num + 1]);
			Public2.PRJ.HP = new Public2.StructHorizPlane[array.GetUpperBound(0) + 1];
			short num5 = 0;
			int upperBound4 = array.GetUpperBound(0);
			for (int m = 0; m <= upperBound4; m++)
			{
				if (num5 == CLR.GetUpperBound(0) + 1)
				{
					num5 = 0;
				}
				Public2.PRJ.HP[m].Activ = true;
				Public2.PRJ.HP[m].Height = array[m];
				Public2.PRJ.HP[m].Color = CLR[num5];
				Public2.PRJ.HP[m].LineType = 0;
				num5++;
			}
			FillDGV1();
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void cmdAutoFill2_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		DGV1.EndEdit();
		if (Interaction.MsgBox(Localization.LocMSG(61), MsgBoxStyle.YesNo) != MsgBoxResult.Yes)
		{
			return;
		}
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.HP))
			{
				int upperBound = Public2.PRJ.HP.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					Public2.PRJ.HP[i].PLNwm = null;
				}
			}
			if (!Information.IsNothing(Public2.PRJ.VP))
			{
				float originHeihgt = Module1.GetOriginHeihgt();
				int upperBound2 = Public2.PRJ.VP.GetUpperBound(0);
				for (int j = 0; j <= upperBound2; j++)
				{
					if (!(Public2.PRJ.VP[j].Activ & (Public2.PRJ.VP[0].LastL1 > 0f)) || Public2.PRJ.VP[j].HeightMaxZO == 9999f)
					{
						continue;
					}
					float num = (float)Math.Round(Public2.PRJ.VP[j].HeightMaxZO + Public2.PRJ.VP[j].Elev0 - originHeihgt, 1);
					bool flag = false;
					if (!Information.IsNothing(Public2.PRJ.HP))
					{
						int upperBound3 = Public2.PRJ.HP.GetUpperBound(0);
						for (int k = 0; k <= upperBound3; k++)
						{
							if (Public2.PRJ.HP[k].Height == num)
							{
								flag = true;
								break;
							}
						}
					}
					if (!flag)
					{
						if (Information.IsNothing(Public2.PRJ.HP))
						{
							Public2.PRJ.HP = new Public2.StructHorizPlane[1];
						}
						else
						{
							ref Public2.StructHorizPlane[] hP = ref Public2.PRJ.HP;
							hP = (Public2.StructHorizPlane[])Utils.CopyArray(hP, new Public2.StructHorizPlane[Public2.PRJ.HP.GetUpperBound(0) + 1 + 1]);
						}
						Public2.PRJ.HP[Public2.PRJ.HP.GetUpperBound(0)].Activ = true;
						Public2.PRJ.HP[Public2.PRJ.HP.GetUpperBound(0)].Height = num;
						Public2.PRJ.HP[Public2.PRJ.HP.GetUpperBound(0)].Color = Color.Red;
						Public2.PRJ.HP[Public2.PRJ.HP.GetUpperBound(0)].LineType = 0;
					}
				}
			}
			if (Information.IsNothing(Public2.PRJ.HP))
			{
				return;
			}
			int num2 = Public2.PRJ.HP.GetUpperBound(0) - 1;
			for (int l = 0; l <= num2; l++)
			{
				int num3 = l + 1;
				int upperBound4 = Public2.PRJ.HP.GetUpperBound(0);
				for (int m = num3; m <= upperBound4; m++)
				{
					if (Public2.PRJ.HP[m].Height < Public2.PRJ.HP[l].Height)
					{
						Public2.StructHorizPlane structHorizPlane = Module1.DeepClone(Public2.PRJ.HP[l]);
						Public2.PRJ.HP[l] = Module1.DeepClone(Public2.PRJ.HP[m]);
						Public2.PRJ.HP[m] = structHorizPlane;
					}
				}
			}
			FillDGV1();
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void CmdAutoFill4_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		DGV1.EndEdit();
		MyProject.Forms.frmHeight05.ShowDialog();
		if (!Form1_Public.Height05OK)
		{
			return;
		}
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.HP))
			{
				int upperBound = Public2.PRJ.HP.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					Public2.PRJ.HP[i].PLNwm = null;
				}
			}
			if (!Information.IsNothing(Public2.PRJ.VP))
			{
				float originHeihgt = Module1.GetOriginHeihgt();
				int upperBound2 = Public2.PRJ.VP.GetUpperBound(0);
				for (int j = 0; j <= upperBound2; j++)
				{
					if (!(Public2.PRJ.VP[j].Activ & (Public2.PRJ.VP[0].LastL1 > 0f)) || Public2.PRJ.VP[j].HeightBottomZO == 9999f)
					{
						continue;
					}
					float num = (float)Math.Round(Public2.PRJ.VP[j].HeightBottomZO + Public2.PRJ.VP[j].Elev0 - originHeihgt + Form1_Public.HeightDH05, 1);
					bool flag = false;
					if (!Information.IsNothing(Public2.PRJ.HP))
					{
						int upperBound3 = Public2.PRJ.HP.GetUpperBound(0);
						for (int k = 0; k <= upperBound3; k++)
						{
							if (Public2.PRJ.HP[k].Height == num)
							{
								flag = true;
								break;
							}
						}
					}
					if (!flag)
					{
						if (Information.IsNothing(Public2.PRJ.HP))
						{
							Public2.PRJ.HP = new Public2.StructHorizPlane[1];
						}
						else
						{
							ref Public2.StructHorizPlane[] hP = ref Public2.PRJ.HP;
							hP = (Public2.StructHorizPlane[])Utils.CopyArray(hP, new Public2.StructHorizPlane[Public2.PRJ.HP.GetUpperBound(0) + 1 + 1]);
						}
						Public2.PRJ.HP[Public2.PRJ.HP.GetUpperBound(0)].Activ = true;
						Public2.PRJ.HP[Public2.PRJ.HP.GetUpperBound(0)].Height = num;
						Public2.PRJ.HP[Public2.PRJ.HP.GetUpperBound(0)].Color = Color.Red;
						Public2.PRJ.HP[Public2.PRJ.HP.GetUpperBound(0)].LineType = 0;
					}
				}
			}
			if (Information.IsNothing(Public2.PRJ.HP))
			{
				return;
			}
			int num2 = Public2.PRJ.HP.GetUpperBound(0) - 1;
			for (int l = 0; l <= num2; l++)
			{
				int num3 = l + 1;
				int upperBound4 = Public2.PRJ.HP.GetUpperBound(0);
				for (int m = num3; m <= upperBound4; m++)
				{
					if (Public2.PRJ.HP[m].Height < Public2.PRJ.HP[l].Height)
					{
						Public2.StructHorizPlane structHorizPlane = Module1.DeepClone(Public2.PRJ.HP[l]);
						Public2.PRJ.HP[l] = Module1.DeepClone(Public2.PRJ.HP[m]);
						Public2.PRJ.HP[m] = structHorizPlane;
					}
				}
			}
			FillDGV1();
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void cmdAutoFill3_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		DGV1.EndEdit();
		MyProject.Forms.frmHeightSpan.ShowDialog();
		if (!Form1_Public.HeightSpanOK || Form1_Public.HeightSpanMax - Form1_Public.HeightSpanMin <= 0f)
		{
			return;
		}
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.HP))
			{
				int upperBound = Public2.PRJ.HP.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					Public2.PRJ.HP[i].PLNwm = null;
				}
			}
			float heightSpanMin = Form1_Public.HeightSpanMin;
			float heightSpanMax = Form1_Public.HeightSpanMax;
			float heightSpanDh = Form1_Public.HeightSpanDh;
			bool flag = heightSpanDh >= 0f;
			for (float num = heightSpanMin; flag ? (num <= heightSpanMax) : (num >= heightSpanMax); num += heightSpanDh)
			{
				float num2 = (float)Math.Round(num, 1);
				bool flag2 = false;
				if (!Information.IsNothing(Public2.PRJ.HP))
				{
					int upperBound2 = Public2.PRJ.HP.GetUpperBound(0);
					for (int j = 0; j <= upperBound2; j++)
					{
						if (Public2.PRJ.HP[j].Height == num2)
						{
							flag2 = true;
							break;
						}
					}
				}
				if (!flag2)
				{
					if (Information.IsNothing(Public2.PRJ.HP))
					{
						Public2.PRJ.HP = new Public2.StructHorizPlane[1];
					}
					else
					{
						ref Public2.StructHorizPlane[] hP = ref Public2.PRJ.HP;
						hP = (Public2.StructHorizPlane[])Utils.CopyArray(hP, new Public2.StructHorizPlane[Public2.PRJ.HP.GetUpperBound(0) + 1 + 1]);
					}
					Public2.PRJ.HP[Public2.PRJ.HP.GetUpperBound(0)].Activ = true;
					Public2.PRJ.HP[Public2.PRJ.HP.GetUpperBound(0)].Height = num2;
					Public2.PRJ.HP[Public2.PRJ.HP.GetUpperBound(0)].Color = Color.Red;
					Public2.PRJ.HP[Public2.PRJ.HP.GetUpperBound(0)].LineType = 0;
				}
			}
			if (Information.IsNothing(Public2.PRJ.HP))
			{
				return;
			}
			int num3 = Public2.PRJ.HP.GetUpperBound(0) - 1;
			for (int k = 0; k <= num3; k++)
			{
				int num4 = k + 1;
				int upperBound3 = Public2.PRJ.HP.GetUpperBound(0);
				for (int l = num4; l <= upperBound3; l++)
				{
					if (Public2.PRJ.HP[l].Height < Public2.PRJ.HP[k].Height)
					{
						Public2.StructHorizPlane structHorizPlane = Module1.DeepClone(Public2.PRJ.HP[k]);
						Public2.PRJ.HP[k] = Module1.DeepClone(Public2.PRJ.HP[l]);
						Public2.PRJ.HP[l] = structHorizPlane;
					}
				}
			}
			FillDGV1();
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void cmdSORT_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Information.IsNothing(Public2.PRJ.HP))
		{
			return;
		}
		checked
		{
			int num = Public2.PRJ.HP.GetUpperBound(0) - 1;
			for (int i = 0; i <= num; i++)
			{
				int num2 = i + 1;
				int upperBound = Public2.PRJ.HP.GetUpperBound(0);
				for (int j = num2; j <= upperBound; j++)
				{
					if (Public2.PRJ.HP[j].Height < Public2.PRJ.HP[i].Height)
					{
						Public2.StructHorizPlane structHorizPlane = Module1.DeepClone(Public2.PRJ.HP[i]);
						Public2.PRJ.HP[i] = Module1.DeepClone(Public2.PRJ.HP[j]);
						Public2.PRJ.HP[j] = structHorizPlane;
					}
				}
			}
			FillDGV1();
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void cmdSameColor_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		checked
		{
			if (Information.IsNothing(Public2.PRJ.HP) || DGV1.SelectedCells.Count < 2 || DGV1.SelectedCells[DGV1.SelectedCells.Count - 1].RowIndex > Public2.PRJ.HP.GetUpperBound(0))
			{
				return;
			}
			int num = DGV1.SelectedCells.Count - 1;
			for (int i = 0; i <= num; i++)
			{
				if (DGV1.SelectedCells[i].RowIndex <= Public2.PRJ.HP.GetUpperBound(0))
				{
					Public2.PRJ.HP[DGV1.SelectedCells[i].RowIndex].Color = DGV1[2, DGV1.SelectedCells[DGV1.SelectedCells.Count - 1].RowIndex].Style.BackColor;
				}
			}
			FillDGV1();
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void CmdDifferentColors_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Information.IsNothing(Public2.PRJ.HP))
		{
			return;
		}
		short num = 0;
		int upperBound = Public2.PRJ.HP.GetUpperBound(0);
		checked
		{
			for (int i = 0; i <= upperBound; i++)
			{
				if (num == CLR.GetUpperBound(0) + 1)
				{
					num = 0;
				}
				Public2.PRJ.HP[i].Color = CLR[num];
				num++;
			}
			FillDGV1();
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void cmdDifferentLineTypes_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Information.IsNothing(Public2.PRJ.HP))
		{
			return;
		}
		int upperBound = Public2.PRJ.HP.GetUpperBound(0);
		checked
		{
			for (int i = 0; i <= upperBound; i++)
			{
				if (i <= Public1.PenDash.GetUpperBound(0))
				{
					Public2.PRJ.HP[i].LineType = (byte)i;
				}
				else
				{
					Public2.PRJ.HP[i].LineType = 0;
				}
			}
			FillDGV1();
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void CmdAddVertPlane_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (!Form1_Public.RulerOn)
		{
			Interaction.MsgBox(Localization.LocMSG(62));
			return;
		}
		float num = (float)(Form1_Public.Ruler[1].Xwm - Form1_Public.Ruler[0].Xwm);
		float num2 = (float)(Form1_Public.Ruler[1].Ywm - Form1_Public.Ruler[0].Ywm);
		float num3 = (float)(Math.Sqrt(num * num + num2 * num2) * CalcCoords.CalcMashtWMtoMeters());
		float num4 = (float)(Math.Atan2(num, num2) * (180.0 / Math.PI));
		if (num4 < 0f)
		{
			num4 += 360f;
		}
		_ = Conversions.ToString(Math.Round(num3, 1)) + "m\r\n" + Conversions.ToString(Math.Round(num4, 1)) + "°";
		if (Information.IsNothing(Public2.PRJ.VP))
		{
			Public2.PRJ.VP = new Public2.StructVertPlane[1];
		}
		else
		{
			ref Public2.StructVertPlane[] vP = ref Public2.PRJ.VP;
			vP = (Public2.StructVertPlane[])Utils.CopyArray(vP, new Public2.StructVertPlane[checked(Public2.PRJ.VP.GetUpperBound(0) + 1 + 1)]);
		}
		Public2.PRJ.VP[Public2.PRJ.VP.GetUpperBound(0)].Activ = true;
		Public2.PRJ.VP[Public2.PRJ.VP.GetUpperBound(0)].Azimuth = (float)Math.Round(num4, 1);
		Public2.PRJ.VP[Public2.PRJ.VP.GetUpperBound(0)].CoordWM = Form1_Public.Ruler[0];
		Public2.PRJ.VP[Public2.PRJ.VP.GetUpperBound(0)].CoordM = CalcCoords.ConvWebMercMetersToMetersRound(Public2.PRJ.VP[Public2.PRJ.VP.GetUpperBound(0)].CoordWM);
		Public2.PRJ.VP[Public2.PRJ.VP.GetUpperBound(0)].H1 = 100f;
		Public2.PRJ.VP[Public2.PRJ.VP.GetUpperBound(0)].L1 = (float)Math.Round(num3);
		Public2.PRJ.VP[Public2.PRJ.VP.GetUpperBound(0)].Name = "Доп. сечение " + Conversions.ToString(Math.Round(num4, 1)) + "°";
		Form1_Public.RulerOn = false;
		MyProject.Forms.Form1.cmdRuler.BackColor = SystemColors.Control;
		if (Interaction.MsgBox(Localization.LocMSG(63) + " " + Conversions.ToString(Math.Round(num4, 1)) + "° L=" + Conversions.ToString(Math.Round(num3)) + "m\r\n" + Localization.LocMSG(64), MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
		{
			try
			{
				MyProject.Forms.Form1.TV.SelectedNode = MyProject.Forms.Form1.TV.Nodes["VertPlanes"];
				MyProject.Forms.Form1.TV.Focus();
				MyProject.Forms.Form1.UC_VertPlanes1.DGV1.CurrentCell = MyProject.Forms.Form1.UC_VertPlanes1.DGV1[0, Public2.PRJ.VP.GetUpperBound(0)];
				MyProject.Forms.Form1.UC_VertPlanes1.DGV1.Rows[Public2.PRJ.VP.GetUpperBound(0)].Selected = true;
				if (CalcSZZ.CalcTmp())
				{
					Public2.PRJ.VP[Form1_Public.VPSelRow].Size = Public2.PRJ.SETT.MaxHorSize[1];
					CalcSZZ.ListHVP = new int[1];
					CalcSZZ.ListHVP[0] = Form1_Public.VPSelRow;
					MyProject.Forms.frmBckGrWrkVert.ShowDialog();
				}
				return;
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
				return;
			}
		}
		MyProject.Forms.Form1.PB.Refresh();
	}

	private void cmdStart0_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		long num = checked((long)Math.Round(DateTime.Now.Subtract(DateTime.MinValue.AddYears(2000)).TotalMilliseconds));
		if (CalcSZZ.CalcTmp())
		{
			Public2.PRJ.SETT.HorSize = Public2.PRJ.SETT.MaxHorSize[0];
			CalcSZZ.CalcAllHorizPlane();
			MyProject.Forms.Form1.lblTime.Text = Conversions.ToString(Math.Round((DateTime.Now.Subtract(DateTime.MinValue.AddYears(2000)).TotalMilliseconds - (double)num) / 1000.0, 3)) + " sec.";
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void cmdStart1_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		long num = checked((long)Math.Round(DateTime.Now.Subtract(DateTime.MinValue.AddYears(2000)).TotalMilliseconds));
		if (CalcSZZ.CalcTmp())
		{
			Public2.PRJ.SETT.HorSize = Public2.PRJ.SETT.MaxHorSize[1];
			CalcSZZ.CalcAllHorizPlane();
			MyProject.Forms.Form1.lblTime.Text = Conversions.ToString(Math.Round((DateTime.Now.Subtract(DateTime.MinValue.AddYears(2000)).TotalMilliseconds - (double)num) / 1000.0, 3)) + " sec.";
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void cmdWord1_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		DGV1.EndEdit();
		ContWord.Show(this, cmdWord1.Location);
	}

	private void cmdHorizontalCutsА4_1_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Information.IsNothing(Public2.PRJ.HP))
		{
			Interaction.MsgBox(Localization.LocMSG(65));
		}
		else
		{
			RepWordAlias.WordReportOneAlias("{HorizontalCutsА4_1}");
		}
	}

	private void cmdHorizontalCutsА3_1_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Information.IsNothing(Public2.PRJ.HP))
		{
			Interaction.MsgBox(Localization.LocMSG(65));
		}
		else
		{
			RepWordAlias.WordReportOneAlias("{HorizontalCutsА3_1}");
		}
	}

	private void cmdCardinalPointsChart_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		RepWordAlias.WordReportOneAlias("{CardinalPointsChart}");
	}

	private void DGV1_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
	{
		if (e.RowIndex >= checked(DGV1.RowCount - 1))
		{
			DGV1.Rows[e.RowIndex].Cells[3].Value = nullImage;
		}
	}

	public void FillDGV1()
	{
		FlagOK = false;
		DGV1.Rows.Clear();
		if (!Information.IsNothing(Public2.PRJ.HP))
		{
			int upperBound = Public2.PRJ.HP.GetUpperBound(0);
			for (int i = 0; i <= upperBound; i = checked(i + 1))
			{
				DGV1.Rows.Add(Public2.PRJ.HP[i].Activ, Public2.PRJ.HP[i].Height, "", ilICO.Images[Public2.PRJ.HP[i].LineType], Public2.PRJ.HP[i].LineType);
				DGV1[2, i].Style.BackColor = Public2.PRJ.HP[i].Color;
			}
			DGV1.ClearSelection();
			FlagOK = true;
		}
	}

	public void SaveForm1()
	{
		ref Public2.StructHorizPlane[] hP = ref Public2.PRJ.HP;
		checked
		{
			hP = (Public2.StructHorizPlane[])Utils.CopyArray(hP, new Public2.StructHorizPlane[DGV1.Rows.Count - 2 + 1]);
			int num = DGV1.Rows.Count - 2;
			for (int i = 0; i <= num; i++)
			{
				Public2.PRJ.HP[i].Activ = Conversions.ToBoolean(DGV1[0, i].Value);
				if (Information.IsNothing(RuntimeHelpers.GetObjectValue(DGV1[1, i].Value)))
				{
					Public2.PRJ.HP[i].Height = 0f;
				}
				else if (Module1.ValidText(Conversions.ToString(DGV1[1, i].Value), CheckLimits: false, 0f, 0f, IncludeMin: false, IncludeMax: false, Message: false))
				{
					Public2.PRJ.HP[i].Height = Module1.QSNG(Conversions.ToString(DGV1[1, i].Value));
				}
				else
				{
					DGV1[1, i].Value = Public2.PRJ.HP[i].Height;
				}
			}
			if (Public2.PRJ.HP.Length == 0)
			{
				Public2.PRJ.HP = null;
			}
		}
	}

	private void DGV1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
	{
		if (DGV1.Rows.Count == 1)
		{
			return;
		}
		checked
		{
			if ((e.RowIndex == -1) & (e.ColumnIndex == 0))
			{
				if (Operators.ConditionalCompareObjectEqual(DGV1[0, 0].Value, false, TextCompare: false))
				{
					int upperBound = Public2.PRJ.HP.GetUpperBound(0);
					for (int i = 0; i <= upperBound; i++)
					{
						Public2.PRJ.HP[i].Activ = true;
					}
				}
				else
				{
					int upperBound2 = Public2.PRJ.HP.GetUpperBound(0);
					for (int j = 0; j <= upperBound2; j++)
					{
						Public2.PRJ.HP[j].Activ = false;
					}
				}
				FillDGV1();
				DGV1.ClearSelection();
				MyProject.Forms.Form1.PB.Refresh();
			}
			else if ((e.RowIndex >= 0) & (e.RowIndex < DGV1.Rows.Count - 1) & (e.ColumnIndex == 2))
			{
				CD1.Color = DGV1[e.ColumnIndex, e.RowIndex].Style.BackColor;
				CD1.FullOpen = true;
				if (CD1.ShowDialog() == DialogResult.OK)
				{
					DGV1[e.ColumnIndex, e.RowIndex].Style.BackColor = CD1.Color;
					DGV1.ClearSelection();
					Public2.PRJ.HP[e.RowIndex].Color = CD1.Color;
					MyProject.Forms.Form1.PB.Refresh();
				}
			}
			else if ((e.RowIndex >= 0) & (e.RowIndex < DGV1.Rows.Count - 1) & (e.ColumnIndex == 3))
			{
				MyProject.Forms.frmLineTypeSelect.Location = new Point((Size)Control.MousePosition);
				MyProject.Forms.frmLineTypeSelect.ShowDialog();
				if (Form1_Public.LineTypeIndex >= 0)
				{
					DGV1[e.ColumnIndex, e.RowIndex].Value = ilICO.Images[Form1_Public.LineTypeIndex];
					DGV1.ClearSelection();
					Public2.PRJ.HP[e.RowIndex].LineType = (byte)Form1_Public.LineTypeIndex;
					MyProject.Forms.Form1.PB.Refresh();
				}
			}
		}
	}

	private void DGV1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
		if (FlagOK)
		{
			SaveForm1();
		}
	}

	private void DGV1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
	{
		SaveForm1();
		DGV1[3, checked(e.Row.Index - 1)].Value = ilICO.Images[0];
	}

	private void DGV1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
	{
		SaveForm1();
		if (!Information.IsNothing(Public2.PRJ.HP))
		{
			int upperBound = Public2.PRJ.HP.GetUpperBound(0);
			for (int i = 0; i <= upperBound; i = checked(i + 1))
			{
				Public2.PRJ.HP[i].PLNwm = null;
			}
		}
		MyProject.Forms.Form1.PB.Refresh();
	}

	private void DGV1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
	{
		DGV1.CommitEdit(DataGridViewDataErrorContexts.Commit);
		if (FlagOK)
		{
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	public void ShowColomnsDGV1()
	{
		if (CalcSZZ.GetFlagManyHorizCutLimits())
		{
			DGV1.Columns[2].Visible = false;
			DGV1.Columns[3].Visible = false;
			chkCombineHorPlanes.Enabled = false;
			chkCombineHorPlanesByColor.Enabled = false;
		}
		else
		{
			DGV1.Columns[2].Visible = true;
			DGV1.Columns[3].Visible = true;
			chkCombineHorPlanes.Enabled = true;
			chkCombineHorPlanesByColor.Enabled = true;
		}
	}

	private void chkCombineHorPlanes_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		Public2.PRJ.SETT.CombineHorPlanes = chkCombineHorPlanes.Checked;
	}

	private void chkCombineHorPlanesByColor_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		Public2.PRJ.SETT.CombineHorPlanesByColor = chkCombineHorPlanesByColor.Checked;
	}

	private void txtTransparFillZO_ValueChanged(object sender, EventArgs e)
	{
		if (FlagOK)
		{
			Public2.PRJ.SETT.TransparFillZO = Convert.ToByte(txtTransparFillZO.Value);
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void txtLineWidthZO_ValueChanged(object sender, EventArgs e)
	{
		if (FlagOK)
		{
			Public2.PRJ.SETT.LineWidthZO = Convert.ToByte(txtLineWidthZO.Value);
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void chkLimit0_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		Public2.PRJ.SETT.HorizCutLimits[0] = chkLimit0.Checked;
		ShowColomnsDGV1();
		MyProject.Forms.Form1.PB.Refresh();
	}

	private void chkLimit1_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		Public2.PRJ.SETT.HorizCutLimits[1] = chkLimit1.Checked;
		ShowColomnsDGV1();
		MyProject.Forms.Form1.PB.Refresh();
	}

	private void chkLimit2_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		Public2.PRJ.SETT.HorizCutLimits[2] = chkLimit2.Checked;
		ShowColomnsDGV1();
		MyProject.Forms.Form1.PB.Refresh();
	}

	private void chkLimit3_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		Public2.PRJ.SETT.HorizCutLimits[3] = chkLimit3.Checked;
		ShowColomnsDGV1();
		MyProject.Forms.Form1.PB.Refresh();
	}

	private void lblWarning_Click(object sender, EventArgs e)
	{
		Interaction.MsgBox("It should be taken into account that changing the activity of rows in the Parameters for calculating horizontal cuts table, as well as the color and type of the cut line after the calculation, not only changes the display of cuts, but also serves as the initial data for the next calculation. We recommend that after changing the parameters of the table Parameters for calculating horizontal cuts, always perform a new calculation. After the calculation is completed, the result may change, because when using the Include areas of lower cuts in higher horizontal cuts option, as well as when selecting a sequence of cuts with the same colors and linetypes, some cuts affect others.");
	}
}
