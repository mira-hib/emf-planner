using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[DesignerGenerated]
public class frmLineTypeSelect : Form
{
	private IContainer components;

	[field: AccessedThroughProperty("ListView1")]
	internal virtual ListView ListView1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = ListView1_MouseClick;
			ListView listView = field;
			if (listView != null)
			{
				listView.MouseClick -= value2;
			}
			field = value;
			listView = field;
			if (listView != null)
			{
				listView.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmdCancel")]
	internal virtual Button cmdCancel
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ilICO")]
	internal virtual ImageList ilICO
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public frmLineTypeSelect()
	{
		Load += frmIcoSelect_Load;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EMFPlanner.frmLineTypeSelect));
		this.ListView1 = new System.Windows.Forms.ListView();
		this.ilICO = new System.Windows.Forms.ImageList(this.components);
		this.cmdCancel = new System.Windows.Forms.Button();
		base.SuspendLayout();
		this.ListView1.AutoArrange = false;
		this.ListView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ListView1.HideSelection = false;
		this.ListView1.LabelWrap = false;
		this.ListView1.Location = new System.Drawing.Point(0, 0);
		this.ListView1.MultiSelect = false;
		this.ListView1.Name = "ListView1";
		this.ListView1.ShowGroups = false;
		this.ListView1.Size = new System.Drawing.Size(232, 503);
		this.ListView1.SmallImageList = this.ilICO;
		this.ListView1.TabIndex = 4;
		this.ListView1.UseCompatibleStateImageBehavior = false;
		this.ListView1.View = System.Windows.Forms.View.SmallIcon;
		this.ilICO.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ilICO.ImageStream");
		this.ilICO.TransparentColor = System.Drawing.Color.Transparent;
		this.ilICO.Images.SetKeyName(0, "0.bmp");
		this.ilICO.Images.SetKeyName(1, "1.bmp");
		this.ilICO.Images.SetKeyName(2, "2.bmp");
		this.ilICO.Images.SetKeyName(3, "3.bmp");
		this.ilICO.Images.SetKeyName(4, "4.bmp");
		this.ilICO.Images.SetKeyName(5, "5.bmp");
		this.ilICO.Images.SetKeyName(6, "6.bmp");
		this.ilICO.Images.SetKeyName(7, "7.bmp");
		this.ilICO.Images.SetKeyName(8, "8.bmp");
		this.ilICO.Images.SetKeyName(9, "9.bmp");
		this.ilICO.Images.SetKeyName(10, "10.bmp");
		this.ilICO.Images.SetKeyName(11, "11.bmp");
		this.ilICO.Images.SetKeyName(12, "12.bmp");
		this.ilICO.Images.SetKeyName(13, "13.bmp");
		this.ilICO.Images.SetKeyName(14, "14.bmp");
		this.ilICO.Images.SetKeyName(15, "15.bmp");
		this.ilICO.Images.SetKeyName(16, "16.bmp");
		this.ilICO.Images.SetKeyName(17, "17.bmp");
		this.ilICO.Images.SetKeyName(18, "18.bmp");
		this.ilICO.Images.SetKeyName(19, "19.bmp");
		this.ilICO.Images.SetKeyName(20, "20.bmp");
		this.ilICO.Images.SetKeyName(21, "21.bmp");
		this.ilICO.Images.SetKeyName(22, "22.bmp");
		this.ilICO.Images.SetKeyName(23, "23.bmp");
		this.ilICO.Images.SetKeyName(24, "24.bmp");
		this.ilICO.Images.SetKeyName(25, "25.bmp");
		this.ilICO.Images.SetKeyName(26, "26.bmp");
		this.ilICO.Images.SetKeyName(27, "27.bmp");
		this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cmdCancel.Location = new System.Drawing.Point(107, 2000);
		this.cmdCancel.Name = "cmdCancel";
		this.cmdCancel.Size = new System.Drawing.Size(75, 24);
		this.cmdCancel.TabIndex = 5;
		this.cmdCancel.Text = "Button1";
		this.cmdCancel.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.cmdCancel;
		base.ClientSize = new System.Drawing.Size(232, 503);
		base.Controls.Add(this.cmdCancel);
		base.Controls.Add(this.ListView1);
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmLineTypeSelect";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		this.Text = "Line type selection";
		base.ResumeLayout(false);
	}

	private void frmIcoSelect_Load(object sender, EventArgs e)
	{
		ListView1.Clear();
		checked
		{
			int num = ilICO.Images.Count - 1;
			for (int i = 0; i <= num; i++)
			{
				ListView1.Items.Add(i.ToString(), i);
			}
			ListView1.Refresh();
			Form1_Public.LineTypeIndex = -1;
		}
	}

	private void ListView1_MouseClick(object sender, MouseEventArgs e)
	{
		if (ListView1.SelectedItems.Count > 0)
		{
			Form1_Public.LineTypeIndex = checked((short)ListView1.SelectedItems[0].Index);
		}
		Close();
	}
}
