using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[DesignerGenerated]
public class frmCableCalc : Form
{
	private IContainer components;

	private float Freq;

	[field: AccessedThroughProperty("gbTEXT")]
	internal virtual GroupBox gbTEXT
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label65")]
	internal virtual Label Label65
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtExtraLoss")]
	internal virtual TextBox txtExtraLoss
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = TXT_TextChanged;
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

	[field: AccessedThroughProperty("Label66")]
	internal virtual Label Label66
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label67")]
	internal virtual Label Label67
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtAftLoss")]
	internal virtual TextBox txtAftLoss
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label68")]
	internal virtual Label Label68
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label51")]
	internal virtual Label Label51
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtLengthJumper")]
	internal virtual TextBox txtLengthJumper
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = TXT_TextChanged;
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

	[field: AccessedThroughProperty("Label59")]
	internal virtual Label Label59
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label60")]
	internal virtual Label Label60
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtJumperLoss")]
	internal virtual TextBox txtJumperLoss
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label61")]
	internal virtual Label Label61
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label62")]
	internal virtual Label Label62
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtJumperUdelLoss")]
	internal virtual TextBox txtJumperUdelLoss
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label63")]
	internal virtual Label Label63
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label64")]
	internal virtual Label Label64
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cbJUMPER")]
	internal virtual ComboBox cbJUMPER
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cbJUMPER_SelectionChangeCommitted;
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

	[field: AccessedThroughProperty("chkLossSocket")]
	internal virtual CheckBox chkLossSocket
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = chkLossSocket_CheckedChanged;
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

	[field: AccessedThroughProperty("Label48")]
	internal virtual Label Label48
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtLossSocketOne")]
	internal virtual TextBox txtLossSocketOne
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = TXT_TextChanged;
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

	[field: AccessedThroughProperty("Label49")]
	internal virtual Label Label49
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtLengthCable")]
	internal virtual TextBox txtLengthCable
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = TXT_TextChanged;
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

	[field: AccessedThroughProperty("Label50")]
	internal virtual Label Label50
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label58")]
	internal virtual Label Label58
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label53")]
	internal virtual Label Label53
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtLossSocketAll")]
	internal virtual TextBox txtLossSocketAll
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtCableLoss")]
	internal virtual TextBox txtCableLoss
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label52")]
	internal virtual Label Label52
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label54")]
	internal virtual Label Label54
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label55")]
	internal virtual Label Label55
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtCableUdelLoss")]
	internal virtual TextBox txtCableUdelLoss
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label56")]
	internal virtual Label Label56
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label57")]
	internal virtual Label Label57
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cbCABLE")]
	internal virtual ComboBox cbCABLE
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cbCABLE_SelectionChangeCommitted;
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

	[field: AccessedThroughProperty("cmdCANCEL")]
	internal virtual Button cmdCANCEL
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdCANCEL_Click;
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

	[field: AccessedThroughProperty("cmdOK")]
	internal virtual Button cmdOK
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdOK_Click;
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

