using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using EMFPlanner.My;
using EMFPlanner.My.Resources;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[DesignerGenerated]
public class UC_VertPlanes : UserControl
{
	public struct VN
	{
		public string Name;

		public string Number;
	}

	private IContainer components;

	private bool FlagProcessing;

	private bool FlagOK;

	private short MarginUP;

	private short MarginDOWN;

	private short MarginLEFT;

	private short MarginRIGHT;

	private Pen PenBlack2;

	private Pen PenRed1;

	private Font Font8;

	private SolidBrush drawBrushBlack;

	private SizeF stringSize;

	private Public2.StructMesurePoint MovePoint;

	private Public2.StructDimension MoveDimension;

	[field: AccessedThroughProperty("GroupBox1")]
	internal virtual GroupBox GroupBox1
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
			DataGridViewCellEventHandler value2 = DGV1_CellEnter;
			DataGridViewCellMouseEventHandler value3 = DGV1_CellMouseDown;
			DataGridViewCellMouseEventHandler value4 = DGV1_CellMouseClick;
			DataGridViewCellEventHandler value5 = DGV1_CellValueChanged;
			DataGridViewRowEventHandler value6 = DGV1_UserAddedRow;
			KeyEventHandler value7 = DGV1_KeyDown;
			DataGridView dataGridView = field;
			if (dataGridView != null)
			{
				dataGridView.CellEnter -= value2;
				dataGridView.CellMouseDown -= value3;
				dataGridView.CellMouseClick -= value4;
				dataGridView.CellValueChanged -= value5;
				dataGridView.UserAddedRow -= value6;
				dataGridView.KeyDown -= value7;
			}
			field = value;
			dataGridView = field;
			if (dataGridView != null)
			{
				dataGridView.CellEnter += value2;
				dataGridView.CellMouseDown += value3;
				dataGridView.CellMouseClick += value4;
				dataGridView.CellValueChanged += value5;
				dataGridView.UserAddedRow += value6;
				dataGridView.KeyDown += value7;
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

	[field: AccessedThroughProperty("GroupBox4")]
	internal virtual GroupBox GroupBox4
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("chkHgtMaxZO")]
	internal virtual CheckBox chkHgtMaxZO
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkHgtMaxZO_MouseClick;
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

	[field: AccessedThroughProperty("chkHgtBottomZO")]
	internal virtual CheckBox chkHgtBottomZO
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkHgtBottomZO_MouseClick;
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

	[field: AccessedThroughProperty("chkLengthZO")]
	internal virtual CheckBox chkLengthZO
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkLengthZO_MouseClick;
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

