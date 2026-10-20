using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[DesignerGenerated]
public class frmHeight05 : Form
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

	public frmHeight05()
	{
		Load += FrmHeight05_Load;
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
		this.Label1 = new System.Windows.Forms.Label();
		this.Label65 = new System.Windows.Forms.Label();
		this.txtDH = new System.Windows.Forms.TextBox();
		this.GroupBox1.SuspendLayout();
		base.SuspendLayout();
		this.cmdCANCEL.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdCANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cmdCANCEL.Location = new System.Drawing.Point(87, 106);
		this.cmdCANCEL.Name = "cmdCANCEL";
		this.cmdCANCEL.Size = new System.Drawing.Size(88, 26);
		this.cmdCANCEL.TabIndex = 12;
		this.cmdCANCEL.TabStop = false;
		this.cmdCANCEL.Text = "Cancel";
		this.cmdCANCEL.UseVisualStyleBackColor = true;
		this.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdOK.Location = new System.Drawing.Point(181, 106);
		this.cmdOK.Name = "cmdOK";
		this.cmdOK.Size = new System.Drawing.Size(88, 26);
		this.cmdOK.TabIndex = 11;
		this.cmdOK.TabStop = false;
		this.cmdOK.Text = "OK";
		this.cmdOK.UseVisualStyleBackColor = true;
		this.GroupBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GroupBox1.Controls.Add(this.Label1);
		this.GroupBox1.Controls.Add(this.Label65);
		this.GroupBox1.Controls.Add(this.txtDH);
		this.GroupBox1.Location = new System.Drawing.Point(4, 12);
		this.GroupBox1.Name = "GroupBox1";
		this.GroupBox1.Size = new System.Drawing.Size(265, 79);
		this.GroupBox1.TabIndex = 10;
		this.GroupBox1.TabStop = false;
		this.Label1.AutoSize = true;
		this.Label1.Location = new System.Drawing.Point(242, 37);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(15, 13);
		this.Label1.TabIndex = 54;
		this.Label1.Text = "m";
		this.Label65.Location = new System.Drawing.Point(6, 27);
		this.Label65.Name = "Label65";
		this.Label65.Size = new System.Drawing.Size(178, 32);
		this.Label65.TabIndex = 53;
		this.Label65.Text = "Addition to the height of the bottom of vertical cuts";
		this.Label65.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.txtDH.BackColor = System.Drawing.SystemColors.Window;
		this.txtDH.Location = new System.Drawing.Point(190, 34);
		this.txtDH.Name = "txtDH";
		this.txtDH.Size = new System.Drawing.Size(46, 20);
		this.txtDH.TabIndex = 52;
		this.txtDH.TabStop = false;
		this.txtDH.Text = "0.5";
		this.txtDH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		base.AcceptButton = this.cmdOK;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.cmdCANCEL;
		base.ClientSize = new System.Drawing.Size(273, 144);
		base.Controls.Add(this.cmdCANCEL);
		base.Controls.Add(this.cmdOK);
		base.Controls.Add(this.GroupBox1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmHeight05";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Cut height selection";
		this.GroupBox1.ResumeLayout(false);
		this.GroupBox1.PerformLayout();
		base.ResumeLayout(false);
	}

	private void FrmHeight05_Load(object sender, EventArgs e)
	{
		txtDH.Text = Conversions.ToString(Form1_Public.HeightDH05);
		Form1_Public.Height05OK = false;
	}

	private void cmdCANCEL_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void cmdOK_Click(object sender, EventArgs e)
	{
		Form1_Public.Height05OK = true;
		Close();
	}

	private void txtDH_Validating(object sender, CancelEventArgs e)
	{
		TextBox textBox = txtDH;
		string TextProperty = textBox.Text;
		double DblVariable = Form1_Public.HeightDH05;
		Module1.TextPropertyValidating(ref TextProperty, ref DblVariable, CheckLimits: false, 0f, 10f, IncludeMin: false, IncludeMax: true, Message: true);
		Form1_Public.HeightDH05 = (float)DblVariable;
		textBox.Text = TextProperty;
	}
}