	public frmCableCalc()
	{
		Load += frmAFT_Load;
		Freq = 900f;
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
		this.gbTEXT = new System.Windows.Forms.GroupBox();
		this.Label65 = new System.Windows.Forms.Label();
		this.txtExtraLoss = new System.Windows.Forms.TextBox();
		this.Label66 = new System.Windows.Forms.Label();
		this.Label67 = new System.Windows.Forms.Label();
		this.txtAftLoss = new System.Windows.Forms.TextBox();
		this.Label68 = new System.Windows.Forms.Label();
		this.Label51 = new System.Windows.Forms.Label();
		this.txtLengthJumper = new System.Windows.Forms.TextBox();
		this.Label59 = new System.Windows.Forms.Label();
		this.Label60 = new System.Windows.Forms.Label();
		this.txtJumperLoss = new System.Windows.Forms.TextBox();
		this.Label61 = new System.Windows.Forms.Label();
		this.Label62 = new System.Windows.Forms.Label();
		this.txtJumperUdelLoss = new System.Windows.Forms.TextBox();
		this.Label63 = new System.Windows.Forms.Label();
		this.Label64 = new System.Windows.Forms.Label();
		this.cbJUMPER = new System.Windows.Forms.ComboBox();
		this.chkLossSocket = new System.Windows.Forms.CheckBox();
		this.Label48 = new System.Windows.Forms.Label();
		this.txtLossSocketOne = new System.Windows.Forms.TextBox();
		this.Label49 = new System.Windows.Forms.Label();
		this.txtLengthCable = new System.Windows.Forms.TextBox();
		this.Label50 = new System.Windows.Forms.Label();
		this.Label58 = new System.Windows.Forms.Label();
		this.Label53 = new System.Windows.Forms.Label();
		this.txtLossSocketAll = new System.Windows.Forms.TextBox();
		this.txtCableLoss = new System.Windows.Forms.TextBox();
		this.Label52 = new System.Windows.Forms.Label();
		this.Label54 = new System.Windows.Forms.Label();
		this.Label55 = new System.Windows.Forms.Label();
		this.txtCableUdelLoss = new System.Windows.Forms.TextBox();
		this.Label56 = new System.Windows.Forms.Label();
		this.Label57 = new System.Windows.Forms.Label();
		this.cbCABLE = new System.Windows.Forms.ComboBox();
		this.cmdCANCEL = new System.Windows.Forms.Button();
		this.cmdOK = new System.Windows.Forms.Button();
		this.gbTEXT.SuspendLayout();
		base.SuspendLayout();
		this.gbTEXT.Controls.Add(this.Label65);
		this.gbTEXT.Controls.Add(this.txtExtraLoss);
		this.gbTEXT.Controls.Add(this.Label66);
		this.gbTEXT.Controls.Add(this.Label67);
		this.gbTEXT.Controls.Add(this.txtAftLoss);
		this.gbTEXT.Controls.Add(this.Label68);
		this.gbTEXT.Controls.Add(this.Label51);
		this.gbTEXT.Controls.Add(this.txtLengthJumper);
		this.gbTEXT.Controls.Add(this.Label59);
		this.gbTEXT.Controls.Add(this.Label60);
		this.gbTEXT.Controls.Add(this.txtJumperLoss);
		this.gbTEXT.Controls.Add(this.Label61);
		this.gbTEXT.Controls.Add(this.Label62);
		this.gbTEXT.Controls.Add(this.txtJumperUdelLoss);
		this.gbTEXT.Controls.Add(this.Label63);
		this.gbTEXT.Controls.Add(this.Label64);
		this.gbTEXT.Controls.Add(this.cbJUMPER);
		this.gbTEXT.Controls.Add(this.chkLossSocket);
		this.gbTEXT.Controls.Add(this.Label48);
		this.gbTEXT.Controls.Add(this.txtLossSocketOne);
		this.gbTEXT.Controls.Add(this.Label49);
		this.gbTEXT.Controls.Add(this.txtLengthCable);
		this.gbTEXT.Controls.Add(this.Label50);
		this.gbTEXT.Controls.Add(this.Label58);
		this.gbTEXT.Controls.Add(this.Label53);
		this.gbTEXT.Controls.Add(this.txtLossSocketAll);
		this.gbTEXT.Controls.Add(this.txtCableLoss);
		this.gbTEXT.Controls.Add(this.Label52);
		this.gbTEXT.Controls.Add(this.Label54);
		this.gbTEXT.Controls.Add(this.Label55);
		this.gbTEXT.Controls.Add(this.txtCableUdelLoss);
		this.gbTEXT.Controls.Add(this.Label56);
		this.gbTEXT.Controls.Add(this.Label57);
		this.gbTEXT.Controls.Add(this.cbCABLE);
		this.gbTEXT.Location = new System.Drawing.Point(8, 8);
		this.gbTEXT.Name = "gbTEXT";
		this.gbTEXT.Size = new System.Drawing.Size(632, 222);
		this.gbTEXT.TabIndex = 5;
		this.gbTEXT.TabStop = false;
		this.gbTEXT.Text = "Radiating source feeder path parameters at the transmitter frequency";
		this.Label65.AutoSize = true;
		this.Label65.Location = new System.Drawing.Point(281, 191);
		this.Label65.Name = "Label65";
		this.Label65.Size = new System.Drawing.Size(20, 13);
		this.Label65.TabIndex = 50;
		this.Label65.Text = "dB";
		this.txtExtraLoss.BackColor = System.Drawing.SystemColors.Window;
		this.txtExtraLoss.Location = new System.Drawing.Point(205, 188);
		this.txtExtraLoss.Name = "txtExtraLoss";
		this.txtExtraLoss.Size = new System.Drawing.Size(70, 20);
		this.txtExtraLoss.TabIndex = 49;
		this.txtExtraLoss.Text = "0";
		this.txtExtraLoss.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.Label66.AutoSize = true;
		this.Label66.Location = new System.Drawing.Point(11, 190);
		this.Label66.Name = "Label66";
		this.Label66.Size = new System.Drawing.Size(109, 13);
		this.Label66.TabIndex = 48;
		this.Label66.Text = "Additional attenuation";
		this.Label67.AutoSize = true;
		this.Label67.Location = new System.Drawing.Point(568, 191);
		this.Label67.Name = "Label67";
		this.Label67.Size = new System.Drawing.Size(20, 13);
		this.Label67.TabIndex = 47;
		this.Label67.Text = "dB";
		this.txtAftLoss.BackColor = System.Drawing.SystemColors.Menu;
		this.txtAftLoss.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
		this.txtAftLoss.Location = new System.Drawing.Point(511, 187);
		this.txtAftLoss.Name = "txtAftLoss";
		this.txtAftLoss.Size = new System.Drawing.Size(53, 20);
		this.txtAftLoss.TabIndex = 46;
		this.txtAftLoss.Text = "123";
		this.txtAftLoss.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.Label68.AutoSize = true;
		this.Label68.Location = new System.Drawing.Point(322, 191);
		this.Label68.Name = "Label68";
		this.Label68.Size = new System.Drawing.Size(87, 13);
		this.Label68.TabIndex = 45;
		this.Label68.Text = "Total attenuation";
		this.Label51.AutoSize = true;
		this.Label51.Location = new System.Drawing.Point(281, 125);
		this.Label51.Name = "Label51";
		this.Label51.Size = new System.Drawing.Size(15, 13);
		this.Label51.TabIndex = 44;
		this.Label51.Text = "m";
		this.txtLengthJumper.BackColor = System.Drawing.SystemColors.Window;
		this.txtLengthJumper.Location = new System.Drawing.Point(205, 122);
		this.txtLengthJumper.Name = "txtLengthJumper";
		this.txtLengthJumper.Size = new System.Drawing.Size(70, 20);
		this.txtLengthJumper.TabIndex = 43;
		this.txtLengthJumper.Text = "3+3";
		this.txtLengthJumper.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.Label59.AutoSize = true;
		this.Label59.Location = new System.Drawing.Point(11, 124);
		this.Label59.Name = "Label59";
		this.Label59.Size = new System.Drawing.Size(150, 13);
		this.Label59.TabIndex = 42;
		this.Label59.Text = "Length and number of jumpers";
		this.Label60.AutoSize = true;
		this.Label60.Location = new System.Drawing.Point(568, 125);
		this.Label60.Name = "Label60";
		this.Label60.Size = new System.Drawing.Size(20, 13);
		this.Label60.TabIndex = 41;
		this.Label60.Text = "dB";
		this.txtJumperLoss.BackColor = System.Drawing.SystemColors.Menu;
		this.txtJumperLoss.Location = new System.Drawing.Point(511, 121);
		this.txtJumperLoss.Name = "txtJumperLoss";
		this.txtJumperLoss.Size = new System.Drawing.Size(53, 20);
		this.txtJumperLoss.TabIndex = 40;
		this.txtJumperLoss.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.Label61.AutoSize = true;
		this.Label61.Location = new System.Drawing.Point(322, 125);
		this.Label61.Name = "Label61";
		this.Label61.Size = new System.Drawing.Size(102, 13);
		this.Label61.TabIndex = 39;
		this.Label61.Text = "Jumpers attenuation";
		this.Label62.AutoSize = true;
		this.Label62.Location = new System.Drawing.Point(568, 92);
		this.Label62.Name = "Label62";
		this.Label62.Size = new System.Drawing.Size(51, 13);
		this.Label62.TabIndex = 38;
		this.Label62.Text = "dB/100m";
		this.txtJumperUdelLoss.BackColor = System.Drawing.SystemColors.Menu;
		this.txtJumperUdelLoss.Location = new System.Drawing.Point(511, 89);
		this.txtJumperUdelLoss.Name = "txtJumperUdelLoss";
		this.txtJumperUdelLoss.Size = new System.Drawing.Size(53, 20);
		this.txtJumperUdelLoss.TabIndex = 37;
		this.txtJumperUdelLoss.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.Label63.AutoSize = true;
		this.Label63.Location = new System.Drawing.Point(322, 92);
		this.Label63.Name = "Label63";
		this.Label63.Size = new System.Drawing.Size(144, 13);
		this.Label63.TabIndex = 36;
		this.Label63.Text = "Attenuation in jumper per unit";
		this.Label64.AutoSize = true;
		this.Label64.Location = new System.Drawing.Point(11, 91);
		this.Label64.Name = "Label64";
		this.Label64.Size = new System.Drawing.Size(64, 13);
		this.Label64.TabIndex = 35;
		this.Label64.Text = "Jumper type";
		this.cbJUMPER.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbJUMPER.FormattingEnabled = true;
		this.cbJUMPER.Location = new System.Drawing.Point(98, 88);
		this.cbJUMPER.Name = "cbJUMPER";
		this.cbJUMPER.Size = new System.Drawing.Size(177, 21);
		this.cbJUMPER.TabIndex = 34;
		this.chkLossSocket.AutoSize = true;
		this.chkLossSocket.Checked = true;
		this.chkLossSocket.CheckState = System.Windows.Forms.CheckState.Checked;
		this.chkLossSocket.Location = new System.Drawing.Point(16, 156);
		this.chkLossSocket.Name = "chkLossSocket";
		this.chkLossSocket.Size = new System.Drawing.Size(179, 17);
		this.chkLossSocket.TabIndex = 33;
		this.chkLossSocket.Text = "Consider connectors attenuation";
		this.chkLossSocket.UseVisualStyleBackColor = true;
		this.Label48.AutoSize = true;
		this.Label48.Location = new System.Drawing.Point(283, 157);
		this.Label48.Name = "Label48";
		this.Label48.Size = new System.Drawing.Size(20, 13);
		this.Label48.TabIndex = 32;
		this.Label48.Text = "dB";
		this.txtLossSocketOne.BackColor = System.Drawing.SystemColors.Window;
		this.txtLossSocketOne.Location = new System.Drawing.Point(205, 154);
		this.txtLossSocketOne.Name = "txtLossSocketOne";
		this.txtLossSocketOne.Size = new System.Drawing.Size(70, 20);
		this.txtLossSocketOne.TabIndex = 31;
		this.txtLossSocketOne.Text = "0.05";
		this.txtLossSocketOne.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.Label49.AutoSize = true;
		this.Label49.Location = new System.Drawing.Point(281, 60);
		this.Label49.Name = "Label49";
		this.Label49.Size = new System.Drawing.Size(15, 13);
		this.Label49.TabIndex = 29;
		this.Label49.Text = "m";
		this.txtLengthCable.BackColor = System.Drawing.SystemColors.Window;
		this.txtLengthCable.Location = new System.Drawing.Point(205, 57);
		this.txtLengthCable.Name = "txtLengthCable";
		this.txtLengthCable.Size = new System.Drawing.Size(70, 20);
		this.txtLengthCable.TabIndex = 28;
		this.txtLengthCable.Text = "0";
		this.txtLengthCable.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.Label50.AutoSize = true;
		this.Label50.Location = new System.Drawing.Point(11, 59);
		this.Label50.Name = "Label50";
		this.Label50.Size = new System.Drawing.Size(40, 13);
		this.Label50.TabIndex = 27;
		this.Label50.Text = "Length";
		this.Label58.AutoSize = true;
		this.Label58.Location = new System.Drawing.Point(570, 158);
		this.Label58.Name = "Label58";
		this.Label58.Size = new System.Drawing.Size(20, 13);
		this.Label58.TabIndex = 22;
		this.Label58.Text = "dB";
		this.Label53.AutoSize = true;
		this.Label53.Location = new System.Drawing.Point(568, 60);
		this.Label53.Name = "Label53";
		this.Label53.Size = new System.Drawing.Size(20, 13);
		this.Label53.TabIndex = 22;
		this.Label53.Text = "dB";
		this.txtLossSocketAll.BackColor = System.Drawing.SystemColors.Menu;
		this.txtLossSocketAll.Location = new System.Drawing.Point(511, 155);
		this.txtLossSocketAll.Name = "txtLossSocketAll";
		this.txtLossSocketAll.Size = new System.Drawing.Size(53, 20);
		this.txtLossSocketAll.TabIndex = 21;
		this.txtLossSocketAll.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.txtCableLoss.BackColor = System.Drawing.SystemColors.Menu;
		this.txtCableLoss.Location = new System.Drawing.Point(511, 56);
		this.txtCableLoss.Name = "txtCableLoss";
		this.txtCableLoss.Size = new System.Drawing.Size(53, 20);
		this.txtCableLoss.TabIndex = 21;
		this.txtCableLoss.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.Label52.AutoSize = true;
		this.Label52.Location = new System.Drawing.Point(322, 158);
		this.Label52.Name = "Label52";
		this.Label52.Size = new System.Drawing.Size(117, 13);
		this.Label52.TabIndex = 20;
		this.Label52.Text = "Connectors attenuation";
		this.Label54.AutoSize = true;
		this.Label54.Location = new System.Drawing.Point(322, 60);
		this.Label54.Name = "Label54";
		this.Label54.Size = new System.Drawing.Size(96, 13);
		this.Label54.TabIndex = 20;
		this.Label54.Text = "Feeder attenuation";
		this.Label55.AutoSize = true;
		this.Label55.Location = new System.Drawing.Point(568, 27);
		this.Label55.Name = "Label55";
		this.Label55.Size = new System.Drawing.Size(51, 13);
		this.Label55.TabIndex = 19;
		this.Label55.Text = "dB/100m";
		this.txtCableUdelLoss.BackColor = System.Drawing.SystemColors.Menu;
		this.txtCableUdelLoss.Location = new System.Drawing.Point(511, 24);
		this.txtCableUdelLoss.Name = "txtCableUdelLoss";
		this.txtCableUdelLoss.Size = new System.Drawing.Size(53, 20);
		this.txtCableUdelLoss.TabIndex = 18;
		this.txtCableUdelLoss.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.Label56.AutoSize = true;
		this.Label56.Location = new System.Drawing.Point(322, 27);
		this.Label56.Name = "Label56";
		this.Label56.Size = new System.Drawing.Size(143, 13);
		this.Label56.TabIndex = 17;
		this.Label56.Text = "Attenuation in feeder per unit";
		this.Label57.AutoSize = true;
		this.Label57.Location = new System.Drawing.Point(11, 26);
		this.Label57.Name = "Label57";
		this.Label57.Size = new System.Drawing.Size(63, 13);
		this.Label57.TabIndex = 3;
		this.Label57.Text = "Feeder type";
		this.cbCABLE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cbCABLE.FormattingEnabled = true;
		this.cbCABLE.Location = new System.Drawing.Point(98, 23);
		this.cbCABLE.Name = "cbCABLE";
		this.cbCABLE.Size = new System.Drawing.Size(177, 21);
		this.cbCABLE.TabIndex = 2;
		this.cmdCANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cmdCANCEL.Location = new System.Drawing.Point(458, 240);
		this.cmdCANCEL.Name = "cmdCANCEL";
		this.cmdCANCEL.Size = new System.Drawing.Size(88, 26);
		this.cmdCANCEL.TabIndex = 4;
		this.cmdCANCEL.Text = "Cancel";
		this.cmdCANCEL.UseVisualStyleBackColor = true;
		this.cmdOK.Location = new System.Drawing.Point(552, 240);
		this.cmdOK.Name = "cmdOK";
		this.cmdOK.Size = new System.Drawing.Size(88, 26);
		this.cmdOK.TabIndex = 3;
		this.cmdOK.Text = "OK";
		this.cmdOK.UseVisualStyleBackColor = true;
		base.AcceptButton = this.cmdOK;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.cmdCANCEL;
		base.ClientSize = new System.Drawing.Size(651, 276);
		base.Controls.Add(this.gbTEXT);
		base.Controls.Add(this.cmdCANCEL);
		base.Controls.Add(this.cmdOK);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmCableCalc";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		this.Text = "Radiating source feeder path parameters:";
		this.gbTEXT.ResumeLayout(false);
		this.gbTEXT.PerformLayout();
		base.ResumeLayout(false);
	}

