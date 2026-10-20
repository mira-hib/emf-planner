using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using EMFPlanner.My;
using EMFPlanner.My.Resources;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[DesignerGenerated]
public class UC_Lines : UserControl
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

	[field: AccessedThroughProperty("cmdAddCopyObject")]
	internal virtual Button cmdAddCopyObject
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = cmdAddCopyObject_MouseClick;
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

	[field: AccessedThroughProperty("cmdDelOneObject")]
	internal virtual Button cmdDelOneObject
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdDelOneObject_Click;
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

	[field: AccessedThroughProperty("gbRoad")]
	internal virtual GroupBox gbRoad
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdDelAllObjects")]
	internal virtual Button cmdDelAllObjects
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdDelAllObjects_Click;
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

	[field: AccessedThroughProperty("cmdGetLinesFromCloud")]
	internal virtual Button cmdGetLinesFromCloud
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdGetLinesFromCloud_Click;
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

	[field: AccessedThroughProperty("cmdAddRoad")]
	internal virtual Button cmdAddRoad
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = cmdAddRoad_MouseClick;
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

	[field: AccessedThroughProperty("gbText")]
	internal virtual GroupBox gbText
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

	[field: AccessedThroughProperty("txtRoadWidth")]
	internal virtual NumericUpDown txtRoadWidth
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = txtRoadWidth_MouseUp;
			MouseEventHandler value3 = txtRoadWidth_MouseUp;
			NumericUpDown numericUpDown = field;
			if (numericUpDown != null)
			{
				numericUpDown.MouseUp -= value2;
				numericUpDown.MouseDown -= value3;
			}
			field = value;
			numericUpDown = field;
			if (numericUpDown != null)
			{
				numericUpDown.MouseUp += value2;
				numericUpDown.MouseDown += value3;
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

	[field: AccessedThroughProperty("ilICO")]
	internal virtual ImageList ilICO
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("gbFence")]
	internal virtual GroupBox gbFence
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label3")]
	internal virtual Label Label3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("pbFenceLineType")]
	internal virtual PictureBox pbFenceLineType
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = pbFenceLineType_MouseClick;
			PictureBox pictureBox = field;
			if (pictureBox != null)
			{
				pictureBox.MouseClick -= value2;
			}
			field = value;
			pictureBox = field;
			if (pictureBox != null)
			{
				pictureBox.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("txtFenceWidth")]
	internal virtual NumericUpDown txtFenceWidth
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = txtFenceWidth_MouseUp;
			MouseEventHandler value3 = txtFenceWidth_MouseUp;
			NumericUpDown numericUpDown = field;
			if (numericUpDown != null)
			{
				numericUpDown.MouseUp -= value2;
				numericUpDown.MouseDown -= value3;
			}
			field = value;
			numericUpDown = field;
			if (numericUpDown != null)
			{
				numericUpDown.MouseUp += value2;
				numericUpDown.MouseDown += value3;
			}
		}
	}

	[field: AccessedThroughProperty("Label5")]
	internal virtual Label Label5
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtWidthBorder")]
	internal virtual NumericUpDown txtWidthBorder
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = txtWidthBorder_MouseUp;
			MouseEventHandler value3 = txtWidthBorder_MouseUp;
			NumericUpDown numericUpDown = field;
			if (numericUpDown != null)
			{
				numericUpDown.MouseUp -= value2;
				numericUpDown.MouseDown -= value3;
			}
			field = value;
			numericUpDown = field;
			if (numericUpDown != null)
			{
				numericUpDown.MouseUp += value2;
				numericUpDown.MouseDown += value3;
			}
		}
	}

	[field: AccessedThroughProperty("Label2")]
	internal virtual Label Label2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdAddText")]
	internal virtual Button cmdAddText
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = cmdAddText_MouseClick;
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

	[field: AccessedThroughProperty("cmdAddFence")]
	internal virtual Button cmdAddFence
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = cmdAddFence_MouseClick;
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

	[field: AccessedThroughProperty("cmdSplitNode")]
	internal virtual Button cmdSplitNode
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = cmdSplitNode_MouseClick;
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

	[field: AccessedThroughProperty("txtFontSize")]
	internal virtual NumericUpDown txtFontSize
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = txtFontSize_MouseUp;
			MouseEventHandler value3 = txtFontSize_MouseUp;
			NumericUpDown numericUpDown = field;
			if (numericUpDown != null)
			{
				numericUpDown.MouseUp -= value2;
				numericUpDown.MouseDown -= value3;
			}
			field = value;
			numericUpDown = field;
			if (numericUpDown != null)
			{
				numericUpDown.MouseUp += value2;
				numericUpDown.MouseDown += value3;
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

	[field: AccessedThroughProperty("chkShowPoint")]
	internal virtual CheckBox chkShowPoint
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkShowPoint_MouseClick;
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

	[field: AccessedThroughProperty("Label6")]
	internal virtual Label Label6
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("tbAngle")]
	internal virtual TrackBar tbAngle
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = tbAngle_ValueChanged;
			TrackBar trackBar = field;
			if (trackBar != null)
			{
				trackBar.ValueChanged -= value2;
			}
			field = value;
			trackBar = field;
			if (trackBar != null)
			{
				trackBar.ValueChanged += value2;
			}
		}
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

	[field: AccessedThroughProperty("Label8")]
	internal virtual Label Label8
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cbFont")]
	internal virtual ComboBox cbFont
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cbFont_SelectionChangeCommitted;
			ComboBox comboBox = field;
			if (comboBox != null)
			{
				comboBox.SelectionChangeCommitted -= value2;
			}
			field = value;
			comboBox = field;
			if (comboBox != null)
			{
				comboBox.SelectionChangeCommitted += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmdIcon")]
	internal virtual Button cmdIcon
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdIcon_Click;
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

	public UC_Lines()
	{
		Load += UC_Lines_Load;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EMFPlanner.UC_Lines));
		this.GroupBox1 = new System.Windows.Forms.GroupBox();
		this.cmdSplitNode = new System.Windows.Forms.Button();
		this.cmdUp = new System.Windows.Forms.Button();
		this.cmdLeft = new System.Windows.Forms.Button();
		this.cmdDown = new System.Windows.Forms.Button();
		this.cmdRight = new System.Windows.Forms.Button();
		this.cmdAddText = new System.Windows.Forms.Button();
		this.cmdAddFence = new System.Windows.Forms.Button();
		this.gbFence = new System.Windows.Forms.GroupBox();
		this.Label3 = new System.Windows.Forms.Label();
		this.pbFenceLineType = new System.Windows.Forms.PictureBox();
		this.txtFenceWidth = new System.Windows.Forms.NumericUpDown();
		this.Label5 = new System.Windows.Forms.Label();
		this.cmdDeleteNode = new System.Windows.Forms.Button();
		this.cmdAddNode = new System.Windows.Forms.Button();
		this.gbText = new System.Windows.Forms.GroupBox();
		this.cmdIcon = new System.Windows.Forms.Button();
		this.Label8 = new System.Windows.Forms.Label();
		this.cbFont = new System.Windows.Forms.ComboBox();
		this.Label6 = new System.Windows.Forms.Label();
		this.tbAngle = new System.Windows.Forms.TrackBar();
		this.Label7 = new System.Windows.Forms.Label();
		this.txtText = new System.Windows.Forms.TextBox();
		this.chkShowPoint = new System.Windows.Forms.CheckBox();
		this.txtFontSize = new System.Windows.Forms.NumericUpDown();
		this.Label4 = new System.Windows.Forms.Label();
		this.cmdRedo = new System.Windows.Forms.Button();
		this.cmdUndo = new System.Windows.Forms.Button();
		this.cmdAddCopyObject = new System.Windows.Forms.Button();
		this.cmdDelOneObject = new System.Windows.Forms.Button();
		this.gbRoad = new System.Windows.Forms.GroupBox();
		this.txtWidthBorder = new System.Windows.Forms.NumericUpDown();
		this.Label2 = new System.Windows.Forms.Label();
		this.txtRoadWidth = new System.Windows.Forms.NumericUpDown();
		this.Label1 = new System.Windows.Forms.Label();
		this.cmdDelAllObjects = new System.Windows.Forms.Button();
		this.cmdSelect = new System.Windows.Forms.Button();
		this.cmdGetLinesFromCloud = new System.Windows.Forms.Button();
		this.cmdAddRoad = new System.Windows.Forms.Button();
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.CD1 = new System.Windows.Forms.ColorDialog();
		this.ilICO = new System.Windows.Forms.ImageList(this.components);
		this.GroupBox1.SuspendLayout();
		this.gbFence.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pbFenceLineType).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFenceWidth).BeginInit();
		this.gbText.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.tbAngle).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtFontSize).BeginInit();
		this.gbRoad.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtWidthBorder).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtRoadWidth).BeginInit();
		base.SuspendLayout();
		this.GroupBox1.Controls.Add(this.cmdSplitNode);
		this.GroupBox1.Controls.Add(this.cmdUp);
		this.GroupBox1.Controls.Add(this.cmdLeft);
		this.GroupBox1.Controls.Add(this.cmdDown);
		this.GroupBox1.Controls.Add(this.cmdRight);
		this.GroupBox1.Controls.Add(this.cmdAddText);
		this.GroupBox1.Controls.Add(this.cmdAddFence);
		this.GroupBox1.Controls.Add(this.gbFence);
		this.GroupBox1.Controls.Add(this.cmdDeleteNode);
		this.GroupBox1.Controls.Add(this.cmdAddNode);
		this.GroupBox1.Controls.Add(this.gbText);
		this.GroupBox1.Controls.Add(this.cmdRedo);
		this.GroupBox1.Controls.Add(this.cmdUndo);
		this.GroupBox1.Controls.Add(this.cmdAddCopyObject);
		this.GroupBox1.Controls.Add(this.cmdDelOneObject);
		this.GroupBox1.Controls.Add(this.gbRoad);
		this.GroupBox1.Controls.Add(this.cmdDelAllObjects);
		this.GroupBox1.Controls.Add(this.cmdSelect);
		this.GroupBox1.Controls.Add(this.cmdGetLinesFromCloud);
		this.GroupBox1.Controls.Add(this.cmdAddRoad);
		this.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GroupBox1.Location = new System.Drawing.Point(0, 0);
		this.GroupBox1.Name = "GroupBox1";
		this.GroupBox1.Size = new System.Drawing.Size(300, 768);
		this.GroupBox1.TabIndex = 2;
		this.GroupBox1.TabStop = false;
		this.GroupBox1.Text = "Lines and labels";
		this.cmdSplitNode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdSplitNode.Image = (System.Drawing.Image)resources.GetObject("cmdSplitNode.Image");
		this.cmdSplitNode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdSplitNode.Location = new System.Drawing.Point(66, 57);
		this.cmdSplitNode.Name = "cmdSplitNode";
		this.cmdSplitNode.Size = new System.Drawing.Size(26, 26);
		this.cmdSplitNode.TabIndex = 174;
		this.cmdSplitNode.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdSplitNode, " \tBreak knot on selected line");
		this.cmdSplitNode.UseVisualStyleBackColor = true;
		this.cmdUp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdUp.Image = (System.Drawing.Image)resources.GetObject("cmdUp.Image");
		this.cmdUp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdUp.Location = new System.Drawing.Point(147, 57);
		this.cmdUp.Name = "cmdUp";
		this.cmdUp.Size = new System.Drawing.Size(26, 26);
		this.cmdUp.TabIndex = 173;
		this.cmdUp.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdUp, "Move all objects by one pixel");
		this.cmdUp.UseVisualStyleBackColor = true;
		this.cmdLeft.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdLeft.Image = (System.Drawing.Image)resources.GetObject("cmdLeft.Image");
		this.cmdLeft.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdLeft.Location = new System.Drawing.Point(93, 57);
		this.cmdLeft.Name = "cmdLeft";
		this.cmdLeft.Size = new System.Drawing.Size(26, 26);
		this.cmdLeft.TabIndex = 172;
		this.cmdLeft.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdLeft, "Move all objects by one pixel");
		this.cmdLeft.UseVisualStyleBackColor = true;
		this.cmdDown.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdDown.Image = (System.Drawing.Image)resources.GetObject("cmdDown.Image");
		this.cmdDown.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdDown.Location = new System.Drawing.Point(174, 57);
		this.cmdDown.Name = "cmdDown";
		this.cmdDown.Size = new System.Drawing.Size(26, 26);
		this.cmdDown.TabIndex = 171;
		this.cmdDown.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdDown, "Move all objects by one pixel");
		this.cmdDown.UseVisualStyleBackColor = true;
		this.cmdRight.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdRight.Image = (System.Drawing.Image)resources.GetObject("cmdRight.Image");
		this.cmdRight.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdRight.Location = new System.Drawing.Point(120, 57);
		this.cmdRight.Name = "cmdRight";
		this.cmdRight.Size = new System.Drawing.Size(26, 26);
		this.cmdRight.TabIndex = 170;
		this.cmdRight.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdRight, "Move all objects by one pixel");
		this.cmdRight.UseVisualStyleBackColor = true;
		this.cmdAddText.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdAddText.Image = (System.Drawing.Image)resources.GetObject("cmdAddText.Image");
		this.cmdAddText.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdAddText.Location = new System.Drawing.Point(120, 25);
		this.cmdAddText.Name = "cmdAddText";
		this.cmdAddText.Size = new System.Drawing.Size(26, 26);
		this.cmdAddText.TabIndex = 169;
		this.cmdAddText.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdAddText, "Add a label");
		this.cmdAddText.UseVisualStyleBackColor = true;
		this.cmdAddFence.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdAddFence.Image = (System.Drawing.Image)resources.GetObject("cmdAddFence.Image");
		this.cmdAddFence.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdAddFence.Location = new System.Drawing.Point(93, 25);
		this.cmdAddFence.Name = "cmdAddFence";
		this.cmdAddFence.Size = new System.Drawing.Size(26, 26);
		this.cmdAddFence.TabIndex = 168;
		this.cmdAddFence.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdAddFence, "Add a barrier");
		this.cmdAddFence.UseVisualStyleBackColor = true;
		this.gbFence.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gbFence.Controls.Add(this.Label3);
		this.gbFence.Controls.Add(this.pbFenceLineType);
		this.gbFence.Controls.Add(this.txtFenceWidth);
		this.gbFence.Controls.Add(this.Label5);
		this.gbFence.Location = new System.Drawing.Point(3, 201);
		this.gbFence.Name = "gbFence";
		this.gbFence.Size = new System.Drawing.Size(294, 90);
		this.gbFence.TabIndex = 167;
		this.gbFence.TabStop = false;
		this.gbFence.Text = "Fencing parameters";
		this.gbFence.Visible = false;
		this.Label3.AutoSize = true;
		this.Label3.Location = new System.Drawing.Point(45, 51);
		this.Label3.Name = "Label3";
		this.Label3.Size = new System.Drawing.Size(50, 13);
		this.Label3.TabIndex = 174;
		this.Label3.Text = "Line type";
		this.pbFenceLineType.BackColor = System.Drawing.Color.Transparent;
		this.pbFenceLineType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.pbFenceLineType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.pbFenceLineType.Location = new System.Drawing.Point(103, 51);
		this.pbFenceLineType.Name = "pbFenceLineType";
		this.pbFenceLineType.Size = new System.Drawing.Size(185, 20);
		this.pbFenceLineType.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
		this.pbFenceLineType.TabIndex = 173;
		this.pbFenceLineType.TabStop = false;
		this.txtFenceWidth.Location = new System.Drawing.Point(103, 22);
		this.txtFenceWidth.Maximum = new decimal(new int[4] { 5, 0, 0, 0 });
		this.txtFenceWidth.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.txtFenceWidth.Name = "txtFenceWidth";
		this.txtFenceWidth.ReadOnly = true;
		this.txtFenceWidth.Size = new System.Drawing.Size(45, 20);
		this.txtFenceWidth.TabIndex = 167;
		this.txtFenceWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtFenceWidth.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		this.Label5.AutoSize = true;
		this.Label5.Location = new System.Drawing.Point(20, 24);
		this.Label5.Name = "Label5";
		this.Label5.Size = new System.Drawing.Size(75, 13);
		this.Label5.TabIndex = 168;
		this.Label5.Text = "Line thickness";
		this.cmdDeleteNode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdDeleteNode.Image = (System.Drawing.Image)resources.GetObject("cmdDeleteNode.Image");
		this.cmdDeleteNode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdDeleteNode.Location = new System.Drawing.Point(39, 57);
		this.cmdDeleteNode.Name = "cmdDeleteNode";
		this.cmdDeleteNode.Size = new System.Drawing.Size(26, 26);
		this.cmdDeleteNode.TabIndex = 166;
		this.cmdDeleteNode.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdDeleteNode, "Delete node on selected line");
		this.cmdDeleteNode.UseVisualStyleBackColor = true;
		this.cmdAddNode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdAddNode.Image = (System.Drawing.Image)resources.GetObject("cmdAddNode.Image");
		this.cmdAddNode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdAddNode.Location = new System.Drawing.Point(12, 57);
		this.cmdAddNode.Name = "cmdAddNode";
		this.cmdAddNode.Size = new System.Drawing.Size(26, 26);
		this.cmdAddNode.TabIndex = 165;
		this.cmdAddNode.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdAddNode, "Add node on selected line");
		this.cmdAddNode.UseVisualStyleBackColor = true;
		this.gbText.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gbText.Controls.Add(this.cmdIcon);
		this.gbText.Controls.Add(this.Label8);
		this.gbText.Controls.Add(this.cbFont);
		this.gbText.Controls.Add(this.Label6);
		this.gbText.Controls.Add(this.tbAngle);
		this.gbText.Controls.Add(this.Label7);
		this.gbText.Controls.Add(this.txtText);
		this.gbText.Controls.Add(this.chkShowPoint);
		this.gbText.Controls.Add(this.txtFontSize);
		this.gbText.Controls.Add(this.Label4);
		this.gbText.Location = new System.Drawing.Point(3, 313);
		this.gbText.Name = "gbText";
		this.gbText.Size = new System.Drawing.Size(294, 210);
		this.gbText.TabIndex = 155;
		this.gbText.TabStop = false;
		this.gbText.Text = "Label parameters";
		this.gbText.Visible = false;
		this.cmdIcon.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdIcon.Image = EMFPlanner.My.Resources.Resources.point2x2;
		this.cmdIcon.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdIcon.Location = new System.Drawing.Point(171, 142);
		this.cmdIcon.Name = "cmdIcon";
		this.cmdIcon.Size = new System.Drawing.Size(26, 26);
		this.cmdIcon.TabIndex = 178;
		this.cmdIcon.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdIcon, "Icon selection");
		this.cmdIcon.UseVisualStyleBackColor = true;
		this.Label8.AutoSize = true;
		this.Label8.Location = new System.Drawing.Point(67, 91);
		this.Label8.Name = "Label8";
		this.Label8.Size = new System.Drawing.Size(28, 13);
		this.Label8.TabIndex = 177;
		this.Label8.Text = "Font";
		this.cbFont.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.cbFont.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbFont.FormattingEnabled = true;
		this.cbFont.Location = new System.Drawing.Point(103, 88);
		this.cbFont.Name = "cbFont";
		this.cbFont.Size = new System.Drawing.Size(185, 21);
		this.cbFont.TabIndex = 176;
		this.Label6.AutoSize = true;
		this.Label6.Location = new System.Drawing.Point(19, 176);
		this.Label6.Name = "Label6";
		this.Label6.Size = new System.Drawing.Size(76, 13);
		this.Label6.TabIndex = 175;
		this.Label6.Text = "Rotation angle";
		this.tbAngle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.tbAngle.AutoSize = false;
		this.tbAngle.LargeChange = 15;
		this.tbAngle.Location = new System.Drawing.Point(103, 174);
		this.tbAngle.Maximum = 180;
		this.tbAngle.Minimum = -180;
		this.tbAngle.Name = "tbAngle";
		this.tbAngle.Size = new System.Drawing.Size(185, 26);
		this.tbAngle.TabIndex = 174;
		this.tbAngle.TickStyle = System.Windows.Forms.TickStyle.None;
		this.Label7.AutoSize = true;
		this.Label7.Location = new System.Drawing.Point(67, 46);
		this.Label7.Name = "Label7";
		this.Label7.Size = new System.Drawing.Size(28, 13);
		this.Label7.TabIndex = 173;
		this.Label7.Text = "Text";
		this.txtText.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtText.Location = new System.Drawing.Point(103, 23);
		this.txtText.Multiline = true;
		this.txtText.Name = "txtText";
		this.txtText.Size = new System.Drawing.Size(185, 59);
		this.txtText.TabIndex = 172;
		this.chkShowPoint.AutoSize = true;
		this.chkShowPoint.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.chkShowPoint.Location = new System.Drawing.Point(69, 151);
		this.chkShowPoint.Name = "chkShowPoint";
		this.chkShowPoint.Size = new System.Drawing.Size(47, 17);
		this.chkShowPoint.TabIndex = 171;
		this.chkShowPoint.TabStop = false;
		this.chkShowPoint.Text = "Icon";
		this.chkShowPoint.UseVisualStyleBackColor = true;
		this.txtFontSize.Location = new System.Drawing.Point(103, 121);
		this.txtFontSize.Maximum = new decimal(new int[4] { 20, 0, 0, 0 });
		this.txtFontSize.Minimum = new decimal(new int[4] { 3, 0, 0, 0 });
		this.txtFontSize.Name = "txtFontSize";
		this.txtFontSize.ReadOnly = true;
		this.txtFontSize.Size = new System.Drawing.Size(45, 20);
		this.txtFontSize.TabIndex = 169;
		this.txtFontSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtFontSize.Value = new decimal(new int[4] { 8, 0, 0, 0 });
		this.Label4.AutoSize = true;
		this.Label4.Location = new System.Drawing.Point(46, 123);
		this.Label4.Name = "Label4";
		this.Label4.Size = new System.Drawing.Size(49, 13);
		this.Label4.TabIndex = 170;
		this.Label4.Text = "Font size";
		this.cmdRedo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdRedo.Image = (System.Drawing.Image)resources.GetObject("cmdRedo.Image");
		this.cmdRedo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdRedo.Location = new System.Drawing.Point(255, 25);
		this.cmdRedo.Name = "cmdRedo";
		this.cmdRedo.Size = new System.Drawing.Size(26, 26);
		this.cmdRedo.TabIndex = 158;
		this.cmdRedo.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdRedo, "Redo");
		this.cmdRedo.UseVisualStyleBackColor = true;
		this.cmdUndo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdUndo.Image = (System.Drawing.Image)resources.GetObject("cmdUndo.Image");
		this.cmdUndo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdUndo.Location = new System.Drawing.Point(228, 25);
		this.cmdUndo.Name = "cmdUndo";
		this.cmdUndo.Size = new System.Drawing.Size(26, 26);
		this.cmdUndo.TabIndex = 157;
		this.cmdUndo.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdUndo, "Undo");
		this.cmdUndo.UseVisualStyleBackColor = true;
		this.cmdAddCopyObject.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdAddCopyObject.Image = (System.Drawing.Image)resources.GetObject("cmdAddCopyObject.Image");
		this.cmdAddCopyObject.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdAddCopyObject.Location = new System.Drawing.Point(147, 25);
		this.cmdAddCopyObject.Name = "cmdAddCopyObject";
		this.cmdAddCopyObject.Size = new System.Drawing.Size(26, 26);
		this.cmdAddCopyObject.TabIndex = 156;
		this.cmdAddCopyObject.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdAddCopyObject, "Create a copy of the selected object");
		this.cmdAddCopyObject.UseVisualStyleBackColor = true;
		this.cmdDelOneObject.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdDelOneObject.Image = (System.Drawing.Image)resources.GetObject("cmdDelOneObject.Image");
		this.cmdDelOneObject.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdDelOneObject.Location = new System.Drawing.Point(174, 25);
		this.cmdDelOneObject.Name = "cmdDelOneObject";
		this.cmdDelOneObject.Size = new System.Drawing.Size(26, 26);
		this.cmdDelOneObject.TabIndex = 155;
		this.cmdDelOneObject.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdDelOneObject, "Delete selected object");
		this.cmdDelOneObject.UseVisualStyleBackColor = true;
		this.gbRoad.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gbRoad.Controls.Add(this.txtWidthBorder);
		this.gbRoad.Controls.Add(this.Label2);
		this.gbRoad.Controls.Add(this.txtRoadWidth);
		this.gbRoad.Controls.Add(this.Label1);
		this.gbRoad.Location = new System.Drawing.Point(3, 95);
		this.gbRoad.Name = "gbRoad";
		this.gbRoad.Size = new System.Drawing.Size(294, 88);
		this.gbRoad.TabIndex = 154;
		this.gbRoad.TabStop = false;
		this.gbRoad.Text = "Road parameters";
		this.gbRoad.Visible = false;
		this.txtWidthBorder.Location = new System.Drawing.Point(103, 52);
		this.txtWidthBorder.Maximum = new decimal(new int[4] { 4, 0, 0, 0 });
		this.txtWidthBorder.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.txtWidthBorder.Name = "txtWidthBorder";
		this.txtWidthBorder.ReadOnly = true;
		this.txtWidthBorder.Size = new System.Drawing.Size(45, 20);
		this.txtWidthBorder.TabIndex = 169;
		this.txtWidthBorder.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtWidthBorder.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		this.Label2.AutoSize = true;
		this.Label2.Location = new System.Drawing.Point(20, 54);
		this.Label2.Name = "Label2";
		this.Label2.Size = new System.Drawing.Size(75, 13);
		this.Label2.TabIndex = 170;
		this.Label2.Text = "Line thickness";
		this.txtRoadWidth.Location = new System.Drawing.Point(103, 26);
		this.txtRoadWidth.Maximum = new decimal(new int[4] { 20, 0, 0, 0 });
		this.txtRoadWidth.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.txtRoadWidth.Name = "txtRoadWidth";
		this.txtRoadWidth.ReadOnly = true;
		this.txtRoadWidth.Size = new System.Drawing.Size(45, 20);
		this.txtRoadWidth.TabIndex = 167;
		this.txtRoadWidth.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtRoadWidth.Value = new decimal(new int[4] { 2, 0, 0, 0 });
		this.Label1.AutoSize = true;
		this.Label1.Location = new System.Drawing.Point(34, 33);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(61, 13);
		this.Label1.TabIndex = 168;
		this.Label1.Text = "Road width";
		this.cmdDelAllObjects.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdDelAllObjects.Image = (System.Drawing.Image)resources.GetObject("cmdDelAllObjects.Image");
		this.cmdDelAllObjects.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdDelAllObjects.Location = new System.Drawing.Point(201, 25);
		this.cmdDelAllObjects.Name = "cmdDelAllObjects";
		this.cmdDelAllObjects.Size = new System.Drawing.Size(26, 26);
		this.cmdDelAllObjects.TabIndex = 152;
		this.cmdDelAllObjects.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdDelAllObjects, "Delete all objects");
		this.cmdDelAllObjects.UseVisualStyleBackColor = true;
		this.cmdSelect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdSelect.Image = (System.Drawing.Image)resources.GetObject("cmdSelect.Image");
		this.cmdSelect.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdSelect.Location = new System.Drawing.Point(12, 25);
		this.cmdSelect.Name = "cmdSelect";
		this.cmdSelect.Size = new System.Drawing.Size(26, 26);
		this.cmdSelect.TabIndex = 151;
		this.cmdSelect.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdSelect, "Select object (line or label)");
		this.cmdSelect.UseVisualStyleBackColor = true;
		this.cmdGetLinesFromCloud.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdGetLinesFromCloud.Image = (System.Drawing.Image)resources.GetObject("cmdGetLinesFromCloud.Image");
		this.cmdGetLinesFromCloud.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdGetLinesFromCloud.Location = new System.Drawing.Point(39, 25);
		this.cmdGetLinesFromCloud.Name = "cmdGetLinesFromCloud";
		this.cmdGetLinesFromCloud.Size = new System.Drawing.Size(26, 26);
		this.cmdGetLinesFromCloud.TabIndex = 150;
		this.cmdGetLinesFromCloud.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdGetLinesFromCloud, "Load polylines of roads and railings within the screen from the OpenStreetMap database");
		this.cmdGetLinesFromCloud.UseVisualStyleBackColor = true;
		this.cmdAddRoad.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdAddRoad.Image = (System.Drawing.Image)resources.GetObject("cmdAddRoad.Image");
		this.cmdAddRoad.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdAddRoad.Location = new System.Drawing.Point(66, 25);
		this.cmdAddRoad.Name = "cmdAddRoad";
		this.cmdAddRoad.Size = new System.Drawing.Size(26, 26);
		this.cmdAddRoad.TabIndex = 149;
		this.cmdAddRoad.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdAddRoad, "Add road");
		this.cmdAddRoad.UseVisualStyleBackColor = true;
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
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.GroupBox1);
		this.MinimumSize = new System.Drawing.Size(300, 0);
		base.Name = "UC_Lines";
		base.Size = new System.Drawing.Size(300, 768);
		this.GroupBox1.ResumeLayout(false);
		this.gbFence.ResumeLayout(false);
		this.gbFence.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pbFenceLineType).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFenceWidth).EndInit();
		this.gbText.ResumeLayout(false);
		this.gbText.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.tbAngle).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtFontSize).EndInit();
		this.gbRoad.ResumeLayout(false);
		this.gbRoad.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtWidthBorder).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtRoadWidth).EndInit();
		base.ResumeLayout(false);
	}

	private void UC_Lines_Load(object sender, EventArgs e)
	{
		gbFence.Location = new Point(3, gbRoad.Location.Y);
		gbText.Location = new Point(3, gbRoad.Location.Y);
		int upperBound = Form1_Public.FontList.GetUpperBound(0);
		for (int i = 0; i <= upperBound; i = checked(i + 1))
		{
			cbFont.Items.Add(Form1_Public.FontList[i]);
		}
		FlagOK = true;
		MyProject.Forms.Form1.DrawPIC();
		cmdSelect.PerformClick();
	}

	private void cmdSelect_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		cmdAddRoad.BackColor = SystemColors.Control;
		cmdAddFence.BackColor = SystemColors.Control;
		cmdAddText.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		cmdSplitNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingRoad = false;
		Form1_Public.StartDrawingFence = false;
		Form1_Public.StartDrawingText = false;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartAddingNodes = false;
		Form1_Public.StartSplittingNodes = false;
		MyProject.Forms.Form1.IndexRoad = -1;
		MyProject.Forms.Form1.IndexFence = -1;
		MyProject.Forms.Form1.IndexText = -1;
		Form1_Public.NewRoad = default;
		Form1_Public.NewFence = default;
		Form1_Public.NewText = default;
		MyProject.Forms.Form1.PB.Refresh();
		gbRoad.Visible = false;
		gbFence.Visible = false;
		gbText.Visible = false;
	}

	private void cmdGetLinesFromCloud_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		cmdAddRoad.BackColor = SystemColors.Control;
		cmdAddFence.BackColor = SystemColors.Control;
		cmdAddText.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		cmdSplitNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingRoad = false;
		Form1_Public.StartDrawingFence = false;
		Form1_Public.StartDrawingText = false;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartAddingNodes = false;
		Form1_Public.StartSplittingNodes = false;
		MyProject.Forms.Form1.IndexRoad = -1;
		MyProject.Forms.Form1.IndexFence = -1;
		MyProject.Forms.Form1.IndexText = -1;
		MyProject.Forms.Form1.PB.Refresh();
		gbRoad.Visible = false;
		gbFence.Visible = false;
		gbText.Visible = false;
		if ((!Information.IsNothing(Public2.PRJ.ROAD) | !Information.IsNothing(Public2.PRJ.FENCE) | !Information.IsNothing(Public2.PRJ.TXT)) && Interaction.MsgBox(Localization.LocMSG(37), MsgBoxStyle.YesNo) != MsgBoxResult.Yes)
		{
			return;
		}
		string inputGeometry = PHP_SQL.SquareMercator2Geometry(Form1_Public.ScrCoord1.Xwm, Form1_Public.ScrCoord2.Xwm, Form1_Public.ScrCoord2.Ywm, Form1_Public.ScrCoord1.Ywm);
		string[] HighwayGeometry = default;
		string[] HighwayValue = default;
		PHP_SQL.GetHighway(inputGeometry, ref HighwayGeometry, ref HighwayValue);
		checked
		{
			PHP_SQL.CoordMercator[][] array = default;
			if (!Information.IsNothing(HighwayGeometry))
			{
				array = new PHP_SQL.CoordMercator[HighwayGeometry.GetUpperBound(0) + 1][];
				int upperBound = HighwayGeometry.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					PHP_SQL.ConvertGeometryPolyline2Coord(HighwayGeometry[i], ref array[i]);
				}
			}
			string[] BarrierGeometry = default;
			string[] BarrierValue = default;
			PHP_SQL.GetBarrier(inputGeometry, ref BarrierGeometry, ref BarrierValue);
			PHP_SQL.CoordMercator[][] array2 = default;
			if (!Information.IsNothing(BarrierGeometry))
			{
				array2 = new PHP_SQL.CoordMercator[BarrierGeometry.GetUpperBound(0) + 1][];
				int upperBound2 = BarrierGeometry.GetUpperBound(0);
				for (int j = 0; j <= upperBound2; j++)
				{
					PHP_SQL.ConvertGeometryPolyline2Coord(BarrierGeometry[j], ref array2[j]);
				}
			}
			string[] LeisureGeometry = default;
			string[] LeisureValue = default;
			PHP_SQL.GetLeisure(inputGeometry, ref LeisureGeometry, ref LeisureValue);
			PHP_SQL.CoordMercator[][][] array3 = default;
			if (!Information.IsNothing(LeisureGeometry))
			{
				array3 = new PHP_SQL.CoordMercator[LeisureGeometry.GetUpperBound(0) + 1][][];
				int upperBound3 = LeisureGeometry.GetUpperBound(0);
				for (int k = 0; k <= upperBound3; k++)
				{
					PHP_SQL.ConvertGeometryPolygon2Coord(LeisureGeometry[k], ref array3[k]);
				}
			}
			string[] AmenityGeometry = default;
			string[] AmenityValue = default;
			PHP_SQL.GetAmenity(inputGeometry, ref AmenityGeometry, ref AmenityValue);
			PHP_SQL.CoordMercator[][][] array4 = default;
			if (!Information.IsNothing(AmenityGeometry))
			{
				array4 = new PHP_SQL.CoordMercator[AmenityGeometry.GetUpperBound(0) + 1][][];
				int upperBound4 = AmenityGeometry.GetUpperBound(0);
				for (int l = 0; l <= upperBound4; l++)
				{
					PHP_SQL.ConvertGeometryPolygon2Coord(AmenityGeometry[l], ref array4[l]);
				}
			}
			int num = 0;
			if (!Information.IsNothing(array2))
			{
				int upperBound5 = array2.GetUpperBound(0);
				for (int m = 0; m <= upperBound5; m++)
				{
					ref Public2.StructFence[] fENCE = ref Public2.PRJ.FENCE;
					fENCE = (Public2.StructFence[])Utils.CopyArray(fENCE, new Public2.StructFence[num + 1]);
					Public2.PRJ.FENCE[num].CoordWM = new Public2.CoordWebMerc[array2[m].GetUpperBound(0) + 1];
					Public2.PRJ.FENCE[num].Type = 8;
					Public2.PRJ.FENCE[num].Width = 1;
					int upperBound6 = array2[m].GetUpperBound(0);
					for (int n = 0; n <= upperBound6; n++)
					{
						Public2.PRJ.FENCE[num].CoordWM[n].Xwm = array2[m][n].X;
						Public2.PRJ.FENCE[num].CoordWM[n].Ywm = array2[m][n].Y;
					}
					num++;
				}
			}
			if (!Information.IsNothing(array3))
			{
				int upperBound7 = array3.GetUpperBound(0);
				for (int num2 = 0; num2 <= upperBound7; num2++)
				{
					ref Public2.StructFence[] fENCE2 = ref Public2.PRJ.FENCE;
					fENCE2 = (Public2.StructFence[])Utils.CopyArray(fENCE2, new Public2.StructFence[num + 1]);
					Public2.PRJ.FENCE[num].CoordWM = new Public2.CoordWebMerc[array3[num2][0].GetUpperBound(0) + 1];
					Public2.PRJ.FENCE[num].Type = 2;
					Public2.PRJ.FENCE[num].Width = 1;
					int upperBound8 = array3[num2][0].GetUpperBound(0);
					for (int num3 = 0; num3 <= upperBound8; num3++)
					{
						Public2.PRJ.FENCE[num].CoordWM[num3].Xwm = array3[num2][0][num3].X;
						Public2.PRJ.FENCE[num].CoordWM[num3].Ywm = array3[num2][0][num3].Y;
					}
					num++;
				}
			}
			if (!Information.IsNothing(array4))
			{
				int upperBound9 = array4.GetUpperBound(0);
				for (int num4 = 0; num4 <= upperBound9; num4++)
				{
					ref Public2.StructFence[] fENCE3 = ref Public2.PRJ.FENCE;
					fENCE3 = (Public2.StructFence[])Utils.CopyArray(fENCE3, new Public2.StructFence[num + 1]);
					Public2.PRJ.FENCE[num].CoordWM = new Public2.CoordWebMerc[array4[num4][0].GetUpperBound(0) + 1];
					Public2.PRJ.FENCE[num].Type = 2;
					Public2.PRJ.FENCE[num].Width = 1;
					int upperBound10 = array4[num4][0].GetUpperBound(0);
					for (int num5 = 0; num5 <= upperBound10; num5++)
					{
						Public2.PRJ.FENCE[num].CoordWM[num5].Xwm = array4[num4][0][num5].X;
						Public2.PRJ.FENCE[num].CoordWM[num5].Ywm = array4[num4][0][num5].Y;
					}
					num++;
				}
			}
			if (!Information.IsNothing(array))
			{
				Public2.PRJ.ROAD = new Public2.StructRoad[array.GetUpperBound(0) + 1];
				int upperBound11 = array.GetUpperBound(0);
				for (int num6 = 0; num6 <= upperBound11; num6++)
				{
					Public2.PRJ.ROAD[num6].CoordWM = new Public2.CoordWebMerc[array[num6].GetUpperBound(0) + 1];
					if (Operators.CompareString(HighwayValue[num6], "motorway", TextCompare: false) == 0)
					{
						Public2.PRJ.ROAD[num6].WidthM = 16;
					}
					else if (Operators.CompareString(HighwayValue[num6], "trunk", TextCompare: false) == 0)
					{
						Public2.PRJ.ROAD[num6].WidthM = 14;
					}
					else if (Operators.CompareString(HighwayValue[num6], "primary", TextCompare: false) == 0)
					{
						Public2.PRJ.ROAD[num6].WidthM = 12;
					}
					else if (Operators.CompareString(HighwayValue[num6], "secondary", TextCompare: false) == 0)
					{
						Public2.PRJ.ROAD[num6].WidthM = 10;
					}
					else if (Operators.CompareString(HighwayValue[num6], "tertiary", TextCompare: false) == 0)
					{
						Public2.PRJ.ROAD[num6].WidthM = 8;
					}
					else if (Operators.CompareString(HighwayValue[num6], "unclassified", TextCompare: false) == 0)
					{
						Public2.PRJ.ROAD[num6].WidthM = 6;
					}
					else if (Operators.CompareString(HighwayValue[num6], "residential", TextCompare: false) == 0)
					{
						Public2.PRJ.ROAD[num6].WidthM = 5;
					}
					else if (Operators.CompareString(HighwayValue[num6], "unclassified", TextCompare: false) == 0)
					{
						Public2.PRJ.ROAD[num6].WidthM = 4;
					}
					else if (Operators.CompareString(HighwayValue[num6], "service", TextCompare: false) == 0)
					{
						Public2.PRJ.ROAD[num6].WidthM = 3;
					}
					else if (Operators.CompareString(HighwayValue[num6], "footway", TextCompare: false) == 0)
					{
						Public2.PRJ.ROAD[num6].WidthM = 2;
					}
					else if (Operators.CompareString(HighwayValue[num6], "bridleway", TextCompare: false) == 0)
					{
						Public2.PRJ.ROAD[num6].WidthM = 2;
					}
					else if (Operators.CompareString(HighwayValue[num6], "steps", TextCompare: false) == 0)
					{
						Public2.PRJ.ROAD[num6].WidthM = 2;
					}
					else if (Operators.CompareString(HighwayValue[num6], "path", TextCompare: false) == 0)
					{
						Public2.PRJ.ROAD[num6].WidthM = 2;
					}
					else
					{
						Public2.PRJ.ROAD[num6].WidthM = 2;
					}
					Public2.PRJ.ROAD[num6].WidthBorder = 2;
					int upperBound12 = array[num6].GetUpperBound(0);
					for (int num7 = 0; num7 <= upperBound12; num7++)
					{
						Public2.PRJ.ROAD[num6].CoordWM[num7].Xwm = array[num6][num7].X;
						Public2.PRJ.ROAD[num6].CoordWM[num7].Ywm = array[num6][num7].Y;
					}
				}
			}
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void cmdAddRoad_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		cmdAddFence.BackColor = SystemColors.Control;
		cmdAddText.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		cmdSplitNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingFence = false;
		Form1_Public.StartDrawingText = false;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartAddingNodes = false;
		Form1_Public.StartSplittingNodes = false;
		MyProject.Forms.Form1.IndexRoad = -1;
		MyProject.Forms.Form1.IndexFence = -1;
		MyProject.Forms.Form1.IndexText = -1;
		Form1_Public.NewRoad = default;
		Form1_Public.NewFence = default;
		Form1_Public.NewText = default;
		if (Form1_Public.StartDrawingRoad)
		{
			cmdAddRoad.BackColor = SystemColors.Control;
			Form1_Public.StartDrawingRoad = false;
		}
		else
		{
			cmdAddRoad.BackColor = SystemColors.ControlDark;
			Form1_Public.StartDrawingRoad = true;
		}
		MyProject.Forms.Form1.PB.Refresh();
		gbRoad.Visible = false;
		gbFence.Visible = false;
		gbText.Visible = false;
	}

	private void cmdAddFence_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		cmdAddRoad.BackColor = SystemColors.Control;
		cmdAddText.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		cmdSplitNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingRoad = false;
		Form1_Public.StartDrawingText = false;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartAddingNodes = false;
		Form1_Public.StartSplittingNodes = false;
		MyProject.Forms.Form1.IndexRoad = -1;
		MyProject.Forms.Form1.IndexFence = -1;
		MyProject.Forms.Form1.IndexText = -1;
		Form1_Public.NewFence = default;
		Form1_Public.NewFence = default;
		Form1_Public.NewText = default;
		if (Form1_Public.StartDrawingFence)
		{
			cmdAddFence.BackColor = SystemColors.Control;
			Form1_Public.StartDrawingFence = false;
		}
		else
		{
			cmdAddFence.BackColor = SystemColors.ControlDark;
			Form1_Public.StartDrawingFence = true;
		}
		MyProject.Forms.Form1.PB.Refresh();
		gbRoad.Visible = false;
		gbFence.Visible = false;
		gbText.Visible = false;
	}

	private void cmdAddText_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		cmdAddFence.BackColor = SystemColors.Control;
		cmdAddRoad.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		cmdSplitNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingFence = false;
		Form1_Public.StartDrawingRoad = false;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartAddingNodes = false;
		Form1_Public.StartSplittingNodes = false;
		MyProject.Forms.Form1.IndexRoad = -1;
		MyProject.Forms.Form1.IndexFence = -1;
		MyProject.Forms.Form1.IndexText = -1;
		Form1_Public.NewRoad = default;
		Form1_Public.NewFence = default;
		Form1_Public.NewText = default;
		if (Form1_Public.StartDrawingText)
		{
			cmdAddText.BackColor = SystemColors.Control;
			Form1_Public.StartDrawingText = false;
		}
		else
		{
			cmdAddText.BackColor = SystemColors.ControlDark;
			Form1_Public.StartDrawingText = true;
		}
		MyProject.Forms.Form1.PB.Refresh();
		gbRoad.Visible = false;
		gbFence.Visible = false;
		gbText.Visible = false;
	}

	private void cmdAddCopyObject_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		cmdAddRoad.BackColor = SystemColors.Control;
		cmdAddFence.BackColor = SystemColors.Control;
		cmdAddText.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		cmdSplitNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingRoad = false;
		Form1_Public.StartDrawingFence = false;
		Form1_Public.StartDrawingText = false;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartAddingNodes = false;
		Form1_Public.StartSplittingNodes = false;
		checked
		{
			if (MyProject.Forms.Form1.IndexRoad > -1)
			{
				ref Public2.StructRoad[] rOAD = ref Public2.PRJ.ROAD;
				rOAD = (Public2.StructRoad[])Utils.CopyArray(rOAD, new Public2.StructRoad[Public2.PRJ.ROAD.GetUpperBound(0) + 1 + 1]);
				Public2.PRJ.ROAD[Public2.PRJ.ROAD.GetUpperBound(0)] = Module1.DeepClone(Public2.PRJ.ROAD[MyProject.Forms.Form1.IndexRoad]);
				int upperBound = Public2.PRJ.ROAD[Public2.PRJ.ROAD.GetUpperBound(0)].CoordWM.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					Public2.PRJ.ROAD[Public2.PRJ.ROAD.GetUpperBound(0)].CoordWM[i].Xwm += 20.0;
					Public2.PRJ.ROAD[Public2.PRJ.ROAD.GetUpperBound(0)].CoordWM[i].Ywm -= 20.0;
				}
				MyProject.Forms.Form1.IndexRoad = (short)Public2.PRJ.ROAD.GetUpperBound(0);
				MyProject.Forms.Form1.PB.Refresh();
				txtRoadWidth.Text = Conversions.ToString(Public2.PRJ.ROAD[MyProject.Forms.Form1.IndexRoad].WidthM);
				txtWidthBorder.Text = Conversions.ToString(Public2.PRJ.ROAD[MyProject.Forms.Form1.IndexRoad].WidthBorder);
				Module1.LineBackUp();
			}
			else if (MyProject.Forms.Form1.IndexFence > -1)
			{
				ref Public2.StructFence[] fENCE = ref Public2.PRJ.FENCE;
				fENCE = (Public2.StructFence[])Utils.CopyArray(fENCE, new Public2.StructFence[Public2.PRJ.FENCE.GetUpperBound(0) + 1 + 1]);
				Public2.PRJ.FENCE[Public2.PRJ.FENCE.GetUpperBound(0)] = Module1.DeepClone(Public2.PRJ.FENCE[MyProject.Forms.Form1.IndexFence]);
				int upperBound2 = Public2.PRJ.FENCE[Public2.PRJ.FENCE.GetUpperBound(0)].CoordWM.GetUpperBound(0);
				for (int j = 0; j <= upperBound2; j++)
				{
					Public2.PRJ.FENCE[Public2.PRJ.FENCE.GetUpperBound(0)].CoordWM[j].Xwm += 20.0;
					Public2.PRJ.FENCE[Public2.PRJ.FENCE.GetUpperBound(0)].CoordWM[j].Ywm -= 20.0;
				}
				MyProject.Forms.Form1.IndexFence = (short)Public2.PRJ.FENCE.GetUpperBound(0);
				MyProject.Forms.Form1.PB.Refresh();
				txtFenceWidth.Text = Conversions.ToString(Public2.PRJ.FENCE[MyProject.Forms.Form1.IndexFence].Width);
				pbFenceLineType.Image = ilICO.Images[Public2.PRJ.FENCE[MyProject.Forms.Form1.IndexFence].Type];
				Module1.LineBackUp();
			}
			else if (MyProject.Forms.Form1.IndexText > -1)
			{
				ref Public2.StructText[] tXT = ref Public2.PRJ.TXT;
				tXT = (Public2.StructText[])Utils.CopyArray(tXT, new Public2.StructText[Public2.PRJ.TXT.GetUpperBound(0) + 1 + 1]);
				Public2.PRJ.TXT[Public2.PRJ.TXT.GetUpperBound(0)] = Module1.DeepClone(Public2.PRJ.TXT[MyProject.Forms.Form1.IndexText]);
				Public2.PRJ.TXT[Public2.PRJ.TXT.GetUpperBound(0)].CoordWM.Xwm += 20.0;
				Public2.PRJ.TXT[Public2.PRJ.TXT.GetUpperBound(0)].CoordWM.Ywm -= 20.0;
				MyProject.Forms.Form1.IndexText = (short)Public2.PRJ.TXT.GetUpperBound(0);
				MyProject.Forms.Form1.PB.Refresh();
				txtText.Text = Public2.PRJ.TXT[Public2.PRJ.TXT.GetUpperBound(0)].Text;
				cbFont.Text = Public2.PRJ.TXT[Public2.PRJ.TXT.GetUpperBound(0)].FontName;
				txtFontSize.Value = new decimal(Public2.PRJ.TXT[Public2.PRJ.TXT.GetUpperBound(0)].Size);
				chkShowPoint.Checked = Public2.PRJ.TXT[Public2.PRJ.TXT.GetUpperBound(0)].Point;
				tbAngle.Value = Public2.PRJ.TXT[Public2.PRJ.TXT.GetUpperBound(0)].Angle;
				Module1.LineBackUp();
			}
			else
			{
				Interaction.MsgBox(Localization.LocMSG(33), MsgBoxStyle.Information);
			}
		}
	}

	private void cmdDelOneObject_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		checked
		{
			if ((MyProject.Forms.Form1.IndexRoad < 0) & (MyProject.Forms.Form1.IndexFence < 0) & (MyProject.Forms.Form1.IndexText < 0))
			{
				Interaction.MsgBox(Localization.LocMSG(33), MsgBoxStyle.Information);
			}
			else
			{
				if (Interaction.MsgBox(Localization.LocMSG(36), MsgBoxStyle.YesNo) != MsgBoxResult.Yes)
				{
					return;
				}
				cmdAddRoad.BackColor = SystemColors.Control;
				cmdAddFence.BackColor = SystemColors.Control;
				cmdAddText.BackColor = SystemColors.Control;
				cmdDeleteNode.BackColor = SystemColors.Control;
				cmdAddNode.BackColor = SystemColors.Control;
				cmdSplitNode.BackColor = SystemColors.Control;
				Form1_Public.StartDrawingRoad = false;
				Form1_Public.StartDrawingFence = false;
				Form1_Public.StartDrawingText = false;
				Form1_Public.StartDeletingNodes = false;
				Form1_Public.StartAddingNodes = false;
				Form1_Public.StartSplittingNodes = false;
				gbRoad.Visible = false;
				gbFence.Visible = false;
				gbText.Visible = false;
				if (MyProject.Forms.Form1.IndexRoad > -1)
				{
					short indexRoad = MyProject.Forms.Form1.IndexRoad;
					int num = Public2.PRJ.ROAD.GetUpperBound(0) - 1;
					for (int i = indexRoad; i <= num; i++)
					{
						Public2.PRJ.ROAD[i] = Module1.DeepClone(Public2.PRJ.ROAD[i + 1]);
					}
					ref Public2.StructRoad[] rOAD = ref Public2.PRJ.ROAD;
					rOAD = (Public2.StructRoad[])Utils.CopyArray(rOAD, new Public2.StructRoad[Public2.PRJ.ROAD.GetUpperBound(0) - 1 + 1]);
					if (Public2.PRJ.ROAD.Length == 0)
					{
						Public2.PRJ.ROAD = null;
					}
				}
				else if (MyProject.Forms.Form1.IndexFence > -1)
				{
					short indexFence = MyProject.Forms.Form1.IndexFence;
					int num2 = Public2.PRJ.FENCE.GetUpperBound(0) - 1;
					for (int j = indexFence; j <= num2; j++)
					{
						Public2.PRJ.FENCE[j] = Module1.DeepClone(Public2.PRJ.FENCE[j + 1]);
					}
					ref Public2.StructFence[] fENCE = ref Public2.PRJ.FENCE;
					fENCE = (Public2.StructFence[])Utils.CopyArray(fENCE, new Public2.StructFence[Public2.PRJ.FENCE.GetUpperBound(0) - 1 + 1]);
					if (Public2.PRJ.FENCE.Length == 0)
					{
						Public2.PRJ.FENCE = null;
					}
				}
				else if (MyProject.Forms.Form1.IndexText > -1)
				{
					short indexText = MyProject.Forms.Form1.IndexText;
					int num3 = Public2.PRJ.TXT.GetUpperBound(0) - 1;
					for (int k = indexText; k <= num3; k++)
					{
						Public2.PRJ.TXT[k] = Module1.DeepClone(Public2.PRJ.TXT[k + 1]);
					}
					ref Public2.StructText[] tXT = ref Public2.PRJ.TXT;
					tXT = (Public2.StructText[])Utils.CopyArray(tXT, new Public2.StructText[Public2.PRJ.TXT.GetUpperBound(0) - 1 + 1]);
					if (Public2.PRJ.TXT.Length == 0)
					{
						Public2.PRJ.TXT = null;
					}
				}
				MyProject.Forms.Form1.IndexRoad = -1;
				MyProject.Forms.Form1.IndexFence = -1;
				MyProject.Forms.Form1.IndexText = -1;
				MyProject.Forms.Form1.PB.Refresh();
			}
		}
	}

	private void cmdDelAllObjects_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Interaction.MsgBox(Localization.LocMSG(35), MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
		{
			cmdAddRoad.BackColor = SystemColors.Control;
			cmdAddFence.BackColor = SystemColors.Control;
			cmdAddText.BackColor = SystemColors.Control;
			cmdDeleteNode.BackColor = SystemColors.Control;
			cmdAddNode.BackColor = SystemColors.Control;
			cmdSplitNode.BackColor = SystemColors.Control;
			Form1_Public.StartDrawingRoad = false;
			Form1_Public.StartDrawingFence = false;
			Form1_Public.StartDrawingText = false;
			Form1_Public.StartDeletingNodes = false;
			Form1_Public.StartAddingNodes = false;
			Form1_Public.StartSplittingNodes = false;
			MyProject.Forms.Form1.IndexRoad = -1;
			MyProject.Forms.Form1.IndexFence = -1;
			MyProject.Forms.Form1.IndexText = -1;
			gbRoad.Visible = false;
			gbFence.Visible = false;
			gbText.Visible = false;
			Module1.LineBackUp();
			Public2.PRJ.ROAD = null;
			Public2.PRJ.FENCE = null;
			Public2.PRJ.TXT = null;
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void cmdUndo_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		cmdAddRoad.BackColor = SystemColors.Control;
		cmdAddFence.BackColor = SystemColors.Control;
		cmdAddText.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		cmdSplitNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingRoad = false;
		Form1_Public.StartDrawingFence = false;
		Form1_Public.StartDrawingText = false;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartAddingNodes = false;
		Form1_Public.StartSplittingNodes = false;
		MyProject.Forms.Form1.IndexRoad = -1;
		MyProject.Forms.Form1.IndexFence = -1;
		MyProject.Forms.Form1.IndexText = -1;
		MyProject.Forms.Form1.PB.Refresh();
		gbRoad.Visible = false;
		gbFence.Visible = false;
		gbText.Visible = false;
		Module1.LineRestore();
		MyProject.Forms.Form1.PB.Refresh();
	}

	private void cmdRedo_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		cmdAddRoad.BackColor = SystemColors.Control;
		cmdAddFence.BackColor = SystemColors.Control;
		cmdAddText.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		cmdSplitNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingRoad = false;
		Form1_Public.StartDrawingFence = false;
		Form1_Public.StartDrawingText = false;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartAddingNodes = false;
		Form1_Public.StartSplittingNodes = false;
		MyProject.Forms.Form1.IndexRoad = -1;
		MyProject.Forms.Form1.IndexFence = -1;
		MyProject.Forms.Form1.IndexText = -1;
		MyProject.Forms.Form1.PB.Refresh();
		gbRoad.Visible = false;
		gbFence.Visible = false;
		gbText.Visible = false;
		Module1.LineForward();
		MyProject.Forms.Form1.PB.Refresh();
	}

	private void cmdDeleteNode_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		if ((MyProject.Forms.Form1.IndexRoad < 0) & (MyProject.Forms.Form1.IndexFence < 0))
		{
			Interaction.MsgBox(Localization.LocMSG(33), MsgBoxStyle.Information);
			return;
		}
		cmdAddRoad.BackColor = SystemColors.Control;
		cmdAddFence.BackColor = SystemColors.Control;
		cmdAddText.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		cmdSplitNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingRoad = false;
		Form1_Public.StartDrawingFence = false;
		Form1_Public.StartDrawingText = false;
		Form1_Public.StartAddingNodes = false;
		Form1_Public.StartSplittingNodes = false;
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
		if ((MyProject.Forms.Form1.IndexRoad < 0) & (MyProject.Forms.Form1.IndexFence < 0))
		{
			Interaction.MsgBox(Localization.LocMSG(33), MsgBoxStyle.Information);
			return;
		}
		cmdAddRoad.BackColor = SystemColors.Control;
		cmdAddFence.BackColor = SystemColors.Control;
		cmdAddText.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		cmdSplitNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingRoad = false;
		Form1_Public.StartDrawingFence = false;
		Form1_Public.StartDrawingText = false;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartSplittingNodes = false;
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

	private void cmdSplitNode_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		if ((MyProject.Forms.Form1.IndexRoad < 0) & (MyProject.Forms.Form1.IndexFence < 0))
		{
			Interaction.MsgBox(Localization.LocMSG(33), MsgBoxStyle.Information);
			return;
		}
		cmdAddRoad.BackColor = SystemColors.Control;
		cmdAddFence.BackColor = SystemColors.Control;
		cmdAddText.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingRoad = false;
		Form1_Public.StartDrawingFence = false;
		Form1_Public.StartDrawingText = false;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartAddingNodes = false;
		if (Form1_Public.StartSplittingNodes)
		{
			cmdSplitNode.BackColor = SystemColors.Control;
			Form1_Public.StartSplittingNodes = false;
		}
		else
		{
			cmdSplitNode.BackColor = SystemColors.ControlDark;
			Form1_Public.StartSplittingNodes = true;
		}
		MyProject.Forms.Form1.PB.Refresh();
	}

	private void cmdLeft_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		cmdAddRoad.BackColor = SystemColors.Control;
		cmdAddFence.BackColor = SystemColors.Control;
		cmdAddText.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		cmdSplitNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingRoad = false;
		Form1_Public.StartDrawingFence = false;
		Form1_Public.StartDrawingText = false;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartAddingNodes = false;
		Form1_Public.StartSplittingNodes = false;
		double num = 1.0 / CalcCoords.CalcMashtWMtoPixel();
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.ROAD))
			{
				int upperBound = Public2.PRJ.ROAD.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					int upperBound2 = Public2.PRJ.ROAD[i].CoordWM.GetUpperBound(0);
					for (int j = 0; j <= upperBound2; j++)
					{
						Public2.PRJ.ROAD[i].CoordWM[j].Xwm = Public2.PRJ.ROAD[i].CoordWM[j].Xwm - num;
					}
				}
			}
			if (!Information.IsNothing(Public2.PRJ.FENCE))
			{
				int upperBound3 = Public2.PRJ.FENCE.GetUpperBound(0);
				for (int k = 0; k <= upperBound3; k++)
				{
					int upperBound4 = Public2.PRJ.FENCE[k].CoordWM.GetUpperBound(0);
					for (int l = 0; l <= upperBound4; l++)
					{
						Public2.PRJ.FENCE[k].CoordWM[l].Xwm = Public2.PRJ.FENCE[k].CoordWM[l].Xwm - num;
					}
				}
			}
			if (!Information.IsNothing(Public2.PRJ.TXT))
			{
				int upperBound5 = Public2.PRJ.TXT.GetUpperBound(0);
				for (int m = 0; m <= upperBound5; m++)
				{
					Public2.PRJ.TXT[m].CoordWM.Xwm = Public2.PRJ.TXT[m].CoordWM.Xwm - num;
				}
			}
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void cmdRight_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		cmdAddRoad.BackColor = SystemColors.Control;
		cmdAddFence.BackColor = SystemColors.Control;
		cmdAddText.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		cmdSplitNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingRoad = false;
		Form1_Public.StartDrawingFence = false;
		Form1_Public.StartDrawingText = false;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartAddingNodes = false;
		Form1_Public.StartSplittingNodes = false;
		double num = 1.0 / CalcCoords.CalcMashtWMtoPixel();
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.ROAD))
			{
				int upperBound = Public2.PRJ.ROAD.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					int upperBound2 = Public2.PRJ.ROAD[i].CoordWM.GetUpperBound(0);
					for (int j = 0; j <= upperBound2; j++)
					{
						Public2.PRJ.ROAD[i].CoordWM[j].Xwm = Public2.PRJ.ROAD[i].CoordWM[j].Xwm + num;
					}
				}
			}
			if (!Information.IsNothing(Public2.PRJ.FENCE))
			{
				int upperBound3 = Public2.PRJ.FENCE.GetUpperBound(0);
				for (int k = 0; k <= upperBound3; k++)
				{
					int upperBound4 = Public2.PRJ.FENCE[k].CoordWM.GetUpperBound(0);
					for (int l = 0; l <= upperBound4; l++)
					{
						Public2.PRJ.FENCE[k].CoordWM[l].Xwm = Public2.PRJ.FENCE[k].CoordWM[l].Xwm + num;
					}
				}
			}
			if (!Information.IsNothing(Public2.PRJ.TXT))
			{
				int upperBound5 = Public2.PRJ.TXT.GetUpperBound(0);
				for (int m = 0; m <= upperBound5; m++)
				{
					Public2.PRJ.TXT[m].CoordWM.Xwm = Public2.PRJ.TXT[m].CoordWM.Xwm + num;
				}
			}
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void cmdUp_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		cmdAddRoad.BackColor = SystemColors.Control;
		cmdAddFence.BackColor = SystemColors.Control;
		cmdAddText.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		cmdSplitNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingRoad = false;
		Form1_Public.StartDrawingFence = false;
		Form1_Public.StartDrawingText = false;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartAddingNodes = false;
		Form1_Public.StartSplittingNodes = false;
		double num = 1.0 / CalcCoords.CalcMashtWMtoPixel();
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.ROAD))
			{
				int upperBound = Public2.PRJ.ROAD.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					int upperBound2 = Public2.PRJ.ROAD[i].CoordWM.GetUpperBound(0);
					for (int j = 0; j <= upperBound2; j++)
					{
						Public2.PRJ.ROAD[i].CoordWM[j].Ywm = Public2.PRJ.ROAD[i].CoordWM[j].Ywm + num;
					}
				}
			}
			if (!Information.IsNothing(Public2.PRJ.FENCE))
			{
				int upperBound3 = Public2.PRJ.FENCE.GetUpperBound(0);
				for (int k = 0; k <= upperBound3; k++)
				{
					int upperBound4 = Public2.PRJ.FENCE[k].CoordWM.GetUpperBound(0);
					for (int l = 0; l <= upperBound4; l++)
					{
						Public2.PRJ.FENCE[k].CoordWM[l].Ywm = Public2.PRJ.FENCE[k].CoordWM[l].Ywm + num;
					}
				}
			}
			if (!Information.IsNothing(Public2.PRJ.TXT))
			{
				int upperBound5 = Public2.PRJ.TXT.GetUpperBound(0);
				for (int m = 0; m <= upperBound5; m++)
				{
					Public2.PRJ.TXT[m].CoordWM.Ywm = Public2.PRJ.TXT[m].CoordWM.Ywm + num;
				}
			}
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void cmdDown_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		cmdAddRoad.BackColor = SystemColors.Control;
		cmdAddFence.BackColor = SystemColors.Control;
		cmdAddText.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		cmdSplitNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingRoad = false;
		Form1_Public.StartDrawingFence = false;
		Form1_Public.StartDrawingText = false;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartAddingNodes = false;
		Form1_Public.StartSplittingNodes = false;
		double num = 1.0 / CalcCoords.CalcMashtWMtoPixel();
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.ROAD))
			{
				int upperBound = Public2.PRJ.ROAD.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					int upperBound2 = Public2.PRJ.ROAD[i].CoordWM.GetUpperBound(0);
					for (int j = 0; j <= upperBound2; j++)
					{
						Public2.PRJ.ROAD[i].CoordWM[j].Ywm = Public2.PRJ.ROAD[i].CoordWM[j].Ywm - num;
					}
				}
			}
			if (!Information.IsNothing(Public2.PRJ.FENCE))
			{
				int upperBound3 = Public2.PRJ.FENCE.GetUpperBound(0);
				for (int k = 0; k <= upperBound3; k++)
				{
					int upperBound4 = Public2.PRJ.FENCE[k].CoordWM.GetUpperBound(0);
					for (int l = 0; l <= upperBound4; l++)
					{
						Public2.PRJ.FENCE[k].CoordWM[l].Ywm = Public2.PRJ.FENCE[k].CoordWM[l].Ywm - num;
					}
				}
			}
			if (!Information.IsNothing(Public2.PRJ.TXT))
			{
				int upperBound5 = Public2.PRJ.TXT.GetUpperBound(0);
				for (int m = 0; m <= upperBound5; m++)
				{
					Public2.PRJ.TXT[m].CoordWM.Ywm = Public2.PRJ.TXT[m].CoordWM.Ywm - num;
				}
			}
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void pbFenceLineType_MouseClick(object sender, MouseEventArgs e)
	{
		MyProject.Forms.frmLineTypeSelect.Location = new Point((Size)Control.MousePosition);
		MyProject.Forms.frmLineTypeSelect.ShowDialog();
		if (Form1_Public.LineTypeIndex >= 0)
		{
			pbFenceLineType.Image = ilICO.Images[Form1_Public.LineTypeIndex];
			Public2.PRJ.FENCE[MyProject.Forms.Form1.IndexFence].Type = checked((byte)Form1_Public.LineTypeIndex);
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void txtFenceWidth_MouseUp(object sender, MouseEventArgs e)
	{
		Public2.PRJ.FENCE[MyProject.Forms.Form1.IndexFence].Width = Convert.ToByte(txtFenceWidth.Value);
		MyProject.Forms.Form1.PB.Refresh();
	}

	private void txtRoadWidth_MouseUp(object sender, MouseEventArgs e)
	{
		Public2.PRJ.ROAD[MyProject.Forms.Form1.IndexRoad].WidthM = Convert.ToByte(txtRoadWidth.Value);
		MyProject.Forms.Form1.PB.Refresh();
	}

	private void txtWidthBorder_MouseUp(object sender, MouseEventArgs e)
	{
		Public2.PRJ.ROAD[MyProject.Forms.Form1.IndexRoad].WidthBorder = Convert.ToByte(txtWidthBorder.Value);
		MyProject.Forms.Form1.PB.Refresh();
	}

	private void txtText_KeyUp(object sender, KeyEventArgs e)
	{
		try
		{
			Public2.PRJ.TXT[MyProject.Forms.Form1.IndexText].Text = txtText.Text;
			Form1_Public.LastText = txtText.Text;
			MyProject.Forms.Form1.PB.Refresh();
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
	}

	private void cbFont_SelectionChangeCommitted(object sender, EventArgs e)
	{
		Public2.PRJ.TXT[MyProject.Forms.Form1.IndexText].FontName = cbFont.Text;
		MyProject.Forms.Form1.PB.Refresh();
	}

	private void txtFontSize_MouseUp(object sender, MouseEventArgs e)
	{
		Public2.PRJ.TXT[MyProject.Forms.Form1.IndexText].Size = Convert.ToByte(txtFontSize.Value);
		MyProject.Forms.Form1.PB.Refresh();
	}

	private void chkShowPoint_MouseClick(object sender, MouseEventArgs e)
	{
		Public2.PRJ.TXT[MyProject.Forms.Form1.IndexText].Point = chkShowPoint.Checked;
		MyProject.Forms.Form1.PB.Refresh();
	}

	private void tbAngle_ValueChanged(object sender, EventArgs e)
	{
		Public2.PRJ.TXT[MyProject.Forms.Form1.IndexText].Angle = checked((short)tbAngle.Value);
		MyProject.Forms.Form1.PB.Refresh();
	}

	private void cmdIcon_Click(object sender, EventArgs e)
	{
		MyProject.Forms.frmIcoSelect.Location = Control.MousePosition;
		MyProject.Forms.frmIcoSelect.ShowDialog();
		if (!Information.IsNothing(Form1_Public.IconImage))
		{
			cmdIcon.Image = Form1_Public.IconImage;
			Public2.PRJ.TXT[MyProject.Forms.Form1.IndexText].Icon = (Bitmap)Form1_Public.IconImage;
			Form1_Public.LastIcon = Form1_Public.IconImage;
			MyProject.Forms.Form1.PB.Refresh();
		}
	}
}
