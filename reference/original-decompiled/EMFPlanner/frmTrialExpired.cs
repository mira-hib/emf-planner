using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using EMFPlanner.My;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[DesignerGenerated]
public class frmTrialExpired : Form
{
	private IContainer components;

	[field: AccessedThroughProperty("Label1")]
	internal virtual Label Label1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
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

	[field: AccessedThroughProperty("cmdPurchase")]
	internal virtual Button cmdPurchase
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdPurchase_Click;
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

	[field: AccessedThroughProperty("cmdActivate")]
	internal virtual Button cmdActivate
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdActivate_Click;
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

	public frmTrialExpired()
	{
		FormClosing += frmTrialExpired_FormClosing;
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
		this.Label1 = new System.Windows.Forms.Label();
		this.cmdClose = new System.Windows.Forms.Button();
		this.cmdPurchase = new System.Windows.Forms.Button();
		this.cmdActivate = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.Label1.Location = new System.Drawing.Point(35, 22);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(318, 50);
		this.Label1.TabIndex = 0;
		this.Label1.Text = "Sorry, but your trial period has expired!\r\nYou have exceeded 7 trial days!\r\nPlease, activate this software to remove trial limitations!";
		this.cmdClose.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cmdClose.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdClose.Location = new System.Drawing.Point(261, 92);
		this.cmdClose.Name = "cmdClose";
		this.cmdClose.Size = new System.Drawing.Size(103, 27);
		this.cmdClose.TabIndex = 14;
		this.cmdClose.Text = "Close";
		this.cmdClose.UseVisualStyleBackColor = true;
		this.cmdPurchase.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdPurchase.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cmdPurchase.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdPurchase.Location = new System.Drawing.Point(152, 92);
		this.cmdPurchase.Name = "cmdPurchase";
		this.cmdPurchase.Size = new System.Drawing.Size(103, 27);
		this.cmdPurchase.TabIndex = 15;
		this.cmdPurchase.Text = "Purchase";
		this.cmdPurchase.UseVisualStyleBackColor = true;
		this.cmdActivate.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdActivate.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cmdActivate.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdActivate.Location = new System.Drawing.Point(43, 92);
		this.cmdActivate.Name = "cmdActivate";
		this.cmdActivate.Size = new System.Drawing.Size(103, 27);
		this.cmdActivate.TabIndex = 16;
		this.cmdActivate.Text = "Activate";
		this.cmdActivate.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.cmdClose;
		base.ClientSize = new System.Drawing.Size(376, 131);
		base.Controls.Add(this.cmdActivate);
		base.Controls.Add(this.cmdPurchase);
		base.Controls.Add(this.cmdClose);
		base.Controls.Add(this.Label1);
		base.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmTrialExpired";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Trial has expired!";
		base.ResumeLayout(false);
	}

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	private void cmdActivate_Click(object sender, EventArgs e)
	{
		try
		{
			Enigma_IDE64.EP_ActivationShowDialog();
			if (Enigma_IDE64.EP_RegLoadAndCheckKey())
			{
				MyProject.Forms.Form1.cmdPurchase.Visible = false;
				MyProject.Forms.Form1.cmdActivation.Visible = false;
				MyProject.Forms.Form1.cmdSAVE.Enabled = true;
				MyProject.Forms.Form1.cmdSaveAS.Enabled = true;
				MyProject.Forms.Form1.cmdSavePanel.Enabled = true;
				Close();
			}
			else
			{
				ProjectData.EndApp();
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.EndApp();
			ProjectData.ClearProjectError();
		}
	}

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	private void cmdPurchase_Click(object sender, EventArgs e)
	{
		Process.Start("https://www.puglieseprogettazioni.it");
		Close();
		ProjectData.EndApp();
	}

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	private void cmdClose_Click(object sender, EventArgs e)
	{
		Close();
		ProjectData.EndApp();
	}

	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	private void frmTrialExpired_FormClosing(object sender, FormClosingEventArgs e)
	{
		try
		{
			if (!Enigma_IDE64.EP_RegLoadAndCheckKey())
			{
				ProjectData.EndApp();
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.EndApp();
			ProjectData.ClearProjectError();
		}
	}
}
