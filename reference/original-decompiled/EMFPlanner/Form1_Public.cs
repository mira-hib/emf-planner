using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using EMFPlanner.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[StandardModule]
internal sealed class Form1_Public
{
	public static Image IconImage;

	public static Color[] DgvColor = new Color[12]
	{
		Color.White,
		Color.LightCoral,
		Color.LightCyan,
		Color.LightGoldenrodYellow,
		Color.LightGreen,
		Color.LightPink,
		Color.LightSalmon,
		Color.LightSeaGreen,
		Color.LightSkyBlue,
		Color.LightYellow,
		Color.LightSteelBlue,
		Color.Yellow
	};

	public static string NodeName;

	public static Public2.CoordWebMerc ScrCoord1;

	public static Public2.CoordWebMerc ScrCoord2;

	public static bool RulerOn;

	public static Public2.CoordWebMerc[] Ruler = new Public2.CoordWebMerc[2];

	public static int IndexTX;

	public static float PowerW;

	public static float CableLoss;

	public static string ChooseAntennaPuth;

	public static short CPGSelRow;

	public static bool StartDrawingBuilding;

	public static bool StartDeletingNodes;

	public static bool StartAddingNodes;

	public static bool StartSplittingNodes;

	public static Public2.StructBuilding NewBLD;

	public static bool StartDrawingRoad;

	public static bool StartDrawingFence;

	public static bool StartDrawingText;

	public static Public2.StructRoad NewRoad;

	public static Public2.StructFence NewFence;

	public static Public2.StructText NewText;

	public static string LastText = "text";

	public static Image LastIcon;

	public static string[] FontList;

	public static bool ShowTriangles;

	public static bool StartDrawingElevPoint;

	public static bool StartDrawingElevLine;

	public static Public2.StructElevPoint NewElevPoint;

	public static Public2.StructElevLine NewElevLine;

	public static float[] HdnPolNorm_h = new float[360];

	public static float[] HdnPolNorm_v = new float[360];

	public static string AntennaName;

	public static string AntennaGain;

	public static bool HeightSpanOK;

	public static float HeightSpanMin = 20f;

	public static float HeightSpanMax = 25f;

	public static float HeightSpanDh = 0.5f;

	public static bool AzimuthSpanOK;

	public static float AzimuthSpanMin = 0f;

	public static float AzimuthSpanMax = 360f;

	public static float AzimuthSpanDh = 20f;

	public static bool Height05OK;

	public static float HeightDH05 = 0.5f;

	public static bool AutoBuildingHeightOK;

	public static float HgtBuilding = 20f;

	public static float HgtFloor = 3f;

	public static float FundationHeight;

	public static bool AreaSizeOK;

	public static float AreaWidth1 = 50f;

	public static float AreaWidth2 = 50f;

	public static float AreaHeight1 = 50f;

	public static float AreaHeight2 = 50f;

	public static short VPSelRow;

	public static short LineTypeIndex;

	public static ClassDN.AntennaPatternClass PatEnv;

	public static bool ClickOK;

	public static void SaveProject(string FileName)
	{
		new BinaryFormatter();
		using (File.Create(FileName))
		{
		}
	}

