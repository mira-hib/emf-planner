using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using EMFPlanner.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[DesignerGenerated]
public class UC_Relief : UserControl
{
	private IContainer components;

	private bool FlagOK;

	[field: AccessedThroughProperty("GroupBox1")]
	internal virtual GroupBox GroupBox1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdRedo")]
	internal virtual Button cmdRedo
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdRedo_Click;
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

	[field: AccessedThroughProperty("cmdUndo")]
	internal virtual Button cmdUndo
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdUndo_Click;
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

	[field: AccessedThroughProperty("cmdAddCopyObject")]
	internal virtual Button cmdAddCopyObject
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = cmdAddCopyObject_MouseClick;
			Button button = field;
			if (button != null)
			{
				button.MouseClick -= value2;
			}
			field = value;
			button = field;
			if (button != null)
			{
				button.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmdDelOneObject")]
	internal virtual Button cmdDelOneObject
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdDelOneObject_Click;
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

	[field: AccessedThroughProperty("gbElev")]
	internal virtual GroupBox gbElev
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdDelAllObjects")]
	internal virtual Button cmdDelAllObjects
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdDelAllObjects_Click;
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

	[field: AccessedThroughProperty("cmdSelect")]
	internal virtual Button cmdSelect
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdSelect_Click;
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

	[field: AccessedThroughProperty("cmdAddPoint")]
	internal virtual Button cmdAddPoint
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = cmdAddPoint_MouseClick;
			Button button = field;
			if (button != null)
			{
				button.MouseClick -= value2;
			}
			field = value;
			button = field;
			if (button != null)
			{
				button.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmdAddLine")]
	internal virtual Button cmdAddLine
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = cmdAddLine_MouseClick;
			Button button = field;
			if (button != null)
			{
				button.MouseClick -= value2;
			}
			field = value;
			button = field;
			if (button != null)
			{
				button.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmdDeleteNode")]
	internal virtual Button cmdDeleteNode
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = cmdDeleteNode_MouseClick;
			Button button = field;
			if (button != null)
			{
				button.MouseClick -= value2;
			}
			field = value;
			button = field;
			if (button != null)
			{
				button.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmdAddNode")]
	internal virtual Button cmdAddNode
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = cmdAddNode_MouseClick;
			Button button = field;
			if (button != null)
			{
				button.MouseClick -= value2;
			}
			field = value;
			button = field;
			if (button != null)
			{
				button.MouseClick += value2;
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

	[field: AccessedThroughProperty("Label1")]
	internal virtual Label Label1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtHeight")]
	internal virtual TextBox txtHeight
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			KeyEventHandler value2 = txtHeight_KeyUp;
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

	[field: AccessedThroughProperty("chkCloseLine")]
	internal virtual CheckBox chkCloseLine
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = chkCloseLine_MouseClick;
			CheckBox checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick -= value2;
			}
			field = value;
			checkBox = field;
			if (checkBox != null)
			{
				checkBox.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmdDelOneBuilding")]
	internal virtual Button cmdDelOneBuilding
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdTriangle")]
	internal virtual Button cmdTriangle
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdTriangle_Click;
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

	[field: AccessedThroughProperty("ToolTip1")]
	internal virtual ToolTip ToolTip1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdSRTM")]
	internal virtual Button cmdSRTM
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdSRTM_Click;
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

	public UC_Relief()
	{
		Load += US_Relief_Load;
		FlagOK = false;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EMFPlanner.UC_Relief));
		this.GroupBox1 = new System.Windows.Forms.GroupBox();
		this.cmdSRTM = new System.Windows.Forms.Button();
		this.cmdTriangle = new System.Windows.Forms.Button();
		this.cmdDeleteNode = new System.Windows.Forms.Button();
		this.cmdAddNode = new System.Windows.Forms.Button();
		this.cmdRedo = new System.Windows.Forms.Button();
		this.cmdUndo = new System.Windows.Forms.Button();
		this.cmdAddCopyObject = new System.Windows.Forms.Button();
		this.cmdDelOneObject = new System.Windows.Forms.Button();
		this.gbElev = new System.Windows.Forms.GroupBox();
		this.chkCloseLine = new System.Windows.Forms.CheckBox();
		this.Label1 = new System.Windows.Forms.Label();
		this.txtHeight = new System.Windows.Forms.TextBox();
		this.Label3 = new System.Windows.Forms.Label();
		this.cmdDelAllObjects = new System.Windows.Forms.Button();
		this.cmdSelect = new System.Windows.Forms.Button();
		this.cmdAddPoint = new System.Windows.Forms.Button();
		this.cmdAddLine = new System.Windows.Forms.Button();
		this.cmdDelOneBuilding = new System.Windows.Forms.Button();
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.GroupBox1.SuspendLayout();
		this.gbElev.SuspendLayout();
		base.SuspendLayout();
		this.GroupBox1.Controls.Add(this.cmdSRTM);
		this.GroupBox1.Controls.Add(this.cmdTriangle);
		this.GroupBox1.Controls.Add(this.cmdDeleteNode);
		this.GroupBox1.Controls.Add(this.cmdAddNode);
		this.GroupBox1.Controls.Add(this.cmdRedo);
		this.GroupBox1.Controls.Add(this.cmdUndo);
		this.GroupBox1.Controls.Add(this.cmdAddCopyObject);
		this.GroupBox1.Controls.Add(this.cmdDelOneObject);
		this.GroupBox1.Controls.Add(this.gbElev);
		this.GroupBox1.Controls.Add(this.cmdDelAllObjects);
		this.GroupBox1.Controls.Add(this.cmdSelect);
		this.GroupBox1.Controls.Add(this.cmdAddPoint);
		this.GroupBox1.Controls.Add(this.cmdAddLine);
		this.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GroupBox1.Location = new System.Drawing.Point(0, 0);
		this.GroupBox1.Name = "GroupBox1";
		this.GroupBox1.Size = new System.Drawing.Size(300, 728);
		this.GroupBox1.TabIndex = 3;
		this.GroupBox1.TabStop = false;
		this.GroupBox1.Text = "Terrain";
		this.cmdSRTM.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdSRTM.Font = new System.Drawing.Font("Microsoft Sans Serif", 5f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
		this.cmdSRTM.Image = (System.Drawing.Image)resources.GetObject("cmdSRTM.Image");
		this.cmdSRTM.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdSRTM.Location = new System.Drawing.Point(98, 25);
		this.cmdSRTM.Name = "cmdSRTM";
		this.cmdSRTM.Size = new System.Drawing.Size(26, 26);
		this.cmdSRTM.TabIndex = 170;
		this.cmdSRTM.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdSRTM, "Use DEM data as terrain elevation");
		this.cmdSRTM.UseVisualStyleBackColor = true;
		this.cmdTriangle.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdTriangle.Image = (System.Drawing.Image)resources.GetObject("cmdTriangle.Image");
		this.cmdTriangle.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdTriangle.Location = new System.Drawing.Point(260, 25);
		this.cmdTriangle.Name = "cmdTriangle";
		this.cmdTriangle.Size = new System.Drawing.Size(26, 26);
		this.cmdTriangle.TabIndex = 169;
		this.cmdTriangle.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdTriangle, "Show triangulation result");
		this.cmdTriangle.UseVisualStyleBackColor = true;
		this.cmdDeleteNode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdDeleteNode.Image = (System.Drawing.Image)resources.GetObject("cmdDeleteNode.Image");
		this.cmdDeleteNode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdDeleteNode.Location = new System.Drawing.Point(44, 57);
		this.cmdDeleteNode.Name = "cmdDeleteNode";
		this.cmdDeleteNode.Size = new System.Drawing.Size(26, 26);
		this.cmdDeleteNode.TabIndex = 168;
		this.cmdDeleteNode.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdDeleteNode, "Delete node on selected elevation contour");
		this.cmdDeleteNode.UseVisualStyleBackColor = true;
		this.cmdAddNode.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdAddNode.Image = (System.Drawing.Image)resources.GetObject("cmdAddNode.Image");
		this.cmdAddNode.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdAddNode.Location = new System.Drawing.Point(17, 57);
		this.cmdAddNode.Name = "cmdAddNode";
		this.cmdAddNode.Size = new System.Drawing.Size(26, 26);
		this.cmdAddNode.TabIndex = 167;
		this.cmdAddNode.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdAddNode, "Add node on selected elevation contour");
		this.cmdAddNode.UseVisualStyleBackColor = true;
		this.cmdRedo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdRedo.Image = (System.Drawing.Image)resources.GetObject("cmdRedo.Image");
		this.cmdRedo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdRedo.Location = new System.Drawing.Point(233, 25);
		this.cmdRedo.Name = "cmdRedo";
		this.cmdRedo.Size = new System.Drawing.Size(26, 26);
		this.cmdRedo.TabIndex = 158;
		this.cmdRedo.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdRedo, "Redo");
		this.cmdRedo.UseVisualStyleBackColor = true;
		this.cmdUndo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdUndo.Image = (System.Drawing.Image)resources.GetObject("cmdUndo.Image");
		this.cmdUndo.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdUndo.Location = new System.Drawing.Point(206, 25);
		this.cmdUndo.Name = "cmdUndo";
		this.cmdUndo.Size = new System.Drawing.Size(26, 26);
		this.cmdUndo.TabIndex = 157;
		this.cmdUndo.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdUndo, "Undo");
		this.cmdUndo.UseVisualStyleBackColor = true;
		this.cmdAddCopyObject.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdAddCopyObject.Image = (System.Drawing.Image)resources.GetObject("cmdAddCopyObject.Image");
		this.cmdAddCopyObject.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdAddCopyObject.Location = new System.Drawing.Point(125, 25);
		this.cmdAddCopyObject.Name = "cmdAddCopyObject";
		this.cmdAddCopyObject.Size = new System.Drawing.Size(26, 26);
		this.cmdAddCopyObject.TabIndex = 156;
		this.cmdAddCopyObject.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdAddCopyObject, "Create a copy of the selected object");
		this.cmdAddCopyObject.UseVisualStyleBackColor = true;
		this.cmdDelOneObject.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdDelOneObject.Image = (System.Drawing.Image)resources.GetObject("cmdDelOneObject.Image");
		this.cmdDelOneObject.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdDelOneObject.Location = new System.Drawing.Point(152, 25);
		this.cmdDelOneObject.Name = "cmdDelOneObject";
		this.cmdDelOneObject.Size = new System.Drawing.Size(26, 26);
		this.cmdDelOneObject.TabIndex = 155;
		this.cmdDelOneObject.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdDelOneObject, "Delete selected object");
		this.cmdDelOneObject.UseVisualStyleBackColor = true;
		this.gbElev.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.gbElev.Controls.Add(this.chkCloseLine);
		this.gbElev.Controls.Add(this.Label1);
		this.gbElev.Controls.Add(this.txtHeight);
		this.gbElev.Controls.Add(this.Label3);
		this.gbElev.Location = new System.Drawing.Point(3, 98);
		this.gbElev.Name = "gbElev";
		this.gbElev.Size = new System.Drawing.Size(294, 106);
		this.gbElev.TabIndex = 154;
		this.gbElev.TabStop = false;
		this.gbElev.Text = "Object parameters";
		this.gbElev.Visible = false;
		this.chkCloseLine.AutoSize = true;
		this.chkCloseLine.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
		this.chkCloseLine.Location = new System.Drawing.Point(14, 72);
		this.chkCloseLine.Name = "chkCloseLine";
		this.chkCloseLine.Size = new System.Drawing.Size(137, 17);
		this.chkCloseLine.TabIndex = 178;
		this.chkCloseLine.TabStop = false;
		this.chkCloseLine.Text = "Close elevation contour";
		this.chkCloseLine.UseVisualStyleBackColor = true;
		this.Label1.AutoSize = true;
		this.Label1.Location = new System.Drawing.Point(194, 37);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(15, 13);
		this.Label1.TabIndex = 177;
		this.Label1.Text = "m";
		this.txtHeight.Location = new System.Drawing.Point(137, 34);
		this.txtHeight.Name = "txtHeight";
		this.txtHeight.Size = new System.Drawing.Size(51, 20);
		this.txtHeight.TabIndex = 176;
		this.Label3.AutoSize = true;
		this.Label3.Location = new System.Drawing.Point(80, 37);
		this.Label3.Name = "Label3";
		this.Label3.Size = new System.Drawing.Size(51, 13);
		this.Label3.TabIndex = 175;
		this.Label3.Text = "Elevation";
		this.cmdDelAllObjects.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdDelAllObjects.Image = (System.Drawing.Image)resources.GetObject("cmdDelAllObjects.Image");
		this.cmdDelAllObjects.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdDelAllObjects.Location = new System.Drawing.Point(179, 25);
		this.cmdDelAllObjects.Name = "cmdDelAllObjects";
		this.cmdDelAllObjects.Size = new System.Drawing.Size(26, 26);
		this.cmdDelAllObjects.TabIndex = 152;
		this.cmdDelAllObjects.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdDelAllObjects, "Delete all objects");
		this.cmdDelAllObjects.UseVisualStyleBackColor = true;
		this.cmdSelect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdSelect.Image = (System.Drawing.Image)resources.GetObject("cmdSelect.Image");
		this.cmdSelect.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdSelect.Location = new System.Drawing.Point(17, 25);
		this.cmdSelect.Name = "cmdSelect";
		this.cmdSelect.Size = new System.Drawing.Size(26, 26);
		this.cmdSelect.TabIndex = 151;
		this.cmdSelect.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdSelect, "Select object (elevation point or elevation contour)");
		this.cmdSelect.UseVisualStyleBackColor = true;
		this.cmdAddPoint.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdAddPoint.Image = (System.Drawing.Image)resources.GetObject("cmdAddPoint.Image");
		this.cmdAddPoint.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdAddPoint.Location = new System.Drawing.Point(44, 25);
		this.cmdAddPoint.Name = "cmdAddPoint";
		this.cmdAddPoint.Size = new System.Drawing.Size(26, 26);
		this.cmdAddPoint.TabIndex = 150;
		this.cmdAddPoint.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdAddPoint, "Add elevation point");
		this.cmdAddPoint.UseVisualStyleBackColor = true;
		this.cmdAddLine.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdAddLine.Image = (System.Drawing.Image)resources.GetObject("cmdAddLine.Image");
		this.cmdAddLine.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdAddLine.Location = new System.Drawing.Point(71, 25);
		this.cmdAddLine.Name = "cmdAddLine";
		this.cmdAddLine.Size = new System.Drawing.Size(26, 26);
		this.cmdAddLine.TabIndex = 149;
		this.cmdAddLine.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdAddLine, "Add elevation contour");
		this.cmdAddLine.UseVisualStyleBackColor = true;
		this.cmdDelOneBuilding.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdDelOneBuilding.Image = (System.Drawing.Image)resources.GetObject("cmdDelOneBuilding.Image");
		this.cmdDelOneBuilding.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdDelOneBuilding.Location = new System.Drawing.Point(125, 25);
		this.cmdDelOneBuilding.Name = "cmdDelOneBuilding";
		this.cmdDelOneBuilding.Size = new System.Drawing.Size(26, 26);
		this.cmdDelOneBuilding.TabIndex = 155;
		this.cmdDelOneBuilding.TabStop = false;
		this.cmdDelOneBuilding.UseVisualStyleBackColor = true;
		base.AutoScaleDimensions = new System.Drawing.SizeF(6f, 13f);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		base.Controls.Add(this.GroupBox1);
		this.MinimumSize = new System.Drawing.Size(300, 0);
		base.Name = "UC_Relief";
		base.Size = new System.Drawing.Size(300, 728);
		this.GroupBox1.ResumeLayout(false);
		this.gbElev.ResumeLayout(false);
		this.gbElev.PerformLayout();
		base.ResumeLayout(false);
	}

	private void US_Relief_Load(object sender, EventArgs e)
	{
		Form1_Public.ShowTriangles = false;
		FlagOK = true;
		MyProject.Forms.Form1.DrawPIC();
	}

	private void cmdSelect_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		Form1_Public.ShowTriangles = false;
		cmdAddLine.BackColor = SystemColors.Control;
		cmdAddPoint.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingElevLine = false;
		Form1_Public.StartDrawingElevPoint = false;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartAddingNodes = false;
		MyProject.Forms.Form1.IndexElevLine = -1;
		MyProject.Forms.Form1.IndexElevPoint = -1;
		Form1_Public.NewElevLine = default;
		Form1_Public.NewElevPoint = default;
		gbElev.Visible = false;
		MyProject.Forms.Form1.PB.Refresh();
	}

	private void cmdAddLine_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		Form1_Public.ShowTriangles = false;
		cmdAddPoint.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingElevPoint = false;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartAddingNodes = false;
		MyProject.Forms.Form1.IndexElevLine = -1;
		MyProject.Forms.Form1.IndexElevPoint = -1;
		Form1_Public.NewElevLine = default;
		Form1_Public.NewElevPoint = default;
		if (Form1_Public.StartDrawingElevLine)
		{
			cmdAddLine.BackColor = SystemColors.Control;
			Form1_Public.StartDrawingElevLine = false;
		}
		else
		{
			cmdAddLine.BackColor = SystemColors.ControlDark;
			Form1_Public.StartDrawingElevLine = true;
		}
		MyProject.Forms.Form1.PB.Refresh();
		gbElev.Visible = false;
	}

	private void cmdAddPoint_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		Form1_Public.ShowTriangles = false;
		cmdAddLine.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingElevLine = false;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartAddingNodes = false;
		MyProject.Forms.Form1.IndexElevLine = -1;
		MyProject.Forms.Form1.IndexElevPoint = -1;
		Form1_Public.NewElevLine = default;
		Form1_Public.NewElevPoint = default;
		if (Form1_Public.StartDrawingElevLine)
		{
			cmdAddPoint.BackColor = SystemColors.Control;
			Form1_Public.StartDrawingElevPoint = false;
		}
		else
		{
			cmdAddPoint.BackColor = SystemColors.ControlDark;
			Form1_Public.StartDrawingElevPoint = true;
		}
		MyProject.Forms.Form1.PB.Refresh();
		gbElev.Visible = false;
	}

	private void cmdDeleteNode_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		Form1_Public.ShowTriangles = false;
		cmdAddLine.BackColor = SystemColors.Control;
		cmdAddPoint.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingElevLine = false;
		Form1_Public.StartDrawingElevPoint = false;
		Form1_Public.StartAddingNodes = false;
		if (MyProject.Forms.Form1.IndexElevLine < 0)
		{
			Interaction.MsgBox(Localization.LocMSG(32), MsgBoxStyle.Information);
			return;
		}
		if (Form1_Public.StartDeletingNodes)
		{
			cmdDeleteNode.BackColor = SystemColors.Control;
			Form1_Public.StartDeletingNodes = false;
		}
		else
		{
			cmdDeleteNode.BackColor = SystemColors.ControlDark;
			Form1_Public.StartDeletingNodes = true;
		}
		MyProject.Forms.Form1.PB.Refresh();
	}

	private void cmdAddNode_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		Form1_Public.ShowTriangles = false;
		if (MyProject.Forms.Form1.IndexElevLine < 0)
		{
			Interaction.MsgBox(Localization.LocMSG(32), MsgBoxStyle.Information);
			return;
		}
		cmdAddLine.BackColor = SystemColors.Control;
		cmdAddPoint.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingElevLine = false;
		Form1_Public.StartDrawingElevPoint = false;
		Form1_Public.StartDeletingNodes = false;
		if (Form1_Public.StartAddingNodes)
		{
			cmdAddNode.BackColor = SystemColors.Control;
			Form1_Public.StartAddingNodes = false;
		}
		else
		{
			cmdAddNode.BackColor = SystemColors.ControlDark;
			Form1_Public.StartAddingNodes = true;
		}
		MyProject.Forms.Form1.PB.Refresh();
	}

	private void cmdAddCopyObject_MouseClick(object sender, MouseEventArgs e)
	{
		GroupBox1.Focus();
		Form1_Public.ShowTriangles = false;
		cmdAddLine.BackColor = SystemColors.Control;
		cmdAddPoint.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingElevLine = false;
		Form1_Public.StartDrawingElevPoint = false;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartAddingNodes = false;
		checked
		{
			if (MyProject.Forms.Form1.IndexElevLine > -1)
			{
				ref Public2.StructElevLine[] elLN = ref Public2.PRJ.ElLN;
				elLN = (Public2.StructElevLine[])Utils.CopyArray(elLN, new Public2.StructElevLine[Public2.PRJ.ElLN.GetUpperBound(0) + 1 + 1]);
				Public2.PRJ.ElLN[Public2.PRJ.ElLN.GetUpperBound(0)] = Module1.DeepClone(Public2.PRJ.ElLN[MyProject.Forms.Form1.IndexElevLine]);
				int upperBound = Public2.PRJ.ElLN[Public2.PRJ.ElLN.GetUpperBound(0)].CoordWM.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					Public2.PRJ.ElLN[Public2.PRJ.ElLN.GetUpperBound(0)].CoordWM[i].Xwm += 20.0;
					Public2.PRJ.ElLN[Public2.PRJ.ElLN.GetUpperBound(0)].CoordWM[i].Ywm -= 20.0;
				}
				MyProject.Forms.Form1.IndexElevLine = (short)Public2.PRJ.ElLN.GetUpperBound(0);
				MyProject.Forms.Form1.PB.Refresh();
				txtHeight.Text = Conversions.ToString(Public2.PRJ.ElLN[MyProject.Forms.Form1.IndexElevLine].H);
				chkCloseLine.Checked = Public2.PRJ.ElLN[MyProject.Forms.Form1.IndexElevLine].CloseLine;
				Module1.ElevBackUp();
			}
			else if (MyProject.Forms.Form1.IndexElevPoint > -1)
			{
				ref Public2.StructElevPoint[] elPT = ref Public2.PRJ.ElPT;
				elPT = (Public2.StructElevPoint[])Utils.CopyArray(elPT, new Public2.StructElevPoint[Public2.PRJ.ElPT.GetUpperBound(0) + 1 + 1]);
				Public2.PRJ.ElPT[Public2.PRJ.ElPT.GetUpperBound(0)] = Module1.DeepClone(Public2.PRJ.ElPT[MyProject.Forms.Form1.IndexElevPoint]);
				Public2.PRJ.ElPT[Public2.PRJ.ElPT.GetUpperBound(0)].CoordWM.Xwm += 20.0;
				Public2.PRJ.ElPT[Public2.PRJ.ElPT.GetUpperBound(0)].CoordWM.Ywm -= 20.0;
				MyProject.Forms.Form1.IndexElevPoint = (short)Public2.PRJ.ElPT.GetUpperBound(0);
				MyProject.Forms.Form1.PB.Refresh();
				txtHeight.Text = Conversions.ToString(Public2.PRJ.ElPT[MyProject.Forms.Form1.IndexElevPoint].H);
				chkCloseLine.Visible = false;
				Module1.ElevBackUp();
			}
			else
			{
				Interaction.MsgBox(Localization.LocMSG(33), MsgBoxStyle.Information);
			}
		}
	}

	private void cmdDelOneObject_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		Form1_Public.ShowTriangles = false;
		checked
		{
			if ((MyProject.Forms.Form1.IndexElevLine < 0) & (MyProject.Forms.Form1.IndexElevPoint < 0))
			{
				Interaction.MsgBox(Localization.LocMSG(33), MsgBoxStyle.Information);
			}
			else
			{
				if (Interaction.MsgBox(Localization.LocMSG(36), MsgBoxStyle.YesNo) != MsgBoxResult.Yes)
				{
					return;
				}
				cmdAddLine.BackColor = SystemColors.Control;
				cmdAddPoint.BackColor = SystemColors.Control;
				cmdDeleteNode.BackColor = SystemColors.Control;
				cmdAddNode.BackColor = SystemColors.Control;
				Form1_Public.StartDrawingElevLine = false;
				Form1_Public.StartDrawingElevPoint = false;
				Form1_Public.StartDeletingNodes = false;
				Form1_Public.StartAddingNodes = false;
				gbElev.Visible = false;
				if (MyProject.Forms.Form1.IndexElevLine > -1)
				{
					short indexElevLine = MyProject.Forms.Form1.IndexElevLine;
					int num = Public2.PRJ.ElLN.GetUpperBound(0) - 1;
					for (int i = indexElevLine; i <= num; i++)
					{
						Public2.PRJ.ElLN[i] = Module1.DeepClone(Public2.PRJ.ElLN[i + 1]);
					}
					ref Public2.StructElevLine[] elLN = ref Public2.PRJ.ElLN;
					elLN = (Public2.StructElevLine[])Utils.CopyArray(elLN, new Public2.StructElevLine[Public2.PRJ.ElLN.GetUpperBound(0) - 1 + 1]);
					if (Public2.PRJ.ElLN.Length == 0)
					{
						Public2.PRJ.ElLN = null;
					}
				}
				else if (MyProject.Forms.Form1.IndexElevPoint > -1)
				{
					short indexElevPoint = MyProject.Forms.Form1.IndexElevPoint;
					int num2 = Public2.PRJ.ElPT.GetUpperBound(0) - 1;
					for (int j = indexElevPoint; j <= num2; j++)
					{
						Public2.PRJ.ElPT[j] = Module1.DeepClone(Public2.PRJ.ElPT[j + 1]);
					}
					ref Public2.StructElevPoint[] elPT = ref Public2.PRJ.ElPT;
					elPT = (Public2.StructElevPoint[])Utils.CopyArray(elPT, new Public2.StructElevPoint[Public2.PRJ.ElPT.GetUpperBound(0) - 1 + 1]);
					if (Public2.PRJ.ElPT.Length == 0)
					{
						Public2.PRJ.ElPT = null;
					}
				}
				MyProject.Forms.Form1.IndexElevLine = -1;
				MyProject.Forms.Form1.IndexElevPoint = -1;
				MyProject.Forms.Form1.PB.Refresh();
			}
		}
	}

	private void cmdDelAllObjects_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		Form1_Public.ShowTriangles = false;
		if (Interaction.MsgBox(Localization.LocMSG(35), MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
		{
			cmdAddLine.BackColor = SystemColors.Control;
			cmdAddPoint.BackColor = SystemColors.Control;
			cmdDeleteNode.BackColor = SystemColors.Control;
			cmdAddNode.BackColor = SystemColors.Control;
			Form1_Public.StartDrawingElevLine = false;
			Form1_Public.StartDrawingElevPoint = false;
			Form1_Public.StartDeletingNodes = false;
			Form1_Public.StartAddingNodes = false;
			MyProject.Forms.Form1.IndexElevLine = -1;
			MyProject.Forms.Form1.IndexElevPoint = -1;
			gbElev.Visible = false;
			Module1.ElevBackUp();
			Public2.PRJ.ElLN = null;
			Public2.PRJ.ElPT = null;
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void cmdUndo_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		Form1_Public.ShowTriangles = false;
		cmdAddLine.BackColor = SystemColors.Control;
		cmdAddPoint.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingElevLine = false;
		Form1_Public.StartDrawingElevPoint = false;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartAddingNodes = false;
		MyProject.Forms.Form1.IndexElevLine = -1;
		MyProject.Forms.Form1.IndexElevPoint = -1;
		Form1_Public.NewElevLine = default;
		Form1_Public.NewElevPoint = default;
		gbElev.Visible = false;
		MyProject.Forms.Form1.PB.Refresh();
		Module1.ElevRestore();
		MyProject.Forms.Form1.PB.Refresh();
	}

	private void cmdRedo_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		Form1_Public.ShowTriangles = false;
		cmdAddLine.BackColor = SystemColors.Control;
		cmdAddPoint.BackColor = SystemColors.Control;
		cmdDeleteNode.BackColor = SystemColors.Control;
		cmdAddNode.BackColor = SystemColors.Control;
		Form1_Public.StartDrawingElevLine = false;
		Form1_Public.StartDrawingElevPoint = false;
		Form1_Public.StartDeletingNodes = false;
		Form1_Public.StartAddingNodes = false;
		MyProject.Forms.Form1.IndexElevLine = -1;
		MyProject.Forms.Form1.IndexElevPoint = -1;
		Form1_Public.NewElevLine = default;
		Form1_Public.NewElevPoint = default;
		gbElev.Visible = false;
		MyProject.Forms.Form1.PB.Refresh();
		Module1.ElevForward();
		MyProject.Forms.Form1.PB.Refresh();
	}

	private void cmdTriangle_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		Triangul.CalcTriangulate();
		Form1_Public.ShowTriangles = true;
		MyProject.Forms.Form1.PB.Refresh();
	}

	private void CmdSRTM_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Interaction.MsgBox(Localization.LocMSG(34), MsgBoxStyle.YesNo) != MsgBoxResult.Yes)
		{
			return;
		}
		Public2.PRJ.ElLN = null;
		Public2.PRJ.ElPT = null;
		Public2.CoordWGS coordWGS = default;
		CalcCoords.ConvWebMercMetersToWGS84(Public2.PRJ.SETT.HorAreaCoordWM[0], ref coordWGS.Lat, ref coordWGS.Lon);
		Public2.CoordWGS coordWGS2 = default;
		CalcCoords.ConvWebMercMetersToWGS84(Public2.PRJ.SETT.HorAreaCoordWM[1], ref coordWGS2.Lat, ref coordWGS2.Lon);
		checked
		{
			short num = (short)Math.Floor(coordWGS.Lat);
			short num2 = (short)Math.Floor(coordWGS2.Lat);
			short num3 = (short)Math.Floor(coordWGS.Lon);
			short num4 = (short)Math.Floor(coordWGS2.Lon);
			int num5 = 0;
			short num6 = num2;
			Public2.CoordWebMerc CoordWM = default;
			Public2.CoordWebMerc CoordWM2 = default;
			for (short num7 = num; num7 <= num6; num7 = (short)unchecked(num7 + 1))
			{
				short num8 = num3;
				short num9 = num4;
				for (short num10 = num8; num10 <= num9; num10 = (short)unchecked(num10 + 1))
				{
					string text = Public2.AppDataFolder + "\\SRTM\\N" + Strings.Format(num7, "00") + "E" + Strings.Format(num10, "000") + ".hgt";
					if (!MyProject.Computer.FileSystem.FileExists(text))
					{
						if (!MyProject.Computer.FileSystem.DirectoryExists(Public2.AppDataFolder + "\\SRTM"))
						{
							Directory.CreateDirectory(Public2.AppDataFolder + "\\SRTM");
						}
						WebClient webClient = new WebClient();
						webClient.UseDefaultCredentials = true;
						WebProxy webProxy = new WebProxy();
						if (Public2.AppSetup.FlagProxy)
						{
							if (Public2.AppSetup.FlagProxyLogin)
							{
								webProxy.UseDefaultCredentials = false;
								webProxy.Credentials = new NetworkCredential(Public2.AppSetup.ProxyLogin, Public2.AppSetup.ProxyPassword);
							}
							else
							{
								webProxy.UseDefaultCredentials = true;
							}
							Uri address = new Uri("http://" + Public2.AppSetup.ProxyIP + ":" + Conversions.ToString(Public2.AppSetup.ProxyPort));
							webProxy.Address = address;
							webClient.Proxy = webProxy;
						}
						else
						{
							webClient.Proxy = WebRequest.GetSystemWebProxy();
						}
						webClient.Headers.Add("User-Agent", "SZ\r\n");
						try
						{
							webClient.DownloadFile(Constants.UrlROOT + "/HGT/N" + Strings.Format(num7, "00") + "/N" + Strings.Format(num7, "00") + "E" + Strings.Format(num10, "000") + ".zip", Public2.AppDataFolder + "\\SRTM\\N" + Strings.Format(num7, "00") + "E" + Strings.Format(num10, "000") + ".zip");
						}
						catch (Exception projectError)
						{
							ProjectData.SetProjectError(projectError);
							webClient.Dispose();
							try
							{
								File.Delete(Public2.AppDataFolder + "\\SRTM\\N" + Strings.Format(num7, "00") + "E" + Strings.Format(num10, "000") + ".zip");
							}
							catch (Exception projectError2)
							{
								ProjectData.SetProjectError(projectError2);
								ProjectData.ClearProjectError();
							}
							ProjectData.ClearProjectError();
							return;
						}
						webClient.Dispose();
						if (MyProject.Computer.FileSystem.FileExists(Public2.AppDataFolder + "\\SRTM\\N" + Strings.Format(num7, "00") + "E" + Strings.Format(num10, "000") + ".zip") && FileSystem.FileLen(Public2.AppDataFolder + "\\SRTM\\N" + Strings.Format(num7, "00") + "E" + Strings.Format(num10, "000") + ".zip") == 0L)
						{
							File.Delete(Public2.AppDataFolder + "\\SRTM\\N" + Strings.Format(num7, "00") + "E" + Strings.Format(num10, "000") + ".zip");
							return;
						}
						object objectValue = RuntimeHelpers.GetObjectValue(Activator.CreateInstance(Type.GetTypeFromProgID("Shell.Application")));
						object objectValue2 = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "NameSpace", new object[1] { Public2.AppDataFolder + "\\SRTM" }, null, null, null));
						object objectValue3 = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "NameSpace", new object[1] { Public2.AppDataFolder + "\\SRTM\\N" + Strings.Format(num7, "00") + "E" + Strings.Format(num10, "000") + ".zip" }, null, null, null));
						NewLateBinding.LateCall(objectValue2, null, "CopyHere", new object[2]
						{
							NewLateBinding.LateGet(objectValue3, null, "Items", new object[0], null, null, null),
							4
						}, null, null, null, IgnoreReturn: true);
						File.Delete(Public2.AppDataFolder + "\\SRTM\\N" + Strings.Format(num7, "00") + "E" + Strings.Format(num10, "000") + ".zip");
					}
					byte[] array = null;
					try
					{
						FileStream fileStream = new FileStream(text, FileMode.Open);
						BinaryReader binaryReader = new BinaryReader(fileStream);
						array = new byte[(int)fileStream.Length + 1];
						array = binaryReader.ReadBytes((int)fileStream.Length);
					}
					catch (Exception projectError3)
					{
						ProjectData.SetProjectError(projectError3);
						ProjectData.ClearProjectError();
						return;
					}
					if (array.Length == 25934402)
					{
						int num11 = 0;
						do
						{
							int num12 = 0;
							do
							{
								double num13 = (double)num7 + (double)num11 / 3600.0;
								double num14 = (double)num10 + (double)num12 / 3600.0;
								if ((num13 >= coordWGS.Lat - 0.0003) & (num13 < coordWGS2.Lat + 0.0003) & (num14 >= coordWGS.Lon - 0.0003) & (num14 < coordWGS2.Lon + 0.0003))
								{
									long num15 = 3601 - num11;
									long num16 = num12 + 1;
									CalcCoords.ConvWGS84toWebMercMeters(num13, num14, ref CoordWM);
									ref Public2.StructElevPoint[] elPT = ref Public2.PRJ.ElPT;
									elPT = (Public2.StructElevPoint[])Utils.CopyArray(elPT, new Public2.StructElevPoint[num5 + 1]);
									Public2.PRJ.ElPT[num5].H = (float)(Conversion.Val(array[(int)((num15 - 1) * 7202 + (num16 - 1) * 2)]) * 256.0 + Conversion.Val(array[(int)((num15 - 1) * 7202 + (num16 - 1) * 2 + 1)]));
									if (Public2.PRJ.ElPT[num5].H > 60000f)
									{
										Public2.PRJ.ElPT[num5].H = 0f - (65536f - Public2.PRJ.ElPT[num5].H);
									}
									Public2.PRJ.ElPT[num5].CoordWM = CoordWM;
									num5++;
								}
								num12++;
							}
							while (num12 <= 3599);
							num11++;
						}
						while (num11 <= 3599);
					}
					else if (array.Length == 2884802)
					{
						int num17 = 0;
						do
						{
							int num18 = 0;
							do
							{
								double num19 = (double)num7 + (double)num17 / 1200.0;
								double num20 = (double)num10 + (double)num18 / 1200.0;
								if ((num19 >= coordWGS.Lat) & (num19 < coordWGS2.Lat) & (num20 >= coordWGS.Lon) & (num20 < coordWGS2.Lon))
								{
									long num21 = 1201 - num17;
									long num22 = num18 + 1;
									CalcCoords.ConvWGS84toWebMercMeters(num19, num20, ref CoordWM2);
									ref Public2.StructElevPoint[] elPT2 = ref Public2.PRJ.ElPT;
									elPT2 = (Public2.StructElevPoint[])Utils.CopyArray(elPT2, new Public2.StructElevPoint[num5 + 1]);
									Public2.PRJ.ElPT[num5].H = (float)(Conversion.Val(array[(int)((num21 - 1) * 2402 + (num22 - 1) * 2)]) * 256.0 + Conversion.Val(array[(int)((num21 - 1) * 2402 + (num22 - 1) * 2 + 1)]));
									if (Public2.PRJ.ElPT[num5].H > 60000f)
									{
										Public2.PRJ.ElPT[num5].H = 0f - (65536f - Public2.PRJ.ElPT[num5].H);
									}
									Public2.PRJ.ElPT[num5].CoordWM = CoordWM2;
									num5++;
								}
								num18++;
							}
							while (num18 <= 1199);
							num17++;
						}
						while (num17 <= 1199);
					}
				}
			}
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void txtHeight_KeyUp(object sender, KeyEventArgs e)
	{
		Form1_Public.ShowTriangles = false;
		try
		{
			if (MyProject.Forms.Form1.IndexElevLine > -1)
			{
				if (Module1.ValidText(txtHeight.Text, CheckLimits: false, -999f, 999f, IncludeMin: true, IncludeMax: true, Message: false))
				{
					Public2.PRJ.ElLN[MyProject.Forms.Form1.IndexElevLine].H = Conversions.ToSingle(txtHeight.Text);
					MyProject.Forms.Form1.PB.Refresh();
				}
			}
			else if (MyProject.Forms.Form1.IndexElevPoint > -1 && Module1.ValidText(txtHeight.Text, CheckLimits: false, -999f, 999f, IncludeMin: true, IncludeMax: true, Message: false))
			{
				Public2.PRJ.ElPT[MyProject.Forms.Form1.IndexElevPoint].H = Conversions.ToSingle(txtHeight.Text);
				MyProject.Forms.Form1.PB.Refresh();
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
	}

	private void chkCloseLine_MouseClick(object sender, MouseEventArgs e)
	{
		Form1_Public.ShowTriangles = false;
		try
		{
			Public2.PRJ.ElLN[MyProject.Forms.Form1.IndexElevLine].CloseLine = chkCloseLine.Checked;
			MyProject.Forms.Form1.PB.Refresh();
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
	}
}
