using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using EMFPlanner.My;
using EMFPlanner.My.Resources;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Eyeshot.Translators;
using devDept.Geometry;
using devDept.Graphics;

namespace EMFPlanner;

[DesignerGenerated]
public class UC_3D : UserControl
{
	private IContainer components;

	[field: AccessedThroughProperty("GroupBox1")]
	internal virtual GroupBox GroupBox1
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
		devDept.Eyeshot.CancelToolBarButton cancelButton = new devDept.Eyeshot.CancelToolBarButton("Cancel", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
		devDept.Eyeshot.ProgressBar progressBar = new devDept.Eyeshot.ProgressBar(devDept.Eyeshot.ProgressBar.styleType.Circular, 0, "Idle", System.Drawing.Color.Black, System.Drawing.Color.Transparent, System.Drawing.Color.Green, 1.0, true, cancelButton, false, 0.1, true);
		devDept.Graphics.BackgroundSettings background = new devDept.Graphics.BackgroundSettings(devDept.Graphics.backgroundStyleType.LinearGradient, System.Drawing.Color.FromArgb(245, 245, 245), System.Drawing.Color.DodgerBlue, System.Drawing.Color.FromArgb(102, 163, 210), 0.75, null, devDept.Graphics.colorThemeType.Auto, 0.33);
		devDept.Eyeshot.Camera camera = new devDept.Eyeshot.Camera(new devDept.Geometry.Point3D(0.0, 0.0, 45.0), 380.0, new devDept.Geometry.Quaternion(0.018434349666532526, 0.03953259043497208, 0.4222160228000619, 0.9054451828447543), devDept.Graphics.projectionType.Perspective, 40.0, 6.670000076293945, false, 0.001);
		devDept.Eyeshot.HomeToolBarButton homeToolBarButton = new devDept.Eyeshot.HomeToolBarButton("Home", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
		devDept.Eyeshot.MagnifyingGlassToolBarButton magnifyingGlassToolBarButton = new devDept.Eyeshot.MagnifyingGlassToolBarButton("Magnifying Glass", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
		devDept.Eyeshot.ZoomWindowToolBarButton zoomWindowToolBarButton = new devDept.Eyeshot.ZoomWindowToolBarButton("Zoom Window", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
		devDept.Eyeshot.ZoomToolBarButton zoomToolBarButton = new devDept.Eyeshot.ZoomToolBarButton("Zoom", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
		devDept.Eyeshot.PanToolBarButton panToolBarButton = new devDept.Eyeshot.PanToolBarButton("Pan", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
		devDept.Eyeshot.RotateToolBarButton rotateToolBarButton = new devDept.Eyeshot.RotateToolBarButton("Rotate", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
		devDept.Eyeshot.ZoomFitToolBarButton zoomFitToolBarButton = new devDept.Eyeshot.ZoomFitToolBarButton("Zoom Fit", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
		devDept.Eyeshot.ToolBar toolBar = new devDept.Eyeshot.ToolBar(devDept.Eyeshot.ToolBar.positionType.HorizontalTopCenter, true, new devDept.Eyeshot.ToolBarButton[7] { homeToolBarButton, magnifyingGlassToolBarButton, zoomWindowToolBarButton, zoomToolBarButton, panToolBarButton, rotateToolBarButton, zoomFitToolBarButton });
		devDept.Eyeshot.Grid grid = new devDept.Eyeshot.Grid(new devDept.Geometry.Point3D(-100.0, -100.0, 0.0), new devDept.Geometry.Point3D(100.0, 100.0, 0.0), 10.0, new devDept.Geometry.Plane(new devDept.Geometry.Point3D(0.0, 0.0, 0.0), new devDept.Geometry.Vector3D(0.0, 0.0, 1.0)), System.Drawing.Color.FromArgb(63, 128, 128, 128), System.Drawing.Color.FromArgb(127, 255, 0, 0), System.Drawing.Color.FromArgb(127, 0, 128, 0), false, false, false, false, 10, 100, 10, System.Drawing.Color.FromArgb(127, 90, 90, 90), System.Drawing.Color.Transparent, false, System.Drawing.Color.FromArgb(12, 0, 0, 255));
		devDept.Eyeshot.OriginSymbol originSymbol = new devDept.Eyeshot.OriginSymbol(10, devDept.Eyeshot.originSymbolStyleType.Ball, System.Drawing.Color.Black, System.Drawing.Color.Red, System.Drawing.Color.Green, System.Drawing.Color.Blue, "Origin", "X", "Y", "Z", false, null, false);
		devDept.Eyeshot.RotateSettings rotate = new devDept.Eyeshot.RotateSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.None), 10.0, true, 1.0, devDept.Eyeshot.rotationType.Trackball, devDept.Eyeshot.rotationCenterType.CursorLocation, new devDept.Geometry.Point3D(0.0, 0.0, 0.0), false);
		devDept.Eyeshot.ZoomSettings zoom = new devDept.Eyeshot.ZoomSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.Shift), 25, true, devDept.Eyeshot.zoomStyleType.AtCursorLocation, false, 1.0, System.Drawing.Color.Empty, devDept.Eyeshot.Camera.perspectiveFitType.Accurate, false, 10, true);
		devDept.Eyeshot.PanSettings pan = new devDept.Eyeshot.PanSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.Ctrl), 25, true);
		devDept.Eyeshot.NavigationSettings navigation = new devDept.Eyeshot.NavigationSettings(devDept.Eyeshot.Camera.navigationType.Examine, new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Left, devDept.Eyeshot.modifierKeys.None), new devDept.Geometry.Point3D(-1000.0, -1000.0, -1000.0), new devDept.Geometry.Point3D(1000.0, 1000.0, 1000.0), 8.0, 50.0, 50.0);
		devDept.Eyeshot.Viewport.SavedViewsManager savedViews = new devDept.Eyeshot.Viewport.SavedViewsManager(8);
		devDept.Eyeshot.Viewport viewport = new devDept.Eyeshot.Viewport(new System.Drawing.Point(0, 0), new System.Drawing.Size(618, 667), background, camera, new devDept.Eyeshot.ToolBar[1] { toolBar }, devDept.Eyeshot.displayType.Rendered, true, false, false, false, new devDept.Eyeshot.Grid[1] { grid }, new devDept.Eyeshot.OriginSymbol[1] { originSymbol }, false, rotate, zoom, pan, navigation, savedViews, devDept.Eyeshot.viewType.Trimetric);
		devDept.Eyeshot.CoordinateSystemIcon coordinateSystemIcon = new devDept.Eyeshot.CoordinateSystemIcon(System.Drawing.Color.Black, System.Drawing.Color.FromArgb(80, 80, 80), System.Drawing.Color.FromArgb(80, 80, 80), System.Drawing.Color.OrangeRed, "Origin", "X", "Y", "Z", true, devDept.Eyeshot.coordinateSystemPositionType.BottomLeft, 37, false);
		devDept.Eyeshot.ViewCubeIcon viewCubeIcon = new devDept.Eyeshot.ViewCubeIcon(devDept.Eyeshot.coordinateSystemPositionType.TopRight, true, System.Drawing.Color.FromArgb(30, 144, 255), true, "FRONT", "BACK", "LEFT", "RIGHT", "TOP", "BOTTOM", System.Drawing.Color.FromArgb(240, 77, 77, 77), System.Drawing.Color.FromArgb(240, 77, 77, 77), System.Drawing.Color.FromArgb(240, 77, 77, 77), System.Drawing.Color.FromArgb(240, 77, 77, 77), System.Drawing.Color.FromArgb(240, 77, 77, 77), System.Drawing.Color.FromArgb(240, 77, 77, 77), 'S', 'N', 'W', 'E', true, System.Drawing.Color.White, System.Drawing.Color.Black, 120, true, true, null, null, null, null, null, null, false);
		this.GroupBox1 = new System.Windows.Forms.GroupBox();
		this.Model1 = new devDept.Eyeshot.Model();
		this.GroupBox1.SuspendLayout();
		((System.ComponentModel.ISupportInitialize)this.Model1).BeginInit();
		base.SuspendLayout();
		this.GroupBox1.Controls.Add(this.Model1);
		this.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.GroupBox1.Location = new System.Drawing.Point(0, 0);
		this.GroupBox1.Name = "GroupBox1";
		this.GroupBox1.Size = new System.Drawing.Size(624, 686);
		this.GroupBox1.TabIndex = 2;
		this.GroupBox1.TabStop = false;
		this.GroupBox1.Text = "3D";
		this.Model1.Cursor = System.Windows.Forms.Cursors.Default;
		this.Model1.Dock = System.Windows.Forms.DockStyle.Fill;
		this.Model1.Location = new System.Drawing.Point(3, 16);
		this.Model1.Name = "Model1";
		this.Model1.ProgressBar = progressBar;
		this.Model1.Size = new System.Drawing.Size(618, 667);
		this.Model1.TabIndex = 0;
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
		base.Name = "UC_3D";
		base.Size = new System.Drawing.Size(624, 686);
		this.GroupBox1.ResumeLayout(false);
		((System.ComponentModel.ISupportInitialize)this.Model1).EndInit();
		base.ResumeLayout(false);
	}

