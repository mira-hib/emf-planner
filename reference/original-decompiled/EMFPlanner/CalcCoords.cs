using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[StandardModule]
internal sealed class CalcCoords
{
	private const double a_earth = 6378.137;

	private const double b_earth = 6378.137;

	private const double a2b2b2_earth = 0.0;

	private const double f_earth = 0.0;

	public const double PiA = Math.PI * 6378137.0;

	public const double PiA_180 = 111319.49079327358;

	public const double Pi_4 = Math.PI / 4.0;

	public static bool TestLatitude(string StringCoord, ref double DoubleCoordOut)
	{
		checked
		{
			bool result;
			if (Strings.Len(StringCoord) == 0)
			{
				result = false;
			}
			else
			{
				try
				{
					double num = 1.0;
					string left = StringCoord.Substring(0, 1);
					if ((Operators.CompareString(left, "S", TextCompare: false) == 0) | (Operators.CompareString(left, "s", TextCompare: false) == 0) | (Operators.CompareString(left, "-", TextCompare: false) == 0))
					{
						num = -1.0;
					}
					object objectValue = RuntimeHelpers.GetObjectValue(Interaction.CreateObject("VBScript.RegExp"));
					NewLateBinding.LateSet(objectValue, null, "Global", new object[1] { true }, null, null);
					NewLateBinding.LateSet(objectValue, null, "Pattern", new object[1] { "[^0-9.,]" }, null, null);
					object[] array;
					bool[] array2;
					object obj = NewLateBinding.LateGet(objectValue, null, "Replace", array = new object[2] { StringCoord, " " }, null, null, array2 = new bool[2] { true, false });
					if (array2[0])
					{
						StringCoord = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array[0]), typeof(string));
					}
					StringCoord = Conversions.ToString(obj);
					StringCoord = "0" + Strings.Trim(StringCoord);
					StringCoord = ((Operators.CompareString(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ",", TextCompare: false) != 0) ? Strings.Replace(StringCoord, ",", ".") : Strings.Replace(StringCoord, ".", ","));
					string[] array3 = new string[4]
					{
						Conversions.ToString(0),
						Conversions.ToString(0),
						Conversions.ToString(0),
						Conversions.ToString(0)
					};
					int num2 = 0;
					string[] array4 = StringCoord.Split(' ', '\t');
					foreach (string text in array4)
					{
						if (Operators.CompareString(text.Trim(), "", TextCompare: false) != 0)
						{
							num2++;
							array3[num2] = text;
						}
					}
					if ((Conversions.ToDouble(array3[1]) < 0.0) | (Conversions.ToDouble(array3[1]) > 80.0))
					{
						result = false;
					}
					else if ((Conversions.ToDouble(array3[2]) < 0.0) | (Conversions.ToDouble(array3[2]) > 59.999999))
					{
						result = false;
					}
					else if ((Conversions.ToDouble(array3[3]) < 0.0) | (Conversions.ToDouble(array3[3]) > 59.999999))
					{
						result = false;
					}
					else
					{
						DoubleCoordOut = Conversions.ToDouble(array3[1]) + Conversions.ToDouble(array3[2]) / 60.0 + Conversions.ToDouble(array3[3]) / 3600.0;
						result = !(DoubleCoordOut <= 0.0);
						int num3 = (int)Conversion.Int(DoubleCoordOut);
						int num4 = (int)Conversion.Int(Math.Round((DoubleCoordOut - (double)num3) * 60.0, 6));
						_ = (float)Math.Round((DoubleCoordOut - (double)num3 - (double)num4 / 60.0) * 3600.0, 2);
						if (num == -1.0)
						{
							DoubleCoordOut *= num;
						}
					}
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					result = false;
					ProjectData.ClearProjectError();
				}
			}
			return result;
		}
	}

	public static bool TestLongitude(string StringCoord, ref double DoubleCoordOut)
	{
		checked
		{
			bool result;
			if (Strings.Len(StringCoord) == 0)
			{
				result = false;
			}
			else
			{
				try
				{
					double num = 1.0;
					string left = StringCoord.Substring(0, 1);
					if ((Operators.CompareString(left, "W", TextCompare: false) == 0) | (Operators.CompareString(left, "w", TextCompare: false) == 0) | (Operators.CompareString(left, "-", TextCompare: false) == 0))
					{
						num = -1.0;
					}
					object objectValue = RuntimeHelpers.GetObjectValue(Interaction.CreateObject("VBScript.RegExp"));
					NewLateBinding.LateSet(objectValue, null, "Global", new object[1] { true }, null, null);
					NewLateBinding.LateSet(objectValue, null, "Pattern", new object[1] { "[^0-9.,]" }, null, null);
					object[] array;
					bool[] array2;
					object obj = NewLateBinding.LateGet(objectValue, null, "Replace", array = new object[2] { StringCoord, " " }, null, null, array2 = new bool[2] { true, false });
					if (array2[0])
					{
						StringCoord = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array[0]), typeof(string));
					}
					StringCoord = Conversions.ToString(obj);
					StringCoord = "0" + Strings.Trim(StringCoord);
					StringCoord = ((Operators.CompareString(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator, ",", TextCompare: false) != 0) ? Strings.Replace(StringCoord, ",", ".") : Strings.Replace(StringCoord, ".", ","));
					string[] array3 = new string[4]
					{
						Conversions.ToString(0),
						Conversions.ToString(0),
						Conversions.ToString(0),
						Conversions.ToString(0)
					};
					int num2 = 0;
					string[] array4 = StringCoord.Split(' ', '\t');
					foreach (string text in array4)
					{
						if (Operators.CompareString(text.Trim(), "", TextCompare: false) != 0)
						{
							num2++;
							array3[num2] = text;
						}
					}
					if ((Conversions.ToDouble(array3[1]) < 0.0) | (Conversions.ToDouble(array3[1]) > 179.0))
					{
						result = false;
					}
					else if ((Conversions.ToDouble(array3[2]) < 0.0) | (Conversions.ToDouble(array3[2]) > 59.999999))
					{
						result = false;
					}
					else if ((Conversions.ToDouble(array3[3]) < 0.0) | (Conversions.ToDouble(array3[3]) > 59.999999))
					{
						result = false;
					}
					else
					{
						DoubleCoordOut = Conversions.ToDouble(array3[1]) + Conversions.ToDouble(array3[2]) / 60.0 + Conversions.ToDouble(array3[3]) / 3600.0;
						result = !(DoubleCoordOut <= 0.0);
						int num3 = (int)Conversion.Int(DoubleCoordOut);
						int num4 = (int)Conversion.Int(Math.Round((DoubleCoordOut - (double)num3) * 60.0, 6));
						_ = (float)Math.Round((DoubleCoordOut - (double)num3 - (double)num4 / 60.0) * 3600.0, 2);
						if (num == -1.0)
						{
							DoubleCoordOut *= num;
						}
					}
				}
				catch (Exception projectError)
				{
					ProjectData.SetProjectError(projectError);
					result = false;
					ProjectData.ClearProjectError();
				}
			}
			return result;
		}
	}

	public static string ConvLatToStr(double DoubleCoord)
	{
		double num = Math.Abs(DoubleCoord);
		checked
		{
			if (Public2.AppSetup.CoordinatesFormat == 1)
			{
				int num2 = (int)Conversion.Int(num);
				int num3 = (int)Conversion.Int(Math.Round((num - (double)num2) * 60.0, 6));
				float num4 = (float)Math.Round((num - (double)num2 - (double)num3 / 60.0) * 3600.0, 2);
				if (DoubleCoord >= 0.0)
				{
					return Strings.Format(num2, "00") + "°" + Strings.Format(num3, "00") + "'" + Strings.Format(num4, "00.0") + "\"";
				}
				return "S" + Strings.Format(num2, "00") + "°" + Strings.Format(num3, "00") + "'" + Strings.Format(num4, "00.00") + "\"";
			}
			if (Public2.AppSetup.CoordinatesFormat == 0)
			{
				if (DoubleCoord >= 0.0)
				{
					return Strings.Format(num, "00.000000") + "°";
				}
				return "S" + Strings.Format(num, "00.000000") + "°";
			}
			if (Public2.AppSetup.CoordinatesFormat == 2)
			{
				int num5 = (int)Conversion.Int(num);
				float num6 = (float)Math.Round((num - (double)num5) * 60.0, 4);
				if (DoubleCoord >= 0.0)
				{
					return Strings.Format(num5, "00") + "°" + Strings.Format(num6, "00.0000") + "'";
				}
				return "S" + Strings.Format(num5, "00") + "°" + Strings.Format(num6, "00.0000") + "'";
			}
			string result = default;
			return result;
		}
	}

	public static string ConvLonToStr(double DoubleCoord)
	{
		double num = Math.Abs(DoubleCoord);
		checked
		{
			if (Public2.AppSetup.CoordinatesFormat == 1)
			{
				int num2 = (int)Conversion.Int(num);
				int num3 = (int)Conversion.Int(Math.Round((num - (double)num2) * 60.0, 6));
				float num4 = (float)Math.Round((num - (double)num2 - (double)num3 / 60.0) * 3600.0, 2);
				if (DoubleCoord >= 0.0)
				{
					return Strings.Format(num2, "00") + "°" + Strings.Format(num3, "00") + "'" + Strings.Format(num4, "00.0") + "\"";
				}
				return "W" + Strings.Format(num2, "00") + "°" + Strings.Format(num3, "00") + "'" + Strings.Format(num4, "00") + "\"";
			}
			if (Public2.AppSetup.CoordinatesFormat == 0)
			{
				if (DoubleCoord >= 0.0)
				{
					return Strings.Format(num, "00.000000") + "°";
				}
				return "W" + Strings.Format(num, "00.000000") + "°";
			}
			if (Public2.AppSetup.CoordinatesFormat == 2)
			{
				int num5 = (int)Conversion.Int(num);
				float num6 = (float)Math.Round((num - (double)num5) * 60.0, 4);
				if (DoubleCoord >= 0.0)
				{
					return Strings.Format(num5, "00") + "°" + Strings.Format(num6, "00.0000") + "'";
				}
				return "W" + Strings.Format(num5, "00") + "°" + Strings.Format(num6, "00.0000") + "'";
			}
			string result = default;
			return result;
		}
	}

	public static string ConvLatToStrN(double DoubleCoord)
	{
		double num = Math.Abs(DoubleCoord);
		checked
		{
			if (Public2.AppSetup.CoordinatesFormat == 1)
			{
				int num2 = (int)Conversion.Int(num);
				int num3 = (int)Conversion.Int(Math.Round((num - (double)num2) * 60.0, 6));
				float num4 = (float)Math.Round((num - (double)num2 - (double)num3 / 60.0) * 3600.0, 2);
				if (DoubleCoord >= 0.0)
				{
					return "N" + Strings.Format(num2, "00") + "°" + Strings.Format(num3, "00") + "'" + Strings.Format(num4, "00.0") + "\"";
				}
				return "S" + Strings.Format(num2, "00") + "°" + Strings.Format(num3, "00") + "'" + Strings.Format(num4, "00.00") + "\"";
			}
			if (Public2.AppSetup.CoordinatesFormat == 0)
			{
				if (DoubleCoord >= 0.0)
				{
					return "N" + Strings.Format(num, "00.000000") + "°";
				}
				return "S" + Strings.Format(num, "00.000000") + "°";
			}
			if (Public2.AppSetup.CoordinatesFormat == 2)
			{
				int num5 = (int)Conversion.Int(num);
				float num6 = (float)Math.Round((num - (double)num5) * 60.0, 4);
				if (DoubleCoord >= 0.0)
				{
					return "N" + Strings.Format(num5, "00") + "°" + Strings.Format(num6, "00.0000") + "'";
				}
				return "S" + Strings.Format(num5, "00") + "°" + Strings.Format(num6, "00.0000") + "'";
			}
			string result = default;
			return result;
		}
	}

	public static string ConvLonToStrE(double DoubleCoord)
	{
		double num = Math.Abs(DoubleCoord);
		checked
		{
			if (Public2.AppSetup.CoordinatesFormat == 1)
			{
				int num2 = (int)Conversion.Int(num);
				int num3 = (int)Conversion.Int(Math.Round((num - (double)num2) * 60.0, 6));
				float num4 = (float)Math.Round((num - (double)num2 - (double)num3 / 60.0) * 3600.0, 2);
				if (DoubleCoord >= 0.0)
				{
					return "E" + Strings.Format(num2, "00") + "°" + Strings.Format(num3, "00") + "'" + Strings.Format(num4, "00.0") + "\"";
				}
				return "W" + Strings.Format(num2, "00") + "°" + Strings.Format(num3, "00") + "'" + Strings.Format(num4, "00") + "\"";
			}
			if (Public2.AppSetup.CoordinatesFormat == 0)
			{
				if (DoubleCoord >= 0.0)
				{
					return "E" + Strings.Format(num, "00.000000") + "°";
				}
				return "W" + Strings.Format(num, "00.000000") + "°";
			}
			if (Public2.AppSetup.CoordinatesFormat == 2)
			{
				int num5 = (int)Conversion.Int(num);
				float num6 = (float)Math.Round((num - (double)num5) * 60.0, 4);
				if (DoubleCoord >= 0.0)
				{
					return "E" + Strings.Format(num5, "00") + "°" + Strings.Format(num6, "00.0000") + "'";
				}
				return "W" + Strings.Format(num5, "00") + "°" + Strings.Format(num6, "00.0000") + "'";
			}
			string result = default;
			return result;
		}
	}

	public static void OGZ(double Lat1, double Lon1, double Lat2, double Lon2, ref float Dist, ref float Az1)
	{
		Lat1 *= Constants.PI_180;
		Lat2 *= Constants.PI_180;
		Lon1 *= Constants.PI_180;
		Lon2 *= Constants.PI_180;
		double num = Lon2 - Lon1;
		double num2 = 1.0 * Math.Tan(Lat1);
		double num3 = 1.0 / Math.Sqrt(1.0 + num2 * num2);
		double num4 = num2 * num3;
		double num5 = 1.0 * Math.Tan(Lat2);
		double num6 = 1.0 / Math.Sqrt(1.0 + num5 * num5);
		double num7 = num5 * num6;
		double num8 = Math.Sin(num);
		double num9 = Math.Cos(num);
		double num10 = num6 * num8;
		double num11 = num3 * num7 - num4 * num6 * num9;
		double num12 = Math.Sqrt(num10 * num10 + num11 * num11);
		double num13 = num4 * num7 + num3 * num6 * num9;
		double num14 = Math.Atan2(num12, num13);
		double num15 = num3 * num6 * num8 / num12;
		double num16 = 1.0 - num15 * num15;
		double num17 = num13 - 2.0 * num4 * num7 / num16;
		double num18 = 0.0 * num16 * (4.0 + 0.0 * (4.0 - 3.0 * num16)) / 16.0;
		double num19 = num + (1.0 - num18) * 0.0 * num15 * (num14 + num18 * num12 * (num17 + num18 * num13 * (-1.0 + 2.0 * num17 * num17)));
		num8 = Math.Sin(num19);
		num9 = Math.Cos(num19);
		double num20 = num6 * num8;
		num11 = num3 * num7 - num4 * num6 * num9;
		num12 = Math.Sqrt(num20 * num20 + num11 * num11);
		num13 = num4 * num7 + num3 * num6 * num9;
		num14 = Math.Atan2(num12, num13);
		num15 = num3 * num6 * num8 / num12;
		num16 = 1.0 - num15 * num15;
		num17 = num13 - 2.0 * num4 * num7 / num16;
		double num21 = num17 * num17;
		num18 = 0.0 * num16 * (4.0 + 0.0 * (4.0 - 3.0 * num16)) / 16.0;
		double num22 = num + (1.0 - num18) * 0.0 * num15 * (num14 + num18 * num12 * (num17 + num18 * num13 * (-1.0 + 2.0 * num21)));
		num8 = Math.Sin(num22);
		num9 = Math.Cos(num22);
		double num23 = num16 * 0.0;
		double num24 = 1.0 + num23 * (4096.0 + num23 * (-768.0 + num23 * (320.0 - 175.0 * num23))) / 16384.0;
		double num25 = num23 * (256.0 + num23 * (-128.0 + num23 * (74.0 - 47.0 * num23))) / 1024.0;
		double num26 = num25 * num12 * (num17 + num25 * (num13 * (-1.0 + 2.0 * num21) - num25 * num17 * (-3.0 + 4.0 * num12 * num12) * (-3.0 + 4.0 * num21) / 6.0) / 4.0);
		Dist = (float)(6378.137 * num24 * (num14 - num26) * 1000.0);
		Az1 = (float)Math.Atan2(num6 * num8, num3 * num7 - num4 * num6 * num9);
		if (Az1 < 0f)
		{
			Az1 = (float)(Math.PI * 2.0 + (double)Az1);
		}
	}

	public static void PGZ(double Lat1, double Lon1, float S, float Alfa1, ref double Lat2, ref double Lon2)
	{
		Lat1 *= Constants.PI_180;
		Lon1 *= Constants.PI_180;
		Alfa1 = (float)((double)Alfa1 * Constants.PI_180);
		double num = 1.0 * Math.Tan(Lat1);
		double num2 = 1.0 / Math.Sqrt(1.0 + num * num);
		double num3 = num * num2;
		double num4 = Math.Sin(Alfa1);
		double num5 = Math.Cos(Alfa1);
		double num6 = Math.Atan2(num, num5);
		double num7 = num2 * num4;
		double num8 = 1.0 - num7 * num7;
		double num9 = num8 * 0.0;
		double num10 = 1.0 + num9 * (4096.0 + num9 * (-768.0 + num9 * (320.0 - 175.0 * num9))) / 16384.0;
		double num11 = num9 * (256.0 + num9 * (-128.0 + num9 * (74.0 - 47.0 * num9))) / 1024.0;
		double num12 = (double)S / (6378.137 * num10);
		double num13 = Math.Cos(2.0 * num6 + num12);
		double num14 = num13 * num13;
		double num15 = Math.Sin(num12);
		double num16 = Math.Cos(num12);
		double num17 = num11 * num15 * (num13 + num11 * (num16 * (-1.0 + 2.0 * num14) - num11 * num13 * (-3.0 + 4.0 * num15 * num15) * (-3.0 + 4.0 * num14) / 6.0) / 4.0);
		num12 = (double)S / (6378.137 * num10) + num17;
		num13 = Math.Cos(2.0 * num6 + num12);
		num14 = num13 * num13;
		num15 = Math.Sin(num12);
		num16 = Math.Cos(num12);
		num17 = num11 * num15 * (num13 + num11 * (num16 * (-1.0 + 2.0 * num14) - num11 * num13 * (-3.0 + 4.0 * num15 * num15) * (-3.0 + 4.0 * num14) / 6.0) / 4.0);
		num12 = (double)S / (6378.137 * num10) + num17;
		num15 = Math.Sin(num12);
		num16 = Math.Cos(num12);
		double num18 = num3 * num15 - num2 * num16 * num5;
		Lat2 = Math.Atan2(num3 * num16 + num2 * num15 * num5, 1.0 * Math.Sqrt(num7 * num7 + num18 * num18)) * (180.0 / Math.PI);
		double num19 = Math.Atan2(num15 * num4, num2 * num16 - num3 * num15 * num5);
		double num20 = 0.0 * num8 * (4.0 + 0.0 * (4.0 - 3.0 * num8)) / 16.0;
		double num21 = num19 - (1.0 - num20) * 0.0 * num7 * (num12 + num20 * num15 * (num13 + num20 * num16 * (-1.0 + 2.0 * num14)));
		Lon2 = (Lon1 + num21) * (180.0 / Math.PI);
		if (Lon2 > 180.0)
		{
			Lon2 -= 360.0;
		}
	}

	public static float RASSTOYANIE(double Lat1, double Lon1, double Lat2, double Lon2)
	{
		Lat1 *= Constants.PI_180;
		Lat2 *= Constants.PI_180;
		Lon1 *= Constants.PI_180;
		Lon2 *= Constants.PI_180;
		double num = Lon2 - Lon1;
		double num2 = 1.0 * Math.Tan(Lat1);
		double num3 = 1.0 / Math.Sqrt(1.0 + num2 * num2);
		double num4 = num2 * num3;
		double num5 = 1.0 * Math.Tan(Lat2);
		double num6 = 1.0 / Math.Sqrt(1.0 + num5 * num5);
		double num7 = num5 * num6;
		double num8 = Math.Sin(num);
		double num9 = Math.Cos(num);
		double num10 = num6 * num8;
		double num11 = num3 * num7 - num4 * num6 * num9;
		double num12 = Math.Sqrt(num10 * num10 + num11 * num11);
		double num13 = num4 * num7 + num3 * num6 * num9;
		double num14 = Math.Atan2(num12, num13);
		double num15 = num3 * num6 * num8 / num12;
		double num16 = 1.0 - num15 * num15;
		double num17 = num13 - 2.0 * num4 * num7 / num16;
		double num18 = 0.0 * num16 * (4.0 + 0.0 * (4.0 - 3.0 * num16)) / 16.0;
		double num19 = num + (1.0 - num18) * 0.0 * num15 * (num14 + num18 * num12 * (num17 + num18 * num13 * (-1.0 + 2.0 * num17 * num17)));
		num8 = Math.Sin(num19);
		num9 = Math.Cos(num19);
		double num20 = num6 * num8;
		num11 = num3 * num7 - num4 * num6 * num9;
		num12 = Math.Sqrt(num20 * num20 + num11 * num11);
		num13 = num4 * num7 + num3 * num6 * num9;
		num14 = Math.Atan2(num12, num13);
		num15 = num3 * num6 * num8 / num12;
		num16 = 1.0 - num15 * num15;
		num17 = num13 - 2.0 * num4 * num7 / num16;
		double num21 = num17 * num17;
		num18 = 0.0 * num16 * (4.0 + 0.0 * (4.0 - 3.0 * num16)) / 16.0;
		double num22 = num + (1.0 - num18) * 0.0 * num15 * (num14 + num18 * num12 * (num17 + num18 * num13 * (-1.0 + 2.0 * num21)));
		num8 = Math.Sin(num22);
		num9 = Math.Cos(num22);
		double num23 = num16 * 0.0;
		double num24 = 1.0 + num23 * (4096.0 + num23 * (-768.0 + num23 * (320.0 - 175.0 * num23))) / 16384.0;
		double num25 = num23 * (256.0 + num23 * (-128.0 + num23 * (74.0 - 47.0 * num23))) / 1024.0;
		double num26 = num25 * num12 * (num17 + num25 * (num13 * (-1.0 + 2.0 * num21) - num25 * num17 * (-3.0 + 4.0 * num12 * num12) * (-3.0 + 4.0 * num21) / 6.0) / 4.0);
		return (float)(6378.137 * num24 * (num14 - num26));
	}

	public static void ConvWebMercMetersToTile(Public2.CoordWebMerc CoordWM, ref double Xt, ref double Yt)
	{
		checked
		{
			int num = (int)Math.Round(Math.Pow(2.0, Public2.PRJ.SETT.ZOOM - 1));
			Xt = (double)num * (CoordWM.Xwm / (Math.PI * 6378137.0) + 1.0);
			Yt = (double)num * (1.0 - CoordWM.Ywm / (Math.PI * 6378137.0));
			if (Public2.PRJ.SETT.Projection == 2)
			{
				CalcTileWeb.CalcY_GoogleToYandex((short)Public2.PRJ.SETT.ZOOM, Yt, ref Yt);
			}
		}
	}

	public static void ConvWebMercMetersToTile(Public2.CoordWebMerc CoordWM, byte Z, ref double Xt, ref double Yt)
	{
		int num = checked((int)Math.Round(Math.Pow(2.0, Z - 1)));
		Xt = (double)num * (CoordWM.Xwm / (Math.PI * 6378137.0) + 1.0);
		Yt = (double)num * (1.0 - CoordWM.Ywm / (Math.PI * 6378137.0));
		if (Public2.PRJ.SETT.Projection == 2)
		{
			CalcTileWeb.CalcY_GoogleToYandex(Z, Yt, ref Yt);
		}
	}

	public static void ConvTileToWebMercMeters(double Xt, double Yt, ref Public2.CoordWebMerc CoordWM)
	{
		checked
		{
			double num = Math.Pow(2.0, 1 - Public2.PRJ.SETT.ZOOM);
			if (Public2.PRJ.SETT.Projection == 2)
			{
				CalcTileWeb.CalcY_YandexToGoogle((short)Public2.PRJ.SETT.ZOOM, Yt, ref Yt);
			}
			CoordWM.Xwm = Math.PI * 6378137.0 * (num * Xt - 1.0);
			CoordWM.Ywm = Math.PI * 6378137.0 * (1.0 - num * Yt);
		}
	}

	public static void ConvWebMercMetersToWGS84(Public2.CoordWebMerc CoordWM, ref double LatWgs, ref double LonWgs)
	{
		LonWgs = CoordWM.Xwm / 111319.49079327358;
		LatWgs = (2.0 * Math.Atan(Math.Exp(CoordWM.Ywm / 6378137.0)) - Math.PI / 2.0) / Constants.PI_180;
	}

	public static void ConvWGS84toWebMercMeters(double LatWgs, double LonWgs, ref Public2.CoordWebMerc CoordWM)
	{
		CoordWM.Xwm = LonWgs * 6378137.0 * Constants.PI_180;
		CoordWM.Ywm = Math.Log(Math.Tan(LatWgs * Constants.PI_180 * 0.5 + Math.PI / 4.0)) * 6378137.0;
	}

	public static Public2.CoordMeters ConvWebMercMetersToMeters(Public2.CoordWebMerc CoordWM)
	{
		Public2.CoordMeters result = default;
		if ((Public2.PRJ.SETT.OriginCoordWM.Xwm == CoordWM.Xwm) & (Public2.PRJ.SETT.OriginCoordWM.Ywm == CoordWM.Ywm))
		{
			result.Xm = 0.0;
			result.Ym = 0.0;
		}
		else
		{
			double LatWgs = default;
			double LonWgs = default;
			ConvWebMercMetersToWGS84(CoordWM, ref LatWgs, ref LonWgs);
			float Dist = default;
			float Az = default;
			OGZ(Public2.PRJ.SETT.OriginCoordWGS.Lat, Public2.PRJ.SETT.OriginCoordWGS.Lon, LatWgs, LonWgs, ref Dist, ref Az);
			result.Xm = (double)Dist * Math.Sin(Az);
			result.Ym = (double)Dist * Math.Cos(Az);
		}
		return result;
	}

	public static Public2.CoordMeters ConvWebMercMetersToMetersRound(Public2.CoordWebMerc CoordWM)
	{
		Public2.CoordMeters result = default;
		if ((Public2.PRJ.SETT.OriginCoordWM.Xwm == CoordWM.Xwm) & (Public2.PRJ.SETT.OriginCoordWM.Ywm == CoordWM.Ywm))
		{
			result.Xm = 0.0;
			result.Ym = 0.0;
		}
		else
		{
			double LatWgs = default;
			double LonWgs = default;
			ConvWebMercMetersToWGS84(CoordWM, ref LatWgs, ref LonWgs);
			float Dist = default;
			float Az = default;
			OGZ(Public2.PRJ.SETT.OriginCoordWGS.Lat, Public2.PRJ.SETT.OriginCoordWGS.Lon, LatWgs, LonWgs, ref Dist, ref Az);
			result.Xm = Math.Round((double)Dist * Math.Sin(Az), 1);
			result.Ym = Math.Round((double)Dist * Math.Cos(Az), 1);
		}
		return result;
	}

	public static Public2.CoordWebMerc ConvMetersToWebMercMeters(Public2.CoordMeters CoordM)
	{
		double num = CalcMashtWMtoMeters();
		Public2.CoordWebMerc result = default;
		result.Xwm = Public2.PRJ.SETT.OriginCoordWM.Xwm + CoordM.Xm / num;
		result.Ywm = Public2.PRJ.SETT.OriginCoordWM.Ywm + CoordM.Ym / num;
		return result;
	}

	public static void ConvScaleWebMercMetersToMeters()
	{
		Public2.PRJ.SETT.HorAreaCoordM[0] = ConvWebMercMetersToMeters(Public2.PRJ.SETT.HorAreaCoordWM[0]);
		Public2.PRJ.SETT.HorAreaCoordM[1] = ConvWebMercMetersToMeters(Public2.PRJ.SETT.HorAreaCoordWM[1]);
	}

	public static void ConvAllTxWebMercMetersToMeters()
	{
		if (!Information.IsNothing(Public2.PRJ.TX))
		{
			int upperBound = Public2.PRJ.TX.GetUpperBound(0);
			for (int i = 0; i <= upperBound; i = checked(i + 1))
			{
				Public2.PRJ.TX[i].CoordM = ConvWebMercMetersToMetersRound(Public2.PRJ.TX[i].CoordWM);
			}
		}
	}

	public static void ConvAllControlPointsWebMercMetersToMeters()
	{
		if (Information.IsNothing(Public2.PRJ.CPG))
		{
			return;
		}
		int upperBound = Public2.PRJ.CPG.GetUpperBound(0);
		checked
		{
			for (int i = 0; i <= upperBound; i++)
			{
				if (!Information.IsNothing(Public2.PRJ.CPG[i].CP))
				{
					int upperBound2 = Public2.PRJ.CPG[i].CP.GetUpperBound(0);
					for (int j = 0; j <= upperBound2; j++)
					{
						Public2.PRJ.CPG[i].CP[j].CoordM = ConvWebMercMetersToMetersRound(Public2.PRJ.CPG[i].CP[j].CoordWM);
					}
				}
			}
		}
	}

	public static void ConvAllVertPlanesWebMercMetersToMeters()
	{
		if (!Information.IsNothing(Public2.PRJ.VP))
		{
			int upperBound = Public2.PRJ.VP.GetUpperBound(0);
			for (int i = 0; i <= upperBound; i = checked(i + 1))
			{
				Public2.PRJ.VP[i].CoordM = ConvWebMercMetersToMetersRound(Public2.PRJ.VP[i].CoordWM);
			}
		}
	}

	public static double CalcMashtWMtoMeters()
	{
		Public2.CoordWebMerc originCoordWM = Public2.PRJ.SETT.OriginCoordWM;
		originCoordWM.Xwm++;
		return ConvWebMercMetersToMeters(originCoordWM).Xm;
	}

	public static double CalcMashtWMtoPixel()
	{
		double Xt = default;
		double Yt = default;
		ConvWebMercMetersToTile(Public2.PRJ.SETT.OriginCoordWM, ref Xt, ref Yt);
		Public2.CoordWebMerc originCoordWM = Public2.PRJ.SETT.OriginCoordWM;
		originCoordWM.Xwm++;
		double Xt2 = default;
		double Yt2 = default;
		ConvWebMercMetersToTile(originCoordWM, ref Xt2, ref Yt2);
		return (Xt2 - Xt) * 256.0;
	}

	public static double CalcMashtMeterstoPixel()
	{
		double Xt = default;
		double Yt = default;
		ConvWebMercMetersToTile(Public2.PRJ.SETT.OriginCoordWM, ref Xt, ref Yt);
		Public2.CoordWebMerc originCoordWM = Public2.PRJ.SETT.OriginCoordWM;
		originCoordWM.Xwm += 1.0 / CalcMashtWMtoMeters();
		double Xt2 = default;
		double Yt2 = default;
		ConvWebMercMetersToTile(originCoordWM, ref Xt2, ref Yt2);
		return (Xt2 - Xt) * 256.0;
	}

	public static double CalcMashtMeterstoPixel(Public2.CoordWebMerc CoordWM)
	{
		double Xt = default;
		double Yt = default;
		ConvWebMercMetersToTile(CoordWM, ref Xt, ref Yt);
		Public2.CoordWebMerc coordWM = CoordWM;
		coordWM.Xwm += 1.0 / CalcMashtWMtoMeters();
		double Xt2 = default;
		double Yt2 = default;
		ConvWebMercMetersToTile(coordWM, ref Xt2, ref Yt2);
		return (Xt2 - Xt) * 256.0;
	}
}
