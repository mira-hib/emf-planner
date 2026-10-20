using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[DesignerGenerated]
public class UC_Tables : UserControl
{
	private IContainer components;

	private bool FlagProcessing;

	[field: AccessedThroughProperty("GroupBox1")]
	internal virtual GroupBox GroupBox1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdEFieldStrengthVerticalCutTables_1")]
	internal virtual Button cmdEFieldStrengthVerticalCutTables_1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdEFieldStrengthVerticalCutTables_1_Click;
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

	[field: AccessedThroughProperty("Label5")]
	internal virtual Label Label5
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtTabl1HeightMax")]
	internal virtual TextBox txtTabl1HeightMax
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			CancelEventHandler value2 = txtTabl1HeightMax_Validating;
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

	[field: AccessedThroughProperty("Label4")]
	internal virtual Label Label4
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtTabl1StepH")]
	internal virtual TextBox txtTabl1StepH
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			CancelEventHandler value2 = txtTabl1StepH_Validating;
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

	[field: AccessedThroughProperty("Label1")]
	internal virtual Label Label1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtTabl1LengthMax")]
	internal virtual TextBox txtTabl1LengthMax
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			CancelEventHandler value2 = txtTabl1LengthMax_Validating;
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

	[field: AccessedThroughProperty("Label2")]
	internal virtual Label Label2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtTabl1StepL")]
	internal virtual TextBox txtTabl1StepL
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			CancelEventHandler value2 = txtTabl1StepL_Validating;
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

	[field: AccessedThroughProperty("GroupBox4")]
	internal virtual GroupBox GroupBox4
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

	[field: AccessedThroughProperty("txtGraph1LengthMax")]
	internal virtual TextBox txtGraph1LengthMax
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			CancelEventHandler value2 = txtGraph1LengthMax_Validating;
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

	[field: AccessedThroughProperty("Label9")]
	internal virtual Label Label9
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtGraph1Height")]
	internal virtual TextBox txtGraph1Height
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			CancelEventHandler value2 = txtGraph1Height_Validating;
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

	[field: AccessedThroughProperty("ToolTip1")]
	internal virtual ToolTip ToolTip1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdEFieldStrengthGraph_1")]
	internal virtual Button cmdEFieldStrengthGraph_1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdEFieldStrengthGraph_1_Click;
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

	[field: AccessedThroughProperty("Tabl1LengthMax")]
	internal virtual TextBox Tabl1LengthMax
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdSimultaneousExposureGraph_1")]
	internal virtual Button cmdSimultaneousExposureGraph_1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = ccmdSimultaneousExposureGraph_1_Click;
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

	[field: AccessedThroughProperty("cmdSimultaneousExposureVerticalCutTables_1")]
	internal virtual Button cmdSimultaneousExposureVerticalCutTables_1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdSimultaneousExposureVerticalCutTables_1_Click;
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

	[field: AccessedThroughProperty("cmdRefEFieldStrengthVerticalCutTables_1")]
	internal virtual Button cmdRefEFieldStrengthVerticalCutTables_1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdRefEFieldStrengthVerticalCutTables_1_Click;
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

	[field: AccessedThroughProperty("cmdRefEFieldStrengthGraph_1")]
	internal virtual Button cmdRefEFieldStrengthGraph_1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdRefEFieldStrengthGraph_1_Click;
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

