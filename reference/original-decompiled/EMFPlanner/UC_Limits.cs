using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[DesignerGenerated]
public class UC_Limits : UserControl
{
	public struct LimPoint
	{
		public float F;

		public float E;

		public float A;

		public LimPoint(float _f, float _e, float _a)
		{
			this = default;
			F = _f;
			E = _e;
			A = _a;
		}
	}

	private IContainer components;

	private bool FlagProcessing;

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
			DataGridViewCellEventHandler value2 = DGV1_CellValidated;
			DataGridViewRowEventHandler value3 = DGV1_UserAddedRow;
			DataGridViewRowCancelEventHandler value4 = DGV1_UserDeletingRow;
			DataGridView dataGridView = field;
			if (dataGridView != null)
			{
				dataGridView.CellValidated -= value2;
				dataGridView.UserAddedRow -= value3;
				dataGridView.UserDeletingRow -= value4;
			}
			field = value;
			dataGridView = field;
			if (dataGridView != null)
			{
				dataGridView.CellValidated += value2;
				dataGridView.UserAddedRow += value3;
				dataGridView.UserDeletingRow += value4;
			}
		}
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

	[field: AccessedThroughProperty("Label2")]
	internal virtual Label Label2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtLimitDescription2")]
	internal virtual TextBox txtLimitDescription2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = txtLimitDescription2_TextChanged;
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

	[field: AccessedThroughProperty("Label1")]
	internal virtual Label Label1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtLimitDescription1")]
	internal virtual TextBox txtLimitDescription1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = txtLimitDescription1_TextChanged;
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

	[field: AccessedThroughProperty("cmdItaly")]
	internal virtual Button cmdItaly
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdItaly_Click;
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

	[field: AccessedThroughProperty("cmdBelgium")]
	internal virtual Button cmdBelgium
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdBelgium_Click;
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

	[field: AccessedThroughProperty("cmdICNIRP")]
	internal virtual Button cmdICNIRP
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdICNIRP_Click;
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

	[field: AccessedThroughProperty("cmdEU")]
	internal virtual Button cmdEU
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdEU_Click;
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

	[field: AccessedThroughProperty("cmdUSA")]
	internal virtual Button cmdUSA
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdUSA_Click;
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

	[field: AccessedThroughProperty("cmdSwitzerland")]
	internal virtual Button cmdSwitzerland
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdSwitzerland_Click;
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

	[field: AccessedThroughProperty("cmdUK")]
	internal virtual Button cmdUK
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdUK_Click;
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

	[field: AccessedThroughProperty("PictureBox1")]
	internal virtual PictureBox PictureBox1
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

	[field: AccessedThroughProperty("txtLimitDescription4")]
	internal virtual TextBox txtLimitDescription4
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = txtLimitDescription4_TextChanged;
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

	[field: AccessedThroughProperty("Label3")]
	internal virtual Label Label3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtLimitDescription3")]
	internal virtual TextBox txtLimitDescription3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = txtLimitDescription3_TextChanged;
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

	[field: AccessedThroughProperty("Label6")]
	internal virtual Label Label6
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

	[field: AccessedThroughProperty("pbColor4")]
	internal virtual PictureBox pbColor4
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = pbColor4_Click;
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

	[field: AccessedThroughProperty("pbColor3")]
	internal virtual PictureBox pbColor3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = pbColor3_Click;
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

	[field: AccessedThroughProperty("pbColor2")]
	internal virtual PictureBox pbColor2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = pbColor2_Click;
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

	[field: AccessedThroughProperty("pbColor1")]
	internal virtual PictureBox pbColor1
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

	[field: AccessedThroughProperty("CD1")]
	internal virtual ColorDialog CD1
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

	[field: AccessedThroughProperty("cmdBulgaria")]
	internal virtual Button cmdBulgaria
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdBulgaria_Click;
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

	[field: AccessedThroughProperty("cmdCroatia")]
	internal virtual Button cmdCroatia
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdCroatia_Click;
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

	[field: AccessedThroughProperty("cmdSlovenia")]
	internal virtual Button cmdSlovenia
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdSlovenia_Click;
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

	[field: AccessedThroughProperty("cmdGreece")]
	internal virtual Button cmdGreece
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdGreece_Click;
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

	[field: AccessedThroughProperty("cmdIndia")]
	internal virtual Button cmdIndia
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdIndia_Click;
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

	[field: AccessedThroughProperty("cmdLithuania")]
	internal virtual Button cmdLithuania
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdLithuania_Click;
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

	[field: AccessedThroughProperty("cmdRussia")]
	internal virtual Button cmdRussia
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdRussia_Click;
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

	[field: AccessedThroughProperty("cmdPoland")]
	internal virtual Button cmdPoland
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdPoland_Click;
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

	[field: AccessedThroughProperty("cmdChina")]
	internal virtual Button cmdChina
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdChina_Click;
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

	[field: AccessedThroughProperty("Label9")]
	internal virtual Label Label9
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

	[field: AccessedThroughProperty("Label7")]
	internal virtual Label Label7
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("chkFprL4")]
	internal virtual CheckBox chkFprL4
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = chkFprL4_CheckedChanged;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.CheckedChanged -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.CheckedChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("chkFtdcL4")]
	internal virtual CheckBox chkFtdcL4
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = chkFtdcL4_CheckedChanged;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.CheckedChanged -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.CheckedChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("chkAlpha24L4")]
	internal virtual CheckBox chkAlpha24L4
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = chkAlpha24L4_CheckedChanged;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.CheckedChanged -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.CheckedChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("chkFprL3")]
	internal virtual CheckBox chkFprL3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = chkFprL3_CheckedChanged;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.CheckedChanged -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.CheckedChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("chkFtdcL3")]
	internal virtual CheckBox chkFtdcL3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = chkFtdcL3_CheckedChanged;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.CheckedChanged -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.CheckedChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("chkAlpha24L3")]
	internal virtual CheckBox chkAlpha24L3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = chkAlpha24L3_CheckedChanged;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.CheckedChanged -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.CheckedChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("chkFprL2")]
	internal virtual CheckBox chkFprL2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = chkFprL2_CheckedChanged;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.CheckedChanged -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.CheckedChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("chkFtdcL2")]
	internal virtual CheckBox chkFtdcL2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = chkFtdcL2_CheckedChanged;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.CheckedChanged -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.CheckedChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("chkAlpha24L2")]
	internal virtual CheckBox chkAlpha24L2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = chkAlpha24L2_CheckedChanged;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.CheckedChanged -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.CheckedChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("chkFprL1")]
	internal virtual CheckBox chkFprL1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = chkFprL1_CheckedChanged;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.CheckedChanged -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.CheckedChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("chkFtdcL1")]
	internal virtual CheckBox chkFtdcL1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = chkFtdcL1_CheckedChanged;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.CheckedChanged -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.CheckedChanged += value2;
			}
		}
	}

	[field: AccessedThroughProperty("chkAlpha24L1")]
	internal virtual CheckBox chkAlpha24L1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = chkAlpha24L1_CheckedChanged;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.CheckedChanged -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.CheckedChanged += value2;
			}
		}
	}

	public UC_Limits()
	{
		Load += UC_Limits_Load;
		FlagProcessing = true;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EMFPlanner.UC_Limits));
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
		this.Label9 = new System.Windows.Forms.Label();
		this.Label8 = new System.Windows.Forms.Label();
		this.Label7 = new System.Windows.Forms.Label();
		this.chkFprL4 = new System.Windows.Forms.CheckBox();
		this.chkFtdcL4 = new System.Windows.Forms.CheckBox();
		this.chkAlpha24L4 = new System.Windows.Forms.CheckBox();
		this.chkFprL3 = new System.Windows.Forms.CheckBox();
		this.chkFtdcL3 = new System.Windows.Forms.CheckBox();
		this.chkAlpha24L3 = new System.Windows.Forms.CheckBox();
		this.chkFprL2 = new System.Windows.Forms.CheckBox();
		this.chkFtdcL2 = new System.Windows.Forms.CheckBox();
		this.chkAlpha24L2 = new System.Windows.Forms.CheckBox();
		this.chkFprL1 = new System.Windows.Forms.CheckBox();
		this.chkFtdcL1 = new System.Windows.Forms.CheckBox();
		this.chkAlpha24L1 = new System.Windows.Forms.CheckBox();
		this.cmdChina = new System.Windows.Forms.Button();
		this.cmdRussia = new System.Windows.Forms.Button();
		this.cmdPoland = new System.Windows.Forms.Button();
		this.cmdIndia = new System.Windows.Forms.Button();
		this.cmdLithuania = new System.Windows.Forms.Button();
		this.cmdSlovenia = new System.Windows.Forms.Button();
		this.cmdGreece = new System.Windows.Forms.Button();
		this.cmdCroatia = new System.Windows.Forms.Button();
		this.cmdBulgaria = new System.Windows.Forms.Button();
		this.pbColor4 = new System.Windows.Forms.PictureBox();
		this.pbColor3 = new System.Windows.Forms.PictureBox();
		this.pbColor2 = new System.Windows.Forms.PictureBox();
		this.pbColor1 = new System.Windows.Forms.PictureBox();
		this.Label6 = new System.Windows.Forms.Label();
		this.Label5 = new System.Windows.Forms.Label();
		this.Label4 = new System.Windows.Forms.Label();
		this.txtLimitDescription4 = new System.Windows.Forms.TextBox();
		this.Label3 = new System.Windows.Forms.Label();
		this.txtLimitDescription3 = new System.Windows.Forms.TextBox();
		this.PictureBox1 = new System.Windows.Forms.PictureBox();
		this.cmdICNIRP = new System.Windows.Forms.Button();
		this.cmdEU = new System.Windows.Forms.Button();
		this.cmdUSA = new System.Windows.Forms.Button();
		this.cmdSwitzerland = new System.Windows.Forms.Button();
		this.cmdUK = new System.Windows.Forms.Button();
		this.cmdBelgium = new System.Windows.Forms.Button();
		this.cmdItaly = new System.Windows.Forms.Button();
		this.PB = new System.Windows.Forms.PictureBox();
		this.Label2 = new System.Windows.Forms.Label();
		this.txtLimitDescription2 = new System.Windows.Forms.TextBox();
		this.Label1 = new System.Windows.Forms.Label();
		this.txtLimitDescription1 = new System.Windows.Forms.TextBox();
		this.DGV1 = new System.Windows.Forms.DataGridView();
		this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.CD1 = new System.Windows.Forms.ColorDialog();
		this.GroupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pbColor4).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pbColor3).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pbColor2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pbColor1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.PictureBox1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.PB).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.DGV1).BeginInit();
		base.SuspendLayout();
		this.GroupBox1.Controls.Add(this.Label9);
		this.GroupBox1.Controls.Add(this.Label8);
		this.GroupBox1.Controls.Add(this.Label7);
		this.GroupBox1.Controls.Add(this.chkFprL4);
		this.GroupBox1.Controls.Add(this.chkFtdcL4);
		this.GroupBox1.Controls.Add(this.chkAlpha24L4);
		this.GroupBox1.Controls.Add(this.chkFprL3);
		this.GroupBox1.Controls.Add(this.chkFtdcL3);
		this.GroupBox1.Controls.Add(this.chkAlpha24L3);
		this.GroupBox1.Controls.Add(this.chkFprL2);
		this.GroupBox1.Controls.Add(this.chkFtdcL2);
		this.GroupBox1.Controls.Add(this.chkAlpha24L2);
		this.GroupBox1.Controls.Add(this.chkFprL1);
		this.GroupBox1.Controls.Add(this.chkFtdcL1);
		this.GroupBox1.Controls.Add(this.chkAlpha24L1);
		this.GroupBox1.Controls.Add(this.cmdChina);
		this.GroupBox1.Controls.Add(this.cmdRussia);
		this.GroupBox1.Controls.Add(this.cmdPoland);
		this.GroupBox1.Controls.Add(this.cmdIndia);
		this.GroupBox1.Controls.Add(this.cmdLithuania);
		this.GroupBox1.Controls.Add(this.cmdSlovenia);
		this.GroupBox1.Controls.Add(this.cmdGreece);
		this.GroupBox1.Controls.Add(this.cmdCroatia);
		this.GroupBox1.Controls.Add(this.cmdBulgaria);
		this.GroupBox1.Controls.Add(this.pbColor4);
		this.GroupBox1.Controls.Add(this.pbColor3);
		this.GroupBox1.Controls.Add(this.pbColor2);
		this.GroupBox1.Controls.Add(this.pbColor1);
		this.GroupBox1.Controls.Add(this.Label6);
		this.GroupBox1.Controls.Add(this.Label5);
		this.GroupBox1.Controls.Add(this.Label4);
		this.GroupBox1.Controls.Add(this.txtLimitDescription4);
		this.GroupBox1.Controls.Add(this.Label3);
		this.GroupBox1.Controls.Add(this.txtLimitDescription3);
		this.GroupBox1.Controls.Add(this.PictureBox1);
		this.GroupBox1.Controls.Add(this.cmdICNIRP);
		this.GroupBox1.Controls.Add(this.cmdEU);
		this.GroupBox1.Controls.Add(this.cmdUSA);
		this.GroupBox1.Controls.Add(this.cmdSwitzerland);
		this.GroupBox1.Controls.Add(this.cmdUK);
		this.GroupBox1.Controls.Add(this.cmdBelgium);
		this.GroupBox1.Controls.Add(this.cmdItaly);
		this.GroupBox1.Controls.Add(this.PB);
		this.GroupBox1.Controls.Add(this.Label2);
		this.GroupBox1.Controls.Add(this.txtLimitDescription2);
		this.GroupBox1.Controls.Add(this.Label1);
		this.GroupBox1.Controls.Add(this.txtLimitDescription1);
		this.GroupBox1.Controls.Add(this.DGV1);
		this.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GroupBox1.Location = new System.Drawing.Point(0, 0);
		this.GroupBox1.Name = "GroupBox1";
		this.GroupBox1.Size = new System.Drawing.Size(1220, 705);
		this.GroupBox1.TabIndex = 4;
		this.GroupBox1.TabStop = false;
		this.GroupBox1.Text = "EMF Limits";
		this.Label9.AutoSize = true;
		this.Label9.Location = new System.Drawing.Point(473, 64);
		this.Label9.Name = "Label9";
		this.Label9.Size = new System.Drawing.Size(22, 13);
		this.Label9.TabIndex = 240;
		this.Label9.Text = "Fpr";
		this.Label8.AutoSize = true;
		this.Label8.Location = new System.Drawing.Point(443, 64);
		this.Label8.Name = "Label8";
		this.Label8.Size = new System.Drawing.Size(28, 13);
		this.Label8.TabIndex = 239;
		this.Label8.Text = "Ftdc";
		this.Label7.AutoSize = true;
		this.Label7.Location = new System.Drawing.Point(417, 64);
		this.Label7.Name = "Label7";
		this.Label7.Size = new System.Drawing.Size(26, 13);
		this.Label7.TabIndex = 238;
		this.Label7.Text = "ɑ24";
		this.chkFprL4.AutoSize = true;
		this.chkFprL4.Location = new System.Drawing.Point(476, 214);
		this.chkFprL4.Name = "chkFprL4";
		this.chkFprL4.Size = new System.Drawing.Size(15, 14);
		this.chkFprL4.TabIndex = 237;
		this.chkFprL4.UseVisualStyleBackColor = true;
		this.chkFtdcL4.AutoSize = true;
		this.chkFtdcL4.Location = new System.Drawing.Point(449, 214);
		this.chkFtdcL4.Name = "chkFtdcL4";
		this.chkFtdcL4.Size = new System.Drawing.Size(15, 14);
		this.chkFtdcL4.TabIndex = 236;
		this.chkFtdcL4.UseVisualStyleBackColor = true;
		this.chkAlpha24L4.AutoSize = true;
		this.chkAlpha24L4.Location = new System.Drawing.Point(422, 214);
		this.chkAlpha24L4.Name = "chkAlpha24L4";
		this.chkAlpha24L4.Size = new System.Drawing.Size(15, 14);
		this.chkAlpha24L4.TabIndex = 235;
		this.chkAlpha24L4.UseVisualStyleBackColor = true;
		this.chkFprL3.AutoSize = true;
		this.chkFprL3.Location = new System.Drawing.Point(476, 173);
		this.chkFprL3.Name = "chkFprL3";
		this.chkFprL3.Size = new System.Drawing.Size(15, 14);
		this.chkFprL3.TabIndex = 234;
		this.chkFprL3.UseVisualStyleBackColor = true;
		this.chkFtdcL3.AutoSize = true;
		this.chkFtdcL3.Location = new System.Drawing.Point(449, 173);
		this.chkFtdcL3.Name = "chkFtdcL3";
		this.chkFtdcL3.Size = new System.Drawing.Size(15, 14);
		this.chkFtdcL3.TabIndex = 233;
		this.chkFtdcL3.UseVisualStyleBackColor = true;
		this.chkAlpha24L3.AutoSize = true;
		this.chkAlpha24L3.Location = new System.Drawing.Point(422, 173);
		this.chkAlpha24L3.Name = "chkAlpha24L3";
		this.chkAlpha24L3.Size = new System.Drawing.Size(15, 14);
		this.chkAlpha24L3.TabIndex = 232;
		this.chkAlpha24L3.UseVisualStyleBackColor = true;
		this.chkFprL2.AutoSize = true;
		this.chkFprL2.Location = new System.Drawing.Point(476, 132);
		this.chkFprL2.Name = "chkFprL2";
		this.chkFprL2.Size = new System.Drawing.Size(15, 14);
		this.chkFprL2.TabIndex = 231;
		this.chkFprL2.UseVisualStyleBackColor = true;
		this.chkFtdcL2.AutoSize = true;
		this.chkFtdcL2.Location = new System.Drawing.Point(449, 132);
		this.chkFtdcL2.Name = "chkFtdcL2";
		this.chkFtdcL2.Size = new System.Drawing.Size(15, 14);
		this.chkFtdcL2.TabIndex = 230;
		this.chkFtdcL2.UseVisualStyleBackColor = true;
		this.chkAlpha24L2.AutoSize = true;
		this.chkAlpha24L2.Location = new System.Drawing.Point(422, 132);
		this.chkAlpha24L2.Name = "chkAlpha24L2";
		this.chkAlpha24L2.Size = new System.Drawing.Size(15, 14);
		this.chkAlpha24L2.TabIndex = 229;
		this.chkAlpha24L2.UseVisualStyleBackColor = true;
		this.chkFprL1.AutoSize = true;
		this.chkFprL1.Location = new System.Drawing.Point(476, 91);
		this.chkFprL1.Name = "chkFprL1";
		this.chkFprL1.Size = new System.Drawing.Size(15, 14);
		this.chkFprL1.TabIndex = 228;
		this.chkFprL1.UseVisualStyleBackColor = true;
		this.chkFtdcL1.AutoSize = true;
		this.chkFtdcL1.Location = new System.Drawing.Point(449, 91);
		this.chkFtdcL1.Name = "chkFtdcL1";
		this.chkFtdcL1.Size = new System.Drawing.Size(15, 14);
		this.chkFtdcL1.TabIndex = 227;
		this.chkFtdcL1.UseVisualStyleBackColor = true;
		this.chkAlpha24L1.AutoSize = true;
		this.chkAlpha24L1.Location = new System.Drawing.Point(422, 91);
		this.chkAlpha24L1.Name = "chkAlpha24L1";
		this.chkAlpha24L1.Size = new System.Drawing.Size(15, 14);
		this.chkAlpha24L1.TabIndex = 226;
		this.chkAlpha24L1.UseVisualStyleBackColor = true;
		this.cmdChina.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdChina.Image = (System.Drawing.Image)resources.GetObject("cmdChina.Image");
		this.cmdChina.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdChina.Location = new System.Drawing.Point(136, 19);
		this.cmdChina.Name = "cmdChina";
		this.cmdChina.Size = new System.Drawing.Size(30, 30);
		this.cmdChina.TabIndex = 225;
		this.cmdChina.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdChina, "Set EMF limits for China");
		this.cmdChina.UseVisualStyleBackColor = true;
		this.cmdRussia.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdRussia.Image = (System.Drawing.Image)resources.GetObject("cmdRussia.Image");
		this.cmdRussia.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdRussia.Location = new System.Drawing.Point(360, 19);
		this.cmdRussia.Name = "cmdRussia";
		this.cmdRussia.Size = new System.Drawing.Size(30, 30);
		this.cmdRussia.TabIndex = 224;
		this.cmdRussia.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdRussia, "Set EMF limits for Russia");
		this.cmdRussia.UseVisualStyleBackColor = true;
		this.cmdPoland.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdPoland.Image = (System.Drawing.Image)resources.GetObject("cmdPoland.Image");
		this.cmdPoland.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdPoland.Location = new System.Drawing.Point(328, 19);
		this.cmdPoland.Name = "cmdPoland";
		this.cmdPoland.Size = new System.Drawing.Size(30, 30);
		this.cmdPoland.TabIndex = 223;
		this.cmdPoland.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdPoland, "Set EMF limits for Poland");
		this.cmdPoland.UseVisualStyleBackColor = true;
		this.cmdIndia.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdIndia.Image = (System.Drawing.Image)resources.GetObject("cmdIndia.Image");
		this.cmdIndia.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdIndia.Location = new System.Drawing.Point(232, 19);
		this.cmdIndia.Name = "cmdIndia";
		this.cmdIndia.Size = new System.Drawing.Size(30, 30);
		this.cmdIndia.TabIndex = 222;
		this.cmdIndia.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdIndia, "Set EMF limits for India");
		this.cmdIndia.UseVisualStyleBackColor = true;
		this.cmdLithuania.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdLithuania.Image = (System.Drawing.Image)resources.GetObject("cmdLithuania.Image");
		this.cmdLithuania.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdLithuania.Location = new System.Drawing.Point(296, 19);
		this.cmdLithuania.Name = "cmdLithuania";
		this.cmdLithuania.Size = new System.Drawing.Size(30, 30);
		this.cmdLithuania.TabIndex = 221;
		this.cmdLithuania.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdLithuania, "Set EMF limits for Lithuania");
		this.cmdLithuania.UseVisualStyleBackColor = true;
		this.cmdSlovenia.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdSlovenia.Image = (System.Drawing.Image)resources.GetObject("cmdSlovenia.Image");
		this.cmdSlovenia.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdSlovenia.Location = new System.Drawing.Point(392, 19);
		this.cmdSlovenia.Name = "cmdSlovenia";
		this.cmdSlovenia.Size = new System.Drawing.Size(30, 30);
		this.cmdSlovenia.TabIndex = 220;
		this.cmdSlovenia.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdSlovenia, "Set EMF limits for Slovenia");
		this.cmdSlovenia.UseVisualStyleBackColor = true;
		this.cmdGreece.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdGreece.Image = (System.Drawing.Image)resources.GetObject("cmdGreece.Image");
		this.cmdGreece.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdGreece.Location = new System.Drawing.Point(200, 19);
		this.cmdGreece.Name = "cmdGreece";
		this.cmdGreece.Size = new System.Drawing.Size(30, 30);
		this.cmdGreece.TabIndex = 219;
		this.cmdGreece.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdGreece, "Set EMF limits for Greece");
		this.cmdGreece.UseVisualStyleBackColor = true;
		this.cmdCroatia.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdCroatia.Image = (System.Drawing.Image)resources.GetObject("cmdCroatia.Image");
		this.cmdCroatia.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdCroatia.Location = new System.Drawing.Point(168, 19);
		this.cmdCroatia.Name = "cmdCroatia";
		this.cmdCroatia.Size = new System.Drawing.Size(30, 30);
		this.cmdCroatia.TabIndex = 218;
		this.cmdCroatia.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdCroatia, "Set EMF limits for Croatia");
		this.cmdCroatia.UseVisualStyleBackColor = true;
		this.cmdBulgaria.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdBulgaria.Image = (System.Drawing.Image)resources.GetObject("cmdBulgaria.Image");
		this.cmdBulgaria.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdBulgaria.Location = new System.Drawing.Point(104, 19);
		this.cmdBulgaria.Name = "cmdBulgaria";
		this.cmdBulgaria.Size = new System.Drawing.Size(30, 30);
		this.cmdBulgaria.TabIndex = 217;
		this.cmdBulgaria.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdBulgaria, "Set EMF limits for Bulgaria");
		this.cmdBulgaria.UseVisualStyleBackColor = true;
		this.pbColor4.BackColor = System.Drawing.Color.Blue;
		this.pbColor4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.pbColor4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.pbColor4.Location = new System.Drawing.Point(498, 203);
		this.pbColor4.Name = "pbColor4";
		this.pbColor4.Size = new System.Drawing.Size(35, 35);
		this.pbColor4.TabIndex = 216;
		this.pbColor4.TabStop = false;
		this.pbColor3.BackColor = System.Drawing.Color.Blue;
		this.pbColor3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.pbColor3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.pbColor3.Location = new System.Drawing.Point(498, 162);
		this.pbColor3.Name = "pbColor3";
		this.pbColor3.Size = new System.Drawing.Size(35, 35);
		this.pbColor3.TabIndex = 215;
		this.pbColor3.TabStop = false;
		this.pbColor2.BackColor = System.Drawing.Color.Blue;
		this.pbColor2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.pbColor2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.pbColor2.Location = new System.Drawing.Point(498, 121);
		this.pbColor2.Name = "pbColor2";
		this.pbColor2.Size = new System.Drawing.Size(35, 35);
		this.pbColor2.TabIndex = 214;
		this.pbColor2.TabStop = false;
		this.pbColor1.BackColor = System.Drawing.Color.Blue;
		this.pbColor1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.pbColor1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.pbColor1.Location = new System.Drawing.Point(498, 80);
		this.pbColor1.Name = "pbColor1";
		this.pbColor1.Size = new System.Drawing.Size(35, 35);
		this.pbColor1.TabIndex = 213;
		this.pbColor1.TabStop = false;
		this.Label6.AutoSize = true;
		this.Label6.Location = new System.Drawing.Point(502, 64);
		this.Label6.Name = "Label6";
		this.Label6.Size = new System.Drawing.Size(31, 13);
		this.Label6.TabIndex = 212;
		this.Label6.Text = "Color";
		this.Label5.AutoSize = true;
		this.Label5.Location = new System.Drawing.Point(202, 64);
		this.Label5.Name = "Label5";
		this.Label5.Size = new System.Drawing.Size(60, 13);
		this.Label5.TabIndex = 211;
		this.Label5.Text = "Description";
		this.Label4.AutoSize = true;
		this.Label4.Location = new System.Drawing.Point(17, 214);
		this.Label4.Name = "Label4";
		this.Label4.Size = new System.Drawing.Size(67, 13);
		this.Label4.TabIndex = 210;
		this.Label4.Text = "EMF Limits 4";
		this.txtLimitDescription4.Location = new System.Drawing.Point(93, 203);
		this.txtLimitDescription4.Multiline = true;
		this.txtLimitDescription4.Name = "txtLimitDescription4";
		this.txtLimitDescription4.Size = new System.Drawing.Size(320, 35);
		this.txtLimitDescription4.TabIndex = 209;
		this.Label3.AutoSize = true;
		this.Label3.Location = new System.Drawing.Point(17, 173);
		this.Label3.Name = "Label3";
		this.Label3.Size = new System.Drawing.Size(67, 13);
		this.Label3.TabIndex = 208;
		this.Label3.Text = "EMF Limits 3";
		this.txtLimitDescription3.Location = new System.Drawing.Point(93, 162);
		this.txtLimitDescription3.Multiline = true;
		this.txtLimitDescription3.Name = "txtLimitDescription3";
		this.txtLimitDescription3.Size = new System.Drawing.Size(320, 35);
		this.txtLimitDescription3.TabIndex = 207;
		this.PictureBox1.Image = (System.Drawing.Image)resources.GetObject("PictureBox1.Image");
		this.PictureBox1.Location = new System.Drawing.Point(8, 447);
		this.PictureBox1.Name = "PictureBox1";
		this.PictureBox1.Size = new System.Drawing.Size(100, 26);
		this.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.PictureBox1.TabIndex = 206;
		this.PictureBox1.TabStop = false;
		this.cmdICNIRP.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdICNIRP.Image = (System.Drawing.Image)resources.GetObject("cmdICNIRP.Image");
		this.cmdICNIRP.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdICNIRP.Location = new System.Drawing.Point(8, 19);
		this.cmdICNIRP.Name = "cmdICNIRP";
		this.cmdICNIRP.Size = new System.Drawing.Size(30, 30);
		this.cmdICNIRP.TabIndex = 205;
		this.cmdICNIRP.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdICNIRP, "Set EMF limits for ICNIRP");
		this.cmdICNIRP.UseVisualStyleBackColor = true;
		this.cmdEU.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdEU.Image = (System.Drawing.Image)resources.GetObject("cmdEU.Image");
		this.cmdEU.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdEU.Location = new System.Drawing.Point(40, 19);
		this.cmdEU.Name = "cmdEU";
		this.cmdEU.Size = new System.Drawing.Size(30, 30);
		this.cmdEU.TabIndex = 204;
		this.cmdEU.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdEU, "Set EMF limits for European Union");
		this.cmdEU.UseVisualStyleBackColor = true;
		this.cmdUSA.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdUSA.Image = (System.Drawing.Image)resources.GetObject("cmdUSA.Image");
		this.cmdUSA.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdUSA.Location = new System.Drawing.Point(488, 19);
		this.cmdUSA.Name = "cmdUSA";
		this.cmdUSA.Size = new System.Drawing.Size(30, 30);
		this.cmdUSA.TabIndex = 203;
		this.cmdUSA.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdUSA, "Set EMF limits for USA");
		this.cmdUSA.UseVisualStyleBackColor = true;
		this.cmdSwitzerland.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdSwitzerland.Image = (System.Drawing.Image)resources.GetObject("cmdSwitzerland.Image");
		this.cmdSwitzerland.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdSwitzerland.Location = new System.Drawing.Point(424, 19);
		this.cmdSwitzerland.Name = "cmdSwitzerland";
		this.cmdSwitzerland.Size = new System.Drawing.Size(30, 30);
		this.cmdSwitzerland.TabIndex = 202;
		this.cmdSwitzerland.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdSwitzerland, "Set EMF limits for Switzerland");
		this.cmdSwitzerland.UseVisualStyleBackColor = true;
		this.cmdUK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdUK.Image = (System.Drawing.Image)resources.GetObject("cmdUK.Image");
		this.cmdUK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdUK.Location = new System.Drawing.Point(456, 19);
		this.cmdUK.Name = "cmdUK";
		this.cmdUK.Size = new System.Drawing.Size(30, 30);
		this.cmdUK.TabIndex = 201;
		this.cmdUK.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdUK, "Set EMF limits for United Kingdom ");
		this.cmdUK.UseVisualStyleBackColor = true;
		this.cmdBelgium.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdBelgium.Image = (System.Drawing.Image)resources.GetObject("cmdBelgium.Image");
		this.cmdBelgium.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdBelgium.Location = new System.Drawing.Point(72, 19);
		this.cmdBelgium.Name = "cmdBelgium";
		this.cmdBelgium.Size = new System.Drawing.Size(30, 30);
		this.cmdBelgium.TabIndex = 199;
		this.cmdBelgium.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdBelgium, "Set EMF limits for Belgium");
		this.cmdBelgium.UseVisualStyleBackColor = true;
		this.cmdItaly.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdItaly.Image = (System.Drawing.Image)resources.GetObject("cmdItaly.Image");
		this.cmdItaly.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdItaly.Location = new System.Drawing.Point(264, 19);
		this.cmdItaly.Name = "cmdItaly";
		this.cmdItaly.Size = new System.Drawing.Size(30, 30);
		this.cmdItaly.TabIndex = 198;
		this.cmdItaly.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdItaly, "Set EMF limits for Italy");
		this.cmdItaly.UseVisualStyleBackColor = true;
		this.PB.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.PB.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.PB.Location = new System.Drawing.Point(545, 57);
		this.PB.Name = "PB";
		this.PB.Size = new System.Drawing.Size(668, 393);
		this.PB.TabIndex = 7;
		this.PB.TabStop = false;
		this.Label2.AutoSize = true;
		this.Label2.Location = new System.Drawing.Point(17, 132);
		this.Label2.Name = "Label2";
		this.Label2.Size = new System.Drawing.Size(67, 13);
		this.Label2.TabIndex = 6;
		this.Label2.Text = "EMF Limits 2";
		this.txtLimitDescription2.Location = new System.Drawing.Point(93, 121);
		this.txtLimitDescription2.Multiline = true;
		this.txtLimitDescription2.Name = "txtLimitDescription2";
		this.txtLimitDescription2.Size = new System.Drawing.Size(320, 35);
		this.txtLimitDescription2.TabIndex = 5;
		this.Label1.AutoSize = true;
		this.Label1.Location = new System.Drawing.Point(17, 91);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(67, 13);
		this.Label1.TabIndex = 4;
		this.Label1.Text = "EMF Limits 1";
		this.txtLimitDescription1.Location = new System.Drawing.Point(93, 80);
		this.txtLimitDescription1.Multiline = true;
		this.txtLimitDescription1.Name = "txtLimitDescription1";
		this.txtLimitDescription1.Size = new System.Drawing.Size(320, 35);
		this.txtLimitDescription1.TabIndex = 3;
		this.DGV1.AllowUserToResizeColumns = false;
		this.DGV1.AllowUserToResizeRows = false;
		this.DGV1.BackgroundColor = System.Drawing.SystemColors.MenuBar;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.DGV1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.DGV1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
		this.DGV1.Columns.AddRange(this.Column1, this.Column2, this.Column3, this.Column4, this.Column5, this.Column6, this.Column7, this.Column8, this.Column9);
		this.DGV1.Location = new System.Drawing.Point(12, 245);
		this.DGV1.MultiSelect = false;
		this.DGV1.Name = "DGV1";
		this.DGV1.ShowCellErrors = false;
		this.DGV1.ShowCellToolTips = false;
		this.DGV1.ShowEditingIcon = false;
		this.DGV1.ShowRowErrors = false;
		this.DGV1.Size = new System.Drawing.Size(527, 196);
		this.DGV1.TabIndex = 0;
		this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Column1.DefaultCellStyle = dataGridViewCellStyle2;
		this.Column1.HeaderText = "Frequency to F, MHz";
		this.Column1.Name = "Column1";
		this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Column2.DefaultCellStyle = dataGridViewCellStyle3;
		this.Column2.HeaderText = "Limit E1, V/m";
		this.Column2.Name = "Column2";
		this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column2.Width = 55;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Column3.DefaultCellStyle = dataGridViewCellStyle4;
		this.Column3.HeaderText = "Param A1";
		this.Column3.Name = "Column3";
		this.Column3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column3.Width = 50;
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Column4.DefaultCellStyle = dataGridViewCellStyle5;
		this.Column4.HeaderText = "Limit E2, V/m";
		this.Column4.Name = "Column4";
		this.Column4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column4.Width = 55;
		dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Column5.DefaultCellStyle = dataGridViewCellStyle6;
		this.Column5.HeaderText = "Param A2";
		this.Column5.Name = "Column5";
		this.Column5.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column5.Width = 50;
		dataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Column6.DefaultCellStyle = dataGridViewCellStyle7;
		this.Column6.HeaderText = "Limit E3, V/m";
		this.Column6.Name = "Column6";
		this.Column6.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column6.Width = 55;
		dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Column7.DefaultCellStyle = dataGridViewCellStyle8;
		this.Column7.HeaderText = "Param A3";
		this.Column7.Name = "Column7";
		this.Column7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column7.Width = 50;
		dataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Column8.DefaultCellStyle = dataGridViewCellStyle9;
		this.Column8.HeaderText = "Limit E4, V/m";
		this.Column8.Name = "Column8";
		this.Column8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column8.Width = 55;
		dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		this.Column9.DefaultCellStyle = dataGridViewCellStyle10;
		this.Column9.HeaderText = "Param A4";
		this.Column9.Name = "Column9";
		this.Column9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column9.Width = 50;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		base.Controls.Add(this.GroupBox1);
		base.Name = "UC_Limits";
		base.Size = new System.Drawing.Size(1220, 705);
		this.GroupBox1.ResumeLayout(false);
		this.GroupBox1.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pbColor4).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pbColor3).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pbColor2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pbColor1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.PictureBox1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.PB).EndInit();
		((System.ComponentModel.ISupportInitialize)this.DGV1).EndInit();
		base.ResumeLayout(false);
	}

	private void UC_Limits_Load(object sender, EventArgs e)
	{
		FillLimits();
		FlagProcessing = true;
		FlagProcessing = false;
	}

	public void FillLimits()
	{
		FlagProcessing = true;
		txtLimitDescription1.Text = Public2.PRJ.SETT.LimitDescription[0];
		txtLimitDescription2.Text = Public2.PRJ.SETT.LimitDescription[1];
		txtLimitDescription3.Text = Public2.PRJ.SETT.LimitDescription[2];
		txtLimitDescription4.Text = Public2.PRJ.SETT.LimitDescription[3];
		pbColor1.BackColor = Public2.PRJ.SETT.LimitColor[0];
		pbColor2.BackColor = Public2.PRJ.SETT.LimitColor[1];
		pbColor3.BackColor = Public2.PRJ.SETT.LimitColor[2];
		pbColor4.BackColor = Public2.PRJ.SETT.LimitColor[3];
		chkAlpha24L1.Checked = Public2.PRJ.SETT.LimitAlpha24[0];
		chkAlpha24L2.Checked = Public2.PRJ.SETT.LimitAlpha24[1];
		chkAlpha24L3.Checked = Public2.PRJ.SETT.LimitAlpha24[2];
		chkAlpha24L4.Checked = Public2.PRJ.SETT.LimitAlpha24[3];
		chkFtdcL1.Checked = Public2.PRJ.SETT.LimitFtdc[0];
		chkFtdcL2.Checked = Public2.PRJ.SETT.LimitFtdc[1];
		chkFtdcL3.Checked = Public2.PRJ.SETT.LimitFtdc[2];
		chkFtdcL4.Checked = Public2.PRJ.SETT.LimitFtdc[3];
		chkFprL1.Checked = Public2.PRJ.SETT.LimitFpr[0];
		chkFprL2.Checked = Public2.PRJ.SETT.LimitFpr[1];
		chkFprL3.Checked = Public2.PRJ.SETT.LimitFpr[2];
		chkFprL4.Checked = Public2.PRJ.SETT.LimitFpr[3];
		DGV1.Rows.Clear();
		if (Public2.PRJ.SETT.LMT.Count < 1)
		{
			return;
		}
		DGV1.Rows.Add(Public2.PRJ.SETT.LMT.Count);
		checked
		{
			int num = Public2.PRJ.SETT.LMT.Count - 1;
			for (int i = 0; i <= num; i++)
			{
				if (Public2.PRJ.SETT.LMT[i].Freq != -999f)
				{
					DGV1.Rows[i].Cells[0].Value = Module1.QSNG(Conversions.ToString(Public2.PRJ.SETT.LMT[i].Freq));
				}
				int upperBound = Public2.PRJ.SETT.LMT[i].E.GetUpperBound(0);
				for (int j = 0; j <= upperBound; j++)
				{
					if (Public2.PRJ.SETT.LMT[i].E[j] != -999f)
					{
						DGV1.Rows[i].Cells[j * 2 + 1].Value = Module1.QSNG(Conversions.ToString(Public2.PRJ.SETT.LMT[i].E[j]));
					}
					if (Public2.PRJ.SETT.LMT[i].A[j] != -999f)
					{
						DGV1.Rows[i].Cells[j * 2 + 2].Value = Module1.QSNG(Conversions.ToString(Public2.PRJ.SETT.LMT[i].A[j]));
					}
				}
			}
			FlagProcessing = false;
			PB.Refresh();
		}
	}

	private void PB_Paint(object sender, PaintEventArgs e)
	{
		e.Graphics.Clear(Color.White);
		short num = 20;
		short num2 = 17;
		short num3 = 25;
		short num4 = 23;
		Pen pen = new Pen(Color.FromArgb(200, 200, 200), 1f);
		Pen pen2 = new Pen(Color.FromArgb(200, 200, 200), 2f);
		checked
		{
			float num5 = (float)((double)(PB.Width - num4 - num3) / 4.0);
			float num6 = (float)((double)(PB.Height - num2 - num) / 3.0);
			int num7 = 1;
			do
			{
				float num8 = (float)((double)num3 + Math.Log10(num7) * (double)num5);
				e.Graphics.DrawLine(pen, num8, num, num8, PB.Height - num2);
				num8 += num5;
				e.Graphics.DrawLine(pen, num8, num, num8, PB.Height - num2);
				num8 += num5;
				e.Graphics.DrawLine(pen, num8, num, num8, PB.Height - num2);
				num8 += num5;
				e.Graphics.DrawLine(pen, num8, num, num8, PB.Height - num2);
				num7++;
			}
			while (num7 <= 10);
			int num9 = 1;
			do
			{
				float num10 = (float)((double)(PB.Height - num2) - Math.Log10(num9) * (double)num6);
				e.Graphics.DrawLine(pen, num3, num10, PB.Width - num4, num10);
				num10 -= num6;
				e.Graphics.DrawLine(pen, num3, num10, PB.Width - num4, num10);
				num10 -= num6;
				e.Graphics.DrawLine(pen, num3, num10, PB.Width - num4, num10);
				num9++;
			}
			while (num9 <= 10);
			if (Public2.PRJ.SETT.LMT.Count > 0)
			{
				int num11 = 0;
				do
				{
					bool flag = false;
					List<LimPoint> list = new List<LimPoint>();
					int num12 = Public2.PRJ.SETT.LMT.Count - 1;
					for (int i = 0; i <= num12; i++)
					{
						if ((Public2.PRJ.SETT.LMT[i].Freq == -999f) | (Public2.PRJ.SETT.LMT[i].E[num11] == -999f) | (Public2.PRJ.SETT.LMT[i].A[num11] == -999f))
						{
							flag = true;
						}
						if (!flag)
						{
							list.Add(new LimPoint(Public2.PRJ.SETT.LMT[i].Freq, Public2.PRJ.SETT.LMT[i].E[num11], Public2.PRJ.SETT.LMT[i].A[num11]));
						}
					}
					if (!flag)
					{
						if (list.First().F > 10f)
						{
							list.Insert(0, new LimPoint(10f, 0f, 0f));
						}
						int num13 = list.Count - 2;
						for (int j = 0; j <= num13; j++)
						{
							float x = (float)((double)num3 + (Math.Log10(list[j].F) - 1.0) * (double)num5);
							float y = (float)((double)(PB.Height - num2) - Math.Log10((double)list[j + 1].E * Math.Pow(list[j].F, list[j + 1].A)) * (double)num6);
							float x2 = (float)((double)num3 + (Math.Log10(list[j + 1].F) - 1.0) * (double)num5);
							float y2 = (float)((double)(PB.Height - num2) - Math.Log10((double)list[j + 1].E * Math.Pow(list[j + 1].F, list[j + 1].A)) * (double)num6);
							e.Graphics.DrawLine(new Pen(Public2.PRJ.SETT.LimitColor[num11], 3f), x, y, x2, y2);
						}
					}
					num11++;
				}
				while (num11 <= 3);
			}
			e.Graphics.FillRectangle(Brushes.White, 0, 0, num3, PB.Height);
			e.Graphics.FillRectangle(Brushes.White, PB.Width - num4, 0, num4, PB.Height);
			e.Graphics.DrawLine(pen2, num3, num, PB.Width - num4, num);
			e.Graphics.DrawLine(pen2, num3, PB.Height - num2, PB.Width - num4, PB.Height - num2);
			e.Graphics.DrawLine(pen2, num3, num, num3, PB.Height - num2);
			e.Graphics.DrawLine(pen2, PB.Width - num4, num, PB.Width - num4, PB.Height - num2);
			StringFormat stringFormat = new StringFormat();
			stringFormat.Alignment = StringAlignment.Center;
			stringFormat.LineAlignment = StringAlignment.Near;
			e.Graphics.DrawString("10 MHz", new Font("Arial", 7f, FontStyle.Regular), Brushes.Black, num3, PB.Height - num2 + 3, stringFormat);
			e.Graphics.DrawString("100 MHz", new Font("Arial", 7f, FontStyle.Regular), Brushes.Black, (float)num3 + num5, PB.Height - num2 + 3, stringFormat);
			e.Graphics.DrawString("1 GHz", new Font("Arial", 7f, FontStyle.Regular), Brushes.Black, (float)num3 + 2f * num5, PB.Height - num2 + 3, stringFormat);
			e.Graphics.DrawString("10 GHz", new Font("Arial", 7f, FontStyle.Regular), Brushes.Black, (float)num3 + 3f * num5, PB.Height - num2 + 3, stringFormat);
			e.Graphics.DrawString("100 GHz", new Font("Arial", 7f, FontStyle.Regular), Brushes.Black, PB.Width - num4, PB.Height - num2 + 3, stringFormat);
			stringFormat.Alignment = StringAlignment.Far;
			stringFormat.LineAlignment = StringAlignment.Center;
			e.Graphics.DrawString("1", new Font("Arial", 7f, FontStyle.Regular), Brushes.Black, num3 - 2, PB.Height - num2, stringFormat);
			e.Graphics.DrawString("10", new Font("Arial", 7f, FontStyle.Regular), Brushes.Black, num3 - 2, (float)(PB.Height - num2) - num6, stringFormat);
			e.Graphics.DrawString("100", new Font("Arial", 7f, FontStyle.Regular), Brushes.Black, num3 - 2, (float)(PB.Height - num2) - 2f * num6, stringFormat);
			e.Graphics.DrawString("1000", new Font("Arial", 7f, FontStyle.Regular), Brushes.Black, num3 - 2, (float)(PB.Height - num2) - 3f * num6, stringFormat);
			e.Graphics.DrawString("V/m", new Font("Arial", 7f, FontStyle.Regular), Brushes.Black, num3 + 10, 10f, stringFormat);
		}
	}

	private void DGV1_CellValidated(object sender, DataGridViewCellEventArgs e)
	{
		if (FlagProcessing || DGV1.Rows.Count == 1)
		{
			return;
		}
		checked
		{
			int num = DGV1.Rows.Count - 2;
			for (int i = 0; i <= num; i++)
			{
				float f = -999f;
				if (Module1.ValidText(Conversions.ToString(DGV1.Rows[i].Cells[0].Value), CheckLimits: true, 0f, 1000000f, IncludeMin: false, IncludeMax: true, Message: false))
				{
					f = Module1.QSNG(Conversions.ToString(DGV1.Rows[i].Cells[0].Value));
				}
				else
				{
					DGV1.Rows[i].Cells[0].Value = "";
				}
				float[] array = new float[4] { -999f, -999f, -999f, -999f };
				float[] array2 = new float[4] { -999f, -999f, -999f, -999f };
				int upperBound = Public2.PRJ.SETT.LMT[i].E.GetUpperBound(0);
				for (int j = 0; j <= upperBound; j++)
				{
					if (Module1.ValidText(Conversions.ToString(DGV1.Rows[i].Cells[j * 2 + 1].Value), CheckLimits: true, 0f, 100000f, IncludeMin: false, IncludeMax: true, Message: false))
					{
						array[j] = Module1.QSNG(Conversions.ToString(DGV1.Rows[i].Cells[j * 2 + 1].Value));
					}
					else
					{
						DGV1.Rows[i].Cells[j * 2 + 1].Value = "";
					}
					if (Module1.ValidText(Conversions.ToString(DGV1.Rows[i].Cells[j * 2 + 2].Value), CheckLimits: true, -3f, 3f, IncludeMin: true, IncludeMax: true, Message: false))
					{
						array2[j] = Module1.QSNG(Conversions.ToString(DGV1.Rows[i].Cells[j * 2 + 2].Value));
					}
					else
					{
						DGV1.Rows[i].Cells[j * 2 + 2].Value = "";
					}
				}
				Public2.PRJ.SETT.LMT[i] = new Public2.LimitPoint(f, array[0], array2[0], array[1], array2[1], array[2], array2[2], array[3], array2[3]);
			}
			PB.Refresh();
		}
	}

	private void DGV1_UserAddedRow(object sender, DataGridViewRowEventArgs e)
	{
		Public2.PRJ.SETT.LMT.Add(new Public2.LimitPoint(-999f, -999f, -999f, -999f, -999f, -999f, -999f, -999f, -999f));
		PB.Refresh();
	}

	private void DGV1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
	{
		Public2.PRJ.SETT.LMT.RemoveAt(e.Row.Index);
		PB.Refresh();
	}

	private void txtLimitDescription1_TextChanged(object sender, EventArgs e)
	{
		if (!FlagProcessing)
		{
			Public2.PRJ.SETT.LimitDescription[0] = txtLimitDescription1.Text;
		}
	}

	private void txtLimitDescription2_TextChanged(object sender, EventArgs e)
	{
		if (!FlagProcessing)
		{
			Public2.PRJ.SETT.LimitDescription[1] = txtLimitDescription2.Text;
		}
	}

	private void txtLimitDescription3_TextChanged(object sender, EventArgs e)
	{
		if (!FlagProcessing)
		{
			Public2.PRJ.SETT.LimitDescription[2] = txtLimitDescription3.Text;
		}
	}

	private void txtLimitDescription4_TextChanged(object sender, EventArgs e)
	{
		if (!FlagProcessing)
		{
			Public2.PRJ.SETT.LimitDescription[3] = txtLimitDescription4.Text;
		}
	}

	private void pbColor1_Click(object sender, EventArgs e)
	{
		CD1.Color = pbColor1.BackColor;
		CD1.FullOpen = true;
		if (CD1.ShowDialog() == DialogResult.OK)
		{
			pbColor1.BackColor = CD1.Color;
			Public2.PRJ.SETT.LimitColor[0] = pbColor1.BackColor;
		}
		PB.Refresh();
	}

	private void pbColor2_Click(object sender, EventArgs e)
	{
		CD1.Color = pbColor2.BackColor;
		CD1.FullOpen = true;
		if (CD1.ShowDialog() == DialogResult.OK)
		{
			pbColor2.BackColor = CD1.Color;
			Public2.PRJ.SETT.LimitColor[1] = pbColor2.BackColor;
		}
		PB.Refresh();
	}

	private void pbColor3_Click(object sender, EventArgs e)
	{
		CD1.Color = pbColor3.BackColor;
		CD1.FullOpen = true;
		if (CD1.ShowDialog() == DialogResult.OK)
		{
			pbColor3.BackColor = CD1.Color;
			Public2.PRJ.SETT.LimitColor[2] = pbColor3.BackColor;
		}
		PB.Refresh();
	}

	private void pbColor4_Click(object sender, EventArgs e)
	{
		CD1.Color = pbColor4.BackColor;
		CD1.FullOpen = true;
		if (CD1.ShowDialog() == DialogResult.OK)
		{
			pbColor4.BackColor = CD1.Color;
			Public2.PRJ.SETT.LimitColor[3] = pbColor4.BackColor;
		}
		PB.Refresh();
	}

	private void chkAlpha24L1_CheckedChanged(object sender, EventArgs e)
	{
		if (!FlagProcessing)
		{
			Public2.PRJ.SETT.LimitAlpha24[0] = chkAlpha24L1.Checked;
		}
	}

	private void chkAlpha24L2_CheckedChanged(object sender, EventArgs e)
	{
		if (!FlagProcessing)
		{
			Public2.PRJ.SETT.LimitAlpha24[1] = chkAlpha24L2.Checked;
		}
	}

	private void chkAlpha24L3_CheckedChanged(object sender, EventArgs e)
	{
		if (!FlagProcessing)
		{
			Public2.PRJ.SETT.LimitAlpha24[2] = chkAlpha24L3.Checked;
		}
	}

	private void chkAlpha24L4_CheckedChanged(object sender, EventArgs e)
	{
		if (!FlagProcessing)
		{
			Public2.PRJ.SETT.LimitAlpha24[3] = chkAlpha24L4.Checked;
		}
	}

	private void chkFtdcL1_CheckedChanged(object sender, EventArgs e)
	{
		if (!FlagProcessing)
		{
			Public2.PRJ.SETT.LimitFtdc[0] = chkFtdcL1.Checked;
		}
	}

	private void chkFtdcL2_CheckedChanged(object sender, EventArgs e)
	{
		if (!FlagProcessing)
		{
			Public2.PRJ.SETT.LimitFtdc[1] = chkFtdcL2.Checked;
		}
	}

	private void chkFtdcL3_CheckedChanged(object sender, EventArgs e)
	{
		if (!FlagProcessing)
		{
			Public2.PRJ.SETT.LimitFtdc[2] = chkFtdcL3.Checked;
		}
	}

	private void chkFtdcL4_CheckedChanged(object sender, EventArgs e)
	{
		if (!FlagProcessing)
		{
			Public2.PRJ.SETT.LimitFtdc[3] = chkFtdcL4.Checked;
		}
	}

	private void chkFprL1_CheckedChanged(object sender, EventArgs e)
	{
		if (!FlagProcessing)
		{
			Public2.PRJ.SETT.LimitFpr[0] = chkFprL1.Checked;
		}
	}

	private void chkFprL2_CheckedChanged(object sender, EventArgs e)
	{
		if (!FlagProcessing)
		{
			Public2.PRJ.SETT.LimitFpr[1] = chkFprL2.Checked;
		}
	}

	private void chkFprL3_CheckedChanged(object sender, EventArgs e)
	{
		if (!FlagProcessing)
		{
			Public2.PRJ.SETT.LimitFpr[2] = chkFprL3.Checked;
		}
	}

	private void chkFprL4_CheckedChanged(object sender, EventArgs e)
	{
		if (!FlagProcessing)
		{
			Public2.PRJ.SETT.LimitFpr[3] = chkFprL4.Checked;
		}
	}

	private void PB_Resize(object sender, EventArgs e)
	{
		PB.Refresh();
	}

	private void cmdICNIRP_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Interaction.MsgBox(Localization.LocMSG(91), MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
		{
			Public2.SetLimitsPRJ("ICNIRP");
			FillLimits();
			PB.Refresh();
		}
	}

	private void cmdEU_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Interaction.MsgBox(Localization.LocMSG(91), MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
		{
			Public2.SetLimitsPRJ("EU");
			FillLimits();
			PB.Refresh();
		}
	}

	private void cmdBelgium_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Interaction.MsgBox(Localization.LocMSG(91), MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
		{
			Public2.SetLimitsPRJ("Belgium");
			FillLimits();
			PB.Refresh();
		}
	}

	private void cmdBulgaria_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Interaction.MsgBox(Localization.LocMSG(91), MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
		{
			Public2.SetLimitsPRJ("Bulgaria");
			FillLimits();
			PB.Refresh();
		}
	}

	private void cmdChina_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Interaction.MsgBox(Localization.LocMSG(91), MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
		{
			Public2.SetLimitsPRJ("China");
			FillLimits();
			PB.Refresh();
		}
	}

	private void cmdCroatia_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Interaction.MsgBox(Localization.LocMSG(91), MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
		{
			Public2.SetLimitsPRJ("Croatia");
			FillLimits();
			PB.Refresh();
		}
	}

	private void cmdGreece_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Interaction.MsgBox(Localization.LocMSG(91), MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
		{
			Public2.SetLimitsPRJ("Greece");
			FillLimits();
			PB.Refresh();
		}
	}

	private void cmdIndia_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Interaction.MsgBox(Localization.LocMSG(91), MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
		{
			Public2.SetLimitsPRJ("India");
			FillLimits();
			PB.Refresh();
		}
	}

	private void cmdItaly_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Interaction.MsgBox(Localization.LocMSG(91), MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
		{
			Public2.SetLimitsPRJ("Italy");
			FillLimits();
			PB.Refresh();
		}
	}

	private void cmdLithuania_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Interaction.MsgBox(Localization.LocMSG(91), MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
		{
			Public2.SetLimitsPRJ("Lithuania");
			FillLimits();
			PB.Refresh();
		}
	}

	private void cmdPoland_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Interaction.MsgBox(Localization.LocMSG(91), MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
		{
			Public2.SetLimitsPRJ("Poland");
			FillLimits();
			PB.Refresh();
		}
	}

	private void cmdRussia_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Interaction.MsgBox(Localization.LocMSG(91), MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
		{
			Public2.SetLimitsPRJ("Russia");
			FillLimits();
			PB.Refresh();
		}
	}

	private void cmdSlovenia_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Interaction.MsgBox(Localization.LocMSG(91), MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
		{
			Public2.SetLimitsPRJ("Slovenia");
			FillLimits();
			PB.Refresh();
		}
	}

	private void cmdSwitzerland_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Interaction.MsgBox(Localization.LocMSG(91), MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
		{
			Public2.SetLimitsPRJ("Switzerland");
			FillLimits();
			PB.Refresh();
		}
	}

	private void cmdUK_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Interaction.MsgBox(Localization.LocMSG(91), MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
		{
			Public2.SetLimitsPRJ("UK");
			FillLimits();
			PB.Refresh();
		}
	}

	private void cmdUSA_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Interaction.MsgBox(Localization.LocMSG(91), MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
		{
			Public2.SetLimitsPRJ("USA");
			FillLimits();
			PB.Refresh();
		}
	}
}
