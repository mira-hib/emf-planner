using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[StandardModule]
internal sealed class Module1
{
	public static string FolderTMP;

	public static Mutex TestMutex;

	private static float[] dXi = new float[94]
	{
		0f, 0.01f, 0.02f, 0.025f, 0.03f, 0.04f, 0.05f, 0.06f, 0.07f, 0.08f,
		0.09f, 0.1f, 0.2f, 0.25f, 0.3f, 0.4f, 0.5f, 0.6f, 0.7f, 0.8f,
		0.9f, 1f, 1.5f, 2f, 2.5f, 3f, 3.5f, 4f, 4.5f, 5f,
		5.5f, 6f, 6.5f, 7f, 7.5f, 8f, 8.5f, 9f, 9.5f, 10f,
		15f, 20f, 25f, 30f, 35f, 40f, 45f, 50f, 55f, 60f,
		65f, 70f, 75f, 80f, 85f, 90f, 95f, 100f, 150f, 200f,
		250f, 300f, 350f, 400f, 450f, 500f, 550f, 600f, 650f, 700f,
		750f, 800f, 850f, 900f, 950f, 1000f, 1500f, 2000f, 2500f, 3000f,
		3500f, 4000f, 4500f, 5000f, 5500f, 6000f, 6500f, 7000f, 7500f, 8000f,
		8500f, 9000f, 9500f, 10000f
	};

	private static byte NumSteps = 9;

	private static Public2.StructBuilding[][] BLD_Back;

	private static Public2.StructBuilding[][] BLD_Forward;

	private static Public2.StructRoad[][] ROAD_Back;

	private static Public2.StructRoad[][] ROAD_Forward;

	private static Public2.StructFence[][] FENCE_Back;

	private static Public2.StructFence[][] FENCE_Forward;

	private static Public2.StructText[][] TEXT_Back;

	private static Public2.StructText[][] TEXT_Forward;

	private static Public2.StructElevLine[][] ElLN_Back;

	private static Public2.StructElevLine[][] ElLN_Forward;

	private static Public2.StructElevPoint[][] ElPT_Back;

	private static Public2.StructElevPoint[][] ElPT_Forward;

	public static string SYMBOLS(string STR)
	{
		string expression = Strings.Replace(STR, "\r", "");
		expression = Strings.Replace(expression, "\n", "");
		expression = Strings.Replace(expression, "/", "_");
		expression = Strings.Replace(expression, "|", "_");
		expression = Strings.Replace(expression, "\\", "_");
		expression = Strings.Replace(expression, "<", "_");
		expression = Strings.Replace(expression, ">", "_");
		expression = Strings.Replace(expression, "*", "_");
		expression = Strings.Replace(expression, "?", "_");
		expression = Strings.Replace(expression, ":", "_");
		return Strings.Replace(expression, "\"", "_");
	}

	public static string ZptToTchk(string STR)
	{
		return Strings.Replace(STR, ",", ".");
	}

	public static bool ValidText(string InputText, bool CheckLimits, float Minimum, float Maximum, bool IncludeMin, bool IncludeMax, bool Message)
	{
		InputText = ((Operators.CompareString(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator, ",", TextCompare: false) != 0) ? Strings.Replace(InputText, ",", ".") : Strings.Replace(InputText, ".", ","));
		bool flag;
		if (!Versioned.IsNumeric(InputText))
		{
			if (Message)
			{
				Interaction.MsgBox(Localization.LocMSG(30) + " " + InputText + " " + Localization.LocMSG(31), MsgBoxStyle.Critical);
			}
			flag = false;
		}
		else
		{
			float num = QSNG(InputText);
			flag = true;
			if (CheckLimits)
			{
				if (IncludeMin)
				{
					if (num < Minimum)
					{
						flag = false;
					}
				}
				else if (num <= Minimum)
				{
					flag = false;
				}
				if (IncludeMax)
				{
					if (num > Maximum)
					{
						flag = false;
					}
				}
				else if (num >= Maximum)
				{
					flag = false;
				}
			}
			if (Message & !flag)
			{
				Interaction.MsgBox(Localization.LocMSG(30) + " " + InputText + " " + Localization.LocMSG(31), MsgBoxStyle.Critical);
			}
		}
		return flag;
	}

	public static void TextPropertyValidating(ref string TextProperty, ref double DblVariable, bool CheckLimits, float Minimum, float Maximum, bool IncludeMin, bool IncludeMax, bool Message)
	{
		if (ValidText(TextProperty, CheckLimits, Minimum, Maximum, IncludeMin, IncludeMax, Message))
		{
			DblVariable = QDBL(Conversions.ToDouble(TextProperty));
			TextProperty = Conversions.ToString(QDBL(Conversions.ToDouble(TextProperty)));
		}
		else
		{
			TextProperty = Conversions.ToString(Math.Round(DblVariable, 6));
		}
	}

	public static float QSNG(string STR)
	{
		if (Operators.CompareString(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator, ",", TextCompare: false) == 0)
		{
			return Conversions.ToSingle(Strings.Replace(STR, ".", ","));
		}
		return Conversions.ToSingle(Strings.Replace(STR, ",", "."));
	}

	public static double QDBL(double STR)
	{
		if (Operators.CompareString(Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator, ",", TextCompare: false) == 0)
		{
			return Conversions.ToDouble(Strings.Replace(Conversions.ToString(STR), ".", ","));
		}
		return Conversions.ToDouble(Strings.Replace(Conversions.ToString(STR), ",", "."));
	}

	public static float RSNG(string STR)
	{
		STR = TchkToZpt(STR);
		if (Versioned.IsNumeric(STR))
		{
			return Conversions.ToSingle(STR);
		}
		return 0f;
	}

	public static string TchkToZpt(string STR)
	{
		if (Operators.CompareString(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ",", TextCompare: false) == 0)
		{
			return Strings.Replace(STR, ".", ",");
		}
		return STR;
	}

	public static string TchkOrZpt(string STR)
	{
		if (Operators.CompareString(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ".", TextCompare: false) == 0)
		{
			return Strings.Replace(STR, ",", ".");
		}
		if (Operators.CompareString(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ",", TextCompare: false) == 0)
		{
			return Strings.Replace(STR, ".", ",");
		}
		return STR;
	}

	public static string Reject0D0A(string STR)
	{
		string expression = Strings.Replace(STR, "\r", "");
		return Strings.Replace(expression, "\n", "");
	}

	public static string Reject0D0A09(string STR)
	{
		string expression = Strings.Replace(STR, "\r", "");
		expression = Strings.Replace(expression, "\n", "");
		return Strings.Replace(expression, "\t", " ");
	}

	public static T DeepClone<T>(T obj)
	{
		if (Information.IsNothing(obj))
		{
			return default;
		}
		using MemoryStream memoryStream = new MemoryStream();
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		binaryFormatter.Serialize(memoryStream, obj);
		memoryStream.Position = 0L;
		return Conversions.ToGenericParameter<T>(binaryFormatter.Deserialize(memoryStream));
	}

