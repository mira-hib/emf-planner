using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[StandardModule]
internal sealed class DouglasPeuckerMarchingSquares
{
	private struct LIN
	{
		public float X1;

		public float Y1;

		public float X2;

		public float Y2;
	}

	public static List<PointD> GetSimplifyDouglasPeucker(List<PointD> points, double tolerance)
	{
		if (points == null || points.Count < 3)
		{
			return points;
		}
		int num = 0;
		checked
		{
			int num2 = points.Count - 1;
			while (points[num].IsCoincident(points[num2]))
			{
				num2--;
			}
			List<int> list = new List<int>();
			list.Add(num);
			list.Add(num2);
			bool flag = true;
			while (flag)
			{
				int num3 = 0;
				int num4 = list.Count - 2;
				for (int i = 0; i <= num4; i++)
				{
					int num5 = iterateDPT(points, list[i], list[i + 1], tolerance);
					if (num5 > num3)
					{
						num3 = num5;
						break;
					}
				}
				if (num3 > 0)
				{
					list.Add(num3);
					list.Sort();
				}
				else
				{
					flag = false;
				}
			}
			List<PointD> list2 = new List<PointD>();
			list.Sort();
			foreach (int item in list)
			{
				list2.Add(points[item]);
			}
			if ((points[0].X == points[points.Count - 1].X) & (points[0].Y == points[points.Count - 1].Y))
			{
				list2.Add(list2[0]);
			}
			return list2;
		}
	}

	public static void SimplifyDouglasPeucker(Public2.CoordSNG[][] P1, ref Public2.CoordSNG[][] P2, double tolerance)
	{
		if (Information.IsNothing(P1))
		{
			P2 = null;
			return;
		}
		checked
		{
			P2 = new Public2.CoordSNG[P1.GetUpperBound(0) + 1][];
			int upperBound = P1.GetUpperBound(0);
			for (int i = 0; i <= upperBound; i++)
			{
				List<PointD> list = new List<PointD>();
				int upperBound2 = P1[i].GetUpperBound(0);
				for (int j = 0; j <= upperBound2; j++)
				{
					list.Add(new PointD
					{
						X = P1[i][j].X,
						Y = P1[i][j].Y
					});
				}
				List<PointD> list2 = new List<PointD>();
				list2 = GetSimplifyDouglasPeucker(list, tolerance);
				ref Public2.CoordSNG[] reference = ref P2[i];
				reference = (Public2.CoordSNG[])Utils.CopyArray(reference, new Public2.CoordSNG[list2.Count - 1 + 1]);
				int num = list2.Count - 1;
				for (int k = 0; k <= num; k++)
				{
					P2[i][k].X = (float)list2[k].X;
					P2[i][k].Y = (float)list2[k].Y;
				}
			}
		}
	}

	private static int iterateDPT(List<PointD> points, int firstpoint, int lastpoint, double tolerance)
	{
		double num = 0.0;
		int num2 = 0;
		double num3 = 0.0;
		for (int i = firstpoint; i <= lastpoint; i = checked(i + 1))
		{
			num3 = GetTriangleHeight(points[firstpoint], points[lastpoint], points[i]);
			if (num3 > num)
			{
				num = num3;
				num2 = i;
			}
		}
		if (num > tolerance && num2 != 0)
		{
			return num2;
		}
		return 0;
	}

	public static double GetTriangleArea(PointD base1, PointD base2, PointD apex)
	{
		double result;
		try
		{
			result = Math.Abs(0.5 * (base1.X * base2.Y + base2.X * apex.Y + apex.X * base1.Y - base2.X * base1.Y - apex.X * base2.Y - base1.X * apex.Y));
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			result = double.NaN;
			ProjectData.ClearProjectError();
		}
		return result;
	}

	public static double GetTriangleBase(PointD base1, PointD base2, PointD apex)
	{
		double result;
		try
		{
			result = Math.Pow(Math.Pow(base1.X - base2.X, 2.0) + Math.Pow(base1.Y - base2.Y, 2.0), 0.5);
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			result = double.NaN;
			ProjectData.ClearProjectError();
		}
		return result;
	}

	public static double GetTriangleHeight(PointD base1, PointD base2, PointD apex)
	{
		return GetTriangleArea(base1, base2, apex) / GetTriangleBase(base1, base2, apex) * 2.0;
	}

