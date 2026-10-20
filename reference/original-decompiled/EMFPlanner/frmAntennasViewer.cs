using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using EMFPlanner.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[DesignerGenerated]
public class frmAntennasViewer : Form
{
	private IContainer components;

	private bool FlagAntError;

	private string tmpChooseAntennaPuth;

	private bool FlagStartEnd;

	private short IndexLV;

	private TreeNode SelTN;

	[field: AccessedThroughProperty("SplitContainer1")]
	internal virtual SplitContainer SplitContainer1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("TreeView1")]
	internal virtual TreeView TreeView1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			TreeViewEventHandler value2 = TreeView1_AfterSelect;
			TreeView treeView = field;
			if (treeView != null)
			{
				treeView.AfterSelect -= value2;
			}
			field = value;
			treeView = field;
			if (treeView != null)
			{
				treeView.AfterSelect += value2;
			}
		}
	}

	[field: AccessedThroughProperty("SplitContainer2")]
	internal virtual SplitContainer SplitContainer2
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
			EventHandler value2 = ListView1_SelectedIndexChanged;
			EventHandler value3 = ListView1_DoubleClick;
			ListView listView = field;
			if (listView != null)
			{
				listView.SelectedIndexChanged -= value2;
				listView.DoubleClick -= value3;
			}
			field = value;
			listView = field;
			if (listView != null)
			{
				listView.SelectedIndexChanged += value2;
				listView.DoubleClick += value3;
			}
		}
	}

	[field: AccessedThroughProperty("ColumnHeader1")]
	internal virtual ColumnHeader ColumnHeader1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("ColumnHeader2")]
	internal virtual ColumnHeader ColumnHeader2
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

	[field: AccessedThroughProperty("ImageList1")]
	internal virtual ImageList ImageList1
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

	[field: AccessedThroughProperty("GroupBox4")]
	internal virtual GroupBox GroupBox4
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

	[field: AccessedThroughProperty("Label3")]
	internal virtual Label Label3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("pbDnVert2")]
	internal virtual PictureBox pbDnVert2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			PaintEventHandler value2 = pbDnVert2_Paint;
			PictureBox pictureBox = field;
			if (pictureBox != null)
			{
				pictureBox.Paint -= value2;
			}
			field = value;
			pictureBox = field;
			if (pictureBox != null)
			{
				pictureBox.Paint += value2;
			}
		}
	}

	[field: AccessedThroughProperty("pbDnHoriz2")]
	internal virtual PictureBox pbDnHoriz2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			PaintEventHandler value2 = pbDnHoriz2_Paint;
			PictureBox pictureBox = field;
			if (pictureBox != null)
			{
				pictureBox.Paint -= value2;
			}
			field = value;
			pictureBox = field;
			if (pictureBox != null)
			{
				pictureBox.Paint += value2;
			}
		}
	}

	[field: AccessedThroughProperty("Label8")]
	internal virtual Label Label8
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label9")]
	internal virtual Label Label9
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("GroupBox3")]
	internal virtual GroupBox GroupBox3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label11")]
	internal virtual Label Label11
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label12")]
	internal virtual Label Label12
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("pbDnVert1")]
	internal virtual PictureBox pbDnVert1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			PaintEventHandler value2 = pbDnVert_Paint;
			PictureBox pictureBox = field;
			if (pictureBox != null)
			{
				pictureBox.Paint -= value2;
			}
			field = value;
			pictureBox = field;
			if (pictureBox != null)
			{
				pictureBox.Paint += value2;
			}
		}
	}

	[field: AccessedThroughProperty("pbDnHoriz1")]
	internal virtual PictureBox pbDnHoriz1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			PaintEventHandler value2 = pbDnHoriz_Paint;
			PictureBox pictureBox = field;
			if (pictureBox != null)
			{
				pictureBox.Paint -= value2;
			}
			field = value;
			pictureBox = field;
			if (pictureBox != null)
			{
				pictureBox.Paint += value2;
			}
		}
	}

	[field: AccessedThroughProperty("Label13")]
	internal virtual Label Label13
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Label14")]
	internal virtual Label Label14
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdINVERT")]
	internal virtual Button cmdINVERT
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdINVERT_Click;
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

	[field: AccessedThroughProperty("txtUgolH")]
	internal virtual TextBox txtUgolH
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdTurnHor")]
	internal virtual Button cmdTurnHor
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdTurnHor_Click;
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

	[field: AccessedThroughProperty("Label4")]
	internal virtual Label Label4
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtUgolV")]
	internal virtual TextBox txtUgolV
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdTurnVert")]
	internal virtual Button cmdTurnVert
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdTurnVert_Click;
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

	[field: AccessedThroughProperty("lblVertWide")]
	internal virtual Label lblVertWide
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblHorWide")]
	internal virtual Label lblHorWide
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	public frmAntennasViewer()
	{
		Load += Form1_Load;
		FlagAntError = true;
		tmpChooseAntennaPuth = "";
		FlagStartEnd = false;
		IndexLV = 0;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EMFPlanner.frmAntennasViewer));
		this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
		this.TreeView1 = new System.Windows.Forms.TreeView();
		this.ImageList1 = new System.Windows.Forms.ImageList(this.components);
		this.SplitContainer2 = new System.Windows.Forms.SplitContainer();
		this.ListView1 = new System.Windows.Forms.ListView();
		this.ColumnHeader1 = new System.Windows.Forms.ColumnHeader();
		this.ColumnHeader2 = new System.Windows.Forms.ColumnHeader();
		this.lblVertWide = new System.Windows.Forms.Label();
		this.lblHorWide = new System.Windows.Forms.Label();
		this.Label4 = new System.Windows.Forms.Label();
		this.txtUgolV = new System.Windows.Forms.TextBox();
		this.cmdTurnVert = new System.Windows.Forms.Button();
		this.Label2 = new System.Windows.Forms.Label();
		this.txtUgolH = new System.Windows.Forms.TextBox();
		this.cmdTurnHor = new System.Windows.Forms.Button();
		this.cmdINVERT = new System.Windows.Forms.Button();
		this.TextBox1 = new System.Windows.Forms.TextBox();
		this.GroupBox4 = new System.Windows.Forms.GroupBox();
		this.Label1 = new System.Windows.Forms.Label();
		this.Label3 = new System.Windows.Forms.Label();
		this.pbDnVert2 = new System.Windows.Forms.PictureBox();
		this.pbDnHoriz2 = new System.Windows.Forms.PictureBox();
		this.Label8 = new System.Windows.Forms.Label();
		this.Label9 = new System.Windows.Forms.Label();
		this.GroupBox3 = new System.Windows.Forms.GroupBox();
		this.Label11 = new System.Windows.Forms.Label();
		this.Label12 = new System.Windows.Forms.Label();
		this.pbDnVert1 = new System.Windows.Forms.PictureBox();
		this.pbDnHoriz1 = new System.Windows.Forms.PictureBox();
		this.Label13 = new System.Windows.Forms.Label();
		this.Label14 = new System.Windows.Forms.Label();
		this.cmdOK = new System.Windows.Forms.Button();
		this.cmdCANCEL = new System.Windows.Forms.Button();
		((System.ComponentModel.ISupportInitialize)this.SplitContainer1).BeginInit();
		this.SplitContainer1.Panel1.SuspendLayout();
		this.SplitContainer1.Panel2.SuspendLayout();
		this.SplitContainer1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.SplitContainer2).BeginInit();
		this.SplitContainer2.Panel1.SuspendLayout();
		this.SplitContainer2.Panel2.SuspendLayout();
		this.SplitContainer2.SuspendLayout();
		this.GroupBox4.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pbDnVert2).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pbDnHoriz2).BeginInit();
		this.GroupBox3.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.pbDnVert1).BeginInit();
		((System.ComponentModel.ISupportInitialize)this.pbDnHoriz1).BeginInit();
		base.SuspendLayout();
		this.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.SplitContainer1.Location = new System.Drawing.Point(0, 0);
		this.SplitContainer1.Name = "SplitContainer1";
		this.SplitContainer1.Panel1.Controls.Add(this.TreeView1);
		this.SplitContainer1.Panel1MinSize = 220;
		this.SplitContainer1.Panel2.Controls.Add(this.SplitContainer2);
		this.SplitContainer1.Panel2MinSize = 300;
		this.SplitContainer1.Size = new System.Drawing.Size(992, 656);
		this.SplitContainer1.SplitterDistance = 259;
		this.SplitContainer1.TabIndex = 1;
		this.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.TreeView1.ImageIndex = 0;
		this.TreeView1.ImageList = this.ImageList1;
		this.TreeView1.Location = new System.Drawing.Point(0, 0);
		this.TreeView1.Name = "TreeView1";
		this.TreeView1.SelectedImageIndex = 0;
		this.TreeView1.Size = new System.Drawing.Size(259, 656);
		this.TreeView1.TabIndex = 0;
		this.ImageList1.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("ImageList1.ImageStream");
		this.ImageList1.TransparentColor = System.Drawing.Color.Transparent;
		this.ImageList1.Images.SetKeyName(0, "folder.ico");
		this.ImageList1.Images.SetKeyName(1, "file.ico");
		this.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
		this.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
		this.SplitContainer2.Location = new System.Drawing.Point(0, 0);
		this.SplitContainer2.Name = "SplitContainer2";
		this.SplitContainer2.Panel1.Controls.Add(this.ListView1);
		this.SplitContainer2.Panel1MinSize = 100;
		this.SplitContainer2.Panel2.Controls.Add(this.lblVertWide);
		this.SplitContainer2.Panel2.Controls.Add(this.lblHorWide);
		this.SplitContainer2.Panel2.Controls.Add(this.Label4);
		this.SplitContainer2.Panel2.Controls.Add(this.txtUgolV);
		this.SplitContainer2.Panel2.Controls.Add(this.cmdTurnVert);
		this.SplitContainer2.Panel2.Controls.Add(this.Label2);
		this.SplitContainer2.Panel2.Controls.Add(this.txtUgolH);
		this.SplitContainer2.Panel2.Controls.Add(this.cmdTurnHor);
		this.SplitContainer2.Panel2.Controls.Add(this.cmdINVERT);
		this.SplitContainer2.Panel2.Controls.Add(this.TextBox1);
		this.SplitContainer2.Panel2.Controls.Add(this.GroupBox4);
		this.SplitContainer2.Panel2.Controls.Add(this.GroupBox3);
		this.SplitContainer2.Panel2.Controls.Add(this.cmdOK);
		this.SplitContainer2.Panel2.Controls.Add(this.cmdCANCEL);
		this.SplitContainer2.Panel2MinSize = 200;
		this.SplitContainer2.Size = new System.Drawing.Size(729, 656);
		this.SplitContainer2.SplitterDistance = 317;
		this.SplitContainer2.TabIndex = 1;
		this.ListView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[2] { this.ColumnHeader1, this.ColumnHeader2 });
		this.ListView1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.ListView1.HideSelection = false;
		this.ListView1.Location = new System.Drawing.Point(0, 0);
		this.ListView1.Name = "ListView1";
		this.ListView1.Size = new System.Drawing.Size(317, 656);
		this.ListView1.SmallImageList = this.ImageList1;
		this.ListView1.TabIndex = 0;
		this.ListView1.UseCompatibleStateImageBehavior = false;
		this.ListView1.View = System.Windows.Forms.View.Details;
		this.ColumnHeader1.Text = "Antenna name";
		this.ColumnHeader1.Width = 100;
		this.ColumnHeader2.Text = "Antenna pattern file path";
		this.ColumnHeader2.Width = 400;
		this.lblVertWide.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblVertWide.AutoSize = true;
		this.lblVertWide.Location = new System.Drawing.Point(25, 61);
		this.lblVertWide.Name = "lblVertWide";
		this.lblVertWide.Size = new System.Drawing.Size(198, 13);
		this.lblVertWide.TabIndex = 31;
		this.lblVertWide.Text = "Antenna beamwidth V-plane (measured):";
		this.lblHorWide.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.lblHorWide.AutoSize = true;
		this.lblHorWide.Location = new System.Drawing.Point(24, 41);
		this.lblHorWide.Name = "lblHorWide";
		this.lblHorWide.Size = new System.Drawing.Size(199, 13);
		this.lblHorWide.TabIndex = 30;
		this.lblHorWide.Text = "Antenna beamwidth H-plane (measured):";
		this.Label4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.Label4.AutoSize = true;
		this.Label4.Location = new System.Drawing.Point(294, 566);
		this.Label4.Name = "Label4";
		this.Label4.Size = new System.Drawing.Size(28, 13);
		this.Label4.TabIndex = 29;
		this.Label4.Text = "deg.";
		this.txtUgolV.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtUgolV.Location = new System.Drawing.Point(234, 563);
		this.txtUgolV.Name = "txtUgolV";
		this.txtUgolV.Size = new System.Drawing.Size(46, 20);
		this.txtUgolV.TabIndex = 28;
		this.txtUgolV.Text = "0";
		this.txtUgolV.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.cmdTurnVert.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdTurnVert.Location = new System.Drawing.Point(17, 559);
		this.cmdTurnVert.Name = "cmdTurnVert";
		this.cmdTurnVert.Size = new System.Drawing.Size(192, 26);
		this.cmdTurnVert.TabIndex = 27;
		this.cmdTurnVert.Text = "Rotate pattern V-plane";
		this.cmdTurnVert.UseVisualStyleBackColor = true;
		this.Label2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.Label2.AutoSize = true;
		this.Label2.Location = new System.Drawing.Point(294, 534);
		this.Label2.Name = "Label2";
		this.Label2.Size = new System.Drawing.Size(28, 13);
		this.Label2.TabIndex = 26;
		this.Label2.Text = "deg.";
		this.txtUgolH.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.txtUgolH.Location = new System.Drawing.Point(234, 531);
		this.txtUgolH.Name = "txtUgolH";
		this.txtUgolH.Size = new System.Drawing.Size(46, 20);
		this.txtUgolH.TabIndex = 16;
		this.txtUgolH.Text = "0";
		this.txtUgolH.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.cmdTurnHor.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdTurnHor.Location = new System.Drawing.Point(17, 527);
		this.cmdTurnHor.Name = "cmdTurnHor";
		this.cmdTurnHor.Size = new System.Drawing.Size(192, 26);
		this.cmdTurnHor.TabIndex = 15;
		this.cmdTurnHor.Text = "Rotate pattern H-plane";
		this.cmdTurnHor.UseVisualStyleBackColor = true;
		this.cmdINVERT.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
		this.cmdINVERT.Location = new System.Drawing.Point(17, 494);
		this.cmdINVERT.Name = "cmdINVERT";
		this.cmdINVERT.Size = new System.Drawing.Size(192, 26);
		this.cmdINVERT.TabIndex = 14;
		this.cmdINVERT.Text = "\"Rotate\" antenna";
		this.cmdINVERT.UseVisualStyleBackColor = true;
		this.TextBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.TextBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
		this.TextBox1.Location = new System.Drawing.Point(7, 12);
		this.TextBox1.Name = "TextBox1";
		this.TextBox1.ReadOnly = true;
		this.TextBox1.Size = new System.Drawing.Size(391, 20);
		this.TextBox1.TabIndex = 12;
		this.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.GroupBox4.Controls.Add(this.Label1);
		this.GroupBox4.Controls.Add(this.Label3);
		this.GroupBox4.Controls.Add(this.pbDnVert2);
		this.GroupBox4.Controls.Add(this.pbDnHoriz2);
		this.GroupBox4.Controls.Add(this.Label8);
		this.GroupBox4.Controls.Add(this.Label9);
		this.GroupBox4.Location = new System.Drawing.Point(207, 88);
		this.GroupBox4.Name = "GroupBox4";
		this.GroupBox4.Size = new System.Drawing.Size(192, 391);
		this.GroupBox4.TabIndex = 11;
		this.GroupBox4.TabStop = false;
		this.GroupBox4.Text = "dB";
		this.Label1.AutoSize = true;
		this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
		this.Label1.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.Label1.Location = new System.Drawing.Point(165, 291);
		this.Label1.Margin = new System.Windows.Forms.Padding(0);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(19, 13);
		this.Label1.TabIndex = 29;
		this.Label1.Text = "0°";
		this.Label3.AutoSize = true;
		this.Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
		this.Label3.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.Label3.Location = new System.Drawing.Point(165, 111);
		this.Label3.Name = "Label3";
		this.Label3.Size = new System.Drawing.Size(19, 13);
		this.Label3.TabIndex = 28;
		this.Label3.Text = "0°";
		this.pbDnVert2.Location = new System.Drawing.Point(15, 226);
		this.pbDnVert2.Name = "pbDnVert2";
		this.pbDnVert2.Size = new System.Drawing.Size(150, 150);
		this.pbDnVert2.TabIndex = 27;
		this.pbDnVert2.TabStop = false;
		this.pbDnHoriz2.Location = new System.Drawing.Point(15, 46);
		this.pbDnHoriz2.Name = "pbDnHoriz2";
		this.pbDnHoriz2.Size = new System.Drawing.Size(150, 150);
		this.pbDnHoriz2.TabIndex = 26;
		this.pbDnHoriz2.TabStop = false;
		this.Label8.AutoSize = true;
		this.Label8.Location = new System.Drawing.Point(70, 209);
		this.Label8.Name = "Label8";
		this.Label8.Size = new System.Drawing.Size(43, 13);
		this.Label8.TabIndex = 25;
		this.Label8.Text = "V-plane";
		this.Label9.AutoSize = true;
		this.Label9.Location = new System.Drawing.Point(59, 28);
		this.Label9.Name = "Label9";
		this.Label9.Size = new System.Drawing.Size(44, 13);
		this.Label9.TabIndex = 24;
		this.Label9.Text = "H-plane";
		this.GroupBox3.Controls.Add(this.Label11);
		this.GroupBox3.Controls.Add(this.Label12);
		this.GroupBox3.Controls.Add(this.pbDnVert1);
		this.GroupBox3.Controls.Add(this.pbDnHoriz1);
		this.GroupBox3.Controls.Add(this.Label13);
		this.GroupBox3.Controls.Add(this.Label14);
		this.GroupBox3.Location = new System.Drawing.Point(9, 88);
		this.GroupBox3.Name = "GroupBox3";
		this.GroupBox3.Size = new System.Drawing.Size(192, 391);
		this.GroupBox3.TabIndex = 10;
		this.GroupBox3.TabStop = false;
		this.GroupBox3.Text = "Relative";
		this.Label11.AutoSize = true;
		this.Label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
		this.Label11.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.Label11.Location = new System.Drawing.Point(165, 291);
		this.Label11.Margin = new System.Windows.Forms.Padding(0);
		this.Label11.Name = "Label11";
		this.Label11.Size = new System.Drawing.Size(19, 13);
		this.Label11.TabIndex = 29;
		this.Label11.Text = "0°";
		this.Label12.AutoSize = true;
		this.Label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, 204);
		this.Label12.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
		this.Label12.Location = new System.Drawing.Point(165, 111);
		this.Label12.Name = "Label12";
		this.Label12.Size = new System.Drawing.Size(19, 13);
		this.Label12.TabIndex = 28;
		this.Label12.Text = "0°";
		this.pbDnVert1.Location = new System.Drawing.Point(15, 226);
		this.pbDnVert1.Name = "pbDnVert1";
		this.pbDnVert1.Size = new System.Drawing.Size(150, 150);
		this.pbDnVert1.TabIndex = 27;
		this.pbDnVert1.TabStop = false;
		this.pbDnHoriz1.Location = new System.Drawing.Point(15, 46);
		this.pbDnHoriz1.Name = "pbDnHoriz1";
		this.pbDnHoriz1.Size = new System.Drawing.Size(150, 150);
		this.pbDnHoriz1.TabIndex = 26;
		this.pbDnHoriz1.TabStop = false;
		this.Label13.AutoSize = true;
		this.Label13.Location = new System.Drawing.Point(67, 209);
		this.Label13.Name = "Label13";
		this.Label13.Size = new System.Drawing.Size(43, 13);
		this.Label13.TabIndex = 25;
		this.Label13.Text = "V-plane";
		this.Label14.AutoSize = true;
		this.Label14.Location = new System.Drawing.Point(56, 27);
		this.Label14.Name = "Label14";
		this.Label14.Size = new System.Drawing.Size(44, 13);
		this.Label14.TabIndex = 24;
		this.Label14.Text = "H-plane";
		this.cmdOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdOK.Location = new System.Drawing.Point(297, 618);
		this.cmdOK.Name = "cmdOK";
		this.cmdOK.Size = new System.Drawing.Size(94, 26);
		this.cmdOK.TabIndex = 1;
		this.cmdOK.Text = "OK";
		this.cmdOK.UseVisualStyleBackColor = true;
		this.cmdCANCEL.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
		this.cmdCANCEL.DialogResult = System.Windows.Forms.DialogResult.Cancel;
		this.cmdCANCEL.Location = new System.Drawing.Point(196, 618);
		this.cmdCANCEL.Name = "cmdCANCEL";
		this.cmdCANCEL.Size = new System.Drawing.Size(95, 26);
		this.cmdCANCEL.TabIndex = 0;
		this.cmdCANCEL.Text = "Cancel";
		this.cmdCANCEL.UseVisualStyleBackColor = true;
		base.AcceptButton = this.cmdOK;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.CancelButton = this.cmdCANCEL;
		base.ClientSize = new System.Drawing.Size(992, 656);
		base.Controls.Add(this.SplitContainer1);
		base.KeyPreview = true;
		base.MaximizeBox = false;
		base.MinimizeBox = false;
		this.MinimumSize = new System.Drawing.Size(1000, 650);
		base.Name = "frmAntennasViewer";
		base.ShowIcon = false;
		base.ShowInTaskbar = false;
		base.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
		this.Text = "Antenna pattern selection";
		this.SplitContainer1.Panel1.ResumeLayout(false);
		this.SplitContainer1.Panel2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.SplitContainer1).EndInit();
		this.SplitContainer1.ResumeLayout(false);
		this.SplitContainer2.Panel1.ResumeLayout(false);
		this.SplitContainer2.Panel2.ResumeLayout(false);
		this.SplitContainer2.Panel2.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.SplitContainer2).EndInit();
		this.SplitContainer2.ResumeLayout(false);
		this.GroupBox4.ResumeLayout(false);
		this.GroupBox4.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pbDnVert2).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pbDnHoriz2).EndInit();
		this.GroupBox3.ResumeLayout(false);
		this.GroupBox3.PerformLayout();
		((System.ComponentModel.ISupportInitialize)this.pbDnVert1).EndInit();
		((System.ComponentModel.ISupportInitialize)this.pbDnHoriz1).EndInit();
		base.ResumeLayout(false);
	}

	private void Form1_Load(object sender, EventArgs e)
	{
		if (SelTN == null)
		{
			PopulateTreeView();
		}
		else
		{
			try
			{
				if (!FlagStartEnd && ListView1.Items.Count > IndexLV)
				{
					ListView1.Items[IndexLV].Selected = true;
					ListView1.Select();
					ListView1.EnsureVisible(IndexLV);
					FlagStartEnd = true;
				}
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
		}
		FlagStartEnd = false;
	}

	private void PopulateTreeView()
	{
		TreeView1.Nodes.Clear();
		DirectoryInfo directoryInfo = new DirectoryInfo(Public2.AppSetup.AntennaFolder);
		if (directoryInfo.Exists)
		{
			TreeNode treeNode = new TreeNode(directoryInfo.Name);
			treeNode.Tag = directoryInfo;
			GetDirectories(directoryInfo.GetDirectories(), treeNode);
			TreeView1.Nodes.Add(treeNode);
		}
		TreeView1.Width = 0 - (AutoSize ? 1 : 0);
	}

	private void GetDirectories(DirectoryInfo[] subDirs, TreeNode nodeToAddTo)
	{
		DirectoryInfo[] directories = default;
		foreach (DirectoryInfo directoryInfo in subDirs)
		{
			TreeNode treeNode = new TreeNode(directoryInfo.Name, 0, 0);
			treeNode.Tag = directoryInfo;
			treeNode.ImageKey = "folder";
			try
			{
				directories = directoryInfo.GetDirectories();
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
			if (directories.Length != 0)
			{
				GetDirectories(directories, treeNode);
			}
			nodeToAddTo.Nodes.Add(treeNode);
		}
	}

	private void TreeView1_AfterSelect(object sender, TreeViewEventArgs e)
	{
		SelTN = e.Node;
		ListView1.Items.Clear();
		DirectoryInfo directoryInfo = (DirectoryInfo)SelTN.Tag;
		ListViewItem listViewItem = null;
		ListView1.BeginUpdate();
		FileInfo[] files = directoryInfo.GetFiles();
		foreach (FileInfo fileInfo in files)
		{
			listViewItem = new ListViewItem(fileInfo.Name, 1);
			ListViewItem.ListViewSubItem[] items = new ListViewItem.ListViewSubItem[1]
			{
				new ListViewItem.ListViewSubItem(listViewItem, fileInfo.FullName)
			};
			listViewItem.SubItems.AddRange(items);
			ListView1.Items.Add(listViewItem);
		}
		ListView1.EndUpdate();
		ListView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
		if (ListView1.Items.Count > IndexLV)
		{
			ListView1.Items[IndexLV].Selected = true;
			ListView1.Select();
			ListView1.EnsureVisible(IndexLV);
		}
	}

	private void ListView1_SelectedIndexChanged(object sender, EventArgs e)
	{
		FlagAntError = true;
		checked
		{
			try
			{
				IndexLV = (short)ListView1.SelectedItems[0].Index;
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				ProjectData.ClearProjectError();
			}
			tmpChooseAntennaPuth = "";
			try
			{
				if (!MyProject.Computer.FileSystem.FileExists(ListView1.SelectedItems[0].SubItems[1].Text))
				{
					return;
				}
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
				return;
			}
			ClassDN.AntennaPatternClass antennaPatternClass = ClassDN.LoadAntennaFile(ListView1.SelectedItems[0].SubItems[1].Text);
			if (antennaPatternClass == null)
			{
				TextBox1.Text = "";
				pbDnHoriz1.Refresh();
				pbDnVert1.Refresh();
				pbDnHoriz2.Refresh();
				pbDnVert2.Refresh();
				return;
			}
			if ((antennaPatternClass.GR[0].HZ.Count != 360) | (antennaPatternClass.GR[0].VR.Count != 360))
			{
				TextBox1.Text = "";
				pbDnHoriz1.Refresh();
				pbDnVert1.Refresh();
				pbDnHoriz2.Refresh();
				pbDnVert2.Refresh();
				return;
			}
			FlagAntError = false;
			tmpChooseAntennaPuth = ListView1.SelectedItems[0].SubItems[1].Text;
			TextBox1.Text = antennaPatternClass.Name;
			Form1_Public.AntennaName = antennaPatternClass.Name;
			Form1_Public.AntennaGain = antennaPatternClass.GainDBi;
			int num = 0;
			do
			{
				Form1_Public.HdnPolNorm_h[num] = Math.Abs(antennaPatternClass.GR[0].HZ[num].G);
				Form1_Public.HdnPolNorm_v[num] = Math.Abs(antennaPatternClass.GR[0].VR[num].G);
				num++;
			}
			while (num <= 359);
			pbDnHoriz1.Refresh();
			pbDnVert1.Refresh();
			pbDnHoriz2.Refresh();
			pbDnVert2.Refresh();
			try
			{
				if ((Form1_Public.HdnPolNorm_h.GetUpperBound(0) == 359) | (Form1_Public.HdnPolNorm_v.GetUpperBound(0) == 359))
				{
					float num2 = 0f;
					float num3 = 0f;
					if ((Form1_Public.HdnPolNorm_h[359] <= 3f) & (Form1_Public.HdnPolNorm_h[0] >= 3f))
					{
						num2 = 359f + (3f - Form1_Public.HdnPolNorm_h[359]) / (Form1_Public.HdnPolNorm_h[0] - Form1_Public.HdnPolNorm_h[359]);
					}
					else
					{
						int num4 = 0;
						do
						{
							if ((Form1_Public.HdnPolNorm_h[num4] <= 3f) & (Form1_Public.HdnPolNorm_h[num4 + 1] >= 3f))
							{
								num2 = (float)num4 + (3f - Form1_Public.HdnPolNorm_h[num4]) / (Form1_Public.HdnPolNorm_h[num4 + 1] - Form1_Public.HdnPolNorm_h[num4]);
								break;
							}
							num4++;
						}
						while (num4 <= 358);
					}
					if ((Form1_Public.HdnPolNorm_h[0] <= 3f) & (Form1_Public.HdnPolNorm_h[359] >= 3f))
					{
						num3 = 359f + (3f - Form1_Public.HdnPolNorm_h[359]) / (Form1_Public.HdnPolNorm_h[0] - Form1_Public.HdnPolNorm_h[359]);
					}
					else
					{
						int num5 = 359;
						do
						{
							if ((Form1_Public.HdnPolNorm_h[num5] <= 3f) & (Form1_Public.HdnPolNorm_h[num5 - 1] >= 3f))
							{
								num3 = (float)(num5 - 1) + (3f - Form1_Public.HdnPolNorm_h[num5 - 1]) / (Form1_Public.HdnPolNorm_h[num5] - Form1_Public.HdnPolNorm_h[num5 - 1]);
								break;
							}
							num5 += -1;
						}
						while (num5 >= 1);
					}
					float num6 = ((!((num2 < 180f) & (num3 > 180f))) ? Math.Abs(num2 - num3) : (num2 + (360f - num3)));
					if ((num6 > 0f) & (num6 < 340f))
					{
						lblHorWide.Text = "Antenna beamwidth H-plane (measured): " + Strings.Format(num6, "0.0") + "°";
					}
					else
					{
						lblHorWide.Text = "Antenna beamwidth V-plane (measured): 360°";
					}
					num2 = 0f;
					num3 = 0f;
					if ((Form1_Public.HdnPolNorm_v[359] <= 3f) & (Form1_Public.HdnPolNorm_v[0] >= 3f))
					{
						num2 = 359f + (3f - Form1_Public.HdnPolNorm_v[359]) / (Form1_Public.HdnPolNorm_v[0] - Form1_Public.HdnPolNorm_v[359]);
					}
					else
					{
						int num7 = 0;
						do
						{
							if ((Form1_Public.HdnPolNorm_v[num7] <= 3f) & (Form1_Public.HdnPolNorm_v[num7 + 1] >= 3f))
							{
								num2 = (float)num7 + (3f - Form1_Public.HdnPolNorm_v[num7]) / (Form1_Public.HdnPolNorm_v[num7 + 1] - Form1_Public.HdnPolNorm_v[num7]);
								break;
							}
							num7++;
						}
						while (num7 <= 358);
					}
					if ((Form1_Public.HdnPolNorm_v[0] <= 3f) & (Form1_Public.HdnPolNorm_v[359] >= 3f))
					{
						num3 = 359f + (3f - Form1_Public.HdnPolNorm_v[359]) / (Form1_Public.HdnPolNorm_v[0] - Form1_Public.HdnPolNorm_v[359]);
					}
					else
					{
						int num8 = 359;
						do
						{
							if ((Form1_Public.HdnPolNorm_v[num8] <= 3f) & (Form1_Public.HdnPolNorm_v[num8 - 1] >= 3f))
							{
								num3 = (float)(num8 - 1) + (3f - Form1_Public.HdnPolNorm_v[num8 - 1]) / (Form1_Public.HdnPolNorm_v[num8] - Form1_Public.HdnPolNorm_v[num8 - 1]);
								break;
							}
							num8 += -1;
						}
						while (num8 >= 1);
					}
					num6 = ((!((num2 < 180f) & (num3 > 180f))) ? Math.Abs(num2 - num3) : (num2 + (360f - num3)));
					if ((num6 > 0f) & (num6 < 340f))
					{
						lblVertWide.Text = "Antenna beamwidth V-plane (measured): " + Strings.Format(num6, "0.0") + "°";
					}
					else
					{
						lblVertWide.Text = "Antenna beamwidth V-plane (measured): 360°";
					}
				}
			}
			catch (Exception projectError2)
			{
				ProjectData.SetProjectError(projectError2);
				ProjectData.ClearProjectError();
			}
			if (!FlagStartEnd)
			{
				if (ListView1.Items.Count > IndexLV)
				{
					ListView1.Items[IndexLV].Selected = true;
					ListView1.Select();
					ListView1.EnsureVisible(IndexLV);
				}
				FlagStartEnd = true;
			}
		}
	}

	private void cmdINVERT_Click(object sender, EventArgs e)
	{
		if (!FlagAntError)
		{
			Module1.InvertDN(ref Form1_Public.HdnPolNorm_h, ref Form1_Public.HdnPolNorm_v);
			pbDnHoriz1.Refresh();
			pbDnVert1.Refresh();
			pbDnHoriz2.Refresh();
			pbDnVert2.Refresh();
		}
	}

	private void cmdTurnHor_Click(object sender, EventArgs e)
	{
		if (!FlagAntError)
		{
			Module1.TurnDn(ref Form1_Public.HdnPolNorm_h, txtUgolH.Text);
			pbDnHoriz1.Refresh();
			pbDnVert1.Refresh();
			pbDnHoriz2.Refresh();
			pbDnVert2.Refresh();
		}
	}

	private void cmdTurnVert_Click(object sender, EventArgs e)
	{
		if (!FlagAntError)
		{
			Module1.TurnDn(ref Form1_Public.HdnPolNorm_v, txtUgolV.Text);
			pbDnHoriz1.Refresh();
			pbDnVert1.Refresh();
			pbDnHoriz2.Refresh();
			pbDnVert2.Refresh();
		}
	}

	private void cmdCANCEL_Click(object sender, EventArgs e)
	{
		Close();
	}

	private void cmdOK_Click(object sender, EventArgs e)
	{
		if (!FlagAntError)
		{
			if (Operators.CompareString(tmpChooseAntennaPuth, "", TextCompare: false) == 0)
			{
				Interaction.MsgBox(Localization.LocMSG(7));
				return;
			}
			Form1_Public.ChooseAntennaPuth = tmpChooseAntennaPuth;
			Close();
		}
	}

	private void ListView1_DoubleClick(object sender, EventArgs e)
	{
		if (!FlagAntError)
		{
			if (Operators.CompareString(tmpChooseAntennaPuth, "", TextCompare: false) == 0)
			{
				Interaction.MsgBox(Localization.LocMSG(7));
				return;
			}
			Form1_Public.ChooseAntennaPuth = tmpChooseAntennaPuth;
			Close();
		}
	}

	private void pbDnHoriz_Paint(object sender, PaintEventArgs e)
	{
		Pen pen = new Pen(Color.Black, 1f);
		Pen pen2 = new Pen(Color.Red, 2f);
		checked
		{
			int num = Math.Min(pbDnHoriz1.Width - 5, pbDnHoriz1.Height - 5);
			float num3;
			if (!FlagAntError)
			{
				int num2 = 0;
				do
				{
					num3 = (float)Math.Pow(10.0, (0f - Form1_Public.HdnPolNorm_h[num2]) / 20f);
					float num4 = (float)Math.Pow(10.0, (0f - Form1_Public.HdnPolNorm_h[num2 + 1]) / 20f);
					e.Graphics.DrawLine(pen2, Convert.ToSingle((double)num / 2.0 + (double)num3 * Math.Sin((double)(num2 + 90) / (180.0 / Math.PI)) * (double)num / 2.0), Convert.ToSingle((double)num - (double)num / 2.0 - (double)num3 * Math.Cos((double)(num2 + 90) / (180.0 / Math.PI)) * (double)num / 2.0), Convert.ToSingle((double)num / 2.0 + (double)num4 * Math.Sin((double)(num2 + 90 + 1) / (180.0 / Math.PI)) * (double)num / 2.0), Convert.ToSingle((double)num - (double)num / 2.0 - (double)num4 * Math.Cos((double)(num2 + 90 + 1) / (180.0 / Math.PI)) * (double)num / 2.0));
					num2++;
				}
				while (num2 <= 358);
			}
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), 0, (int)Math.Round((double)num / 2.0), num);
			e.Graphics.DrawLine(pen, 0, (int)Math.Round((double)num / 2.0), num, (int)Math.Round((double)num / 2.0));
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin(Math.PI / 6.0) * (double)num / 2.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos(Math.PI / 6.0) * (double)num / 2.0));
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin(Math.PI / 3.0) * (double)num / 2.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos(Math.PI / 3.0) * (double)num / 2.0));
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin(Math.PI * 2.0 / 3.0) * (double)num / 2.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos(Math.PI * 2.0 / 3.0) * (double)num / 2.0));
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin(Math.PI * 5.0 / 6.0) * (double)num / 2.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos(Math.PI * 5.0 / 6.0) * (double)num / 2.0));
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin(3.665191429188092) * (double)num / 2.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos(3.665191429188092) * (double)num / 2.0));
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin(4.1887902047863905) * (double)num / 2.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos(4.1887902047863905) * (double)num / 2.0));
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin(5.235987755982989) * (double)num / 2.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos(5.235987755982989) * (double)num / 2.0));
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin(5.759586531581287) * (double)num / 2.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos(5.759586531581287) * (double)num / 2.0));
			e.Graphics.DrawEllipse(pen, 0, 0, num, num);
			num3 = 0.70794576f;
			e.Graphics.DrawEllipse(pen, (int)Math.Round(0.5 * (double)num * (double)(1f - num3)), (int)Math.Round(0.5 * (double)num * (double)(1f - num3)), (int)Math.Round((float)num * num3), (int)Math.Round((float)num * num3));
			e.Graphics.DrawString("-3dB", new Font("Arial", 7f, FontStyle.Regular), new SolidBrush(Color.Blue), (int)Math.Round((double)num / 2.0) - 2, (float)((double)num - 0.5 * (double)num * (double)(1f - num3) + 0.0));
			num3 = 0.31622776f;
			e.Graphics.DrawEllipse(pen, (int)Math.Round(0.5 * (double)num * (double)(1f - num3)), (int)Math.Round(0.5 * (double)num * (double)(1f - num3)), (int)Math.Round((float)num * num3), (int)Math.Round((float)num * num3));
			e.Graphics.DrawString("-10dB", new Font("Arial", 7f, FontStyle.Regular), new SolidBrush(Color.Blue), (int)Math.Round((double)num / 2.0) - 2, (float)((double)num - 0.5 * (double)num * (double)(1f - num3) + 0.0));
			num3 = 0.1f;
			e.Graphics.DrawEllipse(pen, (int)Math.Round(0.5 * (double)num * (double)(1f - num3)), (int)Math.Round(0.5 * (double)num * (double)(1f - num3)), (int)Math.Round((float)num * num3), (int)Math.Round((float)num * num3));
			e.Graphics.DrawString("-20dB", new Font("Arial", 7f, FontStyle.Regular), new SolidBrush(Color.Blue), (int)Math.Round((double)num / 2.0) - 2, (float)((double)num - 0.5 * (double)num * (double)(1f - num3) + 0.0));
		}
	}

	private void pbDnVert_Paint(object sender, PaintEventArgs e)
	{
		Pen pen = new Pen(Color.Black, 1f);
		Pen pen2 = new Pen(Color.Red, 2f);
		checked
		{
			int num = Math.Min(pbDnVert1.Width - 5, pbDnVert1.Height - 5);
			float num3;
			if (!FlagAntError)
			{
				int num2 = 0;
				do
				{
					num3 = (float)Math.Pow(10.0, (0f - Form1_Public.HdnPolNorm_v[num2]) / 20f);
					float num4 = (float)Math.Pow(10.0, (0f - Form1_Public.HdnPolNorm_v[num2 + 1]) / 20f);
					e.Graphics.DrawLine(pen2, Convert.ToSingle((double)num / 2.0 + (double)num3 * Math.Sin((double)(num2 + 90) / (180.0 / Math.PI)) * (double)num / 2.0), Convert.ToSingle((double)num - (double)num / 2.0 - (double)num3 * Math.Cos((double)(num2 + 90) / (180.0 / Math.PI)) * (double)num / 2.0), Convert.ToSingle((double)num / 2.0 + (double)num4 * Math.Sin((double)(num2 + 90 + 1) / (180.0 / Math.PI)) * (double)num / 2.0), Convert.ToSingle((double)num - (double)num / 2.0 - (double)num4 * Math.Cos((double)(num2 + 90 + 1) / (180.0 / Math.PI)) * (double)num / 2.0));
					num2++;
				}
				while (num2 <= 358);
			}
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), 0, (int)Math.Round((double)num / 2.0), num);
			e.Graphics.DrawLine(pen, 0, (int)Math.Round((double)num / 2.0), num, (int)Math.Round((double)num / 2.0));
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin(Math.PI / 6.0) * (double)num / 2.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos(Math.PI / 6.0) * (double)num / 2.0));
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin(Math.PI / 3.0) * (double)num / 2.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos(Math.PI / 3.0) * (double)num / 2.0));
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin(Math.PI * 2.0 / 3.0) * (double)num / 2.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos(Math.PI * 2.0 / 3.0) * (double)num / 2.0));
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin(Math.PI * 5.0 / 6.0) * (double)num / 2.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos(Math.PI * 5.0 / 6.0) * (double)num / 2.0));
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin(3.665191429188092) * (double)num / 2.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos(3.665191429188092) * (double)num / 2.0));
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin(4.1887902047863905) * (double)num / 2.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos(4.1887902047863905) * (double)num / 2.0));
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin(5.235987755982989) * (double)num / 2.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos(5.235987755982989) * (double)num / 2.0));
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin(5.759586531581287) * (double)num / 2.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos(5.759586531581287) * (double)num / 2.0));
			e.Graphics.DrawEllipse(pen, 0, 0, num, num);
			num3 = 0.70794576f;
			e.Graphics.DrawEllipse(pen, (int)Math.Round(0.5 * (double)num * (double)(1f - num3)), (int)Math.Round(0.5 * (double)num * (double)(1f - num3)), (int)Math.Round((float)num * num3), (int)Math.Round((float)num * num3));
			e.Graphics.DrawString("-3dB", new Font("Arial", 7f, FontStyle.Regular), new SolidBrush(Color.Blue), (int)Math.Round((double)num / 2.0) - 2, (float)((double)num - 0.5 * (double)num * (double)(1f - num3) + 0.0));
			num3 = 0.31622776f;
			e.Graphics.DrawEllipse(pen, (int)Math.Round(0.5 * (double)num * (double)(1f - num3)), (int)Math.Round(0.5 * (double)num * (double)(1f - num3)), (int)Math.Round((float)num * num3), (int)Math.Round((float)num * num3));
			e.Graphics.DrawString("-10dB", new Font("Arial", 7f, FontStyle.Regular), new SolidBrush(Color.Blue), (int)Math.Round((double)num / 2.0) - 2, (float)((double)num - 0.5 * (double)num * (double)(1f - num3) + 0.0));
			num3 = 0.1f;
			e.Graphics.DrawEllipse(pen, (int)Math.Round(0.5 * (double)num * (double)(1f - num3)), (int)Math.Round(0.5 * (double)num * (double)(1f - num3)), (int)Math.Round((float)num * num3), (int)Math.Round((float)num * num3));
			e.Graphics.DrawString("-20dB", new Font("Arial", 7f, FontStyle.Regular), new SolidBrush(Color.Blue), (int)Math.Round((double)num / 2.0) - 2, (float)((double)num - 0.5 * (double)num * (double)(1f - num3) + 0.0));
		}
	}

	private void pbDnHoriz2_Paint(object sender, PaintEventArgs e)
	{
		Pen pen = new Pen(Color.Black, 1f);
		Pen pen2 = new Pen(Color.Red, 2f);
		checked
		{
			int num = Math.Min(pbDnHoriz2.Width - 5, pbDnHoriz2.Height - 5);
			float num3;
			if (!FlagAntError)
			{
				int num2 = 0;
				do
				{
					num3 = (30f - Form1_Public.HdnPolNorm_h[num2]) / 30f;
					float num4 = (30f - Form1_Public.HdnPolNorm_h[num2 + 1]) / 30f;
					if (num3 < 0f)
					{
						num3 = 0f;
					}
					if (num4 < 0f)
					{
						num4 = 0f;
					}
					e.Graphics.DrawLine(pen2, Convert.ToSingle((double)num / 2.0 + (double)num3 * Math.Sin((double)(num2 + 90) / (180.0 / Math.PI)) * (double)num / 2.0), Convert.ToSingle((double)num - (double)num / 2.0 - (double)num3 * Math.Cos((double)(num2 + 90) / (180.0 / Math.PI)) * (double)num / 2.0), Convert.ToSingle((double)num / 2.0 + (double)num4 * Math.Sin((double)(num2 + 90 + 1) / (180.0 / Math.PI)) * (double)num / 2.0), Convert.ToSingle((double)num - (double)num / 2.0 - (double)num4 * Math.Cos((double)(num2 + 90 + 1) / (180.0 / Math.PI)) * (double)num / 2.0));
					num2++;
				}
				while (num2 <= 358);
			}
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), 0, (int)Math.Round((double)num / 2.0), num);
			e.Graphics.DrawLine(pen, 0, (int)Math.Round((double)num / 2.0), num, (int)Math.Round((double)num / 2.0));
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin(Math.PI / 6.0) * (double)num / 2.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos(Math.PI / 6.0) * (double)num / 2.0));
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin(Math.PI / 3.0) * (double)num / 2.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos(Math.PI / 3.0) * (double)num / 2.0));
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin(Math.PI * 2.0 / 3.0) * (double)num / 2.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos(Math.PI * 2.0 / 3.0) * (double)num / 2.0));
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin(Math.PI * 5.0 / 6.0) * (double)num / 2.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos(Math.PI * 5.0 / 6.0) * (double)num / 2.0));
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin(3.665191429188092) * (double)num / 2.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos(3.665191429188092) * (double)num / 2.0));
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin(4.1887902047863905) * (double)num / 2.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos(4.1887902047863905) * (double)num / 2.0));
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin(5.235987755982989) * (double)num / 2.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos(5.235987755982989) * (double)num / 2.0));
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin(5.759586531581287) * (double)num / 2.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos(5.759586531581287) * (double)num / 2.0));
			e.Graphics.DrawEllipse(pen, 0, 0, num, num);
			num3 = 0.66f;
			e.Graphics.DrawEllipse(pen, (int)Math.Round(0.5 * (double)num * (double)(1f - num3)), (int)Math.Round(0.5 * (double)num * (double)(1f - num3)), (int)Math.Round((float)num * num3), (int)Math.Round((float)num * num3));
			e.Graphics.DrawString("-10dB", new Font("Arial", 7f, FontStyle.Regular), new SolidBrush(Color.Blue), (int)Math.Round((double)num / 2.0) - 2, (float)((double)num - 0.5 * (double)num * (double)(1f - num3) + 1.0));
			num3 = 0.33f;
			e.Graphics.DrawEllipse(pen, (int)Math.Round(0.5 * (double)num * (double)(1f - num3)), (int)Math.Round(0.5 * (double)num * (double)(1f - num3)), (int)Math.Round((float)num * num3), (int)Math.Round((float)num * num3));
			e.Graphics.DrawString("-20dB", new Font("Arial", 7f, FontStyle.Regular), new SolidBrush(Color.Blue), (int)Math.Round((double)num / 2.0) - 2, (float)((double)num - 0.5 * (double)num * (double)(1f - num3) + 1.0));
			num3 = 0f;
			e.Graphics.DrawString("-30dB", new Font("Arial", 7f, FontStyle.Regular), new SolidBrush(Color.Blue), (int)Math.Round((double)num / 2.0) - 2, (float)((double)num - 0.5 * (double)num * (double)(1f - num3) + 1.0));
		}
	}

	private void pbDnVert2_Paint(object sender, PaintEventArgs e)
	{
		Pen pen = new Pen(Color.Black, 1f);
		Pen pen2 = new Pen(Color.Red, 2f);
		checked
		{
			int num = Math.Min(pbDnVert2.Width - 5, pbDnVert2.Height - 5);
			float num3;
			if (!FlagAntError)
			{
				int num2 = 0;
				do
				{
					num3 = (30f - Form1_Public.HdnPolNorm_v[num2]) / 30f;
					float num4 = (30f - Form1_Public.HdnPolNorm_v[num2 + 1]) / 30f;
					if (num3 < 0f)
					{
						num3 = 0f;
					}
					if (num4 < 0f)
					{
						num4 = 0f;
					}
					e.Graphics.DrawLine(pen2, Convert.ToSingle((double)num / 2.0 + (double)num3 * Math.Sin((double)(num2 + 90) / (180.0 / Math.PI)) * (double)num / 2.0), Convert.ToSingle((double)num - (double)num / 2.0 - (double)num3 * Math.Cos((double)(num2 + 90) / (180.0 / Math.PI)) * (double)num / 2.0), Convert.ToSingle((double)num / 2.0 + (double)num4 * Math.Sin((double)(num2 + 90 + 1) / (180.0 / Math.PI)) * (double)num / 2.0), Convert.ToSingle((double)num - (double)num / 2.0 - (double)num4 * Math.Cos((double)(num2 + 90 + 1) / (180.0 / Math.PI)) * (double)num / 2.0));
					num2++;
				}
				while (num2 <= 358);
			}
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), 0, (int)Math.Round((double)num / 2.0), num);
			e.Graphics.DrawLine(pen, 0, (int)Math.Round((double)num / 2.0), num, (int)Math.Round((double)num / 2.0));
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin(Math.PI / 6.0) * (double)num / 2.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos(Math.PI / 6.0) * (double)num / 2.0));
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin(Math.PI / 3.0) * (double)num / 2.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos(Math.PI / 3.0) * (double)num / 2.0));
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin(Math.PI * 2.0 / 3.0) * (double)num / 2.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos(Math.PI * 2.0 / 3.0) * (double)num / 2.0));
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin(Math.PI * 5.0 / 6.0) * (double)num / 2.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos(Math.PI * 5.0 / 6.0) * (double)num / 2.0));
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin(3.665191429188092) * (double)num / 2.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos(3.665191429188092) * (double)num / 2.0));
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin(4.1887902047863905) * (double)num / 2.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos(4.1887902047863905) * (double)num / 2.0));
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin(5.235987755982989) * (double)num / 2.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos(5.235987755982989) * (double)num / 2.0));
			e.Graphics.DrawLine(pen, (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0), (int)Math.Round((double)num / 2.0 + Math.Sin(5.759586531581287) * (double)num / 2.0), (int)Math.Round((double)num - (double)num / 2.0 - Math.Cos(5.759586531581287) * (double)num / 2.0));
			e.Graphics.DrawEllipse(pen, 0, 0, num, num);
			num3 = 0.66f;
			e.Graphics.DrawEllipse(pen, (int)Math.Round(0.5 * (double)num * (double)(1f - num3)), (int)Math.Round(0.5 * (double)num * (double)(1f - num3)), (int)Math.Round((float)num * num3), (int)Math.Round((float)num * num3));
			e.Graphics.DrawString("-10dB", new Font("Arial", 7f, FontStyle.Regular), new SolidBrush(Color.Blue), (int)Math.Round((double)num / 2.0) - 2, (float)((double)num - 0.5 * (double)num * (double)(1f - num3) + 1.0));
			num3 = 0.33f;
			e.Graphics.DrawEllipse(pen, (int)Math.Round(0.5 * (double)num * (double)(1f - num3)), (int)Math.Round(0.5 * (double)num * (double)(1f - num3)), (int)Math.Round((float)num * num3), (int)Math.Round((float)num * num3));
			e.Graphics.DrawString("-20dB", new Font("Arial", 7f, FontStyle.Regular), new SolidBrush(Color.Blue), (int)Math.Round((double)num / 2.0) - 2, (float)((double)num - 0.5 * (double)num * (double)(1f - num3) + 1.0));
			num3 = 0f;
			e.Graphics.DrawString("-30dB", new Font("Arial", 7f, FontStyle.Regular), new SolidBrush(Color.Blue), (int)Math.Round((double)num / 2.0) - 2, (float)((double)num - 0.5 * (double)num * (double)(1f - num3) + 1.0));
		}
	}
}
