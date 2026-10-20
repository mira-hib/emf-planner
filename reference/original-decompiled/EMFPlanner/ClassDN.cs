using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[StandardModule]
public sealed class ClassDN
{
	[Serializable]
	public class AntennaPatternEnvelop
	{
		public bool Active;

		public string Name;

		public float GainDBi;

		public List<PatternPoint> HZ;

		public List<PatternPoint> VR;

		public AntennaPatternEnvelop(string _Name, float _GainDBi, List<PatternPoint> _HZ, List<PatternPoint> _VR)
		{
			Active = true;
			Name = _Name;
			GainDBi = _GainDBi;
			HZ = _HZ;
			VR = _VR;
		}
	}

	[Serializable]
	public class AntennaPatternClass
	{
		public AntennaPattern[] GR;

		public bool VertPatternFormat;

		public bool GroupFormat;

		public int GroupeIndex;

		public short SliceIndex;

		public bool Flag3D;

		public string FileFormat;

		public string Name;

		public string GainDBi;

		public string MSI_NAME;

		public string MSI_MAKE;

		public string MSI_FREQUENCY;

		public string MSI_H_WIDTH;

		public string MSI_V_WIDTH;

		public string MSI_FRONT_TO_BACK;

		public string MSI_GAIN;

		public string MSI_TILT;

		public string MSI_POLARIZATION;

		public string MSI_COMMENT;

		public AntennaPatternClass(bool _VertPatternFornmat, bool _GroupFormat)
		{
			GR = new AntennaPattern[1];
			GR = new AntennaPattern[1];
			GR[0] = new AntennaPattern();
			VertPatternFormat = _VertPatternFornmat;
			GroupFormat = _GroupFormat;
			GroupeIndex = 0;
			SliceIndex = 0;
			Flag3D = false;
			FileFormat = "";
		}
	}

	[Serializable]
	public class AntennaPattern
	{
		public string GroupName;

		public List<PatternPoint> HZ;

		public List<PatternPoint> VR;

		public List<PatternSlice> SL;

		public AntennaPattern()
		{
			GroupName = "";
			HZ = new List<PatternPoint>();
			VR = new List<PatternPoint>();
			SL = new List<PatternSlice>();
		}

		public AntennaPattern(string _GroupName)
		{
			GroupName = _GroupName;
			HZ = new List<PatternPoint>();
			VR = new List<PatternPoint>();
			SL = new List<PatternSlice>();
		}

		public float GetHZ(float Phi)
		{
			if (Phi == HZ.First().Angle)
			{
				return HZ.First().G;
			}
			if ((Phi < HZ.First().Angle) | (Phi > HZ.Last().Angle))
			{
				PatternPoint patternPoint = HZ.Last();
				PatternPoint patternPoint2 = HZ.First();
				float num = patternPoint2.Angle - patternPoint.Angle;
				if (num < 0f)
				{
					num += 360f;
				}
				return patternPoint.G + (patternPoint2.G - patternPoint.G) * (Phi - patternPoint.Angle) / num;
			}
			checked
			{
				short num2 = (short)HZ.FindIndex([SpecialName] (PatternPoint pp) => pp.Angle >= Phi);
				PatternPoint patternPoint2 = HZ[num2];
				PatternPoint patternPoint = HZ[num2 - 1];
				return patternPoint.G + (patternPoint2.G - patternPoint.G) * (Phi - patternPoint.Angle) / (patternPoint2.Angle - patternPoint.Angle);
			}
		}

		public float GetHZold(float Phi)
		{
			checked
			{
				short index;
				short num;
				if (Phi <= HZ.First().Angle)
				{
					index = (short)(HZ.Count - 1);
					num = 0;
				}
				else if (Phi >= HZ.Last().Angle)
				{
					index = (short)(HZ.Count - 1);
					num = 0;
				}
				else
				{
					num = (short)HZ.FindIndex([SpecialName] (PatternPoint pp) => pp.Angle >= Phi);
					index = (short)(num - 1);
				}
				return HZ[index].G + (HZ[num].G - HZ[index].G) * (Phi - HZ[index].Angle) / (HZ[num].Angle - HZ[index].Angle);
			}
		}

		public float GetHZsimpl(float Phi)
		{
			if (Phi > HZ[checked(HZ.Count - 1)].Angle)
			{
				return HZ[0].G;
			}
			return HZ.Find([SpecialName] (PatternPoint pp) => pp.Angle >= Phi).G;
		}

