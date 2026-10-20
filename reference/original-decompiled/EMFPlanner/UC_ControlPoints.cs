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
public class UC_ControlPoints : UserControl
{
	private IContainer components;

	private bool FlagProcessing;

	private bool FlagFillDGVisGoingOn;

	public bool FlagPaintAllow;

	public short SelRowDGV1;

	public short SelRowDGV2;

	private bool FlagGroupChangeDGV2;

	private bool FlagGroupChangeDGV3;

	[field: AccessedThroughProperty("GroupBox1")]
	internal virtual GroupBox GroupBox1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("SplitContainer1")]
	internal virtual SplitContainer SplitContainer1
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
			EventHandler value2 = DGV1_SelectionChanged;
			DataGridViewCellEventHandler value3 = DGV1_CellValueChanged;
			DataGridViewCellMouseEventHandler value4 = DGV1_CellMouseDown;
			EventHandler value5 = DGV1_CurrentCellDirtyStateChanged;
			KeyEventHandler value6 = DGV1_KeyDown;
			DataGridView dataGridView = field;
			if (dataGridView != null)
			{
				dataGridView.SelectionChanged -= value2;
				dataGridView.CellValueChanged -= value3;
				dataGridView.CellMouseDown -= value4;
				dataGridView.CurrentCellDirtyStateChanged -= value5;
				dataGridView.KeyDown -= value6;
			}
			field = value;
			dataGridView = field;
			if (dataGridView != null)
			{
				dataGridView.SelectionChanged += value2;
				dataGridView.CellValueChanged += value3;
				dataGridView.CellMouseDown += value4;
				dataGridView.CurrentCellDirtyStateChanged += value5;
				dataGridView.KeyDown += value6;
			}
		}
	}

	[field: AccessedThroughProperty("DGV3")]
	internal virtual DataGridView DGV3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			DataGridViewCellEventHandler value2 = DGV3_CellValueChanged;
			KeyEventHandler value3 = DGV3_KeyDown;
			DataGridView dataGridView = field;
			if (dataGridView != null)
			{
				dataGridView.CellValueChanged -= value2;
				dataGridView.KeyDown -= value3;
			}
			field = value;
			dataGridView = field;
			if (dataGridView != null)
			{
				dataGridView.CellValueChanged += value2;
				dataGridView.KeyDown += value3;
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

	[field: AccessedThroughProperty("SplitContainer2")]
	internal virtual SplitContainer SplitContainer2
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

	[field: AccessedThroughProperty("GroupBox4")]
	internal virtual GroupBox GroupBox4
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("DGV2")]
	internal virtual DataGridView DGV2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = DGV2_SelectionChanged;
			DataGridViewCellEventHandler value3 = DGV2_CellValueChanged;
			KeyEventHandler value4 = DGV2_KeyDown;
			MouseEventHandler value5 = DGV2_MouseClick;
			DataGridView dataGridView = field;
			if (dataGridView != null)
			{
				dataGridView.SelectionChanged -= value2;
				dataGridView.CellValueChanged -= value3;
				dataGridView.KeyDown -= value4;
				dataGridView.MouseClick -= value5;
			}
			field = value;
			dataGridView = field;
			if (dataGridView != null)
			{
				dataGridView.SelectionChanged += value2;
				dataGridView.CellValueChanged += value3;
				dataGridView.KeyDown += value4;
				dataGridView.MouseClick += value5;
			}
		}
	}

	[field: AccessedThroughProperty("GroupBox5")]
	internal virtual GroupBox GroupBox5
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdAddControlPoint")]
	internal virtual Button cmdAddControlPoint
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdAddControlPoint_Click;
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

	[field: AccessedThroughProperty("cmdContrPointToCenterScreen")]
	internal virtual Button cmdContrPointToCenterScreen
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdContrPointToCenterScreen_Click;
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

	[field: AccessedThroughProperty("cmdCalcOnePoint")]
	internal virtual Button cmdCalcOnePoint
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdCalcOnePoint_Click;
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

	[field: AccessedThroughProperty("cmdCalcAllPoints")]
	internal virtual Button cmdCalcAllPoints
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdCalcAllPoints_Click;
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

	[field: AccessedThroughProperty("cmdMovePointsToAzimuth")]
	internal virtual Button cmdMovePointsToAzimuth
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdMovePointsToAzimuth_Click;
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

	[field: AccessedThroughProperty("ContLocation")]
	internal virtual ContextMenuStrip ContLocation
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			ToolStripItemClickedEventHandler value2 = ContLocation_ItemClicked;
			ContextMenuStrip contextMenuStrip = field;
			if (contextMenuStrip != null)
			{
				contextMenuStrip.ItemClicked -= value2;
			}
			field = value;
			contextMenuStrip = field;
			if (contextMenuStrip != null)
			{
				contextMenuStrip.ItemClicked += value2;
			}
		}
	}

	[field: AccessedThroughProperty("ToolStripMenuItem14")]
	internal virtual ToolStripMenuItem ToolStripMenuItem14
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ToolStripMenuItem15")]
	internal virtual ToolStripMenuItem ToolStripMenuItem15
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdWord")]
	internal virtual Button cmdWord
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdWord_Click;
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

	[field: AccessedThroughProperty("cmdControlPoints_1")]
	internal virtual ToolStripMenuItem cmdControlPoints_1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdControlPoints_1_Click;
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

	[field: AccessedThroughProperty("cmdUp")]
	internal virtual Button cmdUp
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdUp_Click;
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

	[field: AccessedThroughProperty("cmdDown")]
	internal virtual Button cmdDown
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdDown_Click;
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

	[field: AccessedThroughProperty("txtControlPointPrefix")]
	internal virtual TextBox txtControlPointPrefix
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = txtControlPointPrefix_TextChanged;
			TextBox textBox = field;
			if (textBox != null)
			{
				textBox.TextChanged -= value2;
			}
			field = value;
			textBox = field;
			if (textBox != null)
			{
				textBox.TextChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("Column3")]
	internal virtual DataGridViewCheckBoxColumn Column3
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

	[field: AccessedThroughProperty("GroupBox2")]
	internal virtual GroupBox GroupBox2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("rbLimit3")]
	internal virtual RadioButton rbLimit3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = rbLimit_CheckedChanged;
			RadioButton radioButton = field;
			if (radioButton != null)
			{
				radioButton.CheckedChanged -= value2;
			}
			field = value;
			radioButton = field;
			if (radioButton != null)
			{
				radioButton.CheckedChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("rbLimit2")]
	internal virtual RadioButton rbLimit2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = rbLimit_CheckedChanged;
			RadioButton radioButton = field;
			if (radioButton != null)
			{
				radioButton.CheckedChanged -= value2;
			}
			field = value;
			radioButton = field;
			if (radioButton != null)
			{
				radioButton.CheckedChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("rbLimit1")]
	internal virtual RadioButton rbLimit1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = rbLimit_CheckedChanged;
			RadioButton radioButton = field;
			if (radioButton != null)
			{
				radioButton.CheckedChanged -= value2;
			}
			field = value;
			radioButton = field;
			if (radioButton != null)
			{
				radioButton.CheckedChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("rbLimit0")]
	internal virtual RadioButton rbLimit0
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = rbLimit_CheckedChanged;
			RadioButton radioButton = field;
			if (radioButton != null)
			{
				radioButton.CheckedChanged -= value2;
			}
			field = value;
			radioButton = field;
			if (radioButton != null)
			{
				radioButton.CheckedChanged += value2;
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

	[field: AccessedThroughProperty("Label2")]
	internal virtual Label Label2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtFreq")]
	internal virtual TextBox txtFreq
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			CancelEventHandler value2 = txtFreq_Validating;
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

	[field: AccessedThroughProperty("cmdControlPoints_2")]
	internal virtual ToolStripMenuItem cmdControlPoints_2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdControlPoints_2_Click;
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

	[field: AccessedThroughProperty("cmdExcel")]
	internal virtual Button cmdExcel
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdExcel_Click;
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

	[field: AccessedThroughProperty("ContExcel")]
	internal virtual ContextMenuStrip ContExcel
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdExcelExport")]
	internal virtual ToolStripMenuItem cmdExcelExport
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdExcelExport_Click;
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

	[field: AccessedThroughProperty("cmdExcelImport")]
	internal virtual ToolStripMenuItem cmdExcelImport
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdExcelImport_Click;
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

	[field: AccessedThroughProperty("DataGridViewTextBoxColumn2")]
	internal virtual DataGridViewTextBoxColumn DataGridViewTextBoxColumn2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("DataGridViewTextBoxColumn5")]
	internal virtual DataGridViewTextBoxColumn DataGridViewTextBoxColumn5
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

	[field: AccessedThroughProperty("DataGridViewTextBoxColumn6")]
	internal virtual DataGridViewTextBoxColumn DataGridViewTextBoxColumn6
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("DataGridViewTextBoxColumn3")]
	internal virtual DataGridViewTextBoxColumn DataGridViewTextBoxColumn3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Column1")]
	internal virtual DataGridViewTextBoxColumn Column1
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

	[field: AccessedThroughProperty("DataGridViewTextBoxColumn4")]
	internal virtual DataGridViewTextBoxColumn DataGridViewTextBoxColumn4
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public UC_ControlPoints()
	{
		Load += UC_ControlPoints_Load;
		FlagProcessing = true;
		FlagFillDGVisGoingOn = false;
		SelRowDGV1 = -1;
		SelRowDGV2 = -1;
		FlagGroupChangeDGV2 = false;
		FlagGroupChangeDGV3 = false;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EMFPlanner.UC_ControlPoints));
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
		this.GroupBox1 = new System.Windows.Forms.GroupBox();
		this.cmdExcel = new System.Windows.Forms.Button();
		this.cmdUp = new System.Windows.Forms.Button();
		this.cmdDown = new System.Windows.Forms.Button();
		this.cmdWord = new System.Windows.Forms.Button();
		this.cmdMovePointsToAzimuth = new System.Windows.Forms.Button();
		this.cmdCalcAllPoints = new System.Windows.Forms.Button();
		this.cmdCalcOnePoint = new System.Windows.Forms.Button();
		this.cmdContrPointToCenterScreen = new System.Windows.Forms.Button();
		this.cmdAddControlPoint = new System.Windows.Forms.Button();
		this.cmdPosition = new System.Windows.Forms.Button();
		this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
		this.GroupBox3 = new System.Windows.Forms.GroupBox();
		this.DGV1 = new System.Windows.Forms.DataGridView();
		this.Column3 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.SplitContainer2 = new System.Windows.Forms.SplitContainer();
		this.GroupBox4 = new System.Windows.Forms.GroupBox();
		this.Label1 = new System.Windows.Forms.Label();
		this.txtControlPointPrefix = new System.Windows.Forms.TextBox();
		this.DGV2 = new System.Windows.Forms.DataGridView();
		this.Label3 = new System.Windows.Forms.Label();
		this.Label2 = new System.Windows.Forms.Label();
		this.txtFreq = new System.Windows.Forms.TextBox();
		this.GroupBox2 = new System.Windows.Forms.GroupBox();
		this.rbLimit3 = new System.Windows.Forms.RadioButton();
		this.rbLimit2 = new System.Windows.Forms.RadioButton();
		this.rbLimit1 = new System.Windows.Forms.RadioButton();
		this.rbLimit0 = new System.Windows.Forms.RadioButton();
		this.GroupBox5 = new System.Windows.Forms.GroupBox();
		this.DGV3 = new System.Windows.Forms.DataGridView();
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.ContLocation = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.ToolStripMenuItem14 = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripMenuItem15 = new System.Windows.Forms.ToolStripMenuItem();
		this.ContWord = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.cmdControlPoints_1 = new System.Windows.Forms.ToolStripMenuItem();
		this.cmdControlPoints_2 = new System.Windows.Forms.ToolStripMenuItem();
		this.ContExcel = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.cmdExcelExport = new System.Windows.Forms.ToolStripMenuItem();
		this.cmdExcelImport = new System.Windows.Forms.ToolStripMenuItem();
		this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.DataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.DataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.DataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.DataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.DataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.GroupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.SplitContainer1).BeginInit();
		this.SplitContainer1.Panel1.SuspendLayout();
		this.SplitContainer1.Panel2.SuspendLayout();
		this.SplitContainer1.SuspendLayout();
		this.GroupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.DGV1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.SplitContainer2).BeginInit();
		this.SplitContainer2.Panel1.SuspendLayout();
		this.SplitContainer2.Panel2.SuspendLayout();
		this.SplitContainer2.SuspendLayout();
		this.GroupBox4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.DGV2).BeginInit();
		this.GroupBox2.SuspendLayout();
		this.GroupBox5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.DGV3).BeginInit();
		this.ContLocation.SuspendLayout();
		this.ContWord.SuspendLayout();
		this.ContExcel.SuspendLayout();
		base.SuspendLayout();
		this.GroupBox1.Controls.Add(this.cmdExcel);
		this.GroupBox1.Controls.Add(this.cmdUp);
		this.GroupBox1.Controls.Add(this.cmdDown);
		this.GroupBox1.Controls.Add(this.cmdWord);
		this.GroupBox1.Controls.Add(this.cmdMovePointsToAzimuth);
		this.GroupBox1.Controls.Add(this.cmdCalcAllPoints);
		this.GroupBox1.Controls.Add(this.cmdCalcOnePoint);
		this.GroupBox1.Controls.Add(this.cmdContrPointToCenterScreen);
		this.GroupBox1.Controls.Add(this.cmdAddControlPoint);
		this.GroupBox1.Controls.Add(this.cmdPosition);
		this.GroupBox1.Controls.Add(this.SplitContainer1);
		this.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GroupBox1.Location = new System.Drawing.Point(0, 0);
		this.GroupBox1.Name = "GroupBox1";
		this.GroupBox1.Size = new System.Drawing.Size(500, 738);
		this.GroupBox1.TabIndex = 1;
		this.GroupBox1.TabStop = false;
		this.GroupBox1.Text = "Calculation at Control Points";
		this.cmdExcel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdExcel.Image = (System.Drawing.Image)resources.GetObject("cmdExcel.Image");
		this.cmdExcel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdExcel.Location = new System.Drawing.Point(260, 19);
		this.cmdExcel.Name = "cmdExcel";
		this.cmdExcel.Size = new System.Drawing.Size(26, 26);
		this.cmdExcel.TabIndex = 202;
		this.cmdExcel.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdExcel, "Point reports in MS Excel");
		this.cmdExcel.UseVisualStyleBackColor = true;
		this.cmdUp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdUp.Image = (System.Drawing.Image)resources.GetObject("cmdUp.Image");
		this.cmdUp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdUp.Location = new System.Drawing.Point(98, 19);
		this.cmdUp.Name = "cmdUp";
		this.cmdUp.Size = new System.Drawing.Size(26, 26);
		this.cmdUp.TabIndex = 195;
		this.cmdUp.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdUp, "Move Selected Point Group Up");
		this.cmdUp.UseVisualStyleBackColor = true;
		this.cmdDown.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdDown.Image = (System.Drawing.Image)resources.GetObject("cmdDown.Image");
		this.cmdDown.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdDown.Location = new System.Drawing.Point(125, 19);
		this.cmdDown.Name = "cmdDown";
		this.cmdDown.Size = new System.Drawing.Size(26, 26);
		this.cmdDown.TabIndex = 196;
		this.cmdDown.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdDown, "Move Selected Point Group Down");
		this.cmdDown.UseVisualStyleBackColor = true;
		this.cmdWord.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdWord.Image = (System.Drawing.Image)resources.GetObject("cmdWord.Image");
		this.cmdWord.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdWord.Location = new System.Drawing.Point(233, 19);
		this.cmdWord.Name = "cmdWord";
		this.cmdWord.Size = new System.Drawing.Size(26, 26);
		this.cmdWord.TabIndex = 194;
		this.cmdWord.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdWord, "Point reports");
		this.cmdWord.UseVisualStyleBackColor = true;
		this.cmdMovePointsToAzimuth.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdMovePointsToAzimuth.Image = (System.Drawing.Image)resources.GetObject("cmdMovePointsToAzimuth.Image");
		this.cmdMovePointsToAzimuth.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdMovePointsToAzimuth.Location = new System.Drawing.Point(152, 19);
		this.cmdMovePointsToAzimuth.Name = "cmdMovePointsToAzimuth";
		this.cmdMovePointsToAzimuth.Size = new System.Drawing.Size(26, 26);
		this.cmdMovePointsToAzimuth.TabIndex = 159;
		this.cmdMovePointsToAzimuth.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdMovePointsToAzimuth, "Move the points that are less than 2 meters away from the antenna azimuth line exactly on the antenna azimuth line");
		this.cmdMovePointsToAzimuth.UseVisualStyleBackColor = true;
		this.cmdCalcAllPoints.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdCalcAllPoints.Image = (System.Drawing.Image)resources.GetObject("cmdCalcAllPoints.Image");
		this.cmdCalcAllPoints.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdCalcAllPoints.Location = new System.Drawing.Point(179, 19);
		this.cmdCalcAllPoints.Name = "cmdCalcAllPoints";
		this.cmdCalcAllPoints.Size = new System.Drawing.Size(26, 26);
		this.cmdCalcAllPoints.TabIndex = 158;
		this.cmdCalcAllPoints.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdCalcAllPoints, "Calculation at all points");
		this.cmdCalcAllPoints.UseVisualStyleBackColor = true;
		this.cmdCalcOnePoint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdCalcOnePoint.Image = (System.Drawing.Image)resources.GetObject("cmdCalcOnePoint.Image");
		this.cmdCalcOnePoint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdCalcOnePoint.Location = new System.Drawing.Point(206, 19);
		this.cmdCalcOnePoint.Name = "cmdCalcOnePoint";
		this.cmdCalcOnePoint.Size = new System.Drawing.Size(26, 26);
		this.cmdCalcOnePoint.TabIndex = 157;
		this.cmdCalcOnePoint.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdCalcOnePoint, "Calculation at a selected point for a range of heights");
		this.cmdCalcOnePoint.UseVisualStyleBackColor = true;
		this.cmdContrPointToCenterScreen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdContrPointToCenterScreen.Image = (System.Drawing.Image)resources.GetObject("cmdContrPointToCenterScreen.Image");
		this.cmdContrPointToCenterScreen.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdContrPointToCenterScreen.Location = new System.Drawing.Point(44, 19);
		this.cmdContrPointToCenterScreen.Name = "cmdContrPointToCenterScreen";
		this.cmdContrPointToCenterScreen.Size = new System.Drawing.Size(26, 26);
		this.cmdContrPointToCenterScreen.TabIndex = 156;
		this.cmdContrPointToCenterScreen.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdContrPointToCenterScreen, "Move selected points to the center of the screen");
		this.cmdContrPointToCenterScreen.UseVisualStyleBackColor = true;
		this.cmdAddControlPoint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdAddControlPoint.Image = (System.Drawing.Image)resources.GetObject("cmdAddControlPoint.Image");
		this.cmdAddControlPoint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdAddControlPoint.Location = new System.Drawing.Point(71, 19);
		this.cmdAddControlPoint.Name = "cmdAddControlPoint";
		this.cmdAddControlPoint.Size = new System.Drawing.Size(26, 26);
		this.cmdAddControlPoint.TabIndex = 149;
		this.cmdAddControlPoint.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdAddControlPoint, "Create a point in the center of the screen");
		this.cmdAddControlPoint.UseVisualStyleBackColor = true;
		this.cmdPosition.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdPosition.Image = (System.Drawing.Image)resources.GetObject("cmdPosition.Image");
		this.cmdPosition.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdPosition.Location = new System.Drawing.Point(17, 19);
		this.cmdPosition.Name = "cmdPosition";
		this.cmdPosition.Size = new System.Drawing.Size(26, 26);
		this.cmdPosition.TabIndex = 148;
		this.cmdPosition.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdPosition, "Position the map to the selected point");
		this.cmdPosition.UseVisualStyleBackColor = true;
		this.SplitContainer1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.SplitContainer1.Location = new System.Drawing.Point(4, 51);
		this.SplitContainer1.Name = "SplitContainer1";
		this.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.SplitContainer1.Panel1.Controls.Add(this.GroupBox3);
		this.SplitContainer1.Panel2.Controls.Add(this.SplitContainer2);
		this.SplitContainer1.Size = new System.Drawing.Size(494, 681);
		this.SplitContainer1.SplitterDistance = 123;
		this.SplitContainer1.TabIndex = 7;
		this.GroupBox3.Controls.Add(this.DGV1);
		this.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GroupBox3.Location = new System.Drawing.Point(0, 0);
		this.GroupBox3.Name = "GroupBox3";
		this.GroupBox3.Size = new System.Drawing.Size(494, 123);
		this.GroupBox3.TabIndex = 0;
		this.GroupBox3.TabStop = false;
		this.GroupBox3.Text = "Control points groups";
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
		this.DGV1.Columns.AddRange(this.Column3, this.Column2);
		this.DGV1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.DGV1.Location = new System.Drawing.Point(3, 16);
		this.DGV1.Name = "DGV1";
		this.DGV1.RowHeadersWidth = 50;
		this.DGV1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.DGV1.RowTemplate.Height = 18;
		this.DGV1.Size = new System.Drawing.Size(488, 104);
		this.DGV1.TabIndex = 6;
		this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
		this.Column3.HeaderText = "";
		this.Column3.Name = "Column3";
		this.Column3.Width = 21;
		this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.Column2.HeaderText = "Group description";
		this.Column2.Name = "Column2";
		this.Column2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.SplitContainer2.Location = new System.Drawing.Point(0, 0);
		this.SplitContainer2.Name = "SplitContainer2";
		this.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
		this.SplitContainer2.Panel1.Controls.Add(this.GroupBox4);
		this.SplitContainer2.Panel2.Controls.Add(this.Label3);
		this.SplitContainer2.Panel2.Controls.Add(this.Label2);
		this.SplitContainer2.Panel2.Controls.Add(this.txtFreq);
		this.SplitContainer2.Panel2.Controls.Add(this.GroupBox2);
		this.SplitContainer2.Panel2.Controls.Add(this.GroupBox5);
		this.SplitContainer2.Size = new System.Drawing.Size(494, 554);
		this.SplitContainer2.SplitterDistance = 313;
		this.SplitContainer2.TabIndex = 7;
		this.GroupBox4.Controls.Add(this.Label1);
		this.GroupBox4.Controls.Add(this.txtControlPointPrefix);
		this.GroupBox4.Controls.Add(this.DGV2);
		this.GroupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GroupBox4.Location = new System.Drawing.Point(0, 0);
		this.GroupBox4.Name = "GroupBox4";
		this.GroupBox4.Size = new System.Drawing.Size(494, 313);
		this.GroupBox4.TabIndex = 1;
		this.GroupBox4.TabStop = false;
		this.GroupBox4.Text = "Control points";
		this.Label1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.Label1.AutoSize = true;
		this.Label1.Location = new System.Drawing.Point(14, 290);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(119, 13);
		this.Label1.TabIndex = 8;
		this.Label1.Text = "Control point label prefix";
		this.txtControlPointPrefix.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtControlPointPrefix.Location = new System.Drawing.Point(148, 287);
		this.txtControlPointPrefix.Name = "txtControlPointPrefix";
		this.txtControlPointPrefix.Size = new System.Drawing.Size(81, 20);
		this.txtControlPointPrefix.TabIndex = 7;
		this.DGV2.AllowUserToAddRows = false;
		this.DGV2.AllowUserToDeleteRows = false;
		this.DGV2.AllowUserToResizeColumns = false;
		this.DGV2.AllowUserToResizeRows = false;
		this.DGV2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.DGV2.BackgroundColor = System.Drawing.SystemColors.Menu;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.DGV2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
		this.DGV2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGV2.Columns.AddRange(this.Column4, this.Column5, this.DataGridViewTextBoxColumn2, this.DataGridViewTextBoxColumn5, this.Column6, this.DataGridViewTextBoxColumn6);
		this.DGV2.Location = new System.Drawing.Point(3, 16);
		this.DGV2.Name = "DGV2";
		this.DGV2.RowHeadersWidth = 55;
		this.DGV2.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.DGV2.RowTemplate.Height = 18;
		this.DGV2.Size = new System.Drawing.Size(488, 266);
		this.DGV2.TabIndex = 6;
		this.Label3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.Label3.AutoSize = true;
		this.Label3.Location = new System.Drawing.Point(356, 209);
		this.Label3.Name = "Label3";
		this.Label3.Size = new System.Drawing.Size(29, 13);
		this.Label3.TabIndex = 205;
		this.Label3.Text = "MHz";
		this.Label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.Label2.AutoSize = true;
		this.Label2.Location = new System.Drawing.Point(14, 209);
		this.Label2.Name = "Label2";
		this.Label2.Size = new System.Drawing.Size(268, 13);
		this.Label2.TabIndex = 204;
		this.Label2.Text = "Frequency for EMF limit of E-field strength measurement";
		this.txtFreq.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.txtFreq.Location = new System.Drawing.Point(294, 206);
		this.txtFreq.Name = "txtFreq";
		this.txtFreq.Size = new System.Drawing.Size(52, 20);
		this.txtFreq.TabIndex = 203;
		this.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GroupBox2.Controls.Add(this.rbLimit3);
		this.GroupBox2.Controls.Add(this.rbLimit2);
		this.GroupBox2.Controls.Add(this.rbLimit1);
		this.GroupBox2.Controls.Add(this.rbLimit0);
		this.GroupBox2.Location = new System.Drawing.Point(3, 145);
		this.GroupBox2.Name = "GroupBox2";
		this.GroupBox2.Size = new System.Drawing.Size(491, 52);
		this.GroupBox2.TabIndex = 202;
		this.GroupBox2.TabStop = false;
		this.GroupBox2.Text = "Use EMF Limits for calculations";
		this.rbLimit3.AutoSize = true;
		this.rbLimit3.Location = new System.Drawing.Point(335, 23);
		this.rbLimit3.Name = "rbLimit3";
		this.rbLimit3.Size = new System.Drawing.Size(60, 17);
		this.rbLimit3.TabIndex = 4;
		this.rbLimit3.Text = "Limits 4";
		this.rbLimit3.UseVisualStyleBackColor = true;
		this.rbLimit2.AutoSize = true;
		this.rbLimit2.Location = new System.Drawing.Point(230, 23);
		this.rbLimit2.Name = "rbLimit2";
		this.rbLimit2.Size = new System.Drawing.Size(60, 17);
		this.rbLimit2.TabIndex = 3;
		this.rbLimit2.Text = "Limits 3";
		this.rbLimit2.UseVisualStyleBackColor = true;
		this.rbLimit1.AutoSize = true;
		this.rbLimit1.Location = new System.Drawing.Point(125, 23);
		this.rbLimit1.Name = "rbLimit1";
		this.rbLimit1.Size = new System.Drawing.Size(60, 17);
		this.rbLimit1.TabIndex = 2;
		this.rbLimit1.Text = "Limits 2";
		this.rbLimit1.UseVisualStyleBackColor = true;
		this.rbLimit0.AutoSize = true;
		this.rbLimit0.Checked = true;
		this.rbLimit0.Location = new System.Drawing.Point(20, 23);
		this.rbLimit0.Name = "rbLimit0";
		this.rbLimit0.Size = new System.Drawing.Size(60, 17);
		this.rbLimit0.TabIndex = 1;
		this.rbLimit0.TabStop = true;
		this.rbLimit0.Text = "Limits 1";
		this.rbLimit0.UseVisualStyleBackColor = true;
		this.GroupBox5.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GroupBox5.Controls.Add(this.DGV3);
		this.GroupBox5.Location = new System.Drawing.Point(0, 0);
		this.GroupBox5.Name = "GroupBox5";
		this.GroupBox5.Size = new System.Drawing.Size(494, 136);
		this.GroupBox5.TabIndex = 0;
		this.GroupBox5.TabStop = false;
		this.GroupBox5.Text = "Control point heights";
		this.DGV3.AllowUserToDeleteRows = false;
		this.DGV3.AllowUserToResizeColumns = false;
		this.DGV3.AllowUserToResizeRows = false;
		this.DGV3.BackgroundColor = System.Drawing.SystemColors.Menu;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
		dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.DGV3.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
		this.DGV3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGV3.Columns.AddRange(this.DataGridViewTextBoxColumn3, this.Column1, this.Column7, this.DataGridViewTextBoxColumn4);
		this.DGV3.Dock = System.Windows.Forms.DockStyle.Fill;
		this.DGV3.Location = new System.Drawing.Point(3, 16);
		this.DGV3.Name = "DGV3";
		this.DGV3.RowHeadersWidth = 55;
		this.DGV3.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.DGV3.RowTemplate.Height = 18;
		this.DGV3.Size = new System.Drawing.Size(488, 117);
		this.DGV3.TabIndex = 7;
		this.ContLocation.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.ToolStripMenuItem14, this.ToolStripMenuItem15 });
		this.ContLocation.Name = "cmsDBW";
		this.ContLocation.Size = new System.Drawing.Size(119, 48);
		this.ToolStripMenuItem14.Name = "ToolStripMenuItem14";
		this.ToolStripMenuItem14.Size = new System.Drawing.Size(118, 22);
		this.ToolStripMenuItem14.Text = "Ground";
		this.ToolStripMenuItem15.Name = "ToolStripMenuItem15";
		this.ToolStripMenuItem15.Size = new System.Drawing.Size(118, 22);
		this.ToolStripMenuItem15.Text = "Building";
		this.ContWord.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.cmdControlPoints_1, this.cmdControlPoints_2 });
		this.ContWord.Name = "cmsDBW";
		this.ContWord.Size = new System.Drawing.Size(167, 48);
		this.cmdControlPoints_1.Name = "cmdControlPoints_1";
		this.cmdControlPoints_1.Size = new System.Drawing.Size(166, 22);
		this.cmdControlPoints_1.Text = "{ControlPoints_1}";
		this.cmdControlPoints_2.Name = "cmdControlPoints_2";
		this.cmdControlPoints_2.Size = new System.Drawing.Size(166, 22);
		this.cmdControlPoints_2.Text = "{ControlPoints_2}";
		this.ContExcel.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.cmdExcelExport, this.cmdExcelImport });
		this.ContExcel.Name = "cmsDBW";
		this.ContExcel.Size = new System.Drawing.Size(111, 48);
		this.cmdExcelExport.Name = "cmdExcelExport";
		this.cmdExcelExport.Size = new System.Drawing.Size(110, 22);
		this.cmdExcelExport.Text = "Export";
		this.cmdExcelImport.Name = "cmdExcelImport";
		this.cmdExcelImport.Size = new System.Drawing.Size(110, 22);
		this.cmdExcelImport.Text = "Import";
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(70, 70, 70);
		this.Column4.DefaultCellStyle = dataGridViewCellStyle4;
		this.Column4.HeaderText = "Latitude, deg.";
		this.Column4.Name = "Column4";
		this.Column4.ReadOnly = true;
		this.Column4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column4.Width = 65;
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle5.ForeColor = System.Drawing.Color.FromArgb(70, 70, 70);
		this.Column5.DefaultCellStyle = dataGridViewCellStyle5;
		this.Column5.HeaderText = "Longitude, deg.";
		this.Column5.Name = "Column5";
		this.Column5.ReadOnly = true;
		this.Column5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column5.Width = 65;
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.DataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle6;
		this.DataGridViewTextBoxColumn2.HeaderText = "Х, m";
		this.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2";
		this.DataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.DataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.DataGridViewTextBoxColumn2.Width = 45;
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.DataGridViewTextBoxColumn5.DefaultCellStyle = dataGridViewCellStyle7;
		this.DataGridViewTextBoxColumn5.HeaderText = "Y, m";
		this.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5";
		this.DataGridViewTextBoxColumn5.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.DataGridViewTextBoxColumn5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.DataGridViewTextBoxColumn5.Width = 45;
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Column6.DefaultCellStyle = dataGridViewCellStyle8;
		this.Column6.HeaderText = "Location";
		this.Column6.Name = "Column6";
		this.Column6.ReadOnly = true;
		this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column6.Width = 55;
		this.DataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.DataGridViewTextBoxColumn6.HeaderText = "Point description 1";
		this.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6";
		this.DataGridViewTextBoxColumn6.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.DataGridViewTextBoxColumn6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.DataGridViewTextBoxColumn3.DefaultCellStyle = dataGridViewCellStyle9;
		this.DataGridViewTextBoxColumn3.HeaderText = "Height, m";
		this.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3";
		this.DataGridViewTextBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.DataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.DataGridViewTextBoxColumn3.Width = 45;
		dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Column1.DefaultCellStyle = dataGridViewCellStyle10;
		this.Column1.HeaderText = "Attenuation, dB";
		this.Column1.Name = "Column1";
		this.Column1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column1.Width = 65;
		dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Column7.DefaultCellStyle = dataGridViewCellStyle11;
		this.Column7.HeaderText = "Measured E-field strength, V/m";
		this.Column7.Name = "Column7";
		this.Column7.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column7.Width = 80;
		this.DataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		this.DataGridViewTextBoxColumn4.HeaderText = "Point description 2";
		this.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4";
		this.DataGridViewTextBoxColumn4.Resizable = System.Windows.Forms.DataGridViewTriState.False;
		this.DataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		base.Controls.Add(this.GroupBox1);
		base.Name = "UC_ControlPoints";
		base.Size = new System.Drawing.Size(500, 738);
		this.GroupBox1.ResumeLayout(false);
		this.SplitContainer1.Panel1.ResumeLayout(false);
		this.SplitContainer1.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.SplitContainer1).EndInit();
		this.SplitContainer1.ResumeLayout(false);
		this.GroupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.DGV1).EndInit();
		this.SplitContainer2.Panel1.ResumeLayout(false);
		this.SplitContainer2.Panel2.ResumeLayout(false);
		this.SplitContainer2.Panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.SplitContainer2).EndInit();
		this.SplitContainer2.ResumeLayout(false);
		this.GroupBox4.ResumeLayout(false);
		this.GroupBox4.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.DGV2).EndInit();
		this.GroupBox2.ResumeLayout(false);
		this.GroupBox2.PerformLayout();
		this.GroupBox5.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.DGV3).EndInit();
		this.ContLocation.ResumeLayout(false);
		this.ContWord.ResumeLayout(false);
		this.ContExcel.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void UC_ControlPoints_Load(object sender, EventArgs e)
	{
		MyProject.Forms.Form1.DrawPIC();
		Module1.SetDoubleBuffered(DGV2);
		FlagPaintAllow = false;
		FillDGV1();
		FlagPaintAllow = true;
		SelRowDGV1 = Form1_Public.CPGSelRow;
		if (!Information.IsNothing(Public2.PRJ.CPG) && ((SelRowDGV1 > -1) & (SelRowDGV1 <= Public2.PRJ.CPG.GetUpperBound(0))))
		{
			DGV1.CurrentCell = DGV1[0, SelRowDGV1];
			DGV1.Rows[SelRowDGV1].Selected = true;
		}
		txtControlPointPrefix.Text = Public2.PRJ.SETT.ControlPointPrefix;
		rbLimit0.ForeColor = Public2.PRJ.SETT.LimitColor[0];
		rbLimit1.ForeColor = Public2.PRJ.SETT.LimitColor[1];
		rbLimit2.ForeColor = Public2.PRJ.SETT.LimitColor[2];
		rbLimit3.ForeColor = Public2.PRJ.SETT.LimitColor[3];
		FlagProcessing = true;
		if (Public2.PRJ.SETT.ControlPointLimitIndex == 0)
		{
			rbLimit0.Checked = true;
			rbLimit1.Checked = false;
			rbLimit2.Checked = false;
			rbLimit3.Checked = false;
		}
		else if (Public2.PRJ.SETT.ControlPointLimitIndex == 1)
		{
			rbLimit0.Checked = false;
			rbLimit1.Checked = true;
			rbLimit2.Checked = false;
			rbLimit3.Checked = false;
		}
		else if (Public2.PRJ.SETT.ControlPointLimitIndex == 2)
		{
			rbLimit0.Checked = false;
			rbLimit1.Checked = false;
			rbLimit2.Checked = true;
			rbLimit3.Checked = false;
		}
		else if (Public2.PRJ.SETT.ControlPointLimitIndex == 3)
		{
			rbLimit0.Checked = false;
			rbLimit1.Checked = false;
			rbLimit2.Checked = false;
			rbLimit3.Checked = true;
		}
		txtFreq.Text = Conversions.ToString(Public2.PRJ.SETT.ControlPointMesureFreq);
		FlagProcessing = false;
	}

	public void FillDGV1()
	{
		FlagFillDGVisGoingOn = true;
		DGV1.Rows.Clear();
		if (Information.IsNothing(Public2.PRJ.CPG))
		{
			FlagFillDGVisGoingOn = false;
			return;
		}
		int upperBound = Public2.PRJ.CPG.GetUpperBound(0);
		checked
		{
			for (int i = 0; i <= upperBound; i++)
			{
				DGV1.Rows.Add(Public2.PRJ.CPG[i].Active, Public2.PRJ.CPG[i].Name);
				DGV1.Rows[i].HeaderCell.Value = (i + 1).ToString();
			}
			FlagFillDGVisGoingOn = false;
		}
	}

	private void DGV1_SelectionChanged(object sender, EventArgs e)
	{
		checked
		{
			if (DGV1.SelectedCells.Count > 0)
			{
				SelRowDGV1 = (short)DGV1.SelectedCells[0].RowIndex;
			}
			else
			{
				SelRowDGV1 = (short)DGV1.CurrentCell.RowIndex;
			}
			if (Information.IsNothing(Public2.PRJ.CPG))
			{
				SelRowDGV1 = -1;
			}
			else if (SelRowDGV1 == DGV1.RowCount - 1)
			{
				SelRowDGV1 = -1;
			}
			SelRowDGV2 = -1;
			if (!Information.IsNothing(Public2.PRJ.CPG))
			{
				int upperBound = Public2.PRJ.CPG.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					if (Public2.PRJ.CPG[i].Active && !Information.IsNothing(Public2.PRJ.CPG[i].CP))
					{
						int upperBound2 = Public2.PRJ.CPG[i].CP.GetUpperBound(0);
						for (int j = 0; j <= upperBound2; j++)
						{
							Public2.PRJ.CPG[i].CP[j].Selected = false;
						}
					}
				}
			}
			FlagPaintAllow = false;
			FillDGV2();
			FlagPaintAllow = true;
		}
	}

	private void DGV1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
		checked
		{
			if (!FlagFillDGVisGoingOn && e.RowIndex >= 0)
			{
				if (Information.IsNothing(Public2.PRJ.CPG))
				{
					Public2.PRJ.CPG = new Public2.ControlPointGroup[1];
					DGV1.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
				}
				else if (e.RowIndex > Public2.PRJ.CPG.GetUpperBound(0))
				{
					ref Public2.ControlPointGroup[] cPG = ref Public2.PRJ.CPG;
					cPG = (Public2.ControlPointGroup[])Utils.CopyArray(cPG, new Public2.ControlPointGroup[e.RowIndex + 1]);
					DGV1.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
				}
				if (e.ColumnIndex == 0)
				{
					Public2.PRJ.CPG[e.RowIndex].Active = Conversions.ToBoolean(DGV1[0, e.RowIndex].Value);
				}
				else if (e.ColumnIndex == 1)
				{
					Public2.PRJ.CPG[e.RowIndex].Name = Conversions.ToString(DGV1[1, e.RowIndex].Value);
				}
			}
		}
	}

	private void DGV1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
	{
		if (DGV1.SelectedCells.Count < 2 || e.ColumnIndex != 0)
		{
			return;
		}
		int columnIndex = e.ColumnIndex;
		checked
		{
			int num = DGV1.SelectedCells.Count - 1;
			int num2 = 0;
			while (true)
			{
				if (num2 <= num)
				{
					if (DGV1.SelectedCells[num2].ColumnIndex == columnIndex)
					{
						if (DGV1.SelectedCells[num2].RowIndex == DGV1.RowCount - 1)
						{
							DGV1.ClearSelection();
							break;
						}
						num2++;
						continue;
					}
					break;
				}
				bool flag = Conversions.ToBoolean(Operators.NotObject(DGV1.Rows[e.RowIndex].Cells[0].Value));
				int num3 = DGV1.SelectedCells.Count - 1;
				for (int i = 0; i <= num3; i++)
				{
					DGV1[0, DGV1.SelectedCells[i].RowIndex].Value = flag;
				}
				DGV1.Rows[e.RowIndex].Cells[0].Value = Operators.NotObject(DGV1.Rows[e.RowIndex].Cells[0].Value);
				DGV1.ClearSelection();
				break;
			}
		}
	}

	private void DGV1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
	{
		DGV1.CommitEdit(DataGridViewDataErrorContexts.Commit);
		if (FlagPaintAllow)
		{
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void DGV1_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Delete || Information.IsNothing(Public2.PRJ.CPG) || DGV1.SelectedRows.Count < 1 || ((DGV1.SelectedRows.Count == 1) & (DGV1.SelectedRows[0].Index > Public2.PRJ.CPG.GetUpperBound(0))))
		{
			return;
		}
		short[] array = new short[1];
		short num = 0;
		checked
		{
			int num2 = DGV1.SelectedRows.Count - 1;
			for (int i = 0; i <= num2; i++)
			{
				if (DGV1.SelectedRows[i].Index <= Public2.PRJ.CPG.GetUpperBound(0))
				{
					array = (short[])Utils.CopyArray(array, new short[num + 1]);
					array[num] = (short)DGV1.SelectedRows[i].Index;
					num++;
				}
			}
			if (Interaction.MsgBox(Localization.LocMSG(0) + " " + Conversions.ToString(unchecked((int)num)) + " " + Localization.LocMSG(49) + "?", MsgBoxStyle.YesNo) != MsgBoxResult.Yes)
			{
				return;
			}
			int upperBound = array.GetUpperBound(0);
			for (int j = 0; j <= upperBound; j++)
			{
				int num3 = j + 1;
				int upperBound2 = array.GetUpperBound(0);
				for (int k = num3; k <= upperBound2; k++)
				{
					if (array[k] > array[j])
					{
						short num4 = array[j];
						array[j] = array[k];
						array[k] = num4;
					}
				}
			}
			short num5 = 0;
			int upperBound3 = array.GetUpperBound(0);
			for (int l = 0; l <= upperBound3; l++)
			{
				num5++;
				short num6 = array[l];
				int num7 = Public2.PRJ.CPG.GetUpperBound(0) - 1;
				for (int m = num6; m <= num7; m++)
				{
					Public2.PRJ.CPG[m] = Module1.DeepClone(Public2.PRJ.CPG[m + 1]);
				}
			}
			ref Public2.ControlPointGroup[] cPG = ref Public2.PRJ.CPG;
			cPG = (Public2.ControlPointGroup[])Utils.CopyArray(cPG, new Public2.ControlPointGroup[Public2.PRJ.CPG.GetUpperBound(0) - num5 + 1]);
			if (Public2.PRJ.CPG.Length == 0)
			{
				Public2.PRJ.CPG = null;
			}
			FlagPaintAllow = false;
			FillDGV1();
			FlagPaintAllow = true;
			DGV1.ClearSelection();
			if (Information.IsNothing(Public2.PRJ.CPG))
			{
				MyProject.Forms.Form1.PB.Refresh();
			}
		}
	}

	public void FillDGV2()
	{
		FlagFillDGVisGoingOn = true;
		DGV2.Rows.Clear();
		DGV2.Enabled = true;
		if (Information.IsNothing(Public2.PRJ.CPG))
		{
			DGV2.Enabled = false;
			FlagFillDGVisGoingOn = false;
			return;
		}
		if ((SelRowDGV1 == -1) | (SelRowDGV1 > Public2.PRJ.CPG.GetUpperBound(0)))
		{
			DGV2.Enabled = false;
			FlagFillDGVisGoingOn = false;
			return;
		}
		if (Information.IsNothing(Public2.PRJ.CPG[SelRowDGV1].CP))
		{
			FlagFillDGVisGoingOn = false;
			return;
		}
		CalcCoords.ConvAllControlPointsWebMercMetersToMeters();
		int upperBound = Public2.PRJ.CPG[SelRowDGV1].CP.GetUpperBound(0);
		checked
		{
			double LatWgs = default;
			double LonWgs = default;
			for (int i = 0; i <= upperBound; i++)
			{
				CalcCoords.ConvWebMercMetersToWGS84(Public2.PRJ.CPG[SelRowDGV1].CP[i].CoordWM, ref LatWgs, ref LonWgs);
				DGV2.Rows.Add(CalcCoords.ConvLatToStr(LatWgs), CalcCoords.ConvLonToStr(LonWgs), Strings.Format(Public2.PRJ.CPG[SelRowDGV1].CP[i].CoordM.Xm, "0.0"), Strings.Format(Public2.PRJ.CPG[SelRowDGV1].CP[i].CoordM.Ym, "0.0"), Public2.PRJ.CPG[SelRowDGV1].CP[i].Location, Public2.PRJ.CPG[SelRowDGV1].CP[i].Name);
				DGV2.Rows[i].HeaderCell.Value = (i + 1).ToString();
			}
			FlagFillDGVisGoingOn = false;
		}
	}

	private void DGV2_SelectionChanged(object sender, EventArgs e)
	{
		checked
		{
			try
			{
				if (!Information.IsNothing(Public2.PRJ.CPG))
				{
					int upperBound = Public2.PRJ.CPG.GetUpperBound(0);
					for (int i = 0; i <= upperBound; i++)
					{
						if (Public2.PRJ.CPG[i].Active && !Information.IsNothing(Public2.PRJ.CPG[i].CP))
						{
							int upperBound2 = Public2.PRJ.CPG[i].CP.GetUpperBound(0);
							for (int j = 0; j <= upperBound2; j++)
							{
								Public2.PRJ.CPG[i].CP[j].Selected = false;
							}
						}
					}
				}
				if (DGV2.SelectedCells.Count > 0)
				{
					SelRowDGV2 = (short)DGV2.SelectedCells[0].RowIndex;
				}
				else
				{
					SelRowDGV2 = (short)DGV2.CurrentCell.RowIndex;
				}
				if (SelRowDGV1 == -1)
				{
					SelRowDGV2 = -1;
				}
				else if (Information.IsNothing(Public2.PRJ.CPG[SelRowDGV1].CP))
				{
					SelRowDGV2 = -1;
				}
				if ((SelRowDGV1 >= 0) & (SelRowDGV2 >= 0))
				{
					if (DGV2.SelectedRows.Count == 0)
					{
						int upperBound3 = Public2.PRJ.CPG[SelRowDGV1].CP.GetUpperBound(0);
						for (int k = 0; k <= upperBound3; k++)
						{
							Public2.PRJ.CPG[SelRowDGV1].CP[k].Selected = false;
						}
						FlagPaintAllow = false;
						FillDGV3();
						FlagPaintAllow = true;
						if (FlagPaintAllow)
						{
							MyProject.Forms.Form1.PB.Refresh();
						}
						return;
					}
					int upperBound4 = Public2.PRJ.CPG[SelRowDGV1].CP.GetUpperBound(0);
					for (int l = 0; l <= upperBound4; l++)
					{
						Public2.PRJ.CPG[SelRowDGV1].CP[l].Selected = false;
					}
					int num = DGV2.SelectedRows.Count - 1;
					for (int m = 0; m <= num; m++)
					{
						if (DGV2.SelectedRows[m].Index <= Public2.PRJ.CPG[SelRowDGV1].CP.GetUpperBound(0))
						{
							Public2.PRJ.CPG[SelRowDGV1].CP[DGV2.SelectedRows[m].Index].Selected = true;
						}
					}
				}
				FlagPaintAllow = false;
				FillDGV3();
				FlagPaintAllow = true;
				if (FlagPaintAllow)
				{
					MyProject.Forms.Form1.PB.Refresh();
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				ProjectData.ClearProjectError();
			}
		}
	}

	private void DGV2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
		if (FlagFillDGVisGoingOn || FlagGroupChangeDGV2 || e.RowIndex < 0)
		{
			return;
		}
		checked
		{
			if (e.ColumnIndex == 2)
			{
				if (Versioned.IsNumeric(RuntimeHelpers.GetObjectValue(DGV2[e.ColumnIndex, e.RowIndex].Value)))
				{
					if (DGV2.SelectedCells.Count == 1)
					{
						Public2.PRJ.CPG[SelRowDGV1].CP[e.RowIndex].CoordM.Xm = Conversions.ToDouble(DGV2[e.ColumnIndex, e.RowIndex].Value);
						DGV2[e.ColumnIndex, e.RowIndex].Value = Strings.Format(Public2.PRJ.CPG[SelRowDGV1].CP[e.RowIndex].CoordM.Xm, "0.0").ToString();
						Public2.PRJ.CPG[SelRowDGV1].CP[e.RowIndex].CoordWM = CalcCoords.ConvMetersToWebMercMeters(Public2.PRJ.CPG[SelRowDGV1].CP[e.RowIndex].CoordM);
						double LatWgs = default;
						double LonWgs = default;
						CalcCoords.ConvWebMercMetersToWGS84(Public2.PRJ.CPG[SelRowDGV1].CP[e.RowIndex].CoordWM, ref LatWgs, ref LonWgs);
						DGV2[0, e.RowIndex].Value = Strings.Format(LatWgs, "0.000000");
						DGV2[1, e.RowIndex].Value = Strings.Format(LonWgs, "0.000000");
						if (e.RowIndex == DGV2.RowCount - 1)
						{
							MyProject.Forms.Form1.PB.Refresh();
						}
					}
					else
					{
						if (DGV2.SelectedCells.Count <= 1)
						{
							return;
						}
						FlagGroupChangeDGV2 = true;
						string sTR = Conversions.ToString(DGV2[e.ColumnIndex, e.RowIndex].Value);
						int num = DGV2.SelectedCells.Count - 1;
						double LatWgs2 = default;
						double LonWgs2 = default;
						for (int i = 0; i <= num; i++)
						{
							if ((DGV2.SelectedCells[i].ColumnIndex == e.ColumnIndex) & (DGV2.SelectedCells[i].RowIndex <= Public2.PRJ.CPG[SelRowDGV1].CP.GetUpperBound(0)))
							{
								Public2.PRJ.CPG[SelRowDGV1].CP[DGV2.SelectedCells[i].RowIndex].CoordM.Xm = Module1.QSNG(sTR);
								DGV2[e.ColumnIndex, DGV2.SelectedCells[i].RowIndex].Value = Strings.Format(Module1.QSNG(sTR), "0.0").ToString();
								Public2.PRJ.CPG[SelRowDGV1].CP[DGV2.SelectedCells[i].RowIndex].CoordWM = CalcCoords.ConvMetersToWebMercMeters(Public2.PRJ.CPG[SelRowDGV1].CP[DGV2.SelectedCells[i].RowIndex].CoordM);
								CalcCoords.ConvWebMercMetersToWGS84(Public2.PRJ.CPG[SelRowDGV1].CP[DGV2.SelectedCells[i].RowIndex].CoordWM, ref LatWgs2, ref LonWgs2);
								DGV2[0, DGV2.SelectedCells[i].RowIndex].Value = Strings.Format(LatWgs2, "0.000000").ToString();
								DGV2[1, DGV2.SelectedCells[i].RowIndex].Value = Strings.Format(LonWgs2, "0.000000").ToString();
							}
						}
						DGV2.ClearSelection();
						FlagGroupChangeDGV2 = false;
					}
				}
				else
				{
					DGV2[e.ColumnIndex, e.RowIndex].Value = Strings.Format(Public2.PRJ.CPG[SelRowDGV1].CP[e.RowIndex].CoordM.Xm, "0.0").ToString();
				}
			}
			else if (e.ColumnIndex == 3)
			{
				if (Versioned.IsNumeric(RuntimeHelpers.GetObjectValue(DGV2[e.ColumnIndex, e.RowIndex].Value)))
				{
					if (DGV2.SelectedCells.Count == 1)
					{
						Public2.PRJ.CPG[SelRowDGV1].CP[e.RowIndex].CoordM.Ym = Conversions.ToDouble(DGV2[e.ColumnIndex, e.RowIndex].Value);
						DGV2[e.ColumnIndex, e.RowIndex].Value = Strings.Format(Public2.PRJ.CPG[SelRowDGV1].CP[e.RowIndex].CoordM.Ym, "0.0").ToString();
						Public2.PRJ.CPG[SelRowDGV1].CP[e.RowIndex].CoordWM = CalcCoords.ConvMetersToWebMercMeters(Public2.PRJ.CPG[SelRowDGV1].CP[e.RowIndex].CoordM);
						double LatWgs3 = default;
						double LonWgs3 = default;
						CalcCoords.ConvWebMercMetersToWGS84(Public2.PRJ.CPG[SelRowDGV1].CP[e.RowIndex].CoordWM, ref LatWgs3, ref LonWgs3);
						DGV2[0, e.RowIndex].Value = Strings.Format(LatWgs3, "0.000000");
						DGV2[1, e.RowIndex].Value = Strings.Format(LonWgs3, "0.000000");
						if (e.RowIndex == DGV2.RowCount - 1)
						{
							MyProject.Forms.Form1.PB.Refresh();
						}
					}
					else
					{
						if (DGV2.SelectedCells.Count <= 1)
						{
							return;
						}
						FlagGroupChangeDGV2 = true;
						string sTR2 = Conversions.ToString(DGV2[e.ColumnIndex, e.RowIndex].Value);
						int num2 = DGV2.SelectedCells.Count - 1;
						double LatWgs4 = default;
						double LonWgs4 = default;
						for (int j = 0; j <= num2; j++)
						{
							if ((DGV2.SelectedCells[j].ColumnIndex == e.ColumnIndex) & (DGV2.SelectedCells[j].RowIndex <= Public2.PRJ.CPG[SelRowDGV1].CP.GetUpperBound(0)))
							{
								Public2.PRJ.CPG[SelRowDGV1].CP[DGV2.SelectedCells[j].RowIndex].CoordM.Ym = Module1.QSNG(sTR2);
								DGV2[e.ColumnIndex, DGV2.SelectedCells[j].RowIndex].Value = Strings.Format(Module1.QSNG(sTR2), "0.0").ToString();
								Public2.PRJ.CPG[SelRowDGV1].CP[DGV2.SelectedCells[j].RowIndex].CoordWM = CalcCoords.ConvMetersToWebMercMeters(Public2.PRJ.CPG[SelRowDGV1].CP[DGV2.SelectedCells[j].RowIndex].CoordM);
								CalcCoords.ConvWebMercMetersToWGS84(Public2.PRJ.CPG[SelRowDGV1].CP[DGV2.SelectedCells[j].RowIndex].CoordWM, ref LatWgs4, ref LonWgs4);
								DGV2[0, DGV2.SelectedCells[j].RowIndex].Value = Strings.Format(LatWgs4, "0.000000").ToString();
								DGV2[1, DGV2.SelectedCells[j].RowIndex].Value = Strings.Format(LonWgs4, "0.000000").ToString();
							}
						}
						DGV2.ClearSelection();
						FlagGroupChangeDGV2 = false;
					}
				}
				else
				{
					DGV2[e.ColumnIndex, e.RowIndex].Value = Strings.Format(Public2.PRJ.CPG[SelRowDGV1].CP[e.RowIndex].CoordM.Ym, "0.0").ToString();
				}
			}
			else if (e.ColumnIndex == 4)
			{
				if (DGV2.SelectedCells.Count == 1)
				{
					Public2.PRJ.CPG[SelRowDGV1].CP[e.RowIndex].Location = Conversions.ToString(DGV2[e.ColumnIndex, e.RowIndex].Value);
				}
				else
				{
					if (DGV2.SelectedCells.Count <= 1)
					{
						return;
					}
					FlagGroupChangeDGV2 = true;
					string text = Conversions.ToString(DGV2[e.ColumnIndex, e.RowIndex].Value);
					int num3 = DGV2.SelectedCells.Count - 1;
					for (int k = 0; k <= num3; k++)
					{
						if ((DGV2.SelectedCells[k].ColumnIndex == e.ColumnIndex) & (DGV2.SelectedCells[k].RowIndex <= Public2.PRJ.CPG[SelRowDGV1].CP.GetUpperBound(0)))
						{
							Public2.PRJ.CPG[SelRowDGV1].CP[DGV2.SelectedCells[k].RowIndex].Location = text;
							DGV2[e.ColumnIndex, DGV2.SelectedCells[k].RowIndex].Value = text;
						}
					}
					DGV2.ClearSelection();
					FlagGroupChangeDGV2 = false;
				}
			}
			else
			{
				if (e.ColumnIndex != 5)
				{
					return;
				}
				if (DGV2.SelectedCells.Count == 1)
				{
					Public2.PRJ.CPG[SelRowDGV1].CP[e.RowIndex].Name = Conversions.ToString(DGV2[e.ColumnIndex, e.RowIndex].Value);
				}
				else
				{
					if (DGV2.SelectedCells.Count <= 1)
					{
						return;
					}
					FlagGroupChangeDGV2 = true;
					string text2 = Conversions.ToString(DGV2[e.ColumnIndex, e.RowIndex].Value);
					int num4 = DGV2.SelectedCells.Count - 1;
					for (int l = 0; l <= num4; l++)
					{
						if ((DGV2.SelectedCells[l].ColumnIndex == e.ColumnIndex) & (DGV2.SelectedCells[l].RowIndex <= Public2.PRJ.CPG[SelRowDGV1].CP.GetUpperBound(0)))
						{
							Public2.PRJ.CPG[SelRowDGV1].CP[DGV2.SelectedCells[l].RowIndex].Name = text2;
							DGV2[e.ColumnIndex, DGV2.SelectedCells[l].RowIndex].Value = text2;
						}
					}
					DGV2.ClearSelection();
					FlagGroupChangeDGV2 = false;
				}
			}
		}
	}

	private void DGV2_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Delete || Information.IsNothing(Public2.PRJ.CPG) || Information.IsNothing(Public2.PRJ.CPG[SelRowDGV1].CP) || DGV2.SelectedRows.Count < 1 || ((DGV2.SelectedRows.Count == 1) & (DGV2.SelectedRows[0].Index > Public2.PRJ.CPG[SelRowDGV1].CP.GetUpperBound(0))))
		{
			return;
		}
		short[] array = new short[1];
		short num = 0;
		checked
		{
			int num2 = DGV2.SelectedRows.Count - 1;
			for (int i = 0; i <= num2; i++)
			{
				if (DGV2.SelectedRows[i].Index <= Public2.PRJ.CPG[SelRowDGV1].CP.GetUpperBound(0))
				{
					array = (short[])Utils.CopyArray(array, new short[num + 1]);
					array[num] = (short)DGV2.SelectedRows[i].Index;
					num++;
				}
			}
			if (Interaction.MsgBox(Localization.LocMSG(0) + " " + Conversions.ToString(unchecked((int)num)) + " " + Localization.LocMSG(48) + "?", MsgBoxStyle.YesNo) != MsgBoxResult.Yes)
			{
				return;
			}
			int upperBound = array.GetUpperBound(0);
			for (int j = 0; j <= upperBound; j++)
			{
				int num3 = j + 1;
				int upperBound2 = array.GetUpperBound(0);
				for (int k = num3; k <= upperBound2; k++)
				{
					if (array[k] > array[j])
					{
						short num4 = array[j];
						array[j] = array[k];
						array[k] = num4;
					}
				}
			}
			short num5 = 0;
			int upperBound3 = array.GetUpperBound(0);
			for (int l = 0; l <= upperBound3; l++)
			{
				num5++;
				short num6 = array[l];
				int num7 = Public2.PRJ.CPG[SelRowDGV1].CP.GetUpperBound(0) - 1;
				for (int m = num6; m <= num7; m++)
				{
					Public2.PRJ.CPG[SelRowDGV1].CP[m] = Module1.DeepClone(Public2.PRJ.CPG[SelRowDGV1].CP[m + 1]);
				}
			}
			ref Public2.ControlPoint[] cP = ref Public2.PRJ.CPG[SelRowDGV1].CP;
			cP = (Public2.ControlPoint[])Utils.CopyArray(cP, new Public2.ControlPoint[Public2.PRJ.CPG[SelRowDGV1].CP.GetUpperBound(0) - num5 + 1]);
			if (Public2.PRJ.CPG[SelRowDGV1].CP.Length == 0)
			{
				Public2.PRJ.CPG[SelRowDGV1].CP = null;
			}
			FlagPaintAllow = false;
			FillDGV2();
			FlagPaintAllow = true;
			DGV2.ClearSelection();
		}
	}

	private void DGV2_MouseClick(object sender, MouseEventArgs e)
	{
		if (!((DGV2.HitTest(e.X, e.Y).RowIndex < 0) | (DGV2.HitTest(e.X, e.Y).ColumnIndex < 0)) && ((e.Button == MouseButtons.Left) & (DGV2.HitTest(e.X, e.Y).ColumnIndex == 4)))
		{
			ContLocation.Show(DGV2, e.Location);
		}
	}

	private void ContLocation_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
	{
		if (Operators.CompareString(e.ClickedItem.Text, "", TextCompare: false) != 0)
		{
			DGV2[4, DGV2.CurrentCell.RowIndex].Value = e.ClickedItem.Text;
		}
	}

	public void FillDGV3()
	{
		FlagFillDGVisGoingOn = true;
		DGV3.Rows.Clear();
		if ((SelRowDGV1 < 0) | (SelRowDGV2 < 0))
		{
			DGV3.Enabled = false;
			FlagFillDGVisGoingOn = false;
			return;
		}
		DGV3.Enabled = true;
		if (SelRowDGV2 > Public2.PRJ.CPG[SelRowDGV1].CP.GetUpperBound(0))
		{
			FlagFillDGVisGoingOn = false;
			return;
		}
		int upperBound = Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H.GetUpperBound(0);
		checked
		{
			for (int i = 0; i <= upperBound; i++)
			{
				DGV3.Rows.Add(Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H[i].H, Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H[i].L, Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H[i].MeasuredE, Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H[i].Name);
				DGV3.Rows[i].HeaderCell.Value = (i + 1).ToString();
			}
			FlagFillDGVisGoingOn = false;
		}
	}

	private void DGV3_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
		if (FlagFillDGVisGoingOn || FlagGroupChangeDGV3 || e.RowIndex < 0)
		{
			return;
		}
		checked
		{
			if (e.RowIndex > Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H.GetUpperBound(0))
			{
				ref Public2.ControlPointHeight[] h = ref Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H;
				h = (Public2.ControlPointHeight[])Utils.CopyArray(h, new Public2.ControlPointHeight[e.RowIndex + 1]);
				Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H[e.RowIndex].H = 2f;
				DGV3.Rows[e.RowIndex].HeaderCell.Value = (e.RowIndex + 1).ToString();
				if (e.ColumnIndex == 0)
				{
					DGV3[1, e.RowIndex].Value = 0;
					DGV3[2, e.RowIndex].Value = 0;
				}
				else if (e.ColumnIndex == 1)
				{
					DGV3[0, e.RowIndex].Value = 2;
					DGV3[2, e.RowIndex].Value = 0;
				}
				else if (e.ColumnIndex == 2)
				{
					DGV3[0, e.RowIndex].Value = 2;
					DGV3[1, e.RowIndex].Value = 0;
				}
				else if (e.ColumnIndex == 3)
				{
					DGV3[0, e.RowIndex].Value = 2;
					DGV3[1, e.RowIndex].Value = 0;
					DGV3[2, e.RowIndex].Value = 0;
				}
			}
			if (e.ColumnIndex == 0)
			{
				if (Versioned.IsNumeric(RuntimeHelpers.GetObjectValue(DGV3[e.ColumnIndex, e.RowIndex].Value)))
				{
					if (Operators.ConditionalCompareObjectGreaterEqual(DGV3[e.ColumnIndex, e.RowIndex].Value, 0, TextCompare: false))
					{
						Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H[e.RowIndex].H = Conversions.ToSingle(DGV3[e.ColumnIndex, e.RowIndex].Value);
						DGV3[e.ColumnIndex, e.RowIndex].Value = Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H[e.RowIndex].H;
					}
					else
					{
						DGV3[e.ColumnIndex, e.RowIndex].Value = Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H[e.RowIndex].H;
					}
				}
				else
				{
					DGV3[e.ColumnIndex, e.RowIndex].Value = Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H[e.RowIndex].H;
				}
			}
			else if (e.ColumnIndex == 1)
			{
				if (Versioned.IsNumeric(RuntimeHelpers.GetObjectValue(DGV3[e.ColumnIndex, e.RowIndex].Value)))
				{
					if (Operators.ConditionalCompareObjectGreaterEqual(DGV3[e.ColumnIndex, e.RowIndex].Value, 0, TextCompare: false))
					{
						if (DGV3.SelectedCells.Count == 1)
						{
							Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H[e.RowIndex].L = Conversions.ToSingle(DGV3[e.ColumnIndex, e.RowIndex].Value);
							DGV3[e.ColumnIndex, e.RowIndex].Value = Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H[e.RowIndex].L;
						}
						else if (DGV3.SelectedCells.Count > 1)
						{
							FlagGroupChangeDGV3 = true;
							string sTR = Conversions.ToString(DGV3[e.ColumnIndex, e.RowIndex].Value);
							int num = DGV3.SelectedCells.Count - 1;
							for (int i = 0; i <= num; i++)
							{
								if ((DGV3.SelectedCells[i].ColumnIndex == e.ColumnIndex) & (DGV3.SelectedCells[i].RowIndex <= Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H.GetUpperBound(0)))
								{
									Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H[DGV3.SelectedCells[i].RowIndex].L = Module1.QSNG(sTR);
									DGV3[e.ColumnIndex, DGV3.SelectedCells[i].RowIndex].Value = Module1.QSNG(sTR);
								}
							}
							DGV3.ClearSelection();
							FlagGroupChangeDGV3 = false;
						}
					}
					else
					{
						DGV3[e.ColumnIndex, e.RowIndex].Value = Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H[e.RowIndex].L;
					}
				}
				else
				{
					DGV3[e.ColumnIndex, e.RowIndex].Value = Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H[e.RowIndex].L;
				}
			}
			else if (e.ColumnIndex == 2)
			{
				if (Versioned.IsNumeric(RuntimeHelpers.GetObjectValue(DGV3[e.ColumnIndex, e.RowIndex].Value)))
				{
					if (Operators.ConditionalCompareObjectGreaterEqual(DGV3[e.ColumnIndex, e.RowIndex].Value, 0, TextCompare: false))
					{
						if (DGV3.SelectedCells.Count == 1)
						{
							Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H[e.RowIndex].MeasuredE = Module1.QSNG(Conversions.ToString(DGV3[e.ColumnIndex, e.RowIndex].Value));
							DGV3[e.ColumnIndex, e.RowIndex].Value = Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H[e.RowIndex].MeasuredE;
						}
						else if (DGV3.SelectedCells.Count > 1)
						{
							FlagGroupChangeDGV3 = true;
							string sTR2 = Conversions.ToString(DGV3[e.ColumnIndex, e.RowIndex].Value);
							int num2 = DGV3.SelectedCells.Count - 1;
							for (int j = 0; j <= num2; j++)
							{
								if ((DGV3.SelectedCells[j].ColumnIndex == e.ColumnIndex) & (DGV3.SelectedCells[j].RowIndex <= Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H.GetUpperBound(0)))
								{
									Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H[DGV3.SelectedCells[j].RowIndex].MeasuredE = Module1.QSNG(sTR2);
									DGV3[e.ColumnIndex, DGV3.SelectedCells[j].RowIndex].Value = Module1.QSNG(sTR2);
								}
							}
							DGV3.ClearSelection();
							FlagGroupChangeDGV3 = false;
						}
					}
					else
					{
						DGV3[e.ColumnIndex, e.RowIndex].Value = Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H[e.RowIndex].MeasuredE;
					}
				}
				else
				{
					DGV3[e.ColumnIndex, e.RowIndex].Value = Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H[e.RowIndex].MeasuredE;
				}
			}
			else if (e.ColumnIndex == 3)
			{
				if (DGV3.SelectedCells.Count == 1)
				{
					Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H[e.RowIndex].Name = Conversions.ToString(DGV3[e.ColumnIndex, e.RowIndex].Value);
				}
				else if (DGV3.SelectedCells.Count > 1)
				{
					FlagGroupChangeDGV3 = true;
					string text = Conversions.ToString(DGV3[e.ColumnIndex, e.RowIndex].Value);
					int num3 = DGV3.SelectedCells.Count - 1;
					for (int k = 0; k <= num3; k++)
					{
						if ((DGV3.SelectedCells[k].ColumnIndex == e.ColumnIndex) & (DGV3.SelectedCells[k].RowIndex <= Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H.GetUpperBound(0)))
						{
							Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H[DGV3.SelectedCells[k].RowIndex].Name = text;
							DGV3[e.ColumnIndex, DGV3.SelectedCells[k].RowIndex].Value = text;
						}
					}
					DGV3.ClearSelection();
					FlagGroupChangeDGV3 = false;
				}
			}
			if (FlagPaintAllow)
			{
				MyProject.Forms.Form1.PB.Refresh();
			}
		}
	}

	private void DGV3_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Delete || Information.IsNothing(Public2.PRJ.CPG) || Information.IsNothing(Public2.PRJ.CPG[SelRowDGV1].CP) || Information.IsNothing(Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H) || DGV3.SelectedRows.Count < 1 || ((DGV3.SelectedRows.Count == 1) & (DGV3.SelectedRows[0].Index > Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H.GetUpperBound(0))))
		{
			return;
		}
		checked
		{
			if (DGV3.SelectedRows.Count - 1 >= Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H.GetUpperBound(0))
			{
				Interaction.MsgBox(Localization.LocMSG(51), MsgBoxStyle.Critical);
				return;
			}
			short[] array = new short[1];
			short num = 0;
			int num2 = DGV3.SelectedRows.Count - 1;
			for (int i = 0; i <= num2; i++)
			{
				if (DGV3.SelectedRows[i].Index <= Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H.GetUpperBound(0))
				{
					array = (short[])Utils.CopyArray(array, new short[num + 1]);
					array[num] = (short)DGV3.SelectedRows[i].Index;
					num++;
				}
			}
			if (Interaction.MsgBox(Localization.LocMSG(0) + " " + Conversions.ToString(unchecked((int)num)) + " " + Localization.LocMSG(50) + "?", MsgBoxStyle.YesNo) != MsgBoxResult.Yes)
			{
				return;
			}
			int upperBound = array.GetUpperBound(0);
			for (int j = 0; j <= upperBound; j++)
			{
				int num3 = j + 1;
				int upperBound2 = array.GetUpperBound(0);
				for (int k = num3; k <= upperBound2; k++)
				{
					if (array[k] > array[j])
					{
						short num4 = array[j];
						array[j] = array[k];
						array[k] = num4;
					}
				}
			}
			short num5 = 0;
			int upperBound3 = array.GetUpperBound(0);
			for (int l = 0; l <= upperBound3; l++)
			{
				num5++;
				short num6 = array[l];
				int num7 = Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H.GetUpperBound(0) - 1;
				for (int m = num6; m <= num7; m++)
				{
					Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H[m] = Module1.DeepClone(Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H[m + 1]);
				}
			}
			ref Public2.ControlPointHeight[] h = ref Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H;
			h = (Public2.ControlPointHeight[])Utils.CopyArray(h, new Public2.ControlPointHeight[Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].H.GetUpperBound(0) - num5 + 1]);
			FlagPaintAllow = false;
			FillDGV3();
			FlagPaintAllow = true;
			DGV3.ClearSelection();
			if (FlagPaintAllow)
			{
				MyProject.Forms.Form1.PB.Refresh();
			}
		}
	}

	private void cmdPosition_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if ((SelRowDGV1 < 0) | (SelRowDGV2 < 0))
		{
			Interaction.MsgBox(Localization.LocMSG(45), MsgBoxStyle.Critical);
		}
		else if (DGV2.CurrentCell.RowIndex >= 0)
		{
			MyProject.Forms.Form1.MapPositionWM(Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].CoordWM.Xwm, Public2.PRJ.CPG[SelRowDGV1].CP[SelRowDGV2].CoordWM.Ywm);
		}
	}

	private void cmdContrPointToCenterScreen_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		checked
		{
			if (DGV2.SelectedRows.Count == 0)
			{
				Interaction.MsgBox(Localization.LocMSG(45), MsgBoxStyle.Critical);
			}
			else if (Interaction.MsgBox(Localization.LocMSG(53), MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
			{
				Public2.CoordWebMerc screenCenterWM = Form1_Public.GetScreenCenterWM();
				int num = DGV2.SelectedRows.Count - 1;
				for (int i = 0; i <= num; i++)
				{
					Public2.PRJ.CPG[SelRowDGV1].CP[DGV2.SelectedRows[i].Index].CoordWM = screenCenterWM;
				}
				MyProject.Forms.Form1.PB.Refresh();
				FlagPaintAllow = false;
				FillDGV1();
				FlagPaintAllow = true;
			}
		}
	}

	private void cmdAddControlPoint_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if ((SelRowDGV1 == -1) | Information.IsNothing(Public2.PRJ.CPG))
		{
			Interaction.MsgBox(Localization.LocMSG(52), MsgBoxStyle.Critical);
			return;
		}
		if (Information.IsNothing(Public2.PRJ.CPG[SelRowDGV1].CP))
		{
			Public2.PRJ.CPG[SelRowDGV1].CP = new Public2.ControlPoint[1];
			Public2.PRJ.CPG[SelRowDGV1].CP[0].H = new Public2.ControlPointHeight[1];
			Public2.PRJ.CPG[SelRowDGV1].CP[0].CoordWM = Form1_Public.GetScreenCenterWM();
			Public2.PRJ.CPG[SelRowDGV1].CP[0].Location = "Ground";
			Public2.PRJ.CPG[SelRowDGV1].CP[0].H[0].H = 2f;
		}
		else
		{
			ref Public2.ControlPoint[] cP = ref Public2.PRJ.CPG[SelRowDGV1].CP;
			cP = (Public2.ControlPoint[])Utils.CopyArray(cP, new Public2.ControlPoint[checked(Public2.PRJ.CPG[SelRowDGV1].CP.GetUpperBound(0) + 1 + 1)]);
			Public2.PRJ.CPG[SelRowDGV1].CP[Public2.PRJ.CPG[SelRowDGV1].CP.GetUpperBound(0)].H = new Public2.ControlPointHeight[1];
			Public2.PRJ.CPG[SelRowDGV1].CP[Public2.PRJ.CPG[SelRowDGV1].CP.GetUpperBound(0)].CoordWM = Form1_Public.GetScreenCenterWM();
			Public2.PRJ.CPG[SelRowDGV1].CP[Public2.PRJ.CPG[SelRowDGV1].CP.GetUpperBound(0)].Location = "Ground";
			Public2.PRJ.CPG[SelRowDGV1].CP[Public2.PRJ.CPG[SelRowDGV1].CP.GetUpperBound(0)].H[0].H = 2f;
		}
		FlagPaintAllow = false;
		FillDGV2();
		FlagPaintAllow = true;
		DGV2.CurrentCell = DGV2[0, Public2.PRJ.CPG[SelRowDGV1].CP.GetUpperBound(0)];
		Public2.PRJ.CPG[SelRowDGV1].CP[Public2.PRJ.CPG[SelRowDGV1].CP.GetUpperBound(0)].CoordM = CalcCoords.ConvWebMercMetersToMetersRound(Public2.PRJ.CPG[SelRowDGV1].CP[Public2.PRJ.CPG[SelRowDGV1].CP.GetUpperBound(0)].CoordWM);
	}

	private void cmdMovePointsToAzimuth_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Information.IsNothing(Public2.PRJ.CPG) || Information.IsNothing(Public2.PRJ.TX))
		{
			return;
		}
		int upperBound = Public2.PRJ.CPG.GetUpperBound(0);
		checked
		{
			Public2.CoordWebMerc p = default;
			double L = default;
			Public2.CoordWebMerc coordWebMerc = default;
			for (int i = 0; i <= upperBound; i++)
			{
				if (!Public2.PRJ.CPG[i].Active || Information.IsNothing(Public2.PRJ.CPG[i].CP))
				{
					continue;
				}
				int upperBound2 = Public2.PRJ.CPG[i].CP.GetUpperBound(0);
				for (int j = 0; j <= upperBound2; j++)
				{
					double num = 999.0;
					int upperBound3 = Public2.PRJ.TX.GetUpperBound(0);
					for (int k = 0; k <= upperBound3; k++)
					{
						if (!Public2.PRJ.TX[k].Active)
						{
							continue;
						}
						float num2 = Public2.PRJ.TX[k].Az;
						if (num2 == -1f)
						{
							num2 = 0f;
						}
						p.Xwm = Public2.PRJ.TX[k].CoordWM.Xwm + 1000.0 * Math.Sin((double)num2 / (180.0 / Math.PI));
						p.Ywm = Public2.PRJ.TX[k].CoordWM.Ywm + 1000.0 * Math.Cos((double)num2 / (180.0 / Math.PI));
						if (Module1.PointLine(Public2.PRJ.TX[k].CoordWM, p, Public2.PRJ.CPG[i].CP[j].CoordWM, 5.0, ref L))
						{
							double num3 = ((p.Xwm - Public2.PRJ.TX[k].CoordWM.Xwm) * (Public2.PRJ.CPG[i].CP[j].CoordWM.Xwm - Public2.PRJ.TX[k].CoordWM.Xwm) + (p.Ywm - Public2.PRJ.TX[k].CoordWM.Ywm) * (Public2.PRJ.CPG[i].CP[j].CoordWM.Ywm - Public2.PRJ.TX[k].CoordWM.Ywm)) / (Math.Pow(p.Xwm - Public2.PRJ.TX[k].CoordWM.Xwm, 2.0) + Math.Pow(p.Ywm - Public2.PRJ.TX[k].CoordWM.Ywm, 2.0));
							if (((num3 > 0.0) & (num3 < 1.0)) && L < num)
							{
								num = L;
								coordWebMerc.Xwm = Public2.PRJ.TX[k].CoordWM.Xwm + (p.Xwm - Public2.PRJ.TX[k].CoordWM.Xwm) * num3;
								coordWebMerc.Ywm = Public2.PRJ.TX[k].CoordWM.Ywm + (p.Ywm - Public2.PRJ.TX[k].CoordWM.Ywm) * num3;
							}
						}
					}
					if (num < 5.0)
					{
						Public2.PRJ.CPG[i].CP[j].CoordWM.Xwm = coordWebMerc.Xwm;
						Public2.PRJ.CPG[i].CP[j].CoordWM.Ywm = coordWebMerc.Ywm;
					}
				}
			}
			FlagPaintAllow = false;
			FillDGV1();
			FlagPaintAllow = true;
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void CmdUp_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		DGV1.EndEdit();
		if (Information.IsNothing(Public2.PRJ.CPG) || DGV1.SelectedRows.Count < 1 || ((DGV1.SelectedRows.Count == 1) & (DGV1.SelectedRows[0].Index > Public2.PRJ.CPG.GetUpperBound(0))))
		{
			return;
		}
		checked
		{
			int[] array = new int[DGV1.SelectedRows.Count - 1 + 1];
			int num = DGV1.SelectedRows.Count - 1;
			for (int i = 0; i <= num; i++)
			{
				array[i] = DGV1.SelectedRows[i].Index;
			}
			int upperBound = array.GetUpperBound(0);
			for (int j = 0; j <= upperBound; j++)
			{
				int num2 = j + 1;
				int upperBound2 = array.GetUpperBound(0);
				for (int k = num2; k <= upperBound2; k++)
				{
					if (array[k] < array[j])
					{
						int num3 = array[j];
						array[j] = array[k];
						array[k] = num3;
					}
				}
			}
			if (array[0] != 0 && array[array.GetUpperBound(0)] != DGV1.Rows.Count - 1)
			{
				int upperBound3 = array.GetUpperBound(0);
				for (int l = 0; l <= upperBound3; l++)
				{
					Public2.ControlPointGroup obj = Module1.DeepClone(Public2.PRJ.CPG[array[l] - 1]);
					Public2.PRJ.CPG[array[l] - 1] = Module1.DeepClone(Public2.PRJ.CPG[array[l]]);
					Public2.PRJ.CPG[array[l]] = Module1.DeepClone(obj);
				}
				FlagPaintAllow = false;
				FillDGV1();
				FlagPaintAllow = true;
				DGV1.CurrentCell = DGV1[0, array[array.GetUpperBound(0)] - 1];
				int upperBound4 = array.GetUpperBound(0);
				for (int m = 0; m <= upperBound4; m++)
				{
					DGV1.Rows[array[m] - 1].Selected = true;
				}
				DGV1.Focus();
			}
		}
	}

	private void CmdDown_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		DGV1.EndEdit();
		if (Information.IsNothing(Public2.PRJ.CPG) || DGV1.SelectedRows.Count < 1 || ((DGV1.SelectedRows.Count == 1) & (DGV1.SelectedRows[0].Index > Public2.PRJ.CPG.GetUpperBound(0))))
		{
			return;
		}
		checked
		{
			int[] array = new int[DGV1.SelectedRows.Count - 1 + 1];
			int num = DGV1.SelectedRows.Count - 1;
			for (int i = 0; i <= num; i++)
			{
				array[i] = DGV1.SelectedRows[i].Index;
			}
			int upperBound = array.GetUpperBound(0);
			for (int j = 0; j <= upperBound; j++)
			{
				int num2 = j + 1;
				int upperBound2 = array.GetUpperBound(0);
				for (int k = num2; k <= upperBound2; k++)
				{
					if (array[k] < array[j])
					{
						int num3 = array[j];
						array[j] = array[k];
						array[k] = num3;
					}
				}
			}
			if (array[array.GetUpperBound(0)] < DGV1.Rows.Count - 2)
			{
				for (int l = array.GetUpperBound(0); l >= 0; l += -1)
				{
					Public2.ControlPointGroup obj = Module1.DeepClone(Public2.PRJ.CPG[array[l] + 1]);
					Public2.PRJ.CPG[array[l] + 1] = Module1.DeepClone(Public2.PRJ.CPG[array[l]]);
					Public2.PRJ.CPG[array[l]] = Module1.DeepClone(obj);
				}
				FlagPaintAllow = false;
				FillDGV1();
				FlagPaintAllow = true;
				DGV1.CurrentCell = DGV1[0, array[array.GetUpperBound(0)] + 1];
				int upperBound3 = array.GetUpperBound(0);
				for (int m = 0; m <= upperBound3; m++)
				{
					DGV1.Rows[array[m] + 1].Selected = true;
				}
				DGV1.Focus();
			}
		}
	}

	private void cmdCalcAllPoints_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (!Information.IsNothing(Public2.PRJ.CPG) && !Information.IsNothing(Public2.PRJ.TX) && CalcSZZ.CalcTmp())
		{
			MyProject.Forms.frmCalcAllControlPoints.ShowDialog();
		}
	}

	private void cmdCalcOnePoint_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (!Information.IsNothing(Public2.PRJ.CPG) && !Information.IsNothing(Public2.PRJ.TX))
		{
			if ((SelRowDGV1 < 0) | (SelRowDGV2 < 0))
			{
				Interaction.MsgBox(Localization.LocMSG(45), MsgBoxStyle.Critical);
			}
			else if (DGV2.CurrentCell.RowIndex >= 0 && CalcSZZ.CalcTmp())
			{
				MyProject.Forms.frmCalcOneControlPoint.Text = "Calculation results for Control Point " + Conversions.ToString(checked(SelRowDGV2 + 1)) + " (" + Public2.PRJ.CPG[SelRowDGV1].Name + ")";
				MyProject.Forms.frmCalcOneControlPoint.ShowDialog();
			}
		}
	}

	private void cmdWord_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		DGV1.EndEdit();
		ContWord.Show(this, cmdWord.Location);
	}

	private void cmdControlPoints_1_Click(object sender, EventArgs e)
	{
		if (Information.IsNothing(Public2.PRJ.TX))
		{
			Interaction.MsgBox(Localization.LocMSG(46));
		}
		else if (Information.IsNothing(Public2.PRJ.CPG))
		{
			Interaction.MsgBox(Localization.LocMSG(47));
		}
		else if (CalcSZZ.CalcTmp())
		{
			RepWordAlias.WordReportOneAlias("{ControlPoints_1}");
		}
	}

	private void cmdControlPoints_2_Click(object sender, EventArgs e)
	{
		if (Information.IsNothing(Public2.PRJ.TX))
		{
			Interaction.MsgBox(Localization.LocMSG(46));
		}
		else if (Information.IsNothing(Public2.PRJ.CPG))
		{
			Interaction.MsgBox(Localization.LocMSG(47));
		}
		else if (CalcSZZ.CalcTmp())
		{
			RepWordAlias.WordReportOneAlias("{ControlPoints_2}");
		}
	}

	private void cmdExcel_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		DGV1.EndEdit();
		ContExcel.Show(this, cmdExcel.Location);
	}

	private void cmdExcelExport_Click(object sender, EventArgs e)
	{
		ExcelReports.ExcelExportControlPoint1();
	}

	private void cmdExcelImport_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if ((SelRowDGV1 == -1) | Information.IsNothing(Public2.PRJ.CPG))
		{
			Interaction.MsgBox(Localization.LocMSG(52), MsgBoxStyle.Critical);
			return;
		}
		using OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.Filter = "Excel files|*.xlsx;*.xls";
		openFileDialog.AddExtension = true;
		if (openFileDialog.ShowDialog() != DialogResult.OK)
		{
			return;
		}
		object objectValue = RuntimeHelpers.GetObjectValue(Interaction.CreateObject("Excel.Application"));
		object instance = NewLateBinding.LateGet(objectValue, null, "workbooks", new object[0], null, null, null);
		OpenFileDialog openFileDialog2;
		object[] array = new object[1] { (openFileDialog2 = openFileDialog).FileName };
		object[] array2 = array;
		bool[] array3 = new bool[1] { true };
		bool[] array4 = array3;
		NewLateBinding.LateCall(instance, null, "open", array, null, null, array3, IgnoreReturn: true);
		if (array4[0])
		{
			openFileDialog2.FileName = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array2[0]), typeof(string));
		}
		string name = Conversions.ToString(NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "sheets", new object[1] { 1 }, null, null, null), null, "range", new object[1] { "B2" }, null, null, null), null, "value", new object[0], null, null, null));
		string stringCoord = Conversions.ToString(NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "sheets", new object[1] { 1 }, null, null, null), null, "range", new object[1] { "C2" }, null, null, null), null, "value", new object[0], null, null, null));
		string stringCoord2 = Conversions.ToString(NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "sheets", new object[1] { 1 }, null, null, null), null, "range", new object[1] { "D2" }, null, null, null), null, "value", new object[0], null, null, null));
		double DoubleCoordOut = default;
		bool flag = CalcCoords.TestLatitude(stringCoord, ref DoubleCoordOut);
		double DoubleCoordOut2 = default;
		bool flag2 = CalcCoords.TestLongitude(stringCoord2, ref DoubleCoordOut2);
		if (!flag | !flag2)
		{
			Interaction.MsgBox(Localization.LocMSG(111));
			NewLateBinding.LateCall(NewLateBinding.LateGet(objectValue, null, "workbooks", new object[0], null, null, null), null, "close", new object[0], null, null, null, IgnoreReturn: true);
			objectValue = null;
			GC.Collect();
			return;
		}
		Public2.PRJ.CPG[SelRowDGV1].CP = null;
		short num = 2;
		Public2.CoordWebMerc CoordWM = default;
		while (!(!flag | !flag2))
		{
			checked
			{
				if (flag & flag2)
				{
					if (Information.IsNothing(Public2.PRJ.CPG[SelRowDGV1].CP))
					{
						Public2.PRJ.CPG[SelRowDGV1].CP = new Public2.ControlPoint[1];
					}
					else
					{
						ref Public2.ControlPoint[] cP = ref Public2.PRJ.CPG[SelRowDGV1].CP;
						cP = (Public2.ControlPoint[])Utils.CopyArray(cP, new Public2.ControlPoint[Public2.PRJ.CPG[SelRowDGV1].CP.GetUpperBound(0) + 1 + 1]);
					}
					CalcCoords.ConvWGS84toWebMercMeters(DoubleCoordOut, DoubleCoordOut2, ref CoordWM);
					Public2.PRJ.CPG[SelRowDGV1].CP[Public2.PRJ.CPG[SelRowDGV1].CP.GetUpperBound(0)].CoordWM = CoordWM;
					Public2.PRJ.CPG[SelRowDGV1].CP[Public2.PRJ.CPG[SelRowDGV1].CP.GetUpperBound(0)].Name = name;
					Public2.PRJ.CPG[SelRowDGV1].CP[Public2.PRJ.CPG[SelRowDGV1].CP.GetUpperBound(0)].Location = "Ground";
					Public2.PRJ.CPG[SelRowDGV1].CP[Public2.PRJ.CPG[SelRowDGV1].CP.GetUpperBound(0)].H = new Public2.ControlPointHeight[1];
					Public2.PRJ.CPG[SelRowDGV1].CP[Public2.PRJ.CPG[SelRowDGV1].CP.GetUpperBound(0)].H[0].H = 2f;
					Public2.PRJ.CPG[SelRowDGV1].CP[Public2.PRJ.CPG[SelRowDGV1].CP.GetUpperBound(0)].CoordM = CalcCoords.ConvWebMercMetersToMetersRound(Public2.PRJ.CPG[SelRowDGV1].CP[Public2.PRJ.CPG[SelRowDGV1].CP.GetUpperBound(0)].CoordWM);
				}
				num++;
			}
			name = Conversions.ToString(NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "sheets", new object[1] { 1 }, null, null, null), null, "range", new object[1] { "B" + Conversions.ToString((int)num) }, null, null, null), null, "value", new object[0], null, null, null));
			stringCoord = Conversions.ToString(NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "sheets", new object[1] { 1 }, null, null, null), null, "range", new object[1] { "C" + Conversions.ToString((int)num) }, null, null, null), null, "value", new object[0], null, null, null));
			string stringCoord3 = Conversions.ToString(NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "sheets", new object[1] { 1 }, null, null, null), null, "range", new object[1] { "D" + Conversions.ToString((int)num) }, null, null, null), null, "value", new object[0], null, null, null));
			flag = CalcCoords.TestLatitude(stringCoord, ref DoubleCoordOut);
			flag2 = CalcCoords.TestLongitude(stringCoord3, ref DoubleCoordOut2);
		}
		NewLateBinding.LateCall(NewLateBinding.LateGet(objectValue, null, "workbooks", new object[0], null, null, null), null, "close", new object[0], null, null, null, IgnoreReturn: true);
		FlagPaintAllow = false;
		FillDGV2();
		FlagPaintAllow = true;
	}

	private void txtControlPointPrefix_TextChanged(object sender, EventArgs e)
	{
		Public2.PRJ.SETT.ControlPointPrefix = txtControlPointPrefix.Text;
		MyProject.Forms.Form1.PB.Refresh();
	}

	private void rbLimit_CheckedChanged(object sender, EventArgs e)
	{
		if (!FlagProcessing)
		{
			if (rbLimit0.Checked)
			{
				Public2.PRJ.SETT.ControlPointLimitIndex = 0;
			}
			else if (rbLimit1.Checked)
			{
				Public2.PRJ.SETT.ControlPointLimitIndex = 1;
			}
			else if (rbLimit2.Checked)
			{
				Public2.PRJ.SETT.ControlPointLimitIndex = 2;
			}
			else if (rbLimit3.Checked)
			{
				Public2.PRJ.SETT.ControlPointLimitIndex = 3;
			}
		}
	}

	private void txtFreq_Validating(object sender, CancelEventArgs e)
	{
		TextBox textBox = txtFreq;
		string TextProperty = textBox.Text;
		ref float controlPointMesureFreq = ref Public2.PRJ.SETT.ControlPointMesureFreq;
		double DblVariable = controlPointMesureFreq;
		Module1.TextPropertyValidating(ref TextProperty, ref DblVariable, CheckLimits: true, 30f, 300000f, IncludeMin: true, IncludeMax: true, Message: true);
		controlPointMesureFreq = (float)DblVariable;
		textBox.Text = TextProperty;
	}
}