	private void frmAFT_Load(object sender, EventArgs e)
	{
		cbCABLE.Items.Clear();
		cbJUMPER.Items.Clear();
		int upperBound = Public1.FID.GetUpperBound(0);
		for (int i = 0; i <= upperBound; i = checked(i + 1))
		{
			cbCABLE.Items.Add(Public1.FID[i].Name);
			cbJUMPER.Items.Add(Public1.FID[i].Name);
		}
		cbCABLE.SelectedIndex = 7;
		cbJUMPER.SelectedIndex = 4;
		Form1_Public.CableLoss = -999f;
		Freq = Form1_Public.FreqSpanCalc(Public2.PRJ.TX[Form1_Public.IndexTX].F, Public2.PRJ.TX[Form1_Public.IndexTX].F2);
		Text = "Radiating source feeder path parameters: " + Public2.PRJ.TX[Form1_Public.IndexTX].Name;
		gbTEXT.Text = "Radiating source feeder path parameters at the transmitter frequency f= " + Conversions.ToString(Freq) + " MHz";
		if (!Information.IsNothing(Public2.PRJ.TX[Form1_Public.IndexTX].Cable) & !Information.IsNothing(Public2.PRJ.TX[Form1_Public.IndexTX].Jumper) & (Operators.CompareString(Public2.PRJ.TX[Form1_Public.IndexTX].Cable, "", TextCompare: false) != 0) & (Operators.CompareString(Public2.PRJ.TX[Form1_Public.IndexTX].Jumper, "", TextCompare: false) != 0))
		{
			cbCABLE.Text = Public2.PRJ.TX[Form1_Public.IndexTX].Cable;
			txtLengthCable.Text = Conversions.ToString(Public2.PRJ.TX[Form1_Public.IndexTX].LengthCable);
			cbJUMPER.Text = Public2.PRJ.TX[Form1_Public.IndexTX].Jumper;
			txtLengthJumper.Text = Public2.PRJ.TX[Form1_Public.IndexTX].LengthJumper;
			chkLossSocket.Checked = Public2.PRJ.TX[Form1_Public.IndexTX].ChkLossSocket;
			txtLossSocketOne.Text = Conversions.ToString(Public2.PRJ.TX[Form1_Public.IndexTX].LossSocketOne);
			txtExtraLoss.Text = Conversions.ToString(Public2.PRJ.TX[Form1_Public.IndexTX].ExtraLoss);
		}
		else
		{
			cbCABLE.SelectedIndex = 7;
			cbJUMPER.SelectedIndex = 4;
			txtLengthCable.Text = Conversions.ToString(0);
			txtLengthJumper.Text = "3+3";
			chkLossSocket.Checked = true;
			txtLossSocketOne.Text = Conversions.ToString(0.05);
			txtExtraLoss.Text = Conversions.ToString(0);
		}
		CalcLOSS();
	}