		public float GetVR(float Phi, float Theta)
		{
			short index;
			short num;
			float num2;
			float num3;
			checked
			{
				if (Phi <= SL[0].SliceAngle)
				{
					index = (short)(SL.Count - 1);
					num = 0;
				}
				else if (Phi >= SL[SL.Count - 1].SliceAngle)
				{
					index = (short)(SL.Count - 1);
					num = 0;
				}
				else
				{
					num = (short)SL.FindIndex([SpecialName] (PatternSlice pp) => pp.SliceAngle >= Phi);
					index = (short)(num - 1);
				}
				float sliceAngle = SL[index].SliceAngle;
				num2 = SL[num].SliceAngle - sliceAngle;
				if (num2 < 0f)
				{
					num2 += 360f;
				}
				num3 = Phi - sliceAngle;
				if (num3 < 0f)
				{
					num3 += 360f;
				}
			}
			short index2 = (short)((!(Theta >= SL[0].VR[0].Angle)) ? checked((!(Theta <= SL[0].VR[SL[0].VR.Count - 1].Angle)) ? ((short)SL[0].VR.FindIndex([SpecialName] (PatternPoint pp) => pp.Angle <= Theta)) : ((short)(SL[0].VR.Count - 1))) : 0);
			float g = SL[index].VR[index2].G;
			float g2 = SL[num].VR[index2].G;
			return g + (g2 - g) * num3 / num2;
		}

		public float Get3DN(float Phi, float Theta)
		{
			return GetHZsimpl(Phi) + GetVR(Phi, Theta);
		}
	}

	[Serializable]
	public class PatternSlice
	{
		public float SliceAngle;

		public List<PatternPoint> VR;

		public PatternSlice(float Angle)
		{
			SliceAngle = Angle;
			VR = new List<PatternPoint>();
		}
	}

	[Serializable]
	public class PatternPoint
	{
		public float Angle;

		public float G;

		public short X;

		public short Y;

		public PatternPoint(float Angle1, float G1)
		{
			Angle = Angle1;
			G = G1;
		}
	}

	public static AntennaPatternClass LoadAntennaFile(string FileName)
	{
		AntennaPatternClass antennaPatternClass = new AntennaPatternClass(_VertPatternFornmat: false, _GroupFormat: false);
		try
		{
			List<string> list = new List<string>();
			StreamReader streamReader = new StreamReader(FileName, Encoding.GetEncoding(1251));
			if (streamReader.EndOfStream)
			{
				antennaPatternClass = antennaPatternClass;
			}
			else
			{
				while (true)
				{
					if (!streamReader.EndOfStream)
					{
						string text = Strings.Trim(streamReader.ReadLine());
						if (Operators.CompareString(text, "", TextCompare: false) != 0)
						{
							list.Add(text);
						}
						if (list.Count > 30000)
						{
							streamReader.Close();
							antennaPatternClass = antennaPatternClass;
							break;
						}
						continue;
					}
					streamReader.Close();
					foreach (string item in list)
					{
						if (item.Contains("HORIZONTAL") & item.Contains("360"))
						{
							antennaPatternClass = LoadMSI_(list);
							break;
						}
					}
					break;
				}
			}
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			ProjectData.ClearProjectError();
		}
		return antennaPatternClass;
	}

