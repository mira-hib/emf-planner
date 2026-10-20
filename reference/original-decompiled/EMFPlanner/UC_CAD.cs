using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Eyeshot.Translators;
using devDept.Geometry;
using devDept.Graphics;

namespace EMFPlanner;

[DesignerGenerated]
public class UC_CAD : UserControl
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

	[field: AccessedThroughProperty("ToolTip1")]
	internal virtual ToolTip ToolTip1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("Model1")]
	internal virtual Model Model1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		set;
	}

	[field: AccessedThroughProperty("cmdExportToDWG")]
	internal virtual Button cmdExportToDWG
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmdExportToDWG_Click;
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

	[field: AccessedThroughProperty("cmbScale")]
	internal virtual ComboBox cmbScale
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = CmbScale_SelectedIndexChanged;
			ComboBox comboBox = field;
			if (comboBox != null)
			{
				comboBox.SelectedIndexChanged -= value2;
			}
			field = value;
			comboBox = field;
			if (comboBox != null)
			{
				comboBox.SelectedIndexChanged += value2;
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

	[field: AccessedThroughProperty("CLB1")]
	internal virtual CheckedListBox CLB1
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			ItemCheckEventHandler value2 = CLB1_ItemCheck;
			CheckedListBox checkedListBox = field;
			if (checkedListBox != null)
			{
				checkedListBox.ItemCheck -= value2;
			}
			field = value;
			checkedListBox = field;
			if (checkedListBox != null)
			{
				checkedListBox.ItemCheck += value2;
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

	[field: AccessedThroughProperty("cmbFormat")]
	internal virtual ComboBox cmbFormat
	{
		get;
		[MethodImpl(MethodImplOptions.Synchronized)]
		[CompilerGenerated]
		set
		{
			EventHandler value2 = cmbFormat_SelectedIndexChanged;
			ComboBox comboBox = field;
			if (comboBox != null)
			{
				comboBox.SelectedIndexChanged -= value2;
			}
			field = value;
			comboBox = field;
			if (comboBox != null)
			{
				comboBox.SelectedIndexChanged += value2;
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
		System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EMFPlanner.UC_CAD));
		devDept.Eyeshot.CancelToolBarButton cancelButton = new devDept.Eyeshot.CancelToolBarButton("Cancel", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
		devDept.Eyeshot.ProgressBar progressBar = new devDept.Eyeshot.ProgressBar(devDept.Eyeshot.ProgressBar.styleType.Circular, 0, "Idle", System.Drawing.Color.Black, System.Drawing.Color.Transparent, System.Drawing.Color.Green, 1.0, true, cancelButton, false, 0.1, true);
		devDept.Graphics.BackgroundSettings background = new devDept.Graphics.BackgroundSettings(devDept.Graphics.backgroundStyleType.Solid, System.Drawing.Color.DeepSkyBlue, System.Drawing.Color.DodgerBlue, System.Drawing.Color.White, 0.75, null, devDept.Graphics.colorThemeType.Auto, 0.33);
		devDept.Eyeshot.Camera camera = new devDept.Eyeshot.Camera(new devDept.Geometry.Point3D(-4.537403032510781E-16, 2.043464660644531, 47.59656494855879), 97.25790464878082, new devDept.Geometry.Quaternion(0.4999999999999999, 0.5, 0.5, 0.5000000000000001), devDept.Graphics.projectionType.Orthographic, 40.0, 3.2640129304254666, false, 0.001);
		devDept.Eyeshot.MagnifyingGlassToolBarButton magnifyingGlassToolBarButton = new devDept.Eyeshot.MagnifyingGlassToolBarButton("Magnifying Glass", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
		devDept.Eyeshot.ZoomWindowToolBarButton zoomWindowToolBarButton = new devDept.Eyeshot.ZoomWindowToolBarButton("Zoom Window", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
		devDept.Eyeshot.ZoomToolBarButton zoomToolBarButton = new devDept.Eyeshot.ZoomToolBarButton("Zoom", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
		devDept.Eyeshot.PanToolBarButton panToolBarButton = new devDept.Eyeshot.PanToolBarButton("Pan", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
		devDept.Eyeshot.ZoomFitToolBarButton zoomFitToolBarButton = new devDept.Eyeshot.ZoomFitToolBarButton("Zoom Fit", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
		devDept.Eyeshot.ToolBar toolBar = new devDept.Eyeshot.ToolBar(devDept.Eyeshot.ToolBar.positionType.HorizontalTopCenter, false, new devDept.Eyeshot.ToolBarButton[5] { magnifyingGlassToolBarButton, zoomWindowToolBarButton, zoomToolBarButton, panToolBarButton, zoomFitToolBarButton });
		devDept.Eyeshot.Grid grid = new devDept.Eyeshot.Grid(new devDept.Geometry.Point3D(-100.0, -100.0, 0.0), new devDept.Geometry.Point3D(100.0, 100.0, 0.0), 1.0, new devDept.Geometry.Plane(new devDept.Geometry.Point3D(0.0, 0.0, 0.0), new devDept.Geometry.Vector3D(0.0, 0.0, 1.0)), System.Drawing.Color.FromArgb(63, 128, 128, 128), System.Drawing.Color.FromArgb(127, 255, 0, 0), System.Drawing.Color.FromArgb(127, 0, 128, 0), false, false, false, false, 10, 100, 10, System.Drawing.Color.FromArgb(127, 90, 90, 90), System.Drawing.Color.Transparent, false, System.Drawing.Color.FromArgb(12, 0, 0, 255));
		devDept.Eyeshot.OriginSymbol originSymbol = new devDept.Eyeshot.OriginSymbol(10, devDept.Eyeshot.originSymbolStyleType.Ball, System.Drawing.Color.Black, System.Drawing.Color.Red, System.Drawing.Color.Green, System.Drawing.Color.Blue, "Origin", "X", "Y", "Z", false, null, false);
		devDept.Eyeshot.RotateSettings rotate = new devDept.Eyeshot.RotateSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.None, devDept.Eyeshot.modifierKeys.None), 10.0, false, 1.0, devDept.Eyeshot.rotationType.Trackball, devDept.Eyeshot.rotationCenterType.CursorLocation, new devDept.Geometry.Point3D(0.0, 0.0, 0.0), false);
		devDept.Eyeshot.ZoomSettings zoom = new devDept.Eyeshot.ZoomSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.Shift), 25, true, devDept.Eyeshot.zoomStyleType.AtCursorLocation, false, 1.0, System.Drawing.Color.Empty, devDept.Eyeshot.Camera.perspectiveFitType.Accurate, false, 10, true);
		devDept.Eyeshot.PanSettings pan = new devDept.Eyeshot.PanSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.None), 25, true);
		devDept.Eyeshot.NavigationSettings navigation = new devDept.Eyeshot.NavigationSettings(devDept.Eyeshot.Camera.navigationType.Examine, new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Left, devDept.Eyeshot.modifierKeys.None), new devDept.Geometry.Point3D(-1000.0, -1000.0, -1000.0), new devDept.Geometry.Point3D(1000.0, 1000.0, 1000.0), 8.0, 50.0, 50.0);
		devDept.Eyeshot.Viewport.SavedViewsManager savedViews = new devDept.Eyeshot.Viewport.SavedViewsManager(8);
		devDept.Eyeshot.Viewport viewport = new devDept.Eyeshot.Viewport(new System.Drawing.Point(0, 0), new System.Drawing.Size(675, 668), background, camera, new devDept.Eyeshot.ToolBar[1] { toolBar }, devDept.Eyeshot.displayType.Wireframe, true, false, false, false, new devDept.Eyeshot.Grid[1] { grid }, new devDept.Eyeshot.OriginSymbol[1] { originSymbol }, false, rotate, zoom, pan, navigation, savedViews, devDept.Eyeshot.viewType.Top);
		devDept.Eyeshot.CoordinateSystemIcon coordinateSystemIcon = new devDept.Eyeshot.CoordinateSystemIcon(System.Drawing.Color.Black, System.Drawing.Color.FromArgb(80, 80, 80), System.Drawing.Color.FromArgb(80, 80, 80), System.Drawing.Color.OrangeRed, "Origin", "X", "Y", "Z", false, devDept.Eyeshot.coordinateSystemPositionType.BottomLeft, 37, false);
		devDept.Eyeshot.ViewCubeIcon viewCubeIcon = new devDept.Eyeshot.ViewCubeIcon(devDept.Eyeshot.coordinateSystemPositionType.TopRight, false, System.Drawing.Color.FromArgb(30, 144, 255), true, "FRONT", "BACK", "LEFT", "RIGHT", "TOP", "BOTTOM", System.Drawing.Color.FromArgb(240, 77, 77, 77), System.Drawing.Color.FromArgb(240, 77, 77, 77), System.Drawing.Color.FromArgb(240, 77, 77, 77), System.Drawing.Color.FromArgb(240, 77, 77, 77), System.Drawing.Color.FromArgb(240, 77, 77, 77), System.Drawing.Color.FromArgb(240, 77, 77, 77), 'S', 'N', 'W', 'E', true, System.Drawing.Color.White, System.Drawing.Color.Black, 120, true, true, null, null, null, null, null, null, false);
		this.GroupBox1 = new System.Windows.Forms.GroupBox();
		this.GroupBox2 = new System.Windows.Forms.GroupBox();
		this.CLB1 = new System.Windows.Forms.CheckedListBox();
		this.Label2 = new System.Windows.Forms.Label();
		this.cmbFormat = new System.Windows.Forms.ComboBox();
		this.Label1 = new System.Windows.Forms.Label();
		this.cmbScale = new System.Windows.Forms.ComboBox();
		this.cmdExportToDWG = new System.Windows.Forms.Button();
		this.Model1 = new devDept.Eyeshot.Model();
		this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
		this.GroupBox1.SuspendLayout();
		this.GroupBox2.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Model1).BeginInit();
		base.SuspendLayout();
		this.GroupBox1.Controls.Add(this.GroupBox2);
		this.GroupBox1.Controls.Add(this.Label2);
		this.GroupBox1.Controls.Add(this.cmbFormat);
		this.GroupBox1.Controls.Add(this.Label1);
		this.GroupBox1.Controls.Add(this.cmbScale);
		this.GroupBox1.Controls.Add(this.cmdExportToDWG);
		this.GroupBox1.Controls.Add(this.Model1);
		this.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GroupBox1.Location = new System.Drawing.Point(0, 0);
		this.GroupBox1.Name = "GroupBox1";
		this.GroupBox1.Size = new System.Drawing.Size(846, 688);
		this.GroupBox1.TabIndex = 1;
		this.GroupBox1.TabStop = false;
		this.GroupBox1.Text = "CAD";
		this.GroupBox2.Controls.Add(this.CLB1);
		this.GroupBox2.Location = new System.Drawing.Point(8, 120);
		this.GroupBox2.Name = "GroupBox2";
		this.GroupBox2.Size = new System.Drawing.Size(154, 286);
		this.GroupBox2.TabIndex = 154;
		this.GroupBox2.TabStop = false;
		this.GroupBox2.Text = "Layers";
		this.CLB1.CheckOnClick = true;
		this.CLB1.FormattingEnabled = true;
		this.CLB1.Location = new System.Drawing.Point(6, 19);
		this.CLB1.Name = "CLB1";
		this.CLB1.Size = new System.Drawing.Size(142, 259);
		this.CLB1.TabIndex = 151;
		this.Label2.AutoSize = true;
		this.Label2.Location = new System.Drawing.Point(37, 94);
		this.Label2.Name = "Label2";
		this.Label2.Size = new System.Drawing.Size(27, 13);
		this.Label2.TabIndex = 153;
		this.Label2.Text = "Size";
		this.cmbFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbFormat.FormattingEnabled = true;
		this.cmbFormat.Items.AddRange(new object[3] { "A3", "A4", "A2" });
		this.cmbFormat.Location = new System.Drawing.Point(75, 91);
		this.cmbFormat.Name = "cmbFormat";
		this.cmbFormat.Size = new System.Drawing.Size(61, 21);
		this.cmbFormat.TabIndex = 152;
		this.Label1.AutoSize = true;
		this.Label1.Location = new System.Drawing.Point(30, 67);
		this.Label1.Name = "Label1";
		this.Label1.Size = new System.Drawing.Size(34, 13);
		this.Label1.TabIndex = 150;
		this.Label1.Text = "Scale";
		this.cmbScale.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
		this.cmbScale.FormattingEnabled = true;
		this.cmbScale.Items.AddRange(new object[30]
		{
			"1:250", "1:500", "1:750", "1:1000", "1:1250", "1:1500", "1:1750", "1:2000", "1:2250", "1:2500",
			"1:2750", "1:3000", "1:3250", "1:3500", "1:3750", "1:4000", "1:4250", "1:4500", "1:4750", "1:5000",
			"1:5500", "1:6000", "1:6500", "1:7000", "1:7500", "1:8000", "1:8500", "1:9000", "1:9500", "1:10000"
		});
		this.cmbScale.Location = new System.Drawing.Point(75, 64);
		this.cmbScale.MaxDropDownItems = 20;
		this.cmbScale.Name = "cmbScale";
		this.cmbScale.Size = new System.Drawing.Size(61, 21);
		this.cmbScale.TabIndex = 149;
		this.cmdExportToDWG.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
		this.cmdExportToDWG.Image = (System.Drawing.Image)resources.GetObject("cmdExportToDWG.Image");
		this.cmdExportToDWG.ImeMode = System.Windows.Forms.ImeMode.NoControl;
		this.cmdExportToDWG.Location = new System.Drawing.Point(14, 21);
		this.cmdExportToDWG.Name = "cmdExportToDWG";
		this.cmdExportToDWG.Size = new System.Drawing.Size(26, 26);
		this.cmdExportToDWG.TabIndex = 148;
		this.cmdExportToDWG.TabStop = false;
		this.ToolTip1.SetToolTip(this.cmdExportToDWG, "Export drawing to DWG/DXF file");
		this.cmdExportToDWG.UseVisualStyleBackColor = true;
		this.Model1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
		this.Model1.Cursor = System.Windows.Forms.Cursors.Default;
		this.Model1.Location = new System.Drawing.Point(165, 14);
		this.Model1.Name = "Model1";
		this.Model1.ProgressBar = progressBar;
		this.Model1.Size = new System.Drawing.Size(675, 668);
		this.Model1.TabIndex = 137;
		this.Model1.Text = "Model1";
		originSymbol.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
		coordinateSystemIcon.LabelFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 204);
		viewport.CoordinateSystemIcon = coordinateSystemIcon;
		viewport.Legends = new devDept.Eyeshot.Legend[0];
		viewCubeIcon.Font = null;
		viewCubeIcon.InitialRotation = new devDept.Geometry.Quaternion(0.0, 0.0, 0.0, 1.0);
		viewport.ViewCubeIcon = viewCubeIcon;
		this.Model1.Viewports.Add(viewport);
		base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
		base.Controls.Add(this.GroupBox1);
		base.Name = "UC_CAD";
		base.Size = new System.Drawing.Size(846, 688);
		this.GroupBox1.ResumeLayout(false);
		this.GroupBox1.PerformLayout();
		this.GroupBox2.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Model1).EndInit();
		base.ResumeLayout(false);
	}

	public UC_CAD()
	{
		Load += UC_CAD_Load;
		FlagOK = false;
		InitializeComponent();
		Model1.Unlock("PS20-57PM1-2YJ1Q-5UW0-RY76");
	}

	private void UC_CAD_Load(object sender, EventArgs e)
	{
		cmbScale.Text = "1:1000";
		cmbFormat.Text = "A3";
		SubDrawAll();
		checked
		{
			int num = Model1.Layers.Count - 1;
			for (int i = 0; i <= num; i++)
			{
				CLB1.Items.Add(Model1.Layers[i].Name, isChecked: true);
			}
			FlagOK = true;
		}
	}

	private void CLB1_ItemCheck(object sender, ItemCheckEventArgs e)
	{
		if (FlagOK)
		{
			Model1.Layers[e.Index].Visible = e.NewValue != CheckState.Unchecked;
			Model1.Invalidate();
		}
	}

	private void SubDrawAll()
	{
		Model1.Clear();
		Model1.Layers.Add("Drawing frame");
		Model1.Layers.Add("EMF exposure area");
		Model1.Layers.Add("Buildings");
		Model1.Layers.Add("Terrain");
		Model1.Layers.Add("Lines and labels");
		Model1.Layers.Add("Base Map");
		Model1.Layers.Add("Antennas");
		Model1.Layers.Add("Control points");
		Model1.Layers.Add("Origin");
		Model1.Layers.Add("Calculation area");
		Model1.Layers.Remove("Default");
		checked
		{
			if (FlagOK)
			{
				int num = Model1.Layers.Count - 1;
				for (int i = 0; i <= num; i++)
				{
					Model1.Layers[i].Visible = CLB1.GetItemChecked(i);
				}
			}
			double num2 = 1000.0 / Conversions.ToDouble(cmbScale.Text.Substring(2));
			int num3 = default;
			int num4 = default;
			if (Operators.CompareString(cmbFormat.Text, "A3", TextCompare: false) == 0)
			{
				num3 = (int)Math.Round(217.5 - (Public2.PRJ.SETT.HorAreaCoordM[1].Xm + Public2.PRJ.SETT.HorAreaCoordM[0].Xm) / 2.0 * num2);
				num4 = (int)Math.Round(148.5 - (Public2.PRJ.SETT.HorAreaCoordM[1].Ym + Public2.PRJ.SETT.HorAreaCoordM[0].Ym) / 2.0 * num2);
			}
			else if (Operators.CompareString(cmbFormat.Text, "A4", TextCompare: false) == 0)
			{
				num3 = (int)Math.Round(112.5 - (Public2.PRJ.SETT.HorAreaCoordM[1].Xm + Public2.PRJ.SETT.HorAreaCoordM[0].Xm) / 2.0 * num2);
				num4 = (int)Math.Round(148.5 - (Public2.PRJ.SETT.HorAreaCoordM[1].Ym + Public2.PRJ.SETT.HorAreaCoordM[0].Ym) / 2.0 * num2);
			}
			else if (Operators.CompareString(cmbFormat.Text, "A2", TextCompare: false) == 0)
			{
				num3 = (int)Math.Round(304.5 - (Public2.PRJ.SETT.HorAreaCoordM[1].Xm + Public2.PRJ.SETT.HorAreaCoordM[0].Xm) / 2.0 * num2);
				num4 = (int)Math.Round(210.0 - (Public2.PRJ.SETT.HorAreaCoordM[1].Ym + Public2.PRJ.SETT.HorAreaCoordM[0].Ym) / 2.0 * num2);
			}
			if (Public2.PRJ.SETT.ShowMap)
			{
				Bitmap image = RepWordAlias.wDrawMap();
				int num5 = (int)Math.Round((Public2.PRJ.SETT.HorAreaCoordM[1].Xm - Public2.PRJ.SETT.HorAreaCoordM[0].Xm) * num2);
				int num6 = (int)Math.Round((Public2.PRJ.SETT.HorAreaCoordM[1].Ym - Public2.PRJ.SETT.HorAreaCoordM[0].Ym) * num2);
				Ole2Frame entity = new Ole2Frame(new Plane(new Point3D(Public2.PRJ.SETT.HorAreaCoordM[0].Xm * num2 + (double)num3, Public2.PRJ.SETT.HorAreaCoordM[0].Ym * num2 + (double)num4, 0.0), new Vector3D(0.0, 0.0, 1.0)), num5, num6, image);
				Model1.Entities.Add(entity, "Base Map", Color.White);
			}
			Model1.LineTypes.Add("1", new float[2] { 1f, -1f });
			Model1.LineTypes.Add("2", new float[2] { 1f, -3f });
			Model1.LineTypes.Add("3", new float[2] { 1f, -5f });
			Model1.LineTypes.Add("4", new float[2] { 2f, -1f });
			Model1.LineTypes.Add("5", new float[2] { 2f, -3f });
			Model1.LineTypes.Add("6", new float[2] { 2f, -5f });
			Model1.LineTypes.Add("7", new float[2] { 5f, -1f });
			Model1.LineTypes.Add("8", new float[2] { 5f, -3f });
			Model1.LineTypes.Add("9", new float[2] { 5f, -5f });
			Model1.LineTypes.Add("10", new float[2] { 10f, -1f });
			Model1.LineTypes.Add("11", new float[2] { 10f, -3f });
			Model1.LineTypes.Add("12", new float[2] { 10f, -5f });
			Model1.LineTypes.Add("13", new float[4] { 5f, -1f, 1f, -1f });
			Model1.LineTypes.Add("14", new float[4] { 5f, -3f, 1f, -3f });
			Model1.LineTypes.Add("15", new float[4] { 5f, -10f, 1f, -10f });
			Model1.LineTypes.Add("16", new float[6] { 5f, -1f, 1f, -1f, 1f, -1f });
			Model1.LineTypes.Add("17", new float[6] { 5f, -3f, 1f, -1f, 1f, -3f });
			Model1.LineTypes.Add("18", new float[6] { 5f, -5f, 1f, -3f, 1f, -5f });
			Model1.LineTypes.Add("19", new float[4] { 5f, -5f, 10f, -5f });
			Model1.LineTypes.Add("20", new float[4] { 1f, -5f, 1f, -10f });
			Model1.LineTypes.Add("21", new float[6] { 1f, -5f, 1f, -10f, 1f, -10f });
			Model1.LineTypes.Add("22", new float[6] { 4f, -5f, 4f, -5f, 1f, -5f });
			Model1.LineTypes.Add("23", new float[8] { 1f, -5f, 1f, -5f, 4f, -5f, 4f, -5f });
			Model1.LineTypes.Add("24", new float[8] { 1f, -5f, 4f, -5f, 4f, -5f, 4f, -5f });
			Model1.LineTypes.Add("25", new float[10] { 1f, -5f, 1f, -5f, 1f, -5f, 4f, -5f, 4f, -5f });
			Model1.LineTypes.Add("26", new float[10] { 4f, -5f, 4f, -5f, 4f, -5f, 1f, -5f, 1f, -5f });
			Model1.LineTypes.Add("27", new float[10] { 1f, -5f, 4f, -5f, 1f, -5f, 4f, -5f, 1f, -5f });
			if (Public2.PRJ.SETT.ShowRoads & !Information.IsNothing(Public2.PRJ.ROAD))
			{
				int upperBound = Public2.PRJ.ROAD.GetUpperBound(0);
				for (int j = 0; j <= upperBound; j++)
				{
					LinearPath linearPath = new LinearPath(Plane.XY);
					linearPath.AutodeskProperties = new AutodeskMiscProperties
					{
						ExtrusionDir = Vector3D.AxisZ
					};
					linearPath.GlobalWidth = (double)unchecked((int)Public2.PRJ.ROAD[j].WidthM) * num2;
					linearPath.Vertices = new Point3D[Public2.PRJ.ROAD[j].CoordWM.GetUpperBound(0) + 1];
					int upperBound2 = Public2.PRJ.ROAD[j].CoordWM.GetUpperBound(0);
					for (int k = 0; k <= upperBound2; k++)
					{
						Public2.CoordMeters coordMeters = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.ROAD[j].CoordWM[k]);
						linearPath.Vertices[k] = new Point3D(coordMeters.Xm * num2 + (double)num3, coordMeters.Ym * num2 + (double)num4);
					}
					Model1.Entities.Add(linearPath, "Lines and labels", Color.Black);
				}
				int upperBound3 = Public2.PRJ.ROAD.GetUpperBound(0);
				for (int l = 0; l <= upperBound3; l++)
				{
					LinearPath linearPath2 = new LinearPath(Plane.XY);
					linearPath2.AutodeskProperties = new AutodeskMiscProperties
					{
						ExtrusionDir = Vector3D.AxisZ
					};
					linearPath2.GlobalWidth = (double)unchecked((int)Public2.PRJ.ROAD[l].WidthM) * num2 - 0.6;
					linearPath2.Vertices = new Point3D[Public2.PRJ.ROAD[l].CoordWM.GetUpperBound(0) + 1];
					int upperBound4 = Public2.PRJ.ROAD[l].CoordWM.GetUpperBound(0);
					for (int m = 0; m <= upperBound4; m++)
					{
						Public2.CoordMeters coordMeters2 = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.ROAD[l].CoordWM[m]);
						linearPath2.Vertices[m] = new Point3D(coordMeters2.Xm * num2 + (double)num3, coordMeters2.Ym * num2 + (double)num4);
					}
					Model1.Entities.Add(linearPath2, "Lines and labels", Color.FromArgb(254, 254, 254));
				}
			}
			if (Public2.PRJ.SETT.ShowFences & !Information.IsNothing(Public2.PRJ.FENCE))
			{
				int upperBound5 = Public2.PRJ.FENCE.GetUpperBound(0);
				for (int n = 0; n <= upperBound5; n++)
				{
					LinearPath linearPath3 = new LinearPath(Plane.XY);
					linearPath3.AutodeskProperties = new AutodeskMiscProperties
					{
						ExtrusionDir = Vector3D.AxisZ
					};
					linearPath3.GlobalWidth = 0.3;
					linearPath3.Vertices = new Point3D[Public2.PRJ.FENCE[n].CoordWM.GetUpperBound(0) + 1];
					int upperBound6 = Public2.PRJ.FENCE[n].CoordWM.GetUpperBound(0);
					for (int num7 = 0; num7 <= upperBound6; num7++)
					{
						Public2.CoordMeters coordMeters3 = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.FENCE[n].CoordWM[num7]);
						linearPath3.Vertices[num7] = new Point3D(coordMeters3.Xm * num2 + (double)num3, coordMeters3.Ym * num2 + (double)num4);
					}
					if ((Public2.PRJ.FENCE[n].Type > 0) & (Public2.PRJ.FENCE[n].Type < 28))
					{
						linearPath3.LineTypeMethod = colorMethodType.byEntity;
						linearPath3.LineTypeName = Conversions.ToString(Public2.PRJ.FENCE[n].Type);
					}
					else
					{
						linearPath3.LineTypeMethod = colorMethodType.byLayer;
					}
					Model1.Entities.Add(linearPath3, "Lines and labels", Color.Black);
				}
			}
			if (Public2.PRJ.SETT.ShowText & !Information.IsNothing(Public2.PRJ.TXT))
			{
				int upperBound7 = Public2.PRJ.TXT.GetUpperBound(0);
				for (int num8 = 0; num8 <= upperBound7; num8++)
				{
					Public2.CoordMeters coordMeters4 = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.TXT[num8].CoordWM);
					if (Public2.PRJ.TXT[num8].Point)
					{
						Circle circle = new Circle(coordMeters4.Xm * num2 + (double)num3, coordMeters4.Ym * num2 + (double)num4, 0.0, 0.25);
						circle.LineWeightMethod = colorMethodType.byEntity;
						circle.AutodeskProperties = new AutodeskMiscProperties
						{
							ExtrusionDir = Vector3D.AxisZ
						};
						HatchRegion entity2 = new HatchRegion(circle);
						Model1.Entities.Add(entity2, "Lines and labels", Color.Black);
					}
					Text entity3 = new Text(coordMeters4.Xm * num2 + (double)num3 + 1.0, coordMeters4.Ym * num2 + (double)num4, 0.0, Public2.PRJ.TXT[num8].Text, 2.5, devDept.Eyeshot.Entities.Text.alignmentType.BottomLeft);
					Model1.Entities.Add(entity3, "Lines and labels", Color.Black);
				}
			}
			if (!Information.IsNothing(Public2.PRJ.HP))
			{
				if (Public2.PRJ.SETT.FlagLastRezManyLimits)
				{
					int num9 = 0;
					do
					{
						if (Public2.PRJ.SETT.HorizCutLimits[num9] && !Information.IsNothing(Public2.PRJ.HP_PLNwm[num9]))
						{
							int upperBound8 = Public2.PRJ.HP_PLNwm[num9].GetUpperBound(0);
							for (int num10 = 0; num10 <= upperBound8; num10++)
							{
								LinearPath linearPath4 = new LinearPath(Plane.XY);
								linearPath4.AutodeskProperties = new AutodeskMiscProperties
								{
									ExtrusionDir = Vector3D.AxisZ
								};
								linearPath4.GlobalWidth = 0.5;
								linearPath4.Vertices = new Point3D[Public2.PRJ.HP_PLNwm[num9][num10].GetUpperBound(0) + 1];
								int upperBound9 = Public2.PRJ.HP_PLNwm[num9][num10].GetUpperBound(0);
								for (int num11 = 0; num11 <= upperBound9; num11++)
								{
									Public2.CoordMeters coordMeters5 = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.HP_PLNwm[num9][num10][num11]);
									linearPath4.Vertices[num11] = new Point3D(coordMeters5.Xm * num2 + (double)num3, coordMeters5.Ym * num2 + (double)num4);
								}
								linearPath4.LineTypeMethod = colorMethodType.byLayer;
								Model1.Entities.Add(linearPath4, "EMF exposure area", Public2.PRJ.SETT.LimitColor[num9]);
							}
						}
						num9++;
					}
					while (num9 <= 3);
				}
				else
				{
					for (int num12 = Public2.PRJ.HP.GetUpperBound(0); num12 >= 0; num12 += -1)
					{
						if (!Public2.PRJ.HP[num12].Activ || Information.IsNothing(Public2.PRJ.HP[num12].PLNwm))
						{
							continue;
						}
						int upperBound10 = Public2.PRJ.HP[num12].PLNwm.GetUpperBound(0);
						for (int num13 = 0; num13 <= upperBound10; num13++)
						{
							LinearPath linearPath5 = new LinearPath(Plane.XY);
							linearPath5.AutodeskProperties = new AutodeskMiscProperties
							{
								ExtrusionDir = Vector3D.AxisZ
							};
							linearPath5.GlobalWidth = 0.5;
							linearPath5.Vertices = new Point3D[Public2.PRJ.HP[num12].PLNwm[num13].GetUpperBound(0) + 1];
							int upperBound11 = Public2.PRJ.HP[num12].PLNwm[num13].GetUpperBound(0);
							for (int num14 = 0; num14 <= upperBound11; num14++)
							{
								Public2.CoordMeters coordMeters6 = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.HP[num12].PLNwm[num13][num14]);
								linearPath5.Vertices[num14] = new Point3D(coordMeters6.Xm * num2 + (double)num3, coordMeters6.Ym * num2 + (double)num4);
							}
							if ((Public2.PRJ.HP[num12].LineType > 0) & (Public2.PRJ.HP[num12].LineType < 28))
							{
								linearPath5.LineTypeMethod = colorMethodType.byEntity;
								linearPath5.LineTypeName = Conversions.ToString(Public2.PRJ.HP[num12].LineType);
							}
							else
							{
								linearPath5.LineTypeMethod = colorMethodType.byLayer;
							}
							Model1.Entities.Add(linearPath5, "EMF exposure area", Public2.PRJ.HP[num12].Color);
						}
					}
				}
			}
			if (Public2.PRJ.SETT.ShowRelief & !Information.IsNothing(Public2.PRJ.ElLN))
			{
				int upperBound12 = Public2.PRJ.ElLN.GetUpperBound(0);
				for (int num15 = 0; num15 <= upperBound12; num15++)
				{
					LinearPath linearPath6 = new LinearPath(Plane.XY);
					linearPath6.AutodeskProperties = new AutodeskMiscProperties
					{
						ExtrusionDir = Vector3D.AxisZ
					};
					linearPath6.GlobalWidth = 0.3;
					linearPath6.Vertices = new Point3D[Public2.PRJ.ElLN[num15].CoordWM.GetUpperBound(0) + 1];
					int upperBound13 = Public2.PRJ.ElLN[num15].CoordWM.GetUpperBound(0);
					for (int num16 = 0; num16 <= upperBound13; num16++)
					{
						Public2.CoordMeters coordMeters7 = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.ElLN[num15].CoordWM[num16]);
						linearPath6.Vertices[num16] = new Point3D(coordMeters7.Xm * num2 + (double)num3, coordMeters7.Ym * num2 + (double)num4);
					}
					if (Public2.PRJ.ElLN[num15].CloseLine)
					{
						LinearPath linearPath7;
						(linearPath7 = linearPath6).Vertices = (Point3D[])Utils.CopyArray(linearPath7.Vertices, new Point3D[linearPath6.Vertices.Count() + 1]);
						linearPath6.Vertices[linearPath6.Vertices.Count() - 1] = linearPath6.Vertices[0];
					}
					Model1.Entities.Add(linearPath6, "Terrain", Color.Brown);
					Text entity4 = new Text(linearPath6.Vertices[0].X + 1.0, linearPath6.Vertices[0].Y, 0.0, Conversions.ToString(Public2.PRJ.ElLN[num15].H), 2.5, devDept.Eyeshot.Entities.Text.alignmentType.BottomLeft);
					Model1.Entities.Add(entity4, "Terrain", Color.Black);
				}
			}
			if (Public2.PRJ.SETT.ShowRelief & !Information.IsNothing(Public2.PRJ.ElPT))
			{
				int upperBound14 = Public2.PRJ.ElPT.GetUpperBound(0);
				for (int num17 = 0; num17 <= upperBound14; num17++)
				{
					Public2.CoordMeters coordMeters8 = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.ElPT[num17].CoordWM);
					Circle circle2 = new Circle(coordMeters8.Xm * num2 + (double)num3, coordMeters8.Ym * num2 + (double)num4, 0.0, 0.25);
					circle2.LineWeightMethod = colorMethodType.byEntity;
					circle2.AutodeskProperties = new AutodeskMiscProperties
					{
						ExtrusionDir = Vector3D.AxisZ
					};
					HatchRegion entity5 = new HatchRegion(circle2);
					Model1.Entities.Add(entity5, "Terrain", Color.Black);
					Text entity6 = new Text(coordMeters8.Xm * num2 + (double)num3 + 1.0, coordMeters8.Ym * num2 + (double)num4, 0.0, Conversions.ToString(Public2.PRJ.ElPT[num17].H), 2.5, devDept.Eyeshot.Entities.Text.alignmentType.BottomLeft);
					Model1.Entities.Add(entity6, "Terrain", Color.Black);
				}
			}
			if (!Information.IsNothing(Public2.PRJ.BLD))
			{
				int upperBound15 = Public2.PRJ.BLD.GetUpperBound(0);
				for (int num18 = 0; num18 <= upperBound15; num18++)
				{
					LinearPath linearPath8 = new LinearPath(Plane.XY);
					linearPath8.AutodeskProperties = new AutodeskMiscProperties
					{
						ExtrusionDir = Vector3D.AxisZ
					};
					linearPath8.GlobalWidth = 0.3;
					linearPath8.Vertices = new Point3D[Public2.PRJ.BLD[num18].CoordWM.GetUpperBound(0) + 1];
					int upperBound16 = Public2.PRJ.BLD[num18].CoordWM.GetUpperBound(0);
					for (int num19 = 0; num19 <= upperBound16; num19++)
					{
						Public2.CoordMeters coordMeters9 = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.BLD[num18].CoordWM[num19]);
						linearPath8.Vertices[num19] = new Point3D(coordMeters9.Xm * num2 + (double)num3, coordMeters9.Ym * num2 + (double)num4);
					}
					Model1.Entities.Add(linearPath8, "Buildings", Color.Black);
					if (Public2.PRJ.SETT.ShowBuildingText | Public2.PRJ.SETT.ShowBuildingHeight | Public2.PRJ.SETT.ShowBuildingFoundation)
					{
						byte b = 0;
						if (Public2.PRJ.SETT.ShowBuildingText)
						{
							b++;
						}
						if (Public2.PRJ.SETT.ShowBuildingHeight)
						{
							b++;
						}
						if (Public2.PRJ.SETT.ShowBuildingFoundation)
						{
							b++;
						}
						Public2.CoordMeters coordMeters10 = CalcCoords.ConvWebMercMetersToMeters(Module1.CoordCenterSign1(Public2.PRJ.BLD[num18].CoordWM));
						switch (b)
						{
						case 2:
							coordMeters10.Ym += 1.5;
							break;
						case 3:
							coordMeters10.Ym += 3.0;
							break;
						}
						short num20 = 0;
						if (Public2.PRJ.SETT.ShowBuildingText)
						{
							Text entity7 = new Text(coordMeters10.Xm * num2 + (double)num3, coordMeters10.Ym * num2 + (double)num4, 0.0, Public2.PRJ.BLD[num18].Text, 2.5, devDept.Eyeshot.Entities.Text.alignmentType.MiddleCenter);
							Model1.Entities.Add(entity7, "Buildings", Color.Black);
							num20 -= 3;
						}
						if (Public2.PRJ.SETT.ShowBuildingHeight)
						{
							Text entity8 = new Text(coordMeters10.Xm * num2 + (double)num3, coordMeters10.Ym * num2 + (double)num20 + (double)num4, 0.0, Conversions.ToString(Public2.PRJ.BLD[num18].H) + "m", 2.5, devDept.Eyeshot.Entities.Text.alignmentType.MiddleCenter);
							Model1.Entities.Add(entity8, "Buildings", Color.Black);
							num20 -= 3;
						}
						if (Public2.PRJ.SETT.ShowBuildingFoundation)
						{
							Text entity9 = new Text(coordMeters10.Xm * num2 + (double)num3, coordMeters10.Ym * num2 + (double)num20 + (double)num4, 0.0, Conversions.ToString(Public2.PRJ.BLD[num18].Fundation) + "m", 2.5, devDept.Eyeshot.Entities.Text.alignmentType.MiddleCenter);
							Model1.Entities.Add(entity9, "Buildings", Color.Black);
						}
					}
				}
			}
			if (Public2.PRJ.SETT.ShowAntennas & !Information.IsNothing(Public2.PRJ.TX))
			{
				_ = new bool[Public2.PRJ.TX.GetUpperBound(0) + 1];
				int upperBound17 = Public2.PRJ.TX.GetUpperBound(0);
				for (int num21 = 0; num21 <= upperBound17; num21++)
				{
					if (Public2.PRJ.TX[num21].Active)
					{
						Public2.CoordMeters coordMeters11 = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.TX[num21].CoordWM);
						float num22 = Public2.PRJ.TX[num21].Az;
						if (num22 < 0f)
						{
							num22 = 0f;
						}
						int num23 = 80;
						double num24 = coordMeters11.Xm * num2 + (double)num3 + (double)num23 * Math.Sin((double)num22 / (180.0 / Math.PI));
						double num25 = coordMeters11.Ym * num2 + (double)num4 + (double)num23 * Math.Cos((double)num22 / (180.0 / Math.PI));
						double num26 = 2.0 * Math.Sin((double)(num22 + 173f) / (180.0 / Math.PI)) + num24;
						double num27 = 2.0 * Math.Cos((double)(num22 + 173f) / (180.0 / Math.PI)) + num25;
						double num28 = 2.0 * Math.Sin((double)(num22 + 187f) / (180.0 / Math.PI)) + num24;
						double num29 = 2.0 * Math.Cos((double)(num22 + 187f) / (180.0 / Math.PI)) + num25;
						LinearPath linearPath9 = new LinearPath();
						linearPath9.AutodeskProperties = new AutodeskMiscProperties
						{
							ExtrusionDir = Vector3D.AxisZ
						};
						linearPath9.Vertices = new Point3D[5];
						linearPath9.Vertices[0] = new Point3D(coordMeters11.Xm * num2 + (double)num3, coordMeters11.Ym * num2 + (double)num4);
						linearPath9.Vertices[1] = new Point3D(num24, num25);
						linearPath9.Vertices[2] = new Point3D(num26, num27);
						linearPath9.Vertices[3] = new Point3D(num28, num29);
						linearPath9.Vertices[4] = new Point3D(num24, num25);
						linearPath9.GlobalWidth = 0.25;
						Model1.Entities.Add(linearPath9, "Antennas", Color.Black);
						num23 = 83;
						num24 = coordMeters11.Xm * num2 + (double)num3 + (double)num23 * Math.Sin((double)num22 / (180.0 / Math.PI));
						num25 = coordMeters11.Ym * num2 + (double)num4 + (double)num23 * Math.Cos((double)num22 / (180.0 / Math.PI));
						Text entity10 = new Text(num24, num25, 0.0, "№" + Conversions.ToString(num21 + 1) + "(Az=" + Conversions.ToString(num22) + "°)", 2.5, devDept.Eyeshot.Entities.Text.alignmentType.MiddleCenter);
						Model1.Entities.Add(entity10, "Antennas", Color.Black);
						Circle circle3 = new Circle(coordMeters11.Xm * num2 + (double)num3, coordMeters11.Ym * num2 + (double)num4, 0.0, 1.0);
						circle3.LineWeightMethod = colorMethodType.byEntity;
						circle3.AutodeskProperties = new AutodeskMiscProperties
						{
							ExtrusionDir = Vector3D.AxisZ
						};
						HatchRegion entity11 = new HatchRegion(circle3);
						Model1.Entities.Add(entity11, "Antennas", Public2.PRJ.TX[num21].Color);
						Model1.Entities.Add(circle3, "Antennas", Color.Black);
					}
				}
			}
			if (Public2.PRJ.SETT.ShowControlPoints & !Information.IsNothing(Public2.PRJ.CPG))
			{
				int upperBound18 = Public2.PRJ.CPG.GetUpperBound(0);
				for (int num30 = 0; num30 <= upperBound18; num30++)
				{
					if (!Public2.PRJ.CPG[num30].Active || Information.IsNothing(Public2.PRJ.CPG[num30].CP))
					{
						continue;
					}
					int upperBound19 = Public2.PRJ.CPG[num30].CP.GetUpperBound(0);
					for (int num31 = 0; num31 <= upperBound19; num31++)
					{
						Circle circle4 = new Circle(Public2.PRJ.CPG[num30].CP[num31].CoordM.Xm * num2 + (double)num3, Public2.PRJ.CPG[num30].CP[num31].CoordM.Ym * num2 + (double)num4, 0.0, 1.0);
						circle4.LineWeightMethod = colorMethodType.byEntity;
						circle4.AutodeskProperties = new AutodeskMiscProperties
						{
							ExtrusionDir = Vector3D.AxisZ
						};
						HatchRegion entity12 = new HatchRegion(circle4);
						Model1.Entities.Add(entity12, "Control points", Color.FromArgb(50, 50, 50));
						Model1.Entities.Add(circle4, "Control points", Color.Black);
						string text = Public2.PRJ.SETT.ControlPointPrefix;
						if (Public2.PRJ.SETT.ShowControlPointGroups)
						{
							text = text + Conversions.ToString(num30 + 1) + ".";
						}
						text += Conversions.ToString(num31 + 1);
						if (Public2.PRJ.SETT.ShowControlPointHeight)
						{
							text += "(";
							int upperBound20 = Public2.PRJ.CPG[num30].CP[num31].H.GetUpperBound(0);
							for (int num32 = 0; num32 <= upperBound20; num32++)
							{
								text += Conversions.ToString(Public2.PRJ.CPG[num30].CP[num31].H[num32].H);
								if (num32 < Public2.PRJ.CPG[num30].CP[num31].H.GetUpperBound(0))
								{
									text += ";";
								}
							}
							text += "m)";
						}
						Text entity13 = new Text(Public2.PRJ.CPG[num30].CP[num31].CoordM.Xm * num2 + (double)num3 + 1.0, Public2.PRJ.CPG[num30].CP[num31].CoordM.Ym * num2 + (double)num4, 0.0, text, 2.5, devDept.Eyeshot.Entities.Text.alignmentType.BottomLeft);
						Model1.Entities.Add(entity13, "Control points", Color.Black);
					}
				}
			}
			Public2.CoordMeters coordMeters12 = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.SETT.OriginCoordWM);
			LinearPath linearPath10 = new LinearPath(Plane.XY, new Point2D(coordMeters12.Xm * num2 + (double)num3 - 3.0, coordMeters12.Ym * num2 + (double)num4), new Point2D(coordMeters12.Xm * num2 + (double)num3 + 3.0, coordMeters12.Ym * num2 + (double)num4));
			linearPath10.AutodeskProperties = new AutodeskMiscProperties
			{
				ExtrusionDir = Vector3D.AxisZ
			};
			linearPath10.GlobalWidth = 0.5;
			Model1.Entities.Add(linearPath10, "Origin", Color.Black);
			linearPath10 = new LinearPath(Plane.XY, new Point2D(coordMeters12.Xm * num2 + (double)num3, coordMeters12.Ym * num2 + (double)num4 - 3.0), new Point2D(coordMeters12.Xm * num2 + (double)num3, coordMeters12.Ym * num2 + (double)num4 + 3.0));
			linearPath10.AutodeskProperties = new AutodeskMiscProperties
			{
				ExtrusionDir = Vector3D.AxisZ
			};
			linearPath10.GlobalWidth = 0.5;
			Model1.Entities.Add(linearPath10, "Origin", Color.Black);
			linearPath10 = new LinearPath(Plane.XY, new Point2D(Public2.PRJ.SETT.HorAreaCoordM[0].Xm * num2 + (double)num3, Public2.PRJ.SETT.HorAreaCoordM[0].Ym * num2 + (double)num4), new Point2D(Public2.PRJ.SETT.HorAreaCoordM[1].Xm * num2 + (double)num3, Public2.PRJ.SETT.HorAreaCoordM[0].Ym * num2 + (double)num4), new Point2D(Public2.PRJ.SETT.HorAreaCoordM[1].Xm * num2 + (double)num3, Public2.PRJ.SETT.HorAreaCoordM[1].Ym * num2 + (double)num4), new Point2D(Public2.PRJ.SETT.HorAreaCoordM[0].Xm * num2 + (double)num3, Public2.PRJ.SETT.HorAreaCoordM[1].Ym * num2 + (double)num4), new Point2D(Public2.PRJ.SETT.HorAreaCoordM[0].Xm * num2 + (double)num3, Public2.PRJ.SETT.HorAreaCoordM[0].Ym * num2 + (double)num4));
			linearPath10.AutodeskProperties = new AutodeskMiscProperties
			{
				ExtrusionDir = Vector3D.AxisZ
			};
			linearPath10.GlobalWidth = 0.7;
			Model1.Entities.Add(linearPath10, "Calculation area", Color.Black);
			CalcCoords.CalcMashtMeterstoPixel();
			float xmin = (float)Public2.PRJ.SETT.HorAreaCoordM[0].Xm;
			float xmax = (float)Public2.PRJ.SETT.HorAreaCoordM[1].Xm;
			short num33 = default;
			int N = num33;
			float dX = default;
			float X = default;
			Module1.RazmetkaOSI(xmin, xmax, 8, 13, ref N, ref dX, ref X);
			num33 = (short)N;
			N = num33 - 1;
			for (int num34 = 0; num34 <= N; num34++)
			{
				float num35 = X + (float)num34 * dX;
				LinearPath linearPath11 = new LinearPath(Plane.XY, new Point2D((double)num35 * num2 + (double)num3, Public2.PRJ.SETT.HorAreaCoordM[0].Ym * num2 + (double)num4), new Point2D((double)num35 * num2 + (double)num3, Public2.PRJ.SETT.HorAreaCoordM[1].Ym * num2 + (double)num4));
				linearPath11.AutodeskProperties = new AutodeskMiscProperties
				{
					ExtrusionDir = Vector3D.AxisZ
				};
				linearPath11.GlobalWidth = 0.5;
				linearPath11.LineTypeMethod = colorMethodType.byEntity;
				linearPath11.LineTypeName = Conversions.ToString(1);
				Model1.Entities.Add(linearPath11, "Calculation area", Color.Black);
				Text entity14 = new Text((double)num35 * num2 + (double)num3, Public2.PRJ.SETT.HorAreaCoordM[0].Ym * num2 + (double)num4 - 2.0, 0.0, Conversions.ToString(num35), 3.0, devDept.Eyeshot.Entities.Text.alignmentType.TopCenter);
				Model1.Entities.Add(entity14, "Calculation area", Color.Black);
			}
			Text entity15 = new Text(Public2.PRJ.SETT.HorAreaCoordM[1].Xm * num2 + (double)num3 + 2.0, Public2.PRJ.SETT.HorAreaCoordM[0].Ym * num2 + (double)num4, 0.0, "X, m", 3.0, devDept.Eyeshot.Entities.Text.alignmentType.BottomLeft);
			Model1.Entities.Add(entity15, "Calculation area", Color.Black);
			float xmin2 = (float)Public2.PRJ.SETT.HorAreaCoordM[0].Ym;
			float xmax2 = (float)Public2.PRJ.SETT.HorAreaCoordM[1].Ym;
			int N2 = num33;
			Module1.RazmetkaOSI(xmin2, xmax2, 8, 13, ref N2, ref dX, ref X);
			num33 = (short)N2;
			N2 = num33 - 1;
			for (int num36 = 0; num36 <= N2; num36++)
			{
				float num35 = X + (float)num36 * dX;
				LinearPath linearPath12 = new LinearPath(Plane.XY, new Point2D(Public2.PRJ.SETT.HorAreaCoordM[0].Xm * num2 + (double)num3, (double)num35 * num2 + (double)num4), new Point2D(Public2.PRJ.SETT.HorAreaCoordM[1].Xm * num2 + (double)num3, (double)num35 * num2 + (double)num4));
				linearPath12.AutodeskProperties = new AutodeskMiscProperties
				{
					ExtrusionDir = Vector3D.AxisZ
				};
				linearPath12.GlobalWidth = 0.5;
				linearPath12.LineTypeMethod = colorMethodType.byEntity;
				linearPath12.LineTypeName = Conversions.ToString(1);
				Model1.Entities.Add(linearPath12, "Calculation area", Color.Black);
				Text entity16 = new Text(Public2.PRJ.SETT.HorAreaCoordM[0].Xm * num2 + (double)num3 - 2.0, (double)num35 * num2 + (double)num4, 0.0, Conversions.ToString(num35), 3.0, devDept.Eyeshot.Entities.Text.alignmentType.MiddleRight);
				Model1.Entities.Add(entity16, "Calculation area", Color.Black);
			}
			entity15 = new Text(Public2.PRJ.SETT.HorAreaCoordM[0].Xm * num2 + (double)num3, Public2.PRJ.SETT.HorAreaCoordM[1].Ym * num2 + (double)num4 + 2.0, 0.0, "Y, m", 3.0, devDept.Eyeshot.Entities.Text.alignmentType.BottomLeft);
			Model1.Entities.Add(entity15, "Calculation area", Color.Black);
			if (Operators.CompareString(cmbFormat.Text, "A3", TextCompare: false) == 0)
			{
				Text entity17 = new Text(413.0, 61.0, 0.0, "M" + cmbScale.Text, 3.0, devDept.Eyeshot.Entities.Text.alignmentType.BottomRight);
				Model1.Entities.Add(entity17, "Drawing frame", Color.Black);
				linearPath10 = new LinearPath(Plane.XY, new Point2D(230.0, 5.0), new Point2D(230.0, 60.0), new Point2D(415.0, 60.0));
				linearPath10.AutodeskProperties = new AutodeskMiscProperties
				{
					ExtrusionDir = Vector3D.AxisZ
				};
				linearPath10.GlobalWidth = 1.0;
				Model1.Entities.Add(linearPath10, "Drawing frame", Color.Black);
				linearPath10 = new LinearPath(Plane.XY, new Point2D(20.0, 5.0), new Point2D(415.0, 5.0), new Point2D(415.0, 292.0), new Point2D(20.0, 292.0), new Point2D(20.0, 5.0));
				linearPath10.AutodeskProperties = new AutodeskMiscProperties
				{
					ExtrusionDir = Vector3D.AxisZ
				};
				linearPath10.GlobalWidth = 1.0;
				Model1.Entities.Add(linearPath10, "Drawing frame", Color.Black);
				linearPath10 = new LinearPath(Plane.XY, new Point2D(0.0, 0.0), new Point2D(420.0, 0.0), new Point2D(420.0, 297.0), new Point2D(0.0, 297.0), new Point2D(0.0, 0.0));
				linearPath10.AutodeskProperties = new AutodeskMiscProperties
				{
					ExtrusionDir = Vector3D.AxisZ
				};
				linearPath10.GlobalWidth = 0.0;
				Model1.Entities.Add(linearPath10, "Drawing frame", Color.Black);
			}
			else if (Operators.CompareString(cmbFormat.Text, "A4", TextCompare: false) == 0)
			{
				Text entity18 = new Text(203.0, 6.0, 0.0, "M" + cmbScale.Text, 3.0, devDept.Eyeshot.Entities.Text.alignmentType.BottomRight);
				Model1.Entities.Add(entity18, "Drawing frame", Color.Black);
				linearPath10 = new LinearPath(Plane.XY, new Point2D(20.0, 5.0), new Point2D(205.0, 5.0), new Point2D(205.0, 292.0), new Point2D(20.0, 292.0), new Point2D(20.0, 5.0));
				linearPath10.AutodeskProperties = new AutodeskMiscProperties
				{
					ExtrusionDir = Vector3D.AxisZ
				};
				linearPath10.GlobalWidth = 1.0;
				Model1.Entities.Add(linearPath10, "Drawing frame", Color.Black);
				linearPath10 = new LinearPath(Plane.XY, new Point2D(0.0, 0.0), new Point2D(210.0, 0.0), new Point2D(210.0, 297.0), new Point2D(0.0, 297.0), new Point2D(0.0, 0.0));
				linearPath10.AutodeskProperties = new AutodeskMiscProperties
				{
					ExtrusionDir = Vector3D.AxisZ
				};
				linearPath10.GlobalWidth = 0.0;
				Model1.Entities.Add(linearPath10, "Drawing frame", Color.Black);
			}
			else if (Operators.CompareString(cmbFormat.Text, "A2", TextCompare: false) == 0)
			{
				Text entity19 = new Text(587.0, 61.0, 0.0, "M" + cmbScale.Text, 3.0, devDept.Eyeshot.Entities.Text.alignmentType.BottomRight);
				Model1.Entities.Add(entity19, "Drawing frame", Color.Black);
				linearPath10 = new LinearPath(Plane.XY, new Point2D(404.0, 5.0), new Point2D(404.0, 60.0), new Point2D(589.0, 60.0));
				linearPath10.AutodeskProperties = new AutodeskMiscProperties
				{
					ExtrusionDir = Vector3D.AxisZ
				};
				linearPath10.GlobalWidth = 1.0;
				Model1.Entities.Add(linearPath10, "Drawing frame", Color.Black);
				linearPath10 = new LinearPath(Plane.XY, new Point2D(20.0, 5.0), new Point2D(589.0, 5.0), new Point2D(589.0, 415.0), new Point2D(20.0, 415.0), new Point2D(20.0, 5.0));
				linearPath10.AutodeskProperties = new AutodeskMiscProperties
				{
					ExtrusionDir = Vector3D.AxisZ
				};
				linearPath10.GlobalWidth = 1.0;
				Model1.Entities.Add(linearPath10, "Drawing frame", Color.Black);
				linearPath10 = new LinearPath(Plane.XY, new Point2D(0.0, 0.0), new Point2D(594.0, 0.0), new Point2D(594.0, 420.0), new Point2D(0.0, 420.0), new Point2D(0.0, 0.0));
				linearPath10.AutodeskProperties = new AutodeskMiscProperties
				{
					ExtrusionDir = Vector3D.AxisZ
				};
				linearPath10.GlobalWidth = 0.0;
				Model1.Entities.Add(linearPath10, "Drawing frame", Color.Black);
			}
			Model1.Entities.Reverse();
			Model1.Camera.ProjectionMode = projectionType.Orthographic;
			Model1.SetView(viewType.Top);
			Model1.DisplayMode = displayType.Flat;
			Model1.Entities[0].Selected = true;
			Model1.ZoomFitSelectedLeaves();
			Model1.Entities[0].Selected = false;
			Model1.Invalidate();
		}
	}

	private void CmdExportToDWG_Click(object sender, EventArgs e)
	{
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.Filter = "AutoCAD files (*.dwg)|*.dwg|Drawing Exchange Format (*.dxf)|*.dxf";
		saveFileDialog.AddExtension = true;
		saveFileDialog.CheckPathExists = true;
		if (saveFileDialog.ShowDialog() == DialogResult.OK)
		{
			Model1.Entities.Reverse();
			WriteFileAsync workUnit = new WriteAutodesk(new WriteAutodeskParams(Model1), saveFileDialog.FileName);
			Model1.StartWork(workUnit);
			Model1.Entities.Reverse();
		}
	}

	private void CmbScale_SelectedIndexChanged(object sender, EventArgs e)
	{
		SubDrawAll();
	}

	private void cmbFormat_SelectedIndexChanged(object sender, EventArgs e)
	{
		SubDrawAll();
	}
}