	public bool CalcLOSS()
	{
		bool result = true;
		txtCableUdelLoss.Text = "";
		txtCableLoss.Text = "";
		txtJumperUdelLoss.Text = "";
		txtJumperLoss.Text = "";
		txtLossSocketAll.Text = "";
		txtAftLoss.Text = "";
		checked
		{
			if (Operators.CompareString(cbCABLE.Text, "", TextCompare: false) != 0)
			{
				if ((Freq < Public1.FID[cbCABLE.SelectedIndex].Loss[0, 0]) | (Freq > Public1.FID[cbCABLE.SelectedIndex].Loss[0, Public1.FID[cbCABLE.SelectedIndex].Loss.GetUpperBound(1)]))
				{
					txtCableUdelLoss.Text = "вне диапазона";
				}
				else
				{
					int num = Public1.FID[cbCABLE.SelectedIndex].Loss.GetUpperBound(1) - 1;
					float num2 = default;
					for (int i = 0; i <= num; i++)
					{
						if ((Freq >= Public1.FID[cbCABLE.SelectedIndex].Loss[0, i]) & (Freq <= Public1.FID[cbCABLE.SelectedIndex].Loss[0, i + 1]))
						{
							num2 = (float)((double)Public1.FID[cbCABLE.SelectedIndex].Loss[1, i] + (Math.Log10(Freq) - Math.Log10(Public1.FID[cbCABLE.SelectedIndex].Loss[0, i])) * (double)(Public1.FID[cbCABLE.SelectedIndex].Loss[1, i + 1] - Public1.FID[cbCABLE.SelectedIndex].Loss[1, i]) / (Math.Log10(Public1.FID[cbCABLE.SelectedIndex].Loss[0, i + 1]) - Math.Log10(Public1.FID[cbCABLE.SelectedIndex].Loss[0, i])));
							num2 = (float)Math.Round(num2, 3);
							break;
						}
					}
					txtCableUdelLoss.Text = Conversions.ToString(num2);
					if (Versioned.IsNumeric(txtLengthCable.Text) && !(Conversions.ToSingle(txtLengthCable.Text) < 0f))
					{
						float num3 = (float)Math.Round(Conversions.ToSingle(txtLengthCable.Text) * num2 / 100f, 3);
						txtCableLoss.Text = Conversions.ToString(num3);
						if (Operators.CompareString(cbJUMPER.Text, "", TextCompare: false) != 0)
						{
							if ((Freq < Public1.FID[cbJUMPER.SelectedIndex].Loss[0, 0]) | (Freq > Public1.FID[cbJUMPER.SelectedIndex].Loss[0, Public1.FID[cbJUMPER.SelectedIndex].Loss.GetUpperBound(1)]))
							{
								txtJumperUdelLoss.Text = "вне диапазона";
							}
							else
							{
								int num4 = Public1.FID[cbJUMPER.SelectedIndex].Loss.GetUpperBound(1) - 1;
								float num5 = default;
								for (int j = 0; j <= num4; j++)
								{
									if ((Freq >= Public1.FID[cbJUMPER.SelectedIndex].Loss[0, j]) & (Freq <= Public1.FID[cbJUMPER.SelectedIndex].Loss[0, j + 1]))
									{
										num5 = (float)((double)Public1.FID[cbJUMPER.SelectedIndex].Loss[1, j] + (Math.Log10(Freq) - Math.Log10(Public1.FID[cbJUMPER.SelectedIndex].Loss[0, j])) * (double)(Public1.FID[cbJUMPER.SelectedIndex].Loss[1, j + 1] - Public1.FID[cbJUMPER.SelectedIndex].Loss[1, j]) / (Math.Log10(Public1.FID[cbJUMPER.SelectedIndex].Loss[0, j + 1]) - Math.Log10(Public1.FID[cbJUMPER.SelectedIndex].Loss[0, j])));
										num5 = (float)Math.Round(num5, 3);
										break;
									}
								}
								txtJumperUdelLoss.Text = Conversions.ToString(num5);
								string[] array = txtLengthJumper.Text.Split('+');
								int upperBound = array.GetUpperBound(0);
								int num6 = 0;
								while (true)
								{
									if (num6 <= upperBound)
									{
										if (!Versioned.IsNumeric(array[num6]) || Conversions.ToSingle(array[num6]) < 0f)
										{
											break;
										}
										num6++;
										continue;
									}
									float num7 = 0f;
									int upperBound2 = array.GetUpperBound(0);
									for (int k = 0; k <= upperBound2; k++)
									{
										num7 += Conversions.ToSingle(array[k]);
									}
									float num8 = (float)Math.Round(num7 * num5 / 100f, 3);
									txtJumperLoss.Text = Conversions.ToString(num8);
									float num11;
									if (chkLossSocket.Checked)
									{
										if (!Versioned.IsNumeric(txtLossSocketOne.Text) || Conversions.ToSingle(txtLossSocketOne.Text) < 0f)
										{
											break;
										}
										float num9 = Conversions.ToSingle(txtLossSocketOne.Text);
										int num10 = 2;
										if (!((array.GetUpperBound(0) == 0) & (Conversions.ToDouble(array[0]) == 0.0)))
										{
											num10 += (array.GetUpperBound(0) + 1) * 2;
										}
										num11 = num9 * (float)num10;
										txtLossSocketAll.Text = Conversions.ToString(num11);
									}
									else
									{
										num11 = 0f;
										txtLossSocketAll.Text = Conversions.ToString(0);
									}
									if (Versioned.IsNumeric(txtExtraLoss.Text) && !(Conversions.ToSingle(txtExtraLoss.Text) < 0f))
									{
										float num12 = Conversions.ToSingle(txtExtraLoss.Text);
										float num13 = num3 + num8 + num11 + num12;
										txtAftLoss.Text = Conversions.ToString(Math.Round(num13, 3));
										result = false;
									}
									break;
								}
							}
						}
					}
				}
			}
			return result;
		}
	}

