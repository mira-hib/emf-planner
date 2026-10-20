using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using EMFPlanner.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[DesignerGenerated]
public class UC_Origin : UserControl
{
	private IContainer components;

	[field: AccessedThroughProperty("GroupBox1")]
	internal virtual GroupBox GroupBox1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdPosition")]
	internal virtual Button cmdPosition
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdPosition_Click;
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

	[field: AccessedThroughProperty("cmdMoveOriginToCenterScreen")]
	internal virtual Button cmdMoveOriginToCenterScreen
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdMoveOriginToCenterScreen_Click;
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

	[field: AccessedThroughProperty("lblLon")]
	internal virtual Label lblLon
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblLat")]
	internal virtual Label lblLat
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

	[field: AccessedThroughProperty("txtLonPRTO")]
	internal virtual TextBox txtLonPRTO
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = txtLonPRTO_Validated;
			KeyEventHandler value3 = txtLonPRTO_KeyDown;
			TextBox textBox = field;
			if (textBox != null)
			{
				textBox.Validated -= value2;
				textBox.KeyDown -= value3;
			}
			field = value;
			textBox = field;
			if (textBox != null)
			{
				textBox.Validated += value2;
				textBox.KeyDown += value3;
			}
		}
	}

	[field: AccessedThroughProperty("txtLatPRTO")]
	internal virtual TextBox txtLatPRTO
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = txtLatPRTO_Validated;
			KeyEventHandler value3 = txtLatPRTO_KeyDown;
			TextBox textBox = field;
			if (textBox != null)
			{
				textBox.Validated -= value2;
				textBox.KeyDown -= value3;
			}
			field = value;
			textBox = field;
			if (textBox != null)
			{
				textBox.Validated += value2;
				textBox.KeyDown += value3;
			}
		}
	}

	[field: AccessedThroughProperty("cmdPositionToPRTO")]
	internal virtual Button cmdPositionToPRTO
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdPositionToPRTO_Click;
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

	[field: AccessedThroughProperty("GroupBox3")]
	internal virtual GroupBox GroupBox3
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdOriginToPRTO")]
	internal virtual Button cmdOriginToPRTO
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdOriginToPRTO_Click;
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

	[field: AccessedThroughProperty("GroupBox2")]
	internal virtual GroupBox GroupBox2
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("lblHorigin")]
	internal virtual Label lblHorigin
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdSize")]
	internal virtual Button cmdSize
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmdSize_Click;
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

	public UC_Origin()
	{
		Load += UC_Origin_Load;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EMFPlanner.UC_Origin));
		this.GroupBox1 = new System.Windows.Forms.GroupBox();
		this.cmdSize = new System.Windows.Forms.Button();
		this.GroupBox2 = new System.Windows.Forms.GroupBox();
		this.lblHorigin = new System.Windows.Forms.Label();
		this.GroupBox4 = new System.Windows.Forms.GroupBox();
		this.cmdOriginToPRTO = new System.Windows.Forms.Button();
		this.txtLonPRTO = new System.Windows.Forms.TextBox();
		this.txtLatPRTO = new System.Windows.Forms.TextBox();
		this.cmdPositionToPRTO = new System.Windows.Forms.Button();
		this.Label1 = new System.Windows.Forms.Label();
		this.Label2 = new System.Windows.Forms.Label();
		this.GroupBox3 = new System.Windows.Forms.GroupBox();
		this.lblLon = new System.Windows.Forms.Label();
		this.lblLat = new System.Windows.Forms.Label();
		this.cmdMoveOriginToCenterScreen = new System.Windows.Forms.Button();
		this.cmdPosition = new System.Windows.Forms.Button();
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.GroupBox1.SuspendLayout();
		this.GroupBox2.SuspendLayout();
		this.GroupBox4.SuspendLayout();
		this.GroupBox3.SuspendLayout();
		base.SuspendLayout();
		this.GroupBox1.Controls.Add(this.cmdSize);
		this.GroupBox1.Controls.Add(this.GroupBox2);
		this.GroupBox1.Controls.Add(this.GroupBox4);
		this.GroupBox1.Controls.Add(this.GroupBox3);
		this.GroupBox1.Controls.Add(this.cmdMoveOriginToCenterScreen);
		this.GroupBox1.Controls.Add(this.cmdPosition);
		this.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GroupBox1.Location = new System.Drawing.Point(0, 0);
		this.GroupBox1.Name = "GroupBox1";
		this.GroupBox1.Size = new System.Drawing.Size(300, 596);
		this.GroupBox1.TabIndex = 0;
		this.GroupBox1.TabStop = false;
		this.GroupBox1.Text = "Origin and Grid";
		this.cmdSize.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdSize.Image = (System.Drawing.Image)resources.GetObject("cmdSize.Image");
		this.cmdSize.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdSize.Location = new System.Drawing.Point(69, 25);
		this.cmdSize.Name = "cmdSize";
		this.cmdSize.Size = new System.Drawing.Size(26, 26);
		this.cmdSize.TabIndex = 195;
		this.cmdSize.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdSize, "Set calculation grid size relative to origin");
		this.cmdSize.UseVisualStyleBackColor = true;
		this.GroupBox2.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GroupBox2.Controls.Add(this.lblHorigin);
		this.GroupBox2.Location = new System.Drawing.Point(3, 146);
		this.GroupBox2.Name = "GroupBox2";
		this.GroupBox2.Size = new System.Drawing.Size(294, 49);
		this.GroupBox2.TabIndex = 147;
		this.GroupBox2.TabStop = false;
		this.GroupBox2.Text = "Elevation at the origin point";
		this.lblHorigin.AutoSize = true;
		this.lblHorigin.Location = new System.Drawing.Point(17, 24);
		this.lblHorigin.Name = "lblHorigin";
		this.lblHorigin.Size = new System.Drawing.Size(54, 13);
		this.lblHorigin.TabIndex = 144;
		this.lblHorigin.Text = "Elevation:";
		this.GroupBox4.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GroupBox4.Controls.Add(this.cmdOriginToPRTO);
		this.GroupBox4.Controls.Add(this.txtLonPRTO);
		this.GroupBox4.Controls.Add(this.txtLatPRTO);
		this.GroupBox4.Controls.Add(this.cmdPositionToPRTO);
		this.GroupBox4.Controls.Add(this.Label1);
		this.GroupBox4.Controls.Add(this.Label2);
		this.GroupBox4.Location = new System.Drawing.Point(3, 201);
		this.GroupBox4.Name = "GroupBox4";
		this.GroupBox4.Size = new System.Drawing.Size(294, 105);
		this.GroupBox4.TabIndex = 147;
		this.GroupBox4.TabStop = false;
		this.GroupBox4.Text = "Site coordinates for the report {Coordinates}";
		this.cmdOriginToPRTO.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdOriginToPRTO.Image = (System.Drawing.Image)resources.GetObject("cmdOriginToPRTO.Image");
		this.cmdOriginToPRTO.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdOriginToPRTO.Location = new System.Drawing.Point(226, 48);
		this.cmdOriginToPRTO.Name = "cmdOriginToPRTO";
		this.cmdOriginToPRTO.Size = new System.Drawing.Size(26, 26);
		this.cmdOriginToPRTO.TabIndex = 151;
		this.cmdOriginToPRTO.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdOriginToPRTO, "Insert the origin point coordinates");
		this.cmdOriginToPRTO.UseVisualStyleBackColor = true;
		this.txtLonPRTO.Location = new System.Drawing.Point(71, 68);
		this.txtLonPRTO.Name = "txtLonPRTO";
		this.txtLonPRTO.Size = new System.Drawing.Size(111, 20);
		this.txtLonPRTO.TabIndex = 150;
		this.txtLonPRTO.Text = "E82°55'28.19\"";
		this.txtLatPRTO.Location = new System.Drawing.Point(72, 35);
		this.txtLatPRTO.Name = "txtLatPRTO";
		this.txtLatPRTO.Size = new System.Drawing.Size(110, 20);
		this.txtLatPRTO.TabIndex = 149;
		this.txtLatPRTO.Text = "N55°01'49.59\"";
		this.cmdPositionToPRTO.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdPositionToPRTO.Image = (System.Drawing.Image)resources.GetObject("cmdPositionToPRTO.Image");
		this.cmdPositionToPRTO.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdPositionToPRTO.Location = new System.Drawing.Point(199, 48);
		this.cmdPositionToPRTO.Name = "cmdPositionToPRTO";
		this.cmdPositionToPRTO.Size = new System.Drawing.Size(26, 26);
		this.cmdPositionToPRTO.TabIndex = 148;
		this.cmdPositionToPRTO.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdPositionToPRTO, "Display the site at the screen center");
		this.cmdPositionToPRTO.UseVisualStyleBackColor = true;
		this.Label1.AutoSize = true;
		this.Label1.Location = new System.Drawing.Point(12, 71);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(57, 13);
		this.Label1.TabIndex = 147;
		this.Label1.Text = "Longitude:";
		this.Label2.AutoSize = true;
		this.Label2.Location = new System.Drawing.Point(17, 38);
		this.Label2.Name = "Label2";
		this.Label2.Size = new System.Drawing.Size(48, 13);
		this.Label2.TabIndex = 146;
		this.Label2.Text = "Latitude:";
		this.GroupBox3.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.GroupBox3.Controls.Add(this.lblLon);
		this.GroupBox3.Controls.Add(this.lblLat);
		this.GroupBox3.Location = new System.Drawing.Point(3, 66);
		this.GroupBox3.Name = "GroupBox3";
		this.GroupBox3.Size = new System.Drawing.Size(294, 74);
		this.GroupBox3.TabIndex = 146;
		this.GroupBox3.TabStop = false;
		this.GroupBox3.Text = "The origin point coordinates";
		this.lblLon.AutoSize = true;
		this.lblLon.Location = new System.Drawing.Point(12, 47);
		this.lblLon.Name = "lblLon";
		this.lblLon.Size = new System.Drawing.Size(57, 13);
		this.lblLon.TabIndex = 145;
		this.lblLon.Text = "Longitude:";
		this.lblLat.AutoSize = true;
		this.lblLat.Location = new System.Drawing.Point(17, 24);
		this.lblLat.Name = "lblLat";
		this.lblLat.Size = new System.Drawing.Size(48, 13);
		this.lblLat.TabIndex = 144;
		this.lblLat.Text = "Latitude:";
		this.cmdMoveOriginToCenterScreen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdMoveOriginToCenterScreen.Image = (System.Drawing.Image)resources.GetObject("cmdMoveOriginToCenterScreen.Image");
		this.cmdMoveOriginToCenterScreen.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdMoveOriginToCenterScreen.Location = new System.Drawing.Point(42, 25);
		this.cmdMoveOriginToCenterScreen.Name = "cmdMoveOriginToCenterScreen";
		this.cmdMoveOriginToCenterScreen.Size = new System.Drawing.Size(26, 26);
		this.cmdMoveOriginToCenterScreen.TabIndex = 142;
		this.cmdMoveOriginToCenterScreen.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdMoveOriginToCenterScreen, "Set the origin in the screen center");
		this.cmdMoveOriginToCenterScreen.UseVisualStyleBackColor = true;
		this.cmdPosition.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdPosition.Image = (System.Drawing.Image)resources.GetObject("cmdPosition.Image");
		this.cmdPosition.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdPosition.Location = new System.Drawing.Point(15, 25);
		this.cmdPosition.Name = "cmdPosition";
		this.cmdPosition.Size = new System.Drawing.Size(26, 26);
		this.cmdPosition.TabIndex = 141;
		this.cmdPosition.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdPosition, "Show the origin point at the screen center");
		this.cmdPosition.UseVisualStyleBackColor = true;
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		base.Controls.Add(this.GroupBox1);
		this.MinimumSize = new System.Drawing.Size(250, 0);
		base.Name = "UC_Origin";
		base.Size = new System.Drawing.Size(300, 596);
		this.GroupBox1.ResumeLayout(false);
		this.GroupBox2.ResumeLayout(false);
		this.GroupBox2.PerformLayout();
		this.GroupBox4.ResumeLayout(false);
		this.GroupBox4.PerformLayout();
		this.GroupBox3.ResumeLayout(false);
		this.GroupBox3.PerformLayout();
		base.ResumeLayout(false);
	}

	private void UC_Origin_Load(object sender, EventArgs e)
	{
		MyProject.Forms.Form1.DrawPIC();
		FillCoords();
		txtLatPRTO.Text = CalcCoords.ConvLatToStrN(Public2.PRJ.SETT.PRTOCoordWGS.Lat);
		txtLonPRTO.Text = CalcCoords.ConvLonToStrE(Public2.PRJ.SETT.PRTOCoordWGS.Lon);
	}

	private void cmdPosition_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		MyProject.Forms.Form1.MapPositionWM(Public2.PRJ.SETT.OriginCoordWM.Xwm, Public2.PRJ.SETT.OriginCoordWM.Ywm);
	}

	private void cmdMoveOriginToCenterScreen_Click(object sender, EventArgs e)
	{
		GroupBox1.Focus();
		if (Interaction.MsgBox(Localization.LocMSG(21), MsgBoxStyle.YesNo) == MsgBoxResult.Yes)
		{
			Public2.PRJ.SETT.OriginCoordWM = Form1_Public.GetScreenCenterWM();
			if (Public2.PRJ.SETT.OriginCoordWM.Xwm > 20037508.343)
			{
				Public2.PRJ.SETT.OriginCoordWM.Xwm = Public2.PRJ.SETT.OriginCoordWM.Xwm - 40075016.686;
			}
			FillCoords();
			CalcCoords.ConvScaleWebMercMetersToMeters();
			CalcCoords.ConvAllTxWebMercMetersToMeters();
			CalcCoords.ConvAllControlPointsWebMercMetersToMeters();
			CalcCoords.ConvAllVertPlanesWebMercMetersToMeters();
			MyProject.Forms.Form1.PB.Refresh();
		}
	}

	public void FillCoords()
	{
		CalcCoords.ConvWebMercMetersToWGS84(Public2.PRJ.SETT.OriginCoordWM, ref Public2.PRJ.SETT.OriginCoordWGS.Lat, ref Public2.PRJ.SETT.OriginCoordWGS.Lon);
		lblLat.Text = Localization.LocMSG(17) + ": " + CalcCoords.ConvLatToStrN(Public2.PRJ.SETT.OriginCoordWGS.Lat);
		lblLon.Text = Localization.LocMSG(18) + ": " + CalcCoords.ConvLonToStrE(Public2.PRJ.SETT.OriginCoordWGS.Lon);
		lblHorigin.Text = Localization.LocMSG(19) + ": " + Conversions.ToString(Math.Round(Triangul.FindHeightTriang(Public2.PRJ.SETT.OriginCoordWM.Xwm, Public2.PRJ.SETT.OriginCoordWM.Ywm), 1)) + " m";
	}

	private void txtLatPRTO_Validated(object sender, EventArgs e)
	{
		double DoubleCoordOut = default;
		if (CalcCoords.TestLatitude(txtLatPRTO.Text, ref DoubleCoordOut))
		{
			Public2.PRJ.SETT.PRTOCoordWGS.Lat = DoubleCoordOut;
			txtLatPRTO.Text = CalcCoords.ConvLatToStrN(Public2.PRJ.SETT.PRTOCoordWGS.Lat);
		}
		else
		{
			Interaction.MsgBox(Localization.LocMSG(20), MsgBoxStyle.Critical);
			txtLatPRTO.Text = CalcCoords.ConvLatToStrN(Public2.PRJ.SETT.PRTOCoordWGS.Lat);
		}
	}

	private void txtLatPRTO_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			double DoubleCoordOut = default;
			if (CalcCoords.TestLatitude(txtLatPRTO.Text, ref DoubleCoordOut))
			{
				Public2.PRJ.SETT.PRTOCoordWGS.Lat = DoubleCoordOut;
				txtLatPRTO.Text = CalcCoords.ConvLatToStrN(Public2.PRJ.SETT.PRTOCoordWGS.Lat);
			}
			else
			{
				Interaction.MsgBox(Localization.LocMSG(20), MsgBoxStyle.Critical);
				txtLatPRTO.Text = CalcCoords.ConvLatToStrN(Public2.PRJ.SETT.PRTOCoordWGS.Lat);
			}
		}
	}

	private void txtLonPRTO_Validated(object sender, EventArgs e)
	{
		double DoubleCoordOut = default;
		if (CalcCoords.TestLongitude(txtLonPRTO.Text, ref DoubleCoordOut))
		{
			Public2.PRJ.SETT.PRTOCoordWGS.Lon = DoubleCoordOut;
			txtLonPRTO.Text = CalcCoords.ConvLonToStrE(Public2.PRJ.SETT.PRTOCoordWGS.Lon);
		}
		else
		{
			Interaction.MsgBox(Localization.LocMSG(20), MsgBoxStyle.Critical);
			txtLonPRTO.Text = CalcCoords.ConvLonToStrE(Public2.PRJ.SETT.PRTOCoordWGS.Lon);
		}
	}

	private void txtLonPRTO_KeyDown(object sender, KeyEventArgs e)
	{
		if (e.KeyCode == Keys.Return)
		{
			double DoubleCoordOut = default;
			if (CalcCoords.TestLongitude(txtLonPRTO.Text, ref DoubleCoordOut))
			{
				Public2.PRJ.SETT.PRTOCoordWGS.Lon = DoubleCoordOut;
				txtLonPRTO.Text = CalcCoords.ConvLonToStrE(Public2.PRJ.SETT.PRTOCoordWGS.Lon);
			}
			else
			{
				Interaction.MsgBox(Localization.LocMSG(20), MsgBoxStyle.Critical);
				txtLonPRTO.Text = CalcCoords.ConvLonToStrE(Public2.PRJ.SETT.PRTOCoordWGS.Lon);
			}
		}
	}

	private void cmdPositionToPRTO_Click(object sender, EventArgs e)
	{
		MyProject.Forms.Form1.MapPositionWGS84(Public2.PRJ.SETT.PRTOCoordWGS.Lat, Public2.PRJ.SETT.PRTOCoordWGS.Lon);
	}

	private void cmdOriginToPRTO_Click(object sender, EventArgs e)
	{
		Public2.PRJ.SETT.PRTOCoordWGS = Public2.PRJ.SETT.OriginCoordWGS;
		txtLatPRTO.Text = CalcCoords.ConvLatToStrN(Public2.PRJ.SETT.PRTOCoordWGS.Lat);
		txtLonPRTO.Text = CalcCoords.ConvLonToStrE(Public2.PRJ.SETT.PRTOCoordWGS.Lon);
	}

	private void cmdSize_Click(object sender, EventArgs e)
	{
		Form1_Public.AreaWidth1 = (float)Math.Round(Math.Abs(Public2.PRJ.SETT.HorAreaCoordM[0].Xm));
		Form1_Public.AreaWidth2 = (float)Math.Round(Math.Abs(Public2.PRJ.SETT.HorAreaCoordM[1].Xm));
		Form1_Public.AreaHeight1 = (float)Math.Round(Math.Abs(Public2.PRJ.SETT.HorAreaCoordM[1].Ym));
		Form1_Public.AreaHeight2 = (float)Math.Round(Math.Abs(Public2.PRJ.SETT.HorAreaCoordM[0].Ym));
		GroupBox1.Focus();
		MyProject.Forms.frmAreaSize.ShowDialog();
		if (Form1_Public.AreaSizeOK)
		{
			Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm = Public2.PRJ.SETT.OriginCoordWM.Xwm - (double)Form1_Public.AreaWidth1 / CalcCoords.CalcMashtWMtoMeters() - 0.1;
			Public2.PRJ.SETT.HorAreaCoordWM[1].Xwm = Public2.PRJ.SETT.OriginCoordWM.Xwm + (double)Form1_Public.AreaWidth2 / CalcCoords.CalcMashtWMtoMeters() + 0.1;
			Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm = Public2.PRJ.SETT.OriginCoordWM.Ywm - (double)Form1_Public.AreaHeight2 / CalcCoords.CalcMashtWMtoMeters() - 0.1;
			Public2.PRJ.SETT.HorAreaCoordWM[1].Ywm = Public2.PRJ.SETT.OriginCoordWM.Ywm + (double)Form1_Public.AreaHeight1 / CalcCoords.CalcMashtWMtoMeters() + 0.1;
			Public2.PRJ.SETT.HorAreaCoordM[0] = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.SETT.HorAreaCoordWM[0]);
			Public2.PRJ.SETT.HorAreaCoordM[1] = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.SETT.HorAreaCoordWM[1]);
			MyProject.Forms.Form1.PB.Refresh();
		}
	}
}
