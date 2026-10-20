using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using EMFPlanner.My;
using EMFPlanner.My.Resources;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[DesignerGenerated]
public class UC_Raster : UserControl
{
	private IContainer components;

	private bool FlagMouseInPB;

	private Font Font8;

	private bool Moving;

	private int MouseDownX;

	private int MouseDownY;

	private int IndexRPL;

	[field: AccessedThroughProperty("GroupBox1")]
	internal virtual GroupBox GroupBox1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblLength2")]
	internal virtual Label lblLength2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("txtLength")]
	internal virtual TextBox txtLength
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			CancelEventHandler value2 = txtLength_Validating;
			KeyEventHandler value3 = txtLength_KeyDown;
			TextBox textBox = field;
			if (textBox != null)
			{
				textBox.Validating -= value2;
				textBox.KeyDown -= value3;
			}
			field = value;
			textBox = field;
			if (textBox != null)
			{
				textBox.Validating += value2;
				textBox.KeyDown += value3;
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

	[field: AccessedThroughProperty("cmdOpenPicture")]
	internal virtual Button cmdOpenPicture
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdOpenPicture_Click;
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

	[field: AccessedThroughProperty("Panel1")]
	internal virtual Panel Panel1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("PB")]
	internal virtual PictureBox PB
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			PaintEventHandler value2 = PB_Paint;
			MouseEventHandler value3 = PB_MouseDown;
			MouseEventHandler value4 = PB_MouseMove;
			MouseEventHandler value5 = PB_MouseUp;
			MouseEventHandler value6 = PB_MouseWheel;
			EventHandler value7 = PB_MouseEnter;
			EventHandler value8 = PB_MouseLeave;
			PictureBox pictureBox = field;
			if (pictureBox != null)
			{
				pictureBox.Paint -= value2;
				pictureBox.MouseDown -= value3;
				pictureBox.MouseMove -= value4;
				pictureBox.MouseUp -= value5;
				pictureBox.MouseWheel -= value6;
				pictureBox.MouseEnter -= value7;
				pictureBox.MouseLeave -= value8;
			}
			field = value;
			pictureBox = field;
			if (pictureBox != null)
			{
				pictureBox.Paint += value2;
				pictureBox.MouseDown += value3;
				pictureBox.MouseMove += value4;
				pictureBox.MouseUp += value5;
				pictureBox.MouseWheel += value6;
				pictureBox.MouseEnter += value7;
				pictureBox.MouseLeave += value8;
			}
		}
	}

	[field: AccessedThroughProperty("OFD1")]
	internal virtual OpenFileDialog OFD1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("GroupBox2")]
	internal virtual GroupBox GroupBox2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("rbLenght1")]
	internal virtual RadioButton rbLenght1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = rbLenght1_MouseClick;
			RadioButton radioButton = field;
			if (radioButton != null)
			{
				radioButton.MouseClick -= value2;
			}
			field = value;
			radioButton = field;
			if (radioButton != null)
			{
				radioButton.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("rbLenght2")]
	internal virtual RadioButton rbLenght2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			MouseEventHandler value2 = rbLenght1_MouseClick;
			RadioButton radioButton = field;
			if (radioButton != null)
			{
				radioButton.MouseClick -= value2;
			}
			field = value;
			radioButton = field;
			if (radioButton != null)
			{
				radioButton.MouseClick += value2;
			}
		}
	}

	[field: AccessedThroughProperty("cmdDelImage")]
	internal virtual Button cmdDelImage
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdDelImage_Click;
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

	[field: AccessedThroughProperty("cmdMoveScalingToCenterScreen")]
	internal virtual Button cmdMoveScalingToCenterScreen
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdMoveScalingToCenterScreen_Click;
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

	[field: AccessedThroughProperty("cmdMoveRasterScalingToCenterScreen")]
	internal virtual Button cmdMoveRasterScalingToCenterScreen
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdMoveRasterScalingToCenterScreen_Click;
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

	[field: AccessedThroughProperty("cmdClearPiramid")]
	internal virtual Button cmdClearPiramid
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdClearPiramid_Click;
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

	public UC_Raster()
	{
		Load += UC_Raster_Load;
		FlagMouseInPB = false;
		Font8 = new Font("Arial", 8f, FontStyle.Regular);
		Moving = false;
		IndexRPL = -1;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EMFPlanner.UC_Raster));
		this.GroupBox1 = new System.Windows.Forms.GroupBox();
		this.cmdClearPiramid = new System.Windows.Forms.Button();
		this.cmdMoveRasterScalingToCenterScreen = new System.Windows.Forms.Button();
		this.cmdMoveScalingToCenterScreen = new System.Windows.Forms.Button();
		this.cmdDelImage = new System.Windows.Forms.Button();
		this.GroupBox2 = new System.Windows.Forms.GroupBox();
		this.rbLenght1 = new System.Windows.Forms.RadioButton();
		this.rbLenght2 = new System.Windows.Forms.RadioButton();
		this.txtLength = new System.Windows.Forms.TextBox();
		this.lblLength2 = new System.Windows.Forms.Label();
		this.Panel1 = new System.Windows.Forms.Panel();
		this.PB = new System.Windows.Forms.PictureBox();
		this.cmdOpenPicture = new System.Windows.Forms.Button();
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.OFD1 = new System.Windows.Forms.OpenFileDialog();
		this.GroupBox1.SuspendLayout();
		this.GroupBox2.SuspendLayout();
		this.Panel1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.PB).BeginInit();
		base.SuspendLayout();
		this.GroupBox1.Controls.Add(this.cmdClearPiramid);
		this.GroupBox1.Controls.Add(this.cmdMoveRasterScalingToCenterScreen);
		this.GroupBox1.Controls.Add(this.cmdMoveScalingToCenterScreen);
		this.GroupBox1.Controls.Add(this.cmdDelImage);
		this.GroupBox1.Controls.Add(this.GroupBox2);
		this.GroupBox1.Controls.Add(this.Panel1);
		this.GroupBox1.Controls.Add(this.cmdOpenPicture);
		this.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GroupBox1.Location = new System.Drawing.Point(0, 0);
		this.GroupBox1.Name = "GroupBox1";
		this.GroupBox1.Size = new System.Drawing.Size(667, 671);
		this.GroupBox1.TabIndex = 0;
		this.GroupBox1.TabStop = false;
		this.GroupBox1.Text = "Calibrate Map Image";
		this.cmdClearPiramid.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdClearPiramid.Image = (System.Drawing.Image)resources.GetObject("cmdClearPiramid.Image");
		this.cmdClearPiramid.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdClearPiramid.Location = new System.Drawing.Point(123, 23);
		this.cmdClearPiramid.Name = "cmdClearPiramid";
		this.cmdClearPiramid.Size = new System.Drawing.Size(26, 26);
		this.cmdClearPiramid.TabIndex = 162;
		this.cmdClearPiramid.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdClearPiramid, "Refresh the pyramid of tiles");
		this.cmdClearPiramid.UseVisualStyleBackColor = true;
		this.cmdMoveRasterScalingToCenterScreen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdMoveRasterScalingToCenterScreen.Image = (System.Drawing.Image)resources.GetObject("cmdMoveRasterScalingToCenterScreen.Image");
		this.cmdMoveRasterScalingToCenterScreen.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdMoveRasterScalingToCenterScreen.Location = new System.Drawing.Point(69, 23);
		this.cmdMoveRasterScalingToCenterScreen.Name = "cmdMoveRasterScalingToCenterScreen";
		this.cmdMoveRasterScalingToCenterScreen.Size = new System.Drawing.Size(26, 26);
		this.cmdMoveRasterScalingToCenterScreen.TabIndex = 161;
		this.cmdMoveRasterScalingToCenterScreen.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdMoveRasterScalingToCenterScreen, "Move the reference point and the line on the image to the screen center");
		this.cmdMoveRasterScalingToCenterScreen.UseVisualStyleBackColor = true;
		this.cmdMoveScalingToCenterScreen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdMoveScalingToCenterScreen.Image = (System.Drawing.Image)resources.GetObject("cmdMoveScalingToCenterScreen.Image");
		this.cmdMoveScalingToCenterScreen.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdMoveScalingToCenterScreen.Location = new System.Drawing.Point(96, 23);
		this.cmdMoveScalingToCenterScreen.Name = "cmdMoveScalingToCenterScreen";
		this.cmdMoveScalingToCenterScreen.Size = new System.Drawing.Size(26, 26);
		this.cmdMoveScalingToCenterScreen.TabIndex = 160;
		this.cmdMoveScalingToCenterScreen.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdMoveScalingToCenterScreen, "Move the reference point and the line on the base map to the screen center");
		this.cmdMoveScalingToCenterScreen.UseVisualStyleBackColor = true;
		this.cmdDelImage.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdDelImage.Image = (System.Drawing.Image)resources.GetObject("cmdDelImage.Image");
		this.cmdDelImage.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdDelImage.Location = new System.Drawing.Point(42, 23);
		this.cmdDelImage.Name = "cmdDelImage";
		this.cmdDelImage.Size = new System.Drawing.Size(26, 26);
		this.cmdDelImage.TabIndex = 150;
		this.cmdDelImage.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdDelImage, "Delete the Image");
		this.cmdDelImage.UseVisualStyleBackColor = true;
		this.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GroupBox2.Controls.Add(this.rbLenght1);
		this.GroupBox2.Controls.Add(this.rbLenght2);
		this.GroupBox2.Controls.Add(this.txtLength);
		this.GroupBox2.Controls.Add(this.lblLength2);
		this.GroupBox2.Location = new System.Drawing.Point(3, 54);
		this.GroupBox2.Name = "GroupBox2";
		this.GroupBox2.Size = new System.Drawing.Size(660, 48);
		this.GroupBox2.TabIndex = 149;
		this.GroupBox2.TabStop = false;
		this.rbLenght1.AutoSize = true;
		this.rbLenght1.Location = new System.Drawing.Point(398, 18);
		this.rbLenght1.Name = "rbLenght1";
		this.rbLenght1.Size = new System.Drawing.Size(88, 17);
		this.rbLenght1.TabIndex = 147;
		this.rbLenght1.Text = "Use two lines";
		this.rbLenght1.UseVisualStyleBackColor = true;
		this.rbLenght2.AutoSize = true;
		this.rbLenght2.Checked = true;
		this.rbLenght2.Location = new System.Drawing.Point(39, 18);
		this.rbLenght2.Name = "rbLenght2";
		this.rbLenght2.Size = new System.Drawing.Size(158, 17);
		this.rbLenght2.TabIndex = 148;
		this.rbLenght2.TabStop = true;
		this.rbLenght2.Text = "Use line and reference point";
		this.rbLenght2.UseVisualStyleBackColor = true;
		this.txtLength.Location = new System.Drawing.Point(211, 17);
		this.txtLength.Name = "txtLength";
		this.txtLength.Size = new System.Drawing.Size(41, 20);
		this.txtLength.TabIndex = 0;
		this.txtLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
		this.lblLength2.AutoSize = true;
		this.lblLength2.Location = new System.Drawing.Point(256, 20);
		this.lblLength2.Name = "lblLength2";
		this.lblLength2.Size = new System.Drawing.Size(15, 13);
		this.lblLength2.TabIndex = 2;
		this.lblLength2.Text = "m";
		this.Panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
		this.Panel1.Controls.Add(this.PB);
		this.Panel1.Location = new System.Drawing.Point(3, 108);
		this.Panel1.Name = "Panel1";
		this.Panel1.Size = new System.Drawing.Size(661, 560);
		this.Panel1.TabIndex = 146;
		this.PB.Cursor = System.Windows.Forms.Cursors.Cross;
		this.PB.Location = new System.Drawing.Point(2, 2);
		this.PB.Name = "PB";
		this.PB.Size = new System.Drawing.Size(290, 372);
		this.PB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
		this.PB.TabIndex = 145;
		this.PB.TabStop = false;
		this.cmdOpenPicture.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdOpenPicture.Image = (System.Drawing.Image)resources.GetObject("cmdOpenPicture.Image");
		this.cmdOpenPicture.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdOpenPicture.Location = new System.Drawing.Point(15, 23);
		this.cmdOpenPicture.Name = "cmdOpenPicture";
		this.cmdOpenPicture.Size = new System.Drawing.Size(26, 26);
		this.cmdOpenPicture.TabIndex = 144;
		this.cmdOpenPicture.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdOpenPicture, "Load Image");
		this.cmdOpenPicture.UseVisualStyleBackColor = true;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		base.Controls.Add(this.GroupBox1);
		this.MinimumSize = new System.Drawing.Size(250, 0);
		base.Name = "UC_Raster";
		base.Size = new System.Drawing.Size(667, 671);
		this.GroupBox1.ResumeLayout(false);
		this.GroupBox2.ResumeLayout(false);
		this.GroupBox2.PerformLayout();
		this.Panel1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.PB).EndInit();
		base.ResumeLayout(false);
	}

	private void UC_Raster_Load(object sender, EventArgs e)
	{
		GC.Collect();
		MyProject.Forms.Form1.DrawPIC();
		rbLenght1.Checked = Public2.PRJ.RPL.UseScalingLine;
		rbLenght2.Checked = !Public2.PRJ.RPL.UseScalingLine;
		txtLength.Text = Conversions.ToString(Public2.PRJ.RPL.Length);
		if (rbLenght1.Checked)
		{
			lblLength2.Visible = false;
			txtLength.Visible = false;
		}
		else
		{
			lblLength2.Visible = true;
			txtLength.Visible = true;
		}
		if (!Information.IsNothing(Public2.PRJ.RPL.BTM))
		{
			PB.ClientSize = checked(new Size((int)Math.Round((double)Public2.PRJ.RPL.BTM.Width / (double)Public2.PRJ.RPL.RasterScale), (int)Math.Round((double)Public2.PRJ.RPL.BTM.Height / (double)Public2.PRJ.RPL.RasterScale)));
			PB.Location = new Point(Public2.PRJ.RPL.Xpb, Public2.PRJ.RPL.Ypb);
			PB.Image = Public2.PRJ.RPL.BTM;
		}
	}

	private void PB_Paint(object sender, PaintEventArgs e)
	{
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.RPL.BTM))
			{
				e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
				e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
				if (!Public2.PRJ.RPL.UseScalingLine)
				{
					e.Graphics.DrawImage(Resources.OriginYellow, (int)Math.Round((double)Public2.PRJ.RPL.PointRaster[0].Xpix / (double)Public2.PRJ.RPL.RasterScale - 15.0), (int)Math.Round((double)Public2.PRJ.RPL.PointRaster[0].Ypix / (double)Public2.PRJ.RPL.RasterScale - 15.0));
					e.Graphics.DrawString(Localization.LocMSG(14), Font8, new SolidBrush(Color.Blue), (float)((double)Public2.PRJ.RPL.PointRaster[0].Xpix / (double)Public2.PRJ.RPL.RasterScale + 15.0), (float)((double)Public2.PRJ.RPL.PointRaster[0].Ypix / (double)Public2.PRJ.RPL.RasterScale - 15.0));
				}
				e.Graphics.DrawLine(new Pen(Color.Yellow, 3f), (int)Math.Round((double)Public2.PRJ.RPL.PointRaster[1].Xpix / (double)Public2.PRJ.RPL.RasterScale), (int)Math.Round((double)Public2.PRJ.RPL.PointRaster[1].Ypix / (double)Public2.PRJ.RPL.RasterScale), (int)Math.Round((double)Public2.PRJ.RPL.PointRaster[2].Xpix / (double)Public2.PRJ.RPL.RasterScale), (int)Math.Round((double)Public2.PRJ.RPL.PointRaster[2].Ypix / (double)Public2.PRJ.RPL.RasterScale));
				e.Graphics.DrawImage(Resources.ring8x8, (int)Math.Round((double)Public2.PRJ.RPL.PointRaster[1].Xpix / (double)Public2.PRJ.RPL.RasterScale - 8.0), (int)Math.Round((double)Public2.PRJ.RPL.PointRaster[1].Ypix / (double)Public2.PRJ.RPL.RasterScale - 8.0));
				e.Graphics.DrawImage(Resources.ring8x8, (int)Math.Round((double)Public2.PRJ.RPL.PointRaster[2].Xpix / (double)Public2.PRJ.RPL.RasterScale - 8.0), (int)Math.Round((double)Public2.PRJ.RPL.PointRaster[2].Ypix / (double)Public2.PRJ.RPL.RasterScale - 8.0));
				e.Graphics.DrawString(Localization.LocMSG(15), Font8, new SolidBrush(Color.Blue), (int)Math.Round((double)Public2.PRJ.RPL.PointRaster[1].Xpix / (double)Public2.PRJ.RPL.RasterScale + 3.0), (int)Math.Round((double)Public2.PRJ.RPL.PointRaster[1].Ypix / (double)Public2.PRJ.RPL.RasterScale - 12.0));
				e.Graphics.DrawString(Localization.LocMSG(15), Font8, new SolidBrush(Color.Blue), (int)Math.Round((double)Public2.PRJ.RPL.PointRaster[2].Xpix / (double)Public2.PRJ.RPL.RasterScale + 3.0), (int)Math.Round((double)Public2.PRJ.RPL.PointRaster[2].Ypix / (double)Public2.PRJ.RPL.RasterScale - 12.0));
			}
		}
	}

	private void PB_MouseDown(object sender, MouseEventArgs e)
	{
		if ((e.Button == MouseButtons.Middle) | (e.Button == MouseButtons.Right))
		{
			Moving = true;
			MouseDownX = e.X;
			MouseDownY = e.Y;
		}
		else if (e.Button == MouseButtons.Left)
		{
			if ((Math.Abs((double)Public2.PRJ.RPL.PointRaster[0].Xpix / (double)Public2.PRJ.RPL.RasterScale - (double)e.X) < 16.0) & (Math.Abs((double)Public2.PRJ.RPL.PointRaster[0].Ypix / (double)Public2.PRJ.RPL.RasterScale - (double)e.Y) < 16.0))
			{
				IndexRPL = 0;
			}
			if ((Math.Abs((double)Public2.PRJ.RPL.PointRaster[1].Xpix / (double)Public2.PRJ.RPL.RasterScale - (double)e.X) < 8.0) & (Math.Abs((double)Public2.PRJ.RPL.PointRaster[1].Ypix / (double)Public2.PRJ.RPL.RasterScale - (double)e.Y) < 8.0))
			{
				IndexRPL = 1;
			}
			if ((Math.Abs((double)Public2.PRJ.RPL.PointRaster[2].Xpix / (double)Public2.PRJ.RPL.RasterScale - (double)e.X) < 8.0) & (Math.Abs((double)Public2.PRJ.RPL.PointRaster[2].Ypix / (double)Public2.PRJ.RPL.RasterScale - (double)e.Y) < 8.0))
			{
				IndexRPL = 2;
			}
		}
	}

	private void PB_MouseMove(object sender, MouseEventArgs e)
	{
		checked
		{
			if (e.Button == MouseButtons.Left)
			{
				if (IndexRPL == 0)
				{
					Public2.PRJ.RPL.PointRaster[0].Xpix = (short)(e.X * Public2.PRJ.RPL.RasterScale);
					Public2.PRJ.RPL.PointRaster[0].Ypix = (short)(e.Y * Public2.PRJ.RPL.RasterScale);
					PB.Refresh();
				}
				else if (IndexRPL == 1)
				{
					Public2.PRJ.RPL.PointRaster[1].Xpix = (short)(e.X * Public2.PRJ.RPL.RasterScale);
					Public2.PRJ.RPL.PointRaster[1].Ypix = (short)(e.Y * Public2.PRJ.RPL.RasterScale);
					PB.Refresh();
				}
				else if (IndexRPL == 2)
				{
					Public2.PRJ.RPL.PointRaster[2].Xpix = (short)(e.X * Public2.PRJ.RPL.RasterScale);
					Public2.PRJ.RPL.PointRaster[2].Ypix = (short)(e.Y * Public2.PRJ.RPL.RasterScale);
					PB.Refresh();
				}
			}
			if (Moving)
			{
				Public2.PRJ.RPL.Xpb = (short)(PB.Location.X + (e.X - MouseDownX));
				Public2.PRJ.RPL.Ypb = (short)(PB.Location.Y + (e.Y - MouseDownY));
				PB.Location = new Point(Public2.PRJ.RPL.Xpb, Public2.PRJ.RPL.Ypb);
				PB.Refresh();
			}
			else if (((Math.Abs((double)Public2.PRJ.RPL.PointRaster[0].Xpix / (double)Public2.PRJ.RPL.RasterScale - (double)e.X) < 16.0) & (Math.Abs((double)Public2.PRJ.RPL.PointRaster[0].Ypix / (double)Public2.PRJ.RPL.RasterScale - (double)e.Y) < 16.0)) | ((Math.Abs((double)Public2.PRJ.RPL.PointRaster[1].Xpix / (double)Public2.PRJ.RPL.RasterScale - (double)e.X) < 8.0) & (Math.Abs((double)Public2.PRJ.RPL.PointRaster[1].Ypix / (double)Public2.PRJ.RPL.RasterScale - (double)e.Y) < 8.0)) | ((Math.Abs((double)Public2.PRJ.RPL.PointRaster[2].Xpix / (double)Public2.PRJ.RPL.RasterScale - (double)e.X) < 8.0) & (Math.Abs((double)Public2.PRJ.RPL.PointRaster[2].Ypix / (double)Public2.PRJ.RPL.RasterScale - (double)e.Y) < 8.0)))
			{
				PB.Cursor = Cursors.Arrow;
			}
			else
			{
				PB.Cursor = Cursors.Cross;
			}
		}
	}

	private void PB_MouseUp(object sender, MouseEventArgs e)
	{
		if (e.Button == MouseButtons.Left)
		{
			IndexRPL = -1;
		}
		if ((e.Button == MouseButtons.Middle) | (e.Button == MouseButtons.Right))
		{
			Moving = false;
		}
	}

	private void PB_MouseWheel(object sender, MouseEventArgs e)
	{
		checked
		{
			if (FlagMouseInPB)
			{
				short num = (short)(e.X * Public2.PRJ.RPL.RasterScale);
				short num2 = (short)(e.Y * Public2.PRJ.RPL.RasterScale);
				short num3 = (short)e.X;
				short num4 = (short)e.Y;
				if (e.Delta > 0)
				{
					Public2.PRJ.RPL.RasterScale--;
				}
				else
				{
					Public2.PRJ.RPL.RasterScale++;
				}
				if (Public2.PRJ.RPL.RasterScale > 20)
				{
					Public2.PRJ.RPL.RasterScale = 20;
					return;
				}
				if (Public2.PRJ.RPL.RasterScale < 1)
				{
					Public2.PRJ.RPL.RasterScale = 1;
					return;
				}
				PB.ClientSize = new Size((int)Math.Round((double)Public2.PRJ.RPL.BTM.Width / (double)Public2.PRJ.RPL.RasterScale), (int)Math.Round((double)Public2.PRJ.RPL.BTM.Height / (double)Public2.PRJ.RPL.RasterScale));
				Public2.PRJ.RPL.Xpb = (short)Math.Round((double)Public2.PRJ.RPL.Xpb + ((double)num3 - (double)num / (double)Public2.PRJ.RPL.RasterScale));
				Public2.PRJ.RPL.Ypb = (short)Math.Round((double)Public2.PRJ.RPL.Ypb + ((double)num4 - (double)num2 / (double)Public2.PRJ.RPL.RasterScale));
				PB.Location = new Point(Public2.PRJ.RPL.Xpb, Public2.PRJ.RPL.Ypb);
			}
		}
	}

	private void PB_MouseEnter(object sender, EventArgs e)
	{
		FlagMouseInPB = true;
	}

	private void PB_MouseLeave(object sender, EventArgs e)
	{
		FlagMouseInPB = false;
	}

	private void cmdPosition_Click(object sender, EventArgs e)
	{
		checked
		{
			Public2.PRJ.RPL.Xpb = (short)Math.Round((double)Panel1.Width / 2.0 - (double)(short)unchecked(Public2.PRJ.RPL.PointRaster[1].Xpix + Public2.PRJ.RPL.PointRaster[2].Xpix) / 2.0);
			Public2.PRJ.RPL.Ypb = (short)Math.Round((double)Panel1.Height / 2.0 - (double)(short)unchecked(Public2.PRJ.RPL.PointRaster[1].Ypix + Public2.PRJ.RPL.PointRaster[2].Ypix) / 2.0);
			PB.Location = new Point(Public2.PRJ.RPL.Xpb, Public2.PRJ.RPL.Ypb);
			PB.Refresh();
		}
	}

	private void cmdPositionOrigin_Click(object sender, EventArgs e)
	{
		checked
		{
			Public2.PRJ.RPL.Xpb = (short)Math.Round((double)Panel1.Width / 2.0 - (double)Public2.PRJ.RPL.PointRaster[0].Xpix);
			Public2.PRJ.RPL.Ypb = (short)Math.Round((double)Panel1.Height / 2.0 - (double)Public2.PRJ.RPL.PointRaster[0].Ypix);
			PB.Location = new Point(Public2.PRJ.RPL.Xpb, Public2.PRJ.RPL.Ypb);
			PB.Refresh();
		}
	}

	private void cmdOpenPicture_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		OFD1.Filter = Localization.LocMSG(12) + "|*.*";
		if (OFD1.ShowDialog() == DialogResult.OK)
		{
			try
			{
				Public2.PRJ.RPL.Xpb = 0;
				Public2.PRJ.RPL.Ypb = 0;
				Public2.PRJ.RPL.RasterScale = 1;
				PB.Location = new Point(Public2.PRJ.RPL.Xpb, Public2.PRJ.RPL.Ypb);
				Public2.PRJ.RPL.BTM = new Bitmap(OFD1.FileName);
				PB.ClientSize = checked(new Size((int)Math.Round((double)Public2.PRJ.RPL.BTM.Width / (double)Public2.PRJ.RPL.RasterScale), (int)Math.Round((double)Public2.PRJ.RPL.BTM.Height / (double)Public2.PRJ.RPL.RasterScale)));
				PB.Image = Public2.PRJ.RPL.BTM;
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Interaction.MsgBox(ex2.Message);
				ProjectData.ClearProjectError();
			}
		}
	}

	private void cmdDelImage_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Interaction.MsgBox(Localization.LocMSG(10), MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
		{
			Public2.PRJ.RPL.BTM = null;
			PB.Image = null;
			Public2.PRJ.RPL.Xpb = 0;
			Public2.PRJ.RPL.Ypb = 0;
			Public2.PRJ.RPL.RasterScale = 1;
		}
	}

	private void rbLenght1_MouseClick(object sender, MouseEventArgs e)
	{
		Public2.PRJ.RPL.UseScalingLine = rbLenght1.Checked;
		if (rbLenght1.Checked)
		{
			lblLength2.Visible = false;
			txtLength.Visible = false;
		}
		else
		{
			lblLength2.Visible = true;
			txtLength.Visible = true;
		}
		PB.Refresh();
		MyProject.Forms.Form1.PB.Refresh();
	}

	private void cmdMoveRasterScalingToCenterScreen_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		checked
		{
			if (Interaction.MsgBox(Localization.LocMSG(11), MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
			{
				Public2.PRJ.RPL.PointRaster[0].Xpix = (short)Math.Round(((double)(short)unchecked(-Public2.PRJ.RPL.Xpb) + (double)Panel1.Width / 2.0) * (double)Public2.PRJ.RPL.RasterScale);
				Public2.PRJ.RPL.PointRaster[0].Ypix = (short)Math.Round(((double)(short)unchecked(-Public2.PRJ.RPL.Ypb) + (double)Panel1.Height / 2.0) * (double)Public2.PRJ.RPL.RasterScale);
				Public2.PRJ.RPL.PointRaster[1].Xpix = (short)(Public2.PRJ.RPL.PointRaster[0].Xpix - 100);
				Public2.PRJ.RPL.PointRaster[1].Ypix = (short)(Public2.PRJ.RPL.PointRaster[0].Ypix - 100);
				Public2.PRJ.RPL.PointRaster[2].Xpix = (short)(Public2.PRJ.RPL.PointRaster[0].Xpix + 100);
				Public2.PRJ.RPL.PointRaster[2].Ypix = (short)(Public2.PRJ.RPL.PointRaster[0].Ypix + 100);
				PB.Refresh();
			}
		}
	}

	private void cmdMoveScalingToCenterScreen_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Interaction.MsgBox(Localization.LocMSG(13), MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
		{
			Public2.PRJ.RPL.PointMap[0] = Form1_Public.GetScreenCenterWM();
			Public2.PRJ.RPL.PointMap[1].Xwm = Public2.PRJ.RPL.PointMap[0].Xwm - 30.0;
			Public2.PRJ.RPL.PointMap[1].Ywm = Public2.PRJ.RPL.PointMap[0].Ywm - 30.0;
			Public2.PRJ.RPL.PointMap[2].Xwm = Public2.PRJ.RPL.PointMap[0].Xwm + 30.0;
			Public2.PRJ.RPL.PointMap[2].Ywm = Public2.PRJ.RPL.PointMap[0].Ywm + 30.0;
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	private void cmdClearPiramid_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		try
		{
			Directory.Delete(Public2.AppSetup.FolderCach + "\\SitPlan\\" + Conversions.ToString(Public2.PRJ.SETT.GUID), recursive: true);
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		MyProject.Forms.Form1.DrawPIC();
	}

	private void txtLength_Validating(object sender, CancelEventArgs e)
	{
		TextBox textBox = txtLength;
		string TextProperty = textBox.Text;
		ref float length = ref Public2.PRJ.RPL.Length;
		double DblVariable = length;
		Module1.TextPropertyValidating(ref TextProperty, ref DblVariable, CheckLimits: true, 5f, 1000f, IncludeMin: true, IncludeMax: true, Message: true);
		length = (float)DblVariable;
		textBox.Text = TextProperty;
	}

	private void txtLength_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			TextBox textBox = txtLength;
			string TextProperty = textBox.Text;
			ref float length = ref Public2.PRJ.RPL.Length;
			double DblVariable = length;
			Module1.TextPropertyValidating(ref TextProperty, ref DblVariable, CheckLimits: true, 5f, 1000f, IncludeMin: true, IncludeMax: true, Message: true);
			length = (float)DblVariable;
			textBox.Text = TextProperty;
		}
	}

	private void cmdRasterPosition_Click(object sender, EventArgs e)
	{
		Public2.PRJ.RPL.Xpb = 0;
		Public2.PRJ.RPL.Ypb = 0;
		Public2.PRJ.RPL.RasterScale = 1;
		PB.ClientSize = checked(new Size((int)Math.Round((double)Public2.PRJ.RPL.BTM.Width / (double)Public2.PRJ.RPL.RasterScale), (int)Math.Round((double)Public2.PRJ.RPL.BTM.Height / (double)Public2.PRJ.RPL.RasterScale)));
		PB.Location = new Point(Public2.PRJ.RPL.Xpb, Public2.PRJ.RPL.Ypb);
	}
}
