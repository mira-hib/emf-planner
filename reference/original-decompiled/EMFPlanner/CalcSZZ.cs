using System;
using System.Collections.Generic;
using System.Linq;
using EMFPlanner.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[StandardModule]
internal sealed class CalcSZZ
{
	public struct TEMP
	{
		public float H;

		public float Lambda;

		public float Azimut;

		public float Tilt;

		public float[] DnH;

		public float[] DnV;

		public float PROIZV;

		public float Koef;

		public float[] PDU;

		public float[] AttenuationFactor;

		public float BuildCustomAtten;
	}

	public struct LimPoint
	{
		public float F;

		public float E;

		public float A;

		public LimPoint(float _f, float _e, float _a)
		{
			this = default;
			F = _f;
			E = _e;
			A = _a;
		}
	}

	public struct WallBLD
	{
		public Public2.CoordMeters P1;

		public Public2.CoordMeters P2;

		public float H;
	}

	public static int[] ListHVP;

	public static int[] ListTX;

	public static bool FlagStopCalc;

	public const double Pi05 = Math.PI / 2.0;

	public const double Pi15 = 4.71238898038469;

	public static TEMP[] TMP;

	private static float[][][,] hKB;

	private static float[][,] hLR;

	public static float[,] ElevMatr;

	public static float[,] BldMatr;

	public static bool CalcTmp()
	{
		if (Information.IsNothing(Public2.PRJ.TX))
		{
			Interaction.MsgBox(Localization.LocMSG(46));
			return false;
		}
		Thf73();
		checked
		{
			TMP = new TEMP[Public2.PRJ.TX.GetUpperBound(0) + 1];
			Module1.GetOriginHeihgt();
			int upperBound = Public2.PRJ.TX.GetUpperBound(0);
			for (int i = 0; i <= upperBound; i++)
			{
				if (!Public2.PRJ.TX[i].Active)
				{
					continue;
				}
				if (Operators.CompareString(Public2.PRJ.TX[i].Location, "Tower", TextCompare: false) == 0)
				{
					if (!Information.IsNothing(Public2.PRJ.BLD))
					{
						int upperBound2 = Public2.PRJ.BLD.GetUpperBound(0);
						for (int j = 0; j <= upperBound2; j++)
						{
							if (Module1.IsPointInsidePolygonF(Public2.PRJ.TX[i].CoordWM, Public2.PRJ.BLD[j].CoordWM))
							{
								Interaction.MsgBox(Localization.LocMSG(71) + ". " + Localization.LocMSG(77) + " " + Conversions.ToString(i + 1) + " " + Localization.LocMSG(78));
								return false;
							}
						}
					}
					TMP[i].H = Public2.PRJ.TX[i].H + Triangul.FindHeightTriang(Public2.PRJ.TX[i].CoordWM.Xwm, Public2.PRJ.TX[i].CoordWM.Ywm);
				}
				else if (Operators.CompareString(Public2.PRJ.TX[i].Location, "Roof", TextCompare: false) == 0)
				{
					if (Information.IsNothing(Public2.PRJ.BLD))
					{
						Interaction.MsgBox(Localization.LocMSG(71) + ". " + Localization.LocMSG(77) + " " + Conversions.ToString(i + 1) + " " + Localization.LocMSG(79));
						return false;
					}
					bool flag = false;
					int upperBound3 = Public2.PRJ.BLD.GetUpperBound(0);
					for (int k = 0; k <= upperBound3; k++)
					{
						if (Module1.IsPointInsidePolygonF(Public2.PRJ.TX[i].CoordWM, Public2.PRJ.BLD[k].CoordWM))
						{
							if (Public2.PRJ.TX[i].H > Public2.PRJ.BLD[k].H)
							{
								TMP[i].H = Public2.PRJ.TX[i].H + Public2.PRJ.BLD[k].Fundation;
								flag = true;
								break;
							}
							Interaction.MsgBox(Localization.LocMSG(71) + ". " + Localization.LocMSG(77) + " " + Conversions.ToString(i + 1) + " " + Localization.LocMSG(80));
							return false;
						}
					}
					if (!flag)
					{
						Interaction.MsgBox(Localization.LocMSG(71) + ". " + Localization.LocMSG(77) + " " + Conversions.ToString(i + 1) + " " + Localization.LocMSG(81));
						return false;
					}
				}
				else if (Operators.CompareString(Public2.PRJ.TX[i].Location, "Facade", TextCompare: false) == 0)
				{
					if (Information.IsNothing(Public2.PRJ.BLD))
					{
						Interaction.MsgBox(Localization.LocMSG(71) + ". " + Localization.LocMSG(77) + " " + Conversions.ToString(i + 1) + " " + Localization.LocMSG(82));
						return false;
					}
					double d = 2.0 / CalcCoords.CalcMashtWMtoMeters();
					bool flag2 = false;
					int upperBound4 = Public2.PRJ.BLD.GetUpperBound(0);
					for (int l = 0; l <= upperBound4; l++)
					{
						int num = Public2.PRJ.BLD[l].CoordWM.GetUpperBound(0) - 1;
						for (int m = 0; m <= num; m++)
						{
							if (Module1.PointLine(Public2.PRJ.BLD[l].CoordWM[m], Public2.PRJ.BLD[l].CoordWM[m + 1], Public2.PRJ.TX[i].CoordWM, d))
							{
								TMP[i].H = Public2.PRJ.TX[i].H + Public2.PRJ.BLD[l].Fundation;
								flag2 = true;
								break;
							}
						}
					}
					if (!flag2)
					{
						Interaction.MsgBox(Localization.LocMSG(71) + ". " + Localization.LocMSG(77) + " " + Conversions.ToString(i + 1) + " " + Localization.LocMSG(83));
						return false;
					}
				}
				TMP[i].PDU = new float[4];
				TMP[i].PDU[0] = 1000000f;
				TMP[i].PDU[1] = 1000000f;
				TMP[i].PDU[2] = 1000000f;
				TMP[i].PDU[3] = 1000000f;
				if (Public2.PRJ.SETT.LMT.Count > 0)
				{
					int num2 = 0;
					do
					{
						bool flag3 = false;
						List<LimPoint> list = new List<LimPoint>();
						int num3 = Public2.PRJ.SETT.LMT.Count - 1;
						for (int n = 0; n <= num3; n++)
						{
							if ((Public2.PRJ.SETT.LMT[n].Freq == -999f) | (Public2.PRJ.SETT.LMT[n].E[num2] == -999f) | (Public2.PRJ.SETT.LMT[n].A[num2] == -999f))
							{
								flag3 = true;
							}
							if (!flag3)
							{
								list.Add(new LimPoint(Public2.PRJ.SETT.LMT[n].Freq, Public2.PRJ.SETT.LMT[n].E[num2], Public2.PRJ.SETT.LMT[n].A[num2]));
							}
						}
						if (!flag3)
						{
							if (list.First().F > 10f)
							{
								list.Insert(0, new LimPoint(10f, 0f, 0f));
							}
							int num4 = list.Count - 2;
							for (int num5 = 0; num5 <= num4; num5++)
							{
								if ((Public2.PRJ.TX[i].F > list[num5].F) & (Public2.PRJ.TX[i].F <= list[num5 + 1].F))
								{
									float num6 = (float)((double)list[num5 + 1].E * Math.Pow(Public2.PRJ.TX[i].F, list[num5 + 1].A));
									TMP[i].PDU[num2] = (float)((double)(num6 * num6) / 3.77);
									break;
								}
							}
						}
						num2++;
					}
					while (num2 <= 3);
				}
				if (Public2.PRJ.TX[i].Az >= 0f)
				{
					TMP[i].Azimut = (float)((double)Public2.PRJ.TX[i].Az * Constants.PI_180);
				}
				else
				{
					TMP[i].Azimut = 0f;
				}
				if (Information.IsNothing(Public2.PRJ.TX[i].DnH))
				{
					Interaction.MsgBox(Localization.LocMSG(71) + ". " + Localization.LocMSG(84) + " " + Conversions.ToString(i + 1));
					return false;
				}
				if (Public2.PRJ.TX[i].DnH.GetUpperBound(0) < 359)
				{
					Interaction.MsgBox(Localization.LocMSG(71) + ". " + Localization.LocMSG(84) + " " + Conversions.ToString(i + 1));
					return false;
				}
				TMP[i].DnH = new float[360];
				TMP[i].DnV = new float[360];
				int num7 = 0;
				do
				{
					TMP[i].DnH[num7] = (float)Math.Pow(10.0, (0f - Public2.PRJ.TX[i].DnH[num7]) / 20f);
					TMP[i].DnV[num7] = (float)Math.Pow(10.0, (0f - Public2.PRJ.TX[i].DnV[num7]) / 20f);
					num7++;
				}
				while (num7 <= 359);
				TMP[i].Lambda = 300f / Public2.PRJ.TX[i].F;
				TMP[i].PROIZV = (float)((double)Public2.PRJ.SETT.KoefOslabl * Math.Sqrt((double)(30f * Public2.PRJ.TX[i].P * (float)Public2.PRJ.TX[i].N) * Math.Pow(10.0, (double)(Public2.PRJ.TX[i].G / 10f) * (Public2.BufSize - 1023.0)) * Math.Pow(10.0, (0f - Public2.PRJ.TX[i].Loss) / 10f)));
				TMP[i].Koef = (float)(Math.Sqrt(2f * TMP[i].Lambda) / (double)Public2.PRJ.TX[i].D);
				TMP[i].Tilt = (float)((double)(0f - Public2.PRJ.TX[i].Ug) * Constants.PI_180);
				if (Public2.PRJ.SETT.UseBuildings == 2)
				{
					if (Public2.PRJ.TX[i].F <= Public2.PRJ.SETT.BuildFreqBorder)
					{
						TMP[i].BuildCustomAtten = (float)Math.Pow(10.0, (0f - Public2.PRJ.SETT.BuildAttenSubband1) / 10f);
					}
					else
					{
						TMP[i].BuildCustomAtten = (float)Math.Pow(10.0, (0f - Public2.PRJ.SETT.BuildAttenSubband2) / 10f);
					}
				}
				TMP[i].AttenuationFactor = new float[4];
				int num8 = 0;
				do
				{
					float num9 = 1f;
					float num10 = 1f;
					float num11 = 1f;
					if (Public2.PRJ.SETT.LimitAlpha24[num8])
					{
						num9 = Public2.PRJ.TX[i].Alpha24[num8];
					}
					if (Public2.PRJ.SETT.LimitFtdc[num8])
					{
						num10 = Public2.PRJ.TX[i].Ftdc[num8];
					}
					if (Public2.PRJ.SETT.LimitFpr[num8])
					{
						num11 = Public2.PRJ.TX[i].Fpr[num8];
					}
					TMP[i].AttenuationFactor[num8] = num9 * num10 * num11;
					num8++;
				}
				while (num8 <= 3);
			}
			return true;
		}
	}

