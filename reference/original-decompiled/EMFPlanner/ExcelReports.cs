using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[StandardModule]
internal sealed class ExcelReports
{
	public static void ExcelExportPRTO1()
	{
		short num = 0;
		int upperBound = Public2.PRJ.TX.GetUpperBound(0);
		checked
		{
			for (int i = 0; i <= upperBound; i++)
			{
				if (Public2.PRJ.TX[i].Active)
				{
					num++;
				}
			}
			if (num < 1)
			{
				Interaction.MsgBox(Localization.LocMSG(16), MsgBoxStyle.Critical);
				return;
			}
			try
			{
				object objectValue = RuntimeHelpers.GetObjectValue(Interaction.CreateObject("Excel.Application"));
				NewLateBinding.LateCall(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "Workbooks", new object[0], null, null, null), null, "Open", new object[1] { Application.StartupPath + "\\Samples\\RadiatingSourcesEN.xlt" }, null, null, null), null, "Activate", new object[0], null, null, null, IgnoreReturn: true);
				object objectValue2 = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "Worksheets", new object[1] { 1 }, null, null, null));
				object[,] array = new object[num + 1, 19];
				int upperBound2 = Public2.PRJ.TX.GetUpperBound(0);
				for (int j = 0; j <= upperBound2; j++)
				{
					if (Public2.PRJ.TX[j].Active)
					{
						array[j, 0] = Public2.PRJ.TX[j].Name;
						array[j, 1] = Public2.PRJ.TX[j].Sector;
						array[j, 2] = Public2.PRJ.TX[j].TypeOfService;
						array[j, 3] = Form1_Public.FreqSpanString(Public2.PRJ.TX[j].F, Public2.PRJ.TX[j].F2);
						array[j, 4] = Math.Round(Public2.PRJ.TX[j].P, 4);
						array[j, 5] = Public2.PRJ.TX[j].N;
						array[j, 6] = Math.Round(Public2.PRJ.TX[j].Loss, 4);
						array[j, 7] = Public2.PRJ.TX[j].AntType;
						array[j, 8] = Form1_Public.Azimuth360(Public2.PRJ.TX[j].Az);
						array[j, 9] = Math.Round(Public2.PRJ.TX[j].G, 2);
						array[j, 10] = Math.Round(Public2.PRJ.TX[j].H, 2);
						array[j, 11] = Public2.PRJ.TX[j].D;
						array[j, 12] = Public2.PRJ.TX[j].Ug;
						array[j, 13] = Public2.PRJ.TX[j].ElectUg;
						array[j, 14] = Public2.PRJ.TX[j].UgH;
						array[j, 15] = Public2.PRJ.TX[j].UgV;
						array[j, 16] = Public2.PRJ.TX[j].Location;
						array[j, 17] = Public2.PRJ.TX[j].Exist;
						array[j, 18] = Public2.PRJ.TX[j].Owner;
					}
				}
				string text = $"A2:{ExcelColName(19)}{num + 2}";
				object[] array2 = new object[2]
				{
					text,
					Type.Missing
				};
				object[] array3 = array2;
				bool[] array4 = new bool[2] { true, false };
				bool[] array5 = array4;
				object instance = NewLateBinding.LateGet(objectValue2, null, "Range", array2, null, null, array4);
				if (array5[0])
				{
					text = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array3[0]), typeof(string));
				}
				NewLateBinding.LateSetComplex(instance, null, "Value", new object[1] { array }, null, null, OptimisticSet: false, RValueBase: true);
				GC.Collect();
				NewLateBinding.LateSet(objectValue, null, "Visible", new object[1] { true }, null, null);
				RepWordAlias.SetForegroundWindow(Conversions.ToInteger(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "ActiveWindow", new object[0], null, null, null), null, "Hwnd", new object[0], null, null, null)));
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Interaction.MsgBox(ex2.Message);
				ProjectData.ClearProjectError();
			}
		}
	}

	public static void ExcelExportControlPoint1()
	{
		short num = 0;
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.CPG))
			{
				int upperBound = Public2.PRJ.CPG.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					if (!Public2.PRJ.CPG[i].Active || Information.IsNothing(Public2.PRJ.CPG[i].CP))
					{
						continue;
					}
					int upperBound2 = Public2.PRJ.CPG[i].CP.GetUpperBound(0);
					for (int j = 0; j <= upperBound2; j++)
					{
						int upperBound3 = Public2.PRJ.CPG[i].CP[j].H.GetUpperBound(0);
						for (int k = 0; k <= upperBound3; k++)
						{
							num++;
						}
					}
				}
			}
			if (num == 0)
			{
				Interaction.MsgBox(Localization.LocMSG(87) + ". " + Localization.LocMSG(88));
				return;
			}
			try
			{
				object objectValue = RuntimeHelpers.GetObjectValue(Interaction.CreateObject("Excel.Application"));
				NewLateBinding.LateCall(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "Workbooks", new object[0], null, null, null), null, "Open", new object[1] { Application.StartupPath + "\\Samples\\ControlPointsEN.xlt" }, null, null, null), null, "Activate", new object[0], null, null, null, IgnoreReturn: true);
				object objectValue2 = RuntimeHelpers.GetObjectValue(NewLateBinding.LateGet(objectValue, null, "Worksheets", new object[1] { 1 }, null, null, null));
				object[,] array = new object[num + 1, 13];
				short num2 = -1;
				if (!Information.IsNothing(Public2.PRJ.CPG))
				{
					int upperBound4 = Public2.PRJ.CPG.GetUpperBound(0);
					double LatWgs = default;
					double LonWgs = default;
					for (int l = 0; l <= upperBound4; l++)
					{
						if (!Public2.PRJ.CPG[l].Active || Information.IsNothing(Public2.PRJ.CPG[l].CP))
						{
							continue;
						}
						int upperBound5 = Public2.PRJ.CPG[l].CP.GetUpperBound(0);
						for (int m = 0; m <= upperBound5; m++)
						{
							int upperBound6 = Public2.PRJ.CPG[l].CP[m].H.GetUpperBound(0);
							for (int n = 0; n <= upperBound6; n++)
							{
								num2++;
								string text = "";
								if (Public2.PRJ.SETT.ShowControlPointGroups)
								{
									text = Conversions.ToString(l + 1) + ".";
								}
								text += Conversions.ToString(m + 1);
								if (Public2.PRJ.CPG[l].CP[m].H.GetUpperBound(0) > 0)
								{
									text = text + "." + Conversions.ToString(n + 1);
								}
								array[num2, 0] = text;
								if (n == 0)
								{
									array[num2, 1] = Public2.PRJ.CPG[l].CP[m].Name;
									CalcCoords.ConvWebMercMetersToWGS84(Public2.PRJ.CPG[l].CP[m].CoordWM, ref LatWgs, ref LonWgs);
									array[num2, 2] = CalcCoords.ConvLatToStr(LatWgs);
									array[num2, 3] = CalcCoords.ConvLonToStr(LonWgs);
									array[num2, 4] = Strings.Format(Public2.PRJ.CPG[l].CP[m].CoordM.Xm, "0.0");
									array[num2, 5] = Strings.Format(Public2.PRJ.CPG[l].CP[m].CoordM.Ym, "0.0");
									float num3 = (float)Math.Sqrt(Public2.PRJ.CPG[l].CP[m].CoordM.Xm * Public2.PRJ.CPG[l].CP[m].CoordM.Xm + Public2.PRJ.CPG[l].CP[m].CoordM.Ym * Public2.PRJ.CPG[l].CP[m].CoordM.Ym);
									array[num2, 6] = Math.Round(num3, 2);
									float num4 = (float)(Math.Atan2(Public2.PRJ.CPG[l].CP[m].CoordM.Xm, Public2.PRJ.CPG[l].CP[m].CoordM.Ym) * (180.0 / Math.PI));
									if (num4 < 0f)
									{
										num4 = 360f + num4;
									}
									array[num2, 7] = Math.Round(num4, 1);
									array[num2, 8] = Public2.PRJ.CPG[l].CP[m].Location;
								}
								array[num2, 9] = Math.Round(Public2.PRJ.CPG[l].CP[m].H[n].H, 3);
								array[num2, 10] = Math.Round(Public2.PRJ.CPG[l].CP[m].H[n].L, 2);
								array[num2, 11] = Math.Round(Public2.PRJ.CPG[l].CP[m].H[n].MeasuredE, 6);
								array[num2, 12] = Public2.PRJ.CPG[l].CP[m].H[n].Name;
							}
						}
					}
				}
				string text2 = $"A2:{ExcelColName(13)}{num + 2}";
				object[] array2;
				bool[] array3;
				object instance = NewLateBinding.LateGet(objectValue2, null, "Range", array2 = new object[2]
				{
					text2,
					Type.Missing
				}, null, null, array3 = new bool[2] { true, false });
				if (array3[0])
				{
					text2 = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array2[0]), typeof(string));
				}
				NewLateBinding.LateSetComplex(instance, null, "Value", new object[1] { array }, null, null, OptimisticSet: false, RValueBase: true);
				GC.Collect();
				NewLateBinding.LateSet(objectValue, null, "Visible", new object[1] { true }, null, null);
				RepWordAlias.SetForegroundWindow(Conversions.ToInteger(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "ActiveWindow", new object[0], null, null, null), null, "Hwnd", new object[0], null, null, null)));
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Interaction.MsgBox(ex2.Message);
				ProjectData.ClearProjectError();
			}
		}
	}

	public static void ExcelImportPRTO()
	{
		try
		{
			using OpenFileDialog openFileDialog = new OpenFileDialog();
			openFileDialog.Filter = "Excel files|*.xlsx;*.xls";
			openFileDialog.AddExtension = true;
			if (openFileDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			object objectValue = RuntimeHelpers.GetObjectValue(Interaction.CreateObject("Excel.Application"));
			object instance = NewLateBinding.LateGet(objectValue, null, "workbooks", new object[0], null, null, null);
			OpenFileDialog openFileDialog2;
			object[] array = new object[1] { (openFileDialog2 = openFileDialog).FileName };
			object[] array2 = array;
			bool[] array3 = new bool[1] { true };
			bool[] array4 = array3;
			NewLateBinding.LateCall(instance, null, "open", array, null, null, array3, IgnoreReturn: true);
			if (array4[0])
			{
				openFileDialog2.FileName = (string)Conversions.ChangeType(RuntimeHelpers.GetObjectValue(array2[0]), typeof(string));
			}
			short num = 1;
			Public2.PRJ.TX = null;
			string text;
			do
			{
				num = checked((short)(num + 1));
				text = Conversions.ToString(NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "sheets", new object[1] { 1 }, null, null, null), null, "range", new object[1] { "A" + Conversions.ToString((int)num) }, null, null, null), null, "value", new object[0], null, null, null));
				if (Operators.CompareString(text, "", TextCompare: false) == 0)
				{
					break;
				}
				if (Information.IsNothing(Public2.PRJ.TX))
				{
					Public2.PRJ.TX = new Public2.TxPRTO[1];
				}
				else
				{
					ref Public2.TxPRTO[] tX = ref Public2.PRJ.TX;
					tX = (Public2.TxPRTO[])Utils.CopyArray(tX, new Public2.TxPRTO[checked(Public2.PRJ.TX.GetUpperBound(0) + 1 + 1)]);
				}
				Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].Name = text;
				Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].Sector = Conversions.ToString(NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "sheets", new object[1] { 1 }, null, null, null), null, "range", new object[1] { "B" + Conversions.ToString((int)num) }, null, null, null), null, "value", new object[0], null, null, null));
				Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].TypeOfService = Conversions.ToString(NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "sheets", new object[1] { 1 }, null, null, null), null, "range", new object[1] { "C" + Conversions.ToString((int)num) }, null, null, null), null, "value", new object[0], null, null, null));
				Form1_Public.FreqSpanCheck(Conversions.ToString(NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "sheets", new object[1] { 1 }, null, null, null), null, "range", new object[1] { "D" + Conversions.ToString((int)num) }, null, null, null), null, "value", new object[0], null, null, null)), ref Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].F, ref Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].F2);
				string text2 = Conversions.ToString(NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "sheets", new object[1] { 1 }, null, null, null), null, "range", new object[1] { "E" + Conversions.ToString((int)num) }, null, null, null), null, "value", new object[0], null, null, null));
				if (Module1.ValidText(text2, CheckLimits: true, 0f, 100000f, IncludeMin: true, IncludeMax: true, Message: false))
				{
					Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].P = Module1.QSNG(text2);
				}
				else
				{
					Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].P = 0f;
				}
				text2 = Conversions.ToString(NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "sheets", new object[1] { 1 }, null, null, null), null, "range", new object[1] { "F" + Conversions.ToString((int)num) }, null, null, null), null, "value", new object[0], null, null, null));
				if (Module1.ValidText(text2, CheckLimits: true, 1f, 100000f, IncludeMin: true, IncludeMax: true, Message: false))
				{
					Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].N = Conversions.ToInteger(Conversion.Int(text2));
				}
				else
				{
					Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].N = 1;
				}
				text2 = Conversions.ToString(NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "sheets", new object[1] { 1 }, null, null, null), null, "range", new object[1] { "G" + Conversions.ToString((int)num) }, null, null, null), null, "value", new object[0], null, null, null));
				if (Module1.ValidText(text2, CheckLimits: true, 0f, 100f, IncludeMin: true, IncludeMax: true, Message: false))
				{
					Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].Loss = Module1.QSNG(text2);
				}
				else
				{
					Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].Loss = 0f;
				}
				Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].AntType = Conversions.ToString(NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "sheets", new object[1] { 1 }, null, null, null), null, "range", new object[1] { "H" + Conversions.ToString((int)num) }, null, null, null), null, "value", new object[0], null, null, null));
				text2 = Conversions.ToString(NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "sheets", new object[1] { 1 }, null, null, null), null, "range", new object[1] { "I" + Conversions.ToString((int)num) }, null, null, null), null, "value", new object[0], null, null, null));
				if (Module1.ValidText(text2, CheckLimits: true, 0f, 360f, IncludeMin: true, IncludeMax: false, Message: false))
				{
					Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].Az = Module1.QSNG(text2);
				}
				else
				{
					Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].Az = -1f;
				}
				text2 = Conversions.ToString(NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "sheets", new object[1] { 1 }, null, null, null), null, "range", new object[1] { "J" + Conversions.ToString((int)num) }, null, null, null), null, "value", new object[0], null, null, null));
				if (Module1.ValidText(text2, CheckLimits: true, -30f, 65f, IncludeMin: true, IncludeMax: true, Message: false))
				{
					Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].G = Module1.QSNG(text2);
				}
				else
				{
					Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].G = 0f;
				}
				text2 = Conversions.ToString(NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "sheets", new object[1] { 1 }, null, null, null), null, "range", new object[1] { "K" + Conversions.ToString((int)num) }, null, null, null), null, "value", new object[0], null, null, null));
				if (Module1.ValidText(text2, CheckLimits: true, 0f, 1000f, IncludeMin: true, IncludeMax: true, Message: false))
				{
					Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].H = Module1.QSNG(text2);
				}
				else
				{
					Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].H = 0f;
				}
				text2 = Conversions.ToString(NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "sheets", new object[1] { 1 }, null, null, null), null, "range", new object[1] { "L" + Conversions.ToString((int)num) }, null, null, null), null, "value", new object[0], null, null, null));
				if (Module1.ValidText(text2, CheckLimits: true, 0f, 30f, IncludeMin: false, IncludeMax: true, Message: false))
				{
					Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].D = Module1.QSNG(text2);
				}
				else
				{
					Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].D = 1f;
				}
				text2 = Conversions.ToString(NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "sheets", new object[1] { 1 }, null, null, null), null, "range", new object[1] { "M" + Conversions.ToString((int)num) }, null, null, null), null, "value", new object[0], null, null, null));
				if (Module1.ValidText(text2, CheckLimits: true, -45f, 45f, IncludeMin: true, IncludeMax: true, Message: false))
				{
					Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].Ug = Module1.QSNG(text2);
				}
				else
				{
					Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].Ug = 0f;
				}
				text2 = Conversions.ToString(NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "sheets", new object[1] { 1 }, null, null, null), null, "range", new object[1] { "N" + Conversions.ToString((int)num) }, null, null, null), null, "value", new object[0], null, null, null));
				if (Module1.ValidText(text2, CheckLimits: true, -45f, 45f, IncludeMin: true, IncludeMax: true, Message: false))
				{
					Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].ElectUg = Module1.QSNG(text2);
				}
				else
				{
					Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].ElectUg = 0f;
				}
				Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].UgH = Conversions.ToString(NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "sheets", new object[1] { 1 }, null, null, null), null, "range", new object[1] { "O" + Conversions.ToString((int)num) }, null, null, null), null, "value", new object[0], null, null, null));
				Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].UgV = Conversions.ToString(NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "sheets", new object[1] { 1 }, null, null, null), null, "range", new object[1] { "P" + Conversions.ToString((int)num) }, null, null, null), null, "value", new object[0], null, null, null));
				text2 = Conversions.ToString(NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "sheets", new object[1] { 1 }, null, null, null), null, "range", new object[1] { "Q" + Conversions.ToString((int)num) }, null, null, null), null, "value", new object[0], null, null, null));
				if ((Operators.CompareString(text2, "Tower", TextCompare: false) == 0) | (Operators.CompareString(text2, "Roof", TextCompare: false) == 0) | (Operators.CompareString(text2, "Facade", TextCompare: false) == 0))
				{
					Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].Location = text2;
				}
				else
				{
					Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].Location = "Tower";
				}
				text2 = Conversions.ToString(NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "sheets", new object[1] { 1 }, null, null, null), null, "range", new object[1] { "R" + Conversions.ToString((int)num) }, null, null, null), null, "value", new object[0], null, null, null));
				if ((Operators.CompareString(text2, "Existing", TextCompare: false) == 0) | (Operators.CompareString(text2, "New", TextCompare: false) == 0))
				{
					Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].Exist = text2;
				}
				else
				{
					Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].Exist = "Existing";
				}
				Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].Owner = Conversions.ToString(NewLateBinding.LateGet(NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "sheets", new object[1] { 1 }, null, null, null), null, "range", new object[1] { "S" + Conversions.ToString((int)num) }, null, null, null), null, "value", new object[0], null, null, null));
				Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].Active = true;
				Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].CoordWM = Public2.PRJ.SETT.OriginCoordWM;
				Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].Alpha24 = new float[4];
				Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].Ftdc = new float[4];
				Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].Fpr = new float[4];
				int num2 = 0;
				do
				{
					Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].Alpha24[num2] = 1f;
					Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].Ftdc[num2] = 1f;
					Public2.PRJ.TX[Public2.PRJ.TX.GetUpperBound(0)].Fpr[num2] = 1f;
					num2 = checked(num2 + 1);
				}
				while (num2 <= 3);
			}
			while (Operators.CompareString(text, "", TextCompare: false) != 0);
			NewLateBinding.LateCall(NewLateBinding.LateGet(objectValue, null, "workbooks", new object[0], null, null, null), null, "close", new object[0], null, null, null, IgnoreReturn: true);
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Interaction.MsgBox(ex2.Message);
			ProjectData.ClearProjectError();
		}
	}

	public static string ExcelColName(int Col)
	{
		if ((Col < 0) & (Col > 256))
		{
			Interaction.MsgBox("Invalid Argument", MsgBoxStyle.Critical);
			return null;
		}
		checked
		{
			string result;
			if (Col <= 26)
			{
				result = Conversions.ToString(Strings.Chr(Col + 64));
			}
			else
			{
				short num = (short)unchecked(Col % 26);
				short num2 = (short)Math.Floor((double)Col / 26.0);
				if (num == 0)
				{
					num = 26;
					num2--;
				}
				result = Conversions.ToString(Strings.Chr(num2 + 64)) + Conversions.ToString(Strings.Chr(num + 64));
			}
			return result;
		}
	}
}
