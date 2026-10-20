using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[DesignerGenerated]
public class frmAreaSize : Form
{
	private IContainer components;

	[field: AccessedThroughProperty("cmdCANCEL")]
	internal virtual Button cmdCANCEL
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
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

	[field: AccessedThroughProperty("Label3")]
	internal virtual Label Label3
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

	[field: AccessedThroughProperty("txtWidth2")]
	internal virtual TextBox txtWidth2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			CancelEventHandler value2 = txtWidth2_Validating;
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

	[field: AccessedThroughProperty("Label65")]
	internal virtual Label Label65
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtWidth1")]
	internal virtual TextBox txtWidth1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			CancelEventHandler value2 = txtWidth1_Validating;
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

	[field: AccessedThroughProperty("Label5")]
	internal virtual Label Label5
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

	[field: AccessedThroughProperty("txtHeight2")]
	internal virtual TextBox txtHeight2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			CancelEventHandler value2 = txtHeight2_Validating;
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

	[field: AccessedThroughProperty("Label7")]
	internal virtual Label Label7
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtHeight1")]
	internal virtual TextBox txtHeight1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			CancelEventHandler value2 = txtHeight1_Validating;
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

	public frmAreaSize()
	{
		Load += frmAreaSize_Load;
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
		this.Label6 = new System.Windows.Forms.Label();
		this.txtHeight2 = new System.Windows.Forms.TextBox();
		this.Label7 = new System.Windows.Forms.Label();
		this.txtHeight1 = new System.Windows.Forms.TextBox();
		this.Label2 = new System.Windows.Forms.Label();
		this.Label3 = new System.Windows.Forms.Label();
		this.Label1 = new System.Windows.Forms.Label();
		this.txtWidth2 = new System.Windows.Forms.TextBox();
		this.Label65 = new System.Windows.Forms.Label();
		this.txtWidth1 = new System.Windows.Forms.TextBox();
		this.GroupBox1.SuspendLayout();
		base.SuspendLayout();
		this.cmdCANCEL.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdCANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cmdCANCEL.Location = new System.Drawing.Point(136, 154);
		this.cmdCANCEL.Name = "cmdCANCEL";
		this.cmdCANCEL.Size = new System.Drawing.Size(88, 26);
		this.cmdCANCEL.TabIndex = 9;
		this.cmdCANCEL.TabStop = false;
		this.cmdCANCEL.Text = "Cancel";
		this.cmdCANCEL.UseVisualStyleBackColor = true;
		this.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdOK.Location = new System.Drawing.Point(230, 154);
		this.cmdOK.Name = "cmdOK";
		this.cmdOK.Size = new System.Drawing.Size(88, 26);
		this.cmdOK.TabIndex = 8;
		this.cmdOK.TabStop = false;
		this.cmdOK.Text = "OK";
		this.cmdOK.UseVisualStyleBackColor = true;
		this.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GroupBox1.Controls.Add(this.Label4);
		this.GroupBox1.Controls.Add(this.Label5);
		this.GroupBox1.Controls.Add(this.Label6);
		this.GroupBox1.Controls.Add(this.txtHeight2);
		this.GroupBox1.Controls.Add(this.Label7);
		this.GroupBox1.Controls.Add(this.txtHeight1);
		this.GroupBox1.Controls.Add(this.Label2);
		this.GroupBox1.Controls.Add(this.Label3);
		this.GroupBox1.Controls.Add(this.Label1);
		this.GroupBox1.Controls.Add(this.txtWidth2);
		this.GroupBox1.Controls.Add(this.Label65);
		this.GroupBox1.Controls.Add(this.txtWidth1);
		this.GroupBox1.Location = new System.Drawing.Point(7, 9);
		this.GroupBox1.Name = "GroupBox1";
		this.GroupBox1.Size = new System.Drawing.Size(311, 133);
		this.GroupBox1.TabIndex = 7;
		this.GroupBox1.TabStop = false;
		this.Label4.AutoSize = true;
		this.Label4.Location = new System.Drawing.Point(28, 102);
		this.Label4.Name = "Label4";
		this.Label4.Size = new System.Drawing.Size(189, 13);
		this.Label4.TabIndex = 64;
		this.Label4.Text = "The height of the area below the origin";
		this.Label5.AutoSize = true;
		this.Label5.Location = new System.Drawing.Point(286, 102);
		this.Label5.Name = "Label5";
		this.Label5.Size = new System.Drawing.Size(15, 13);
		this.Label5.TabIndex = 63;
		this.Label5.Text = "m";
		this.Label6.AutoSize = true;
		this.Label6.Location = new System.Drawing.Point(286, 76);
		this.Label6.Name = "Label6";
		this.Label6.Size = new System.Drawing.Size(15, 13);
		this.Label6.TabIndex = 62;
		this.Label6.Text = "m";
		this.txtHeight2.BackColor = System.Drawing.SystemColors.Window;
		this.txtHeight2.Location = new System.Drawing.Point(234, 99);
		this.txtHeight2.Name = "txtHeight2";
		this.txtHeight2.Size = new System.Drawing.Size(46, 20);
		this.txtHeight2.TabIndex = 61;
		this.txtHeight2.TabStop = false;
		this.txtHeight2.Text = "50";
		this.txtHeight2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.Label7.AutoSize = true;
		this.Label7.Location = new System.Drawing.Point(18, 76);
		this.Label7.Name = "Label7";
		this.Label7.Size = new System.Drawing.Size(199, 13);
		this.Label7.TabIndex = 60;
		this.Label7.Text = "The height of the region above the origin";
		this.txtHeight1.BackColor = System.Drawing.SystemColors.Window;
		this.txtHeight1.Location = new System.Drawing.Point(234, 73);
		this.txtHeight1.Name = "txtHeight1";
		this.txtHeight1.Size = new System.Drawing.Size(46, 20);
		this.txtHeight1.TabIndex = 59;
		this.txtHeight1.TabStop = false;
		this.txtHeight1.Text = "50";
		this.txtHeight1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.Label2.AutoSize = true;
		this.Label2.Location = new System.Drawing.Point(49, 50);
		this.Label2.Name = "Label2";
		this.Label2.Size = new System.Drawing.Size(168, 13);
		this.Label2.TabIndex = 58;
		this.Label2.Text = "Area width to the right of the origin";
		this.Label3.AutoSize = true;
		this.Label3.Location = new System.Drawing.Point(286, 50);
		this.Label3.Name = "Label3";
		this.Label3.Size = new System.Drawing.Size(15, 13);
		this.Label3.TabIndex = 57;
		this.Label3.Text = "m";
		this.Label1.AutoSize = true;
		this.Label1.Location = new System.Drawing.Point(286, 24);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(15, 13);
		this.Label1.TabIndex = 55;
		this.Label1.Text = "m";
		this.txtWidth2.BackColor = System.Drawing.SystemColors.Window;
		this.txtWidth2.Location = new System.Drawing.Point(234, 47);
		this.txtWidth2.Name = "txtWidth2";
		this.txtWidth2.Size = new System.Drawing.Size(46, 20);
		this.txtWidth2.TabIndex = 54;
		this.txtWidth2.TabStop = false;
		this.txtWidth2.Text = "50";
		this.txtWidth2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.Label65.AutoSize = true;
		this.Label65.Location = new System.Drawing.Point(43, 24);
		this.Label65.Name = "Label65";
		this.Label65.Size = new System.Drawing.Size(174, 13);
		this.Label65.TabIndex = 53;
		this.Label65.Text = "Region width to the left of the origin";
		this.txtWidth1.BackColor = System.Drawing.SystemColors.Window;
		this.txtWidth1.Location = new System.Drawing.Point(234, 21);
		this.txtWidth1.Name = "txtWidth1";
		this.txtWidth1.Size = new System.Drawing.Size(46, 20);
		this.txtWidth1.TabIndex = 52;
		this.txtWidth1.TabStop = false;
		this.txtWidth1.Text = "50";
		this.txtWidth1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		base.AcceptButton = this.cmdOK;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.cmdCANCEL;
		base.ClientSize = new System.Drawing.Size(330, 192);
		base.Controls.Add(this.cmdCANCEL);
		base.Controls.Add(this.cmdOK);
		base.Controls.Add(this.GroupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmAreaSize";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Calculation grid dimensions";
		this.GroupBox1.ResumeLayout(false);
		this.GroupBox1.PerformLayout();
		base.ResumeLayout(false);
	}

	private void frmAreaSize_Load(object sender, EventArgs e)
	{
		Form1_Public.AreaSizeOK = false;
		txtWidth1.Text = Conversions.ToString(Form1_Public.AreaWidth1);
		txtWidth2.Text = Conversions.ToString(Form1_Public.AreaWidth2);
		txtHeight1.Text = Conversions.ToString(Form1_Public.AreaHeight1);
		txtHeight2.Text = Conversions.ToString(Form1_Public.AreaHeight2);
	}

	private void txtWidth1_Validating(object sender, CancelEventArgs e)
	{
		TextBox textBox = txtWidth1;
		string TextProperty = textBox.Text;
		double DblVariable = Form1_Public.AreaWidth1;
		Module1.TextPropertyValidating(ref TextProperty, ref DblVariable, CheckLimits: true, 0f, 5000f, IncludeMin: true, IncludeMax: true, Message: true);
		Form1_Public.AreaWidth1 = (float)DblVariable;
		textBox.Text = TextProperty;
	}

	private void txtWidth2_Validating(object sender, CancelEventArgs e)
	{
		TextBox textBox = txtWidth2;
		string TextProperty = textBox.Text;
		double DblVariable = Form1_Public.AreaWidth2;
		Module1.TextPropertyValidating(ref TextProperty, ref DblVariable, CheckLimits: true, 10f, 5000f, IncludeMin: true, IncludeMax: true, Message: true);
		Form1_Public.AreaWidth2 = (float)DblVariable;
		textBox.Text = TextProperty;
	}

	private void txtHeight1_Validating(object sender, CancelEventArgs e)
	{
		TextBox textBox = txtHeight1;
		string TextProperty = textBox.Text;
		double DblVariable = Form1_Public.AreaHeight1;
		Module1.TextPropertyValidating(ref TextProperty, ref DblVariable, CheckLimits: true, 10f, 5000f, IncludeMin: true, IncludeMax: true, Message: true);
		Form1_Public.AreaHeight1 = (float)DblVariable;
		textBox.Text = TextProperty;
	}

	private void txtHeight2_Validating(object sender, CancelEventArgs e)
	{
		TextBox textBox = txtHeight2;
		string TextProperty = textBox.Text;
		double DblVariable = Form1_Public.AreaHeight2;
		Module1.TextPropertyValidating(ref TextProperty, ref DblVariable, CheckLimits: true, 0f, 5000f, IncludeMin: true, IncludeMax: true, Message: true);
		Form1_Public.AreaHeight2 = (float)DblVariable;
		textBox.Text = TextProperty;
	}

	private void cmdOK_Click(object sender, EventArgs e)
	{
		Form1_Public.AreaSizeOK = true;
		Close();
	}
}
