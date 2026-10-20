using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using EMFPlanner.My;
using EMFPlanner.My.Resources;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;
using Microsoft.VisualBasic.FileIO;

namespace EMFPlanner;

[StandardModule]
internal sealed class RepWordAlias
{
	public struct ResKB
	{
		public string PointName;

		public string PointLabel;

		public string PointLabel2;

		public Public2.CoordMeters CoordM;

		public string PointLocation;

		public float PointH;

		public float PointElev;

		public float MeasuredE;

		public float CalculatedEsum;

		public float CalculatedPPEsum;

		public float TotalEsum;

		public float TotalPPEsum;

		public float KB;

		public float[] V;
	}

	public struct ResTBL
	{
		public float Esum;

		public float PPEsum;

		public float KB;

		public float[] V;
	}

	public struct ResEmfSafetyVolumes1
	{
		public bool Active;

		public string Name;

		public ResEmfSafetyVolumes2[] SV;
	}

	public struct ResEmfSafetyVolumes2
	{
		public bool USE;

		public float Lv;

		public float Lh;

		public float Lm1;

		public float Lm2;
	}

	public struct AzSec
	{
		public float Azimuth;

		public string Sector;

		public AzSec(float _azimuth, string _sector)
		{
			this = default;
			Azimuth = _azimuth;
			Sector = _sector;
		}
	}

	private static Color[] CLR = new Color[40]
	{
		Color.Red,
		Color.Yellow,
		Color.Green,
		Color.SaddleBrown,
		Color.Purple,
		Color.Aqua,
		Color.Chartreuse,
		Color.DarkOrange,
		Color.DeepPink,
		Color.Firebrick,
		Color.Khaki,
		Color.LightSeaGreen,
		Color.LightSkyBlue,
		Color.Olive,
		Color.Gold,
		Color.DarkKhaki,
		Color.Indigo,
		Color.MistyRose,
		Color.DarkSeaGreen,
		Color.DodgerBlue,
		Color.Red,
		Color.Yellow,
		Color.Green,
		Color.SaddleBrown,
		Color.Purple,
		Color.Aqua,
		Color.Chartreuse,
		Color.DarkOrange,
		Color.DeepPink,
		Color.Firebrick,
		Color.Khaki,
		Color.LightSeaGreen,
		Color.LightSkyBlue,
		Color.Olive,
		Color.Gold,
		Color.DarkKhaki,
		Color.Indigo,
		Color.MistyRose,
		Color.DarkSeaGreen,
		Color.DodgerBlue
	};

	private static short wSizeX;

	private static short wSizeY;

	private static short Mup = 15;

	private static short Mdown = 30;

	private static short Mleft = 70;

	private static short Mright = 20;

	private static double M2Pix;

	private static double WM2Pix;

	private static SizeF stringSize = default;

	private static System.Drawing.Font Font8 = new System.Drawing.Font("Arial", 8f, FontStyle.Regular);

	private static Brush BrushBlack = new SolidBrush(Color.Black);

	private static Brush BrushBlue = new SolidBrush(Color.Blue);

	private static System.Drawing.Font Font10 = new System.Drawing.Font("Arial", 10f, FontStyle.Regular);

	private static double tmpTileX1;

	private static double tmpTileY1;

	private static long LineX1;

	private static long LineY1;

	private static long LineX2;

	private static long LineY2;

	private static Pen PenBrown1 = new Pen(Color.Brown, 2f);