	public static void VectorizationMarchingSquares(float[,] M, ref Public2.CoordSNG[][] V)
	{
		List<LIN> list = new List<LIN>();
		checked
		{
			int num = M.GetUpperBound(0) - 1;
			for (int i = 0; i <= num; i++)
			{
				int num2 = M.GetUpperBound(1) - 1;
				for (int j = 0; j <= num2; j++)
				{
					float num3 = M[i, j];
					float num4 = M[i, j + 1];
					float num5 = M[i + 1, j + 1];
					float num6 = M[i + 1, j];
					if (((num3 > 1f) & (num4 < 1f) & (num5 < 1f) & (num6 < 1f)) | ((num3 < 1f) & (num4 > 1f) & (num5 > 1f) & (num6 > 1f)))
					{
						list.Add(new LIN
						{
							X1 = i,
							Y1 = (float)j + (1f - num3) / (num4 - num3),
							X2 = (float)i + (1f - num3) / (num6 - num3),
							Y2 = j
						});
					}
					else if (((num4 > 1f) & (num3 < 1f) & (num5 < 1f) & (num6 < 1f)) | ((num4 < 1f) & (num3 > 1f) & (num5 > 1f) & (num6 > 1f)))
					{
						list.Add(new LIN
						{
							X1 = i,
							Y1 = (float)j + (1f - num3) / (num4 - num3),
							X2 = (float)i + (1f - num4) / (num5 - num4),
							Y2 = j + 1
						});
					}
					else if (((num5 > 1f) & (num3 < 1f) & (num4 < 1f) & (num6 < 1f)) | ((num5 < 1f) & (num3 > 1f) & (num4 > 1f) & (num6 > 1f)))
					{
						list.Add(new LIN
						{
							X1 = (float)i + (1f - num4) / (num5 - num4),
							Y1 = j + 1,
							X2 = i + 1,
							Y2 = (float)j + (1f - num6) / (num5 - num6)
						});
					}
					else if (((num6 > 1f) & (num3 < 1f) & (num4 < 1f) & (num5 < 1f)) | ((num6 < 1f) & (num3 > 1f) & (num4 > 1f) & (num5 > 1f)))
					{
						list.Add(new LIN
						{
							X1 = (float)i + (1f - num3) / (num6 - num3),
							Y1 = j,
							X2 = i + 1,
							Y2 = (float)j + (1f - num6) / (num5 - num6)
						});
					}
					else if (((num3 > 1f) & (num6 > 1f) & (num4 < 1f) & (num5 < 1f)) | ((num3 < 1f) & (num6 < 1f) & (num4 > 1f) & (num5 > 1f)))
					{
						list.Add(new LIN
						{
							X1 = i,
							Y1 = (float)j + (1f - num3) / (num4 - num3),
							X2 = i + 1,
							Y2 = (float)j + (1f - num6) / (num5 - num6)
						});
					}
					else if (((num3 > 1f) & (num4 > 1f) & (num5 < 1f) & (num6 < 1f)) | ((num3 < 1f) & (num4 < 1f) & (num5 > 1f) & (num6 > 1f)))
					{
						list.Add(new LIN
						{
							X1 = (float)i + (1f - num3) / (num6 - num3),
							Y1 = j,
							X2 = (float)i + (1f - num4) / (num5 - num4),
							Y2 = j + 1
						});
					}
					else if ((num3 > 1f) & (num5 > 1f) & (num4 < 1f) & (num6 < 1f))
					{
						list.Add(new LIN
						{
							X1 = i,
							Y1 = (float)j + (1f - num3) / (num4 - num3),
							X2 = (float)i + (1f - num4) / (num5 - num4),
							Y2 = j + 1
						});
						list.Add(new LIN
						{
							X1 = (float)i + (1f - num3) / (num6 - num3),
							Y1 = j,
							X2 = i + 1,
							Y2 = (float)j + (1f - num6) / (num5 - num6)
						});
					}
					else if ((num4 > 1f) & (num6 > 1f) & (num3 < 1f) & (num5 < 1f))
					{
						list.Add(new LIN
						{
							X1 = i,
							Y1 = (float)j + (1f - num3) / (num4 - num3),
							X2 = (float)i + (1f - num3) / (num6 - num3),
							Y2 = j
						});
						list.Add(new LIN
						{
							X1 = (float)i + (1f - num4) / (num5 - num4),
							Y1 = j + 1,
							X2 = i + 1,
							Y2 = (float)j + (1f - num6) / (num5 - num6)
						});
					}
				}
			}
			if (list.Count == 0)
			{
				V = null;
				return;
			}
			bool flag = true;
			short num7 = -1;
			short num8 = default;
			while (list.Count > 0)
			{
				if (flag)
				{
					num7++;
					num8 = 1;
					flag = false;
					V = (Public2.CoordSNG[][])Utils.CopyArray(V, new Public2.CoordSNG[num7 + 1][]);
					ref Public2.CoordSNG[] reference = ref V[num7];
					reference = (Public2.CoordSNG[])Utils.CopyArray(reference, new Public2.CoordSNG[num8 + 1]);
					V[num7][0].X = list[0].X1;
					V[num7][0].Y = list[0].Y1;
					V[num7][1].X = list[0].X2;
					V[num7][1].Y = list[0].Y2;
					list.Remove(list[0]);
				}
				bool flag2 = false;
				int num9 = list.Count - 1;
				for (int k = 0; k <= num9; k++)
				{
					if ((V[num7][num8].X == list[k].X1) & (V[num7][num8].Y == list[k].Y1))
					{
						num8++;
						ref Public2.CoordSNG[] reference2 = ref V[num7];
						reference2 = (Public2.CoordSNG[])Utils.CopyArray(reference2, new Public2.CoordSNG[num8 + 1]);
						V[num7][num8].X = list[k].X2;
						V[num7][num8].Y = list[k].Y2;
						list.Remove(list[k]);
						flag2 = true;
						break;
					}
					if ((V[num7][num8].X == list[k].X2) & (V[num7][num8].Y == list[k].Y2))
					{
						num8++;
						ref Public2.CoordSNG[] reference3 = ref V[num7];
						reference3 = (Public2.CoordSNG[])Utils.CopyArray(reference3, new Public2.CoordSNG[num8 + 1]);
						V[num7][num8].X = list[k].X1;
						V[num7][num8].Y = list[k].Y1;
						list.Remove(list[k]);
						flag2 = true;
						break;
					}
				}
				if (!flag2)
				{
					flag = true;
				}
			}
			list = null;
			SimplifyDouglasPeucker(V, ref V, 0.1);
		}
	}
}
