using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Xml;
using EMFPlanner.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[DesignerGenerated]
public class UC_Buildings : UserControl
{
	private IContainer components;

	private bool FlagOK;

	[field: AccessedThroughProperty("GroupBox1")]
	internal virtual GroupBox GroupBox1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ToolTip1")]
	internal virtual ToolTip ToolTip1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdGetBldFromCloud")]
	internal virtual Button cmdGetBldFromCloud
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdGetBldFromCloud_Click;
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

	[field: AccessedThroughProperty("cmdAddBuilding")]
	internal virtual Button cmdAddBuilding
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = cmdAddBuilding_MouseClick;
			Button button = field;
			if (button != null)
			{
				button.MouseClick -= value2;
			}
			field = value;
			button = field;
			if (button != null)
			{
				button.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmdSelect")]
	internal virtual Button cmdSelect
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdSelect_Click;
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

	[field: AccessedThroughProperty("cmdDelAllBuildings")]
	internal virtual Button cmdDelAllBuildings
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdDelAllBuildings_Click;
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

	[field: AccessedThroughProperty("gbBuildingParameters")]
	internal virtual GroupBox gbBuildingParameters
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label7")]
	internal virtual Label Label7
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtText")]
	internal virtual TextBox txtText
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			KeyEventHandler value2 = txtText_KeyUp;
			TextBox textBox = field;
			if (textBox != null)
			{
				textBox.KeyUp -= value2;
			}
			field = value;
			textBox = field;
			if (textBox != null)
			{
				textBox.KeyUp += value2;
			}
		}
	}

	[field: AccessedThroughProperty("Label4")]
	internal virtual Label Label4
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

	[field: AccessedThroughProperty("txtFundation")]
	internal virtual TextBox txtFundation
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			KeyEventHandler value2 = txtFundation_KeyUp;
			TextBox textBox = field;
			if (textBox != null)
			{
				textBox.KeyUp -= value2;
			}
			field = value;
			textBox = field;
			if (textBox != null)
			{
				textBox.KeyUp += value2;
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

	[field: AccessedThroughProperty("txtHeight")]
	internal virtual TextBox txtHeight
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			KeyEventHandler value2 = txtHeight_KeyUp;
			TextBox textBox = field;
			if (textBox != null)
			{
				textBox.KeyUp -= value2;
			}
			field = value;
			textBox = field;
			if (textBox != null)
			{
				textBox.KeyUp += value2;
			}
		}
	}

	[field: AccessedThroughProperty("chkShow")]
	internal virtual CheckBox chkShow
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkShow_MouseClick;
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

	[field: AccessedThroughProperty("cmdDelOneBuilding")]
	internal virtual Button cmdDelOneBuilding
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdDelOneBuilding_Click;
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

	[field: AccessedThroughProperty("cmdRedo")]
	internal virtual Button cmdRedo
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdRedo_Click;
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

	[field: AccessedThroughProperty("cmdUndo")]
	internal virtual Button cmdUndo
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdUndo_Click;
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

	[field: AccessedThroughProperty("cmdAddCopyBuilding")]
	internal virtual Button cmdAddCopyBuilding
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = cmdAddCopyBuilding_MouseClick;
			Button button = field;
			if (button != null)
			{
				button.MouseClick -= value2;
			}
			field = value;
			button = field;
			if (button != null)
			{
				button.MouseClick += value2;
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

	[field: AccessedThroughProperty("cmdDeleteNode")]
	internal virtual Button cmdDeleteNode
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = cmdDeleteNode_MouseClick;
			Button button = field;
			if (button != null)
			{
				button.MouseClick -= value2;
			}
			field = value;
			button = field;
			if (button != null)
			{
				button.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmdAddNode")]
	internal virtual Button cmdAddNode
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = cmdAddNode_MouseClick;
			Button button = field;
			if (button != null)
			{
				button.MouseClick -= value2;
			}
			field = value;
			button = field;
			if (button != null)
			{
				button.MouseClick += value2;
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
			EventHandler value2 = cmdDown_Click;
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

	[field: AccessedThroughProperty("cmdRight")]
	internal virtual Button cmdRight
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdRight_Click;
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

	[field: AccessedThroughProperty("cmdLayerBackward")]
	internal virtual Button cmdLayerBackward
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdLayerBackward_Click;
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

	[field: AccessedThroughProperty("cmdUp")]
	internal virtual Button cmdUp
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdUp_Click;
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

	[field: AccessedThroughProperty("cmdLeft")]
	internal virtual Button cmdLeft
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdLeft_Click;
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

	[field: AccessedThroughProperty("GroupBox4")]
	internal virtual GroupBox GroupBox4
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label8")]
	internal virtual Label Label8
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtBuildingLineWidth")]
	internal virtual NumericUpDown txtBuildingLineWidth
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = txtBuildingLineWidth_ValueChanged;
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

	[field: AccessedThroughProperty("pbColor")]
	internal virtual PictureBox pbColor
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = pbColor1_Click;
			PictureBox pictureBox = field;
			if (pictureBox != null)
			{
				pictureBox.Click -= value2;
			}
			field = value;
			pictureBox = field;
			if (pictureBox != null)
			{
				pictureBox.Click += value2;
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

	[field: AccessedThroughProperty("txtBuildingTextSize")]
	internal virtual NumericUpDown txtBuildingTextSize
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = txtBuildingTextSize_ValueChanged;
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

	[field: AccessedThroughProperty("cmdSquareBLD")]
	internal virtual Button cmdSquareBLD
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdSquareBLD_Click;
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

	[field: AccessedThroughProperty("cmdAutoFundation")]
	internal virtual Button cmdAutoFundation
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdAutoFundation_Click;
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

	[field: AccessedThroughProperty("ContFundationMethod")]
	internal virtual ContextMenuStrip ContFundationMethod
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdMinHeight")]
	internal virtual ToolStripMenuItem cmdMinHeight
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdMinHeight_Click;
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

	[field: AccessedThroughProperty("cmdMaxHeight")]
	internal virtual ToolStripMenuItem cmdMaxHeight
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdMaxHeight_Click;
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

	[field: AccessedThroughProperty("cmdAverageHeight")]
	internal virtual ToolStripMenuItem cmdAverageHeight
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdAverageHeight_Click;
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

	[field: AccessedThroughProperty("cmdAutoFundationOneBLD")]
	internal virtual Button cmdAutoFundationOneBLD
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdAutoFundationOneBLD_Click;
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

	[field: AccessedThroughProperty("ContFundationMethodOneBLD")]
	internal virtual ContextMenuStrip ContFundationMethodOneBLD
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdMinHeightOneBLD")]
	internal virtual ToolStripMenuItem cmdMinHeightOneBLD
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdMinHeightOneBLD_Click;
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

	[field: AccessedThroughProperty("cmdMaxHeightOneBLD")]
	internal virtual ToolStripMenuItem cmdMaxHeightOneBLD
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdMaxHeightOneBLD_Click;
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

	[field: AccessedThroughProperty("cmdAverageHeightOneBLD")]
	internal virtual ToolStripMenuItem cmdAverageHeightOneBLD
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdAverageHeightOneBLD_Click;
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

	[field: AccessedThroughProperty("gbCustomAtt")]
	internal virtual GroupBox gbCustomAtt
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label13")]
	internal virtual Label Label13
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtBuildFreqBorder")]
	internal virtual TextBox txtBuildFreqBorder
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			CancelEventHandler value2 = txtBuildFreqBorder_Validating;
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

	[field: AccessedThroughProperty("Label14")]
	internal virtual Label Label14
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label11")]
	internal virtual Label Label11
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtBuildAttenSubband2")]
	internal virtual TextBox txtBuildAttenSubband2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			CancelEventHandler value2 = txtBuildAttenSubband2_Validating;
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

	[field: AccessedThroughProperty("Label12")]
	internal virtual Label Label12
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label9")]
	internal virtual Label Label9
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtBuildAttenSubband1")]
	internal virtual TextBox txtBuildAttenSubband1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			CancelEventHandler value2 = txtBuildAttenSubband1_Validating;
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

	[field: AccessedThroughProperty("Label10")]
	internal virtual Label Label10
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("rbBuildingShielding3")]
	internal virtual RadioButton rbBuildingShielding3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = rbBuildingShielding1_CheckedChanged;
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

	[field: AccessedThroughProperty("rbBuildingShielding2")]
	internal virtual RadioButton rbBuildingShielding2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = rbBuildingShielding1_CheckedChanged;
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

	[field: AccessedThroughProperty("rbBuildingShielding1")]
	internal virtual RadioButton rbBuildingShielding1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = rbBuildingShielding1_CheckedChanged;
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

	[field: AccessedThroughProperty("chkUseAttenuation")]
	internal virtual CheckBox chkUseAttenuation
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkUseAttenuation_MouseClick;
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

	[field: AccessedThroughProperty("cmdImportBuildingPolygons")]
	internal virtual Button cmdImportBuildingPolygons
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdImportBuildingPolygons_Click;
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

	public UC_Buildings()
	{
		Load += UC_Buildings_Load;
		FlagOK = false;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EMFPlanner.UC_Buildings));
		this.GroupBox1 = new System.Windows.Forms.GroupBox();
		this.cmdImportBuildingPolygons = new System.Windows.Forms.Button();
		this.cmdAutoFundation = new System.Windows.Forms.Button();
		this.cmdSquareBLD = new System.Windows.Forms.Button();
		this.GroupBox3 = new System.Windows.Forms.GroupBox();
		this.gbCustomAtt = new System.Windows.Forms.GroupBox();
		this.Label13 = new System.Windows.Forms.Label();
		this.txtBuildFreqBorder = new System.Windows.Forms.TextBox();
		this.Label14 = new System.Windows.Forms.Label();
		this.Label11 = new System.Windows.Forms.Label();
		this.txtBuildAttenSubband2 = new System.Windows.Forms.TextBox();
		this.Label12 = new System.Windows.Forms.Label();
		this.Label9 = new System.Windows.Forms.Label();
		this.txtBuildAttenSubband1 = new System.Windows.Forms.TextBox();
		this.Label10 = new System.Windows.Forms.Label();
		this.rbBuildingShielding3 = new System.Windows.Forms.RadioButton();
		this.rbBuildingShielding2 = new System.Windows.Forms.RadioButton();
		this.rbBuildingShielding1 = new System.Windows.Forms.RadioButton();
		this.cmdLayerBackward = new System.Windows.Forms.Button();
		this.cmdUp = new System.Windows.Forms.Button();
		this.cmdLeft = new System.Windows.Forms.Button();
		this.cmdDown = new System.Windows.Forms.Button();
		this.cmdRight = new System.Windows.Forms.Button();
		this.cmdDeleteNode = new System.Windows.Forms.Button();
		this.cmdAddNode = new System.Windows.Forms.Button();
		this.GroupBox4 = new System.Windows.Forms.GroupBox();
		this.Label8 = new System.Windows.Forms.Label();
		this.txtBuildingLineWidth = new System.Windows.Forms.NumericUpDown();
		this.pbColor = new System.Windows.Forms.PictureBox();
		this.Label1 = new System.Windows.Forms.Label();
		this.Label6 = new System.Windows.Forms.Label();
		this.txtBuildingTextSize = new System.Windows.Forms.NumericUpDown();
		this.cmdRedo = new System.Windows.Forms.Button();
		this.cmdUndo = new System.Windows.Forms.Button();
		this.cmdAddCopyBuilding = new System.Windows.Forms.Button();
		this.cmdDelOneBuilding = new System.Windows.Forms.Button();
		this.gbBuildingParameters = new System.Windows.Forms.GroupBox();
		this.chkUseAttenuation = new System.Windows.Forms.CheckBox();
		this.cmdAutoFundationOneBLD = new System.Windows.Forms.Button();
		this.chkShow = new System.Windows.Forms.CheckBox();
		this.Label7 = new System.Windows.Forms.Label();
		this.Label4 = new System.Windows.Forms.Label();
		this.Label5 = new System.Windows.Forms.Label();
		this.txtText = new System.Windows.Forms.TextBox();
		this.txtFundation = new System.Windows.Forms.TextBox();
		this.Label3 = new System.Windows.Forms.Label();
		this.Label2 = new System.Windows.Forms.Label();
		this.txtHeight = new System.Windows.Forms.TextBox();
		this.cmdDelAllBuildings = new System.Windows.Forms.Button();
		this.cmdSelect = new System.Windows.Forms.Button();
		this.cmdGetBldFromCloud = new System.Windows.Forms.Button();
		this.cmdAddBuilding = new System.Windows.Forms.Button();
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.CD1 = new System.Windows.Forms.ColorDialog();
		this.ContFundationMethod = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.cmdMinHeight = new System.Windows.Forms.ToolStripMenuItem();
		this.cmdMaxHeight = new System.Windows.Forms.ToolStripMenuItem();
		this.cmdAverageHeight = new System.Windows.Forms.ToolStripMenuItem();
		this.ContFundationMethodOneBLD = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.cmdMinHeightOneBLD = new System.Windows.Forms.ToolStripMenuItem();
		this.cmdMaxHeightOneBLD = new System.Windows.Forms.ToolStripMenuItem();
		this.cmdAverageHeightOneBLD = new System.Windows.Forms.ToolStripMenuItem();
		this.GroupBox1.SuspendLayout();
		this.GroupBox3.SuspendLayout();
		this.gbCustomAtt.SuspendLayout();
		this.GroupBox4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtBuildingLineWidth).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pbColor).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtBuildingTextSize).BeginInit();
		this.gbBuildingParameters.SuspendLayout();
		this.ContFundationMethod.SuspendLayout();
		this.ContFundationMethodOneBLD.SuspendLayout();
		base.SuspendLayout();
		this.GroupBox1.Controls.Add(this.cmdImportBuildingPolygons);
		this.GroupBox1.Controls.Add(this.cmdAutoFundation);
		this.GroupBox1.Controls.Add(this.cmdSquareBLD);
		this.GroupBox1.Controls.Add(this.GroupBox3);
		this.GroupBox1.Controls.Add(this.cmdLayerBackward);
		this.GroupBox1.Controls.Add(this.cmdUp);
		this.GroupBox1.Controls.Add(this.cmdLeft);
		this.GroupBox1.Controls.Add(this.cmdDown);
		this.GroupBox1.Controls.Add(this.cmdRight);
		this.GroupBox1.Controls.Add(this.cmdDeleteNode);
		this.GroupBox1.Controls.Add(this.cmdAddNode);
		this.GroupBox1.Controls.Add(this.GroupBox4);
		this.GroupBox1.Controls.Add(this.cmdRedo);
		this.GroupBox1.Controls.Add(this.cmdUndo);
		this.GroupBox1.Controls.Add(this.cmdAddCopyBuilding);
		this.GroupBox1.Controls.Add(this.cmdDelOneBuilding);
		this.GroupBox1.Controls.Add(this.gbBuildingParameters);
		this.GroupBox1.Controls.Add(this.cmdDelAllBuildings);
		this.GroupBox1.Controls.Add(this.cmdSelect);
		this.GroupBox1.Controls.Add(this.cmdGetBldFromCloud);
		this.GroupBox1.Controls.Add(this.cmdAddBuilding);
		this.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GroupBox1.Location = new System.Drawing.Point(0, 0);
		this.GroupBox1.Name = "GroupBox1";
		this.GroupBox1.Size = new System.Drawing.Size(300, 778);
		this.GroupBox1.TabIndex = 1;
		this.GroupBox1.TabStop = false;
		this.GroupBox1.Text = "Buildings";
		this.cmdImportBuildingPolygons.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdImportBuildingPolygons.Image = (System.Drawing.Image)resources.GetObject("cmdImportBuildingPolygons.Image");
		this.cmdImportBuildingPolygons.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdImportBuildingPolygons.Location = new System.Drawing.Point(9, 57);
		this.cmdImportBuildingPolygons.Name = "cmdImportBuildingPolygons";
		this.cmdImportBuildingPolygons.Size = new System.Drawing.Size(26, 26);
		this.cmdImportBuildingPolygons.TabIndex = 170;
		this.cmdImportBuildingPolygons.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdImportBuildingPolygons, "Import building polygons from KML file");
		this.cmdImportBuildingPolygons.UseVisualStyleBackColor = true;
		this.cmdAutoFundation.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdAutoFundation.Image = (System.Drawing.Image)resources.GetObject("cmdAutoFundation.Image");
		this.cmdAutoFundation.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdAutoFundation.Location = new System.Drawing.Point(36, 57);
		this.cmdAutoFundation.Name = "cmdAutoFundation";
		this.cmdAutoFundation.Size = new System.Drawing.Size(26, 26);
		this.cmdAutoFundation.TabIndex = 169;
		this.cmdAutoFundation.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdAutoFundation, "Fill in all building base elevations based on the terrain data");
		this.cmdAutoFundation.UseVisualStyleBackColor = true;
		this.cmdSquareBLD.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdSquareBLD.Image = (System.Drawing.Image)resources.GetObject("cmdSquareBLD.Image");
		this.cmdSquareBLD.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdSquareBLD.Location = new System.Drawing.Point(63, 57);
		this.cmdSquareBLD.Name = "cmdSquareBLD";
		this.cmdSquareBLD.Size = new System.Drawing.Size(26, 26);
		this.cmdSquareBLD.TabIndex = 168;
		this.cmdSquareBLD.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdSquareBLD, "Square building corners");
		this.cmdSquareBLD.UseVisualStyleBackColor = true;
		this.GroupBox3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GroupBox3.Controls.Add(this.gbCustomAtt);
		this.GroupBox3.Controls.Add(this.rbBuildingShielding3);
		this.GroupBox3.Controls.Add(this.rbBuildingShielding2);
		this.GroupBox3.Controls.Add(this.rbBuildingShielding1);
		this.GroupBox3.Location = new System.Drawing.Point(3, 442);
		this.GroupBox3.Name = "GroupBox3";
		this.GroupBox3.Size = new System.Drawing.Size(294, 207);
		this.GroupBox3.TabIndex = 167;
		this.GroupBox3.TabStop = false;
		this.GroupBox3.Text = "Calculation parameters";
		this.gbCustomAtt.Controls.Add(this.Label13);
		this.gbCustomAtt.Controls.Add(this.txtBuildFreqBorder);
		this.gbCustomAtt.Controls.Add(this.Label14);
		this.gbCustomAtt.Controls.Add(this.Label11);
		this.gbCustomAtt.Controls.Add(this.txtBuildAttenSubband2);
		this.gbCustomAtt.Controls.Add(this.Label12);
		this.gbCustomAtt.Controls.Add(this.Label9);
		this.gbCustomAtt.Controls.Add(this.txtBuildAttenSubband1);
		this.gbCustomAtt.Controls.Add(this.Label10);
		this.gbCustomAtt.Location = new System.Drawing.Point(3, 94);
		this.gbCustomAtt.Name = "gbCustomAtt";
		this.gbCustomAtt.Size = new System.Drawing.Size(288, 106);
		this.gbCustomAtt.TabIndex = 177;
		this.gbCustomAtt.TabStop = false;
		this.gbCustomAtt.Text = "Custom attenuation inside buildings";
		this.Label13.AutoSize = true;
		this.Label13.Location = new System.Drawing.Point(216, 76);
		this.Label13.Name = "Label13";
		this.Label13.Size = new System.Drawing.Size(29, 13);
		this.Label13.TabIndex = 185;
		this.Label13.Text = "MHz";
		this.txtBuildFreqBorder.Location = new System.Drawing.Point(172, 73);
		this.txtBuildFreqBorder.Name = "txtBuildFreqBorder";
		this.txtBuildFreqBorder.Size = new System.Drawing.Size(38, 20);
		this.txtBuildFreqBorder.TabIndex = 183;
		this.Label14.AutoSize = true;
		this.Label14.Location = new System.Drawing.Point(21, 76);
		this.Label14.Name = "Label14";
		this.Label14.Size = new System.Drawing.Size(133, 13);
		this.Label14.TabIndex = 184;
		this.Label14.Text = "Subband border frequency";
		this.Label11.AutoSize = true;
		this.Label11.Location = new System.Drawing.Point(216, 50);
		this.Label11.Name = "Label11";
		this.Label11.Size = new System.Drawing.Size(20, 13);
		this.Label11.TabIndex = 182;
		this.Label11.Text = "dB";
		this.txtBuildAttenSubband2.Location = new System.Drawing.Point(172, 47);
		this.txtBuildAttenSubband2.Name = "txtBuildAttenSubband2";
		this.txtBuildAttenSubband2.Size = new System.Drawing.Size(38, 20);
		this.txtBuildAttenSubband2.TabIndex = 180;
		this.Label12.AutoSize = true;
		this.Label12.Location = new System.Drawing.Point(23, 50);
		this.Label12.Name = "Label12";
		this.Label12.Size = new System.Drawing.Size(129, 13);
		this.Label12.TabIndex = 181;
		this.Label12.Text = "Attenuation for subband 2";
		this.Label9.AutoSize = true;
		this.Label9.Location = new System.Drawing.Point(216, 25);
		this.Label9.Name = "Label9";
		this.Label9.Size = new System.Drawing.Size(20, 13);
		this.Label9.TabIndex = 179;
		this.Label9.Text = "dB";
		this.txtBuildAttenSubband1.Location = new System.Drawing.Point(172, 22);
		this.txtBuildAttenSubband1.Name = "txtBuildAttenSubband1";
		this.txtBuildAttenSubband1.Size = new System.Drawing.Size(38, 20);
		this.txtBuildAttenSubband1.TabIndex = 177;
		this.Label10.AutoSize = true;
		this.Label10.Location = new System.Drawing.Point(23, 25);
		this.Label10.Name = "Label10";
		this.Label10.Size = new System.Drawing.Size(129, 13);
		this.Label10.TabIndex = 178;
		this.Label10.Text = "Attenuation for subband 1";
		this.rbBuildingShielding3.AutoSize = true;
		this.rbBuildingShielding3.Location = new System.Drawing.Point(9, 67);
		this.rbBuildingShielding3.Name = "rbBuildingShielding3";
		this.rbBuildingShielding3.Size = new System.Drawing.Size(279, 17);
		this.rbBuildingShielding3.TabIndex = 176;
		this.rbBuildingShielding3.TabStop = true;
		this.rbBuildingShielding3.Text = "Take into account custom attenuation inside buildings";
		this.rbBuildingShielding3.UseVisualStyleBackColor = true;
		this.rbBuildingShielding2.AutoSize = true;
		this.rbBuildingShielding2.Location = new System.Drawing.Point(9, 44);
		this.rbBuildingShielding2.Name = "rbBuildingShielding2";
		this.rbBuildingShielding2.Size = new System.Drawing.Size(196, 17);
		this.rbBuildingShielding2.TabIndex = 175;
		this.rbBuildingShielding2.TabStop = true;
		this.rbBuildingShielding2.Text = "Complete EMF shielding by buildings";
		this.rbBuildingShielding2.UseVisualStyleBackColor = true;
		this.rbBuildingShielding1.AutoSize = true;
		this.rbBuildingShielding1.Location = new System.Drawing.Point(9, 21);
		this.rbBuildingShielding1.Name = "rbBuildingShielding1";
		this.rbBuildingShielding1.Size = new System.Drawing.Size(204, 17);
		this.rbBuildingShielding1.TabIndex = 174;
		this.rbBuildingShielding1.TabStop = true;
		this.rbBuildingShielding1.Text = "Do not use buildings in the calculation";
		this.rbBuildingShielding1.UseVisualStyleBackColor = true;
		this.cmdLayerBackward.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdLayerBackward.Image = (System.Drawing.Image)resources.GetObject("cmdLayerBackward.Image");
		this.cmdLayerBackward.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdLayerBackward.Location = new System.Drawing.Point(90, 57);
		this.cmdLayerBackward.Name = "cmdLayerBackward";
		this.cmdLayerBackward.Size = new System.Drawing.Size(26, 26);
		this.cmdLayerBackward.TabIndex = 167;
		this.cmdLayerBackward.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdLayerBackward, "Move the selected building to the back");
		this.cmdLayerBackward.UseVisualStyleBackColor = true;
		this.cmdUp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdUp.Image = (System.Drawing.Image)resources.GetObject("cmdUp.Image");
		this.cmdUp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdUp.Location = new System.Drawing.Point(171, 57);
		this.cmdUp.Name = "cmdUp";
		this.cmdUp.Size = new System.Drawing.Size(26, 26);
		this.cmdUp.TabIndex = 166;
		this.cmdUp.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdUp, "Move all buildings by one pixel");
		this.cmdUp.UseVisualStyleBackColor = true;
		this.cmdLeft.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdLeft.Image = (System.Drawing.Image)resources.GetObject("cmdLeft.Image");
		this.cmdLeft.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdLeft.Location = new System.Drawing.Point(117, 57);
		this.cmdLeft.Name = "cmdLeft";
		this.cmdLeft.Size = new System.Drawing.Size(26, 26);
		this.cmdLeft.TabIndex = 165;
		this.cmdLeft.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdLeft, "Move all buildings by one pixel");
		this.cmdLeft.UseVisualStyleBackColor = true;
		this.cmdDown.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdDown.Image = (System.Drawing.Image)resources.GetObject("cmdDown.Image");
		this.cmdDown.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdDown.Location = new System.Drawing.Point(198, 57);
		this.cmdDown.Name = "cmdDown";
		this.cmdDown.Size = new System.Drawing.Size(26, 26);
		this.cmdDown.TabIndex = 164;
		this.cmdDown.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdDown, "Move all buildings by one pixel");
		this.cmdDown.UseVisualStyleBackColor = true;
		this.cmdRight.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdRight.Image = (System.Drawing.Image)resources.GetObject("cmdRight.Image");
		this.cmdRight.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdRight.Location = new System.Drawing.Point(144, 57);
		this.cmdRight.Name = "cmdRight";
		this.cmdRight.Size = new System.Drawing.Size(26, 26);
		this.cmdRight.TabIndex = 163;
		this.cmdRight.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdRight, "Move all buildings by one pixel");
		this.cmdRight.UseVisualStyleBackColor = true;
		this.cmdDeleteNode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdDeleteNode.Image = (System.Drawing.Image)resources.GetObject("cmdDeleteNode.Image");
		this.cmdDeleteNode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdDeleteNode.Location = new System.Drawing.Point(198, 25);
		this.cmdDeleteNode.Name = "cmdDeleteNode";
		this.cmdDeleteNode.Size = new System.Drawing.Size(26, 26);
		this.cmdDeleteNode.TabIndex = 162;
		this.cmdDeleteNode.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdDeleteNode, "Delete node");
		this.cmdDeleteNode.UseVisualStyleBackColor = true;
		this.cmdAddNode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdAddNode.Image = (System.Drawing.Image)resources.GetObject("cmdAddNode.Image");
		this.cmdAddNode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdAddNode.Location = new System.Drawing.Point(171, 25);
		this.cmdAddNode.Name = "cmdAddNode";
		this.cmdAddNode.Size = new System.Drawing.Size(26, 26);
		this.cmdAddNode.TabIndex = 160;
		this.cmdAddNode.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdAddNode, "Add Node");
		this.cmdAddNode.UseVisualStyleBackColor = true;
		this.GroupBox4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GroupBox4.Controls.Add(this.Label8);
		this.GroupBox4.Controls.Add(this.txtBuildingLineWidth);
		this.GroupBox4.Controls.Add(this.pbColor);
		this.GroupBox4.Controls.Add(this.Label1);
		this.GroupBox4.Controls.Add(this.Label6);
		this.GroupBox4.Controls.Add(this.txtBuildingTextSize);
		this.GroupBox4.Location = new System.Drawing.Point(3, 655);
		this.GroupBox4.Name = "GroupBox4";
		this.GroupBox4.Size = new System.Drawing.Size(294, 117);
		this.GroupBox4.TabIndex = 159;
		this.GroupBox4.TabStop = false;
		this.GroupBox4.Text = "Building display options";
		this.Label8.AutoSize = true;
		this.Label8.Location = new System.Drawing.Point(82, 86);
		this.Label8.Name = "Label8";
		this.Label8.Size = new System.Drawing.Size(31, 13);
		this.Label8.TabIndex = 166;
		this.Label8.Text = "Color";
		this.txtBuildingLineWidth.Location = new System.Drawing.Point(125, 25);
		this.txtBuildingLineWidth.Maximum = new decimal(new int[4] { 5, 0, 0, 0 });
		this.txtBuildingLineWidth.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.txtBuildingLineWidth.Name = "txtBuildingLineWidth";
		this.txtBuildingLineWidth.Size = new System.Drawing.Size(45, 20);
		this.txtBuildingLineWidth.TabIndex = 3;
		this.txtBuildingLineWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtBuildingLineWidth.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		this.pbColor.BackColor = System.Drawing.Color.Black;
		this.pbColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.pbColor.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.pbColor.Location = new System.Drawing.Point(125, 83);
		this.pbColor.Name = "pbColor";
		this.pbColor.Size = new System.Drawing.Size(30, 20);
		this.pbColor.TabIndex = 165;
		this.pbColor.TabStop = false;
		this.Label1.AutoSize = true;
		this.Label1.Location = new System.Drawing.Point(44, 27);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(75, 13);
		this.Label1.TabIndex = 146;
		this.Label1.Text = "Line thickness";
		this.Label6.AutoSize = true;
		this.Label6.Location = new System.Drawing.Point(64, 56);
		this.Label6.Name = "Label6";
		this.Label6.Size = new System.Drawing.Size(49, 13);
		this.Label6.TabIndex = 164;
		this.Label6.Text = "Font size";
		this.txtBuildingTextSize.Location = new System.Drawing.Point(125, 54);
		this.txtBuildingTextSize.Maximum = new decimal(new int[4] { 18, 0, 0, 0 });
		this.txtBuildingTextSize.Minimum = new decimal(new int[4] { 4, 0, 0, 0 });
		this.txtBuildingTextSize.Name = "txtBuildingTextSize";
		this.txtBuildingTextSize.Size = new System.Drawing.Size(45, 20);
		this.txtBuildingTextSize.TabIndex = 163;
		this.txtBuildingTextSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtBuildingTextSize.Value = new decimal(new int[4] { 8, 0, 0, 0 });
		this.cmdRedo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdRedo.Image = (System.Drawing.Image)resources.GetObject("cmdRedo.Image");
		this.cmdRedo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdRedo.Location = new System.Drawing.Point(252, 25);
		this.cmdRedo.Name = "cmdRedo";
		this.cmdRedo.Size = new System.Drawing.Size(26, 26);
		this.cmdRedo.TabIndex = 158;
		this.cmdRedo.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdRedo, "Redo");
		this.cmdRedo.UseVisualStyleBackColor = true;
		this.cmdUndo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdUndo.Image = (System.Drawing.Image)resources.GetObject("cmdUndo.Image");
		this.cmdUndo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdUndo.Location = new System.Drawing.Point(225, 25);
		this.cmdUndo.Name = "cmdUndo";
		this.cmdUndo.Size = new System.Drawing.Size(26, 26);
		this.cmdUndo.TabIndex = 157;
		this.cmdUndo.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdUndo, "Undo");
		this.cmdUndo.UseVisualStyleBackColor = true;
		this.cmdAddCopyBuilding.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdAddCopyBuilding.Image = (System.Drawing.Image)resources.GetObject("cmdAddCopyBuilding.Image");
		this.cmdAddCopyBuilding.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdAddCopyBuilding.Location = new System.Drawing.Point(90, 25);
		this.cmdAddCopyBuilding.Name = "cmdAddCopyBuilding";
		this.cmdAddCopyBuilding.Size = new System.Drawing.Size(26, 26);
		this.cmdAddCopyBuilding.TabIndex = 156;
		this.cmdAddCopyBuilding.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdAddCopyBuilding, "Create a copy of the selected building");
		this.cmdAddCopyBuilding.UseVisualStyleBackColor = true;
		this.cmdDelOneBuilding.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdDelOneBuilding.Image = (System.Drawing.Image)resources.GetObject("cmdDelOneBuilding.Image");
		this.cmdDelOneBuilding.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdDelOneBuilding.Location = new System.Drawing.Point(117, 25);
		this.cmdDelOneBuilding.Name = "cmdDelOneBuilding";
		this.cmdDelOneBuilding.Size = new System.Drawing.Size(26, 26);
		this.cmdDelOneBuilding.TabIndex = 155;
		this.cmdDelOneBuilding.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdDelOneBuilding, "Delete selected building");
		this.cmdDelOneBuilding.UseVisualStyleBackColor = true;
		this.gbBuildingParameters.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gbBuildingParameters.Controls.Add(this.chkUseAttenuation);
		this.gbBuildingParameters.Controls.Add(this.cmdAutoFundationOneBLD);
		this.gbBuildingParameters.Controls.Add(this.chkShow);
		this.gbBuildingParameters.Controls.Add(this.Label7);
		this.gbBuildingParameters.Controls.Add(this.Label4);
		this.gbBuildingParameters.Controls.Add(this.Label5);
		this.gbBuildingParameters.Controls.Add(this.txtText);
		this.gbBuildingParameters.Controls.Add(this.txtFundation);
		this.gbBuildingParameters.Controls.Add(this.Label3);
		this.gbBuildingParameters.Controls.Add(this.Label2);
		this.gbBuildingParameters.Controls.Add(this.txtHeight);
		this.gbBuildingParameters.Location = new System.Drawing.Point(3, 94);
		this.gbBuildingParameters.Name = "gbBuildingParameters";
		this.gbBuildingParameters.Size = new System.Drawing.Size(294, 155);
		this.gbBuildingParameters.TabIndex = 154;
		this.gbBuildingParameters.TabStop = false;
		this.gbBuildingParameters.Text = "Building parameters";
		this.chkUseAttenuation.AutoSize = true;
		this.chkUseAttenuation.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.chkUseAttenuation.Location = new System.Drawing.Point(8, 104);
		this.chkUseAttenuation.Name = "chkUseAttenuation";
		this.chkUseAttenuation.Size = new System.Drawing.Size(158, 17);
		this.chkUseAttenuation.TabIndex = 171;
		this.chkUseAttenuation.TabStop = false;
		this.chkUseAttenuation.Text = "Use building for calculations";
		this.chkUseAttenuation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.chkUseAttenuation.UseVisualStyleBackColor = true;
		this.cmdAutoFundationOneBLD.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdAutoFundationOneBLD.Image = (System.Drawing.Image)resources.GetObject("cmdAutoFundationOneBLD.Image");
		this.cmdAutoFundationOneBLD.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdAutoFundationOneBLD.Location = new System.Drawing.Point(238, 70);
		this.cmdAutoFundationOneBLD.Name = "cmdAutoFundationOneBLD";
		this.cmdAutoFundationOneBLD.Size = new System.Drawing.Size(26, 26);
		this.cmdAutoFundationOneBLD.TabIndex = 170;
		this.cmdAutoFundationOneBLD.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdAutoFundationOneBLD, "Set the selected building base elevation based on the terrain data");
		this.cmdAutoFundationOneBLD.UseVisualStyleBackColor = true;
		this.chkShow.AutoSize = true;
		this.chkShow.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.chkShow.Location = new System.Drawing.Point(14, 127);
		this.chkShow.Name = "chkShow";
		this.chkShow.Size = new System.Drawing.Size(152, 17);
		this.chkShow.TabIndex = 161;
		this.chkShow.TabStop = false;
		this.chkShow.Text = "Show on the horizontal cut";
		this.chkShow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.chkShow.UseVisualStyleBackColor = true;
		this.Label7.AutoSize = true;
		this.Label7.Location = new System.Drawing.Point(117, 22);
		this.Label7.Name = "Label7";
		this.Label7.Size = new System.Drawing.Size(28, 13);
		this.Label7.TabIndex = 160;
		this.Label7.Text = "Text";
		this.Label4.AutoSize = true;
		this.Label4.Location = new System.Drawing.Point(201, 77);
		this.Label4.Name = "Label4";
		this.Label4.Size = new System.Drawing.Size(15, 13);
		this.Label4.TabIndex = 158;
		this.Label4.Text = "m";
		this.Label5.AutoSize = true;
		this.Label5.Location = new System.Drawing.Point(24, 74);
		this.Label5.Name = "Label5";
		this.Label5.Size = new System.Drawing.Size(121, 13);
		this.Label5.TabIndex = 157;
		this.Label5.Text = "Buildings base elevation";
		this.txtText.Location = new System.Drawing.Point(153, 22);
		this.txtText.Name = "txtText";
		this.txtText.Size = new System.Drawing.Size(133, 20);
		this.txtText.TabIndex = 159;
		this.txtFundation.Location = new System.Drawing.Point(153, 74);
		this.txtFundation.Name = "txtFundation";
		this.txtFundation.Size = new System.Drawing.Size(38, 20);
		this.txtFundation.TabIndex = 156;
		this.Label3.AutoSize = true;
		this.Label3.Location = new System.Drawing.Point(201, 51);
		this.Label3.Name = "Label3";
		this.Label3.Size = new System.Drawing.Size(15, 13);
		this.Label3.TabIndex = 155;
		this.Label3.Text = "m";
		this.Label2.AutoSize = true;
		this.Label2.Location = new System.Drawing.Point(69, 48);
		this.Label2.Name = "Label2";
		this.Label2.Size = new System.Drawing.Size(76, 13);
		this.Label2.TabIndex = 154;
		this.Label2.Text = "Building height";
		this.txtHeight.Location = new System.Drawing.Point(154, 48);
		this.txtHeight.Name = "txtHeight";
		this.txtHeight.Size = new System.Drawing.Size(38, 20);
		this.txtHeight.TabIndex = 153;
		this.cmdDelAllBuildings.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdDelAllBuildings.Image = (System.Drawing.Image)resources.GetObject("cmdDelAllBuildings.Image");
		this.cmdDelAllBuildings.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdDelAllBuildings.Location = new System.Drawing.Point(144, 25);
		this.cmdDelAllBuildings.Name = "cmdDelAllBuildings";
		this.cmdDelAllBuildings.Size = new System.Drawing.Size(26, 26);
		this.cmdDelAllBuildings.TabIndex = 152;
		this.cmdDelAllBuildings.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdDelAllBuildings, "Delete all buildings");
		this.cmdDelAllBuildings.UseVisualStyleBackColor = true;
		this.cmdSelect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdSelect.Image = (System.Drawing.Image)resources.GetObject("cmdSelect.Image");
		this.cmdSelect.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdSelect.Location = new System.Drawing.Point(9, 25);
		this.cmdSelect.Name = "cmdSelect";
		this.cmdSelect.Size = new System.Drawing.Size(26, 26);
		this.cmdSelect.TabIndex = 151;
		this.cmdSelect.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdSelect, "Select a building");
		this.cmdSelect.UseVisualStyleBackColor = true;
		this.cmdGetBldFromCloud.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdGetBldFromCloud.Image = (System.Drawing.Image)resources.GetObject("cmdGetBldFromCloud.Image");
		this.cmdGetBldFromCloud.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdGetBldFromCloud.Location = new System.Drawing.Point(36, 25);
		this.cmdGetBldFromCloud.Name = "cmdGetBldFromCloud";
		this.cmdGetBldFromCloud.Size = new System.Drawing.Size(26, 26);
		this.cmdGetBldFromCloud.TabIndex = 150;
		this.cmdGetBldFromCloud.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdGetBldFromCloud, "Load building polygons within the screen from the OpenStreetMap database");
		this.cmdGetBldFromCloud.UseVisualStyleBackColor = true;
		this.cmdAddBuilding.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdAddBuilding.Image = (System.Drawing.Image)resources.GetObject("cmdAddBuilding.Image");
		this.cmdAddBuilding.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdAddBuilding.Location = new System.Drawing.Point(63, 25);
		this.cmdAddBuilding.Name = "cmdAddBuilding";
		this.cmdAddBuilding.Size = new System.Drawing.Size(26, 26);
		this.cmdAddBuilding.TabIndex = 149;
		this.cmdAddBuilding.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdAddBuilding, "Add building");
		this.cmdAddBuilding.UseVisualStyleBackColor = true;
		this.ContFundationMethod.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.cmdMinHeight, this.cmdMaxHeight, this.cmdAverageHeight });
		this.ContFundationMethod.Name = "cmsDBW";
		this.ContFundationMethod.Size = new System.Drawing.Size(209, 70);
		this.cmdMinHeight.Name = "cmdMinHeight";
		this.cmdMinHeight.Size = new System.Drawing.Size(208, 22);
		this.cmdMinHeight.Text = "Minimum elevation";
		this.cmdMaxHeight.Name = "cmdMaxHeight";
		this.cmdMaxHeight.Size = new System.Drawing.Size(208, 22);
		this.cmdMaxHeight.Text = "Maximum relief elevation";
		this.cmdAverageHeight.Name = "cmdAverageHeight";
		this.cmdAverageHeight.Size = new System.Drawing.Size(208, 22);
		this.cmdAverageHeight.Text = "Average elevation";
		this.ContFundationMethodOneBLD.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.cmdMinHeightOneBLD, this.cmdMaxHeightOneBLD, this.cmdAverageHeightOneBLD });
		this.ContFundationMethodOneBLD.Name = "cmsDBW";
		this.ContFundationMethodOneBLD.Size = new System.Drawing.Size(209, 70);
		this.cmdMinHeightOneBLD.Name = "cmdMinHeightOneBLD";
		this.cmdMinHeightOneBLD.Size = new System.Drawing.Size(208, 22);
		this.cmdMinHeightOneBLD.Text = "Minimum elevation";
		this.cmdMaxHeightOneBLD.Name = "cmdMaxHeightOneBLD";
		this.cmdMaxHeightOneBLD.Size = new System.Drawing.Size(208, 22);
		this.cmdMaxHeightOneBLD.Text = "Maximum relief elevation";
		this.cmdAverageHeightOneBLD.Name = "cmdAverageHeightOneBLD";
		this.cmdAverageHeightOneBLD.Size = new System.Drawing.Size(208, 22);
		this.cmdAverageHeightOneBLD.Text = "Average elevation";
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.GroupBox1);
		this.MinimumSize = new System.Drawing.Size(300, 0);
		base.Name = "UC_Buildings";
		base.Size = new System.Drawing.Size(300, 778);
		this.GroupBox1.ResumeLayout(false);
		this.GroupBox3.ResumeLayout(false);
		this.GroupBox3.PerformLayout();
		this.gbCustomAtt.ResumeLayout(false);
		this.gbCustomAtt.PerformLayout();
		this.GroupBox4.ResumeLayout(false);
		this.GroupBox4.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtBuildingLineWidth).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pbColor).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtBuildingTextSize).EndInit();
		this.gbBuildingParameters.ResumeLayout(false);
		this.gbBuildingParameters.PerformLayout();
		this.ContFundationMethod.ResumeLayout(false);
		this.ContFundationMethodOneBLD.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void UC_Buildings_Load(object sender, EventArgs e)
	{
		FlagOK = false;
		Form1_Public.StartDrawingBuilding = false;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartAddingNodes = false;
		txtBuildingLineWidth.Value = new decimal(Public2.PRJ.SETT.BuildingLineWidth);
		txtBuildingTextSize.Value = new decimal(Public2.PRJ.SETT.BuildingTextSize);
		pbColor.BackColor = Public2.PRJ.SETT.BuildingColor;
		gbBuildingParameters.Visible = false;
		gbCustomAtt.Enabled = false;
		if (Public2.PRJ.SETT.UseBuildings == 0)
		{
			rbBuildingShielding1.Checked = true;
			rbBuildingShielding2.Checked = false;
			rbBuildingShielding3.Checked = false;
		}
		else if (Public2.PRJ.SETT.UseBuildings == 1)
		{
			rbBuildingShielding1.Checked = false;
			rbBuildingShielding2.Checked = true;
			rbBuildingShielding3.Checked = false;
		}
		else if (Public2.PRJ.SETT.UseBuildings == 2)
		{
			rbBuildingShielding1.Checked = false;
			rbBuildingShielding2.Checked = false;
			rbBuildingShielding3.Checked = true;
			gbCustomAtt.Enabled = true;
		}
		txtBuildAttenSubband1.Text = Conversions.ToString(Public2.PRJ.SETT.BuildAttenSubband1);
		txtBuildAttenSubband2.Text = Conversions.ToString(Public2.PRJ.SETT.BuildAttenSubband2);
		txtBuildFreqBorder.Text = Conversions.ToString(Public2.PRJ.SETT.BuildFreqBorder);
		FlagOK = true;
		MyProject.Forms.Form1.DrawPIC();
	}

	private void cmdSelect_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		cmdAddBuilding.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingBuilding = false;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartAddingNodes = false;
		MyProject.Forms.Form1.IndexBLD = -1;
		MyProject.Forms.Form1.IndexPNT = -1;
		Form1_Public.NewBLD = default;
		MyProject.Forms.Form1.PB.Refresh();
		gbBuildingParameters.Visible = false;
	}

	private void cmdAddBuilding_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		cmdDeleteNode.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartAddingNodes = false;
		if (Form1_Public.StartDrawingBuilding)
		{
			cmdAddBuilding.BackColor = SystemColors.Control;
			Form1_Public.StartDrawingBuilding = false;
		}
		else
		{
			cmdAddBuilding.BackColor = SystemColors.ControlDark;
			Form1_Public.StartDrawingBuilding = true;
		}
		MyProject.Forms.Form1.IndexBLD = -1;
		MyProject.Forms.Form1.IndexPNT = -1;
		Form1_Public.NewBLD = default;
		MyProject.Forms.Form1.PB.Refresh();
		gbBuildingParameters.Visible = false;
	}

	private void cmdGetBldFromCloud_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		cmdAddBuilding.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingBuilding = false;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartAddingNodes = false;
		if (!Information.IsNothing(Public2.PRJ.BLD) && Interaction.MsgBox(Localization.LocMSG(22), MsgBoxStyle.YesNo) != MsgBoxResult.Yes)
		{
			return;
		}
		MyProject.Forms.frmAutoBuildingHeight.Location = new Point(Control.MousePosition.X, Control.MousePosition.Y);
		MyProject.Forms.frmAutoBuildingHeight.ShowDialog();
		if (!Form1_Public.AutoBuildingHeightOK)
		{
			return;
		}
		Module1.BuildingBackUp();
		string[] BuildingGeometry = default;
		string[] BuildingLevels = default;
		string[] BuildingHeight = default;
		PHP_SQL.GetBuilding(PHP_SQL.SquareMercator2Geometry(Form1_Public.ScrCoord1.Xwm, Form1_Public.ScrCoord2.Xwm, Form1_Public.ScrCoord2.Ywm, Form1_Public.ScrCoord1.Ywm), ref BuildingGeometry, ref BuildingLevels, ref BuildingHeight);
		short num;
		checked
		{
			PHP_SQL.CoordMercator[][][] array = default;
			if (!Information.IsNothing(BuildingGeometry))
			{
				array = new PHP_SQL.CoordMercator[BuildingGeometry.GetUpperBound(0) + 1][][];
				int upperBound = BuildingGeometry.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					PHP_SQL.ConvertGeometryPolygon2Coord(BuildingGeometry[i], ref array[i]);
				}
			}
			num = 0;
			if (!Information.IsNothing(BuildingGeometry))
			{
				int upperBound2 = array.GetUpperBound(0);
				for (int j = 0; j <= upperBound2; j++)
				{
					bool flag = false;
					if (!Information.IsNothing(Public2.PRJ.BLD))
					{
						int upperBound3 = Public2.PRJ.BLD.GetUpperBound(0);
						for (int k = 0; k <= upperBound3; k++)
						{
							if ((array[j][0][0].X == Public2.PRJ.BLD[k].CoordWM[0].Xwm) & (array[j][0][0].Y == Public2.PRJ.BLD[k].CoordWM[0].Ywm))
							{
								flag = true;
								break;
							}
						}
					}
					if (flag)
					{
						continue;
					}
					num++;
					if (Information.IsNothing(Public2.PRJ.BLD))
					{
						ref Public2.StructBuilding[] bLD = ref Public2.PRJ.BLD;
						bLD = (Public2.StructBuilding[])Utils.CopyArray(bLD, new Public2.StructBuilding[1]);
					}
					else
					{
						ref Public2.StructBuilding[] bLD2 = ref Public2.PRJ.BLD;
						bLD2 = (Public2.StructBuilding[])Utils.CopyArray(bLD2, new Public2.StructBuilding[Public2.PRJ.BLD.GetUpperBound(0) + 1 + 1]);
					}
					ref Public2.CoordWebMerc[] coordWM = ref Public2.PRJ.BLD[Public2.PRJ.BLD.GetUpperBound(0)].CoordWM;
					coordWM = (Public2.CoordWebMerc[])Utils.CopyArray(coordWM, new Public2.CoordWebMerc[array[j][0].GetUpperBound(0) + 1]);
					if (Operators.CompareString(BuildingHeight[j], "", TextCompare: false) != 0)
					{
						Public2.PRJ.BLD[Public2.PRJ.BLD.GetUpperBound(0)].H = Conversions.ToSingle(BuildingHeight[j]);
						if (Operators.CompareString(BuildingLevels[j], "", TextCompare: false) != 0)
						{
							Public2.PRJ.BLD[Public2.PRJ.BLD.GetUpperBound(0)].Text = BuildingLevels[j] + Localization.LocMSG(29);
						}
					}
					else if (Operators.CompareString(BuildingLevels[j], "", TextCompare: false) != 0)
					{
						Public2.PRJ.BLD[Public2.PRJ.BLD.GetUpperBound(0)].Text = BuildingLevels[j] + Localization.LocMSG(29);
						Public2.PRJ.BLD[Public2.PRJ.BLD.GetUpperBound(0)].H = (float)(Conversions.ToDouble(BuildingLevels[j]) * (double)Form1_Public.HgtFloor);
					}
					else
					{
						Public2.PRJ.BLD[Public2.PRJ.BLD.GetUpperBound(0)].H = Form1_Public.HgtBuilding;
					}
					Public2.PRJ.BLD[Public2.PRJ.BLD.GetUpperBound(0)].Fundation = Form1_Public.FundationHeight;
					Public2.PRJ.BLD[Public2.PRJ.BLD.GetUpperBound(0)].Show = true;
					Public2.PRJ.BLD[Public2.PRJ.BLD.GetUpperBound(0)].UseAttenuation = true;
					int upperBound4 = array[j][0].GetUpperBound(0);
					for (int l = 0; l <= upperBound4; l++)
					{
						Public2.PRJ.BLD[Public2.PRJ.BLD.GetUpperBound(0)].CoordWM[l].Xwm = array[j][0][l].X;
						Public2.PRJ.BLD[Public2.PRJ.BLD.GetUpperBound(0)].CoordWM[l].Ywm = array[j][0][l].Y;
					}
				}
			}
			MyProject.Forms.Form1.IndexBLD = -1;
			MyProject.Forms.Form1.IndexPNT = -1;
			MyProject.Forms.Form1.PB.Refresh();
			gbBuildingParameters.Visible = false;
		}
		Interaction.MsgBox(Localization.LocMSG(23) + " " + Conversions.ToString((int)num) + " " + Localization.LocMSG(24));
	}

	private void cmdImportBuildingPolygons_Click(object sender, EventArgs e)
	{
		checked
		{
			try
			{
				using OpenFileDialog openFileDialog = new OpenFileDialog();
				openFileDialog.Filter = "KML files (*.kml)|*.kml";
				if (openFileDialog.ShowDialog() != DialogResult.OK)
				{
					return;
				}
				Module1.BuildingBackUp();
				int num = -1;
				int num2 = 0;
				if (Information.IsNothing(Public2.PRJ.BLD))
				{
					ref Public2.StructBuilding[] bLD = ref Public2.PRJ.BLD;
					bLD = (Public2.StructBuilding[])Utils.CopyArray(bLD, new Public2.StructBuilding[1]);
				}
				else
				{
					num = Public2.PRJ.BLD.GetUpperBound(0);
					ref Public2.StructBuilding[] bLD2 = ref Public2.PRJ.BLD;
					bLD2 = (Public2.StructBuilding[])Utils.CopyArray(bLD2, new Public2.StructBuilding[Public2.PRJ.BLD.GetUpperBound(0) + 1 + 1]);
				}
				XmlTextReader xmlTextReader = new XmlTextReader(openFileDialog.FileName);
				bool flag = default;
				bool flag3 = default;
				bool flag4 = default;
				bool flag2 = default;
				string value = default;
				while (xmlTextReader.Read())
				{
					switch (xmlTextReader.NodeType)
					{
					case XmlNodeType.Element:
						switch (xmlTextReader.Name.ToUpper())
						{
						case "PLACEMARK":
							flag = true;
							break;
						case "LINEARRING":
							flag3 = true;
							break;
						case "COORDINATES":
							flag4 = true;
							break;
						case "NAME":
							flag2 = true;
							break;
						}
						break;
					case XmlNodeType.EndElement:
						switch (xmlTextReader.Name.ToUpper())
						{
						case "PLACEMARK":
							flag = false;
							break;
						case "LINEARRING":
							flag3 = false;
							break;
						case "COORDINATES":
							flag4 = false;
							break;
						case "NAME":
							flag2 = false;
							break;
						}
						break;
					case XmlNodeType.Text:
						if (flag & flag2)
						{
							value = xmlTextReader.Value;
						}
						else if (flag & flag3 & flag4)
						{
							string expression = Strings.Trim(xmlTextReader.Value.Replace("\r\n", " ").Replace("\t", "").Replace("\n", ""));
							num++;
							num2++;
							ref Public2.StructBuilding[] bLD3 = ref Public2.PRJ.BLD;
							bLD3 = (Public2.StructBuilding[])Utils.CopyArray(bLD3, new Public2.StructBuilding[num + 1]);
							string[] array = Strings.Split(expression);
							Public2.PRJ.BLD[num].CoordWM = new Public2.CoordWebMerc[array.GetUpperBound(0) + 1];
							int upperBound = array.GetUpperBound(0);
							for (int i = 0; i <= upperBound; i++)
							{
								string[] array2 = Strings.Split(array[i], ",");
								PHP_SQL.Wgs2Mercator(Conversions.ToDouble(Module1.TchkToZpt(array2[1])), Conversions.ToDouble(Module1.TchkToZpt(array2[0])), ref Public2.PRJ.BLD[num].CoordWM[i].Ywm, ref Public2.PRJ.BLD[num].CoordWM[i].Xwm);
							}
							string[] array3 = Strings.Split(value, ";");
							if (array3.Length == 2)
							{
								Public2.PRJ.BLD[num].H = Conversions.ToSingle(Module1.TchkToZpt(array3[0]));
								Public2.PRJ.BLD[num].Text = Module1.TchkToZpt(array3[1]);
							}
							else
							{
								Public2.PRJ.BLD[num].H = 10f;
								Public2.PRJ.BLD[num].Text = "";
							}
							Public2.PRJ.BLD[num].Show = true;
							Public2.PRJ.BLD[num].Fundation = 0f;
							Public2.PRJ.BLD[num].UseAttenuation = true;
						}
						break;
					}
				}
				MyProject.Forms.Form1.IndexBLD = -1;
				MyProject.Forms.Form1.IndexPNT = -1;
				MyProject.Forms.Form1.PB.Refresh();
				gbBuildingParameters.Visible = false;
				Interaction.MsgBox("There are " + Conversions.ToString(num2) + " building polygons imported", MsgBoxStyle.Information);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Interaction.MsgBox(ex2.Message);
				ProjectData.ClearProjectError();
			}
		}
	}

	private void cmdDelAllBuildings_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Interaction.MsgBox(Localization.LocMSG(25), MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
		{
			cmdAddBuilding.BackColor = SystemColors.Control;
			cmdDeleteNode.BackColor = SystemColors.Control;
			cmdAddNode.BackColor = SystemColors.Control;
			Form1_Public.StartDrawingBuilding = false;
			Form1_Public.StartDeletingNodes = false;
			Form1_Public.StartAddingNodes = false;
			MyProject.Forms.Form1.IndexBLD = -1;
			MyProject.Forms.Form1.IndexPNT = -1;
			gbBuildingParameters.Visible = false;
			Module1.BuildingBackUp();
			Public2.PRJ.BLD = null;
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void cmdDelOneBuilding_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		checked
		{
			if (MyProject.Forms.Form1.IndexBLD < 0)
			{
				Interaction.MsgBox(Localization.LocMSG(26), MsgBoxStyle.Information);
			}
			else if (Interaction.MsgBox(Localization.LocMSG(27), MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
			{
				cmdAddBuilding.BackColor = SystemColors.Control;
				cmdDeleteNode.BackColor = SystemColors.Control;
				cmdAddNode.BackColor = SystemColors.Control;
				Form1_Public.StartDrawingBuilding = false;
				Form1_Public.StartDeletingNodes = false;
				Form1_Public.StartAddingNodes = false;
				short indexBLD = MyProject.Forms.Form1.IndexBLD;
				int num = Public2.PRJ.BLD.GetUpperBound(0) - 1;
				for (int i = indexBLD; i <= num; i++)
				{
					Public2.PRJ.BLD[i] = Module1.DeepClone(Public2.PRJ.BLD[i + 1]);
				}
				ref Public2.StructBuilding[] bLD = ref Public2.PRJ.BLD;
				bLD = (Public2.StructBuilding[])Utils.CopyArray(bLD, new Public2.StructBuilding[Public2.PRJ.BLD.GetUpperBound(0) - 1 + 1]);
				if (Public2.PRJ.BLD.Length == 0)
				{
					Public2.PRJ.BLD = null;
				}
				MyProject.Forms.Form1.IndexBLD = -1;
				MyProject.Forms.Form1.IndexPNT = -1;
				gbBuildingParameters.Visible = false;
				MyProject.Forms.Form1.PB.Refresh();
			}
		}
	}

	private void cmdAddCopyBuilding_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		cmdAddBuilding.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingBuilding = false;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartAddingNodes = false;
		if (MyProject.Forms.Form1.IndexBLD < 0)
		{
			Interaction.MsgBox(Localization.LocMSG(26), MsgBoxStyle.Information);
			return;
		}
		Module1.BuildingBackUp();
		ref Public2.StructBuilding[] bLD = ref Public2.PRJ.BLD;
		checked
		{
			bLD = (Public2.StructBuilding[])Utils.CopyArray(bLD, new Public2.StructBuilding[Public2.PRJ.BLD.GetUpperBound(0) + 1 + 1]);
			Public2.PRJ.BLD[Public2.PRJ.BLD.GetUpperBound(0)] = Module1.DeepClone(Public2.PRJ.BLD[MyProject.Forms.Form1.IndexBLD]);
			int upperBound = Public2.PRJ.BLD[Public2.PRJ.BLD.GetUpperBound(0)].CoordWM.GetUpperBound(0);
			for (int i = 0; i <= upperBound; i++)
			{
				Public2.PRJ.BLD[Public2.PRJ.BLD.GetUpperBound(0)].CoordWM[i].Xwm += 20.0;
				Public2.PRJ.BLD[Public2.PRJ.BLD.GetUpperBound(0)].CoordWM[i].Ywm -= 20.0;
			}
			MyProject.Forms.Form1.IndexBLD = (short)Public2.PRJ.BLD.GetUpperBound(0);
			MyProject.Forms.Form1.PB.Refresh();
			gbBuildingParameters.Visible = true;
			txtHeight.Text = Conversions.ToString(Public2.PRJ.BLD[MyProject.Forms.Form1.IndexBLD].H);
			txtText.Text = Public2.PRJ.BLD[MyProject.Forms.Form1.IndexBLD].Text;
			txtFundation.Text = Conversions.ToString(Public2.PRJ.BLD[MyProject.Forms.Form1.IndexBLD].Fundation);
			chkShow.Checked = Public2.PRJ.BLD[MyProject.Forms.Form1.IndexBLD].Show;
			chkUseAttenuation.Checked = Public2.PRJ.BLD[MyProject.Forms.Form1.IndexBLD].UseAttenuation;
		}
	}

	private void cmdDeleteNode_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		if (MyProject.Forms.Form1.IndexBLD == -1)
		{
			Interaction.MsgBox(Localization.LocMSG(26), MsgBoxStyle.Exclamation);
			return;
		}
		cmdAddBuilding.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingBuilding = false;
		Form1_Public.StartAddingNodes = false;
		if (Form1_Public.StartDeletingNodes)
		{
			cmdDeleteNode.BackColor = SystemColors.Control;
			Form1_Public.StartDeletingNodes = false;
		}
		else
		{
			cmdDeleteNode.BackColor = SystemColors.ControlDark;
			Form1_Public.StartDeletingNodes = true;
		}
		MyProject.Forms.Form1.PB.Refresh();
	}

	private void cmdAddNode_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		if (MyProject.Forms.Form1.IndexBLD == -1)
		{
			Interaction.MsgBox(Localization.LocMSG(26), MsgBoxStyle.Exclamation);
			return;
		}
		cmdAddBuilding.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingBuilding = false;
		Form1_Public.StartDeletingNodes = false;
		if (Form1_Public.StartAddingNodes)
		{
			cmdAddNode.BackColor = SystemColors.Control;
			Form1_Public.StartAddingNodes = false;
		}
		else
		{
			cmdAddNode.BackColor = SystemColors.ControlDark;
			Form1_Public.StartAddingNodes = true;
		}
		MyProject.Forms.Form1.PB.Refresh();
	}

	private void cmdLayerBackward_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (MyProject.Forms.Form1.IndexBLD == -1)
		{
			Interaction.MsgBox(Localization.LocMSG(26), MsgBoxStyle.Exclamation);
			return;
		}
		Public2.StructBuilding obj = Public2.PRJ.BLD[MyProject.Forms.Form1.IndexBLD];
		short indexBLD = MyProject.Forms.Form1.IndexBLD;
		checked
		{
			int num = Public2.PRJ.BLD.GetUpperBound(0) - 1;
			for (int i = indexBLD; i <= num; i++)
			{
				Public2.PRJ.BLD[i] = Module1.DeepClone(Public2.PRJ.BLD[i + 1]);
			}
			Public2.PRJ.BLD[Public2.PRJ.BLD.GetUpperBound(0)] = Module1.DeepClone(obj);
			MyProject.Forms.Form1.IndexBLD = -1;
			MyProject.Forms.Form1.IndexPNT = -1;
			gbBuildingParameters.Visible = false;
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void CmdSquareBLD_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (MyProject.Forms.Form1.IndexBLD < 0)
		{
			Interaction.MsgBox(Localization.LocMSG(26), MsgBoxStyle.Information);
			return;
		}
		cmdAddBuilding.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingBuilding = false;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartAddingNodes = false;
		Module1.BuildingBackUp();
		Module1.SquarePline(Public2.PRJ.BLD[MyProject.Forms.Form1.IndexBLD].CoordWM);
		MyProject.Forms.Form1.PB.Refresh();
	}

	private void cmdLeft_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		cmdAddBuilding.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingBuilding = false;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartAddingNodes = false;
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.BLD))
			{
				double num = 1.0 / CalcCoords.CalcMashtWMtoPixel();
				int upperBound = Public2.PRJ.BLD.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					int upperBound2 = Public2.PRJ.BLD[i].CoordWM.GetUpperBound(0);
					for (int j = 0; j <= upperBound2; j++)
					{
						Public2.PRJ.BLD[i].CoordWM[j].Xwm = Public2.PRJ.BLD[i].CoordWM[j].Xwm - num;
					}
				}
			}
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void cmdRight_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		cmdAddBuilding.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingBuilding = false;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartAddingNodes = false;
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.BLD))
			{
				double num = 1.0 / CalcCoords.CalcMashtWMtoPixel();
				int upperBound = Public2.PRJ.BLD.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					int upperBound2 = Public2.PRJ.BLD[i].CoordWM.GetUpperBound(0);
					for (int j = 0; j <= upperBound2; j++)
					{
						Public2.PRJ.BLD[i].CoordWM[j].Xwm = Public2.PRJ.BLD[i].CoordWM[j].Xwm + num;
					}
				}
			}
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void cmdUp_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		cmdAddBuilding.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingBuilding = false;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartAddingNodes = false;
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.BLD))
			{
				double num = 1.0 / CalcCoords.CalcMashtWMtoPixel();
				int upperBound = Public2.PRJ.BLD.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					int upperBound2 = Public2.PRJ.BLD[i].CoordWM.GetUpperBound(0);
					for (int j = 0; j <= upperBound2; j++)
					{
						Public2.PRJ.BLD[i].CoordWM[j].Ywm = Public2.PRJ.BLD[i].CoordWM[j].Ywm + num;
					}
				}
			}
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void cmdDown_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		cmdAddBuilding.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingBuilding = false;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartAddingNodes = false;
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.BLD))
			{
				double num = 1.0 / CalcCoords.CalcMashtWMtoPixel();
				int upperBound = Public2.PRJ.BLD.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					int upperBound2 = Public2.PRJ.BLD[i].CoordWM.GetUpperBound(0);
					for (int j = 0; j <= upperBound2; j++)
					{
						Public2.PRJ.BLD[i].CoordWM[j].Ywm = Public2.PRJ.BLD[i].CoordWM[j].Ywm - num;
					}
				}
			}
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void cmdUndo_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		cmdAddBuilding.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingBuilding = false;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartAddingNodes = false;
		MyProject.Forms.Form1.IndexBLD = -1;
		MyProject.Forms.Form1.IndexPNT = -1;
		gbBuildingParameters.Visible = false;
		Module1.BuildingRestore();
		MyProject.Forms.Form1.PB.Refresh();
	}

	private void cmdRedo_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		cmdAddBuilding.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingBuilding = false;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartAddingNodes = false;
		MyProject.Forms.Form1.IndexBLD = -1;
		MyProject.Forms.Form1.IndexPNT = -1;
		gbBuildingParameters.Visible = false;
		Module1.BuildingForward();
		MyProject.Forms.Form1.PB.Refresh();
	}

	private void cmdAutoFundation_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		cmdAddBuilding.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingBuilding = false;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartAddingNodes = false;
		MyProject.Forms.Form1.IndexBLD = -1;
		MyProject.Forms.Form1.IndexPNT = -1;
		gbBuildingParameters.Visible = false;
		ContFundationMethod.Show(this, cmdAutoFundation.Location);
	}

	private void CmdMinHeight_Click(object sender, EventArgs e)
	{
		if (Information.IsNothing(Public2.PRJ.BLD) || (!Information.IsNothing(Public2.PRJ.BLD) && Interaction.MsgBox(Localization.LocMSG(28), MsgBoxStyle.YesNo) != MsgBoxResult.Yes))
		{
			return;
		}
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.BLD))
			{
				int upperBound = Public2.PRJ.BLD.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					float num = Triangul.FindHeightTriang(Public2.PRJ.BLD[i].CoordWM[0].Xwm, Public2.PRJ.BLD[i].CoordWM[0].Ywm);
					int num2 = Public2.PRJ.BLD[i].CoordWM.GetUpperBound(0) - 1;
					for (int j = 1; j <= num2; j++)
					{
						float num3 = Triangul.FindHeightTriang(Public2.PRJ.BLD[i].CoordWM[j].Xwm, Public2.PRJ.BLD[i].CoordWM[j].Ywm);
						if (num3 < num)
						{
							num = num3;
						}
					}
					Public2.PRJ.BLD[i].Fundation = (float)Math.Round(num, 1);
				}
			}
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void CmdMaxHeight_Click(object sender, EventArgs e)
	{
		if (Information.IsNothing(Public2.PRJ.BLD) || (!Information.IsNothing(Public2.PRJ.BLD) && Interaction.MsgBox(Localization.LocMSG(28), MsgBoxStyle.YesNo) != MsgBoxResult.Yes))
		{
			return;
		}
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.BLD))
			{
				int upperBound = Public2.PRJ.BLD.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					float num = Triangul.FindHeightTriang(Public2.PRJ.BLD[i].CoordWM[0].Xwm, Public2.PRJ.BLD[i].CoordWM[0].Ywm);
					int num2 = Public2.PRJ.BLD[i].CoordWM.GetUpperBound(0) - 1;
					for (int j = 1; j <= num2; j++)
					{
						float num3 = Triangul.FindHeightTriang(Public2.PRJ.BLD[i].CoordWM[j].Xwm, Public2.PRJ.BLD[i].CoordWM[j].Ywm);
						if (num3 > num)
						{
							num = num3;
						}
					}
					Public2.PRJ.BLD[i].Fundation = (float)Math.Round(num, 1);
				}
			}
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void cmdAverageHeight_Click(object sender, EventArgs e)
	{
		if (Information.IsNothing(Public2.PRJ.BLD) || (!Information.IsNothing(Public2.PRJ.BLD) && Interaction.MsgBox(Localization.LocMSG(28), MsgBoxStyle.YesNo) != MsgBoxResult.Yes))
		{
			return;
		}
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.BLD))
			{
				int upperBound = Public2.PRJ.BLD.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					float num = 0f;
					int num2 = Public2.PRJ.BLD[i].CoordWM.GetUpperBound(0) - 1;
					for (int j = 0; j <= num2; j++)
					{
						num += Triangul.FindHeightTriang(Public2.PRJ.BLD[i].CoordWM[j].Xwm, Public2.PRJ.BLD[i].CoordWM[j].Ywm);
					}
					Public2.PRJ.BLD[i].Fundation = (float)Math.Round(num / (float)Public2.PRJ.BLD[i].CoordWM.GetUpperBound(0), 1);
				}
			}
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void cmdAutoFundationOneBLD_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		ContFundationMethodOneBLD.Show(this, cmdAutoFundationOneBLD.Location);
	}

	private void cmdMinHeightOneBLD_Click(object sender, EventArgs e)
	{
		float num = Triangul.FindHeightTriang(Public2.PRJ.BLD[MyProject.Forms.Form1.IndexBLD].CoordWM[0].Xwm, Public2.PRJ.BLD[MyProject.Forms.Form1.IndexBLD].CoordWM[0].Ywm);
		checked
		{
			int num2 = Public2.PRJ.BLD[MyProject.Forms.Form1.IndexBLD].CoordWM.GetUpperBound(0) - 1;
			for (int i = 1; i <= num2; i++)
			{
				float num3 = Triangul.FindHeightTriang(Public2.PRJ.BLD[MyProject.Forms.Form1.IndexBLD].CoordWM[i].Xwm, Public2.PRJ.BLD[MyProject.Forms.Form1.IndexBLD].CoordWM[i].Ywm);
				if (num3 < num)
				{
					num = num3;
				}
			}
			txtFundation.Text = Conversions.ToString(Math.Round(num, 1));
			Public2.PRJ.BLD[MyProject.Forms.Form1.IndexBLD].Fundation = (float)Math.Round(num, 1);
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void CmdMaxHeightOneBLD_Click(object sender, EventArgs e)
	{
		float num = Triangul.FindHeightTriang(Public2.PRJ.BLD[MyProject.Forms.Form1.IndexBLD].CoordWM[0].Xwm, Public2.PRJ.BLD[MyProject.Forms.Form1.IndexBLD].CoordWM[0].Ywm);
		checked
		{
			int num2 = Public2.PRJ.BLD[MyProject.Forms.Form1.IndexBLD].CoordWM.GetUpperBound(0) - 1;
			for (int i = 1; i <= num2; i++)
			{
				float num3 = Triangul.FindHeightTriang(Public2.PRJ.BLD[MyProject.Forms.Form1.IndexBLD].CoordWM[i].Xwm, Public2.PRJ.BLD[MyProject.Forms.Form1.IndexBLD].CoordWM[i].Ywm);
				if (num3 > num)
				{
					num = num3;
				}
			}
			Public2.PRJ.BLD[MyProject.Forms.Form1.IndexBLD].Fundation = (float)Math.Round(num, 1);
			txtFundation.Text = Conversions.ToString(Math.Round(num, 1));
			Public2.PRJ.BLD[MyProject.Forms.Form1.IndexBLD].Fundation = (float)Math.Round(num, 1);
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void cmdAverageHeightOneBLD_Click(object sender, EventArgs e)
	{
		float num = 0f;
		checked
		{
			int num2 = Public2.PRJ.BLD[MyProject.Forms.Form1.IndexBLD].CoordWM.GetUpperBound(0) - 1;
			for (int i = 0; i <= num2; i++)
			{
				num += Triangul.FindHeightTriang(Public2.PRJ.BLD[MyProject.Forms.Form1.IndexBLD].CoordWM[i].Xwm, Public2.PRJ.BLD[MyProject.Forms.Form1.IndexBLD].CoordWM[i].Ywm);
			}
			Public2.PRJ.BLD[MyProject.Forms.Form1.IndexBLD].Fundation = (float)Math.Round(num / (float)Public2.PRJ.BLD[MyProject.Forms.Form1.IndexBLD].CoordWM.GetUpperBound(0), 1);
			txtFundation.Text = Conversions.ToString(Public2.PRJ.BLD[MyProject.Forms.Form1.IndexBLD].Fundation);
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void txtHeight_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			if (Module1.ValidText(txtHeight.Text, CheckLimits: true, 0f, 300f, IncludeMin: true, IncludeMax: true, Message: false))
			{
				Public2.PRJ.BLD[MyProject.Forms.Form1.IndexBLD].H = Module1.QSNG(txtHeight.Text);
				MyProject.Forms.Form1.PB.Refresh();
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
	}

	private void txtFundation_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			if (Module1.ValidText(txtFundation.Text, CheckLimits: true, -100f, 5000f, IncludeMin: true, IncludeMax: true, Message: false))
			{
				Public2.PRJ.BLD[MyProject.Forms.Form1.IndexBLD].Fundation = Module1.QSNG(txtFundation.Text);
				MyProject.Forms.Form1.PB.Refresh();
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
	}

	private void txtText_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			Public2.PRJ.BLD[MyProject.Forms.Form1.IndexBLD].Text = txtText.Text;
			MyProject.Forms.Form1.PB.Refresh();
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
	}

	private void chkShow_MouseClick(object sender, MouseEventArgs e)
	{
		try
		{
			Public2.PRJ.BLD[MyProject.Forms.Form1.IndexBLD].Show = chkShow.Checked;
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
	}

	private void txtBuildingLineWidth_ValueChanged(object sender, EventArgs e)
	{
		if (FlagOK)
		{
			Public2.PRJ.SETT.BuildingLineWidth = Convert.ToByte(txtBuildingLineWidth.Value);
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void txtBuildingTextSize_ValueChanged(object sender, EventArgs e)
	{
		if (FlagOK)
		{
			Public2.PRJ.SETT.BuildingTextSize = Convert.ToByte(txtBuildingTextSize.Value);
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void pbColor1_Click(object sender, EventArgs e)
	{
		CD1.Color = pbColor.BackColor;
		CD1.FullOpen = true;
		if (CD1.ShowDialog() == DialogResult.OK)
		{
			pbColor.BackColor = CD1.Color;
			Public2.PRJ.SETT.BuildingColor = pbColor.BackColor;
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void rbBuildingShielding1_CheckedChanged(object sender, EventArgs e)
	{
		if (FlagOK)
		{
			if (rbBuildingShielding1.Checked)
			{
				Public2.PRJ.SETT.UseBuildings = 0;
				gbCustomAtt.Enabled = false;
			}
			else if (rbBuildingShielding2.Checked)
			{
				Public2.PRJ.SETT.UseBuildings = 1;
				gbCustomAtt.Enabled = false;
			}
			else if (rbBuildingShielding3.Checked)
			{
				Public2.PRJ.SETT.UseBuildings = 2;
				gbCustomAtt.Enabled = true;
			}
		}
	}

	private void txtBuildAttenSubband1_Validating(object sender, CancelEventArgs e)
	{
		TextBox textBox = txtBuildAttenSubband1;
		string TextProperty = textBox.Text;
		ref float buildAttenSubband = ref Public2.PRJ.SETT.BuildAttenSubband1;
		double DblVariable = buildAttenSubband;
		Module1.TextPropertyValidating(ref TextProperty, ref DblVariable, CheckLimits: true, 0f, 30f, IncludeMin: true, IncludeMax: true, Message: true);
		buildAttenSubband = (float)DblVariable;
		textBox.Text = TextProperty;
	}

	private void txtBuildAttenSubband2_Validating(object sender, CancelEventArgs e)
	{
		TextBox textBox = txtBuildAttenSubband2;
		string TextProperty = textBox.Text;
		ref float buildAttenSubband = ref Public2.PRJ.SETT.BuildAttenSubband2;
		double DblVariable = buildAttenSubband;
		Module1.TextPropertyValidating(ref TextProperty, ref DblVariable, CheckLimits: true, 0f, 30f, IncludeMin: true, IncludeMax: true, Message: true);
		buildAttenSubband = (float)DblVariable;
		textBox.Text = TextProperty;
	}

	private void txtBuildFreqBorder_Validating(object sender, CancelEventArgs e)
	{
		TextBox textBox = txtBuildFreqBorder;
		string TextProperty = textBox.Text;
		ref float buildFreqBorder = ref Public2.PRJ.SETT.BuildFreqBorder;
		double DblVariable = buildFreqBorder;
		Module1.TextPropertyValidating(ref TextProperty, ref DblVariable, CheckLimits: true, 30f, 100000f, IncludeMin: true, IncludeMax: true, Message: true);
		buildFreqBorder = (float)DblVariable;
		textBox.Text = TextProperty;
	}

	private void chkUseAttenuation_MouseClick(object sender, MouseEventArgs e)
	{
		try
		{
			Public2.PRJ.BLD[MyProject.Forms.Form1.IndexBLD].UseAttenuation = chkUseAttenuation.Checked;
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
	}
}