	public static float CalcPPE(short I, float Xt, float Yt, float Zt)
	{
		float num = (float)((double)Xt - Public2.PRJ.TX[I].CoordM.Xm);
		float num2 = (float)((double)Yt - Public2.PRJ.TX[I].CoordM.Ym);
		float num3 = Zt - TMP[I].H;
		float num4 = (float)Math.Sqrt(num * num + num2 * num2);
		float num5 = default;
		if ((num >= 0f) & (num2 > 0f))
		{
			num5 = (float)Math.Atan(num / num2);
		}
		else if ((num > 0f) & (num2 <= 0f))
		{
			num5 = (float)(Math.PI / 2.0 - Math.Atan(num2 / num));
		}
		else if ((num <= 0f) & (num2 < 0f))
		{
			num5 = (float)(Math.PI + Math.Atan(num / num2));
		}
		else if ((num < 0f) & (num2 >= 0f))
		{
			num5 = (float)(4.71238898038469 - Math.Atan(num2 / num));
		}
		float num6 = num5 - TMP[I].Azimut;
		if ((double)num6 >= Math.PI * 2.0)
		{
			num6 = (float)((double)num6 - Math.PI * 2.0);
		}
		if (num6 < 0f)
		{
			num6 = (float)((double)num6 + Math.PI * 2.0);
		}
		float num7 = num6;
		if (num7 >= 0f && num7 <= (float)Math.PI / 2f)
		{
			num = (float)((double)num4 * Math.Sin(num6));
			num2 = (float)((double)num4 * Math.Cos(num6));
		}
		else if (num7 >= (float)Math.PI / 2f && num7 <= (float)Math.PI)
		{
			num6 = (float)((double)num6 - Math.PI / 2.0);
			num = (float)((double)num4 * Math.Cos(num6));
			num2 = (float)((double)(0f - num4) * Math.Sin(num6));
		}
		else if (num7 >= (float)Math.PI && num7 <= 4.712389f)
		{
			num6 = (float)((double)num6 - Math.PI);
			num = (float)((double)(0f - num4) * Math.Sin(num6));
			num2 = (float)((double)(0f - num4) * Math.Cos(num6));
		}
		else if (num7 >= 4.712389f && num7 <= (float)Math.PI * 2f)
		{
			num6 = (float)((double)num6 - 4.71238898038469);
			num = (float)((double)(0f - num4) * Math.Cos(num6));
			num2 = (float)((double)num4 * Math.Sin(num6));
		}
		if (TMP[I].Tilt != 0f)
		{
			if (num2 == 0f)
			{
				num2 = 1E-05f;
			}
			num4 = (float)Math.Sqrt(num2 * num2 + num3 * num3);
			if ((num2 > 0f) & (num3 >= 0f))
			{
				num5 = (float)Math.Atan(num3 / num2);
				num6 = num5 + TMP[I].Tilt;
				float num8 = num6;
				if (num8 >= 0f && num8 <= (float)Math.PI / 2f)
				{
					num2 = (float)((double)num4 * Math.Cos(num6));
					num3 = (float)((double)num4 * Math.Sin(num6));
				}
				else if (num8 < 0f)
				{
					num6 = 0f - num6;
					num2 = (float)((double)num4 * Math.Cos(num6));
					num3 = (float)((double)(0f - num4) * Math.Sin(num6));
				}
				else if (num8 > (float)Math.PI / 2f)
				{
					num6 = (float)(Math.PI - (double)num6);
					num2 = (float)((double)(0f - num4) * Math.Cos(num6));
					num3 = (float)((double)num4 * Math.Sin(num6));
				}
			}
			else if ((num2 > 0f) & (num3 <= 0f))
			{
				num5 = (float)(0.0 - Math.Atan(num3 / num2));
				num6 = num5 - TMP[I].Tilt;
				float num9 = num6;
				if (num9 >= 0f && num9 <= (float)Math.PI / 2f)
				{
					num2 = (float)((double)num4 * Math.Cos(num6));
					num3 = (float)((double)(0f - num4) * Math.Sin(num6));
				}
				else if (num9 < 0f)
				{
					num6 = 0f - num6;
					num2 = (float)((double)num4 * Math.Cos(num6));
					num3 = (float)((double)num4 * Math.Sin(num6));
				}
				else if (num9 > (float)Math.PI / 2f)
				{
					num6 = (float)(Math.PI - (double)num6);
					num2 = (float)((double)(0f - num4) * Math.Cos(num6));
					num3 = (float)((double)(0f - num4) * Math.Sin(num6));
				}
			}
			else if ((num2 < 0f) & (num3 <= 0f))
			{
				num5 = (float)Math.Atan(num3 / num2);
				num6 = num5 + TMP[I].Tilt;
				float num10 = num6;
				if (num10 >= 0f && num10 <= (float)Math.PI / 2f)
				{
					num2 = (float)((double)(0f - num4) * Math.Cos(num6));
					num3 = (float)((double)(0f - num4) * Math.Sin(num6));
				}
				else if (num10 < 0f)
				{
					num6 = 0f - num6;
					num2 = (float)((double)(0f - num4) * Math.Cos(num6));
					num3 = (float)((double)num4 * Math.Sin(num6));
				}
				else if (num10 > (float)Math.PI / 2f)
				{
					num6 = (float)(Math.PI - (double)num6);
					num2 = (float)((double)num4 * Math.Cos(num6));
					num3 = (float)((double)(0f - num4) * Math.Sin(num6));
				}
			}
			else if ((num2 < 0f) & (num3 >= 0f))
			{
				num5 = (float)(0.0 - Math.Atan(num3 / num2));
				num6 = num5 - TMP[I].Tilt;
				float num11 = num6;
				if (num11 >= 0f && num11 <= (float)Math.PI / 2f)
				{
					num2 = (float)((double)(0f - num4) * Math.Cos(num6));
					num3 = (float)((double)num4 * Math.Sin(num6));
				}
				else if (num11 < 0f)
				{
					num6 = 0f - num6;
					num2 = (float)((double)(0f - num4) * Math.Cos(num6));
					num3 = (float)((double)(0f - num4) * Math.Sin(num6));
				}
				else if (num11 > (float)Math.PI / 2f)
				{
					num6 = (float)(Math.PI - (double)num6);
					num2 = (float)((double)num4 * Math.Cos(num6));
					num3 = (float)((double)num4 * Math.Sin(num6));
				}
			}
		}
		float num12 = (float)Math.Sqrt(num * num + num2 * num2);
		float num13 = (float)Math.Sqrt(num12 * num12 + num3 * num3);
		if (num12 == 0f)
		{
			num12 = 1E-08f;
		}
		double num14;
		if ((num >= 0f) & (num2 > 0f))
		{
			num14 = Math.Atan(num / num2);
		}
		else if ((num > 0f) & (num2 <= 0f))
		{
			num14 = Math.PI / 2.0 - Math.Atan(num2 / num);
		}
		else if ((num <= 0f) & (num2 < 0f))
		{
			num14 = Math.PI + Math.Atan(num / num2);
		}
		else
		{
			num14 = ((!((num < 0f) & (num2 >= 0f))) ? 0.0 : (4.71238898038469 - Math.Atan(num2 / num)));
		}
		double num15 = 0.0 - Math.Atan(num3 / num12);
		num15 *= 180.0 / Math.PI;
		num14 *= 180.0 / Math.PI;
		if (num15 < 0.0)
		{
			num15 += 360.0;
		}
		else if (num15 >= 360.0)
		{
			num15 -= 360.0;
		}
		if (num14 < 0.0)
		{
			num14 += 360.0;
		}
		else if (num14 >= 360.0)
		{
			num14 -= 360.0;
		}
		checked
		{
			short num16 = (short)Math.Floor(num15);
			short num17 = (short)(num16 + 1);
			if (num17 >= 360)
			{
				num17 -= 360;
			}
			float num18 = (float)((double)(TMP[I].DnV[num17] - TMP[I].DnV[num16]) * (num15 - (double)num16) + (double)TMP[I].DnV[num16]);
			short num19 = (short)Math.Floor(num14);
			short num20 = (short)(num19 + 1);
			if (num20 >= 360)
			{
				num20 -= 360;
			}
			float num21 = (float)((double)(TMP[I].DnH[num20] - TMP[I].DnH[num19]) * (num14 - (double)num19) + (double)TMP[I].DnH[num19]);
			if (num13 > 0f)
			{
				float num22 = TMP[I].PROIZV * num18 * num21 / num13;
				return (float)((double)(num22 * num22) / 3.77);
			}
			return 999f;
		}
	}

