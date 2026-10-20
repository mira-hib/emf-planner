using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[DesignerGenerated]
public class frmUpdateMessage : Form
{
	private IContainer components;

	[field: AccessedThroughProperty("chkCheckNewVersionEveryAppStart")]
	internal virtual CheckBox chkCheckNewVersionEveryAppStart
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = chkCheckNewVersionEveryAppStart_Click;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.Click -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.Click += value2;
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

	[field: AccessedThroughProperty("Label2")]
	internal virtual Label Label2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblCurrentVersion")]
	internal virtual Label lblCurrentVersion
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblAvailableVersion")]
	internal virtual Label lblAvailableVersion
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("RichTextBox1")]
	internal virtual RichTextBox RichTextBox1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			LinkClickedEventHandler value2 = RichTextBox1_LinkClicked;
			RichTextBox richTextBox = field;
			if (richTextBox != null)
			{
				richTextBox.LinkClicked -= value2;
			}
			field = value;
			richTextBox = field;
			if (richTextBox != null)
			{
				richTextBox.LinkClicked += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmdClose")]
	internal virtual Button cmdClose
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdClose_Click;
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

	public frmUpdateMessage()
	{
		Load += frmUpdateMessage_Load;
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
		this.chkCheckNewVersionEveryAppStart = new System.Windows.Forms.CheckBox();
		this.Label1 = new System.Windows.Forms.Label();
		this.Label2 = new System.Windows.Forms.Label();
		this.lblCurrentVersion = new System.Windows.Forms.Label();
		this.lblAvailableVersion = new System.Windows.Forms.Label();
		this.RichTextBox1 = new System.Windows.Forms.RichTextBox();
		this.cmdClose = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.chkCheckNewVersionEveryAppStart.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
		this.chkCheckNewVersionEveryAppStart.AutoSize = true;
		this.chkCheckNewVersionEveryAppStart.Location = new System.Drawing.Point(19, 520);
		this.chkCheckNewVersionEveryAppStart.Name = "chkCheckNewVersionEveryAppStart";
		this.chkCheckNewVersionEveryAppStart.Size = new System.Drawing.Size(219, 17);
		this.chkCheckNewVersionEveryAppStart.TabIndex = 0;
		this.chkCheckNewVersionEveryAppStart.Text = "Check for updates every application start";
		this.chkCheckNewVersionEveryAppStart.UseVisualStyleBackColor = true;
		this.Label1.AutoSize = true;
		this.Label1.Location = new System.Drawing.Point(58, 15);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(81, 13);
		this.Label1.TabIndex = 1;
		this.Label1.Text = "Current version:";
		this.Label2.AutoSize = true;
		this.Label2.Location = new System.Drawing.Point(48, 40);
		this.Label2.Name = "Label2";
		this.Label2.Size = new System.Drawing.Size(90, 13);
		this.Label2.TabIndex = 2;
		this.Label2.Text = "Available version:";
		this.lblCurrentVersion.AutoSize = true;
		this.lblCurrentVersion.Location = new System.Drawing.Point(180, 15);
		this.lblCurrentVersion.Name = "lblCurrentVersion";
		this.lblCurrentVersion.Size = new System.Drawing.Size(22, 13);
		this.lblCurrentVersion.TabIndex = 3;
		this.lblCurrentVersion.Text = "- - -";
		this.lblAvailableVersion.AutoSize = true;
		this.lblAvailableVersion.Location = new System.Drawing.Point(180, 40);
		this.lblAvailableVersion.Name = "lblAvailableVersion";
		this.lblAvailableVersion.Size = new System.Drawing.Size(22, 13);
		this.lblAvailableVersion.TabIndex = 4;
		this.lblAvailableVersion.Text = "- - -";
		this.RichTextBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.RichTextBox1.BackColor = System.Drawing.SystemColors.Control;
		this.RichTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.RichTextBox1.Location = new System.Drawing.Point(4, 66);
		this.RichTextBox1.Name = "RichTextBox1";
		this.RichTextBox1.ReadOnly = true;
		this.RichTextBox1.Size = new System.Drawing.Size(715, 438);
		this.RichTextBox1.TabIndex = 5;
		this.RichTextBox1.Text = "";
		this.cmdClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cmdClose.Location = new System.Drawing.Point(609, 514);
		this.cmdClose.Name = "cmdClose";
		this.cmdClose.Size = new System.Drawing.Size(103, 27);
		this.cmdClose.TabIndex = 6;
		this.cmdClose.Text = "Close";
		this.cmdClose.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.cmdClose;
		base.ClientSize = new System.Drawing.Size(724, 549);
		base.Controls.Add(this.cmdClose);
		base.Controls.Add(this.RichTextBox1);
		base.Controls.Add(this.lblAvailableVersion);
		base.Controls.Add(this.lblCurrentVersion);
		base.Controls.Add(this.Label2);
		base.Controls.Add(this.Label1);
		base.Controls.Add(this.chkCheckNewVersionEveryAppStart);
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmUpdateMessage";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Check for Updates";
		base.TopMost = true;
		base.ResumeLayout(false);
		base.PerformLayout();
	}

	private void frmUpdateMessage_Load(object sender, EventArgs e)
	{
		lblCurrentVersion.Text = UpdateMessage.AppCurrentVersion;
		lblAvailableVersion.Text = UpdateMessage.AppAvailableVersion;
		chkCheckNewVersionEveryAppStart.Checked = !Public2.AppSetup.DontCheckNewVersionEveryAppStart;
		RichTextBox1.LoadFile(Public2.AppDataFolder + "\\History.rtf");
	}

	private void cmdClose_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void RichTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
	{
		Process.Start(e.LinkText);
	}

	private void chkCheckNewVersionEveryAppStart_Click(object sender, EventArgs e)
	{
		Public2.AppSetup.DontCheckNewVersionEveryAppStart = !chkCheckNewVersionEveryAppStart.Checked;
	}
}
