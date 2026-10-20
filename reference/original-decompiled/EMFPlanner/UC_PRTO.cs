using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using EMFPlanner.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.FileIO;

namespace EMFPlanner;

[DesignerGenerated]
public class UC_PRTO : UserControl
{
	private IContainer components;

	private bool FlagOK;

	public short SelRow;

	private float DbCenter;

	public const double PI180 = 180.0 / Math.PI;

	private string LblName;

	[field: AccessedThroughProperty("gb1")]
	internal virtual GroupBox gb1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdVertPatternDown")]
	internal virtual Button cmdVertPatternDown
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdVertPatternDown_Click;
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

	[field: AccessedThroughProperty("cmdVertPatternUp")]
	internal virtual Button cmdVertPatternUp
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdVertPatternUp_Click;
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

	[field: AccessedThroughProperty("PB1")]
	internal virtual PictureBox PB1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			PaintEventHandler value2 = PB1_Paint;
			PictureBox pictureBox = field;
			if (pictureBox != null)
			{
				pictureBox.Paint -= value2;
			}
			field = value;
			pictureBox = field;
			if (pictureBox != null)
			{
				pictureBox.Paint += value2;
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

	[field: AccessedThroughProperty("DGV1")]
	internal virtual DataGridView DGV1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			DataGridViewCellEventHandler value2 = DGV1_CellEnter;
			DataGridViewCellEventHandler value3 = DGV1_CellValueChanged;
			DataGridViewCellMouseEventHandler value4 = DGV1_CellMouseDown;
			DataGridViewRowEventHandler value5 = DGV1_UserAddedRow;
			MouseEventHandler value6 = DGV1_MouseDoubleClick;
			DataGridViewCellMouseEventHandler value7 = DGV1_CellMouseClick;
			MouseEventHandler value8 = DGV1_MouseClick;
			KeyEventHandler value9 = DGV1_KeyDown;
			DataGridViewCellMouseEventHandler value10 = DGV1_CellMouseUp;
			DataGridView dataGridView = field;
			if (dataGridView != null)
			{
				dataGridView.CellEnter -= value2;
				dataGridView.CellValueChanged -= value3;
				dataGridView.CellMouseDown -= value4;
				dataGridView.UserAddedRow -= value5;
				dataGridView.MouseDoubleClick -= value6;
				dataGridView.CellMouseClick -= value7;
				dataGridView.MouseClick -= value8;
				dataGridView.KeyDown -= value9;
				dataGridView.CellMouseUp -= value10;
			}
			field = value;
			dataGridView = field;
			if (dataGridView != null)
			{
				dataGridView.CellEnter += value2;
				dataGridView.CellValueChanged += value3;
				dataGridView.CellMouseDown += value4;
				dataGridView.UserAddedRow += value5;
				dataGridView.MouseDoubleClick += value6;
				dataGridView.CellMouseClick += value7;
				dataGridView.MouseClick += value8;
				dataGridView.KeyDown += value9;
				dataGridView.CellMouseUp += value10;
			}
		}
	}

	[field: AccessedThroughProperty("cmdPasteFromBuffer")]
	internal virtual Button cmdPasteFromBuffer
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdPasteFromBuffer_Click;
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

	[field: AccessedThroughProperty("cmdCopyToBuffer")]
	internal virtual Button cmdCopyToBuffer
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdCopyToBuffer_Click;
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

	[field: AccessedThroughProperty("cmdCopySelectedTX")]
	internal virtual Button cmdCopySelectedTX
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdCopySelectedTX_Click;
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

	[field: AccessedThroughProperty("ContDBW")]
	internal virtual ContextMenuStrip ContDBW
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ВтToolStripMenuItem")]
	internal virtual ToolStripMenuItem ВтToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ДБВтToolStripMenuItem")]
	internal virtual ToolStripMenuItem ДБВтToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ДБмВтToolStripMenuItem")]
	internal virtual ToolStripMenuItem ДБмВтToolStripMenuItem
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ДБмВтToolStripMenuItem1")]
	internal virtual ToolStripMenuItem ДБмВтToolStripMenuItem1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ContExist")]
	internal virtual ContextMenuStrip ContExist
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			ToolStripItemClickedEventHandler value2 = ContExist_ItemClicked;
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

	[field: AccessedThroughProperty("ToolStripMenuItem5")]
	internal virtual ToolStripMenuItem ToolStripMenuItem5
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ToolStripMenuItem6")]
	internal virtual ToolStripMenuItem ToolStripMenuItem6
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ContOwner")]
	internal virtual ContextMenuStrip ContOwner
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			ToolStripItemClickedEventHandler value2 = ContOwner_ItemClicked;
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

	[field: AccessedThroughProperty("ToolTip1")]
	internal virtual ToolTip ToolTip1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdColor1")]
	internal virtual Button cmdColor1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdColorAll_Click;
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

	[field: AccessedThroughProperty("cmdColor2")]
	internal virtual Button cmdColor2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdColorAll_Click;
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

	[field: AccessedThroughProperty("cmdColor4")]
	internal virtual Button cmdColor4
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdColorAll_Click;
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

	[field: AccessedThroughProperty("cmdColor3")]
	internal virtual Button cmdColor3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdColorAll_Click;
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

	[field: AccessedThroughProperty("cmdColor8")]
	internal virtual Button cmdColor8
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdColorAll_Click;
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

	[field: AccessedThroughProperty("cmdColor7")]
	internal virtual Button cmdColor7
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdColorAll_Click;
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

	[field: AccessedThroughProperty("cmdColor6")]
	internal virtual Button cmdColor6
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdColorAll_Click;
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

	[field: AccessedThroughProperty("cmdColor5")]
	internal virtual Button cmdColor5
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdColorAll_Click;
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

	[field: AccessedThroughProperty("cmdColor0")]
	internal virtual Button cmdColor0
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdColorAll_Click;
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

	[field: AccessedThroughProperty("cmdColor11")]
	internal virtual Button cmdColor11
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdColorAll_Click;
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

	[field: AccessedThroughProperty("cmdColor10")]
	internal virtual Button cmdColor10
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdColorAll_Click;
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

	[field: AccessedThroughProperty("cmdColor9")]
	internal virtual Button cmdColor9
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdColorAll_Click;
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

	[field: AccessedThroughProperty("GB2")]
	internal virtual GroupBox GB2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblEIM")]
	internal virtual Label lblEIM
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = lblEIM_Click;
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

	[field: AccessedThroughProperty("lblPant")]
	internal virtual Label lblPant
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = lblPant_Click;
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

	[field: AccessedThroughProperty("Label10")]
	internal virtual Label Label10
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

	[field: AccessedThroughProperty("lblP")]
	internal virtual Label lblP
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = lblP_Click;
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

	[field: AccessedThroughProperty("txtP")]
	internal virtual TextBox txtP
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

	[field: AccessedThroughProperty("txtN")]
	internal virtual TextBox txtN
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtEIM")]
	internal virtual TextBox txtEIM
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

	[field: AccessedThroughProperty("Label8")]
	internal virtual Label Label8
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtPant")]
	internal virtual TextBox txtPant
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtNu")]
	internal virtual TextBox txtNu
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

	[field: AccessedThroughProperty("txtG")]
	internal virtual TextBox txtG
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label12")]
	internal virtual Label Label12
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

	[field: AccessedThroughProperty("cmdEditorDN")]
	internal virtual Button cmdEditorDN
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdEditorDN_Click;
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

	[field: AccessedThroughProperty("cmdFillWidthDN")]
	internal virtual Button cmdFillWidthDN
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdFillWidthDN_Click;
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

	[field: AccessedThroughProperty("cmdSaveMSI")]
	internal virtual Button cmdSaveMSI
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdSaveMSI_Click;
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

	[field: AccessedThroughProperty("cmdLoadDN2")]
	internal virtual Button cmdLoadDN2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdLoadDN2_Click;
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

	[field: AccessedThroughProperty("cmdLoadDN")]
	internal virtual Button cmdLoadDN
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdLoadDN_Click;
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

	[field: AccessedThroughProperty("cmsDBW")]
	internal virtual ContextMenuStrip cmsDBW
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			ToolStripItemClickedEventHandler value2 = cmsDBW_ItemClicked;
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

	[field: AccessedThroughProperty("ToolStripMenuItem9")]
	internal virtual ToolStripMenuItem ToolStripMenuItem9
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ToolStripMenuItem11")]
	internal virtual ToolStripMenuItem ToolStripMenuItem11
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ToolStripMenuItem12")]
	internal virtual ToolStripMenuItem ToolStripMenuItem12
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ToolStripMenuItem13")]
	internal virtual ToolStripMenuItem ToolStripMenuItem13
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdCalcEIM")]
	internal virtual Button cmdCalcEIM
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdCalcEIM_Click;
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

	[field: AccessedThroughProperty("cmdCalcNu")]
	internal virtual Button cmdCalcNu
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdCalcNu_Click;
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

	[field: AccessedThroughProperty("cmdCalcP")]
	internal virtual Button cmdCalcP
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdCalcP_Click;
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

	[field: AccessedThroughProperty("OFD1")]
	internal virtual OpenFileDialog OFD1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("SFD1")]
	internal virtual SaveFileDialog SFD1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdCheckUncheck")]
	internal virtual Button cmdCheckUncheck
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdCheckUncheck_Click;
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

	[field: AccessedThroughProperty("ToolStripMenuItem16")]
	internal virtual ToolStripMenuItem ToolStripMenuItem16
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdFillWidthDnOne")]
	internal virtual Button cmdFillWidthDnOne
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdFillWidthDnOne_Click;
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

	[field: AccessedThroughProperty("cmdRadiatingSources_1")]
	internal virtual ToolStripMenuItem cmdRadiatingSources_1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdRadiatingSources_1_Click;
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

	[field: AccessedThroughProperty("cmdAntennaPatterns")]
	internal virtual ToolStripMenuItem cmdAntennaPatterns
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdAntennaPatterns_Click;
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

	[field: AccessedThroughProperty("cmdMoveTop")]
	internal virtual Button cmdMoveTop
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdMoveTop_Click;
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

	[field: AccessedThroughProperty("cmdMoveBottom")]
	internal virtual Button cmdMoveBottom
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdMoveBottom_Click;
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

	[field: AccessedThroughProperty("cmdExcel1")]
	internal virtual ToolStripMenuItem cmdExcel1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdExcel1_Click;
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

	[field: AccessedThroughProperty("GB3")]
	internal virtual GroupBox GB3
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
			DataGridViewCellEventHandler value2 = DGV2_CellValidated;
			DataGridView dataGridView = field;
			if (dataGridView != null)
			{
				dataGridView.CellValidated -= value2;
			}
			field = value;
			dataGridView = field;
			if (dataGridView != null)
			{
				dataGridView.CellValidated += value2;
			}
		}
	}

	[field: AccessedThroughProperty("Column11")]
	internal virtual DataGridViewTextBoxColumn Column11
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Column12")]
	internal virtual DataGridViewTextBoxColumn Column12
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Column13")]
	internal virtual DataGridViewTextBoxColumn Column13
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Column14")]
	internal virtual DataGridViewTextBoxColumn Column14
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdEnvelope")]
	internal virtual Button cmdEnvelope
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdEnvelope_Click;
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

	[field: AccessedThroughProperty("cmdApert")]
	internal virtual Button cmdApert
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdApert_Click;
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

	[field: AccessedThroughProperty("cmdEMFSafetyVolumes1")]
	internal virtual ToolStripMenuItem cmdEMFSafetyVolumes1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdEMFSafetyVolumes1_Click;
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

	[field: AccessedThroughProperty("cmdImportRadiatingSources")]
	internal virtual ToolStripMenuItem cmdImportRadiatingSources
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdImportRadiatingSources_Click;
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

	[field: AccessedThroughProperty("Column9")]
	internal virtual DataGridViewCheckBoxColumn Column9
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

	[field: AccessedThroughProperty("Column20")]
	internal virtual DataGridViewTextBoxColumn Column20
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("F")]
	internal virtual DataGridViewTextBoxColumn F
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

	[field: AccessedThroughProperty("Column1")]
	internal virtual DataGridViewTextBoxColumn Column1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Column10")]
	internal virtual DataGridViewTextBoxColumn Column10
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

	[field: AccessedThroughProperty("Column6")]
	internal virtual DataGridViewTextBoxColumn Column6
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

	[field: AccessedThroughProperty("Column8")]
	internal virtual DataGridViewTextBoxColumn Column8
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Column24")]
	internal virtual DataGridViewTextBoxColumn Column24
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Column16")]
	internal virtual DataGridViewTextBoxColumn Column16
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Column17")]
	internal virtual DataGridViewTextBoxColumn Column17
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Column23")]
	internal virtual DataGridViewTextBoxColumn Column23
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Column21")]
	internal virtual DataGridViewTextBoxColumn Column21
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Column18")]
	internal virtual DataGridViewTextBoxColumn Column18
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Column15")]
	internal virtual DataGridViewTextBoxColumn Column15
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Column19")]
	internal virtual DataGridViewTextBoxColumn Column19
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public UC_PRTO()
	{
		ControlAdded += UC_PRTO_ControlAdded;
		Load += UC_PRTO_Load;
		DbCenter = 30f;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EMFPlanner.UC_PRTO));
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle17 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle18 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle19 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle20 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle21 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle22 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle23 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle24 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle25 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle26 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle27 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle28 = new System.Windows.Forms.DataGridViewCellStyle();
		this.gb1 = new System.Windows.Forms.GroupBox();
		this.cmdApert = new System.Windows.Forms.Button();
		this.cmdEnvelope = new System.Windows.Forms.Button();
		this.cmdSaveMSI = new System.Windows.Forms.Button();
		this.cmdLoadDN2 = new System.Windows.Forms.Button();
		this.cmdLoadDN = new System.Windows.Forms.Button();
		this.cmdVertPatternDown = new System.Windows.Forms.Button();
		this.cmdVertPatternUp = new System.Windows.Forms.Button();
		this.PB1 = new System.Windows.Forms.PictureBox();
		this.cmdEditorDN = new System.Windows.Forms.Button();
		this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
		this.DGV1 = new System.Windows.Forms.DataGridView();
		this.Column9 = new System.Windows.Forms.DataGridViewCheckBoxColumn();
		this.DataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column20 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.F = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column24 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column16 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column17 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column23 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column21 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column18 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column15 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column19 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.GB3 = new System.Windows.Forms.GroupBox();
		this.DGV2 = new System.Windows.Forms.DataGridView();
		this.Column11 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column12 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column13 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column14 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.GB2 = new System.Windows.Forms.GroupBox();
		this.txtP = new System.Windows.Forms.TextBox();
		this.txtN = new System.Windows.Forms.TextBox();
		this.txtEIM = new System.Windows.Forms.TextBox();
		this.txtPant = new System.Windows.Forms.TextBox();
		this.txtNu = new System.Windows.Forms.TextBox();
		this.txtG = new System.Windows.Forms.TextBox();
		this.cmdCalcEIM = new System.Windows.Forms.Button();
		this.cmdCalcNu = new System.Windows.Forms.Button();
		this.cmdCalcP = new System.Windows.Forms.Button();
		this.Label1 = new System.Windows.Forms.Label();
		this.lblEIM = new System.Windows.Forms.Label();
		this.lblPant = new System.Windows.Forms.Label();
		this.Label10 = new System.Windows.Forms.Label();
		this.Label9 = new System.Windows.Forms.Label();
		this.lblP = new System.Windows.Forms.Label();
		this.Label6 = new System.Windows.Forms.Label();
		this.Label7 = new System.Windows.Forms.Label();
		this.Label8 = new System.Windows.Forms.Label();
		this.Label11 = new System.Windows.Forms.Label();
		this.Label12 = new System.Windows.Forms.Label();
		this.ContDBW = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.ВтToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ДБВтToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ДБмВтToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
		this.ДБмВтToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
		this.ContExist = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.ToolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
		this.ContOwner = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.cmdColor1 = new System.Windows.Forms.Button();
		this.cmdColor2 = new System.Windows.Forms.Button();
		this.cmdColor4 = new System.Windows.Forms.Button();
		this.cmdColor3 = new System.Windows.Forms.Button();
		this.cmdColor8 = new System.Windows.Forms.Button();
		this.cmdColor7 = new System.Windows.Forms.Button();
		this.cmdColor6 = new System.Windows.Forms.Button();
		this.cmdColor5 = new System.Windows.Forms.Button();
		this.cmdColor0 = new System.Windows.Forms.Button();
		this.cmdColor11 = new System.Windows.Forms.Button();
		this.cmdColor10 = new System.Windows.Forms.Button();
		this.cmdColor9 = new System.Windows.Forms.Button();
		this.cmdExcel = new System.Windows.Forms.Button();
		this.cmdMoveBottom = new System.Windows.Forms.Button();
		this.cmdMoveTop = new System.Windows.Forms.Button();
		this.cmdWord = new System.Windows.Forms.Button();
		this.cmdFillWidthDnOne = new System.Windows.Forms.Button();
		this.cmdFillWidthDN = new System.Windows.Forms.Button();
		this.cmdCheckUncheck = new System.Windows.Forms.Button();
		this.cmdPasteFromBuffer = new System.Windows.Forms.Button();
		this.cmdCopyToBuffer = new System.Windows.Forms.Button();
		this.cmdUp = new System.Windows.Forms.Button();
		this.cmdDown = new System.Windows.Forms.Button();
		this.cmdCopySelectedTX = new System.Windows.Forms.Button();
		this.cmsDBW = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.ToolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
		this.OFD1 = new System.Windows.Forms.OpenFileDialog();
		this.SFD1 = new System.Windows.Forms.SaveFileDialog();
		this.ContLocation = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.ToolStripMenuItem14 = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripMenuItem15 = new System.Windows.Forms.ToolStripMenuItem();
		this.ToolStripMenuItem16 = new System.Windows.Forms.ToolStripMenuItem();
		this.ContWord = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.cmdRadiatingSources_1 = new System.Windows.Forms.ToolStripMenuItem();
		this.cmdAntennaPatterns = new System.Windows.Forms.ToolStripMenuItem();
		this.cmdEMFSafetyVolumes1 = new System.Windows.Forms.ToolStripMenuItem();
		this.cmdCardinalPointsChart = new System.Windows.Forms.ToolStripMenuItem();
		this.ContExcel = new System.Windows.Forms.ContextMenuStrip(this.components);
		this.cmdExcel1 = new System.Windows.Forms.ToolStripMenuItem();
		this.cmdImportRadiatingSources = new System.Windows.Forms.ToolStripMenuItem();
		this.gb1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.PB1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.SplitContainer1).BeginInit();
		this.SplitContainer1.Panel1.SuspendLayout();
		this.SplitContainer1.Panel2.SuspendLayout();
		this.SplitContainer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.DGV1).BeginInit();
		this.GB3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.DGV2).BeginInit();
		this.GB2.SuspendLayout();
		this.ContDBW.SuspendLayout();
		this.ContExist.SuspendLayout();
		this.cmsDBW.SuspendLayout();
		this.ContLocation.SuspendLayout();
		this.ContWord.SuspendLayout();
		this.ContExcel.SuspendLayout();
		base.SuspendLayout();
		this.gb1.Controls.Add(this.cmdApert);
		this.gb1.Controls.Add(this.cmdEnvelope);
		this.gb1.Controls.Add(this.cmdSaveMSI);
		this.gb1.Controls.Add(this.cmdLoadDN2);
		this.gb1.Controls.Add(this.cmdLoadDN);
		this.gb1.Controls.Add(this.cmdVertPatternDown);
		this.gb1.Controls.Add(this.cmdVertPatternUp);
		this.gb1.Controls.Add(this.PB1);
		this.gb1.Controls.Add(this.cmdEditorDN);
		this.gb1.Location = new System.Drawing.Point(2, 3);
		this.gb1.Name = "gb1";
		this.gb1.Size = new System.Drawing.Size(196, 253);
		this.gb1.TabIndex = 0;
		this.gb1.TabStop = false;
		this.gb1.Text = "Antenna Pattern";
		this.cmdApert.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdApert.Image = (System.Drawing.Image)resources.GetObject("cmdApert.Image");
		this.cmdApert.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdApert.Location = new System.Drawing.Point(87, 21);
		this.cmdApert.Name = "cmdApert";
		this.cmdApert.Size = new System.Drawing.Size(26, 26);
		this.cmdApert.TabIndex = 151;
		this.cmdApert.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdApert, "Create pattern for rotationally symmetrical antenna using Rec. ITU-R F.699-8");
		this.cmdApert.UseVisualStyleBackColor = true;
		this.cmdEnvelope.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdEnvelope.Image = (System.Drawing.Image)resources.GetObject("cmdEnvelope.Image");
		this.cmdEnvelope.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdEnvelope.Location = new System.Drawing.Point(60, 21);
		this.cmdEnvelope.Name = "cmdEnvelope";
		this.cmdEnvelope.Size = new System.Drawing.Size(26, 26);
		this.cmdEnvelope.TabIndex = 150;
		this.cmdEnvelope.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdEnvelope, "Create envelope of antenna patterns");
		this.cmdEnvelope.UseVisualStyleBackColor = true;
		this.cmdSaveMSI.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdSaveMSI.Image = (System.Drawing.Image)resources.GetObject("cmdSaveMSI.Image");
		this.cmdSaveMSI.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdSaveMSI.Location = new System.Drawing.Point(114, 21);
		this.cmdSaveMSI.Name = "cmdSaveMSI";
		this.cmdSaveMSI.Size = new System.Drawing.Size(26, 26);
		this.cmdSaveMSI.TabIndex = 147;
		this.cmdSaveMSI.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdSaveMSI, "Save antenna pattern as MSI file");
		this.cmdSaveMSI.UseVisualStyleBackColor = true;
		this.cmdLoadDN2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdLoadDN2.Image = (System.Drawing.Image)resources.GetObject("cmdLoadDN2.Image");
		this.cmdLoadDN2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdLoadDN2.Location = new System.Drawing.Point(33, 21);
		this.cmdLoadDN2.Name = "cmdLoadDN2";
		this.cmdLoadDN2.Size = new System.Drawing.Size(26, 26);
		this.cmdLoadDN2.TabIndex = 146;
		this.cmdLoadDN2.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdLoadDN2, "Select antenna pattern from the list");
		this.cmdLoadDN2.UseVisualStyleBackColor = true;
		this.cmdLoadDN.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdLoadDN.Image = (System.Drawing.Image)resources.GetObject("cmdLoadDN.Image");
		this.cmdLoadDN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdLoadDN.Location = new System.Drawing.Point(6, 21);
		this.cmdLoadDN.Name = "cmdLoadDN";
		this.cmdLoadDN.Size = new System.Drawing.Size(26, 26);
		this.cmdLoadDN.TabIndex = 145;
		this.cmdLoadDN.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdLoadDN, "Load antenna pattern");
		this.cmdLoadDN.UseVisualStyleBackColor = true;
		this.cmdVertPatternDown.Location = new System.Drawing.Point(168, 223);
		this.cmdVertPatternDown.Name = "cmdVertPatternDown";
		this.cmdVertPatternDown.Size = new System.Drawing.Size(22, 22);
		this.cmdVertPatternDown.TabIndex = 41;
		this.cmdVertPatternDown.Text = "-";
		this.ToolTip1.SetToolTip(this.cmdVertPatternDown, "Rotate the vertical antenna pattern down 1 degree");
		this.cmdVertPatternDown.UseVisualStyleBackColor = true;
		this.cmdVertPatternUp.Location = new System.Drawing.Point(168, 59);
		this.cmdVertPatternUp.Name = "cmdVertPatternUp";
		this.cmdVertPatternUp.Size = new System.Drawing.Size(22, 22);
		this.cmdVertPatternUp.TabIndex = 40;
		this.cmdVertPatternUp.Text = "+";
		this.ToolTip1.SetToolTip(this.cmdVertPatternUp, "Rotate the vertical antenna pattern up 1 degree");
		this.cmdVertPatternUp.UseVisualStyleBackColor = true;
		this.PB1.Location = new System.Drawing.Point(2, 57);
		this.PB1.Name = "PB1";
		this.PB1.Size = new System.Drawing.Size(190, 190);
		this.PB1.TabIndex = 3;
		this.PB1.TabStop = false;
		this.cmdEditorDN.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdEditorDN.Image = (System.Drawing.Image)resources.GetObject("cmdEditorDN.Image");
		this.cmdEditorDN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdEditorDN.Location = new System.Drawing.Point(141, 21);
		this.cmdEditorDN.Name = "cmdEditorDN";
		this.cmdEditorDN.Size = new System.Drawing.Size(26, 26);
		this.cmdEditorDN.TabIndex = 149;
		this.cmdEditorDN.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdEditorDN, "Launch Antenna Pattern Editor");
		this.cmdEditorDN.UseVisualStyleBackColor = true;
		this.SplitContainer1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
		this.SplitContainer1.IsSplitterFixed = true;
		this.SplitContainer1.Location = new System.Drawing.Point(0, 29);
		this.SplitContainer1.Name = "SplitContainer1";
		this.SplitContainer1.Panel1.Controls.Add(this.DGV1);
		this.SplitContainer1.Panel1MinSize = 300;
		this.SplitContainer1.Panel2.Controls.Add(this.GB3);
		this.SplitContainer1.Panel2.Controls.Add(this.GB2);
		this.SplitContainer1.Panel2.Controls.Add(this.gb1);
		this.SplitContainer1.Panel2MinSize = 150;
		this.SplitContainer1.Size = new System.Drawing.Size(1552, 761);
		this.SplitContainer1.SplitterDistance = 1350;
		this.SplitContainer1.TabIndex = 6;
		this.DGV1.AllowUserToDeleteRows = false;
		this.DGV1.AllowUserToOrderColumns = true;
		this.DGV1.AllowUserToResizeColumns = false;
		this.DGV1.AllowUserToResizeRows = false;
		this.DGV1.BackgroundColor = System.Drawing.SystemColors.Menu;
		this.DGV1.BorderStyle = System.Windows.Forms.BorderStyle.None;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.DGV1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.DGV1.ColumnHeadersHeight = 45;
		this.DGV1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.DGV1.Columns.AddRange(this.Column9, this.DataGridViewTextBoxColumn1, this.Column20, this.F, this.Column4, this.Column1, this.Column10, this.Column2, this.Column7, this.Column5, this.Column6, this.Column3, this.Column8, this.Column24, this.Column16, this.Column17, this.Column23, this.Column21, this.Column18, this.Column15, this.Column19);
		this.DGV1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.DGV1.Location = new System.Drawing.Point(0, 0);
		this.DGV1.Name = "DGV1";
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
		dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.DGV1.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
		this.DGV1.RowHeadersWidth = 55;
		this.DGV1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
		this.DGV1.RowTemplate.Height = 18;
		this.DGV1.Size = new System.Drawing.Size(1350, 761);
		this.DGV1.TabIndex = 2;
		this.Column9.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
		this.Column9.FillWeight = 10f;
		this.Column9.Frozen = true;
		this.Column9.HeaderText = "";
		this.Column9.Name = "Column9";
		this.Column9.ToolTipText = "Include in calculation";
		this.Column9.Width = 21;
		this.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
		this.DataGridViewTextBoxColumn1.Frozen = true;
		this.DataGridViewTextBoxColumn1.HeaderText = "Radiating source name";
		this.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1";
		this.DataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.DataGridViewTextBoxColumn1.ToolTipText = "Radiating source name";
		this.DataGridViewTextBoxColumn1.Width = 58;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Column20.DefaultCellStyle = dataGridViewCellStyle3;
		this.Column20.HeaderText = "Type of service";
		this.Column20.Name = "Column20";
		this.Column20.Resizable = System.Windows.Forms.DataGridViewTriState.True;
		this.Column20.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column20.ToolTipText = "Type of service (this text field is only used for the reports, you can leave it empty)";
		this.Column20.Width = 63;
		this.F.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle4.NullValue = "1000";
		this.F.DefaultCellStyle = dataGridViewCellStyle4;
		this.F.HeaderText = "Frequency, MHz";
		this.F.Name = "F";
		this.F.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.F.ToolTipText = "Frequency or frequency band, MHz";
		this.F.Width = 73;
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle5.NullValue = "0";
		this.Column4.DefaultCellStyle = dataGridViewCellStyle5;
		this.Column4.HeaderText = "Transmitter power, W";
		this.Column4.Name = "Column4";
		this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column4.ToolTipText = "Transmitter power, W";
		this.Column4.Width = 57;
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle6.NullValue = "1";
		this.Column1.DefaultCellStyle = dataGridViewCellStyle6;
		this.Column1.HeaderText = "Number of TX";
		this.Column1.Name = "Column1";
		this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column1.ToolTipText = "Number of transmitters";
		this.Column1.Width = 42;
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle7.NullValue = "0";
		this.Column10.DefaultCellStyle = dataGridViewCellStyle7;
		this.Column10.HeaderText = "Total atten., dB";
		this.Column10.Name = "Column10";
		this.Column10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column10.ToolTipText = "Total attenuation, dB";
		this.Column10.Width = 42;
		this.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		this.Column2.DefaultCellStyle = dataGridViewCellStyle8;
		this.Column2.HeaderText = "Antenna model";
		this.Column2.Name = "Column2";
		this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column2.ToolTipText = "Transmitting antenna model";
		this.Column2.Width = 66;
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle9.NullValue = "0";
		this.Column7.DefaultCellStyle = dataGridViewCellStyle9;
		this.Column7.HeaderText = "Antenna azimuth, deg";
		this.Column7.Name = "Column7";
		this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column7.ToolTipText = "Antenna azimuth, deg";
		this.Column7.Width = 50;
		dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle10.NullValue = "0";
		this.Column5.DefaultCellStyle = dataGridViewCellStyle10;
		this.Column5.HeaderText = "Antenna gain, dBi";
		this.Column5.Name = "Column5";
		this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column5.ToolTipText = "Antenna gain, dBi";
		this.Column5.Width = 55;
		dataGridViewCellStyle11.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle11.NullValue = "0";
		this.Column6.DefaultCellStyle = dataGridViewCellStyle11;
		this.Column6.HeaderText = "Antenna height, m";
		this.Column6.Name = "Column6";
		this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column6.ToolTipText = "Antenna height, m";
		this.Column6.Width = 50;
		dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle12.NullValue = "1";
		this.Column3.DefaultCellStyle = dataGridViewCellStyle12;
		this.Column3.HeaderText = "Antenna size, m";
		this.Column3.Name = "Column3";
		this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column3.ToolTipText = "Maximum antenna size, m";
		this.Column3.Width = 42;
		dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle13.NullValue = "0";
		this.Column8.DefaultCellStyle = dataGridViewCellStyle13;
		this.Column8.HeaderText = "Mech. downtilt, deg";
		this.Column8.Name = "Column8";
		this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column8.ToolTipText = "Mechanical downtilt, deg. Downtilt - negative, uptilt - positive.";
		this.Column8.Width = 45;
		dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle14.NullValue = "0";
		this.Column24.DefaultCellStyle = dataGridViewCellStyle14;
		this.Column24.HeaderText = "Electrical downtilt, deg";
		this.Column24.Name = "Column24";
		this.Column24.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column24.ToolTipText = "Electrical downtilt , deg. This value is only used for the reports. Downtilt - negative, uptilt - positive.";
		this.Column24.Width = 45;
		dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Column16.DefaultCellStyle = dataGridViewCellStyle15;
		this.Column16.HeaderText = "Antenna BW H-plane";
		this.Column16.Name = "Column16";
		this.Column16.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column16.ToolTipText = "Antenna beamwidth in the H-plane, deg. This value is only used for the reports";
		this.Column16.Width = 45;
		dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Column17.DefaultCellStyle = dataGridViewCellStyle16;
		this.Column17.HeaderText = "Antenna BW V-plane";
		this.Column17.Name = "Column17";
		this.Column17.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column17.ToolTipText = "Antenna beamwidth in the V-plane, deg. This value is only used for the reports";
		this.Column17.Width = 45;
		dataGridViewCellStyle17.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle17.NullValue = "Tower";
		this.Column23.DefaultCellStyle = dataGridViewCellStyle17;
		this.Column23.HeaderText = "Location";
		this.Column23.Name = "Column23";
		this.Column23.ReadOnly = true;
		this.Column23.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column23.ToolTipText = "Location (Communication tower, Building roof, Building facade)";
		this.Column23.Width = 50;
		dataGridViewCellStyle18.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle18.NullValue = "Existing";
		this.Column21.DefaultCellStyle = dataGridViewCellStyle18;
		this.Column21.HeaderText = "New or existing";
		this.Column21.Name = "Column21";
		this.Column21.ReadOnly = true;
		this.Column21.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column21.ToolTipText = "Existing/New Source";
		this.Column21.Width = 50;
		this.Column18.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
		dataGridViewCellStyle19.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		this.Column18.DefaultCellStyle = dataGridViewCellStyle19;
		this.Column18.HeaderText = "Owner";
		this.Column18.Name = "Column18";
		this.Column18.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column18.ToolTipText = "Owner";
		this.Column18.Width = 39;
		dataGridViewCellStyle20.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle20.NullValue = "0.0000";
		this.Column15.DefaultCellStyle = dataGridViewCellStyle20;
		this.Column15.HeaderText = "Power at antenna port, W";
		this.Column15.Name = "Column15";
		this.Column15.ReadOnly = true;
		this.Column15.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column15.ToolTipText = "Total power at antenna port excluding attenuation factors (calculated value, cannot be changed)";
		this.Column15.Width = 48;
		this.Column19.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
		dataGridViewCellStyle21.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Column19.DefaultCellStyle = dataGridViewCellStyle21;
		this.Column19.HeaderText = "Sector ID";
		this.Column19.Name = "Column19";
		this.Column19.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column19.ToolTipText = "Sector ID (this text field is only used for the reports, you can leave it empty)";
		this.Column19.Width = 45;
		this.GB3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GB3.Controls.Add(this.DGV2);
		this.GB3.Location = new System.Drawing.Point(2, 458);
		this.GB3.Name = "GB3";
		this.GB3.Size = new System.Drawing.Size(196, 140);
		this.GB3.TabIndex = 45;
		this.GB3.TabStop = false;
		this.GB3.Text = "Attenuation factors";
		this.DGV2.AllowUserToAddRows = false;
		this.DGV2.AllowUserToDeleteRows = false;
		this.DGV2.AllowUserToResizeColumns = false;
		this.DGV2.AllowUserToResizeRows = false;
		this.DGV2.BackgroundColor = System.Drawing.SystemColors.Menu;
		this.DGV2.BorderStyle = System.Windows.Forms.BorderStyle.None;
		dataGridViewCellStyle22.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle22.BackColor = System.Drawing.SystemColors.Menu;
		dataGridViewCellStyle22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
		dataGridViewCellStyle22.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle22.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle22.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle22.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.DGV2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle22;
		this.DGV2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
		this.DGV2.Columns.AddRange(this.Column11, this.Column12, this.Column13, this.Column14);
		this.DGV2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.DGV2.EnableHeadersVisualStyles = false;
		this.DGV2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.DGV2.Location = new System.Drawing.Point(3, 16);
		this.DGV2.MultiSelect = false;
		this.DGV2.Name = "DGV2";
		dataGridViewCellStyle23.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		dataGridViewCellStyle23.BackColor = System.Drawing.SystemColors.Window;
		dataGridViewCellStyle23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
		dataGridViewCellStyle23.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle23.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle23.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle23.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.DGV2.RowHeadersDefaultCellStyle = dataGridViewCellStyle23;
		this.DGV2.RowHeadersVisible = false;
		dataGridViewCellStyle24.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle24.BackColor = System.Drawing.SystemColors.Window;
		this.DGV2.RowsDefaultCellStyle = dataGridViewCellStyle24;
		this.DGV2.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.SystemColors.Window;
		this.DGV2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
		this.DGV2.ShowCellErrors = false;
		this.DGV2.ShowCellToolTips = false;
		this.DGV2.ShowEditingIcon = false;
		this.DGV2.Size = new System.Drawing.Size(190, 121);
		this.DGV2.TabIndex = 46;
		this.Column11.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		dataGridViewCellStyle25.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Column11.DefaultCellStyle = dataGridViewCellStyle25;
		this.Column11.HeaderText = "Limits";
		this.Column11.Name = "Column11";
		this.Column11.ReadOnly = true;
		this.Column11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		dataGridViewCellStyle26.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Column12.DefaultCellStyle = dataGridViewCellStyle26;
		this.Column12.HeaderText = "ɑ24";
		this.Column12.Name = "Column12";
		this.Column12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column12.Width = 43;
		dataGridViewCellStyle27.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Column13.DefaultCellStyle = dataGridViewCellStyle27;
		this.Column13.HeaderText = "Ftdc";
		this.Column13.Name = "Column13";
		this.Column13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column13.Width = 43;
		dataGridViewCellStyle28.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Column14.DefaultCellStyle = dataGridViewCellStyle28;
		this.Column14.HeaderText = "Fpr";
		this.Column14.Name = "Column14";
		this.Column14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column14.Width = 43;
		this.GB2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GB2.Controls.Add(this.txtP);
		this.GB2.Controls.Add(this.txtN);
		this.GB2.Controls.Add(this.txtEIM);
		this.GB2.Controls.Add(this.txtPant);
		this.GB2.Controls.Add(this.txtNu);
		this.GB2.Controls.Add(this.txtG);
		this.GB2.Controls.Add(this.cmdCalcEIM);
		this.GB2.Controls.Add(this.cmdCalcNu);
		this.GB2.Controls.Add(this.cmdCalcP);
		this.GB2.Controls.Add(this.Label1);
		this.GB2.Controls.Add(this.lblEIM);
		this.GB2.Controls.Add(this.lblPant);
		this.GB2.Controls.Add(this.Label10);
		this.GB2.Controls.Add(this.Label9);
		this.GB2.Controls.Add(this.lblP);
		this.GB2.Controls.Add(this.Label6);
		this.GB2.Controls.Add(this.Label7);
		this.GB2.Controls.Add(this.Label8);
		this.GB2.Controls.Add(this.Label11);
		this.GB2.Controls.Add(this.Label12);
		this.GB2.Location = new System.Drawing.Point(2, 264);
		this.GB2.Name = "GB2";
		this.GB2.Size = new System.Drawing.Size(196, 188);
		this.GB2.TabIndex = 19;
		this.GB2.TabStop = false;
		this.GB2.Text = "EIRP Calculator";
		this.txtP.Location = new System.Drawing.Point(86, 23);
		this.txtP.Name = "txtP";
		this.txtP.Size = new System.Drawing.Size(50, 20);
		this.txtP.TabIndex = 1;
		this.txtP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.ToolTip1.SetToolTip(this.txtP, "Мощность передатчика");
		this.txtN.Location = new System.Drawing.Point(86, 49);
		this.txtN.Name = "txtN";
		this.txtN.Size = new System.Drawing.Size(50, 20);
		this.txtN.TabIndex = 2;
		this.txtN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.ToolTip1.SetToolTip(this.txtN, "Количество передатчиков");
		this.txtEIM.BackColor = System.Drawing.SystemColors.Menu;
		this.txtEIM.Location = new System.Drawing.Point(86, 156);
		this.txtEIM.Name = "txtEIM";
		this.txtEIM.ReadOnly = true;
		this.txtEIM.Size = new System.Drawing.Size(50, 20);
		this.txtEIM.TabIndex = 6;
		this.txtEIM.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtPant.BackColor = System.Drawing.SystemColors.Window;
		this.txtPant.Location = new System.Drawing.Point(86, 130);
		this.txtPant.Name = "txtPant";
		this.txtPant.Size = new System.Drawing.Size(50, 20);
		this.txtPant.TabIndex = 5;
		this.txtPant.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.ToolTip1.SetToolTip(this.txtPant, "Мощность подводимая к антенне");
		this.txtNu.Location = new System.Drawing.Point(86, 75);
		this.txtNu.Name = "txtNu";
		this.txtNu.Size = new System.Drawing.Size(50, 20);
		this.txtNu.TabIndex = 3;
		this.txtNu.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.ToolTip1.SetToolTip(this.txtNu, "Потери в антенно-фидерном тракте");
		this.txtG.Location = new System.Drawing.Point(86, 101);
		this.txtG.Name = "txtG";
		this.txtG.Size = new System.Drawing.Size(50, 20);
		this.txtG.TabIndex = 4;
		this.txtG.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.ToolTip1.SetToolTip(this.txtG, "Коэффициент усиления антенны");
		this.cmdCalcEIM.Image = (System.Drawing.Image)resources.GetObject("cmdCalcEIM.Image");
		this.cmdCalcEIM.Location = new System.Drawing.Point(170, 130);
		this.cmdCalcEIM.Name = "cmdCalcEIM";
		this.cmdCalcEIM.Size = new System.Drawing.Size(22, 46);
		this.cmdCalcEIM.TabIndex = 44;
		this.ToolTip1.SetToolTip(this.cmdCalcEIM, "Antenna power and EIRP calculation ");
		this.cmdCalcEIM.UseVisualStyleBackColor = true;
		this.cmdCalcNu.Image = (System.Drawing.Image)resources.GetObject("cmdCalcNu.Image");
		this.cmdCalcNu.Location = new System.Drawing.Point(170, 75);
		this.cmdCalcNu.Name = "cmdCalcNu";
		this.cmdCalcNu.Size = new System.Drawing.Size(22, 20);
		this.cmdCalcNu.TabIndex = 43;
		this.ToolTip1.SetToolTip(this.cmdCalcNu, "Total attenuation calculation");
		this.cmdCalcNu.UseVisualStyleBackColor = true;
		this.cmdCalcP.Image = (System.Drawing.Image)resources.GetObject("cmdCalcP.Image");
		this.cmdCalcP.Location = new System.Drawing.Point(170, 23);
		this.cmdCalcP.Name = "cmdCalcP";
		this.cmdCalcP.Size = new System.Drawing.Size(22, 20);
		this.cmdCalcP.TabIndex = 42;
		this.ToolTip1.SetToolTip(this.cmdCalcP, "Transmitter power calculation");
		this.cmdCalcP.UseVisualStyleBackColor = true;
		this.Label1.AutoSize = true;
		this.Label1.Location = new System.Drawing.Point(18, 27);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(61, 13);
		this.Label1.TabIndex = 36;
		this.Label1.Text = "TRX power";
		this.lblEIM.AutoSize = true;
		this.lblEIM.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
		this.lblEIM.ForeColor = System.Drawing.Color.MediumBlue;
		this.lblEIM.Location = new System.Drawing.Point(137, 159);
		this.lblEIM.Name = "lblEIM";
		this.lblEIM.Size = new System.Drawing.Size(18, 13);
		this.lblEIM.TabIndex = 35;
		this.lblEIM.Text = "W";
		this.ToolTip1.SetToolTip(this.lblEIM, "Эффективно-излучаемая мощность");
		this.lblPant.AutoSize = true;
		this.lblPant.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
		this.lblPant.ForeColor = System.Drawing.Color.MediumBlue;
		this.lblPant.Location = new System.Drawing.Point(137, 133);
		this.lblPant.Name = "lblPant";
		this.lblPant.Size = new System.Drawing.Size(18, 13);
		this.lblPant.TabIndex = 34;
		this.lblPant.Text = "W";
		this.Label10.AutoSize = true;
		this.Label10.Location = new System.Drawing.Point(137, 104);
		this.Label10.Name = "Label10";
		this.Label10.Size = new System.Drawing.Size(22, 13);
		this.Label10.TabIndex = 33;
		this.Label10.Text = "dBi";
		this.Label9.AutoSize = true;
		this.Label9.Location = new System.Drawing.Point(137, 78);
		this.Label9.Name = "Label9";
		this.Label9.Size = new System.Drawing.Size(20, 13);
		this.Label9.TabIndex = 32;
		this.Label9.Text = "dB";
		this.lblP.AutoSize = true;
		this.lblP.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
		this.lblP.ForeColor = System.Drawing.Color.MediumBlue;
		this.lblP.Location = new System.Drawing.Point(137, 26);
		this.lblP.Name = "lblP";
		this.lblP.Size = new System.Drawing.Size(18, 13);
		this.lblP.TabIndex = 31;
		this.lblP.Text = "W";
		this.Label6.AutoSize = true;
		this.Label6.Location = new System.Drawing.Point(47, 159);
		this.Label6.Name = "Label6";
		this.Label6.Size = new System.Drawing.Size(32, 13);
		this.Label6.TabIndex = 10;
		this.Label6.Text = "EIRP";
		this.Label7.Location = new System.Drawing.Point(4, 125);
		this.Label7.Name = "Label7";
		this.Label7.Size = new System.Drawing.Size(75, 29);
		this.Label7.TabIndex = 12;
		this.Label7.Text = "Power at antenna port";
		this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.Label8.AutoSize = true;
		this.Label8.Location = new System.Drawing.Point(6, 52);
		this.Label8.Name = "Label8";
		this.Label8.Size = new System.Drawing.Size(73, 13);
		this.Label8.TabIndex = 3;
		this.Label8.Text = "Number of TX";
		this.Label11.AutoSize = true;
		this.Label11.Location = new System.Drawing.Point(3, 79);
		this.Label11.Name = "Label11";
		this.Label11.Size = new System.Drawing.Size(76, 13);
		this.Label11.TabIndex = 5;
		this.Label11.Text = "Total attenuat.";
		this.Label12.AutoSize = true;
		this.Label12.Location = new System.Drawing.Point(9, 104);
		this.Label12.Name = "Label12";
		this.Label12.Size = new System.Drawing.Size(70, 13);
		this.Label12.TabIndex = 7;
		this.Label12.Text = "Antenna gain";
		this.ContDBW.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.ВтToolStripMenuItem, this.ДБВтToolStripMenuItem, this.ДБмВтToolStripMenuItem, this.ДБмВтToolStripMenuItem1 });
		this.ContDBW.Name = "cmsDBW";
		this.ContDBW.Size = new System.Drawing.Size(109, 92);
		this.ВтToolStripMenuItem.Name = "ВтToolStripMenuItem";
		this.ВтToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
		this.ВтToolStripMenuItem.Text = "Вт";
		this.ДБВтToolStripMenuItem.Name = "ДБВтToolStripMenuItem";
		this.ДБВтToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
		this.ДБВтToolStripMenuItem.Text = "дБВт";
		this.ДБмВтToolStripMenuItem.Name = "ДБмВтToolStripMenuItem";
		this.ДБмВтToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
		this.ДБмВтToolStripMenuItem.Text = "мВт";
		this.ДБмВтToolStripMenuItem1.Name = "ДБмВтToolStripMenuItem1";
		this.ДБмВтToolStripMenuItem1.Size = new System.Drawing.Size(108, 22);
		this.ДБмВтToolStripMenuItem1.Text = "дБмВт";
		this.ContExist.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.ToolStripMenuItem5, this.ToolStripMenuItem6 });
		this.ContExist.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
		this.ContExist.Name = "ContextMenuSite";
		this.ContExist.ShowImageMargin = false;
		this.ContExist.ShowItemToolTips = false;
		this.ContExist.Size = new System.Drawing.Size(90, 48);
		this.ToolStripMenuItem5.Name = "ToolStripMenuItem5";
		this.ToolStripMenuItem5.Size = new System.Drawing.Size(89, 22);
		this.ToolStripMenuItem5.Text = "Existing";
		this.ToolStripMenuItem6.Name = "ToolStripMenuItem6";
		this.ToolStripMenuItem6.Size = new System.Drawing.Size(89, 22);
		this.ToolStripMenuItem6.Text = "New";
		this.ContOwner.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
		this.ContOwner.Name = "ContextMenuSite";
		this.ContOwner.ShowImageMargin = false;
		this.ContOwner.ShowItemToolTips = false;
		this.ContOwner.Size = new System.Drawing.Size(36, 4);
		this.cmdColor1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdColor1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdColor1.Location = new System.Drawing.Point(551, 0);
		this.cmdColor1.Name = "cmdColor1";
		this.cmdColor1.Size = new System.Drawing.Size(26, 26);
		this.cmdColor1.TabIndex = 143;
		this.cmdColor1.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdColor1, "Change color of selected rows");
		this.cmdColor1.UseVisualStyleBackColor = true;
		this.cmdColor2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdColor2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdColor2.Location = new System.Drawing.Point(578, 0);
		this.cmdColor2.Name = "cmdColor2";
		this.cmdColor2.Size = new System.Drawing.Size(26, 26);
		this.cmdColor2.TabIndex = 144;
		this.cmdColor2.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdColor2, "Change color of selected rows");
		this.cmdColor2.UseVisualStyleBackColor = true;
		this.cmdColor4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdColor4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdColor4.Location = new System.Drawing.Point(632, 0);
		this.cmdColor4.Name = "cmdColor4";
		this.cmdColor4.Size = new System.Drawing.Size(26, 26);
		this.cmdColor4.TabIndex = 146;
		this.cmdColor4.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdColor4, "Change color of selected rows");
		this.cmdColor4.UseVisualStyleBackColor = true;
		this.cmdColor3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdColor3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdColor3.Location = new System.Drawing.Point(605, 0);
		this.cmdColor3.Name = "cmdColor3";
		this.cmdColor3.Size = new System.Drawing.Size(26, 26);
		this.cmdColor3.TabIndex = 145;
		this.cmdColor3.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdColor3, "Change color of selected rows");
		this.cmdColor3.UseVisualStyleBackColor = true;
		this.cmdColor8.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdColor8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdColor8.Location = new System.Drawing.Point(740, 0);
		this.cmdColor8.Name = "cmdColor8";
		this.cmdColor8.Size = new System.Drawing.Size(26, 26);
		this.cmdColor8.TabIndex = 150;
		this.cmdColor8.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdColor8, "Change color of selected rows");
		this.cmdColor8.UseVisualStyleBackColor = true;
		this.cmdColor7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdColor7.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdColor7.Location = new System.Drawing.Point(713, 0);
		this.cmdColor7.Name = "cmdColor7";
		this.cmdColor7.Size = new System.Drawing.Size(26, 26);
		this.cmdColor7.TabIndex = 149;
		this.cmdColor7.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdColor7, "Change color of selected rows");
		this.cmdColor7.UseVisualStyleBackColor = true;
		this.cmdColor6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdColor6.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdColor6.Location = new System.Drawing.Point(686, 0);
		this.cmdColor6.Name = "cmdColor6";
		this.cmdColor6.Size = new System.Drawing.Size(26, 26);
		this.cmdColor6.TabIndex = 148;
		this.cmdColor6.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdColor6, "Change color of selected rows");
		this.cmdColor6.UseVisualStyleBackColor = true;
		this.cmdColor5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdColor5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdColor5.Location = new System.Drawing.Point(659, 0);
		this.cmdColor5.Name = "cmdColor5";
		this.cmdColor5.Size = new System.Drawing.Size(26, 26);
		this.cmdColor5.TabIndex = 147;
		this.cmdColor5.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdColor5, "Change color of selected rows");
		this.cmdColor5.UseVisualStyleBackColor = true;
		this.cmdColor0.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdColor0.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdColor0.Location = new System.Drawing.Point(524, 0);
		this.cmdColor0.Name = "cmdColor0";
		this.cmdColor0.Size = new System.Drawing.Size(26, 26);
		this.cmdColor0.TabIndex = 151;
		this.cmdColor0.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdColor0, "Change color of selected rows");
		this.cmdColor0.UseVisualStyleBackColor = true;
		this.cmdColor11.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdColor11.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdColor11.Location = new System.Drawing.Point(821, 0);
		this.cmdColor11.Name = "cmdColor11";
		this.cmdColor11.Size = new System.Drawing.Size(26, 26);
		this.cmdColor11.TabIndex = 154;
		this.cmdColor11.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdColor11, "Change color of selected rows");
		this.cmdColor11.UseVisualStyleBackColor = true;
		this.cmdColor10.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdColor10.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdColor10.Location = new System.Drawing.Point(794, 0);
		this.cmdColor10.Name = "cmdColor10";
		this.cmdColor10.Size = new System.Drawing.Size(26, 26);
		this.cmdColor10.TabIndex = 153;
		this.cmdColor10.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdColor10, "Change color of selected rows");
		this.cmdColor10.UseVisualStyleBackColor = true;
		this.cmdColor9.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdColor9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdColor9.Location = new System.Drawing.Point(767, 0);
		this.cmdColor9.Name = "cmdColor9";
		this.cmdColor9.Size = new System.Drawing.Size(26, 26);
		this.cmdColor9.TabIndex = 152;
		this.cmdColor9.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdColor9, "Change color of selected rows");
		this.cmdColor9.UseVisualStyleBackColor = true;
		this.cmdExcel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdExcel.Image = (System.Drawing.Image)resources.GetObject("cmdExcel.Image");
		this.cmdExcel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdExcel.Location = new System.Drawing.Point(297, 0);
		this.cmdExcel.Name = "cmdExcel";
		this.cmdExcel.Size = new System.Drawing.Size(26, 26);
		this.cmdExcel.TabIndex = 201;
		this.cmdExcel.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdExcel, "Radiating source reports in MS Excel");
		this.cmdExcel.UseVisualStyleBackColor = true;
		this.cmdMoveBottom.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdMoveBottom.Image = (System.Drawing.Image)resources.GetObject("cmdMoveBottom.Image");
		this.cmdMoveBottom.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdMoveBottom.Location = new System.Drawing.Point(108, 0);
		this.cmdMoveBottom.Name = "cmdMoveBottom";
		this.cmdMoveBottom.Size = new System.Drawing.Size(26, 26);
		this.cmdMoveBottom.TabIndex = 200;
		this.cmdMoveBottom.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdMoveBottom, "Move selected rows to the end of the table");
		this.cmdMoveBottom.UseVisualStyleBackColor = true;
		this.cmdMoveTop.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdMoveTop.Image = (System.Drawing.Image)resources.GetObject("cmdMoveTop.Image");
		this.cmdMoveTop.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdMoveTop.Location = new System.Drawing.Point(81, 0);
		this.cmdMoveTop.Name = "cmdMoveTop";
		this.cmdMoveTop.Size = new System.Drawing.Size(26, 26);
		this.cmdMoveTop.TabIndex = 199;
		this.cmdMoveTop.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdMoveTop, "Move selected rows to the beginning of the table");
		this.cmdMoveTop.UseVisualStyleBackColor = true;
		this.cmdWord.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdWord.Image = (System.Drawing.Image)resources.GetObject("cmdWord.Image");
		this.cmdWord.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdWord.Location = new System.Drawing.Point(270, 0);
		this.cmdWord.Name = "cmdWord";
		this.cmdWord.Size = new System.Drawing.Size(26, 26);
		this.cmdWord.TabIndex = 197;
		this.cmdWord.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdWord, "Radiating source reports in MS Word");
		this.cmdWord.UseVisualStyleBackColor = true;
		this.cmdFillWidthDnOne.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdFillWidthDnOne.Image = (System.Drawing.Image)resources.GetObject("cmdFillWidthDnOne.Image");
		this.cmdFillWidthDnOne.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdFillWidthDnOne.Location = new System.Drawing.Point(243, 0);
		this.cmdFillWidthDnOne.Name = "cmdFillWidthDnOne";
		this.cmdFillWidthDnOne.Size = new System.Drawing.Size(26, 26);
		this.cmdFillWidthDnOne.TabIndex = 156;
		this.cmdFillWidthDnOne.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdFillWidthDnOne, "Calculate the value of the antenna beamwidth for the current source based on antenna pattern file");
		this.cmdFillWidthDnOne.UseVisualStyleBackColor = true;
		this.cmdFillWidthDN.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdFillWidthDN.Image = (System.Drawing.Image)resources.GetObject("cmdFillWidthDN.Image");
		this.cmdFillWidthDN.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdFillWidthDN.Location = new System.Drawing.Point(216, 0);
		this.cmdFillWidthDN.Name = "cmdFillWidthDN";
		this.cmdFillWidthDN.Size = new System.Drawing.Size(26, 26);
		this.cmdFillWidthDN.TabIndex = 148;
		this.cmdFillWidthDN.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdFillWidthDN, "Calculate antenna beamwidths for all sources based on antenna pattern files");
		this.cmdFillWidthDN.UseVisualStyleBackColor = true;
		this.cmdCheckUncheck.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdCheckUncheck.Image = (System.Drawing.Image)resources.GetObject("cmdCheckUncheck.Image");
		this.cmdCheckUncheck.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdCheckUncheck.Location = new System.Drawing.Point(135, 0);
		this.cmdCheckUncheck.Name = "cmdCheckUncheck";
		this.cmdCheckUncheck.Size = new System.Drawing.Size(26, 26);
		this.cmdCheckUncheck.TabIndex = 155;
		this.cmdCheckUncheck.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdCheckUncheck, "Inclusion/Exclusion of all sources in the calculation. You can also make all sources active at once or turn them off by clicking on the column heading");
		this.cmdCheckUncheck.UseVisualStyleBackColor = true;
		this.cmdPasteFromBuffer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdPasteFromBuffer.Image = (System.Drawing.Image)resources.GetObject("cmdPasteFromBuffer.Image");
		this.cmdPasteFromBuffer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdPasteFromBuffer.Location = new System.Drawing.Point(189, 0);
		this.cmdPasteFromBuffer.Name = "cmdPasteFromBuffer";
		this.cmdPasteFromBuffer.Size = new System.Drawing.Size(26, 26);
		this.cmdPasteFromBuffer.TabIndex = 142;
		this.cmdPasteFromBuffer.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdPasteFromBuffer, "Paste lines from clipboard (after current line)");
		this.cmdPasteFromBuffer.UseVisualStyleBackColor = true;
		this.cmdCopyToBuffer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdCopyToBuffer.Image = (System.Drawing.Image)resources.GetObject("cmdCopyToBuffer.Image");
		this.cmdCopyToBuffer.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdCopyToBuffer.Location = new System.Drawing.Point(162, 0);
		this.cmdCopyToBuffer.Name = "cmdCopyToBuffer";
		this.cmdCopyToBuffer.Size = new System.Drawing.Size(26, 26);
		this.cmdCopyToBuffer.TabIndex = 141;
		this.cmdCopyToBuffer.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdCopyToBuffer, "Copy selected lines to clipboard");
		this.cmdCopyToBuffer.UseVisualStyleBackColor = true;
		this.cmdUp.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdUp.Image = (System.Drawing.Image)resources.GetObject("cmdUp.Image");
		this.cmdUp.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdUp.Location = new System.Drawing.Point(27, 0);
		this.cmdUp.Name = "cmdUp";
		this.cmdUp.Size = new System.Drawing.Size(26, 26);
		this.cmdUp.TabIndex = 136;
		this.cmdUp.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdUp, "Move selected rows up one position");
		this.cmdUp.UseVisualStyleBackColor = true;
		this.cmdDown.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdDown.Image = (System.Drawing.Image)resources.GetObject("cmdDown.Image");
		this.cmdDown.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdDown.Location = new System.Drawing.Point(54, 0);
		this.cmdDown.Name = "cmdDown";
		this.cmdDown.Size = new System.Drawing.Size(26, 26);
		this.cmdDown.TabIndex = 137;
		this.cmdDown.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdDown, "Move selected rows down one position");
		this.cmdDown.UseVisualStyleBackColor = true;
		this.cmdCopySelectedTX.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdCopySelectedTX.Image = (System.Drawing.Image)resources.GetObject("cmdCopySelectedTX.Image");
		this.cmdCopySelectedTX.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdCopySelectedTX.Location = new System.Drawing.Point(0, 0);
		this.cmdCopySelectedTX.Name = "cmdCopySelectedTX";
		this.cmdCopySelectedTX.Size = new System.Drawing.Size(26, 26);
		this.cmdCopySelectedTX.TabIndex = 135;
		this.cmdCopySelectedTX.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdCopySelectedTX, "Create a copy of the selected rows");
		this.cmdCopySelectedTX.UseVisualStyleBackColor = true;
		this.cmsDBW.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.ToolStripMenuItem9, this.ToolStripMenuItem11, this.ToolStripMenuItem12, this.ToolStripMenuItem13 });
		this.cmsDBW.Name = "cmsDBW";
		this.cmsDBW.Size = new System.Drawing.Size(100, 92);
		this.ToolStripMenuItem9.Name = "ToolStripMenuItem9";
		this.ToolStripMenuItem9.Size = new System.Drawing.Size(99, 22);
		this.ToolStripMenuItem9.Text = "W";
		this.ToolStripMenuItem11.Name = "ToolStripMenuItem11";
		this.ToolStripMenuItem11.Size = new System.Drawing.Size(99, 22);
		this.ToolStripMenuItem11.Text = "dBW";
		this.ToolStripMenuItem12.Name = "ToolStripMenuItem12";
		this.ToolStripMenuItem12.Size = new System.Drawing.Size(99, 22);
		this.ToolStripMenuItem12.Text = "mW";
		this.ToolStripMenuItem13.Name = "ToolStripMenuItem13";
		this.ToolStripMenuItem13.Size = new System.Drawing.Size(99, 22);
		this.ToolStripMenuItem13.Text = "dBm";
		this.ContLocation.Items.AddRange(new System.Windows.Forms.ToolStripItem[3] { this.ToolStripMenuItem14, this.ToolStripMenuItem15, this.ToolStripMenuItem16 });
		this.ContLocation.Name = "cmsDBW";
		this.ContLocation.Size = new System.Drawing.Size(112, 70);
		this.ToolStripMenuItem14.Name = "ToolStripMenuItem14";
		this.ToolStripMenuItem14.Size = new System.Drawing.Size(111, 22);
		this.ToolStripMenuItem14.Text = "Tower";
		this.ToolStripMenuItem15.Name = "ToolStripMenuItem15";
		this.ToolStripMenuItem15.Size = new System.Drawing.Size(111, 22);
		this.ToolStripMenuItem15.Text = "Roof";
		this.ToolStripMenuItem16.Name = "ToolStripMenuItem16";
		this.ToolStripMenuItem16.Size = new System.Drawing.Size(111, 22);
		this.ToolStripMenuItem16.Text = "Facade";
		this.ContWord.Items.AddRange(new System.Windows.Forms.ToolStripItem[4] { this.cmdRadiatingSources_1, this.cmdAntennaPatterns, this.cmdEMFSafetyVolumes1, this.cmdCardinalPointsChart });
		this.ContWord.Name = "cmsDBW";
		this.ContWord.Size = new System.Drawing.Size(307, 92);
		this.cmdRadiatingSources_1.Name = "cmdRadiatingSources_1";
		this.cmdRadiatingSources_1.Size = new System.Drawing.Size(306, 22);
		this.cmdRadiatingSources_1.Text = "Radiating sources {RadiatingSources_1}";
		this.cmdAntennaPatterns.Name = "cmdAntennaPatterns";
		this.cmdAntennaPatterns.Size = new System.Drawing.Size(306, 22);
		this.cmdAntennaPatterns.Text = "Antenna patterns {AntennaPatterns}";
		this.cmdEMFSafetyVolumes1.Name = "cmdEMFSafetyVolumes1";
		this.cmdEMFSafetyVolumes1.Size = new System.Drawing.Size(306, 22);
		this.cmdEMFSafetyVolumes1.Text = "EMF safety volumes {EMF_Safety_Volumes1}";
		this.cmdCardinalPointsChart.Name = "cmdCardinalPointsChart";
		this.cmdCardinalPointsChart.Size = new System.Drawing.Size(306, 22);
		this.cmdCardinalPointsChart.Text = "Cardinal points chart {CardinalPointsChart}";
		this.ContExcel.Items.AddRange(new System.Windows.Forms.ToolStripItem[2] { this.cmdExcel1, this.cmdImportRadiatingSources });
		this.ContExcel.Name = "cmsDBW";
		this.ContExcel.Size = new System.Drawing.Size(204, 48);
		this.cmdExcel1.Name = "cmdExcel1";
		this.cmdExcel1.Size = new System.Drawing.Size(203, 22);
		this.cmdExcel1.Text = "Export radiating sources";
		this.cmdImportRadiatingSources.Name = "cmdImportRadiatingSources";
		this.cmdImportRadiatingSources.Size = new System.Drawing.Size(203, 22);
		this.cmdImportRadiatingSources.Text = "Import radiating sources";
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		base.Controls.Add(this.cmdExcel);
		base.Controls.Add(this.cmdMoveBottom);
		base.Controls.Add(this.cmdMoveTop);
		base.Controls.Add(this.cmdWord);
		base.Controls.Add(this.cmdFillWidthDnOne);
		base.Controls.Add(this.cmdFillWidthDN);
		base.Controls.Add(this.cmdCheckUncheck);
		base.Controls.Add(this.cmdColor11);
		base.Controls.Add(this.cmdColor10);
		base.Controls.Add(this.cmdColor9);
		base.Controls.Add(this.cmdColor0);
		base.Controls.Add(this.cmdColor8);
		base.Controls.Add(this.cmdColor7);
		base.Controls.Add(this.cmdColor6);
		base.Controls.Add(this.cmdColor5);
		base.Controls.Add(this.cmdColor4);
		base.Controls.Add(this.cmdColor3);
		base.Controls.Add(this.cmdColor2);
		base.Controls.Add(this.cmdColor1);
		base.Controls.Add(this.cmdPasteFromBuffer);
		base.Controls.Add(this.cmdCopyToBuffer);
		base.Controls.Add(this.cmdUp);
		base.Controls.Add(this.cmdDown);
		base.Controls.Add(this.cmdCopySelectedTX);
		base.Controls.Add(this.SplitContainer1);
		base.Name = "UC_PRTO";
		base.Size = new System.Drawing.Size(1555, 790);
		this.gb1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.PB1).EndInit();
		this.SplitContainer1.Panel1.ResumeLayout(false);
		this.SplitContainer1.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.SplitContainer1).EndInit();
		this.SplitContainer1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.DGV1).EndInit();
		this.GB3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.DGV2).EndInit();
		this.GB2.ResumeLayout(false);
		this.GB2.PerformLayout();
		this.ContDBW.ResumeLayout(false);
		this.ContExist.ResumeLayout(false);
		this.cmsDBW.ResumeLayout(false);
		this.ContLocation.ResumeLayout(false);
		this.ContWord.ResumeLayout(false);
		this.ContExcel.ResumeLayout(false);
		base.ResumeLayout(false);
	}

	private void UC_PRTO_ControlAdded(object sender, ControlEventArgs e)
	{
		DGV2.Rows.Clear();
		DGV2.Rows.Add(4);
		DGV2.Rows[0].Cells[0].Value = "Limit 1";
		DGV2.Rows[1].Cells[0].Value = "Limit 2";
		DGV2.Rows[2].Cells[0].Value = "Limit 3";
		DGV2.Rows[3].Cells[0].Value = "Limit 4";
		DGV2.Rows[0].Cells[0].Style.BackColor = SystemColors.Menu;
		DGV2.Rows[1].Cells[0].Style.BackColor = SystemColors.Menu;
		DGV2.Rows[2].Cells[0].Style.BackColor = SystemColors.Menu;
		DGV2.Rows[3].Cells[0].Style.BackColor = SystemColors.Menu;
		RefreshRightPanel();
	}

	private void UC_PRTO_Load(object sender, EventArgs e)
	{
		Module1.SetDoubleBuffered(DGV1);
		cmdColor0.BackColor = Form1_Public.DgvColor[0];
		cmdColor1.BackColor = Form1_Public.DgvColor[1];
		cmdColor2.BackColor = Form1_Public.DgvColor[2];
		cmdColor3.BackColor = Form1_Public.DgvColor[3];
		cmdColor4.BackColor = Form1_Public.DgvColor[4];
		cmdColor5.BackColor = Form1_Public.DgvColor[5];
		cmdColor6.BackColor = Form1_Public.DgvColor[6];
		cmdColor7.BackColor = Form1_Public.DgvColor[7];
		cmdColor8.BackColor = Form1_Public.DgvColor[8];
		cmdColor9.BackColor = Form1_Public.DgvColor[9];
		cmdColor10.BackColor = Form1_Public.DgvColor[10];
		cmdColor11.BackColor = Form1_Public.DgvColor[11];
		ContOwner.Items.Clear();
		checked
		{
			if (!Information.IsNothing(Public2.AppSetup.OwnerNameList))
			{
				int upperBound = Public2.AppSetup.OwnerNameList.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					if (Operators.CompareString(Strings.Trim(Public2.AppSetup.OwnerNameList[i]), "", TextCompare: false) != 0)
					{
						ContOwner.Items.Add(Public2.AppSetup.OwnerNameList[i]);
						ContOwner.Items[i].BackColor = Public2.AppSetup.OwnerColorList[i];
					}
				}
				ContOwner.Items.Add(Localization.LocMSG(4));
			}
			if (Public2.AppSetup.PrtoOrder.GetUpperBound(0) < 20)
			{
				Public2.SetPrtoOrder();
			}
			int upperBound2 = Public2.AppSetup.PrtoOrder.GetUpperBound(0);
			for (int j = 0; j <= upperBound2; j++)
			{
				int upperBound3 = Public2.AppSetup.PrtoOrder.GetUpperBound(0);
				for (int k = 0; k <= upperBound3; k++)
				{
					if (Public2.AppSetup.PrtoOrder[k] == j)
					{
						DGV1.Columns[k].DisplayIndex = j;
						break;
					}
				}
			}
			FillDGV1();
			if (!Information.IsNothing(Public2.PRJ.TX) && Form1_Public.IndexTX <= Public2.PRJ.TX.GetUpperBound(0))
			{
				DGV1.CurrentCell = DGV1[0, Form1_Public.IndexTX];
				DGV1.Rows[Form1_Public.IndexTX].Selected = true;
			}
		}
	}

	public void FillDGV1()
	{
		FlagOK = false;
		DGV1.Rows.Clear();
		if (Information.IsNothing(Public2.PRJ.TX))
		{
			FlagOK = true;
			return;
		}
		int upperBound = Public2.PRJ.TX.GetUpperBound(0);
		checked
		{
			for (int i = 0; i <= upperBound; i++)
			{
				DGV1.Rows.Add(Public2.PRJ.TX[i].Active, Public2.PRJ.TX[i].Name, Public2.PRJ.TX[i].TypeOfService, Form1_Public.FreqSpanString(Public2.PRJ.TX[i].F, Public2.PRJ.TX[i].F2), Public2.PRJ.TX[i].P, Public2.PRJ.TX[i].N, Public2.PRJ.TX[i].Loss, Public2.PRJ.TX[i].AntType, Form1_Public.Azimuth360(Public2.PRJ.TX[i].Az), Public2.PRJ.TX[i].G, Public2.PRJ.TX[i].H, Public2.PRJ.TX[i].D, Public2.PRJ.TX[i].Ug, Public2.PRJ.TX[i].ElectUg, Module1.TchkOrZpt(Public2.PRJ.TX[i].UgH), Module1.TchkOrZpt(Public2.PRJ.TX[i].UgV), Public2.PRJ.TX[i].Location, Public2.PRJ.TX[i].Exist, Public2.PRJ.TX[i].Owner, Public2.PRJ.TX[i].PTOT(), Public2.PRJ.TX[i].Sector);
			}
			int upperBound2 = Public2.PRJ.TX.GetUpperBound(0);
			for (int j = 0; j <= upperBound2; j++)
			{
				DGV1.Rows[j].HeaderCell.Value = Strings.Format(j + 1, "000").ToString();
				DGV1.Rows[j].DefaultCellStyle.BackColor = Public2.PRJ.TX[j].Color;
			}
			DGV1.ClearSelection();
			FlagOK = true;
		}
	}

	private void DGV1_CellEnter(object sender, DataGridViewCellEventArgs e)
	{
		if (FlagOK)
		{
			SelRow = checked((short)e.RowIndex);
			Form1_Public.IndexTX = e.RowIndex;
			RefreshRightPanel();
		}
	}

	public void RefreshRightPanel()
	{
		PB1.Refresh();
		int num = 0;
		checked
		{
			do
			{
				if (Public2.PRJ.SETT.LimitAlpha24[num])
				{
					DGV2.Rows[num].Cells[1].ReadOnly = false;
					DGV2.Rows[num].Cells[1].Style.BackColor = SystemColors.Window;
				}
				else
				{
					DGV2.Rows[num].Cells[1].ReadOnly = true;
					DGV2.Rows[num].Cells[1].Style.BackColor = SystemColors.Menu;
				}
				if (Public2.PRJ.SETT.LimitFtdc[num])
				{
					DGV2.Rows[num].Cells[2].ReadOnly = false;
					DGV2.Rows[num].Cells[2].Style.BackColor = SystemColors.Window;
				}
				else
				{
					DGV2.Rows[num].Cells[2].ReadOnly = true;
					DGV2.Rows[num].Cells[2].Style.BackColor = SystemColors.Menu;
				}
				if (Public2.PRJ.SETT.LimitFpr[num])
				{
					DGV2.Rows[num].Cells[3].ReadOnly = false;
					DGV2.Rows[num].Cells[3].Style.BackColor = SystemColors.Window;
				}
				else
				{
					DGV2.Rows[num].Cells[3].ReadOnly = true;
					DGV2.Rows[num].Cells[3].Style.BackColor = SystemColors.Menu;
				}
				num++;
			}
			while (num <= 3);
			if (Information.IsNothing(Public2.PRJ.TX))
			{
				txtP.Text = "";
				txtN.Text = "";
				txtNu.Text = "";
				txtP.Text = "";
				txtG.Text = "";
				CalcEIM();
				int num2 = 0;
				do
				{
					DGV2.Rows[num2].Cells[1].Value = "";
					DGV2.Rows[num2].Cells[2].Value = "";
					DGV2.Rows[num2].Cells[3].Value = "";
					num2++;
				}
				while (num2 <= 3);
				DGV2.ClearSelection();
				DGV2.CurrentCell = null;
				gb1.Enabled = false;
				GB2.Enabled = false;
				GB3.Enabled = false;
				return;
			}
			if (Form1_Public.IndexTX > Public2.PRJ.TX.GetUpperBound(0))
			{
				txtP.Text = "";
				txtN.Text = "";
				txtNu.Text = "";
				txtP.Text = "";
				txtG.Text = "";
				CalcEIM();
				int num3 = 0;
				do
				{
					DGV2.Rows[num3].Cells[1].Value = "";
					DGV2.Rows[num3].Cells[2].Value = "";
					DGV2.Rows[num3].Cells[3].Value = "";
					num3++;
				}
				while (num3 <= 3);
				DGV2.ClearSelection();
				DGV2.CurrentCell = null;
				gb1.Enabled = false;
				GB2.Enabled = false;
				GB3.Enabled = false;
				return;
			}
			gb1.Enabled = true;
			GB2.Enabled = true;
			GB3.Enabled = true;
			if (Operators.CompareString(lblP.Text, "W", TextCompare: false) == 0)
			{
				txtP.Text = Conversions.ToString(Public2.PRJ.TX[SelRow].P);
			}
			else if (Operators.CompareString(lblP.Text, "dBW", TextCompare: false) == 0)
			{
				txtP.Text = Conversions.ToString(Math.Round(10.0 * Math.Log10(Public2.PRJ.TX[SelRow].P), 3));
			}
			else if (Operators.CompareString(lblP.Text, "mW", TextCompare: false) == 0)
			{
				txtP.Text = Conversions.ToString(Public2.PRJ.TX[SelRow].P * 1000f);
			}
			else if (Operators.CompareString(lblP.Text, "dBm", TextCompare: false) == 0)
			{
				txtP.Text = Conversions.ToString(Math.Round(30.0 + 10.0 * Math.Log10(Public2.PRJ.TX[SelRow].P), 3));
			}
			txtN.Text = Conversions.ToString(Public2.PRJ.TX[SelRow].N);
			txtNu.Text = Conversions.ToString(Public2.PRJ.TX[SelRow].Loss);
			txtG.Text = Conversions.ToString(Public2.PRJ.TX[SelRow].G);
			CalcEIM();
			int num4 = 0;
			do
			{
				if (Public2.PRJ.SETT.LimitAlpha24[num4])
				{
					DGV2.Rows[num4].Cells[1].Value = Public2.PRJ.TX[SelRow].Alpha24[num4];
				}
				else
				{
					DGV2.Rows[num4].Cells[1].Value = "";
				}
				if (Public2.PRJ.SETT.LimitFtdc[num4])
				{
					DGV2.Rows[num4].Cells[2].Value = Public2.PRJ.TX[SelRow].Ftdc[num4];
				}
				else
				{
					DGV2.Rows[num4].Cells[2].Value = "";
				}
				if (Public2.PRJ.SETT.LimitFpr[num4])
				{
					DGV2.Rows[num4].Cells[3].Value = Public2.PRJ.TX[SelRow].Fpr[num4];
				}
				else
				{
					DGV2.Rows[num4].Cells[3].Value = "";
				}
				num4++;
			}
			while (num4 <= 3);
			DGV2.ClearSelection();
			DGV2.CurrentCell = null;
		}
	}

	private void DGV1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
		if (Information.IsNothing(Public2.PRJ.TX) || !FlagOK)
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
					if (DGV1.SelectedCells[i].RowIndex == Public2.PRJ.TX.Length)
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
					Public2.PRJ.TX[e.RowIndex].Active = Conversions.ToBoolean(DGV1[num, e.RowIndex].Value);
					return;
				}
				string text = Conversions.ToString(DGV1[num, DGV1.SelectedCells[0].RowIndex].Value);
				Color backColor = DGV1.Rows[DGV1.SelectedCells[0].RowIndex].DefaultCellStyle.BackColor;
				int num3 = DGV1.SelectedCells.Count - 1;
				float F = default;
				float F2 = default;
				for (int j = 0; j <= num3; j++)
				{
					switch (num)
					{
					case 1:
						Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].Name = text;
						FlagOK = false;
						DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = text;
						FlagOK = true;
						break;
					case 2:
						Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].TypeOfService = text;
						FlagOK = false;
						DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = text;
						FlagOK = true;
						break;
					case 3:
						if (Information.IsNothing(text))
						{
							Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].F = 1000f;
							Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].F2 = 0f;
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = 1000;
							FlagOK = true;
							break;
						}
						if (Module1.ValidText(text, CheckLimits: true, 30f, 100000000f, IncludeMin: false, IncludeMax: false, Message: false))
						{
							Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].F = Module1.QSNG(text);
							Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].F2 = 0f;
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = Module1.QSNG(text);
							FlagOK = true;
							break;
						}
						if (Form1_Public.FreqSpanCheck(text, ref F, ref F2))
						{
							Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].F = F;
							Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].F2 = F2;
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = Form1_Public.FreqSpanString(F, F2);
							FlagOK = true;
							break;
						}
						FlagOK = false;
						DGV1[num, DGV1.SelectedCells[0].RowIndex].Value = Form1_Public.FreqSpanString(Public2.PRJ.TX[DGV1.SelectedCells[0].RowIndex].F, Public2.PRJ.TX[DGV1.SelectedCells[0].RowIndex].F2);
						FlagOK = true;
						return;
					case 4:
						if (Information.IsNothing(text))
						{
							Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].P = 0f;
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = 0;
							DGV1[19, DGV1.SelectedCells[j].RowIndex].Value = Module1.SuperFormat(0f, 6);
							FlagOK = true;
							break;
						}
						if (Module1.ValidText(text, CheckLimits: true, 0f, 100000f, IncludeMin: true, IncludeMax: true, Message: false))
						{
							Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].P = Module1.QSNG(text);
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = Module1.QSNG(text);
							DGV1[19, DGV1.SelectedCells[j].RowIndex].Value = Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].PTOT();
							FlagOK = true;
							break;
						}
						FlagOK = false;
						DGV1[num, DGV1.SelectedCells[0].RowIndex].Value = Public2.PRJ.TX[DGV1.SelectedCells[0].RowIndex].P;
						FlagOK = true;
						return;
					case 5:
						if (Information.IsNothing(text))
						{
							Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].N = 1;
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = 1;
							DGV1[19, DGV1.SelectedCells[j].RowIndex].Value = Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].PTOT();
							FlagOK = true;
							break;
						}
						if (Module1.ValidText(text, CheckLimits: true, 1f, 100f, IncludeMin: true, IncludeMax: false, Message: false))
						{
							Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].N = Conversions.ToInteger(Conversion.Int(text));
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = RuntimeHelpers.GetObjectValue(Conversion.Int(text));
							DGV1[19, DGV1.SelectedCells[j].RowIndex].Value = Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].PTOT();
							FlagOK = true;
							break;
						}
						FlagOK = false;
						DGV1[num, DGV1.SelectedCells[0].RowIndex].Value = Public2.PRJ.TX[DGV1.SelectedCells[0].RowIndex].N;
						FlagOK = true;
						return;
					case 6:
						if (Information.IsNothing(text))
						{
							Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].Loss = 0f;
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = 0;
							DGV1[19, DGV1.SelectedCells[j].RowIndex].Value = Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].PTOT();
							FlagOK = true;
							break;
						}
						if (Module1.ValidText(text, CheckLimits: true, 0f, 100f, IncludeMin: true, IncludeMax: true, Message: false))
						{
							Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].Loss = Module1.QSNG(text);
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = Module1.QSNG(text);
							DGV1[19, DGV1.SelectedCells[j].RowIndex].Value = Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].PTOT();
							FlagOK = true;
							break;
						}
						FlagOK = false;
						DGV1[num, DGV1.SelectedCells[0].RowIndex].Value = Public2.PRJ.TX[DGV1.SelectedCells[0].RowIndex].Loss;
						FlagOK = true;
						return;
					case 7:
						Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].AntType = text;
						FlagOK = false;
						DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = text;
						FlagOK = true;
						break;
					case 8:
						if (Information.IsNothing(text))
						{
							Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].Az = 0f;
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = 0;
							FlagOK = true;
						}
						else if (Module1.ValidText(text, CheckLimits: true, 0f, 360f, IncludeMin: true, IncludeMax: false, Message: false))
						{
							Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].Az = Module1.QSNG(text);
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = Module1.QSNG(text);
							FlagOK = true;
						}
						else
						{
							FlagOK = false;
							Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].Az = -1f;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = "0÷360";
							FlagOK = true;
						}
						break;
					case 9:
						if (Information.IsNothing(text))
						{
							Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].G = 0f;
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = 0;
							FlagOK = true;
							break;
						}
						if (Module1.ValidText(text, CheckLimits: true, -100f, 100f, IncludeMin: false, IncludeMax: false, Message: false))
						{
							Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].G = Module1.QSNG(text);
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = Module1.QSNG(text);
							FlagOK = true;
							break;
						}
						FlagOK = false;
						DGV1[num, DGV1.SelectedCells[0].RowIndex].Value = Public2.PRJ.TX[DGV1.SelectedCells[0].RowIndex].G;
						FlagOK = true;
						return;
					case 10:
						if (Information.IsNothing(text))
						{
							Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].H = 0f;
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = 0;
							FlagOK = true;
							break;
						}
						if (Module1.ValidText(text, CheckLimits: true, 0f, 1000f, IncludeMin: true, IncludeMax: false, Message: false))
						{
							Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].H = Module1.QSNG(text);
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = Module1.QSNG(text);
							FlagOK = true;
							break;
						}
						FlagOK = false;
						DGV1[num, DGV1.SelectedCells[0].RowIndex].Value = Public2.PRJ.TX[DGV1.SelectedCells[0].RowIndex].H;
						FlagOK = true;
						return;
					case 11:
						if (Information.IsNothing(text))
						{
							Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].D = 1f;
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = 1;
							FlagOK = true;
							break;
						}
						if (Module1.ValidText(text, CheckLimits: true, 0f, 30f, IncludeMin: false, IncludeMax: false, Message: false))
						{
							Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].D = Module1.QSNG(text);
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = Module1.QSNG(text);
							FlagOK = true;
							break;
						}
						FlagOK = false;
						DGV1[num, DGV1.SelectedCells[0].RowIndex].Value = Public2.PRJ.TX[DGV1.SelectedCells[0].RowIndex].D;
						FlagOK = true;
						return;
					case 12:
						if (Information.IsNothing(text))
						{
							Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].Ug = 0f;
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = 0;
							FlagOK = true;
							break;
						}
						if (Module1.ValidText(text, CheckLimits: true, -90f, 90f, IncludeMin: true, IncludeMax: true, Message: false))
						{
							Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].Ug = Module1.QSNG(text);
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = Module1.QSNG(text);
							FlagOK = true;
							break;
						}
						FlagOK = false;
						DGV1[num, DGV1.SelectedCells[0].RowIndex].Value = Public2.PRJ.TX[DGV1.SelectedCells[0].RowIndex].Ug;
						FlagOK = true;
						return;
					case 13:
						if (Information.IsNothing(text))
						{
							Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].ElectUg = 0f;
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = 0;
							FlagOK = true;
							break;
						}
						if (Module1.ValidText(text, CheckLimits: true, -90f, 90f, IncludeMin: true, IncludeMax: true, Message: false))
						{
							Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].ElectUg = Module1.QSNG(text);
							FlagOK = false;
							DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = Module1.QSNG(text);
							FlagOK = true;
							break;
						}
						FlagOK = false;
						DGV1[num, DGV1.SelectedCells[0].RowIndex].Value = Public2.PRJ.TX[DGV1.SelectedCells[0].RowIndex].ElectUg;
						FlagOK = true;
						return;
					case 14:
						Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].UgH = text;
						FlagOK = false;
						DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = text;
						FlagOK = true;
						break;
					case 15:
						Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].UgV = text;
						FlagOK = false;
						DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = text;
						FlagOK = true;
						break;
					case 16:
						Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].Location = text;
						FlagOK = false;
						DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = text;
						FlagOK = true;
						break;
					case 17:
						Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].Exist = text;
						FlagOK = false;
						DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = text;
						FlagOK = true;
						break;
					case 18:
						Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].Owner = text;
						Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].Color = backColor;
						FlagOK = false;
						DGV1[num, DGV1.SelectedCells[j].RowIndex].Value = text;
						DGV1.Rows[DGV1.SelectedCells[j].RowIndex].DefaultCellStyle.BackColor = backColor;
						FlagOK = true;
						break;
					case 20:
						Public2.PRJ.TX[DGV1.SelectedCells[j].RowIndex].Sector = text;
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

	private void DGV1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
	{
		if (e.ColumnIndex != 0 || DGV1.SelectedCells.Count <= 1)
		{
			return;
		}
		checked
		{
			int num = DGV1.SelectedCells.Count - 1;
			int num2 = 0;
			while (true)
			{
				if (num2 <= num)
				{
					if (DGV1.SelectedCells[num2].ColumnIndex == e.ColumnIndex)
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
					Public2.PRJ.TX[DGV1.SelectedCells[i].RowIndex].Active = flag;
				}
				DGV1.Rows[e.RowIndex].Cells[0].Value = Operators.NotObject(DGV1.Rows[e.RowIndex].Cells[0].Value);
				Public2.PRJ.TX[e.RowIndex].Active = Conversions.ToBoolean(Operators.NotObject(DGV1.Rows[e.RowIndex].Cells[0].Value));
				DGV1.ClearSelection();
				break;
			}
		}
	}

	private void DGV1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
	{
		ref Public2.TxPRTO[] tX = ref Public2.PRJ.TX;
		checked
		{
			tX = (Public2.TxPRTO[])Utils.CopyArray(tX, new Public2.TxPRTO[DGV1.Rows.Count - 2 + 1]);
			short num = (short)Public2.PRJ.TX.GetUpperBound(0);
			DGV1.Rows[num].HeaderCell.Value = Strings.Format(num + 1, "000").ToString();
			Public2.PRJ.TX[num].Active = true;
			DGV1[0, num].Value = true;
			Public2.PRJ.TX[num].Name = "";
			Public2.PRJ.TX[num].TypeOfService = "Other";
			Public2.PRJ.TX[num].F = 1000f;
			Public2.PRJ.TX[num].P = 0f;
			Public2.PRJ.TX[num].N = 1;
			Public2.PRJ.TX[num].Loss = 0f;
			Public2.PRJ.TX[num].AntType = "";
			Public2.PRJ.TX[num].Az = 0f;
			Public2.PRJ.TX[num].G = 0f;
			Public2.PRJ.TX[num].H = 0f;
			Public2.PRJ.TX[num].D = 1f;
			Public2.PRJ.TX[num].Ug = 0f;
			Public2.PRJ.TX[num].ElectUg = 0f;
			Public2.PRJ.TX[num].UgH = "";
			Public2.PRJ.TX[num].UgV = "";
			Public2.PRJ.TX[num].Location = "Tower";
			Public2.PRJ.TX[num].Exist = "Existing";
			Public2.PRJ.TX[num].Owner = "";
			Public2.PRJ.TX[num].Sector = "";
			Public2.PRJ.TX[num].CoordWM = Public2.PRJ.SETT.OriginCoordWM;
			Public2.PRJ.TX[num].Alpha24 = new float[4];
			Public2.PRJ.TX[num].Ftdc = new float[4];
			Public2.PRJ.TX[num].Fpr = new float[4];
			int num2 = 0;
			do
			{
				Public2.PRJ.TX[num].Alpha24[num2] = 1f;
				Public2.PRJ.TX[num].Ftdc[num2] = 1f;
				Public2.PRJ.TX[num].Fpr[num2] = 1f;
				num2++;
			}
			while (num2 <= 3);
		}
	}

	private void DGV1_MouseDoubleClick(object sender, MouseEventArgs e)
	{
		if ((DGV1.HitTest(e.X, e.Y).RowIndex < 0) | (DGV1.HitTest(e.X, e.Y).RowIndex == checked(DGV1.RowCount - 1)) | (DGV1.HitTest(e.X, e.Y).ColumnIndex < 0))
		{
			return;
		}
		if ((e.Button == MouseButtons.Left) & (DGV1.HitTest(e.X, e.Y).ColumnIndex == 6))
		{
			DGV1.EndEdit();
			Form1_Public.IndexTX = DGV1.HitTest(e.X, e.Y).RowIndex;
			MyProject.Forms.frmCableCalc.Location = Control.MousePosition;
			MyProject.Forms.frmCableCalc.ShowDialog();
			if (Form1_Public.CableLoss != -999f)
			{
				DGV1[6, Form1_Public.IndexTX].Value = Form1_Public.CableLoss;
			}
		}
		else if ((e.Button == MouseButtons.Left) & (DGV1.HitTest(e.X, e.Y).ColumnIndex == 4))
		{
			DGV1.EndEdit();
			Form1_Public.IndexTX = DGV1.HitTest(e.X, e.Y).RowIndex;
			MyProject.Forms.frmDBW.txtP_W.Text = Conversions.ToString(DGV1[4, Form1_Public.IndexTX].Value);
			MyProject.Forms.frmDBW.Location = new Point(Control.MousePosition.X, Control.MousePosition.Y);
			MyProject.Forms.frmDBW.ShowDialog();
			if (Form1_Public.PowerW != 0f)
			{
				DGV1[4, Form1_Public.IndexTX].Value = Form1_Public.PowerW;
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
					int upperBound = Public2.PRJ.TX.GetUpperBound(0);
					for (int i = 0; i <= upperBound; i++)
					{
						Public2.PRJ.TX[i].Active = true;
					}
				}
				else
				{
					int upperBound2 = Public2.PRJ.TX.GetUpperBound(0);
					for (int j = 0; j <= upperBound2; j++)
					{
						Public2.PRJ.TX[j].Active = false;
					}
				}
				FillDGV1();
				DGV1.ClearSelection();
			}
			else if ((e.RowIndex == -1) & (e.ColumnIndex > 0))
			{
				DGV1.CurrentCell = DGV1[e.ColumnIndex, 0];
				DGV1.ClearSelection();
				for (int k = Public2.PRJ.TX.GetUpperBound(0); k >= 0; k += -1)
				{
					DGV1[e.ColumnIndex, k].Selected = true;
				}
			}
		}
	}

	private void DGV1_MouseClick(object sender, MouseEventArgs e)
	{
		if (!((DGV1.HitTest(e.X, e.Y).RowIndex < 0) | (DGV1.HitTest(e.X, e.Y).ColumnIndex < 0)) && DGV1.HitTest(e.X, e.Y).RowIndex != checked(DGV1.Rows.Count - 1))
		{
			if ((e.Button == MouseButtons.Left) & (DGV1.HitTest(e.X, e.Y).ColumnIndex == 16))
			{
				ContLocation.Show(DGV1, e.Location);
			}
			else if ((e.Button == MouseButtons.Left) & (DGV1.HitTest(e.X, e.Y).ColumnIndex == 17))
			{
				ContExist.Show(DGV1, e.Location);
			}
			else if ((e.Button == MouseButtons.Left) & (DGV1.HitTest(e.X, e.Y).ColumnIndex == 18))
			{
				ContOwner.Show(DGV1, e.Location);
			}
		}
	}

	private void ContLocation_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
	{
		if (Operators.CompareString(e.ClickedItem.Text, "", TextCompare: false) != 0)
		{
			DGV1[16, SelRow].Value = e.ClickedItem.Text;
		}
	}

	private void ContExist_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
	{
		if (Operators.CompareString(e.ClickedItem.Text, "", TextCompare: false) != 0)
		{
			DGV1[17, SelRow].Value = e.ClickedItem.Text;
		}
	}

	private void ContOwner_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
	{
		if (Operators.CompareString(e.ClickedItem.Text, "", TextCompare: false) != 0)
		{
			if (Operators.CompareString(e.ClickedItem.Text, Localization.LocMSG(4), TextCompare: false) == 0)
			{
				DGV1.Rows[SelRow].DefaultCellStyle.BackColor = Color.White;
				DGV1[18, SelRow].Value = "";
			}
			else
			{
				DGV1.Rows[SelRow].DefaultCellStyle.BackColor = e.ClickedItem.BackColor;
				DGV1[18, SelRow].Value = e.ClickedItem.Text;
			}
		}
	}

	private void DGV1_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode != Keys.Delete || Information.IsNothing(Public2.PRJ.TX) || DGV1.SelectedRows.Count < 1 || ((DGV1.SelectedRows.Count == 1) & (DGV1.SelectedRows[0].Index > Public2.PRJ.TX.GetUpperBound(0))))
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
				if (DGV1.SelectedRows[i].Index <= Public2.PRJ.TX.GetUpperBound(0))
				{
					array = (short[])Utils.CopyArray(array, new short[num + 1]);
					array[num] = (short)DGV1.SelectedRows[i].Index;
					num++;
				}
			}
			if (Interaction.MsgBox(Localization.LocMSG(0) + " " + Conversions.ToString(unchecked((int)num)) + " " + Localization.LocMSG(1) + "?", MsgBoxStyle.YesNo) != MsgBoxResult.Yes)
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
				int num7 = Public2.PRJ.TX.GetUpperBound(0) - 1;
				for (int m = num6; m <= num7; m++)
				{
					Public2.PRJ.TX[m] = Module1.DeepClone(Public2.PRJ.TX[m + 1]);
				}
			}
			ref Public2.TxPRTO[] tX = ref Public2.PRJ.TX;
			tX = (Public2.TxPRTO[])Utils.CopyArray(tX, new Public2.TxPRTO[Public2.PRJ.TX.GetUpperBound(0) - num5 + 1]);
			if (Public2.PRJ.TX.Length == 0)
			{
				Public2.PRJ.TX = null;
			}
			FillDGV1();
			DGV1.CurrentCell = DGV1[0, array[array.GetUpperBound(0)]];
			DGV1.Rows[array[array.GetUpperBound(0)]].Selected = true;
			DGV1.FirstDisplayedScrollingRowIndex = array[array.GetUpperBound(0)];
		}
	}

	private void DGV1_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
	{
		checked
		{
			if (e.RowIndex == -1)
			{
				int upperBound = Public2.AppSetup.PrtoOrder.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					Public2.AppSetup.PrtoOrder[i] = (byte)DGV1.Columns[i].DisplayIndex;
				}
			}
		}
	}

	private void cmdCopySelectedTX_Click(object sender, EventArgs e)
	{
		gb1.Focus();
		DGV1.EndEdit();
		if (Information.IsNothing(Public2.PRJ.TX) || DGV1.SelectedRows.Count < 1 || ((DGV1.SelectedRows.Count == 1) & (DGV1.SelectedRows[0].Index > Public2.PRJ.TX.GetUpperBound(0))))
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
				if (DGV1.SelectedRows[i].Index <= Public2.PRJ.TX.GetUpperBound(0))
				{
					array = (short[])Utils.CopyArray(array, new short[num + 1]);
					array[num] = (short)DGV1.SelectedRows[i].Index;
					num++;
				}
			}
			int upperBound = array.GetUpperBound(0);
			for (int j = 0; j <= upperBound; j++)
			{
				int num3 = j + 1;
				int upperBound2 = array.GetUpperBound(0);
				for (int k = num3; k <= upperBound2; k++)
				{
					if (array[k] < array[j])
					{
						short num4 = array[j];
						array[j] = array[k];
						array[k] = num4;
					}
				}
			}
			short num5 = array[array.GetUpperBound(0)];
			ref Public2.TxPRTO[] tX = ref Public2.PRJ.TX;
			tX = (Public2.TxPRTO[])Utils.CopyArray(tX, new Public2.TxPRTO[Public2.PRJ.TX.GetUpperBound(0) + array.GetUpperBound(0) + 1 + 1]);
			int upperBound3 = Public2.PRJ.TX.GetUpperBound(0);
			int num6 = num5 + 1;
			for (int l = upperBound3; l >= num6; l += -1)
			{
				Public2.PRJ.TX[l] = Module1.DeepClone(Public2.PRJ.TX[l - array.GetUpperBound(0) - 1]);
			}
			int upperBound4 = array.GetUpperBound(0);
			for (int m = 0; m <= upperBound4; m++)
			{
				Public2.PRJ.TX[m + num5 + 1] = Module1.DeepClone(Public2.PRJ.TX[array[m]]);
				Public2.PRJ.TX[m + num5 + 1].Name = Public2.PRJ.TX[m + num5 + 1].Name + " - Copy";
			}
			FillDGV1();
			DGV1.CurrentCell = DGV1[0, num5 + 1];
			int upperBound5 = array.GetUpperBound(0);
			for (int n = 0; n <= upperBound5; n++)
			{
				DGV1.Rows[n + num5 + 1].Selected = true;
			}
			DGV1.FirstDisplayedScrollingRowIndex = num5 + 1;
			DGV1.Focus();
		}
	}

	private void cmdUp_Click(object sender, EventArgs e)
	{
		gb1.Focus();
		DGV1.EndEdit();
		if (Information.IsNothing(Public2.PRJ.TX) || DGV1.SelectedRows.Count < 1 || ((DGV1.SelectedRows.Count == 1) & (DGV1.SelectedRows[0].Index > Public2.PRJ.TX.GetUpperBound(0))))
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
					Public2.TxPRTO obj = Module1.DeepClone(Public2.PRJ.TX[array[l] - 1]);
					Public2.PRJ.TX[array[l] - 1] = Module1.DeepClone(Public2.PRJ.TX[array[l]]);
					Public2.PRJ.TX[array[l]] = Module1.DeepClone(obj);
				}
				FillDGV1();
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

	private void cmdDown_Click(object sender, EventArgs e)
	{
		gb1.Focus();
		DGV1.EndEdit();
		if (Information.IsNothing(Public2.PRJ.TX) || DGV1.SelectedRows.Count < 1 || ((DGV1.SelectedRows.Count == 1) & (DGV1.SelectedRows[0].Index > Public2.PRJ.TX.GetUpperBound(0))))
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
					Public2.TxPRTO obj = Module1.DeepClone(Public2.PRJ.TX[array[l] + 1]);
					Public2.PRJ.TX[array[l] + 1] = Module1.DeepClone(Public2.PRJ.TX[array[l]]);
					Public2.PRJ.TX[array[l]] = Module1.DeepClone(obj);
				}
				FillDGV1();
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

	private void CmdMoveTop_Click(object sender, EventArgs e)
	{
		gb1.Focus();
		DGV1.EndEdit();
		if (Information.IsNothing(Public2.PRJ.TX) || DGV1.SelectedRows.Count < 1 || ((DGV1.SelectedRows.Count == 1) & (DGV1.SelectedRows[0].Index > Public2.PRJ.TX.GetUpperBound(0))))
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
			if (array[0] == 0 || array[array.GetUpperBound(0)] == DGV1.Rows.Count - 1)
			{
				return;
			}
			short num4 = 0;
			short num5 = 0;
			int upperBound3 = Public2.PRJ.TX.GetUpperBound(0);
			Public2.TxPRTO[] array2 = default;
			Public2.TxPRTO[] array3 = default;
			for (int l = 0; l <= upperBound3; l++)
			{
				bool flag = false;
				int upperBound4 = array.GetUpperBound(0);
				for (int m = 0; m <= upperBound4; m++)
				{
					if (l == array[m])
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					array2 = (Public2.TxPRTO[])Utils.CopyArray(array2, new Public2.TxPRTO[num4 + 1]);
					array2[num4] = Module1.DeepClone(Public2.PRJ.TX[l]);
					num4++;
				}
				else
				{
					array3 = (Public2.TxPRTO[])Utils.CopyArray(array3, new Public2.TxPRTO[num5 + 1]);
					array3[num5] = Module1.DeepClone(Public2.PRJ.TX[l]);
					num5++;
				}
			}
			num4 = 0;
			int upperBound5 = array2.GetUpperBound(0);
			for (int n = 0; n <= upperBound5; n++)
			{
				Public2.PRJ.TX[num4] = Module1.DeepClone(array2[n]);
				num4++;
			}
			int upperBound6 = array3.GetUpperBound(0);
			for (int num6 = 0; num6 <= upperBound6; num6++)
			{
				Public2.PRJ.TX[num4] = Module1.DeepClone(array3[num6]);
				num4++;
			}
			FillDGV1();
			DGV1.CurrentCell = DGV1[0, 0];
			int upperBound7 = array2.GetUpperBound(0);
			for (int num7 = 0; num7 <= upperBound7; num7++)
			{
				DGV1.Rows[num7].Selected = true;
			}
			DGV1.Focus();
		}
	}

	private void CmdMoveBottom_Click(object sender, EventArgs e)
	{
		gb1.Focus();
		DGV1.EndEdit();
		if (Information.IsNothing(Public2.PRJ.TX) || DGV1.SelectedRows.Count < 1 || ((DGV1.SelectedRows.Count == 1) & (DGV1.SelectedRows[0].Index > Public2.PRJ.TX.GetUpperBound(0))))
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
			if (array[array.GetUpperBound(0)] >= DGV1.Rows.Count - 2)
			{
				return;
			}
			short num4 = 0;
			short num5 = 0;
			int upperBound3 = Public2.PRJ.TX.GetUpperBound(0);
			Public2.TxPRTO[] array2 = default;
			Public2.TxPRTO[] array3 = default;
			for (int l = 0; l <= upperBound3; l++)
			{
				bool flag = false;
				int upperBound4 = array.GetUpperBound(0);
				for (int m = 0; m <= upperBound4; m++)
				{
					if (l == array[m])
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					array2 = (Public2.TxPRTO[])Utils.CopyArray(array2, new Public2.TxPRTO[num4 + 1]);
					array2[num4] = Module1.DeepClone(Public2.PRJ.TX[l]);
					num4++;
				}
				else
				{
					array3 = (Public2.TxPRTO[])Utils.CopyArray(array3, new Public2.TxPRTO[num5 + 1]);
					array3[num5] = Module1.DeepClone(Public2.PRJ.TX[l]);
					num5++;
				}
			}
			num4 = 0;
			int upperBound5 = array3.GetUpperBound(0);
			for (int n = 0; n <= upperBound5; n++)
			{
				Public2.PRJ.TX[num4] = Module1.DeepClone(array3[n]);
				num4++;
			}
			int upperBound6 = array2.GetUpperBound(0);
			for (int num6 = 0; num6 <= upperBound6; num6++)
			{
				Public2.PRJ.TX[num4] = Module1.DeepClone(array2[num6]);
				num4++;
			}
			FillDGV1();
			DGV1.CurrentCell = DGV1[0, Public2.PRJ.TX.GetUpperBound(0)];
			int num7 = array3.GetUpperBound(0) + 1;
			int upperBound7 = Public2.PRJ.TX.GetUpperBound(0);
			for (int num8 = num7; num8 <= upperBound7; num8++)
			{
				DGV1.Rows[num8].Selected = true;
			}
			DGV1.Focus();
		}
	}

	private void cmdCheckUncheck_Click(object sender, EventArgs e)
	{
		gb1.Focus();
		DGV1.EndEdit();
		if (DGV1.Rows.Count == 1)
		{
			return;
		}
		checked
		{
			if (Operators.ConditionalCompareObjectEqual(DGV1[0, 0].Value, false, TextCompare: false))
			{
				int upperBound = Public2.PRJ.TX.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					Public2.PRJ.TX[i].Active = true;
				}
			}
			else
			{
				int upperBound2 = Public2.PRJ.TX.GetUpperBound(0);
				for (int j = 0; j <= upperBound2; j++)
				{
					Public2.PRJ.TX[j].Active = false;
				}
			}
			FillDGV1();
			DGV1.ClearSelection();
		}
	}

	private void cmdCopyToBuffer_Click(object sender, EventArgs e)
	{
		gb1.Focus();
		DGV1.EndEdit();
		if (Information.IsNothing(Public2.PRJ.TX) || DGV1.SelectedRows.Count < 1 || ((DGV1.SelectedRows.Count == 1) & (DGV1.SelectedRows[0].Index > Public2.PRJ.TX.GetUpperBound(0))))
		{
			return;
		}
		Public2.TxPRTO[] array = new Public2.TxPRTO[1];
		short[] array2 = new short[1];
		short num = 0;
		checked
		{
			int num2 = DGV1.SelectedRows.Count - 1;
			for (int i = 0; i <= num2; i++)
			{
				if (i <= Public2.PRJ.TX.GetUpperBound(0))
				{
					array = (Public2.TxPRTO[])Utils.CopyArray(array, new Public2.TxPRTO[num + 1]);
					array2 = (short[])Utils.CopyArray(array2, new short[num + 1]);
					array[num] = Module1.DeepClone(Public2.PRJ.TX[DGV1.SelectedRows[i].Index]);
					array2[num] = (short)DGV1.SelectedRows[i].Index;
					num++;
				}
			}
			int upperBound = array2.GetUpperBound(0);
			for (int j = 0; j <= upperBound; j++)
			{
				int num3 = j + 1;
				int upperBound2 = array2.GetUpperBound(0);
				for (int k = num3; k <= upperBound2; k++)
				{
					if (array2[k] < array2[j])
					{
						short num4 = array2[j];
						array2[j] = array2[k];
						array2[k] = num4;
						Public2.TxPRTO obj = Module1.DeepClone(array[j]);
						array[j] = Module1.DeepClone(array[k]);
						array[k] = Module1.DeepClone(obj);
					}
				}
			}
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			using Stream serializationStream = File.Create(Public2.AppDataFolder + "\\buffer.tmp");
			binaryFormatter.Serialize(serializationStream, array);
		}
	}

	private void cmdPasteFromBuffer_Click(object sender, EventArgs e)
	{
		if (SelRow == 0)
		{
			SelRow = 1;
		}
		DGV1.EndEdit();
		Public2.TxPRTO[] array = default;
		try
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			using Stream serializationStream = File.Open(Public2.AppDataFolder + "\\buffer.tmp", FileMode.Open);
			array = (Public2.TxPRTO[])binaryFormatter.Deserialize(serializationStream);
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			ProjectData.ClearProjectError();
		}
		checked
		{
			if (Information.IsNothing(array))
			{
				Interaction.MsgBox(Localization.LocMSG(8), MsgBoxStyle.Critical);
			}
			else if (array.Length > 0)
			{
				short num;
				if (Information.IsNothing(Public2.PRJ.TX))
				{
					Public2.PRJ.TX = Module1.DeepClone(array);
					num = 0;
				}
				else
				{
					num = (short)DGV1.CurrentCell.RowIndex;
					if (num > Public2.PRJ.TX.GetUpperBound(0))
					{
						num = (short)Public2.PRJ.TX.GetUpperBound(0);
					}
					ref Public2.TxPRTO[] tX = ref Public2.PRJ.TX;
					tX = (Public2.TxPRTO[])Utils.CopyArray(tX, new Public2.TxPRTO[Public2.PRJ.TX.GetUpperBound(0) + array.GetUpperBound(0) + 1 + 1]);
					int upperBound = Public2.PRJ.TX.GetUpperBound(0);
					int num2 = num + array.GetUpperBound(0) + 1;
					for (int i = upperBound; i >= num2; i += -1)
					{
						Public2.PRJ.TX[i] = Module1.DeepClone(Public2.PRJ.TX[i - array.GetUpperBound(0) - 1]);
					}
					int upperBound2 = array.GetUpperBound(0);
					for (int j = 0; j <= upperBound2; j++)
					{
						Public2.PRJ.TX[j + num + 1] = Module1.DeepClone(array[j]);
					}
				}
				FillDGV1();
				DGV1.CurrentCell = DGV1[0, num + 1];
				int upperBound3 = array.GetUpperBound(0);
				for (int k = 0; k <= upperBound3; k++)
				{
					DGV1.Rows[k + num + 1].Selected = true;
				}
				DGV1.FirstDisplayedScrollingRowIndex = num;
				DGV1.Focus();
			}
			else
			{
				Interaction.MsgBox(Localization.LocMSG(8), MsgBoxStyle.Critical);
			}
		}
	}

	private void cmdColorAll_Click(object sender, EventArgs e)
	{
		gb1.Focus();
		DGV1.EndEdit();
		if (DGV1.SelectedRows.Count == 0)
		{
			return;
		}
		checked
		{
			int num = DGV1.SelectedRows.Count - 1;
			for (int i = 0; i <= num; i++)
			{
				if (DGV1.SelectedRows[i].Index < DGV1.RowCount - 1)
				{
					DataGridViewCellStyle defaultCellStyle = DGV1.Rows[DGV1.SelectedRows[i].Index].DefaultCellStyle;
					object obj = NewLateBinding.LateGet(sender, null, "BackColor", new object[0], null, null, null);
					defaultCellStyle.BackColor = ((obj != null) ? ((Color)obj) : default(Color));
					ref Public2.TxPRTO reference = ref Public2.PRJ.TX[DGV1.SelectedRows[i].Index];
					object obj2 = NewLateBinding.LateGet(sender, null, "BackColor", new object[0], null, null, null);
					reference.Color = ((obj2 != null) ? ((Color)obj2) : default(Color));
				}
			}
			DGV1.CurrentCell = DGV1[0, DGV1.SelectedRows[0].Index];
			DGV1.ClearSelection();
		}
	}

	private void cmdWord_Click(object sender, EventArgs e)
	{
		gb1.Focus();
		DGV1.EndEdit();
		ContWord.Show(this, cmdWord.Location);
	}

	private void cmdRadiatingSources_1_Click(object sender, EventArgs e)
	{
		if (Information.IsNothing(Public2.PRJ.TX))
		{
			Interaction.MsgBox(Localization.LocMSG(46));
		}
		else
		{
			RepWordAlias.WordReportOneAlias("{RadiatingSources_1}");
		}
	}

	private void cmdAntennaPatterns_Click(object sender, EventArgs e)
	{
		if (Information.IsNothing(Public2.PRJ.TX))
		{
			Interaction.MsgBox(Localization.LocMSG(46));
		}
		else
		{
			RepWordAlias.WordReportOneAlias("{AntennaPatterns}");
		}
	}

	private void cmdEMFSafetyVolumes1_Click(object sender, EventArgs e)
	{
		if (Information.IsNothing(Public2.PRJ.TX))
		{
			Interaction.MsgBox(Localization.LocMSG(46));
		}
		else
		{
			RepWordAlias.WordReportOneAlias("{EMF_Safety_Volumes1}");
		}
	}

	private void cmdCardinalPointsChart_Click(object sender, EventArgs e)
	{
		RepWordAlias.WordReportOneAlias("{CardinalPointsChart}");
	}

	private void cmdExcel_Click(object sender, EventArgs e)
	{
		gb1.Focus();
		DGV1.EndEdit();
		ContExcel.Show(this, cmdWord.Location);
	}

	private void cmdExcel1_Click(object sender, EventArgs e)
	{
		if (Information.IsNothing(Public2.PRJ.TX))
		{
			Interaction.MsgBox(Localization.LocMSG(46));
		}
		else
		{
			ExcelReports.ExcelExportPRTO1();
		}
	}

	private void cmdImportRadiatingSources_Click(object sender, EventArgs e)
	{
		ExcelReports.ExcelImportPRTO();
		FillDGV1();
	}

	private void cmdLoadDN_Click(object sender, EventArgs e)
	{
		gb1.Focus();
		DGV1.EndEdit();
		checked
		{
			if (SelRow == DGV1.Rows.Count - 1)
			{
				return;
			}
			OFD1.Filter = Localization.LocMSG(2) + "|*.*";
			if (Operators.CompareString(Public2.AppSetup.FolderAntennaPattren, "", TextCompare: false) != 0)
			{
				OFD1.InitialDirectory = Public2.AppSetup.FolderAntennaPattren;
			}
			if (OFD1.ShowDialog() == DialogResult.Cancel)
			{
				return;
			}
			Public2.AppSetup.FolderAntennaPattren = Microsoft.VisualBasic.FileIO.FileSystem.GetFileInfo(OFD1.FileName).DirectoryName;
			ClassDN.AntennaPatternClass antennaPatternClass = ClassDN.LoadAntennaFile(OFD1.FileName);
			if (antennaPatternClass == null)
			{
				Interaction.MsgBox(Localization.LocMSG(3));
				return;
			}
			if ((antennaPatternClass.GR[0].HZ.Count != 360) | (antennaPatternClass.GR[0].VR.Count != 360))
			{
				Interaction.MsgBox(Localization.LocMSG(3));
				return;
			}
			Public2.PRJ.TX[SelRow].AntType = antennaPatternClass.Name;
			Public2.PRJ.TX[SelRow].G = Conversions.ToSingle(antennaPatternClass.GainDBi);
			FlagOK = false;
			DGV1[7, SelRow].Value = antennaPatternClass.Name;
			DGV1[9, SelRow].Value = antennaPatternClass.GainDBi;
			FlagOK = true;
			Public2.PRJ.TX[SelRow].DnH = new float[360];
			Public2.PRJ.TX[SelRow].DnV = new float[360];
			int num = 0;
			do
			{
				Public2.PRJ.TX[SelRow].DnH[num] = Math.Abs(antennaPatternClass.GR[0].HZ[num].G);
				Public2.PRJ.TX[SelRow].DnV[num] = Math.Abs(antennaPatternClass.GR[0].VR[num].G);
				num++;
			}
			while (num <= 359);
			PB1.Refresh();
			if (Information.IsNothing(Public2.PRJ.TX[SelRow].DnH))
			{
				return;
			}
			if ((Public2.PRJ.TX[SelRow].DnH.GetUpperBound(0) == 359) | (Public2.PRJ.TX[SelRow].DnV.GetUpperBound(0) == 359))
			{
				float num2 = 0f;
				float num3 = 0f;
				if ((Public2.PRJ.TX[SelRow].DnH[359] <= 3f) & (Public2.PRJ.TX[SelRow].DnH[0] >= 3f))
				{
					num2 = 359f + (3f - Public2.PRJ.TX[SelRow].DnH[359]) / (Public2.PRJ.TX[SelRow].DnH[0] - Public2.PRJ.TX[SelRow].DnH[359]);
				}
				else
				{
					int num4 = 0;
					do
					{
						if ((Public2.PRJ.TX[SelRow].DnH[num4] <= 3f) & (Public2.PRJ.TX[SelRow].DnH[num4 + 1] >= 3f))
						{
							num2 = (float)num4 + (3f - Public2.PRJ.TX[SelRow].DnH[num4]) / (Public2.PRJ.TX[SelRow].DnH[num4 + 1] - Public2.PRJ.TX[SelRow].DnH[num4]);
							break;
						}
						num4++;
					}
					while (num4 <= 358);
				}
				if ((Public2.PRJ.TX[SelRow].DnH[0] <= 3f) & (Public2.PRJ.TX[SelRow].DnH[359] >= 3f))
				{
					num3 = 359f + (3f - Public2.PRJ.TX[SelRow].DnH[359]) / (Public2.PRJ.TX[SelRow].DnH[0] - Public2.PRJ.TX[SelRow].DnH[359]);
				}
				else
				{
					int num5 = 359;
					do
					{
						if ((Public2.PRJ.TX[SelRow].DnH[num5] <= 3f) & (Public2.PRJ.TX[SelRow].DnH[num5 - 1] >= 3f))
						{
							num3 = (float)(num5 - 1) + (3f - Public2.PRJ.TX[SelRow].DnH[num5 - 1]) / (Public2.PRJ.TX[SelRow].DnH[num5] - Public2.PRJ.TX[SelRow].DnH[num5 - 1]);
							break;
						}
						num5 += -1;
					}
					while (num5 >= 1);
				}
				float num6 = ((!((num2 < 180f) & (num3 > 180f))) ? Math.Abs(num2 - num3) : (num2 + (360f - num3)));
				if ((num6 > 0f) & (num6 < 340f))
				{
					Public2.PRJ.TX[SelRow].UgH = Strings.Format(num6, "0.0");
				}
				else
				{
					Public2.PRJ.TX[SelRow].UgH = Conversions.ToString(360);
				}
				num2 = 0f;
				num3 = 0f;
				if ((Public2.PRJ.TX[SelRow].DnV[359] <= 3f) & (Public2.PRJ.TX[SelRow].DnV[0] >= 3f))
				{
					num2 = 359f + (3f - Public2.PRJ.TX[SelRow].DnV[359]) / (Public2.PRJ.TX[SelRow].DnV[0] - Public2.PRJ.TX[SelRow].DnV[359]);
				}
				else
				{
					int num7 = 0;
					do
					{
						if ((Public2.PRJ.TX[SelRow].DnV[num7] <= 3f) & (Public2.PRJ.TX[SelRow].DnV[num7 + 1] >= 3f))
						{
							num2 = (float)num7 + (3f - Public2.PRJ.TX[SelRow].DnV[num7]) / (Public2.PRJ.TX[SelRow].DnV[num7 + 1] - Public2.PRJ.TX[SelRow].DnV[num7]);
							break;
						}
						num7++;
					}
					while (num7 <= 358);
				}
				if ((Public2.PRJ.TX[SelRow].DnV[0] <= 3f) & (Public2.PRJ.TX[SelRow].DnV[359] >= 3f))
				{
					num3 = 359f + (3f - Public2.PRJ.TX[SelRow].DnV[359]) / (Public2.PRJ.TX[SelRow].DnV[0] - Public2.PRJ.TX[SelRow].DnV[359]);
				}
				else
				{
					int num8 = 359;
					do
					{
						if ((Public2.PRJ.TX[SelRow].DnV[num8] <= 3f) & (Public2.PRJ.TX[SelRow].DnV[num8 - 1] >= 3f))
						{
							num3 = (float)(num8 - 1) + (3f - Public2.PRJ.TX[SelRow].DnV[num8 - 1]) / (Public2.PRJ.TX[SelRow].DnV[num8] - Public2.PRJ.TX[SelRow].DnV[num8 - 1]);
							break;
						}
						num8 += -1;
					}
					while (num8 >= 1);
				}
				num6 = ((!((num2 < 180f) & (num3 > 180f))) ? Math.Abs(num2 - num3) : (num2 + (360f - num3)));
				if ((num6 > 0f) & (num6 < 340f))
				{
					Public2.PRJ.TX[SelRow].UgV = Strings.Format(num6, "0.0");
				}
				else
				{
					Public2.PRJ.TX[SelRow].UgV = Conversions.ToString(360);
				}
			}
			FlagOK = false;
			DGV1[14, SelRow].Value = Public2.PRJ.TX[SelRow].UgH;
			DGV1[15, SelRow].Value = Public2.PRJ.TX[SelRow].UgV;
			FlagOK = true;
		}
	}

	private void cmdLoadDN2_Click(object sender, EventArgs e)
	{
		gb1.Focus();
		DGV1.EndEdit();
		checked
		{
			if (!MyProject.Computer.FileSystem.DirectoryExists(Public2.AppSetup.AntennaFolder))
			{
				Interaction.MsgBox(Localization.LocMSG(9));
			}
			else
			{
				if (SelRow == DGV1.Rows.Count - 1)
				{
					return;
				}
				Form1_Public.ChooseAntennaPuth = "";
				MyProject.Forms.frmAntennasViewer.ShowDialog();
				if (Operators.CompareString(Form1_Public.ChooseAntennaPuth, "", TextCompare: false) == 0)
				{
					return;
				}
				Public2.PRJ.TX[SelRow].AntType = Form1_Public.AntennaName;
				Public2.PRJ.TX[SelRow].G = Conversions.ToSingle(Form1_Public.AntennaGain);
				FlagOK = false;
				DGV1[7, SelRow].Value = Form1_Public.AntennaName;
				DGV1[9, SelRow].Value = Form1_Public.AntennaGain;
				FlagOK = true;
				Public2.PRJ.TX[SelRow].DnH = Module1.DeepClone(Form1_Public.HdnPolNorm_h);
				Public2.PRJ.TX[SelRow].DnV = Module1.DeepClone(Form1_Public.HdnPolNorm_v);
				PB1.Refresh();
				if (Information.IsNothing(Public2.PRJ.TX[SelRow].DnH))
				{
					return;
				}
				if ((Public2.PRJ.TX[SelRow].DnH.GetUpperBound(0) == 359) | (Public2.PRJ.TX[SelRow].DnV.GetUpperBound(0) == 359))
				{
					float num = 0f;
					float num2 = 0f;
					if ((Public2.PRJ.TX[SelRow].DnH[359] <= 3f) & (Public2.PRJ.TX[SelRow].DnH[0] >= 3f))
					{
						num = 359f + (3f - Public2.PRJ.TX[SelRow].DnH[359]) / (Public2.PRJ.TX[SelRow].DnH[0] - Public2.PRJ.TX[SelRow].DnH[359]);
					}
					else
					{
						int num3 = 0;
						do
						{
							if ((Public2.PRJ.TX[SelRow].DnH[num3] <= 3f) & (Public2.PRJ.TX[SelRow].DnH[num3 + 1] >= 3f))
							{
								num = (float)num3 + (3f - Public2.PRJ.TX[SelRow].DnH[num3]) / (Public2.PRJ.TX[SelRow].DnH[num3 + 1] - Public2.PRJ.TX[SelRow].DnH[num3]);
								break;
							}
							num3++;
						}
						while (num3 <= 358);
					}
					if ((Public2.PRJ.TX[SelRow].DnH[0] <= 3f) & (Public2.PRJ.TX[SelRow].DnH[359] >= 3f))
					{
						num2 = 359f + (3f - Public2.PRJ.TX[SelRow].DnH[359]) / (Public2.PRJ.TX[SelRow].DnH[0] - Public2.PRJ.TX[SelRow].DnH[359]);
					}
					else
					{
						int num4 = 359;
						do
						{
							if ((Public2.PRJ.TX[SelRow].DnH[num4] <= 3f) & (Public2.PRJ.TX[SelRow].DnH[num4 - 1] >= 3f))
							{
								num2 = (float)(num4 - 1) + (3f - Public2.PRJ.TX[SelRow].DnH[num4 - 1]) / (Public2.PRJ.TX[SelRow].DnH[num4] - Public2.PRJ.TX[SelRow].DnH[num4 - 1]);
								break;
							}
							num4 += -1;
						}
						while (num4 >= 1);
					}
					float num5 = ((!((num < 180f) & (num2 > 180f))) ? Math.Abs(num - num2) : (num + (360f - num2)));
					if ((num5 > 0f) & (num5 < 340f))
					{
						Public2.PRJ.TX[SelRow].UgH = Strings.Format(num5, "0.0");
					}
					else
					{
						Public2.PRJ.TX[SelRow].UgH = Conversions.ToString(360);
					}
					num = 0f;
					num2 = 0f;
					if ((Public2.PRJ.TX[SelRow].DnV[359] <= 3f) & (Public2.PRJ.TX[SelRow].DnV[0] >= 3f))
					{
						num = 359f + (3f - Public2.PRJ.TX[SelRow].DnV[359]) / (Public2.PRJ.TX[SelRow].DnV[0] - Public2.PRJ.TX[SelRow].DnV[359]);
					}
					else
					{
						int num6 = 0;
						do
						{
							if ((Public2.PRJ.TX[SelRow].DnV[num6] <= 3f) & (Public2.PRJ.TX[SelRow].DnV[num6 + 1] >= 3f))
							{
								num = (float)num6 + (3f - Public2.PRJ.TX[SelRow].DnV[num6]) / (Public2.PRJ.TX[SelRow].DnV[num6 + 1] - Public2.PRJ.TX[SelRow].DnV[num6]);
								break;
							}
							num6++;
						}
						while (num6 <= 358);
					}
					if ((Public2.PRJ.TX[SelRow].DnV[0] <= 3f) & (Public2.PRJ.TX[SelRow].DnV[359] >= 3f))
					{
						num2 = 359f + (3f - Public2.PRJ.TX[SelRow].DnV[359]) / (Public2.PRJ.TX[SelRow].DnV[0] - Public2.PRJ.TX[SelRow].DnV[359]);
					}
					else
					{
						int num7 = 359;
						do
						{
							if ((Public2.PRJ.TX[SelRow].DnV[num7] <= 3f) & (Public2.PRJ.TX[SelRow].DnV[num7 - 1] >= 3f))
							{
								num2 = (float)(num7 - 1) + (3f - Public2.PRJ.TX[SelRow].DnV[num7 - 1]) / (Public2.PRJ.TX[SelRow].DnV[num7] - Public2.PRJ.TX[SelRow].DnV[num7 - 1]);
								break;
							}
							num7 += -1;
						}
						while (num7 >= 1);
					}
					num5 = ((!((num < 180f) & (num2 > 180f))) ? Math.Abs(num - num2) : (num + (360f - num2)));
					if ((num5 > 0f) & (num5 < 340f))
					{
						Public2.PRJ.TX[SelRow].UgV = Strings.Format(num5, "0.0");
					}
					else
					{
						Public2.PRJ.TX[SelRow].UgV = Conversions.ToString(360);
					}
				}
				FlagOK = false;
				DGV1[14, SelRow].Value = Public2.PRJ.TX[SelRow].UgH;
				DGV1[15, SelRow].Value = Public2.PRJ.TX[SelRow].UgV;
				FlagOK = true;
			}
		}
	}

	private void cmdEnvelope_Click(object sender, EventArgs e)
	{
		gb1.Focus();
		DGV1.EndEdit();
		MyProject.Forms.frmEnvelope.ShowDialog();
		if (!Form1_Public.ClickOK)
		{
			return;
		}
		if (Form1_Public.PatEnv == null)
		{
			Interaction.MsgBox(Localization.LocMSG(110));
			return;
		}
		Public2.PRJ.TX[SelRow].AntType = Form1_Public.PatEnv.Name;
		FlagOK = false;
		DGV1[7, SelRow].Value = Form1_Public.PatEnv.Name;
		FlagOK = true;
		Public2.PRJ.TX[SelRow].DnH = new float[360];
		Public2.PRJ.TX[SelRow].DnV = new float[360];
		int num = 0;
		checked
		{
			do
			{
				Public2.PRJ.TX[SelRow].DnH[num] = Math.Abs(Form1_Public.PatEnv.GR[0].HZ[num].G);
				Public2.PRJ.TX[SelRow].DnV[num] = Math.Abs(Form1_Public.PatEnv.GR[0].VR[num].G);
				num++;
			}
			while (num <= 359);
			PB1.Refresh();
			if (Information.IsNothing(Public2.PRJ.TX[SelRow].DnH))
			{
				return;
			}
			if ((Public2.PRJ.TX[SelRow].DnH.GetUpperBound(0) == 359) | (Public2.PRJ.TX[SelRow].DnV.GetUpperBound(0) == 359))
			{
				float num2 = 0f;
				float num3 = 0f;
				if ((Public2.PRJ.TX[SelRow].DnH[359] <= 3f) & (Public2.PRJ.TX[SelRow].DnH[0] >= 3f))
				{
					num2 = 359f + (3f - Public2.PRJ.TX[SelRow].DnH[359]) / (Public2.PRJ.TX[SelRow].DnH[0] - Public2.PRJ.TX[SelRow].DnH[359]);
				}
				else
				{
					int num4 = 0;
					do
					{
						if ((Public2.PRJ.TX[SelRow].DnH[num4] <= 3f) & (Public2.PRJ.TX[SelRow].DnH[num4 + 1] >= 3f))
						{
							num2 = (float)num4 + (3f - Public2.PRJ.TX[SelRow].DnH[num4]) / (Public2.PRJ.TX[SelRow].DnH[num4 + 1] - Public2.PRJ.TX[SelRow].DnH[num4]);
							break;
						}
						num4++;
					}
					while (num4 <= 358);
				}
				if ((Public2.PRJ.TX[SelRow].DnH[0] <= 3f) & (Public2.PRJ.TX[SelRow].DnH[359] >= 3f))
				{
					num3 = 359f + (3f - Public2.PRJ.TX[SelRow].DnH[359]) / (Public2.PRJ.TX[SelRow].DnH[0] - Public2.PRJ.TX[SelRow].DnH[359]);
				}
				else
				{
					int num5 = 359;
					do
					{
						if ((Public2.PRJ.TX[SelRow].DnH[num5] <= 3f) & (Public2.PRJ.TX[SelRow].DnH[num5 - 1] >= 3f))
						{
							num3 = (float)(num5 - 1) + (3f - Public2.PRJ.TX[SelRow].DnH[num5 - 1]) / (Public2.PRJ.TX[SelRow].DnH[num5] - Public2.PRJ.TX[SelRow].DnH[num5 - 1]);
							break;
						}
						num5 += -1;
					}
					while (num5 >= 1);
				}
				float num6 = ((!((num2 < 180f) & (num3 > 180f))) ? Math.Abs(num2 - num3) : (num2 + (360f - num3)));
				if ((num6 > 0f) & (num6 < 340f))
				{
					Public2.PRJ.TX[SelRow].UgH = Strings.Format(num6, "0.0");
				}
				else
				{
					Public2.PRJ.TX[SelRow].UgH = Conversions.ToString(360);
				}
				num2 = 0f;
				num3 = 0f;
				if ((Public2.PRJ.TX[SelRow].DnV[359] <= 3f) & (Public2.PRJ.TX[SelRow].DnV[0] >= 3f))
				{
					num2 = 359f + (3f - Public2.PRJ.TX[SelRow].DnV[359]) / (Public2.PRJ.TX[SelRow].DnV[0] - Public2.PRJ.TX[SelRow].DnV[359]);
				}
				else
				{
					int num7 = 0;
					do
					{
						if ((Public2.PRJ.TX[SelRow].DnV[num7] <= 3f) & (Public2.PRJ.TX[SelRow].DnV[num7 + 1] >= 3f))
						{
							num2 = (float)num7 + (3f - Public2.PRJ.TX[SelRow].DnV[num7]) / (Public2.PRJ.TX[SelRow].DnV[num7 + 1] - Public2.PRJ.TX[SelRow].DnV[num7]);
							break;
						}
						num7++;
					}
					while (num7 <= 358);
				}
				if ((Public2.PRJ.TX[SelRow].DnV[0] <= 3f) & (Public2.PRJ.TX[SelRow].DnV[359] >= 3f))
				{
					num3 = 359f + (3f - Public2.PRJ.TX[SelRow].DnV[359]) / (Public2.PRJ.TX[SelRow].DnV[0] - Public2.PRJ.TX[SelRow].DnV[359]);
				}
				else
				{
					int num8 = 359;
					do
					{
						if ((Public2.PRJ.TX[SelRow].DnV[num8] <= 3f) & (Public2.PRJ.TX[SelRow].DnV[num8 - 1] >= 3f))
						{
							num3 = (float)(num8 - 1) + (3f - Public2.PRJ.TX[SelRow].DnV[num8 - 1]) / (Public2.PRJ.TX[SelRow].DnV[num8] - Public2.PRJ.TX[SelRow].DnV[num8 - 1]);
							break;
						}
						num8 += -1;
					}
					while (num8 >= 1);
				}
				num6 = ((!((num2 < 180f) & (num3 > 180f))) ? Math.Abs(num2 - num3) : (num2 + (360f - num3)));
				if ((num6 > 0f) & (num6 < 340f))
				{
					Public2.PRJ.TX[SelRow].UgV = Strings.Format(num6, "0.0");
				}
				else
				{
					Public2.PRJ.TX[SelRow].UgV = Conversions.ToString(360);
				}
			}
			FlagOK = false;
			DGV1[14, SelRow].Value = Public2.PRJ.TX[SelRow].UgH;
			DGV1[15, SelRow].Value = Public2.PRJ.TX[SelRow].UgV;
			FlagOK = true;
		}
	}

	private void cmdApert_Click(object sender, EventArgs e)
	{
		gb1.Focus();
		DGV1.EndEdit();
		MyProject.Forms.frmParabola.ShowDialog();
		if (!Form1_Public.ClickOK)
		{
			return;
		}
		Public2.PRJ.TX[SelRow].G = Conversions.ToSingle(Form1_Public.PatEnv.GainDBi);
		Public2.PRJ.TX[SelRow].AntType = Form1_Public.PatEnv.Name;
		FlagOK = false;
		DGV1[9, SelRow].Value = Form1_Public.PatEnv.GainDBi;
		DGV1[7, SelRow].Value = Form1_Public.PatEnv.Name;
		FlagOK = true;
		Public2.PRJ.TX[SelRow].DnH = new float[360];
		Public2.PRJ.TX[SelRow].DnV = new float[360];
		int num = 0;
		checked
		{
			do
			{
				Public2.PRJ.TX[SelRow].DnH[num] = Math.Abs(Form1_Public.PatEnv.GR[0].HZ[num].G);
				Public2.PRJ.TX[SelRow].DnV[num] = Math.Abs(Form1_Public.PatEnv.GR[0].VR[num].G);
				num++;
			}
			while (num <= 359);
			PB1.Refresh();
			if (Information.IsNothing(Public2.PRJ.TX[SelRow].DnH))
			{
				return;
			}
			if ((Public2.PRJ.TX[SelRow].DnH.GetUpperBound(0) == 359) | (Public2.PRJ.TX[SelRow].DnV.GetUpperBound(0) == 359))
			{
				float num2 = 0f;
				float num3 = 0f;
				if ((Public2.PRJ.TX[SelRow].DnH[359] <= 3f) & (Public2.PRJ.TX[SelRow].DnH[0] >= 3f))
				{
					num2 = 359f + (3f - Public2.PRJ.TX[SelRow].DnH[359]) / (Public2.PRJ.TX[SelRow].DnH[0] - Public2.PRJ.TX[SelRow].DnH[359]);
				}
				else
				{
					int num4 = 0;
					do
					{
						if ((Public2.PRJ.TX[SelRow].DnH[num4] <= 3f) & (Public2.PRJ.TX[SelRow].DnH[num4 + 1] >= 3f))
						{
							num2 = (float)num4 + (3f - Public2.PRJ.TX[SelRow].DnH[num4]) / (Public2.PRJ.TX[SelRow].DnH[num4 + 1] - Public2.PRJ.TX[SelRow].DnH[num4]);
							break;
						}
						num4++;
					}
					while (num4 <= 358);
				}
				if ((Public2.PRJ.TX[SelRow].DnH[0] <= 3f) & (Public2.PRJ.TX[SelRow].DnH[359] >= 3f))
				{
					num3 = 359f + (3f - Public2.PRJ.TX[SelRow].DnH[359]) / (Public2.PRJ.TX[SelRow].DnH[0] - Public2.PRJ.TX[SelRow].DnH[359]);
				}
				else
				{
					int num5 = 359;
					do
					{
						if ((Public2.PRJ.TX[SelRow].DnH[num5] <= 3f) & (Public2.PRJ.TX[SelRow].DnH[num5 - 1] >= 3f))
						{
							num3 = (float)(num5 - 1) + (3f - Public2.PRJ.TX[SelRow].DnH[num5 - 1]) / (Public2.PRJ.TX[SelRow].DnH[num5] - Public2.PRJ.TX[SelRow].DnH[num5 - 1]);
							break;
						}
						num5 += -1;
					}
					while (num5 >= 1);
				}
				float num6 = ((!((num2 < 180f) & (num3 > 180f))) ? Math.Abs(num2 - num3) : (num2 + (360f - num3)));
				if ((num6 > 0f) & (num6 < 340f))
				{
					Public2.PRJ.TX[SelRow].UgH = Strings.Format(num6, "0.0");
				}
				else
				{
					Public2.PRJ.TX[SelRow].UgH = Conversions.ToString(360);
				}
				num2 = 0f;
				num3 = 0f;
				if ((Public2.PRJ.TX[SelRow].DnV[359] <= 3f) & (Public2.PRJ.TX[SelRow].DnV[0] >= 3f))
				{
					num2 = 359f + (3f - Public2.PRJ.TX[SelRow].DnV[359]) / (Public2.PRJ.TX[SelRow].DnV[0] - Public2.PRJ.TX[SelRow].DnV[359]);
				}
				else
				{
					int num7 = 0;
					do
					{
						if ((Public2.PRJ.TX[SelRow].DnV[num7] <= 3f) & (Public2.PRJ.TX[SelRow].DnV[num7 + 1] >= 3f))
						{
							num2 = (float)num7 + (3f - Public2.PRJ.TX[SelRow].DnV[num7]) / (Public2.PRJ.TX[SelRow].DnV[num7 + 1] - Public2.PRJ.TX[SelRow].DnV[num7]);
							break;
						}
						num7++;
					}
					while (num7 <= 358);
				}
				if ((Public2.PRJ.TX[SelRow].DnV[0] <= 3f) & (Public2.PRJ.TX[SelRow].DnV[359] >= 3f))
				{
					num3 = 359f + (3f - Public2.PRJ.TX[SelRow].DnV[359]) / (Public2.PRJ.TX[SelRow].DnV[0] - Public2.PRJ.TX[SelRow].DnV[359]);
				}
				else
				{
					int num8 = 359;
					do
					{
						if ((Public2.PRJ.TX[SelRow].DnV[num8] <= 3f) & (Public2.PRJ.TX[SelRow].DnV[num8 - 1] >= 3f))
						{
							num3 = (float)(num8 - 1) + (3f - Public2.PRJ.TX[SelRow].DnV[num8 - 1]) / (Public2.PRJ.TX[SelRow].DnV[num8] - Public2.PRJ.TX[SelRow].DnV[num8 - 1]);
							break;
						}
						num8 += -1;
					}
					while (num8 >= 1);
				}
				num6 = ((!((num2 < 180f) & (num3 > 180f))) ? Math.Abs(num2 - num3) : (num2 + (360f - num3)));
				if ((num6 > 0f) & (num6 < 340f))
				{
					Public2.PRJ.TX[SelRow].UgV = Strings.Format(num6, "0.0");
				}
				else
				{
					Public2.PRJ.TX[SelRow].UgV = Conversions.ToString(360);
				}
			}
			FlagOK = false;
			DGV1[14, SelRow].Value = Public2.PRJ.TX[SelRow].UgH;
			DGV1[15, SelRow].Value = Public2.PRJ.TX[SelRow].UgV;
			FlagOK = true;
		}
	}

	private void cmdSaveMSI_Click(object sender, EventArgs e)
	{
		gb1.Focus();
		DGV1.EndEdit();
		checked
		{
			if (SelRow == DGV1.Rows.Count - 1)
			{
				return;
			}
			if (Information.IsNothing(Public2.PRJ.TX[SelRow].DnH) | Information.IsNothing(Public2.PRJ.TX[SelRow].DnV))
			{
				Interaction.MsgBox(Localization.LocMSG(6));
				return;
			}
			if ((Public2.PRJ.TX[SelRow].DnH.GetUpperBound(0) < 359) | (Public2.PRJ.TX[SelRow].DnV.GetUpperBound(0) < 359))
			{
				Interaction.MsgBox(Localization.LocMSG(6));
				return;
			}
			SFD1.Filter = Localization.LocMSG(5) + "|*.msi";
			SFD1.FileName = Module1.Reject0D0A(Public2.PRJ.TX[SelRow].AntType) + ".msi";
			if (Operators.CompareString(Public2.AppSetup.FolderAntennaPattren, "", TextCompare: false) != 0)
			{
				SFD1.InitialDirectory = Public2.AppSetup.FolderAntennaPattren;
			}
			if (SFD1.ShowDialog() == DialogResult.OK)
			{
				Public2.AppSetup.FolderAntennaPattren = Microsoft.VisualBasic.FileIO.FileSystem.GetFileInfo(SFD1.FileName).DirectoryName;
				StreamWriter streamWriter = new StreamWriter(SFD1.FileName, append: false, Encoding.GetEncoding(1251));
				streamWriter.WriteLine("NAME " + Module1.Reject0D0A(Public2.PRJ.TX[SelRow].AntType));
				streamWriter.WriteLine("FREQUENCY " + Module1.ZptToTchk(Conversions.ToString(Public2.PRJ.TX[SelRow].F)));
				streamWriter.WriteLine("GAIN " + Module1.ZptToTchk(Conversions.ToString(Math.Round((double)Public2.PRJ.TX[SelRow].G - 2.15, 2))) + " dBd");
				streamWriter.WriteLine("TILT ");
				streamWriter.WriteLine("COMMENT ");
				streamWriter.WriteLine("HORIZONTAL 360");
				int num = 0;
				do
				{
					streamWriter.WriteLine(Conversions.ToString(num) + " " + Module1.ZptToTchk(Conversions.ToString(Public2.PRJ.TX[SelRow].DnH[num])));
					num++;
				}
				while (num <= 359);
				streamWriter.WriteLine("VERTICAL 360");
				num = 0;
				do
				{
					streamWriter.WriteLine(Conversions.ToString(num) + " " + Module1.ZptToTchk(Conversions.ToString(Public2.PRJ.TX[SelRow].DnV[num])));
					num++;
				}
				while (num <= 359);
				streamWriter.Close();
			}
		}
	}

	private void cmdFillWidthDN_Click(object sender, EventArgs e)
	{
		gb1.Focus();
		if (Information.IsNothing(Public2.PRJ.TX))
		{
			return;
		}
		int upperBound = Public2.PRJ.TX.GetUpperBound(0);
		checked
		{
			for (int i = 0; i <= upperBound; i++)
			{
				if (Information.IsNothing(Public2.PRJ.TX[i].DnH) || !((Public2.PRJ.TX[i].DnH.GetUpperBound(0) == 359) | (Public2.PRJ.TX[i].DnV.GetUpperBound(0) == 359)))
				{
					continue;
				}
				float num = 0f;
				float num2 = 0f;
				if ((Public2.PRJ.TX[i].DnH[359] <= 3f) & (Public2.PRJ.TX[i].DnH[0] >= 3f))
				{
					num = 359f + (3f - Public2.PRJ.TX[i].DnH[359]) / (Public2.PRJ.TX[i].DnH[0] - Public2.PRJ.TX[i].DnH[359]);
				}
				else
				{
					int num3 = 0;
					do
					{
						if ((Public2.PRJ.TX[i].DnH[num3] <= 3f) & (Public2.PRJ.TX[i].DnH[num3 + 1] >= 3f))
						{
							num = (float)num3 + (3f - Public2.PRJ.TX[i].DnH[num3]) / (Public2.PRJ.TX[i].DnH[num3 + 1] - Public2.PRJ.TX[i].DnH[num3]);
							break;
						}
						num3++;
					}
					while (num3 <= 358);
				}
				if ((Public2.PRJ.TX[i].DnH[0] <= 3f) & (Public2.PRJ.TX[i].DnH[359] >= 3f))
				{
					num2 = 359f + (3f - Public2.PRJ.TX[i].DnH[359]) / (Public2.PRJ.TX[i].DnH[0] - Public2.PRJ.TX[i].DnH[359]);
				}
				else
				{
					int num4 = 359;
					do
					{
						if ((Public2.PRJ.TX[i].DnH[num4] <= 3f) & (Public2.PRJ.TX[i].DnH[num4 - 1] >= 3f))
						{
							num2 = (float)(num4 - 1) + (3f - Public2.PRJ.TX[i].DnH[num4 - 1]) / (Public2.PRJ.TX[i].DnH[num4] - Public2.PRJ.TX[i].DnH[num4 - 1]);
							break;
						}
						num4 += -1;
					}
					while (num4 >= 1);
				}
				float num5 = ((!((num < 180f) & (num2 > 180f))) ? Math.Abs(num - num2) : (num + (360f - num2)));
				if ((num5 > 0f) & (num5 < 340f))
				{
					Public2.PRJ.TX[i].UgH = Strings.Format(num5, "0.0");
				}
				else
				{
					Public2.PRJ.TX[i].UgH = Conversions.ToString(360);
				}
				num = 0f;
				num2 = 0f;
				if ((Public2.PRJ.TX[i].DnV[359] <= 3f) & (Public2.PRJ.TX[i].DnV[0] >= 3f))
				{
					num = 359f + (3f - Public2.PRJ.TX[i].DnV[359]) / (Public2.PRJ.TX[i].DnV[0] - Public2.PRJ.TX[i].DnV[359]);
				}
				else
				{
					int num6 = 0;
					do
					{
						if ((Public2.PRJ.TX[i].DnV[num6] <= 3f) & (Public2.PRJ.TX[i].DnV[num6 + 1] >= 3f))
						{
							num = (float)num6 + (3f - Public2.PRJ.TX[i].DnV[num6]) / (Public2.PRJ.TX[i].DnV[num6 + 1] - Public2.PRJ.TX[i].DnV[num6]);
							break;
						}
						num6++;
					}
					while (num6 <= 358);
				}
				if ((Public2.PRJ.TX[i].DnV[0] <= 3f) & (Public2.PRJ.TX[i].DnV[359] >= 3f))
				{
					num2 = 359f + (3f - Public2.PRJ.TX[i].DnV[359]) / (Public2.PRJ.TX[i].DnV[0] - Public2.PRJ.TX[i].DnV[359]);
				}
				else
				{
					int num7 = 359;
					do
					{
						if ((Public2.PRJ.TX[i].DnV[num7] <= 3f) & (Public2.PRJ.TX[i].DnV[num7 - 1] >= 3f))
						{
							num2 = (float)(num7 - 1) + (3f - Public2.PRJ.TX[i].DnV[num7 - 1]) / (Public2.PRJ.TX[i].DnV[num7] - Public2.PRJ.TX[i].DnV[num7 - 1]);
							break;
						}
						num7 += -1;
					}
					while (num7 >= 1);
				}
				num5 = ((!((num < 180f) & (num2 > 180f))) ? Math.Abs(num - num2) : (num + (360f - num2)));
				if ((num5 > 0f) & (num5 < 340f))
				{
					Public2.PRJ.TX[i].UgV = Strings.Format(num5, "0.0");
				}
				else
				{
					Public2.PRJ.TX[i].UgV = Conversions.ToString(360);
				}
			}
			FillDGV1();
		}
	}

	private void cmdFillWidthDnOne_Click(object sender, EventArgs e)
	{
		gb1.Focus();
		if (Information.IsNothing(Public2.PRJ.TX) || SelRow > Public2.PRJ.TX.GetUpperBound(0))
		{
			return;
		}
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.TX[SelRow].DnH) && ((Public2.PRJ.TX[SelRow].DnH.GetUpperBound(0) == 359) | (Public2.PRJ.TX[SelRow].DnV.GetUpperBound(0) == 359)))
			{
				float num = 0f;
				float num2 = 0f;
				if ((Public2.PRJ.TX[SelRow].DnH[359] <= 3f) & (Public2.PRJ.TX[SelRow].DnH[0] >= 3f))
				{
					num = 359f + (3f - Public2.PRJ.TX[SelRow].DnH[359]) / (Public2.PRJ.TX[SelRow].DnH[0] - Public2.PRJ.TX[SelRow].DnH[359]);
				}
				else
				{
					int num3 = 0;
					do
					{
						if ((Public2.PRJ.TX[SelRow].DnH[num3] <= 3f) & (Public2.PRJ.TX[SelRow].DnH[num3 + 1] >= 3f))
						{
							num = (float)num3 + (3f - Public2.PRJ.TX[SelRow].DnH[num3]) / (Public2.PRJ.TX[SelRow].DnH[num3 + 1] - Public2.PRJ.TX[SelRow].DnH[num3]);
							break;
						}
						num3++;
					}
					while (num3 <= 358);
				}
				if ((Public2.PRJ.TX[SelRow].DnH[0] <= 3f) & (Public2.PRJ.TX[SelRow].DnH[359] >= 3f))
				{
					num2 = 359f + (3f - Public2.PRJ.TX[SelRow].DnH[359]) / (Public2.PRJ.TX[SelRow].DnH[0] - Public2.PRJ.TX[SelRow].DnH[359]);
				}
				else
				{
					int num4 = 359;
					do
					{
						if ((Public2.PRJ.TX[SelRow].DnH[num4] <= 3f) & (Public2.PRJ.TX[SelRow].DnH[num4 - 1] >= 3f))
						{
							num2 = (float)(num4 - 1) + (3f - Public2.PRJ.TX[SelRow].DnH[num4 - 1]) / (Public2.PRJ.TX[SelRow].DnH[num4] - Public2.PRJ.TX[SelRow].DnH[num4 - 1]);
							break;
						}
						num4 += -1;
					}
					while (num4 >= 1);
				}
				float num5 = ((!((num < 180f) & (num2 > 180f))) ? Math.Abs(num - num2) : (num + (360f - num2)));
				if ((num5 > 0f) & (num5 < 340f))
				{
					Public2.PRJ.TX[SelRow].UgH = Strings.Format(num5, "0.0");
				}
				else
				{
					Public2.PRJ.TX[SelRow].UgH = Conversions.ToString(360);
				}
				num = 0f;
				num2 = 0f;
				if ((Public2.PRJ.TX[SelRow].DnV[359] <= 3f) & (Public2.PRJ.TX[SelRow].DnV[0] >= 3f))
				{
					num = 359f + (3f - Public2.PRJ.TX[SelRow].DnV[359]) / (Public2.PRJ.TX[SelRow].DnV[0] - Public2.PRJ.TX[SelRow].DnV[359]);
				}
				else
				{
					int num6 = 0;
					do
					{
						if ((Public2.PRJ.TX[SelRow].DnV[num6] <= 3f) & (Public2.PRJ.TX[SelRow].DnV[num6 + 1] >= 3f))
						{
							num = (float)num6 + (3f - Public2.PRJ.TX[SelRow].DnV[num6]) / (Public2.PRJ.TX[SelRow].DnV[num6 + 1] - Public2.PRJ.TX[SelRow].DnV[num6]);
							break;
						}
						num6++;
					}
					while (num6 <= 358);
				}
				if ((Public2.PRJ.TX[SelRow].DnV[0] <= 3f) & (Public2.PRJ.TX[SelRow].DnV[359] >= 3f))
				{
					num2 = 359f + (3f - Public2.PRJ.TX[SelRow].DnV[359]) / (Public2.PRJ.TX[SelRow].DnV[0] - Public2.PRJ.TX[SelRow].DnV[359]);
				}
				else
				{
					int num7 = 359;
					do
					{
						if ((Public2.PRJ.TX[SelRow].DnV[num7] <= 3f) & (Public2.PRJ.TX[SelRow].DnV[num7 - 1] >= 3f))
						{
							num2 = (float)(num7 - 1) + (3f - Public2.PRJ.TX[SelRow].DnV[num7 - 1]) / (Public2.PRJ.TX[SelRow].DnV[num7] - Public2.PRJ.TX[SelRow].DnV[num7 - 1]);
							break;
						}
						num7 += -1;
					}
					while (num7 >= 1);
				}
				num5 = ((!((num < 180f) & (num2 > 180f))) ? Math.Abs(num - num2) : (num + (360f - num2)));
				if ((num5 > 0f) & (num5 < 340f))
				{
					Public2.PRJ.TX[SelRow].UgV = Strings.Format(num5, "0.0");
				}
				else
				{
					Public2.PRJ.TX[SelRow].UgV = Conversions.ToString(360);
				}
			}
			FlagOK = false;
			DGV1[14, SelRow].Value = Public2.PRJ.TX[SelRow].UgH;
			DGV1[15, SelRow].Value = Public2.PRJ.TX[SelRow].UgV;
			FlagOK = true;
		}
	}

	private void cmdEditorDN_Click(object sender, EventArgs e)
	{
		try
		{
			Process.Start("C:\\Program Files (x86)\\AntennaPatternEditor2\\AntennaPatternEditor2.exe");
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Process.Start("https://www.wireless-planning.com/antenna-pattern-editor");
			ProjectData.ClearProjectError();
		}
	}

	private void PB1_Paint(object sender, PaintEventArgs e)
	{
		e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
		e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
		e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
		Pen pen = new Pen(Color.FromArgb(200, 0, 0, 0), 1f);
		Pen pen2 = new Pen(Color.FromArgb(100, 0, 0, 0), 1f);
		Pen pen3 = new Pen(Color.FromArgb(50, 0, 0, 0), 1f);
		Pen pen4 = new Pen(Color.Blue, 2f);
		Pen pen5 = new Pen(Color.Red, 2f);
		checked
		{
			int num = Math.Min(PB1.Width - 5, PB1.Height - 5);
			int num2 = 15;
			int num3 = (int)Math.Round((double)num / 2.0 - (double)num2);
			float num5;
			if (!Information.IsNothing(Public2.PRJ.TX) && SelRow < Public2.PRJ.TX.Length)
			{
				try
				{
					if (!Information.IsNothing(Public2.PRJ.TX[SelRow].DnH))
					{
						Point[] array = new Point[361];
						int num4 = 0;
						do
						{
							num5 = (DbCenter - Public2.PRJ.TX[SelRow].DnH[num4]) / DbCenter;
							if (num5 < 0f)
							{
								num5 = 0f;
							}
							array[num4] = new Point((int)Math.Round(Convert.ToSingle((double)num / 2.0 + (double)num5 * Math.Sin((double)(num4 + 90) / (180.0 / Math.PI)) * (double)num3 + 5.0)), (int)Math.Round(Convert.ToSingle((double)num - (double)num / 2.0 - (double)num5 * Math.Cos((double)(num4 + 90) / (180.0 / Math.PI)) * (double)num3)));
							num4++;
						}
						while (num4 <= 359);
						num5 = (DbCenter - Public2.PRJ.TX[SelRow].DnH[0]) / DbCenter;
						if (num5 < 0f)
						{
							num5 = 0f;
						}
						array[360] = new Point((int)Math.Round(Convert.ToSingle((double)num / 2.0 + (double)num5 * Math.Sin(Math.PI / 2.0) * (double)num3 + 5.0)), (int)Math.Round(Convert.ToSingle((double)num - (double)num / 2.0 - (double)num5 * Math.Cos(Math.PI / 2.0) * (double)num3)));
						e.Graphics.FillPolygon(new SolidBrush(Color.FromArgb(60, 0, 0, 255)), array);
						e.Graphics.DrawLines(pen4, array);
						int num6 = 0;
						do
						{
							num5 = (DbCenter - Public2.PRJ.TX[SelRow].DnV[num6]) / DbCenter;
							if (num5 < 0f)
							{
								num5 = 0f;
							}
							array[num6] = new Point((int)Math.Round(Convert.ToSingle((double)num / 2.0 + (double)num5 * Math.Sin((double)(num6 + 90) / (180.0 / Math.PI)) * (double)num3 + 5.0)), (int)Math.Round(Convert.ToSingle((double)num - (double)num / 2.0 - (double)num5 * Math.Cos((double)(num6 + 90) / (180.0 / Math.PI)) * (double)num3)));
							num6++;
						}
						while (num6 <= 359);
						num5 = (DbCenter - Public2.PRJ.TX[SelRow].DnV[0]) / DbCenter;
						if (num5 < 0f)
						{
							num5 = 0f;
						}
						array[360] = new Point((int)Math.Round(Convert.ToSingle((double)num / 2.0 + (double)num5 * Math.Sin(Math.PI / 2.0) * (double)num3 + 5.0)), (int)Math.Round(Convert.ToSingle((double)num - (double)num / 2.0 - (double)num5 * Math.Cos(Math.PI / 2.0) * (double)num3)));
						e.Graphics.FillPolygon(new SolidBrush(Color.FromArgb(60, 255, 0, 0)), array);
						e.Graphics.DrawLines(pen5, array);
					}
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					ProjectData.ClearProjectError();
				}
			}
			int num7 = 0;
			do
			{
				e.Graphics.DrawLine(pen3, (int)Math.Round((double)num / 2.0 + 5.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin((double)num7 / (180.0 / Math.PI)) * (double)num3 + 5.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos((double)num7 / (180.0 / Math.PI)) * (double)num3));
				num7 += 5;
			}
			while (num7 <= 355);
			int num8 = 0;
			do
			{
				e.Graphics.DrawLine(pen2, (int)Math.Round((double)num / 2.0 + 5.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin((double)num8 / (180.0 / Math.PI)) * (double)num3 + 5.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos((double)num8 / (180.0 / Math.PI)) * (double)num3));
				num8 += 10;
			}
			while (num8 <= 350);
			int num9 = 0;
			do
			{
				e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0 + 5.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin((double)num9 / (180.0 / Math.PI)) * (double)num3 + 5.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos((double)num9 / (180.0 / Math.PI)) * (double)num3));
				StringFormat stringFormat = new StringFormat();
				stringFormat.Alignment = StringAlignment.Center;
				stringFormat.LineAlignment = StringAlignment.Center;
				short num10 = (short)(num9 - 90);
				if (num10 < 0)
				{
					num10 += 360;
				}
				e.Graphics.DrawString(Conversions.ToString(unchecked((int)num10)) + "°", new Font("Arial", 7f, FontStyle.Regular), new SolidBrush(Color.Red), (int)Math.Round((double)num / 2.0 + Math.Sin((double)num9 / (180.0 / Math.PI)) * (double)(num3 + 10) + 5.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos((double)num9 / (180.0 / Math.PI)) * (double)(num3 + 8)), stringFormat);
				num9 += 30;
			}
			while (num9 <= 330);
			e.Graphics.DrawEllipse(pen, num2 + 5, num2, num3 * 2, num3 * 2);
			float dbCenter = DbCenter;
			for (float num11 = 10f; num11 <= dbCenter; num11 += 10f)
			{
				num5 = (num11 - DbCenter) / DbCenter;
				e.Graphics.DrawEllipse(pen, (int)Math.Round((float)num2 + (float)num3 * (1f - num5) + 5f), (int)Math.Round((float)num2 + (float)num3 * (1f - num5)), (int)Math.Round((float)(num3 * 2) * num5), (int)Math.Round((float)(num3 * 2) * num5));
				e.Graphics.DrawString("-" + Conversions.ToString(num11) + "dB", new Font("Arial", 7f, FontStyle.Regular), new SolidBrush(Color.Red), (int)Math.Round((double)num / 2.0 - 2.0 + 5.0), (float)(num - num2) - (float)num3 * (1f + num5) + 1f);
			}
			Pen pen6 = new Pen(Color.Gray, 1f);
			pen6.DashStyle = DashStyle.Custom;
			pen6.DashPattern = new float[2] { 5f, 2f };
			num5 = (3f - DbCenter) / DbCenter;
			e.Graphics.DrawEllipse(pen6, (int)Math.Round((float)num2 + (float)num3 * (1f - num5) + 5f), (int)Math.Round((float)num2 + (float)num3 * (1f - num5)), (int)Math.Round((float)(num3 * 2) * num5), (int)Math.Round((float)(num3 * 2) * num5));
			e.Graphics.DrawString("-3dB", new Font("Arial", 7f, FontStyle.Regular), new SolidBrush(Color.Red), (int)Math.Round((double)num / 2.0 - 2.0 + 5.0), (float)(num - num2) - (float)num3 * (1f - num5) + 1f);
		}
	}

	private void cmdVertPatternDown_Click(object sender, EventArgs e)
	{
		gb1.Focus();
		if (SelRow != DGV1.Rows.Count && Public2.PRJ.TX[SelRow].DnV.GetUpperBound(0) == 359)
		{
			Module1.TurnDn(ref Public2.PRJ.TX[SelRow].DnV, Conversions.ToString(-1));
			PB1.Refresh();
			Public2.PRJ.TX[SelRow].ElectUg = Public2.PRJ.TX[SelRow].ElectUg - 1f;
			DGV1.Rows[SelRow].Cells[13].Value = Public2.PRJ.TX[SelRow].ElectUg;
			DGV1.CurrentCell = DGV1[13, SelRow];
		}
	}

	private void cmdVertPatternUp_Click(object sender, EventArgs e)
	{
		gb1.Focus();
		if (SelRow != DGV1.Rows.Count && Public2.PRJ.TX[SelRow].DnV.GetUpperBound(0) == 359)
		{
			Module1.TurnDn(ref Public2.PRJ.TX[SelRow].DnV, Conversions.ToString(1));
			PB1.Refresh();
			Public2.PRJ.TX[SelRow].ElectUg = Public2.PRJ.TX[SelRow].ElectUg + 1f;
			DGV1.Rows[SelRow].Cells[13].Value = Public2.PRJ.TX[SelRow].ElectUg;
			DGV1.CurrentCell = DGV1[13, SelRow];
		}
	}

	private void lblP_Click(object sender, EventArgs e)
	{
		LblName = "lblP";
		cmsDBW.Show(GB2, lblP.Location);
	}

	private void lblPant_Click(object sender, EventArgs e)
	{
		LblName = "lblPant";
		cmsDBW.Show(GB2, lblPant.Location);
	}

	private void lblEIM_Click(object sender, EventArgs e)
	{
		LblName = "lblEIM";
		cmsDBW.Show(GB2, lblEIM.Location);
	}

	private void cmsDBW_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
	{
		if (Operators.CompareString(LblName, "lblP", TextCompare: false) == 0)
		{
			lblP.Text = e.ClickedItem.Text;
		}
		else if (Operators.CompareString(LblName, "lblPant", TextCompare: false) == 0)
		{
			lblPant.Text = e.ClickedItem.Text;
		}
		else if (Operators.CompareString(LblName, "lblEIM", TextCompare: false) == 0)
		{
			lblEIM.Text = e.ClickedItem.Text;
		}
	}

	private void cmdCalcP_Click(object sender, EventArgs e)
	{
		gb1.Focus();
		double num2 = default;
		try
		{
			double num = Conversions.ToSingle(txtPant.Text);
			if (Operators.CompareString(lblPant.Text, "W", TextCompare: false) == 0)
			{
				num2 = num;
			}
			else if (Operators.CompareString(lblPant.Text, "mW", TextCompare: false) == 0)
			{
				num2 = num / 1000.0;
			}
			else if (Operators.CompareString(lblPant.Text, "dBW", TextCompare: false) == 0)
			{
				num2 = Math.Pow(10.0, num / 10.0);
			}
			else if (Operators.CompareString(lblPant.Text, "dBm", TextCompare: false) == 0)
			{
				num2 = Math.Pow(10.0, (num - 30.0) / 10.0);
			}
			if (num2 <= 0.0)
			{
				num2 = -99999999.0;
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			num2 = -99999999.0;
			ProjectData.ClearProjectError();
		}
		double num3;
		try
		{
			double num = Conversions.ToSingle(txtN.Text);
			if (num <= 0.0)
			{
				num3 = -99999999.0;
			}
			num3 = num;
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			num3 = -99999999.0;
			ProjectData.ClearProjectError();
		}
		double num4;
		try
		{
			double num = Conversions.ToSingle(txtNu.Text);
			if (num < 0.0)
			{
				num4 = -99999999.0;
			}
			num4 = num;
		}
		catch (Exception projectError3)
		{
			ProjectData.SetProjectError(projectError3);
			num4 = -99999999.0;
			ProjectData.ClearProjectError();
		}
		double num5;
		try
		{
			double num = Conversions.ToSingle(txtG.Text);
			num5 = num;
		}
		catch (Exception projectError4)
		{
			ProjectData.SetProjectError(projectError4);
			num5 = -99999999.0;
			ProjectData.ClearProjectError();
		}
		if ((num2 == -99999999.0) | (num3 == -99999999.0) | (num4 == -99999999.0) | (num5 == -99999999.0))
		{
			txtP.Text = "";
			txtEIM.Text = "";
			return;
		}
		double d = num2;
		double num6 = 10.0 * Math.Log10(d);
		double num7 = num6 + num4;
		double num8 = Math.Pow(10.0, num7 / 10.0) / num3;
		double value = num8 * 1000.0;
		double num9 = 10.0 * Math.Log10(num8);
		double value2 = num9 + 30.0;
		double num10 = num6 + num5;
		Math.Pow(10.0, num10 / 10.0);
		if (Operators.CompareString(lblP.Text, "W", TextCompare: false) == 0)
		{
			txtP.Text = Conversions.ToString(Math.Round(num8, 3));
		}
		else if (Operators.CompareString(lblP.Text, "mW", TextCompare: false) == 0)
		{
			txtP.Text = Conversions.ToString(Math.Round(value, 3));
		}
		else if (Operators.CompareString(lblP.Text, "dBW", TextCompare: false) == 0)
		{
			txtP.Text = Conversions.ToString(Math.Round(num9, 3));
		}
		else if (Operators.CompareString(lblP.Text, "dBm", TextCompare: false) == 0)
		{
			txtP.Text = Conversions.ToString(Math.Round(value2, 3));
		}
	}

	private void cmdCalcNu_Click(object sender, EventArgs e)
	{
		gb1.Focus();
		double num2 = default;
		try
		{
			double num = Conversions.ToSingle(txtP.Text);
			if (Operators.CompareString(lblP.Text, "W", TextCompare: false) == 0)
			{
				num2 = num;
			}
			else if (Operators.CompareString(lblP.Text, "mW", TextCompare: false) == 0)
			{
				num2 = num / 1000.0;
			}
			else if (Operators.CompareString(lblP.Text, "dBW", TextCompare: false) == 0)
			{
				num2 = Math.Pow(10.0, num / 10.0);
			}
			else if (Operators.CompareString(lblP.Text, "dBm", TextCompare: false) == 0)
			{
				num2 = Math.Pow(10.0, (num - 30.0) / 10.0);
			}
			if (num2 <= 0.0)
			{
				num2 = -99999999.0;
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			num2 = -99999999.0;
			ProjectData.ClearProjectError();
		}
		double num3 = default;
		try
		{
			double num = Conversions.ToSingle(txtPant.Text);
			if (Operators.CompareString(lblPant.Text, "W", TextCompare: false) == 0)
			{
				num3 = num;
			}
			else if (Operators.CompareString(lblPant.Text, "mW", TextCompare: false) == 0)
			{
				num3 = num / 1000.0;
			}
			else if (Operators.CompareString(lblPant.Text, "dBW", TextCompare: false) == 0)
			{
				num3 = Math.Pow(10.0, num / 10.0);
			}
			else if (Operators.CompareString(lblPant.Text, "dBm", TextCompare: false) == 0)
			{
				num3 = Math.Pow(10.0, (num - 30.0) / 10.0);
			}
			if (num3 <= 0.0)
			{
				num3 = -99999999.0;
			}
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			num3 = -99999999.0;
			ProjectData.ClearProjectError();
		}
		double num4;
		try
		{
			double num = Conversions.ToSingle(txtN.Text);
			if (num <= 0.0)
			{
				num4 = -99999999.0;
			}
			num4 = num;
		}
		catch (Exception projectError3)
		{
			ProjectData.SetProjectError(projectError3);
			num4 = -99999999.0;
			ProjectData.ClearProjectError();
		}
		double num5;
		try
		{
			double num = Conversions.ToSingle(txtG.Text);
			num5 = num;
		}
		catch (Exception projectError4)
		{
			ProjectData.SetProjectError(projectError4);
			num5 = -99999999.0;
			ProjectData.ClearProjectError();
		}
		if ((num3 == -99999999.0) | (num4 == -99999999.0) | (num2 == -99999999.0) | (num5 == -99999999.0))
		{
			txtNu.Text = "";
			txtEIM.Text = "";
			return;
		}
		double num6 = 10.0 * Math.Log10(num2 * num4);
		double d = num3;
		double num7 = 10.0 * Math.Log10(d);
		double num8 = num6 - num7;
		if (num8 < 0.0)
		{
			txtNu.Text = "";
			txtEIM.Text = "";
		}
		else
		{
			double num9 = num7 + num5;
			Math.Pow(10.0, num9 / 10.0);
			txtNu.Text = Conversions.ToString(Math.Round(num8, 3));
		}
	}

	private void cmdCalcEIM_Click(object sender, EventArgs e)
	{
		gb1.Focus();
		CalcEIM();
	}

	public void CalcEIM()
	{
		if (!FlagOK || !GB2.Enabled)
		{
			return;
		}
		double num2 = default;
		try
		{
			double num = Conversions.ToSingle(txtP.Text);
			if (Operators.CompareString(lblP.Text, "W", TextCompare: false) == 0)
			{
				num2 = num;
			}
			else if (Operators.CompareString(lblP.Text, "mW", TextCompare: false) == 0)
			{
				num2 = num / 1000.0;
			}
			else if (Operators.CompareString(lblP.Text, "dBW", TextCompare: false) == 0)
			{
				num2 = Math.Pow(10.0, num / 10.0);
			}
			else if (Operators.CompareString(lblP.Text, "dBm", TextCompare: false) == 0)
			{
				num2 = Math.Pow(10.0, (num - 30.0) / 10.0);
			}
			if (num2 <= 0.0)
			{
				num2 = -99999999.0;
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			num2 = -99999999.0;
			ProjectData.ClearProjectError();
		}
		double num3;
		try
		{
			double num = Conversions.ToSingle(txtN.Text);
			if (num <= 0.0)
			{
				num3 = -99999999.0;
			}
			num3 = num;
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			num3 = -99999999.0;
			ProjectData.ClearProjectError();
		}
		double num4;
		try
		{
			double num = Conversions.ToSingle(txtNu.Text);
			if (num < 0.0)
			{
				num4 = -99999999.0;
			}
			num4 = num;
		}
		catch (Exception projectError3)
		{
			ProjectData.SetProjectError(projectError3);
			num4 = -99999999.0;
			ProjectData.ClearProjectError();
		}
		double num5;
		try
		{
			double num = Conversions.ToSingle(txtG.Text);
			num5 = num;
		}
		catch (Exception projectError4)
		{
			ProjectData.SetProjectError(projectError4);
			num5 = -99999999.0;
			ProjectData.ClearProjectError();
		}
		if ((num2 == -99999999.0) | (num3 == -99999999.0) | (num4 == -99999999.0) | (num5 == -99999999.0))
		{
			txtPant.Text = "";
			txtEIM.Text = "";
			return;
		}
		double num6 = 10.0 * Math.Log10(num2 * num3) - num4;
		double value = num6 + 30.0;
		double num7 = Math.Pow(10.0, num6 / 10.0);
		double value2 = num7 * 1000.0;
		double num8 = num6 + num5;
		double value3 = num8 + 30.0;
		double num9 = Math.Pow(10.0, num8 / 10.0);
		double value4 = num9 * 1000.0;
		if (Operators.CompareString(lblPant.Text, "W", TextCompare: false) == 0)
		{
			txtPant.Text = Conversions.ToString(Math.Round(num7, 3));
		}
		else if (Operators.CompareString(lblPant.Text, "mW", TextCompare: false) == 0)
		{
			txtPant.Text = Conversions.ToString(Math.Round(value2, 3));
		}
		else if (Operators.CompareString(lblPant.Text, "dBW", TextCompare: false) == 0)
		{
			txtPant.Text = Conversions.ToString(Math.Round(num6, 3));
		}
		else if (Operators.CompareString(lblPant.Text, "dBm", TextCompare: false) == 0)
		{
			txtPant.Text = Conversions.ToString(Math.Round(value, 3));
		}
		if (Operators.CompareString(lblEIM.Text, "W", TextCompare: false) == 0)
		{
			txtEIM.Text = Conversions.ToString(Math.Round(num9, 1));
		}
		else if (Operators.CompareString(lblEIM.Text, "mW", TextCompare: false) == 0)
		{
			txtEIM.Text = Conversions.ToString(Math.Round(value4, 3));
		}
		else if (Operators.CompareString(lblEIM.Text, "dBW", TextCompare: false) == 0)
		{
			txtEIM.Text = Conversions.ToString(Math.Round(num8, 1));
		}
		else if (Operators.CompareString(lblEIM.Text, "dBm", TextCompare: false) == 0)
		{
			txtEIM.Text = Conversions.ToString(Math.Round(value3, 1));
		}
	}

	private void DGV2_CellValidated(object sender, DataGridViewCellEventArgs e)
	{
		if (!Information.IsNothing(Public2.PRJ.TX) && Form1_Public.IndexTX >= 0 && Form1_Public.IndexTX <= Public2.PRJ.TX.GetUpperBound(0) && FlagOK && e.ColumnIndex >= 1)
		{
			if ((e.ColumnIndex == 1) & Public2.PRJ.SETT.LimitAlpha24[e.RowIndex])
			{
				DataGridViewCell dataGridViewCell = DGV2.Rows[e.RowIndex].Cells[1];
				string TextProperty = Conversions.ToString(dataGridViewCell.Value);
				ref float reference = ref Public2.PRJ.TX[Form1_Public.IndexTX].Alpha24[e.RowIndex];
				ref float reference2 = ref reference;
				double DblVariable = reference;
				Module1.TextPropertyValidating(ref TextProperty, ref DblVariable, CheckLimits: true, 0.1f, 1f, IncludeMin: true, IncludeMax: true, Message: false);
				reference2 = (float)DblVariable;
				dataGridViewCell.Value = TextProperty;
			}
			if ((e.ColumnIndex == 2) & Public2.PRJ.SETT.LimitFtdc[e.RowIndex])
			{
				DataGridViewCell dataGridViewCell2 = DGV2.Rows[e.RowIndex].Cells[2];
				string TextProperty = Conversions.ToString(dataGridViewCell2.Value);
				ref float reference3 = ref Public2.PRJ.TX[Form1_Public.IndexTX].Ftdc[e.RowIndex];
				ref float reference2 = ref reference3;
				double DblVariable = reference3;
				Module1.TextPropertyValidating(ref TextProperty, ref DblVariable, CheckLimits: true, 0.1f, 1f, IncludeMin: true, IncludeMax: true, Message: false);
				reference2 = (float)DblVariable;
				dataGridViewCell2.Value = TextProperty;
			}
			if ((e.ColumnIndex == 3) & Public2.PRJ.SETT.LimitFpr[e.RowIndex])
			{
				DataGridViewCell dataGridViewCell3 = DGV2.Rows[e.RowIndex].Cells[3];
				string TextProperty = Conversions.ToString(dataGridViewCell3.Value);
				ref float reference4 = ref Public2.PRJ.TX[Form1_Public.IndexTX].Fpr[e.RowIndex];
				ref float reference2 = ref reference4;
				double DblVariable = reference4;
				Module1.TextPropertyValidating(ref TextProperty, ref DblVariable, CheckLimits: true, 0.1f, 1f, IncludeMin: true, IncludeMax: true, Message: false);
				reference2 = (float)DblVariable;
				dataGridViewCell3.Value = TextProperty;
			}
		}
	}
}
