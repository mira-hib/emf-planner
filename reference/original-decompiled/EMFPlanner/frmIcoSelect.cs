using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[DesignerGenerated]
public class frmIcoSelect : Form
{
	private IContainer components;

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

	[field: AccessedThroughProperty("ilICO1")]
	internal virtual ImageList ilICO1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public frmIcoSelect()
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EMFPlanner.frmIcoSelect));
		this.cmdCancel = new System.Windows.Forms.Button();
		this.ilICO = new System.Windows.Forms.ImageList(this.components);
		this.ListView1 = new System.Windows.Forms.ListView();
		this.ilICO1 = new System.Windows.Forms.ImageList(this.components);
		base.SuspendLayout();
		this.cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cmdCancel.Location = new System.Drawing.Point(115, 2000);
		this.cmdCancel.Name = "cmdCancel";
		this.cmdCancel.Size = new System.Drawing.Size(80, 24);
		this.cmdCancel.TabIndex = 0;
		this.cmdCancel.Text = "Button1";
		this.cmdCancel.UseVisualStyleBackColor = true;
		this.ilICO.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ilICO.ImageStream");
		this.ilICO.TransparentColor = System.Drawing.Color.Transparent;
		this.ilICO.Images.SetKeyName(0, "tree.png");
		this.ilICO.Images.SetKeyName(1, "Trava.png");
		this.ilICO.Images.SetKeyName(2, "tree-.png");
		this.ilICO.Images.SetKeyName(3, "");
		this.ilICO.Images.SetKeyName(4, "");
		this.ilICO.Images.SetKeyName(5, "");
		this.ilICO.Images.SetKeyName(6, "");
		this.ilICO.Images.SetKeyName(7, "");
		this.ilICO.Images.SetKeyName(8, "City-Water-Tower-icon (1).png");
		this.ilICO.Images.SetKeyName(9, "");
		this.ListView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ListView1.HideSelection = false;
		this.ListView1.LargeImageList = this.ilICO1;
		this.ListView1.Location = new System.Drawing.Point(0, 0);
		this.ListView1.MultiSelect = false;
		this.ListView1.Name = "ListView1";
		this.ListView1.Size = new System.Drawing.Size(190, 163);
		this.ListView1.SmallImageList = this.ilICO1;
		this.ListView1.TabIndex = 2;
		this.ListView1.TileSize = new System.Drawing.Size(25, 30);
		this.ListView1.UseCompatibleStateImageBehavior = false;
		this.ListView1.View = System.Windows.Forms.View.Tile;
		this.ilICO1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ilICO1.ImageStream");
		this.ilICO1.TransparentColor = System.Drawing.Color.Transparent;
		this.ilICO1.Images.SetKeyName(0, "point2x2.png");
		this.ilICO1.Images.SetKeyName(1, "tree.png");
		this.ilICO1.Images.SetKeyName(2, "tree-.png");
		this.ilICO1.Images.SetKeyName(3, "Trava.png");
		this.ilICO1.Images.SetKeyName(4, "Boloto.png");
		this.ilICO1.Images.SetKeyName(5, "Elka.png");
		this.ilICO1.Images.SetKeyName(6, "Kust.png");
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.cmdCancel;
		base.ClientSize = new System.Drawing.Size(190, 163);
		base.Controls.Add(this.ListView1);
		base.Controls.Add(this.cmdCancel);
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		base.Name = "frmIcoSelect";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
		this.Text = "Select image";
		base.ResumeLayout(false);
	}

	private void frmIcoSelect_Load(object sender, EventArgs e)
	{
		ListView1.Clear();
		checked
		{
			int num = ilICO1.Images.Count - 1;
			for (int i = 0; i <= num; i++)
			{
				ListView1.Items.Add("", i);
			}
			ListView1.Refresh();
			Form1_Public.IconImage = null;
		}
	}

	private void ListView1_MouseClick(object sender, MouseEventArgs e)
	{
		if (ListView1.SelectedItems.Count > 0)
		{
			Form1_Public.IconImage = ilICO1.Images[ListView1.SelectedItems[0].Index];
		}
		Close();
	}
}