	[field: AccessedThroughProperty("SplitContainer1")]
	internal virtual SplitContainer SplitContainer1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("GroupBox5")]
	internal virtual GroupBox GroupBox5
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
			MouseEventHandler value3 = PB_MouseMove;
			MouseEventHandler value4 = PB_MouseClick;
			EventHandler value5 = PB_MouseLeave;
			EventHandler value6 = PB_Resize;
			PictureBox pictureBox = field;
			if (pictureBox != null)
			{
				pictureBox.Paint -= value2;
				pictureBox.MouseMove -= value3;
				pictureBox.MouseClick -= value4;
				pictureBox.MouseLeave -= value5;
				pictureBox.Resize -= value6;
			}
			field = value;
			pictureBox = field;
			if (pictureBox != null)
			{
				pictureBox.Paint += value2;
				pictureBox.MouseMove += value3;
				pictureBox.MouseClick += value4;
				pictureBox.MouseLeave += value5;
				pictureBox.Resize += value6;
			}
		}
	}

	[field: AccessedThroughProperty("PictureBox2")]
	internal virtual PictureBox PictureBox2
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

	[field: AccessedThroughProperty("cmdAdjustLeft")]
	internal virtual Button cmdAdjustLeft
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdAdjustLeft_Click;
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

	[field: AccessedThroughProperty("chkLen")]
	internal virtual CheckBox chkLen
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ContMesure")]
	internal virtual ContextMenuStrip ContMesure
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			ToolStripItemClickedEventHandler value2 = ContMesure_ItemClicked;
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

	[field: AccessedThroughProperty("cmdAddLine")]
	internal virtual ToolStripMenuItem cmdAddLine
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdDelLine")]
	internal virtual ToolStripMenuItem cmdDelLine
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdDelAllPoints")]
	internal virtual ToolStripMenuItem cmdDelAllPoints
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdClearAllMesurePoints")]
	internal virtual Button cmdClearAllMesurePoints
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdClearAllMesurePoints_Click;
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

	[field: AccessedThroughProperty("cmdStartOne1")]
	internal virtual Button cmdStartOne1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdStartOne1_Click;
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

	[field: AccessedThroughProperty("cmdStartOne0")]
	internal virtual Button cmdStartOne0
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdStartOne0_Click;
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

	[field: AccessedThroughProperty("chkHgtTopZO")]
	internal virtual CheckBox chkHgtTopZO
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkHgtTopZO_MouseClick;
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

	[field: AccessedThroughProperty("cmdAdjustUp")]
	internal virtual Button cmdAdjustUp
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdAdjustUp_Click;
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

	[field: AccessedThroughProperty("chkShowManualPointsZO")]
	internal virtual CheckBox chkShowManualPointsZO
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkShowManualPointsZO_MouseClick;
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

	[field: AccessedThroughProperty("GroupBox2")]
	internal virtual GroupBox GroupBox2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("chkAddOwner")]
	internal virtual CheckBox chkAddOwner
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

	[field: AccessedThroughProperty("cmdVerticalCuts_1")]
	internal virtual ToolStripMenuItem cmdVerticalCuts_1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdVerticalCuts_1_Click;
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

	[field: AccessedThroughProperty("chkShowPlus2m")]
	internal virtual CheckBox chkShowPlus2m
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkShowPlus2m_MouseClick;
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

	[field: AccessedThroughProperty("chkShowKPonVert")]
	internal virtual CheckBox chkShowKPonVert
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkchkShowKPonVert_MouseClick;
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

	[field: AccessedThroughProperty("chkUseAntName")]
	internal virtual CheckBox chkUseAntName
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
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

	[field: AccessedThroughProperty("cmdAddPoint")]
	internal virtual ToolStripMenuItem cmdAddPoint
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ToolStripSeparator1")]
	internal virtual ToolStripSeparator ToolStripSeparator1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ToolStripSeparator2")]
	internal virtual ToolStripSeparator ToolStripSeparator2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdDelPoint")]
	internal virtual ToolStripMenuItem cmdDelPoint
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdDelAllLines")]
	internal virtual ToolStripMenuItem cmdDelAllLines
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ToolStripSeparator3")]
	internal virtual ToolStripSeparator ToolStripSeparator3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdAddControlPoint")]
	internal virtual ToolStripMenuItem cmdAddControlPoint
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("chkShowMinDistToZO")]
	internal virtual CheckBox chkShowMinDistToZO
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkShowMinDistToZO_MouseClick;
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

	[field: AccessedThroughProperty("Column1")]
	internal virtual DataGridViewCheckBoxColumn Column1
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

	[field: AccessedThroughProperty("Column8")]
	internal virtual DataGridViewTextBoxColumn Column8
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Column9")]
	internal virtual DataGridViewTextBoxColumn Column9
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

	[field: AccessedThroughProperty("Column5")]
	internal virtual DataGridViewTextBoxColumn Column5
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("GroupBox6")]
	internal virtual GroupBox GroupBox6
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

	[field: AccessedThroughProperty("cmdAddRangeAndStep")]
	internal virtual Button cmdAddRangeAndStep
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdAddRangeAndStep_Click;
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

	[field: AccessedThroughProperty("txtLineHightAboveTerrain")]
	internal virtual NumericUpDown txtLineHightAboveTerrain
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = txtLineHightAboveTerrain_ValueChanged;
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

	[field: AccessedThroughProperty("GroupBox7")]
	internal virtual GroupBox GroupBox7
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtLineWidthVertCut")]
	internal virtual NumericUpDown txtLineWidthVertCut
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = txtLineWidthVertCut_ValueChanged;
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

	[field: AccessedThroughProperty("Label2")]
	internal virtual Label Label2
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

	[field: AccessedThroughProperty("txtTransparFillVertCut")]
	internal virtual NumericUpDown txtTransparFillVertCut
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = txtTransparFillVertCut_ValueChanged;
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

	public UC_VertPlanes()
	{
		Load += UC_VertPlanes_Load;
		FlagProcessing = true;
		MarginUP = 25;
		MarginDOWN = 20;
		MarginLEFT = 30;
		MarginRIGHT = 35;
		PenBlack2 = new Pen(Color.Black, 2f);
		PenRed1 = new Pen(Color.Red, 1f);
		Font8 = new Font("Arial", 8f, FontStyle.Regular);
		drawBrushBlack = new SolidBrush(Color.Black);
		stringSize = default;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EMFPlanner.UC_VertPlanes));
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
		this.GroupBox1 = new System.Windows.Forms.GroupBox();
		this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
		this.GroupBox7 = new System.Windows.Forms.GroupBox();
		this.txtLineWidthVertCut = new System.Windows.Forms.NumericUpDown();
		this.Label2 = new System.Windows.Forms.Label();
		this.Label6 = new System.Windows.Forms.Label();
		this.txtTransparFillVertCut = new System.Windows.Forms.NumericUpDown();
		this.cmdAddRangeAndStep = new System.Windows.Forms.Button();
		this.GroupBox6 = new System.Windows.Forms.GroupBox();
		this.chkLimit3 = new System.Windows.Forms.CheckBox();
		this.chkLimit2 = new System.Windows.Forms.CheckBox();
		this.chkLimit1 = new System.Windows.Forms.CheckBox();
		this.chkLimit0 = new System.Windows.Forms.CheckBox();
		this.cmdAutoFill3 = new System.Windows.Forms.Button();
		this.cmdWord = new System.Windows.Forms.Button();
		this.GroupBox2 = new System.Windows.Forms.GroupBox();
		this.Label1 = new System.Windows.Forms.Label();
		this.txtLineHightAboveTerrain = new System.Windows.Forms.NumericUpDown();
		this.chkUseAntName = new System.Windows.Forms.CheckBox();
		this.chkShowPlus2m = new System.Windows.Forms.CheckBox();
		this.chkAddOwner = new System.Windows.Forms.CheckBox();
		this.cmdAutoFill2 = new System.Windows.Forms.Button();
		this.cmdAdjustUp = new System.Windows.Forms.Button();
		this.cmdStartOne1 = new System.Windows.Forms.Button();
		this.cmdStartOne0 = new System.Windows.Forms.Button();
		this.cmdClearAllMesurePoints = new System.Windows.Forms.Button();
		this.cmdAdjustLeft = new System.Windows.Forms.Button();
		this.cmdStart1 = new System.Windows.Forms.Button();
		this.cmdStart0 = new System.Windows.Forms.Button();
		this.PictureBox2 = new System.Windows.Forms.PictureBox();
		this.GroupBox3 = new System.Windows.Forms.GroupBox();
		this.DGV1 = new System.Windows.Forms.DataGridView();
		this.Column1 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Button1 = new System.Windows.Forms.Button();
		this.GroupBox4 = new System.Windows.Forms.GroupBox();
		this.chkShowMinDistToZO = new System.Windows.Forms.CheckBox();
		this.chkShowKPonVert = new System.Windows.Forms.CheckBox();
		this.chkShowManualPointsZO = new System.Windows.Forms.CheckBox();
		this.chkHgtTopZO = new System.Windows.Forms.CheckBox();
		this.chkHgtMaxZO = new System.Windows.Forms.CheckBox();
		this.chkHgtBottomZO = new System.Windows.Forms.CheckBox();
		this.chkLengthZO = new System.Windows.Forms.CheckBox();
		this.cmdAutoFill1 = new System.Windows.Forms.Button();
		this.cmdSORT = new System.Windows.Forms.Button();
		this.GroupBox5 = new System.Windows.Forms.GroupBox();
		this.PB = new System.Windows.Forms.PictureBox();
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.chkLen = new System.Windows.Forms.CheckBox();
		this.ContMesure = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.cmdAddPoint = new System.Windows.Forms.ToolStripMenuItem();
		this.cmdAddLine = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
		this.cmdDelPoint = new System.Windows.Forms.ToolStripMenuItem();
		this.cmdDelLine = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
		this.cmdDelAllPoints = new System.Windows.Forms.ToolStripMenuItem();
		this.cmdDelAllLines = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
		this.cmdAddControlPoint = new System.Windows.Forms.ToolStripMenuItem();
		this.ContWord = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.cmdVerticalCuts_1 = new System.Windows.Forms.ToolStripMenuItem();
		this.GroupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.SplitContainer1).BeginInit();
		this.SplitContainer1.Panel1.SuspendLayout();
		this.SplitContainer1.Panel2.SuspendLayout();
		this.SplitContainer1.SuspendLayout();
		this.GroupBox7.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtLineWidthVertCut).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtTransparFillVertCut).BeginInit();
		this.GroupBox6.SuspendLayout();
		this.GroupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtLineHightAboveTerrain).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.PictureBox2).BeginInit();
		this.GroupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.DGV1).BeginInit();
		this.GroupBox4.SuspendLayout();
		this.GroupBox5.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.PB).BeginInit();
		this.ContMesure.SuspendLayout();
		this.ContWord.SuspendLayout();
		base.SuspendLayout();
		this.GroupBox1.Controls.Add(this.SplitContainer1);
		this.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GroupBox1.Location = new System.Drawing.Point(0, 0);
		this.GroupBox1.Name = "GroupBox1";
		this.GroupBox1.Size = new System.Drawing.Size(1600, 808);
		this.GroupBox1.TabIndex = 1;
		this.GroupBox1.TabStop = false;
		this.GroupBox1.Text = "Vertical cuts";
		this.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
		this.SplitContainer1.Location = new System.Drawing.Point(3, 16);
		this.SplitContainer1.Name = "SplitContainer1";
		this.SplitContainer1.Panel1.Controls.Add(this.GroupBox7);
		this.SplitContainer1.Panel1.Controls.Add(this.cmdAddRangeAndStep);
		this.SplitContainer1.Panel1.Controls.Add(this.GroupBox6);
		this.SplitContainer1.Panel1.Controls.Add(this.cmdAutoFill3);
		this.SplitContainer1.Panel1.Controls.Add(this.cmdWord);
		this.SplitContainer1.Panel1.Controls.Add(this.GroupBox2);
		this.SplitContainer1.Panel1.Controls.Add(this.cmdAutoFill2);
		this.SplitContainer1.Panel1.Controls.Add(this.cmdAdjustUp);
		this.SplitContainer1.Panel1.Controls.Add(this.cmdStartOne1);
		this.SplitContainer1.Panel1.Controls.Add(this.cmdStartOne0);
		this.SplitContainer1.Panel1.Controls.Add(this.cmdClearAllMesurePoints);
		this.SplitContainer1.Panel1.Controls.Add(this.cmdAdjustLeft);
		this.SplitContainer1.Panel1.Controls.Add(this.cmdStart1);
		this.SplitContainer1.Panel1.Controls.Add(this.cmdStart0);
		this.SplitContainer1.Panel1.Controls.Add(this.PictureBox2);
		this.SplitContainer1.Panel1.Controls.Add(this.GroupBox3);
		this.SplitContainer1.Panel1.Controls.Add(this.GroupBox4);
		this.SplitContainer1.Panel1.Controls.Add(this.cmdAutoFill1);
		this.SplitContainer1.Panel1.Controls.Add(this.cmdSORT);
		this.SplitContainer1.Panel2.Controls.Add(this.GroupBox5);
		this.SplitContainer1.Size = new System.Drawing.Size(1594, 789);
		this.SplitContainer1.SplitterDistance = 670;
		this.SplitContainer1.TabIndex = 179;
		this.GroupBox7.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GroupBox7.Controls.Add(this.txtLineWidthVertCut);
		this.GroupBox7.Controls.Add(this.Label2);
		this.GroupBox7.Controls.Add(this.Label6);
		this.GroupBox7.Controls.Add(this.txtTransparFillVertCut);
		this.GroupBox7.Location = new System.Drawing.Point(360, 579);
		this.GroupBox7.Name = "GroupBox7";
		this.GroupBox7.Size = new System.Drawing.Size(304, 52);
		this.GroupBox7.TabIndex = 205;
		this.GroupBox7.TabStop = false;
		this.GroupBox7.Text = "Cuts visualization";
		this.txtLineWidthVertCut.Location = new System.Drawing.Point(97, 23);
		this.txtLineWidthVertCut.Maximum = new decimal(new int[4] { 10, 0, 0, 0 });
		this.txtLineWidthVertCut.Minimum = new decimal(new int[4] { 1, 0, 0, 0 });
		this.txtLineWidthVertCut.Name = "txtLineWidthVertCut";
		this.txtLineWidthVertCut.Size = new System.Drawing.Size(45, 20);
		this.txtLineWidthVertCut.TabIndex = 3;
		this.txtLineWidthVertCut.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtLineWidthVertCut.Value = new decimal(new int[4] { 2, 0, 0, 0 });
		this.Label2.AutoSize = true;
		this.Label2.Location = new System.Drawing.Point(11, 25);
		this.Label2.Name = "Label2";
		this.Label2.Size = new System.Drawing.Size(75, 13);
		this.Label2.TabIndex = 146;
		this.Label2.Text = "Line thickness";
		this.Label6.AutoSize = true;
		this.Label6.Location = new System.Drawing.Point(163, 25);
		this.Label6.Name = "Label6";
		this.Label6.Size = new System.Drawing.Size(72, 13);
		this.Label6.TabIndex = 164;
		this.Label6.Text = "Transparency";
		this.txtTransparFillVertCut.Location = new System.Drawing.Point(243, 23);
		this.txtTransparFillVertCut.Maximum = new decimal(new int[4] { 10, 0, 0, 0 });
		this.txtTransparFillVertCut.Name = "txtTransparFillVertCut";
		this.txtTransparFillVertCut.Size = new System.Drawing.Size(45, 20);
		this.txtTransparFillVertCut.TabIndex = 163;
		this.txtTransparFillVertCut.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtTransparFillVertCut.Value = new decimal(new int[4] { 1, 0, 0, 0 });
		this.cmdAddRangeAndStep.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdAddRangeAndStep.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdAddRangeAndStep.Location = new System.Drawing.Point(95, 10);
		this.cmdAddRangeAndStep.Name = "cmdAddRangeAndStep";
		this.cmdAddRangeAndStep.Size = new System.Drawing.Size(26, 26);
		this.cmdAddRangeAndStep.TabIndex = 204;
		this.cmdAddRangeAndStep.TabStop = false;
		this.cmdAddRangeAndStep.Text = "+";
		this.ToolTip1.SetToolTip(this.cmdAddRangeAndStep, "Add additional vertical cuts in a range of angles from the origin");
		this.cmdAddRangeAndStep.UseVisualStyleBackColor = true;
		this.GroupBox6.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.GroupBox6.Controls.Add(this.chkLimit3);
		this.GroupBox6.Controls.Add(this.chkLimit2);
		this.GroupBox6.Controls.Add(this.chkLimit1);
		this.GroupBox6.Controls.Add(this.chkLimit0);
		this.GroupBox6.Location = new System.Drawing.Point(3, 579);
		this.GroupBox6.Name = "GroupBox6";
		this.GroupBox6.Size = new System.Drawing.Size(351, 52);
		this.GroupBox6.TabIndex = 203;
		this.GroupBox6.TabStop = false;
		this.GroupBox6.Text = "Use EMF Limits for calculations";
		this.chkLimit3.AutoSize = true;
		this.chkLimit3.Location = new System.Drawing.Point(265, 22);
		this.chkLimit3.Name = "chkLimit3";
		this.chkLimit3.Size = new System.Drawing.Size(61, 17);
		this.chkLimit3.TabIndex = 7;
		this.chkLimit3.TabStop = false;
		this.chkLimit3.Text = "Limits 4";
		this.chkLimit3.UseVisualStyleBackColor = true;
		this.chkLimit2.AutoSize = true;
		this.chkLimit2.Location = new System.Drawing.Point(182, 22);
		this.chkLimit2.Name = "chkLimit2";
		this.chkLimit2.Size = new System.Drawing.Size(61, 17);
		this.chkLimit2.TabIndex = 6;
		this.chkLimit2.TabStop = false;
		this.chkLimit2.Text = "Limits 3";
		this.chkLimit2.UseVisualStyleBackColor = true;
		this.chkLimit1.AutoSize = true;
		this.chkLimit1.Location = new System.Drawing.Point(99, 22);
		this.chkLimit1.Name = "chkLimit1";
		this.chkLimit1.Size = new System.Drawing.Size(61, 17);
		this.chkLimit1.TabIndex = 5;
		this.chkLimit1.TabStop = false;
		this.chkLimit1.Text = "Limits 2";
		this.chkLimit1.UseVisualStyleBackColor = true;
		this.chkLimit0.AutoSize = true;
		this.chkLimit0.Location = new System.Drawing.Point(16, 22);
		this.chkLimit0.Name = "chkLimit0";
		this.chkLimit0.Size = new System.Drawing.Size(61, 17);
		this.chkLimit0.TabIndex = 4;
		this.chkLimit0.TabStop = false;
		this.chkLimit0.Text = "Limits 1";
		this.chkLimit0.UseVisualStyleBackColor = true;
		this.cmdAutoFill3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdAutoFill3.Image = (System.Drawing.Image)resources.GetObject("cmdAutoFill3.Image");
		this.cmdAutoFill3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdAutoFill3.Location = new System.Drawing.Point(68, 10);
		this.cmdAutoFill3.Name = "cmdAutoFill3";
		this.cmdAutoFill3.Size = new System.Drawing.Size(26, 26);
		this.cmdAutoFill3.TabIndex = 197;
		this.cmdAutoFill3.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdAutoFill3, resources.GetString("cmdAutoFill3.ToolTip"));
		this.cmdAutoFill3.UseVisualStyleBackColor = true;
		this.cmdWord.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdWord.Image = (System.Drawing.Image)resources.GetObject("cmdWord.Image");
		this.cmdWord.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdWord.Location = new System.Drawing.Point(338, 10);
		this.cmdWord.Name = "cmdWord";
		this.cmdWord.Size = new System.Drawing.Size(26, 26);
		this.cmdWord.TabIndex = 196;
		this.cmdWord.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdWord, "Reports for vertical cuts");
		this.cmdWord.UseVisualStyleBackColor = true;
		this.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GroupBox2.Controls.Add(this.Label1);
		this.GroupBox2.Controls.Add(this.txtLineHightAboveTerrain);
		this.GroupBox2.Controls.Add(this.chkUseAntName);
		this.GroupBox2.Controls.Add(this.chkShowPlus2m);
		this.GroupBox2.Controls.Add(this.chkAddOwner);
		this.GroupBox2.Location = new System.Drawing.Point(436, 637);
		this.GroupBox2.Name = "GroupBox2";
		this.GroupBox2.Size = new System.Drawing.Size(228, 149);
		this.GroupBox2.TabIndex = 192;
		this.GroupBox2.TabStop = false;
		this.Label1.AutoSize = true;
		this.Label1.Location = new System.Drawing.Point(198, 110);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(15, 13);
		this.Label1.TabIndex = 15;
		this.Label1.Text = "m";
		this.txtLineHightAboveTerrain.DecimalPlaces = 1;
		this.txtLineHightAboveTerrain.Increment = new decimal(new int[4] { 1, 0, 0, 65536 });
		this.txtLineHightAboveTerrain.Location = new System.Drawing.Point(154, 108);
		this.txtLineHightAboveTerrain.Maximum = new decimal(new int[4] { 10, 0, 0, 0 });
		this.txtLineHightAboveTerrain.Minimum = new decimal(new int[4] { 1, 0, 0, 65536 });
		this.txtLineHightAboveTerrain.Name = "txtLineHightAboveTerrain";
		this.txtLineHightAboveTerrain.Size = new System.Drawing.Size(41, 20);
		this.txtLineHightAboveTerrain.TabIndex = 14;
		this.txtLineHightAboveTerrain.Value = new decimal(new int[4] { 5, 0, 0, 65536 });
		this.chkUseAntName.Location = new System.Drawing.Point(17, 60);
		this.chkUseAntName.Name = "chkUseAntName";
		this.chkUseAntName.Size = new System.Drawing.Size(205, 43);
		this.chkUseAntName.TabIndex = 13;
		this.chkUseAntName.TabStop = false;
		this.chkUseAntName.Text = "Use antenna type instead of radiating source name when generating cut table";
		this.chkUseAntName.UseVisualStyleBackColor = true;
		this.chkShowPlus2m.AutoSize = true;
		this.chkShowPlus2m.Location = new System.Drawing.Point(17, 109);
		this.chkShowPlus2m.Name = "chkShowPlus2m";
		this.chkShowPlus2m.Size = new System.Drawing.Size(138, 17);
		this.chkShowPlus2m.TabIndex = 12;
		this.chkShowPlus2m.TabStop = false;
		this.chkShowPlus2m.Text = "Add a line above terrain";
		this.chkShowPlus2m.UseVisualStyleBackColor = true;
		this.chkAddOwner.Location = new System.Drawing.Point(17, 11);
		this.chkAddOwner.Name = "chkAddOwner";
		this.chkAddOwner.Size = new System.Drawing.Size(205, 47);
		this.chkAddOwner.TabIndex = 11;
		this.chkAddOwner.TabStop = false;
		this.chkAddOwner.Text = "Add the owner name to the name of the radiating source when generating cut table";
		this.chkAddOwner.UseVisualStyleBackColor = true;
		this.cmdAutoFill2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdAutoFill2.Image = (System.Drawing.Image)resources.GetObject("cmdAutoFill2.Image");
		this.cmdAutoFill2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
		this.cmdAutoFill2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdAutoFill2.Location = new System.Drawing.Point(41, 10);
		this.cmdAutoFill2.Name = "cmdAutoFill2";
		this.cmdAutoFill2.Size = new System.Drawing.Size(26, 26);
		this.cmdAutoFill2.TabIndex = 191;
		this.cmdAutoFill2.TabStop = false;
		this.cmdAutoFill2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.ToolTip1.SetToolTip(this.cmdAutoFill2, resources.GetString("cmdAutoFill2.ToolTip"));
		this.cmdAutoFill2.UseVisualStyleBackColor = true;
		this.cmdAdjustUp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdAdjustUp.Image = (System.Drawing.Image)resources.GetObject("cmdAdjustUp.Image");
		this.cmdAdjustUp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdAdjustUp.Location = new System.Drawing.Point(284, 10);
		this.cmdAdjustUp.Name = "cmdAdjustUp";
		this.cmdAdjustUp.Size = new System.Drawing.Size(26, 26);
		this.cmdAdjustUp.TabIndex = 189;
		this.cmdAdjustUp.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdAdjustUp, "Reduce the height of the cuts according to the results of the calculation");
		this.cmdAdjustUp.UseVisualStyleBackColor = true;
		this.cmdStartOne1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdStartOne1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
		this.cmdStartOne1.Image = (System.Drawing.Image)resources.GetObject("cmdStartOne1.Image");
		this.cmdStartOne1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdStartOne1.Location = new System.Drawing.Point(230, 10);
		this.cmdStartOne1.Name = "cmdStartOne1";
		this.cmdStartOne1.Size = new System.Drawing.Size(26, 26);
		this.cmdStartOne1.TabIndex = 186;
		this.cmdStartOne1.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdStartOne1, "Calculation of the selected vertical cuts with grid 2");
		this.cmdStartOne1.UseVisualStyleBackColor = true;
		this.cmdStartOne0.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdStartOne0.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
		this.cmdStartOne0.Image = (System.Drawing.Image)resources.GetObject("cmdStartOne0.Image");
		this.cmdStartOne0.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdStartOne0.Location = new System.Drawing.Point(203, 10);
		this.cmdStartOne0.Name = "cmdStartOne0";
		this.cmdStartOne0.Size = new System.Drawing.Size(26, 26);
		this.cmdStartOne0.TabIndex = 185;
		this.cmdStartOne0.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdStartOne0, "Calculation of the selected vertical cuts with grid 1");
		this.cmdStartOne0.UseVisualStyleBackColor = true;
		this.cmdClearAllMesurePoints.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdClearAllMesurePoints.Image = (System.Drawing.Image)resources.GetObject("cmdClearAllMesurePoints.Image");
		this.cmdClearAllMesurePoints.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdClearAllMesurePoints.Location = new System.Drawing.Point(311, 10);
		this.cmdClearAllMesurePoints.Name = "cmdClearAllMesurePoints";
		this.cmdClearAllMesurePoints.Size = new System.Drawing.Size(26, 26);
		this.cmdClearAllMesurePoints.TabIndex = 184;
		this.cmdClearAllMesurePoints.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdClearAllMesurePoints, "Delete all dimension lines for all cuts");
		this.cmdClearAllMesurePoints.UseVisualStyleBackColor = true;
		this.cmdAdjustLeft.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdAdjustLeft.Image = (System.Drawing.Image)resources.GetObject("cmdAdjustLeft.Image");
		this.cmdAdjustLeft.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdAdjustLeft.Location = new System.Drawing.Point(257, 10);
		this.cmdAdjustLeft.Name = "cmdAdjustLeft";
		this.cmdAdjustLeft.Size = new System.Drawing.Size(26, 26);
		this.cmdAdjustLeft.TabIndex = 182;
		this.cmdAdjustLeft.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdAdjustLeft, "Reduce the length of the cuts according to the results of the calculation");
		this.cmdAdjustLeft.UseVisualStyleBackColor = true;
		this.cmdStart1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdStart1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
		this.cmdStart1.Image = (System.Drawing.Image)resources.GetObject("cmdStart1.Image");
		this.cmdStart1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdStart1.Location = new System.Drawing.Point(176, 10);
		this.cmdStart1.Name = "cmdStart1";
		this.cmdStart1.Size = new System.Drawing.Size(26, 26);
		this.cmdStart1.TabIndex = 180;
		this.cmdStart1.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdStart1, "Calculation of all active vertical cuts with grid 2");
		this.cmdStart1.UseVisualStyleBackColor = true;
		this.cmdStart0.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdStart0.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
		this.cmdStart0.Image = (System.Drawing.Image)resources.GetObject("cmdStart0.Image");
		this.cmdStart0.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdStart0.Location = new System.Drawing.Point(149, 10);
		this.cmdStart0.Name = "cmdStart0";
		this.cmdStart0.Size = new System.Drawing.Size(26, 26);
		this.cmdStart0.TabIndex = 179;
		this.cmdStart0.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdStart0, "Calculation of all active vertical cuts with grid 1");
		this.cmdStart0.UseVisualStyleBackColor = true;
		this.PictureBox2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.PictureBox2.Image = (System.Drawing.Image)resources.GetObject("PictureBox2.Image");
		this.PictureBox2.Location = new System.Drawing.Point(12, 637);
		this.PictureBox2.Name = "PictureBox2";
		this.PictureBox2.Size = new System.Drawing.Size(232, 145);
		this.PictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.PictureBox2.TabIndex = 4;
		this.PictureBox2.TabStop = false;
		this.GroupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GroupBox3.Controls.Add(this.DGV1);
		this.GroupBox3.Controls.Add(this.Button1);
		this.GroupBox3.Location = new System.Drawing.Point(3, 39);
		this.GroupBox3.Name = "GroupBox3";
		this.GroupBox3.Padding = new System.Windows.Forms.Padding(3, 5, 3, 3);
		this.GroupBox3.Size = new System.Drawing.Size(664, 534);
		this.GroupBox3.TabIndex = 177;
		this.GroupBox3.TabStop = false;
		this.GroupBox3.Text = "Vertical cut paremeters";
		this.DGV1.AllowUserToDeleteRows = false;
		this.DGV1.AllowUserToResizeColumns = false;
		this.DGV1.AllowUserToResizeRows = false;
		this.DGV1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
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
		this.DGV1.Columns.AddRange(this.Column1, this.Column4, this.Column8, this.Column9, this.Column2, this.Column3, this.Column6, this.Column7, this.Column5);
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.DGV1.DefaultCellStyle = dataGridViewCellStyle2;
		this.DGV1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.DGV1.Location = new System.Drawing.Point(3, 18);
		this.DGV1.Name = "DGV1";
		this.DGV1.RowHeadersWidth = 35;
		this.DGV1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.DGV1.RowTemplate.Height = 18;
		this.DGV1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.DGV1.Size = new System.Drawing.Size(658, 513);
		this.DGV1.TabIndex = 3;
		this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
		this.Column1.HeaderText = "";
		this.Column1.MinimumWidth = 21;
		this.Column1.Name = "Column1";
		this.Column1.Width = 21;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle3.NullValue = "0";
		this.Column4.DefaultCellStyle = dataGridViewCellStyle3;
		this.Column4.HeaderText = "Azimuth, °";
		this.Column4.Name = "Column4";
		this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column4.Width = 45;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle4.NullValue = "0";
		this.Column8.DefaultCellStyle = dataGridViewCellStyle4;
		this.Column8.HeaderText = "Xo, m";
		this.Column8.Name = "Column8";
		this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column8.ToolTipText = "X coordinate of the start of the cut, m";
		this.Column8.Width = 45;
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle5.NullValue = "0";
		this.Column9.DefaultCellStyle = dataGridViewCellStyle5;
		this.Column9.HeaderText = "Yo, m";
		this.Column9.Name = "Column9";
		this.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column9.ToolTipText = "Y coordinate of the start of the cut, m";
		this.Column9.Width = 45;
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle6.NullValue = "150";
		this.Column2.DefaultCellStyle = dataGridViewCellStyle6;
		this.Column2.HeaderText = "L1, m";
		this.Column2.Name = "Column2";
		this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column2.Width = 45;
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle7.NullValue = "0";
		this.Column3.DefaultCellStyle = dataGridViewCellStyle7;
		this.Column3.HeaderText = "L2, m";
		this.Column3.Name = "Column3";
		this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column3.Width = 45;
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle8.NullValue = "100";
		this.Column6.DefaultCellStyle = dataGridViewCellStyle8;
		this.Column6.HeaderText = "H1, m";
		this.Column6.Name = "Column6";
		this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column6.Width = 45;
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle9.NullValue = "0";
		this.Column7.DefaultCellStyle = dataGridViewCellStyle9;
		this.Column7.HeaderText = "H2, m";
		this.Column7.Name = "Column7";
		this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column7.Width = 45;
		this.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.Column5.DefaultCellStyle = dataGridViewCellStyle10;
		this.Column5.HeaderText = "Radiating sources located at this point with the direction along this azimuth";
		this.Column5.MinimumWidth = 200;
		this.Column5.Name = "Column5";
		this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Button1.Location = new System.Drawing.Point(0, 2000);
		this.Button1.Name = "Button1";
		this.Button1.Size = new System.Drawing.Size(51, 21);
		this.Button1.TabIndex = 1;
		this.Button1.Text = "refresh";
		this.Button1.UseVisualStyleBackColor = true;
		this.GroupBox4.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.GroupBox4.Controls.Add(this.chkShowMinDistToZO);
		this.GroupBox4.Controls.Add(this.chkShowKPonVert);
		this.GroupBox4.Controls.Add(this.chkShowManualPointsZO);
		this.GroupBox4.Controls.Add(this.chkHgtTopZO);
		this.GroupBox4.Controls.Add(this.chkHgtMaxZO);
		this.GroupBox4.Controls.Add(this.chkHgtBottomZO);
		this.GroupBox4.Controls.Add(this.chkLengthZO);
		this.GroupBox4.Location = new System.Drawing.Point(255, 637);
		this.GroupBox4.Name = "GroupBox4";
		this.GroupBox4.Size = new System.Drawing.Size(175, 149);
		this.GroupBox4.TabIndex = 178;
		this.GroupBox4.TabStop = false;
		this.GroupBox4.Text = "Display options";
		this.chkShowMinDistToZO.AutoSize = true;
		this.chkShowMinDistToZO.Checked = true;
		this.chkShowMinDistToZO.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkShowMinDistToZO.Location = new System.Drawing.Point(17, 88);
		this.chkShowMinDistToZO.Name = "chkShowMinDistToZO";
		this.chkShowMinDistToZO.Size = new System.Drawing.Size(145, 17);
		this.chkShowMinDistToZO.TabIndex = 18;
		this.chkShowMinDistToZO.TabStop = false;
		this.chkShowMinDistToZO.Text = "minimum distance to area";
		this.chkShowMinDistToZO.UseVisualStyleBackColor = true;
		this.chkShowKPonVert.AutoSize = true;
		this.chkShowKPonVert.Location = new System.Drawing.Point(17, 124);
		this.chkShowKPonVert.Name = "chkShowKPonVert";
		this.chkShowKPonVert.Size = new System.Drawing.Size(89, 17);
		this.chkShowKPonVert.TabIndex = 17;
		this.chkShowKPonVert.TabStop = false;
		this.chkShowKPonVert.Text = "control points";
		this.chkShowKPonVert.UseVisualStyleBackColor = true;
		this.chkShowManualPointsZO.AutoSize = true;
		this.chkShowManualPointsZO.Checked = true;
		this.chkShowManualPointsZO.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkShowManualPointsZO.Location = new System.Drawing.Point(17, 106);
		this.chkShowManualPointsZO.Name = "chkShowManualPointsZO";
		this.chkShowManualPointsZO.Size = new System.Drawing.Size(145, 17);
		this.chkShowManualPointsZO.TabIndex = 16;
		this.chkShowManualPointsZO.TabStop = false;
		this.chkShowManualPointsZO.Text = "additional dimension lines";
		this.chkShowManualPointsZO.UseVisualStyleBackColor = true;
		this.chkHgtTopZO.AutoSize = true;
		this.chkHgtTopZO.Checked = true;
		this.chkHgtTopZO.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkHgtTopZO.Location = new System.Drawing.Point(17, 70);
		this.chkHgtTopZO.Name = "chkHgtTopZO";
		this.chkHgtTopZO.Size = new System.Drawing.Size(109, 17);
		this.chkHgtTopZO.TabIndex = 15;
		this.chkHgtTopZO.TabStop = false;
		this.chkHgtTopZO.Text = "height of top area";
		this.chkHgtTopZO.UseVisualStyleBackColor = true;
		this.chkHgtMaxZO.AutoSize = true;
		this.chkHgtMaxZO.Checked = true;
		this.chkHgtMaxZO.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkHgtMaxZO.Location = new System.Drawing.Point(17, 52);
		this.chkHgtMaxZO.Name = "chkHgtMaxZO";
		this.chkHgtMaxZO.Size = new System.Drawing.Size(137, 17);
		this.chkHgtMaxZO.TabIndex = 13;
		this.chkHgtMaxZO.TabStop = false;
		this.chkHgtMaxZO.Text = "height of maximum area";
		this.chkHgtMaxZO.UseVisualStyleBackColor = true;
		this.chkHgtBottomZO.AutoSize = true;
		this.chkHgtBottomZO.Checked = true;
		this.chkHgtBottomZO.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkHgtBottomZO.Location = new System.Drawing.Point(17, 34);
		this.chkHgtBottomZO.Name = "chkHgtBottomZO";
		this.chkHgtBottomZO.Size = new System.Drawing.Size(126, 17);
		this.chkHgtBottomZO.TabIndex = 12;
		this.chkHgtBottomZO.TabStop = false;
		this.chkHgtBottomZO.Text = "height of bottom area";
		this.chkHgtBottomZO.UseVisualStyleBackColor = true;
		this.chkLengthZO.AutoSize = true;
		this.chkLengthZO.Checked = true;
		this.chkLengthZO.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkLengthZO.Location = new System.Drawing.Point(17, 16);
		this.chkLengthZO.Name = "chkLengthZO";
		this.chkLengthZO.Size = new System.Drawing.Size(137, 17);
		this.chkLengthZO.TabIndex = 11;
		this.chkLengthZO.TabStop = false;
		this.chkLengthZO.Text = "length of exposure area";
		this.chkLengthZO.UseVisualStyleBackColor = true;
		this.cmdAutoFill1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdAutoFill1.Image = (System.Drawing.Image)resources.GetObject("cmdAutoFill1.Image");
		this.cmdAutoFill1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdAutoFill1.Location = new System.Drawing.Point(14, 10);
		this.cmdAutoFill1.Name = "cmdAutoFill1";
		this.cmdAutoFill1.Size = new System.Drawing.Size(26, 26);
		this.cmdAutoFill1.TabIndex = 173;
		this.cmdAutoFill1.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdAutoFill1, "Fill in the list of vertical cuts by azimuths of antennas of active radiating sources");
		this.cmdAutoFill1.UseVisualStyleBackColor = true;
		this.cmdSORT.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdSORT.Image = (System.Drawing.Image)resources.GetObject("cmdSORT.Image");
		this.cmdSORT.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdSORT.Location = new System.Drawing.Point(122, 10);
		this.cmdSORT.Name = "cmdSORT";
		this.cmdSORT.Size = new System.Drawing.Size(26, 26);
		this.cmdSORT.TabIndex = 172;
		this.cmdSORT.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdSORT, "Sort cuts by azimuth");
		this.cmdSORT.UseVisualStyleBackColor = true;
		this.GroupBox5.Controls.Add(this.PB);
		this.GroupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GroupBox5.Location = new System.Drawing.Point(0, 0);
		this.GroupBox5.Name = "GroupBox5";
		this.GroupBox5.Size = new System.Drawing.Size(920, 789);
		this.GroupBox5.TabIndex = 180;
		this.GroupBox5.TabStop = false;
		this.GroupBox5.Text = "Image of EMF exposure area";
		this.PB.BackColor = System.Drawing.SystemColors.Window;
		this.PB.Dock = System.Windows.Forms.DockStyle.Fill;
		this.PB.Location = new System.Drawing.Point(3, 16);
		this.PB.Name = "PB";
		this.PB.Size = new System.Drawing.Size(914, 770);
		this.PB.TabIndex = 0;
		this.PB.TabStop = false;
		this.chkLen.AutoSize = true;
		this.chkLen.Checked = true;
		this.chkLen.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkLen.Location = new System.Drawing.Point(17, 28);
		this.chkLen.Name = "chkLen";
		this.chkLen.Size = new System.Drawing.Size(74, 17);
		this.chkLen.TabIndex = 11;
		this.chkLen.Text = "длина ЗО";
		this.chkLen.UseVisualStyleBackColor = true;
		this.ContMesure.Items.AddRange(new System.Windows.Forms.ToolStripItem[10] { this.cmdAddPoint, this.cmdAddLine, this.ToolStripSeparator1, this.cmdDelPoint, this.cmdDelLine, this.ToolStripSeparator2, this.cmdDelAllPoints, this.cmdDelAllLines, this.ToolStripSeparator3, this.cmdAddControlPoint });
		this.ContMesure.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
		this.ContMesure.Name = "ContextMenuSite";
		this.ContMesure.ShowImageMargin = false;
		this.ContMesure.ShowItemToolTips = false;
		this.ContMesure.Size = new System.Drawing.Size(231, 176);
		this.cmdAddPoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.cmdAddPoint.Name = "cmdAddPoint";
		this.cmdAddPoint.Size = new System.Drawing.Size(230, 22);
		this.cmdAddPoint.Text = "Add measurement point";
		this.cmdAddLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.cmdAddLine.Name = "cmdAddLine";
		this.cmdAddLine.Size = new System.Drawing.Size(230, 22);
		this.cmdAddLine.Text = "Add dimension line";
		this.ToolStripSeparator1.Name = "ToolStripSeparator1";
		this.ToolStripSeparator1.Size = new System.Drawing.Size(227, 6);
		this.cmdDelPoint.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.cmdDelPoint.Name = "cmdDelPoint";
		this.cmdDelPoint.Size = new System.Drawing.Size(230, 22);
		this.cmdDelPoint.Text = "Delete nearest measurement point";
		this.cmdDelLine.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
		this.cmdDelLine.Name = "cmdDelLine";
		this.cmdDelLine.Size = new System.Drawing.Size(230, 22);
		this.cmdDelLine.Text = "Delete nearest dimension Line";
		this.ToolStripSeparator2.Name = "ToolStripSeparator2";
		this.ToolStripSeparator2.Size = new System.Drawing.Size(227, 6);
		this.cmdDelAllPoints.Name = "cmdDelAllPoints";
		this.cmdDelAllPoints.Size = new System.Drawing.Size(230, 22);
		this.cmdDelAllPoints.Text = "Delete all measurement points";
		this.cmdDelAllLines.Name = "cmdDelAllLines";
		this.cmdDelAllLines.Size = new System.Drawing.Size(230, 22);
		this.cmdDelAllLines.Text = "Delete all dimension lines";
		this.ToolStripSeparator3.Name = "ToolStripSeparator3";
		this.ToolStripSeparator3.Size = new System.Drawing.Size(227, 6);
		this.cmdAddControlPoint.Name = "cmdAddControlPoint";
		this.cmdAddControlPoint.Size = new System.Drawing.Size(230, 22);
		this.cmdAddControlPoint.Text = "Add control point";
		this.ContWord.Items.AddRange(new System.Windows.Forms.ToolStripItem[1] { this.cmdVerticalCuts_1 });
		this.ContWord.Name = "cmsDBW";
		this.ContWord.Size = new System.Drawing.Size(222, 26);
		this.cmdVerticalCuts_1.Name = "cmdVerticalCuts_1";
		this.cmdVerticalCuts_1.Size = new System.Drawing.Size(221, 22);
		this.cmdVerticalCuts_1.Text = "Vertical cuts {VerticalCuts_1}";
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		base.Controls.Add(this.GroupBox1);
		this.MinimumSize = new System.Drawing.Size(800, 0);
		base.Name = "UC_VertPlanes";
		base.Size = new System.Drawing.Size(1600, 808);
		this.GroupBox1.ResumeLayout(false);
		this.SplitContainer1.Panel1.ResumeLayout(false);
		this.SplitContainer1.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.SplitContainer1).EndInit();
		this.SplitContainer1.ResumeLayout(false);
		this.GroupBox7.ResumeLayout(false);
		this.GroupBox7.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtLineWidthVertCut).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtTransparFillVertCut).EndInit();
		this.GroupBox6.ResumeLayout(false);
		this.GroupBox6.PerformLayout();
		this.GroupBox2.ResumeLayout(false);
		this.GroupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtLineHightAboveTerrain).EndInit();
		((System.ComponentModel.ISupportInitialize)this.PictureBox2).EndInit();
		this.GroupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.DGV1).EndInit();
		this.GroupBox4.ResumeLayout(false);
		this.GroupBox4.PerformLayout();
		this.GroupBox5.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.PB).EndInit();
		this.ContMesure.ResumeLayout(false);
		this.ContWord.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void UC_VertPlanes_Load(object sender, EventArgs e)
	{
		Module1.SetDoubleBuffered(DGV1);
		FillDGV1();
		if (!Information.IsNothing(Public2.PRJ.VP))
		{
			if (Form1_Public.VPSelRow > Public2.PRJ.VP.GetUpperBound(0))
			{
				Form1_Public.VPSelRow = 0;
			}
		}
		else
		{
			Form1_Public.VPSelRow = 0;
		}
		DGV1.CurrentCell = DGV1[0, Form1_Public.VPSelRow];
		DGV1.Rows[Form1_Public.VPSelRow].Selected = true;
		DGV1.FirstDisplayedScrollingRowIndex = Form1_Public.VPSelRow;
		FlagProcessing = true;
		chkLengthZO.Checked = Public2.PRJ.SETT.ShowLengthZO;
		chkHgtBottomZO.Checked = Public2.PRJ.SETT.ShowHgtBottomZO;
		chkHgtMaxZO.Checked = Public2.PRJ.SETT.ShowHgtMaxZO;
		chkHgtTopZO.Checked = Public2.PRJ.SETT.ShowHgtTopZO;
		chkShowManualPointsZO.Checked = Public2.PRJ.SETT.ShowManualPointsZO;
		chkShowMinDistToZO.Checked = Public2.PRJ.SETT.ShowMinDistToZO;
		chkShowKPonVert.Checked = Public2.PRJ.SETT.ShowKPonVert;
		chkShowPlus2m.Checked = Public2.PRJ.SETT.ShowPlus2m;
		txtLineHightAboveTerrain.Value = new decimal(Public2.PRJ.SETT.LineHightAboveTerrain);
		txtTransparFillVertCut.Value = new decimal(Public2.PRJ.SETT.TransparFillVertCut);
		txtLineWidthVertCut.Value = new decimal(Public2.PRJ.SETT.LineWidthVertCut);
		chkLimit0.Checked = Public2.PRJ.SETT.VertCutLimits[0];
		chkLimit1.Checked = Public2.PRJ.SETT.VertCutLimits[1];
		chkLimit2.Checked = Public2.PRJ.SETT.VertCutLimits[2];
		chkLimit3.Checked = Public2.PRJ.SETT.VertCutLimits[3];
		FlagProcessing = false;
		chkLimit0.ForeColor = Public2.PRJ.SETT.LimitColor[0];
		chkLimit1.ForeColor = Public2.PRJ.SETT.LimitColor[1];
		chkLimit2.ForeColor = Public2.PRJ.SETT.LimitColor[2];
		chkLimit3.ForeColor = Public2.PRJ.SETT.LimitColor[3];
	}

	public void FillDGV1()
	{
		FlagOK = false;
		DGV1.Rows.Clear();
		if (Information.IsNothing(Public2.PRJ.VP))
		{
			FlagOK = true;
			return;
		}
		int upperBound = Public2.PRJ.VP.GetUpperBound(0);
		for (int i = 0; i <= upperBound; i = checked(i + 1))
		{
			DGV1.Rows.Add(Public2.PRJ.VP[i].Activ, Public2.PRJ.VP[i].Azimuth, Public2.PRJ.VP[i].CoordM.Xm, Math.Round(Public2.PRJ.VP[i].CoordM.Ym, 1), Math.Round(Public2.PRJ.VP[i].L1, 1), Public2.PRJ.VP[i].L2, Public2.PRJ.VP[i].H1, Public2.PRJ.VP[i].H2, Public2.PRJ.VP[i].Name);
		}
		DGV1.ClearSelection();
		FlagOK = true;
	}

	private void DGV1_CellEnter(object sender, DataGridViewCellEventArgs e)
	{
		if (FlagOK)
		{
			Form1_Public.VPSelRow = checked((short)e.RowIndex);
			PB.Refresh();
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
					Public2.PRJ.VP[DGV1.SelectedCells[i].RowIndex].Activ = flag;
				}
				DGV1.Rows[e.RowIndex].Cells[0].Value = Operators.NotObject(DGV1.Rows[e.RowIndex].Cells[0].Value);
				Public2.PRJ.VP[e.RowIndex].Activ = Conversions.ToBoolean(Operators.NotObject(DGV1.Rows[e.RowIndex].Cells[0].Value));
				DGV1.ClearSelection();
				break;
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
					int upperBound = Public2.PRJ.VP.GetUpperBound(0);
					for (int i = 0; i <= upperBound; i++)
					{
						Public2.PRJ.VP[i].Activ = true;
					}
				}
				else
				{
					int upperBound2 = Public2.PRJ.VP.GetUpperBound(0);
					for (int j = 0; j <= upperBound2; j++)
					{
						Public2.PRJ.VP[j].Activ = false;
					}
				}
				FillDGV1();
				DGV1.ClearSelection();
			}
			else if ((e.RowIndex == -1) & (e.ColumnIndex > 0))
			{
				DGV1.CurrentCell = DGV1[e.ColumnIndex, 0];
				DGV1.ClearSelection();
				for (int k = Public2.PRJ.VP.GetUpperBound(0); k >= 0; k += -1)
				{
					DGV1[e.ColumnIndex, k].Selected = true;
				}
			}
		}
	}

	private void DGV1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
		if (!FlagOK)
		{
			return;
		}
		short num = -1;
		checked
		{
			if (DGV1.SelectedCells.Count > 0)
			{
				int num2 = DGV1.SelectedCells.Count - 1;
				for (int i = 0; i <= num2; i++)
				{
					if (DGV1.SelectedCells[i].RowIndex == Public2.PRJ.VP.Length)
					{
						FillDGV1();
						return;
					}
					if (num == -1)
					{
						num = (short)DGV1.SelectedCells[i].ColumnIndex;
					}
					else if (num != DGV1.SelectedCells[i].ColumnIndex)
					{
						FillDGV1();
						return;
					}
				}
				if ((num == 0) & (DGV1.SelectedCells.Count == 1))
				{
					Public2.PRJ.VP[DGV1.SelectedCells[0].RowIndex].Activ = Conversions.ToBoolean(DGV1[num, DGV1.SelectedCells[0].RowIndex].Value);
					return;
				}
				string text = Conversions.ToString(DGV1[num, DGV1.SelectedCells[0].RowIndex].Value);
				int num3 = DGV1.SelectedCells.Count - 1;
				for (int j = 0; j <= num3; j++)
				{
					switch (num)
					{
					case 1:
						if (Information.IsNothing(text))
						{
							Public2.PRJ.VP[DGV1.SelectedCells[j].RowIndex].Azimuth = 0f;
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = 0;
							FlagOK = true;
							break;
						}
						if (Module1.ValidText(text, CheckLimits: true, 0f, 360f, IncludeMin: true, IncludeMax: false, Message: false))
						{
							Public2.PRJ.VP[DGV1.SelectedCells[j].RowIndex].Azimuth = Module1.QSNG(text);
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = Module1.QSNG(text);
							FlagOK = true;
							break;
						}
						FlagOK = false;
						DGV1[num, DGV1.SelectedCells[0].RowIndex].Value = Public2.PRJ.VP[DGV1.SelectedCells[0].RowIndex].Azimuth;
						FlagOK = true;
						return;
					case 2:
						if (Information.IsNothing(text))
						{
							Public2.PRJ.VP[DGV1.SelectedCells[j].RowIndex].CoordM.Xm = 0.0;
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = 0;
							FlagOK = true;
							Public2.PRJ.VP[DGV1.SelectedCells[j].RowIndex].CoordWM = CalcCoords.ConvMetersToWebMercMeters(Public2.PRJ.VP[DGV1.SelectedCells[j].RowIndex].CoordM);
							break;
						}
						if (Module1.ValidText(Conversions.ToString(DGV1[num, DGV1.SelectedCells[0].RowIndex].Value), CheckLimits: false, 0f, 0f, IncludeMin: false, IncludeMax: false, Message: false))
						{
							Public2.PRJ.VP[DGV1.SelectedCells[j].RowIndex].CoordM.Xm = Math.Round(Module1.QSNG(Conversions.ToString(DGV1[num, DGV1.SelectedCells[0].RowIndex].Value)), 1);
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = Math.Round(Module1.QSNG(Conversions.ToString(DGV1[num, DGV1.SelectedCells[0].RowIndex].Value)), 1);
							FlagOK = true;
							Public2.PRJ.VP[DGV1.SelectedCells[j].RowIndex].CoordWM = CalcCoords.ConvMetersToWebMercMeters(Public2.PRJ.VP[DGV1.SelectedCells[j].RowIndex].CoordM);
							break;
						}
						FlagOK = false;
						DGV1[num, DGV1.SelectedCells[0].RowIndex].Value = Public2.PRJ.VP[DGV1.SelectedCells[0].RowIndex].CoordM.Xm;
						FlagOK = true;
						return;
					case 3:
						if (Information.IsNothing(text))
						{
							Public2.PRJ.VP[DGV1.SelectedCells[j].RowIndex].CoordM.Ym = 0.0;
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = 0;
							FlagOK = true;
							Public2.PRJ.VP[DGV1.SelectedCells[j].RowIndex].CoordWM = CalcCoords.ConvMetersToWebMercMeters(Public2.PRJ.VP[DGV1.SelectedCells[j].RowIndex].CoordM);
							break;
						}
						if (Module1.ValidText(Conversions.ToString(DGV1[num, DGV1.SelectedCells[0].RowIndex].Value), CheckLimits: false, 0f, 0f, IncludeMin: false, IncludeMax: false, Message: false))
						{
							Public2.PRJ.VP[DGV1.SelectedCells[j].RowIndex].CoordM.Ym = Math.Round(Module1.QSNG(Conversions.ToString(DGV1[num, DGV1.SelectedCells[0].RowIndex].Value)), 1);
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = Math.Round(Module1.QSNG(Conversions.ToString(DGV1[num, DGV1.SelectedCells[0].RowIndex].Value)), 1);
							FlagOK = true;
							Public2.PRJ.VP[DGV1.SelectedCells[j].RowIndex].CoordWM = CalcCoords.ConvMetersToWebMercMeters(Public2.PRJ.VP[DGV1.SelectedCells[j].RowIndex].CoordM);
							break;
						}
						FlagOK = false;
						DGV1[num, DGV1.SelectedCells[0].RowIndex].Value = Public2.PRJ.VP[DGV1.SelectedCells[0].RowIndex].CoordM.Ym;
						FlagOK = true;
						return;
					case 4:
						if (Information.IsNothing(text))
						{
							Public2.PRJ.VP[DGV1.SelectedCells[j].RowIndex].L1 = 150f;
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = 150;
							FlagOK = true;
							break;
						}
						if (Module1.ValidText(text, CheckLimits: true, 0f, 10000f, IncludeMin: false, IncludeMax: true, Message: false))
						{
							Public2.PRJ.VP[DGV1.SelectedCells[j].RowIndex].L1 = Module1.QSNG(text);
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = Module1.QSNG(text);
							FlagOK = true;
							break;
						}
						FlagOK = false;
						DGV1[num, DGV1.SelectedCells[0].RowIndex].Value = Public2.PRJ.VP[DGV1.SelectedCells[0].RowIndex].L1;
						FlagOK = true;
						return;
					case 5:
						if (Information.IsNothing(text))
						{
							Public2.PRJ.VP[DGV1.SelectedCells[j].RowIndex].L2 = 0f;
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = 0;
							FlagOK = true;
							break;
						}
						if (Module1.ValidText(text, CheckLimits: true, 0f, 1000f, IncludeMin: true, IncludeMax: true, Message: false))
						{
							Public2.PRJ.VP[DGV1.SelectedCells[j].RowIndex].L2 = Module1.QSNG(text);
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = Module1.QSNG(text);
							FlagOK = true;
							break;
						}
						FlagOK = false;
						DGV1[num, DGV1.SelectedCells[0].RowIndex].Value = Public2.PRJ.VP[DGV1.SelectedCells[0].RowIndex].L2;
						FlagOK = true;
						return;
					case 6:
						if (Information.IsNothing(text))
						{
							Public2.PRJ.VP[DGV1.SelectedCells[j].RowIndex].H1 = 100f;
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = 100;
							FlagOK = true;
							break;
						}
						if (Module1.ValidText(text, CheckLimits: true, 0f, 10000f, IncludeMin: false, IncludeMax: true, Message: false))
						{
							Public2.PRJ.VP[DGV1.SelectedCells[j].RowIndex].H1 = Module1.QSNG(text);
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = Module1.QSNG(text);
							FlagOK = true;
							break;
						}
						FlagOK = false;
						DGV1[num, DGV1.SelectedCells[0].RowIndex].Value = Public2.PRJ.VP[DGV1.SelectedCells[0].RowIndex].H1;
						FlagOK = true;
						return;
					case 7:
						if (Information.IsNothing(text))
						{
							Public2.PRJ.VP[DGV1.SelectedCells[j].RowIndex].H2 = 0f;
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = 0;
							FlagOK = true;
							break;
						}
						if (Module1.ValidText(text, CheckLimits: true, 0f, 1000f, IncludeMin: true, IncludeMax: true, Message: false))
						{
							Public2.PRJ.VP[DGV1.SelectedCells[j].RowIndex].H2 = Module1.QSNG(text);
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = Module1.QSNG(text);
							FlagOK = true;
							break;
						}
						FlagOK = false;
						DGV1[num, DGV1.SelectedCells[0].RowIndex].Value = Public2.PRJ.VP[DGV1.SelectedCells[0].RowIndex].H2;
						FlagOK = true;
						return;
					case 8:
						Public2.PRJ.VP[DGV1.SelectedCells[j].RowIndex].Name = text;
						FlagOK = false;
						DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = text;
						FlagOK = true;
						break;
					}
				}
			}
			else
			{
				FillDGV1();
			}
		}
	}

	private void DGV1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
	{
		ref Public2.StructVertPlane[] vP = ref Public2.PRJ.VP;
		vP = (Public2.StructVertPlane[])Utils.CopyArray(vP, new Public2.StructVertPlane[checked(DGV1.Rows.Count - 2 + 1)]);
		Public2.PRJ.VP[Public2.PRJ.VP.GetUpperBound(0)].Activ = true;
		Public2.PRJ.VP[Public2.PRJ.VP.GetUpperBound(0)].Azimuth = 0f;
		Public2.PRJ.VP[Public2.PRJ.VP.GetUpperBound(0)].CoordM.Xm = 0.0;
		Public2.PRJ.VP[Public2.PRJ.VP.GetUpperBound(0)].CoordM.Ym = 0.0;
		Public2.PRJ.VP[Public2.PRJ.VP.GetUpperBound(0)].CoordWM = CalcCoords.ConvMetersToWebMercMeters(Public2.PRJ.VP[Public2.PRJ.VP.GetUpperBound(0)].CoordM);
		Public2.PRJ.VP[Public2.PRJ.VP.GetUpperBound(0)].L1 = 150f;
		Public2.PRJ.VP[Public2.PRJ.VP.GetUpperBound(0)].L2 = 0f;
		Public2.PRJ.VP[Public2.PRJ.VP.GetUpperBound(0)].H1 = 100f;
		Public2.PRJ.VP[Public2.PRJ.VP.GetUpperBound(0)].H2 = 0f;
		Public2.PRJ.VP[Public2.PRJ.VP.GetUpperBound(0)].Name = "";
	}

	private void DGV1_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Delete || Information.IsNothing(Public2.PRJ.VP) || DGV1.SelectedRows.Count < 1 || ((DGV1.SelectedRows.Count == 1) & (DGV1.SelectedRows[0].Index > Public2.PRJ.VP.GetUpperBound(0))))
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
				if (DGV1.SelectedRows[i].Index <= Public2.PRJ.VP.GetUpperBound(0))
				{
					array = (short[])Utils.CopyArray(array, new short[num + 1]);
					array[num] = (short)DGV1.SelectedRows[i].Index;
					num++;
				}
			}
			if (Interaction.MsgBox(Localization.LocMSG(0) + " " + Conversions.ToString(unchecked((int)num)) + " " + Localization.LocMSG(67) + "?", MsgBoxStyle.YesNo) != MsgBoxResult.Yes)
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
				int num7 = Public2.PRJ.VP.GetUpperBound(0) - 1;
				for (int m = num6; m <= num7; m++)
				{
					Public2.PRJ.VP[m] = Module1.DeepClone(Public2.PRJ.VP[m + 1]);
				}
			}
			ref Public2.StructVertPlane[] vP = ref Public2.PRJ.VP;
			vP = (Public2.StructVertPlane[])Utils.CopyArray(vP, new Public2.StructVertPlane[Public2.PRJ.VP.GetUpperBound(0) - num5 + 1]);
			if (Public2.PRJ.VP.Length == 0)
			{
				Public2.PRJ.VP = null;
			}
			FillDGV1();
			DGV1.CurrentCell = DGV1[0, array[array.GetUpperBound(0)]];
			DGV1.Rows[array[array.GetUpperBound(0)]].Selected = true;
			DGV1.FirstDisplayedScrollingRowIndex = array[array.GetUpperBound(0)];
		}
	}

	private void PB_Paint(object sender, PaintEventArgs e)
	{
		e.Graphics.InterpolationMode = InterpolationMode.High;
		if (Information.IsNothing(Public2.PRJ.VP) || Form1_Public.VPSelRow > Public2.PRJ.VP.GetUpperBound(0) || Public2.PRJ.VP[Form1_Public.VPSelRow].LastL1 == 0f)
		{
			return;
		}
		Pen pen = new Pen(Color.Black, 1f);
		pen.DashStyle = DashStyle.Dash;
		Pen pen2 = new Pen(Color.Red, 1f);
		pen2.DashStyle = DashStyle.Dash;
		double num = Public2.PRJ.VP[Form1_Public.VPSelRow].LastL1 + Public2.PRJ.VP[Form1_Public.VPSelRow].LastL2;
		checked
		{
			short num2 = (short)(PB.Width - MarginLEFT - MarginRIGHT);
			short num3 = (short)(PB.Height - MarginUP - MarginDOWN);
			double num4 = (double)num2 / num;
			double num5 = (float)num3 / (Public2.PRJ.VP[Form1_Public.VPSelRow].LastH1 + Public2.PRJ.VP[Form1_Public.VPSelRow].LastH2);
			if (!Information.IsNothing(Public2.PRJ.TXT))
			{
				int upperBound = Public2.PRJ.TXT.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					if (!(Form1_Public.MastDimension(Public2.PRJ.TXT[i].Text) > 0f))
					{
						continue;
					}
					Public2.CoordMeters coordMeters = CalcCoords.ConvWebMercMetersToMetersRound(Public2.PRJ.TXT[i].CoordWM);
					float num6 = (float)(Public2.PRJ.VP[Form1_Public.VPSelRow].Coord1M.Xm - coordMeters.Xm);
					float num7 = (float)(Public2.PRJ.VP[Form1_Public.VPSelRow].Coord1M.Ym - coordMeters.Ym);
					float num8 = (float)Math.Sqrt(num6 * num6 + num7 * num7);
					float num9 = Form1_Public.MastDimension(Public2.PRJ.TXT[i].Text);
					if ((num8 < num9) | Module1.PointLineD(Public2.PRJ.VP[Form1_Public.VPSelRow].Coord1M.Xm, Public2.PRJ.VP[Form1_Public.VPSelRow].Coord1M.Ym, Public2.PRJ.VP[Form1_Public.VPSelRow].Coord2M.Xm, Public2.PRJ.VP[Form1_Public.VPSelRow].Coord2M.Ym, coordMeters.Xm, coordMeters.Ym, num9))
					{
						float xo = (float)((double)MarginLEFT + (double)num8 * num4);
						float yo = (float)((double)(short)unchecked(MarginUP + num3) - (double)(Triangul.FindElevWithBldPlusHbld(Public2.PRJ.TXT[i].CoordWM) - Public2.PRJ.VP[Form1_Public.VPSelRow].Elev0 + Public2.PRJ.VP[Form1_Public.VPSelRow].LastH2) * num5);
						if (num8 < num9)
						{
							xo = MarginLEFT;
						}
						Form1_Public.DrawMast2D(Public2.PRJ.TXT[i].Text, xo, yo, num4, num5, e);
					}
				}
				e.Graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, MarginLEFT, PB.Height);
			}
			if (!Information.IsNothing(Public2.PRJ.VP[Form1_Public.VPSelRow].Elev))
			{
				double num10 = (double)num2 / (double)Public2.PRJ.VP[Form1_Public.VPSelRow].Elev.GetUpperBound(0);
				PointF[] array = new PointF[Public2.PRJ.VP[Form1_Public.VPSelRow].Elev.GetUpperBound(0) + 3 + 1];
				int upperBound2 = Public2.PRJ.VP[Form1_Public.VPSelRow].Elev.GetUpperBound(0);
				for (int j = 0; j <= upperBound2; j++)
				{
					array[j].X = (float)((double)MarginLEFT + (double)j * num10);
					float num11 = (float)((double)(short)unchecked(MarginUP + num3) - (double)(Public2.PRJ.VP[Form1_Public.VPSelRow].Elev[j] - Public2.PRJ.VP[Form1_Public.VPSelRow].Elev0 + Public2.PRJ.VP[Form1_Public.VPSelRow].LastH2) * num5);
					if (num11 < (float)(short)unchecked(MarginUP + num3))
					{
						array[j].Y = num11;
					}
					else
					{
						array[j].Y = (short)unchecked(MarginUP + num3);
					}
				}
				if (!Information.IsNothing(Public2.PRJ.VP[Form1_Public.VPSelRow].Bld))
				{
					int upperBound3 = Public2.PRJ.VP[Form1_Public.VPSelRow].Bld.GetUpperBound(0);
					for (int k = 0; k <= upperBound3; k++)
					{
						short num12 = (short)Math.Floor((double)Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[k].L1 / num * (double)Public2.PRJ.VP[Form1_Public.VPSelRow].Elev.GetUpperBound(0));
						short num13 = (short)Math.Ceiling((double)Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[k].L2 / num * (double)Public2.PRJ.VP[Form1_Public.VPSelRow].Elev.GetUpperBound(0));
						short num14 = num12;
						short num15 = num13;
						for (short num16 = num14; num16 <= num15; num16 = (short)unchecked(num16 + 1))
						{
							array[num16].Y = (float)((double)(short)unchecked(MarginUP + num3) - (double)(Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[k].Fundation - Public2.PRJ.VP[Form1_Public.VPSelRow].Elev0 + Public2.PRJ.VP[Form1_Public.VPSelRow].LastH2) * num5);
						}
						array[num12].X = (float)((double)MarginLEFT + (double)Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[k].L1 / num * (double)Public2.PRJ.VP[Form1_Public.VPSelRow].Elev.GetUpperBound(0) * num10);
						array[num13].X = (float)((double)MarginLEFT + (double)Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[k].L2 / num * (double)Public2.PRJ.VP[Form1_Public.VPSelRow].Elev.GetUpperBound(0) * num10);
					}
				}
				array[array.GetUpperBound(0) - 2].X = array[array.GetUpperBound(0) - 3].X;
				array[array.GetUpperBound(0) - 2].Y = (short)unchecked(MarginUP + num3);
				array[array.GetUpperBound(0) - 1].X = MarginLEFT;
				array[array.GetUpperBound(0) - 1].Y = (short)unchecked(MarginUP + num3);
				array[array.GetUpperBound(0)].X = array[0].X;
				array[array.GetUpperBound(0)].Y = array[0].Y;
				e.Graphics.FillPolygon(new SolidBrush(Color.FromArgb(220, 220, 220)), array);
				e.Graphics.DrawLines(PenBlack2, array);
				if (Public2.PRJ.SETT.ShowPlus2m)
				{
					Pen pen3 = new Pen(Color.Red, 1f);
					pen3.DashStyle = DashStyle.Custom;
					pen3.DashPattern = new float[2] { 8f, 4f };
					array = new PointF[Public2.PRJ.VP[Form1_Public.VPSelRow].Elev.GetUpperBound(0) + 1];
					int upperBound4 = Public2.PRJ.VP[Form1_Public.VPSelRow].Elev.GetUpperBound(0);
					for (int l = 0; l <= upperBound4; l++)
					{
						array[l].X = (float)((double)MarginLEFT + (double)l * num10);
						float num17 = (float)((double)(short)unchecked(MarginUP + num3) - (double)(Public2.PRJ.VP[Form1_Public.VPSelRow].Elev[l] - Public2.PRJ.VP[Form1_Public.VPSelRow].Elev0 + Public2.PRJ.VP[Form1_Public.VPSelRow].LastH2 + Public2.PRJ.SETT.LineHightAboveTerrain) * num5);
						if (num17 < (float)(short)unchecked(MarginUP + num3))
						{
							array[l].Y = num17;
						}
						else
						{
							array[l].Y = (short)unchecked(MarginUP + num3);
						}
					}
					if (!Information.IsNothing(Public2.PRJ.VP[Form1_Public.VPSelRow].Bld))
					{
						int upperBound5 = Public2.PRJ.VP[Form1_Public.VPSelRow].Bld.GetUpperBound(0);
						for (int m = 0; m <= upperBound5; m++)
						{
							float num18 = (float)Math.Floor((double)Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[m].L1 / num * (double)Public2.PRJ.VP[Form1_Public.VPSelRow].Elev.GetUpperBound(0));
							float num19 = (float)Math.Ceiling((double)Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[m].L2 / num * (double)Public2.PRJ.VP[Form1_Public.VPSelRow].Elev.GetUpperBound(0));
							float num20 = (float)((double)MarginLEFT + (double)Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[m].L1 / num * (double)Public2.PRJ.VP[Form1_Public.VPSelRow].Elev.GetUpperBound(0) * num10);
							float num21 = (float)((double)MarginLEFT + (double)Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[m].L2 / num * (double)Public2.PRJ.VP[Form1_Public.VPSelRow].Elev.GetUpperBound(0) * num10);
							float num22 = (float)((double)(short)unchecked(MarginUP + num3) - (double)(Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[m].Fundation - Public2.PRJ.VP[Form1_Public.VPSelRow].Elev0 + Public2.PRJ.VP[Form1_Public.VPSelRow].LastH2 + Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[m].H + Public2.PRJ.SETT.LineHightAboveTerrain) * num5);
							float num23 = num19;
							for (float num24 = num18; num24 <= num23; num24++)
							{
								int upperBound6 = array.GetUpperBound(0);
								for (int n = 0; n <= upperBound6; n++)
								{
									if ((array[n].X > num20) & (array[n].X < num21))
									{
										array[n].Y = num22;
									}
								}
							}
						}
						List<PointF> list = new List<PointF>();
						list = array.ToList();
						int upperBound7 = Public2.PRJ.VP[Form1_Public.VPSelRow].Bld.GetUpperBound(0);
						for (int num25 = 0; num25 <= upperBound7; num25++)
						{
							float num26 = (float)((double)MarginLEFT + (double)Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[num25].L1 / num * (double)Public2.PRJ.VP[Form1_Public.VPSelRow].Elev.GetUpperBound(0) * num10);
							float num27 = (float)((double)MarginLEFT + (double)Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[num25].L2 / num * (double)Public2.PRJ.VP[Form1_Public.VPSelRow].Elev.GetUpperBound(0) * num10);
							float num28 = (float)((double)(short)unchecked(MarginUP + num3) - (double)(Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[num25].Fundation - Public2.PRJ.VP[Form1_Public.VPSelRow].Elev0 + Public2.PRJ.VP[Form1_Public.VPSelRow].LastH2 + Public2.PRJ.SETT.LineHightAboveTerrain) * num5);
							float num29 = (float)((double)(short)unchecked(MarginUP + num3) - (double)(Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[num25].Fundation - Public2.PRJ.VP[Form1_Public.VPSelRow].Elev0 + Public2.PRJ.VP[Form1_Public.VPSelRow].LastH2 + Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[num25].H + Public2.PRJ.SETT.LineHightAboveTerrain) * num5);
							list.Add(new PointF(num26, num28));
							list.Add(new PointF((float)((double)num26 + 0.001), num29));
							list.Add(new PointF((float)((double)num27 - 0.001), num29));
							list.Add(new PointF(num27, num28));
						}
						list.Sort([SpecialName] (PointF a1, PointF b1) => a1.X.CompareTo(b1.X));
						array = list.ToArray();
					}
					e.Graphics.DrawLines(pen3, array);
				}
			}
			if (!Information.IsNothing(Public2.PRJ.VP[Form1_Public.VPSelRow].Bld))
			{
				PointF[] array2 = new PointF[5];
				int upperBound8 = Public2.PRJ.VP[Form1_Public.VPSelRow].Bld.GetUpperBound(0);
				for (int num30 = 0; num30 <= upperBound8; num30++)
				{
					if ((short)Math.Round((double)(Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[num30].Fundation + Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[num30].H - Public2.PRJ.VP[Form1_Public.VPSelRow].Elev0 + Public2.PRJ.VP[Form1_Public.VPSelRow].LastH2) * num5) > 0)
					{
						array2[0].X = (float)((double)MarginLEFT + (double)Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[num30].L1 * num4);
						array2[0].Y = (float)((double)(short)unchecked(MarginUP + num3) - (double)(Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[num30].Fundation - Public2.PRJ.VP[Form1_Public.VPSelRow].Elev0 + Public2.PRJ.VP[Form1_Public.VPSelRow].LastH2) * num5);
						array2[1].X = array2[0].X;
						array2[1].Y = (float)((double)(short)unchecked(MarginUP + num3) - (double)(Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[num30].Fundation + Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[num30].H - Public2.PRJ.VP[Form1_Public.VPSelRow].Elev0 + Public2.PRJ.VP[Form1_Public.VPSelRow].LastH2) * num5);
						array2[2].X = (float)((double)MarginLEFT + (double)Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[num30].L2 * num4);
						array2[2].Y = array2[1].Y;
						array2[3].X = array2[2].X;
						array2[3].Y = array2[0].Y;
						array2[4].X = array2[0].X;
						array2[4].Y = array2[0].Y;
						e.Graphics.FillPolygon(new SolidBrush(Color.FromArgb(180, 180, 180)), array2);
						e.Graphics.DrawLines(PenBlack2, array2);
						string text = "";
						if (Public2.PRJ.SETT.ShowBuildingText)
						{
							text = text + Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[num30].Text + "\r\n";
						}
						if (Public2.PRJ.SETT.ShowBuildingHeight)
						{
							text = text + Conversions.ToString(Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[num30].H) + "m\r\n";
						}
						if (Public2.PRJ.SETT.ShowBuildingFoundation)
						{
							text = text + Conversions.ToString(Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[num30].Fundation) + "m\r\n";
						}
						if (text.Length > 0)
						{
							text = text.Substring(0, text.Length - 1);
							stringSize = e.Graphics.MeasureString(text, Font8);
							e.Graphics.DrawString(text, Font8, Brushes.Black, (array2[0].X + array2[2].X) / 2f - stringSize.Width / 2f, (array2[0].Y + array2[1].Y) / 2f - stringSize.Height / 2f);
						}
					}
				}
			}
			if (!Information.IsNothing(Public2.PRJ.VP[Form1_Public.VPSelRow].PLNs))
			{
				int num31 = 0;
				do
				{
					if (Public2.PRJ.SETT.VertCutLimits[num31] && !Information.IsNothing(Public2.PRJ.VP[Form1_Public.VPSelRow].PLNs[num31]))
					{
						double num32 = (double)num2 / (double)Public2.PRJ.VP[Form1_Public.VPSelRow].Size;
						double num33 = (double)num3 / (double)Public2.PRJ.VP[Form1_Public.VPSelRow].Size;
						Region region = new Region(new GraphicsPath(FillMode.Winding));
						int upperBound9 = Public2.PRJ.VP[Form1_Public.VPSelRow].PLNs[num31].GetUpperBound(0);
						for (int num34 = 0; num34 <= upperBound9; num34++)
						{
							Point[] array3 = new Point[Public2.PRJ.VP[Form1_Public.VPSelRow].PLNs[num31][num34].GetUpperBound(0) + 1];
							int upperBound10 = Public2.PRJ.VP[Form1_Public.VPSelRow].PLNs[num31][num34].GetUpperBound(0);
							for (int num35 = 0; num35 <= upperBound10; num35++)
							{
								array3[num35].X = (int)Math.Round((double)MarginLEFT + (double)Public2.PRJ.VP[Form1_Public.VPSelRow].PLNs[num31][num34][num35].X * num32);
								array3[num35].Y = (int)Math.Round((double)(short)unchecked(MarginUP + num3) - (double)Public2.PRJ.VP[Form1_Public.VPSelRow].PLNs[num31][num34][num35].Y * num33);
							}
							if (Public2.PRJ.SETT.TransparFillVertCut > 0)
							{
								GraphicsPath graphicsPath = new GraphicsPath(FillMode.Winding);
								graphicsPath.AddLines(array3);
								region.Xor(graphicsPath);
							}
							Pen pen4 = new Pen(Public2.PRJ.SETT.LimitColor[num31], unchecked((int)Public2.PRJ.SETT.LineWidthVertCut));
							pen4.LineJoin = LineJoin.Bevel;
							e.Graphics.DrawLines(pen4, array3);
						}
						if (Public2.PRJ.SETT.TransparFillVertCut > 0)
						{
							e.Graphics.FillRegion(new SolidBrush(Color.FromArgb((int)Math.Round((double)unchecked((int)Public2.PRJ.SETT.TransparFillVertCut) * 25.5), Public2.PRJ.SETT.LimitColor[num31])), region);
						}
					}
					num31++;
				}
				while (num31 <= 3);
			}
			float xmin = 0f - Public2.PRJ.VP[Form1_Public.VPSelRow].LastL2;
			float lastL = Public2.PRJ.VP[Form1_Public.VPSelRow].LastL1;
			short num36 = default;
			int N = num36;
			float dX = default;
			float X = default;
			Module1.RazmetkaOSI(xmin, lastL, 8, 13, ref N, ref dX, ref X);
			num36 = (short)N;
			N = num36 - 1;
			for (int num37 = 0; num37 <= N; num37++)
			{
				short num38 = (short)(int)Math.Round((double)MarginLEFT + (double)(X + Public2.PRJ.VP[Form1_Public.VPSelRow].LastL2) * num4);
				e.Graphics.DrawLine(pen, num38, MarginUP, num38, (short)unchecked(MarginUP + num3));
				stringSize = e.Graphics.MeasureString(Conversions.ToString(X), Font8);
				e.Graphics.DrawString(Conversions.ToString(X), Font8, drawBrushBlack, (float)num38 - stringSize.Width / 2f, (short)unchecked(MarginUP + num3) + 2);
				X += dX;
			}
			e.Graphics.DrawString("R, m", Font8, drawBrushBlack, (short)unchecked(MarginLEFT + num2) + 3, (short)unchecked(MarginUP + num3) - 10);
			float xmin2 = 0f - Public2.PRJ.VP[Form1_Public.VPSelRow].LastH2;
			float lastH = Public2.PRJ.VP[Form1_Public.VPSelRow].LastH1;
			int N2 = num36;
			Module1.RazmetkaOSI(xmin2, lastH, 8, 13, ref N2, ref dX, ref X);
			num36 = (short)N2;
			N2 = num36 - 1;
			for (int num39 = 0; num39 <= N2; num39++)
			{
				short num38 = (short)(int)Math.Round((double)(short)unchecked(MarginUP + num3) - (double)(X + Public2.PRJ.VP[Form1_Public.VPSelRow].LastH2) * num5);
				e.Graphics.DrawLine(pen, MarginLEFT, num38, (short)unchecked(MarginLEFT + num2), num38);
				stringSize = e.Graphics.MeasureString(Conversions.ToString(X), Font8);
				e.Graphics.DrawString(Conversions.ToString(X), Font8, drawBrushBlack, (float)MarginLEFT - stringSize.Width - 2f, num38 - 6);
				X += dX;
			}
			e.Graphics.DrawString("H, m", Font8, drawBrushBlack, 5f, MarginUP - 22);
			e.Graphics.DrawLine(new Pen(Color.Black, 2f), MarginLEFT, MarginUP, (short)unchecked(MarginLEFT + num2), MarginUP);
			e.Graphics.DrawLine(new Pen(Color.Black, 2f), (short)unchecked(MarginLEFT + num2), MarginUP, (short)unchecked(MarginLEFT + num2), (short)unchecked(MarginUP + num3));
			e.Graphics.DrawLine(new Pen(Color.Black, 2f), (short)unchecked(MarginLEFT + num2), (short)unchecked(MarginUP + num3), MarginLEFT, (short)unchecked(MarginUP + num3));
			e.Graphics.DrawLine(new Pen(Color.Black, 2f), MarginLEFT, (short)unchecked(MarginUP + num3), MarginLEFT, MarginUP);
			if (Public2.PRJ.SETT.ShowKPonVert && !Information.IsNothing(Public2.PRJ.CPG))
			{
				int upperBound11 = Public2.PRJ.CPG.GetUpperBound(0);
				for (int num40 = 0; num40 <= upperBound11; num40++)
				{
					if (!Public2.PRJ.CPG[num40].Active || Information.IsNothing(Public2.PRJ.CPG[num40].CP))
					{
						continue;
					}
					int upperBound12 = Public2.PRJ.CPG[num40].CP.GetUpperBound(0);
					for (int num41 = 0; num41 <= upperBound12; num41++)
					{
						if (!Module1.PointLineD(Public2.PRJ.VP[Form1_Public.VPSelRow].Coord1M.Xm, Public2.PRJ.VP[Form1_Public.VPSelRow].Coord1M.Ym, Public2.PRJ.VP[Form1_Public.VPSelRow].Coord2M.Xm, Public2.PRJ.VP[Form1_Public.VPSelRow].Coord2M.Ym, Public2.PRJ.CPG[num40].CP[num41].CoordM.Xm, Public2.PRJ.CPG[num40].CP[num41].CoordM.Ym, 1.0))
						{
							continue;
						}
						float num42 = (float)Math.Sqrt(Math.Pow(Public2.PRJ.CPG[num40].CP[num41].CoordM.Xm - Public2.PRJ.VP[Form1_Public.VPSelRow].Coord1M.Xm, 2.0) + Math.Pow(Public2.PRJ.CPG[num40].CP[num41].CoordM.Ym - Public2.PRJ.VP[Form1_Public.VPSelRow].Coord1M.Ym, 2.0));
						short num43 = (short)Math.Round((double)MarginLEFT + (double)num42 * num4 - 7.0);
						short num44 = (short)Math.Round((double)num42 / num * (double)Public2.PRJ.VP[Form1_Public.VPSelRow].Elev.GetUpperBound(0));
						if (!((num44 >= 0) & (num44 <= Public2.PRJ.VP[Form1_Public.VPSelRow].Elev.GetUpperBound(0))))
						{
							continue;
						}
						float num45 = Public2.PRJ.VP[Form1_Public.VPSelRow].Elev[(int)Math.Round((double)num42 / num * (double)Public2.PRJ.VP[Form1_Public.VPSelRow].Elev.GetUpperBound(0))];
						if (Operators.CompareString(Public2.PRJ.CPG[num40].CP[num41].Location, "Building", TextCompare: false) == 0 && !Information.IsNothing(Public2.PRJ.VP[Form1_Public.VPSelRow].Bld))
						{
							int upperBound13 = Public2.PRJ.VP[Form1_Public.VPSelRow].Bld.GetUpperBound(0);
							for (int num46 = 0; num46 <= upperBound13; num46++)
							{
								if ((num42 > Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[num46].L1) & (num42 < Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[num46].L2))
								{
									num45 = Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[num46].Fundation;
									break;
								}
							}
						}
						int upperBound14 = Public2.PRJ.CPG[num40].CP[num41].H.GetUpperBound(0);
						for (int num47 = 0; num47 <= upperBound14; num47++)
						{
							short num48 = (short)Math.Round((double)(short)unchecked(MarginUP + num3) - (double)(Public2.PRJ.CPG[num40].CP[num41].H[num47].H + num45 - Public2.PRJ.VP[Form1_Public.VPSelRow].Elev0 + Public2.PRJ.VP[Form1_Public.VPSelRow].LastH2) * num5 - 7.0);
							e.Graphics.DrawImage(Resources.ring8x8, num43, num48);
							string text2 = Public2.PRJ.SETT.ControlPointPrefix;
							if (Public2.PRJ.SETT.ShowControlPointGroups)
							{
								text2 = text2 + Conversions.ToString(num40 + 1) + ".";
							}
							text2 += Conversions.ToString(num41 + 1);
							if (Public2.PRJ.SETT.ShowControlPointHeight)
							{
								text2 = text2 + " (" + Conversions.ToString(Public2.PRJ.CPG[num40].CP[num41].H[num47].H) + "м)";
							}
							e.Graphics.DrawString(text2, Font8, Brushes.Blue, num43 + 8, num48 - 8);
						}
					}
				}
			}
			if (Public2.PRJ.SETT.ShowManualPointsZO && !Information.IsNothing(Public2.PRJ.VP[Form1_Public.VPSelRow].ManualPointZO))
			{
				int upperBound15 = Public2.PRJ.VP[Form1_Public.VPSelRow].ManualPointZO.GetUpperBound(0);
				for (int num49 = 0; num49 <= upperBound15; num49++)
				{
					short num50 = (short)Math.Round((double)MarginLEFT + (double)(Public2.PRJ.VP[Form1_Public.VPSelRow].ManualPointZO[num49].R + Public2.PRJ.VP[Form1_Public.VPSelRow].LastL2) * num4);
					short num51 = (short)Math.Round((double)(short)unchecked(MarginUP + num3) - (double)(Public2.PRJ.VP[Form1_Public.VPSelRow].ManualPointZO[num49].H + Public2.PRJ.VP[Form1_Public.VPSelRow].LastH2) * num5);
					e.Graphics.FillEllipse(Brushes.Red, num50 - 3, num51 - 3, 6, 6);
					e.Graphics.DrawLine(new Pen(Color.Red, 1f), (int)Math.Round((double)MarginLEFT + (double)Public2.PRJ.VP[Form1_Public.VPSelRow].LastL2 * num4), num51, num50, num51);
					e.Graphics.DrawLine(new Pen(Color.Red, 1f), num50, MarginUP, num50, num51);
					string s = "R=" + Conversions.ToString(Public2.PRJ.VP[Form1_Public.VPSelRow].ManualPointZO[num49].R) + "m\r\nH=" + Conversions.ToString(Public2.PRJ.VP[Form1_Public.VPSelRow].ManualPointZO[num49].H) + "m";
					e.Graphics.DrawString(s, Font8, Brushes.Red, num50 + 2, num51 - 26);
				}
			}
			if (Public2.PRJ.SETT.ShowManualPointsZO && !Information.IsNothing(Public2.PRJ.VP[Form1_Public.VPSelRow].DimensionZO))
			{
				int upperBound16 = Public2.PRJ.VP[Form1_Public.VPSelRow].DimensionZO.GetUpperBound(0);
				for (int num52 = 0; num52 <= upperBound16; num52++)
				{
					short num53 = (short)Math.Round((double)MarginLEFT + (double)(Public2.PRJ.VP[Form1_Public.VPSelRow].DimensionZO[num52].R + Public2.PRJ.VP[Form1_Public.VPSelRow].LastL2) * num4);
					short num54 = (short)Math.Round((double)(short)unchecked(MarginUP + num3) - (double)(Public2.PRJ.VP[Form1_Public.VPSelRow].DimensionZO[num52].H + Public2.PRJ.VP[Form1_Public.VPSelRow].LastH2) * num5);
					short num55 = (short)Math.Round((double)(short)unchecked(MarginUP + num3) - (double)(Public2.PRJ.VP[Form1_Public.VPSelRow].DimensionZO[num52].Hz + Public2.PRJ.VP[Form1_Public.VPSelRow].LastH2) * num5);
					e.Graphics.DrawLine(PenBlack2, num53, num55, num53, num54);
					e.Graphics.DrawLine(PenBlack2, num53, num54, num53 + 4, num54 + 8);
					e.Graphics.DrawLine(PenBlack2, num53, num54, num53 - 4, num54 + 8);
					e.Graphics.DrawLine(PenBlack2, num53, num55, num53 + 4, num55 - 8);
					e.Graphics.DrawLine(PenBlack2, num53, num55, num53 - 4, num55 - 8);
					Size size = PB.CreateGraphics().MeasureString(Public2.PRJ.VP[Form1_Public.VPSelRow].DimensionZO[num52].Text, Font8).ToSize();
					Point point = new Point((int)Math.Round((double)num53 - (double)size.Width / 2.0), (int)Math.Round((double)(short)unchecked(num55 + num54) / 2.0 - (double)size.Height / 2.0));
					e.Graphics.FillRectangle(Brushes.White, new Rectangle(point, size));
					e.Graphics.DrawString(Public2.PRJ.VP[Form1_Public.VPSelRow].DimensionZO[num52].Text, Font8, Brushes.Black, point);
				}
			}
			if (!Information.IsNothing(Public2.PRJ.VP))
			{
				if (Public2.PRJ.SETT.ShowLengthZO & (Public2.PRJ.VP[Form1_Public.VPSelRow].LenghtZO != 9999f))
				{
					short num56 = (short)Math.Round((double)MarginLEFT + (double)(Public2.PRJ.VP[Form1_Public.VPSelRow].LenghtZO + Public2.PRJ.VP[Form1_Public.VPSelRow].LastL2) * num4);
					e.Graphics.DrawLine(pen2, num56, MarginUP, num56, (short)unchecked(MarginUP + num3));
					string s2 = Conversions.ToString(Math.Round(Public2.PRJ.VP[Form1_Public.VPSelRow].LenghtZO, 1));
					stringSize = e.Graphics.MeasureString(s2, Font8);
					e.Graphics.DrawString(s2, Font8, Brushes.Red, (float)num56 - stringSize.Width / 2f, MarginUP - 15);
				}
				if (Public2.PRJ.SETT.ShowHgtMaxZO & (Public2.PRJ.VP[Form1_Public.VPSelRow].HeightMaxZO != 9999f))
				{
					short num57 = (short)Math.Round((double)(short)unchecked(MarginUP + num3) - (double)(Public2.PRJ.VP[Form1_Public.VPSelRow].HeightMaxZO + Public2.PRJ.VP[Form1_Public.VPSelRow].LastH2) * num5);
					e.Graphics.DrawLine(pen2, MarginLEFT, num57, (short)unchecked(MarginLEFT + num2), num57);
					string s3 = Conversions.ToString(Math.Round(Public2.PRJ.VP[Form1_Public.VPSelRow].HeightMaxZO, 1));
					stringSize = e.Graphics.MeasureString(s3, Font8);
					e.Graphics.DrawString(s3, Font8, Brushes.Red, (short)unchecked(MarginLEFT + num2) + 3, (float)num57 - stringSize.Height / 2f);
				}
				if (Public2.PRJ.SETT.ShowHgtBottomZO & (Public2.PRJ.VP[Form1_Public.VPSelRow].HeightBottomZO != 9999f))
				{
					short num58 = (short)Math.Round((double)(short)unchecked(MarginUP + num3) - (double)(Public2.PRJ.VP[Form1_Public.VPSelRow].HeightBottomZO + Public2.PRJ.VP[Form1_Public.VPSelRow].LastH2) * num5);
					e.Graphics.DrawLine(pen2, MarginLEFT, num58, (short)unchecked(MarginLEFT + num2), num58);
					string s4 = Conversions.ToString(Math.Round(Public2.PRJ.VP[Form1_Public.VPSelRow].HeightBottomZO, 1));
					stringSize = e.Graphics.MeasureString(s4, Font8);
					e.Graphics.DrawString(s4, Font8, Brushes.Red, (short)unchecked(MarginLEFT + num2) + 3, (float)num58 - stringSize.Height / 2f);
				}
				if (Public2.PRJ.SETT.ShowHgtTopZO & (Public2.PRJ.VP[Form1_Public.VPSelRow].HeightTopZO != 9999f))
				{
					short num59 = (short)Math.Round((double)(short)unchecked(MarginUP + num3) - (double)(Public2.PRJ.VP[Form1_Public.VPSelRow].HeightTopZO + Public2.PRJ.VP[Form1_Public.VPSelRow].LastH2) * num5);
					e.Graphics.DrawLine(pen2, MarginLEFT, num59, (short)unchecked(MarginLEFT + num2), num59);
					string s5 = Conversions.ToString(Math.Round(Public2.PRJ.VP[Form1_Public.VPSelRow].HeightTopZO, 1));
					stringSize = e.Graphics.MeasureString(s5, Font8);
					e.Graphics.DrawString(s5, Font8, Brushes.Red, (short)unchecked(MarginLEFT + num2) + 3, (float)num59 - stringSize.Height / 2f);
				}
			}
			if (Public2.PRJ.SETT.ShowMinDistToZO && Public2.PRJ.VP[Form1_Public.VPSelRow].DimensionLowerestZO.R < 9999f)
			{
				short num60 = (short)Math.Round((double)MarginLEFT + (double)(Public2.PRJ.VP[Form1_Public.VPSelRow].DimensionLowerestZO.R + Public2.PRJ.VP[Form1_Public.VPSelRow].LastL2) * num4);
				short num61 = (short)Math.Round((double)(short)unchecked(MarginUP + num3) - (double)(Public2.PRJ.VP[Form1_Public.VPSelRow].DimensionLowerestZO.H + Public2.PRJ.VP[Form1_Public.VPSelRow].LastH2) * num5);
				short num62 = (short)Math.Round((double)(short)unchecked(MarginUP + num3) - (double)(Public2.PRJ.VP[Form1_Public.VPSelRow].DimensionLowerestZO.Hz + Public2.PRJ.VP[Form1_Public.VPSelRow].LastH2) * num5);
				e.Graphics.DrawLine(PenBlack2, num60, num62, num60, num61);
				e.Graphics.DrawLine(PenBlack2, num60, num61, num60 + 4, num61 + 8);
				e.Graphics.DrawLine(PenBlack2, num60, num61, num60 - 4, num61 + 8);
				e.Graphics.DrawLine(PenBlack2, num60, num62, num60 + 4, num62 - 8);
				e.Graphics.DrawLine(PenBlack2, num60, num62, num60 - 4, num62 - 8);
				Size size2 = PB.CreateGraphics().MeasureString(Public2.PRJ.VP[Form1_Public.VPSelRow].DimensionLowerestZO.Text + "m", Font8).ToSize();
				Point point2 = new Point((int)Math.Round((double)num60 - (double)size2.Width / 2.0), (int)Math.Round((double)(short)unchecked(num62 + num61) / 2.0 - (double)size2.Height / 2.0));
				e.Graphics.FillRectangle(Brushes.White, new Rectangle(point2, size2));
				e.Graphics.DrawString(Public2.PRJ.VP[Form1_Public.VPSelRow].DimensionLowerestZO.Text + "m", Font8, Brushes.Black, point2);
			}
		}
	}

	private void PB_MouseMove(object sender, MouseEventArgs e)
	{
		checked
		{
			if (Information.IsNothing(Public2.PRJ.VP) || Form1_Public.VPSelRow > Public2.PRJ.VP.GetUpperBound(0) || Public2.PRJ.VP[Form1_Public.VPSelRow].LastL1 == 0f || !((e.X > MarginLEFT) & (e.X < PB.Width - MarginRIGHT) & (e.Y > MarginUP) & (e.Y < PB.Height - MarginDOWN)))
			{
				return;
			}
			double num = Public2.PRJ.VP[Form1_Public.VPSelRow].LastL1 + Public2.PRJ.VP[Form1_Public.VPSelRow].LastL2;
			double num2 = num / (double)(PB.Width - MarginLEFT - MarginRIGHT);
			double num3 = (Public2.PRJ.VP[Form1_Public.VPSelRow].LastH1 + Public2.PRJ.VP[Form1_Public.VPSelRow].LastH2) / (float)(PB.Height - MarginUP - MarginDOWN);
			float num4 = Conversions.ToSingle(Strings.Format((double)(e.X - MarginLEFT) * num2 - (double)Public2.PRJ.VP[Form1_Public.VPSelRow].LastL2, "0.0"));
			float num5 = Conversions.ToSingle(Strings.Format((double)(PB.Height - MarginDOWN - e.Y) * num3 - (double)Public2.PRJ.VP[Form1_Public.VPSelRow].LastH2, "0.0"));
			string s = "R=" + Conversions.ToString(num4) + "m\r\nH=" + Conversions.ToString(num5) + "m";
			float num6 = Public2.PRJ.VP[Form1_Public.VPSelRow].Elev[(int)Math.Round((double)((float)Public2.PRJ.VP[Form1_Public.VPSelRow].Elev.GetUpperBound(0) * (num4 + Public2.PRJ.VP[Form1_Public.VPSelRow].LastL2)) / num)];
			if (!Information.IsNothing(Public2.PRJ.VP[Form1_Public.VPSelRow].Bld))
			{
				int upperBound = Public2.PRJ.VP[Form1_Public.VPSelRow].Bld.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					if ((num4 + Public2.PRJ.VP[Form1_Public.VPSelRow].LastL2 > Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[i].L1) & (num4 + Public2.PRJ.VP[Form1_Public.VPSelRow].LastL2 < Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[i].L2))
					{
						num6 = Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[i].Fundation + Public2.PRJ.VP[Form1_Public.VPSelRow].Bld[i].H;
					}
				}
			}
			short num7 = (short)Math.Round((double)(PB.Height - MarginDOWN) - (double)(num6 - Public2.PRJ.VP[Form1_Public.VPSelRow].Elev0 + Public2.PRJ.VP[Form1_Public.VPSelRow].LastH2) / num3);
			string s2 = Strings.Format(num5 - num6 + Public2.PRJ.VP[Form1_Public.VPSelRow].Elev0, "0.0") + "m";
			Size size = PB.CreateGraphics().MeasureString(s2, Font8).ToSize();
			Point point = new Point((int)Math.Round((double)e.X - (double)size.Width / 2.0), (int)Math.Round((double)(num7 + e.Y) / 2.0 - (double)size.Height / 2.0));
			PB.Refresh();
			PB.CreateGraphics().DrawLine(PenRed1, e.X, MarginUP, e.X, PB.Height - MarginDOWN);
			PB.CreateGraphics().DrawLine(PenRed1, MarginLEFT, e.Y, PB.Width - MarginRIGHT, e.Y);
			PB.CreateGraphics().DrawString(s, Font8, Brushes.Red, e.X + 2, e.Y - 26);
			MovePoint.R = num4;
			MovePoint.H = num5;
			PB.CreateGraphics().DrawLine(PenBlack2, e.X, num7, e.X, e.Y);
			PB.CreateGraphics().DrawLine(PenBlack2, e.X, e.Y, e.X + 4, e.Y + 8);
			PB.CreateGraphics().DrawLine(PenBlack2, e.X, e.Y, e.X - 4, e.Y + 8);
			PB.CreateGraphics().DrawLine(PenBlack2, e.X, num7, e.X + 4, num7 - 8);
			PB.CreateGraphics().DrawLine(PenBlack2, e.X, num7, e.X - 4, num7 - 8);
			PB.CreateGraphics().FillRectangle(Brushes.White, new Rectangle(point, size));
			PB.CreateGraphics().DrawString(s2, Font8, Brushes.Black, point);
			MoveDimension.R = num4;
			MoveDimension.H = num5;
			MoveDimension.Hz = num6 - Public2.PRJ.VP[Form1_Public.VPSelRow].Elev0;
			MoveDimension.Text = s2;
		}
	}

	private void PB_MouseClick(object sender, MouseEventArgs e)
	{
		if (!Information.IsNothing(Public2.PRJ.VP) && Form1_Public.VPSelRow <= Public2.PRJ.VP.GetUpperBound(0) && Public2.PRJ.VP[Form1_Public.VPSelRow].LastL1 != 0f && e.Button == MouseButtons.Right && checked((e.X > MarginLEFT) & (e.X < PB.Width - MarginRIGHT) & (e.Y > MarginUP) & (e.Y < PB.Height - MarginDOWN)))
		{
			ContMesure.Show(PB, e.Location);
		}
	}

	private void ContMesure_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
	{
		checked
		{
			if (Operators.CompareString(e.ClickedItem.Name, "cmdAddPoint", TextCompare: false) == 0)
			{
				if (Information.IsNothing(Public2.PRJ.VP[Form1_Public.VPSelRow].ManualPointZO))
				{
					Public2.PRJ.VP[Form1_Public.VPSelRow].ManualPointZO = new Public2.StructMesurePoint[1];
				}
				else
				{
					ref Public2.StructMesurePoint[] manualPointZO = ref Public2.PRJ.VP[Form1_Public.VPSelRow].ManualPointZO;
					manualPointZO = (Public2.StructMesurePoint[])Utils.CopyArray(manualPointZO, new Public2.StructMesurePoint[Public2.PRJ.VP[Form1_Public.VPSelRow].ManualPointZO.GetUpperBound(0) + 1 + 1]);
				}
				Public2.PRJ.VP[Form1_Public.VPSelRow].ManualPointZO[Public2.PRJ.VP[Form1_Public.VPSelRow].ManualPointZO.GetUpperBound(0)] = Module1.DeepClone(MovePoint);
			}
			else if (Operators.CompareString(e.ClickedItem.Name, "cmdAddLine", TextCompare: false) == 0)
			{
				if (Information.IsNothing(Public2.PRJ.VP[Form1_Public.VPSelRow].DimensionZO))
				{
					Public2.PRJ.VP[Form1_Public.VPSelRow].DimensionZO = new Public2.StructDimension[1];
				}
				else
				{
					ref Public2.StructDimension[] dimensionZO = ref Public2.PRJ.VP[Form1_Public.VPSelRow].DimensionZO;
					dimensionZO = (Public2.StructDimension[])Utils.CopyArray(dimensionZO, new Public2.StructDimension[Public2.PRJ.VP[Form1_Public.VPSelRow].DimensionZO.GetUpperBound(0) + 1 + 1]);
				}
				Public2.PRJ.VP[Form1_Public.VPSelRow].DimensionZO[Public2.PRJ.VP[Form1_Public.VPSelRow].DimensionZO.GetUpperBound(0)] = Module1.DeepClone(MoveDimension);
			}
			else if (Operators.CompareString(e.ClickedItem.Name, "cmdDelPoint", TextCompare: false) == 0)
			{
				if (Information.IsNothing(Public2.PRJ.VP[Form1_Public.VPSelRow].ManualPointZO))
				{
					return;
				}
				double num = 999999999.0;
				int upperBound = Public2.PRJ.VP[Form1_Public.VPSelRow].ManualPointZO.GetUpperBound(0);
				short num3 = default;
				for (int i = 0; i <= upperBound; i++)
				{
					double num2 = Math.Sqrt(Math.Pow(Public2.PRJ.VP[Form1_Public.VPSelRow].ManualPointZO[i].R - MovePoint.R, 2.0) + Math.Pow(Public2.PRJ.VP[Form1_Public.VPSelRow].ManualPointZO[i].H - MovePoint.H, 2.0));
					if (num2 < num)
					{
						num = num2;
						num3 = (short)i;
					}
				}
				short num4 = num3;
				int num5 = Public2.PRJ.VP[Form1_Public.VPSelRow].ManualPointZO.GetUpperBound(0) - 1;
				for (int j = num4; j <= num5; j++)
				{
					Public2.PRJ.VP[Form1_Public.VPSelRow].ManualPointZO[j] = Public2.PRJ.VP[Form1_Public.VPSelRow].ManualPointZO[j + 1];
				}
				ref Public2.StructMesurePoint[] manualPointZO2 = ref Public2.PRJ.VP[Form1_Public.VPSelRow].ManualPointZO;
				manualPointZO2 = (Public2.StructMesurePoint[])Utils.CopyArray(manualPointZO2, new Public2.StructMesurePoint[Public2.PRJ.VP[Form1_Public.VPSelRow].ManualPointZO.GetUpperBound(0) - 1 + 1]);
				if (Public2.PRJ.VP[Form1_Public.VPSelRow].ManualPointZO.Length == 0)
				{
					Public2.PRJ.VP[Form1_Public.VPSelRow].ManualPointZO = null;
				}
			}
			else if (Operators.CompareString(e.ClickedItem.Name, "cmdDelLine", TextCompare: false) == 0)
			{
				if (Information.IsNothing(Public2.PRJ.VP[Form1_Public.VPSelRow].DimensionZO))
				{
					return;
				}
				double num6 = 999999999.0;
				int upperBound2 = Public2.PRJ.VP[Form1_Public.VPSelRow].DimensionZO.GetUpperBound(0);
				short num8 = default;
				for (int k = 0; k <= upperBound2; k++)
				{
					double num7 = Math.Sqrt(Math.Pow(Public2.PRJ.VP[Form1_Public.VPSelRow].DimensionZO[k].R - MovePoint.R, 2.0) + Math.Pow(Public2.PRJ.VP[Form1_Public.VPSelRow].DimensionZO[k].H - MovePoint.H, 2.0));
					if (num7 < num6)
					{
						num6 = num7;
						num8 = (short)k;
					}
				}
				short num9 = num8;
				int num10 = Public2.PRJ.VP[Form1_Public.VPSelRow].DimensionZO.GetUpperBound(0) - 1;
				for (int l = num9; l <= num10; l++)
				{
					Public2.PRJ.VP[Form1_Public.VPSelRow].DimensionZO[l] = Public2.PRJ.VP[Form1_Public.VPSelRow].DimensionZO[l + 1];
				}
				ref Public2.StructDimension[] dimensionZO2 = ref Public2.PRJ.VP[Form1_Public.VPSelRow].DimensionZO;
				dimensionZO2 = (Public2.StructDimension[])Utils.CopyArray(dimensionZO2, new Public2.StructDimension[Public2.PRJ.VP[Form1_Public.VPSelRow].DimensionZO.GetUpperBound(0) - 1 + 1]);
				if (Public2.PRJ.VP[Form1_Public.VPSelRow].DimensionZO.Length == 0)
				{
					Public2.PRJ.VP[Form1_Public.VPSelRow].DimensionZO = null;
				}
			}
			else if (Operators.CompareString(e.ClickedItem.Name, "cmdDelAllPoints", TextCompare: false) == 0)
			{
				Public2.PRJ.VP[Form1_Public.VPSelRow].ManualPointZO = null;
			}
			else if (Operators.CompareString(e.ClickedItem.Name, "cmdDelAllLines", TextCompare: false) == 0)
			{
				Public2.PRJ.VP[Form1_Public.VPSelRow].DimensionZO = null;
			}
			else if (Operators.CompareString(e.ClickedItem.Name, "cmdAddControlPoint", TextCompare: false) == 0)
			{
				if (Information.IsNothing(Public2.PRJ.CPG))
				{
					Interaction.MsgBox(Localization.LocMSG(68), MsgBoxStyle.Critical);
					return;
				}
				if (!Public2.PRJ.CPG[0].Active)
				{
					Public2.PRJ.CPG[0].Active = true;
				}
				if (Information.IsNothing(Public2.PRJ.CPG[0].CP))
				{
					Public2.PRJ.CPG[0].CP = new Public2.ControlPoint[1];
				}
				else
				{
					ref Public2.ControlPoint[] cP = ref Public2.PRJ.CPG[0].CP;
					cP = (Public2.ControlPoint[])Utils.CopyArray(cP, new Public2.ControlPoint[Public2.PRJ.CPG[0].CP.GetUpperBound(0) + 1 + 1]);
				}
				Public2.PRJ.CPG[0].CP[Public2.PRJ.CPG[0].CP.GetUpperBound(0)].H = new Public2.ControlPointHeight[1];
				Public2.PRJ.CPG[0].CP[Public2.PRJ.CPG[0].CP.GetUpperBound(0)].CoordM.Xm = Public2.PRJ.VP[Form1_Public.VPSelRow].CoordM.Xm + (double)MovePoint.R * Math.Sin((double)Public2.PRJ.VP[Form1_Public.VPSelRow].Azimuth / (180.0 / Math.PI));
				Public2.PRJ.CPG[0].CP[Public2.PRJ.CPG[0].CP.GetUpperBound(0)].CoordM.Ym = Public2.PRJ.VP[Form1_Public.VPSelRow].CoordM.Ym + (double)MovePoint.R * Math.Cos((double)Public2.PRJ.VP[Form1_Public.VPSelRow].Azimuth / (180.0 / Math.PI));
				Public2.PRJ.CPG[0].CP[Public2.PRJ.CPG[0].CP.GetUpperBound(0)].H[0].H = (float)Math.Round(MovePoint.H + Public2.PRJ.VP[Form1_Public.VPSelRow].Elev0 - Public2.PRJ.VP[Form1_Public.VPSelRow].Elev[(int)Math.Round((float)Public2.PRJ.VP[Form1_Public.VPSelRow].Elev.GetUpperBound(0) * (MovePoint.R + Public2.PRJ.VP[Form1_Public.VPSelRow].LastL2) / (Public2.PRJ.VP[Form1_Public.VPSelRow].LastL1 + Public2.PRJ.VP[Form1_Public.VPSelRow].LastL2))], 1);
				Public2.PRJ.CPG[0].CP[Public2.PRJ.CPG[0].CP.GetUpperBound(0)].CoordWM = CalcCoords.ConvMetersToWebMercMeters(Public2.PRJ.CPG[0].CP[Public2.PRJ.CPG[0].CP.GetUpperBound(0)].CoordM);
				Public2.PRJ.CPG[0].CP[Public2.PRJ.CPG[0].CP.GetUpperBound(0)].Location = "Ground";
				if (!Information.IsNothing(Public2.PRJ.BLD))
				{
					int upperBound3 = Public2.PRJ.BLD.GetUpperBound(0);
					for (int m = 0; m <= upperBound3; m++)
					{
						if (Module1.IsPointInsidePolygonF(Public2.PRJ.CPG[0].CP[Public2.PRJ.CPG[0].CP.GetUpperBound(0)].CoordWM, Public2.PRJ.BLD[m].CoordWM))
						{
							Public2.PRJ.CPG[0].CP[Public2.PRJ.CPG[0].CP.GetUpperBound(0)].Location = "Building";
							break;
						}
					}
				}
			}
			PB.Refresh();
		}
	}

	private void PB_MouseLeave(object sender, EventArgs e)
	{
		PB.Refresh();
	}

	private void PB_Resize(object sender, EventArgs e)
	{
		PB.Refresh();
	}

	private void cmdAutoFill1_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Information.IsNothing(Public2.PRJ.TX) || Interaction.MsgBox(Localization.LocMSG(69), MsgBoxStyle.YesNo) != MsgBoxResult.Yes)
		{
			return;
		}
		short num = -1;
		int upperBound = Public2.PRJ.TX.GetUpperBound(0);
		checked
		{
			Public2.StructVertPlane[] array = default;
			for (int i = 0; i <= upperBound; i++)
			{
				if (Public2.PRJ.TX[i].Active)
				{
					num++;
					array = (Public2.StructVertPlane[])Utils.CopyArray(array, new Public2.StructVertPlane[num + 1]);
					if (Public2.PRJ.TX[i].Az >= 0f)
					{
						array[num].Azimuth = Public2.PRJ.TX[i].Az;
					}
					else
					{
						array[num].Azimuth = 0f;
					}
					if (!chkUseAntName.Checked)
					{
						array[num].Name = Strings.Format(i + 1, "00").ToString() + ". " + Public2.PRJ.TX[i].Name;
					}
					else
					{
						array[num].Name = Strings.Format(i + 1, "00").ToString() + ". " + Public2.PRJ.TX[i].AntType;
					}
					if (chkAddOwner.Checked)
					{
						array[num].Name = array[num].Name + " " + Public2.PRJ.TX[i].Owner;
					}
					array[num].CoordWM = Public2.PRJ.TX[i].CoordWM;
					array[num].CoordM = Public2.PRJ.TX[i].CoordM;
				}
			}
			if (num == -1)
			{
				Public2.PRJ.VP = null;
				FillDGV1();
				return;
			}
			int num2 = array.GetUpperBound(0) - 1;
			for (int j = 0; j <= num2; j++)
			{
				int num3 = j + 1;
				int upperBound2 = array.GetUpperBound(0);
				for (int k = num3; k <= upperBound2; k++)
				{
					if (array[k].Azimuth < array[j].Azimuth)
					{
						Public2.StructVertPlane obj = Module1.DeepClone(array[j]);
						array[j] = Module1.DeepClone(array[k]);
						array[k] = Module1.DeepClone(obj);
					}
					else if ((array[k].Azimuth == array[j].Azimuth) & (Math.Sqrt(array[k].CoordM.Xm * array[k].CoordM.Xm + array[k].CoordM.Ym * array[k].CoordM.Ym) < Math.Sqrt(array[j].CoordM.Xm * array[j].CoordM.Xm + array[j].CoordM.Ym * array[j].CoordM.Ym)))
					{
						Public2.StructVertPlane obj = Module1.DeepClone(array[j]);
						array[j] = Module1.DeepClone(array[k]);
						array[k] = Module1.DeepClone(obj);
					}
					else if ((array[k].Azimuth == array[j].Azimuth) & (array[k].CoordM.Xm == array[j].CoordM.Xm) & (array[k].CoordM.Ym == array[j].CoordM.Ym) & (Operators.CompareString(array[k].Name, array[j].Name, TextCompare: false) < 0))
					{
						Public2.StructVertPlane obj = Module1.DeepClone(array[j]);
						array[j] = Module1.DeepClone(array[k]);
						array[k] = Module1.DeepClone(obj);
					}
				}
			}
			num = 0;
			int upperBound3 = array.GetUpperBound(0);
			for (int l = 1; l <= upperBound3; l++)
			{
				if ((array[l].Azimuth > array[l - 1].Azimuth) | (array[l].CoordM.Xm != array[l - 1].CoordM.Xm) | (array[l].CoordM.Ym != array[l - 1].CoordM.Ym))
				{
					num++;
					array[num] = Module1.DeepClone(array[l]);
				}
				else
				{
					array[num].Name = array[num].Name + "\r\n" + array[l].Name;
				}
			}
			array = (Public2.StructVertPlane[])Utils.CopyArray(array, new Public2.StructVertPlane[num + 1]);
			if (chkUseAntName.Checked)
			{
				int upperBound4 = array.GetUpperBound(0);
				for (int m = 0; m <= upperBound4; m++)
				{
					string[] array2 = array[m].Name.Split('\r');
					if (array2.Length <= 1)
					{
						continue;
					}
					array[m].Name = "";
					VN[] array3 = new VN[array2.GetUpperBound(0) + 1];
					int upperBound5 = array2.GetUpperBound(0);
					for (int n = 0; n <= upperBound5; n++)
					{
						array3[n].Name = array2[n].Substring(Strings.InStr(array2[n], ".") + 1);
						array3[n].Number = Conversions.ToString(Conversions.ToInteger(array2[n].Substring(0, Strings.InStr(array2[n], ".") - 1)));
					}
					int upperBound6 = array3.GetUpperBound(0);
					for (int num4 = 0; num4 <= upperBound6; num4++)
					{
						if (Operators.CompareString(array3[num4].Name, "", TextCompare: false) == 0)
						{
							continue;
						}
						if (num4 > 0)
						{
							array[m].Name = array[m].Name + "\r\n";
						}
						array[m].Name = array[m].Name + array3[num4].Number;
						int upperBound7 = array3.GetUpperBound(0);
						for (int num5 = 0; num5 <= upperBound7; num5++)
						{
							if ((num4 != num5) & (Operators.CompareString(array3[num4].Name, array3[num5].Name, TextCompare: false) == 0))
							{
								array[m].Name = array[m].Name + "," + array3[num5].Number;
								array3[num5].Name = "";
							}
						}
						array[m].Name = array[m].Name + ". " + array3[num4].Name;
						array3[num4].Name = "";
					}
				}
			}
			int upperBound8 = array.GetUpperBound(0);
			for (int num6 = 0; num6 <= upperBound8; num6++)
			{
				bool flag = false;
				if (!Information.IsNothing(Public2.PRJ.VP) && Operators.CompareString(array[num6].Name, "", TextCompare: false) != 0)
				{
					int upperBound9 = Public2.PRJ.VP.GetUpperBound(0);
					for (int num7 = 0; num7 <= upperBound9; num7++)
					{
						if (Operators.CompareString(Public2.PRJ.VP[num7].Name, array[num6].Name, TextCompare: false) == 0)
						{
							flag = true;
							array[num6].H1 = Public2.PRJ.VP[num7].H1;
							array[num6].H2 = Public2.PRJ.VP[num7].H2;
							array[num6].L1 = Public2.PRJ.VP[num7].L1;
							array[num6].L2 = Public2.PRJ.VP[num7].L2;
							break;
						}
					}
				}
				if (!flag)
				{
					array[num6].H1 = 100f;
					array[num6].H2 = 0f;
					array[num6].L1 = 150f;
					array[num6].L2 = 0f;
				}
				array[num6].Activ = true;
			}
			Public2.PRJ.VP = Module1.DeepClone(array);
			FillDGV1();
		}
	}

	private void cmdAutoFill2_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Information.IsNothing(Public2.PRJ.TX) || Interaction.MsgBox(Localization.LocMSG(69), MsgBoxStyle.YesNo) != MsgBoxResult.Yes)
		{
			return;
		}
		short num = -1;
		int upperBound = Public2.PRJ.TX.GetUpperBound(0);
		checked
		{
			Public2.StructVertPlane[] array = default;
			for (int i = 0; i <= upperBound; i++)
			{
				if (!Public2.PRJ.TX[i].Active)
				{
					continue;
				}
				num++;
				array = (Public2.StructVertPlane[])Utils.CopyArray(array, new Public2.StructVertPlane[num + 1]);
				if (Public2.PRJ.TX[i].Az >= 0f)
				{
					array[num].Azimuth = Public2.PRJ.TX[i].Az;
				}
				else
				{
					array[num].Azimuth = 0f;
				}
				if (!chkUseAntName.Checked)
				{
					array[num].Name = Strings.Format(i + 1, "00").ToString() + ". " + Public2.PRJ.TX[i].Name;
				}
				else
				{
					array[num].Name = Strings.Format(i + 1, "00").ToString() + ". " + Public2.PRJ.TX[i].AntType;
				}
				if (chkAddOwner.Checked)
				{
					array[num].Name = array[num].Name + " " + Public2.PRJ.TX[i].Owner;
				}
				array[num].CoordWM = Public2.PRJ.TX[i].CoordWM;
				array[num].CoordM = Public2.PRJ.TX[i].CoordM;
				if (Operators.CompareString(Public2.PRJ.TX[i].TypeOfService, "ДН", TextCompare: false) != 0 || Information.IsNothing(Public2.PRJ.TX[i].DnH) || !(Public2.PRJ.TX[i].DnH[0] > 3f))
				{
					continue;
				}
				float num2 = Public2.PRJ.TX[i].DnH[0];
				short num3 = 0;
				int num4 = 0;
				do
				{
					if (Public2.PRJ.TX[i].DnH[num4] < num2)
					{
						num2 = Public2.PRJ.TX[i].DnH[num4];
						num3 = (short)num4;
					}
					num4++;
				}
				while (num4 <= 359);
				array[num].Azimuth = array[num].Azimuth + (float)num3;
				if (array[num].Azimuth >= 360f)
				{
					array[num].Azimuth = array[num].Azimuth - 360f;
				}
			}
			if (num == -1)
			{
				Public2.PRJ.VP = null;
				FillDGV1();
				return;
			}
			int num5 = array.GetUpperBound(0) - 1;
			for (int j = 0; j <= num5; j++)
			{
				int num6 = j + 1;
				int upperBound2 = array.GetUpperBound(0);
				for (int k = num6; k <= upperBound2; k++)
				{
					if (array[k].Azimuth < array[j].Azimuth)
					{
						Public2.StructVertPlane obj = Module1.DeepClone(array[j]);
						array[j] = Module1.DeepClone(array[k]);
						array[k] = Module1.DeepClone(obj);
					}
					else if ((array[k].Azimuth == array[j].Azimuth) & (Math.Sqrt(array[k].CoordM.Xm * array[k].CoordM.Xm + array[k].CoordM.Ym * array[k].CoordM.Ym) < Math.Sqrt(array[j].CoordM.Xm * array[j].CoordM.Xm + array[j].CoordM.Ym * array[j].CoordM.Ym)))
					{
						Public2.StructVertPlane obj = Module1.DeepClone(array[j]);
						array[j] = Module1.DeepClone(array[k]);
						array[k] = Module1.DeepClone(obj);
					}
					else if ((array[k].Azimuth == array[j].Azimuth) & (array[k].CoordM.Xm == array[j].CoordM.Xm) & (array[k].CoordM.Ym == array[j].CoordM.Ym) & (Operators.CompareString(array[k].Name, array[j].Name, TextCompare: false) < 0))
					{
						Public2.StructVertPlane obj = Module1.DeepClone(array[j]);
						array[j] = Module1.DeepClone(array[k]);
						array[k] = Module1.DeepClone(obj);
					}
				}
			}
			num = 0;
			int upperBound3 = array.GetUpperBound(0);
			for (int l = 1; l <= upperBound3; l++)
			{
				if ((array[l].Azimuth > array[l - 1].Azimuth) | (array[l].CoordM.Xm != array[l - 1].CoordM.Xm) | (array[l].CoordM.Ym != array[l - 1].CoordM.Ym))
				{
					num++;
					array[num] = Module1.DeepClone(array[l]);
				}
				else
				{
					array[num].Name = array[num].Name + "\r\n" + array[l].Name;
				}
			}
			array = (Public2.StructVertPlane[])Utils.CopyArray(array, new Public2.StructVertPlane[num + 1]);
			if (chkUseAntName.Checked)
			{
				int upperBound4 = array.GetUpperBound(0);
				for (int m = 0; m <= upperBound4; m++)
				{
					string[] array2 = array[m].Name.Split('\r');
					if (array2.Length <= 1)
					{
						continue;
					}
					array[m].Name = "";
					VN[] array3 = new VN[array2.GetUpperBound(0) + 1];
					int upperBound5 = array2.GetUpperBound(0);
					for (int n = 0; n <= upperBound5; n++)
					{
						array3[n].Name = array2[n].Substring(Strings.InStr(array2[n], ".") + 1);
						array3[n].Number = Conversions.ToString(Conversions.ToInteger(array2[n].Substring(0, Strings.InStr(array2[n], ".") - 1)));
					}
					int upperBound6 = array3.GetUpperBound(0);
					for (int num7 = 0; num7 <= upperBound6; num7++)
					{
						if (Operators.CompareString(array3[num7].Name, "", TextCompare: false) == 0)
						{
							continue;
						}
						if (num7 > 0)
						{
							array[m].Name = array[m].Name + "\r\n";
						}
						array[m].Name = array[m].Name + array3[num7].Number;
						int upperBound7 = array3.GetUpperBound(0);
						for (int num8 = 0; num8 <= upperBound7; num8++)
						{
							if ((num7 != num8) & (Operators.CompareString(array3[num7].Name, array3[num8].Name, TextCompare: false) == 0))
							{
								array[m].Name = array[m].Name + "," + array3[num8].Number;
								array3[num8].Name = "";
							}
						}
						array[m].Name = array[m].Name + ". " + array3[num7].Name;
						array3[num7].Name = "";
					}
				}
			}
			int upperBound8 = array.GetUpperBound(0);
			for (int num9 = 0; num9 <= upperBound8; num9++)
			{
				bool flag = false;
				if (!Information.IsNothing(Public2.PRJ.VP) && Operators.CompareString(array[num9].Name, "", TextCompare: false) != 0)
				{
					int upperBound9 = Public2.PRJ.VP.GetUpperBound(0);
					for (int num10 = 0; num10 <= upperBound9; num10++)
					{
						if (Operators.CompareString(Public2.PRJ.VP[num10].Name, array[num9].Name, TextCompare: false) == 0)
						{
							flag = true;
							array[num9].H1 = Public2.PRJ.VP[num10].H1;
							array[num9].H2 = Public2.PRJ.VP[num10].H2;
							array[num9].L1 = Public2.PRJ.VP[num10].L1;
							array[num9].L2 = Public2.PRJ.VP[num10].L2;
							break;
						}
					}
				}
				if (!flag)
				{
					array[num9].H1 = 100f;
					array[num9].H2 = 0f;
					array[num9].L1 = 150f;
					array[num9].L2 = 0f;
				}
				array[num9].Activ = true;
			}
			Public2.PRJ.VP = Module1.DeepClone(array);
			FillDGV1();
		}
	}

	private void cmdAutoFill3_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Information.IsNothing(Public2.PRJ.TX) || Interaction.MsgBox(Localization.LocMSG(69), MsgBoxStyle.YesNo) != MsgBoxResult.Yes)
		{
			return;
		}
		short num = -1;
		int upperBound = Public2.PRJ.TX.GetUpperBound(0);
		checked
		{
			Public2.StructVertPlane[] array = default;
			for (int i = 0; i <= upperBound; i++)
			{
				if (Public2.PRJ.TX[i].Active)
				{
					num++;
					array = (Public2.StructVertPlane[])Utils.CopyArray(array, new Public2.StructVertPlane[num + 1]);
					if (Public2.PRJ.TX[i].Az >= 0f)
					{
						array[num].Azimuth = Public2.PRJ.TX[i].Az;
					}
					else
					{
						array[num].Azimuth = 0f;
					}
					if (!chkUseAntName.Checked)
					{
						array[num].Name = Strings.Format(i + 1, "00").ToString() + ". " + Public2.PRJ.TX[i].Name;
					}
					else
					{
						array[num].Name = Strings.Format(i + 1, "00").ToString() + ". " + Public2.PRJ.TX[i].AntType;
					}
					if (chkAddOwner.Checked)
					{
						array[num].Name = array[num].Name + " " + Public2.PRJ.TX[i].Owner;
					}
					array[num].CoordWM = Public2.PRJ.TX[i].CoordWM;
					array[num].CoordM = Public2.PRJ.TX[i].CoordM;
				}
			}
			if (num == -1)
			{
				Public2.PRJ.VP = null;
				FillDGV1();
				return;
			}
			if (chkUseAntName.Checked)
			{
				int upperBound2 = array.GetUpperBound(0);
				for (int j = 0; j <= upperBound2; j++)
				{
					string[] array2 = array[j].Name.Split('\r');
					if (array2.Length <= 1)
					{
						continue;
					}
					array[j].Name = "";
					VN[] array3 = new VN[array2.GetUpperBound(0) + 1];
					int upperBound3 = array2.GetUpperBound(0);
					for (int k = 0; k <= upperBound3; k++)
					{
						array3[k].Name = array2[k].Substring(Strings.InStr(array2[k], ".") + 1);
						array3[k].Number = Conversions.ToString(Conversions.ToInteger(array2[k].Substring(0, Strings.InStr(array2[k], ".") - 1)));
					}
					int upperBound4 = array3.GetUpperBound(0);
					for (int l = 0; l <= upperBound4; l++)
					{
						if (Operators.CompareString(array3[l].Name, "", TextCompare: false) == 0)
						{
							continue;
						}
						if (l > 0)
						{
							array[j].Name = array[j].Name + "\r\n";
						}
						array[j].Name = array[j].Name + array3[l].Number;
						int upperBound5 = array3.GetUpperBound(0);
						for (int m = 0; m <= upperBound5; m++)
						{
							if ((l != m) & (Operators.CompareString(array3[l].Name, array3[m].Name, TextCompare: false) == 0))
							{
								array[j].Name = array[j].Name + "," + array3[m].Number;
								array3[m].Name = "";
							}
						}
						array[j].Name = array[j].Name + ". " + array3[l].Name;
						array3[l].Name = "";
					}
				}
			}
			int upperBound6 = array.GetUpperBound(0);
			for (int n = 0; n <= upperBound6; n++)
			{
				bool flag = false;
				if (!Information.IsNothing(Public2.PRJ.VP) && Operators.CompareString(array[n].Name, "", TextCompare: false) != 0)
				{
					int upperBound7 = Public2.PRJ.VP.GetUpperBound(0);
					for (int num2 = 0; num2 <= upperBound7; num2++)
					{
						if (Operators.CompareString(Public2.PRJ.VP[num2].Name, array[n].Name, TextCompare: false) == 0)
						{
							flag = true;
							array[n].H1 = Public2.PRJ.VP[num2].H1;
							array[n].H2 = Public2.PRJ.VP[num2].H2;
							array[n].L1 = Public2.PRJ.VP[num2].L1;
							array[n].L2 = Public2.PRJ.VP[num2].L2;
							break;
						}
					}
				}
				if (!flag)
				{
					array[n].H1 = 100f;
					array[n].H2 = 0f;
					array[n].L1 = 150f;
					array[n].L2 = 0f;
				}
				array[n].Activ = true;
			}
			Public2.PRJ.VP = Module1.DeepClone(array);
			FillDGV1();
		}
	}

	private void cmdAddRangeAndStep_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		DGV1.EndEdit();
		MyProject.Forms.frmAzimuthSpan.ShowDialog();
		if (!Form1_Public.AzimuthSpanOK || Form1_Public.AzimuthSpanMax - Form1_Public.AzimuthSpanMin <= 0f || Interaction.MsgBox(Localization.LocMSG(69), MsgBoxStyle.YesNo) != MsgBoxResult.Yes)
		{
			return;
		}
		List<float> list = new List<float>();
		float azimuthSpanMin = Form1_Public.AzimuthSpanMin;
		float azimuthSpanMax = Form1_Public.AzimuthSpanMax;
		float azimuthSpanDh = Form1_Public.AzimuthSpanDh;
		bool flag = azimuthSpanDh >= 0f;
		for (float num = azimuthSpanMin; (flag ? (num <= azimuthSpanMax) : (num >= azimuthSpanMax)) && num != 360f; num += azimuthSpanDh)
		{
			float item = (float)Math.Round(num, 1);
			list.Add(item);
		}
		checked
		{
			if (list.Count != 0)
			{
				short num2 = 0;
				if (!Information.IsNothing(Public2.PRJ.VP))
				{
					num2 = (short)Public2.PRJ.VP.Count();
					ref Public2.StructVertPlane[] vP = ref Public2.PRJ.VP;
					vP = (Public2.StructVertPlane[])Utils.CopyArray(vP, new Public2.StructVertPlane[Public2.PRJ.VP.GetUpperBound(0) + list.Count + 1]);
				}
				else
				{
					Public2.PRJ.VP = new Public2.StructVertPlane[list.Count - 1 + 1];
				}
				int num3 = list.Count - 1;
				for (int i = 0; i <= num3; i++)
				{
					Public2.PRJ.VP[num2 + i].Activ = true;
					Public2.PRJ.VP[num2 + i].Name = "Azimuth " + Conversions.ToString(list[i]) + "°";
					Public2.PRJ.VP[num2 + i].Azimuth = list[i];
					Public2.PRJ.VP[num2 + i].CoordWM = Public2.PRJ.SETT.OriginCoordWM;
					Public2.PRJ.VP[num2 + i].CoordM = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.VP[num2 + i].CoordWM);
					Public2.PRJ.VP[num2 + i].H1 = 100f;
					Public2.PRJ.VP[num2 + i].H2 = 0f;
					Public2.PRJ.VP[num2 + i].L1 = 150f;
					Public2.PRJ.VP[num2 + i].L2 = 0f;
				}
				FillDGV1();
			}
		}
	}

	private void cmdSORT_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Information.IsNothing(Public2.PRJ.VP))
		{
			return;
		}
		checked
		{
			int num = Public2.PRJ.VP.GetUpperBound(0) - 1;
			for (int i = 0; i <= num; i++)
			{
				int num2 = i + 1;
				int upperBound = Public2.PRJ.VP.GetUpperBound(0);
				for (int j = num2; j <= upperBound; j++)
				{
					if (Public2.PRJ.VP[j].Azimuth < Public2.PRJ.VP[i].Azimuth)
					{
						Public2.StructVertPlane obj = Module1.DeepClone(Public2.PRJ.VP[i]);
						Public2.PRJ.VP[i] = Module1.DeepClone(Public2.PRJ.VP[j]);
						Public2.PRJ.VP[j] = Module1.DeepClone(obj);
					}
					else if ((Public2.PRJ.VP[j].Azimuth == Public2.PRJ.VP[i].Azimuth) & (Math.Sqrt(Public2.PRJ.VP[j].CoordM.Xm * Public2.PRJ.VP[j].CoordM.Xm + Public2.PRJ.VP[j].CoordM.Ym * Public2.PRJ.VP[j].CoordM.Ym) < Math.Sqrt(Public2.PRJ.VP[i].CoordM.Xm * Public2.PRJ.VP[i].CoordM.Xm + Public2.PRJ.VP[i].CoordM.Ym * Public2.PRJ.VP[i].CoordM.Ym)))
					{
						Public2.StructVertPlane obj = Module1.DeepClone(Public2.PRJ.VP[i]);
						Public2.PRJ.VP[i] = Module1.DeepClone(Public2.PRJ.VP[j]);
						Public2.PRJ.VP[j] = Module1.DeepClone(obj);
					}
					else if ((Public2.PRJ.VP[j].Azimuth == Public2.PRJ.VP[i].Azimuth) & (Public2.PRJ.VP[j].CoordM.Xm == Public2.PRJ.VP[i].CoordM.Xm) & (Public2.PRJ.VP[j].CoordM.Ym == Public2.PRJ.VP[i].CoordM.Ym) & (Operators.CompareString(Public2.PRJ.VP[j].Name, Public2.PRJ.VP[i].Name, TextCompare: false) < 0))
					{
						Public2.StructVertPlane obj = Module1.DeepClone(Public2.PRJ.VP[i]);
						Public2.PRJ.VP[i] = Module1.DeepClone(Public2.PRJ.VP[j]);
						Public2.PRJ.VP[j] = Module1.DeepClone(obj);
					}
				}
			}
			FillDGV1();
		}
	}

	private void cmdStart0_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		DGV1.EndEdit();
		if (Information.IsNothing(Public2.PRJ.VP))
		{
			return;
		}
		checked
		{
			long num = (long)Math.Round(DateTime.Now.Subtract(DateTime.MinValue.AddYears(2000)).TotalMilliseconds);
			if (!CalcSZZ.CalcTmp())
			{
				return;
			}
			int num2 = 0;
			int upperBound = Public2.PRJ.VP.GetUpperBound(0);
			for (int i = 0; i <= upperBound; i++)
			{
				if (Public2.PRJ.VP[i].Activ)
				{
					Public2.PRJ.VP[i].Size = Public2.PRJ.SETT.MaxHorSize[0];
					CalcSZZ.ListHVP = (int[])Utils.CopyArray(CalcSZZ.ListHVP, new int[num2 + 1]);
					CalcSZZ.ListHVP[num2] = i;
					num2++;
				}
			}
			MyProject.Forms.frmBckGrWrkVert.ShowDialog();
			if (CalcSZZ.FlagStopCalc)
			{
				int upperBound2 = Public2.PRJ.VP.GetUpperBound(0);
				for (int j = 0; j <= upperBound2; j++)
				{
					Public2.PRJ.VP[j].LastL1 = 0f;
					Public2.PRJ.VP[j].PLNs = null;
				}
			}
			PB.Refresh();
			MyProject.Forms.Form1.lblTime.Text = Conversions.ToString(Math.Round((DateTime.Now.Subtract(DateTime.MinValue.AddYears(2000)).TotalMilliseconds - (double)num) / 1000.0, 3)) + " sec.";
		}
	}

	private void cmdStart1_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		DGV1.EndEdit();
		if (Information.IsNothing(Public2.PRJ.VP))
		{
			return;
		}
		checked
		{
			long num = (long)Math.Round(DateTime.Now.Subtract(DateTime.MinValue.AddYears(2000)).TotalMilliseconds);
			if (!CalcSZZ.CalcTmp())
			{
				return;
			}
			int num2 = 0;
			int upperBound = Public2.PRJ.VP.GetUpperBound(0);
			for (int i = 0; i <= upperBound; i++)
			{
				if (Public2.PRJ.VP[i].Activ)
				{
					Public2.PRJ.VP[i].Size = Public2.PRJ.SETT.MaxHorSize[1];
					CalcSZZ.ListHVP = (int[])Utils.CopyArray(CalcSZZ.ListHVP, new int[num2 + 1]);
					CalcSZZ.ListHVP[num2] = i;
					num2++;
				}
			}
			MyProject.Forms.frmBckGrWrkVert.ShowDialog();
			if (CalcSZZ.FlagStopCalc)
			{
				int upperBound2 = Public2.PRJ.VP.GetUpperBound(0);
				for (int j = 0; j <= upperBound2; j++)
				{
					Public2.PRJ.VP[j].LastL1 = 0f;
					Public2.PRJ.VP[j].PLNs = null;
				}
			}
			PB.Refresh();
			MyProject.Forms.Form1.lblTime.Text = Conversions.ToString(Math.Round((DateTime.Now.Subtract(DateTime.MinValue.AddYears(2000)).TotalMilliseconds - (double)num) / 1000.0, 3)) + " sec.";
		}
	}

	private void cmdStartOne0_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		DGV1.EndEdit();
		if (Information.IsNothing(Public2.PRJ.VP) || Form1_Public.VPSelRow > Public2.PRJ.VP.GetUpperBound(0))
		{
			return;
		}
		long num = checked((long)Math.Round(DateTime.Now.Subtract(DateTime.MinValue.AddYears(2000)).TotalMilliseconds));
		if (CalcSZZ.CalcTmp())
		{
			Public2.PRJ.VP[Form1_Public.VPSelRow].Size = Public2.PRJ.SETT.MaxHorSize[0];
			CalcSZZ.ListHVP = new int[1];
			CalcSZZ.ListHVP[0] = Form1_Public.VPSelRow;
			MyProject.Forms.frmBckGrWrkVert.ShowDialog();
			if (!CalcSZZ.FlagStopCalc)
			{
				PB.Refresh();
				MyProject.Forms.Form1.lblTime.Text = Conversions.ToString(Math.Round((DateTime.Now.Subtract(DateTime.MinValue.AddYears(2000)).TotalMilliseconds - (double)num) / 1000.0, 3)) + " sec.";
			}
		}
	}

	private void cmdStartOne1_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		DGV1.EndEdit();
		if (Information.IsNothing(Public2.PRJ.VP) || Form1_Public.VPSelRow > Public2.PRJ.VP.GetUpperBound(0))
		{
			return;
		}
		long num = checked((long)Math.Round(DateTime.Now.Subtract(DateTime.MinValue.AddYears(2000)).TotalMilliseconds));
		if (CalcSZZ.CalcTmp())
		{
			Public2.PRJ.VP[Form1_Public.VPSelRow].Size = Public2.PRJ.SETT.MaxHorSize[1];
			CalcSZZ.ListHVP = new int[1];
			CalcSZZ.ListHVP[0] = Form1_Public.VPSelRow;
			MyProject.Forms.frmBckGrWrkVert.ShowDialog();
			if (!CalcSZZ.FlagStopCalc)
			{
				PB.Refresh();
				MyProject.Forms.Form1.lblTime.Text = Conversions.ToString(Math.Round((DateTime.Now.Subtract(DateTime.MinValue.AddYears(2000)).TotalMilliseconds - (double)num) / 1000.0, 3)) + " sec.";
			}
		}
	}

	private void cmdAdjustLeft_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Information.IsNothing(Public2.PRJ.VP))
		{
			return;
		}
		int upperBound = Public2.PRJ.VP.GetUpperBound(0);
		checked
		{
			for (int i = 0; i <= upperBound; i++)
			{
				if (!(Public2.PRJ.VP[i].Activ & (Public2.PRJ.VP[i].LastL1 > 0f)) || Information.IsNothing(Public2.PRJ.VP[i].PLNs))
				{
					continue;
				}
				float num = -1f;
				int num2 = 0;
				do
				{
					if (Public2.PRJ.SETT.VertCutLimits[num2] && !Information.IsNothing(Public2.PRJ.VP[i].PLNs[num2]))
					{
						int upperBound2 = Public2.PRJ.VP[i].PLNs[num2].GetUpperBound(0);
						for (int j = 0; j <= upperBound2; j++)
						{
							int upperBound3 = Public2.PRJ.VP[i].PLNs[num2][j].GetUpperBound(0);
							for (int k = 0; k <= upperBound3; k++)
							{
								if (Public2.PRJ.VP[i].PLNs[num2][j][k].X > num)
								{
									num = Public2.PRJ.VP[i].PLNs[num2][j][k].X;
								}
							}
						}
					}
					num2++;
				}
				while (num2 <= 3);
				if (num > 0f)
				{
					double num3 = (Public2.PRJ.VP[i].LastL1 + Public2.PRJ.VP[i].LastL2) / (float)Public2.PRJ.VP[i].Size;
					Public2.PRJ.VP[i].L1 = (float)(Math.Ceiling(((double)num * num3 - (double)Public2.PRJ.VP[i].LastL2 + 1.0) / 10.0) * 10.0);
					FlagOK = false;
					DGV1[4, i].Value = Public2.PRJ.VP[i].L1;
					FlagOK = true;
				}
			}
		}
	}

	private void cmdAdjustUp_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Information.IsNothing(Public2.PRJ.VP))
		{
			return;
		}
		int upperBound = Public2.PRJ.VP.GetUpperBound(0);
		checked
		{
			for (int i = 0; i <= upperBound; i++)
			{
				if (!(Public2.PRJ.VP[i].Activ & (Public2.PRJ.VP[i].LastL1 > 0f)) || Information.IsNothing(Public2.PRJ.VP[i].PLNs))
				{
					continue;
				}
				float num = -1f;
				int num2 = 0;
				do
				{
					if (Public2.PRJ.SETT.VertCutLimits[num2] && !Information.IsNothing(Public2.PRJ.VP[i].PLNs[num2]))
					{
						int upperBound2 = Public2.PRJ.VP[i].PLNs[num2].GetUpperBound(0);
						for (int j = 0; j <= upperBound2; j++)
						{
							int upperBound3 = Public2.PRJ.VP[i].PLNs[num2][j].GetUpperBound(0);
							for (int k = 0; k <= upperBound3; k++)
							{
								if (Public2.PRJ.VP[i].PLNs[num2][j][k].Y > num)
								{
									num = Public2.PRJ.VP[i].PLNs[num2][j][k].Y;
								}
							}
						}
					}
					num2++;
				}
				while (num2 <= 3);
				if (num > 0f)
				{
					double num3 = (Public2.PRJ.VP[i].LastH1 + Public2.PRJ.VP[i].LastH2) / (float)Public2.PRJ.VP[i].Size;
					Public2.PRJ.VP[i].H1 = (float)(Math.Ceiling(((double)num * num3 - (double)Public2.PRJ.VP[i].LastH2 + 1.0) / 10.0) * 10.0);
					FlagOK = false;
					DGV1[6, i].Value = Public2.PRJ.VP[i].H1;
					FlagOK = true;
				}
			}
		}
	}

	private void cmdClearAllMesurePoints_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (!Information.IsNothing(Public2.PRJ.VP) && Interaction.MsgBox(Localization.LocMSG(70), MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
		{
			int upperBound = Public2.PRJ.VP.GetUpperBound(0);
			for (int i = 0; i <= upperBound; i = checked(i + 1))
			{
				Public2.PRJ.VP[i].ManualPointZO = null;
			}
			PB.Refresh();
		}
	}

	private void cmdWord_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		DGV1.EndEdit();
		ContWord.Show(this, cmdWord.Location);
	}

	private void cmdVerticalCuts_1_Click(object sender, EventArgs e)
	{
		if (Information.IsNothing(Public2.PRJ.VP))
		{
			Interaction.MsgBox(Localization.LocMSG(66));
		}
		else
		{
			RepWordAlias.WordReportOneAlias("{VerticalCuts_1}");
		}
	}

	private void chkLengthZO_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		if (!FlagProcessing)
		{
			Public2.PRJ.SETT.ShowLengthZO = chkLengthZO.Checked;
			PB.Refresh();
		}
	}

	private void chkHgtBottomZO_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		if (!FlagProcessing)
		{
			Public2.PRJ.SETT.ShowHgtBottomZO = chkHgtBottomZO.Checked;
			PB.Refresh();
		}
	}

	private void chkHgtMaxZO_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		if (!FlagProcessing)
		{
			Public2.PRJ.SETT.ShowHgtMaxZO = chkHgtMaxZO.Checked;
			PB.Refresh();
		}
	}

	private void chkHgtTopZO_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		if (!FlagProcessing)
		{
			Public2.PRJ.SETT.ShowHgtTopZO = chkHgtTopZO.Checked;
			PB.Refresh();
		}
	}

	private void chkShowManualPointsZO_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		if (!FlagProcessing)
		{
			Public2.PRJ.SETT.ShowManualPointsZO = chkShowManualPointsZO.Checked;
			PB.Refresh();
		}
	}

	private void chkShowMinDistToZO_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		if (!FlagProcessing)
		{
			Public2.PRJ.SETT.ShowMinDistToZO = chkShowMinDistToZO.Checked;
			PB.Refresh();
		}
	}

	private void chkchkShowKPonVert_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		if (!FlagProcessing)
		{
			Public2.PRJ.SETT.ShowKPonVert = chkShowKPonVert.Checked;
			PB.Refresh();
		}
	}

	private void chkShowPlus2m_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		if (!FlagProcessing)
		{
			Public2.PRJ.SETT.ShowPlus2m = chkShowPlus2m.Checked;
			PB.Refresh();
		}
	}

	private void chkLimit0_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		if (!FlagProcessing)
		{
			Public2.PRJ.SETT.VertCutLimits[0] = chkLimit0.Checked;
			PB.Refresh();
		}
	}

	private void chkLimit1_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		if (!FlagProcessing)
		{
			Public2.PRJ.SETT.VertCutLimits[1] = chkLimit1.Checked;
			PB.Refresh();
		}
	}

	private void chkLimit2_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		if (!FlagProcessing)
		{
			Public2.PRJ.SETT.VertCutLimits[2] = chkLimit2.Checked;
			PB.Refresh();
		}
	}

	private void chkLimit3_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		if (!FlagProcessing)
		{
			Public2.PRJ.SETT.VertCutLimits[3] = chkLimit3.Checked;
			PB.Refresh();
		}
	}

	private void txtLineHightAboveTerrain_ValueChanged(object sender, EventArgs e)
	{
		if (!FlagProcessing)
		{
			GroupBox1.Focus();
			Public2.PRJ.SETT.LineHightAboveTerrain = Convert.ToSingle(txtLineHightAboveTerrain.Value);
			PB.Refresh();
		}
	}

	private void txtTransparFillVertCut_ValueChanged(object sender, EventArgs e)
	{
		if (!FlagProcessing)
		{
			GroupBox1.Focus();
			Public2.PRJ.SETT.TransparFillVertCut = Convert.ToByte(txtTransparFillVertCut.Value);
			PB.Refresh();
		}
	}

	private void txtLineWidthVertCut_ValueChanged(object sender, EventArgs e)
	{
		if (!FlagProcessing)
		{
			GroupBox1.Focus();
			Public2.PRJ.SETT.LineWidthVertCut = Convert.ToByte(txtLineWidthVertCut.Value);
			PB.Refresh();
		}
	}
}