	public static void SetDoubleBuffered(Control control)
	{
		typeof(Control).InvokeMember("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetProperty, null, control, new object[1] { true });
	}

	public static string SuperFormat(float SNG, byte N)
	{
		return Strings.Format(SNG, "0.00000000").Substring(0, N);
	}

	public static PHP_SQL.CoordMercator CoordCenterSign(PHP_SQL.CoordMercator[] CoordMerc)
	{
		double y = CoordMerc[0].Y;
		double num = y;
		double num2 = 1E+99;
		bool flag = false;
		int upperBound = CoordMerc.GetUpperBound(0);
		checked
		{
			for (int i = 1; i <= upperBound; i++)
			{
				if (CoordMerc[i].Y < y)
				{
					y = CoordMerc[i].Y;
				}
				if (CoordMerc[i].Y > num)
				{
					num = CoordMerc[i].Y;
				}
			}
			PHP_SQL.CoordMercator result = default;
			result.Y = (num + y) / 2.0;
			int num3 = CoordMerc.GetUpperBound(0) - 1;
			double num5 = default;
			for (int j = 0; j <= num3; j++)
			{
				if (((CoordMerc[j].Y > result.Y) & (CoordMerc[j + 1].Y < result.Y)) | ((CoordMerc[j].Y < result.Y) & (CoordMerc[j + 1].Y > result.Y)))
				{
					double num4 = CoordMerc[j].X + (CoordMerc[j + 1].X - CoordMerc[j].X) * (result.Y - CoordMerc[j].Y) / (CoordMerc[j + 1].Y - CoordMerc[j].Y);
					if (!flag)
					{
						num5 = num4;
						flag = true;
					}
					else if (num4 < num5)
					{
						num2 = num5;
						num5 = num4;
					}
					else if (num4 < num2)
					{
						num2 = num4;
					}
				}
			}
			result.X = (num2 + num5) / 2.0;
			return result;
		}
	}

	public static Public2.CoordWebMerc CoordCenterSign1(Public2.CoordWebMerc[] CoordP)
	{
		checked
		{
			Public2.CoordWebMerc result = default;
			if (CoordP.GetUpperBound(0) < 3)
			{
				result.Xwm = CoordP[1].Xwm;
				result.Ywm = CoordP[1].Ywm;
			}
			else
			{
				double ywm = CoordP[1].Ywm;
				double num = ywm;
				int upperBound = CoordP.GetUpperBound(0);
				for (int i = 1; i <= upperBound; i++)
				{
					if (CoordP[i].Ywm < ywm)
					{
						ywm = CoordP[i].Ywm;
					}
					if (CoordP[i].Ywm > num)
					{
						num = CoordP[i].Ywm;
					}
				}
				result.Ywm = (num + ywm) / 2.0;
				short num2 = 0;
				int num3 = CoordP.GetUpperBound(0) - 1;
				double[] array = default;
				for (int j = 0; j <= num3; j++)
				{
					if (((CoordP[j].Ywm >= result.Ywm) & (CoordP[j + 1].Ywm < result.Ywm)) | ((CoordP[j].Ywm < result.Ywm) & (CoordP[j + 1].Ywm >= result.Ywm)))
					{
						array = (double[])Utils.CopyArray(array, new double[num2 + 1]);
						array[num2] = CoordP[j].Xwm + (CoordP[j + 1].Xwm - CoordP[j].Xwm) * (result.Ywm - CoordP[j].Ywm) / (CoordP[j + 1].Ywm - CoordP[j].Ywm);
						num2++;
					}
				}
				int upperBound2 = array.GetUpperBound(0);
				for (int k = 0; k <= upperBound2; k++)
				{
					short num4 = (short)(k + 1);
					short num5 = (short)array.GetUpperBound(0);
					for (num2 = num4; num2 <= num5; num2 = (short)unchecked(num2 + 1))
					{
						if (array[num2] < array[k])
						{
							ywm = array[k];
							array[k] = array[num2];
							array[num2] = ywm;
						}
					}
				}
				num = 0.0;
				int upperBound3 = array.GetUpperBound(0);
				for (int l = 0; l <= upperBound3; l += 2)
				{
					ywm = array[l + 1] - array[l];
					if (ywm > num)
					{
						num = ywm;
						result.Xwm = (array[l + 1] + array[l]) / 2.0;
					}
				}
			}
			return result;
		}
	}

	public static PHP_SQL.CoordMercator CoordCenterSign2(PHP_SQL.CoordMercator[] CoordMerc)
	{
		double y = CoordMerc[0].Y;
		double num = y;
		int upperBound = CoordMerc.GetUpperBound(0);
		checked
		{
			for (int i = 1; i <= upperBound; i++)
			{
				if (CoordMerc[i].Y < y)
				{
					y = CoordMerc[i].Y;
				}
				if (CoordMerc[i].Y > num)
				{
					num = CoordMerc[i].Y;
				}
			}
			PHP_SQL.CoordMercator result = default;
			result.Y = (num + y) / 2.0;
			short num2 = 0;
			int num3 = CoordMerc.GetUpperBound(0) - 1;
			double[] array = default;
			for (int j = 0; j <= num3; j++)
			{
				if (((CoordMerc[j].Y > result.Y) & (CoordMerc[j + 1].Y < result.Y)) | ((CoordMerc[j].Y < result.Y) & (CoordMerc[j + 1].Y > result.Y)))
				{
					array = (double[])Utils.CopyArray(array, new double[num2 + 1]);
					array[num2] = CoordMerc[j].X + (CoordMerc[j + 1].X - CoordMerc[j].X) * (result.Y - CoordMerc[j].Y) / (CoordMerc[j + 1].Y - CoordMerc[j].Y);
					num2++;
				}
			}
			int upperBound2 = array.GetUpperBound(0);
			for (int k = 0; k <= upperBound2; k++)
			{
				short num4 = (short)(k + 1);
				short num5 = (short)array.GetUpperBound(0);
				for (num2 = num4; num2 <= num5; num2 = (short)unchecked(num2 + 1))
				{
					if (array[num2] < array[k])
					{
						y = array[k];
						array[k] = array[num2];
						array[num2] = y;
					}
				}
			}
			num = 0.0;
			int upperBound3 = array.GetUpperBound(0);
			for (int l = 0; l <= upperBound3; l += 2)
			{
				y = array[l + 1] - array[l];
				if (y > num)
				{
					num = y;
					result.X = (array[l + 1] + array[l]) / 2.0;
				}
			}
			return result;
		}
	}

	public static bool CalcCrossOtrez(double X1, double Y1, double X2, double Y2, double X3, double Y3, double X4, double Y4, ref double Xcross, ref double Ycross)
	{
		double num = ((X3 - X1) * (Y2 - Y1) - (Y3 - Y1) * (X2 - X1)) * ((X4 - X1) * (Y2 - Y1) - (Y4 - Y1) * (X2 - X1));
		double num2 = ((X1 - X3) * (Y4 - Y3) - (Y1 - Y3) * (X4 - X3)) * ((X2 - X3) * (Y4 - Y3) - (Y2 - Y3) * (X4 - X3));
		if ((num < 0.0) & (num2 < 0.0))
		{
			if (X4 < X3)
			{
				double num3 = X3;
				double num4 = Y3;
				X3 = X4;
				Y3 = Y4;
				X4 = num3;
				Y4 = num4;
			}
			if ((X2 - X1 != 0.0) & (X4 - X3 != 0.0))
			{
				double num5 = (Y2 - Y1) / (X2 - X1);
				double num6 = (Y4 - Y3) / (X4 - X3);
				Xcross = (num5 * X1 - num6 * X3 + Y3 - Y1) / (num5 - num6);
				Ycross = num5 * (Xcross - X1) + Y1;
			}
			else if (X2 == X1)
			{
				double num6 = (Y4 - Y3) / (X4 - X3);
				Xcross = X1;
				Ycross = num6 * (Xcross - X3) + Y3;
			}
			else if (X3 == X4)
			{
				double num5 = (Y2 - Y1) / (X2 - X1);
				Xcross = X3;
				Ycross = num5 * (Xcross - X1) + Y1;
			}
			return true;
		}
		return false;
	}

	public static bool CalcCrossOtrezM(Public2.CoordMeters P1, Public2.CoordMeters P2, Public2.CoordMeters P3, Public2.CoordMeters P4, ref Public2.CoordMeters P)
	{
		double num = ((P3.Xm - P1.Xm) * (P2.Ym - P1.Ym) - (P3.Ym - P1.Ym) * (P2.Xm - P1.Xm)) * ((P4.Xm - P1.Xm) * (P2.Ym - P1.Ym) - (P4.Ym - P1.Ym) * (P2.Xm - P1.Xm));
		double num2 = ((P1.Xm - P3.Xm) * (P4.Ym - P3.Ym) - (P1.Ym - P3.Ym) * (P4.Xm - P3.Xm)) * ((P2.Xm - P3.Xm) * (P4.Ym - P3.Ym) - (P2.Ym - P3.Ym) * (P4.Xm - P3.Xm));
		if ((num < 0.0) & (num2 < 0.0))
		{
			if (P4.Xm < P3.Xm)
			{
				double xm = P3.Xm;
				double ym = P3.Ym;
				P3.Xm = P4.Xm;
				P3.Ym = P4.Ym;
				P4.Xm = xm;
				P4.Ym = ym;
			}
			if ((P2.Xm - P1.Xm != 0.0) & (P4.Xm - P3.Xm != 0.0))
			{
				double num3 = (P2.Ym - P1.Ym) / (P2.Xm - P1.Xm);
				double num4 = (P4.Ym - P3.Ym) / (P4.Xm - P3.Xm);
				P.Xm = (num3 * P1.Xm - num4 * P3.Xm + P3.Ym - P1.Ym) / (num3 - num4);
				P.Ym = num3 * (P.Xm - P1.Xm) + P1.Ym;
			}
			else if (P2.Xm == P1.Xm)
			{
				double num4 = (P4.Ym - P3.Ym) / (P4.Xm - P3.Xm);
				P.Xm = P1.Xm;
				P.Ym = num4 * (P.Xm - P3.Xm) + P3.Ym;
			}
			else if (P3.Xm == P4.Xm)
			{
				double num3 = (P2.Ym - P1.Ym) / (P2.Xm - P1.Xm);
				P.Xm = P3.Xm;
				P.Ym = num3 * (P.Xm - P1.Xm) + P1.Ym;
			}
			return true;
		}
		return false;
	}

	public static bool IsPointInsidePolygonF(Public2.CoordWebMerc PNT, Public2.CoordWebMerc[] PLG)
	{
		Public2.CoordWebMerc coordWebMerc = PLG[0];
		Public2.CoordWebMerc coordWebMerc2 = PLG[0];
		int upperBound = PLG.GetUpperBound(0);
		checked
		{
			for (int i = 0; i <= upperBound; i++)
			{
				if (PLG[i].Xwm > coordWebMerc2.Xwm)
				{
					coordWebMerc2.Xwm = PLG[i].Xwm;
				}
				if (PLG[i].Xwm < coordWebMerc.Xwm)
				{
					coordWebMerc.Xwm = PLG[i].Xwm;
				}
				if (PLG[i].Ywm > coordWebMerc2.Ywm)
				{
					coordWebMerc2.Ywm = PLG[i].Ywm;
				}
				if (PLG[i].Ywm < coordWebMerc.Ywm)
				{
					coordWebMerc.Ywm = PLG[i].Ywm;
				}
			}
			bool flag;
			if ((PNT.Xwm < coordWebMerc.Xwm) | (PNT.Xwm > coordWebMerc2.Xwm) | (PNT.Ywm < coordWebMerc.Ywm) | (PNT.Ywm > coordWebMerc2.Ywm))
			{
				flag = false;
			}
			else
			{
				flag = false;
				int num = PLG.GetUpperBound(0);
				for (int j = 0; j < PLG.GetUpperBound(0) + 1; j++)
				{
					if (((PLG[j].Ywm < PNT.Ywm && PLG[num].Ywm >= PNT.Ywm) || (PLG[num].Ywm < PNT.Ywm && PLG[j].Ywm >= PNT.Ywm)) && PLG[j].Xwm + (PNT.Ywm - PLG[j].Ywm) / (PLG[num].Ywm - PLG[j].Ywm) * (PLG[num].Xwm - PLG[j].Xwm) < PNT.Xwm)
					{
						flag = !flag;
					}
					num = j;
				}
			}
			return flag;
		}
	}

	public static bool IsPointInsidePolygon(Public2.CoordWebMerc PNT, Public2.CoordWebMerc[] PLG)
	{
		bool flag = false;
		int num = PLG.GetUpperBound(0);
		checked
		{
			for (int i = 0; i < PLG.GetUpperBound(0) + 1; i++)
			{
				if (((PLG[i].Ywm < PNT.Ywm && PLG[num].Ywm >= PNT.Ywm) || (PLG[num].Ywm < PNT.Ywm && PLG[i].Ywm >= PNT.Ywm)) && PLG[i].Xwm + (PNT.Ywm - PLG[i].Ywm) / (PLG[num].Ywm - PLG[i].Ywm) * (PLG[num].Xwm - PLG[i].Xwm) < PNT.Xwm)
				{
					flag = !flag;
				}
				num = i;
			}
			return flag;
		}
	}

	public static bool IsPointInsidePolygonI(int X, int Y, int[] Xp, int[] Yp)
	{
		bool flag = false;
		int num = Xp.GetUpperBound(0);
		checked
		{
			for (int i = 0; i < Xp.GetUpperBound(0) + 1; i++)
			{
				if (((Yp[i] < Y && Yp[num] >= Y) || (Yp[num] < Y && Yp[i] >= Y)) && (double)Xp[i] + (double)(Y - Yp[i]) / (double)(Yp[num] - Yp[i]) * (double)(Xp[num] - Xp[i]) < (double)X)
				{
					flag = !flag;
				}
				num = i;
			}
			return flag;
		}
	}

	public static bool IsPointInsideTriangleF(Public2.CoordWebMerc PNT, Public2.CoordWebMerc PNTa, Public2.CoordWebMerc PNTb, Public2.CoordWebMerc PNTc)
	{
		Public2.CoordWebMerc coordWebMerc = default;
		coordWebMerc.Xwm = Math.Min(PNTa.Xwm, Math.Min(PNTb.Xwm, PNTc.Xwm));
		Public2.CoordWebMerc coordWebMerc2 = default;
		coordWebMerc2.Xwm = Math.Max(PNTa.Xwm, Math.Max(PNTb.Xwm, PNTc.Xwm));
		coordWebMerc.Ywm = Math.Min(PNTa.Ywm, Math.Min(PNTb.Ywm, PNTc.Ywm));
		coordWebMerc2.Ywm = Math.Max(PNTa.Ywm, Math.Max(PNTb.Ywm, PNTc.Ywm));
		if ((PNT.Xwm < coordWebMerc.Xwm) | (PNT.Xwm > coordWebMerc2.Xwm) | (PNT.Ywm < coordWebMerc.Ywm) | (PNT.Ywm > coordWebMerc2.Ywm))
		{
			return false;
		}
		return ((PNT.Xwm - PNTa.Xwm) * (PNTb.Ywm - PNTa.Ywm) - (PNT.Ywm - PNTa.Ywm) * (PNTb.Xwm - PNTa.Xwm)) * ((PNTc.Xwm - PNTa.Xwm) * (PNTb.Ywm - PNTa.Ywm) - (PNTc.Ywm - PNTa.Ywm) * (PNTb.Xwm - PNTa.Xwm)) >= 0.0 && ((PNT.Xwm - PNTb.Xwm) * (PNTc.Ywm - PNTb.Ywm) - (PNT.Ywm - PNTb.Ywm) * (PNTc.Xwm - PNTb.Xwm)) * ((PNTa.Xwm - PNTb.Xwm) * (PNTc.Ywm - PNTb.Ywm) - (PNTa.Ywm - PNTb.Ywm) * (PNTc.Xwm - PNTb.Xwm)) >= 0.0 && ((PNT.Xwm - PNTc.Xwm) * (PNTa.Ywm - PNTc.Ywm) - (PNT.Ywm - PNTc.Ywm) * (PNTa.Xwm - PNTc.Xwm)) * ((PNTb.Xwm - PNTc.Xwm) * (PNTa.Ywm - PNTc.Ywm) - (PNTb.Ywm - PNTc.Ywm) * (PNTa.Xwm - PNTc.Xwm)) >= 0.0;
	}

	public static bool IsPointInsideTriangle(Public2.CoordWebMerc PNT, Public2.CoordWebMerc PNTa, Public2.CoordWebMerc PNTb, Public2.CoordWebMerc PNTc)
	{
		if (((PNT.Xwm - PNTa.Xwm) * (PNTb.Ywm - PNTa.Ywm) - (PNT.Ywm - PNTa.Ywm) * (PNTb.Xwm - PNTa.Xwm)) * ((PNTc.Xwm - PNTa.Xwm) * (PNTb.Ywm - PNTa.Ywm) - (PNTc.Ywm - PNTa.Ywm) * (PNTb.Xwm - PNTa.Xwm)) >= 0.0 && ((PNT.Xwm - PNTb.Xwm) * (PNTc.Ywm - PNTb.Ywm) - (PNT.Ywm - PNTb.Ywm) * (PNTc.Xwm - PNTb.Xwm)) * ((PNTa.Xwm - PNTb.Xwm) * (PNTc.Ywm - PNTb.Ywm) - (PNTa.Ywm - PNTb.Ywm) * (PNTc.Xwm - PNTb.Xwm)) >= 0.0)
		{
			return ((PNT.Xwm - PNTc.Xwm) * (PNTa.Ywm - PNTc.Ywm) - (PNT.Ywm - PNTc.Ywm) * (PNTa.Xwm - PNTc.Xwm)) * ((PNTb.Xwm - PNTc.Xwm) * (PNTa.Ywm - PNTc.Ywm) - (PNTb.Ywm - PNTc.Ywm) * (PNTa.Xwm - PNTc.Xwm)) >= 0.0;
		}
		return false;
	}

	public static bool IsPointInsideTriangleI(ref int X, ref int Y, int Xa, int Ya, int Xb, int Yb, int Xc, int Yc)
	{
		checked
		{
			if (((X - Xa) * (Yb - Ya) - (Y - Ya) * (Xb - Xa)) * ((Xc - Xa) * (Yb - Ya) - (Yc - Ya) * (Xb - Xa)) >= 0 && ((X - Xb) * (Yc - Yb) - (Y - Yb) * (Xc - Xb)) * ((Xa - Xb) * (Yc - Yb) - (Ya - Yb) * (Xc - Xb)) >= 0)
			{
				return ((X - Xc) * (Ya - Yc) - (Y - Yc) * (Xa - Xc)) * ((Xb - Xc) * (Ya - Yc) - (Yb - Yc) * (Xa - Xc)) >= 0;
			}
			return false;
		}
	}

	public static bool IsPointInsideTriangleD(ref double X, ref double Y, double Xa, double Ya, double Xb, double Yb, double Xc, double Yc)
	{
		if (((X - Xa) * (Yb - Ya) - (Y - Ya) * (Xb - Xa)) * ((Xc - Xa) * (Yb - Ya) - (Yc - Ya) * (Xb - Xa)) >= 0.0 && ((X - Xb) * (Yc - Yb) - (Y - Yb) * (Xc - Xb)) * ((Xa - Xb) * (Yc - Yb) - (Ya - Yb) * (Xc - Xb)) >= 0.0)
		{
			return ((X - Xc) * (Ya - Yc) - (Y - Yc) * (Xa - Xc)) * ((Xb - Xc) * (Ya - Yc) - (Yb - Yc) * (Xa - Xc)) >= 0.0;
		}
		return false;
	}

	public static bool IsPointInsideTriangleDF(ref double X, ref double Y, double Xa, double Ya, double Xb, double Yb, double Xc, double Yc)
	{
		Public2.CoordWebMerc coordWebMerc = default;
		coordWebMerc.Xwm = Math.Min(Xa, Math.Min(Xb, Xc));
		Public2.CoordWebMerc coordWebMerc2 = default;
		coordWebMerc2.Xwm = Math.Max(Xa, Math.Max(Xb, Xc));
		coordWebMerc.Ywm = Math.Min(Ya, Math.Min(Yb, Yc));
		coordWebMerc2.Ywm = Math.Max(Ya, Math.Max(Yb, Yc));
		if ((X < coordWebMerc.Xwm) | (X > coordWebMerc2.Xwm) | (Y < coordWebMerc.Ywm) | (Y > coordWebMerc2.Ywm))
		{
			return false;
		}
		return ((X - Xa) * (Yb - Ya) - (Y - Ya) * (Xb - Xa)) * ((Xc - Xa) * (Yb - Ya) - (Yc - Ya) * (Xb - Xa)) >= 0.0 && ((X - Xb) * (Yc - Yb) - (Y - Yb) * (Xc - Xb)) * ((Xa - Xb) * (Yc - Yb) - (Ya - Yb) * (Xc - Xb)) >= 0.0 && ((X - Xc) * (Ya - Yc) - (Y - Yc) * (Xa - Xc)) * ((Xb - Xc) * (Ya - Yc) - (Yb - Yc) * (Xa - Xc)) >= 0.0;
	}

	public static bool PointLine(Public2.CoordWebMerc P1, Public2.CoordWebMerc P2, Public2.CoordWebMerc P, double D, ref double L)
	{
		bool result = true;
		L = Math.Abs(((P1.Ywm - P2.Ywm) * P.Xwm + (P2.Xwm - P1.Xwm) * P.Ywm + (P1.Xwm * P2.Ywm - P2.Xwm * P1.Ywm)) / Math.Sqrt(Math.Pow(P2.Xwm - P1.Xwm, 2.0) + Math.Pow(P2.Ywm - P1.Ywm, 2.0)));
		double num = default;
		double num2 = default;
		if (L > D)
		{
			result = false;
		}
		else
		{
			if ((P1.Xwm != P2.Xwm) & (P1.Ywm != P2.Ywm))
			{
				num = (P2.Ywm - P1.Ywm) / (P2.Xwm - P1.Xwm);
				num2 = (P1.Xwm - P2.Xwm) / (P2.Ywm - P1.Ywm);
				goto IL_0203;
			}
			if ((P1.Xwm == P2.Xwm) & (P1.Ywm == P2.Ywm))
			{
				if (!((Math.Abs(P1.Xwm - P.Xwm) < D) & (Math.Abs(P1.Ywm - P.Ywm) < D)))
				{
					goto IL_0203;
				}
				result = true;
			}
			else if (P1.Xwm == P2.Xwm)
			{
				result = ((((P.Ywm > P1.Ywm) & (P.Ywm < P2.Ywm)) | ((P.Ywm < P1.Ywm) & (P.Ywm > P2.Ywm))) ? true : false);
			}
			else
			{
				if (P1.Ywm != P2.Ywm)
				{
					goto IL_0203;
				}
				result = ((((P.Xwm > P1.Xwm) & (P.Xwm < P2.Xwm)) | ((P.Xwm < P1.Xwm) & (P.Xwm > P2.Xwm))) ? true : false);
			}
		}
		goto IL_02cc;
		IL_0203:
		double num3 = P.Ywm - P1.Ywm;
		double num4 = (num * P1.Xwm - num2 * P.Xwm + num3) / (num - num2);
		if (((num4 - P1.Xwm > D) & (num4 - P2.Xwm > D)) | ((num4 - P1.Xwm < 0.0 - D) & (num4 - P2.Xwm < 0.0 - D)))
		{
			result = false;
		}
		else
		{
			double num5 = (num4 - P1.Xwm) * (P2.Ywm - P1.Ywm) / (P2.Xwm - P1.Xwm) + P1.Ywm;
			if (((num5 - P1.Ywm > D) & (num5 - P2.Ywm > D)) | ((num5 - P1.Ywm < 0.0 - D) & (num5 - P2.Ywm < 0.0 - D)))
			{
				result = false;
			}
		}
		goto IL_02cc;
		IL_02cc:
		return result;
	}

	public static bool PointLine(Public2.CoordWebMerc P1, Public2.CoordWebMerc P2, Public2.CoordWebMerc P)
	{
		bool result = true;
		double num = 1.0;
		double num2 = default;
		double num3 = default;
		if (Math.Abs(((P1.Ywm - P2.Ywm) * P.Xwm + (P2.Xwm - P1.Xwm) * P.Ywm + (P1.Xwm * P2.Ywm - P2.Xwm * P1.Ywm)) / Math.Sqrt(Math.Pow(P2.Xwm - P1.Xwm, 2.0) + Math.Pow(P2.Ywm - P1.Ywm, 2.0))) > num)
		{
			result = false;
		}
		else
		{
			if ((P1.Xwm != P2.Xwm) & (P1.Ywm != P2.Ywm))
			{
				num2 = (P2.Ywm - P1.Ywm) / (P2.Xwm - P1.Xwm);
				num3 = (P1.Xwm - P2.Xwm) / (P2.Ywm - P1.Ywm);
				goto IL_0207;
			}
			if ((P1.Xwm == P2.Xwm) & (P1.Ywm == P2.Ywm))
			{
				if (!((Math.Abs(P1.Xwm - P.Xwm) < num) & (Math.Abs(P1.Ywm - P.Ywm) < num)))
				{
					goto IL_0207;
				}
				result = true;
			}
			else if (P1.Xwm == P2.Xwm)
			{
				result = ((((P.Ywm > P1.Ywm) & (P.Ywm < P2.Ywm)) | ((P.Ywm < P1.Ywm) & (P.Ywm > P2.Ywm))) ? true : false);
			}
			else
			{
				if (P1.Ywm != P2.Ywm)
				{
					goto IL_0207;
				}
				result = ((((P.Xwm > P1.Xwm) & (P.Xwm < P2.Xwm)) | ((P.Xwm < P1.Xwm) & (P.Xwm > P2.Xwm))) ? true : false);
			}
		}
		goto IL_02d2;
		IL_0207:
		double num4 = P.Ywm - P1.Ywm;
		double num5 = (num2 * P1.Xwm - num3 * P.Xwm + num4) / (num2 - num3);
		if (((num5 - P1.Xwm > num) & (num5 - P2.Xwm > num)) | ((num5 - P1.Xwm < 0.0 - num) & (num5 - P2.Xwm < 0.0 - num)))
		{
			result = false;
		}
		else
		{
			double num6 = (num5 - P1.Xwm) * (P2.Ywm - P1.Ywm) / (P2.Xwm - P1.Xwm) + P1.Ywm;
			if (((num6 - P1.Ywm > num) & (num6 - P2.Ywm > num)) | ((num6 - P1.Ywm < 0.0 - num) & (num6 - P2.Ywm < 0.0 - num)))
			{
				result = false;
			}
		}
		goto IL_02d2;
		IL_02d2:
		return result;
	}

	public static bool PointLine(Public2.CoordWebMerc P1, Public2.CoordWebMerc P2, Public2.CoordWebMerc P, double D)
	{
		bool result = true;
		double num = default;
		double num2 = default;
		if (Math.Abs(((P1.Ywm - P2.Ywm) * P.Xwm + (P2.Xwm - P1.Xwm) * P.Ywm + (P1.Xwm * P2.Ywm - P2.Xwm * P1.Ywm)) / Math.Sqrt(Math.Pow(P2.Xwm - P1.Xwm, 2.0) + Math.Pow(P2.Ywm - P1.Ywm, 2.0))) > D)
		{
			result = false;
		}
		else
		{
			if ((P1.Xwm != P2.Xwm) & (P1.Ywm != P2.Ywm))
			{
				num = (P2.Ywm - P1.Ywm) / (P2.Xwm - P1.Xwm);
				num2 = (P1.Xwm - P2.Xwm) / (P2.Ywm - P1.Ywm);
				goto IL_01fd;
			}
			if ((P1.Xwm == P2.Xwm) & (P1.Ywm == P2.Ywm))
			{
				if (!((Math.Abs(P1.Xwm - P.Xwm) < D) & (Math.Abs(P1.Ywm - P.Ywm) < D)))
				{
					goto IL_01fd;
				}
				result = true;
			}
			else if (P1.Xwm == P2.Xwm)
			{
				result = ((((P.Ywm > P1.Ywm) & (P.Ywm < P2.Ywm)) | ((P.Ywm < P1.Ywm) & (P.Ywm > P2.Ywm))) ? true : false);
			}
			else
			{
				if (P1.Ywm != P2.Ywm)
				{
					goto IL_01fd;
				}
				result = ((((P.Xwm > P1.Xwm) & (P.Xwm < P2.Xwm)) | ((P.Xwm < P1.Xwm) & (P.Xwm > P2.Xwm))) ? true : false);
			}
		}
		goto IL_02c6;
		IL_01fd:
		double num3 = P.Ywm - P1.Ywm;
		double num4 = (num * P1.Xwm - num2 * P.Xwm + num3) / (num - num2);
		if (((num4 - P1.Xwm > D) & (num4 - P2.Xwm > D)) | ((num4 - P1.Xwm < 0.0 - D) & (num4 - P2.Xwm < 0.0 - D)))
		{
			result = false;
		}
		else
		{
			double num5 = (num4 - P1.Xwm) * (P2.Ywm - P1.Ywm) / (P2.Xwm - P1.Xwm) + P1.Ywm;
			if (((num5 - P1.Ywm > D) & (num5 - P2.Ywm > D)) | ((num5 - P1.Ywm < 0.0 - D) & (num5 - P2.Ywm < 0.0 - D)))
			{
				result = false;
			}
		}
		goto IL_02c6;
		IL_02c6:
		return result;
	}

	public static bool PointLineD(double P1x, double P1y, double P2x, double P2y, double Px, double Py, double D)
	{
		bool result = true;
		double num = default;
		double num2 = default;
		if (Math.Abs(((P1y - P2y) * Px + (P2x - P1x) * Py + (P1x * P2y - P2x * P1y)) / Math.Sqrt(Math.Pow(P2x - P1x, 2.0) + Math.Pow(P2y - P1y, 2.0))) > D)
		{
			result = false;
		}
		else
		{
			if ((P1x != P2x) & (P1y != P2y))
			{
				num = (P2y - P1y) / (P2x - P1x);
				num2 = (P1x - P2x) / (P2y - P1y);
				goto IL_00f2;
			}
			if ((P1x == P2x) & (P1y == P2y))
			{
				if (!((Math.Abs(P1x - Px) < D) & (Math.Abs(P1y - Py) < D)))
				{
					goto IL_00f2;
				}
				result = true;
			}
			else if (P1x == P2x)
			{
				result = ((((Py > P1y) & (Py < P2y)) | ((Py < P1y) & (Py > P2y))) ? true : false);
			}
			else
			{
				if (P1y != P2y)
				{
					goto IL_00f2;
				}
				result = ((((Px > P1x) & (Px < P2x)) | ((Px < P1x) & (Px > P2x))) ? true : false);
			}
		}
		goto IL_016b;
		IL_00f2:
		double num3 = Py - P1y;
		double num4 = (num * P1x - num2 * Px + num3) / (num - num2);
		if (((num4 - P1x > D) & (num4 - P2x > D)) | ((num4 - P1x < 0.0 - D) & (num4 - P2x < 0.0 - D)))
		{
			result = false;
		}
		else
		{
			double num5 = (num4 - P1x) * (P2y - P1y) / (P2x - P1x) + P1y;
			if (((num5 - P1y > D) & (num5 - P2y > D)) | ((num5 - P1y < 0.0 - D) & (num5 - P2y < 0.0 - D)))
			{
				result = false;
			}
		}
		goto IL_016b;
		IL_016b:
		return result;
	}

	public static float GetOriginHeihgt()
	{
		if (!Information.IsNothing(Public2.PRJ.BLD))
		{
			int upperBound = Public2.PRJ.BLD.GetUpperBound(0);
			for (int i = 0; i <= upperBound; i = checked(i + 1))
			{
				if (IsPointInsidePolygonF(Public2.PRJ.SETT.OriginCoordWM, Public2.PRJ.BLD[i].CoordWM))
				{
					return Public2.PRJ.BLD[i].Fundation;
				}
			}
		}
		return Triangul.FindHeightTriang(Public2.PRJ.SETT.OriginCoordWM.Xwm, Public2.PRJ.SETT.OriginCoordWM.Ywm);
	}

	public static string TestFileDN(string FileDn)
	{
		short num = 0;
		string result;
		try
		{
			StreamReader streamReader = new StreamReader(FileDn, Encoding.GetEncoding(1251));
			while (true)
			{
				if (!streamReader.EndOfStream)
				{
					string text = Strings.Trim(streamReader.ReadLine());
					if (Strings.Len(text) <= 0)
					{
						continue;
					}
					num = checked((short)(num + 1));
					if ((Operators.CompareString(text, "HORIZONTAL 360", TextCompare: false) == 0) | (Operators.CompareString(text, "HORIZONTAL\t360", TextCompare: false) == 0) | (Operators.CompareString(text, "HORIZONTAL 360 CO-POLAR", TextCompare: false) == 0))
					{
						streamReader.Close();
						result = "MSI";
						break;
					}
					if ((Operators.CompareString(text, "FSTLST:,-180,179", TextCompare: false) == 0) | (Operators.CompareString(text, "FSTLST:,-180.0,179.0", TextCompare: false) == 0))
					{
						streamReader.Close();
						result = "NSMA360";
						break;
					}
					if (text.Length > 8)
					{
						if (Operators.CompareString(text.Substring(0, 6), "FSTLST", TextCompare: false) == 0)
						{
							streamReader.Close();
							result = "";
							break;
						}
					}
					else if (num > 100)
					{
						streamReader.Close();
						result = "";
						break;
					}
					continue;
				}
				streamReader.Close();
				result = "";
				break;
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			result = "";
			ProjectData.ClearProjectError();
		}
		return result;
	}

	public static void LoadMSI(string FileDnMSI, ref string AntennaName, ref float[] Hdn, ref float[] Vdn, ref bool FlagError)
	{
		Hdn = new float[360];
		Vdn = new float[360];
		FlagError = true;
		int num = 0;
		checked
		{
			try
			{
				StreamReader streamReader = new StreamReader(FileDnMSI, Encoding.GetEncoding(1251));
				AntennaName = streamReader.ReadLine();
				AntennaName = AntennaName.Replace("NAME ", "");
				AntennaName = AntennaName.Replace("NAME\t", "");
				string left = Strings.Trim(streamReader.ReadLine());
				while (!((Operators.CompareString(left, "HORIZONTAL 360", TextCompare: false) == 0) | (Operators.CompareString(left, "HORIZONTAL\t360", TextCompare: false) == 0) | (Operators.CompareString(left, "HORIZONTAL 360 CO-POLAR", TextCompare: false) == 0)))
				{
					left = Strings.Trim(streamReader.ReadLine());
					num++;
					if (num == 1000)
					{
						streamReader.Close();
						FlagError = false;
						return;
					}
				}
				int num2 = 0;
				do
				{
					left = TchkToZpt(Strings.Trim(streamReader.ReadLine()));
					short num3 = (short)Strings.InStr(left, " ");
					if (num3 == 0)
					{
						num3 = (short)Strings.InStr(left, "\t");
					}
					float num4 = Conversions.ToSingle(left.Substring(num3));
					Hdn[num2] = num4;
					num2++;
				}
				while (num2 <= 359);
				left = TchkToZpt(Strings.Trim(streamReader.ReadLine()));
				int num5 = 0;
				do
				{
					left = TchkToZpt(Strings.Trim(streamReader.ReadLine()));
					short num3 = (short)Strings.InStr(left, " ");
					if (num3 == 0)
					{
						num3 = (short)Strings.InStr(left, "\t");
					}
					float num4 = Conversions.ToSingle(left.Substring(num3));
					Vdn[num5] = num4;
					num5++;
				}
				while (num5 <= 359);
				streamReader.Close();
				FlagError = false;
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
		}
	}

	public static void LoadNSMA360(string FileDnMSI, ref string AntennaName, ref float[] Hdn, ref float[] Vdn, ref bool FlagError)
	{
		Hdn = new float[360];
		Vdn = new float[360];
		FlagError = true;
		int num = 0;
		checked
		{
			try
			{
				StreamReader streamReader = new StreamReader(FileDnMSI, Encoding.GetEncoding(1251));
				string text = Strings.Trim(streamReader.ReadLine());
				while (Operators.CompareString(text.Substring(0, 6), "MODNUM", TextCompare: false) != 0)
				{
					text = Strings.Trim(streamReader.ReadLine());
					num++;
					if (num == 1000)
					{
						streamReader.Close();
						FlagError = false;
						return;
					}
				}
				AntennaName = text.Replace("MODNUM:,", "");
				text = Strings.Trim(streamReader.ReadLine());
				while (!((Operators.CompareString(text, "FSTLST:,-180,179", TextCompare: false) == 0) | (Operators.CompareString(text, "FSTLST:,-180.0,179.0", TextCompare: false) == 0)))
				{
					text = Strings.Trim(streamReader.ReadLine());
					num++;
					if (num == 1000)
					{
						streamReader.Close();
						FlagError = false;
						return;
					}
				}
				int num2 = -180;
				do
				{
					text = TchkToZpt(Strings.Trim(streamReader.ReadLine()));
					short startIndex = (short)Strings.InStr(text, ",");
					float num3 = Conversions.ToSingle(text.Substring(startIndex).Replace(",", ""));
					if (num2 >= 0)
					{
						Hdn[num2] = 0f - num3;
					}
					else
					{
						Hdn[360 + num2] = 0f - num3;
					}
					num2++;
				}
				while (num2 <= 179);
				while (!((Operators.CompareString(text, "FSTLST:,-180,179", TextCompare: false) == 0) | (Operators.CompareString(text, "FSTLST:,-180.0,179.0", TextCompare: false) == 0)))
				{
					text = Strings.Trim(streamReader.ReadLine());
					num++;
					if (num == 1000)
					{
						streamReader.Close();
						FlagError = false;
						return;
					}
				}
				int num4 = -180;
				do
				{
					text = TchkToZpt(Strings.Trim(streamReader.ReadLine()));
					short startIndex = (short)Strings.InStr(text, ",");
					float num3 = Conversions.ToSingle(text.Substring(startIndex).Replace(",", ""));
					if (num4 >= 0)
					{
						Vdn[num4] = 0f - num3;
					}
					else
					{
						Vdn[360 + num4] = 0f - num3;
					}
					num4++;
				}
				while (num4 <= 179);
				streamReader.Close();
				FlagError = false;
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
		}
	}

	public static void InvertDN(ref float[] Hdn, ref float[] Vdn)
	{
		float[] array = new float[360];
		int num = 1;
		checked
		{
			do
			{
				array[num] = Hdn[num];
				num++;
			}
			while (num <= 359);
			int num2 = 1;
			do
			{
				Hdn[num2] = array[360 - num2];
				num2++;
			}
			while (num2 <= 359);
			int num3 = 1;
			do
			{
				array[num3] = Vdn[num3];
				num3++;
			}
			while (num3 <= 359);
			int num4 = 1;
			do
			{
				Vdn[num4] = array[360 - num4];
				num4++;
			}
			while (num4 <= 359);
		}
	}

	public static void TurnDn(ref float[] DN, string Ugol)
	{
		float[] array = new float[360];
		if (!Versioned.IsNumeric(Ugol))
		{
			Interaction.MsgBox(Localization.LocMSG(103));
			return;
		}
		if ((Conversions.ToSingle(Ugol) <= -179f) | (Conversions.ToSingle(Ugol) > 179f))
		{
			Interaction.MsgBox(Localization.LocMSG(103));
			return;
		}
		float num = Conversions.ToSByte(Ugol);
		int num2 = 0;
		checked
		{
			do
			{
				array[num2] = DN[num2];
				num2++;
			}
			while (num2 <= 359);
			int num3 = 0;
			do
			{
				float num4 = (float)num3 + num;
				if (num4 > 359f)
				{
					num4 -= 360f;
				}
				if (num4 < 0f)
				{
					num4 += 360f;
				}
				DN[num3] = array[(int)Math.Round(num4)];
				num3++;
			}
			while (num3 <= 359);
		}
	}

	public static void RazmetkaOSI(float Xmin, float Xmax, int Nmin, int Nmax, ref int N, ref float dX, ref float X1)
	{
		long num = 0L;
		float num2 = 999999f;
		N = 0;
		dX = 0f;
		X1 = 0f;
		int num3 = 1;
		checked
		{
			do
			{
				long num4 = (long)Conversion.Int(Xmin / dXi[num3]);
				long num5 = (long)Conversion.Int(Xmax / dXi[num3]);
				if ((float)num4 * dXi[num3] < Xmin)
				{
					num4++;
				}
				num = num5 - num4;
				if ((num >= Nmin) & (num <= Nmax) & (Math.Abs(Xmax - Xmin - (float)num * dXi[num3]) < num2))
				{
					num2 = Math.Abs(Xmax - Xmin - (float)num * dXi[num3]);
					N = (int)(num + 1);
					dX = dXi[num3];
					X1 = (float)num4 * dXi[num3];
				}
				num3++;
			}
			while (num3 <= 93);
		}
	}

	public static void RazmetkaOSI2(float Xmin, float Xmax, int Nmin, int Nmax, ref int N, ref double dX, ref double X1)
	{
		if ((double)Xmax > 100000000.0)
		{
			N = Nmax;
			dX = 10.0;
			X1 = 0.0;
			return;
		}
		double[] array = new double[141]
		{
			0.0, 1E-05, 2E-05, 2.5E-05, 3E-05, 4E-05, 5E-05, 6E-05, 7E-05, 8E-05,
			9E-05, 0.0001, 0.0002, 0.00025, 0.0003, 0.0004, 0.0005, 0.0006, 0.0007, 0.0008,
			0.0009, 0.001, 0.002, 0.0025, 0.003, 0.004, 0.005, 0.006, 0.007, 0.008,
			0.009, 0.01, 0.02, 0.025, 0.03, 0.04, 0.05, 0.06, 0.07, 0.08,
			0.09, 0.1, 0.2, 0.25, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8,
			0.9, 1.0, 1.5, 2.0, 2.5, 3.0, 3.5, 4.0, 4.5, 5.0,
			5.5, 6.0, 6.5, 7.0, 7.5, 8.0, 8.5, 9.0, 9.5, 10.0,
			15.0, 20.0, 25.0, 30.0, 35.0, 40.0, 45.0, 50.0, 55.0, 60.0,
			65.0, 70.0, 75.0, 80.0, 85.0, 90.0, 95.0, 100.0, 150.0, 200.0,
			250.0, 300.0, 350.0, 400.0, 450.0, 500.0, 550.0, 600.0, 650.0, 700.0,
			750.0, 800.0, 850.0, 900.0, 950.0, 1000.0, 1500.0, 2000.0, 2500.0, 3000.0,
			3500.0, 4000.0, 4500.0, 5000.0, 5500.0, 6000.0, 6500.0, 7000.0, 7500.0, 8000.0,
			8500.0, 9000.0, 9500.0, 10000.0, 0.15, 0.0, 0.0, 0.0, 0.0, 0.0,
			0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0,
			0.0
		};
		long num = 0L;
		double num2 = 999999.0;
		N = 0;
		dX = 0.0;
		X1 = 0.0;
		int num3 = 1;
		checked
		{
			do
			{
				long num4 = (long)Conversion.Int((double)Xmin / array[num3]);
				long num5 = (long)Conversion.Int((double)Xmax / array[num3]);
				if ((double)num4 * array[num3] < (double)Xmin)
				{
					num4++;
				}
				num = num5 - num4;
				if ((num >= Nmin) & (num <= Nmax) & (Math.Abs((double)(Xmax - Xmin) - (double)num * array[num3]) < num2))
				{
					num2 = Math.Abs((double)(Xmax - Xmin) - (double)num * array[num3]);
					N = (int)(num + 1);
					dX = array[num3];
					X1 = (double)num4 * array[num3];
				}
				num3++;
			}
			while (num3 <= 124);
		}
	}

	public static void BuildingBackUp()
	{
		checked
		{
			if (Information.IsNothing(BLD_Back))
			{
				BLD_Back = new Public2.StructBuilding[1][];
			}
			else if (BLD_Back.GetUpperBound(0) < NumSteps)
			{
				BLD_Back = (Public2.StructBuilding[][])Utils.CopyArray(BLD_Back, new Public2.StructBuilding[BLD_Back.GetUpperBound(0) + 1 + 1][]);
			}
			for (int i = BLD_Back.GetUpperBound(0); i >= 1; i += -1)
			{
				BLD_Back[i] = DeepClone(BLD_Back[i - 1]);
			}
			BLD_Back[0] = DeepClone(Public2.PRJ.BLD);
		}
	}

	public static void BuildingRestore()
	{
		checked
		{
			if (!Information.IsNothing(BLD_Back))
			{
				if (Information.IsNothing(BLD_Forward))
				{
					BLD_Forward = new Public2.StructBuilding[1][];
				}
				else if (BLD_Forward.GetUpperBound(0) < NumSteps)
				{
					BLD_Forward = (Public2.StructBuilding[][])Utils.CopyArray(BLD_Forward, new Public2.StructBuilding[BLD_Forward.GetUpperBound(0) + 1 + 1][]);
				}
				for (int i = BLD_Forward.GetUpperBound(0); i >= 1; i += -1)
				{
					BLD_Forward[i] = DeepClone(BLD_Forward[i - 1]);
				}
				BLD_Forward[0] = DeepClone(Public2.PRJ.BLD);
				Public2.PRJ.BLD = DeepClone(BLD_Back[0]);
				int num = BLD_Back.GetUpperBound(0) - 1;
				for (int j = 0; j <= num; j++)
				{
					BLD_Back[j] = DeepClone(BLD_Back[j + 1]);
				}
				if (BLD_Back.GetUpperBound(0) > 0)
				{
					BLD_Back = (Public2.StructBuilding[][])Utils.CopyArray(BLD_Back, new Public2.StructBuilding[BLD_Back.GetUpperBound(0) - 1 + 1][]);
				}
				else
				{
					BLD_Back = null;
				}
			}
		}
	}

	public static void BuildingForward()
	{
		checked
		{
			if (!Information.IsNothing(BLD_Forward))
			{
				if (Information.IsNothing(BLD_Back))
				{
					BLD_Back = new Public2.StructBuilding[1][];
				}
				else if (BLD_Back.GetUpperBound(0) < NumSteps)
				{
					BLD_Back = (Public2.StructBuilding[][])Utils.CopyArray(BLD_Back, new Public2.StructBuilding[BLD_Back.GetUpperBound(0) + 1 + 1][]);
				}
				for (int i = BLD_Back.GetUpperBound(0); i >= 1; i += -1)
				{
					BLD_Back[i] = DeepClone(BLD_Back[i - 1]);
				}
				BLD_Back[0] = DeepClone(Public2.PRJ.BLD);
				Public2.PRJ.BLD = DeepClone(BLD_Forward[0]);
				int num = BLD_Forward.GetUpperBound(0) - 1;
				for (int j = 0; j <= num; j++)
				{
					BLD_Forward[j] = DeepClone(BLD_Forward[j + 1]);
				}
				if (BLD_Forward.GetUpperBound(0) > 0)
				{
					BLD_Forward = (Public2.StructBuilding[][])Utils.CopyArray(BLD_Forward, new Public2.StructBuilding[BLD_Forward.GetUpperBound(0) - 1 + 1][]);
				}
				else
				{
					BLD_Forward = null;
				}
			}
		}
	}

	public static void LineBackUp()
	{
		checked
		{
			if (Information.IsNothing(ROAD_Back))
			{
				ROAD_Back = new Public2.StructRoad[1][];
			}
			else if (ROAD_Back.GetUpperBound(0) < NumSteps)
			{
				ROAD_Back = (Public2.StructRoad[][])Utils.CopyArray(ROAD_Back, new Public2.StructRoad[ROAD_Back.GetUpperBound(0) + 1 + 1][]);
			}
			for (int i = ROAD_Back.GetUpperBound(0); i >= 1; i += -1)
			{
				ROAD_Back[i] = DeepClone(ROAD_Back[i - 1]);
			}
			ROAD_Back[0] = DeepClone(Public2.PRJ.ROAD);
			if (Information.IsNothing(FENCE_Back))
			{
				FENCE_Back = new Public2.StructFence[1][];
			}
			else if (FENCE_Back.GetUpperBound(0) < NumSteps)
			{
				FENCE_Back = (Public2.StructFence[][])Utils.CopyArray(FENCE_Back, new Public2.StructFence[FENCE_Back.GetUpperBound(0) + 1 + 1][]);
			}
			for (int j = FENCE_Back.GetUpperBound(0); j >= 1; j += -1)
			{
				FENCE_Back[j] = DeepClone(FENCE_Back[j - 1]);
			}
			FENCE_Back[0] = DeepClone(Public2.PRJ.FENCE);
			if (Information.IsNothing(TEXT_Back))
			{
				TEXT_Back = new Public2.StructText[1][];
			}
			else if (TEXT_Back.GetUpperBound(0) < NumSteps)
			{
				TEXT_Back = (Public2.StructText[][])Utils.CopyArray(TEXT_Back, new Public2.StructText[TEXT_Back.GetUpperBound(0) + 1 + 1][]);
			}
			for (int k = TEXT_Back.GetUpperBound(0); k >= 1; k += -1)
			{
				TEXT_Back[k] = DeepClone(TEXT_Back[k - 1]);
			}
			TEXT_Back[0] = DeepClone(Public2.PRJ.TXT);
		}
	}

	public static void LineRestore()
	{
		checked
		{
			if (!Information.IsNothing(ROAD_Back))
			{
				if (Information.IsNothing(ROAD_Forward))
				{
					ROAD_Forward = new Public2.StructRoad[1][];
				}
				else if (ROAD_Forward.GetUpperBound(0) < NumSteps)
				{
					ROAD_Forward = (Public2.StructRoad[][])Utils.CopyArray(ROAD_Forward, new Public2.StructRoad[ROAD_Forward.GetUpperBound(0) + 1 + 1][]);
				}
				for (int i = ROAD_Forward.GetUpperBound(0); i >= 1; i += -1)
				{
					ROAD_Forward[i] = DeepClone(ROAD_Forward[i - 1]);
				}
				ROAD_Forward[0] = DeepClone(Public2.PRJ.ROAD);
				Public2.PRJ.ROAD = DeepClone(ROAD_Back[0]);
				int num = ROAD_Back.GetUpperBound(0) - 1;
				for (int j = 0; j <= num; j++)
				{
					ROAD_Back[j] = DeepClone(ROAD_Back[j + 1]);
				}
				if (ROAD_Back.GetUpperBound(0) > 0)
				{
					ROAD_Back = (Public2.StructRoad[][])Utils.CopyArray(ROAD_Back, new Public2.StructRoad[ROAD_Back.GetUpperBound(0) - 1 + 1][]);
				}
				else
				{
					ROAD_Back = null;
				}
			}
			if (!Information.IsNothing(FENCE_Back))
			{
				if (Information.IsNothing(FENCE_Forward))
				{
					FENCE_Forward = new Public2.StructFence[1][];
				}
				else if (FENCE_Forward.GetUpperBound(0) < NumSteps)
				{
					FENCE_Forward = (Public2.StructFence[][])Utils.CopyArray(FENCE_Forward, new Public2.StructFence[FENCE_Forward.GetUpperBound(0) + 1 + 1][]);
				}
				for (int k = FENCE_Forward.GetUpperBound(0); k >= 1; k += -1)
				{
					FENCE_Forward[k] = DeepClone(FENCE_Forward[k - 1]);
				}
				FENCE_Forward[0] = DeepClone(Public2.PRJ.FENCE);
				Public2.PRJ.FENCE = DeepClone(FENCE_Back[0]);
				int num2 = FENCE_Back.GetUpperBound(0) - 1;
				for (int l = 0; l <= num2; l++)
				{
					FENCE_Back[l] = DeepClone(FENCE_Back[l + 1]);
				}
				if (FENCE_Back.GetUpperBound(0) > 0)
				{
					FENCE_Back = (Public2.StructFence[][])Utils.CopyArray(FENCE_Back, new Public2.StructFence[FENCE_Back.GetUpperBound(0) - 1 + 1][]);
				}
				else
				{
					FENCE_Back = null;
				}
			}
			if (!Information.IsNothing(TEXT_Back))
			{
				if (Information.IsNothing(TEXT_Forward))
				{
					TEXT_Forward = new Public2.StructText[1][];
				}
				else if (TEXT_Forward.GetUpperBound(0) < NumSteps)
				{
					TEXT_Forward = (Public2.StructText[][])Utils.CopyArray(TEXT_Forward, new Public2.StructText[TEXT_Forward.GetUpperBound(0) + 1 + 1][]);
				}
				for (int m = TEXT_Forward.GetUpperBound(0); m >= 1; m += -1)
				{
					TEXT_Forward[m] = DeepClone(TEXT_Forward[m - 1]);
				}
				TEXT_Forward[0] = DeepClone(Public2.PRJ.TXT);
				Public2.PRJ.TXT = DeepClone(TEXT_Back[0]);
				int num3 = TEXT_Back.GetUpperBound(0) - 1;
				for (int n = 0; n <= num3; n++)
				{
					TEXT_Back[n] = DeepClone(TEXT_Back[n + 1]);
				}
				if (TEXT_Back.GetUpperBound(0) > 0)
				{
					TEXT_Back = (Public2.StructText[][])Utils.CopyArray(TEXT_Back, new Public2.StructText[TEXT_Back.GetUpperBound(0) - 1 + 1][]);
				}
				else
				{
					TEXT_Back = null;
				}
			}
		}
	}

	public static void LineForward()
	{
		checked
		{
			if (!Information.IsNothing(ROAD_Forward))
			{
				if (Information.IsNothing(ROAD_Back))
				{
					ROAD_Back = new Public2.StructRoad[1][];
				}
				else if (ROAD_Back.GetUpperBound(0) < NumSteps)
				{
					ROAD_Back = (Public2.StructRoad[][])Utils.CopyArray(ROAD_Back, new Public2.StructRoad[ROAD_Back.GetUpperBound(0) + 1 + 1][]);
				}
				for (int i = ROAD_Back.GetUpperBound(0); i >= 1; i += -1)
				{
					ROAD_Back[i] = DeepClone(ROAD_Back[i - 1]);
				}
				ROAD_Back[0] = DeepClone(Public2.PRJ.ROAD);
				Public2.PRJ.ROAD = DeepClone(ROAD_Forward[0]);
				int num = ROAD_Forward.GetUpperBound(0) - 1;
				for (int j = 0; j <= num; j++)
				{
					ROAD_Forward[j] = DeepClone(ROAD_Forward[j + 1]);
				}
				if (ROAD_Forward.GetUpperBound(0) > 0)
				{
					ROAD_Forward = (Public2.StructRoad[][])Utils.CopyArray(ROAD_Forward, new Public2.StructRoad[ROAD_Forward.GetUpperBound(0) - 1 + 1][]);
				}
				else
				{
					ROAD_Forward = null;
				}
			}
			if (!Information.IsNothing(FENCE_Forward))
			{
				if (Information.IsNothing(FENCE_Back))
				{
					FENCE_Back = new Public2.StructFence[1][];
				}
				else if (FENCE_Back.GetUpperBound(0) < NumSteps)
				{
					FENCE_Back = (Public2.StructFence[][])Utils.CopyArray(FENCE_Back, new Public2.StructFence[FENCE_Back.GetUpperBound(0) + 1 + 1][]);
				}
				for (int k = FENCE_Back.GetUpperBound(0); k >= 1; k += -1)
				{
					FENCE_Back[k] = DeepClone(FENCE_Back[k - 1]);
				}
				FENCE_Back[0] = DeepClone(Public2.PRJ.FENCE);
				Public2.PRJ.FENCE = DeepClone(FENCE_Forward[0]);
				int num2 = FENCE_Forward.GetUpperBound(0) - 1;
				for (int l = 0; l <= num2; l++)
				{
					FENCE_Forward[l] = DeepClone(FENCE_Forward[l + 1]);
				}
				if (FENCE_Forward.GetUpperBound(0) > 0)
				{
					FENCE_Forward = (Public2.StructFence[][])Utils.CopyArray(FENCE_Forward, new Public2.StructFence[FENCE_Forward.GetUpperBound(0) - 1 + 1][]);
				}
				else
				{
					FENCE_Forward = null;
				}
			}
			if (!Information.IsNothing(TEXT_Forward))
			{
				if (Information.IsNothing(TEXT_Back))
				{
					TEXT_Back = new Public2.StructText[1][];
				}
				else if (TEXT_Back.GetUpperBound(0) < NumSteps)
				{
					TEXT_Back = (Public2.StructText[][])Utils.CopyArray(TEXT_Back, new Public2.StructText[TEXT_Back.GetUpperBound(0) + 1 + 1][]);
				}
				for (int m = TEXT_Back.GetUpperBound(0); m >= 1; m += -1)
				{
					TEXT_Back[m] = DeepClone(TEXT_Back[m - 1]);
				}
				TEXT_Back[0] = DeepClone(Public2.PRJ.TXT);
				Public2.PRJ.TXT = DeepClone(TEXT_Forward[0]);
				int num3 = TEXT_Forward.GetUpperBound(0) - 1;
				for (int n = 0; n <= num3; n++)
				{
					TEXT_Forward[n] = DeepClone(TEXT_Forward[n + 1]);
				}
				if (TEXT_Forward.GetUpperBound(0) > 0)
				{
					TEXT_Forward = (Public2.StructText[][])Utils.CopyArray(TEXT_Forward, new Public2.StructText[TEXT_Forward.GetUpperBound(0) - 1 + 1][]);
				}
				else
				{
					TEXT_Forward = null;
				}
			}
		}
	}

	public static void ElevBackUp()
	{
		checked
		{
			if (Information.IsNothing(ElLN_Back))
			{
				ElLN_Back = new Public2.StructElevLine[1][];
			}
			else if (ElLN_Back.GetUpperBound(0) < NumSteps)
			{
				ElLN_Back = (Public2.StructElevLine[][])Utils.CopyArray(ElLN_Back, new Public2.StructElevLine[ElLN_Back.GetUpperBound(0) + 1 + 1][]);
			}
			for (int i = ElLN_Back.GetUpperBound(0); i >= 1; i += -1)
			{
				ElLN_Back[i] = DeepClone(ElLN_Back[i - 1]);
			}
			ElLN_Back[0] = DeepClone(Public2.PRJ.ElLN);
			if (Information.IsNothing(ElPT_Back))
			{
				ElPT_Back = new Public2.StructElevPoint[1][];
			}
			else if (ElPT_Back.GetUpperBound(0) < NumSteps)
			{
				ElPT_Back = (Public2.StructElevPoint[][])Utils.CopyArray(ElPT_Back, new Public2.StructElevPoint[ElPT_Back.GetUpperBound(0) + 1 + 1][]);
			}
			for (int j = ElPT_Back.GetUpperBound(0); j >= 1; j += -1)
			{
				ElPT_Back[j] = DeepClone(ElPT_Back[j - 1]);
			}
			ElPT_Back[0] = DeepClone(Public2.PRJ.ElPT);
		}
	}

	public static void ElevRestore()
	{
		checked
		{
			if (!Information.IsNothing(ElLN_Back))
			{
				if (Information.IsNothing(ElLN_Forward))
				{
					ElLN_Forward = new Public2.StructElevLine[1][];
				}
				else if (ElLN_Forward.GetUpperBound(0) < NumSteps)
				{
					ElLN_Forward = (Public2.StructElevLine[][])Utils.CopyArray(ElLN_Forward, new Public2.StructElevLine[ElLN_Forward.GetUpperBound(0) + 1 + 1][]);
				}
				for (int i = ElLN_Forward.GetUpperBound(0); i >= 1; i += -1)
				{
					ElLN_Forward[i] = DeepClone(ElLN_Forward[i - 1]);
				}
				ElLN_Forward[0] = DeepClone(Public2.PRJ.ElLN);
				Public2.PRJ.ElLN = DeepClone(ElLN_Back[0]);
				int num = ElLN_Back.GetUpperBound(0) - 1;
				for (int j = 0; j <= num; j++)
				{
					ElLN_Back[j] = DeepClone(ElLN_Back[j + 1]);
				}
				if (ElLN_Back.GetUpperBound(0) > 0)
				{
					ElLN_Back = (Public2.StructElevLine[][])Utils.CopyArray(ElLN_Back, new Public2.StructElevLine[ElLN_Back.GetUpperBound(0) - 1 + 1][]);
				}
				else
				{
					ElLN_Back = null;
				}
			}
			if (!Information.IsNothing(ElPT_Back))
			{
				if (Information.IsNothing(ElPT_Forward))
				{
					ElPT_Forward = new Public2.StructElevPoint[1][];
				}
				else if (ElPT_Forward.GetUpperBound(0) < NumSteps)
				{
					ElPT_Forward = (Public2.StructElevPoint[][])Utils.CopyArray(ElPT_Forward, new Public2.StructElevPoint[ElPT_Forward.GetUpperBound(0) + 1 + 1][]);
				}
				for (int k = ElPT_Forward.GetUpperBound(0); k >= 1; k += -1)
				{
					ElPT_Forward[k] = DeepClone(ElPT_Forward[k - 1]);
				}
				ElPT_Forward[0] = DeepClone(Public2.PRJ.ElPT);
				Public2.PRJ.ElPT = DeepClone(ElPT_Back[0]);
				int num2 = ElPT_Back.GetUpperBound(0) - 1;
				for (int l = 0; l <= num2; l++)
				{
					ElPT_Back[l] = DeepClone(ElPT_Back[l + 1]);
				}
				if (ElPT_Back.GetUpperBound(0) > 0)
				{
					ElPT_Back = (Public2.StructElevPoint[][])Utils.CopyArray(ElPT_Back, new Public2.StructElevPoint[ElPT_Back.GetUpperBound(0) - 1 + 1][]);
				}
				else
				{
					ElPT_Back = null;
				}
			}
		}
	}

	public static void ElevForward()
	{
		checked
		{
			if (!Information.IsNothing(ElLN_Forward))
			{
				if (Information.IsNothing(ElLN_Back))
				{
					ElLN_Back = new Public2.StructElevLine[1][];
				}
				else if (ElLN_Back.GetUpperBound(0) < NumSteps)
				{
					ElLN_Back = (Public2.StructElevLine[][])Utils.CopyArray(ElLN_Back, new Public2.StructElevLine[ElLN_Back.GetUpperBound(0) + 1 + 1][]);
				}
				for (int i = ElLN_Back.GetUpperBound(0); i >= 1; i += -1)
				{
					ElLN_Back[i] = DeepClone(ElLN_Back[i - 1]);
				}
				ElLN_Back[0] = DeepClone(Public2.PRJ.ElLN);
				Public2.PRJ.ElLN = DeepClone(ElLN_Forward[0]);
				int num = ElLN_Forward.GetUpperBound(0) - 1;
				for (int j = 0; j <= num; j++)
				{
					ElLN_Forward[j] = DeepClone(ElLN_Forward[j + 1]);
				}
				if (ElLN_Forward.GetUpperBound(0) > 0)
				{
					ElLN_Forward = (Public2.StructElevLine[][])Utils.CopyArray(ElLN_Forward, new Public2.StructElevLine[ElLN_Forward.GetUpperBound(0) - 1 + 1][]);
				}
				else
				{
					ElLN_Forward = null;
				}
			}
			if (!Information.IsNothing(ElPT_Forward))
			{
				if (Information.IsNothing(ElPT_Back))
				{
					ElPT_Back = new Public2.StructElevPoint[1][];
				}
				else if (ElPT_Back.GetUpperBound(0) < NumSteps)
				{
					ElPT_Back = (Public2.StructElevPoint[][])Utils.CopyArray(ElPT_Back, new Public2.StructElevPoint[ElPT_Back.GetUpperBound(0) + 1 + 1][]);
				}
				for (int k = ElPT_Back.GetUpperBound(0); k >= 1; k += -1)
				{
					ElPT_Back[k] = DeepClone(ElPT_Back[k - 1]);
				}
				ElPT_Back[0] = DeepClone(Public2.PRJ.ElPT);
				Public2.PRJ.ElPT = DeepClone(ElPT_Forward[0]);
				int num2 = ElPT_Forward.GetUpperBound(0) - 1;
				for (int l = 0; l <= num2; l++)
				{
					ElPT_Forward[l] = DeepClone(ElPT_Forward[l + 1]);
				}
				if (ElPT_Forward.GetUpperBound(0) > 0)
				{
					ElPT_Forward = (Public2.StructElevPoint[][])Utils.CopyArray(ElPT_Forward, new Public2.StructElevPoint[ElPT_Forward.GetUpperBound(0) - 1 + 1][]);
				}
				else
				{
					ElPT_Forward = null;
				}
			}
		}
	}

	public static Bitmap CBrightness2(Image CurrentImage, byte Brightness)
	{
		if (Brightness == 0)
		{
			return (Bitmap)CurrentImage.Clone();
		}
		Bitmap original = (Bitmap)CurrentImage.Clone();
		Bitmap bitmap = new Bitmap(original);
		Rectangle destRect = new Rectangle(0, 0, bitmap.Width, bitmap.Height);
		using (Graphics graphics = Graphics.FromImage(bitmap))
		{
			ImageAttributes imageAttributes = new ImageAttributes();
			float[][] array = new float[5][]
			{
				new float[5],
				new float[5],
				new float[5],
				null,
				null
			};
			float[] array2 = new float[5] { 1f, 1f, 1f, 0f, 0f };
			array2[3] = (float)((double)(int)Brightness / 10.0);
			array[3] = array2;
			array[4] = new float[5];
			imageAttributes.SetColorMatrix(new ColorMatrix(array));
			graphics.DrawImage(bitmap, destRect, 0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, imageAttributes);
		}
		return bitmap;
	}

	public static bool FileCompare(string file1, string file2)
	{
		if (Operators.CompareString(file1, file2, TextCompare: false) == 0)
		{
			return true;
		}
		FileStream fileStream = new FileStream(file1, FileMode.Open);
		FileStream fileStream2 = new FileStream(file2, FileMode.Open);
		if (fileStream.Length != fileStream2.Length)
		{
			fileStream.Close();
			fileStream2.Close();
			return false;
		}
		int num;
		int num2;
		do
		{
			num = fileStream.ReadByte();
			num2 = fileStream2.ReadByte();
		}
		while ((num == num2) & (num != -1));
		fileStream.Close();
		fileStream2.Close();
		return checked(num - num2) == 0;
	}

	public static double DistPointToLine(double X0, double Y0, double X1, double Y1, double X2, double Y2)
	{
		double num = X2 - X1;
		double num2 = Y2 - Y1;
		if ((X1 == X2) & (Y1 == Y2))
		{
			return 0.0;
		}
		return Math.Abs(num2 * X0 - num * Y0 + X2 * Y1 - Y2 * X1) / Math.Sqrt(num2 * num2 + num * num);
	}

	public static void SquarePline(Public2.CoordWebMerc[] P)
	{
		if (P.GetUpperBound(0) % 2 != 0)
		{
			return;
		}
		int upperBound = P.GetUpperBound(0);
		checked
		{
			for (int i = 0; i <= upperBound; i++)
			{
				if (i + 1 < P.GetUpperBound(0))
				{
					P[i + 1] = ClosestPointToLine(P[i], P[i + 1], P[i + 2]);
				}
				else if (i + 1 == P.GetUpperBound(0))
				{
					P[i + 1] = ClosestPointToLine(P[i], P[i + 1], P[1]);
					P[0] = P[i + 1];
				}
			}
		}
	}

	public static Public2.CoordWebMerc ClosestPointToLine(Public2.CoordWebMerc A, Public2.CoordWebMerc B, Public2.CoordWebMerc C)
	{
		double num = ((B.Xwm - A.Xwm) * (C.Xwm - A.Xwm) + (B.Ywm - A.Ywm) * (C.Ywm - A.Ywm)) / ((B.Xwm - A.Xwm) * (B.Xwm - A.Xwm) + (B.Ywm - A.Ywm) * (B.Ywm - A.Ywm));
		Public2.CoordWebMerc result = default;
		result.Xwm = A.Xwm + (B.Xwm - A.Xwm) * num;
		result.Ywm = A.Ywm + (B.Ywm - A.Ywm) * num;
		return result;
	}
}