	[DllImport("user32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
	public static extern int SetForegroundWindow(int hwnd);

	public static void WordReportOneAlias(string OneAlias)
	{
		try
		{
			Microsoft.Office.Interop.Word.Application oWord = (Microsoft.Office.Interop.Word.Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("000209FF-0000-0000-C000-000000000046")));
			Documents documents = oWord.Documents;
			object Template = Public2.AppStartupPath + "\\Samples\\" + OneAlias + ".dot";
			object NewTemplate = RuntimeHelpers.GetObjectValue(Missing.Value);
			object DocumentType = RuntimeHelpers.GetObjectValue(Missing.Value);
			object Visible = RuntimeHelpers.GetObjectValue(Missing.Value);
			Document oDoc = documents.Add(ref Template, ref NewTemplate, ref DocumentType, ref Visible);
			oDoc.ShowSpellingErrors = false;
			oDoc.ShowGrammaticalErrors = false;
			oWord.Options.AutoFormatAsYouTypeReplaceQuotes = false;
			oWord.AutoCorrect.ReplaceText = false;
			oWord.AutoCorrect.CorrectSentenceCaps = false;
			oWord.AutoCorrect.CorrectTableCells = false;
			if (Operators.CompareString(OneAlias, "{RadiatingSources_1}", TextCompare: false) == 0)
			{
				WordAddTablePRTO1(ref oWord);
			}
			if (Operators.CompareString(OneAlias, "{AntennaPatterns}", TextCompare: false) == 0)
			{
				WordAddAntennaDN(ref oWord, ref oDoc);
			}
			if (Operators.CompareString(OneAlias, "{HorizontalCutsА4_1}", TextCompare: false) == 0)
			{
				WordAddHorizCutA4(ref oWord, ref oDoc);
			}
			if (Operators.CompareString(OneAlias, "{HorizontalCutsА3_1}", TextCompare: false) == 0)
			{
				WordAddHorizCutA3(ref oWord, ref oDoc);
			}
			if (Operators.CompareString(OneAlias, "{VerticalCuts_1}", TextCompare: false) == 0)
			{
				WordAddVertCut1(ref oWord, ref oDoc);
			}
			if (Operators.CompareString(OneAlias, "{ControlPoints_1}", TextCompare: false) == 0)
			{
				WordAddTableControlPoint1(ref oWord);
			}
			if (Operators.CompareString(OneAlias, "{ControlPoints_2}", TextCompare: false) == 0)
			{
				WordAddTableControlPoint2(ref oWord);
			}
			if (Operators.CompareString(OneAlias, "{SimultaneousExposureVerticalCutTables_1}", TextCompare: false) == 0)
			{
				WordAddTablesVertKB1(ref oWord, ref oDoc);
			}
			if (Operators.CompareString(OneAlias, "{EFieldStrengthVerticalCutTables_1}", TextCompare: false) == 0)
			{
				WordAddTablesVertE1(ref oWord, ref oDoc);
			}
			if (Operators.CompareString(OneAlias, "{EFieldStrengthGraph_1}", TextCompare: false) == 0)
			{
				WordAddGraphAllE(ref oWord, ref oDoc);
			}
			if (Operators.CompareString(OneAlias, "{SimultaneousExposureGraph_1}", TextCompare: false) == 0)
			{
				WordAddGraphAll(ref oWord, ref oDoc);
			}
			if (Operators.CompareString(OneAlias, "{EMF_Safety_Volumes1}", TextCompare: false) == 0)
			{
				WordAddTableEMFSafetyVolumes1(ref oWord);
			}
			if (Operators.CompareString(OneAlias, "{CardinalPointsChart}", TextCompare: false) == 0)
			{
				WordAddBeetle(ref oWord);
			}
			oDoc.ActiveWindow.ActivePane.VerticalPercentScrolled = 0;
			oWord.Visible = true;
			SetForegroundWindow(oWord.ActiveWindow.Hwnd);
		}
		catch (Exception ex)
		{
			ProjectData.SetProjectError(ex);
			Exception ex2 = ex;
			Interaction.MsgBox(ex2.Message);
			ProjectData.ClearProjectError();
		}
	}

	public static void ReportAlias()
	{
		if (Information.IsNothing(Public2.PRJ.TX) || !CalcSZZ.CalcTmp())
		{
			return;
		}
		string text = Public2.PRJ.SETT.RepFileName;
		if (!(text.Contains("\\") | text.Contains("/")))
		{
			text = Microsoft.VisualBasic.FileIO.FileSystem.GetFileInfo(Public2.ProjectFileName).DirectoryName + "\\" + text;
		}
		if (!MyProject.Computer.FileSystem.FileExists(text))
		{
			Interaction.MsgBox(Localization.LocMSG(108));
			return;
		}
		Microsoft.Office.Interop.Word.Application oWord = (Microsoft.Office.Interop.Word.Application)Activator.CreateInstance(Marshal.GetTypeFromCLSID(new Guid("000209FF-0000-0000-C000-000000000046")));
		Documents documents = oWord.Documents;
		object Template = text;
		object NewTemplate = RuntimeHelpers.GetObjectValue(Missing.Value);
		object DocumentType = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Visible = RuntimeHelpers.GetObjectValue(Missing.Value);
		Document document = documents.Add(ref Template, ref NewTemplate, ref DocumentType, ref Visible);
		text = Conversions.ToString(Template);
		Document oDoc = document;
		oDoc.ShowSpellingErrors = false;
		oDoc.ShowGrammaticalErrors = false;
		oWord.Options.AutoFormatAsYouTypeReplaceQuotes = false;
		oWord.AutoCorrect.ReplaceText = false;
		oWord.AutoCorrect.CorrectSentenceCaps = false;
		oWord.AutoCorrect.CorrectTableCells = false;
		MyProject.Forms.Form1.ProgressBar.Value = 1;
		MyProject.Forms.Form1.ProgressBar.Maximum = 16;
		object MatchSoundsLike = RuntimeHelpers.GetObjectValue(Type.Missing);
		Find find = oWord.Selection.Find;
		object Wrap = WdFindWrap.wdFindContinue;
		_ = (object)WdReplace.wdReplaceAll;
		if (!Information.IsNothing(Public2.PRJ.SETT.ALS))
		{
			int upperBound = Public2.PRJ.SETT.ALS.GetUpperBound(1);
			for (int i = 0; i <= upperBound; i = checked(i + 1))
			{
				find.Text = Public2.PRJ.SETT.ALS[0, i];
				find.Replacement.Text = Module1.Reject0D0A09(Public2.PRJ.SETT.ALS[1, i]);
				Visible = RuntimeHelpers.GetObjectValue(Type.Missing);
				DocumentType = false;
				NewTemplate = false;
				Template = false;
				object MatchAllWordForms = false;
				object Forward = true;
				object Format = false;
				object Replace = WdReplace.wdReplaceAll;
				object MatchKashida = RuntimeHelpers.GetObjectValue(Missing.Value);
				object MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
				object MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
				object MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
				find.Execute(ref Visible, ref DocumentType, ref NewTemplate, ref Template, ref MatchSoundsLike, ref MatchAllWordForms, ref Forward, ref Wrap, ref Format, ref MatchSoundsLike, ref Replace, ref MatchKashida, ref MatchDiacritics, ref MatchAlefHamza, ref MatchControl);
				foreach (object section in oDoc.Sections)
				{
					object objectValue = RuntimeHelpers.GetObjectValue(section);
					Find find2 = ((Range)NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "Footers", new object[1] { WdHeaderFooterIndex.wdHeaderFooterPrimary }, null, null, null), null, "Range", new object[0], null, null, null)).Find;
					ref string reference = ref Public2.PRJ.SETT.ALS[0, i];
					ref string reference2 = ref reference;
					MatchControl = reference;
					MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchKashida = RuntimeHelpers.GetObjectValue(Missing.Value);
					Replace = RuntimeHelpers.GetObjectValue(Missing.Value);
					Format = RuntimeHelpers.GetObjectValue(Missing.Value);
					Forward = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchAllWordForms = RuntimeHelpers.GetObjectValue(Missing.Value);
					Template = RuntimeHelpers.GetObjectValue(Missing.Value);
					NewTemplate = Module1.Reject0D0A09(Public2.PRJ.SETT.ALS[1, i]);
					DocumentType = WdReplace.wdReplaceAll;
					Visible = RuntimeHelpers.GetObjectValue(Missing.Value);
					object MatchDiacritics2 = RuntimeHelpers.GetObjectValue(Missing.Value);
					object MatchAlefHamza2 = RuntimeHelpers.GetObjectValue(Missing.Value);
					object MatchControl2 = RuntimeHelpers.GetObjectValue(Missing.Value);
					find2.Execute(ref MatchControl, ref MatchAlefHamza, ref MatchDiacritics, ref MatchKashida, ref Replace, ref Format, ref Forward, ref MatchAllWordForms, ref Template, ref NewTemplate, ref DocumentType, ref Visible, ref MatchDiacritics2, ref MatchAlefHamza2, ref MatchControl2);
					reference2 = Conversions.ToString(MatchControl);
					Find find3 = ((Range)NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "Footers", new object[1] { WdHeaderFooterIndex.wdHeaderFooterFirstPage }, null, null, null), null, "Range", new object[0], null, null, null)).Find;
					ref string reference3 = ref Public2.PRJ.SETT.ALS[0, i];
					reference2 = ref reference3;
					MatchControl2 = reference3;
					MatchAlefHamza2 = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchDiacritics2 = RuntimeHelpers.GetObjectValue(Missing.Value);
					Visible = RuntimeHelpers.GetObjectValue(Missing.Value);
					DocumentType = RuntimeHelpers.GetObjectValue(Missing.Value);
					NewTemplate = RuntimeHelpers.GetObjectValue(Missing.Value);
					Template = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchAllWordForms = RuntimeHelpers.GetObjectValue(Missing.Value);
					Forward = RuntimeHelpers.GetObjectValue(Missing.Value);
					Format = Module1.Reject0D0A09(Public2.PRJ.SETT.ALS[1, i]);
					Replace = WdReplace.wdReplaceAll;
					MatchKashida = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
					find3.Execute(ref MatchControl2, ref MatchAlefHamza2, ref MatchDiacritics2, ref Visible, ref DocumentType, ref NewTemplate, ref Template, ref MatchAllWordForms, ref Forward, ref Format, ref Replace, ref MatchKashida, ref MatchDiacritics, ref MatchAlefHamza, ref MatchControl);
					reference2 = Conversions.ToString(MatchControl2);
					Find find4 = ((Range)NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "Headers", new object[1] { WdHeaderFooterIndex.wdHeaderFooterPrimary }, null, null, null), null, "Range", new object[0], null, null, null)).Find;
					ref string reference4 = ref Public2.PRJ.SETT.ALS[0, i];
					reference2 = ref reference4;
					MatchControl = reference4;
					MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchKashida = RuntimeHelpers.GetObjectValue(Missing.Value);
					Replace = RuntimeHelpers.GetObjectValue(Missing.Value);
					Format = RuntimeHelpers.GetObjectValue(Missing.Value);
					Forward = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchAllWordForms = RuntimeHelpers.GetObjectValue(Missing.Value);
					Template = RuntimeHelpers.GetObjectValue(Missing.Value);
					NewTemplate = Module1.Reject0D0A09(Public2.PRJ.SETT.ALS[1, i]);
					DocumentType = WdReplace.wdReplaceAll;
					Visible = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchDiacritics2 = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchAlefHamza2 = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchControl2 = RuntimeHelpers.GetObjectValue(Missing.Value);
					find4.Execute(ref MatchControl, ref MatchAlefHamza, ref MatchDiacritics, ref MatchKashida, ref Replace, ref Format, ref Forward, ref MatchAllWordForms, ref Template, ref NewTemplate, ref DocumentType, ref Visible, ref MatchDiacritics2, ref MatchAlefHamza2, ref MatchControl2);
					reference2 = Conversions.ToString(MatchControl);
					Find find5 = ((Range)NewLateBinding.LateGet(NewLateBinding.LateGet(objectValue, null, "Headers", new object[1] { WdHeaderFooterIndex.wdHeaderFooterFirstPage }, null, null, null), null, "Range", new object[0], null, null, null)).Find;
					ref string reference5 = ref Public2.PRJ.SETT.ALS[0, i];
					reference2 = ref reference5;
					MatchControl2 = reference5;
					MatchAlefHamza2 = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchDiacritics2 = RuntimeHelpers.GetObjectValue(Missing.Value);
					Visible = RuntimeHelpers.GetObjectValue(Missing.Value);
					DocumentType = RuntimeHelpers.GetObjectValue(Missing.Value);
					NewTemplate = RuntimeHelpers.GetObjectValue(Missing.Value);
					Template = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchAllWordForms = RuntimeHelpers.GetObjectValue(Missing.Value);
					Forward = RuntimeHelpers.GetObjectValue(Missing.Value);
					Format = Module1.Reject0D0A09(Public2.PRJ.SETT.ALS[1, i]);
					Replace = WdReplace.wdReplaceAll;
					MatchKashida = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
					find5.Execute(ref MatchControl2, ref MatchAlefHamza2, ref MatchDiacritics2, ref Visible, ref DocumentType, ref NewTemplate, ref Template, ref MatchAllWordForms, ref Forward, ref Format, ref Replace, ref MatchKashida, ref MatchDiacritics, ref MatchAlefHamza, ref MatchControl);
					reference2 = Conversions.ToString(MatchControl2);
				}
			}
			oWord.ActiveWindow.ActivePane.View.SeekView = WdSeekView.wdSeekMainDocument;
		}
		MyProject.Forms.Form1.ProgressBar.Value = 2;
		WordAddCoordSitPlan(ref oWord);
		MyProject.Forms.Form1.ProgressBar.Value = 3;
		WordAddTablePRTO1(ref oWord);
		MyProject.Forms.Form1.ProgressBar.Value = 4;
		WordAddAntennaDN(ref oWord, ref oDoc);
		MyProject.Forms.Form1.ProgressBar.Value = 5;
		WordAddHorizCutA4(ref oWord, ref oDoc);
		MyProject.Forms.Form1.ProgressBar.Value = 6;
		WordAddHorizCutA3(ref oWord, ref oDoc);
		MyProject.Forms.Form1.ProgressBar.Value = 7;
		WordAddVertCut1(ref oWord, ref oDoc);
		MyProject.Forms.Form1.ProgressBar.Value = 8;
		WordAddTableControlPoint1(ref oWord);
		MyProject.Forms.Form1.ProgressBar.Value = 9;
		WordAddTablesVertE1(ref oWord, ref oDoc);
		MyProject.Forms.Form1.ProgressBar.Value = 10;
		WordAddTablesVertKB1(ref oWord, ref oDoc);
		MyProject.Forms.Form1.ProgressBar.Value = 11;
		WordAddGraphAllE(ref oWord, ref oDoc);
		MyProject.Forms.Form1.ProgressBar.Value = 12;
		WordAddGraphAll(ref oWord, ref oDoc);
		MyProject.Forms.Form1.ProgressBar.Value = 13;
		WordAddTableControlPoint2(ref oWord);
		MyProject.Forms.Form1.ProgressBar.Value = 14;
		WordAddTableEMFSafetyVolumes1(ref oWord);
		MyProject.Forms.Form1.ProgressBar.Value = 15;
		WordAddBeetle(ref oWord);
		MyProject.Forms.Form1.ProgressBar.Value = 16;
		if (oDoc.TablesOfContents.Count > 0)
		{
			oDoc.TablesOfContents[1].Update();
			oDoc.TablesOfContents[1].Range.Font.SmallCaps = 0;
			oDoc.TablesOfContents[1].Range.Font.AllCaps = 0;
			oDoc.TablesOfContents[1].Range.Font.Bold = 0;
		}
		MyProject.Forms.Form1.ProgressBar.Value = 0;
		oDoc.ActiveWindow.ActivePane.VerticalPercentScrolled = 0;
		oWord.Visible = true;
	}

	[DllImport("user32.dll")]
	public static extern short GetAsyncKeyState(Keys vKey);

	public static void WordAddTablePRTO1(ref Microsoft.Office.Interop.Word.Application oWord)
	{
		Find find = oWord.Selection.Find;
		find.Forward = true;
		find.ClearFormatting();
		find.MatchWholeWord = true;
		find.MatchCase = false;
		find.Wrap = WdFindWrap.wdFindContinue;
		object FindText = "{RadiatingSources_1}";
		object MatchCase = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchWholeWord = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchWildcards = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchSoundsLike = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchAllWordForms = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Forward = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Wrap = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Format = RuntimeHelpers.GetObjectValue(Missing.Value);
		object ReplaceWith = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Replace = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchKashida = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
		find.Execute(ref FindText, ref MatchCase, ref MatchWholeWord, ref MatchWildcards, ref MatchSoundsLike, ref MatchAllWordForms, ref Forward, ref Wrap, ref Format, ref ReplaceWith, ref Replace, ref MatchKashida, ref MatchDiacritics, ref MatchAlefHamza, ref MatchControl);
		_ = null;
		if (!oWord.Selection.Find.Found)
		{
			return;
		}
		oWord.Selection.Range.Select();
		string sDest = "";
		int num = 1;
		checked
		{
			do
			{
				sDest += " \t";
				num++;
			}
			while (num <= 17);
			StringType.MidStmtStr(ref sDest, Strings.Len(sDest), 1, "\r");
			int upperBound = Public2.PRJ.TX.GetUpperBound(0);
			for (int i = 0; i <= upperBound; i++)
			{
				if (Public2.PRJ.TX[i].Active)
				{
					sDest = sDest + Conversions.ToString(i + 1) + "\t";
					sDest = sDest + Module1.Reject0D0A09(Public2.PRJ.TX[i].Name) + "\t";
					sDest = sDest + Public2.PRJ.TX[i].TypeOfService + "\t";
					sDest = sDest + Form1_Public.FreqSpanString(Public2.PRJ.TX[i].F, Public2.PRJ.TX[i].F2) + "\t";
					sDest = sDest + Conversions.ToString(Public2.PRJ.TX[i].P) + "\t";
					sDest = sDest + Conversions.ToString(Public2.PRJ.TX[i].N) + "\t";
					sDest = sDest + Conversions.ToString(Math.Round(Public2.PRJ.TX[i].Loss, 4)) + "\t";
					sDest = sDest + Module1.Reject0D0A09(Public2.PRJ.TX[i].AntType) + "\t";
					sDest = sDest + Form1_Public.Azimuth360(Public2.PRJ.TX[i].Az) + "\t";
					sDest = sDest + Conversions.ToString(Public2.PRJ.TX[i].G) + "\t";
					sDest = sDest + Conversions.ToString(Public2.PRJ.TX[i].H) + "\t";
					sDest = sDest + Conversions.ToString(Public2.PRJ.TX[i].D) + "\t";
					sDest = sDest + Conversions.ToString(Public2.PRJ.TX[i].Ug) + "\t";
					sDest = sDest + Conversions.ToString(Public2.PRJ.TX[i].ElectUg) + "\t";
					sDest = sDest + Module1.Reject0D0A09(Public2.PRJ.TX[i].UgH) + "\t";
					sDest = sDest + Module1.Reject0D0A09(Public2.PRJ.TX[i].UgV) + "\t";
					sDest = sDest + Public2.PRJ.TX[i].Owner + "\t";
					StringType.MidStmtStr(ref sDest, Strings.Len(sDest), 1, "\r");
				}
			}
			oWord.Selection.Text = sDest;
			Selection selection = oWord.Selection;
			MatchControl = WdTableFieldSeparator.wdSeparateByTabs;
			MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchKashida = oWord.MillimetersToPoints(10f);
			Replace = RuntimeHelpers.GetObjectValue(Missing.Value);
			ReplaceWith = RuntimeHelpers.GetObjectValue(Missing.Value);
			Format = RuntimeHelpers.GetObjectValue(Missing.Value);
			Wrap = RuntimeHelpers.GetObjectValue(Missing.Value);
			Forward = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchAllWordForms = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchSoundsLike = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchWildcards = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchWholeWord = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchCase = RuntimeHelpers.GetObjectValue(Missing.Value);
			FindText = WdAutoFitBehavior.wdAutoFitFixed;
			object DefaultTableBehavior = RuntimeHelpers.GetObjectValue(Missing.Value);
			Table table = selection.ConvertToTable(ref MatchControl, ref MatchAlefHamza, ref MatchDiacritics, ref MatchKashida, ref Replace, ref ReplaceWith, ref Format, ref Wrap, ref Forward, ref MatchAllWordForms, ref MatchSoundsLike, ref MatchWildcards, ref MatchWholeWord, ref MatchCase, ref FindText, ref DefaultTableBehavior);
			table.Rows.Alignment = WdRowAlignment.wdAlignRowCenter;
			short num2 = 1;
			int upperBound2 = Public2.PRJ.TX.GetUpperBound(0);
			for (int j = 0; j <= upperBound2; j++)
			{
				if (Public2.PRJ.TX[j].Active)
				{
					num2++;
					if (Operators.CompareString(Public2.PRJ.TX[j].Exist, "New", TextCompare: false) == 0)
					{
						table.Rows[num2].Select();
						oWord.Selection.Shading.BackgroundPatternColor = WdColor.wdColorGray20;
					}
				}
			}
			table.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
			table.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
			table.Borders.InsideLineWidth = WdLineWidth.wdLineWidth050pt;
			table.Borders.OutsideLineWidth = WdLineWidth.wdLineWidth050pt;
			table.Range.Font.Name = "Times New Roman";
			table.Range.Font.Size = 10f;
			table.Range.Font.Bold = 0;
			table.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
			ParagraphFormat paragraphFormat = table.Range.ParagraphFormat;
			paragraphFormat.LeftIndent = oWord.MillimetersToPoints(-2f);
			paragraphFormat.RightIndent = oWord.MillimetersToPoints(-2f);
			paragraphFormat.SpaceBefore = 0f;
			paragraphFormat.SpaceBeforeAuto = 0;
			paragraphFormat.SpaceAfter = 0f;
			paragraphFormat.SpaceAfterAuto = 0;
			paragraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
			paragraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
			paragraphFormat.WidowControl = -1;
			paragraphFormat.KeepWithNext = 0;
			paragraphFormat.KeepTogether = 0;
			paragraphFormat.PageBreakBefore = 0;
			paragraphFormat.NoLineNumber = 0;
			paragraphFormat.Hyphenation = 0;
			paragraphFormat.FirstLineIndent = oWord.MillimetersToPoints(0f);
			paragraphFormat.OutlineLevel = WdOutlineLevel.wdOutlineLevelBodyText;
			paragraphFormat.CharacterUnitLeftIndent = 0f;
			paragraphFormat.CharacterUnitRightIndent = 0f;
			paragraphFormat.CharacterUnitFirstLineIndent = 0f;
			paragraphFormat.LineUnitBefore = 0f;
			paragraphFormat.LineUnitAfter = 0f;
			paragraphFormat.MirrorIndents = 0;
			paragraphFormat.TextboxTightWrap = WdTextboxTightWrap.wdTightNone;
			_ = null;
			table.Columns[2].Select();
			oWord.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
			oWord.Selection.ParagraphFormat.LeftIndent = oWord.MillimetersToPoints(0f);
			table.Columns[8].Select();
			oWord.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
			oWord.Selection.ParagraphFormat.LeftIndent = oWord.MillimetersToPoints(0f);
			table.Columns[17].Select();
			oWord.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
			oWord.Selection.ParagraphFormat.LeftIndent = oWord.MillimetersToPoints(0f);
			table.Rows[1].Height = oWord.MillimetersToPoints(25f);
			table.Rows[1].Select();
			oWord.Selection.Font.Size = 9f;
			oWord.Selection.Orientation = WdTextOrientation.wdTextOrientationUpward;
			ParagraphFormat paragraphFormat2 = oWord.Selection.Range.ParagraphFormat;
			paragraphFormat2.LeftIndent = oWord.MillimetersToPoints(-2f);
			paragraphFormat2.RightIndent = oWord.MillimetersToPoints(-2f);
			paragraphFormat2.SpaceBefore = 0f;
			paragraphFormat2.SpaceBeforeAuto = 0;
			paragraphFormat2.SpaceAfter = 0f;
			paragraphFormat2.SpaceAfterAuto = 0;
			paragraphFormat2.LineSpacingRule = WdLineSpacing.wdLineSpaceExactly;
			paragraphFormat2.LineSpacing = 8f;
			paragraphFormat2.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
			paragraphFormat2.WidowControl = -1;
			paragraphFormat2.KeepWithNext = 0;
			paragraphFormat2.KeepTogether = 0;
			paragraphFormat2.PageBreakBefore = 0;
			paragraphFormat2.NoLineNumber = 0;
			paragraphFormat2.Hyphenation = 0;
			paragraphFormat2.FirstLineIndent = oWord.MillimetersToPoints(0f);
			paragraphFormat2.OutlineLevel = WdOutlineLevel.wdOutlineLevelBodyText;
			paragraphFormat2.CharacterUnitLeftIndent = 0f;
			paragraphFormat2.CharacterUnitRightIndent = 0f;
			paragraphFormat2.CharacterUnitFirstLineIndent = 0f;
			paragraphFormat2.LineUnitBefore = 0f;
			paragraphFormat2.LineUnitAfter = 0f;
			paragraphFormat2.MirrorIndents = 0;
			paragraphFormat2.TextboxTightWrap = WdTextboxTightWrap.wdTightNone;
			_ = null;
			table.Cell(1, 1).Range.Orientation = WdTextOrientation.wdTextOrientationHorizontal;
			table.Cell(1, 2).Range.Orientation = WdTextOrientation.wdTextOrientationHorizontal;
			table.Cell(1, 8).Range.Orientation = WdTextOrientation.wdTextOrientationHorizontal;
			table.Cell(1, 17).Range.Orientation = WdTextOrientation.wdTextOrientationHorizontal;
			table.Columns[1].Width = oWord.MillimetersToPoints(5f);
			table.Columns[2].Width = oWord.MillimetersToPoints(38f);
			table.Columns[3].Width = oWord.MillimetersToPoints(16f);
			table.Columns[4].Width = oWord.MillimetersToPoints(15f);
			table.Columns[5].Width = oWord.MillimetersToPoints(15f);
			table.Columns[6].Width = oWord.MillimetersToPoints(8f);
			table.Columns[7].Width = oWord.MillimetersToPoints(8f);
			table.Columns[8].Width = oWord.MillimetersToPoints(44f);
			table.Columns[9].Width = oWord.MillimetersToPoints(15f);
			table.Columns[10].Width = oWord.MillimetersToPoints(11f);
			table.Columns[11].Width = oWord.MillimetersToPoints(11f);
			table.Columns[12].Width = oWord.MillimetersToPoints(11f);
			table.Columns[13].Width = oWord.MillimetersToPoints(11f);
			table.Columns[14].Width = oWord.MillimetersToPoints(11f);
			table.Columns[15].Width = oWord.MillimetersToPoints(11f);
			table.Columns[16].Width = oWord.MillimetersToPoints(11f);
			table.Columns[17].Width = oWord.MillimetersToPoints(30f);
			table.Cell(1, 1).Range.Text = "№";
			table.Cell(1, 2).Range.Text = "Radiating source name";
			table.Cell(1, 3).Range.Text = "Type of service";
			table.Cell(1, 4).Range.Text = "Frequency, MHz";
			table.Cell(1, 5).Range.Text = "Transmitter\vpower, W";
			table.Cell(1, 6).Range.Text = "Number of TX";
			table.Cell(1, 7).Range.Text = "Total attenuation,\v dB";
			table.Cell(1, 8).Range.Text = "Transmitting\vantenna model";
			table.Cell(1, 9).Range.Text = "Antenna\vazimuth, deg";
			table.Cell(1, 10).Range.Text = "Antenna\vgain, dBi";
			table.Cell(1, 11).Range.Text = "Antenna\vheight, m";
			table.Cell(1, 12).Range.Text = "Antenna\vsize, m";
			table.Cell(1, 13).Range.Text = "Mechanical\vdowntilt, deg";
			table.Cell(1, 14).Range.Text = "Electrical\vdowntilt, deg";
			table.Cell(1, 15).Range.Text = "Antenna\vbeam width\vH-plane, deg";
			table.Cell(1, 16).Range.Text = "Antenna\vbeam width\vV-plane, deg";
			table.Cell(1, 17).Range.Text = "Owner";
		}
	}

	public static string CorrectStr(string tSTR)
	{
		tSTR = Strings.Trim(tSTR);
		if (Operators.CompareString(tSTR, "", TextCompare: false) == 0)
		{
			return "-";
		}
		return tSTR;
	}

	public static void WordAddAntennaDN(ref Microsoft.Office.Interop.Word.Application oWord, ref Document oDoc)
	{
		Find find = oWord.Selection.Find;
		find.Forward = true;
		find.ClearFormatting();
		find.MatchWholeWord = true;
		find.MatchCase = false;
		find.Wrap = WdFindWrap.wdFindContinue;
		object FindText = "{AntennaPatterns}";
		object MatchCase = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchWholeWord = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchWildcards = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchSoundsLike = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchAllWordForms = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Forward = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Wrap = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Format = RuntimeHelpers.GetObjectValue(Missing.Value);
		object ReplaceWith = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Replace = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchKashida = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
		find.Execute(ref FindText, ref MatchCase, ref MatchWholeWord, ref MatchWildcards, ref MatchSoundsLike, ref MatchAllWordForms, ref Forward, ref Wrap, ref Format, ref ReplaceWith, ref Replace, ref MatchKashida, ref MatchDiacritics, ref MatchAlefHamza, ref MatchControl);
		_ = null;
		if (!oWord.Selection.Find.Found)
		{
			return;
		}
		int upperBound = Public2.PRJ.TX.GetUpperBound(0);
		checked
		{
			for (int i = 0; i <= upperBound; i++)
			{
				if (Public2.PRJ.TX[i].Active && (Information.IsNothing(Public2.PRJ.TX[i].DnH) | Information.IsNothing(Public2.PRJ.TX[i].DnV)))
				{
					Interaction.MsgBox(Localization.LocMSG(84) + " " + Conversions.ToString(i + 1));
					return;
				}
			}
			oWord.Selection.Range.Select();
			Bitmap bitmap = new Bitmap(Public2.AppStartupPath + "\\template\\ReportPatternDB_DEG.png");
			int num = Math.Min(bitmap.Width - 5, bitmap.Height - 5);
			int upperBound2 = Public2.PRJ.TX.GetUpperBound(0);
			for (int j = 0; j <= upperBound2; j++)
			{
				if (!Public2.PRJ.TX[j].Active)
				{
					continue;
				}
				int num2 = j - 1;
				for (int k = 0; k <= num2 && !((Operators.CompareString(Public2.PRJ.TX[k].AntType, Public2.PRJ.TX[j].AntType, TextCompare: false) == 0) & (Public2.PRJ.TX[k].G == Public2.PRJ.TX[j].G) & (Operators.CompareString(Public2.PRJ.TX[k].UgV, Public2.PRJ.TX[j].UgV, TextCompare: false) == 0) & (Operators.CompareString(Public2.PRJ.TX[k].UgH, Public2.PRJ.TX[j].UgH, TextCompare: false) == 0) & (Public2.PRJ.TX[k].F == Public2.PRJ.TX[j].F) & (Public2.PRJ.TX[k].F2 == Public2.PRJ.TX[j].F2)); k++)
				{
				}
				if (Public2.PRJ.TX[j].DnH.GetUpperBound(0) != 359)
				{
					continue;
				}
				Bitmap bitmap2 = new Bitmap(bitmap.Width + 10, bitmap.Height + 10);
				Bitmap bitmap3 = new Bitmap(bitmap.Width + 10, bitmap.Height + 10);
				Graphics graphics = Graphics.FromImage(bitmap2);
				Graphics graphics2 = Graphics.FromImage(bitmap3);
				graphics.DrawImage(bitmap, new Rectangle(5, 5, bitmap.Width, bitmap.Height));
				graphics2.DrawImage(bitmap, new Rectangle(5, 5, bitmap.Width, bitmap.Height));
				Point[] array = new Point[361];
				int num3 = 0;
				float num4;
				do
				{
					num4 = (float)((double)((30f - Public2.PRJ.TX[j].DnH[num3]) / 30f) * 0.863);
					if (num4 < 0f)
					{
						num4 = 0f;
					}
					array[num3] = new Point((int)Math.Round(Convert.ToSingle((double)num / 2.0 + (double)num4 * Math.Sin((double)(num3 + 90) / (180.0 / Math.PI)) * (double)num / 2.0) + 5f + 3f), (int)Math.Round(Convert.ToSingle((double)num - (double)num / 2.0 - (double)num4 * Math.Cos((double)(num3 + 90) / (180.0 / Math.PI)) * (double)num / 2.0) + 5f + 3f));
					num3++;
				}
				while (num3 <= 359);
				num4 = (float)((double)((30f - Public2.PRJ.TX[j].DnH[0]) / 30f) * 0.863);
				if (num4 < 0f)
				{
					num4 = 0f;
				}
				array[360] = new Point((int)Math.Round(Convert.ToSingle((double)num / 2.0 + (double)num4 * Math.Sin(7.853981633974483) * (double)num / 2.0) + 5f + 3f), (int)Math.Round(Convert.ToSingle((double)num - (double)num / 2.0 - (double)num4 * Math.Cos(7.853981633974483) * (double)num / 2.0) + 5f + 3f));
				graphics.FillPolygon(new SolidBrush(Color.FromArgb(50, 180, 180, 180)), array, FillMode.Alternate);
				graphics.DrawLines(new Pen(Color.Black, 6f), array);
				Point[] array2 = new Point[361];
				int num5 = 0;
				do
				{
					num4 = (float)((double)((30f - Public2.PRJ.TX[j].DnV[num5]) / 30f) * 0.863);
					if (num4 < 0f)
					{
						num4 = 0f;
					}
					array2[num5] = new Point((int)Math.Round(Convert.ToSingle((double)num / 2.0 + (double)num4 * Math.Sin((double)(num5 + 90) / (180.0 / Math.PI)) * (double)num / 2.0) + 5f + 3f), (int)Math.Round(Convert.ToSingle((double)num - (double)num / 2.0 - (double)num4 * Math.Cos((double)(num5 + 90) / (180.0 / Math.PI)) * (double)num / 2.0) + 5f + 3f));
					num5++;
				}
				while (num5 <= 359);
				num4 = (float)((double)((30f - Public2.PRJ.TX[j].DnV[0]) / 30f) * 0.863);
				if (num4 < 0f)
				{
					num4 = 0f;
				}
				array2[360] = new Point((int)Math.Round(Convert.ToSingle((double)num / 2.0 + (double)num4 * Math.Sin(7.853981633974483) * (double)num / 2.0) + 5f + 3f), (int)Math.Round(Convert.ToSingle((double)num - (double)num / 2.0 - (double)num4 * Math.Cos(7.853981633974483) * (double)num / 2.0) + 5f + 3f));
				graphics2.FillPolygon(new SolidBrush(Color.FromArgb(50, 180, 180, 180)), array2, FillMode.Alternate);
				graphics2.DrawLines(new Pen(Color.Black, 6f), array2);
				bitmap2.Save(Module1.FolderTMP + "\\AntennaH.png", ImageFormat.Png);
				bitmap3.Save(Module1.FolderTMP + "\\AntennaV.png", ImageFormat.Png);
				Selection selection = oWord.Selection;
				MatchControl = WdUnits.wdLine;
				MatchAlefHamza = WdMovementType.wdExtend;
				selection.EndKey(ref MatchControl, ref MatchAlefHamza);
				oWord.Selection.TypeText("Antenna " + Public2.PRJ.TX[j].AntType + " (G = " + Conversions.ToString(Public2.PRJ.TX[j].G) + " dBi, f = " + Form1_Public.FreqSpanString(Public2.PRJ.TX[j].F, Public2.PRJ.TX[j].F2) + " MHz)\r\n");
				Tables tables = oDoc.Tables;
				Range range = oWord.Selection.Range;
				MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
				Table table = tables.Add(range, 1, 2, ref MatchAlefHamza, ref MatchControl);
				table.Range.ParagraphFormat.SpaceAfter = 0f;
				table.Range.ParagraphFormat.SpaceBefore = 0f;
				table.Range.ParagraphFormat.LeftIndent = oWord.MillimetersToPoints(0f);
				table.Range.ParagraphFormat.RightIndent = oWord.MillimetersToPoints(0f);
				table.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
				table.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
				table.Columns.Width = oWord.MillimetersToPoints(78f);
				table.Rows.Height = oWord.MillimetersToPoints(78f);
				InlineShapes inlineShapes = table.Cell(1, 1).Range.InlineShapes;
				string fileName = Module1.FolderTMP + "\\AntennaH.png";
				MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
				inlineShapes.AddPicture(fileName, ref MatchControl, ref MatchAlefHamza, ref MatchDiacritics);
				InlineShapes inlineShapes2 = table.Cell(1, 2).Range.InlineShapes;
				string fileName2 = Module1.FolderTMP + "\\AntennaV.png";
				MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
				inlineShapes2.AddPicture(fileName2, ref MatchDiacritics, ref MatchAlefHamza, ref MatchControl);
				table.Select();
				Selection selection2 = oWord.Selection;
				MatchControl = WdCollapseDirection.wdCollapseEnd;
				selection2.Collapse(ref MatchControl);
				oWord.Selection.Text = " ";
				bitmap2.Dispose();
				bitmap3.Dispose();
			}
		}
	}

	public static void WordAddHorizCutA4(ref Microsoft.Office.Interop.Word.Application oWord, ref Document oDoc)
	{
		Find find = oWord.Selection.Find;
		find.Forward = true;
		find.ClearFormatting();
		find.MatchWholeWord = true;
		find.MatchCase = false;
		find.Wrap = WdFindWrap.wdFindContinue;
		object FindText = "{HorizontalCutsА4_1}";
		object MatchCase = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchWholeWord = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchWildcards = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchSoundsLike = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchAllWordForms = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Forward = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Wrap = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Format = RuntimeHelpers.GetObjectValue(Missing.Value);
		object ReplaceWith = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Replace = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchKashida = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
		find.Execute(ref FindText, ref MatchCase, ref MatchWholeWord, ref MatchWildcards, ref MatchSoundsLike, ref MatchAllWordForms, ref Forward, ref Wrap, ref Format, ref ReplaceWith, ref Replace, ref MatchKashida, ref MatchDiacritics, ref MatchAlefHamza, ref MatchControl);
		_ = null;
		if (!oWord.Selection.Find.Found)
		{
			return;
		}
		short num = 0;
		checked
		{
			if (Public2.PRJ.SETT.FlagLastRezManyLimits)
			{
				int num2 = 0;
				do
				{
					if (Public2.PRJ.SETT.HorizCutLimits[num2])
					{
						num++;
					}
					num2++;
				}
				while (num2 <= 3);
			}
			else
			{
				int upperBound = Public2.PRJ.HP.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					if (Public2.PRJ.HP[i].Activ)
					{
						num++;
					}
				}
			}
			if (num == 0)
			{
				return;
			}
			wDrawHorizPlane().Save(Module1.FolderTMP + "\\Horiz.jpg", ImageFormat.Jpeg);
			oWord.Selection.Range.Select();
			InlineShapes inlineShapes = oWord.Selection.InlineShapes;
			string fileName = Module1.FolderTMP + "\\Horiz.jpg";
			MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
			inlineShapes.AddPicture(fileName, ref MatchControl, ref MatchAlefHamza, ref MatchDiacritics);
			if (Public2.PRJ.SETT.FlagLastRezManyLimits)
			{
				Tables tables = oDoc.Tables;
				Range range = oWord.Selection.Range;
				int numRows = num * 2;
				MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
				Table table = tables.Add(range, numRows, 2, ref MatchDiacritics, ref MatchAlefHamza);
				table.Range.Font.Size = 10f;
				table.Range.Font.Name = "Times New Roman";
				table.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
				table.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
				table.Range.ParagraphFormat.SpaceBefore = 0f;
				table.Range.ParagraphFormat.SpaceAfter = 0f;
				table.Columns[1].Width = oWord.MillimetersToPoints(10f);
				table.Columns[2].Width = oWord.MillimetersToPoints(150f);
				num = 1;
				int num3 = 0;
				do
				{
					if (Public2.PRJ.SETT.HorizCutLimits[num3])
					{
						unchecked
						{
							table.Cell(num, 1).Shading.BackgroundPatternColor = (WdColor)checked(65536 * Public2.PRJ.SETT.LimitColor[num3].B + 256 * Public2.PRJ.SETT.LimitColor[num3].G + Public2.PRJ.SETT.LimitColor[num3].R);
							table.Cell(num, 2).Range.Text = "- " + Public2.PRJ.SETT.LimitDescription[num3];
						}
						table.Rows[num + 1].Height = oWord.MillimetersToPoints(1f);
						table.Rows[num + 1].HeightRule = WdRowHeightRule.wdRowHeightExactly;
						num += 2;
					}
					num3++;
				}
				while (num3 <= 3);
				table.Range.Select();
				Selection selection = oWord.Selection;
				MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
				selection.MoveDown(ref MatchAlefHamza, ref MatchDiacritics, ref MatchControl);
				return;
			}
			bool flag = false;
			int upperBound2 = Public2.PRJ.HP.GetUpperBound(0);
			for (int j = 0; j <= upperBound2; j++)
			{
				if (Public2.PRJ.HP[j].LineType > 0)
				{
					flag = true;
				}
			}
			Table table2;
			if (flag)
			{
				Tables tables2 = oDoc.Tables;
				Range range2 = oWord.Selection.Range;
				int numRows2 = num * 2;
				MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
				table2 = tables2.Add(range2, numRows2, 3, ref MatchControl, ref MatchDiacritics);
			}
			else
			{
				Tables tables3 = oDoc.Tables;
				Range range3 = oWord.Selection.Range;
				int numRows3 = num * 2;
				MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
				table2 = tables3.Add(range3, numRows3, 2, ref MatchDiacritics, ref MatchControl);
			}
			table2.Range.Font.Size = 10f;
			table2.Range.Font.Name = "Times New Roman";
			table2.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
			table2.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
			table2.Range.ParagraphFormat.SpaceBefore = 0f;
			table2.Range.ParagraphFormat.SpaceAfter = 0f;
			table2.Columns[1].Width = oWord.MillimetersToPoints(10f);
			if (flag)
			{
				table2.Columns[2].Width = oWord.MillimetersToPoints(28f);
				table2.Columns[3].Width = oWord.MillimetersToPoints(80f);
			}
			else
			{
				table2.Columns[2].Width = oWord.MillimetersToPoints(80f);
			}
			num = 1;
			int upperBound3 = Public2.PRJ.HP.GetUpperBound(0);
			for (int k = 0; k <= upperBound3; k++)
			{
				if (Public2.PRJ.HP[k].Activ)
				{
					unchecked
					{
						table2.Cell(num, 1).Shading.BackgroundPatternColor = (WdColor)checked(65536 * Public2.PRJ.HP[k].Color.B + 256 * Public2.PRJ.HP[k].Color.G + Public2.PRJ.HP[k].Color.R);
						if (flag)
						{
							InlineShapes inlineShapes2 = table2.Cell(num, 2).Range.InlineShapes;
							string fileName2 = System.Windows.Forms.Application.StartupPath + "\\template\\" + Conversions.ToString(Public2.PRJ.HP[k].LineType) + ".jpg";
							MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
							MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
							MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
							inlineShapes2.AddPicture(fileName2, ref MatchControl, ref MatchDiacritics, ref MatchAlefHamza);
							table2.Cell(num, 3).Range.Text = "- " + Localization.LocMSG(92) + " " + Conversions.ToString(Public2.PRJ.HP[k].Height) + " m";
						}
						else
						{
							table2.Cell(num, 2).Range.Text = "- " + Localization.LocMSG(92) + " " + Conversions.ToString(Public2.PRJ.HP[k].Height) + " m";
						}
					}
					table2.Rows[num + 1].Height = oWord.MillimetersToPoints(1f);
					table2.Rows[num + 1].HeightRule = WdRowHeightRule.wdRowHeightExactly;
					num += 2;
				}
			}
			table2.Range.Select();
			Selection selection2 = oWord.Selection;
			MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
			selection2.MoveDown(ref MatchAlefHamza, ref MatchDiacritics, ref MatchControl);
		}
	}

	public static void WordAddHorizCutA3(ref Microsoft.Office.Interop.Word.Application oWord, ref Document oDoc)
	{
		Find find = oWord.Selection.Find;
		find.Forward = true;
		find.ClearFormatting();
		find.MatchWholeWord = true;
		find.MatchCase = false;
		find.Wrap = WdFindWrap.wdFindContinue;
		object FindText = "{HorizontalCutsА3_1}";
		object MatchCase = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchWholeWord = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchWildcards = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchSoundsLike = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchAllWordForms = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Forward = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Wrap = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Format = RuntimeHelpers.GetObjectValue(Missing.Value);
		object ReplaceWith = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Replace = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchKashida = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
		find.Execute(ref FindText, ref MatchCase, ref MatchWholeWord, ref MatchWildcards, ref MatchSoundsLike, ref MatchAllWordForms, ref Forward, ref Wrap, ref Format, ref ReplaceWith, ref Replace, ref MatchKashida, ref MatchDiacritics, ref MatchAlefHamza, ref MatchControl);
		_ = null;
		if (!oWord.Selection.Find.Found)
		{
			return;
		}
		short num = 0;
		checked
		{
			if (Public2.PRJ.SETT.FlagLastRezManyLimits)
			{
				int num2 = 0;
				do
				{
					if (Public2.PRJ.SETT.HorizCutLimits[num2])
					{
						num++;
					}
					num2++;
				}
				while (num2 <= 3);
			}
			else
			{
				int upperBound = Public2.PRJ.HP.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					if (Public2.PRJ.HP[i].Activ)
					{
						num++;
					}
				}
			}
			if (num == 0)
			{
				return;
			}
			oWord.Selection.Range.Select();
			Bitmap bitmap = wDrawHorizPlane();
			bitmap.Save(Module1.FolderTMP + "\\Horiz.jpg", ImageFormat.Jpeg);
			double num3 = (double)bitmap.Width / M2Pix * 1000.0;
			double num4 = (double)bitmap.Height / M2Pix * 1000.0;
			double num5 = 350.0;
			double num6 = 285.0;
			double val = num3 / num5;
			double val2 = num4 / num6;
			double num7 = Math.Max(val, val2);
			double num8 = 5000.0;
			if (num7 <= 500.0)
			{
				num8 = 500.0;
			}
			else if (num7 <= 1000.0)
			{
				num8 = 1000.0;
			}
			else if (num7 <= 2000.0)
			{
				num8 = 2000.0;
			}
			double num9 = num3 / num8;
			double num10 = num4 / num8;
			InlineShapes inlineShapes = oWord.Selection.InlineShapes;
			string fileName = Module1.FolderTMP + "\\Horiz.jpg";
			MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
			Shape shape = inlineShapes.AddPicture(fileName, ref MatchControl, ref MatchAlefHamza, ref MatchDiacritics).ConvertToShape();
			shape.LockAspectRatio = MsoTriState.msoFalse;
			shape.Width = oWord.MillimetersToPoints((float)num9);
			shape.Height = oWord.MillimetersToPoints((float)num10);
			shape.RelativeHorizontalPosition = WdRelativeHorizontalPosition.wdRelativeHorizontalPositionMargin;
			shape.RelativeVerticalPosition = WdRelativeVerticalPosition.wdRelativeVerticalPositionMargin;
			shape.Left = oWord.MillimetersToPoints(5f);
			shape.Top = -999995f;
			shape.ZOrder(MsoZOrderCmd.msoSendBehindText);
			oWord.Selection.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
			oWord.Selection.Range.ParagraphFormat.LeftIndent = oWord.MillimetersToPoints(300f);
			oWord.Selection.Range.ParagraphFormat.SpaceBefore = oWord.MillimetersToPoints(20f);
			oWord.Selection.Range.ParagraphFormat.SpaceAfter = 0f;
			oWord.Selection.Range.HighlightColorIndex = WdColorIndex.wdWhite;
			oWord.Selection.TypeText("Scale M1:" + Conversions.ToString(num8));
			if (Public2.PRJ.SETT.FlagLastRezManyLimits)
			{
				Tables tables = oDoc.Tables;
				Range range = oWord.Selection.Range;
				int numRows = num * 2;
				MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
				Table table = tables.Add(range, numRows, 2, ref MatchDiacritics, ref MatchAlefHamza);
				table.Rows.RelativeHorizontalPosition = WdRelativeHorizontalPosition.wdRelativeHorizontalPositionMargin;
				table.Rows.RelativeVerticalPosition = WdRelativeVerticalPosition.wdRelativeVerticalPositionMargin;
				table.Rows.HorizontalPosition = oWord.MillimetersToPoints(300f);
				table.Rows.VerticalPosition = oWord.MillimetersToPoints(40f);
				table.Range.Font.Size = 10f;
				table.Range.Font.Name = "Times New Roman";
				table.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
				table.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
				table.Range.ParagraphFormat.SpaceBefore = 0f;
				table.Range.ParagraphFormat.SpaceAfter = 0f;
				table.Columns[1].Width = oWord.MillimetersToPoints(10f);
				table.Columns[2].Width = oWord.MillimetersToPoints(45f);
				num = 1;
				int num11 = 0;
				do
				{
					if (Public2.PRJ.SETT.HorizCutLimits[num11])
					{
						unchecked
						{
							table.Cell(num, 1).Shading.BackgroundPatternColor = (WdColor)checked(65536 * Public2.PRJ.SETT.LimitColor[num11].B + 256 * Public2.PRJ.SETT.LimitColor[num11].G + Public2.PRJ.SETT.LimitColor[num11].R);
							table.Cell(num, 2).Range.Text = "- " + Public2.PRJ.SETT.LimitDescription[num11];
						}
						table.Rows[num + 1].Height = oWord.MillimetersToPoints(1f);
						table.Rows[num + 1].HeightRule = WdRowHeightRule.wdRowHeightExactly;
						num += 2;
					}
					num11++;
				}
				while (num11 <= 3);
				table.Range.Select();
				Selection selection = oWord.Selection;
				MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
				selection.MoveDown(ref MatchAlefHamza, ref MatchDiacritics, ref MatchControl);
				return;
			}
			bool flag = false;
			int upperBound2 = Public2.PRJ.HP.GetUpperBound(0);
			for (int j = 0; j <= upperBound2; j++)
			{
				if (Public2.PRJ.HP[j].LineType > 0)
				{
					flag = true;
				}
			}
			Table table2;
			if (flag)
			{
				Tables tables2 = oDoc.Tables;
				Range range2 = oWord.Selection.Range;
				int numRows2 = num * 2;
				MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
				table2 = tables2.Add(range2, numRows2, 3, ref MatchControl, ref MatchDiacritics);
			}
			else
			{
				Tables tables3 = oDoc.Tables;
				Range range3 = oWord.Selection.Range;
				int numRows3 = num * 2;
				MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
				table2 = tables3.Add(range3, numRows3, 2, ref MatchDiacritics, ref MatchControl);
			}
			table2.Rows.RelativeHorizontalPosition = WdRelativeHorizontalPosition.wdRelativeHorizontalPositionMargin;
			table2.Rows.RelativeVerticalPosition = WdRelativeVerticalPosition.wdRelativeVerticalPositionMargin;
			table2.Rows.HorizontalPosition = oWord.MillimetersToPoints(300f);
			table2.Rows.VerticalPosition = oWord.MillimetersToPoints(40f);
			table2.Range.Font.Size = 10f;
			table2.Range.Font.Name = "Times New Roman";
			table2.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
			table2.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
			table2.Range.ParagraphFormat.SpaceBefore = 0f;
			table2.Range.ParagraphFormat.SpaceAfter = 0f;
			table2.Columns[1].Width = oWord.MillimetersToPoints(10f);
			if (flag)
			{
				table2.Columns[2].Width = oWord.MillimetersToPoints(28f);
				table2.Columns[3].Width = oWord.MillimetersToPoints(45f);
			}
			else
			{
				table2.Columns[2].Width = oWord.MillimetersToPoints(45f);
			}
			num = 1;
			int upperBound3 = Public2.PRJ.HP.GetUpperBound(0);
			for (int k = 0; k <= upperBound3; k++)
			{
				if (Public2.PRJ.HP[k].Activ)
				{
					unchecked
					{
						table2.Cell(num, 1).Shading.BackgroundPatternColor = (WdColor)checked(65536 * Public2.PRJ.HP[k].Color.B + 256 * Public2.PRJ.HP[k].Color.G + Public2.PRJ.HP[k].Color.R);
						if (flag)
						{
							InlineShapes inlineShapes2 = table2.Cell(num, 2).Range.InlineShapes;
							string fileName2 = System.Windows.Forms.Application.StartupPath + "\\template\\" + Conversions.ToString(Public2.PRJ.HP[k].LineType) + ".jpg";
							MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
							MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
							MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
							inlineShapes2.AddPicture(fileName2, ref MatchControl, ref MatchDiacritics, ref MatchAlefHamza);
							table2.Cell(num, 3).Range.Text = "- " + Localization.LocMSG(92) + " " + Conversions.ToString(Public2.PRJ.HP[k].Height) + " m";
						}
						else
						{
							table2.Cell(num, 2).Range.Text = "- " + Localization.LocMSG(92) + " " + Conversions.ToString(Public2.PRJ.HP[k].Height) + " m";
						}
					}
					table2.Rows[num + 1].Height = oWord.MillimetersToPoints(1f);
					table2.Rows[num + 1].HeightRule = WdRowHeightRule.wdRowHeightExactly;
					num += 2;
				}
			}
			table2.Range.Select();
			Selection selection2 = oWord.Selection;
			MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
			selection2.MoveDown(ref MatchAlefHamza, ref MatchDiacritics, ref MatchControl);
		}
	}

	public static void WordAddVertCut1(ref Microsoft.Office.Interop.Word.Application oWord, ref Document oDoc)
	{
		Find find = oWord.Selection.Find;
		find.Forward = true;
		find.ClearFormatting();
		find.MatchWholeWord = true;
		find.MatchCase = false;
		find.Wrap = WdFindWrap.wdFindContinue;
		object FindText = "{VerticalCuts_1}";
		object MatchCase = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchWholeWord = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchWildcards = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchSoundsLike = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchAllWordForms = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Forward = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Wrap = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Format = RuntimeHelpers.GetObjectValue(Missing.Value);
		object ReplaceWith = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Replace = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchKashida = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
		find.Execute(ref FindText, ref MatchCase, ref MatchWholeWord, ref MatchWildcards, ref MatchSoundsLike, ref MatchAllWordForms, ref Forward, ref Wrap, ref Format, ref ReplaceWith, ref Replace, ref MatchKashida, ref MatchDiacritics, ref MatchAlefHamza, ref MatchControl);
		_ = null;
		if (!oWord.Selection.Find.Found)
		{
			return;
		}
		short num = 0;
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.VP))
			{
				int upperBound = Public2.PRJ.VP.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					if (Public2.PRJ.VP[i].Activ & (Public2.PRJ.VP[i].LastL1 > 0f))
					{
						num++;
					}
				}
			}
			if (num < 1)
			{
				return;
			}
			oWord.Selection.Range.Select();
			short num2 = 30;
			short num3 = 30;
			short num4 = 50;
			short num5 = 60;
			Pen pen = new Pen(Color.Black, 2f);
			new Pen(Color.Red, 1f);
			System.Drawing.Font font = new System.Drawing.Font("Arial", 14f, FontStyle.Regular);
			SolidBrush brush = new SolidBrush(Color.Black);
			Pen pen2 = new Pen(Color.Black, 1f);
			pen2.DashStyle = DashStyle.Dash;
			Pen pen3 = new Pen(Color.Red, 1f);
			pen3.DashStyle = DashStyle.Dash;
			SizeF sizeF = default;
			short num6 = 0;
			int upperBound2 = Public2.PRJ.VP.GetUpperBound(0);
			float dX = default;
			float X = default;
			short num41 = default;
			for (int j = 0; j <= upperBound2; j++)
			{
				if (!(Public2.PRJ.VP[j].Activ & (Public2.PRJ.VP[j].LastL1 > 0f)))
				{
					continue;
				}
				num6++;
				Bitmap bitmap = new Bitmap(1000, 1000);
				Graphics graphics = Graphics.FromImage(bitmap);
				graphics.Clear(Color.White);
				double num7 = Public2.PRJ.VP[j].LastL1 + Public2.PRJ.VP[j].LastL2;
				short num8 = (short)(bitmap.Width - num4 - num5);
				short num9 = (short)(bitmap.Height - num2 - num3);
				double num10 = (double)num8 / num7;
				double num11 = (float)num9 / (Public2.PRJ.VP[j].LastH1 + Public2.PRJ.VP[j].LastH2);
				if (!Information.IsNothing(Public2.PRJ.TXT))
				{
					int upperBound3 = Public2.PRJ.TXT.GetUpperBound(0);
					for (int k = 0; k <= upperBound3; k++)
					{
						if (!(Form1_Public.MastDimension(Public2.PRJ.TXT[k].Text) > 0f))
						{
							continue;
						}
						Public2.CoordMeters coordMeters = CalcCoords.ConvWebMercMetersToMetersRound(Public2.PRJ.TXT[k].CoordWM);
						float num12 = (float)(Public2.PRJ.VP[j].Coord1M.Xm - coordMeters.Xm);
						float num13 = (float)(Public2.PRJ.VP[j].Coord1M.Ym - coordMeters.Ym);
						float num14 = (float)Math.Sqrt(num12 * num12 + num13 * num13);
						float num15 = Form1_Public.MastDimension(Public2.PRJ.TXT[k].Text);
						if ((num14 < num15) | Module1.PointLineD(Public2.PRJ.VP[j].Coord1M.Xm, Public2.PRJ.VP[j].Coord1M.Ym, Public2.PRJ.VP[j].Coord2M.Xm, Public2.PRJ.VP[j].Coord2M.Ym, coordMeters.Xm, coordMeters.Ym, num15))
						{
							float xo = (float)((double)num4 + (double)num14 * num10);
							float yo = (float)((double)(short)unchecked(num2 + num9) - (double)(Triangul.FindElevWithBldPlusHbld(Public2.PRJ.TXT[k].CoordWM) - Public2.PRJ.VP[j].Elev0 + Public2.PRJ.VP[j].LastH2) * num11);
							if (num14 < num15)
							{
								xo = num4;
							}
							Form1_Public.DrawMast2D(Public2.PRJ.TXT[k].Text, xo, yo, num10, num11, graphics);
						}
					}
					graphics.FillRectangle(new SolidBrush(Color.White), 0, 0, num4, bitmap.Height);
				}
				if (!Information.IsNothing(Public2.PRJ.VP[j].Elev))
				{
					double num16 = (double)num8 / (double)Public2.PRJ.VP[j].Elev.GetUpperBound(0);
					PointF[] array = new PointF[Public2.PRJ.VP[j].Elev.GetUpperBound(0) + 3 + 1];
					int upperBound4 = Public2.PRJ.VP[j].Elev.GetUpperBound(0);
					for (int l = 0; l <= upperBound4; l++)
					{
						array[l].X = (float)((double)num4 + (double)l * num16);
						float num17 = (float)((double)(short)unchecked(num2 + num9) - (double)(Public2.PRJ.VP[j].Elev[l] - Public2.PRJ.VP[j].Elev0 + Public2.PRJ.VP[j].LastH2) * num11);
						if (num17 < (float)(short)unchecked(num2 + num9))
						{
							array[l].Y = num17;
						}
						else
						{
							array[l].Y = (short)unchecked(num2 + num9);
						}
					}
					if (!Information.IsNothing(Public2.PRJ.VP[j].Bld))
					{
						int upperBound5 = Public2.PRJ.VP[j].Bld.GetUpperBound(0);
						for (int m = 0; m <= upperBound5; m++)
						{
							short num18 = (short)Math.Floor((double)Public2.PRJ.VP[j].Bld[m].L1 / num7 * (double)Public2.PRJ.VP[j].Elev.GetUpperBound(0));
							short num19 = (short)Math.Ceiling((double)Public2.PRJ.VP[j].Bld[m].L2 / num7 * (double)Public2.PRJ.VP[j].Elev.GetUpperBound(0));
							short num20 = num18;
							short num21 = num19;
							for (short num22 = num20; num22 <= num21; num22 = (short)unchecked(num22 + 1))
							{
								array[num22].Y = (float)((double)(short)unchecked(num2 + num9) - (double)(Public2.PRJ.VP[j].Bld[m].Fundation - Public2.PRJ.VP[j].Elev0 + Public2.PRJ.VP[j].LastH2) * num11);
							}
							array[num18].X = (float)((double)num4 + (double)Public2.PRJ.VP[j].Bld[m].L1 / num7 * (double)Public2.PRJ.VP[j].Elev.GetUpperBound(0) * num16);
							array[num19].X = (float)((double)num4 + (double)Public2.PRJ.VP[j].Bld[m].L2 / num7 * (double)Public2.PRJ.VP[j].Elev.GetUpperBound(0) * num16);
						}
					}
					array[array.GetUpperBound(0) - 2].X = array[array.GetUpperBound(0) - 3].X;
					array[array.GetUpperBound(0) - 2].Y = (short)unchecked(num2 + num9);
					array[array.GetUpperBound(0) - 1].X = num4;
					array[array.GetUpperBound(0) - 1].Y = (short)unchecked(num2 + num9);
					array[array.GetUpperBound(0)].X = array[0].X;
					array[array.GetUpperBound(0)].Y = array[0].Y;
					graphics.FillPolygon(new SolidBrush(Color.FromArgb(220, 220, 220)), array);
					graphics.DrawLines(pen, array);
					if (Public2.PRJ.SETT.ShowPlus2m)
					{
						Pen pen4 = new Pen(Color.Red, 1f);
						pen4.DashStyle = DashStyle.Custom;
						pen4.DashPattern = new float[2] { 8f, 4f };
						array = new PointF[Public2.PRJ.VP[j].Elev.GetUpperBound(0) + 1];
						int upperBound6 = Public2.PRJ.VP[j].Elev.GetUpperBound(0);
						for (int n = 0; n <= upperBound6; n++)
						{
							array[n].X = (float)((double)num4 + (double)n * num16);
							float num23 = (float)((double)(short)unchecked(num2 + num9) - (double)(Public2.PRJ.VP[j].Elev[n] - Public2.PRJ.VP[j].Elev0 + Public2.PRJ.VP[j].LastH2 + Public2.PRJ.SETT.LineHightAboveTerrain) * num11);
							if (num23 < (float)(short)unchecked(num2 + num9))
							{
								array[n].Y = num23;
							}
							else
							{
								array[n].Y = (short)unchecked(num2 + num9);
							}
						}
						if (!Information.IsNothing(Public2.PRJ.VP[j].Bld))
						{
							int upperBound7 = Public2.PRJ.VP[j].Bld.GetUpperBound(0);
							for (int num24 = 0; num24 <= upperBound7; num24++)
							{
								float num25 = (float)Math.Floor((double)Public2.PRJ.VP[j].Bld[num24].L1 / num7 * (double)Public2.PRJ.VP[j].Elev.GetUpperBound(0));
								float num26 = (float)Math.Ceiling((double)Public2.PRJ.VP[j].Bld[num24].L2 / num7 * (double)Public2.PRJ.VP[j].Elev.GetUpperBound(0));
								float num27 = (float)((double)num4 + (double)Public2.PRJ.VP[j].Bld[num24].L1 / num7 * (double)Public2.PRJ.VP[j].Elev.GetUpperBound(0) * num16);
								float num28 = (float)((double)num4 + (double)Public2.PRJ.VP[j].Bld[num24].L2 / num7 * (double)Public2.PRJ.VP[j].Elev.GetUpperBound(0) * num16);
								float y = (float)((double)(short)unchecked(num2 + num9) - (double)(Public2.PRJ.VP[j].Bld[num24].Fundation - Public2.PRJ.VP[j].Elev0 + Public2.PRJ.VP[j].LastH2 + Public2.PRJ.VP[j].Bld[num24].H + Public2.PRJ.SETT.LineHightAboveTerrain) * num11);
								float num29 = num26;
								for (float num30 = num25; num30 <= num29; num30++)
								{
									int upperBound8 = array.GetUpperBound(0);
									for (int num31 = 0; num31 <= upperBound8; num31++)
									{
										if ((array[num31].X > num27) & (array[num31].X < num28))
										{
											array[num31].Y = y;
										}
									}
								}
							}
							List<PointF> list = new List<PointF>();
							list = array.ToList();
							int upperBound9 = Public2.PRJ.VP[j].Bld.GetUpperBound(0);
							for (int num32 = 0; num32 <= upperBound9; num32++)
							{
								float num33 = (float)((double)num4 + (double)Public2.PRJ.VP[j].Bld[num32].L1 / num7 * (double)Public2.PRJ.VP[j].Elev.GetUpperBound(0) * num16);
								float num34 = (float)((double)num4 + (double)Public2.PRJ.VP[j].Bld[num32].L2 / num7 * (double)Public2.PRJ.VP[j].Elev.GetUpperBound(0) * num16);
								float y2 = (float)((double)(short)unchecked(num2 + num9) - (double)(Public2.PRJ.VP[j].Bld[num32].Fundation - Public2.PRJ.VP[j].Elev0 + Public2.PRJ.VP[j].LastH2 + Public2.PRJ.SETT.LineHightAboveTerrain) * num11);
								float y3 = (float)((double)(short)unchecked(num2 + num9) - (double)(Public2.PRJ.VP[j].Bld[num32].Fundation - Public2.PRJ.VP[j].Elev0 + Public2.PRJ.VP[j].LastH2 + Public2.PRJ.VP[j].Bld[num32].H + Public2.PRJ.SETT.LineHightAboveTerrain) * num11);
								list.Add(new PointF(num33, y2));
								list.Add(new PointF((float)((double)num33 + 0.001), y3));
								list.Add(new PointF((float)((double)num34 - 0.001), y3));
								list.Add(new PointF(num34, y2));
							}
							list.Sort([SpecialName] (PointF a1, PointF b1) => a1.X.CompareTo(b1.X));
							array = list.ToArray();
						}
						graphics.DrawLines(pen4, array);
					}
				}
				if (!Information.IsNothing(Public2.PRJ.VP[j].Bld))
				{
					Point[] array2 = new Point[5];
					int upperBound10 = Public2.PRJ.VP[j].Bld.GetUpperBound(0);
					for (int num35 = 0; num35 <= upperBound10; num35++)
					{
						if ((short)Math.Round((double)(Public2.PRJ.VP[j].Bld[num35].Fundation + Public2.PRJ.VP[j].Bld[num35].H - Public2.PRJ.VP[j].Elev0 + Public2.PRJ.VP[j].LastH2) * num11) > 0)
						{
							array2[0].X = (int)Math.Round((double)num4 + (double)Public2.PRJ.VP[j].Bld[num35].L1 * num10);
							array2[0].Y = (int)Math.Round((double)(short)unchecked(num2 + num9) - (double)(Public2.PRJ.VP[j].Bld[num35].Fundation - Public2.PRJ.VP[j].Elev0 + Public2.PRJ.VP[j].LastH2) * num11);
							array2[1].X = array2[0].X;
							array2[1].Y = (int)Math.Round((double)(short)unchecked(num2 + num9) - (double)(Public2.PRJ.VP[j].Bld[num35].Fundation + Public2.PRJ.VP[j].Bld[num35].H - Public2.PRJ.VP[j].Elev0 + Public2.PRJ.VP[j].LastH2) * num11);
							array2[2].X = (int)Math.Round((double)num4 + (double)Public2.PRJ.VP[j].Bld[num35].L2 * num10);
							array2[2].Y = array2[1].Y;
							array2[3].X = array2[2].X;
							array2[3].Y = array2[0].Y;
							array2[4].X = array2[0].X;
							array2[4].Y = array2[0].Y;
							graphics.FillPolygon(new SolidBrush(Color.FromArgb(180, 180, 180)), array2);
							graphics.DrawLines(pen, array2);
							string text = "";
							if (Public2.PRJ.SETT.ShowBuildingText)
							{
								text = text + Public2.PRJ.VP[j].Bld[num35].Text + "\r\n";
							}
							if (Public2.PRJ.SETT.ShowBuildingHeight)
							{
								text = text + Conversions.ToString(Public2.PRJ.VP[j].Bld[num35].H) + "m\r\n";
							}
							if (Public2.PRJ.SETT.ShowBuildingFoundation)
							{
								text = text + Conversions.ToString(Public2.PRJ.VP[j].Bld[num35].Fundation) + "m\r\n";
							}
							if (text.Length > 0)
							{
								text = text.Substring(0, text.Length - 1);
								sizeF = graphics.MeasureString(text, font);
								graphics.DrawString(text, font, Brushes.Black, (float)((double)(array2[0].X + array2[2].X) / 2.0 - (double)(sizeF.Width / 2f)), (float)((double)(array2[0].Y + array2[1].Y) / 2.0 - (double)(sizeF.Height / 2f)));
							}
						}
					}
				}
				if (!Information.IsNothing(Public2.PRJ.VP[j].PLNs))
				{
					int num36 = 0;
					do
					{
						if (Public2.PRJ.SETT.VertCutLimits[num36] && !Information.IsNothing(Public2.PRJ.VP[j].PLNs[num36]))
						{
							double num37 = (double)num8 / (double)Public2.PRJ.VP[j].Size;
							double num38 = (double)num9 / (double)Public2.PRJ.VP[j].Size;
							Region region = new Region(new GraphicsPath(FillMode.Winding));
							int upperBound11 = Public2.PRJ.VP[j].PLNs[num36].GetUpperBound(0);
							for (int num39 = 0; num39 <= upperBound11; num39++)
							{
								Point[] array3 = new Point[Public2.PRJ.VP[j].PLNs[num36][num39].GetUpperBound(0) + 1];
								int upperBound12 = Public2.PRJ.VP[j].PLNs[num36][num39].GetUpperBound(0);
								for (int num40 = 0; num40 <= upperBound12; num40++)
								{
									array3[num40].X = (int)Math.Round((double)num4 + (double)Public2.PRJ.VP[j].PLNs[num36][num39][num40].X * num37);
									array3[num40].Y = (int)Math.Round((double)(short)unchecked(num2 + num9) - (double)Public2.PRJ.VP[j].PLNs[num36][num39][num40].Y * num38);
								}
								if (Public2.PRJ.SETT.TransparFillVertCut > 0)
								{
									GraphicsPath graphicsPath = new GraphicsPath(FillMode.Winding);
									graphicsPath.AddLines(array3);
									region.Xor(graphicsPath);
								}
								Pen pen5 = new Pen(Public2.PRJ.SETT.LimitColor[num36], unchecked((int)Public2.PRJ.SETT.LineWidthVertCut));
								pen5.LineJoin = LineJoin.Bevel;
								graphics.DrawLines(pen5, array3);
							}
							if (Public2.PRJ.SETT.TransparFillVertCut > 0)
							{
								graphics.FillRegion(new SolidBrush(Color.FromArgb((int)Math.Round((double)unchecked((int)Public2.PRJ.SETT.TransparFillVertCut) * 25.5), Public2.PRJ.SETT.LimitColor[num36])), region);
							}
						}
						num36++;
					}
					while (num36 <= 3);
				}
				float xmin = 0f - Public2.PRJ.VP[j].LastL2;
				float lastL = Public2.PRJ.VP[j].LastL1;
				int N = num41;
				Module1.RazmetkaOSI(xmin, lastL, 8, 13, ref N, ref dX, ref X);
				num41 = (short)N;
				N = num41 - 1;
				for (int num42 = 0; num42 <= N; num42++)
				{
					int num43 = (int)Math.Round((double)num4 + (double)(X + Public2.PRJ.VP[j].LastL2) * num10);
					graphics.DrawLine(pen2, num43, num2, num43, (short)unchecked(num2 + num9));
					sizeF = graphics.MeasureString(Conversions.ToString(X), font);
					graphics.DrawString(Conversions.ToString(X), font, brush, (float)num43 - sizeF.Width / 2f, (short)unchecked(num2 + num9) + 2);
					X += dX;
				}
				graphics.DrawString("R, m", font, brush, (short)unchecked(num4 + num8) + 3, (short)unchecked(num2 + num9) - 20);
				float xmin2 = 0f - Public2.PRJ.VP[j].LastH2;
				float lastH = Public2.PRJ.VP[j].LastH1;
				int N2 = num41;
				Module1.RazmetkaOSI(xmin2, lastH, 8, 13, ref N2, ref dX, ref X);
				num41 = (short)N2;
				N2 = num41 - 1;
				for (int num44 = 0; num44 <= N2; num44++)
				{
					int num43 = (int)Math.Round((double)(short)unchecked(num2 + num9) - (double)(X + Public2.PRJ.VP[j].LastH2) * num11);
					graphics.DrawLine(pen2, num4, num43, (short)unchecked(num4 + num8), num43);
					sizeF = graphics.MeasureString(Conversions.ToString(X), font);
					graphics.DrawString(Conversions.ToString(X), font, brush, (float)num4 - sizeF.Width - 2f, (float)num43 - sizeF.Height / 2f);
					X += dX;
				}
				graphics.DrawString("H, m", font, brush, 5f, num2 - 30);
				graphics.DrawLine(new Pen(Color.Black, 2f), num4, num2, (short)unchecked(num4 + num8), num2);
				graphics.DrawLine(new Pen(Color.Black, 2f), (short)unchecked(num4 + num8), num2, (short)unchecked(num4 + num8), (short)unchecked(num2 + num9));
				graphics.DrawLine(new Pen(Color.Black, 2f), (short)unchecked(num4 + num8), (short)unchecked(num2 + num9), num4, (short)unchecked(num2 + num9));
				graphics.DrawLine(new Pen(Color.Black, 2f), num4, (short)unchecked(num2 + num9), num4, num2);
				if (Public2.PRJ.SETT.ShowKPonVert && !Information.IsNothing(Public2.PRJ.CPG))
				{
					int upperBound13 = Public2.PRJ.CPG.GetUpperBound(0);
					for (int num45 = 0; num45 <= upperBound13; num45++)
					{
						if (!Public2.PRJ.CPG[num45].Active || Information.IsNothing(Public2.PRJ.CPG[num45].CP))
						{
							continue;
						}
						int upperBound14 = Public2.PRJ.CPG[num45].CP.GetUpperBound(0);
						for (int num46 = 0; num46 <= upperBound14; num46++)
						{
							if (!Module1.PointLineD(Public2.PRJ.VP[j].Coord1M.Xm, Public2.PRJ.VP[j].Coord1M.Ym, Public2.PRJ.VP[j].Coord2M.Xm, Public2.PRJ.VP[j].Coord2M.Ym, Public2.PRJ.CPG[num45].CP[num46].CoordM.Xm, Public2.PRJ.CPG[num45].CP[num46].CoordM.Ym, 1.0))
							{
								continue;
							}
							float num47 = (float)Math.Sqrt(Math.Pow(Public2.PRJ.CPG[num45].CP[num46].CoordM.Xm - Public2.PRJ.VP[j].Coord1M.Xm, 2.0) + Math.Pow(Public2.PRJ.CPG[num45].CP[num46].CoordM.Ym - Public2.PRJ.VP[j].Coord1M.Ym, 2.0));
							short num48 = (short)Math.Round((double)num4 + (double)num47 * num10 - 7.0);
							short num49 = (short)Math.Round((double)num47 / num7 * (double)Public2.PRJ.VP[j].Elev.GetUpperBound(0));
							if (!((num49 >= 0) & (num49 <= Public2.PRJ.VP[j].Elev.GetUpperBound(0))))
							{
								continue;
							}
							float num50 = Public2.PRJ.VP[j].Elev[(int)Math.Round((double)num47 / num7 * (double)Public2.PRJ.VP[j].Elev.GetUpperBound(0))];
							if (Operators.CompareString(Public2.PRJ.CPG[num45].CP[num46].Location, "Building", TextCompare: false) == 0 && !Information.IsNothing(Public2.PRJ.VP[j].Bld))
							{
								int upperBound15 = Public2.PRJ.VP[j].Bld.GetUpperBound(0);
								for (int num51 = 0; num51 <= upperBound15; num51++)
								{
									if ((num47 > Public2.PRJ.VP[j].Bld[num51].L1) & (num47 < Public2.PRJ.VP[j].Bld[num51].L2))
									{
										num50 = Public2.PRJ.VP[j].Bld[num51].Fundation;
										break;
									}
								}
							}
							int upperBound16 = Public2.PRJ.CPG[num45].CP[num46].H.GetUpperBound(0);
							for (int num52 = 0; num52 <= upperBound16; num52++)
							{
								short num53 = (short)Math.Round((double)(short)unchecked(num2 + num9) - (double)(Public2.PRJ.CPG[num45].CP[num46].H[num52].H + num50 - Public2.PRJ.VP[j].Elev0 + Public2.PRJ.VP[j].LastH2) * num11 - 7.0);
								graphics.DrawImage(Resources.ring8x8, num48, num53);
								string text2 = Public2.PRJ.SETT.ControlPointPrefix;
								if (Public2.PRJ.SETT.ShowControlPointGroups)
								{
									text2 = text2 + Conversions.ToString(num45 + 1) + ".";
								}
								text2 += Conversions.ToString(num46 + 1);
								if (Public2.PRJ.SETT.ShowControlPointHeight)
								{
									text2 = text2 + "(" + Conversions.ToString(Public2.PRJ.CPG[num45].CP[num46].H[num52].H) + "м)";
								}
								graphics.DrawString(text2, font, Brushes.Blue, num48 + 10, num53 - 10);
							}
						}
					}
				}
				if (Public2.PRJ.SETT.ShowManualPointsZO && !Information.IsNothing(Public2.PRJ.VP[j].ManualPointZO))
				{
					int upperBound17 = Public2.PRJ.VP[j].ManualPointZO.GetUpperBound(0);
					for (int num54 = 0; num54 <= upperBound17; num54++)
					{
						short num55 = (short)Math.Round((double)num4 + (double)(Public2.PRJ.VP[j].ManualPointZO[num54].R + Public2.PRJ.VP[j].LastL2) * num10);
						short num56 = (short)Math.Round((double)(short)unchecked(num2 + num9) - (double)(Public2.PRJ.VP[j].ManualPointZO[num54].H + Public2.PRJ.VP[j].LastH2) * num11);
						graphics.FillEllipse(Brushes.Red, num55 - 3, num56 - 3, 6, 6);
						graphics.DrawLine(new Pen(Color.Red, 1f), (int)Math.Round((double)num4 + (double)Public2.PRJ.VP[j].LastL2 * num10), num56, num55, num56);
						graphics.DrawLine(new Pen(Color.Red, 1f), num55, num2, num55, num56);
						string s = "R=" + Conversions.ToString(Public2.PRJ.VP[j].ManualPointZO[num54].R) + "m\r\nH=" + Conversions.ToString(Public2.PRJ.VP[j].ManualPointZO[num54].H) + "m";
						graphics.DrawString(s, font, Brushes.Red, num55 + 3, num56 - 42);
					}
				}
				if (Public2.PRJ.SETT.ShowManualPointsZO && !Information.IsNothing(Public2.PRJ.VP[j].DimensionZO))
				{
					int upperBound18 = Public2.PRJ.VP[j].DimensionZO.GetUpperBound(0);
					for (int num57 = 0; num57 <= upperBound18; num57++)
					{
						short num58 = (short)Math.Round((double)num4 + (double)(Public2.PRJ.VP[j].DimensionZO[num57].R + Public2.PRJ.VP[j].LastL2) * num10);
						short num59 = (short)Math.Round((double)(short)unchecked(num2 + num9) - (double)(Public2.PRJ.VP[j].DimensionZO[num57].H + Public2.PRJ.VP[j].LastH2) * num11);
						short num60 = (short)Math.Round((double)(short)unchecked(num2 + num9) - (double)(Public2.PRJ.VP[j].DimensionZO[num57].Hz + Public2.PRJ.VP[j].LastH2) * num11);
						graphics.DrawLine(pen, num58, num60, num58, num59);
						graphics.DrawLine(pen, num58, num59, num58 + 4, num59 + 8);
						graphics.DrawLine(pen, num58, num59, num58 - 4, num59 + 8);
						graphics.DrawLine(pen, num58, num60, num58 + 4, num60 - 8);
						graphics.DrawLine(pen, num58, num60, num58 - 4, num60 - 8);
						Size size = graphics.MeasureString(Public2.PRJ.VP[j].DimensionZO[num57].Text, font).ToSize();
						Point point = new Point((int)Math.Round((double)num58 - (double)size.Width / 2.0), (int)Math.Round((double)(short)unchecked(num60 + num59) / 2.0 - (double)size.Height / 2.0));
						graphics.FillRectangle(Brushes.White, new Rectangle(point, size));
						graphics.DrawString(Public2.PRJ.VP[j].DimensionZO[num57].Text, font, Brushes.Black, point);
					}
				}
				if (Public2.PRJ.SETT.ShowMinDistToZO && Public2.PRJ.VP[j].DimensionLowerestZO.R < 9999f)
				{
					short num61 = (short)Math.Round((double)num4 + (double)(Public2.PRJ.VP[j].DimensionLowerestZO.R + Public2.PRJ.VP[j].LastL2) * num10);
					short num62 = (short)Math.Round((double)(short)unchecked(num2 + num9) - (double)(Public2.PRJ.VP[j].DimensionLowerestZO.H + Public2.PRJ.VP[j].LastH2) * num11);
					short num63 = (short)Math.Round((double)(short)unchecked(num2 + num9) - (double)(Public2.PRJ.VP[j].DimensionLowerestZO.Hz + Public2.PRJ.VP[j].LastH2) * num11);
					graphics.DrawLine(pen, num61, num63, num61, num62);
					graphics.DrawLine(pen, num61, num62, num61 + 4, num62 + 8);
					graphics.DrawLine(pen, num61, num62, num61 - 4, num62 + 8);
					graphics.DrawLine(pen, num61, num63, num61 + 4, num63 - 8);
					graphics.DrawLine(pen, num61, num63, num61 - 4, num63 - 8);
					Size size2 = graphics.MeasureString(Public2.PRJ.VP[j].DimensionLowerestZO.Text + "m", font).ToSize();
					Point point2 = new Point((int)Math.Round((double)num61 - (double)size2.Width / 2.0), (int)Math.Round((double)(short)unchecked(num63 + num62) / 2.0 - (double)size2.Height / 2.0));
					graphics.FillRectangle(Brushes.White, new Rectangle(point2, size2));
					graphics.DrawString(Public2.PRJ.VP[j].DimensionLowerestZO.Text + "m", font, Brushes.Black, point2);
				}
				if (Public2.PRJ.SETT.ShowLengthZO & (Public2.PRJ.VP[j].LenghtZO != 9999f))
				{
					short num64 = (short)Math.Round((double)num4 + (double)(Public2.PRJ.VP[j].LenghtZO + Public2.PRJ.VP[j].LastL2) * num10);
					graphics.DrawLine(pen3, num64, num2, num64, (short)unchecked(num2 + num9));
					string text3 = Conversions.ToString(Math.Round(Public2.PRJ.VP[j].LenghtZO, 1));
					sizeF = graphics.MeasureString(text3, font);
					graphics.DrawString(text3, font, Brushes.Red, (float)num64 - sizeF.Width / 2f, num2 - 25);
				}
				if (Public2.PRJ.SETT.ShowHgtMaxZO & (Public2.PRJ.VP[j].HeightMaxZO != 9999f))
				{
					short num65 = (short)Math.Round((double)(short)unchecked(num2 + num9) - (double)(Public2.PRJ.VP[j].HeightMaxZO + Public2.PRJ.VP[j].LastH2) * num11);
					graphics.DrawLine(pen3, num4, num65, (short)unchecked(num4 + num8), num65);
					string text4 = Conversions.ToString(Math.Round(Public2.PRJ.VP[j].HeightMaxZO, 1));
					sizeF = graphics.MeasureString(text4, font);
					graphics.DrawString(text4, font, Brushes.Red, (short)unchecked(num4 + num8) + 3, (float)num65 - sizeF.Height / 2f);
				}
				if (Public2.PRJ.SETT.ShowHgtBottomZO & (Public2.PRJ.VP[j].HeightBottomZO != 9999f))
				{
					short num66 = (short)Math.Round((double)(short)unchecked(num2 + num9) - (double)(Public2.PRJ.VP[j].HeightBottomZO + Public2.PRJ.VP[j].LastH2) * num11);
					graphics.DrawLine(pen3, num4, num66, (short)unchecked(num4 + num8), num66);
					string text5 = Conversions.ToString(Math.Round(Public2.PRJ.VP[j].HeightBottomZO, 1));
					sizeF = graphics.MeasureString(text5, font);
					graphics.DrawString(text5, font, Brushes.Red, (short)unchecked(num4 + num8) + 3, (float)num66 - sizeF.Height / 2f);
				}
				if (Public2.PRJ.SETT.ShowHgtTopZO & (Public2.PRJ.VP[j].HeightTopZO != 9999f))
				{
					short num67 = (short)Math.Round((double)(short)unchecked(num2 + num9) - (double)(Public2.PRJ.VP[j].HeightTopZO + Public2.PRJ.VP[j].LastH2) * num11);
					graphics.DrawLine(pen3, num4, num67, (short)unchecked(num4 + num8), num67);
					string text6 = Conversions.ToString(Math.Round(Public2.PRJ.VP[j].HeightTopZO, 1));
					sizeF = graphics.MeasureString(text6, font);
					graphics.DrawString(text6, font, Brushes.Red, (short)unchecked(num4 + num8) + 3, (float)num67 - sizeF.Height / 2f);
				}
				bitmap.Save(Module1.FolderTMP + "\\Vert.jpg", ImageFormat.Jpeg);
				Selection selection = oWord.Selection;
				MatchControl = WdUnits.wdLine;
				MatchAlefHamza = WdMovementType.wdExtend;
				selection.EndKey(ref MatchControl, ref MatchAlefHamza);
				oWord.Selection.Font.Name = "Times New Roman";
				oWord.Selection.Font.Size = 12f;
				oWord.Selection.Range.Font.Bold = 0;
				oWord.Selection.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
				oWord.Selection.Range.ParagraphFormat.SpaceBefore = 10f;
				oWord.Selection.Range.ParagraphFormat.SpaceAfter = 2f;
				oWord.Selection.TypeText(Localization.LocMSG(93) + " " + Conversions.ToString(Public2.PRJ.VP[j].Azimuth) + "°\v(" + Localization.LocMSG(94) + " " + Strings.Replace(Public2.PRJ.VP[j].Name, "\r\n", "; ") + ")\r\n");
				oWord.Selection.Range.ParagraphFormat.SpaceBefore = 0f;
				oWord.Selection.Range.ParagraphFormat.SpaceAfter = 0f;
				InlineShapes inlineShapes = oWord.Selection.InlineShapes;
				string fileName = Module1.FolderTMP + "\\Vert.jpg";
				MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
				InlineShape inlineShape = inlineShapes.AddPicture(fileName, ref MatchAlefHamza, ref MatchControl, ref MatchDiacritics);
				inlineShape.Width = oWord.MillimetersToPoints(110f);
				inlineShape.Height = oWord.MillimetersToPoints(110f);
				oWord.Selection.TypeText(" \r\n");
			}
		}
	}

	public static bool FindPointInPolygon(double X, double Y, Public2.CoordWebMerc[] Polygon)
	{
		double num = 0.0;
		checked
		{
			int num2 = Polygon.GetUpperBound(0) - 1;
			for (int i = 0; i <= num2; i++)
			{
				double num3 = Math.Atan2(Polygon[i].Xwm - X, Polygon[i].Ywm - Y);
				if (num3 < 0.0)
				{
					num3 = Math.PI * 2.0 + num3;
				}
				double num4 = Math.Atan2(Polygon[i + 1].Xwm - X, Polygon[i + 1].Ywm - Y);
				if (num4 < 0.0)
				{
					num4 = Math.PI * 2.0 + num4;
				}
				double num5 = num4 - num3;
				if (num5 > Math.PI)
				{
					num5 -= Math.PI * 2.0;
				}
				else if (num5 < -Math.PI)
				{
					num5 += Math.PI * 2.0;
				}
				num += num5;
			}
			if (Math.Abs(num) > 3.0)
			{
				return true;
			}
			return false;
		}
	}

	public static bool CrossPolygon(Public2.CoordWebMerc[] Polygon1, Public2.CoordWebMerc[] Polygon2)
	{
		int upperBound = Polygon1.GetUpperBound(0);
		checked
		{
			for (int i = 0; i <= upperBound; i++)
			{
				if (FindPointInPolygon(Polygon1[i].Xwm, Polygon1[i].Ywm, Polygon2))
				{
					return true;
				}
			}
			int upperBound2 = Polygon2.GetUpperBound(0);
			for (int j = 0; j <= upperBound2; j++)
			{
				if (FindPointInPolygon(Polygon2[j].Xwm, Polygon2[j].Ywm, Polygon1))
				{
					return true;
				}
			}
			return false;
		}
	}

	public static void WordAddTableControlPoint1(ref Microsoft.Office.Interop.Word.Application oWord)
	{
		Find find = oWord.Selection.Find;
		find.Forward = true;
		find.ClearFormatting();
		find.MatchWholeWord = true;
		find.MatchCase = false;
		find.Wrap = WdFindWrap.wdFindContinue;
		object FindText = "{ControlPoints_1}";
		object MatchCase = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchWholeWord = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchWildcards = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchSoundsLike = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchAllWordForms = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Forward = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Wrap = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Format = RuntimeHelpers.GetObjectValue(Missing.Value);
		object ReplaceWith = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Replace = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchKashida = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
		find.Execute(ref FindText, ref MatchCase, ref MatchWholeWord, ref MatchWildcards, ref MatchSoundsLike, ref MatchAllWordForms, ref Forward, ref Wrap, ref Format, ref ReplaceWith, ref Replace, ref MatchKashida, ref MatchDiacritics, ref MatchAlefHamza, ref MatchControl);
		_ = null;
		ResKB[] RKB = default;
		if (!oWord.Selection.Find.Found || !CalcKT(ref RKB))
		{
			return;
		}
		oWord.Selection.Range.Select();
		string sDest = "";
		int num = 1;
		checked
		{
			do
			{
				sDest += " \t";
				num++;
			}
			while (num <= 10);
			StringType.MidStmtStr(ref sDest, Strings.Len(sDest), 1, "\r");
			int upperBound = RKB.GetUpperBound(0);
			for (int i = 0; i <= upperBound; i++)
			{
				sDest = sDest + RKB[i].PointName + "\t";
				float num2 = (float)(Math.Atan2(RKB[i].CoordM.Xm, RKB[i].CoordM.Ym) * (180.0 / Math.PI));
				if (num2 < 0f)
				{
					num2 = 360f + num2;
				}
				sDest = sDest + Conversions.ToString(Math.Round(num2, 2)) + "\t";
				sDest = sDest + Conversions.ToString(Math.Round(RKB[i].CoordM.Xm, 1)) + "\t";
				sDest = sDest + Conversions.ToString(Math.Round(RKB[i].CoordM.Ym, 1)) + "\t";
				sDest = sDest + Conversions.ToString(Math.Round(Math.Sqrt(RKB[i].CoordM.Xm * RKB[i].CoordM.Xm + RKB[i].CoordM.Ym * RKB[i].CoordM.Ym), 2)) + "\t";
				sDest = sDest + RKB[i].PointLocation + "\t";
				sDest = sDest + Conversions.ToString(RKB[i].PointH) + "\t";
				sDest = sDest + Module1.SuperFormat(RKB[i].KB * 100f, 8) + "\t";
				sDest = sDest + Module1.SuperFormat(RKB[i].TotalEsum, 8) + "\t";
				sDest = sDest + Module1.SuperFormat(RKB[i].TotalPPEsum / 100f, 8) + "\t";
				StringType.MidStmtStr(ref sDest, Strings.Len(sDest), 1, "\r");
			}
			oWord.Selection.Text = sDest;
			Selection selection = oWord.Selection;
			MatchControl = WdTableFieldSeparator.wdSeparateByTabs;
			MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchKashida = oWord.MillimetersToPoints(30f);
			Replace = RuntimeHelpers.GetObjectValue(Missing.Value);
			ReplaceWith = RuntimeHelpers.GetObjectValue(Missing.Value);
			Format = RuntimeHelpers.GetObjectValue(Missing.Value);
			Wrap = RuntimeHelpers.GetObjectValue(Missing.Value);
			Forward = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchAllWordForms = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchSoundsLike = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchWildcards = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchWholeWord = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchCase = RuntimeHelpers.GetObjectValue(Missing.Value);
			FindText = WdAutoFitBehavior.wdAutoFitFixed;
			object DefaultTableBehavior = RuntimeHelpers.GetObjectValue(Missing.Value);
			Table table = selection.ConvertToTable(ref MatchControl, ref MatchAlefHamza, ref MatchDiacritics, ref MatchKashida, ref Replace, ref ReplaceWith, ref Format, ref Wrap, ref Forward, ref MatchAllWordForms, ref MatchSoundsLike, ref MatchWildcards, ref MatchWholeWord, ref MatchCase, ref FindText, ref DefaultTableBehavior);
			Selection selection2 = oWord.Selection;
			DefaultTableBehavior = WdUnits.wdLine;
			FindText = 1;
			MatchCase = RuntimeHelpers.GetObjectValue(Missing.Value);
			selection2.MoveDown(ref DefaultTableBehavior, ref FindText, ref MatchCase);
			oWord.Selection.Font.Size = 2f;
			oWord.Selection.Range.ParagraphFormat.SpaceBefore = 0f;
			oWord.Selection.Range.ParagraphFormat.SpaceAfter = 0f;
			table.Rows.Alignment = WdRowAlignment.wdAlignRowCenter;
			table.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
			table.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
			table.Borders.InsideLineWidth = WdLineWidth.wdLineWidth050pt;
			table.Borders.OutsideLineWidth = WdLineWidth.wdLineWidth050pt;
			table.Range.Font.Name = "Times New Roman";
			table.Range.Font.Size = 10f;
			table.Range.Font.Bold = 0;
			table.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
			ParagraphFormat paragraphFormat = table.Range.ParagraphFormat;
			paragraphFormat.LeftIndent = oWord.MillimetersToPoints(-2f);
			paragraphFormat.RightIndent = oWord.MillimetersToPoints(-2f);
			paragraphFormat.SpaceBefore = 0f;
			paragraphFormat.SpaceBeforeAuto = 0;
			paragraphFormat.SpaceAfter = 0f;
			paragraphFormat.SpaceAfterAuto = 0;
			paragraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
			paragraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
			paragraphFormat.WidowControl = -1;
			paragraphFormat.KeepWithNext = 0;
			paragraphFormat.KeepTogether = 0;
			paragraphFormat.PageBreakBefore = 0;
			paragraphFormat.NoLineNumber = 0;
			paragraphFormat.Hyphenation = 0;
			paragraphFormat.FirstLineIndent = oWord.MillimetersToPoints(0f);
			paragraphFormat.OutlineLevel = WdOutlineLevel.wdOutlineLevelBodyText;
			paragraphFormat.CharacterUnitLeftIndent = 0f;
			paragraphFormat.CharacterUnitRightIndent = 0f;
			paragraphFormat.CharacterUnitFirstLineIndent = 0f;
			paragraphFormat.LineUnitBefore = 0f;
			paragraphFormat.LineUnitAfter = 0f;
			paragraphFormat.MirrorIndents = 0;
			paragraphFormat.TextboxTightWrap = WdTextboxTightWrap.wdTightNone;
			_ = null;
			table.Rows[1].Height = oWord.MillimetersToPoints(12f);
			table.Rows[1].Select();
			oWord.Selection.Font.Size = 9f;
			table.Columns[1].Width = oWord.MillimetersToPoints(15f);
			table.Columns[2].Width = oWord.MillimetersToPoints(15f);
			table.Columns[3].Width = oWord.MillimetersToPoints(15f);
			table.Columns[4].Width = oWord.MillimetersToPoints(15f);
			table.Columns[5].Width = oWord.MillimetersToPoints(15f);
			table.Columns[6].Width = oWord.MillimetersToPoints(20f);
			table.Columns[7].Width = oWord.MillimetersToPoints(20f);
			table.Columns[8].Width = oWord.MillimetersToPoints(20f);
			table.Columns[9].Width = oWord.MillimetersToPoints(20f);
			table.Columns[10].Width = oWord.MillimetersToPoints(20f);
			table.Cell(1, 1).Range.Text = "Number\vof\vpoint";
			table.Cell(1, 2).Range.Text = "Azimuth\v(deg)";
			table.Cell(1, 3).Range.Text = "X, m";
			table.Cell(1, 4).Range.Text = "Y, m";
			table.Cell(1, 5).Range.Text = "R, m";
			table.Cell(1, 6).Range.Text = "Location";
			table.Cell(1, 7).Range.Text = "Height above ground level\v(m)";
			table.Cell(1, 8).Range.Text = "Simultaneous\vexposure\v(%)";
			table.Cell(1, 9).Range.Text = "Total E-field\vstrength\v(V/m)";
			table.Cell(1, 10).Range.Text = "Total eq. plane-wave power density (W/m²)";
		}
	}

	public static void WordAddTableControlPoint2(ref Microsoft.Office.Interop.Word.Application oWord)
	{
		Find find = oWord.Selection.Find;
		find.Forward = true;
		find.ClearFormatting();
		find.MatchWholeWord = true;
		find.MatchCase = false;
		find.Wrap = WdFindWrap.wdFindContinue;
		object FindText = "{ControlPoints_2}";
		object MatchCase = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchWholeWord = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchWildcards = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchSoundsLike = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchAllWordForms = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Forward = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Wrap = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Format = RuntimeHelpers.GetObjectValue(Missing.Value);
		object ReplaceWith = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Replace = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchKashida = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
		find.Execute(ref FindText, ref MatchCase, ref MatchWholeWord, ref MatchWildcards, ref MatchSoundsLike, ref MatchAllWordForms, ref Forward, ref Wrap, ref Format, ref ReplaceWith, ref Replace, ref MatchKashida, ref MatchDiacritics, ref MatchAlefHamza, ref MatchControl);
		_ = null;
		ResKB[] RKB = default;
		if (!oWord.Selection.Find.Found || !CalcKT(ref RKB))
		{
			return;
		}
		oWord.Selection.Range.Select();
		string sDest = "";
		int num = 1;
		checked
		{
			do
			{
				sDest += " \t";
				num++;
			}
			while (num <= 13);
			StringType.MidStmtStr(ref sDest, Strings.Len(sDest), 1, "\r");
			int upperBound = RKB.GetUpperBound(0);
			for (int i = 0; i <= upperBound; i++)
			{
				sDest = sDest + RKB[i].PointName + "\t";
				sDest = sDest + RKB[i].PointLabel2 + "\t";
				float num2 = (float)(Math.Atan2(RKB[i].CoordM.Xm, RKB[i].CoordM.Ym) * (180.0 / Math.PI));
				if (num2 < 0f)
				{
					num2 = 360f + num2;
				}
				sDest = sDest + Conversions.ToString(Math.Round(num2, 2)) + "\t";
				sDest = sDest + Conversions.ToString(Math.Round(RKB[i].CoordM.Xm, 1)) + "\t";
				sDest = sDest + Conversions.ToString(Math.Round(RKB[i].CoordM.Ym, 1)) + "\t";
				sDest = sDest + Conversions.ToString(Math.Round(Math.Sqrt(RKB[i].CoordM.Xm * RKB[i].CoordM.Xm + RKB[i].CoordM.Ym * RKB[i].CoordM.Ym), 2)) + "\t";
				sDest = sDest + RKB[i].PointLocation + "\t";
				sDest = sDest + Conversions.ToString(RKB[i].PointH) + "\t";
				sDest = sDest + Module1.SuperFormat(RKB[i].MeasuredE, 8) + "\t";
				sDest = sDest + Module1.SuperFormat(RKB[i].CalculatedEsum, 8) + "\t";
				sDest = sDest + Module1.SuperFormat(RKB[i].KB * 100f, 8) + "\t";
				sDest = sDest + Module1.SuperFormat(RKB[i].TotalEsum, 8) + "\t";
				sDest = sDest + Module1.SuperFormat(RKB[i].TotalPPEsum / 100f, 8) + "\t";
				StringType.MidStmtStr(ref sDest, Strings.Len(sDest), 1, "\r");
			}
			oWord.Selection.Text = sDest;
			Selection selection = oWord.Selection;
			MatchControl = WdTableFieldSeparator.wdSeparateByTabs;
			MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchKashida = oWord.MillimetersToPoints(30f);
			Replace = RuntimeHelpers.GetObjectValue(Missing.Value);
			ReplaceWith = RuntimeHelpers.GetObjectValue(Missing.Value);
			Format = RuntimeHelpers.GetObjectValue(Missing.Value);
			Wrap = RuntimeHelpers.GetObjectValue(Missing.Value);
			Forward = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchAllWordForms = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchSoundsLike = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchWildcards = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchWholeWord = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchCase = RuntimeHelpers.GetObjectValue(Missing.Value);
			FindText = WdAutoFitBehavior.wdAutoFitFixed;
			object DefaultTableBehavior = RuntimeHelpers.GetObjectValue(Missing.Value);
			Table table = selection.ConvertToTable(ref MatchControl, ref MatchAlefHamza, ref MatchDiacritics, ref MatchKashida, ref Replace, ref ReplaceWith, ref Format, ref Wrap, ref Forward, ref MatchAllWordForms, ref MatchSoundsLike, ref MatchWildcards, ref MatchWholeWord, ref MatchCase, ref FindText, ref DefaultTableBehavior);
			Selection selection2 = oWord.Selection;
			DefaultTableBehavior = WdUnits.wdLine;
			FindText = 1;
			MatchCase = RuntimeHelpers.GetObjectValue(Missing.Value);
			selection2.MoveDown(ref DefaultTableBehavior, ref FindText, ref MatchCase);
			oWord.Selection.Font.Size = 2f;
			oWord.Selection.Range.ParagraphFormat.SpaceBefore = 0f;
			oWord.Selection.Range.ParagraphFormat.SpaceAfter = 0f;
			table.Rows.Alignment = WdRowAlignment.wdAlignRowCenter;
			table.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
			table.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
			table.Borders.InsideLineWidth = WdLineWidth.wdLineWidth050pt;
			table.Borders.OutsideLineWidth = WdLineWidth.wdLineWidth050pt;
			table.Range.Font.Name = "Times New Roman";
			table.Range.Font.Size = 10f;
			table.Range.Font.Bold = 0;
			table.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
			ParagraphFormat paragraphFormat = table.Range.ParagraphFormat;
			paragraphFormat.LeftIndent = oWord.MillimetersToPoints(-2f);
			paragraphFormat.RightIndent = oWord.MillimetersToPoints(-2f);
			paragraphFormat.SpaceBefore = 0f;
			paragraphFormat.SpaceBeforeAuto = 0;
			paragraphFormat.SpaceAfter = 0f;
			paragraphFormat.SpaceAfterAuto = 0;
			paragraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
			paragraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
			paragraphFormat.WidowControl = -1;
			paragraphFormat.KeepWithNext = 0;
			paragraphFormat.KeepTogether = 0;
			paragraphFormat.PageBreakBefore = 0;
			paragraphFormat.NoLineNumber = 0;
			paragraphFormat.Hyphenation = 0;
			paragraphFormat.FirstLineIndent = oWord.MillimetersToPoints(0f);
			paragraphFormat.OutlineLevel = WdOutlineLevel.wdOutlineLevelBodyText;
			paragraphFormat.CharacterUnitLeftIndent = 0f;
			paragraphFormat.CharacterUnitRightIndent = 0f;
			paragraphFormat.CharacterUnitFirstLineIndent = 0f;
			paragraphFormat.LineUnitBefore = 0f;
			paragraphFormat.LineUnitAfter = 0f;
			paragraphFormat.MirrorIndents = 0;
			paragraphFormat.TextboxTightWrap = WdTextboxTightWrap.wdTightNone;
			_ = null;
			table.Columns[2].Select();
			oWord.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
			oWord.Selection.ParagraphFormat.LeftIndent = oWord.MillimetersToPoints(0f);
			table.Rows[1].Height = oWord.MillimetersToPoints(12f);
			table.Rows[1].Select();
			oWord.Selection.Font.Size = 9f;
			oWord.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
			table.Columns[1].Width = oWord.MillimetersToPoints(15f);
			table.Columns[2].Width = oWord.MillimetersToPoints(55f);
			table.Columns[3].Width = oWord.MillimetersToPoints(15f);
			table.Columns[4].Width = oWord.MillimetersToPoints(15f);
			table.Columns[5].Width = oWord.MillimetersToPoints(15f);
			table.Columns[6].Width = oWord.MillimetersToPoints(15f);
			table.Columns[7].Width = oWord.MillimetersToPoints(15f);
			table.Columns[8].Width = oWord.MillimetersToPoints(20f);
			table.Columns[9].Width = oWord.MillimetersToPoints(22f);
			table.Columns[10].Width = oWord.MillimetersToPoints(22f);
			table.Columns[11].Width = oWord.MillimetersToPoints(22f);
			table.Columns[12].Width = oWord.MillimetersToPoints(22f);
			table.Columns[13].Width = oWord.MillimetersToPoints(22f);
			table.Cell(1, 1).Range.Text = "Number of point";
			table.Cell(1, 2).Range.Text = "Name of point";
			table.Cell(1, 3).Range.Text = "Azimuth\v(deg)";
			table.Cell(1, 4).Range.Text = "X (m)";
			table.Cell(1, 5).Range.Text = "Y (m)";
			table.Cell(1, 6).Range.Text = "R (m)";
			table.Cell(1, 7).Range.Text = "Location";
			table.Cell(1, 8).Range.Text = "Height above ground level\v(m)";
			table.Cell(1, 9).Range.Text = "Measured\vE-field strength\v(V/m)";
			table.Cell(1, 10).Range.Text = "Calculated E-field strength\v(V/m)";
			table.Cell(1, 11).Range.Text = "Simultaneous exposure\v(%)";
			table.Cell(1, 12).Range.Text = "Total E-field strength\v(V/m)";
			table.Cell(1, 13).Range.Text = "Total eq. plane-wave power density (W/m²)";
		}
	}

	public static bool CalcKT(ref ResKB[] RKB)
	{
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
						if (Operators.CompareString(Public2.PRJ.CPG[i].CP[j].Location, "Building", TextCompare: false) == 0)
						{
							if (Information.IsNothing(Public2.PRJ.BLD))
							{
								Interaction.MsgBox(Localization.LocMSG(87) + ". " + Localization.LocMSG(72) + " " + Conversions.ToString(j + 1) + " " + Localization.LocMSG(73));
								return false;
							}
							bool flag = false;
							int upperBound3 = Public2.PRJ.BLD.GetUpperBound(0);
							for (int k = 0; k <= upperBound3; k++)
							{
								if (Module1.IsPointInsidePolygonF(Public2.PRJ.CPG[i].CP[j].CoordWM, Public2.PRJ.BLD[k].CoordWM))
								{
									flag = true;
									break;
								}
							}
							if (!flag)
							{
								Interaction.MsgBox(Localization.LocMSG(87) + ". " + Localization.LocMSG(72) + " " + Conversions.ToString(j + 1) + " " + Localization.LocMSG(74));
								return false;
							}
						}
						else if (Operators.CompareString(Public2.PRJ.CPG[i].CP[j].Location, "Window", TextCompare: false) == 0)
						{
							if (Information.IsNothing(Public2.PRJ.BLD))
							{
								Interaction.MsgBox(Localization.LocMSG(87) + ". " + Localization.LocMSG(72) + " " + Conversions.ToString(j + 1) + " " + Localization.LocMSG(89));
								return false;
							}
							bool flag2 = false;
							int upperBound4 = Public2.PRJ.BLD.GetUpperBound(0);
							for (int l = 0; l <= upperBound4; l++)
							{
								if (Module1.IsPointInsidePolygonF(Public2.PRJ.CPG[i].CP[j].CoordWM, Public2.PRJ.BLD[l].CoordWM))
								{
									flag2 = true;
									break;
								}
							}
							if (!flag2)
							{
								Interaction.MsgBox(Localization.LocMSG(87) + ". " + Localization.LocMSG(72) + " " + Conversions.ToString(j + 1) + " " + Localization.LocMSG(90));
								return false;
							}
						}
						else
						{
							if (Information.IsNothing(Public2.PRJ.BLD))
							{
								continue;
							}
							int upperBound5 = Public2.PRJ.BLD.GetUpperBound(0);
							for (int m = 0; m <= upperBound5; m++)
							{
								if (Module1.IsPointInsidePolygonF(Public2.PRJ.CPG[i].CP[j].CoordWM, Public2.PRJ.BLD[m].CoordWM))
								{
									Interaction.MsgBox(Localization.LocMSG(87) + ". " + Localization.LocMSG(72) + " " + Conversions.ToString(j + 1) + " " + Localization.LocMSG(75));
									return false;
								}
							}
						}
					}
				}
				short num = -1;
				if (!Information.IsNothing(Public2.PRJ.CPG))
				{
					int upperBound6 = Public2.PRJ.CPG.GetUpperBound(0);
					for (int n = 0; n <= upperBound6; n++)
					{
						if (!Public2.PRJ.CPG[n].Active || Information.IsNothing(Public2.PRJ.CPG[n].CP))
						{
							continue;
						}
						int upperBound7 = Public2.PRJ.CPG[n].CP.GetUpperBound(0);
						for (int num2 = 0; num2 <= upperBound7; num2++)
						{
							int upperBound8 = Public2.PRJ.CPG[n].CP[num2].H.GetUpperBound(0);
							for (int num3 = 0; num3 <= upperBound8; num3++)
							{
								num++;
								RKB = (ResKB[])Utils.CopyArray(RKB, new ResKB[num + 1]);
								ref float[] v = ref RKB[num].V;
								v = (float[])Utils.CopyArray(v, new float[Public2.PRJ.TX.GetUpperBound(0) + 1]);
								if (Public2.PRJ.SETT.ShowControlPointGroups)
								{
									RKB[num].PointName = Conversions.ToString(n + 1) + ".";
								}
								RKB[num].PointName = RKB[num].PointName + Conversions.ToString(num2 + 1);
								if (Public2.PRJ.CPG[n].CP[num2].H.GetUpperBound(0) > 0)
								{
									RKB[num].PointName = RKB[num].PointName + "." + Conversions.ToString(num3 + 1);
								}
								RKB[num].PointLabel = Public2.PRJ.CPG[n].CP[num2].Name;
								RKB[num].PointLabel2 = Public2.PRJ.CPG[n].CP[num2].H[num3].Name;
								RKB[num].CoordM = Public2.PRJ.CPG[n].CP[num2].CoordM;
								RKB[num].PointLocation = Public2.PRJ.CPG[n].CP[num2].Location;
								RKB[num].PointH = Public2.PRJ.CPG[n].CP[num2].H[num3].H;
								short indExclBLD = -1;
								short indExclWall = -1;
								bool inBuilding = false;
								if (Operators.CompareString(Public2.PRJ.CPG[n].CP[num2].Location, "Building", TextCompare: false) == 0)
								{
									int upperBound9 = Public2.PRJ.BLD.GetUpperBound(0);
									for (int num4 = 0; num4 <= upperBound9; num4++)
									{
										if (Module1.IsPointInsidePolygonF(Public2.PRJ.CPG[n].CP[num2].CoordWM, Public2.PRJ.BLD[num4].CoordWM))
										{
											RKB[num].PointElev = Public2.PRJ.BLD[num4].Fundation;
											indExclBLD = (short)num4;
											if (Public2.PRJ.BLD[num4].UseAttenuation & (RKB[num].PointH < Public2.PRJ.BLD[num4].H))
											{
												inBuilding = true;
											}
											break;
										}
									}
								}
								else if (Operators.CompareString(Public2.PRJ.CPG[n].CP[num2].Location, "Window", TextCompare: false) == 0)
								{
									int upperBound10 = Public2.PRJ.BLD.GetUpperBound(0);
									for (int num5 = 0; num5 <= upperBound10; num5++)
									{
										if (!Module1.IsPointInsidePolygonF(Public2.PRJ.CPG[n].CP[num2].CoordWM, Public2.PRJ.BLD[num5].CoordWM))
										{
											continue;
										}
										RKB[num].PointElev = Public2.PRJ.BLD[num5].Fundation;
										indExclBLD = (short)num5;
										double num6 = 99999.0;
										indExclWall = 0;
										int num7 = Public2.PRJ.BLD[num5].CoordWM.GetUpperBound(0) - 1;
										for (int num8 = 0; num8 <= num7; num8++)
										{
											double num9 = Module1.DistPointToLine(Public2.PRJ.CPG[n].CP[num2].CoordWM.Xwm, Public2.PRJ.CPG[n].CP[num2].CoordWM.Ywm, Public2.PRJ.BLD[num5].CoordWM[num8].Xwm, Public2.PRJ.BLD[num5].CoordWM[num8].Ywm, Public2.PRJ.BLD[num5].CoordWM[num8 + 1].Xwm, Public2.PRJ.BLD[num5].CoordWM[num8 + 1].Ywm);
											if (num9 < num6)
											{
												num6 = num9;
												indExclWall = (short)num8;
											}
										}
										break;
									}
								}
								else
								{
									RKB[num].PointElev = Triangul.FindHeightTriang(Public2.PRJ.CPG[n].CP[num2].CoordWM.Xwm, Public2.PRJ.CPG[n].CP[num2].CoordWM.Ywm);
								}
								float zt = RKB[num].PointH + RKB[num].PointElev;
								RKB[num].KB = CalcSZZ.CalcPointKB(inBuilding, Public2.PRJ.CPG[n].CP[num2].H[num3].L, indExclBLD, indExclWall, (float)Public2.PRJ.CPG[n].CP[num2].CoordM.Xm, (float)Public2.PRJ.CPG[n].CP[num2].CoordM.Ym, zt, ref RKB[num].V, ref RKB[num].CalculatedPPEsum, ref RKB[num].CalculatedEsum);
								RKB[num].MeasuredE = Public2.PRJ.CPG[n].CP[num2].H[num3].MeasuredE;
								RKB[num].TotalPPEsum = (float)((double)RKB[num].CalculatedPPEsum + Math.Pow(RKB[num].MeasuredE, 2.0) / 3.77);
								RKB[num].TotalEsum = (float)Math.Sqrt(Math.Pow(RKB[num].CalculatedEsum, 2.0) + Math.Pow(RKB[num].MeasuredE, 2.0));
								if (!(RKB[num].MeasuredE > 0f))
								{
									continue;
								}
								float num10 = 1000000f;
								if (Public2.PRJ.SETT.LMT.Count > 0)
								{
									bool flag3 = false;
									List<CalcSZZ.LimPoint> list = new List<CalcSZZ.LimPoint>();
									int num11 = Public2.PRJ.SETT.LMT.Count - 1;
									for (int num12 = 0; num12 <= num11; num12++)
									{
										if ((Public2.PRJ.SETT.LMT[num12].Freq == -999f) | (Public2.PRJ.SETT.LMT[num12].E[Public2.PRJ.SETT.ControlPointLimitIndex] == -999f) | (Public2.PRJ.SETT.LMT[num12].A[Public2.PRJ.SETT.ControlPointLimitIndex] == -999f))
										{
											flag3 = true;
										}
										if (!flag3)
										{
											list.Add(new CalcSZZ.LimPoint(Public2.PRJ.SETT.LMT[num12].Freq, Public2.PRJ.SETT.LMT[num12].E[Public2.PRJ.SETT.ControlPointLimitIndex], Public2.PRJ.SETT.LMT[num12].A[Public2.PRJ.SETT.ControlPointLimitIndex]));
										}
									}
									if (flag3)
									{
										Interaction.MsgBox(Localization.LocMSG(87) + ". " + Localization.LocMSG(109));
										return false;
									}
									if (list.First().F > 10f)
									{
										list.Insert(0, new CalcSZZ.LimPoint(10f, 0f, 0f));
									}
									int num13 = list.Count - 2;
									for (int num14 = 0; num14 <= num13; num14++)
									{
										if ((Public2.PRJ.SETT.ControlPointMesureFreq > list[num14].F) & (Public2.PRJ.SETT.ControlPointMesureFreq <= list[num14 + 1].F))
										{
											float num15 = (float)((double)list[num14 + 1].E * Math.Pow(Public2.PRJ.SETT.ControlPointMesureFreq, list[num14 + 1].A));
											num10 = (float)((double)(num15 * num15) / 3.77);
											break;
										}
									}
								}
								RKB[num].KB = (float)((double)RKB[num].KB + Math.Pow(RKB[num].MeasuredE, 2.0) / 3.77 / (double)num10);
							}
						}
					}
				}
				if (num < 0)
				{
					Interaction.MsgBox(Localization.LocMSG(87) + ". " + Localization.LocMSG(88));
					return false;
				}
				return true;
			}
			Interaction.MsgBox(Localization.LocMSG(87) + ". " + Localization.LocMSG(88));
			return false;
		}
	}

	public static void WordAddTablesVertE1(ref Microsoft.Office.Interop.Word.Application oWord, ref Document oDoc)
	{
		Find find = oWord.Selection.Find;
		find.Forward = true;
		find.ClearFormatting();
		find.MatchWholeWord = true;
		find.MatchCase = false;
		find.Wrap = WdFindWrap.wdFindContinue;
		object FindText = "{EFieldStrengthVerticalCutTables_1}";
		object MatchCase = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchWholeWord = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchWildcards = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchSoundsLike = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchAllWordForms = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Forward = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Wrap = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Format = RuntimeHelpers.GetObjectValue(Missing.Value);
		object ReplaceWith = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Replace = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchKashida = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
		find.Execute(ref FindText, ref MatchCase, ref MatchWholeWord, ref MatchWildcards, ref MatchSoundsLike, ref MatchAllWordForms, ref Forward, ref Wrap, ref Format, ref ReplaceWith, ref Replace, ref MatchKashida, ref MatchDiacritics, ref MatchAlefHamza, ref MatchControl);
		_ = null;
		if (!oWord.Selection.Find.Found)
		{
			return;
		}
		short num = 0;
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.VP))
			{
				int upperBound = Public2.PRJ.VP.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					if (Public2.PRJ.VP[i].Activ)
					{
						num++;
					}
				}
			}
			if (num < 1 || Public2.PRJ.SETT.Tabl1LengthMax == 0f || Public2.PRJ.SETT.Tabl1StepL == 0f || Public2.PRJ.SETT.Tabl1LengthMax <= Public2.PRJ.SETT.Tabl1StepL || Public2.PRJ.SETT.Tabl1HeightMax == 0f || Public2.PRJ.SETT.Tabl1StepH == 0f || Public2.PRJ.SETT.Tabl1HeightMax <= Public2.PRJ.SETT.Tabl1StepH)
			{
				return;
			}
			_ = num - 1;
			int num2 = (int)Math.Floor(Public2.PRJ.SETT.Tabl1HeightMax / Public2.PRJ.SETT.Tabl1StepH);
			int num3 = (int)Math.Floor(Public2.PRJ.SETT.Tabl1LengthMax / Public2.PRJ.SETT.Tabl1StepL);
			ResTBL[][,] array = new ResTBL[Public2.PRJ.VP.GetUpperBound(0) + 1][,];
			int upperBound2 = Public2.PRJ.VP.GetUpperBound(0);
			Public2.CoordMeters coordMeters = default;
			for (int j = 0; j <= upperBound2; j++)
			{
				if (!Public2.PRJ.VP[j].Activ)
				{
					continue;
				}
				float num4 = Triangul.FindElevWithBLD(Public2.PRJ.VP[j].CoordWM);
				double num5 = Math.Sin((double)Public2.PRJ.VP[j].Azimuth * Constants.PI_180);
				double num6 = Math.Cos((double)Public2.PRJ.VP[j].Azimuth * Constants.PI_180);
				ref ResTBL[,] reference = ref array[j];
				reference = (ResTBL[,])Utils.CopyArray(reference, new ResTBL[num3 + 1, num2 + 1]);
				int num7 = num3;
				for (int k = 0; k <= num7; k++)
				{
					coordMeters.Xm = Public2.PRJ.VP[j].CoordM.Xm + (double)((float)k * Public2.PRJ.SETT.Tabl1StepL) * num5;
					coordMeters.Ym = Public2.PRJ.VP[j].CoordM.Ym + (double)((float)k * Public2.PRJ.SETT.Tabl1StepL) * num6;
					int num8 = num2;
					for (int l = 0; l <= num8; l++)
					{
						array[j][k, l].KB = CalcSZZ.CalcPointKB2((float)coordMeters.Xm, (float)coordMeters.Ym, num4 + (float)(num2 - l) * Public2.PRJ.SETT.Tabl1StepH, ref array[j][k, l].V, ref array[j][k, l].PPEsum, ref array[j][k, l].Esum);
					}
				}
			}
			oWord.Selection.Range.Select();
			int num9 = num3 + 1;
			int num10;
			int num11;
			int num12;
			if (num9 <= 21)
			{
				num10 = 1;
				num11 = num9;
				num12 = 0;
			}
			else
			{
				num10 = (int)Math.Round(Math.Ceiling((double)(num9 - 21) / 21.0) + 1.0);
				num11 = 21;
				num12 = num9 - 21 - (num10 - 2) * 21;
			}
			string text = "";
			int upperBound3 = Public2.PRJ.VP.GetUpperBound(0);
			for (int m = 0; m <= upperBound3; m++)
			{
				if (!Public2.PRJ.VP[m].Activ)
				{
					continue;
				}
				Selection selection = oWord.Selection;
				MatchControl = WdUnits.wdLine;
				MatchAlefHamza = WdMovementType.wdExtend;
				selection.EndKey(ref MatchControl, ref MatchAlefHamza);
				oWord.Selection.Font.Name = "Times New Roman";
				oWord.Selection.Font.Size = 10f;
				oWord.Selection.Range.Font.Bold = 0;
				oWord.Selection.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
				oWord.Selection.Range.ParagraphFormat.SpaceBefore = 10f;
				oWord.Selection.Range.ParagraphFormat.SpaceAfter = 2f;
				oWord.Selection.TypeText("E-field strength (V/m) along azimuth " + Conversions.ToString(Public2.PRJ.VP[m].Azimuth) + "° (along direction of antenna " + Strings.Replace(Public2.PRJ.VP[m].Name, "\r\n", "; ") + ")\r\n");
				text = " \tR, m\t";
				int num13 = num11 - 1;
				for (int n = 1; n <= num13; n++)
				{
					text += "\t";
				}
				StringType.MidStmtStr(ref text, Strings.Len(text), 1, "\r");
				text += "H, m\t";
				int num14 = num11 - 1;
				for (int num15 = 0; num15 <= num14; num15++)
				{
					text = text + Conversions.ToString((float)num15 * Public2.PRJ.SETT.Tabl1StepL) + " m\t";
				}
				StringType.MidStmtStr(ref text, Strings.Len(text), 1, "\r");
				int num16 = num2;
				for (int num17 = 0; num17 <= num16; num17++)
				{
					text = text + Conversions.ToString((double)(num2 - num17) * Math.Round(Public2.PRJ.SETT.Tabl1StepH, 1)) + " m\t";
					int num18 = num11 - 1;
					for (int num19 = 0; num19 <= num18; num19++)
					{
						text = text + Module1.SuperFormat(array[m][num19, num17].Esum, 6) + "\t";
					}
					StringType.MidStmtStr(ref text, Strings.Len(text), 1, "\r");
				}
				oWord.Selection.Text = text;
				Selection selection2 = oWord.Selection;
				MatchAlefHamza = WdTableFieldSeparator.wdSeparateByTabs;
				MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchKashida = oWord.MillimetersToPoints(12f);
				Replace = RuntimeHelpers.GetObjectValue(Missing.Value);
				ReplaceWith = RuntimeHelpers.GetObjectValue(Missing.Value);
				Format = RuntimeHelpers.GetObjectValue(Missing.Value);
				Wrap = RuntimeHelpers.GetObjectValue(Missing.Value);
				Forward = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchAllWordForms = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchSoundsLike = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchWildcards = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchWholeWord = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchCase = RuntimeHelpers.GetObjectValue(Missing.Value);
				FindText = WdAutoFitBehavior.wdAutoFitFixed;
				object DefaultTableBehavior = RuntimeHelpers.GetObjectValue(Missing.Value);
				Table table = selection2.ConvertToTable(ref MatchAlefHamza, ref MatchControl, ref MatchDiacritics, ref MatchKashida, ref Replace, ref ReplaceWith, ref Format, ref Wrap, ref Forward, ref MatchAllWordForms, ref MatchSoundsLike, ref MatchWildcards, ref MatchWholeWord, ref MatchCase, ref FindText, ref DefaultTableBehavior);
				table.Rows.Alignment = WdRowAlignment.wdAlignRowLeft;
				table.Rows.SetLeftIndent(0f, WdRulerStyle.wdAdjustNone);
				table.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
				table.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
				table.Borders.InsideLineWidth = WdLineWidth.wdLineWidth050pt;
				table.Borders.OutsideLineWidth = WdLineWidth.wdLineWidth050pt;
				table.Range.Font.Name = "Times New Roman";
				table.Range.Font.Size = 10f;
				table.Range.Font.Bold = 0;
				table.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
				ParagraphFormat paragraphFormat = table.Range.ParagraphFormat;
				paragraphFormat.LeftIndent = oWord.MillimetersToPoints(-2f);
				paragraphFormat.RightIndent = oWord.MillimetersToPoints(-2f);
				paragraphFormat.SpaceBefore = 2f;
				paragraphFormat.SpaceBeforeAuto = 0;
				paragraphFormat.SpaceAfter = 2f;
				paragraphFormat.SpaceAfterAuto = 0;
				paragraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
				paragraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
				paragraphFormat.WidowControl = -1;
				paragraphFormat.KeepWithNext = 0;
				paragraphFormat.KeepTogether = 0;
				paragraphFormat.PageBreakBefore = 0;
				paragraphFormat.NoLineNumber = 0;
				paragraphFormat.Hyphenation = 0;
				paragraphFormat.FirstLineIndent = oWord.MillimetersToPoints(0f);
				paragraphFormat.OutlineLevel = WdOutlineLevel.wdOutlineLevelBodyText;
				paragraphFormat.CharacterUnitLeftIndent = 0f;
				paragraphFormat.CharacterUnitRightIndent = 0f;
				paragraphFormat.CharacterUnitFirstLineIndent = 0f;
				paragraphFormat.LineUnitBefore = 0f;
				paragraphFormat.LineUnitAfter = 0f;
				paragraphFormat.MirrorIndents = 0;
				paragraphFormat.TextboxTightWrap = WdTextboxTightWrap.wdTightNone;
				_ = null;
				table.Columns[1].Width = oWord.MillimetersToPoints(16f);
				table.Cell(1, 2).Merge(table.Cell(1, num11 + 1));
				table.Select();
				Selection selection3 = oWord.Selection;
				DefaultTableBehavior = WdCollapseDirection.wdCollapseEnd;
				selection3.Collapse(ref DefaultTableBehavior);
				oWord.Selection.Text = " ";
				int num20 = num10 - 1;
				for (int num21 = 2; num21 <= num20; num21++)
				{
					oWord.Selection.Font.Name = "Times New Roman";
					oWord.Selection.Font.Size = 10f;
					oWord.Selection.Range.ParagraphFormat.SpaceBefore = 0f;
					oWord.Selection.Range.ParagraphFormat.SpaceAfter = 0f;
					oWord.Selection.TypeParagraph();
					text = " \tR, m\t";
					int num22 = 1;
					do
					{
						text += "\t";
						num22++;
					}
					while (num22 <= 20);
					StringType.MidStmtStr(ref text, Strings.Len(text), 1, "\r");
					text += "H, m\t";
					int num23 = 0;
					do
					{
						text = text + Conversions.ToString((float)(21 + (num21 - 2) * 21 + num23) * Public2.PRJ.SETT.Tabl1StepL) + " m\t";
						num23++;
					}
					while (num23 <= 20);
					StringType.MidStmtStr(ref text, Strings.Len(text), 1, "\r");
					int num24 = num2;
					for (int num25 = 0; num25 <= num24; num25++)
					{
						text = text + Conversions.ToString(Math.Round((float)(num2 - num25) * Public2.PRJ.SETT.Tabl1StepH, 1)) + " m\t";
						int num26 = 0;
						do
						{
							text = text + Module1.SuperFormat(array[m][21 + (num21 - 2) * 21 + num26, num25].Esum, 6) + "\t";
							num26++;
						}
						while (num26 <= 20);
						StringType.MidStmtStr(ref text, Strings.Len(text), 1, "\r");
					}
					oWord.Selection.Text = text;
					Selection selection4 = oWord.Selection;
					DefaultTableBehavior = WdTableFieldSeparator.wdSeparateByTabs;
					FindText = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchCase = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchWholeWord = oWord.MillimetersToPoints(12f);
					MatchWildcards = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchSoundsLike = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchAllWordForms = RuntimeHelpers.GetObjectValue(Missing.Value);
					Forward = RuntimeHelpers.GetObjectValue(Missing.Value);
					Wrap = RuntimeHelpers.GetObjectValue(Missing.Value);
					Format = RuntimeHelpers.GetObjectValue(Missing.Value);
					ReplaceWith = RuntimeHelpers.GetObjectValue(Missing.Value);
					Replace = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchKashida = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchControl = WdAutoFitBehavior.wdAutoFitFixed;
					MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
					Table table2 = selection4.ConvertToTable(ref DefaultTableBehavior, ref FindText, ref MatchCase, ref MatchWholeWord, ref MatchWildcards, ref MatchSoundsLike, ref MatchAllWordForms, ref Forward, ref Wrap, ref Format, ref ReplaceWith, ref Replace, ref MatchKashida, ref MatchDiacritics, ref MatchControl, ref MatchAlefHamza);
					table2.Rows.Alignment = WdRowAlignment.wdAlignRowLeft;
					table2.Rows.SetLeftIndent(0f, WdRulerStyle.wdAdjustNone);
					table2.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
					table2.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
					table2.Borders.InsideLineWidth = WdLineWidth.wdLineWidth050pt;
					table2.Borders.OutsideLineWidth = WdLineWidth.wdLineWidth050pt;
					table2.Range.Font.Name = "Times New Roman";
					table2.Range.Font.Size = 10f;
					table2.Range.Font.Bold = 0;
					table2.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
					ParagraphFormat paragraphFormat2 = table2.Range.ParagraphFormat;
					paragraphFormat2.LeftIndent = oWord.MillimetersToPoints(-2f);
					paragraphFormat2.RightIndent = oWord.MillimetersToPoints(-2f);
					paragraphFormat2.SpaceBefore = 2f;
					paragraphFormat2.SpaceBeforeAuto = 0;
					paragraphFormat2.SpaceAfter = 2f;
					paragraphFormat2.SpaceAfterAuto = 0;
					paragraphFormat2.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
					paragraphFormat2.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
					paragraphFormat2.WidowControl = -1;
					paragraphFormat2.KeepWithNext = 0;
					paragraphFormat2.KeepTogether = 0;
					paragraphFormat2.PageBreakBefore = 0;
					paragraphFormat2.NoLineNumber = 0;
					paragraphFormat2.Hyphenation = 0;
					paragraphFormat2.FirstLineIndent = oWord.MillimetersToPoints(0f);
					paragraphFormat2.OutlineLevel = WdOutlineLevel.wdOutlineLevelBodyText;
					paragraphFormat2.CharacterUnitLeftIndent = 0f;
					paragraphFormat2.CharacterUnitRightIndent = 0f;
					paragraphFormat2.CharacterUnitFirstLineIndent = 0f;
					paragraphFormat2.LineUnitBefore = 0f;
					paragraphFormat2.LineUnitAfter = 0f;
					paragraphFormat2.MirrorIndents = 0;
					paragraphFormat2.TextboxTightWrap = WdTextboxTightWrap.wdTightNone;
					_ = null;
					table2.Columns[1].Width = oWord.MillimetersToPoints(16f);
					table2.Cell(1, 2).Merge(table2.Cell(1, 22));
					table2.Select();
					Selection selection5 = oWord.Selection;
					MatchAlefHamza = WdCollapseDirection.wdCollapseEnd;
					selection5.Collapse(ref MatchAlefHamza);
					oWord.Selection.Text = " ";
				}
				if (num12 <= 0)
				{
					continue;
				}
				oWord.Selection.Font.Name = "Times New Roman";
				oWord.Selection.Font.Size = 10f;
				oWord.Selection.Range.ParagraphFormat.SpaceBefore = 0f;
				oWord.Selection.Range.ParagraphFormat.SpaceAfter = 0f;
				oWord.Selection.TypeParagraph();
				text = " \tR, m\t";
				int num27 = num12 - 1;
				for (int num28 = 1; num28 <= num27; num28++)
				{
					text += "\t";
				}
				StringType.MidStmtStr(ref text, Strings.Len(text), 1, "\r");
				text += "H, m\t";
				int num29 = num12 - 1;
				for (int num30 = 0; num30 <= num29; num30++)
				{
					text = text + Conversions.ToString((float)(21 + (num10 - 2) * 21 + num30) * Public2.PRJ.SETT.Tabl1StepL) + " m\t";
				}
				StringType.MidStmtStr(ref text, Strings.Len(text), 1, "\r");
				int num31 = num2;
				for (int num32 = 0; num32 <= num31; num32++)
				{
					text = text + Conversions.ToString(Math.Round((float)(num2 - num32) * Public2.PRJ.SETT.Tabl1StepH, 1)) + " m\t";
					int num33 = num12 - 1;
					for (int num34 = 0; num34 <= num33; num34++)
					{
						text = text + Module1.SuperFormat(array[m][21 + (num10 - 2) * 21 + num34, num32].Esum, 6) + "\t";
					}
					StringType.MidStmtStr(ref text, Strings.Len(text), 1, "\r");
				}
				oWord.Selection.Text = text;
				Selection selection6 = oWord.Selection;
				MatchAlefHamza = WdTableFieldSeparator.wdSeparateByTabs;
				MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchKashida = oWord.MillimetersToPoints(12f);
				Replace = RuntimeHelpers.GetObjectValue(Missing.Value);
				ReplaceWith = RuntimeHelpers.GetObjectValue(Missing.Value);
				Format = RuntimeHelpers.GetObjectValue(Missing.Value);
				Wrap = RuntimeHelpers.GetObjectValue(Missing.Value);
				Forward = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchAllWordForms = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchSoundsLike = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchWildcards = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchWholeWord = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchCase = RuntimeHelpers.GetObjectValue(Missing.Value);
				FindText = WdAutoFitBehavior.wdAutoFitFixed;
				DefaultTableBehavior = RuntimeHelpers.GetObjectValue(Missing.Value);
				Table table3 = selection6.ConvertToTable(ref MatchAlefHamza, ref MatchControl, ref MatchDiacritics, ref MatchKashida, ref Replace, ref ReplaceWith, ref Format, ref Wrap, ref Forward, ref MatchAllWordForms, ref MatchSoundsLike, ref MatchWildcards, ref MatchWholeWord, ref MatchCase, ref FindText, ref DefaultTableBehavior);
				table3.Rows.Alignment = WdRowAlignment.wdAlignRowLeft;
				table3.Rows.SetLeftIndent(0f, WdRulerStyle.wdAdjustNone);
				table3.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
				table3.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
				table3.Borders.InsideLineWidth = WdLineWidth.wdLineWidth050pt;
				table3.Borders.OutsideLineWidth = WdLineWidth.wdLineWidth050pt;
				table3.Range.Font.Name = "Times New Roman";
				table3.Range.Font.Size = 10f;
				table3.Range.Font.Bold = 0;
				table3.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
				ParagraphFormat paragraphFormat3 = table3.Range.ParagraphFormat;
				paragraphFormat3.LeftIndent = oWord.MillimetersToPoints(-2f);
				paragraphFormat3.RightIndent = oWord.MillimetersToPoints(-2f);
				paragraphFormat3.SpaceBefore = 2f;
				paragraphFormat3.SpaceBeforeAuto = 0;
				paragraphFormat3.SpaceAfter = 2f;
				paragraphFormat3.SpaceAfterAuto = 0;
				paragraphFormat3.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
				paragraphFormat3.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
				paragraphFormat3.WidowControl = -1;
				paragraphFormat3.KeepWithNext = 0;
				paragraphFormat3.KeepTogether = 0;
				paragraphFormat3.PageBreakBefore = 0;
				paragraphFormat3.NoLineNumber = 0;
				paragraphFormat3.Hyphenation = 0;
				paragraphFormat3.FirstLineIndent = oWord.MillimetersToPoints(0f);
				paragraphFormat3.OutlineLevel = WdOutlineLevel.wdOutlineLevelBodyText;
				paragraphFormat3.CharacterUnitLeftIndent = 0f;
				paragraphFormat3.CharacterUnitRightIndent = 0f;
				paragraphFormat3.CharacterUnitFirstLineIndent = 0f;
				paragraphFormat3.LineUnitBefore = 0f;
				paragraphFormat3.LineUnitAfter = 0f;
				paragraphFormat3.MirrorIndents = 0;
				paragraphFormat3.TextboxTightWrap = WdTextboxTightWrap.wdTightNone;
				_ = null;
				table3.Columns[1].Width = oWord.MillimetersToPoints(16f);
				table3.Cell(1, 2).Merge(table3.Cell(1, num12 - 1 + 2));
				table3.Select();
				Selection selection7 = oWord.Selection;
				DefaultTableBehavior = WdCollapseDirection.wdCollapseEnd;
				selection7.Collapse(ref DefaultTableBehavior);
				oWord.Selection.Text = " ";
			}
		}
	}

	public static void WordAddTablesVertKB1(ref Microsoft.Office.Interop.Word.Application oWord, ref Document oDoc)
	{
		Find find = oWord.Selection.Find;
		find.Forward = true;
		find.ClearFormatting();
		find.MatchWholeWord = true;
		find.MatchCase = false;
		find.Wrap = WdFindWrap.wdFindContinue;
		object FindText = "{SimultaneousExposureVerticalCutTables_1}";
		object MatchCase = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchWholeWord = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchWildcards = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchSoundsLike = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchAllWordForms = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Forward = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Wrap = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Format = RuntimeHelpers.GetObjectValue(Missing.Value);
		object ReplaceWith = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Replace = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchKashida = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
		find.Execute(ref FindText, ref MatchCase, ref MatchWholeWord, ref MatchWildcards, ref MatchSoundsLike, ref MatchAllWordForms, ref Forward, ref Wrap, ref Format, ref ReplaceWith, ref Replace, ref MatchKashida, ref MatchDiacritics, ref MatchAlefHamza, ref MatchControl);
		_ = null;
		if (!oWord.Selection.Find.Found)
		{
			return;
		}
		short num = 0;
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.VP))
			{
				int upperBound = Public2.PRJ.VP.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					if (Public2.PRJ.VP[i].Activ)
					{
						num++;
					}
				}
			}
			if (num < 1 || Public2.PRJ.SETT.Tabl1LengthMax == 0f || Public2.PRJ.SETT.Tabl1StepL == 0f || Public2.PRJ.SETT.Tabl1LengthMax <= Public2.PRJ.SETT.Tabl1StepL || Public2.PRJ.SETT.Tabl1HeightMax == 0f || Public2.PRJ.SETT.Tabl1StepH == 0f || Public2.PRJ.SETT.Tabl1HeightMax <= Public2.PRJ.SETT.Tabl1StepH)
			{
				return;
			}
			_ = num - 1;
			int num2 = (int)Math.Floor(Public2.PRJ.SETT.Tabl1HeightMax / Public2.PRJ.SETT.Tabl1StepH);
			int num3 = (int)Math.Floor(Public2.PRJ.SETT.Tabl1LengthMax / Public2.PRJ.SETT.Tabl1StepL);
			ResTBL[][,] array = new ResTBL[Public2.PRJ.VP.GetUpperBound(0) + 1][,];
			int upperBound2 = Public2.PRJ.VP.GetUpperBound(0);
			Public2.CoordMeters coordMeters = default;
			for (int j = 0; j <= upperBound2; j++)
			{
				if (!Public2.PRJ.VP[j].Activ)
				{
					continue;
				}
				float num4 = Triangul.FindElevWithBLD(Public2.PRJ.VP[j].CoordWM);
				double num5 = Math.Sin((double)Public2.PRJ.VP[j].Azimuth * Constants.PI_180);
				double num6 = Math.Cos((double)Public2.PRJ.VP[j].Azimuth * Constants.PI_180);
				ref ResTBL[,] reference = ref array[j];
				reference = (ResTBL[,])Utils.CopyArray(reference, new ResTBL[num3 + 1, num2 + 1]);
				int num7 = num3;
				for (int k = 0; k <= num7; k++)
				{
					coordMeters.Xm = Public2.PRJ.VP[j].CoordM.Xm + (double)((float)k * Public2.PRJ.SETT.Tabl1StepL) * num5;
					coordMeters.Ym = Public2.PRJ.VP[j].CoordM.Ym + (double)((float)k * Public2.PRJ.SETT.Tabl1StepL) * num6;
					int num8 = num2;
					for (int l = 0; l <= num8; l++)
					{
						array[j][k, l].KB = CalcSZZ.CalcPointKB2((float)coordMeters.Xm, (float)coordMeters.Ym, num4 + (float)(num2 - l) * Public2.PRJ.SETT.Tabl1StepH, ref array[j][k, l].V, ref array[j][k, l].PPEsum, ref array[j][k, l].Esum) * 100f;
					}
				}
			}
			oWord.Selection.Range.Select();
			int num9 = num3 + 1;
			int num10;
			int num11;
			int num12;
			if (num9 <= 21)
			{
				num10 = 1;
				num11 = num9;
				num12 = 0;
			}
			else
			{
				num10 = (int)Math.Round(Math.Ceiling((double)(num9 - 21) / 21.0) + 1.0);
				num11 = 21;
				num12 = num9 - 21 - (num10 - 2) * 21;
			}
			string text = "";
			int upperBound3 = Public2.PRJ.VP.GetUpperBound(0);
			for (int m = 0; m <= upperBound3; m++)
			{
				if (!Public2.PRJ.VP[m].Activ)
				{
					continue;
				}
				Selection selection = oWord.Selection;
				MatchControl = WdUnits.wdLine;
				MatchAlefHamza = WdMovementType.wdExtend;
				selection.EndKey(ref MatchControl, ref MatchAlefHamza);
				oWord.Selection.Font.Name = "Times New Roman";
				oWord.Selection.Font.Size = 10f;
				oWord.Selection.Range.Font.Bold = 0;
				oWord.Selection.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
				oWord.Selection.Range.ParagraphFormat.SpaceBefore = 10f;
				oWord.Selection.Range.ParagraphFormat.SpaceAfter = 2f;
				oWord.Selection.TypeText("Simultaneous exposure (%) along azimuth " + Conversions.ToString(Public2.PRJ.VP[m].Azimuth) + "° (along direction of antenna " + Strings.Replace(Public2.PRJ.VP[m].Name, "\r\n", "; ") + ")\r\n");
				text = " \tR, m\t";
				int num13 = num11 - 1;
				for (int n = 1; n <= num13; n++)
				{
					text += "\t";
				}
				StringType.MidStmtStr(ref text, Strings.Len(text), 1, "\r");
				text += "H, m\t";
				int num14 = num11 - 1;
				for (int num15 = 0; num15 <= num14; num15++)
				{
					text = text + Conversions.ToString((float)num15 * Public2.PRJ.SETT.Tabl1StepL) + " m\t";
				}
				StringType.MidStmtStr(ref text, Strings.Len(text), 1, "\r");
				int num16 = num2;
				for (int num17 = 0; num17 <= num16; num17++)
				{
					text = text + Conversions.ToString((double)(num2 - num17) * Math.Round(Public2.PRJ.SETT.Tabl1StepH, 1)) + " m\t";
					int num18 = num11 - 1;
					for (int num19 = 0; num19 <= num18; num19++)
					{
						text = text + Module1.SuperFormat(array[m][num19, num17].KB, 6) + "\t";
					}
					StringType.MidStmtStr(ref text, Strings.Len(text), 1, "\r");
				}
				oWord.Selection.Text = text;
				Selection selection2 = oWord.Selection;
				MatchAlefHamza = WdTableFieldSeparator.wdSeparateByTabs;
				MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchKashida = oWord.MillimetersToPoints(12f);
				Replace = RuntimeHelpers.GetObjectValue(Missing.Value);
				ReplaceWith = RuntimeHelpers.GetObjectValue(Missing.Value);
				Format = RuntimeHelpers.GetObjectValue(Missing.Value);
				Wrap = RuntimeHelpers.GetObjectValue(Missing.Value);
				Forward = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchAllWordForms = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchSoundsLike = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchWildcards = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchWholeWord = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchCase = RuntimeHelpers.GetObjectValue(Missing.Value);
				FindText = WdAutoFitBehavior.wdAutoFitFixed;
				object DefaultTableBehavior = RuntimeHelpers.GetObjectValue(Missing.Value);
				Table table = selection2.ConvertToTable(ref MatchAlefHamza, ref MatchControl, ref MatchDiacritics, ref MatchKashida, ref Replace, ref ReplaceWith, ref Format, ref Wrap, ref Forward, ref MatchAllWordForms, ref MatchSoundsLike, ref MatchWildcards, ref MatchWholeWord, ref MatchCase, ref FindText, ref DefaultTableBehavior);
				table.Rows.Alignment = WdRowAlignment.wdAlignRowLeft;
				table.Rows.SetLeftIndent(0f, WdRulerStyle.wdAdjustNone);
				table.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
				table.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
				table.Borders.InsideLineWidth = WdLineWidth.wdLineWidth050pt;
				table.Borders.OutsideLineWidth = WdLineWidth.wdLineWidth050pt;
				table.Range.Font.Name = "Times New Roman";
				table.Range.Font.Size = 10f;
				table.Range.Font.Bold = 0;
				table.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
				ParagraphFormat paragraphFormat = table.Range.ParagraphFormat;
				paragraphFormat.LeftIndent = oWord.MillimetersToPoints(-2f);
				paragraphFormat.RightIndent = oWord.MillimetersToPoints(-2f);
				paragraphFormat.SpaceBefore = 2f;
				paragraphFormat.SpaceBeforeAuto = 0;
				paragraphFormat.SpaceAfter = 2f;
				paragraphFormat.SpaceAfterAuto = 0;
				paragraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
				paragraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
				paragraphFormat.WidowControl = -1;
				paragraphFormat.KeepWithNext = 0;
				paragraphFormat.KeepTogether = 0;
				paragraphFormat.PageBreakBefore = 0;
				paragraphFormat.NoLineNumber = 0;
				paragraphFormat.Hyphenation = 0;
				paragraphFormat.FirstLineIndent = oWord.MillimetersToPoints(0f);
				paragraphFormat.OutlineLevel = WdOutlineLevel.wdOutlineLevelBodyText;
				paragraphFormat.CharacterUnitLeftIndent = 0f;
				paragraphFormat.CharacterUnitRightIndent = 0f;
				paragraphFormat.CharacterUnitFirstLineIndent = 0f;
				paragraphFormat.LineUnitBefore = 0f;
				paragraphFormat.LineUnitAfter = 0f;
				paragraphFormat.MirrorIndents = 0;
				paragraphFormat.TextboxTightWrap = WdTextboxTightWrap.wdTightNone;
				_ = null;
				table.Columns[1].Width = oWord.MillimetersToPoints(16f);
				table.Cell(1, 2).Merge(table.Cell(1, num11 + 1));
				int num20 = num2;
				for (int num21 = 0; num21 <= num20; num21++)
				{
					int num22 = num11 - 1;
					for (int num23 = 0; num23 <= num22; num23++)
					{
						if (Conversions.ToDouble(Module1.TchkToZpt(Conversions.ToString(array[m][num23, num21].KB))) >= 100.0)
						{
							table.Cell(num21 + 3, num23 + 2).Range.Font.Bold = -1;
						}
					}
				}
				table.Select();
				Selection selection3 = oWord.Selection;
				DefaultTableBehavior = WdCollapseDirection.wdCollapseEnd;
				selection3.Collapse(ref DefaultTableBehavior);
				oWord.Selection.Text = " ";
				int num24 = num10 - 1;
				for (int num25 = 2; num25 <= num24; num25++)
				{
					oWord.Selection.Font.Name = "Times New Roman";
					oWord.Selection.Font.Size = 10f;
					oWord.Selection.Range.ParagraphFormat.SpaceBefore = 0f;
					oWord.Selection.Range.ParagraphFormat.SpaceAfter = 0f;
					oWord.Selection.TypeParagraph();
					text = " \tR, m\t";
					int num26 = 1;
					do
					{
						text += "\t";
						num26++;
					}
					while (num26 <= 20);
					StringType.MidStmtStr(ref text, Strings.Len(text), 1, "\r");
					text += "H, m\t";
					int num27 = 0;
					do
					{
						text = text + Conversions.ToString((float)(21 + (num25 - 2) * 21 + num27) * Public2.PRJ.SETT.Tabl1StepL) + " m\t";
						num27++;
					}
					while (num27 <= 20);
					StringType.MidStmtStr(ref text, Strings.Len(text), 1, "\r");
					int num28 = num2;
					for (int num29 = 0; num29 <= num28; num29++)
					{
						text = text + Conversions.ToString(Math.Round((float)(num2 - num29) * Public2.PRJ.SETT.Tabl1StepH, 1)) + " m\t";
						int num30 = 0;
						do
						{
							text = text + Module1.SuperFormat(array[m][21 + (num25 - 2) * 21 + num30, num29].KB, 6) + "\t";
							num30++;
						}
						while (num30 <= 20);
						StringType.MidStmtStr(ref text, Strings.Len(text), 1, "\r");
					}
					oWord.Selection.Text = text;
					Selection selection4 = oWord.Selection;
					DefaultTableBehavior = WdTableFieldSeparator.wdSeparateByTabs;
					FindText = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchCase = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchWholeWord = oWord.MillimetersToPoints(12f);
					MatchWildcards = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchSoundsLike = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchAllWordForms = RuntimeHelpers.GetObjectValue(Missing.Value);
					Forward = RuntimeHelpers.GetObjectValue(Missing.Value);
					Wrap = RuntimeHelpers.GetObjectValue(Missing.Value);
					Format = RuntimeHelpers.GetObjectValue(Missing.Value);
					ReplaceWith = RuntimeHelpers.GetObjectValue(Missing.Value);
					Replace = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchKashida = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
					MatchControl = WdAutoFitBehavior.wdAutoFitFixed;
					MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
					Table table2 = selection4.ConvertToTable(ref DefaultTableBehavior, ref FindText, ref MatchCase, ref MatchWholeWord, ref MatchWildcards, ref MatchSoundsLike, ref MatchAllWordForms, ref Forward, ref Wrap, ref Format, ref ReplaceWith, ref Replace, ref MatchKashida, ref MatchDiacritics, ref MatchControl, ref MatchAlefHamza);
					table2.Rows.Alignment = WdRowAlignment.wdAlignRowLeft;
					table2.Rows.SetLeftIndent(0f, WdRulerStyle.wdAdjustNone);
					table2.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
					table2.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
					table2.Borders.InsideLineWidth = WdLineWidth.wdLineWidth050pt;
					table2.Borders.OutsideLineWidth = WdLineWidth.wdLineWidth050pt;
					table2.Range.Font.Name = "Times New Roman";
					table2.Range.Font.Size = 10f;
					table2.Range.Font.Bold = 0;
					table2.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
					ParagraphFormat paragraphFormat2 = table2.Range.ParagraphFormat;
					paragraphFormat2.LeftIndent = oWord.MillimetersToPoints(-2f);
					paragraphFormat2.RightIndent = oWord.MillimetersToPoints(-2f);
					paragraphFormat2.SpaceBefore = 2f;
					paragraphFormat2.SpaceBeforeAuto = 0;
					paragraphFormat2.SpaceAfter = 2f;
					paragraphFormat2.SpaceAfterAuto = 0;
					paragraphFormat2.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
					paragraphFormat2.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
					paragraphFormat2.WidowControl = -1;
					paragraphFormat2.KeepWithNext = 0;
					paragraphFormat2.KeepTogether = 0;
					paragraphFormat2.PageBreakBefore = 0;
					paragraphFormat2.NoLineNumber = 0;
					paragraphFormat2.Hyphenation = 0;
					paragraphFormat2.FirstLineIndent = oWord.MillimetersToPoints(0f);
					paragraphFormat2.OutlineLevel = WdOutlineLevel.wdOutlineLevelBodyText;
					paragraphFormat2.CharacterUnitLeftIndent = 0f;
					paragraphFormat2.CharacterUnitRightIndent = 0f;
					paragraphFormat2.CharacterUnitFirstLineIndent = 0f;
					paragraphFormat2.LineUnitBefore = 0f;
					paragraphFormat2.LineUnitAfter = 0f;
					paragraphFormat2.MirrorIndents = 0;
					paragraphFormat2.TextboxTightWrap = WdTextboxTightWrap.wdTightNone;
					_ = null;
					table2.Columns[1].Width = oWord.MillimetersToPoints(16f);
					table2.Cell(1, 2).Merge(table2.Cell(1, 22));
					int num31 = num2;
					for (int num32 = 0; num32 <= num31; num32++)
					{
						int num33 = num11 - 1;
						for (int num34 = 0; num34 <= num33; num34++)
						{
							if (Conversions.ToDouble(Module1.TchkToZpt(Conversions.ToString(array[m][21 + (num25 - 2) * 21 + num34, num32].KB))) >= 100.0)
							{
								table2.Cell(num32 + 3, num34 + 2).Range.Font.Bold = -1;
							}
						}
					}
					table2.Select();
					Selection selection5 = oWord.Selection;
					MatchAlefHamza = WdCollapseDirection.wdCollapseEnd;
					selection5.Collapse(ref MatchAlefHamza);
					oWord.Selection.Text = " ";
				}
				if (num12 <= 0)
				{
					continue;
				}
				oWord.Selection.Font.Name = "Times New Roman";
				oWord.Selection.Font.Size = 10f;
				oWord.Selection.Range.ParagraphFormat.SpaceBefore = 0f;
				oWord.Selection.Range.ParagraphFormat.SpaceAfter = 0f;
				oWord.Selection.TypeParagraph();
				text = " \tR, m\t";
				int num35 = num12 - 1;
				for (int num36 = 1; num36 <= num35; num36++)
				{
					text += "\t";
				}
				StringType.MidStmtStr(ref text, Strings.Len(text), 1, "\r");
				text += "H, m\t";
				int num37 = num12 - 1;
				for (int num38 = 0; num38 <= num37; num38++)
				{
					text = text + Conversions.ToString((float)(21 + (num10 - 2) * 21 + num38) * Public2.PRJ.SETT.Tabl1StepL) + " m\t";
				}
				StringType.MidStmtStr(ref text, Strings.Len(text), 1, "\r");
				int num39 = num2;
				for (int num40 = 0; num40 <= num39; num40++)
				{
					text = text + Conversions.ToString(Math.Round((float)(num2 - num40) * Public2.PRJ.SETT.Tabl1StepH, 1)) + " m\t";
					int num41 = num12 - 1;
					for (int num42 = 0; num42 <= num41; num42++)
					{
						text = text + Module1.SuperFormat(array[m][21 + (num10 - 2) * 21 + num42, num40].KB, 6) + "\t";
					}
					StringType.MidStmtStr(ref text, Strings.Len(text), 1, "\r");
				}
				oWord.Selection.Text = text;
				Selection selection6 = oWord.Selection;
				MatchAlefHamza = WdTableFieldSeparator.wdSeparateByTabs;
				MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchKashida = oWord.MillimetersToPoints(12f);
				Replace = RuntimeHelpers.GetObjectValue(Missing.Value);
				ReplaceWith = RuntimeHelpers.GetObjectValue(Missing.Value);
				Format = RuntimeHelpers.GetObjectValue(Missing.Value);
				Wrap = RuntimeHelpers.GetObjectValue(Missing.Value);
				Forward = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchAllWordForms = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchSoundsLike = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchWildcards = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchWholeWord = RuntimeHelpers.GetObjectValue(Missing.Value);
				MatchCase = RuntimeHelpers.GetObjectValue(Missing.Value);
				FindText = WdAutoFitBehavior.wdAutoFitFixed;
				DefaultTableBehavior = RuntimeHelpers.GetObjectValue(Missing.Value);
				Table table3 = selection6.ConvertToTable(ref MatchAlefHamza, ref MatchControl, ref MatchDiacritics, ref MatchKashida, ref Replace, ref ReplaceWith, ref Format, ref Wrap, ref Forward, ref MatchAllWordForms, ref MatchSoundsLike, ref MatchWildcards, ref MatchWholeWord, ref MatchCase, ref FindText, ref DefaultTableBehavior);
				table3.Rows.Alignment = WdRowAlignment.wdAlignRowLeft;
				table3.Rows.SetLeftIndent(0f, WdRulerStyle.wdAdjustNone);
				table3.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
				table3.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
				table3.Borders.InsideLineWidth = WdLineWidth.wdLineWidth050pt;
				table3.Borders.OutsideLineWidth = WdLineWidth.wdLineWidth050pt;
				table3.Range.Font.Name = "Times New Roman";
				table3.Range.Font.Size = 10f;
				table3.Range.Font.Bold = 0;
				table3.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
				ParagraphFormat paragraphFormat3 = table3.Range.ParagraphFormat;
				paragraphFormat3.LeftIndent = oWord.MillimetersToPoints(-2f);
				paragraphFormat3.RightIndent = oWord.MillimetersToPoints(-2f);
				paragraphFormat3.SpaceBefore = 2f;
				paragraphFormat3.SpaceBeforeAuto = 0;
				paragraphFormat3.SpaceAfter = 2f;
				paragraphFormat3.SpaceAfterAuto = 0;
				paragraphFormat3.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
				paragraphFormat3.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
				paragraphFormat3.WidowControl = -1;
				paragraphFormat3.KeepWithNext = 0;
				paragraphFormat3.KeepTogether = 0;
				paragraphFormat3.PageBreakBefore = 0;
				paragraphFormat3.NoLineNumber = 0;
				paragraphFormat3.Hyphenation = 0;
				paragraphFormat3.FirstLineIndent = oWord.MillimetersToPoints(0f);
				paragraphFormat3.OutlineLevel = WdOutlineLevel.wdOutlineLevelBodyText;
				paragraphFormat3.CharacterUnitLeftIndent = 0f;
				paragraphFormat3.CharacterUnitRightIndent = 0f;
				paragraphFormat3.CharacterUnitFirstLineIndent = 0f;
				paragraphFormat3.LineUnitBefore = 0f;
				paragraphFormat3.LineUnitAfter = 0f;
				paragraphFormat3.MirrorIndents = 0;
				paragraphFormat3.TextboxTightWrap = WdTextboxTightWrap.wdTightNone;
				_ = null;
				table3.Columns[1].Width = oWord.MillimetersToPoints(16f);
				table3.Cell(1, 2).Merge(table3.Cell(1, num12 - 1 + 2));
				int num43 = num2;
				for (int num44 = 0; num44 <= num43; num44++)
				{
					int num45 = num12 - 1;
					for (int num46 = 0; num46 <= num45; num46++)
					{
						if (Conversions.ToDouble(Module1.TchkToZpt(Conversions.ToString(array[m][21 + (num10 - 2) * 21 + num46, num44].KB))) >= 100.0)
						{
							table3.Cell(num44 + 3, num46 + 2).Range.Font.Bold = -1;
						}
					}
				}
				table3.Select();
				Selection selection7 = oWord.Selection;
				DefaultTableBehavior = WdCollapseDirection.wdCollapseEnd;
				selection7.Collapse(ref DefaultTableBehavior);
				oWord.Selection.Text = " ";
			}
		}
	}

	public static void WordAddGraphAllE(ref Microsoft.Office.Interop.Word.Application oWord, ref Document oDoc)
	{
		Find find = oWord.Selection.Find;
		find.Forward = true;
		find.ClearFormatting();
		find.MatchWholeWord = true;
		find.MatchCase = false;
		find.Wrap = WdFindWrap.wdFindContinue;
		object FindText = "{EFieldStrengthGraph_1}";
		object MatchCase = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchWholeWord = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchWildcards = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchSoundsLike = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchAllWordForms = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Forward = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Wrap = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Format = RuntimeHelpers.GetObjectValue(Missing.Value);
		object ReplaceWith = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Replace = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchKashida = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
		find.Execute(ref FindText, ref MatchCase, ref MatchWholeWord, ref MatchWildcards, ref MatchSoundsLike, ref MatchAllWordForms, ref Forward, ref Wrap, ref Format, ref ReplaceWith, ref Replace, ref MatchKashida, ref MatchDiacritics, ref MatchAlefHamza, ref MatchControl);
		_ = null;
		if (!oWord.Selection.Find.Found)
		{
			return;
		}
		short num = 0;
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.VP))
			{
				int upperBound = Public2.PRJ.VP.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					if (Public2.PRJ.VP[i].Activ)
					{
						num++;
					}
				}
			}
			if (num < 1)
			{
				return;
			}
			ResTBL[][] array = new ResTBL[Public2.PRJ.VP.GetUpperBound(0) + 1][];
			float num2 = Public2.PRJ.SETT.Graph1LengthMax / 300f;
			int upperBound2 = Public2.PRJ.VP.GetUpperBound(0);
			Public2.CoordMeters coordMeters = default;
			for (int j = 0; j <= upperBound2; j++)
			{
				if (Public2.PRJ.VP[j].Activ)
				{
					double num3 = Math.Sin((double)Public2.PRJ.VP[j].Azimuth * Constants.PI_180);
					double num4 = Math.Cos((double)Public2.PRJ.VP[j].Azimuth * Constants.PI_180);
					float num5 = Triangul.FindElevWithBLD(Public2.PRJ.VP[j].CoordWM);
					ref ResTBL[] reference = ref array[j];
					reference = (ResTBL[])Utils.CopyArray(reference, new ResTBL[301]);
					int num6 = 0;
					do
					{
						float num7 = num2 * (float)num6;
						coordMeters.Xm = Public2.PRJ.VP[j].CoordM.Xm + (double)num7 * num3;
						coordMeters.Ym = Public2.PRJ.VP[j].CoordM.Ym + (double)num7 * num4;
						array[j][num6].KB = CalcSZZ.CalcPointKB2((float)coordMeters.Xm, (float)coordMeters.Ym, num5 + Public2.PRJ.SETT.Graph1Height, ref array[j][num6].V, ref array[j][num6].PPEsum, ref array[j][num6].Esum);
						num6++;
					}
					while (num6 <= 300);
				}
			}
			System.Drawing.Font font = new System.Drawing.Font("Arial", 17f, FontStyle.Regular);
			SolidBrush brush = new SolidBrush(Color.Black);
			Pen pen = new Pen(Color.Black, 1f);
			pen.DashStyle = DashStyle.Dash;
			new Pen(Color.Red, 2f).DashStyle = DashStyle.Dash;
			short num8 = 1000;
			short num9 = 1000;
			short num10 = 30;
			short num11 = 35;
			short num12 = 80;
			short num13 = 53;
			Bitmap bitmap = new Bitmap((short)unchecked(checked((short)unchecked(num8 + num12)) + num13), (short)unchecked(checked((short)unchecked(num9 + num10)) + num11));
			Graphics graphics = Graphics.FromImage(bitmap);
			SizeF sizeF = default;
			graphics.Clear(Color.White);
			graphics.DrawLine(new Pen(Color.Black, 2f), num12, num10, (short)unchecked(num12 + num8), num10);
			graphics.DrawLine(new Pen(Color.Black, 2f), (short)unchecked(num12 + num8), num10, (short)unchecked(num12 + num8), (short)unchecked(num10 + num9));
			graphics.DrawLine(new Pen(Color.Black, 2f), (short)unchecked(num12 + num8), (short)unchecked(num10 + num9), num12, (short)unchecked(num10 + num9));
			graphics.DrawLine(new Pen(Color.Black, 2f), num12, (short)unchecked(num10 + num9), num12, num10);
			float num14 = 0f;
			int upperBound3 = Public2.PRJ.VP.GetUpperBound(0);
			for (int k = 0; k <= upperBound3; k++)
			{
				if (!Public2.PRJ.VP[k].Activ)
				{
					continue;
				}
				int upperBound4 = array[k].GetUpperBound(0);
				for (int l = 0; l <= upperBound4; l++)
				{
					if (array[k][l].Esum > num14)
					{
						num14 = array[k][l].Esum;
					}
				}
			}
			if (num14 == 0f)
			{
				num14 = 0.1f;
			}
			num14 = (float)((double)num14 * 1.05);
			float num15 = (float)num8 / Public2.PRJ.SETT.Graph1LengthMax;
			float num16 = (float)num9 / num14;
			float graph1LengthMax = Public2.PRJ.SETT.Graph1LengthMax;
			short num17 = default;
			int N = num17;
			double num18 = default;
			float dX = (float)num18;
			double num19 = default;
			float X = (float)num19;
			Module1.RazmetkaOSI(0f, graph1LengthMax, 8, 13, ref N, ref dX, ref X);
			num19 = X;
			num18 = dX;
			num17 = (short)N;
			N = num17 - 1;
			for (int m = 0; m <= N; m++)
			{
				int num20 = (int)Math.Round((double)num12 + num19 * (double)num15);
				graphics.DrawLine(pen, num20, num10, num20, (short)unchecked(num10 + num9));
				sizeF = graphics.MeasureString(Conversions.ToString(num19), font);
				graphics.DrawString(Conversions.ToString(num19), font, brush, (float)num20 - sizeF.Width / 2f, (short)unchecked(num10 + num9) + 2);
				num19 += num18;
			}
			sizeF = graphics.MeasureString("R, m", font);
			graphics.DrawString("R, m", font, brush, (short)unchecked(num12 + num8) + 3, (float)(short)unchecked(num10 + num9) - sizeF.Height);
			float xmax = num14;
			int N2 = num17;
			Module1.RazmetkaOSI2(0f, xmax, 8, 15, ref N2, ref num18, ref num19);
			num17 = (short)N2;
			N2 = num17 - 1;
			for (int n = 0; n <= N2; n++)
			{
				int num20 = (int)Math.Round((double)(short)unchecked(num10 + num9) - num19 * (double)num16);
				graphics.DrawLine(pen, num12, num20, (short)unchecked(num12 + num8), num20);
				sizeF = graphics.MeasureString(Conversions.ToString(num19), font);
				graphics.DrawString(Conversions.ToString(num19), font, brush, (float)num12 - sizeF.Width - 1f, (float)num20 - sizeF.Height / 2f);
				num19 += num18;
			}
			sizeF = graphics.MeasureString("R, m", font);
			graphics.DrawString("R, m", font, brush, (short)unchecked(num12 + num8) + 3, (float)(short)unchecked(num10 + num9) - sizeF.Height);
			graphics.DrawString("E-field Strength (V/m)", font, brush, num12 - 2, 0f);
			float num21 = num2 * num15;
			short num22 = 0;
			int upperBound5 = Public2.PRJ.VP.GetUpperBound(0);
			for (int num23 = 0; num23 <= upperBound5; num23++)
			{
				if (Public2.PRJ.VP[num23].Activ)
				{
					Point[] array2 = new Point[array[num23].GetUpperBound(0) + 1];
					int upperBound6 = array[num23].GetUpperBound(0);
					for (int num24 = 0; num24 <= upperBound6; num24++)
					{
						array2[num24] = new Point((int)Math.Round((float)num12 + (float)num24 * num21), (int)Math.Round((float)(short)unchecked(num10 + num9) - array[num23][num24].Esum * num16));
					}
					if (num22 <= CLR.GetUpperBound(0))
					{
						graphics.DrawLines(new Pen(CLR[num22], 4f), array2);
					}
					else
					{
						graphics.DrawLines(new Pen(Brushes.Black, 4f), array2);
					}
					num22++;
				}
			}
			bitmap.Save(Module1.FolderTMP + "\\GraphAll.jpg", ImageFormat.Jpeg);
			oWord.Selection.Range.Select();
			InlineShapes inlineShapes = oWord.Selection.InlineShapes;
			string fileName = Module1.FolderTMP + "\\GraphAll.jpg";
			MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
			InlineShape inlineShape = inlineShapes.AddPicture(fileName, ref MatchControl, ref MatchAlefHamza, ref MatchDiacritics);
			inlineShape.Width = oWord.MillimetersToPoints(170f);
			inlineShape.Height = oWord.MillimetersToPoints(170f);
			Tables tables = oDoc.Tables;
			Range range = oWord.Selection.Range;
			int numRows = num22 * 2;
			MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
			Table table = tables.Add(range, numRows, 2, ref MatchDiacritics, ref MatchAlefHamza);
			table.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
			table.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
			table.Range.ParagraphFormat.SpaceBefore = 0f;
			table.Range.ParagraphFormat.SpaceAfter = 0f;
			table.Range.Font.Name = "Times New Roman";
			table.Range.Font.Size = 10f;
			table.Columns[1].Width = oWord.MillimetersToPoints(10f);
			table.Columns[2].Width = oWord.MillimetersToPoints(160f);
			short num25 = 1;
			num22 = 0;
			int upperBound7 = Public2.PRJ.VP.GetUpperBound(0);
			for (int num26 = 0; num26 <= upperBound7; num26++)
			{
				if (Public2.PRJ.VP[num26].Activ)
				{
					unchecked
					{
						if (num22 <= CLR.GetUpperBound(0))
						{
							table.Cell(num25, 1).Shading.BackgroundPatternColor = (WdColor)checked(65536 * CLR[num22].B + 256 * CLR[num22].G + CLR[num22].R);
						}
						else
						{
							table.Cell(num25, 1).Shading.BackgroundPatternColor = WdColor.wdColorBlack;
						}
						table.Cell(num25, 2).Range.Text = "- graph at hight " + Conversions.ToString(Public2.PRJ.SETT.Graph1Height) + " m along azimuth " + Conversions.ToString(Public2.PRJ.VP[num26].Azimuth) + "° (along direction of antenna " + Strings.Replace(Public2.PRJ.VP[num26].Name, "\r\n", "; ") + ")";
					}
					table.Rows[num25 + 1].Height = oWord.MillimetersToPoints(1f);
					table.Rows[num25 + 1].HeightRule = WdRowHeightRule.wdRowHeightExactly;
					num25 += 2;
					num22++;
				}
			}
			table.Range.Select();
			Selection selection = oWord.Selection;
			MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
			selection.MoveDown(ref MatchAlefHamza, ref MatchDiacritics, ref MatchControl);
		}
	}

	public static void WordAddGraphAll(ref Microsoft.Office.Interop.Word.Application oWord, ref Document oDoc)
	{
		Find find = oWord.Selection.Find;
		find.Forward = true;
		find.ClearFormatting();
		find.MatchWholeWord = true;
		find.MatchCase = false;
		find.Wrap = WdFindWrap.wdFindContinue;
		object FindText = "{SimultaneousExposureGraph_1}";
		object MatchCase = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchWholeWord = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchWildcards = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchSoundsLike = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchAllWordForms = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Forward = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Wrap = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Format = RuntimeHelpers.GetObjectValue(Missing.Value);
		object ReplaceWith = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Replace = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchKashida = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
		find.Execute(ref FindText, ref MatchCase, ref MatchWholeWord, ref MatchWildcards, ref MatchSoundsLike, ref MatchAllWordForms, ref Forward, ref Wrap, ref Format, ref ReplaceWith, ref Replace, ref MatchKashida, ref MatchDiacritics, ref MatchAlefHamza, ref MatchControl);
		_ = null;
		if (!oWord.Selection.Find.Found)
		{
			return;
		}
		short num = 0;
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.VP))
			{
				int upperBound = Public2.PRJ.VP.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					if (Public2.PRJ.VP[i].Activ)
					{
						num++;
					}
				}
			}
			if (num < 1)
			{
				return;
			}
			ResTBL[][] array = new ResTBL[Public2.PRJ.VP.GetUpperBound(0) + 1][];
			float num2 = Public2.PRJ.SETT.Graph1LengthMax / 300f;
			int upperBound2 = Public2.PRJ.VP.GetUpperBound(0);
			Public2.CoordMeters coordMeters = default;
			for (int j = 0; j <= upperBound2; j++)
			{
				if (Public2.PRJ.VP[j].Activ)
				{
					double num3 = Math.Sin((double)Public2.PRJ.VP[j].Azimuth * Constants.PI_180);
					double num4 = Math.Cos((double)Public2.PRJ.VP[j].Azimuth * Constants.PI_180);
					float num5 = Triangul.FindElevWithBLD(Public2.PRJ.VP[j].CoordWM);
					ref ResTBL[] reference = ref array[j];
					reference = (ResTBL[])Utils.CopyArray(reference, new ResTBL[301]);
					int num6 = 0;
					do
					{
						float num7 = num2 * (float)num6;
						coordMeters.Xm = Public2.PRJ.VP[j].CoordM.Xm + (double)num7 * num3;
						coordMeters.Ym = Public2.PRJ.VP[j].CoordM.Ym + (double)num7 * num4;
						array[j][num6].KB = CalcSZZ.CalcPointKB2((float)coordMeters.Xm, (float)coordMeters.Ym, num5 + Public2.PRJ.SETT.Graph1Height, ref array[j][num6].V, ref array[j][num6].PPEsum, ref array[j][num6].Esum) * 100f;
						num6++;
					}
					while (num6 <= 300);
				}
			}
			System.Drawing.Font font = new System.Drawing.Font("Arial", 17f, FontStyle.Regular);
			SolidBrush brush = new SolidBrush(Color.Black);
			Pen pen = new Pen(Color.Black, 1f);
			pen.DashStyle = DashStyle.Dash;
			new Pen(Color.Red, 2f).DashStyle = DashStyle.Dash;
			short num8 = 1000;
			short num9 = 1000;
			short num10 = 30;
			short num11 = 35;
			short num12 = 80;
			short num13 = 53;
			Bitmap bitmap = new Bitmap((short)unchecked(checked((short)unchecked(num8 + num12)) + num13), (short)unchecked(checked((short)unchecked(num9 + num10)) + num11));
			Graphics graphics = Graphics.FromImage(bitmap);
			SizeF sizeF = default;
			graphics.Clear(Color.White);
			graphics.DrawLine(new Pen(Color.Black, 2f), num12, num10, (short)unchecked(num12 + num8), num10);
			graphics.DrawLine(new Pen(Color.Black, 2f), (short)unchecked(num12 + num8), num10, (short)unchecked(num12 + num8), (short)unchecked(num10 + num9));
			graphics.DrawLine(new Pen(Color.Black, 2f), (short)unchecked(num12 + num8), (short)unchecked(num10 + num9), num12, (short)unchecked(num10 + num9));
			graphics.DrawLine(new Pen(Color.Black, 2f), num12, (short)unchecked(num10 + num9), num12, num10);
			float num14 = 0f;
			int upperBound3 = Public2.PRJ.VP.GetUpperBound(0);
			for (int k = 0; k <= upperBound3; k++)
			{
				if (!Public2.PRJ.VP[k].Activ)
				{
					continue;
				}
				int upperBound4 = array[k].GetUpperBound(0);
				for (int l = 0; l <= upperBound4; l++)
				{
					if (array[k][l].KB > num14)
					{
						num14 = array[k][l].KB;
					}
				}
			}
			num14 = (float)((double)num14 * 1.05);
			float num15 = (float)num8 / Public2.PRJ.SETT.Graph1LengthMax;
			float num16 = (float)num9 / num14;
			float graph1LengthMax = Public2.PRJ.SETT.Graph1LengthMax;
			short num17 = default;
			int N = num17;
			double num18 = default;
			float dX = (float)num18;
			double num19 = default;
			float X = (float)num19;
			Module1.RazmetkaOSI(0f, graph1LengthMax, 8, 13, ref N, ref dX, ref X);
			num19 = X;
			num18 = dX;
			num17 = (short)N;
			N = num17 - 1;
			for (int m = 0; m <= N; m++)
			{
				int num20 = (int)Math.Round((double)num12 + num19 * (double)num15);
				graphics.DrawLine(pen, num20, num10, num20, (short)unchecked(num10 + num9));
				sizeF = graphics.MeasureString(Conversions.ToString(num19), font);
				graphics.DrawString(Conversions.ToString(num19), font, brush, (float)num20 - sizeF.Width / 2f, (short)unchecked(num10 + num9) + 2);
				num19 += num18;
			}
			sizeF = graphics.MeasureString("R, m", font);
			graphics.DrawString("R, m", font, brush, (short)unchecked(num12 + num8) + 3, (float)(short)unchecked(num10 + num9) - sizeF.Height);
			float xmax = num14;
			int N2 = num17;
			Module1.RazmetkaOSI2(0f, xmax, 8, 15, ref N2, ref num18, ref num19);
			num17 = (short)N2;
			N2 = num17 - 1;
			for (int n = 0; n <= N2; n++)
			{
				int num20 = (int)Math.Round((double)(short)unchecked(num10 + num9) - num19 * (double)num16);
				graphics.DrawLine(pen, num12, num20, (short)unchecked(num12 + num8), num20);
				sizeF = graphics.MeasureString(Conversions.ToString(num19), font);
				graphics.DrawString(Conversions.ToString(num19), font, brush, (float)num12 - sizeF.Width - 1f, (float)num20 - sizeF.Height / 2f);
				num19 += num18;
			}
			sizeF = graphics.MeasureString("R, m", font);
			graphics.DrawString("R, m", font, brush, (short)unchecked(num12 + num8) + 3, (float)(short)unchecked(num10 + num9) - sizeF.Height);
			graphics.DrawString("Simultaneous exposure (%)", font, brush, num12 - 2, 0f);
			float num21 = num2 * num15;
			short num22 = 0;
			int upperBound5 = Public2.PRJ.VP.GetUpperBound(0);
			for (int num23 = 0; num23 <= upperBound5; num23++)
			{
				if (Public2.PRJ.VP[num23].Activ)
				{
					Point[] array2 = new Point[array[num23].GetUpperBound(0) + 1];
					int upperBound6 = array[num23].GetUpperBound(0);
					for (int num24 = 0; num24 <= upperBound6; num24++)
					{
						array2[num24] = new Point((int)Math.Round((float)num12 + (float)num24 * num21), (int)Math.Round((float)(short)unchecked(num10 + num9) - array[num23][num24].KB * num16));
					}
					if (num22 <= CLR.GetUpperBound(0))
					{
						graphics.DrawLines(new Pen(CLR[num22], 4f), array2);
					}
					else
					{
						graphics.DrawLines(new Pen(Brushes.Black, 4f), array2);
					}
					num22++;
				}
			}
			bitmap.Save(Module1.FolderTMP + "\\GraphAll.jpg", ImageFormat.Jpeg);
			oWord.Selection.Range.Select();
			InlineShapes inlineShapes = oWord.Selection.InlineShapes;
			string fileName = Module1.FolderTMP + "\\GraphAll.jpg";
			MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
			InlineShape inlineShape = inlineShapes.AddPicture(fileName, ref MatchControl, ref MatchAlefHamza, ref MatchDiacritics);
			inlineShape.Width = oWord.MillimetersToPoints(170f);
			inlineShape.Height = oWord.MillimetersToPoints(170f);
			Tables tables = oDoc.Tables;
			Range range = oWord.Selection.Range;
			int numRows = num22 * 2;
			MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
			Table table = tables.Add(range, numRows, 2, ref MatchDiacritics, ref MatchAlefHamza);
			table.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
			table.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
			table.Range.ParagraphFormat.SpaceBefore = 0f;
			table.Range.ParagraphFormat.SpaceAfter = 0f;
			table.Range.Font.Name = "Times New Roman";
			table.Range.Font.Size = 10f;
			table.Columns[1].Width = oWord.MillimetersToPoints(10f);
			table.Columns[2].Width = oWord.MillimetersToPoints(160f);
			short num25 = 1;
			num22 = 0;
			int upperBound7 = Public2.PRJ.VP.GetUpperBound(0);
			for (int num26 = 0; num26 <= upperBound7; num26++)
			{
				if (Public2.PRJ.VP[num26].Activ)
				{
					unchecked
					{
						if (num22 <= CLR.GetUpperBound(0))
						{
							table.Cell(num25, 1).Shading.BackgroundPatternColor = (WdColor)checked(65536 * CLR[num22].B + 256 * CLR[num22].G + CLR[num22].R);
						}
						else
						{
							table.Cell(num25, 1).Shading.BackgroundPatternColor = WdColor.wdColorBlack;
						}
						table.Cell(num25, 2).Range.Text = "- graph at hight " + Conversions.ToString(Public2.PRJ.SETT.Graph1Height) + " m along azimuth " + Conversions.ToString(Public2.PRJ.VP[num26].Azimuth) + "° (along direction of antenna " + Strings.Replace(Public2.PRJ.VP[num26].Name, "\r\n", "; ") + ")";
					}
					table.Rows[num25 + 1].Height = oWord.MillimetersToPoints(1f);
					table.Rows[num25 + 1].HeightRule = WdRowHeightRule.wdRowHeightExactly;
					num25 += 2;
					num22++;
				}
			}
			table.Range.Select();
			Selection selection = oWord.Selection;
			MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
			selection.MoveDown(ref MatchAlefHamza, ref MatchDiacritics, ref MatchControl);
		}
	}

	public static void WordAddCoordSitPlan(ref Microsoft.Office.Interop.Word.Application oWord)
	{
		Find find = oWord.Selection.Find;
		find.Forward = true;
		find.ClearFormatting();
		find.MatchWholeWord = true;
		find.MatchCase = false;
		find.Wrap = WdFindWrap.wdFindContinue;
		object FindText = "{Coordinates}";
		object MatchCase = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchWholeWord = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchWildcards = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchSoundsLike = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchAllWordForms = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Forward = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Wrap = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Format = RuntimeHelpers.GetObjectValue(Missing.Value);
		object ReplaceWith = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Replace = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchKashida = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
		find.Execute(ref FindText, ref MatchCase, ref MatchWholeWord, ref MatchWildcards, ref MatchSoundsLike, ref MatchAllWordForms, ref Forward, ref Wrap, ref Format, ref ReplaceWith, ref Replace, ref MatchKashida, ref MatchDiacritics, ref MatchAlefHamza, ref MatchControl);
		_ = null;
		if (oWord.Selection.Find.Found)
		{
			object MatchSoundsLike2 = RuntimeHelpers.GetObjectValue(Type.Missing);
			Find find2 = oWord.Selection.Find;
			object Wrap2 = WdFindWrap.wdFindContinue;
			object Replace2 = WdReplace.wdReplaceAll;
			find2.Text = "{Coordinates}";
			find2.Replacement.Text = CalcCoords.ConvLatToStr(Public2.PRJ.SETT.PRTOCoordWGS.Lat) + " " + CalcCoords.ConvLonToStr(Public2.PRJ.SETT.PRTOCoordWGS.Lon);
			MatchControl = RuntimeHelpers.GetObjectValue(Type.Missing);
			MatchAlefHamza = false;
			MatchDiacritics = false;
			MatchKashida = false;
			Replace = false;
			ReplaceWith = true;
			Format = false;
			Wrap = RuntimeHelpers.GetObjectValue(Missing.Value);
			Forward = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchAllWordForms = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchSoundsLike = RuntimeHelpers.GetObjectValue(Missing.Value);
			find2.Execute(ref MatchControl, ref MatchAlefHamza, ref MatchDiacritics, ref MatchKashida, ref MatchSoundsLike2, ref Replace, ref ReplaceWith, ref Wrap2, ref Format, ref MatchSoundsLike2, ref Replace2, ref Wrap, ref Forward, ref MatchAllWordForms, ref MatchSoundsLike);
		}
	}

	public static void WordAddTableEMFSafetyVolumes1(ref Microsoft.Office.Interop.Word.Application oWord)
	{
		Find find = oWord.Selection.Find;
		find.Forward = true;
		find.ClearFormatting();
		find.MatchWholeWord = true;
		find.MatchCase = false;
		find.Wrap = WdFindWrap.wdFindContinue;
		object FindText = "{EMF_Safety_Volumes1}";
		object MatchCase = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchWholeWord = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchWildcards = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchSoundsLike = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchAllWordForms = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Forward = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Wrap = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Format = RuntimeHelpers.GetObjectValue(Missing.Value);
		object ReplaceWith = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Replace = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchKashida = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
		find.Execute(ref FindText, ref MatchCase, ref MatchWholeWord, ref MatchWildcards, ref MatchSoundsLike, ref MatchAllWordForms, ref Forward, ref Wrap, ref Format, ref ReplaceWith, ref Replace, ref MatchKashida, ref MatchDiacritics, ref MatchAlefHamza, ref MatchControl);
		_ = null;
		ResEmfSafetyVolumes1[] ESV = default;
		if (!oWord.Selection.Find.Found || !CalcEmfSafetyVolumes(ref ESV))
		{
			return;
		}
		oWord.Selection.Range.Select();
		string text = "";
		text = "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t";
		StringType.MidStmtStr(ref text, Strings.Len(text), 1, "\r");
		text += "\tLv\tLh\tLm1\tLm2\tLv\tLh\tLm1\tLm2\tLv\tLh\tLm1\tLm2\tLv\tLh\tLm1\tLm2\t";
		StringType.MidStmtStr(ref text, Strings.Len(text), 1, "\r");
		int upperBound = ESV.GetUpperBound(0);
		checked
		{
			for (int i = 0; i <= upperBound; i++)
			{
				if (!ESV[i].Active)
				{
					continue;
				}
				text = text + ESV[i].Name + "\t";
				int num = 0;
				do
				{
					if (ESV[i].SV[num].USE)
					{
						text = text + Strings.Format(ESV[i].SV[num].Lv, "0.0") + "\t";
						text = text + Strings.Format(ESV[i].SV[num].Lh, "0.0") + "\t";
						text = text + Strings.Format(ESV[i].SV[num].Lm1, "0.0") + "\t";
						text = text + Strings.Format(ESV[i].SV[num].Lm2, "0.0") + "\t";
					}
					else
					{
						text += " \t";
						text += " \t";
						text += " \t";
						text += " \t";
					}
					num++;
				}
				while (num <= 3);
				StringType.MidStmtStr(ref text, Strings.Len(text), 1, "\r");
			}
			oWord.Selection.Text = text;
			Selection selection = oWord.Selection;
			MatchControl = WdTableFieldSeparator.wdSeparateByTabs;
			MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchKashida = oWord.MillimetersToPoints(10f);
			Replace = RuntimeHelpers.GetObjectValue(Missing.Value);
			ReplaceWith = RuntimeHelpers.GetObjectValue(Missing.Value);
			Format = RuntimeHelpers.GetObjectValue(Missing.Value);
			Wrap = RuntimeHelpers.GetObjectValue(Missing.Value);
			Forward = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchAllWordForms = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchSoundsLike = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchWildcards = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchWholeWord = RuntimeHelpers.GetObjectValue(Missing.Value);
			MatchCase = RuntimeHelpers.GetObjectValue(Missing.Value);
			FindText = WdAutoFitBehavior.wdAutoFitFixed;
			object DefaultTableBehavior = RuntimeHelpers.GetObjectValue(Missing.Value);
			Table table = selection.ConvertToTable(ref MatchControl, ref MatchAlefHamza, ref MatchDiacritics, ref MatchKashida, ref Replace, ref ReplaceWith, ref Format, ref Wrap, ref Forward, ref MatchAllWordForms, ref MatchSoundsLike, ref MatchWildcards, ref MatchWholeWord, ref MatchCase, ref FindText, ref DefaultTableBehavior);
			table.Rows.Alignment = WdRowAlignment.wdAlignRowCenter;
			table.Borders.InsideLineStyle = WdLineStyle.wdLineStyleSingle;
			table.Borders.OutsideLineStyle = WdLineStyle.wdLineStyleSingle;
			table.Borders.InsideLineWidth = WdLineWidth.wdLineWidth050pt;
			table.Borders.OutsideLineWidth = WdLineWidth.wdLineWidth050pt;
			table.Range.Font.Name = "Times New Roman";
			table.Range.Font.Size = 10f;
			table.Range.Font.Bold = 0;
			table.Range.Cells.VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
			ParagraphFormat paragraphFormat = table.Range.ParagraphFormat;
			paragraphFormat.LeftIndent = oWord.MillimetersToPoints(-2f);
			paragraphFormat.RightIndent = oWord.MillimetersToPoints(-2f);
			paragraphFormat.SpaceBefore = 0f;
			paragraphFormat.SpaceBeforeAuto = 0;
			paragraphFormat.SpaceAfter = 0f;
			paragraphFormat.SpaceAfterAuto = 0;
			paragraphFormat.LineSpacingRule = WdLineSpacing.wdLineSpaceSingle;
			paragraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
			paragraphFormat.WidowControl = -1;
			paragraphFormat.KeepWithNext = 0;
			paragraphFormat.KeepTogether = 0;
			paragraphFormat.PageBreakBefore = 0;
			paragraphFormat.NoLineNumber = 0;
			paragraphFormat.Hyphenation = 0;
			paragraphFormat.FirstLineIndent = oWord.MillimetersToPoints(0f);
			paragraphFormat.OutlineLevel = WdOutlineLevel.wdOutlineLevelBodyText;
			paragraphFormat.CharacterUnitLeftIndent = 0f;
			paragraphFormat.CharacterUnitRightIndent = 0f;
			paragraphFormat.CharacterUnitFirstLineIndent = 0f;
			paragraphFormat.LineUnitBefore = 0f;
			paragraphFormat.LineUnitAfter = 0f;
			paragraphFormat.MirrorIndents = 0;
			paragraphFormat.TextboxTightWrap = WdTextboxTightWrap.wdTightNone;
			_ = null;
			table.Columns[1].Select();
			oWord.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphLeft;
			oWord.Selection.ParagraphFormat.LeftIndent = oWord.MillimetersToPoints(0f);
			table.Columns[1].Width = oWord.MillimetersToPoints(35f);
			table.Columns[2].Width = oWord.MillimetersToPoints(9f);
			table.Columns[3].Width = oWord.MillimetersToPoints(9f);
			table.Columns[4].Width = oWord.MillimetersToPoints(9f);
			table.Columns[5].Width = oWord.MillimetersToPoints(9f);
			table.Columns[6].Width = oWord.MillimetersToPoints(9f);
			table.Columns[7].Width = oWord.MillimetersToPoints(9f);
			table.Columns[8].Width = oWord.MillimetersToPoints(9f);
			table.Columns[9].Width = oWord.MillimetersToPoints(9f);
			table.Columns[10].Width = oWord.MillimetersToPoints(9f);
			table.Columns[11].Width = oWord.MillimetersToPoints(9f);
			table.Columns[12].Width = oWord.MillimetersToPoints(9f);
			table.Columns[13].Width = oWord.MillimetersToPoints(9f);
			table.Columns[14].Width = oWord.MillimetersToPoints(9f);
			table.Columns[15].Width = oWord.MillimetersToPoints(9f);
			table.Columns[16].Width = oWord.MillimetersToPoints(9f);
			table.Columns[17].Width = oWord.MillimetersToPoints(9f);
			table.Cell(1, 2).Merge(table.Cell(1, 5));
			table.Cell(1, 3).Merge(table.Cell(1, 6));
			table.Cell(1, 4).Merge(table.Cell(1, 7));
			table.Cell(1, 5).Merge(table.Cell(1, 8));
			table.Cell(1, 1).Range.Text = "Name";
			table.Cell(1, 2).Range.Text = "Limit 1";
			table.Cell(1, 3).Range.Text = "Limit 2";
			table.Cell(1, 4).Range.Text = "Limit 3";
			table.Cell(1, 5).Range.Text = "Limit 4";
			table.Rows[1].Select();
			oWord.Selection.Range.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
			_ = null;
			table.Cell(1, 1).Merge(table.Cell(2, 1));
		}
	}

	public static bool CalcEmfSafetyVolumes(ref ResEmfSafetyVolumes1[] ESV)
	{
		if (!CalcSZZ.CalcTmp())
		{
			return false;
		}
		checked
		{
			ESV = new ResEmfSafetyVolumes1[Public2.PRJ.TX.GetUpperBound(0) + 1];
			int upperBound = Public2.PRJ.TX.GetUpperBound(0);
			for (int i = 0; i <= upperBound; i++)
			{
				ESV[i].SV = new ResEmfSafetyVolumes2[4];
				ESV[i].Active = Public2.PRJ.TX[i].Active;
				if (!Public2.PRJ.TX[i].Active)
				{
					continue;
				}
				int num = 0;
				do
				{
					if (CalcSZZ.TMP[i].PDU[num] < 1000000f)
					{
						ESV[i].SV[num].USE = true;
						ESV[i].Name = Module1.Reject0D0A09(Public2.PRJ.TX[i].Name);
						if ((Public2.PRJ.TX[i].DnH == null) | (Public2.PRJ.TX[i].DnV == null))
						{
							Interaction.MsgBox(Localization.LocMSG(84) + " " + Conversions.ToString(i));
							return false;
						}
						float num2 = (float)((double)CalcSZZ.TMP[i].PROIZV / Math.Sqrt(3.77 * (double)CalcSZZ.TMP[i].PDU[num] / (double)CalcSZZ.TMP[i].AttenuationFactor[num]));
						float num3 = 0f;
						float num4 = 0f;
						int num5 = 0;
						do
						{
							float num6 = (float)((double)num5 * Constants.PI_180);
							short num7 = (short)Math.Round((float)num5 - CalcSZZ.TMP[i].Tilt);
							if (num7 < 0)
							{
								num7 += 360;
							}
							if (num7 >= 360)
							{
								num7 -= 360;
							}
							float num8 = (float)((double)num2 * Math.Sin(num6) * (double)CalcSZZ.TMP[i].DnV[num5]);
							if (num8 > num3)
							{
								num3 = num8;
							}
							if (num8 < num4)
							{
								num4 = num8;
							}
							num5++;
						}
						while (num5 <= 359);
						ESV[i].SV[num].Lv = num3 - num4;
						float num9 = 0f;
						float num10 = 0f;
						float num11 = 0f;
						float num12 = 0f;
						int num13 = 0;
						do
						{
							float num14 = (float)((double)num13 * Constants.PI_180);
							float num15 = (float)((double)num2 * Math.Cos(num14) * (double)CalcSZZ.TMP[i].DnH[num13]);
							float num16 = (float)((double)num2 * Math.Sin(num14) * (double)CalcSZZ.TMP[i].DnH[num13]);
							if (num15 > num9)
							{
								num9 = num15;
							}
							if (num15 < num10)
							{
								num10 = num15;
							}
							if (num16 > num11)
							{
								num11 = num16;
							}
							if (num16 < num12)
							{
								num12 = num16;
							}
							num13++;
						}
						while (num13 <= 359);
						ESV[i].SV[num].Lm1 = num9;
						ESV[i].SV[num].Lm2 = Math.Abs(num10);
						ESV[i].SV[num].Lh = num11 - num12;
					}
					else
					{
						ESV[i].SV[num].USE = false;
					}
					num++;
				}
				while (num <= 3);
			}
			return true;
		}
	}

	public static Bitmap wDrawHorizPlane()
	{
		if (Public2.PRJ.SETT.ShowHorizPlaneScale)
		{
			Mup = 15;
			Mdown = 30;
			Mleft = 70;
			Mright = 20;
		}
		else
		{
			Mup = 0;
			Mdown = 0;
			Mleft = 0;
			Mright = 0;
		}
		Bitmap bitmap = wDrawMap();
		checked
		{
			wSizeX = (short)bitmap.Width;
			wSizeY = (short)bitmap.Height;
			M2Pix = (double)wSizeX / (Public2.PRJ.SETT.HorAreaCoordM[1].Xm - Public2.PRJ.SETT.HorAreaCoordM[0].Xm);
			WM2Pix = (double)wSizeX / (Public2.PRJ.SETT.HorAreaCoordWM[1].Xwm - Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm);
			Graphics g = Graphics.FromImage(bitmap);
			wDrawLines(g);
			wDrawBuildings(g);
			wDrawHorizontalPlanes(g);
			wDrawAntennas(g);
			wDrawControlPoints(g);
			wDrawNorth(g);
			wDrawMesureStrip(g);
			wDrawElevation(g);
			wDrawConcentricCircles(g);
			Bitmap bitmap2 = new Bitmap((short)unchecked(checked((short)unchecked(wSizeX + Mleft)) + Mright), (short)unchecked(checked((short)unchecked(wSizeY + Mup)) + Mdown));
			Graphics graphics = Graphics.FromImage(bitmap2);
			graphics.Clear(Color.White);
			graphics.DrawImage(bitmap, Mleft, Mup, bitmap.Width, bitmap.Height);
			wDrawHorizontalArea(graphics);
			bitmap.Dispose();
			graphics.Dispose();
			GC.Collect();
			GC.GetTotalMemory(forceFullCollection: true);
			return bitmap2;
		}
	}

	public static Bitmap wDrawMap()
	{
		GC.Collect();
		GC.GetTotalMemory(forceFullCollection: true);
		double Xt = default;
		double Yt = default;
		CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.SETT.HorAreaCoordWM[0], ref Xt, ref Yt);
		double Xt2 = default;
		double Yt2 = default;
		CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.SETT.HorAreaCoordWM[1], ref Xt2, ref Yt2);
		checked
		{
			int num = (int)Math.Floor(Xt);
			int num2 = (int)Math.Floor(Yt2);
			int num3 = (int)Math.Ceiling(Xt2);
			int num4 = (int)Math.Ceiling(Yt);
			short width = (short)((num3 - num) * 256);
			short height = (short)((num4 - num2) * 256);
			CalcTileWeb.ListTiles = null;
			int num5 = -1;
			string NameTileCACH = default;
			string NameTileURL = default;
			string NameTileDir = default;
			if (Public2.PRJ.SETT.ShowMap)
			{
				int num6 = num3;
				for (int i = num; i <= num6; i++)
				{
					int num7 = num4;
					for (int j = num2; j <= num7; j++)
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
								num5++;
								CalcTileWeb.ListTiles = (CalcTileWeb.TileItem[])Utils.CopyArray(CalcTileWeb.ListTiles, new CalcTileWeb.TileItem[num5 + 1]);
								CalcTileWeb.ListTiles[num5].FileName = NameTileCACH;
								CalcTileWeb.ListTiles[num5].FolderName = NameTileDir;
								CalcTileWeb.ListTiles[num5].UrlName = NameTileURL;
								CalcTileWeb.ListTiles[num5].X = i;
								CalcTileWeb.ListTiles[num5].Y = j;
								CalcTileWeb.ListTiles[num5].Z = Public2.PRJ.SETT.ZOOM;
							}
						}
						else
						{
							num5 = 0;
							CalcTileWeb.ListTiles = (CalcTileWeb.TileItem[])Utils.CopyArray(CalcTileWeb.ListTiles, new CalcTileWeb.TileItem[num5 + 1]);
							CalcTileWeb.ListTiles[num5].FileName = NameTileCACH;
							CalcTileWeb.ListTiles[num5].FolderName = NameTileDir;
							CalcTileWeb.ListTiles[num5].UrlName = NameTileURL;
							CalcTileWeb.ListTiles[num5].X = i;
							CalcTileWeb.ListTiles[num5].Y = j;
							CalcTileWeb.ListTiles[num5].Z = Public2.PRJ.SETT.ZOOM;
						}
					}
				}
				if (!Information.IsNothing(CalcTileWeb.ListTiles))
				{
					MyProject.Forms.frmBckGrWrkDownloadTiles.ShowDialog();
				}
			}
			Bitmap image = new Bitmap(width, height);
			Graphics graphics = Graphics.FromImage(image);
			graphics.Clear(Color.White);
			if (Public2.PRJ.SETT.ShowMap)
			{
				int num8 = num3;
				for (int l = num; l <= num8; l++)
				{
					int num9 = num4;
					for (int m = num2; m <= num9; m++)
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
			short num10 = (short)Math.Round((Xt - (double)num) * 256.0);
			short num11 = (short)Math.Round((Yt2 - (double)num2) * 256.0);
			short width2 = (short)Math.Round((Xt2 - Xt) * 256.0);
			short height2 = (short)Math.Round((Yt - Yt2) * 256.0);
			Bitmap bitmap = new Bitmap(width2, height2);
			graphics = Graphics.FromImage(bitmap);
			graphics.DrawImage(image, (short)unchecked(-num10), (short)unchecked(-num11), width, height);
			short width3;
			short height3;
			if (Public2.PRJ.SETT.HorAreaCoordM[1].Xm - Public2.PRJ.SETT.HorAreaCoordM[0].Xm > Public2.PRJ.SETT.HorAreaCoordM[1].Ym - Public2.PRJ.SETT.HorAreaCoordM[0].Ym)
			{
				width3 = 1500;
				height3 = (short)Math.Round(1500.0 * (Public2.PRJ.SETT.HorAreaCoordM[1].Ym - Public2.PRJ.SETT.HorAreaCoordM[0].Ym) / (Public2.PRJ.SETT.HorAreaCoordM[1].Xm - Public2.PRJ.SETT.HorAreaCoordM[0].Xm));
			}
			else
			{
				width3 = (short)Math.Round(1500.0 * (Public2.PRJ.SETT.HorAreaCoordM[1].Xm - Public2.PRJ.SETT.HorAreaCoordM[0].Xm) / (Public2.PRJ.SETT.HorAreaCoordM[1].Ym - Public2.PRJ.SETT.HorAreaCoordM[0].Ym));
				height3 = 1500;
			}
			image = new Bitmap(width3, height3);
			graphics = Graphics.FromImage(image);
			graphics.DrawImage(bitmap, 0, 0, width3, height3);
			bitmap.Dispose();
			graphics.Dispose();
			GC.Collect();
			GC.GetTotalMemory(forceFullCollection: true);
			return image;
		}
	}

	public static void wDrawHorizontalArea(Graphics G)
	{
		Pen pen = new Pen(Color.Black, 1f);
		pen.DashStyle = DashStyle.Custom;
		pen.DashPattern = new float[2] { 6f, 3f };
		checked
		{
			if (Public2.PRJ.SETT.ShowHorizPlaneScale)
			{
				G.DrawLine(new Pen(Color.Black, 3f), Mleft, Mup, (short)unchecked(Mleft + wSizeX) - 1, Mup);
				G.DrawLine(new Pen(Color.Black, 3f), Mleft, (short)unchecked(Mup + wSizeY) - 1, (short)unchecked(Mleft + wSizeX) - 1, (short)unchecked(Mup + wSizeY) - 1);
				G.DrawLine(new Pen(Color.Black, 3f), Mleft, Mup, Mleft, (short)unchecked(Mup + wSizeY) - 1);
				G.DrawLine(new Pen(Color.Black, 3f), (short)unchecked(Mleft + wSizeX) - 1, Mup, (short)unchecked(Mleft + wSizeX) - 1, (short)unchecked(Mup + wSizeY) - 1);
				SizeF sizeF = default;
				float xmin = (float)Public2.PRJ.SETT.HorAreaCoordM[0].Xm;
				float xmax = (float)Public2.PRJ.SETT.HorAreaCoordM[1].Xm;
				short num = default;
				int N = num;
				float dX = default;
				float X = default;
				Module1.RazmetkaOSI(xmin, xmax, 8, 13, ref N, ref dX, ref X);
				num = (short)N;
				N = num - 1;
				for (int i = 0; i <= N; i++)
				{
					float num2 = X + (float)i * dX;
					int num3 = (int)Math.Round((double)Mleft + ((double)num2 - Public2.PRJ.SETT.HorAreaCoordM[0].Xm) * M2Pix);
					G.DrawLine(pen, num3, (short)unchecked(Mup + wSizeY), num3, Mup);
					sizeF = G.MeasureString(Conversions.ToString(num2), new System.Drawing.Font("Arial", 18f, FontStyle.Regular));
					G.DrawString(Conversions.ToString(num2), new System.Drawing.Font("Arial", 18f, FontStyle.Regular), new SolidBrush(Color.Black), (float)num3 - sizeF.Width / 2f, (short)unchecked(wSizeY + Mup) + 2);
				}
				float xmin2 = (float)Public2.PRJ.SETT.HorAreaCoordM[0].Ym;
				float xmax2 = (float)Public2.PRJ.SETT.HorAreaCoordM[1].Ym;
				int N2 = num;
				Module1.RazmetkaOSI(xmin2, xmax2, 8, 13, ref N2, ref dX, ref X);
				num = (short)N2;
				N2 = num - 1;
				for (int j = 0; j <= N2; j++)
				{
					float num2 = X + (float)j * dX;
					int num3 = (int)Math.Round((double)(short)unchecked(Mup + wSizeY) - ((double)num2 - Public2.PRJ.SETT.HorAreaCoordM[0].Ym) * M2Pix);
					G.DrawLine(pen, Mleft, num3, (short)unchecked(Mleft + wSizeX), num3);
					sizeF = G.MeasureString(Conversions.ToString(num2), new System.Drawing.Font("Arial", 18f, FontStyle.Regular));
					G.DrawString(Conversions.ToString(num2), new System.Drawing.Font("Arial", 18f, FontStyle.Regular), new SolidBrush(Color.Black), (float)Mleft - sizeF.Width, (float)num3 - sizeF.Height / 2f);
				}
			}
		}
	}

	public static void wDrawHorizontalPlanes(Graphics G)
	{
		if (Information.IsNothing(Public2.PRJ.HP))
		{
			return;
		}
		checked
		{
			if (Public2.PRJ.SETT.FlagLastRezManyLimits)
			{
				int num = 0;
				do
				{
					if (Public2.PRJ.SETT.HorizCutLimits[num] && !Information.IsNothing(Public2.PRJ.HP_PLNwm[num]))
					{
						Region region = new Region(new GraphicsPath(FillMode.Winding));
						int upperBound = Public2.PRJ.HP_PLNwm[num].GetUpperBound(0);
						for (int i = 0; i <= upperBound; i++)
						{
							Point[] array = new Point[Public2.PRJ.HP_PLNwm[num][i].GetUpperBound(0) + 1];
							int upperBound2 = Public2.PRJ.HP_PLNwm[num][i].GetUpperBound(0);
							for (int j = 0; j <= upperBound2; j++)
							{
								array[j].X = (int)Math.Round((Public2.PRJ.HP_PLNwm[num][i][j].Xwm - Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm) * WM2Pix);
								array[j].Y = (int)Math.Round((double)wSizeY - (Public2.PRJ.HP_PLNwm[num][i][j].Ywm - Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm) * WM2Pix);
							}
							if (Public2.PRJ.SETT.TransparFillZO > 0)
							{
								GraphicsPath graphicsPath = new GraphicsPath(FillMode.Winding);
								graphicsPath.AddLines(array);
								region.Xor(graphicsPath);
							}
							if (Public2.PRJ.SETT.ShowConturZO)
							{
								Pen pen = new Pen(Public2.PRJ.SETT.LimitColor[num], unchecked((int)Public2.PRJ.SETT.LineWidthZO));
								pen.LineJoin = LineJoin.Bevel;
								G.DrawLines(pen, array);
							}
						}
						if (Public2.PRJ.SETT.TransparFillZO > 0)
						{
							G.FillRegion(new SolidBrush(Color.FromArgb((int)Math.Round((double)unchecked((int)Public2.PRJ.SETT.TransparFillZO) * 25.5), Public2.PRJ.SETT.LimitColor[num])), region);
						}
					}
					num++;
				}
				while (num <= 3);
				return;
			}
			for (int k = Public2.PRJ.HP.GetUpperBound(0); k >= 0; k += -1)
			{
				if (!Public2.PRJ.HP[k].Activ || Information.IsNothing(Public2.PRJ.HP[k].PLNwm))
				{
					continue;
				}
				Region region2 = new Region(new GraphicsPath(FillMode.Winding));
				int upperBound3 = Public2.PRJ.HP[k].PLNwm.GetUpperBound(0);
				for (int l = 0; l <= upperBound3; l++)
				{
					Point[] array2 = new Point[Public2.PRJ.HP[k].PLNwm[l].GetUpperBound(0) + 1];
					int upperBound4 = Public2.PRJ.HP[k].PLNwm[l].GetUpperBound(0);
					for (int m = 0; m <= upperBound4; m++)
					{
						array2[m].X = (int)Math.Round((Public2.PRJ.HP[k].PLNwm[l][m].Xwm - Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm) * WM2Pix);
						array2[m].Y = (int)Math.Round((double)wSizeY - (Public2.PRJ.HP[k].PLNwm[l][m].Ywm - Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm) * WM2Pix);
					}
					if (Public2.PRJ.SETT.TransparFillZO > 0)
					{
						GraphicsPath graphicsPath2 = new GraphicsPath(FillMode.Winding);
						graphicsPath2.AddLines(array2);
						region2.Xor(graphicsPath2);
					}
					unchecked
					{
						if (Public2.PRJ.SETT.ShowConturZO)
						{
							if (Public2.PRJ.HP[k].LineType == 0)
							{
								G.DrawLines(new Pen(Public2.PRJ.HP[k].Color, (int)Public2.PRJ.SETT.LineWidthZO), array2);
								continue;
							}
							Pen pen2 = Public1.PenDash[Public2.PRJ.HP[k].LineType];
							pen2.Width = (int)Public2.PRJ.SETT.LineWidthZO;
							pen2.Color = Public2.PRJ.HP[k].Color;
							pen2.DashStyle = DashStyle.Custom;
							G.DrawLines(pen2, array2);
						}
					}
				}
				if (Public2.PRJ.SETT.TransparFillZO > 0)
				{
					G.FillRegion(new SolidBrush(Color.FromArgb((int)Math.Round((double)unchecked((int)Public2.PRJ.SETT.TransparFillZO) * 25.5), Public2.PRJ.HP[k].Color)), region2);
				}
			}
		}
	}

	public static void wDrawAntennas(Graphics G)
	{
		if (!Public2.PRJ.SETT.ShowAntennas || Information.IsNothing(Public2.PRJ.TX))
		{
			return;
		}
		checked
		{
			bool[] array = new bool[Public2.PRJ.TX.GetUpperBound(0) + 1];
			int upperBound = Public2.PRJ.TX.GetUpperBound(0);
			string text = default;
			for (int i = 0; i <= upperBound; i++)
			{
				if (!Public2.PRJ.TX[i].Active)
				{
					continue;
				}
				float num = Public2.PRJ.TX[i].Az;
				if (num < 0f)
				{
					num = 0f;
				}
				int num2 = (int)Math.Round((Public2.PRJ.TX[i].CoordWM.Xwm - Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm) * WM2Pix);
				int num3 = (int)Math.Round((double)wSizeY - (Public2.PRJ.TX[i].CoordWM.Ywm - Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm) * WM2Pix);
				if (Public2.PRJ.SETT.ShowAntennaAzimuth)
				{
					wArrowAz(num2, num3, num, G);
				}
				if (Public2.PRJ.SETT.ShowLongAzimuth)
				{
					wLongLineAz(num2, num3, num, G);
				}
				G.FillEllipse(Brushes.White, num2 - 9, num3 - 9, 18, 18);
				G.DrawEllipse(new Pen(Color.Black, 3f), num2 - 6, num3 - 6, 12, 12);
				if (!Public2.PRJ.SETT.ShowAntennaLabels)
				{
					continue;
				}
				bool flag = false;
				if (!array[i])
				{
					text = Conversions.ToString(i + 1);
					array[i] = true;
					flag = true;
				}
				int num4 = i + 1;
				int upperBound2 = Public2.PRJ.TX.GetUpperBound(0);
				for (int j = num4; j <= upperBound2; j++)
				{
					if ((Public2.PRJ.TX[i].CoordWM.Xwm == Public2.PRJ.TX[j].CoordWM.Xwm) & (Public2.PRJ.TX[i].CoordWM.Ywm == Public2.PRJ.TX[j].CoordWM.Ywm) & !array[j] & Public2.PRJ.TX[j].Active)
					{
						text = text + "," + Conversions.ToString(j + 1);
						array[j] = true;
					}
				}
				if (flag)
				{
					G.DrawString(text, new System.Drawing.Font("Arial", 16f, FontStyle.Regular), Brushes.White, num2 + 5 - 3, num3 - 22);
					G.DrawString(text, new System.Drawing.Font("Arial", 16f, FontStyle.Regular), Brushes.White, num2 + 5 + 3, num3 - 22);
					G.DrawString(text, new System.Drawing.Font("Arial", 16f, FontStyle.Regular), Brushes.White, num2 + 5, num3 - 22 - 3);
					G.DrawString(text, new System.Drawing.Font("Arial", 16f, FontStyle.Regular), Brushes.White, num2 + 5, num3 - 22 + 3);
					G.DrawString(text, new System.Drawing.Font("Arial", 16f, FontStyle.Regular), BrushBlack, num2 + 5, num3 - 22);
				}
			}
		}
	}

	public static void wArrowAz(int X1, int Y1, float Azimuth, Graphics G)
	{
		int num = 300;
		checked
		{
			int num2 = (int)Math.Round((double)X1 + (double)num * Math.Sin((double)Azimuth / (180.0 / Math.PI)));
			int num3 = (int)Math.Round((double)Y1 - (double)num * Math.Cos((double)Azimuth / (180.0 / Math.PI)));
			int num4 = (int)Math.Round((double)(X1 + num2) / 2.0);
			int num5 = (int)Math.Round((double)(Y1 + num3) / 2.0);
			float angle = ((!((Azimuth >= 0f) & (Azimuth < 180f))) ? (Azimuth + 90f) : (Azimuth - 90f));
			G.DrawLine(new Pen(Color.Black, 2f), X1, Y1, num2, num3);
			int x = (int)Math.Round(12.0 * Math.Sin((double)(Azimuth + 160f) / (180.0 / Math.PI)) + (double)num2);
			int y = (int)Math.Round(-12.0 * Math.Cos((double)(Azimuth + 160f) / (180.0 / Math.PI)) + (double)num3);
			G.DrawLine(new Pen(Color.Black, 2f), num2, num3, x, y);
			x = (int)Math.Round(12.0 * Math.Sin((double)(Azimuth + 200f) / (180.0 / Math.PI)) + (double)num2);
			y = (int)Math.Round(-12.0 * Math.Cos((double)(Azimuth + 200f) / (180.0 / Math.PI)) + (double)num3);
			G.DrawLine(new Pen(Color.Black, 2f), num2, num3, x, y);
			G.TranslateTransform(num4, num5);
			G.RotateTransform(angle);
			SizeF sizeF = default;
			sizeF = G.MeasureString(Conversions.ToString(Azimuth) + "°", new System.Drawing.Font("Arial", 16f, FontStyle.Regular));
			G.DrawString(Conversions.ToString(Azimuth) + "°", new System.Drawing.Font("Arial", 16f, FontStyle.Regular), new SolidBrush(Color.White), (0f - sizeF.Width) / 2f - 3f, (0f - sizeF.Height) / 2f);
			G.DrawString(Conversions.ToString(Azimuth) + "°", new System.Drawing.Font("Arial", 16f, FontStyle.Regular), new SolidBrush(Color.White), (0f - sizeF.Width) / 2f + 3f, (0f - sizeF.Height) / 2f);
			G.DrawString(Conversions.ToString(Azimuth) + "°", new System.Drawing.Font("Arial", 16f, FontStyle.Regular), new SolidBrush(Color.White), (0f - sizeF.Width) / 2f, (0f - sizeF.Height) / 2f - 3f);
			G.DrawString(Conversions.ToString(Azimuth) + "°", new System.Drawing.Font("Arial", 16f, FontStyle.Regular), new SolidBrush(Color.White), (0f - sizeF.Width) / 2f, (0f - sizeF.Height) / 2f + 3f);
			G.DrawString(Conversions.ToString(Azimuth) + "°", new System.Drawing.Font("Arial", 16f, FontStyle.Regular), new SolidBrush(Color.Black), (0f - sizeF.Width) / 2f, (0f - sizeF.Height) / 2f);
			G.ResetTransform();
		}
	}

	public static void wLongLineAz(int X1, int Y1, float Azimuth, Graphics G)
	{
		Pen pen = new Pen(Color.Black, 1f);
		pen.DashStyle = DashStyle.Custom;
		pen.DashPattern = new float[2] { 6f, 3f };
		int num = 2000;
		checked
		{
			int x = (int)Math.Round((double)X1 + (double)num * Math.Sin((double)Azimuth / (180.0 / Math.PI)));
			int y = (int)Math.Round((double)Y1 - (double)num * Math.Cos((double)Azimuth / (180.0 / Math.PI)));
			G.DrawLine(pen, X1, Y1, x, y);
		}
	}

	public static void wDrawControlPoints(Graphics G)
	{
		if (!Public2.PRJ.SETT.ShowControlPoints || Information.IsNothing(Public2.PRJ.CPG))
		{
			return;
		}
		int upperBound = Public2.PRJ.CPG.GetUpperBound(0);
		checked
		{
			for (int i = 0; i <= upperBound; i++)
			{
				if (!Public2.PRJ.CPG[i].Active || Information.IsNothing(Public2.PRJ.CPG[i].CP))
				{
					continue;
				}
				int upperBound2 = Public2.PRJ.CPG[i].CP.GetUpperBound(0);
				for (int j = 0; j <= upperBound2; j++)
				{
					int num = (int)Math.Round((Public2.PRJ.CPG[i].CP[j].CoordWM.Xwm - Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm) * WM2Pix);
					int num2 = (int)Math.Round((double)wSizeY - (Public2.PRJ.CPG[i].CP[j].CoordWM.Ywm - Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm) * WM2Pix);
					G.FillEllipse(Brushes.White, num - 9, num2 - 9, 18, 18);
					G.FillEllipse(Brushes.Black, num - 6, num2 - 6, 12, 12);
					string text = Public2.PRJ.SETT.ControlPointPrefix;
					if (Public2.PRJ.SETT.ShowControlPointGroups)
					{
						text = text + Conversions.ToString(i + 1) + ".";
					}
					text += Conversions.ToString(j + 1);
					if (Public2.PRJ.SETT.ShowControlPointHeight)
					{
						text += "(";
						int upperBound3 = Public2.PRJ.CPG[i].CP[j].H.GetUpperBound(0);
						for (int k = 0; k <= upperBound3; k++)
						{
							text += Conversions.ToString(Public2.PRJ.CPG[i].CP[j].H[k].H);
							if (k < Public2.PRJ.CPG[i].CP[j].H.GetUpperBound(0))
							{
								text += ";";
							}
						}
						text += "m)";
					}
					G.DrawString(text, new System.Drawing.Font("Arial", 20f, FontStyle.Regular), Brushes.White, num + 8 - 3, num2 - 27);
					G.DrawString(text, new System.Drawing.Font("Arial", 20f, FontStyle.Regular), Brushes.White, num + 8 + 3, num2 - 27);
					G.DrawString(text, new System.Drawing.Font("Arial", 20f, FontStyle.Regular), Brushes.White, num + 8, num2 - 27 - 3);
					G.DrawString(text, new System.Drawing.Font("Arial", 20f, FontStyle.Regular), Brushes.White, num + 8, num2 - 27 + 3);
					G.DrawString(text, new System.Drawing.Font("Arial", 20f, FontStyle.Regular), Brushes.Black, num + 8, num2 - 27);
				}
			}
		}
	}

	public static void wDrawBuildings(Graphics G)
	{
		if (!Public2.PRJ.SETT.ShowAllBuildings || Information.IsNothing(Public2.PRJ.BLD))
		{
			return;
		}
		int upperBound = Public2.PRJ.BLD.GetUpperBound(0);
		checked
		{
			for (int i = 0; i <= upperBound; i++)
			{
				if (!Public2.PRJ.BLD[i].Show)
				{
					continue;
				}
				Point[] array = new Point[Public2.PRJ.BLD[i].CoordWM.GetUpperBound(0) + 1];
				int upperBound2 = Public2.PRJ.BLD[i].CoordWM.GetUpperBound(0);
				for (int j = 0; j <= upperBound2; j++)
				{
					array[j].X = (int)Math.Round((Public2.PRJ.BLD[i].CoordWM[j].Xwm - Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm) * WM2Pix);
					array[j].Y = (int)Math.Round((double)wSizeY - (Public2.PRJ.BLD[i].CoordWM[j].Ywm - Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm) * WM2Pix);
				}
				G.DrawLines(new Pen(Color.Black, unchecked((int)Public2.PRJ.SETT.BuildingLineWidth)), array);
				if (Public2.PRJ.SETT.ShowBuildingText | Public2.PRJ.SETT.ShowBuildingHeight | Public2.PRJ.SETT.ShowBuildingFoundation)
				{
					string text = "";
					if (Public2.PRJ.SETT.ShowBuildingText)
					{
						text = text + Public2.PRJ.BLD[i].Text + "\r\n";
					}
					if (Public2.PRJ.SETT.ShowBuildingHeight)
					{
						text = text + Conversions.ToString(Public2.PRJ.BLD[i].H) + "m\r\n";
					}
					if (Public2.PRJ.SETT.ShowBuildingFoundation)
					{
						text = text + Conversions.ToString(Public2.PRJ.BLD[i].Fundation) + "m\r\n";
					}
					text = text.Substring(0, text.Length - 1);
					Public2.CoordWebMerc coordWebMerc = Module1.CoordCenterSign1(Public2.PRJ.BLD[i].CoordWM);
					int num = (int)Math.Round((coordWebMerc.Xwm - Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm) * WM2Pix);
					int num2 = (int)Math.Round((double)wSizeY - (coordWebMerc.Ywm - Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm) * WM2Pix);
					stringSize = G.MeasureString(text, new System.Drawing.Font("Arial", Public2.PRJ.SETT.BuildingTextSize * 2, FontStyle.Regular));
					G.DrawString(text, new System.Drawing.Font("Arial", Public2.PRJ.SETT.BuildingTextSize * 2, FontStyle.Regular), Brushes.Black, (float)num - stringSize.Width / 2f, (float)num2 - stringSize.Height / 2f + 3f);
				}
			}
		}
	}

	public static void wDrawLines(Graphics G)
	{
		new Pen(Brushes.Black, 1f);
		new Pen(Color.Black, 1f)
		{
			DashStyle = DashStyle.Custom,
			DashPattern = new float[2] { 8f, 4f }
		};
		checked
		{
			if (Public2.PRJ.SETT.ShowRoads && !Information.IsNothing(Public2.PRJ.ROAD))
			{
				int upperBound = Public2.PRJ.ROAD.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					Point[] array = new Point[Public2.PRJ.ROAD[i].CoordWM.GetUpperBound(0) + 1];
					int upperBound2 = Public2.PRJ.ROAD[i].CoordWM.GetUpperBound(0);
					for (int j = 0; j <= upperBound2; j++)
					{
						array[j].X = (int)Math.Round((Public2.PRJ.ROAD[i].CoordWM[j].Xwm - Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm) * WM2Pix);
						array[j].Y = (int)Math.Round((double)wSizeY - (Public2.PRJ.ROAD[i].CoordWM[j].Ywm - Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm) * WM2Pix);
					}
					G.DrawLines(new Pen(Brushes.Black, (float)((double)unchecked((int)Public2.PRJ.ROAD[i].WidthM) * M2Pix)), array);
				}
				int upperBound3 = Public2.PRJ.ROAD.GetUpperBound(0);
				for (int k = 0; k <= upperBound3; k++)
				{
					Point[] array2 = new Point[Public2.PRJ.ROAD[k].CoordWM.GetUpperBound(0) + 1];
					int upperBound4 = Public2.PRJ.ROAD[k].CoordWM.GetUpperBound(0);
					for (int l = 0; l <= upperBound4; l++)
					{
						array2[l].X = (int)Math.Round((Public2.PRJ.ROAD[k].CoordWM[l].Xwm - Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm) * WM2Pix);
						array2[l].Y = (int)Math.Round((double)wSizeY - (Public2.PRJ.ROAD[k].CoordWM[l].Ywm - Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm) * WM2Pix);
					}
					G.DrawLines(new Pen(Brushes.White, (float)unchecked((double)(int)Public2.PRJ.ROAD[k].WidthM * M2Pix - (double)(int)Public2.PRJ.ROAD[k].WidthBorder)), array2);
				}
			}
			if (Public2.PRJ.SETT.ShowFences && !Information.IsNothing(Public2.PRJ.FENCE))
			{
				int upperBound5 = Public2.PRJ.FENCE.GetUpperBound(0);
				for (int m = 0; m <= upperBound5; m++)
				{
					Point[] array3 = new Point[Public2.PRJ.FENCE[m].CoordWM.GetUpperBound(0) + 1];
					int upperBound6 = Public2.PRJ.FENCE[m].CoordWM.GetUpperBound(0);
					for (int n = 0; n <= upperBound6; n++)
					{
						array3[n].X = (int)Math.Round((Public2.PRJ.FENCE[m].CoordWM[n].Xwm - Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm) * WM2Pix);
						array3[n].Y = (int)Math.Round((double)wSizeY - (Public2.PRJ.FENCE[m].CoordWM[n].Ywm - Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm) * WM2Pix);
					}
					Public1.PenDash[Public2.PRJ.FENCE[m].Type].Width = unchecked((int)Public2.PRJ.FENCE[m].Width);
					if (!Information.IsNothing(array3))
					{
						G.DrawLines(Public1.PenDash[Public2.PRJ.FENCE[m].Type], array3);
					}
				}
			}
			if (!Public2.PRJ.SETT.ShowText || Information.IsNothing(Public2.PRJ.TXT))
			{
				return;
			}
			int upperBound7 = Public2.PRJ.TXT.GetUpperBound(0);
			for (int num = 0; num <= upperBound7; num++)
			{
				if (Public2.PRJ.TXT[num].Text.Contains("Tower"))
				{
					continue;
				}
				int num2 = (int)Math.Round((Public2.PRJ.TXT[num].CoordWM.Xwm - Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm) * WM2Pix);
				int num3 = (int)Math.Round((double)wSizeY - (Public2.PRJ.TXT[num].CoordWM.Ywm - Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm) * WM2Pix);
				G.TranslateTransform(num2, num3);
				G.RotateTransform(Public2.PRJ.TXT[num].Angle);
				unchecked
				{
					stringSize = G.MeasureString(Public2.PRJ.TXT[num].Text, new System.Drawing.Font(Public2.PRJ.TXT[num].FontName, (int)Public2.PRJ.TXT[num].Size, FontStyle.Regular));
					G.DrawString(Public2.PRJ.TXT[num].Text, new System.Drawing.Font(Public2.PRJ.TXT[num].FontName, (int)Public2.PRJ.TXT[num].Size, FontStyle.Regular), BrushBlack, 0f, 0f);
					if (!Information.IsNothing(Public2.PRJ.TXT[num].Icon))
					{
						if (Public2.PRJ.TXT[num].Point)
						{
							G.DrawImage(Public2.PRJ.TXT[num].Icon, -16, -16, 32, 32);
						}
					}
					else if (Public2.PRJ.TXT[num].Point)
					{
						G.FillEllipse(Brushes.Black, -4, -4, 8, 8);
					}
					G.ResetTransform();
				}
			}
		}
	}

	public static void wDrawNorth(Graphics G)
	{
		G.DrawLine(new Pen(Color.White, 10f), 30, 5, 30, 290);
		G.DrawLine(new Pen(Color.White, 10f), 30, 5, 55, 65);
		G.DrawLine(new Pen(Color.White, 10f), 30, 5, 5, 65);
		G.DrawLine(new Pen(Color.Black, 4f), 30, 5, 30, 285);
		G.DrawLine(new Pen(Color.Black, 4f), 30, 5, 55, 65);
		G.DrawLine(new Pen(Color.Black, 4f), 30, 5, 5, 65);
		G.DrawString("N", new System.Drawing.Font("Arial", 40f, FontStyle.Regular), Brushes.White, 43f, 0f);
		G.DrawString("N", new System.Drawing.Font("Arial", 40f, FontStyle.Regular), Brushes.White, 37f, 0f);
		G.DrawString("N", new System.Drawing.Font("Arial", 40f, FontStyle.Regular), Brushes.White, 40f, 3f);
		G.DrawString("N", new System.Drawing.Font("Arial", 40f, FontStyle.Regular), Brushes.White, 40f, -3f);
		G.DrawString("N", new System.Drawing.Font("Arial", 40f, FontStyle.Regular), Brushes.Black, 40f, 0f);
	}

	public static void wDrawMesureStrip(Graphics G)
	{
		float xmin = (float)Public2.PRJ.SETT.HorAreaCoordM[0].Xm;
		float xmax = (float)Public2.PRJ.SETT.HorAreaCoordM[1].Xm;
		short num = default;
		int N = num;
		float dX = default;
		float X = default;
		Module1.RazmetkaOSI(xmin, xmax, 5, 6, ref N, ref dX, ref X);
		checked
		{
			num = (short)N;
			short num2 = (short)Math.Round((double)dX * M2Pix);
			G.DrawLine(new Pen(Color.White, 10f), 20, wSizeY - 20, 20 + num2, wSizeY - 20);
			G.DrawLine(new Pen(Color.White, 10f), 20, wSizeY - 20, 20, wSizeY - 40);
			G.DrawLine(new Pen(Color.White, 10f), 20 + num2, wSizeY - 20, 20 + num2, wSizeY - 40);
			G.DrawLine(new Pen(Color.Black, 4f), 20, wSizeY - 20, 20 + num2, wSizeY - 20);
			G.DrawLine(new Pen(Color.Black, 4f), 20, wSizeY - 20, 20, wSizeY - 40);
			G.DrawLine(new Pen(Color.Black, 4f), 20 + num2, wSizeY - 20, 20 + num2, wSizeY - 40);
			stringSize = G.MeasureString(Conversions.ToString(dX) + " m", new System.Drawing.Font("Arial", 20f, FontStyle.Regular), 0);
			G.DrawString(Conversions.ToString(dX) + " m", new System.Drawing.Font("Arial", 20f, FontStyle.Regular), Brushes.White, (float)(20.0 + (double)num2 / 2.0 - (double)(stringSize.Width / 2f) - 3.0), wSizeY - 60);
			G.DrawString(Conversions.ToString(dX) + " m", new System.Drawing.Font("Arial", 20f, FontStyle.Regular), Brushes.White, (float)(20.0 + (double)num2 / 2.0 - (double)(stringSize.Width / 2f) + 3.0), wSizeY - 60);
			G.DrawString(Conversions.ToString(dX) + " m", new System.Drawing.Font("Arial", 20f, FontStyle.Regular), Brushes.White, (float)(20.0 + (double)num2 / 2.0 - (double)(stringSize.Width / 2f)), wSizeY - 60 - 3);
			G.DrawString(Conversions.ToString(dX) + " m", new System.Drawing.Font("Arial", 20f, FontStyle.Regular), Brushes.White, (float)(20.0 + (double)num2 / 2.0 - (double)(stringSize.Width / 2f)), wSizeY - 60 + 3);
			G.DrawString(Conversions.ToString(dX) + " m", new System.Drawing.Font("Arial", 20f, FontStyle.Regular), Brushes.Black, (float)(20.0 + (double)num2 / 2.0 - (double)(stringSize.Width / 2f)), wSizeY - 60);
		}
	}

	public static void wDrawElevation(Graphics G)
	{
		if (!Public2.PRJ.SETT.ShowRelief)
		{
			return;
		}
		checked
		{
			if (!Information.IsNothing(Public2.PRJ.ElLN))
			{
				int upperBound = Public2.PRJ.ElLN.GetUpperBound(0);
				for (int i = 0; i <= upperBound; i++)
				{
					Point[] array = new Point[Public2.PRJ.ElLN[i].CoordWM.GetUpperBound(0) + 1];
					int upperBound2 = Public2.PRJ.ElLN[i].CoordWM.GetUpperBound(0);
					for (int j = 0; j <= upperBound2; j++)
					{
						array[j].X = (int)Math.Round((Public2.PRJ.ElLN[i].CoordWM[j].Xwm - Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm) * WM2Pix);
						array[j].Y = (int)Math.Round((double)wSizeY - (Public2.PRJ.ElLN[i].CoordWM[j].Ywm - Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm) * WM2Pix);
						G.DrawString(Conversions.ToString(Public2.PRJ.ElLN[i].H), new System.Drawing.Font("Arial", 12f, FontStyle.Regular), Brushes.Brown, array[j].X + 0, array[j].Y - 17);
					}
					if (!Information.IsNothing(array))
					{
						G.DrawLines(PenBrown1, array);
					}
				}
			}
			if (!Information.IsNothing(Public2.PRJ.ElPT))
			{
				int upperBound3 = Public2.PRJ.ElPT.GetUpperBound(0);
				for (int k = 0; k <= upperBound3; k++)
				{
					float num = (float)((Public2.PRJ.ElPT[k].CoordWM.Xwm - Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm) * WM2Pix);
					float num2 = (float)((double)wSizeY - (Public2.PRJ.ElPT[k].CoordWM.Ywm - Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm) * WM2Pix);
					G.FillEllipse(Brushes.Black, num - 3f, num2 - 3f, 6f, 6f);
					G.DrawString(Conversions.ToString(Public2.PRJ.ElPT[k].H), new System.Drawing.Font("Arial", 12f, FontStyle.Regular), Brushes.Brown, num + 2f, num2 - 17f);
				}
			}
		}
	}

	public static void wDrawConcentricCircles(Graphics G)
	{
		checked
		{
			if (Public2.PRJ.SETT.ShowCircles)
			{
				Pen pen = new Pen(Color.Black, 2f);
				pen.DashStyle = DashStyle.Custom;
				pen.DashPattern = new float[2] { 8f, 8f };
				int num = (int)Math.Round((Public2.PRJ.SETT.OriginCoordWM.Xwm - Public2.PRJ.SETT.HorAreaCoordWM[0].Xwm) * WM2Pix);
				int num2 = (int)Math.Round((double)wSizeY - (Public2.PRJ.SETT.OriginCoordWM.Ywm - Public2.PRJ.SETT.HorAreaCoordWM[0].Ywm) * WM2Pix);
				float circleStep = Public2.PRJ.SETT.CircleStep;
				float circleStep2 = Public2.PRJ.SETT.CircleStep;
				bool flag = circleStep2 >= 0f;
				for (float num3 = circleStep; flag ? (num3 <= 500f) : (num3 >= 500f); num3 += circleStep2)
				{
					int num4 = (int)Math.Round((double)num3 * M2Pix);
					int num5 = 2 * num4;
					G.DrawEllipse(pen, num - num4 + 5, num2 - num4 - 22, num5, num5);
				}
			}
		}
	}

	public static void WordAddBeetle(ref Microsoft.Office.Interop.Word.Application oWord)
	{
		Find find = oWord.Selection.Find;
		find.Forward = true;
		find.ClearFormatting();
		find.MatchWholeWord = true;
		find.MatchCase = false;
		find.Wrap = WdFindWrap.wdFindContinue;
		object FindText = "{CardinalPointsChart}";
		object MatchCase = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchWholeWord = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchWildcards = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchSoundsLike = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchAllWordForms = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Forward = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Wrap = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Format = RuntimeHelpers.GetObjectValue(Missing.Value);
		object ReplaceWith = RuntimeHelpers.GetObjectValue(Missing.Value);
		object Replace = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchKashida = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
		object MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
		find.Execute(ref FindText, ref MatchCase, ref MatchWholeWord, ref MatchWildcards, ref MatchSoundsLike, ref MatchAllWordForms, ref Forward, ref Wrap, ref Format, ref ReplaceWith, ref Replace, ref MatchKashida, ref MatchDiacritics, ref MatchAlefHamza, ref MatchControl);
		_ = null;
		if (!oWord.Selection.Find.Found)
		{
			return;
		}
		List<AzSec> list = new List<AzSec>();
		if (Public2.PRJ.TX != null)
		{
			int upperBound = Public2.PRJ.TX.GetUpperBound(0);
			for (int i = 0; i <= upperBound; i = checked(i + 1))
			{
				if (!Public2.PRJ.TX[i].Active || Operators.CompareString(Public2.PRJ.TX[i].Sector, "", TextCompare: false) == 0)
				{
					continue;
				}
				bool flag = false;
				foreach (AzSec item in list)
				{
					if ((Operators.CompareString(Public2.PRJ.TX[i].Sector, "", TextCompare: false) != 0) & (Operators.CompareString(item.Sector, Public2.PRJ.TX[i].Sector, TextCompare: false) == 0))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					list.Add(new AzSec(Public2.PRJ.TX[i].Az, Module1.Reject0D0A09(Public2.PRJ.TX[i].Sector)));
				}
			}
		}
		list.Sort([SpecialName] (AzSec a1, AzSec b1) => a1.Azimuth.CompareTo(b1.Azimuth));
		oWord.Selection.Range.Select();
		DrawBeetle(list).Save(Module1.FolderTMP + "\\North.png", ImageFormat.Png);
		InlineShapes inlineShapes = oWord.Selection.InlineShapes;
		string fileName = Module1.FolderTMP + "\\North.png";
		MatchControl = RuntimeHelpers.GetObjectValue(Missing.Value);
		MatchAlefHamza = RuntimeHelpers.GetObjectValue(Missing.Value);
		MatchDiacritics = RuntimeHelpers.GetObjectValue(Missing.Value);
		Shape shape = inlineShapes.AddPicture(fileName, ref MatchControl, ref MatchAlefHamza, ref MatchDiacritics).ConvertToShape();
		shape.LockAspectRatio = MsoTriState.msoTrue;
		shape.Width = oWord.MillimetersToPoints(100f);
		shape.RelativeHorizontalPosition = WdRelativeHorizontalPosition.wdRelativeHorizontalPositionMargin;
		shape.RelativeVerticalPosition = WdRelativeVerticalPosition.wdRelativeVerticalPositionMargin;
		shape.Left = -999995f;
		shape.Top = -999995f;
		shape.ZOrder(MsoZOrderCmd.msoBringInFrontOfText);
	}

	public static Bitmap DrawBeetle(List<AzSec> AzimuthList)
	{
		short num = 600;
		short num2 = 120;
		short width = num;
		checked
		{
			short height = (short)unchecked(num + num2);
			short num3 = (short)Math.Round((double)num / 2.0);
			short num4 = (short)Math.Round((double)num / 2.0 + (double)num2);
			Bitmap bitmap = new Bitmap(width, height);
			short num5 = (short)Math.Round(0.25 * (double)num / 2.0);
			short num6 = (short)Math.Round(0.45 * (double)num / 2.0);
			short num7 = (short)Math.Round((double)(short)unchecked(num5 + num6) / 2.0);
			short num8 = (short)Math.Round(0.95 * (double)num / 2.0);
			Graphics graphics = Graphics.FromImage(bitmap);
			graphics.Clear(Color.White);
			graphics.DrawEllipse(new Pen(Color.Black, 2f), (short)unchecked(num3 - num5), (short)unchecked(num4 - num5), 2 * num5, 2 * num5);
			graphics.DrawEllipse(new Pen(Color.Black, 2f), (short)unchecked(num3 - num6), (short)unchecked(num4 - num6), 2 * num6, 2 * num6);
			int num9 = 0;
			do
			{
				short x = (short)Math.Round((double)num3 + (double)num5 * Math.Cos((double)num9 * Constants.PI_180));
				short y = (short)Math.Round((double)num4 + (double)num5 * Math.Sin((double)num9 * Constants.PI_180));
				short x2 = (short)Math.Round((double)num3 + (double)num6 * Math.Cos((double)num9 * Constants.PI_180));
				short y2 = (short)Math.Round((double)num4 + (double)num6 * Math.Sin((double)num9 * Constants.PI_180));
				graphics.DrawLine(new Pen(Color.Black, 2f), x, y, x2, y2);
				num9 += 15;
			}
			while (num9 <= 359);
			int num10 = 0;
			do
			{
				short x3 = (short)Math.Round((double)num3 + (double)num7 * Math.Cos((double)num10 * Constants.PI_180));
				short y3 = (short)Math.Round((double)num4 + (double)num7 * Math.Sin((double)num10 * Constants.PI_180));
				short x4 = (short)Math.Round((double)num3 + (double)num6 * Math.Cos((double)num10 * Constants.PI_180));
				short y4 = (short)Math.Round((double)num4 + (double)num6 * Math.Sin((double)num10 * Constants.PI_180));
				graphics.DrawLine(new Pen(Color.Black, 2f), x3, y3, x4, y4);
				num10 += 5;
			}
			while (num10 <= 359);
			int num11 = 0;
			SizeF sizeF;
			do
			{
				short num12 = (short)Math.Round((double)num3 + 0.8 * (double)num5 * Math.Sin((double)num11 * Constants.PI_180));
				short num13 = (short)Math.Round((double)num4 - 0.8 * (double)num5 * Math.Cos((double)num11 * Constants.PI_180));
				short x5 = (short)Math.Round((double)num3 + (double)num6 * Math.Cos((double)num11 * Constants.PI_180));
				short y5 = (short)Math.Round((double)num4 + (double)num6 * Math.Sin((double)num11 * Constants.PI_180));
				short x6 = (short)Math.Round((double)num3 + 1.18 * (double)num6 * Math.Cos((double)num11 * Constants.PI_180));
				short y6 = (short)Math.Round((double)num4 + 1.18 * (double)num6 * Math.Sin((double)num11 * Constants.PI_180));
				graphics.DrawLine(new Pen(Color.Black, 2f), x5, y5, x6, y6);
				if (num11 == 0)
				{
					num12 += 7;
				}
				graphics.TranslateTransform(num12, num13);
				graphics.RotateTransform(num11);
				sizeF = graphics.MeasureString(Conversions.ToString(num11), new System.Drawing.Font("Arial", 12f, FontStyle.Regular));
				graphics.DrawString(Conversions.ToString(num11), new System.Drawing.Font("Arial", 12f, FontStyle.Regular), new SolidBrush(Color.Black), (0f - sizeF.Width) / 2f, (0f - sizeF.Height) / 2f);
				graphics.ResetTransform();
				num11 += 45;
			}
			while (num11 <= 359);
			graphics.DrawLine(new Pen(Color.Black, 2f), num3, num4, num3, 0);
			graphics.FillPolygon(new SolidBrush(Color.Black), new PointF[4]
			{
				new PointF(num3, 0f),
				new PointF(num3 + 12, 95f),
				new PointF(num3, 80f),
				new PointF(num3 - 12, 95f)
			});
			graphics.DrawLine(new Pen(Color.Black, 2f), num3 - 40, 80, num3 + 40, 80);
			graphics.DrawEllipse(new Pen(Color.Black, 2f), num3 - 30, 50, 60, 60);
			graphics.TranslateTransform(num3 - 18, (short)unchecked(num4 - num6) - 8);
			graphics.RotateTransform(270f);
			sizeF = graphics.MeasureString("NORTH", new System.Drawing.Font("Arial", 12f, FontStyle.Regular));
			graphics.DrawString("NORTH", new System.Drawing.Font("Arial", 12f, FontStyle.Regular), new SolidBrush(Color.Black), 0f, 0f);
			graphics.ResetTransform();
			graphics.TranslateTransform(num3 + 20, (short)unchecked(num4 + num6) + 10);
			graphics.RotateTransform(90f);
			sizeF = graphics.MeasureString("SOUTH", new System.Drawing.Font("Arial", 12f, FontStyle.Regular));
			graphics.DrawString("SOUTH", new System.Drawing.Font("Arial", 12f, FontStyle.Regular), new SolidBrush(Color.Black), 0f, 0f);
			graphics.ResetTransform();
			graphics.DrawString("WEST", new System.Drawing.Font("Arial", 12f, FontStyle.Regular), new SolidBrush(Color.Black), (short)unchecked(num3 - num6) - 55, num4 - 20);
			graphics.DrawString("EAST", new System.Drawing.Font("Arial", 12f, FontStyle.Regular), new SolidBrush(Color.Black), (short)unchecked(num3 + num6) + 10, num4 - 20);
			Pen pen = new Pen(Color.Black, 2f);
			pen.DashStyle = DashStyle.Custom;
			pen.DashPattern = new float[4] { 22f, 8f, 1f, 8f };
			foreach (AzSec Azimuth in AzimuthList)
			{
				short num14 = (short)Math.Round((double)num3 + (double)num8 * Math.Sin((double)Azimuth.Azimuth * Constants.PI_180));
				short num15 = (short)Math.Round((double)num4 - (double)num8 * Math.Cos((double)Azimuth.Azimuth * Constants.PI_180));
				graphics.DrawLine(pen, num3, num4, num14, num15);
				graphics.FillPolygon(new SolidBrush(Color.Black), new PointF[3]
				{
					new PointF((float)(12.0 * Math.Sin((double)(Azimuth.Azimuth + 160f) * Constants.PI_180) + (double)num14), (float)(-12.0 * Math.Cos((double)(Azimuth.Azimuth + 160f) * Constants.PI_180) + (double)num15)),
					new PointF(num14, num15),
					new PointF((float)(12.0 * Math.Sin((double)(Azimuth.Azimuth + 200f) * Constants.PI_180) + (double)num14), (float)(-12.0 * Math.Cos((double)(Azimuth.Azimuth + 200f) * Constants.PI_180) + (double)num15))
				});
				float angle = ((!((Azimuth.Azimuth >= 0f) & (Azimuth.Azimuth < 180f))) ? (Azimuth.Azimuth + 90f) : (Azimuth.Azimuth - 90f));
				short num16 = (short)Math.Round((double)num3 + 0.9 * (double)num8 * Math.Sin((double)Azimuth.Azimuth * Constants.PI_180));
				short num17 = (short)Math.Round((double)num4 - 0.9 * (double)num8 * Math.Cos((double)Azimuth.Azimuth * Constants.PI_180));
				graphics.TranslateTransform(num16, num17);
				graphics.RotateTransform(angle);
				sizeF = graphics.MeasureString(Conversions.ToString(Azimuth.Azimuth) + "°", new System.Drawing.Font("Arial", 12f, FontStyle.Regular));
				graphics.DrawString(Conversions.ToString(Azimuth.Azimuth) + "°", new System.Drawing.Font("Arial", 12f, FontStyle.Regular), new SolidBrush(Color.White), (0f - sizeF.Width) / 2f + 1f, 4f);
				graphics.DrawString(Conversions.ToString(Azimuth.Azimuth) + "°", new System.Drawing.Font("Arial", 12f, FontStyle.Regular), new SolidBrush(Color.White), (0f - sizeF.Width) / 2f - 1f, 2f);
				graphics.DrawString(Conversions.ToString(Azimuth.Azimuth) + "°", new System.Drawing.Font("Arial", 12f, FontStyle.Regular), new SolidBrush(Color.White), (0f - sizeF.Width) / 2f + 1f, 2f);
				graphics.DrawString(Conversions.ToString(Azimuth.Azimuth) + "°", new System.Drawing.Font("Arial", 12f, FontStyle.Regular), new SolidBrush(Color.White), (0f - sizeF.Width) / 2f - 1f, 4f);
				graphics.DrawString(Conversions.ToString(Azimuth.Azimuth) + "°", new System.Drawing.Font("Arial", 12f, FontStyle.Regular), new SolidBrush(Color.Black), (0f - sizeF.Width) / 2f, 3f);
				sizeF = graphics.MeasureString(Azimuth.Sector, new System.Drawing.Font("Arial", 12f, FontStyle.Regular));
				graphics.DrawString(Azimuth.Sector, new System.Drawing.Font("Arial", 12f, FontStyle.Regular), new SolidBrush(Color.White), (0f - sizeF.Width) / 2f + 1f, -17f);
				graphics.DrawString(Azimuth.Sector, new System.Drawing.Font("Arial", 12f, FontStyle.Regular), new SolidBrush(Color.White), (0f - sizeF.Width) / 2f - 1f, -19f);
				graphics.DrawString(Azimuth.Sector, new System.Drawing.Font("Arial", 12f, FontStyle.Regular), new SolidBrush(Color.White), (0f - sizeF.Width) / 2f + 1f, -19f);
				graphics.DrawString(Azimuth.Sector, new System.Drawing.Font("Arial", 12f, FontStyle.Regular), new SolidBrush(Color.White), (0f - sizeF.Width) / 2f - 1f, -17f);
				graphics.DrawString(Azimuth.Sector, new System.Drawing.Font("Arial", 12f, FontStyle.Regular), new SolidBrush(Color.Black), (0f - sizeF.Width) / 2f, -18f);
				graphics.ResetTransform();
			}
			bitmap.MakeTransparent(Color.White);
			return bitmap;
		}
	}

	public static void bArrowAz(float Azimuth, string Text, Graphics G)
	{
	}
}