	public static void OpenProject(string FileName)
	{
		try
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			using Stream serializationStream = File.Open(FileName, FileMode.Open);
			object obj = binaryFormatter.Deserialize(serializationStream);
			Public2.PRJ = ((obj != null) ? ((Public2.Project)obj) : default(Public2.Project));
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Interaction.MsgBox(Localization.LocMSG(104));
			ProjectData.ClearProjectError();
		}
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.TX))
			{
				int upperBound = Public2.PRJ.TX.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					Public2.PRJ.TX[i].UgH = Module1.TchkOrZpt(Public2.PRJ.TX[i].UgH);
					Public2.PRJ.TX[i].UgV = Module1.TchkOrZpt(Public2.PRJ.TX[i].UgV);
				}
			}
			Public2.AppSetup.OriginCoordWM = Module1.DeepClone(Public2.PRJ.SETT.OriginCoordWM);
			Public2.AppSetup.OriginCoordWGS = Module1.DeepClone(Public2.PRJ.SETT.OriginCoordWGS);
			Public2.AppSetup.PRTOCoordWGS = Module1.DeepClone(Public2.PRJ.SETT.PRTOCoordWGS);
			Public2.AppSetup.HorAreaCoordWM = Module1.DeepClone(Public2.PRJ.SETT.HorAreaCoordWM);
			Public2.AppSetup.HorAreaCoordM = Module1.DeepClone(Public2.PRJ.SETT.HorAreaCoordM);
			if (Public2.PRJ.SETT.LineWidthVertCut == 0)
			{
				Public2.PRJ.SETT.LineWidthVertCut = 3;
			}
			if (Public2.PRJ.SETT.LineHightAboveTerrain == 0f)
			{
				Public2.PRJ.SETT.LineHightAboveTerrain = 2f;
			}
			if (Public2.PRJ.SETT.ControlPointMesureFreq == 0f)
			{
				Public2.PRJ.SETT.ControlPointMesureFreq = 1000f;
			}
			if (!Information.IsNothing(Public2.PRJ.TX))
			{
				int upperBound2 = Public2.PRJ.TX.GetUpperBound(0);
				for (int j = 0; j <= upperBound2; j++)
				{
					if (Information.IsNothing(Public2.PRJ.TX[j].Alpha24))
					{
						Public2.PRJ.TX[j].Alpha24 = new float[4];
						Public2.PRJ.TX[j].Fpr = new float[4];
						Public2.PRJ.TX[j].Ftdc = new float[4];
						int num = 0;
						do
						{
							Public2.PRJ.TX[j].Alpha24[num] = 1f;
							Public2.PRJ.TX[j].Fpr[num] = 1f;
							Public2.PRJ.TX[j].Ftdc[num] = 1f;
							num++;
						}
						while (num <= 3);
					}
				}
			}
			if (Public2.PRJ.SETT.CircleStep == 0f)
			{
				Public2.PRJ.SETT.CircleStep = 50f;
			}
			if (Public2.PRJ.SETT.HorizCutLimits == null)
			{
				Public2.PRJ.SETT.HorizCutLimits = new bool[4];
				Public2.PRJ.SETT.HorizCutLimits[0] = true;
			}
			if (Public2.PRJ.SETT.VertCutLimits == null)
			{
				Public2.PRJ.SETT.VertCutLimits = new bool[4];
				Public2.PRJ.SETT.VertCutLimits[0] = true;
			}
			if (Public2.PRJ.SETT.LimitAlpha24 == null)
			{
				Public2.AppSetup.LimitAlpha24 = new bool[4] { true, true, true, true };
				Public2.AppSetup.LimitFtdc = new bool[4] { true, true, true, true };
				Public2.AppSetup.LimitFpr = new bool[4] { true, true, true, true };
				Public2.PRJ.SETT.LimitAlpha24 = Module1.DeepClone(Public2.AppSetup.LimitAlpha24);
				Public2.PRJ.SETT.LimitFtdc = Module1.DeepClone(Public2.AppSetup.LimitFtdc);
				Public2.PRJ.SETT.LimitFpr = Module1.DeepClone(Public2.AppSetup.LimitFpr);
			}
		}
	}

	public static Public2.CoordWebMerc GetScreenCenterWM()
	{
		Public2.CoordWebMerc CoordWM = default;
		CalcCoords.ConvTileToWebMercMeters((double)MyProject.Forms.Form1.TileX + (double)MyProject.Forms.Form1.Panel1.Width / 512.0 - (double)MyProject.Forms.Form1.PointX / 256.0, (double)MyProject.Forms.Form1.TileY + (double)MyProject.Forms.Form1.Panel1.Height / 512.0 - (double)MyProject.Forms.Form1.PointY / 256.0, ref CoordWM);
		return CoordWM;
	}

	public static void SetScreenBorders()
	{
		CalcCoords.ConvTileToWebMercMeters((double)MyProject.Forms.Form1.TileX - (double)MyProject.Forms.Form1.PointX / 256.0, (double)MyProject.Forms.Form1.TileY - (double)MyProject.Forms.Form1.PointY / 256.0, ref ScrCoord1);
		checked
		{
			CalcCoords.ConvTileToWebMercMeters((double)MyProject.Forms.Form1.TileX + (double)(MyProject.Forms.Form1.Panel1.Width - MyProject.Forms.Form1.PointX) / 256.0, (double)MyProject.Forms.Form1.TileY + (double)(MyProject.Forms.Form1.Panel1.Height - MyProject.Forms.Form1.PointY) / 256.0, ref ScrCoord2);
		}
	}

	public static bool ShowPointOnMap(Public2.CoordWebMerc CoordWM)
	{
		if ((CoordWM.Xwm > ScrCoord1.Xwm) & (CoordWM.Xwm < ScrCoord2.Xwm) & (CoordWM.Ywm < ScrCoord1.Ywm) & (CoordWM.Ywm > ScrCoord2.Ywm))
		{
			return true;
		}
		return false;
	}

	public static bool PointIsideArea(Public2.CoordWebMerc CoordWM)
	{
		if ((CoordWM.Xwm > Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm) & (CoordWM.Xwm < Public2.PRJ.SETT.HorAreaCoordWM[1].Xwm) & (CoordWM.Ywm > Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm) & (CoordWM.Ywm < Public2.PRJ.SETT.HorAreaCoordWM[1].Ywm))
		{
			return true;
		}
		return false;
	}

	public static void FillFontList()
	{
		short num = 0;
		FontFamily[] families = FontFamily.Families;
		checked
		{
			foreach (FontFamily fontFamily in families)
			{
				try
				{
					new Font(fontFamily.Name, 8f);
					FontList = (string[])Utils.CopyArray(FontList, new string[num + 1]);
					FontList[num] = fontFamily.Name;
					num++;
				}
				catch (Exception ex)
				{
					ProjectData.SetProjectError(ex);
					Exception ex2 = ex;
					ProjectData.ClearProjectError();
				}
			}
		}
	}

	[DllImport("psapi.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
	private static extern int EmptyWorkingSet(int hProcess);

	public static void ClearMemory()
	{
		Process[] processes = Process.GetProcesses();
		foreach (Process process in processes)
		{
			try
			{
				EmptyWorkingSet((int)process.Handle);
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				ProjectData.ClearProjectError();
			}
		}
	}

	public static SizeF MeasureString(string s, Font font)
	{
		using Bitmap image = new Bitmap(1, 1);
		object obj = Graphics.FromImage(image);
		try
		{
			object[] array = new object[2] { s, font };
			object[] array2 = array;
			bool[] array3 = new bool[2] { true, true };
			bool[] array4 = array3;
			object obj2 = NewLateBinding.LateGet(obj, null, "MeasureString", array, null, null, array3);
			if (array4[0])
			{
				s = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array2[0]), typeof(string));
			}
			if (array4[1])
			{
				font = (Font)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array2[1]), typeof(Font));
			}
			return (obj2 != null) ? ((SizeF)obj2) : default(SizeF);
		}
		finally
		{
			if (obj != null)
			{
				((IDisposable)obj).Dispose();
			}
		}
	}

	public static string Azimuth360(float Az)
	{
		if (Az == -1f)
		{
			return "0÷360";
		}
		return Conversions.ToString(Az);
	}

	public static string FreqSpanString(float F, float F2)
	{
		if (!Information.IsNothing(F2) & (F2 > 0f))
		{
			return F + "-" + F2;
		}
		return F.ToString();
	}

	public static bool FreqSpanCheck(string InputSTR, ref float F, ref float F2)
	{
		if (Versioned.IsNumeric(InputSTR))
		{
			if (Conversions.ToSingle(InputSTR) < 0f)
			{
				F = 1000f;
				F2 = 0f;
				return false;
			}
			F = Conversions.ToSingle(InputSTR);
			F2 = 0f;
			return true;
		}
		if (Information.IsNothing(InputSTR))
		{
			F = 1000f;
			F2 = 0f;
			return false;
		}
		if (InputSTR.Split('-').GetUpperBound(0) == 1)
		{
			string text = InputSTR.Split('-')[0];
			string text2 = InputSTR.Split('-')[1];
			if (Versioned.IsNumeric(text) & Versioned.IsNumeric(text2))
			{
				if ((Conversions.ToSingle(text) > 30f) & (Conversions.ToSingle(text2) > 30f) & (Conversions.ToSingle(text2) >= Conversions.ToSingle(text)))
				{
					F = Conversions.ToSingle(text);
					F2 = Conversions.ToSingle(text2);
					return true;
				}
				if ((Conversions.ToSingle(text) > 30f) & (Conversions.ToSingle(text2) > 30f) & (Conversions.ToSingle(text2) < Conversions.ToSingle(text)))
				{
					F = Conversions.ToSingle(text2);
					F2 = Conversions.ToSingle(text);
					return true;
				}
				F = 1000f;
				F2 = 0f;
				return false;
			}
			F = 1000f;
			F2 = 0f;
			return false;
		}
		F = 1000f;
		F2 = 0f;
		return false;
	}

	public static float FreqSpanCalc(float F, float F2)
	{
		if (!Information.IsNothing(F2) & (F2 > 0f))
		{
			return (float)Math.Round((F + F2) / 2f, 1);
		}
		return F;
	}

	public static void DrawMast2D(string STR, float Xo, float Yo, double kX, double kY, PaintEventArgs e)
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
		if ((num3 == 0f) | (num4 == 0f) | (kX == 0.0) | (kY == 0.0))
		{
			return;
		}
		Pen pen = new Pen(Color.Black, 2f);
		checked
		{
			if (num2 > 0f)
			{
				float num5 = (float)((double)(num4 / 2f) * kX);
				float num6 = 0f;
				float num7 = (float)((double)(num3 / 2f) * kX);
				float num8 = (float)((double)num2 * kY);
				e.Graphics.DrawLine(pen, Xo - num5, Yo - num6, Xo - num7, Yo - num8);
				e.Graphics.DrawLine(pen, Xo + num5, Yo - num6, Xo + num7, Yo - num8);
				short num9 = (short)Math.Ceiling(0.5 * (double)num2 / (double)num3);
				float num10 = num2 / (float)num9;
				int num11 = num9;
				for (int i = 1; i <= num11; i++)
				{
					float num12 = (float)((double)((float)i * num10) * kY);
					float num13 = (float)(((double)(num3 / 2f) + 0.5 * (double)(num4 - num3) * (double)(num9 - i) / (double)num9) * kX);
					float num14 = (float)((double)((float)(i - 1) * num10) * kY);
					float num15 = (float)(((double)(num3 / 2f) + 0.5 * (double)(num4 - num3) * (double)(num9 - i + 1) / (double)num9) * kX);
					e.Graphics.DrawLine(pen, Xo - num13, Yo - num12, Xo + num13, Yo - num12);
					e.Graphics.DrawLine(pen, Xo - num13, Yo - num12, Xo + num15, Yo - num14);
					e.Graphics.DrawLine(pen, Xo + num13, Yo - num12, Xo - num15, Yo - num14);
				}
			}
			if (num > 0f)
			{
				float num16 = (float)((double)(num3 / 2f) * kX);
				float num17 = (float)((double)num2 * kY);
				float num18 = (float)((double)(num3 / 2f) * kX);
				float num19 = (float)((double)(num2 + num) * kY);
				e.Graphics.DrawLine(pen, Xo - num16, Yo - num17, Xo - num18, Yo - num19);
				e.Graphics.DrawLine(pen, Xo + num16, Yo - num17, Xo + num18, Yo - num19);
				short num20 = (short)Math.Ceiling(0.5 * (double)num / (double)num3);
				float num21 = num / (float)num20;
				int num22 = num20;
				for (int j = 1; j <= num22; j++)
				{
					float num23 = (float)((double)(num2 + (float)j * num21) * kY);
					float num24 = (float)((double)(num3 / 2f) * kX);
					float num25 = (float)((double)(num2 + (float)(j - 1) * num21) * kY);
					float num26 = (float)((double)(num3 / 2f) * kX);
					e.Graphics.DrawLine(pen, Xo - num24, Yo - num23, Xo + num24, Yo - num23);
					e.Graphics.DrawLine(pen, Xo - num24, Yo - num23, Xo + num26, Yo - num25);
					e.Graphics.DrawLine(pen, Xo + num24, Yo - num23, Xo - num26, Yo - num25);
				}
			}
		}
	}

	public static void DrawMast2D(string STR, float Xo, float Yo, double kX, double kY, Graphics e)
	{
		float num = default;
		float num2 = default;
		float num3 = default;
		float num4 = default;
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
		}
		if ((num3 == 0f) | (num4 == 0f) | (kX == 0.0) | (kY == 0.0))
		{
			return;
		}
		Pen pen = new Pen(Color.Black, 2f);
		checked
		{
			if (num2 > 0f)
			{
				float num5 = (float)((double)(num4 / 2f) * kX);
				float num6 = 0f;
				float num7 = (float)((double)(num3 / 2f) * kX);
				float num8 = (float)((double)num2 * kY);
				e.DrawLine(pen, Xo - num5, Yo - num6, Xo - num7, Yo - num8);
				e.DrawLine(pen, Xo + num5, Yo - num6, Xo + num7, Yo - num8);
				short num9 = (short)Math.Ceiling(0.5 * (double)num2 / (double)num3);
				float num10 = num2 / (float)num9;
				int num11 = num9;
				for (int i = 1; i <= num11; i++)
				{
					float num12 = (float)((double)((float)i * num10) * kY);
					float num13 = (float)(((double)(num3 / 2f) + 0.5 * (double)(num4 - num3) * (double)(num9 - i) / (double)num9) * kX);
					float num14 = (float)((double)((float)(i - 1) * num10) * kY);
					float num15 = (float)(((double)(num3 / 2f) + 0.5 * (double)(num4 - num3) * (double)(num9 - i + 1) / (double)num9) * kX);
					e.DrawLine(pen, Xo - num13, Yo - num12, Xo + num13, Yo - num12);
					e.DrawLine(pen, Xo - num13, Yo - num12, Xo + num15, Yo - num14);
					e.DrawLine(pen, Xo + num13, Yo - num12, Xo - num15, Yo - num14);
				}
			}
			if (num > 0f)
			{
				float num16 = (float)((double)(num3 / 2f) * kX);
				float num17 = (float)((double)num2 * kY);
				float num18 = (float)((double)(num3 / 2f) * kX);
				float num19 = (float)((double)(num2 + num) * kY);
				e.DrawLine(pen, Xo - num16, Yo - num17, Xo - num18, Yo - num19);
				e.DrawLine(pen, Xo + num16, Yo - num17, Xo + num18, Yo - num19);
				short num20 = (short)Math.Ceiling(0.5 * (double)num / (double)num3);
				float num21 = num / (float)num20;
				int num22 = num20;
				for (int j = 1; j <= num22; j++)
				{
					float num23 = (float)((double)(num2 + (float)j * num21) * kY);
					float num24 = (float)((double)(num3 / 2f) * kX);
					float num25 = (float)((double)(num2 + (float)(j - 1) * num21) * kY);
					float num26 = (float)((double)(num3 / 2f) * kX);
					e.DrawLine(pen, Xo - num24, Yo - num23, Xo + num24, Yo - num23);
					e.DrawLine(pen, Xo - num24, Yo - num23, Xo + num26, Yo - num25);
					e.DrawLine(pen, Xo + num24, Yo - num23, Xo - num26, Yo - num25);
				}
			}
		}
	}

	public static float MastDimension(string STR)
	{
		float result;
		if (!STR.Contains("Tower"))
		{
			result = 0f;
		}
		else if (STR.Split('/').GetUpperBound(0) != 4)
		{
			result = 0f;
		}
		else
		{
			float num;
			float num2;
			try
			{
				num = Module1.QSNG(STR.Split('/')[3]);
				num2 = Module1.QSNG(STR.Split('/')[4]);
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				result = 0f;
				ProjectData.ClearProjectError();
				goto IL_00a4;
			}
			result = ((!((num == 0f) | (num2 == 0f))) ? (num / 2f) : 0f);
		}
		goto IL_00a4;
		IL_00a4:
		return result;
	}
}
