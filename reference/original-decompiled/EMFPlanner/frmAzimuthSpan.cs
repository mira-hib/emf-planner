using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[DesignerGenerated]
public class frmAzimuthSpan : Form
{
	private IContainer components;

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

	[field: AccessedThroughProperty("GroupBox1")]
	internal virtual GroupBox GroupBox1
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

	[field: AccessedThroughProperty("Label5")]
	internal virtual Label Label5
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtDH")]
	internal virtual TextBox txtDH
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			CancelEventHandler value2 = txtDH_Validating;
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

	[field: AccessedThroughProperty("Label3")]
	internal virtual Label Label3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtHmax")]
	internal virtual TextBox txtHmax
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			CancelEventHandler value2 = txtHmax_Validating;
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

	[field: AccessedThroughProperty("Label65")]
	internal virtual Label Label65
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtHmin")]
	internal virtual TextBox txtHmin
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			CancelEventHandler value2 = txtHmin_Validating;
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

	public frmAzimuthSpan()
	{
		Load += frmHeightSpan_Load;
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
		this.cmdCANCEL = new System.Windows.Forms.Button();
		this.cmdOK = new System.Windows.Forms.Button();
		this.GroupBox1 = new System.Windows.Forms.GroupBox();
		this.Label4 = new System.Windows.Forms.Label();
		this.Label5 = new System.Windows.Forms.Label();
		this.txtDH = new System.Windows.Forms.TextBox();
		this.Label2 = new System.Windows.Forms.Label();
		this.Label3 = new System.Windows.Forms.Label();
		this.txtHmax = new System.Windows.Forms.TextBox();
		this.Label1 = new System.Windows.Forms.Label();
		this.Label65 = new System.Windows.Forms.Label();
		this.txtHmin = new System.Windows.Forms.TextBox();
		this.GroupBox1.SuspendLayout();
		base.SuspendLayout();
		this.cmdCANCEL.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdCANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cmdCANCEL.Location = new System.Drawing.Point(152, 154);
		this.cmdCANCEL.Name = "cmdCANCEL";
		this.cmdCANCEL.Size = new System.Drawing.Size(88, 26);
		this.cmdCANCEL.TabIndex = 12;
		this.cmdCANCEL.TabStop = false;
		this.cmdCANCEL.Text = "Cancel";
		this.cmdCANCEL.UseVisualStyleBackColor = true;
		this.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdOK.Location = new System.Drawing.Point(246, 154);
		this.cmdOK.Name = "cmdOK";
		this.cmdOK.Size = new System.Drawing.Size(88, 26);
		this.cmdOK.TabIndex = 11;
		this.cmdOK.TabStop = false;
		this.cmdOK.Text = "OK";
		this.cmdOK.UseVisualStyleBackColor = true;
		this.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GroupBox1.Controls.Add(this.Label4);
		this.GroupBox1.Controls.Add(this.Label5);
		this.GroupBox1.Controls.Add(this.txtDH);
		this.GroupBox1.Controls.Add(this.Label2);
		this.GroupBox1.Controls.Add(this.Label3);
		this.GroupBox1.Controls.Add(this.txtHmax);
		this.GroupBox1.Controls.Add(this.Label1);
		this.GroupBox1.Controls.Add(this.Label65);
		this.GroupBox1.Controls.Add(this.txtHmin);
		this.GroupBox1.Location = new System.Drawing.Point(5, 9);
		this.GroupBox1.Name = "GroupBox1";
		this.GroupBox1.Size = new System.Drawing.Size(330, 127);
		this.GroupBox1.TabIndex = 10;
		this.GroupBox1.TabStop = false;
		this.Label4.AutoSize = true;
		this.Label4.Location = new System.Drawing.Point(228, 89);
		this.Label4.Name = "Label4";
		this.Label4.Size = new System.Drawing.Size(28, 13);
		this.Label4.TabIndex = 60;
		this.Label4.Text = "deg.";
		this.Label5.AutoSize = true;
		this.Label5.Location = new System.Drawing.Point(96, 89);
		this.Label5.Name = "Label5";
		this.Label5.Size = new System.Drawing.Size(67, 13);
		this.Label5.TabIndex = 59;
		this.Label5.Text = "Azimuth step";
		this.txtDH.BackColor = System.Drawing.SystemColors.Window;
		this.txtDH.Location = new System.Drawing.Point(176, 86);
		this.txtDH.Name = "txtDH";
		this.txtDH.Size = new System.Drawing.Size(46, 20);
		this.txtDH.TabIndex = 58;
		this.txtDH.TabStop = false;
		this.txtDH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.Label2.AutoSize = true;
		this.Label2.Location = new System.Drawing.Point(228, 58);
		this.Label2.Name = "Label2";
		this.Label2.Size = new System.Drawing.Size(28, 13);
		this.Label2.TabIndex = 57;
		this.Label2.Text = "deg.";
		this.Label3.AutoSize = true;
		this.Label3.Location = new System.Drawing.Point(73, 58);
		this.Label3.Name = "Label3";
		this.Label3.Size = new System.Drawing.Size(90, 13);
		this.Label3.TabIndex = 56;
		this.Label3.Text = "Maximum azimuth";
		this.txtHmax.BackColor = System.Drawing.SystemColors.Window;
		this.txtHmax.Location = new System.Drawing.Point(176, 55);
		this.txtHmax.Name = "txtHmax";
		this.txtHmax.Size = new System.Drawing.Size(46, 20);
		this.txtHmax.TabIndex = 55;
		this.txtHmax.TabStop = false;
		this.txtHmax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.Label1.AutoSize = true;
		this.Label1.Location = new System.Drawing.Point(228, 27);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(28, 13);
		this.Label1.TabIndex = 54;
		this.Label1.Text = "deg.";
		this.Label65.AutoSize = true;
		this.Label65.Location = new System.Drawing.Point(76, 27);
		this.Label65.Name = "Label65";
		this.Label65.Size = new System.Drawing.Size(87, 13);
		this.Label65.TabIndex = 53;
		this.Label65.Text = "Minimum azimuth";
		this.txtHmin.BackColor = System.Drawing.SystemColors.Window;
		this.txtHmin.Location = new System.Drawing.Point(176, 24);
		this.txtHmin.Name = "txtHmin";
		this.txtHmin.Size = new System.Drawing.Size(46, 20);
		this.txtHmin.TabIndex = 52;
		this.txtHmin.TabStop = false;
		this.txtHmin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		base.AcceptButton = this.cmdOK;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.cmdCANCEL;
		base.ClientSize = new System.Drawing.Size(341, 188);
		base.Controls.Add(this.cmdCANCEL);
		base.Controls.Add(this.cmdOK);
		base.Controls.Add(this.GroupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmAzimuthSpan";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Selecting Additional Vertical Cuts";
		this.GroupBox1.ResumeLayout(false);
		this.GroupBox1.PerformLayout();
		base.ResumeLayout(false);
	}

	private void frmHeightSpan_Load(object sender, EventArgs e)
	{
		txtHmin.Text = Conversions.ToString(Form1_Public.AzimuthSpanMin);
		txtHmax.Text = Conversions.ToString(Form1_Public.AzimuthSpanMax);
		txtDH.Text = Conversions.ToString(Form1_Public.AzimuthSpanDh);
		Form1_Public.AzimuthSpanOK = false;
	}

	private void cmdCANCEL_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void cmdOK_Click(object sender, EventArgs e)
	{
		Form1_Public.AzimuthSpanOK = true;
		Close();
	}

	private void txtHmin_Validating(object sender, CancelEventArgs e)
	{
		TextBox textBox = txtHmin;
		string TextProperty = textBox.Text;
		double DblVariable = Form1_Public.AzimuthSpanMin;
		Module1.TextPropertyValidating(ref TextProperty, ref DblVariable, CheckLimits: true, 0f, 360f, IncludeMin: true, IncludeMax: false, Message: true);
		Form1_Public.AzimuthSpanMin = (float)DblVariable;
		textBox.Text = TextProperty;
	}

	private void txtHmax_Validating(object sender, CancelEventArgs e)
	{
		TextBox textBox = txtHmax;
		string TextProperty = textBox.Text;
		double DblVariable = Form1_Public.AzimuthSpanMax;
		Module1.TextPropertyValidating(ref TextProperty, ref DblVariable, CheckLimits: true, 0f, 360f, IncludeMin: false, IncludeMax: true, Message: true);
		Form1_Public.AzimuthSpanMax = (float)DblVariable;
		textBox.Text = TextProperty;
	}

	private void txtDH_Validating(object sender, CancelEventArgs e)
	{
		TextBox textBox = txtDH;
		string TextProperty = textBox.Text;
		double DblVariable = Form1_Public.AzimuthSpanDh;
		Module1.TextPropertyValidating(ref TextProperty, ref DblVariable, CheckLimits: true, 0f, 90f, IncludeMin: false, IncludeMax: false, Message: true);
		Form1_Public.AzimuthSpanDh = (float)DblVariable;
		textBox.Text = TextProperty;
	}
}
