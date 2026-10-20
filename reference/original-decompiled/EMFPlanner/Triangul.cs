using System;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[StandardModule]
internal sealed class Triangul
{
	[Serializable]
	public struct Triangle
	{
		public double X1;

		public double Y1;

		public float H1;

		public double X2;

		public double Y2;

		public float H2;

		public double X3;

		public double Y3;

		public float H3;
	}

	[Serializable]
	public struct Frontier
	{
		public double X1;

		public double Y1;

		public float H1;

		public double X2;

		public double Y2;

		public float H2;
	}

	public const double Pi90 = Math.PI / 2.0;

	public const double Pi270 = 4.71238898038469;

	public static Triangle[] TRG;

	public static Frontier[] ActiveFRT = new Frontier[1];

	public static void CalcTriangulate()
	{
		TRG = null;
		if (Information.IsNothing(Public2.PRJ.ElLN) & Information.IsNothing(Public2.PRJ.ElPT))
		{
			return;
		}
		int num = 0;
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.ElLN))
			{
				int upperBound = Public2.PRJ.ElLN.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					num += Public2.PRJ.ElLN[i].CoordWM.Length;
				}
			}
			if (!Information.IsNothing(Public2.PRJ.ElPT))
			{
				num += Public2.PRJ.ElPT.GetUpperBound(0);
			}
			ActiveFRT = new Frontier[1];
			AddNewActiveFrontier(-50.0, -50.0, 9999.0, -40.0, -50.0, 9999.0);
			bool flag = false;
			double Xpoint = default;
			double Ypoint = default;
			double Hpoint = default;
			while (ActiveFRT.GetUpperBound(0) > 0)
			{
				double x = ActiveFRT[ActiveFRT.GetUpperBound(0)].X1;
				double y = ActiveFRT[ActiveFRT.GetUpperBound(0)].Y1;
				double h = ActiveFRT[ActiveFRT.GetUpperBound(0)].H1;
				double x2 = ActiveFRT[ActiveFRT.GetUpperBound(0)].X2;
				double y2 = ActiveFRT[ActiveFRT.GetUpperBound(0)].Y2;
				double h2 = ActiveFRT[ActiveFRT.GetUpperBound(0)].H2;
				ActiveFRT = (Frontier[])Utils.CopyArray(ActiveFRT, new Frontier[ActiveFRT.GetUpperBound(0) - 1 + 1]);
				if (SearchPoint(x, y, x2, y2, ref Xpoint, ref Ypoint, ref Hpoint))
				{
					if (flag & (ActiveFRT.GetUpperBound(0) == 0))
					{
						break;
					}
					flag = true;
					AddNewActiveFrontier(x, y, h, Xpoint, Ypoint, Hpoint);
					AddNewActiveFrontier(Xpoint, Ypoint, Hpoint, x2, y2, h2);
					AddNewTriangle(x, y, h, x2, y2, h2, Xpoint, Ypoint, Hpoint);
					if ((double)TRG.GetUpperBound(0) > 2.5 * (double)num)
					{
						break;
					}
				}
			}
			Triangle[] array = null;
			int upperBound2 = TRG.GetUpperBound(0);
			for (int j = 0; j <= upperBound2; j++)
			{
				if (!(((TRG[j].X1 == -40.0) & (TRG[j].Y1 == -50.0)) | ((TRG[j].X2 == -40.0) & (TRG[j].Y2 == -50.0)) | ((TRG[j].X3 == -40.0) & (TRG[j].Y3 == -50.0)) | ((TRG[j].X1 == -50.0) & (TRG[j].Y1 == -50.0)) | ((TRG[j].X2 == -50.0) & (TRG[j].Y2 == -50.0)) | ((TRG[j].X3 == -50.0) & (TRG[j].Y3 == -50.0))))
				{
					array = ((!Information.IsNothing(array)) ? ((Triangle[])Utils.CopyArray(array, new Triangle[array.GetUpperBound(0) + 1 + 1])) : new Triangle[1]);
					array[array.GetUpperBound(0)] = TRG[j];
				}
			}
			TRG = Module1.DeepClone(array);
		}
	}

	public static void AddNewActiveFrontier(double X1, double Y1, double H1, double X2, double Y2, double H2)
	{
		int upperBound = ActiveFRT.GetUpperBound(0);
		checked
		{
			for (int i = 1; i <= upperBound; i++)
			{
				if (!(((ActiveFRT[i].X1 == X1) & (ActiveFRT[i].Y1 == Y1) & (ActiveFRT[i].X2 == X2) & (ActiveFRT[i].Y2 == Y2)) | ((ActiveFRT[i].X1 == X2) & (ActiveFRT[i].Y1 == Y2) & (ActiveFRT[i].X2 == X1) & (ActiveFRT[i].Y2 == Y1))))
				{
					continue;
				}
				if (i < ActiveFRT.GetUpperBound(0))
				{
					int num = i;
					int num2 = ActiveFRT.GetUpperBound(0) - 1;
					for (int j = num; j <= num2; j++)
					{
						ActiveFRT[j].X1 = ActiveFRT[j + 1].X1;
						ActiveFRT[j].Y1 = ActiveFRT[j + 1].Y1;
						ActiveFRT[j].H1 = ActiveFRT[j + 1].H1;
						ActiveFRT[j].X2 = ActiveFRT[j + 1].X2;
						ActiveFRT[j].Y2 = ActiveFRT[j + 1].Y2;
						ActiveFRT[j].H2 = ActiveFRT[j + 1].H2;
					}
				}
				ActiveFRT = (Frontier[])Utils.CopyArray(ActiveFRT, new Frontier[ActiveFRT.GetUpperBound(0) - 1 + 1]);
				return;
			}
			ActiveFRT = (Frontier[])Utils.CopyArray(ActiveFRT, new Frontier[ActiveFRT.GetUpperBound(0) + 1 + 1]);
			ActiveFRT[ActiveFRT.GetUpperBound(0)].X1 = X1;
			ActiveFRT[ActiveFRT.GetUpperBound(0)].Y1 = Y1;
			ActiveFRT[ActiveFRT.GetUpperBound(0)].H1 = (float)H1;
			ActiveFRT[ActiveFRT.GetUpperBound(0)].X2 = X2;
			ActiveFRT[ActiveFRT.GetUpperBound(0)].Y2 = Y2;
			ActiveFRT[ActiveFRT.GetUpperBound(0)].H2 = (float)H2;
		}
	}

	public static void AddNewTriangle(double X1, double Y1, double H1, double X2, double Y2, double H2, double X3, double Y3, double H3)
	{
		if (Information.IsNothing(TRG))
		{
			TRG = new Triangle[1];
		}
		else
		{
			TRG = (Triangle[])Utils.CopyArray(TRG, new Triangle[checked(TRG.GetUpperBound(0) + 1 + 1)]);
		}
		TRG[TRG.GetUpperBound(0)].X1 = X1;
		TRG[TRG.GetUpperBound(0)].Y1 = Y1;
		TRG[TRG.GetUpperBound(0)].H1 = (float)H1;
		TRG[TRG.GetUpperBound(0)].X2 = X2;
		TRG[TRG.GetUpperBound(0)].Y2 = Y2;
		TRG[TRG.GetUpperBound(0)].H2 = (float)H2;
		TRG[TRG.GetUpperBound(0)].X3 = X3;
		TRG[TRG.GetUpperBound(0)].Y3 = Y3;
		TRG[TRG.GetUpperBound(0)].H3 = (float)H3;
	}

	public static bool SearchPoint(double X1, double Y1, double X2, double Y2, ref double Xpoint, ref double Ypoint, ref double Hpoint)
	{
		double num = 0.0;
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.ElPT))
			{
				int upperBound = Public2.PRJ.ElPT.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					if ((Public2.PRJ.ElPT[i].CoordWM.Xwm - X1) * (Y2 - Y1) - (Public2.PRJ.ElPT[i].CoordWM.Ywm - Y1) * (X2 - X1) < 0.0)
					{
						double num2 = ((X1 == Public2.PRJ.ElPT[i].CoordWM.Xwm) ? 9999999999999.0 : ((Y1 - Public2.PRJ.ElPT[i].CoordWM.Ywm) / (X1 - Public2.PRJ.ElPT[i].CoordWM.Xwm)));
						double num3 = ((X2 == Public2.PRJ.ElPT[i].CoordWM.Xwm) ? 9999999999999.0 : ((Y2 - Public2.PRJ.ElPT[i].CoordWM.Ywm) / (X2 - Public2.PRJ.ElPT[i].CoordWM.Xwm)));
						double num4 = Math.Atan2(num3 - num2, 1.0 + num2 * num3);
						if (num4 < 0.0)
						{
							num4 += Math.PI;
						}
						num4 = Math.Abs(num4);
						if (num4 > num)
						{
							num = num4;
							Xpoint = Public2.PRJ.ElPT[i].CoordWM.Xwm;
							Ypoint = Public2.PRJ.ElPT[i].CoordWM.Ywm;
							Hpoint = Public2.PRJ.ElPT[i].H;
						}
					}
				}
			}
			if (!Information.IsNothing(Public2.PRJ.ElLN))
			{
				int upperBound2 = Public2.PRJ.ElLN.GetUpperBound(0);
				for (int j = 0; j <= upperBound2; j++)
				{
					int upperBound3 = Public2.PRJ.ElLN[j].CoordWM.GetUpperBound(0);
					for (int k = 0; k <= upperBound3; k++)
					{
						if ((Public2.PRJ.ElLN[j].CoordWM[k].Xwm - X1) * (Y2 - Y1) - (Public2.PRJ.ElLN[j].CoordWM[k].Ywm - Y1) * (X2 - X1) < 0.0)
						{
							double num2 = ((X1 == Public2.PRJ.ElLN[j].CoordWM[k].Xwm) ? 10000000000000000.0 : ((Y1 - Public2.PRJ.ElLN[j].CoordWM[k].Ywm) / (X1 - Public2.PRJ.ElLN[j].CoordWM[k].Xwm)));
							double num3 = ((X2 == Public2.PRJ.ElLN[j].CoordWM[k].Xwm) ? 9999999999999.0 : ((Y2 - Public2.PRJ.ElLN[j].CoordWM[k].Ywm) / (X2 - Public2.PRJ.ElLN[j].CoordWM[k].Xwm)));
							double num4 = Math.Atan2(num3 - num2, 1.0 + num2 * num3);
							if (num4 < 0.0)
							{
								num4 += Math.PI;
							}
							num4 = Math.Abs(num4);
							if (num4 > num)
							{
								num = num4;
								Xpoint = Public2.PRJ.ElLN[j].CoordWM[k].Xwm;
								Ypoint = Public2.PRJ.ElLN[j].CoordWM[k].Ywm;
								Hpoint = Public2.PRJ.ElLN[j].H;
							}
						}
					}
					_ = Public2.PRJ.ElLN[j].CloseLine;
				}
			}
			if ((-50.0 - X1) * (Y2 - Y1) - (-50.0 - Y1) * (X2 - X1) < 0.0)
			{
				double num2 = ((X1 == -50.0) ? 10000000000000000.0 : ((Y1 - -50.0) / (X1 - -50.0)));
				double num3 = ((X2 == -50.0) ? 9999999999999.0 : ((Y2 - -50.0) / (X2 - -50.0)));
				double num4 = Math.Atan2(num3 - num2, 1.0 + num2 * num3);
				if (num4 < 0.0)
				{
					num4 += Math.PI;
				}
				num4 = Math.Abs(num4);
				if (num4 > num)
				{
					num = num4;
					Xpoint = -50.0;
					Ypoint = -50.0;
					Hpoint = 9999.0;
				}
			}
			if (num > 0.0)
			{
				return true;
			}
			return false;
		}
	}

	public static bool CalcCross(double X1, double Y1, double X2, double Y2)
	{
		checked
		{
			bool result = default;
			if (!Information.IsNothing(Public2.PRJ.ElLN))
			{
				result = false;
				int upperBound = Public2.PRJ.ElLN.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					int num = Public2.PRJ.ElLN[i].CoordWM.GetUpperBound(0) - 1;
					for (int j = 0; j <= num; j++)
					{
						if (!(!((X1 == Public2.PRJ.ElLN[i].CoordWM[j].Xwm) & (Y1 == Public2.PRJ.ElLN[i].CoordWM[j].Ywm)) & !((X2 != Public2.PRJ.ElLN[i].CoordWM[j].Xwm) & (Y2 == Public2.PRJ.ElLN[i].CoordWM[j].Ywm)) & !((X1 == Public2.PRJ.ElLN[i].CoordWM[j + 1].Xwm) & (Y1 == Public2.PRJ.ElLN[i].CoordWM[j + 1].Ywm)) & !((X2 == Public2.PRJ.ElLN[i].CoordWM[j + 1].Xwm) & (Y2 == Public2.PRJ.ElLN[i].CoordWM[j + 1].Ywm))))
						{
							continue;
						}
						double num2 = ((Public2.PRJ.ElLN[i].CoordWM[j].Xwm - X1) * (Y2 - Y1) - (Public2.PRJ.ElLN[i].CoordWM[j].Ywm - Y1) * (X2 - X1)) * ((Public2.PRJ.ElLN[i].CoordWM[j + 1].Xwm - X1) * (Y2 - Y1) - (Public2.PRJ.ElLN[i].CoordWM[j].Ywm - Y1) * (X2 - X1));
						double num3 = ((X1 - Public2.PRJ.ElLN[i].CoordWM[j].Xwm) * (Public2.PRJ.ElLN[i].CoordWM[j + 1].Ywm - Public2.PRJ.ElLN[i].CoordWM[j].Ywm) - (Y1 - Public2.PRJ.ElLN[i].CoordWM[j].Ywm) * (Public2.PRJ.ElLN[i].CoordWM[j + 1].Xwm - Public2.PRJ.ElLN[i].CoordWM[j].Xwm)) * ((X2 - Public2.PRJ.ElLN[i].CoordWM[j].Xwm) * (Public2.PRJ.ElLN[i].CoordWM[j + 1].Ywm - Public2.PRJ.ElLN[i].CoordWM[j].Ywm) - (Y2 - Public2.PRJ.ElLN[i].CoordWM[j].Ywm) * (Public2.PRJ.ElLN[i].CoordWM[j + 1].Xwm - Public2.PRJ.ElLN[i].CoordWM[j].Xwm));
						if (!((num2 < 0.0) & (num3 < 0.0)))
						{
							continue;
						}
						goto IL_03fb;
					}
					continue;
					IL_03fb:
					result = true;
					break;
				}
			}
			return result;
		}
	}

	public static float FindElevWithBLD(Public2.CoordWebMerc CoordWM)
	{
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.BLD))
			{
				int upperBound = Public2.PRJ.BLD.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					if (Module1.IsPointInsidePolygonF(CoordWM, Public2.PRJ.BLD[i].CoordWM))
					{
						return Public2.PRJ.BLD[i].Fundation;
					}
				}
			}
			if (!Information.IsNothing(TRG))
			{
				int upperBound2 = TRG.GetUpperBound(0);
				for (int j = 0; j <= upperBound2; j++)
				{
					if (Module1.IsPointInsideTriangleDF(ref CoordWM.Xwm, ref CoordWM.Ywm, TRG[j].X1, TRG[j].Y1, TRG[j].X2, TRG[j].Y2, TRG[j].X3, TRG[j].Y3))
					{
						return (float)CalcHeightPlosk(TRG[j].X1, TRG[j].Y1, TRG[j].H1, TRG[j].X2, TRG[j].Y2, TRG[j].H2, TRG[j].X3, TRG[j].Y3, TRG[j].H3, CoordWM.Xwm, CoordWM.Ywm);
					}
				}
			}
			return 0f;
		}
	}

	public static float FindElevWithBldPlusHbld(Public2.CoordWebMerc CoordWM)
	{
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.BLD))
			{
				int upperBound = Public2.PRJ.BLD.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					if (Module1.IsPointInsidePolygonF(CoordWM, Public2.PRJ.BLD[i].CoordWM))
					{
						return Public2.PRJ.BLD[i].Fundation + Public2.PRJ.BLD[i].H;
					}
				}
			}
			if (!Information.IsNothing(TRG))
			{
				int upperBound2 = TRG.GetUpperBound(0);
				for (int j = 0; j <= upperBound2; j++)
				{
					if (Module1.IsPointInsideTriangleDF(ref CoordWM.Xwm, ref CoordWM.Ywm, TRG[j].X1, TRG[j].Y1, TRG[j].X2, TRG[j].Y2, TRG[j].X3, TRG[j].Y3))
					{
						return (float)CalcHeightPlosk(TRG[j].X1, TRG[j].Y1, TRG[j].H1, TRG[j].X2, TRG[j].Y2, TRG[j].H2, TRG[j].X3, TRG[j].Y3, TRG[j].H3, CoordWM.Xwm, CoordWM.Ywm);
					}
				}
			}
			return 0f;
		}
	}

	public static float FindHeightTriang(double X1, double Y1)
	{
		if (Information.IsNothing(TRG))
		{
			return 0f;
		}
		int upperBound = TRG.GetUpperBound(0);
		for (int i = 0; i <= upperBound; i = checked(i + 1))
		{
			if (Module1.IsPointInsideTriangleDF(ref X1, ref Y1, TRG[i].X1, TRG[i].Y1, TRG[i].X2, TRG[i].Y2, TRG[i].X3, TRG[i].Y3))
			{
				return (float)CalcHeightPlosk(TRG[i].X1, TRG[i].Y1, TRG[i].H1, TRG[i].X2, TRG[i].Y2, TRG[i].H2, TRG[i].X3, TRG[i].Y3, TRG[i].H3, X1, Y1);
			}
		}
		return 0f;
	}

	public static float FindHeightTriangOLD(double X1, double Y1)
	{
		if (Information.IsNothing(TRG))
		{
			return 0f;
		}
		int upperBound = TRG.GetUpperBound(0);
		for (int i = 0; i <= upperBound; i = checked(i + 1))
		{
			double num = 0.0;
			double uG = UGOL(TRG[i].X1 - X1, TRG[i].Y1 - Y1);
			double num2 = UGOL(TRG[i].X2 - X1, TRG[i].Y2 - Y1);
			num += UgMin((float)num2, uG);
			uG = UGOL(TRG[i].X2 - X1, TRG[i].Y2 - Y1);
			num2 = UGOL(TRG[i].X3 - X1, TRG[i].Y3 - Y1);
			num += UgMin((float)num2, uG);
			uG = UGOL(TRG[i].X3 - X1, TRG[i].Y3 - Y1);
			num2 = UGOL(TRG[i].X1 - X1, TRG[i].Y1 - Y1);
			num += UgMin((float)num2, uG);
			if ((num < -10.0) | (num >= 10.0))
			{
				return (float)CalcHeightPlosk(TRG[i].X1, TRG[i].Y1, TRG[i].H1, TRG[i].X2, TRG[i].Y2, TRG[i].H2, TRG[i].X3, TRG[i].Y3, TRG[i].H3, X1, Y1);
			}
		}
		return 0f;
	}

	public static double UGOL(double X, double Y)
	{
		double num;
		if ((X == 0.0) & (Y > 0.0))
		{
			num = 0.0;
		}
		else if ((X == 0.0) & (Y < 0.0))
		{
			num = 180.0;
		}
		else if ((X > 0.0) & (Y == 0.0))
		{
			num = 90.0;
		}
		else if ((X < 0.0) & (Y == 0.0))
		{
			num = 270.0;
		}
		else if ((X == 0.0) & (Y == 0.0))
		{
			num = 0.0;
		}
		else
		{
			num = Math.Atan(Math.Abs(X / Y)) * 180.0 / Math.PI;
			if ((X > 0.0) & (Y < 0.0))
			{
				num = 180.0 - num;
			}
			else if ((X < 0.0) & (Y < 0.0))
			{
				num = 180.0 + num;
			}
			else if ((X < 0.0) & (Y > 0.0))
			{
				num = 360.0 - num;
			}
		}
		return num;
	}

	public static double UgMin(float UG1, double UG2)
	{
		double num = UG2 - (double)UG1;
		if (num > 180.0)
		{
			num -= 360.0;
		}
		if (num < -180.0)
		{
			num = 360.0 + num;
		}
		return num;
	}

	public static double CalcHeightPlosk(double X1, double Y1, double H1, double X2, double Y2, double H2, double X3, double Y3, double H3, double Xp, double Yp)
	{
		double num = (Y2 - Y1) * (H3 - H1) - (H2 - H1) * (Y3 - Y1);
		double num2 = (X2 - X1) * (H3 - H1) - (H2 - H1) * (X3 - X1);
		double num3 = (X2 - X1) * (Y3 - Y1) - (Y2 - Y1) * (X3 - X1);
		if (num3 == 0.0)
		{
			return H1;
		}
		return H1 - (num * (Xp - X1) - num2 * (Yp - Y1)) / num3;
	}

	public static float CalcHeightPloskI(int X1, int Y1, int H1, int X2, int Y2, int H2, int X3, int Y3, int H3, int Xp, int Yp)
	{
		checked
		{
			float num = (Y2 - Y1) * (H3 - H1) - (H2 - H1) * (Y3 - Y1);
			float num2 = (X2 - X1) * (H3 - H1) - (H2 - H1) * (X3 - X1);
			float num3 = (X2 - X1) * (Y3 - Y1) - (Y2 - Y1) * (X3 - X1);
			if (num3 == 0f)
			{
				return H1;
			}
			return (float)H1 - (num * (float)(Xp - X1) - num2 * (float)(Yp - Y1)) / num3;
		}
	}
}
