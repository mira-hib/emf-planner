using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[DesignerGenerated]
public class frmAbout : Form
{
	private IContainer components;

	[field: AccessedThroughProperty("GroupBox1")]
	internal virtual GroupBox GroupBox1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblVersion")]
	internal virtual Label lblVersion
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdCLOSE")]
	internal virtual Button cmdCLOSE
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdCLOSE_Click;
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

	[field: AccessedThroughProperty("Label2")]
	internal virtual Label Label2
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

	[field: AccessedThroughProperty("TextBox1")]
	internal virtual TextBox TextBox1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("LinkLabel1")]
	internal virtual LinkLabel LinkLabel1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			LinkLabelLinkClickedEventHandler value2 = LinkLabel1_LinkClicked_1;
			LinkLabel linkLabel = field;
			if (linkLabel != null)
			{
				linkLabel.LinkClicked -= value2;
			}
			field = value;
			linkLabel = field;
			if (linkLabel != null)
			{
				linkLabel.LinkClicked += value2;
			}
		}
	}

	public frmAbout()
	{
		Load += frmAbout_Load;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EMFPlanner.frmAbout));
		this.GroupBox1 = new System.Windows.Forms.GroupBox();
		this.Label2 = new System.Windows.Forms.Label();
		this.lblVersion = new System.Windows.Forms.Label();
		this.cmdCLOSE = new System.Windows.Forms.Button();
		this.Label1 = new System.Windows.Forms.Label();
		this.TextBox1 = new System.Windows.Forms.TextBox();
		this.LinkLabel1 = new System.Windows.Forms.LinkLabel();
		this.GroupBox1.SuspendLayout();
		base.SuspendLayout();
		this.GroupBox1.Controls.Add(this.LinkLabel1);
		this.GroupBox1.Controls.Add(this.TextBox1);
		this.GroupBox1.Controls.Add(this.Label1);
		this.GroupBox1.Controls.Add(this.Label2);
		this.GroupBox1.Controls.Add(this.lblVersion);
		resources.ApplyResources(this.GroupBox1, "GroupBox1");
		this.GroupBox1.Name = "GroupBox1";
		this.GroupBox1.TabStop = false;
		resources.ApplyResources(this.Label2, "Label2");
		this.Label2.Name = "Label2";
		resources.ApplyResources(this.lblVersion, "lblVersion");
		this.lblVersion.Name = "lblVersion";
		this.cmdCLOSE.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		resources.ApplyResources(this.cmdCLOSE, "cmdCLOSE");
		this.cmdCLOSE.Name = "cmdCLOSE";
		this.cmdCLOSE.UseVisualStyleBackColor = true;
		resources.ApplyResources(this.Label1, "Label1");
		this.Label1.Name = "Label1";
		this.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
		resources.ApplyResources(this.TextBox1, "TextBox1");
		this.TextBox1.ForeColor = System.Drawing.Color.Blue;
		this.TextBox1.Name = "TextBox1";
		this.TextBox1.ReadOnly = true;
		resources.ApplyResources(this.LinkLabel1, "LinkLabel1");
		this.LinkLabel1.Name = "LinkLabel1";
		this.LinkLabel1.TabStop = true;
		resources.ApplyResources(this, "$this");
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.cmdCLOSE;
		base.Controls.Add(this.GroupBox1);
		base.Controls.Add(this.cmdCLOSE);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmAbout";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		this.GroupBox1.ResumeLayout(false);
		this.GroupBox1.PerformLayout();
		base.ResumeLayout(false);
	}

	private void frmAbout_Load(object sender, EventArgs e)
	{
		try
		{
			StreamReader streamReader = new StreamReader(Application.StartupPath + "\\CurrentVersion.txt", Encoding.GetEncoding(1251));
			UpdateMessage.AppCurrentVersion = Strings.Trim(streamReader.ReadLine());
			streamReader.Close();
			string text = "Not activated";
			try
			{
				if (Enigma_IDE64.EP_RegLoadAndCheckKey())
				{
					text = "Activated";
				}
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
			lblVersion.Text = "EMF Planner 7.1 " + UpdateMessage.AppCurrentVersion + "  " + text;
		}
		catch (Exception projectError2)
		{
			ProjectData.SetProjectError(projectError2);
			ProjectData.ClearProjectError();
		}
	}

	private void cmdCLOSE_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void lblNEW_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
	{
		Process.Start("http://www.wireless-planning.com");
	}

	private void LinkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
	{
		Process.Start("http://www.puglieseprogettazioni.it");
	}
}