	public UC_Tables()
	{
		Load += UC_Tables_Load;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EMFPlanner.UC_Tables));
		this.GroupBox1 = new System.Windows.Forms.GroupBox();
		this.GroupBox3 = new System.Windows.Forms.GroupBox();
		this.rbLimit3 = new System.Windows.Forms.RadioButton();
		this.rbLimit2 = new System.Windows.Forms.RadioButton();
		this.rbLimit1 = new System.Windows.Forms.RadioButton();
		this.rbLimit0 = new System.Windows.Forms.RadioButton();
		this.GroupBox4 = new System.Windows.Forms.GroupBox();
		this.cmdRefEFieldStrengthGraph_1 = new System.Windows.Forms.Button();
		this.cmdSimultaneousExposureGraph_1 = new System.Windows.Forms.Button();
		this.Label6 = new System.Windows.Forms.Label();
		this.txtGraph1LengthMax = new System.Windows.Forms.TextBox();
		this.Label9 = new System.Windows.Forms.Label();
		this.txtGraph1Height = new System.Windows.Forms.TextBox();
		this.cmdEFieldStrengthGraph_1 = new System.Windows.Forms.Button();
		this.GroupBox2 = new System.Windows.Forms.GroupBox();
		this.cmdRefEFieldStrengthVerticalCutTables_1 = new System.Windows.Forms.Button();
		this.cmdSimultaneousExposureVerticalCutTables_1 = new System.Windows.Forms.Button();
		this.Label5 = new System.Windows.Forms.Label();
		this.txtTabl1HeightMax = new System.Windows.Forms.TextBox();
		this.Label4 = new System.Windows.Forms.Label();
		this.txtTabl1StepH = new System.Windows.Forms.TextBox();
		this.Label1 = new System.Windows.Forms.Label();
		this.txtTabl1LengthMax = new System.Windows.Forms.TextBox();
		this.Label2 = new System.Windows.Forms.Label();
		this.txtTabl1StepL = new System.Windows.Forms.TextBox();
		this.cmdEFieldStrengthVerticalCutTables_1 = new System.Windows.Forms.Button();
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.Tabl1LengthMax = new System.Windows.Forms.TextBox();
		this.GroupBox1.SuspendLayout();
		this.GroupBox3.SuspendLayout();
		this.GroupBox4.SuspendLayout();
		this.GroupBox2.SuspendLayout();
		base.SuspendLayout();
		this.GroupBox1.Controls.Add(this.GroupBox3);
		this.GroupBox1.Controls.Add(this.GroupBox4);
		this.GroupBox1.Controls.Add(this.GroupBox2);
		this.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GroupBox1.Location = new System.Drawing.Point(0, 0);
		this.GroupBox1.Name = "GroupBox1";
		this.GroupBox1.Size = new System.Drawing.Size(1131, 745);
		this.GroupBox1.TabIndex = 3;
		this.GroupBox1.TabStop = false;
		this.GroupBox1.Text = "Parameters of standard aliases for a report (tables and graphs)";
		this.GroupBox3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.GroupBox3.Controls.Add(this.rbLimit3);
		this.GroupBox3.Controls.Add(this.rbLimit2);
		this.GroupBox3.Controls.Add(this.rbLimit1);
		this.GroupBox3.Controls.Add(this.rbLimit0);
		this.GroupBox3.Location = new System.Drawing.Point(6, 683);
		this.GroupBox3.Name = "GroupBox3";
		this.GroupBox3.Size = new System.Drawing.Size(491, 52);
		this.GroupBox3.TabIndex = 203;
		this.GroupBox3.TabStop = false;
		this.GroupBox3.Text = "Use EMF Limits for calculations";
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
		this.GroupBox4.Controls.Add(this.cmdRefEFieldStrengthGraph_1);
		this.GroupBox4.Controls.Add(this.cmdSimultaneousExposureGraph_1);
		this.GroupBox4.Controls.Add(this.Label6);
		this.GroupBox4.Controls.Add(this.txtGraph1LengthMax);
		this.GroupBox4.Controls.Add(this.Label9);
		this.GroupBox4.Controls.Add(this.txtGraph1Height);
		this.GroupBox4.Controls.Add(this.cmdEFieldStrengthGraph_1);
		this.GroupBox4.Location = new System.Drawing.Point(6, 213);
		this.GroupBox4.Name = "GroupBox4";
		this.GroupBox4.Size = new System.Drawing.Size(307, 131);
		this.GroupBox4.TabIndex = 205;
		this.GroupBox4.TabStop = false;
		this.GroupBox4.Text = "{EFieldStrengthGraph_1}                 {SimultaneousExposureGraph_1}";
		this.cmdRefEFieldStrengthGraph_1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdRefEFieldStrengthGraph_1.Image = (System.Drawing.Image)resources.GetObject("cmdRefEFieldStrengthGraph_1.Image");
		this.cmdRefEFieldStrengthGraph_1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdRefEFieldStrengthGraph_1.Location = new System.Drawing.Point(63, 37);
		this.cmdRefEFieldStrengthGraph_1.Name = "cmdRefEFieldStrengthGraph_1";
		this.cmdRefEFieldStrengthGraph_1.Size = new System.Drawing.Size(26, 26);
		this.cmdRefEFieldStrengthGraph_1.TabIndex = 206;
		this.cmdRefEFieldStrengthGraph_1.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdRefEFieldStrengthGraph_1, "Alias description");
		this.cmdRefEFieldStrengthGraph_1.UseVisualStyleBackColor = true;
		this.cmdSimultaneousExposureGraph_1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdSimultaneousExposureGraph_1.Image = (System.Drawing.Image)resources.GetObject("cmdSimultaneousExposureGraph_1.Image");
		this.cmdSimultaneousExposureGraph_1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdSimultaneousExposureGraph_1.Location = new System.Drawing.Point(36, 37);
		this.cmdSimultaneousExposureGraph_1.Name = "cmdSimultaneousExposureGraph_1";
		this.cmdSimultaneousExposureGraph_1.Size = new System.Drawing.Size(26, 26);
		this.cmdSimultaneousExposureGraph_1.TabIndex = 204;
		this.cmdSimultaneousExposureGraph_1.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdSimultaneousExposureGraph_1, "{SimultaneousExposureGraph_1}");
		this.cmdSimultaneousExposureGraph_1.UseVisualStyleBackColor = true;
		this.Label6.AutoSize = true;
		this.Label6.Location = new System.Drawing.Point(55, 103);
		this.Label6.Name = "Label6";
		this.Label6.Size = new System.Drawing.Size(55, 13);
		this.Label6.TabIndex = 203;
		this.Label6.Text = "Height (m)";
		this.txtGraph1LengthMax.Location = new System.Drawing.Point(9, 74);
		this.txtGraph1LengthMax.Name = "txtGraph1LengthMax";
		this.txtGraph1LengthMax.Size = new System.Drawing.Size(35, 20);
		this.txtGraph1LengthMax.TabIndex = 7;
		this.txtGraph1LengthMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.Label9.AutoSize = true;
		this.Label9.Location = new System.Drawing.Point(55, 77);
		this.Label9.Name = "Label9";
		this.Label9.Size = new System.Drawing.Size(57, 13);
		this.Label9.TabIndex = 197;
		this.Label9.Text = "Lenght (m)";
		this.txtGraph1Height.Location = new System.Drawing.Point(9, 100);
		this.txtGraph1Height.Name = "txtGraph1Height";
		this.txtGraph1Height.Size = new System.Drawing.Size(35, 20);
		this.txtGraph1Height.TabIndex = 8;
		this.txtGraph1Height.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.cmdEFieldStrengthGraph_1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdEFieldStrengthGraph_1.Image = (System.Drawing.Image)resources.GetObject("cmdEFieldStrengthGraph_1.Image");
		this.cmdEFieldStrengthGraph_1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdEFieldStrengthGraph_1.Location = new System.Drawing.Point(9, 37);
		this.cmdEFieldStrengthGraph_1.Name = "cmdEFieldStrengthGraph_1";
		this.cmdEFieldStrengthGraph_1.Size = new System.Drawing.Size(26, 26);
		this.cmdEFieldStrengthGraph_1.TabIndex = 192;
		this.cmdEFieldStrengthGraph_1.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdEFieldStrengthGraph_1, "{EFieldStrengthGraph_1}");
		this.cmdEFieldStrengthGraph_1.UseVisualStyleBackColor = true;
		this.GroupBox2.Controls.Add(this.cmdRefEFieldStrengthVerticalCutTables_1);
		this.GroupBox2.Controls.Add(this.cmdSimultaneousExposureVerticalCutTables_1);
		this.GroupBox2.Controls.Add(this.Label5);
		this.GroupBox2.Controls.Add(this.txtTabl1HeightMax);
		this.GroupBox2.Controls.Add(this.Label4);
		this.GroupBox2.Controls.Add(this.txtTabl1StepH);
		this.GroupBox2.Controls.Add(this.Label1);
		this.GroupBox2.Controls.Add(this.txtTabl1LengthMax);
		this.GroupBox2.Controls.Add(this.Label2);
		this.GroupBox2.Controls.Add(this.txtTabl1StepL);
		this.GroupBox2.Controls.Add(this.cmdEFieldStrengthVerticalCutTables_1);
		this.GroupBox2.Location = new System.Drawing.Point(6, 23);
		this.GroupBox2.Name = "GroupBox2";
		this.GroupBox2.Size = new System.Drawing.Size(307, 184);
		this.GroupBox2.TabIndex = 194;
		this.GroupBox2.TabStop = false;
		this.GroupBox2.Text = "{EFieldStrengthVerticalCutTables_1} {SimultaneousExposureVerticalCutTables_1}";
		this.cmdRefEFieldStrengthVerticalCutTables_1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdRefEFieldStrengthVerticalCutTables_1.Image = (System.Drawing.Image)resources.GetObject("cmdRefEFieldStrengthVerticalCutTables_1.Image");
		this.cmdRefEFieldStrengthVerticalCutTables_1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdRefEFieldStrengthVerticalCutTables_1.Location = new System.Drawing.Point(63, 38);
		this.cmdRefEFieldStrengthVerticalCutTables_1.Name = "cmdRefEFieldStrengthVerticalCutTables_1";
		this.cmdRefEFieldStrengthVerticalCutTables_1.Size = new System.Drawing.Size(26, 26);
		this.cmdRefEFieldStrengthVerticalCutTables_1.TabIndex = 205;
		this.cmdRefEFieldStrengthVerticalCutTables_1.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdRefEFieldStrengthVerticalCutTables_1, "Alias description");
		this.cmdRefEFieldStrengthVerticalCutTables_1.UseVisualStyleBackColor = true;
		this.cmdSimultaneousExposureVerticalCutTables_1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdSimultaneousExposureVerticalCutTables_1.Image = (System.Drawing.Image)resources.GetObject("cmdSimultaneousExposureVerticalCutTables_1.Image");
		this.cmdSimultaneousExposureVerticalCutTables_1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdSimultaneousExposureVerticalCutTables_1.Location = new System.Drawing.Point(36, 38);
		this.cmdSimultaneousExposureVerticalCutTables_1.Name = "cmdSimultaneousExposureVerticalCutTables_1";
		this.cmdSimultaneousExposureVerticalCutTables_1.Size = new System.Drawing.Size(26, 26);
		this.cmdSimultaneousExposureVerticalCutTables_1.TabIndex = 204;
		this.cmdSimultaneousExposureVerticalCutTables_1.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdSimultaneousExposureVerticalCutTables_1, "{SimultaneousExposureVerticalCutTables_1}");
		this.cmdSimultaneousExposureVerticalCutTables_1.UseVisualStyleBackColor = true;
		this.Label5.AutoSize = true;
		this.Label5.Location = new System.Drawing.Point(55, 130);
		this.Label5.Name = "Label5";
		this.Label5.Size = new System.Drawing.Size(100, 13);
		this.Label5.TabIndex = 203;
		this.Label5.Text = "Maximum height (m)";
		this.txtTabl1HeightMax.Location = new System.Drawing.Point(9, 127);
		this.txtTabl1HeightMax.Name = "txtTabl1HeightMax";
		this.txtTabl1HeightMax.Size = new System.Drawing.Size(35, 20);
		this.txtTabl1HeightMax.TabIndex = 2;
		this.txtTabl1HeightMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.Label4.AutoSize = true;
		this.Label4.Location = new System.Drawing.Point(55, 156);
		this.Label4.Name = "Label4";
		this.Label4.Size = new System.Drawing.Size(108, 13);
		this.Label4.TabIndex = 201;
		this.Label4.Text = "Vertical point step (m)";
		this.txtTabl1StepH.Location = new System.Drawing.Point(9, 153);
		this.txtTabl1StepH.Name = "txtTabl1StepH";
		this.txtTabl1StepH.Size = new System.Drawing.Size(35, 20);
		this.txtTabl1StepH.TabIndex = 3;
		this.txtTabl1StepH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.Label1.AutoSize = true;
		this.Label1.Location = new System.Drawing.Point(55, 78);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(159, 13);
		this.Label1.TabIndex = 197;
		this.Label1.Text = "Maximum horizontal distance (m)";
		this.txtTabl1LengthMax.Location = new System.Drawing.Point(9, 75);
		this.txtTabl1LengthMax.Name = "txtTabl1LengthMax";
		this.txtTabl1LengthMax.Size = new System.Drawing.Size(35, 20);
		this.txtTabl1LengthMax.TabIndex = 0;
		this.txtTabl1LengthMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.Label2.AutoSize = true;
		this.Label2.Location = new System.Drawing.Point(55, 104);
		this.Label2.Name = "Label2";
		this.Label2.Size = new System.Drawing.Size(120, 13);
		this.Label2.TabIndex = 195;
		this.Label2.Text = "Horizontal point step (m)";
		this.txtTabl1StepL.Location = new System.Drawing.Point(9, 101);
		this.txtTabl1StepL.Name = "txtTabl1StepL";
		this.txtTabl1StepL.Size = new System.Drawing.Size(35, 20);
		this.txtTabl1StepL.TabIndex = 1;
		this.txtTabl1StepL.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.cmdEFieldStrengthVerticalCutTables_1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdEFieldStrengthVerticalCutTables_1.Image = (System.Drawing.Image)resources.GetObject("cmdEFieldStrengthVerticalCutTables_1.Image");
		this.cmdEFieldStrengthVerticalCutTables_1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdEFieldStrengthVerticalCutTables_1.Location = new System.Drawing.Point(9, 38);
		this.cmdEFieldStrengthVerticalCutTables_1.Name = "cmdEFieldStrengthVerticalCutTables_1";
		this.cmdEFieldStrengthVerticalCutTables_1.Size = new System.Drawing.Size(26, 26);
		this.cmdEFieldStrengthVerticalCutTables_1.TabIndex = 1;
		this.cmdEFieldStrengthVerticalCutTables_1.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdEFieldStrengthVerticalCutTables_1, "{EFieldStrengthVerticalCutTables_1}");
		this.cmdEFieldStrengthVerticalCutTables_1.UseVisualStyleBackColor = true;
		this.Tabl1LengthMax.Location = new System.Drawing.Point(9, 64);
		this.Tabl1LengthMax.Name = "Tabl1LengthMax";
		this.Tabl1LengthMax.Size = new System.Drawing.Size(35, 20);
		this.Tabl1LengthMax.TabIndex = 196;
		this.Tabl1LengthMax.Text = "200";
		this.Tabl1LengthMax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		base.Controls.Add(this.GroupBox1);
		this.MinimumSize = new System.Drawing.Size(700, 0);
		base.Name = "UC_Tables";
		base.Size = new System.Drawing.Size(1131, 745);
		this.GroupBox1.ResumeLayout(false);
		this.GroupBox3.ResumeLayout(false);
		this.GroupBox3.PerformLayout();
		this.GroupBox4.ResumeLayout(false);
		this.GroupBox4.PerformLayout();
		this.GroupBox2.ResumeLayout(false);
		this.GroupBox2.PerformLayout();
		base.ResumeLayout(false);
	}

	private void UC_Tables_Load(object sender, EventArgs e)
	{
		txtTabl1LengthMax.Text = Conversions.ToString(Public2.PRJ.SETT.Tabl1LengthMax);
		txtTabl1StepL.Text = Conversions.ToString(Public2.PRJ.SETT.Tabl1StepL);
		txtTabl1HeightMax.Text = Conversions.ToString(Public2.PRJ.SETT.Tabl1HeightMax);
		txtTabl1StepH.Text = Conversions.ToString(Public2.PRJ.SETT.Tabl1StepH);
		txtGraph1LengthMax.Text = Conversions.ToString(Public2.PRJ.SETT.Graph1LengthMax);
		txtGraph1Height.Text = Conversions.ToString(Public2.PRJ.SETT.Graph1Height);
		if (Public2.PRJ.SETT.TablHoriz1Angle == 0f)
		{
			Public2.PRJ.SETT.TablHoriz1Angle = 5f;
		}
		rbLimit0.ForeColor = Public2.PRJ.SETT.LimitColor[0];
		rbLimit1.ForeColor = Public2.PRJ.SETT.LimitColor[1];
		rbLimit2.ForeColor = Public2.PRJ.SETT.LimitColor[2];
		rbLimit3.ForeColor = Public2.PRJ.SETT.LimitColor[3];
		FlagProcessing = true;
		if (Public2.PRJ.SETT.TableGraphLimitIndex == 0)
		{
			rbLimit0.Checked = true;
			rbLimit1.Checked = false;
			rbLimit2.Checked = false;
			rbLimit3.Checked = false;
		}
		else if (Public2.PRJ.SETT.TableGraphLimitIndex == 1)
		{
			rbLimit0.Checked = false;
			rbLimit1.Checked = true;
			rbLimit2.Checked = false;
			rbLimit3.Checked = false;
		}
		else if (Public2.PRJ.SETT.TableGraphLimitIndex == 2)
		{
			rbLimit0.Checked = false;
			rbLimit1.Checked = false;
			rbLimit2.Checked = true;
			rbLimit3.Checked = false;
		}
		else if (Public2.PRJ.SETT.TableGraphLimitIndex == 3)
		{
			rbLimit0.Checked = false;
			rbLimit1.Checked = false;
			rbLimit2.Checked = false;
			rbLimit3.Checked = true;
		}
		FlagProcessing = false;
	}

	private void cmdEFieldStrengthVerticalCutTables_1_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Information.IsNothing(Public2.PRJ.TX))
		{
			Interaction.MsgBox(Localization.LocMSG(46), MsgBoxStyle.Critical);
		}
		else if (Information.IsNothing(Public2.PRJ.VP))
		{
			Interaction.MsgBox(Localization.LocMSG(66), MsgBoxStyle.Critical);
		}
		else if ((Public2.PRJ.SETT.Tabl1StepL >= Public2.PRJ.SETT.Tabl1LengthMax) | (Public2.PRJ.SETT.Tabl1StepH >= Public2.PRJ.SETT.Tabl1HeightMax))
		{
			Interaction.MsgBox(Localization.LocMSG(20), MsgBoxStyle.Critical);
		}
		else if (CalcSZZ.CalcTmp())
		{
			RepWordAlias.WordReportOneAlias("{EFieldStrengthVerticalCutTables_1}");
		}
	}

	private void cmdSimultaneousExposureVerticalCutTables_1_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Information.IsNothing(Public2.PRJ.TX))
		{
			Interaction.MsgBox(Localization.LocMSG(46), MsgBoxStyle.Critical);
		}
		else if (Information.IsNothing(Public2.PRJ.VP))
		{
			Interaction.MsgBox(Localization.LocMSG(66), MsgBoxStyle.Critical);
		}
		else if ((Public2.PRJ.SETT.Tabl1StepL >= Public2.PRJ.SETT.Tabl1LengthMax) | (Public2.PRJ.SETT.Tabl1StepH >= Public2.PRJ.SETT.Tabl1HeightMax))
		{
			Interaction.MsgBox(Localization.LocMSG(20), MsgBoxStyle.Critical);
		}
		else if (CalcSZZ.CalcTmp())
		{
			RepWordAlias.WordReportOneAlias("{SimultaneousExposureVerticalCutTables_1}");
		}
	}

	private void cmdRefEFieldStrengthVerticalCutTables_1_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		try
		{
			Microsoft.Office.Interop.Word.Application application = (Microsoft.Office.Interop.Word.Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("000209FF-0000-0000-C000-000000000046")));
			Documents documents = application.Documents;
			object Template = Public2.AppStartupPath + "\\Samples\\Aliases for a custom report.dot";
			object NewTemplate = RuntimeHelpers.GetObjectValue(Missing.Value);
			object DocumentType = RuntimeHelpers.GetObjectValue(Missing.Value);
			object Visible = RuntimeHelpers.GetObjectValue(Missing.Value);
			Document document = documents.Add(ref Template, ref NewTemplate, ref DocumentType, ref Visible);
			document.ShowSpellingErrors = false;
			document.ShowGrammaticalErrors = false;
			Find find = application.Selection.Find;
			find.Forward = true;
			find.ClearFormatting();
			find.MatchWholeWord = true;
			find.MatchCase = false;
			find.Wrap = WdFindWrap.wdFindContinue;
			Visible = "{EFieldStrengthVerticalCutTables_1}";
			DocumentType = RuntimeHelpers.GetObjectValue(Missing.Value);
			NewTemplate = RuntimeHelpers.GetObjectValue(Missing.Value);
			Template = RuntimeHelpers.GetObjectValue(Missing.Value);
			object MatchSoundsLike = RuntimeHelpers.GetObjectValue(Missing.Value);
			object MatchAllWordForms = RuntimeHelpers.GetObjectValue(Missing.Value);
			object Forward = RuntimeHelpers.GetObjectValue(Missing.Value);
			object Wrap = RuntimeHelpers.GetObjectValue(Missing.Value);
			object Format = RuntimeHelpers.GetObjectValue(Missing.Value);
			object ReplaceWith = RuntimeHelpers.GetObjectValue(Missing.Value);
			object Replace = RuntimeHelpers.GetObjectValue(Missing.Value);
			object MatchKashida = RuntimeHelpers.GetObjectValue(Missing.Value);
			object MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
			object MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
			object MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
			find.Execute(ref Visible, ref DocumentType, ref NewTemplate, ref Template, ref MatchSoundsLike, ref MatchAllWordForms, ref Forward, ref Wrap, ref Format, ref ReplaceWith, ref Replace, ref MatchKashida, ref MatchDiacritics, ref MatchAlefHamza, ref MatchControl);
			_ = null;
			Selection selection = application.Selection;
			MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
			selection.MoveEnd(ref MatchControl, ref MatchAlefHamza);
			application.Selection.Range.HighlightColorIndex = WdColorIndex.wdYellow;
			application.Visible = true;
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Interaction.MsgBox(ex2.Message);
			ProjectData.ClearProjectError();
		}
	}

	private void txtTabl1LengthMax_Validating(object sender, CancelEventArgs e)
	{
		TextBox textBox = txtTabl1LengthMax;
		string TextProperty = textBox.Text;
		ref float tabl1LengthMax = ref Public2.PRJ.SETT.Tabl1LengthMax;
		double DblVariable = tabl1LengthMax;
		Module1.TextPropertyValidating(ref TextProperty, ref DblVariable, CheckLimits: true, 10f, 10000f, IncludeMin: true, IncludeMax: true, Message: true);
		tabl1LengthMax = (float)DblVariable;
		textBox.Text = TextProperty;
	}

	private void txtTabl1StepL_Validating(object sender, CancelEventArgs e)
	{
		TextBox textBox = txtTabl1StepL;
		string TextProperty = textBox.Text;
		ref float tabl1StepL = ref Public2.PRJ.SETT.Tabl1StepL;
		double DblVariable = tabl1StepL;
		Module1.TextPropertyValidating(ref TextProperty, ref DblVariable, CheckLimits: true, 0.1f, 100f, IncludeMin: true, IncludeMax: true, Message: true);
		tabl1StepL = (float)DblVariable;
		textBox.Text = TextProperty;
	}

	private void txtTabl1HeightMax_Validating(object sender, CancelEventArgs e)
	{
		TextBox textBox = txtTabl1HeightMax;
		string TextProperty = textBox.Text;
		ref float tabl1HeightMax = ref Public2.PRJ.SETT.Tabl1HeightMax;
		double DblVariable = tabl1HeightMax;
		Module1.TextPropertyValidating(ref TextProperty, ref DblVariable, CheckLimits: true, 1f, 1000f, IncludeMin: true, IncludeMax: true, Message: true);
		tabl1HeightMax = (float)DblVariable;
		textBox.Text = TextProperty;
	}

	private void txtTabl1StepH_Validating(object sender, CancelEventArgs e)
	{
		TextBox textBox = txtTabl1StepH;
		string TextProperty = textBox.Text;
		ref float tabl1StepH = ref Public2.PRJ.SETT.Tabl1StepH;
		double DblVariable = tabl1StepH;
		Module1.TextPropertyValidating(ref TextProperty, ref DblVariable, CheckLimits: true, 0.1f, 50f, IncludeMin: true, IncludeMax: true, Message: true);
		tabl1StepH = (float)DblVariable;
		textBox.Text = TextProperty;
	}

	private void cmdEFieldStrengthGraph_1_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Information.IsNothing(Public2.PRJ.TX))
		{
			Interaction.MsgBox(Localization.LocMSG(46), MsgBoxStyle.Critical);
		}
		else if (Information.IsNothing(Public2.PRJ.VP))
		{
			Interaction.MsgBox(Localization.LocMSG(66), MsgBoxStyle.Critical);
		}
		else if (CalcSZZ.CalcTmp())
		{
			RepWordAlias.WordReportOneAlias("{EFieldStrengthGraph_1}");
		}
	}

	private void ccmdSimultaneousExposureGraph_1_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Information.IsNothing(Public2.PRJ.TX))
		{
			Interaction.MsgBox(Localization.LocMSG(46), MsgBoxStyle.Critical);
		}
		else if (Information.IsNothing(Public2.PRJ.VP))
		{
			Interaction.MsgBox(Localization.LocMSG(66), MsgBoxStyle.Critical);
		}
		else if (CalcSZZ.CalcTmp())
		{
			RepWordAlias.WordReportOneAlias("{SimultaneousExposureGraph_1}");
		}
	}

	private void cmdRefEFieldStrengthGraph_1_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		try
		{
			Microsoft.Office.Interop.Word.Application application = (Microsoft.Office.Interop.Word.Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("000209FF-0000-0000-C000-000000000046")));
			Documents documents = application.Documents;
			object Template = Public2.AppStartupPath + "\\Samples\\Aliases for a custom report.dot";
			object NewTemplate = RuntimeHelpers.GetObjectValue(Missing.Value);
			object DocumentType = RuntimeHelpers.GetObjectValue(Missing.Value);
			object Visible = RuntimeHelpers.GetObjectValue(Missing.Value);
			Document document = documents.Add(ref Template, ref NewTemplate, ref DocumentType, ref Visible);
			document.ShowSpellingErrors = false;
			document.ShowGrammaticalErrors = false;
			Find find = application.Selection.Find;
			find.Forward = true;
			find.ClearFormatting();
			find.MatchWholeWord = true;
			find.MatchCase = false;
			find.Wrap = WdFindWrap.wdFindContinue;
			Visible = "{EFieldStrengthGraph_1}";
			DocumentType = RuntimeHelpers.GetObjectValue(Missing.Value);
			NewTemplate = RuntimeHelpers.GetObjectValue(Missing.Value);
			Template = RuntimeHelpers.GetObjectValue(Missing.Value);
			object MatchSoundsLike = RuntimeHelpers.GetObjectValue(Missing.Value);
			object MatchAllWordForms = RuntimeHelpers.GetObjectValue(Missing.Value);
			object Forward = RuntimeHelpers.GetObjectValue(Missing.Value);
			object Wrap = RuntimeHelpers.GetObjectValue(Missing.Value);
			object Format = RuntimeHelpers.GetObjectValue(Missing.Value);
			object ReplaceWith = RuntimeHelpers.GetObjectValue(Missing.Value);
			object Replace = RuntimeHelpers.GetObjectValue(Missing.Value);
			object MatchKashida = RuntimeHelpers.GetObjectValue(Missing.Value);
			object MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
			object MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
			object MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
			find.Execute(ref Visible, ref DocumentType, ref NewTemplate, ref Template, ref MatchSoundsLike, ref MatchAllWordForms, ref Forward, ref Wrap, ref Format, ref ReplaceWith, ref Replace, ref MatchKashida, ref MatchDiacritics, ref MatchAlefHamza, ref MatchControl);
			_ = null;
			Selection selection = application.Selection;
			MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
			selection.MoveEnd(ref MatchControl, ref MatchAlefHamza);
			application.Selection.Range.HighlightColorIndex = WdColorIndex.wdYellow;
			application.Visible = true;
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Interaction.MsgBox(ex2.Message);
			ProjectData.ClearProjectError();
		}
	}

	private void txtGraph1LengthMax_Validating(object sender, CancelEventArgs e)
	{
		TextBox textBox = txtGraph1LengthMax;
		string TextProperty = textBox.Text;
		ref float graph1LengthMax = ref Public2.PRJ.SETT.Graph1LengthMax;
		double DblVariable = graph1LengthMax;
		Module1.TextPropertyValidating(ref TextProperty, ref DblVariable, CheckLimits: true, 10f, 10000f, IncludeMin: true, IncludeMax: true, Message: true);
		graph1LengthMax = (float)DblVariable;
		textBox.Text = TextProperty;
	}

	private void txtGraph1Height_Validating(object sender, CancelEventArgs e)
	{
		TextBox textBox = txtGraph1Height;
		string TextProperty = textBox.Text;
		ref float graph1Height = ref Public2.PRJ.SETT.Graph1Height;
		double DblVariable = graph1Height;
		Module1.TextPropertyValidating(ref TextProperty, ref DblVariable, CheckLimits: true, 1f, 1000f, IncludeMin: true, IncludeMax: true, Message: true);
		graph1Height = (float)DblVariable;
		textBox.Text = TextProperty;
	}

	private void rbLimit_CheckedChanged(object sender, EventArgs e)
	{
		if (!FlagProcessing)
		{
			if (rbLimit0.Checked)
			{
				Public2.PRJ.SETT.TableGraphLimitIndex = 0;
			}
			else if (rbLimit1.Checked)
			{
				Public2.PRJ.SETT.TableGraphLimitIndex = 1;
			}
			else if (rbLimit2.Checked)
			{
				Public2.PRJ.SETT.TableGraphLimitIndex = 2;
			}
			else if (rbLimit3.Checked)
			{
				Public2.PRJ.SETT.TableGraphLimitIndex = 3;
			}
		}
	}
}
