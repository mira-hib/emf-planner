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
public class frmDBW : Form
{
	private IContainer components;

	private double P_W;

	private double P_mW;

	private double P_dBW;

	private double P_dBm;

	[field: AccessedThroughProperty("GroupBox1")]
	internal virtual GroupBox GroupBox1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
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

	[field: AccessedThroughProperty("Label3")]
	internal virtual Label Label3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtP_dBm")]
	internal virtual TextBox txtP_dBm
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			KeyEventHandler value2 = txtP_dBm_KeyUp;
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

	[field: AccessedThroughProperty("Label2")]
	internal virtual Label Label2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtP_dBW")]
	internal virtual TextBox txtP_dBW
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			KeyEventHandler value2 = txtP_dBW_KeyUp;
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

	[field: AccessedThroughProperty("Label1")]
	internal virtual Label Label1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtP_mW")]
	internal virtual TextBox txtP_mW
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			KeyEventHandler value2 = txtP_mW_KeyUp;
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

	[field: AccessedThroughProperty("Label65")]
	internal virtual Label Label65
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtP_W")]
	internal virtual TextBox txtP_W
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			KeyEventHandler value2 = txtW_KeyUp;
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

	public frmDBW()
	{
		Activated += frmDBW_Activated;
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
		this.GroupBox1 = new System.Windows.Forms.GroupBox();
		this.Label3 = new System.Windows.Forms.Label();
		this.txtP_dBm = new System.Windows.Forms.TextBox();
		this.Label2 = new System.Windows.Forms.Label();
		this.txtP_dBW = new System.Windows.Forms.TextBox();
		this.Label1 = new System.Windows.Forms.Label();
		this.txtP_mW = new System.Windows.Forms.TextBox();
		this.Label65 = new System.Windows.Forms.Label();
		this.txtP_W = new System.Windows.Forms.TextBox();
		this.cmdCANCEL = new System.Windows.Forms.Button();
		this.cmdOK = new System.Windows.Forms.Button();
		this.GroupBox1.SuspendLayout();
		base.SuspendLayout();
		this.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GroupBox1.Controls.Add(this.Label3);
		this.GroupBox1.Controls.Add(this.txtP_dBm);
		this.GroupBox1.Controls.Add(this.Label2);
		this.GroupBox1.Controls.Add(this.txtP_dBW);
		this.GroupBox1.Controls.Add(this.Label1);
		this.GroupBox1.Controls.Add(this.txtP_mW);
		this.GroupBox1.Controls.Add(this.Label65);
		this.GroupBox1.Controls.Add(this.txtP_W);
		this.GroupBox1.Location = new System.Drawing.Point(2, 2);
		this.GroupBox1.Name = "GroupBox1";
		this.GroupBox1.Size = new System.Drawing.Size(193, 140);
		this.GroupBox1.TabIndex = 0;
		this.GroupBox1.TabStop = false;
		this.Label3.AutoSize = true;
		this.Label3.Location = new System.Drawing.Point(120, 104);
		this.Label3.Name = "Label3";
		this.Label3.Size = new System.Drawing.Size(28, 13);
		this.Label3.TabIndex = 59;
		this.Label3.Text = "dBm";
		this.txtP_dBm.BackColor = System.Drawing.SystemColors.Window;
		this.txtP_dBm.Location = new System.Drawing.Point(44, 101);
		this.txtP_dBm.Name = "txtP_dBm";
		this.txtP_dBm.Size = new System.Drawing.Size(70, 20);
		this.txtP_dBm.TabIndex = 58;
		this.txtP_dBm.TabStop = false;
		this.txtP_dBm.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.Label2.AutoSize = true;
		this.Label2.Location = new System.Drawing.Point(120, 78);
		this.Label2.Name = "Label2";
		this.Label2.Size = new System.Drawing.Size(31, 13);
		this.Label2.TabIndex = 57;
		this.Label2.Text = "dBW";
		this.txtP_dBW.BackColor = System.Drawing.SystemColors.Window;
		this.txtP_dBW.Location = new System.Drawing.Point(44, 75);
		this.txtP_dBW.Name = "txtP_dBW";
		this.txtP_dBW.Size = new System.Drawing.Size(70, 20);
		this.txtP_dBW.TabIndex = 56;
		this.txtP_dBW.TabStop = false;
		this.txtP_dBW.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.Label1.AutoSize = true;
		this.Label1.Location = new System.Drawing.Point(120, 52);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(26, 13);
		this.Label1.TabIndex = 55;
		this.Label1.Text = "mW";
		this.txtP_mW.BackColor = System.Drawing.SystemColors.Window;
		this.txtP_mW.Location = new System.Drawing.Point(44, 49);
		this.txtP_mW.Name = "txtP_mW";
		this.txtP_mW.Size = new System.Drawing.Size(70, 20);
		this.txtP_mW.TabIndex = 54;
		this.txtP_mW.TabStop = false;
		this.txtP_mW.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.Label65.AutoSize = true;
		this.Label65.Location = new System.Drawing.Point(120, 26);
		this.Label65.Name = "Label65";
		this.Label65.Size = new System.Drawing.Size(18, 13);
		this.Label65.TabIndex = 53;
		this.Label65.Text = "W";
		this.txtP_W.BackColor = System.Drawing.SystemColors.Window;
		this.txtP_W.Location = new System.Drawing.Point(44, 23);
		this.txtP_W.Name = "txtP_W";
		this.txtP_W.Size = new System.Drawing.Size(70, 20);
		this.txtP_W.TabIndex = 52;
		this.txtP_W.TabStop = false;
		this.txtP_W.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.cmdCANCEL.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdCANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cmdCANCEL.Location = new System.Drawing.Point(8, 152);
		this.cmdCANCEL.Name = "cmdCANCEL";
		this.cmdCANCEL.Size = new System.Drawing.Size(88, 26);
		this.cmdCANCEL.TabIndex = 6;
		this.cmdCANCEL.TabStop = false;
		this.cmdCANCEL.Text = "Cancel";
		this.cmdCANCEL.UseVisualStyleBackColor = true;
		this.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdOK.Location = new System.Drawing.Point(102, 152);
		this.cmdOK.Name = "cmdOK";
		this.cmdOK.Size = new System.Drawing.Size(88, 26);
		this.cmdOK.TabIndex = 5;
		this.cmdOK.TabStop = false;
		this.cmdOK.Text = "OK";
		this.cmdOK.UseVisualStyleBackColor = true;
		base.AcceptButton = this.cmdOK;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.cmdCANCEL;
		base.ClientSize = new System.Drawing.Size(196, 186);
		base.Controls.Add(this.cmdCANCEL);
		base.Controls.Add(this.cmdOK);
		base.Controls.Add(this.GroupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmDBW";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		this.Text = "Unit conversion";
		this.GroupBox1.ResumeLayout(false);
		this.GroupBox1.PerformLayout();
		base.ResumeLayout(false);
	}

	private void frmDBW_Activated(object sender, EventArgs e)
	{
		txtP_mW.Text = "";
		txtP_dBW.Text = "";
		txtP_dBm.Text = "";
		if (Versioned.IsNumeric(txtP_W.Text) && !(Conversions.ToDouble(txtP_W.Text) <= 0.0))
		{
			P_W = Conversions.ToDouble(txtP_W.Text);
			P_mW = P_W * 1000.0;
			P_dBW = 10.0 * Math.Log10(P_W);
			P_dBm = P_dBW + 30.0;
			txtP_mW.Text = Conversions.ToString(Math.Round(P_mW, 1));
			txtP_dBW.Text = Conversions.ToString(Math.Round(P_dBW, 1));
			txtP_dBm.Text = Conversions.ToString(Math.Round(P_dBm, 1));
		}
	}

	private void cmdOK_Click(object sender, EventArgs e)
	{
		if (!Versioned.IsNumeric(txtP_W.Text))
		{
			Interaction.MsgBox(Localization.LocMSG(105));
			return;
		}
		if (Conversions.ToDouble(txtP_W.Text) <= 0.0)
		{
			Interaction.MsgBox(Localization.LocMSG(105));
			return;
		}
		Form1_Public.PowerW = (float)Math.Round(Conversions.ToDouble(txtP_W.Text), 4);
		Close();
	}

	private void cmdCANCEL_Click(object sender, EventArgs e)
	{
		Form1_Public.PowerW = 0f;
	}

	private void txtW_KeyUp(object sender, EventArgs e)
	{
		txtP_mW.Text = "";
		txtP_dBW.Text = "";
		txtP_dBm.Text = "";
		if (Versioned.IsNumeric(txtP_W.Text) && !(Conversions.ToDouble(txtP_W.Text) <= 0.0))
		{
			P_W = Conversions.ToDouble(txtP_W.Text);
			P_mW = P_W * 1000.0;
			P_dBW = 10.0 * Math.Log10(P_W);
			P_dBm = P_dBW + 30.0;
			txtP_mW.Text = Conversions.ToString(Math.Round(P_mW, 1));
			txtP_dBW.Text = Conversions.ToString(Math.Round(P_dBW, 1));
			txtP_dBm.Text = Conversions.ToString(Math.Round(P_dBm, 1));
		}
	}

	private void txtP_mW_KeyUp(object sender, EventArgs e)
	{
		txtP_W.Text = "";
		txtP_dBW.Text = "";
		txtP_dBm.Text = "";
		if (Versioned.IsNumeric(txtP_mW.Text) && !(Conversions.ToDouble(txtP_mW.Text) <= 0.0))
		{
			P_mW = Conversions.ToDouble(txtP_mW.Text);
			P_W = P_mW / 1000.0;
			P_dBW = 10.0 * Math.Log10(P_W);
			P_dBm = P_dBW + 30.0;
			txtP_W.Text = Conversions.ToString(Math.Round(P_W, 4));
			txtP_dBW.Text = Conversions.ToString(Math.Round(P_dBW, 1));
			txtP_dBm.Text = Conversions.ToString(Math.Round(P_dBm, 1));
		}
	}

	private void txtP_dBW_KeyUp(object sender, KeyEventArgs e)
	{
		txtP_W.Text = "";
		txtP_mW.Text = "";
		txtP_dBm.Text = "";
		if (Versioned.IsNumeric(txtP_dBW.Text))
		{
			P_dBW = Conversions.ToDouble(txtP_dBW.Text);
			P_W = Math.Pow(10.0, P_dBW / 10.0);
			P_mW = P_W * 1000.0;
			P_dBm = P_dBW + 30.0;
			txtP_W.Text = Conversions.ToString(Math.Round(P_W, 4));
			txtP_mW.Text = Conversions.ToString(Math.Round(P_mW, 1));
			txtP_dBm.Text = Conversions.ToString(Math.Round(P_dBm, 1));
		}
	}

	private void txtP_dBm_KeyUp(object sender, KeyEventArgs e)
	{
		txtP_W.Text = "";
		txtP_mW.Text = "";
		txtP_dBW.Text = "";
		if (Versioned.IsNumeric(txtP_dBm.Text))
		{
			P_dBm = Conversions.ToDouble(txtP_dBm.Text);
			P_dBW = P_dBm - 30.0;
			P_W = Math.Pow(10.0, P_dBW / 10.0);
			P_mW = P_W * 1000.0;
			txtP_W.Text = Conversions.ToString(Math.Round(P_W, 4));
			txtP_mW.Text = Conversions.ToString(Math.Round(P_mW, 1));
			txtP_dBW.Text = Conversions.ToString(Math.Round(P_dBW, 1));
		}
	}
}
