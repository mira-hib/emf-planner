using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using EMFPlanner.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[DesignerGenerated]
public class UC_Setup : UserControl
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

	[field: AccessedThroughProperty("GroupBox6")]
	internal virtual GroupBox GroupBox6
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("gbCoordinatesFormat")]
	internal virtual GroupBox gbCoordinatesFormat
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("rbCoordinatesFormat0")]
	internal virtual RadioButton rbCoordinatesFormat0
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = rbCoordinatesFormat0_CheckedChanged;
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

	[field: AccessedThroughProperty("rbCoordinatesFormat2")]
	internal virtual RadioButton rbCoordinatesFormat2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = rbCoordinatesFormat0_CheckedChanged;
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

	[field: AccessedThroughProperty("rbCoordinatesFormat1")]
	internal virtual RadioButton rbCoordinatesFormat1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = rbCoordinatesFormat0_CheckedChanged;
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

	[field: AccessedThroughProperty("GroupBox9")]
	internal virtual GroupBox GroupBox9
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

	[field: AccessedThroughProperty("txtProxyPassword")]
	internal virtual TextBox txtProxyPassword
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			CancelEventHandler value2 = txtProxyPassword_Validating;
			KeyEventHandler value3 = txtProxyPassword_KeyUp;
			TextBox textBox = field;
			if (textBox != null)
			{
				textBox.Validating -= value2;
				textBox.KeyUp -= value3;
			}
			field = value;
			textBox = field;
			if (textBox != null)
			{
				textBox.Validating += value2;
				textBox.KeyUp += value3;
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

	[field: AccessedThroughProperty("txtProxyLogin")]
	internal virtual TextBox txtProxyLogin
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			CancelEventHandler value2 = txtProxyLogin_Validating;
			KeyEventHandler value3 = txtProxyLogin_KeyUp;
			TextBox textBox = field;
			if (textBox != null)
			{
				textBox.Validating -= value2;
				textBox.KeyUp -= value3;
			}
			field = value;
			textBox = field;
			if (textBox != null)
			{
				textBox.Validating += value2;
				textBox.KeyUp += value3;
			}
		}
	}

	[field: AccessedThroughProperty("chkProxyLogin")]
	internal virtual CheckBox chkProxyLogin
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = chkProxyLogin_CheckedChanged;
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

	[field: AccessedThroughProperty("Label3")]
	internal virtual Label Label3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtProxyPORT")]
	internal virtual TextBox txtProxyPORT
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			CancelEventHandler value2 = txtProxyPORT_Validating;
			KeyEventHandler value3 = txtProxyPORT_KeyUp;
			TextBox textBox = field;
			if (textBox != null)
			{
				textBox.Validating -= value2;
				textBox.KeyUp -= value3;
			}
			field = value;
			textBox = field;
			if (textBox != null)
			{
				textBox.Validating += value2;
				textBox.KeyUp += value3;
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

	[field: AccessedThroughProperty("txtProxyIP")]
	internal virtual TextBox txtProxyIP
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			CancelEventHandler value2 = txtProxyIP_Validating;
			KeyEventHandler value3 = txtProxyIP_KeyUp;
			TextBox textBox = field;
			if (textBox != null)
			{
				textBox.Validating -= value2;
				textBox.KeyUp -= value3;
			}
			field = value;
			textBox = field;
			if (textBox != null)
			{
				textBox.Validating += value2;
				textBox.KeyUp += value3;
			}
		}
	}

	[field: AccessedThroughProperty("chkProxy")]
	internal virtual CheckBox chkProxy
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = chkProxy_CheckedChanged;
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

	[field: AccessedThroughProperty("GroupBox8")]
	internal virtual GroupBox GroupBox8
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdChangeFolderCache")]
	internal virtual Button cmdChangeFolderCache
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdChangeFolderCache_Click;
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

	[field: AccessedThroughProperty("txtFolderCahe")]
	internal virtual TextBox txtFolderCahe
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

	[field: AccessedThroughProperty("txtSize1")]
	internal virtual NumericUpDown txtSize1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = txtSize1_ValueChanged;
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

	[field: AccessedThroughProperty("txtSize0")]
	internal virtual NumericUpDown txtSize0
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = txtSize0_ValueChanged;
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

	[field: AccessedThroughProperty("GroupBox5")]
	internal virtual GroupBox GroupBox5
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdChangeAntennasFolder")]
	internal virtual Button cmdChangeAntennasFolder
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = ccmdChangeAntennasFolder_Click;
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

	[field: AccessedThroughProperty("txtAntennaFolder")]
	internal virtual TextBox txtAntennaFolder
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("FBD1")]
	internal virtual FolderBrowserDialog FBD1
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

	[field: AccessedThroughProperty("Label8")]
	internal virtual Label Label8
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
			DataGridViewCellEventHandler value2 = DGV1_CellClick;
			DataGridViewCellEventHandler value3 = DGV1_CellValueChanged;
			DataGridViewRowEventHandler value4 = DGV1_UserDeletedRow;
			DataGridView dataGridView = field;
			if (dataGridView != null)
			{
				dataGridView.CellClick -= value2;
				dataGridView.CellValueChanged -= value3;
				dataGridView.UserDeletedRow -= value4;
			}
			field = value;
			dataGridView = field;
			if (dataGridView != null)
			{
				dataGridView.CellClick += value2;
				dataGridView.CellValueChanged += value3;
				dataGridView.UserDeletedRow += value4;
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

	[field: AccessedThroughProperty("Label1")]
	internal virtual Label Label1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtK")]
	internal virtual TextBox txtK
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			CancelEventHandler value2 = txtK_Validating;
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

	[field: AccessedThroughProperty("GroupBox10")]
	internal virtual GroupBox GroupBox10
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdApplyBaseMapsSettings")]
	internal virtual Button cmdApplyBaseMapsSettings
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdApplyBaseMapsSettings_Click;
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

	[field: AccessedThroughProperty("cmdResetToDefault")]
	internal virtual Button cmdResetToDefault
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdResetToDefault_Click;
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

	[field: AccessedThroughProperty("DGV")]
	internal virtual DataGridView DGV
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

	[field: AccessedThroughProperty("lblSRV")]
	internal virtual Label lblSRV
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public UC_Setup()
	{
		Load += UC_Setup_Load;
		Validating += UC_ProjectSettings_Validating;
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
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
		System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EMFPlanner.UC_Setup));
		this.GroupBox1 = new System.Windows.Forms.GroupBox();
		this.GroupBox6 = new System.Windows.Forms.GroupBox();
		this.GroupBox10 = new System.Windows.Forms.GroupBox();
		this.lblSRV = new System.Windows.Forms.Label();
		this.cmdApplyBaseMapsSettings = new System.Windows.Forms.Button();
		this.cmdResetToDefault = new System.Windows.Forms.Button();
		this.DGV = new System.Windows.Forms.DataGridView();
		this.DataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.DataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.GroupBox3 = new System.Windows.Forms.GroupBox();
		this.DGV1 = new System.Windows.Forms.DataGridView();
		this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
		this.GroupBox5 = new System.Windows.Forms.GroupBox();
		this.cmdChangeAntennasFolder = new System.Windows.Forms.Button();
		this.txtAntennaFolder = new System.Windows.Forms.TextBox();
		this.gbCoordinatesFormat = new System.Windows.Forms.GroupBox();
		this.rbCoordinatesFormat0 = new System.Windows.Forms.RadioButton();
		this.rbCoordinatesFormat2 = new System.Windows.Forms.RadioButton();
		this.rbCoordinatesFormat1 = new System.Windows.Forms.RadioButton();
		this.GroupBox9 = new System.Windows.Forms.GroupBox();
		this.Label5 = new System.Windows.Forms.Label();
		this.txtProxyPassword = new System.Windows.Forms.TextBox();
		this.Label4 = new System.Windows.Forms.Label();
		this.txtProxyLogin = new System.Windows.Forms.TextBox();
		this.chkProxyLogin = new System.Windows.Forms.CheckBox();
		this.Label3 = new System.Windows.Forms.Label();
		this.txtProxyPORT = new System.Windows.Forms.TextBox();
		this.Label2 = new System.Windows.Forms.Label();
		this.txtProxyIP = new System.Windows.Forms.TextBox();
		this.chkProxy = new System.Windows.Forms.CheckBox();
		this.GroupBox8 = new System.Windows.Forms.GroupBox();
		this.cmdChangeFolderCache = new System.Windows.Forms.Button();
		this.txtFolderCahe = new System.Windows.Forms.TextBox();
		this.GroupBox4 = new System.Windows.Forms.GroupBox();
		this.Label1 = new System.Windows.Forms.Label();
		this.txtK = new System.Windows.Forms.TextBox();
		this.GroupBox2 = new System.Windows.Forms.GroupBox();
		this.Label9 = new System.Windows.Forms.Label();
		this.Label8 = new System.Windows.Forms.Label();
		this.txtSize0 = new System.Windows.Forms.NumericUpDown();
		this.txtSize1 = new System.Windows.Forms.NumericUpDown();
		this.FBD1 = new System.Windows.Forms.FolderBrowserDialog();
		this.CD1 = new System.Windows.Forms.ColorDialog();
		this.GroupBox1.SuspendLayout();
		this.GroupBox6.SuspendLayout();
		this.GroupBox10.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.DGV).BeginInit();
		this.GroupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.DGV1).BeginInit();
		this.GroupBox5.SuspendLayout();
		this.gbCoordinatesFormat.SuspendLayout();
		this.GroupBox9.SuspendLayout();
		this.GroupBox8.SuspendLayout();
		this.GroupBox4.SuspendLayout();
		this.GroupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.txtSize0).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.txtSize1).BeginInit();
		base.SuspendLayout();
		this.GroupBox1.Controls.Add(this.GroupBox6);
		this.GroupBox1.Controls.Add(this.GroupBox4);
		this.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GroupBox1.Location = new System.Drawing.Point(0, 0);
		this.GroupBox1.Name = "GroupBox1";
		this.GroupBox1.Size = new System.Drawing.Size(800, 836);
		this.GroupBox1.TabIndex = 0;
		this.GroupBox1.TabStop = false;
		this.GroupBox1.Text = "Settings";
		this.GroupBox6.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GroupBox6.Controls.Add(this.GroupBox10);
		this.GroupBox6.Controls.Add(this.GroupBox3);
		this.GroupBox6.Controls.Add(this.GroupBox5);
		this.GroupBox6.Controls.Add(this.gbCoordinatesFormat);
		this.GroupBox6.Controls.Add(this.GroupBox9);
		this.GroupBox6.Controls.Add(this.GroupBox8);
		this.GroupBox6.Location = new System.Drawing.Point(6, 124);
		this.GroupBox6.Name = "GroupBox6";
		this.GroupBox6.Size = new System.Drawing.Size(788, 706);
		this.GroupBox6.TabIndex = 47;
		this.GroupBox6.TabStop = false;
		this.GroupBox6.Text = "Application settings";
		this.GroupBox10.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GroupBox10.Controls.Add(this.lblSRV);
		this.GroupBox10.Controls.Add(this.cmdApplyBaseMapsSettings);
		this.GroupBox10.Controls.Add(this.cmdResetToDefault);
		this.GroupBox10.Controls.Add(this.DGV);
		this.GroupBox10.Location = new System.Drawing.Point(5, 366);
		this.GroupBox10.Name = "GroupBox10";
		this.GroupBox10.Size = new System.Drawing.Size(777, 334);
		this.GroupBox10.TabIndex = 48;
		this.GroupBox10.TabStop = false;
		this.GroupBox10.Text = "Base map settings";
		this.lblSRV.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.lblSRV.AutoSize = true;
		this.lblSRV.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.lblSRV.Location = new System.Drawing.Point(752, 316);
		this.lblSRV.Name = "lblSRV";
		this.lblSRV.Size = new System.Drawing.Size(21, 13);
		this.lblSRV.TabIndex = 50;
		this.lblSRV.Text = "srv";
		this.cmdApplyBaseMapsSettings.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.cmdApplyBaseMapsSettings.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdApplyBaseMapsSettings.Location = new System.Drawing.Point(228, 304);
		this.cmdApplyBaseMapsSettings.Name = "cmdApplyBaseMapsSettings";
		this.cmdApplyBaseMapsSettings.Size = new System.Drawing.Size(227, 24);
		this.cmdApplyBaseMapsSettings.TabIndex = 48;
		this.cmdApplyBaseMapsSettings.Text = "Apply base maps settings";
		this.cmdApplyBaseMapsSettings.UseVisualStyleBackColor = true;
		this.cmdResetToDefault.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.cmdResetToDefault.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdResetToDefault.Location = new System.Drawing.Point(6, 304);
		this.cmdResetToDefault.Name = "cmdResetToDefault";
		this.cmdResetToDefault.Size = new System.Drawing.Size(201, 24);
		this.cmdResetToDefault.TabIndex = 47;
		this.cmdResetToDefault.Text = "Reset to default   ";
		this.cmdResetToDefault.UseVisualStyleBackColor = true;
		this.DGV.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		dataGridViewCellStyle.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
		dataGridViewCellStyle.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.DGV.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle;
		this.DGV.ColumnHeadersHeight = 25;
		this.DGV.Columns.AddRange(this.DataGridViewTextBoxColumn1, this.DataGridViewTextBoxColumn2);
		this.DGV.Location = new System.Drawing.Point(6, 19);
		this.DGV.Name = "DGV";
		this.DGV.RowHeadersWidth = 30;
		this.DGV.RowTemplate.Height = 18;
		this.DGV.Size = new System.Drawing.Size(766, 279);
		this.DGV.TabIndex = 46;
		dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		this.DataGridViewTextBoxColumn1.DefaultCellStyle = dataGridViewCellStyle2;
		this.DataGridViewTextBoxColumn1.HeaderText = "Map Name";
		this.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1";
		this.DataGridViewTextBoxColumn1.Width = 150;
		this.DataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		this.DataGridViewTextBoxColumn2.DefaultCellStyle = dataGridViewCellStyle3;
		this.DataGridViewTextBoxColumn2.HeaderText = "URL";
		this.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2";
		this.GroupBox3.Controls.Add(this.DGV1);
		this.GroupBox3.Location = new System.Drawing.Point(429, 80);
		this.GroupBox3.Name = "GroupBox3";
		this.GroupBox3.Size = new System.Drawing.Size(353, 280);
		this.GroupBox3.TabIndex = 15;
		this.GroupBox3.TabStop = false;
		this.GroupBox3.Text = "Owner list";
		this.DGV1.AllowUserToResizeColumns = false;
		this.DGV1.AllowUserToResizeRows = false;
		this.DGV1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.DGV1.BackgroundColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
		dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
		dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
		dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
		dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
		dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
		dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
		this.DGV1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
		this.DGV1.ColumnHeadersHeight = 25;
		this.DGV1.Columns.AddRange(this.Column1, this.Column2);
		this.DGV1.Location = new System.Drawing.Point(8, 19);
		this.DGV1.Name = "DGV1";
		this.DGV1.RowHeadersWidth = 35;
		this.DGV1.RowTemplate.Height = 18;
		this.DGV1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
		this.DGV1.Size = new System.Drawing.Size(339, 255);
		this.DGV1.TabIndex = 7;
		this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
		dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
		this.Column1.DefaultCellStyle = dataGridViewCellStyle5;
		this.Column1.HeaderText = "Owner name";
		this.Column1.Name = "Column1";
		this.Column1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column2.HeaderText = "Color";
		this.Column2.Name = "Column2";
		this.Column2.ReadOnly = true;
		this.Column2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
		this.Column2.Width = 50;
		this.GroupBox5.Controls.Add(this.cmdChangeAntennasFolder);
		this.GroupBox5.Controls.Add(this.txtAntennaFolder);
		this.GroupBox5.Location = new System.Drawing.Point(6, 80);
		this.GroupBox5.Name = "GroupBox5";
		this.GroupBox5.Size = new System.Drawing.Size(418, 55);
		this.GroupBox5.TabIndex = 14;
		this.GroupBox5.TabStop = false;
		this.GroupBox5.Text = "Path to the folder with antenna pattern files (to display the list of antenna patterns)";
		this.cmdChangeAntennasFolder.Image = (System.Drawing.Image)resources.GetObject("cmdChangeAntennasFolder.Image");
		this.cmdChangeAntennasFolder.Location = new System.Drawing.Point(385, 20);
		this.cmdChangeAntennasFolder.Name = "cmdChangeAntennasFolder";
		this.cmdChangeAntennasFolder.Size = new System.Drawing.Size(25, 20);
		this.cmdChangeAntennasFolder.TabIndex = 3;
		this.cmdChangeAntennasFolder.UseVisualStyleBackColor = true;
		this.txtAntennaFolder.Location = new System.Drawing.Point(9, 20);
		this.txtAntennaFolder.Name = "txtAntennaFolder";
		this.txtAntennaFolder.ReadOnly = true;
		this.txtAntennaFolder.Size = new System.Drawing.Size(368, 20);
		this.txtAntennaFolder.TabIndex = 0;
		this.gbCoordinatesFormat.Controls.Add(this.rbCoordinatesFormat0);
		this.gbCoordinatesFormat.Controls.Add(this.rbCoordinatesFormat2);
		this.gbCoordinatesFormat.Controls.Add(this.rbCoordinatesFormat1);
		this.gbCoordinatesFormat.Location = new System.Drawing.Point(5, 260);
		this.gbCoordinatesFormat.Name = "gbCoordinatesFormat";
		this.gbCoordinatesFormat.Size = new System.Drawing.Size(418, 100);
		this.gbCoordinatesFormat.TabIndex = 12;
		this.gbCoordinatesFormat.TabStop = false;
		this.gbCoordinatesFormat.Text = "Coordinate Format";
		this.rbCoordinatesFormat0.AutoSize = true;
		this.rbCoordinatesFormat0.Checked = true;
		this.rbCoordinatesFormat0.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.rbCoordinatesFormat0.Location = new System.Drawing.Point(19, 21);
		this.rbCoordinatesFormat0.Name = "rbCoordinatesFormat0";
		this.rbCoordinatesFormat0.Size = new System.Drawing.Size(101, 17);
		this.rbCoordinatesFormat0.TabIndex = 0;
		this.rbCoordinatesFormat0.TabStop = true;
		this.rbCoordinatesFormat0.Text = "Decimal Degree";
		this.rbCoordinatesFormat0.UseVisualStyleBackColor = true;
		this.rbCoordinatesFormat2.AutoSize = true;
		this.rbCoordinatesFormat2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.rbCoordinatesFormat2.Location = new System.Drawing.Point(19, 73);
		this.rbCoordinatesFormat2.Name = "rbCoordinatesFormat2";
		this.rbCoordinatesFormat2.Size = new System.Drawing.Size(149, 17);
		this.rbCoordinatesFormat2.TabIndex = 1;
		this.rbCoordinatesFormat2.Text = "Degrees, Decimal Minutes";
		this.rbCoordinatesFormat2.UseVisualStyleBackColor = true;
		this.rbCoordinatesFormat1.AutoSize = true;
		this.rbCoordinatesFormat1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.rbCoordinatesFormat1.Location = new System.Drawing.Point(19, 47);
		this.rbCoordinatesFormat1.Name = "rbCoordinatesFormat1";
		this.rbCoordinatesFormat1.Size = new System.Drawing.Size(156, 17);
		this.rbCoordinatesFormat1.TabIndex = 0;
		this.rbCoordinatesFormat1.Text = "Degrees, Minutes, Seconds";
		this.rbCoordinatesFormat1.UseVisualStyleBackColor = true;
		this.GroupBox9.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GroupBox9.Controls.Add(this.Label5);
		this.GroupBox9.Controls.Add(this.txtProxyPassword);
		this.GroupBox9.Controls.Add(this.Label4);
		this.GroupBox9.Controls.Add(this.txtProxyLogin);
		this.GroupBox9.Controls.Add(this.chkProxyLogin);
		this.GroupBox9.Controls.Add(this.Label3);
		this.GroupBox9.Controls.Add(this.txtProxyPORT);
		this.GroupBox9.Controls.Add(this.Label2);
		this.GroupBox9.Controls.Add(this.txtProxyIP);
		this.GroupBox9.Controls.Add(this.chkProxy);
		this.GroupBox9.Location = new System.Drawing.Point(5, 141);
		this.GroupBox9.Name = "GroupBox9";
		this.GroupBox9.Size = new System.Drawing.Size(419, 113);
		this.GroupBox9.TabIndex = 13;
		this.GroupBox9.TabStop = false;
		this.GroupBox9.Text = "Proxy Settings";
		this.Label5.AutoSize = true;
		this.Label5.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.Label5.Location = new System.Drawing.Point(187, 79);
		this.Label5.Name = "Label5";
		this.Label5.Size = new System.Drawing.Size(53, 13);
		this.Label5.TabIndex = 11;
		this.Label5.Text = "Password";
		this.txtProxyPassword.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtProxyPassword.Location = new System.Drawing.Point(247, 76);
		this.txtProxyPassword.Name = "txtProxyPassword";
		this.txtProxyPassword.Size = new System.Drawing.Size(164, 20);
		this.txtProxyPassword.TabIndex = 10;
		this.Label4.AutoSize = true;
		this.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.Label4.Location = new System.Drawing.Point(187, 53);
		this.Label4.Name = "Label4";
		this.Label4.Size = new System.Drawing.Size(55, 13);
		this.Label4.TabIndex = 9;
		this.Label4.Text = "Username";
		this.txtProxyLogin.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtProxyLogin.Location = new System.Drawing.Point(247, 50);
		this.txtProxyLogin.Name = "txtProxyLogin";
		this.txtProxyLogin.Size = new System.Drawing.Size(164, 20);
		this.txtProxyLogin.TabIndex = 8;
		this.chkProxyLogin.AutoSize = true;
		this.chkProxyLogin.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.chkProxyLogin.Location = new System.Drawing.Point(172, 23);
		this.chkProxyLogin.Name = "chkProxyLogin";
		this.chkProxyLogin.Size = new System.Drawing.Size(215, 17);
		this.chkProxyLogin.TabIndex = 7;
		this.chkProxyLogin.Text = "The proxy server requires authentication";
		this.chkProxyLogin.UseVisualStyleBackColor = true;
		this.Label3.AutoSize = true;
		this.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.Label3.Location = new System.Drawing.Point(19, 79);
		this.Label3.Name = "Label3";
		this.Label3.Size = new System.Drawing.Size(26, 13);
		this.Label3.TabIndex = 6;
		this.Label3.Text = "Port";
		this.txtProxyPORT.Location = new System.Drawing.Point(51, 76);
		this.txtProxyPORT.Name = "txtProxyPORT";
		this.txtProxyPORT.Size = new System.Drawing.Size(45, 20);
		this.txtProxyPORT.TabIndex = 5;
		this.txtProxyPORT.Text = "3128";
		this.txtProxyPORT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.Label2.AutoSize = true;
		this.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.Label2.Location = new System.Drawing.Point(28, 53);
		this.Label2.Name = "Label2";
		this.Label2.Size = new System.Drawing.Size(17, 13);
		this.Label2.TabIndex = 4;
		this.Label2.Text = "IP";
		this.txtProxyIP.Location = new System.Drawing.Point(51, 50);
		this.txtProxyIP.Name = "txtProxyIP";
		this.txtProxyIP.Size = new System.Drawing.Size(102, 20);
		this.txtProxyIP.TabIndex = 3;
		this.txtProxyIP.Text = "80.255.145.41";
		this.txtProxyIP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.chkProxy.AutoSize = true;
		this.chkProxy.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.chkProxy.Location = new System.Drawing.Point(16, 23);
		this.chkProxy.Name = "chkProxy";
		this.chkProxy.Size = new System.Drawing.Size(114, 17);
		this.chkProxy.TabIndex = 2;
		this.chkProxy.Text = "Use proxy server   ";
		this.chkProxy.UseVisualStyleBackColor = true;
		this.GroupBox8.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GroupBox8.Controls.Add(this.cmdChangeFolderCache);
		this.GroupBox8.Controls.Add(this.txtFolderCahe);
		this.GroupBox8.Location = new System.Drawing.Point(5, 19);
		this.GroupBox8.Name = "GroupBox8";
		this.GroupBox8.Size = new System.Drawing.Size(777, 55);
		this.GroupBox8.TabIndex = 11;
		this.GroupBox8.TabStop = false;
		this.GroupBox8.Text = "Path to the map cache folder";
		this.cmdChangeFolderCache.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdChangeFolderCache.Image = (System.Drawing.Image)resources.GetObject("cmdChangeFolderCache.Image");
		this.cmdChangeFolderCache.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdChangeFolderCache.Location = new System.Drawing.Point(744, 20);
		this.cmdChangeFolderCache.Name = "cmdChangeFolderCache";
		this.cmdChangeFolderCache.Size = new System.Drawing.Size(25, 20);
		this.cmdChangeFolderCache.TabIndex = 3;
		this.cmdChangeFolderCache.UseVisualStyleBackColor = true;
		this.txtFolderCahe.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.txtFolderCahe.Location = new System.Drawing.Point(9, 20);
		this.txtFolderCahe.Name = "txtFolderCahe";
		this.txtFolderCahe.ReadOnly = true;
		this.txtFolderCahe.Size = new System.Drawing.Size(727, 20);
		this.txtFolderCahe.TabIndex = 0;
		this.GroupBox4.Controls.Add(this.Label1);
		this.GroupBox4.Controls.Add(this.txtK);
		this.GroupBox4.Controls.Add(this.GroupBox2);
		this.GroupBox4.Location = new System.Drawing.Point(6, 19);
		this.GroupBox4.Name = "GroupBox4";
		this.GroupBox4.Size = new System.Drawing.Size(788, 99);
		this.GroupBox4.TabIndex = 46;
		this.GroupBox4.TabStop = false;
		this.GroupBox4.Text = "Project Settings";
		this.Label1.AutoSize = true;
		this.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.Label1.Location = new System.Drawing.Point(64, 44);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(107, 13);
		this.Label1.TabIndex = 175;
		this.Label1.Text = "Reflection coefficient";
		this.txtK.Location = new System.Drawing.Point(177, 41);
		this.txtK.Name = "txtK";
		this.txtK.Size = new System.Drawing.Size(45, 20);
		this.txtK.TabIndex = 174;
		this.txtK.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GroupBox2.Controls.Add(this.Label9);
		this.GroupBox2.Controls.Add(this.Label8);
		this.GroupBox2.Controls.Add(this.txtSize0);
		this.GroupBox2.Controls.Add(this.txtSize1);
		this.GroupBox2.Location = new System.Drawing.Point(282, 19);
		this.GroupBox2.Name = "GroupBox2";
		this.GroupBox2.Size = new System.Drawing.Size(500, 68);
		this.GroupBox2.TabIndex = 173;
		this.GroupBox2.TabStop = false;
		this.GroupBox2.Text = "Maximum matrix size for cut calculation";
		this.Label9.AutoSize = true;
		this.Label9.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.Label9.Location = new System.Drawing.Point(190, 34);
		this.Label9.Name = "Label9";
		this.Label9.Size = new System.Drawing.Size(35, 13);
		this.Label9.TabIndex = 172;
		this.Label9.Text = "Grid 2";
		this.Label8.AutoSize = true;
		this.Label8.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.Label8.Location = new System.Drawing.Point(45, 34);
		this.Label8.Name = "Label8";
		this.Label8.Size = new System.Drawing.Size(35, 13);
		this.Label8.TabIndex = 171;
		this.Label8.Text = "Grid 1";
		this.txtSize0.Location = new System.Drawing.Point(92, 32);
		this.txtSize0.Maximum = new decimal(new int[4] { 1000, 0, 0, 0 });
		this.txtSize0.Minimum = new decimal(new int[4] { 50, 0, 0, 0 });
		this.txtSize0.Name = "txtSize0";
		this.txtSize0.Size = new System.Drawing.Size(62, 20);
		this.txtSize0.TabIndex = 168;
		this.txtSize0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtSize0.Value = new decimal(new int[4] { 800, 0, 0, 0 });
		this.txtSize1.Location = new System.Drawing.Point(237, 32);
		this.txtSize1.Maximum = new decimal(new int[4] { 1000, 0, 0, 0 });
		this.txtSize1.Minimum = new decimal(new int[4] { 50, 0, 0, 0 });
		this.txtSize1.Name = "txtSize1";
		this.txtSize1.Size = new System.Drawing.Size(62, 20);
		this.txtSize1.TabIndex = 169;
		this.txtSize1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtSize1.Value = new decimal(new int[4] { 400, 0, 0, 0 });
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		base.Controls.Add(this.GroupBox1);
		this.MinimumSize = new System.Drawing.Size(800, 0);
		base.Name = "UC_Setup";
		base.Size = new System.Drawing.Size(800, 836);
		this.GroupBox1.ResumeLayout(false);
		this.GroupBox6.ResumeLayout(false);
		this.GroupBox10.ResumeLayout(false);
		this.GroupBox10.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.DGV).EndInit();
		this.GroupBox3.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.DGV1).EndInit();
		this.GroupBox5.ResumeLayout(false);
		this.GroupBox5.PerformLayout();
		this.gbCoordinatesFormat.ResumeLayout(false);
		this.gbCoordinatesFormat.PerformLayout();
		this.GroupBox9.ResumeLayout(false);
		this.GroupBox9.PerformLayout();
		this.GroupBox8.ResumeLayout(false);
		this.GroupBox8.PerformLayout();
		this.GroupBox4.ResumeLayout(false);
		this.GroupBox4.PerformLayout();
		this.GroupBox2.ResumeLayout(false);
		this.GroupBox2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.txtSize0).EndInit();
		((System.ComponentModel.ISupportInitialize)this.txtSize1).EndInit();
		base.ResumeLayout(false);
	}

	private void UC_Setup_Load(object sender, EventArgs e)
	{
		FlagOK = false;
		txtSize0.Value = new decimal(Public2.PRJ.SETT.MaxHorSize[0]);
		txtSize1.Value = new decimal(Public2.PRJ.SETT.MaxHorSize[1]);
		txtK.Text = Conversions.ToString(Public2.PRJ.SETT.KoefOslabl);
		txtFolderCahe.Text = Public2.AppSetup.FolderCach;
		txtAntennaFolder.Text = Public2.AppSetup.AntennaFolder;
		chkProxy.Checked = Public2.AppSetup.FlagProxy;
		chkProxyLogin.Checked = Public2.AppSetup.FlagProxyLogin;
		txtProxyIP.Text = Public2.AppSetup.ProxyIP;
		txtProxyPORT.Text = Conversions.ToString(Public2.AppSetup.ProxyPort);
		txtProxyLogin.Text = Public2.AppSetup.ProxyLogin;
		txtProxyPassword.Text = Public2.AppSetup.ProxyPassword;
		if (Public2.AppSetup.CoordinatesFormat == 0)
		{
			rbCoordinatesFormat0.Checked = true;
			rbCoordinatesFormat1.Checked = false;
			rbCoordinatesFormat2.Checked = false;
		}
		else if (Public2.AppSetup.CoordinatesFormat == 1)
		{
			rbCoordinatesFormat0.Checked = false;
			rbCoordinatesFormat1.Checked = true;
			rbCoordinatesFormat2.Checked = false;
		}
		else if (Public2.AppSetup.CoordinatesFormat == 2)
		{
			rbCoordinatesFormat0.Checked = false;
			rbCoordinatesFormat1.Checked = false;
			rbCoordinatesFormat2.Checked = true;
		}
		DGV1.Rows.Clear();
		checked
		{
			if (!Information.IsNothing(Public2.AppSetup.OwnerNameList))
			{
				int upperBound = Public2.AppSetup.OwnerNameList.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					DGV1.Rows.Add(Public2.AppSetup.OwnerNameList[i]);
					DGV1.Rows[i].Cells[1].Style.BackColor = Public2.AppSetup.OwnerColorList[i];
				}
			}
			DGV.Rows.Clear();
			StreamReader streamReader = new StreamReader(Public2.AppDataFolder + "\\CustomBaseMaps.set", Encoding.GetEncoding(1251));
			while (!streamReader.EndOfStream)
			{
				string text = streamReader.ReadLine();
				if (Strings.Len(Strings.Trim(text)) > 7)
				{
					short num = (short)Strings.InStr(text, "\t");
					string expression = text.Substring(0, num);
					string expression2 = text.Substring(num);
					expression = Strings.Replace(expression, "\t", "");
					expression2 = Strings.Replace(expression2, "\t", "");
					DGV.Rows.Add(expression, expression2);
				}
			}
			streamReader.Close();
			FlagOK = true;
			if (Constants.UrlROOT.Contains("1"))
			{
				lblSRV.Text = "1";
			}
			else
			{
				lblSRV.Text = "2";
			}
		}
	}

	private void cmdChangeFolderCache_Click(object sender, EventArgs e)
	{
		FBD1.SelectedPath = Public2.AppSetup.FolderCach;
		if (FBD1.ShowDialog() == DialogResult.OK)
		{
			txtFolderCahe.Text = FBD1.SelectedPath;
			Public2.AppSetup.FolderCach = FBD1.SelectedPath;
		}
	}

	private void ccmdChangeAntennasFolder_Click(object sender, EventArgs e)
	{
		FBD1.SelectedPath = Public2.AppSetup.AntennaFolder;
		if (FBD1.ShowDialog() == DialogResult.OK)
		{
			txtAntennaFolder.Text = FBD1.SelectedPath;
			Public2.AppSetup.AntennaFolder = FBD1.SelectedPath;
		}
	}

	private void chkProxy_CheckedChanged(object sender, EventArgs e)
	{
		if (FlagOK)
		{
			Public2.AppSetup.FlagProxy = chkProxy.Checked;
		}
	}

	private void chkProxyLogin_CheckedChanged(object sender, EventArgs e)
	{
		if (FlagOK)
		{
			Public2.AppSetup.FlagProxyLogin = chkProxyLogin.Checked;
		}
	}

	private void txtProxyIP_Validating(object sender, CancelEventArgs e)
	{
		Public2.AppSetup.ProxyIP = txtProxyIP.Text;
	}

	private void txtProxyIP_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			Public2.AppSetup.ProxyIP = txtProxyIP.Text;
		}
	}

	private void txtProxyPORT_Validating(object sender, CancelEventArgs e)
	{
		Public2.AppSetup.ProxyPort = Conversions.ToInteger(txtProxyPORT.Text);
	}

	private void txtProxyPORT_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			Public2.AppSetup.ProxyPort = Conversions.ToInteger(txtProxyPORT.Text);
		}
	}

	private void txtProxyLogin_Validating(object sender, CancelEventArgs e)
	{
		Public2.AppSetup.ProxyLogin = txtProxyLogin.Text;
	}

	private void txtProxyLogin_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			Public2.AppSetup.ProxyLogin = txtProxyLogin.Text;
		}
	}

	private void txtProxyPassword_Validating(object sender, CancelEventArgs e)
	{
		Public2.AppSetup.ProxyPassword = txtProxyPassword.Text;
	}

	private void txtProxyPassword_KeyUp(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			Public2.AppSetup.ProxyPassword = txtProxyPassword.Text;
		}
	}

	private void rbCoordinatesFormat0_CheckedChanged(object sender, EventArgs e)
	{
		if (FlagOK)
		{
			if (rbCoordinatesFormat0.Checked)
			{
				Public2.AppSetup.CoordinatesFormat = 0;
			}
			else if (rbCoordinatesFormat1.Checked)
			{
				Public2.AppSetup.CoordinatesFormat = 1;
			}
			else if (rbCoordinatesFormat2.Checked)
			{
				Public2.AppSetup.CoordinatesFormat = 2;
			}
		}
	}

	private void DGV1_CellClick(object sender, DataGridViewCellEventArgs e)
	{
		if (e.ColumnIndex != 1 || e.RowIndex < 0)
		{
			return;
		}
		CD1.Color = DGV1.Rows[e.RowIndex].Cells[1].Style.BackColor;
		CD1.FullOpen = true;
		checked
		{
			if (CD1.ShowDialog() == DialogResult.OK)
			{
				DGV1.Rows[e.RowIndex].Cells[1].Style.BackColor = CD1.Color;
				if (Information.IsNothing(Public2.AppSetup.OwnerNameList))
				{
					Public2.AppSetup.OwnerColorList = new Color[1];
					Public2.AppSetup.OwnerNameList = new string[1];
				}
				else if (Public2.AppSetup.OwnerNameList.GetUpperBound(0) < e.RowIndex)
				{
					ref Color[] ownerColorList = ref Public2.AppSetup.OwnerColorList;
					ownerColorList = (Color[])Utils.CopyArray(ownerColorList, new Color[e.RowIndex + 1]);
					ref string[] ownerNameList = ref Public2.AppSetup.OwnerNameList;
					ownerNameList = (string[])Utils.CopyArray(ownerNameList, new string[e.RowIndex + 1]);
				}
				Public2.AppSetup.OwnerColorList[e.RowIndex] = CD1.Color;
			}
			DGV1.SelectedCells[0].Selected = false;
		}
	}

	private void DGV1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
	{
		checked
		{
			if (e.ColumnIndex == 0 && e.RowIndex >= 0)
			{
				if (Information.IsNothing(Public2.AppSetup.OwnerNameList))
				{
					Public2.AppSetup.OwnerColorList = new Color[1];
					Public2.AppSetup.OwnerNameList = new string[1];
				}
				else if (Public2.AppSetup.OwnerNameList.GetUpperBound(0) < e.RowIndex)
				{
					ref Color[] ownerColorList = ref Public2.AppSetup.OwnerColorList;
					ownerColorList = (Color[])Utils.CopyArray(ownerColorList, new Color[e.RowIndex + 1]);
					ref string[] ownerNameList = ref Public2.AppSetup.OwnerNameList;
					ownerNameList = (string[])Utils.CopyArray(ownerNameList, new string[e.RowIndex + 1]);
				}
				Public2.AppSetup.OwnerNameList[e.RowIndex] = Conversions.ToString(DGV1[e.ColumnIndex, e.RowIndex].Value);
			}
		}
	}

	private void DGV1_UserDeletedRow(object sender, DataGridViewRowEventArgs e)
	{
		if (DGV1.Rows.Count == 1)
		{
			Public2.AppSetup.OwnerColorList = null;
			Public2.AppSetup.OwnerNameList = null;
			return;
		}
		checked
		{
			int num = DGV1.Rows.Count - 1;
			for (int i = 0; i <= num; i++)
			{
				Public2.AppSetup.OwnerNameList[i] = Conversions.ToString(DGV1[0, i].Value);
				Public2.AppSetup.OwnerColorList[i] = DGV1[1, i].Style.BackColor;
			}
			ref Color[] ownerColorList = ref Public2.AppSetup.OwnerColorList;
			ownerColorList = (Color[])Utils.CopyArray(ownerColorList, new Color[DGV1.Rows.Count - 2 + 1]);
			ref string[] ownerNameList = ref Public2.AppSetup.OwnerNameList;
			ownerNameList = (string[])Utils.CopyArray(ownerNameList, new string[DGV1.Rows.Count - 2 + 1]);
		}
	}

	private void cmdResetToDefault_Click(object sender, EventArgs e)
	{
		if (MessageBox.Show("Настройки базовых карт будут установлены в настройки по умолчанию. Продолжить?", "Предупреждение!", MessageBoxButtons.YesNo) != DialogResult.Yes)
		{
			return;
		}
		File.Copy(Application.StartupPath + "\\CustomBaseMaps.set", Public2.AppDataFolder + "\\CustomBaseMaps.set", overwrite: true);
		string text = MyProject.Forms.Form1.TScmbURL.Text;
		DGV.Rows.Clear();
		StreamReader streamReader = new StreamReader(Public2.AppDataFolder + "\\CustomBaseMaps.set", Encoding.GetEncoding(1251));
		MyProject.Forms.Form1.TScmbURL.Items.Clear();
		while (!streamReader.EndOfStream)
		{
			string text2 = streamReader.ReadLine();
			if (Strings.Len(Strings.Trim(text2)) > 7)
			{
				short num = checked((short)Strings.InStr(text2, "\t"));
				string expression = text2.Substring(0, num);
				string expression2 = text2.Substring(num);
				expression = Strings.Replace(expression, "\t", "");
				expression2 = Strings.Replace(expression2, "\t", "");
				DGV.Rows.Add(expression, expression2);
				MyProject.Forms.Form1.TScmbURL.Items.Add(expression);
			}
		}
		streamReader.Close();
		try
		{
			MyProject.Forms.Form1.TScmbURL.Text = text;
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
	}

	private void UC_ProjectSettings_Validating(object sender, CancelEventArgs e)
	{
	}

	private void cmdApplyBaseMapsSettings_Click(object sender, EventArgs e)
	{
		checked
		{
			int num = DGV.Rows.Count - 2;
			for (int i = 0; i <= num; i++)
			{
				if (Conversions.ToBoolean(Operators.OrObject(Operators.CompareObjectEqual(DGV[0, i].Value, "", TextCompare: false), DGV[1, i].Value.ToString().Length < 10)))
				{
					Interaction.MsgBox(Localization.LocMSG(95), MsgBoxStyle.Critical);
					return;
				}
			}
			string text = MyProject.Forms.Form1.TScmbURL.Text;
			MyProject.Forms.Form1.TScmbURL.Items.Clear();
			StreamWriter streamWriter = new StreamWriter(Public2.AppDataFolder + "\\CustomBaseMaps.set", append: false, Encoding.GetEncoding(1251));
			int num2 = DGV.Rows.Count - 2;
			for (int j = 0; j <= num2; j++)
			{
				streamWriter.WriteLine(Operators.ConcatenateObject(Operators.ConcatenateObject(DGV[0, j].Value, '\t'), DGV[1, j].Value));
				MyProject.Forms.Form1.TScmbURL.Items.Add(RuntimeHelpers.GetObjectValue(DGV[0, j].Value));
			}
			streamWriter.Close();
			try
			{
				MyProject.Forms.Form1.TScmbURL.Text = text;
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
		}
	}

	private void txtSize0_ValueChanged(object sender, EventArgs e)
	{
		if (FlagOK)
		{
			Public2.PRJ.SETT.MaxHorSize[0] = checked((short)Convert.ToInt32(txtSize0.Value));
		}
	}

	private void txtSize1_ValueChanged(object sender, EventArgs e)
	{
		if (FlagOK)
		{
			Public2.PRJ.SETT.MaxHorSize[1] = checked((short)Convert.ToInt32(txtSize1.Value));
		}
	}

	private void txtK_Validating(object sender, CancelEventArgs e)
	{
		TextBox textBox = txtK;
		string TextProperty = textBox.Text;
		ref float koefOslabl = ref Public2.PRJ.SETT.KoefOslabl;
		double DblVariable = koefOslabl;
		Module1.TextPropertyValidating(ref TextProperty, ref DblVariable, CheckLimits: true, 1f, 2f, IncludeMin: true, IncludeMax: true, Message: true);
		koefOslabl = (float)DblVariable;
		textBox.Text = TextProperty;
	}
}
