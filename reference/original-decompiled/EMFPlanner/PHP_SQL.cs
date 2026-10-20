using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[StandardModule]
internal sealed class PHP_SQL
{
	public struct CoordMercator
	{
		public double X;

		public double Y;
	}

	private static GisDatabaseConnector ApiConnector;

	public const double Pi_360 = Math.PI / 360.0;

	public const double Pi_4 = Math.PI / 4.0;

	public const double A_Pi_180 = 111319.49079327358;

	private static double A1;

	private static double B1;

	private static double C1;

	private static double A2;

	private static double B2;

	private static double C2;

	private static double C1op;

	private static double C2op;

	private static bool FlagVert1;

	private static bool FlagVert2;

	public static void GetBuilding(string InputGeometry, ref string[] BuildingGeometry, ref string[] BuildingLevels, ref string[] BuildingHeight)
	{
		ApiConnector = new GisDatabaseConnector();
		checked
		{
			try
			{
				List<Building> buildings = ApiConnector.GetBuildings(InputGeometry);
				long num = 0L;
				foreach (Building item in buildings)
				{
					BuildingGeometry = (string[])Utils.CopyArray(BuildingGeometry, new string[(int)num + 1]);
					BuildingLevels = (string[])Utils.CopyArray(BuildingLevels, new string[(int)num + 1]);
					BuildingHeight = (string[])Utils.CopyArray(BuildingHeight, new string[(int)num + 1]);
					BuildingLevels[(int)num] = GetStringValue(item.buildingLevels);
					BuildingHeight[(int)num] = Module1.TchkToZpt(GetStringValue(item.buildingHeight));
					BuildingGeometry[(int)num] = GetStringValue(item.way);
					num++;
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Interaction.MsgBox(ex2.Message, MsgBoxStyle.Critical, "Error!");
				ProjectData.ClearProjectError();
			}
		}
	}

	public static void GetHighway(string InputGeometry, ref string[] HighwayGeometry, ref string[] HighwayValue)
	{
		ApiConnector = new GisDatabaseConnector();
		checked
		{
			try
			{
				List<Building> highway = ApiConnector.GetHighway(InputGeometry);
				long num = 0L;
				foreach (Building item in highway)
				{
					HighwayGeometry = (string[])Utils.CopyArray(HighwayGeometry, new string[(int)num + 1]);
					HighwayValue = (string[])Utils.CopyArray(HighwayValue, new string[(int)num + 1]);
					HighwayValue[(int)num] = GetStringValue(item.highway);
					HighwayGeometry[(int)num] = GetStringValue(item.way);
					num++;
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				ProjectData.ClearProjectError();
			}
		}
	}

	public static void GetBarrier(string InputGeometry, ref string[] BarrierGeometry, ref string[] BarrierValue)
	{
		ApiConnector = new GisDatabaseConnector();
		checked
		{
			try
			{
				List<Building> barrier = ApiConnector.GetBarrier(InputGeometry);
				long num = 0L;
				foreach (Building item in barrier)
				{
					BarrierGeometry = (string[])Utils.CopyArray(BarrierGeometry, new string[(int)num + 1]);
					BarrierValue = (string[])Utils.CopyArray(BarrierValue, new string[(int)num + 1]);
					BarrierValue[(int)num] = GetStringValue(item.barrier);
					BarrierGeometry[(int)num] = GetStringValue(item.way);
					num++;
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				ProjectData.ClearProjectError();
			}
		}
	}

	public static void GetLeisure(string InputGeometry, ref string[] LeisureGeometry, ref string[] LeisureValue)
	{
		ApiConnector = new GisDatabaseConnector();
		checked
		{
			try
			{
				List<Building> leisure = ApiConnector.GetLeisure(InputGeometry);
				long num = 0L;
				foreach (Building item in leisure)
				{
					LeisureGeometry = (string[])Utils.CopyArray(LeisureGeometry, new string[(int)num + 1]);
					LeisureValue = (string[])Utils.CopyArray(LeisureValue, new string[(int)num + 1]);
					LeisureValue[(int)num] = GetStringValue(item.leisure);
					LeisureGeometry[(int)num] = GetStringValue(item.way);
					num++;
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				ProjectData.ClearProjectError();
			}
		}
	}

	public static void GetAmenity(string InputGeometry, ref string[] AmenityGeometry, ref string[] AmenityValue)
	{
		ApiConnector = new GisDatabaseConnector();
		checked
		{
			try
			{
				List<Building> amenity = ApiConnector.GetAmenity(InputGeometry);
				long num = 0L;
				foreach (Building item in amenity)
				{
					AmenityGeometry = (string[])Utils.CopyArray(AmenityGeometry, new string[(int)num + 1]);
					AmenityValue = (string[])Utils.CopyArray(AmenityValue, new string[(int)num + 1]);
					AmenityValue[(int)num] = GetStringValue(item.leisure);
					AmenityGeometry[(int)num] = GetStringValue(item.way);
					num++;
				}
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				ProjectData.ClearProjectError();
			}
		}
	}

	public static string Double2Hex(double D)
	{
		string text = Conversion.Hex(IPAddress.HostToNetworkOrder(BitConverter.DoubleToInt64Bits(D)));
		if (text.Length < 16)
		{
			text = new string('0', checked(16 - text.Length)) + text;
		}
		return text;
	}

	public static double Hex2Double(string S)
	{
		return BitConverter.Int64BitsToDouble(IPAddress.HostToNetworkOrder(long.Parse(S, NumberStyles.HexNumber)));
	}

	public static double Hex2Int64(string S)
	{
		return Convert.ToInt32(S.Substring(6, 2) + S.Substring(4, 2) + S.Substring(2, 2) + S.Substring(0, 2), 16);
	}

	public static void Wgs2Mercator(double LatW, double LonW, ref double LatM, ref double LonM)
	{
		LonM = 111319.49079327358 * LonW;
		LatM = 6378137.0 * Math.Log(Math.Tan(Math.PI / 4.0 + LatW * (Math.PI / 360.0)));
	}

	public static void Mercator2Wgs(double LatM, double LonM, ref double LatW, ref double LonW)
	{
		LonW = LonM / 111319.49079327358;
		LatW = (2.0 * Math.Atan(Math.Pow(Math.E, LatM / 6378137.0)) - Math.PI / 2.0) / Constants.PI_180;
	}

	public static string CoordPoint2Geometry(double Lat, double Lon)
	{
		double LatM = default;
		double LonM = default;
		Wgs2Mercator(Lat, Lon, ref LatM, ref LonM);
		return "0101000020110F0000" + Double2Hex(LonM) + Double2Hex(LatM);
	}

	public static string CoordSquare2Geometry(double Lat, double Lon, double W, double H)
	{
		double num = W / 2.0;
		double num2 = H / 2.0;
		double LatM = default;
		double LonM = default;
		Wgs2Mercator(Lat, Lon, ref LatM, ref LonM);
		return "0103000020110F00000100000005000000" + Double2Hex(LonM - num) + Double2Hex(LatM + num2) + Double2Hex(LonM + num) + Double2Hex(LatM + num2) + Double2Hex(LonM + num) + Double2Hex(LatM - num2) + Double2Hex(LonM - num) + Double2Hex(LatM - num2) + Double2Hex(LonM - num) + Double2Hex(LatM + num2);
	}

	public static string SquareMercator2Geometry(double Xmin, double Xmax, double Ymin, double Ymax)
	{
		return "0103000020110F00000100000005000000" + Double2Hex(Xmin) + Double2Hex(Ymin) + Double2Hex(Xmin) + Double2Hex(Ymax) + Double2Hex(Xmax) + Double2Hex(Ymax) + Double2Hex(Xmax) + Double2Hex(Ymin) + Double2Hex(Xmin) + Double2Hex(Ymin);
	}

	public static string PointMercator2Geometry(double X, double Y)
	{
		return "0101000020110F0000" + Double2Hex(X) + Double2Hex(Y);
	}

	public static string LineMercator2Geometry(double X1, double Y1, double X2, double Y2)
	{
		return "0102000020110F000002000000" + Double2Hex(X1) + Double2Hex(Y1) + Double2Hex(X2) + Double2Hex(Y2);
	}

	public static void ConvertGeometryPolygon2Coord(string GEOM, ref CoordMercator[][] CoordMerc)
	{
		GEOM = GEOM.Substring(18);
		string s = GEOM.Substring(0, 8);
		checked
		{
			int num = (int)Math.Round(Hex2Int64(s) - 1.0);
			CoordMerc = new CoordMercator[num + 1][];
			GEOM = GEOM.Substring(8);
			int num2 = num;
			for (int i = 0; i <= num2; i++)
			{
				s = GEOM.Substring(0, 8);
				int num3 = (int)Math.Round(Hex2Int64(s) - 1.0);
				CoordMerc[i] = new CoordMercator[num3 + 1];
				GEOM = GEOM.Substring(8);
				int num4 = num3;
				for (int j = 0; j <= num4; j++)
				{
					s = GEOM.Substring(0, 16);
					CoordMerc[i][j].X = Hex2Double(s);
					GEOM = GEOM.Substring(16);
					s = GEOM.Substring(0, 16);
					CoordMerc[i][j].Y = Hex2Double(s);
					GEOM = GEOM.Substring(16);
				}
			}
		}
	}

	public static void ConvertGeometryPolyline2Coord(string GEOM, ref CoordMercator[] CoordMerc)
	{
		GEOM = GEOM.Substring(18);
		string s = GEOM.Substring(0, 8);
		checked
		{
			int num = (int)Math.Round(Hex2Int64(s) - 1.0);
			CoordMerc = new CoordMercator[num + 1];
			GEOM = GEOM.Substring(8);
			int num2 = num;
			for (int i = 0; i <= num2; i++)
			{
				s = GEOM.Substring(0, 16);
				CoordMerc[i].X = Hex2Double(s);
				GEOM = GEOM.Substring(16);
				s = GEOM.Substring(0, 16);
				CoordMerc[i].Y = Hex2Double(s);
				GEOM = GEOM.Substring(16);
			}
		}
	}

	public static string GetStringValue(object dbValue)
	{
		if (dbValue != DBNull.Value)
		{
			return Convert.ToString(RuntimeHelpers.GetObjectValue(dbValue));
		}
		return "";
	}

	public static CoordMercator CoordCenterSign(CoordMercator[] CoordMerc)
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
			CoordMercator result = default;
			result.Y = (num + y) / 2.0;
			int num3 = CoordMerc.GetUpperBound(0) - 1;
			double num5 = default;
			for (int j = 0; j <= num3; j++)
			{
				if (((CoordMerc[j].Y >= result.Y) & (CoordMerc[j + 1].Y < result.Y)) | ((CoordMerc[j].Y < result.Y) & (CoordMerc[j + 1].Y >= result.Y)))
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

	public static CoordMercator CoordCenterSign2(CoordMercator[] CoordMerc)
	{
		double y = CoordMerc[0].Y;
		double num = y;
		int upperBound = CoordMerc.GetUpperBound(0);
		checked
		{
			for (int i = 0; i <= upperBound; i++)
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
			CoordMercator result = default;
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

	public static void CreateRoad(CoordMercator[] PolyLine1, float Width, ref CoordMercator[] PolyLine2, ref CoordMercator[] PolyLine3)
	{
		if (PolyLine1.GetUpperBound(0) < 1)
		{
			return;
		}
		checked
		{
			PolyLine2 = (CoordMercator[])Utils.CopyArray(PolyLine2, new CoordMercator[PolyLine1.GetUpperBound(0) + 1]);
			PolyLine3 = (CoordMercator[])Utils.CopyArray(PolyLine3, new CoordMercator[PolyLine1.GetUpperBound(0) + 1]);
			int num = PolyLine1.GetUpperBound(0) - 2;
			for (int i = 0; i <= num; i++)
			{
				FlagVert1 = false;
				FlagVert2 = false;
				if (PolyLine1[i + 1].X > PolyLine1[i].X)
				{
					A1 = (PolyLine1[i + 1].Y - PolyLine1[i].Y) / (PolyLine1[i + 1].X - PolyLine1[i].X);
					B1 = PolyLine1[i].Y - PolyLine1[i].X * A1;
					C1 = B1 + (double)Width * Math.Sqrt(1.0 + A1 * A1);
					C1op = B1 - (double)Width * Math.Sqrt(1.0 + A1 * A1);
				}
				else if (PolyLine1[i + 1].X < PolyLine1[i].X)
				{
					A1 = (PolyLine1[i + 1].Y - PolyLine1[i].Y) / (PolyLine1[i + 1].X - PolyLine1[i].X);
					B1 = PolyLine1[i].Y - PolyLine1[i].X * A1;
					C1 = B1 - (double)Width * Math.Sqrt(1.0 + A1 * A1);
					C1op = B1 + (double)Width * Math.Sqrt(1.0 + A1 * A1);
				}
				else
				{
					FlagVert1 = true;
				}
				if (PolyLine1[i + 2].X > PolyLine1[i + 1].X)
				{
					A2 = (PolyLine1[i + 2].Y - PolyLine1[i + 1].Y) / (PolyLine1[i + 2].X - PolyLine1[i + 1].X);
					B2 = PolyLine1[i + 1].Y - PolyLine1[i + 1].X * A2;
					C2 = B2 + (double)Width * Math.Sqrt(1.0 + A2 * A2);
					C2op = B2 - (double)Width * Math.Sqrt(1.0 + A2 * A2);
				}
				else if (PolyLine1[i + 2].X < PolyLine1[i + 1].X)
				{
					A2 = (PolyLine1[i + 2].Y - PolyLine1[i + 1].Y) / (PolyLine1[i + 2].X - PolyLine1[i + 1].X);
					B2 = PolyLine1[i + 1].Y - PolyLine1[i + 1].X * A2;
					C2 = B2 - (double)Width * Math.Sqrt(1.0 + A2 * A2);
					C2op = B2 + (double)Width * Math.Sqrt(1.0 + A2 * A2);
				}
				else
				{
					FlagVert2 = true;
				}
				if (!FlagVert1 & !FlagVert2 & (A1 != A2))
				{
					PolyLine2[i + 1].X = (C2 - C1) / (A1 - A2);
					PolyLine2[i + 1].Y = A1 * PolyLine2[i + 1].X + C1;
					PolyLine3[i + 1].X = (C2op - C1op) / (A1 - A2);
					PolyLine3[i + 1].Y = A1 * PolyLine3[i + 1].X + C1op;
				}
				else if (FlagVert1 & !FlagVert2)
				{
					if (PolyLine1[i].Y < PolyLine1[i + 1].Y)
					{
						PolyLine2[i + 1].X = PolyLine1[i].X - (double)Width;
						PolyLine2[i + 1].Y = A2 * PolyLine2[i + 1].X + C2;
						PolyLine3[i + 1].X = PolyLine1[i].X + (double)Width;
						PolyLine3[i + 1].Y = A2 * PolyLine3[i + 1].X + C2op;
					}
					else
					{
						PolyLine2[i + 1].X = PolyLine1[i].X + (double)Width;
						PolyLine2[i + 1].Y = A2 * PolyLine2[i + 1].X + C2;
						PolyLine3[i + 1].X = PolyLine1[i].X - (double)Width;
						PolyLine3[i + 1].Y = A2 * PolyLine3[i + 1].X + C2op;
					}
				}
				else if (FlagVert2 & !FlagVert1)
				{
					if (PolyLine1[i + 1].Y < PolyLine1[i + 2].Y)
					{
						PolyLine2[i + 1].X = PolyLine1[i + 1].X - (double)Width;
						PolyLine2[i + 1].Y = A1 * PolyLine2[i + 1].X + C1;
						PolyLine3[i + 1].X = PolyLine1[i + 1].X + (double)Width;
						PolyLine3[i + 1].Y = A1 * PolyLine3[i + 1].X + C1op;
					}
					else
					{
						PolyLine2[i + 1].X = PolyLine1[i + 1].X + (double)Width;
						PolyLine2[i + 1].Y = A1 * PolyLine2[i + 1].X + C1;
						PolyLine3[i + 1].X = PolyLine1[i + 1].X - (double)Width;
						PolyLine3[i + 1].Y = A1 * PolyLine3[i + 1].X + C1op;
					}
				}
				else if (FlagVert1 & FlagVert2)
				{
					if (PolyLine1[i].Y < PolyLine1[i + 1].Y)
					{
						PolyLine2[i + 1].X = PolyLine1[i].X - (double)Width;
						PolyLine2[i + 1].Y = PolyLine1[i].Y;
						PolyLine3[i + 1].X = PolyLine1[i].X + (double)Width;
						PolyLine3[i + 1].Y = PolyLine1[i].Y;
					}
					else
					{
						PolyLine2[i + 1].X = PolyLine1[i].X + (double)Width;
						PolyLine2[i + 1].Y = PolyLine1[i].Y;
						PolyLine3[i + 1].X = PolyLine1[i].X - (double)Width;
						PolyLine3[i + 1].Y = PolyLine1[i].Y;
					}
				}
				else if (PolyLine1[i].X < PolyLine1[i + 1].X)
				{
					PolyLine2[i + 1].Y = PolyLine1[i + 1].Y + (double)Width;
					PolyLine2[i + 1].X = PolyLine1[i + 1].X;
					PolyLine3[i + 1].Y = PolyLine1[i + 1].Y - (double)Width;
					PolyLine3[i + 1].X = PolyLine1[i + 1].X;
				}
				else
				{
					PolyLine2[i + 1].Y = PolyLine1[i + 1].Y - (double)Width;
					PolyLine2[i + 1].X = PolyLine1[i + 1].X;
					PolyLine3[i + 1].Y = PolyLine1[i + 1].Y + (double)Width;
					PolyLine3[i + 1].X = PolyLine1[i + 1].X;
				}
			}
			double num2 = Math.Atan2(PolyLine1[1].X - PolyLine1[0].X, PolyLine1[1].Y - PolyLine1[0].Y);
			double num3 = num2 - Math.PI / 2.0;
			double num4 = num2 + Math.PI / 2.0;
			PolyLine2[0].X = PolyLine1[0].X + (double)Width * Math.Sin(num3);
			PolyLine2[0].Y = PolyLine1[0].Y + (double)Width * Math.Cos(num3);
			PolyLine3[0].X = PolyLine1[0].X + (double)Width * Math.Sin(num4);
			PolyLine3[0].Y = PolyLine1[0].Y + (double)Width * Math.Cos(num4);
			double num5 = Math.Atan2(PolyLine1[PolyLine1.GetUpperBound(0)].X - PolyLine1[PolyLine1.GetUpperBound(0) - 1].X, PolyLine1[PolyLine1.GetUpperBound(0)].Y - PolyLine1[PolyLine1.GetUpperBound(0) - 1].Y);
			num3 = num5 - Math.PI / 2.0;
			num4 = num5 + Math.PI / 2.0;
			PolyLine2[PolyLine2.GetUpperBound(0)].X = PolyLine1[PolyLine1.GetUpperBound(0)].X + (double)Width * Math.Sin(num3);
			PolyLine2[PolyLine2.GetUpperBound(0)].Y = PolyLine1[PolyLine1.GetUpperBound(0)].Y + (double)Width * Math.Cos(num3);
			PolyLine3[PolyLine3.GetUpperBound(0)].X = PolyLine1[PolyLine1.GetUpperBound(0)].X + (double)Width * Math.Sin(num4);
			PolyLine3[PolyLine3.GetUpperBound(0)].Y = PolyLine1[PolyLine1.GetUpperBound(0)].Y + (double)Width * Math.Cos(num4);
		}
	}

	public static double RASSTOYANIE(double SHIR_GR1, double DOLG_GR1, double SHIR_GR2, double DOLG_GR2)
	{
		double num = SHIR_GR1 * 3600.0;
		double num2 = DOLG_GR1 * 3600.0;
		double num3 = SHIR_GR2 * 3600.0;
		double num4 = DOLG_GR2 * 3600.0;
		if ((num == num3) & (num2 == num4))
		{
			return 0.0;
		}
		if (num2 == num4)
		{
			num4 += 1E-10;
		}
		double num5 = 593.60216;
		double num6 = 197.867385;
		double num7 = (num3 - num) * 0.0001;
		double num8 = (num4 - num2) * 0.0001;
		double num9 = (num + num3) / 2.0 * Math.PI / 180.0 / 3600.0;
		double num10 = Math.Cos(num9);
		double num11 = (num5 + Math.Pow(num10, 2.0)) / (num6 + Math.Pow(num10, 2.0));
		double num12 = 103422.05 * num10;
		double num13 = 9.5144 * num10 + Math.Pow(0.5525 * num10, 3.0) - 0.0078 * Math.Pow(num10, 5.0);
		double num14 = -10.1287 * num10 + 10.1287 * Math.Pow(num10, 3.0);
		double num15 = 103422.05 - 696.9116 * Math.Pow(num10, 2.0) + 4.6954 * Math.Pow(num10, 4.0) - 0.031 * Math.Pow(num10, 6.0);
		double num16 = -30.386 + 10.3334 * Math.Pow(num10, 2.0) - 0.2061 * Math.Pow(num10, 4.0) + 0.0014 * Math.Pow(num10, 6.0);
		double num17 = -0.2048 + 0.4192 * Math.Pow(num10, 2.0) - 0.0124 * Math.Pow(num10, 4.0);
		double num18 = 2.9381 + 0.0132 * Math.Pow(num10, 2.0);
		double num19 = 1.9587 * Math.Pow(num10, 2.0) + 0.0132 * Math.Pow(num10, 4.0);
		double num20 = num12 * num8 + num13 * Math.Pow(num7, 2.0) * num8 + num14 * Math.Pow(num8, 3.0);
		double x = num15 * num7 + num16 * num7 * Math.Pow(num8, 2.0) + num17 * Math.Pow(num7, 3.0);
		double num21 = 10000.0 * num8 + num18 * Math.Pow(num7, 2.0) * num8 + num19 * Math.Pow(num8, 3.0);
		Math.Sin(num9);
		double a = Math.Atan2(num20, x);
		return Math.Abs(num11 * num20 / Math.Sin(a));
	}
}