	private void cbCABLE_SelectionChangeCommitted(object sender, EventArgs e)
	{
		CalcLOSS();
	}

	private void cbJUMPER_SelectionChangeCommitted(object sender, EventArgs e)
	{
		CalcLOSS();
	}

	private void TXT_TextChanged(object sender, EventArgs e)
	{
		CalcLOSS();
	}

	private void chkLossSocket_CheckedChanged(object sender, EventArgs e)
	{
		CalcLOSS();
	}

	private void cmdCANCEL_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void cmdOK_Click(object sender, EventArgs e)
	{
		if (CalcLOSS())
		{
			Interaction.MsgBox(Localization.LocMSG(105));
			return;
		}
		Public2.PRJ.TX[Form1_Public.IndexTX].Cable = cbCABLE.Text;
		Public2.PRJ.TX[Form1_Public.IndexTX].LengthCable = Conversions.ToSingle(txtLengthCable.Text);
		Public2.PRJ.TX[Form1_Public.IndexTX].Jumper = cbJUMPER.Text;
		Public2.PRJ.TX[Form1_Public.IndexTX].LengthJumper = txtLengthJumper.Text;
		Public2.PRJ.TX[Form1_Public.IndexTX].ChkLossSocket = chkLossSocket.Checked;
		Public2.PRJ.TX[Form1_Public.IndexTX].LossSocketOne = Conversions.ToSingle(txtLossSocketOne.Text);
		Public2.PRJ.TX[Form1_Public.IndexTX].ExtraLoss = Conversions.ToSingle(txtExtraLoss.Text);
		Form1_Public.CableLoss = Conversions.ToSingle(txtAftLoss.Text);
		Close();
	}
}