	public static AntennaPatternClass LoadMSI_(List<string> STR)
	{
		AntennaPatternClass antennaPatternClass = new AntennaPatternClass(_VertPatternFornmat: false, _GroupFormat: false);
		antennaPatternClass.FileFormat = "MSI Planet";
		antennaPatternClass.Name = STR[0];
		antennaPatternClass.Name = antennaPatternClass.Name.Replace("NAME", "");
		antennaPatternClass.Name = antennaPatternClass.Name.Replace("\t", "");
		antennaPatternClass.Name = Strings.Trim(antennaPatternClass.Name);
		antennaPatternClass.MSI_NAME = antennaPatternClass.Name;
		foreach (string item in STR)
		{
			if (!item.Contains("GAIN"))
			{
				continue;
			}
			string text = item.Replace("GAIN", "");
			text = text.Replace("(", "");
			text = text.Replace(")", "");
			text = text.Replace("\t", "");
			text = text.ToLower();
			antennaPatternClass.MSI_GAIN = Strings.Trim(text);
			bool flag = false;
			if (text.ToLower().Contains("dbd"))
			{
				flag = true;
			}
			text = text.ToLower().Replace("dbd", "");
			text = text.ToLower().Replace("dbi", "");
			try
			{
				float num = Module1.QSNG(text);
				if (flag)
				{
					num = (float)((double)num + 2.15);
				}
				antennaPatternClass.GainDBi = Conversions.ToString(num);
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				ProjectData.ClearProjectError();
			}
			break;
		}
		foreach (string item2 in STR)
		{
			if (item2.Contains("MAKE"))
			{
				string text2 = item2.Replace("MAKE", "");
				text2 = text2.Replace("\t", "");
				antennaPatternClass.MSI_MAKE = Strings.Trim(text2);
				break;
			}
		}
		foreach (string item3 in STR)
		{
			if (item3.Contains("FREQUENCY"))
			{
				string text3 = item3.Replace("FREQUENCY", "");
				text3 = text3.Replace("\t", "");
				antennaPatternClass.MSI_FREQUENCY = Strings.Trim(text3);
				break;
			}
		}
		foreach (string item4 in STR)
		{
			if (item4.Contains("H_WIDTH"))
			{
				string text4 = item4.Replace("H_WIDTH", "");
				text4 = text4.Replace("\t", "");
				antennaPatternClass.MSI_H_WIDTH = Strings.Trim(text4);
				break;
			}
		}
		foreach (string item5 in STR)
		{
			if (item5.Contains("V_WIDTH"))
			{
				string text5 = item5.Replace("V_WIDTH", "");
				text5 = text5.Replace("\t", "");
				antennaPatternClass.MSI_V_WIDTH = Strings.Trim(text5);
				break;
			}
		}
		foreach (string item6 in STR)
		{
			if (item6.Contains("FRONT_TO_BACK"))
			{
				string text6 = item6.Replace("FRONT_TO_BACK", "");
				text6 = text6.Replace("\t", "");
				antennaPatternClass.MSI_FRONT_TO_BACK = Strings.Trim(text6);
				break;
			}
		}
		foreach (string item7 in STR)
		{
			if (item7.Contains("TILT"))
			{
				string text7 = item7.Replace("TILT", "");
				text7 = text7.Replace("\t", "");
				antennaPatternClass.MSI_TILT = Strings.Trim(text7);
				break;
			}
		}
		foreach (string item8 in STR)
		{
			if (item8.Contains("POLARIZATION"))
			{
				string text8 = item8.Replace("POLARIZATION", "");
				text8 = text8.Replace("\t", "");
				antennaPatternClass.MSI_POLARIZATION = Strings.Trim(text8);
				break;
			}
		}
		foreach (string item9 in STR)
		{
			if (item9.Contains("COMMENT"))
			{
				string text9 = item9.Replace("COMMENT", "");
				text9 = text9.Replace("\t", "");
				antennaPatternClass.MSI_COMMENT = Strings.Trim(text9);
				break;
			}
		}
		short num2 = 0;
		checked
		{
			foreach (string item10 in STR)
			{
				num2++;
				if (item10.ToUpper().Contains("HORIZONTAL") & item10.Contains("360"))
				{
					break;
				}
			}
			antennaPatternClass.GR[0].HZ.Clear();
			while (true)
			{
				if (!STR[num2].ToUpper().Contains("VERTICAL"))
				{
					string[] array = STR[num2].Split(new string[5] { ",", ":", ";", " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
					if (array.Count() != 2)
					{
						break;
					}
					string sTR = array[0];
					string sTR2 = array[1];
					antennaPatternClass.GR[0].HZ.Add(new PatternPoint(Module1.QSNG(sTR), 0f - Math.Abs(Module1.QSNG(sTR2))));
					num2++;
					continue;
				}
				num2++;
				antennaPatternClass.GR[0].VR.Clear();
				do
				{
					string[] array2 = STR[num2].Split(new string[5] { ",", ":", ";", " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
					if (array2.Count() != 2)
					{
						break;
					}
					string sTR3 = array2[0];
					string sTR4 = array2[1];
					antennaPatternClass.GR[0].VR.Add(new PatternPoint(Module1.QSNG(sTR3), 0f - Math.Abs(Module1.QSNG(sTR4))));
					num2++;
				}
				while (num2 != STR.Count);
				break;
			}
			return antennaPatternClass;
		}
	}

	public static float GetDnValue(List<PatternPoint> DN, float Phi)
	{
		checked
		{
			short index2;
			short index;
			if (Phi <= DN[0].Angle)
			{
				index = (short)(DN.Count - 1);
				index2 = 0;
				return DN[index].G + (DN[index2].G - DN[index].G) * (Phi - DN[index].Angle + 360f) / (DN[index2].Angle - DN[index].Angle + 360f);
			}
			if (Phi >= DN[DN.Count - 1].Angle)
			{
				index = (short)(DN.Count - 1);
				index2 = 0;
				return DN[index].G + (DN[index2].G - DN[index].G) * (Phi - DN[index].Angle) / (DN[index2].Angle - DN[index].Angle + 360f);
			}
			index2 = (short)DN.FindIndex([SpecialName] (PatternPoint pp) => pp.Angle >= Phi);
			index = (short)(index2 - 1);
			return DN[index].G + (DN[index2].G - DN[index].G) * (Phi - DN[index].Angle) / (DN[index2].Angle - DN[index].Angle);
		}
	}

	public static string GetPatternzWidth(List<PatternPoint> DN)
	{
		if (DN == null)
		{
			return "";
		}
		if (DN.Count < 4)
		{
			return "";
		}
		float g = DN.First().G;
		float angle = DN.First().Angle;
		checked
		{
			int num = DN.Count - 1;
			for (int i = 0; i <= num; i++)
			{
				if (DN[i].G > g)
				{
					g = DN[i].G;
					angle = DN[i].Angle;
				}
			}
			float num2 = g - 3f;
			float num3 = 0f;
			if ((DN.Last().G >= num2) & (DN.First().G <= num2))
			{
				float num4 = DN.Last().Angle - DN.First().Angle;
				if (num4 > 180f)
				{
					num4 = 360f - num4;
				}
				num3 = DN.First().Angle + (num2 - DN.First().G) / (DN.Last().G - DN.First().G) * num4;
				if (num3 >= 360f)
				{
					num3 -= 360f;
				}
			}
			else
			{
				int num5 = DN.Count - 2;
				for (int j = 0; j <= num5; j++)
				{
					if ((DN[j].G >= num2) & (DN[j + 1].G <= num2))
					{
						num3 = DN[j].Angle + (num2 - DN[j].G) / (DN[j + 1].G - DN[j].G) * (DN[j + 1].Angle - DN[j].Angle);
						break;
					}
				}
			}
			float num6 = 0f;
			if ((DN.Last().G <= num2) & (DN.First().G >= num2))
			{
				float num7 = DN.First().Angle - DN.Last().Angle;
				if (num7 < 0f)
				{
					num7 += 360f;
				}
				num6 = DN.Last().Angle + (num2 - DN.Last().G) / (DN.First().G - DN.Last().G) * num7;
				if (num6 >= 360f)
				{
					num6 -= 360f;
				}
			}
			else
			{
				for (int k = DN.Count - 1; k >= 1; k += -1)
				{
					if ((DN[k].G >= num2) & (DN[k - 1].G <= num2))
					{
						num6 = DN[k].Angle + (num2 - DN[k].G) / (DN[k - 1].G - DN[k].G) * (DN[k - 1].Angle - DN[k].Angle);
						break;
					}
				}
			}
			float num8 = num3;
			num8 = ((!((num3 < 180f) & (num6 > 180f))) ? Math.Abs(num3 - num6) : (num3 + (360f - num6)));
			if ((num8 > 0f) & (num8 < 340f))
			{
				return "bw:" + Conversions.ToString(Math.Round(num8, 1)) + "°;max:" + Conversions.ToString(Math.Round(angle, 1)) + "°";
			}
			return "bw:360°;max:" + Conversions.ToString(Math.Round(angle, 1)) + "°";
		}
	}

	public static void Interpol1deg360points(ref List<PatternPoint> DN)
	{
		List<PatternPoint> list = Module1.DeepClone(DN);
		foreach (PatternPoint item in list)
		{
			if (item.Angle > 180f)
			{
				item.Angle -= 360f;
			}
		}
		list.Sort([SpecialName] (PatternPoint x1, PatternPoint y1) => x1.Angle.CompareTo(y1.Angle));
		List<PatternPoint> list2 = new List<PatternPoint>();
		int num = -179;
		do
		{
			list2.Add(new PatternPoint(num, GetDnValue(list, num)));
			num = checked(num + 1);
		}
		while (num <= 180);
		foreach (PatternPoint item2 in list2)
		{
			if (item2.Angle < 0f)
			{
				item2.Angle += 360f;
			}
		}
		list2.Sort([SpecialName] (PatternPoint x1, PatternPoint y1) => x1.Angle.CompareTo(y1.Angle));
		DN = Module1.DeepClone(list2);
	}

	public static void PatternNormalize(ref List<PatternPoint> DN)
	{
		float g = DN.First().G;
		foreach (PatternPoint item in DN)
		{
			if (item.G > g)
			{
				g = item.G;
			}
		}
		if (g == 0f)
		{
			return;
		}
		foreach (PatternPoint item2 in DN)
		{
			item2.G -= g;
		}
	}

	public static double RoundUp(double dblNumToRound, long lMultiple)
	{
		object obj = decimal.Divide(new decimal(dblNumToRound), new decimal(lMultiple));
		object left = RuntimeHelpers.GetObjectValue(Conversion.Int(RuntimeHelpers.GetObjectValue(obj)));
		if (Operators.ConditionalCompareObjectNotEqual(left, obj, TextCompare: false))
		{
			left = Operators.AddObject(left, 1);
		}
		return Conversions.ToDouble(Operators.MultiplyObject(left, lMultiple));
	}

	public static double RoundDown(double dblNumToRound, long lMultiple)
	{
		return Conversions.ToDouble(Operators.MultiplyObject(RuntimeHelpers.GetObjectValue(Conversion.Int(RuntimeHelpers.GetObjectValue(decimal.Divide(new decimal(dblNumToRound), new decimal(lMultiple))))), lMultiple));
	}
}