	public static float CalcPointKB(bool InBuilding, float Loss, short IndExclBLD, short IndExclWall, float Xt, float Yt, float Zt, ref float[] Value, ref float PPEsum, ref float Esum)
	{
		bool flag = false;
		checked
		{
			WallBLD[] array = default;
			if (Public2.PRJ.SETT.UseBuildings == 1 && !Information.IsNothing(Public2.PRJ.BLD))
			{
				short num = 0;
				int upperBound = Public2.PRJ.BLD.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					if (!Public2.PRJ.BLD[i].UseAttenuation)
					{
						continue;
					}
					int num2 = Public2.PRJ.BLD[i].CoordWM.GetUpperBound(0) - 1;
					for (int j = 0; j <= num2; j++)
					{
						if (!((Public2.PRJ.BLD[i].CoordWM[j].Xwm > Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm) & (Public2.PRJ.BLD[i].CoordWM[j].Xwm < Public2.PRJ.SETT.HorAreaCoordWM[1].Xwm) & (Public2.PRJ.BLD[i].CoordWM[j].Ywm > Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm) & (Public2.PRJ.BLD[i].CoordWM[j].Ywm < Public2.PRJ.SETT.HorAreaCoordWM[1].Ywm)))
						{
							continue;
						}
						if (IndExclBLD != i)
						{
							array = (WallBLD[])Utils.CopyArray(array, new WallBLD[num + 1]);
							array[num].P1 = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.BLD[i].CoordWM[j]);
							array[num].P2 = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.BLD[i].CoordWM[j + 1]);
							array[num].H = Public2.PRJ.BLD[i].H + Public2.PRJ.BLD[i].Fundation;
							num++;
						}
						else if ((IndExclBLD == i) & (IndExclWall > -1) & (IndExclWall != j))
						{
							if (Zt < Public2.PRJ.BLD[i].H)
							{
								array = (WallBLD[])Utils.CopyArray(array, new WallBLD[num + 1]);
								array[num].P1 = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.BLD[i].CoordWM[j]);
								array[num].P2 = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.BLD[i].CoordWM[j + 1]);
								array[num].H = Public2.PRJ.BLD[i].H + Public2.PRJ.BLD[i].Fundation;
								num++;
							}
							else
							{
								flag = true;
							}
						}
					}
				}
			}
			Value = new float[Public2.PRJ.TX.GetUpperBound(0) + 1];
			float num3 = 0f;
			PPEsum = 0f;
			Esum = 0f;
			int upperBound2 = Public2.PRJ.TX.GetUpperBound(0);
			Public2.CoordMeters p = default;
			Public2.CoordMeters P = default;
			for (int k = 0; k <= upperBound2; k++)
			{
				if (!Public2.PRJ.TX[k].Active)
				{
					continue;
				}
				bool flag2 = false;
				if (!Information.IsNothing(array))
				{
					double num4 = -9999.0;
					p.Xm = Xt;
					p.Ym = Yt;
					int upperBound3 = array.GetUpperBound(0);
					for (int l = 0; l <= upperBound3; l++)
					{
						if (Module1.CalcCrossOtrezM(Public2.PRJ.TX[k].CoordM, p, array[l].P1, array[l].P2, ref P))
						{
							double num5 = Math.Sqrt((P.Xm - Public2.PRJ.TX[k].CoordM.Xm) * (P.Xm - Public2.PRJ.TX[k].CoordM.Xm) + (P.Ym - Public2.PRJ.TX[k].CoordM.Ym) * (P.Ym - Public2.PRJ.TX[k].CoordM.Ym));
							double num6 = (double)(array[l].H - TMP[k].H) / num5;
							if (num6 > num4)
							{
								num4 = num6;
							}
						}
					}
					if (num4 > -9999.0 && Math.Sqrt(((double)Xt - Public2.PRJ.TX[k].CoordM.Xm) * ((double)Xt - Public2.PRJ.TX[k].CoordM.Xm) + ((double)Yt - Public2.PRJ.TX[k].CoordM.Ym) * ((double)Yt - Public2.PRJ.TX[k].CoordM.Ym)) * num4 + (double)TMP[k].H > (double)Zt)
					{
						flag2 = true;
					}
					if ((IndExclWall > -1) & !flag2 & !flag)
					{
						short num7 = -1;
						int num8 = Public2.PRJ.BLD[IndExclBLD].CoordWM.GetUpperBound(0) - 1;
						for (int m = 0; m <= num8; m++)
						{
							Public2.CoordMeters p2 = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.BLD[IndExclBLD].CoordWM[m]);
							Public2.CoordMeters p3 = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.BLD[IndExclBLD].CoordWM[m + 1]);
							if (Module1.CalcCrossOtrezM(Public2.PRJ.TX[k].CoordM, p, p2, p3, ref P))
							{
								double num9 = Math.Sqrt((P.Xm - Public2.PRJ.TX[k].CoordM.Xm) * (P.Xm - Public2.PRJ.TX[k].CoordM.Xm) + (P.Ym - Public2.PRJ.TX[k].CoordM.Ym) * (P.Ym - Public2.PRJ.TX[k].CoordM.Ym));
								double num10 = (double)(Public2.PRJ.BLD[IndExclBLD].H + Public2.PRJ.BLD[IndExclBLD].Fundation - TMP[k].H) / num9;
								double num11 = Math.Sqrt(((double)Xt - Public2.PRJ.TX[k].CoordM.Xm) * ((double)Xt - Public2.PRJ.TX[k].CoordM.Xm) + ((double)Yt - Public2.PRJ.TX[k].CoordM.Ym) * ((double)Yt - Public2.PRJ.TX[k].CoordM.Ym));
								if ((double)(Zt - TMP[k].H) / num11 > num10)
								{
									flag2 = true;
								}
								num7 = (short)m;
								break;
							}
						}
						if ((num7 != IndExclWall) & !flag2)
						{
							flag2 = true;
						}
					}
				}
				if (Public2.PRJ.SETT.UseBuildings == 0)
				{
					Value[k] = CalcPPE((short)k, Xt, Yt, Zt) * TMP[k].AttenuationFactor[Public2.PRJ.SETT.ControlPointLimitIndex];
					if (Loss > 0f)
					{
						Value[k] = (float)((double)Value[k] * Math.Pow(10.0, (0f - Loss) / 10f));
					}
					num3 += Value[k] / TMP[k].PDU[Public2.PRJ.SETT.ControlPointLimitIndex];
					PPEsum += Value[k];
					Value[k] = (float)Math.Sqrt((double)Value[k] * 3.77);
					Esum += Value[k] * Value[k];
				}
				else if (Public2.PRJ.SETT.UseBuildings == 1)
				{
					if (!flag2)
					{
						Value[k] = CalcPPE((short)k, Xt, Yt, Zt) * TMP[k].AttenuationFactor[Public2.PRJ.SETT.ControlPointLimitIndex];
						if (Loss > 0f)
						{
							Value[k] = (float)((double)Value[k] * Math.Pow(10.0, (0f - Loss) / 10f));
						}
						num3 += Value[k] / TMP[k].PDU[Public2.PRJ.SETT.ControlPointLimitIndex];
						PPEsum += Value[k];
						Value[k] = (float)Math.Sqrt((double)Value[k] * 3.77);
						Esum += Value[k] * Value[k];
					}
				}
				else if (Public2.PRJ.SETT.UseBuildings == 2 && !flag2)
				{
					Value[k] = CalcPPE((short)k, Xt, Yt, Zt) * TMP[k].AttenuationFactor[Public2.PRJ.SETT.ControlPointLimitIndex];
					if (InBuilding)
					{
						Value[k] *= TMP[k].BuildCustomAtten;
					}
					if (Loss > 0f)
					{
						Value[k] = (float)((double)Value[k] * Math.Pow(10.0, (0f - Loss) / 10f));
					}
					num3 += Value[k] / TMP[k].PDU[Public2.PRJ.SETT.ControlPointLimitIndex];
					PPEsum += Value[k];
					Value[k] = (float)Math.Sqrt((double)Value[k] * 3.77);
					Esum += Value[k] * Value[k];
				}
			}
			Esum = (float)Math.Sqrt(Esum);
			return num3;
		}
	}

	public static float CalcPointKB2(float Xt, float Yt, float Zt, ref float[] Value, ref float PPEsum, ref float Esum)
	{
		bool flag = false;
		if (Public2.PRJ.SETT.UseBuildings > 0 && !Information.IsNothing(Public2.PRJ.BLD))
		{
			Public2.CoordMeters coordM = default;
			coordM.Xm = Xt;
			coordM.Ym = Yt;
			Public2.CoordWebMerc pNT = CalcCoords.ConvMetersToWebMercMeters(coordM);
			int upperBound = Public2.PRJ.BLD.GetUpperBound(0);
			int num = 0;
			while (num <= upperBound)
			{
				if (!Public2.PRJ.BLD[num].UseAttenuation || !Module1.IsPointInsidePolygonF(pNT, Public2.PRJ.BLD[num].CoordWM) || !(Zt < Public2.PRJ.BLD[num].H + Public2.PRJ.BLD[num].Fundation))
				{
					num = checked(num + 1);
					continue;
				}
				goto IL_00cc;
			}
		}
		goto IL_0150;
		IL_00cc:
		float num2;
		checked
		{
			Value = new float[Public2.PRJ.TX.GetUpperBound(0) + 1];
			int upperBound2 = Public2.PRJ.TX.GetUpperBound(0);
			for (int i = 0; i <= upperBound2; i++)
			{
				Value[i] = 0f;
			}
			PPEsum = 0f;
			Esum = 0f;
			num2 = 0f;
			if (Public2.PRJ.SETT.UseBuildings != 1)
			{
				flag = true;
				goto IL_0150;
			}
			goto IL_0869;
		}
		IL_0150:
		checked
		{
			WallBLD[] array = default;
			if (Public2.PRJ.SETT.UseBuildings == 1 && !Information.IsNothing(Public2.PRJ.BLD))
			{
				short num3 = 0;
				int upperBound3 = Public2.PRJ.BLD.GetUpperBound(0);
				for (int j = 0; j <= upperBound3; j++)
				{
					if (!Public2.PRJ.BLD[j].UseAttenuation)
					{
						continue;
					}
					int num4 = Public2.PRJ.BLD[j].CoordWM.GetUpperBound(0) - 1;
					for (int k = 0; k <= num4; k++)
					{
						if ((Public2.PRJ.BLD[j].CoordWM[k].Xwm > Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm) & (Public2.PRJ.BLD[j].CoordWM[k].Xwm < Public2.PRJ.SETT.HorAreaCoordWM[1].Xwm) & (Public2.PRJ.BLD[j].CoordWM[k].Ywm > Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm) & (Public2.PRJ.BLD[j].CoordWM[k].Ywm < Public2.PRJ.SETT.HorAreaCoordWM[1].Ywm))
						{
							array = (WallBLD[])Utils.CopyArray(array, new WallBLD[num3 + 1]);
							array[num3].P1 = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.BLD[j].CoordWM[k]);
							array[num3].P2 = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.BLD[j].CoordWM[k + 1]);
							array[num3].H = Public2.PRJ.BLD[j].H + Public2.PRJ.BLD[j].Fundation;
							num3++;
						}
					}
				}
			}
			Value = new float[Public2.PRJ.TX.GetUpperBound(0) + 1];
			num2 = 0f;
			PPEsum = 0f;
			Esum = 0f;
			if (Public2.PRJ.SETT.UseBuildings == 1)
			{
				int upperBound4 = Public2.PRJ.TX.GetUpperBound(0);
				Public2.CoordMeters p = default;
				Public2.CoordMeters P = default;
				for (int l = 0; l <= upperBound4; l++)
				{
					if (!Public2.PRJ.TX[l].Active)
					{
						continue;
					}
					bool flag2 = false;
					if (!Information.IsNothing(array))
					{
						double num5 = -9999.0;
						p.Xm = Xt;
						p.Ym = Yt;
						int upperBound5 = array.GetUpperBound(0);
						for (int m = 0; m <= upperBound5; m++)
						{
							if (Module1.CalcCrossOtrezM(Public2.PRJ.TX[l].CoordM, p, array[m].P1, array[m].P2, ref P))
							{
								double num6 = Math.Sqrt((P.Xm - Public2.PRJ.TX[l].CoordM.Xm) * (P.Xm - Public2.PRJ.TX[l].CoordM.Xm) + (P.Ym - Public2.PRJ.TX[l].CoordM.Ym) * (P.Ym - Public2.PRJ.TX[l].CoordM.Ym));
								double num7 = (double)(array[m].H - TMP[l].H) / num6;
								if (num7 > num5)
								{
									num5 = num7;
								}
							}
						}
						if (num5 > -9999.0 && Math.Sqrt(((double)Xt - Public2.PRJ.TX[l].CoordM.Xm) * ((double)Xt - Public2.PRJ.TX[l].CoordM.Xm) + ((double)Yt - Public2.PRJ.TX[l].CoordM.Ym) * ((double)Yt - Public2.PRJ.TX[l].CoordM.Ym)) * num5 + (double)TMP[l].H > (double)Zt)
						{
							flag2 = true;
						}
					}
					if (!flag2)
					{
						Value[l] = CalcPPE((short)l, Xt, Yt, Zt) * TMP[l].AttenuationFactor[Public2.PRJ.SETT.TableGraphLimitIndex];
						num2 += Value[l] / TMP[l].PDU[Public2.PRJ.SETT.TableGraphLimitIndex];
						PPEsum += Value[l];
						Value[l] = (float)Math.Sqrt((double)Value[l] * 3.77);
						Esum += Value[l] * Value[l];
					}
				}
			}
			else
			{
				int upperBound6 = Public2.PRJ.TX.GetUpperBound(0);
				for (int n = 0; n <= upperBound6; n++)
				{
					if (Public2.PRJ.TX[n].Active)
					{
						if (flag)
						{
							Value[n] = CalcPPE((short)n, Xt, Yt, Zt) * TMP[n].BuildCustomAtten * TMP[n].AttenuationFactor[Public2.PRJ.SETT.TableGraphLimitIndex];
							num2 += Value[n] / TMP[n].PDU[Public2.PRJ.SETT.TableGraphLimitIndex];
							PPEsum += Value[n];
							Value[n] = (float)Math.Sqrt((double)Value[n] * 3.77);
							Esum += Value[n] * Value[n];
						}
						else
						{
							Value[n] = CalcPPE((short)n, Xt, Yt, Zt) * TMP[n].AttenuationFactor[Public2.PRJ.SETT.TableGraphLimitIndex];
							num2 += Value[n] / TMP[n].PDU[Public2.PRJ.SETT.TableGraphLimitIndex];
							PPEsum += Value[n];
							Value[n] = (float)Math.Sqrt((double)Value[n] * 3.77);
							Esum += Value[n] * Value[n];
						}
					}
				}
			}
			Esum = (float)Math.Sqrt(Esum);
			goto IL_0869;
		}
		IL_0869:
		return num2;
	}

	public static void CalcVertPlaneEcran(short Index)
	{
		Public2.PRJ.VP[Index].LastH1 = Public2.PRJ.VP[Index].H1;
		Public2.PRJ.VP[Index].LastH2 = Public2.PRJ.VP[Index].H2;
		Public2.PRJ.VP[Index].LastL1 = Public2.PRJ.VP[Index].L1;
		Public2.PRJ.VP[Index].LastL2 = Public2.PRJ.VP[Index].L2;
		Public2.PRJ.VP[Index].Size = Public2.PRJ.VP[Index].Size;
		double num = Math.Sin((double)Public2.PRJ.VP[Index].Azimuth * Constants.PI_180);
		double num2 = Math.Cos((double)Public2.PRJ.VP[Index].Azimuth * Constants.PI_180);
		Public2.PRJ.VP[Index].Coord2M.Xm = Public2.PRJ.VP[Index].CoordM.Xm + (double)Public2.PRJ.VP[Index].L1 * num;
		Public2.PRJ.VP[Index].Coord2M.Ym = Public2.PRJ.VP[Index].CoordM.Ym + (double)Public2.PRJ.VP[Index].L1 * num2;
		Public2.PRJ.VP[Index].Coord1M.Xm = Public2.PRJ.VP[Index].CoordM.Xm + (double)Public2.PRJ.VP[Index].L2 * (0.0 - num);
		Public2.PRJ.VP[Index].Coord1M.Ym = Public2.PRJ.VP[Index].CoordM.Ym + (double)Public2.PRJ.VP[Index].L2 * (0.0 - num2);
		CalcProfile(Index);
		checked
		{
			float[][,] array = new float[4][,]
			{
				new float[Public2.PRJ.VP[Index].Size + 1, Public2.PRJ.VP[Index].Size + 1],
				new float[Public2.PRJ.VP[Index].Size + 1, Public2.PRJ.VP[Index].Size + 1],
				new float[Public2.PRJ.VP[Index].Size + 1, Public2.PRJ.VP[Index].Size + 1],
				new float[Public2.PRJ.VP[Index].Size + 1, Public2.PRJ.VP[Index].Size + 1]
			};
			byte[,] array2 = new byte[Public2.PRJ.VP[Index].Size + 1, Public2.PRJ.VP[Index].Size + 1];
			float num3 = (Public2.PRJ.VP[Index].LastL1 + Public2.PRJ.VP[Index].LastL2) / (float)Public2.PRJ.VP[Index].Size;
			float num4 = (Public2.PRJ.VP[Index].LastH1 + Public2.PRJ.VP[Index].LastH2) / (float)Public2.PRJ.VP[Index].Size;
			Public2.PRJ.VP[Index].PLNs = new Public2.CoordSNG[4][][];
			if (!Information.IsNothing(Public2.PRJ.VP[Index].Elev))
			{
				Public2.PRJ.VP[Index].Elev0 = Public2.PRJ.VP[Index].Elev[(int)Math.Round((float)Public2.PRJ.VP[Index].Elev.GetUpperBound(0) * Public2.PRJ.VP[Index].LastL2 / (Public2.PRJ.VP[Index].LastL1 + Public2.PRJ.VP[Index].LastL2))];
				if (!Information.IsNothing(Public2.PRJ.VP[Index].Bld))
				{
					int upperBound = Public2.PRJ.VP[Index].Bld.GetUpperBound(0);
					for (int i = 0; i <= upperBound; i++)
					{
						if ((Public2.PRJ.VP[Index].LastL2 > Public2.PRJ.VP[Index].Bld[i].L1) & (Public2.PRJ.VP[Index].LastL2 < Public2.PRJ.VP[Index].Bld[i].L2))
						{
							Public2.PRJ.VP[Index].Elev0 = Public2.PRJ.VP[Index].Bld[i].Fundation;
						}
					}
				}
			}
			int upperBound2 = Public2.PRJ.VP[Index].Elev.GetUpperBound(0);
			for (int j = 0; j <= upperBound2; j++)
			{
				float num5 = Public2.PRJ.VP[Index].Elev[j] - Public2.PRJ.VP[Index].Elev0 + Public2.PRJ.VP[Index].LastH2;
				if (num5 > 0f)
				{
					short num6 = (short)Math.Round(num5 / num4);
					if (num6 > array2.GetUpperBound(1))
					{
						num6 = (short)array2.GetUpperBound(0);
					}
					int num7 = num6;
					for (int k = 0; k <= num7; k++)
					{
						array2[j, k] = 1;
					}
				}
			}
			if (Public2.PRJ.SETT.UseBuildings > 0 && !Information.IsNothing(Public2.PRJ.VP[Index].Bld))
			{
				int upperBound3 = Public2.PRJ.VP[Index].Bld.GetUpperBound(0);
				for (int l = 0; l <= upperBound3; l++)
				{
					short num8 = (short)Math.Round(Public2.PRJ.VP[Index].Bld[l].L1 / num3);
					short num9 = (short)Math.Round(Public2.PRJ.VP[Index].Bld[l].L2 / num3);
					float num10 = Public2.PRJ.VP[Index].Bld[l].Fundation + Public2.PRJ.VP[Index].Bld[l].H - Public2.PRJ.VP[Index].Elev0 + Public2.PRJ.VP[Index].LastH2;
					if (!(num10 > 0f))
					{
						continue;
					}
					short num11 = (short)Math.Round(num10 / num4);
					if (num11 > array2.GetUpperBound(1))
					{
						num11 = (short)array2.GetUpperBound(0);
					}
					short num12 = num8;
					short num13 = num9;
					for (short num14 = num12; num14 <= num13; num14 = (short)unchecked(num14 + 1))
					{
						int num15 = num11;
						for (int m = 0; m <= num15; m++)
						{
							if (Public2.PRJ.VP[Index].Bld[l].UseAttenuation)
							{
								array2[num14, m] = 2;
							}
						}
					}
				}
			}
			if (Public2.PRJ.SETT.UseBuildings == 0)
			{
				int upperBound4 = Public2.PRJ.TX.GetUpperBound(0);
				for (int n = 0; n <= upperBound4; n++)
				{
					if (!Public2.PRJ.TX[n].Active)
					{
						continue;
					}
					int size = Public2.PRJ.VP[Index].Size;
					for (int num16 = 0; num16 <= size; num16++)
					{
						float num17 = (float)num16 * num3;
						float xt = (float)(Public2.PRJ.VP[Index].Coord1M.Xm + (double)num17 * num);
						float yt = (float)(Public2.PRJ.VP[Index].Coord1M.Ym + (double)num17 * num2);
						for (int num18 = Public2.PRJ.VP[Index].Size; num18 >= 0; num18 += -1)
						{
							if (array2[num16, num18] == 1)
							{
								continue;
							}
							float zt = (float)num18 * num4 + Public2.PRJ.VP[Index].Elev0 - Public2.PRJ.VP[Index].LastH2;
							int num19 = 0;
							do
							{
								if (Public2.PRJ.SETT.VertCutLimits[num19])
								{
									float num20 = CalcPPE((short)n, xt, yt, zt) * TMP[n].AttenuationFactor[num19];
									array[num19][num16, num18] += num20 / TMP[n].PDU[num19];
								}
								num19++;
							}
							while (num19 <= 3);
						}
					}
				}
			}
			else if (Public2.PRJ.SETT.UseBuildings == 1)
			{
				Public2.StructBuilding[] array3 = default;
				if (!Information.IsNothing(Public2.PRJ.BLD))
				{
					short num21 = 0;
					int upperBound5 = Public2.PRJ.BLD.GetUpperBound(0);
					for (int num22 = 0; num22 <= upperBound5; num22++)
					{
						int num23 = Public2.PRJ.BLD[num22].CoordWM.GetUpperBound(0) - 1;
						for (int num24 = 0; num24 <= num23; num24++)
						{
							if (!((Public2.PRJ.BLD[num22].CoordWM[num24].Xwm > Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm) & (Public2.PRJ.BLD[num22].CoordWM[num24].Xwm < Public2.PRJ.SETT.HorAreaCoordWM[1].Xwm) & (Public2.PRJ.BLD[num22].CoordWM[num24].Ywm > Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm) & (Public2.PRJ.BLD[num22].CoordWM[num24].Ywm < Public2.PRJ.SETT.HorAreaCoordWM[1].Ywm)))
							{
								continue;
							}
							array3 = (Public2.StructBuilding[])Utils.CopyArray(array3, new Public2.StructBuilding[num21 + 1]);
							array3[num21] = Module1.DeepClone(Public2.PRJ.BLD[num22]);
							ref Public2.CoordMeters[] coordM = ref array3[num21].CoordM;
							coordM = (Public2.CoordMeters[])Utils.CopyArray(coordM, new Public2.CoordMeters[array3[num21].CoordWM.GetUpperBound(0) + 1]);
							int upperBound6 = Public2.PRJ.BLD[num22].CoordWM.GetUpperBound(0);
							for (int num25 = 0; num25 <= upperBound6; num25++)
							{
								if (Public2.PRJ.BLD[num22].UseAttenuation)
								{
									array3[num21].CoordM[num25] = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.BLD[num22].CoordWM[num25]);
								}
							}
							num21++;
							break;
						}
					}
				}
				int upperBound7 = Public2.PRJ.TX.GetUpperBound(0);
				Public2.CoordMeters p = default;
				Public2.CoordMeters P = default;
				for (int num26 = 0; num26 <= upperBound7; num26++)
				{
					if (!Public2.PRJ.TX[num26].Active)
					{
						continue;
					}
					int size2 = Public2.PRJ.VP[Index].Size;
					for (int num27 = 0; num27 <= size2; num27++)
					{
						float num28 = (float)num27 * num3;
						float xt = (float)(Public2.PRJ.VP[Index].Coord1M.Xm + (double)num28 * num);
						float yt = (float)(Public2.PRJ.VP[Index].Coord1M.Ym + (double)num28 * num2);
						p.Xm = xt;
						p.Ym = yt;
						double num29 = -9999.0;
						if (!Information.IsNothing(array3))
						{
							int upperBound8 = array3.GetUpperBound(0);
							for (int num30 = 0; num30 <= upperBound8; num30++)
							{
								int num31 = array3[num30].CoordM.GetUpperBound(0) - 1;
								for (int num32 = 0; num32 <= num31; num32++)
								{
									if (Module1.CalcCrossOtrezM(Public2.PRJ.TX[num26].CoordM, p, array3[num30].CoordM[num32], array3[num30].CoordM[num32 + 1], ref P))
									{
										double num33 = P.Xm - Public2.PRJ.TX[num26].CoordM.Xm;
										double num34 = P.Ym - Public2.PRJ.TX[num26].CoordM.Ym;
										double num35 = Math.Sqrt(num33 * num33 + num34 * num34);
										double num36 = (double)(array3[num30].Fundation + array3[num30].H - TMP[num26].H) / num35;
										if (num36 > num29)
										{
											num29 = num36;
										}
									}
								}
							}
						}
						for (int num37 = Public2.PRJ.VP[Index].Size; num37 >= 0; num37 += -1)
						{
							if (array2[num27, num37] != 0)
							{
								continue;
							}
							float zt = (float)num37 * num4 + Public2.PRJ.VP[Index].Elev0 - Public2.PRJ.VP[Index].LastH2;
							double num38 = (double)xt - Public2.PRJ.TX[num26].CoordM.Xm;
							double num39 = (double)yt - Public2.PRJ.TX[num26].CoordM.Ym;
							double num40 = Math.Sqrt(num38 * num38 + num39 * num39);
							if (!((double)(zt - TMP[num26].H) / num40 > num29))
							{
								break;
							}
							int num41 = 0;
							do
							{
								if (Public2.PRJ.SETT.VertCutLimits[num41])
								{
									float num42 = CalcPPE((short)num26, xt, yt, zt) * TMP[num26].AttenuationFactor[num41];
									array[num41][num27, num37] += num42 / TMP[num26].PDU[num41];
								}
								num41++;
							}
							while (num41 <= 3);
						}
					}
				}
			}
			else if (Public2.PRJ.SETT.UseBuildings == 2)
			{
				int upperBound9 = Public2.PRJ.TX.GetUpperBound(0);
				for (int num43 = 0; num43 <= upperBound9; num43++)
				{
					if (!Public2.PRJ.TX[num43].Active)
					{
						continue;
					}
					int size3 = Public2.PRJ.VP[Index].Size;
					for (int num44 = 0; num44 <= size3; num44++)
					{
						float num45 = (float)num44 * num3;
						float xt = (float)(Public2.PRJ.VP[Index].Coord1M.Xm + (double)num45 * num);
						float yt = (float)(Public2.PRJ.VP[Index].Coord1M.Ym + (double)num45 * num2);
						for (int num46 = Public2.PRJ.VP[Index].Size; num46 >= 0; num46 += -1)
						{
							if (array2[num44, num46] == 0)
							{
								float zt = (float)num46 * num4 + Public2.PRJ.VP[Index].Elev0 - Public2.PRJ.VP[Index].LastH2;
								int num47 = 0;
								do
								{
									if (Public2.PRJ.SETT.VertCutLimits[num47])
									{
										float num48 = CalcPPE((short)num43, xt, yt, zt) * TMP[num43].AttenuationFactor[num47];
										array[num47][num44, num46] += num48 / TMP[num43].PDU[num47];
									}
									num47++;
								}
								while (num47 <= 3);
							}
							else
							{
								if (array2[num44, num46] != 2)
								{
									continue;
								}
								float zt = (float)num46 * num4 + Public2.PRJ.VP[Index].Elev0 - Public2.PRJ.VP[Index].LastH2;
								int num49 = 0;
								do
								{
									if (Public2.PRJ.SETT.VertCutLimits[num49])
									{
										float num50 = CalcPPE((short)num43, xt, yt, zt) * TMP[num43].BuildCustomAtten * TMP[num43].AttenuationFactor[num49];
										array[num49][num44, num46] += num50 / TMP[num43].PDU[num49];
									}
									num49++;
								}
								while (num49 <= 3);
							}
						}
					}
				}
			}
			int num51 = 0;
			do
			{
				if (Public2.PRJ.SETT.VertCutLimits[num51])
				{
					DouglasPeuckerMarchingSquares.VectorizationMarchingSquares(array[num51], ref Public2.PRJ.VP[Index].PLNs[num51]);
				}
				num51++;
			}
			while (num51 <= 3);
			if (Information.IsNothing(Public2.PRJ.VP[Index].PLNs))
			{
				return;
			}
			int num52 = 0;
			float y = default;
			do
			{
				if (Public2.PRJ.SETT.VertCutLimits[num52] && !Information.IsNothing(Public2.PRJ.VP[Index].PLNs[num52]))
				{
					float num53 = -1f;
					float num54 = -1f;
					float num55 = 9999f;
					float num56 = 9999f;
					Public2.PRJ.VP[Index].DimensionLowerestZO.R = 9999f;
					double num57 = (Public2.PRJ.VP[Index].LastL1 + Public2.PRJ.VP[Index].LastL2) / (float)Public2.PRJ.VP[Index].Size;
					double num58 = (Public2.PRJ.VP[Index].LastH1 + Public2.PRJ.VP[Index].LastH2) / (float)Public2.PRJ.VP[Index].Size;
					int upperBound10 = Public2.PRJ.VP[Index].PLNs[num52].GetUpperBound(0);
					for (int num59 = 0; num59 <= upperBound10; num59++)
					{
						int upperBound11 = Public2.PRJ.VP[Index].PLNs[num52][num59].GetUpperBound(0);
						for (int num60 = 0; num60 <= upperBound11; num60++)
						{
							if (Public2.PRJ.VP[Index].PLNs[num52][num59][num60].X > num53)
							{
								num53 = Public2.PRJ.VP[Index].PLNs[num52][num59][num60].X;
								y = Public2.PRJ.VP[Index].PLNs[num52][num59][num60].Y;
							}
							if (Public2.PRJ.VP[Index].PLNs[num52][num59][num60].Y < num55)
							{
								num55 = Public2.PRJ.VP[Index].PLNs[num52][num59][num60].Y;
							}
							if (Public2.PRJ.VP[Index].PLNs[num52][num59][num60].Y > num54)
							{
								num54 = Public2.PRJ.VP[Index].PLNs[num52][num59][num60].Y;
							}
							short num61 = (short)Math.Floor(Public2.PRJ.VP[Index].PLNs[num52][num59][num60].X);
							float num62 = (float)((double)(Public2.PRJ.VP[Index].Elev0 - Public2.PRJ.VP[Index].Elev[num61]) + (double)Public2.PRJ.VP[Index].PLNs[num52][num59][num60].Y * num58 - (double)Public2.PRJ.VP[Index].LastH2);
							if (num62 < num56)
							{
								num56 = num62;
								Public2.PRJ.VP[Index].DimensionLowerestZO.R = (float)((double)Public2.PRJ.VP[Index].PLNs[num52][num59][num60].X * num57 - (double)Public2.PRJ.VP[Index].LastL2);
								Public2.PRJ.VP[Index].DimensionLowerestZO.H = (float)((double)Public2.PRJ.VP[Index].PLNs[num52][num59][num60].Y * num58 - (double)Public2.PRJ.VP[Index].LastH2);
								Public2.PRJ.VP[Index].DimensionLowerestZO.Hz = Public2.PRJ.VP[Index].Elev[num61] - Public2.PRJ.VP[Index].Elev0;
								Public2.PRJ.VP[Index].DimensionLowerestZO.Text = Conversions.ToString(Math.Round(num56, 1));
							}
						}
					}
					if ((num53 > -1f) & (num53 < (float)Public2.PRJ.VP[Index].Size))
					{
						Public2.PRJ.VP[Index].LenghtZO = (float)((double)num53 * num57 - (double)Public2.PRJ.VP[Index].LastL2);
						Public2.PRJ.VP[Index].HeightMaxZO = (float)((double)y * num58 - (double)Public2.PRJ.VP[Index].LastH2);
					}
					else
					{
						Public2.PRJ.VP[Index].LenghtZO = 9999f;
						Public2.PRJ.VP[Index].HeightMaxZO = 9999f;
					}
					if ((num55 > -1f) & (num55 < (float)Public2.PRJ.VP[Index].Size))
					{
						Public2.PRJ.VP[Index].HeightBottomZO = (float)((double)num55 * num58 - (double)Public2.PRJ.VP[Index].LastH2);
					}
					else
					{
						Public2.PRJ.VP[Index].HeightBottomZO = 9999f;
					}
					if ((num54 > -1f) & (num54 < (float)Public2.PRJ.VP[Index].Size))
					{
						Public2.PRJ.VP[Index].HeightTopZO = (float)((double)num54 * num58 - (double)Public2.PRJ.VP[Index].LastH2);
					}
					else
					{
						Public2.PRJ.VP[Index].HeightTopZO = 9999f;
					}
				}
				num52++;
			}
			while (num52 <= 3);
		}
	}

	public static void CalcProfile(short Index)
	{
		double num = Math.Sin((double)Public2.PRJ.VP[Index].Azimuth * Constants.PI_180);
		double num2 = Math.Cos((double)Public2.PRJ.VP[Index].Azimuth * Constants.PI_180);
		Public2.CoordMeters coordM = default;
		coordM.Xm = Public2.PRJ.VP[Index].CoordM.Xm + (double)Public2.PRJ.VP[Index].L1 * num;
		coordM.Ym = Public2.PRJ.VP[Index].CoordM.Ym + (double)Public2.PRJ.VP[Index].L1 * num2;
		Public2.CoordMeters coordM2 = default;
		coordM2.Xm = Public2.PRJ.VP[Index].CoordM.Xm + (double)Public2.PRJ.VP[Index].L2 * (0.0 - num);
		coordM2.Ym = Public2.PRJ.VP[Index].CoordM.Ym + (double)Public2.PRJ.VP[Index].L2 * (0.0 - num2);
		Public2.CoordWebMerc pNT = CalcCoords.ConvMetersToWebMercMeters(coordM2);
		Public2.CoordWebMerc pNT2 = CalcCoords.ConvMetersToWebMercMeters(coordM);
		double num3 = CalcCoords.CalcMashtWMtoMeters();
		Public2.PRJ.VP[Index].Bld = null;
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.BLD))
			{
				int upperBound = Public2.PRJ.BLD.GetUpperBound(0);
				Public2.CoordMeters coordMeters = default;
				for (int i = 0; i <= upperBound; i++)
				{
					Public2.StructBuildingProf[] array = null;
					int num4 = Public2.PRJ.BLD[i].CoordWM.GetUpperBound(0) - 1;
					for (int j = 0; j <= num4; j++)
					{
						if (Module1.CalcCrossOtrez(pNT.Xwm, pNT.Ywm, pNT2.Xwm, pNT2.Ywm, Public2.PRJ.BLD[i].CoordWM[j].Xwm, Public2.PRJ.BLD[i].CoordWM[j].Ywm, Public2.PRJ.BLD[i].CoordWM[j + 1].Xwm, Public2.PRJ.BLD[i].CoordWM[j + 1].Ywm, ref coordMeters.Xm, ref coordMeters.Ym))
						{
							array = ((!Information.IsNothing(array)) ? ((Public2.StructBuildingProf[])Utils.CopyArray(array, new Public2.StructBuildingProf[array.GetUpperBound(0) + 1 + 1])) : new Public2.StructBuildingProf[1]);
							array[array.GetUpperBound(0)].Fundation = Public2.PRJ.BLD[i].Fundation;
							array[array.GetUpperBound(0)].UseAttenuation = Public2.PRJ.BLD[i].UseAttenuation;
							array[array.GetUpperBound(0)].H = Public2.PRJ.BLD[i].H;
							array[array.GetUpperBound(0)].Text = Public2.PRJ.BLD[i].Text;
							array[array.GetUpperBound(0)].L1 = (float)(Math.Sqrt(Math.Pow(coordMeters.Xm - pNT.Xwm, 2.0) + Math.Pow(coordMeters.Ym - pNT.Ywm, 2.0)) * num3);
						}
					}
					if (Information.IsNothing(array))
					{
						continue;
					}
					int upperBound2 = array.GetUpperBound(0);
					for (int k = 0; k <= upperBound2; k++)
					{
						int num5 = k + 1;
						int upperBound3 = array.GetUpperBound(0);
						for (int l = num5; l <= upperBound3; l++)
						{
							if (array[l].L1 < array[k].L1)
							{
								Public2.StructBuildingProf obj = Module1.DeepClone(array[k]);
								array[k] = Module1.DeepClone(array[l]);
								array[l] = Module1.DeepClone(obj);
							}
						}
					}
					if (Module1.IsPointInsidePolygonF(pNT, Public2.PRJ.BLD[i].CoordWM))
					{
						array = (Public2.StructBuildingProf[])Utils.CopyArray(array, new Public2.StructBuildingProf[array.GetUpperBound(0) + 1 + 1]);
						for (int m = array.GetUpperBound(0); m >= 1; m += -1)
						{
							array[m] = Module1.DeepClone(array[m - 1]);
						}
						array[0] = Module1.DeepClone(array[1]);
						array[0].L1 = 0f;
					}
					if (Module1.IsPointInsidePolygonF(pNT2, Public2.PRJ.BLD[i].CoordWM))
					{
						array = (Public2.StructBuildingProf[])Utils.CopyArray(array, new Public2.StructBuildingProf[array.GetUpperBound(0) + 1 + 1]);
						array[array.GetUpperBound(0)] = Module1.DeepClone(array[array.GetUpperBound(0) - 1]);
						array[array.GetUpperBound(0)].L1 = Public2.PRJ.VP[Index].L1 + Public2.PRJ.VP[Index].L2;
					}
					int upperBound4 = array.GetUpperBound(0);
					for (int n = 0; n <= upperBound4; n += 2)
					{
						if (Information.IsNothing(Public2.PRJ.VP[Index].Bld))
						{
							Public2.PRJ.VP[Index].Bld = new Public2.StructBuildingProf[1];
						}
						else
						{
							ref Public2.StructBuildingProf[] bld = ref Public2.PRJ.VP[Index].Bld;
							bld = (Public2.StructBuildingProf[])Utils.CopyArray(bld, new Public2.StructBuildingProf[Public2.PRJ.VP[Index].Bld.GetUpperBound(0) + 1 + 1]);
						}
						Public2.PRJ.VP[Index].Bld[Public2.PRJ.VP[Index].Bld.GetUpperBound(0)].Fundation = array[n].Fundation;
						Public2.PRJ.VP[Index].Bld[Public2.PRJ.VP[Index].Bld.GetUpperBound(0)].UseAttenuation = array[n].UseAttenuation;
						Public2.PRJ.VP[Index].Bld[Public2.PRJ.VP[Index].Bld.GetUpperBound(0)].H = array[n].H;
						Public2.PRJ.VP[Index].Bld[Public2.PRJ.VP[Index].Bld.GetUpperBound(0)].Text = array[n].Text;
						Public2.PRJ.VP[Index].Bld[Public2.PRJ.VP[Index].Bld.GetUpperBound(0)].L1 = array[n].L1;
						Public2.PRJ.VP[Index].Bld[Public2.PRJ.VP[Index].Bld.GetUpperBound(0)].L2 = array[n + 1].L1;
					}
				}
			}
			Public2.PRJ.VP[Index].Elev = new float[Public2.PRJ.VP[Index].Size + 1];
			double num6 = (double)(Public2.PRJ.VP[Index].L1 + Public2.PRJ.VP[Index].L2) / num3 / (double)Public2.PRJ.VP[Index].Elev.GetUpperBound(0);
			int upperBound5 = Public2.PRJ.VP[Index].Elev.GetUpperBound(0);
			for (int num7 = 0; num7 <= upperBound5; num7++)
			{
				double num8 = num6 * (double)num7;
				Public2.PRJ.VP[Index].Elev[num7] = Triangul.FindHeightTriang(pNT.Xwm + num8 * num, pNT.Ywm + num8 * num2);
			}
		}
	}

	public static void CalcAllHorizPlane()
	{
		if (Information.IsNothing(Public2.PRJ.HP))
		{
			return;
		}
		bool flag = false;
		int upperBound = Public2.PRJ.HP.GetUpperBound(0);
		checked
		{
			for (int i = 0; i <= upperBound; i++)
			{
				if (Public2.PRJ.HP[i].Activ)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				Interaction.MsgBox(Localization.LocMSG(71) + ". " + Localization.LocMSG(85));
				return;
			}
			int upperBound2 = Public2.PRJ.HP.GetUpperBound(0);
			for (int j = 1; j <= upperBound2; j++)
			{
				if (Public2.PRJ.HP[j].Height <= Public2.PRJ.HP[j - 1].Height)
				{
					Interaction.MsgBox(Localization.LocMSG(71) + ". " + Localization.LocMSG(86));
					return;
				}
			}
			Public2.PRJ.SETT.FlagLastRezManyLimits = GetFlagManyHorizCutLimits();
			short num;
			short num2;
			if (Public2.PRJ.SETT.HorAreaCoordM[1].Xm - Public2.PRJ.SETT.HorAreaCoordM[0].Xm > Public2.PRJ.SETT.HorAreaCoordM[1].Ym - Public2.PRJ.SETT.HorAreaCoordM[0].Ym)
			{
				num = Public2.PRJ.SETT.HorSize;
				num2 = (short)Math.Round((double)Public2.PRJ.SETT.HorSize * (Public2.PRJ.SETT.HorAreaCoordM[1].Ym - Public2.PRJ.SETT.HorAreaCoordM[0].Ym) / (Public2.PRJ.SETT.HorAreaCoordM[1].Xm - Public2.PRJ.SETT.HorAreaCoordM[0].Xm));
			}
			else
			{
				num = (short)Math.Round((double)Public2.PRJ.SETT.HorSize * (Public2.PRJ.SETT.HorAreaCoordM[1].Xm - Public2.PRJ.SETT.HorAreaCoordM[0].Xm) / (Public2.PRJ.SETT.HorAreaCoordM[1].Ym - Public2.PRJ.SETT.HorAreaCoordM[0].Ym));
				num2 = Public2.PRJ.SETT.HorSize;
			}
			hLR = new float[Public2.PRJ.TX.GetUpperBound(0) + 1][,];
			int num3 = 0;
			int upperBound3 = Public2.PRJ.TX.GetUpperBound(0);
			for (int k = 0; k <= upperBound3; k++)
			{
				if (Public2.PRJ.TX[k].Active)
				{
					hLR[k] = new float[num + 1, num2 + 1];
					ListTX = (int[])Utils.CopyArray(ListTX, new int[num3 + 1]);
					ListTX[num3] = k;
					num3++;
				}
			}
			MyProject.Forms.frmBckGrWrkHorizEkr.ShowDialog();
			if (FlagStopCalc)
			{
				int upperBound4 = Public2.PRJ.HP.GetUpperBound(0);
				for (int l = 0; l <= upperBound4; l++)
				{
					Public2.PRJ.HP[l].PLNwm = null;
				}
				return;
			}
			Public2.PRJ.HP_PLNwm = new Public2.CoordWebMerc[4][][];
			if (Public2.PRJ.SETT.FlagLastRezManyLimits)
			{
				hKB = new float[4][][,];
				hKB[0] = new float[Public2.PRJ.HP.GetUpperBound(0) + 1][,];
				hKB[1] = new float[Public2.PRJ.HP.GetUpperBound(0) + 1][,];
				hKB[2] = new float[Public2.PRJ.HP.GetUpperBound(0) + 1][,];
				hKB[3] = new float[Public2.PRJ.HP.GetUpperBound(0) + 1][,];
				num3 = 0;
				int upperBound5 = Public2.PRJ.HP.GetUpperBound(0);
				for (int m = 0; m <= upperBound5; m++)
				{
					Public2.PRJ.HP[m].PLNwm = null;
					hKB[0][m] = new float[num + 1, num2 + 1];
					hKB[1][m] = new float[num + 1, num2 + 1];
					hKB[2][m] = new float[num + 1, num2 + 1];
					hKB[3][m] = new float[num + 1, num2 + 1];
					if (Public2.PRJ.HP[m].Activ)
					{
						ListHVP = (int[])Utils.CopyArray(ListHVP, new int[num3 + 1]);
						ListHVP[num3] = m;
						num3++;
					}
				}
			}
			else
			{
				hKB = new float[1][][,];
				hKB[0] = new float[Public2.PRJ.HP.GetUpperBound(0) + 1][,];
				num3 = 0;
				int upperBound6 = Public2.PRJ.HP.GetUpperBound(0);
				for (int n = 0; n <= upperBound6; n++)
				{
					Public2.PRJ.HP[n].PLNwm = null;
					if (Public2.PRJ.HP[n].Activ)
					{
						hKB[0][n] = new float[num + 1, num2 + 1];
						ListHVP = (int[])Utils.CopyArray(ListHVP, new int[num3 + 1]);
						ListHVP[num3] = n;
						num3++;
					}
				}
			}
			MyProject.Forms.frmBckGrWrkHoriz.ShowDialog();
			if (FlagStopCalc)
			{
				int upperBound7 = Public2.PRJ.HP.GetUpperBound(0);
				for (int num4 = 0; num4 <= upperBound7; num4++)
				{
				}
				return;
			}
			if (Public2.PRJ.SETT.FlagLastRezManyLimits)
			{
				num3 = 0;
				do
				{
					if (Public2.PRJ.SETT.HorizCutLimits[num3])
					{
						int upperBound8 = Public2.PRJ.HP.GetUpperBound(0);
						for (int num5 = 0; num5 <= upperBound8; num5++)
						{
							if (!Public2.PRJ.HP[num5].Activ)
							{
								continue;
							}
							short num6 = (short)hKB[0][num5].GetUpperBound(0);
							for (short num7 = 0; num7 <= num6; num7 = (short)unchecked(num7 + 1))
							{
								short num8 = (short)hKB[0][num5].GetUpperBound(1);
								for (short num9 = 0; num9 <= num8; num9 = (short)unchecked(num9 + 1))
								{
									if (hKB[num3][num5][num7, num9] > hKB[num3][0][num7, num9])
									{
										hKB[num3][0][num7, num9] = hKB[num3][num5][num7, num9];
									}
								}
							}
						}
					}
					num3++;
				}
				while (num3 <= 3);
			}
			else
			{
				int num10 = -1;
				int num11 = -1;
				if (Public2.PRJ.SETT.CombineHorPlanesByColor)
				{
					int upperBound9 = Public2.PRJ.HP.GetUpperBound(0);
					for (int num12 = 0; num12 <= upperBound9; num12++)
					{
						Public2.PRJ.HP[num12].FlagCombine = false;
						if (!Public2.PRJ.HP[num12].Activ)
						{
							continue;
						}
						if (num10 >= 0)
						{
							if ((Public2.PRJ.HP[num12].Color == Public2.PRJ.HP[num10].Color) & (Public2.PRJ.HP[num12].LineType == Public2.PRJ.HP[num10].LineType))
							{
								Public2.PRJ.HP[num12].FlagCombine = true;
								short num13 = (short)hKB[0][num12].GetUpperBound(0);
								for (short num14 = 0; num14 <= num13; num14 = (short)unchecked(num14 + 1))
								{
									short num15 = (short)hKB[0][num12].GetUpperBound(1);
									for (short num16 = 0; num16 <= num15; num16 = (short)unchecked(num16 + 1))
									{
										if (hKB[0][num12][num14, num16] > hKB[0][num11][num14, num16])
										{
											hKB[0][num11][num14, num16] = hKB[0][num12][num14, num16];
										}
									}
								}
							}
							else
							{
								num11 = num12;
							}
						}
						else
						{
							num11 = num12;
						}
						num10 = num12;
					}
				}
				else
				{
					int upperBound10 = Public2.PRJ.HP.GetUpperBound(0);
					for (int num17 = 0; num17 <= upperBound10; num17++)
					{
						Public2.PRJ.HP[num17].FlagCombine = false;
					}
				}
				if (Public2.PRJ.SETT.CombineHorPlanes)
				{
					num10 = -1;
					int upperBound11 = Public2.PRJ.HP.GetUpperBound(0);
					for (int num18 = 0; num18 <= upperBound11; num18++)
					{
						if (!(Public2.PRJ.HP[num18].Activ & !Public2.PRJ.HP[num18].FlagCombine))
						{
							continue;
						}
						if (num10 >= 0)
						{
							short num19 = (short)hKB[0][num18].GetUpperBound(0);
							for (short num20 = 0; num20 <= num19; num20 = (short)unchecked(num20 + 1))
							{
								short num21 = (short)hKB[0][num18].GetUpperBound(1);
								for (short num22 = 0; num22 <= num21; num22 = (short)unchecked(num22 + 1))
								{
									if (hKB[0][num10][num20, num22] > hKB[0][num18][num20, num22])
									{
										hKB[0][num18][num20, num22] = hKB[0][num10][num20, num22];
									}
								}
							}
						}
						num10 = num18;
					}
				}
			}
			num3 = 0;
			if (Public2.PRJ.SETT.FlagLastRezManyLimits)
			{
				int num23 = 0;
				do
				{
					if (Public2.PRJ.SETT.HorizCutLimits[num23])
					{
						ListHVP = (int[])Utils.CopyArray(ListHVP, new int[num3 + 1]);
						ListHVP[num3] = num23;
						num3++;
					}
					num23++;
				}
				while (num23 <= 3);
			}
			else
			{
				int upperBound12 = Public2.PRJ.HP.GetUpperBound(0);
				for (int num24 = 0; num24 <= upperBound12; num24++)
				{
					if (Public2.PRJ.HP[num24].Activ & !Public2.PRJ.HP[num24].FlagCombine)
					{
						ListHVP = (int[])Utils.CopyArray(ListHVP, new int[num3 + 1]);
						ListHVP[num3] = num24;
						num3++;
					}
					else
					{
						Public2.PRJ.HP[num24].PLNwm = null;
					}
				}
			}
			MyProject.Forms.frmBckGrWrkHorizVectorize.ShowDialog();
			_ = FlagStopCalc;
		}
	}

	public static void CalcOneHorizPlane(short Index)
	{
		checked
		{
			bool[,] array = new bool[hKB[0][Index].GetUpperBound(0) + 1, hKB[0][Index].GetUpperBound(1) + 1];
			float num = Public2.PRJ.HP[Index].Height + Module1.GetOriginHeihgt();
			if (!Information.IsNothing(Public2.PRJ.BLD) & (Public2.PRJ.SETT.UseBuildings > 0))
			{
				double num2 = (Public2.PRJ.SETT.HorAreaCoordWM[1].Xwm - Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm) / (double)array.GetUpperBound(0);
				double num3 = (Public2.PRJ.SETT.HorAreaCoordWM[1].Ywm - Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm) / (double)array.GetUpperBound(1);
				int upperBound = Public2.PRJ.BLD.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					if (!Public2.PRJ.BLD[i].UseAttenuation)
					{
						continue;
					}
					int num4 = Public2.PRJ.BLD[i].CoordWM.GetUpperBound(0) - 1;
					for (int j = 0; j <= num4; j++)
					{
						if (!((Public2.PRJ.BLD[i].CoordWM[j].Xwm > Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm) & (Public2.PRJ.BLD[i].CoordWM[j].Xwm < Public2.PRJ.SETT.HorAreaCoordWM[1].Xwm) & (Public2.PRJ.BLD[i].CoordWM[j].Ywm > Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm) & (Public2.PRJ.BLD[i].CoordWM[j].Ywm < Public2.PRJ.SETT.HorAreaCoordWM[1].Ywm)))
						{
							continue;
						}
						int[] array2 = new int[Public2.PRJ.BLD[i].CoordWM.GetUpperBound(0) + 1];
						int[] array3 = new int[Public2.PRJ.BLD[i].CoordWM.GetUpperBound(0) + 1];
						int num5 = 99999;
						int num6 = -99999;
						int num7 = 99999;
						int num8 = -99999;
						int upperBound2 = Public2.PRJ.BLD[i].CoordWM.GetUpperBound(0);
						for (int k = 0; k <= upperBound2; k++)
						{
							array2[k] = (int)Math.Round((Public2.PRJ.BLD[i].CoordWM[k].Xwm - Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm) / num2);
							array3[k] = (int)Math.Round((Public2.PRJ.BLD[i].CoordWM[k].Ywm - Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm) / num3);
							num5 = Math.Min(array2[k], num5);
							num6 = Math.Max(array2[k], num6);
							num7 = Math.Min(array3[k], num7);
							num8 = Math.Max(array3[k], num8);
						}
						int num9 = num5;
						int num10 = num6;
						for (int l = num9; l <= num10; l++)
						{
							int num11 = num7;
							int num12 = num8;
							for (int m = num11; m <= num12; m++)
							{
								if (((l >= 0) & (l <= array.GetUpperBound(0)) & (m >= 0) & (m <= array.GetUpperBound(1))) && Module1.IsPointInsidePolygonI(l, m, array2, array3) && num < Public2.PRJ.BLD[i].Fundation + Public2.PRJ.BLD[i].H)
								{
									array[l, m] = true;
								}
							}
						}
						break;
					}
				}
			}
			byte limitIndexHorizCut = default;
			if (!Public2.PRJ.SETT.FlagLastRezManyLimits)
			{
				limitIndexHorizCut = GetLimitIndexHorizCut();
			}
			float num13 = (float)((Public2.PRJ.SETT.HorAreaCoordM[1].Xm - Public2.PRJ.SETT.HorAreaCoordM[0].Xm) / (double)array.GetUpperBound(0));
			float num14 = (float)((Public2.PRJ.SETT.HorAreaCoordM[1].Ym - Public2.PRJ.SETT.HorAreaCoordM[0].Ym) / (double)array.GetUpperBound(1));
			if (Public2.PRJ.SETT.UseBuildings == 0)
			{
				short num15 = (short)array.GetUpperBound(0);
				for (short num16 = 0; num16 <= num15; num16 = (short)unchecked(num16 + 1))
				{
					float xt = (float)(Public2.PRJ.SETT.HorAreaCoordM[0].Xm + (double)((float)num16 * num13));
					short num17 = (short)array.GetUpperBound(1);
					for (short num18 = 0; num18 <= num17; num18 = (short)unchecked(num18 + 1))
					{
						float yt = (float)(Public2.PRJ.SETT.HorAreaCoordM[0].Ym + (double)((float)num18 * num14));
						int upperBound3 = Public2.PRJ.TX.GetUpperBound(0);
						for (int n = 0; n <= upperBound3; n++)
						{
							if (!Public2.PRJ.TX[n].Active)
							{
								continue;
							}
							if (Public2.PRJ.SETT.FlagLastRezManyLimits)
							{
								int num19 = 0;
								do
								{
									if (Public2.PRJ.SETT.HorizCutLimits[num19])
									{
										float num20 = CalcPPE((short)n, xt, yt, num) * TMP[n].AttenuationFactor[num19];
										hKB[num19][Index][num16, num18] += num20 / TMP[n].PDU[num19];
									}
									num19++;
								}
								while (num19 <= 3);
							}
							else
							{
								float num21 = CalcPPE((short)n, xt, yt, num) * TMP[n].AttenuationFactor[limitIndexHorizCut];
								hKB[0][Index][num16, num18] += num21 / TMP[n].PDU[limitIndexHorizCut];
							}
						}
					}
				}
			}
			else if (Public2.PRJ.SETT.UseBuildings == 1)
			{
				short num22 = (short)array.GetUpperBound(0);
				for (short num23 = 0; num23 <= num22; num23 = (short)unchecked(num23 + 1))
				{
					float xt = (float)(Public2.PRJ.SETT.HorAreaCoordM[0].Xm + (double)((float)num23 * num13));
					short num24 = (short)array.GetUpperBound(1);
					for (short num25 = 0; num25 <= num24; num25 = (short)unchecked(num25 + 1))
					{
						float yt = (float)(Public2.PRJ.SETT.HorAreaCoordM[0].Ym + (double)((float)num25 * num14));
						if (!array[num23, num25])
						{
							int upperBound4 = Public2.PRJ.TX.GetUpperBound(0);
							for (int num26 = 0; num26 <= upperBound4; num26++)
							{
								if (!Public2.PRJ.TX[num26].Active || !(num > hLR[num26][num23, num25]))
								{
									continue;
								}
								if (Public2.PRJ.SETT.FlagLastRezManyLimits)
								{
									int num27 = 0;
									do
									{
										float num28 = CalcPPE((short)num26, xt, yt, num) * TMP[num26].AttenuationFactor[num27];
										if (Public2.PRJ.SETT.HorizCutLimits[num27])
										{
											hKB[num27][Index][num23, num25] += num28 / TMP[num26].PDU[num27];
										}
										num27++;
									}
									while (num27 <= 3);
								}
								else
								{
									float num29 = CalcPPE((short)num26, xt, yt, num) * TMP[num26].AttenuationFactor[limitIndexHorizCut];
									hKB[0][Index][num23, num25] += num29 / TMP[num26].PDU[limitIndexHorizCut];
								}
							}
						}
					}
				}
			}
			else
			{
				if (Public2.PRJ.SETT.UseBuildings != 2)
				{
					return;
				}
				short num30 = (short)array.GetUpperBound(0);
				for (short num31 = 0; num31 <= num30; num31 = (short)unchecked(num31 + 1))
				{
					float xt = (float)(Public2.PRJ.SETT.HorAreaCoordM[0].Xm + (double)((float)num31 * num13));
					short num32 = (short)array.GetUpperBound(1);
					for (short num33 = 0; num33 <= num32; num33 = (short)unchecked(num33 + 1))
					{
						float yt = (float)(Public2.PRJ.SETT.HorAreaCoordM[0].Ym + (double)((float)num33 * num14));
						if (array[num31, num33])
						{
							int upperBound5 = Public2.PRJ.TX.GetUpperBound(0);
							for (int num34 = 0; num34 <= upperBound5; num34++)
							{
								if (!Public2.PRJ.TX[num34].Active || !(num > hLR[num34][num31, num33]))
								{
									continue;
								}
								if (Public2.PRJ.SETT.FlagLastRezManyLimits)
								{
									int num35 = 0;
									do
									{
										if (Public2.PRJ.SETT.HorizCutLimits[num35])
										{
											float num36 = CalcPPE((short)num34, xt, yt, num) * TMP[num34].BuildCustomAtten * TMP[num34].AttenuationFactor[num35];
											hKB[num35][Index][num31, num33] += num36 / TMP[num34].PDU[num35];
										}
										num35++;
									}
									while (num35 <= 3);
								}
								else
								{
									float num37 = CalcPPE((short)num34, xt, yt, num) * TMP[num34].BuildCustomAtten * TMP[num34].AttenuationFactor[limitIndexHorizCut];
									hKB[0][Index][num31, num33] += num37 / TMP[num34].PDU[limitIndexHorizCut];
								}
							}
						}
						else
						{
							int upperBound6 = Public2.PRJ.TX.GetUpperBound(0);
							for (int num38 = 0; num38 <= upperBound6; num38++)
							{
								if (!Public2.PRJ.TX[num38].Active || !(num > hLR[num38][num31, num33]))
								{
									continue;
								}
								if (Public2.PRJ.SETT.FlagLastRezManyLimits)
								{
									int num39 = 0;
									do
									{
										if (Public2.PRJ.SETT.HorizCutLimits[num39])
										{
											float num40 = CalcPPE((short)num38, xt, yt, num) * TMP[num38].AttenuationFactor[num39];
											hKB[num39][Index][num31, num33] += num40 / TMP[num38].PDU[num39];
										}
										num39++;
									}
									while (num39 <= 3);
								}
								else
								{
									float num41 = CalcPPE((short)num38, xt, yt, num) * TMP[num38].AttenuationFactor[limitIndexHorizCut];
									hKB[0][Index][num31, num33] += num41 / TMP[num38].PDU[limitIndexHorizCut];
								}
							}
						}
					}
				}
			}
		}
	}

	public static void CalcOneHorizVictorize(short Index)
	{
		checked
		{
			if (Public2.PRJ.SETT.FlagLastRezManyLimits)
			{
				float num = (float)((Public2.PRJ.SETT.HorAreaCoordWM[1].Xwm - Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm) / (double)hKB[Index][0].GetUpperBound(0));
				float num2 = (float)((Public2.PRJ.SETT.HorAreaCoordWM[1].Ywm - Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm) / (double)hKB[Index][0].GetUpperBound(1));
				Public2.CoordSNG[][] V = default;
				DouglasPeuckerMarchingSquares.VectorizationMarchingSquares(hKB[Index][0], ref V);
				if (!Information.IsNothing(V))
				{
					Public2.PRJ.HP_PLNwm[Index] = new Public2.CoordWebMerc[V.GetUpperBound(0) + 1][];
					int upperBound = V.GetUpperBound(0);
					for (int i = 0; i <= upperBound; i++)
					{
						Public2.PRJ.HP_PLNwm[Index][i] = new Public2.CoordWebMerc[V[i].GetUpperBound(0) + 1];
						int upperBound2 = V[i].GetUpperBound(0);
						for (int j = 0; j <= upperBound2; j++)
						{
							Public2.PRJ.HP_PLNwm[Index][i][j].Xwm = Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm + (double)(V[i][j].X * num);
							Public2.PRJ.HP_PLNwm[Index][i][j].Ywm = Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm + (double)(V[i][j].Y * num2);
						}
					}
				}
				else
				{
					Public2.PRJ.HP[Index].PLNwm = null;
				}
				return;
			}
			float num3 = (float)((Public2.PRJ.SETT.HorAreaCoordWM[1].Xwm - Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm) / (double)hKB[0][Index].GetUpperBound(0));
			float num4 = (float)((Public2.PRJ.SETT.HorAreaCoordWM[1].Ywm - Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm) / (double)hKB[0][Index].GetUpperBound(1));
			Public2.CoordSNG[][] V2 = default;
			DouglasPeuckerMarchingSquares.VectorizationMarchingSquares(hKB[0][Index], ref V2);
			if (!Information.IsNothing(V2))
			{
				Public2.PRJ.HP[Index].PLNwm = new Public2.CoordWebMerc[V2.GetUpperBound(0) + 1][];
				int upperBound3 = V2.GetUpperBound(0);
				for (int k = 0; k <= upperBound3; k++)
				{
					Public2.PRJ.HP[Index].PLNwm[k] = new Public2.CoordWebMerc[V2[k].GetUpperBound(0) + 1];
					int upperBound4 = V2[k].GetUpperBound(0);
					for (int l = 0; l <= upperBound4; l++)
					{
						Public2.PRJ.HP[Index].PLNwm[k][l].Xwm = Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm + (double)(V2[k][l].X * num3);
						Public2.PRJ.HP[Index].PLNwm[k][l].Ywm = Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm + (double)(V2[k][l].Y * num4);
					}
				}
			}
			else
			{
				Public2.PRJ.HP[Index].PLNwm = null;
			}
		}
	}

	public static void FillMatrHLR(short IndexTX)
	{
		if (Public2.PRJ.SETT.UseBuildings != 1)
		{
			return;
		}
		checked
		{
			WallBLD[] array = default;
			if (!Information.IsNothing(Public2.PRJ.BLD))
			{
				short num = 0;
				int upperBound = Public2.PRJ.BLD.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					if (!Public2.PRJ.BLD[i].UseAttenuation)
					{
						continue;
					}
					int num2 = Public2.PRJ.BLD[i].CoordWM.GetUpperBound(0) - 1;
					for (int j = 0; j <= num2; j++)
					{
						if ((Public2.PRJ.BLD[i].CoordWM[j].Xwm > Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm) & (Public2.PRJ.BLD[i].CoordWM[j].Xwm < Public2.PRJ.SETT.HorAreaCoordWM[1].Xwm) & (Public2.PRJ.BLD[i].CoordWM[j].Ywm > Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm) & (Public2.PRJ.BLD[i].CoordWM[j].Ywm < Public2.PRJ.SETT.HorAreaCoordWM[1].Ywm))
						{
							array = (WallBLD[])Utils.CopyArray(array, new WallBLD[num + 1]);
							array[num].P1 = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.BLD[i].CoordWM[j]);
							array[num].P2 = CalcCoords.ConvWebMercMetersToMeters(Public2.PRJ.BLD[i].CoordWM[j + 1]);
							array[num].H = Public2.PRJ.BLD[i].H + Public2.PRJ.BLD[i].Fundation;
							num++;
						}
					}
				}
			}
			if (Information.IsNothing(array))
			{
				return;
			}
			double num3 = (Public2.PRJ.SETT.HorAreaCoordM[1].Xm - Public2.PRJ.SETT.HorAreaCoordM[0].Xm) / (double)hLR[IndexTX].GetUpperBound(0);
			double num4 = (Public2.PRJ.SETT.HorAreaCoordM[1].Ym - Public2.PRJ.SETT.HorAreaCoordM[0].Ym) / (double)hLR[IndexTX].GetUpperBound(1);
			short num5 = (short)hLR[IndexTX].GetUpperBound(0);
			Public2.CoordMeters p = default;
			Public2.CoordMeters P = default;
			for (short num6 = 0; num6 <= num5; num6 = (short)unchecked(num6 + 1))
			{
				p.Xm = Public2.PRJ.SETT.HorAreaCoordM[0].Xm + (double)num6 * num3;
				short num7 = (short)hLR[IndexTX].GetUpperBound(1);
				for (short num8 = 0; num8 <= num7; num8 = (short)unchecked(num8 + 1))
				{
					p.Ym = Public2.PRJ.SETT.HorAreaCoordM[0].Ym + (double)num8 * num4;
					double num9 = -9999.0;
					int upperBound2 = array.GetUpperBound(0);
					for (int k = 0; k <= upperBound2; k++)
					{
						if (Module1.CalcCrossOtrezM(Public2.PRJ.TX[IndexTX].CoordM, p, array[k].P1, array[k].P2, ref P))
						{
							double num10 = Math.Sqrt((P.Xm - Public2.PRJ.TX[IndexTX].CoordM.Xm) * (P.Xm - Public2.PRJ.TX[IndexTX].CoordM.Xm) + (P.Ym - Public2.PRJ.TX[IndexTX].CoordM.Ym) * (P.Ym - Public2.PRJ.TX[IndexTX].CoordM.Ym));
							double num11 = (double)(array[k].H - TMP[IndexTX].H) / num10;
							if (num11 > num9)
							{
								num9 = num11;
							}
						}
					}
					if (num9 > -9999.0)
					{
						double num12 = Math.Sqrt((p.Xm - Public2.PRJ.TX[IndexTX].CoordM.Xm) * (p.Xm - Public2.PRJ.TX[IndexTX].CoordM.Xm) + (p.Ym - Public2.PRJ.TX[IndexTX].CoordM.Ym) * (p.Ym - Public2.PRJ.TX[IndexTX].CoordM.Ym));
						hLR[IndexTX][num6, num8] = (float)(num12 * num9 + (double)TMP[IndexTX].H);
					}
				}
			}
		}
	}

	public static void CalcElevMatr(byte Size)
	{
		float num = (float)Math.Abs(Public2.PRJ.SETT.HorAreaCoordM[1].Xm - Public2.PRJ.SETT.HorAreaCoordM[0].Xm);
		float num2 = (float)Math.Abs(Public2.PRJ.SETT.HorAreaCoordM[1].Ym - Public2.PRJ.SETT.HorAreaCoordM[0].Ym);
		checked
		{
			int num3;
			int num4;
			if (num > num2)
			{
				num3 = Public2.PRJ.SETT.MaxHorSize[Size];
				num4 = (int)Math.Round((float)Public2.PRJ.SETT.MaxHorSize[Size] * num2 / num);
			}
			else
			{
				num3 = (int)Math.Round((float)Public2.PRJ.SETT.MaxHorSize[Size] * num / num2);
				num4 = Public2.PRJ.SETT.MaxHorSize[Size];
			}
			ElevMatr = new float[num3 + 1, num4 + 1];
			BldMatr = new float[num3 + 1, num4 + 1];
			double num5 = (Public2.PRJ.SETT.HorAreaCoordWM[1].Xwm - Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm) / (double)num3;
			double num6 = (Public2.PRJ.SETT.HorAreaCoordWM[1].Ywm - Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm) / (double)num4;
			if (!Information.IsNothing(Triangul.TRG))
			{
				int upperBound = Triangul.TRG.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					int num7 = (int)Math.Round((Triangul.TRG[i].X1 - Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm) / num5);
					int num8 = (int)Math.Round((Triangul.TRG[i].X2 - Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm) / num5);
					int num9 = (int)Math.Round((Triangul.TRG[i].X3 - Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm) / num5);
					int num10 = (int)Math.Round((Triangul.TRG[i].Y1 - Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm) / num6);
					int num11 = (int)Math.Round((Triangul.TRG[i].Y2 - Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm) / num6);
					int num12 = (int)Math.Round((Triangul.TRG[i].Y3 - Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm) / num6);
					int num13 = Math.Min(num7, Math.Min(num8, num9));
					int num14 = Math.Max(num7, Math.Max(num8, num9));
					int num15 = Math.Min(num10, Math.Min(num11, num12));
					int num16 = Math.Max(num10, Math.Max(num11, num12));
					if (!(((num13 >= 0) & (num13 <= num3) & (num15 >= 0) & (num15 <= num4)) | ((num14 >= 0) & (num14 <= num3) & (num16 >= 0) & (num16 <= num4)) | ((num13 >= 0) & (num13 <= num3) & (num16 >= 0) & (num16 <= num4)) | ((num14 >= 0) & (num14 <= num3) & (num15 >= 0) & (num15 <= num4))))
					{
						continue;
					}
					double num17 = (float)(num11 - num10) * (Triangul.TRG[i].H3 - Triangul.TRG[i].H1) - (Triangul.TRG[i].H2 - Triangul.TRG[i].H1) * (float)(num12 - num10);
					double num18 = (float)(num8 - num7) * (Triangul.TRG[i].H3 - Triangul.TRG[i].H1) - (Triangul.TRG[i].H2 - Triangul.TRG[i].H1) * (float)(num9 - num7);
					double num19 = (num8 - num7) * (num12 - num10) - (num11 - num10) * (num9 - num7);
					int num20 = num14;
					for (int j = num13; j <= num20; j++)
					{
						int num21 = num16;
						for (int k = num15; k <= num21; k++)
						{
							if (!((j >= 0) & (j <= num3) & (k >= 0) & (k <= num4)))
							{
								continue;
							}
							double X = j;
							double Y = k;
							bool flag = Module1.IsPointInsideTriangleD(ref X, ref Y, num7, num10, num8, num11, num9, num12);
							k = (int)Math.Round(Y);
							j = (int)Math.Round(X);
							if (flag)
							{
								if (num19 == 0.0)
								{
									ElevMatr[j, k] = Triangul.TRG[i].H1;
								}
								else
								{
									ElevMatr[j, k] = (float)((double)Triangul.TRG[i].H1 - (num17 * (double)(j - num7) - num18 * (double)(k - num10)) / num19);
								}
							}
						}
					}
				}
			}
			if (Information.IsNothing(Public2.PRJ.BLD))
			{
				return;
			}
			int upperBound2 = Public2.PRJ.BLD.GetUpperBound(0);
			for (int l = 0; l <= upperBound2; l++)
			{
				int num22 = Public2.PRJ.BLD[l].CoordWM.GetUpperBound(0) - 1;
				for (int m = 0; m <= num22; m++)
				{
					if (!((Public2.PRJ.BLD[l].CoordWM[m].Xwm > Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm) & (Public2.PRJ.BLD[l].CoordWM[m].Xwm < Public2.PRJ.SETT.HorAreaCoordWM[1].Xwm) & (Public2.PRJ.BLD[l].CoordWM[m].Ywm > Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm) & (Public2.PRJ.BLD[l].CoordWM[m].Ywm < Public2.PRJ.SETT.HorAreaCoordWM[1].Ywm)))
					{
						continue;
					}
					int[] array = new int[Public2.PRJ.BLD[l].CoordWM.GetUpperBound(0) + 1];
					int[] array2 = new int[Public2.PRJ.BLD[l].CoordWM.GetUpperBound(0) + 1];
					int num23 = 99999;
					int num24 = -99999;
					int num25 = 99999;
					int num26 = -99999;
					int upperBound3 = Public2.PRJ.BLD[l].CoordWM.GetUpperBound(0);
					for (int n = 0; n <= upperBound3; n++)
					{
						array[n] = (int)Math.Round((Public2.PRJ.BLD[l].CoordWM[n].Xwm - Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm) / num5);
						array2[n] = (int)Math.Round((Public2.PRJ.BLD[l].CoordWM[n].Ywm - Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm) / num6);
						num23 = Math.Min(array[n], num23);
						num24 = Math.Max(array[n], num24);
						num25 = Math.Min(array2[n], num25);
						num26 = Math.Max(array2[n], num26);
					}
					int num27 = num23;
					int num28 = num24;
					for (int num29 = num27; num29 <= num28; num29++)
					{
						int num30 = num25;
						int num31 = num26;
						for (int num32 = num30; num32 <= num31; num32++)
						{
							if (((num29 >= 0) & (num29 <= num3) & (num32 >= 0) & (num32 <= num4)) && Module1.IsPointInsidePolygonI(num29, num32, array, array2))
							{
								BldMatr[num29, num32] = Public2.PRJ.BLD[l].Fundation + Public2.PRJ.BLD[l].H;
							}
						}
					}
					break;
				}
			}
		}
	}

	public static bool GetFlagManyHorizCutLimits()
	{
		byte b = 0;
		int num = 0;
		checked
		{
			do
			{
				if (Public2.PRJ.SETT.HorizCutLimits[num])
				{
					b++;
				}
				num++;
			}
			while (num <= 3);
			if (b > 1)
			{
				return true;
			}
			return false;
		}
	}

	public static byte GetLimitIndexHorizCut()
	{
		byte result = 0;
		int num = 0;
		checked
		{
			do
			{
				if (Public2.PRJ.SETT.HorizCutLimits[num])
				{
					result = (byte)num;
					break;
				}
				num++;
			}
			while (num <= 3);
			return result;
		}
	}

	public static void Vectorization(float[,] M, ref Public2.CoordSNG[][] V)
	{
		float[,] array = new float[5, 1];
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
					if (((num3 > 1f) & (num4 < 1f) & (num5 < 1f)) | ((num3 < 1f) & (num4 > 1f) & (num5 > 1f)))
					{
						array = (float[,])Utils.CopyArray(array, new float[5, array.GetUpperBound(1) + 1 + 1]);
						array[1, array.GetUpperBound(1)] = i;
						array[2, array.GetUpperBound(1)] = (float)j + (1f - num3) / (num4 - num3);
						array[3, array.GetUpperBound(1)] = (float)i + (1f - num3) / (num5 - num3);
						array[4, array.GetUpperBound(1)] = (float)j + (1f - num3) / (num5 - num3);
					}
					else if (((num4 > 1f) & (num3 < 1f) & (num5 < 1f)) | ((num4 < 1f) & (num3 > 1f) & (num5 > 1f)))
					{
						array = (float[,])Utils.CopyArray(array, new float[5, array.GetUpperBound(1) + 1 + 1]);
						array[1, array.GetUpperBound(1)] = i;
						array[2, array.GetUpperBound(1)] = (float)j + (1f - num3) / (num4 - num3);
						array[3, array.GetUpperBound(1)] = (float)i + (1f - num4) / (num5 - num4);
						array[4, array.GetUpperBound(1)] = j + 1;
					}
					else if (((num5 > 1f) & (num3 < 1f) & (num4 < 1f)) | ((num5 < 1f) & (num3 > 1f) & (num4 > 1f)))
					{
						array = (float[,])Utils.CopyArray(array, new float[5, array.GetUpperBound(1) + 1 + 1]);
						array[1, array.GetUpperBound(1)] = (float)i + (1f - num4) / (num5 - num4);
						array[2, array.GetUpperBound(1)] = j + 1;
						array[3, array.GetUpperBound(1)] = (float)i + (1f - num3) / (num5 - num3);
						array[4, array.GetUpperBound(1)] = (float)j + (1f - num3) / (num5 - num3);
					}
					if (((num3 > 1f) & (num5 < 1f) & (num6 < 1f)) | ((num3 < 1f) & (num5 > 1f) & (num6 > 1f)))
					{
						array = (float[,])Utils.CopyArray(array, new float[5, array.GetUpperBound(1) + 1 + 1]);
						array[1, array.GetUpperBound(1)] = (float)i + (1f - num3) / (num5 - num3);
						array[2, array.GetUpperBound(1)] = (float)j + (1f - num3) / (num5 - num3);
						array[3, array.GetUpperBound(1)] = (float)i + (1f - num3) / (num6 - num3);
						array[4, array.GetUpperBound(1)] = j;
					}
					else if (((num5 > 1f) & (num3 < 1f) & (num6 < 1f)) | ((num5 < 1f) & (num3 > 1f) & (num6 > 1f)))
					{
						array = (float[,])Utils.CopyArray(array, new float[5, array.GetUpperBound(1) + 1 + 1]);
						array[1, array.GetUpperBound(1)] = (float)i + (1f - num3) / (num5 - num3);
						array[2, array.GetUpperBound(1)] = (float)j + (1f - num3) / (num5 - num3);
						array[3, array.GetUpperBound(1)] = i + 1;
						array[4, array.GetUpperBound(1)] = (float)j + (1f - num6) / (num5 - num6);
					}
					else if (((num6 > 1f) & (num3 < 1f) & (num5 < 1f)) | ((num6 < 1f) & (num3 > 1f) & (num5 > 1f)))
					{
						array = (float[,])Utils.CopyArray(array, new float[5, array.GetUpperBound(1) + 1 + 1]);
						array[1, array.GetUpperBound(1)] = (float)i + (1f - num3) / (num6 - num3);
						array[2, array.GetUpperBound(1)] = j;
						array[3, array.GetUpperBound(1)] = i + 1;
						array[4, array.GetUpperBound(1)] = (float)j + (1f - num6) / (num5 - num6);
					}
				}
			}
			float[,][] array2 = new float[0, 0][];
			int num7 = 1;
			while (true)
			{
				int num8 = 0;
				int num9 = 1;
				int upperBound = array.GetUpperBound(1);
				for (num9 = 1; num9 <= upperBound && array[1, num9] == 999f; num9++)
				{
				}
				if (num9 >= array.GetUpperBound(1))
				{
					break;
				}
				array2 = (float[,][])Utils.CopyArray(array2, new float[3, num7 + 1][]);
				array2[1, num7] = new float[0];
				array2[2, num7] = new float[0];
				ref float[] reference = ref array2[1, num7];
				reference = (float[])Utils.CopyArray(reference, new float[3]);
				ref float[] reference2 = ref array2[2, num7];
				reference2 = (float[])Utils.CopyArray(reference2, new float[3]);
				array2[1, num7][1] = array[1, num9];
				array2[2, num7][1] = array[2, num9];
				array2[1, num7][2] = array[3, num9];
				array2[2, num7][2] = array[4, num9];
				array[1, num9] = 999f;
				num8 = 3;
				while (true)
				{
					int upperBound2 = array.GetUpperBound(1);
					num9 = 1;
					while (num9 <= upperBound2)
					{
						if ((array2[1, num7][num8 - 1] == array[1, num9]) & (array2[2, num7][num8 - 1] == array[2, num9]) & (array[1, num9] != 999f))
						{
							goto IL_0690;
						}
						if (!((array2[1, num7][num8 - 1] == array[3, num9]) & (array2[2, num7][num8 - 1] == array[4, num9]) & (array[1, num9] != 999f)))
						{
							num9++;
							continue;
						}
						goto IL_076b;
					}
					break;
					IL_076b:
					ref float[] reference3 = ref array2[1, num7];
					reference3 = (float[])Utils.CopyArray(reference3, new float[num8 + 1]);
					ref float[] reference4 = ref array2[2, num7];
					reference4 = (float[])Utils.CopyArray(reference4, new float[num8 + 1]);
					array2[1, num7][num8] = array[1, num9];
					array2[2, num7][num8] = array[2, num9];
					array[1, num9] = 999f;
					num8++;
					continue;
					IL_0690:
					ref float[] reference5 = ref array2[1, num7];
					reference5 = (float[])Utils.CopyArray(reference5, new float[num8 + 1]);
					ref float[] reference6 = ref array2[2, num7];
					reference6 = (float[])Utils.CopyArray(reference6, new float[num8 + 1]);
					array2[1, num7][num8] = array[3, num9];
					array2[2, num7][num8] = array[4, num9];
					array[1, num9] = 999f;
					num8++;
				}
				if (num8 > 0)
				{
					num7++;
					continue;
				}
				num7--;
				break;
			}
			V = null;
			if (array2.Length <= 0)
			{
				return;
			}
			V = new Public2.CoordSNG[array2.GetUpperBound(1) - 1 + 1][];
			int upperBound3 = array2.GetUpperBound(1);
			for (int k = 1; k <= upperBound3; k++)
			{
				ref Public2.CoordSNG[] reference7 = ref V[k - 1];
				reference7 = (Public2.CoordSNG[])Utils.CopyArray(reference7, new Public2.CoordSNG[array2[1, k].GetUpperBound(0) - 1 + 1]);
				int upperBound4 = array2[1, k].GetUpperBound(0);
				for (int l = 1; l <= upperBound4; l++)
				{
					V[k - 1][l - 1].X = array2[1, k][l];
					V[k - 1][l - 1].Y = array2[2, k][l];
				}
			}
		}
	}

	public static void Thf73()
	{
		try
		{
			Public2.BufSize = Conversions.ToDouble(Enigma_IDE64.EP_ProtectedStringByKeyAsAnsiString("cZugQ6qx"));
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
	}
}