	public UC_3D()
	{
		Load += UC_3D_Load;
		InitializeComponent();
		Model1.Unlock("PS20-57PM1-2YJ1Q-5UW0-RY76");
		devDept.Eyeshot.ToolBarButton[] array = new devDept.Eyeshot.ToolBarButton[4];
		Bitmap upload = Resources.Upload;
		array[0] = new devDept.Eyeshot.ToolBarButton(upload, "SavePDF", "Save 3D PDF file", devDept.Eyeshot.ToolBarButton.styleType.PushButton, visible: true);
		array[1] = Model1.ActiveViewport.ToolBar.Buttons[4];
		array[2] = Model1.ActiveViewport.ToolBar.Buttons[5];
		array[3] = Model1.ActiveViewport.ToolBar.Buttons[6];
		Model1.ActiveViewport.ToolBar.Buttons = new ToolBarButtonList(Model1.ActiveViewport.ToolBar, array);
	}

	private void UC_3D_Load(object sender, EventArgs e)
	{
		Model1.ActiveViewport.ToolBar.Buttons[0].Click += SaveButtonClickEventHandler;
		Model1.Rendered.SilhouettesDrawingMode = silhouettesDrawingType.Never;
		Model1.Rendered.EnvironmentMapping = false;
		Mesh mesh = new Mesh();
		List<Point3D> list = new List<Point3D>();
		checked
		{
			Public2.CoordMeters coordM = default;
			Public2.CoordMeters coordM2 = default;
			if (!Information.IsNothing(Public2.PRJ.ElLN) | !Information.IsNothing(Public2.PRJ.ElPT))
			{
				coordM.Xm = 10000000000.0;
				coordM.Ym = 10000000000.0;
				coordM2.Xm = -10000000000.0;
				coordM2.Ym = -10000000000.0;
				if (!Information.IsNothing(Public2.PRJ.ElLN))
				{
					int upperBound = Public2.PRJ.ElLN.GetUpperBound(0);
					for (int i = 0; i <= upperBound; i++)
					{
						int upperBound2 = Public2.PRJ.ElLN[i].CoordWM.GetUpperBound(0);
						for (int j = 0; j <= upperBound2; j++)
						{
							Public2.CoordMeters coordMeters = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.ElLN[i].CoordWM[j]);
							list.Add(new Point3D(coordMeters.Xm, coordMeters.Ym, Public2.PRJ.ElLN[i].H));
							if (coordMeters.Xm > coordM2.Xm)
							{
								coordM2.Xm = coordMeters.Xm;
							}
							if (coordMeters.Ym > coordM2.Ym)
							{
								coordM2.Ym = coordMeters.Ym;
							}
							if (coordMeters.Xm < coordM.Xm)
							{
								coordM.Xm = coordMeters.Xm;
							}
							if (coordMeters.Ym < coordM.Ym)
							{
								coordM.Ym = coordMeters.Ym;
							}
						}
					}
				}
				if (!Information.IsNothing(Public2.PRJ.ElPT))
				{
					int upperBound3 = Public2.PRJ.ElPT.GetUpperBound(0);
					for (int k = 0; k <= upperBound3; k++)
					{
						Public2.CoordMeters coordMeters2 = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.ElPT[k].CoordWM);
						list.Add(new Point3D(coordMeters2.Xm, coordMeters2.Ym, Public2.PRJ.ElPT[k].H));
						if (coordMeters2.Xm > coordM2.Xm)
						{
							coordM2.Xm = coordMeters2.Xm;
						}
						if (coordMeters2.Ym > coordM2.Ym)
						{
							coordM2.Ym = coordMeters2.Ym;
						}
						if (coordMeters2.Xm < coordM.Xm)
						{
							coordM.Xm = coordMeters2.Xm;
						}
						if (coordMeters2.Ym < coordM.Ym)
						{
							coordM.Ym = coordMeters2.Ym;
						}
					}
					foreach (Point3D item in list)
					{
						if (Math.Abs(item.X - coordM2.Xm) < 1.0)
						{
							item.X = coordM2.Xm;
						}
						if (Math.Abs(item.X - coordM.Xm) < 1.0)
						{
							item.X = coordM.Xm;
						}
						if (Math.Abs(item.Y - coordM2.Ym) < 1.0)
						{
							item.Y = coordM2.Ym;
						}
						if (Math.Abs(item.Y - coordM.Ym) < 1.0)
						{
							item.Y = coordM.Ym;
						}
					}
				}
			}
			else
			{
				Public2.CoordMeters coordMeters3 = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.SETT.HorAreaCoordWM[0]);
				Public2.CoordMeters coordMeters4 = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.SETT.HorAreaCoordWM[1]);
				list.Add(new Point3D(coordMeters3.Xm, coordMeters3.Ym, 0.0));
				list.Add(new Point3D(coordMeters3.Xm, coordMeters4.Ym, 0.0));
				list.Add(new Point3D(coordMeters4.Xm, coordMeters3.Ym, 0.0));
				list.Add(new Point3D(coordMeters4.Xm, coordMeters4.Ym, 0.0));
				coordM.Xm = Public2.PRJ.SETT.HorAreaCoordM[0].Xm;
				coordM.Ym = Public2.PRJ.SETT.HorAreaCoordM[0].Ym;
				coordM2.Xm = Public2.PRJ.SETT.HorAreaCoordM[1].Xm;
				coordM2.Ym = Public2.PRJ.SETT.HorAreaCoordM[1].Ym;
			}
			mesh = UtilityEx.Triangulate(list, Mesh.natureType.RichSmooth);
			Public2.CoordWebMerc coordWMmin = CalcCoords.ConvMetersToWebMercMeters(coordM);
			Public2.CoordWebMerc coordWMmax = CalcCoords.ConvMetersToWebMercMeters(coordM2);
			Bitmap bitmap;
			if ((coordM2.Xm - coordM.Xm < 10000.0) & (coordM2.Ym - coordM.Ym < 10000.0))
			{
				if (coordM2.Xm - coordM.Xm > coordM2.Ym - coordM.Ym)
				{
					int num = (int)Math.Round((coordM2.Xm - coordM.Xm) * 2.5);
					bitmap = new Bitmap(num, (int)Math.Round((double)num * (coordM2.Ym - coordM.Ym) / (coordM2.Xm - coordM.Xm)));
				}
				else
				{
					int num2 = (int)Math.Round((coordM2.Ym - coordM.Ym) * 2.5);
					bitmap = new Bitmap((int)Math.Round((double)num2 * (coordM2.Xm - coordM.Xm) / (coordM2.Ym - coordM.Ym)), num2);
				}
			}
			else
			{
				bitmap = new Bitmap(100, 100);
			}
			if (Public2.PRJ.SETT.ShowMap)
			{
				bitmap = DrawMap(coordWMmin, coordWMmax, (short)bitmap.Width, (short)bitmap.Height);
			}
			Graphics graphics = Graphics.FromImage(bitmap);
			if (!Public2.PRJ.SETT.ShowMap)
			{
				graphics.Clear(Color.White);
			}
			double num3 = (double)bitmap.Width / (coordM2.Xm - coordM.Xm);
			if (!Information.IsNothing(Public2.PRJ.ROAD) & Public2.PRJ.SETT.ShowRoads)
			{
				int upperBound4 = Public2.PRJ.ROAD.GetUpperBound(0);
				for (int l = 0; l <= upperBound4; l++)
				{
					System.Drawing.Point[] array = new System.Drawing.Point[Public2.PRJ.ROAD[l].CoordWM.GetUpperBound(0) + 1];
					int upperBound5 = Public2.PRJ.ROAD[l].CoordWM.GetUpperBound(0);
					for (int m = 0; m <= upperBound5; m++)
					{
						Public2.CoordMeters coordMeters5 = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.ROAD[l].CoordWM[m]);
						array[m].X = (int)Math.Round((coordMeters5.Xm - coordM.Xm) * num3);
						array[m].Y = (int)Math.Round((double)bitmap.Height - (coordMeters5.Ym - coordM.Ym) * num3);
					}
					graphics.DrawLines(new Pen(Brushes.Black, (float)((double)unchecked((int)Public2.PRJ.ROAD[l].WidthM) * num3)), array);
				}
				int upperBound6 = Public2.PRJ.ROAD.GetUpperBound(0);
				for (int n = 0; n <= upperBound6; n++)
				{
					System.Drawing.Point[] array2 = new System.Drawing.Point[Public2.PRJ.ROAD[n].CoordWM.GetUpperBound(0) + 1];
					int upperBound7 = Public2.PRJ.ROAD[n].CoordWM.GetUpperBound(0);
					for (int num4 = 0; num4 <= upperBound7; num4++)
					{
						Public2.CoordMeters coordMeters6 = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.ROAD[n].CoordWM[num4]);
						array2[num4].X = (int)Math.Round((coordMeters6.Xm - coordM.Xm) * num3);
						array2[num4].Y = (int)Math.Round((double)bitmap.Height - (coordMeters6.Ym - coordM.Ym) * num3);
					}
					graphics.DrawLines(new Pen(Brushes.White, (float)unchecked((double)(int)Public2.PRJ.ROAD[n].WidthM * num3 - (double)(int)Public2.PRJ.ROAD[n].WidthBorder)), array2);
				}
			}
			Material material = new Material("Map", bitmap);
			material.MinifyingFunction = textureFilteringFunctionType.Linear;
			Model1.Materials.Add(material);
			if (bitmap.Width >= bitmap.Height)
			{
				mesh.ApplyMaterial("Map", textureMappingType.Plate, 1.0, (double)bitmap.Height / (double)bitmap.Width);
			}
			else
			{
				mesh.ApplyMaterial("Map", textureMappingType.Plate, (double)bitmap.Width / (double)bitmap.Height, 1.0);
			}
			Model1.Entities.Add(mesh, "Default");
			graphics.Dispose();
			GC.Collect();
			if (!Information.IsNothing(Public2.PRJ.BLD))
			{
				int upperBound8 = Public2.PRJ.BLD.GetUpperBound(0);
				for (int num5 = 0; num5 <= upperBound8; num5++)
				{
					LinearPath linearPath = new LinearPath();
					linearPath.Vertices = new Point3D[Public2.PRJ.BLD[num5].CoordWM.GetUpperBound(0) + 1];
					int upperBound9 = Public2.PRJ.BLD[num5].CoordWM.GetUpperBound(0);
					for (int num6 = 0; num6 <= upperBound9; num6++)
					{
						Public2.CoordMeters coordMeters7 = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.BLD[num5].CoordWM[num6]);
						linearPath.Vertices[num6] = new Point3D(coordMeters7.Xm, coordMeters7.Ym, Public2.PRJ.BLD[num5].Fundation);
					}
					devDept.Eyeshot.Entities.Region region = new devDept.Eyeshot.Entities.Region(linearPath);
					Mesh entity = ((!PolygonIsOrientedClockwise(Public2.PRJ.BLD[num5].CoordWM)) ? region.ExtrudeAsMesh(0f - Public2.PRJ.BLD[num5].H, 0.1, Mesh.natureType.RichSmooth) : region.ExtrudeAsMesh(Public2.PRJ.BLD[num5].H, 0.1, Mesh.natureType.RichSmooth));
					Model1.Entities.Add(entity, "Default", Color.Gray);
				}
			}
			if (!Information.IsNothing(Public2.PRJ.HP))
			{
				float originHeihgt = Module1.GetOriginHeihgt();
				if (Public2.PRJ.SETT.FlagLastRezManyLimits)
				{
					int num7 = 0;
					do
					{
						if (Public2.PRJ.SETT.HorizCutLimits[num7] && !Information.IsNothing(Public2.PRJ.HP_PLNwm[num7]))
						{
							int upperBound10 = Public2.PRJ.HP_PLNwm[num7].GetUpperBound(0);
							for (int num8 = 0; num8 <= upperBound10; num8++)
							{
								LinearPath linearPath2 = new LinearPath();
								linearPath2.Vertices = new Point3D[Public2.PRJ.HP_PLNwm[num7][num8].GetUpperBound(0) + 1];
								int upperBound11 = Public2.PRJ.HP_PLNwm[num7][num8].GetUpperBound(0);
								for (int num9 = 0; num9 <= upperBound11; num9++)
								{
									Public2.CoordMeters coordMeters8 = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.HP_PLNwm[num7][num8][num9]);
									float num10 = Public2.PRJ.HP[0].Height + originHeihgt;
									linearPath2.Vertices[num9] = new Point3D(coordMeters8.Xm, coordMeters8.Ym, num10);
								}
								if ((Public2.PRJ.HP_PLNwm[num7][num8][0].Xwm == Public2.PRJ.HP_PLNwm[num7][num8][Public2.PRJ.HP_PLNwm[num7][num8].GetUpperBound(0)].Xwm) & (Public2.PRJ.HP_PLNwm[num7][num8][0].Ywm == Public2.PRJ.HP_PLNwm[num7][num8][Public2.PRJ.HP_PLNwm[num7][num8].GetUpperBound(0)].Ywm))
								{
									devDept.Eyeshot.Entities.Region entity2 = new devDept.Eyeshot.Entities.Region(linearPath2);
									Model1.Entities.Add(entity2, "Default", Color.FromArgb(70, Public2.PRJ.SETT.LimitColor[num7].R, Public2.PRJ.SETT.LimitColor[num7].G, Public2.PRJ.SETT.LimitColor[num7].B));
								}
								else
								{
									Model1.Entities.Add(linearPath2, "Default", Public2.PRJ.SETT.LimitColor[num7]);
								}
							}
						}
						num7++;
					}
					while (num7 <= 3);
				}
				else
				{
					int upperBound12 = Public2.PRJ.HP.GetUpperBound(0);
					for (int num11 = 0; num11 <= upperBound12; num11++)
					{
						if (!Public2.PRJ.HP[num11].Activ || Information.IsNothing(Public2.PRJ.HP[num11].PLNwm))
						{
							continue;
						}
						int upperBound13 = Public2.PRJ.HP[num11].PLNwm.GetUpperBound(0);
						for (int num12 = 0; num12 <= upperBound13; num12++)
						{
							LinearPath linearPath3 = new LinearPath();
							linearPath3.Vertices = new Point3D[Public2.PRJ.HP[num11].PLNwm[num12].GetUpperBound(0) + 1];
							int upperBound14 = Public2.PRJ.HP[num11].PLNwm[num12].GetUpperBound(0);
							for (int num13 = 0; num13 <= upperBound14; num13++)
							{
								Public2.CoordMeters coordMeters9 = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.HP[num11].PLNwm[num12][num13]);
								float num14 = Public2.PRJ.HP[num11].Height + originHeihgt;
								linearPath3.Vertices[num13] = new Point3D(coordMeters9.Xm, coordMeters9.Ym, num14);
							}
							if ((Public2.PRJ.HP[num11].PLNwm[num12][0].Xwm == Public2.PRJ.HP[num11].PLNwm[num12][Public2.PRJ.HP[num11].PLNwm[num12].GetUpperBound(0)].Xwm) & (Public2.PRJ.HP[num11].PLNwm[num12][0].Ywm == Public2.PRJ.HP[num11].PLNwm[num12][Public2.PRJ.HP[num11].PLNwm[num12].GetUpperBound(0)].Ywm))
							{
								devDept.Eyeshot.Entities.Region entity3 = new devDept.Eyeshot.Entities.Region(linearPath3);
								Model1.Entities.Add(entity3, "Default", Color.FromArgb(70, Public2.PRJ.HP[num11].Color.R, Public2.PRJ.HP[num11].Color.G, Public2.PRJ.HP[num11].Color.B));
							}
							else
							{
								Model1.Entities.Add(linearPath3, "Default", Public2.PRJ.HP[num11].Color);
							}
						}
					}
				}
			}
			if (!Information.IsNothing(Public2.PRJ.VP))
			{
				int num15 = 0;
				do
				{
					if (Public2.PRJ.SETT.VertCutLimits[num15])
					{
						int upperBound15 = Public2.PRJ.VP.GetUpperBound(0);
						for (int num16 = 0; num16 <= upperBound15; num16++)
						{
							if (!Public2.PRJ.VP[num16].Activ || Information.IsNothing(Public2.PRJ.VP[num16].PLNs[num15]))
							{
								continue;
							}
							int upperBound16 = Public2.PRJ.VP[num16].PLNs[num15].GetUpperBound(0);
							for (int num17 = 0; num17 <= upperBound16; num17++)
							{
								LinearPath linearPath4 = new LinearPath();
								linearPath4.Vertices = new Point3D[Public2.PRJ.VP[num16].PLNs[num15][num17].GetUpperBound(0) + 1];
								int upperBound17 = Public2.PRJ.VP[num16].PLNs[num15][num17].GetUpperBound(0);
								for (int num18 = 0; num18 <= upperBound17; num18++)
								{
									float num19 = (float)(Public2.PRJ.VP[num16].Coord1M.Xm + (double)(Public2.PRJ.VP[num16].PLNs[num15][num17][num18].X / (float)Public2.PRJ.VP[num16].Size * (Public2.PRJ.VP[num16].LastL1 + Public2.PRJ.VP[num16].LastL2)) * Math.Sin((double)Public2.PRJ.VP[num16].Azimuth * Constants.PI_180));
									float num20 = (float)(Public2.PRJ.VP[num16].Coord1M.Ym + (double)(Public2.PRJ.VP[num16].PLNs[num15][num17][num18].X / (float)Public2.PRJ.VP[num16].Size * (Public2.PRJ.VP[num16].LastL1 + Public2.PRJ.VP[num16].LastL2)) * Math.Cos((double)Public2.PRJ.VP[num16].Azimuth * Constants.PI_180));
									float num21 = Public2.PRJ.VP[num16].Elev0 - Public2.PRJ.VP[num16].LastH2 + Public2.PRJ.VP[num16].PLNs[num15][num17][num18].Y / (float)Public2.PRJ.VP[num16].Size * (Public2.PRJ.VP[num16].LastH1 + Public2.PRJ.VP[num16].LastH2);
									linearPath4.Vertices[num18] = new Point3D(num19, num20, num21);
								}
								Model1.Entities.Add(linearPath4, "Default", Public2.PRJ.SETT.LimitColor[num15]);
							}
						}
					}
					num15++;
				}
				while (num15 <= 3);
			}
			if (!Information.IsNothing(Public2.PRJ.TXT))
			{
				int upperBound18 = Public2.PRJ.TXT.GetUpperBound(0);
				for (int num22 = 0; num22 <= upperBound18; num22++)
				{
					if (Form1_Public.MastDimension(Public2.PRJ.TXT[num22].Text) > 0f)
					{
						Public2.CoordMeters coordMeters10 = CalcCoords.ConvWebMercMetersToMetersRound(Public2.PRJ.TXT[num22].CoordWM);
						DrawMast3D(Public2.PRJ.TXT[num22].Text, (float)coordMeters10.Xm, (float)coordMeters10.Ym, Triangul.FindElevWithBldPlusHbld(Public2.PRJ.TXT[num22].CoordWM));
					}
				}
			}
			Model1.Camera.ProjectionMode = projectionType.Orthographic;
			Model1.SetView(viewType.vcFrontFaceTop);
			Model1.ZoomFit();
			Model1.Invalidate();
		}
	}

	private Bitmap DrawMap(Public2.CoordWebMerc CoordWMmin, Public2.CoordWebMerc CoordWMmax, short Width, short Height)
	{
		GC.Collect();
		GC.GetTotalMemory(forceFullCollection: true);
		double Xt = default;
		double Yt = default;
		CalcCoords.ConvWebMercMetersToTile(CoordWMmin, ref Xt, ref Yt);
		double Xt2 = default;
		double Yt2 = default;
		CalcCoords.ConvWebMercMetersToTile(CoordWMmax, ref Xt2, ref Yt2);
		checked
		{
			int num = (int)Math.Floor(Xt);
			int num2 = (int)Math.Floor(Yt2);
			int num3 = (int)Math.Ceiling(Xt2);
			int num4 = (int)Math.Ceiling(Yt);
			short num5 = (short)((num3 - num) * 256);
			short num6 = (short)((num4 - num2) * 256);
			CalcTileWeb.ListTiles = null;
			int num7 = -1;
			string NameTileCACH = default;
			string NameTileURL = default;
			string NameTileDir = default;
			if (Public2.PRJ.SETT.ShowMap)
			{
				int num8 = num3;
				for (int i = num; i <= num8; i++)
				{
					int num9 = num4;
					for (int j = num2; j <= num9; j++)
					{
						_ = (int)Conversion.Int((double)i / 1024.0);
						_ = (int)Conversion.Int((double)j / 1024.0);
						CalcTileWeb.CalcNameTile(i, j, Conversions.ToString(Public2.PRJ.SETT.ZOOM), MyProject.Forms.Form1.NameInCacheTileServer, MyProject.Forms.Form1.ExtentionTileServer, MyProject.Forms.Form1.UrlTileServer, ref NameTileCACH, ref NameTileURL, ref NameTileDir);
						if (MyProject.Computer.FileSystem.FileExists(NameTileCACH))
						{
							continue;
						}
						if (!Information.IsNothing(CalcTileWeb.ListTiles))
						{
							bool flag = true;
							int upperBound = CalcTileWeb.ListTiles.GetUpperBound(0);
							for (int k = 0; k <= upperBound; k++)
							{
								if (Operators.CompareString(CalcTileWeb.ListTiles[k].FileName, NameTileCACH, TextCompare: false) == 0)
								{
									flag = false;
									break;
								}
							}
							if (flag)
							{
								num7++;
								CalcTileWeb.ListTiles = (CalcTileWeb.TileItem[])Utils.CopyArray(CalcTileWeb.ListTiles, new CalcTileWeb.TileItem[num7 + 1]);
								CalcTileWeb.ListTiles[num7].FileName = NameTileCACH;
								CalcTileWeb.ListTiles[num7].FolderName = NameTileDir;
								CalcTileWeb.ListTiles[num7].UrlName = NameTileURL;
								CalcTileWeb.ListTiles[num7].X = i;
								CalcTileWeb.ListTiles[num7].Y = j;
								CalcTileWeb.ListTiles[num7].Z = Public2.PRJ.SETT.ZOOM;
							}
						}
						else
						{
							num7 = 0;
							CalcTileWeb.ListTiles = (CalcTileWeb.TileItem[])Utils.CopyArray(CalcTileWeb.ListTiles, new CalcTileWeb.TileItem[num7 + 1]);
							CalcTileWeb.ListTiles[num7].FileName = NameTileCACH;
							CalcTileWeb.ListTiles[num7].FolderName = NameTileDir;
							CalcTileWeb.ListTiles[num7].UrlName = NameTileURL;
							CalcTileWeb.ListTiles[num7].X = i;
							CalcTileWeb.ListTiles[num7].Y = j;
							CalcTileWeb.ListTiles[num7].Z = Public2.PRJ.SETT.ZOOM;
						}
					}
				}
				if (!Information.IsNothing(CalcTileWeb.ListTiles))
				{
					MyProject.Forms.frmBckGrWrkDownloadTiles.ShowDialog();
				}
			}
			Bitmap image = new Bitmap(num5, num6);
			Graphics graphics = Graphics.FromImage(image);
			graphics.Clear(Color.White);
			if (Public2.PRJ.SETT.ShowMap)
			{
				int num10 = num3;
				for (int l = num; l <= num10; l++)
				{
					int num11 = num4;
					for (int m = num2; m <= num11; m++)
					{
						_ = (int)Conversion.Int((double)l / 1024.0);
						_ = (int)Conversion.Int((double)m / 1024.0);
						CalcTileWeb.CalcNameTile(l, m, Conversions.ToString(Public2.PRJ.SETT.ZOOM), MyProject.Forms.Form1.NameInCacheTileServer, MyProject.Forms.Form1.ExtentionTileServer, MyProject.Forms.Form1.UrlTileServer, ref NameTileCACH, ref NameTileURL, ref NameTileDir);
						if (MyProject.Computer.FileSystem.FileExists(NameTileCACH))
						{
							try
							{
								graphics.DrawImage(Module1.CBrightness2(new Bitmap(NameTileCACH), Public2.PRJ.SETT.MapBrightness), (l - num) * 256, (m - num2) * 256, 256, 256);
							}
							catch (Exception projectError)
							{
								ProjectData.SetProjectError(projectError);
								ProjectData.ClearProjectError();
							}
						}
					}
				}
			}
			short num12 = (short)Math.Round((Xt - (double)num) * 256.0);
			short num13 = (short)Math.Round((Yt2 - (double)num2) * 256.0);
			short num14 = (short)Math.Round((Xt2 - Xt) * 256.0);
			short num15 = (short)Math.Round((Yt - Yt2) * 256.0);
			Bitmap bitmap = new Bitmap(num14, num15);
			graphics = Graphics.FromImage(bitmap);
			graphics.DrawImage(image, (short)unchecked(-num12), (short)unchecked(-num13), num5, num6);
			image = new Bitmap(Width, Height);
			graphics = Graphics.FromImage(image);
			graphics.DrawImage(bitmap, 0, 0, Width, Height);
			bitmap.Dispose();
			graphics.Dispose();
			GC.Collect();
			GC.GetTotalMemory(forceFullCollection: true);
			return image;
		}
	}

	public void DrawMast3D(string STR, float Xo, float Yo, float Zo)
	{
		float num;
		float num2;
		float num3;
		float num4;
		try
		{
			num = Module1.QSNG(STR.Split('/')[1]);
			num2 = Module1.QSNG(STR.Split('/')[2]);
			num3 = Module1.QSNG(STR.Split('/')[3]);
			num4 = Module1.QSNG(STR.Split('/')[4]);
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
			return;
		}
		if ((num3 == 0f) | (num4 == 0f))
		{
			return;
		}
		new Pen(Color.Black, 2f);
		checked
		{
			if (num2 > 0f)
			{
				float num5 = num4 / 2f;
				float num6 = 0f;
				float num7 = num3 / 2f;
				float num8 = num2;
				LinearPath entity = new LinearPath(new Point3D(Xo - num5, Yo - num5, Zo + num6), new Point3D(Xo - num7, Yo - num7, Zo + num8));
				Model1.Entities.Add(entity, "Default", Color.Black);
				LinearPath entity2 = new LinearPath(new Point3D(Xo - num5, Yo + num5, Zo + num6), new Point3D(Xo - num7, Yo + num7, Zo + num8));
				Model1.Entities.Add(entity2, "Default", Color.Black);
				LinearPath entity3 = new LinearPath(new Point3D(Xo + num5, Yo - num5, Zo + num6), new Point3D(Xo + num7, Yo - num7, Zo + num8));
				Model1.Entities.Add(entity3, "Default", Color.Black);
				LinearPath entity4 = new LinearPath(new Point3D(Xo + num5, Yo + num5, Zo + num6), new Point3D(Xo + num7, Yo + num7, Zo + num8));
				Model1.Entities.Add(entity4, "Default", Color.Black);
				short num9 = (short)Math.Ceiling(0.5 * (double)num2 / (double)num3);
				float num10 = num2 / (float)num9;
				int num11 = num9;
				for (int i = 1; i <= num11; i++)
				{
					float num12 = (float)i * num10;
					float num13 = (float)((double)(num3 / 2f) + 0.5 * (double)(num4 - num3) * (double)(num9 - i) / (double)num9);
					float num14 = (float)(i - 1) * num10;
					float num15 = (float)((double)(num3 / 2f) + 0.5 * (double)(num4 - num3) * (double)(num9 - i + 1) / (double)num9);
					LinearPath entity5 = new LinearPath(new Point3D(Xo - num13, Yo - num13, Zo + num12), new Point3D(Xo + num13, Yo - num13, Zo + num12));
					Model1.Entities.Add(entity5, "Default", Color.Black);
					LinearPath entity6 = new LinearPath(new Point3D(Xo - num13, Yo + num13, Zo + num12), new Point3D(Xo + num13, Yo + num13, Zo + num12));
					Model1.Entities.Add(entity6, "Default", Color.Black);
					LinearPath entity7 = new LinearPath(new Point3D(Xo - num13, Yo - num13, Zo + num12), new Point3D(Xo - num13, Yo + num13, Zo + num12));
					Model1.Entities.Add(entity7, "Default", Color.Black);
					LinearPath entity8 = new LinearPath(new Point3D(Xo + num13, Yo - num13, Zo + num12), new Point3D(Xo + num13, Yo + num13, Zo + num12));
					Model1.Entities.Add(entity8, "Default", Color.Black);
					LinearPath entity9 = new LinearPath(new Point3D(Xo - num15, Yo - num15, Zo + num14), new Point3D(Xo + num13, Yo - num13, Zo + num12));
					Model1.Entities.Add(entity9, "Default", Color.Black);
					LinearPath entity10 = new LinearPath(new Point3D(Xo + num15, Yo - num15, Zo + num14), new Point3D(Xo - num13, Yo - num13, Zo + num12));
					Model1.Entities.Add(entity10, "Default", Color.Black);
					LinearPath entity11 = new LinearPath(new Point3D(Xo - num15, Yo + num15, Zo + num14), new Point3D(Xo + num13, Yo + num13, Zo + num12));
					Model1.Entities.Add(entity11, "Default", Color.Black);
					LinearPath entity12 = new LinearPath(new Point3D(Xo + num15, Yo + num15, Zo + num14), new Point3D(Xo - num13, Yo + num13, Zo + num12));
					Model1.Entities.Add(entity12, "Default", Color.Black);
					LinearPath entity13 = new LinearPath(new Point3D(Xo - num15, Yo - num15, Zo + num14), new Point3D(Xo - num13, Yo + num13, Zo + num12));
					Model1.Entities.Add(entity13, "Default", Color.Black);
					LinearPath entity14 = new LinearPath(new Point3D(Xo - num15, Yo + num15, Zo + num14), new Point3D(Xo - num13, Yo - num13, Zo + num12));
					Model1.Entities.Add(entity14, "Default", Color.Black);
					LinearPath entity15 = new LinearPath(new Point3D(Xo + num15, Yo - num15, Zo + num14), new Point3D(Xo + num13, Yo + num13, Zo + num12));
					Model1.Entities.Add(entity15, "Default", Color.Black);
					LinearPath entity16 = new LinearPath(new Point3D(Xo + num15, Yo + num15, Zo + num14), new Point3D(Xo + num13, Yo - num13, Zo + num12));
					Model1.Entities.Add(entity16, "Default", Color.Black);
				}
			}
			if (num > 0f)
			{
				float num16 = num3 / 2f;
				float num17 = num2;
				float num18 = num3 / 2f;
				float num19 = num2 + num;
				Model1.Entities.Add(new LinearPath(new Point3D(Xo - num16, Yo - num16, Zo + num17), new Point3D(Xo - num18, Yo - num18, Zo + num19)), "Default", Color.Black);
				Model1.Entities.Add(new LinearPath(new Point3D(Xo - num16, Yo + num16, Zo + num17), new Point3D(Xo - num18, Yo + num18, Zo + num19)), "Default", Color.Black);
				Model1.Entities.Add(new LinearPath(new Point3D(Xo + num16, Yo - num16, Zo + num17), new Point3D(Xo + num18, Yo - num18, Zo + num19)), "Default", Color.Black);
				Model1.Entities.Add(new LinearPath(new Point3D(Xo + num16, Yo + num16, Zo + num17), new Point3D(Xo + num18, Yo + num18, Zo + num19)), "Default", Color.Black);
				short num20 = (short)Math.Ceiling(0.5 * (double)num / (double)num3);
				float num21 = num / (float)num20;
				int num22 = num20;
				for (int j = 1; j <= num22; j++)
				{
					float num23 = num2 + (float)j * num21;
					float num24 = num3 / 2f;
					float num25 = num2 + (float)(j - 1) * num21;
					float num26 = num3 / 2f;
					Model1.Entities.Add(new LinearPath(new Point3D(Xo - num24, Yo - num24, Zo + num23), new Point3D(Xo + num24, Yo - num24, Zo + num23)), "Default", Color.Black);
					Model1.Entities.Add(new LinearPath(new Point3D(Xo - num24, Yo + num24, Zo + num23), new Point3D(Xo + num24, Yo + num24, Zo + num23)), "Default", Color.Black);
					Model1.Entities.Add(new LinearPath(new Point3D(Xo - num24, Yo - num24, Zo + num23), new Point3D(Xo - num24, Yo + num24, Zo + num23)), "Default", Color.Black);
					Model1.Entities.Add(new LinearPath(new Point3D(Xo + num24, Yo - num24, Zo + num23), new Point3D(Xo + num24, Yo + num24, Zo + num23)), "Default", Color.Black);
					Model1.Entities.Add(new LinearPath(new Point3D(Xo - num26, Yo - num26, Zo + num25), new Point3D(Xo + num24, Yo - num24, Zo + num23)), "Default", Color.Black);
					Model1.Entities.Add(new LinearPath(new Point3D(Xo + num26, Yo - num26, Zo + num25), new Point3D(Xo - num24, Yo - num24, Zo + num23)), "Default", Color.Black);
					Model1.Entities.Add(new LinearPath(new Point3D(Xo - num26, Yo + num26, Zo + num25), new Point3D(Xo + num24, Yo + num24, Zo + num23)), "Default", Color.Black);
					Model1.Entities.Add(new LinearPath(new Point3D(Xo + num26, Yo + num26, Zo + num25), new Point3D(Xo - num24, Yo + num24, Zo + num23)), "Default", Color.Black);
					Model1.Entities.Add(new LinearPath(new Point3D(Xo - num26, Yo - num26, Zo + num25), new Point3D(Xo - num24, Yo + num24, Zo + num23)), "Default", Color.Black);
					Model1.Entities.Add(new LinearPath(new Point3D(Xo - num26, Yo + num26, Zo + num25), new Point3D(Xo - num24, Yo - num24, Zo + num23)), "Default", Color.Black);
					Model1.Entities.Add(new LinearPath(new Point3D(Xo + num26, Yo - num26, Zo + num25), new Point3D(Xo + num24, Yo + num24, Zo + num23)), "Default", Color.Black);
					Model1.Entities.Add(new LinearPath(new Point3D(Xo + num26, Yo + num26, Zo + num25), new Point3D(Xo + num24, Yo - num24, Zo + num23)), "Default", Color.Black);
				}
			}
		}
	}

	public void SaveButtonClickEventHandler(object sender, EventArgs e)
	{
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.Filter = "3D PDF file (*.pdf)|*.pdf";
		saveFileDialog.AddExtension = true;
		saveFileDialog.CheckPathExists = true;
		if (saveFileDialog.ShowDialog() == DialogResult.OK)
		{
			WriteFileAsync workUnit = new WritePDF(new WritePdfParams(Model1), saveFileDialog.FileName);
			Model1.StartWork(workUnit);
		}
	}

	public void DrawMast(float Xo, float Yo, float Zo, float H, float HeightSect, float WidthSect, ref Model M)
	{
		checked
		{
			short num = (short)Math.Floor(H / HeightSect);
			float num2 = WidthSect / 2f;
			float num3 = HeightSect / 2f;
			LinearPath entity = new LinearPath(new Point3D(Xo - num2, Yo - num2, Zo), new Point3D(Xo - num2, Yo - num2, Zo + HeightSect * (float)(num + 1)));
			Model1.Entities.Add(entity, "Default", Color.Black);
			LinearPath entity2 = new LinearPath(new Point3D(Xo - num2, Yo + num2, Zo), new Point3D(Xo - num2, Yo + num2, Zo + HeightSect * (float)(num + 1)));
			Model1.Entities.Add(entity2, "Default", Color.Black);
			LinearPath entity3 = new LinearPath(new Point3D(Xo + num2, Yo + num2, Zo), new Point3D(Xo + num2, Yo + num2, Zo + HeightSect * (float)(num + 1)));
			Model1.Entities.Add(entity3, "Default", Color.Black);
			LinearPath entity4 = new LinearPath(new Point3D(Xo + num2, Yo - num2, Zo), new Point3D(Xo + num2, Yo - num2, Zo + HeightSect * (float)(num + 1)));
			Model1.Entities.Add(entity4, "Default", Color.Black);
			int num4 = num;
			for (int i = 0; i <= num4; i++)
			{
				LinearPath entity5 = new LinearPath(new Point3D(Xo - num2, Yo - num2, Zo + (float)i * HeightSect), new Point3D(Xo - num2, Yo + num2, Zo + (float)i * HeightSect), new Point3D(Xo + num2, Yo + num2, Zo + (float)i * HeightSect), new Point3D(Xo + num2, Yo - num2, Zo + (float)i * HeightSect), new Point3D(Xo - num2, Yo - num2, Zo + (float)i * HeightSect));
				Model1.Entities.Add(entity5, "Default", Color.Black);
				LinearPath entity6 = new LinearPath(new Point3D(Xo - num2, Yo - num2, Zo + (float)i * HeightSect), new Point3D(Xo - num2, Yo + num2, Zo + (float)i * HeightSect + num3), new Point3D(Xo - num2, Yo - num2, Zo + (float)i * HeightSect + HeightSect));
				Model1.Entities.Add(entity6, "Default", Color.Black);
				LinearPath entity7 = new LinearPath(new Point3D(Xo - num2, Yo + num2, Zo + (float)i * HeightSect), new Point3D(Xo + num2, Yo + num2, Zo + (float)i * HeightSect + num3), new Point3D(Xo - num2, Yo + num2, Zo + (float)i * HeightSect + HeightSect));
				Model1.Entities.Add(entity7, "Default", Color.Black);
				LinearPath entity8 = new LinearPath(new Point3D(Xo + num2, Yo + num2, Zo + (float)i * HeightSect), new Point3D(Xo + num2, Yo - num2, Zo + (float)i * HeightSect + num3), new Point3D(Xo + num2, Yo + num2, Zo + (float)i * HeightSect + HeightSect));
				Model1.Entities.Add(entity8, "Default", Color.Black);
				LinearPath entity9 = new LinearPath(new Point3D(Xo + num2, Yo - num2, Zo + (float)i * HeightSect), new Point3D(Xo - num2, Yo - num2, Zo + (float)i * HeightSect + num3), new Point3D(Xo + num2, Yo - num2, Zo + (float)i * HeightSect + HeightSect));
				Model1.Entities.Add(entity9, "Default", Color.Black);
			}
			LinearPath entity10 = new LinearPath(new Point3D(Xo - num2, Yo - num2, Zo + (float)(num + 1) * HeightSect), new Point3D(Xo - num2, Yo + num2, Zo + (float)(num + 1) * HeightSect), new Point3D(Xo + num2, Yo + num2, Zo + (float)(num + 1) * HeightSect), new Point3D(Xo + num2, Yo - num2, Zo + (float)(num + 1) * HeightSect), new Point3D(Xo - num2, Yo - num2, Zo + (float)(num + 1) * HeightSect));
			Model1.Entities.Add(entity10, "Default", Color.Black);
		}
	}

	private bool PolygonIsOrientedClockwise(Public2.CoordWebMerc[] points)
	{
		return SignedPolygonArea(points) < 0f;
	}

	private float PolygonArea(Public2.CoordWebMerc[] points)
	{
		return Math.Abs(SignedPolygonArea(points));
	}

	private float SignedPolygonArea(Public2.CoordWebMerc[] points)
	{
		checked
		{
			points = (Public2.CoordWebMerc[])Utils.CopyArray(points, new Public2.CoordWebMerc[points.Length + 1]);
			points[points.Length - 1] = points[0];
			float num = 0f;
			int num2 = points.Length - 2;
			for (int i = 0; i <= num2; i++)
			{
				num = (float)((double)num + (points[i + 1].Xwm - points[i].Xwm) * (points[i + 1].Ywm + points[i].Ywm) / 2.0);
			}
			return num;
		}
	}
}
