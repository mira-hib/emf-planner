using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[StandardModule]
internal sealed class Public1
{
	[Serializable]
	public struct COORD
	{
		public float X;

		public float Y;
	}

	[Serializable]
	public struct INFO
	{
		public string Owner;

		public string Name;

		public string Adress;

		public string Year;

		public string Repair;

		public string Regime;

		public string Coord;

		public string Info;

		public string[] REP;

		public bool AddPZ;

		public string RepFileName;

		public string[,] ALS;
	}

	[Serializable]
	public struct PRTO
	{
		public string Name;

		public bool Active;

		public float F;

		public string Ptv;

		public int N;

		public string AntType;

		public float G;

		public float D;

		public float H;

		public float Az;

		public float Ug;

		public float ElectUg;

		public float Loss;

		public string PDU;

		public float X;

		public float Y;

		public string Exist;

		public string Modul;

		public string Polar;

		public string Hroof;

		public string UgH;

		public string UgV;

		public string Owner;

		public string Opposite;

		public string MUK;

		public float[] DnH;

		public float[] DnV;

		public string Cable;

		public float LengthCable;

		public string Jumper;

		public string LengthJumper;

		public bool ChkLossSocket;

		public float LossSocketOne;

		public float ExtraLoss;

		public float dH;

		public Color Color;
	}

	[Serializable]
	public struct ControlPOINT
	{
		public float X;

		public float Y;

		public float H;

		public float Lb;

		public string Raspol;

		public string HeightEarth;

		public string[] Value;

		public string Esum;

		public string PPEsum;

		public string KB;

		public string T;
	}

	[Serializable]
	public struct RasterPlan
	{
		public Bitmap BTM;

		public Bitmap BTM2;

		public int X1pic;

		public int Y1pic;

		public int X2pic;

		public int Y2pic;

		public int Xpic;

		public int Ypic;

		public float Len;

		public float Koef;

		public double MashtPIC;

		public double PicDX;

		public double PicDY;

		public double MashtPIC2;

		public double PicDX2;

		public double PicDY2;

		public double MashtPIC3;

		public double PicDX3;

		public double PicDY3;

		public string NameTileServer;

		public int ZOOM;

		public string ShirCenter;

		public string DolgCenter;

		public string NameTileServerMap2;

		public bool DoNotUseBTM;

		public string ShirCenterMerc;

		public string DolgCenterMerc;

		public string CoordSystem;

		public double LatMainPoint;

		public double LonMainPoint;
	}

	[Serializable]
	public struct ParamCalc
	{
		public float hXmax;

		public float hXmin;

		public float hYmax;

		public float hYmin;

		public bool[] hActiv;

		public float[] hHeight;

		public Color[] hColor;

		public byte[] hLineType;

		public float hTabXmin;

		public float hTabXmax;

		public float hTabDeltaX;

		public float hTabYmin;

		public float hTabYmax;

		public float hTabDeltaY;

		public float hTabH;

		public bool[] vActiv;

		public float[] vAzimut;

		public float[] vXo;

		public float[] vYo;

		public float[] vHeight;

		public float[] vDepth;

		public float[] vLenght;

		public string[] vName;

		public float[] gHeight;

		public float[] gLenght;

		public float gStepLen;

		public float gLenMax;

		public float gStepAzim;

		public float gStepHeight;

		public float gHeightMax;

		public float[] tHGT;

		public bool SitPlan;

		public bool Number;

		public bool Name;

		public bool ANT;

		public bool Grid;

		public bool FILL;

		public bool Contur;

		public bool Point;

		public bool Sector;

		public bool Len;

		public bool Hgt;

		public bool HgtMax;

		public bool AcadStart;

		public bool ShowRelief;

		public bool NotShowAbsoluteHeight;

		public bool ShowBuildings;

		public bool CombineFlats;

		public bool TablUnderGraph;

		public byte LineWidth;

		public bool VertBuildingLabel;

		public bool DontShowNorth;

		public float LevelHeight;

		public bool UseEkranirVertFlat;

		public bool UseEkranirHorizFlat;

		public bool UseEkranirGraph;

		public bool UseEkranirKT;

		public float ZoomAntennaSign;

		public float ZoomAntennaLine;

		public float ZoomPointSign;

		public float ZoomPointLine;

		public float ZoomAntennaText;

		public float ZoomPointText;

		public float ZoomBuildingText;

		public float ZoomLabelText;

		public float AntStep;

		public float Koef;
	}

	[Serializable]
	public struct PrjTemplate
	{
		public bool AcadStart;

		public bool ShowRelief;

		public bool NotShowAbsoluteHeight;

		public bool ShowBuildings;

		public bool CombineFlats;

		public bool TablUnderGraph;

		public byte LineWidth;

		public bool VertBuildingLabel;

		public bool DontShowNorth;

		public float LevelHeight;

		public bool UseEkranirVertFlat;

		public bool UseEkranirGraph;

		public bool UseEkranirHorizFlat;

		public bool UseEkranirKT;

		public float ZoomAntennaSign;

		public float ZoomAntennaLine;

		public float ZoomPointSign;

		public float ZoomPointLine;

		public float ZoomAntennaText;

		public float ZoomPointText;

		public float ZoomBuildingText;

		public float ZoomLabelText;

		public float AntStep;

		public float Koef;
	}

	[Serializable]
	public struct ResultCalc
	{
		public float hXmax;

		public float hXmin;

		public float hYmax;

		public float hYmin;

		public Bitmap hBTM1;

		public Bitmap hBTMfill1;

		public float hKoef;

		public COORD[][][] hPline;

		public float[] hSquare;

		public bool[] vActiv;

		public float[] vAzimut;

		public float[] vXo;

		public float[] vYo;

		public float[] vHeight;

		public float[] vDepth;

		public float[] vLenght;

		public string[] vName;

		public Bitmap[] vBTM;

		public float[] vBtmHeightUndground;

		public float[,] vMesureLenght;

		public float[] vRelief0;

		public COORD[][][] vPline;

		public float[][,] PROFprof;

		public float[][,] PREPprof;

		public string[][] PREPprofLabel;

		public float[] gHeight;

		public float[] gLenght;

		public Bitmap[] gBTM;

		public string[,,] gTablKB;
	}

	public struct TableVert
	{
		public string Name;

		public float Azimut;

		public float Xo;

		public float Yo;

		public float Height;

		public float Lenght;

		public string[,] KB;

		public string[,] Esum;

		public string[,] PPEsum;
	}

	[Serializable]
	public struct MesureHeight
	{
		public string[] Height;

		public string[] Length;
	}

	[Serializable]
	public struct Points
	{
		public int X;

		public int Y;

		public float H;
	}

	[Serializable]
	public struct Pline
	{
		public int X;

		public int Y;

		public float H;

		public bool ViewHoriz;

		public string Label;

		public bool Ekranir;
	}

	[Serializable]
	public struct TextPoints
	{
		public int X;

		public int Y;

		public int Angle;

		public string Text;

		public string Font;
	}

	[Serializable]
	public struct PlineM
	{
		public float X;

		public float Y;

		public float H;
	}

	[Serializable]
	public struct FIDER
	{
		public string Name;

		public float[,] Loss;
	}

	public static INFO INF;

	public static PRTO[] PRT = new PRTO[1];

	public static ControlPOINT[] PT = new ControlPOINT[1];

	public static RasterPlan RST;

	public static ParamCalc PC;

	public static PrjTemplate PrjSetupTemplate;

	public static ResultCalc ResCalc;

	public static TableVert[] TBLv;

	public static MesureHeight[] MHL;

	public static Points[] PNT = new Points[1];

	public static Pline[][] PLN = new Pline[0][];

	public static Pline[][] BLD = new Pline[0][];

	public static Pline[][] ROAD = new Pline[0][];

	public static TextPoints[] TextPoint;

	public static PlineM[][] BLDm = new PlineM[0][];

	public static PlineM[][] ROADm = new PlineM[0][];

	public static FIDER[] FID = new FIDER[1];

	public static Pen[] PenDash = new Pen[28];

	public static void CopyPrjSetupToTemplate()
	{
		PrjSetupTemplate.AcadStart = PC.AcadStart;
		PrjSetupTemplate.ShowRelief = PC.ShowRelief;
		PrjSetupTemplate.NotShowAbsoluteHeight = PC.NotShowAbsoluteHeight;
		PrjSetupTemplate.ShowBuildings = PC.ShowBuildings;
		PrjSetupTemplate.CombineFlats = PC.CombineFlats;
		PrjSetupTemplate.TablUnderGraph = PC.TablUnderGraph;
		PrjSetupTemplate.LineWidth = PC.LineWidth;
		PrjSetupTemplate.VertBuildingLabel = PC.VertBuildingLabel;
		PrjSetupTemplate.DontShowNorth = PC.DontShowNorth;
		PrjSetupTemplate.LevelHeight = PC.LevelHeight;
		PrjSetupTemplate.UseEkranirVertFlat = PC.UseEkranirVertFlat;
		PrjSetupTemplate.UseEkranirGraph = PC.UseEkranirGraph;
		PrjSetupTemplate.UseEkranirHorizFlat = PC.UseEkranirHorizFlat;
		PrjSetupTemplate.UseEkranirKT = PC.UseEkranirKT;
		PrjSetupTemplate.ZoomAntennaSign = PC.ZoomAntennaSign;
		PrjSetupTemplate.ZoomAntennaLine = PC.ZoomAntennaLine;
		PrjSetupTemplate.ZoomPointSign = PC.ZoomPointSign;
		PrjSetupTemplate.ZoomPointLine = PC.ZoomPointLine;
		PrjSetupTemplate.ZoomAntennaText = PC.ZoomAntennaText;
		PrjSetupTemplate.ZoomPointText = PC.ZoomPointText;
		PrjSetupTemplate.ZoomBuildingText = PC.ZoomBuildingText;
		PrjSetupTemplate.ZoomLabelText = PC.ZoomLabelText;
		PrjSetupTemplate.AntStep = PC.AntStep;
		PrjSetupTemplate.Koef = PC.Koef;
	}

	public static void CopyPrjSetupFromTemplate()
	{
		PC.AcadStart = PrjSetupTemplate.AcadStart;
		PC.ShowRelief = PrjSetupTemplate.ShowRelief;
		PC.NotShowAbsoluteHeight = PrjSetupTemplate.NotShowAbsoluteHeight;
		PC.ShowBuildings = PrjSetupTemplate.ShowBuildings;
		PC.CombineFlats = PrjSetupTemplate.CombineFlats;
		PC.TablUnderGraph = PrjSetupTemplate.TablUnderGraph;
		PC.LineWidth = PrjSetupTemplate.LineWidth;
		PC.VertBuildingLabel = PrjSetupTemplate.VertBuildingLabel;
		PC.DontShowNorth = PrjSetupTemplate.DontShowNorth;
		PC.LevelHeight = PrjSetupTemplate.LevelHeight;
		PC.UseEkranirVertFlat = PrjSetupTemplate.UseEkranirVertFlat;
		PC.UseEkranirGraph = PrjSetupTemplate.UseEkranirGraph;
		PC.UseEkranirHorizFlat = PrjSetupTemplate.UseEkranirHorizFlat;
		PC.UseEkranirKT = PrjSetupTemplate.UseEkranirKT;
		PC.ZoomAntennaSign = PrjSetupTemplate.ZoomAntennaSign;
		PC.ZoomAntennaLine = PrjSetupTemplate.ZoomAntennaLine;
		PC.ZoomPointSign = PrjSetupTemplate.ZoomPointSign;
		PC.ZoomPointLine = PrjSetupTemplate.ZoomPointLine;
		PC.ZoomAntennaText = PrjSetupTemplate.ZoomAntennaText;
		PC.ZoomPointText = PrjSetupTemplate.ZoomPointText;
		PC.ZoomBuildingText = PrjSetupTemplate.ZoomBuildingText;
		PC.ZoomLabelText = PrjSetupTemplate.ZoomLabelText;
		PC.AntStep = PrjSetupTemplate.AntStep;
		PC.Koef = PrjSetupTemplate.Koef;
	}

	public static void InitialPenDash()
	{
		int upperBound = PenDash.GetUpperBound(0);
		for (int i = 0; i <= upperBound; i = checked(i + 1))
		{
			PenDash[i] = new Pen(Brushes.Black, 2f);
			PenDash[i].DashStyle = DashStyle.Custom;
		}
		PenDash[0] = new Pen(Brushes.Black, 2f);
		PenDash[0].DashStyle = DashStyle.Solid;
		PenDash[1].DashPattern = new float[2] { 1f, 1f };
		PenDash[2].DashPattern = new float[2] { 1f, 3f };
		PenDash[3].DashPattern = new float[2] { 1f, 5f };
		PenDash[4].DashPattern = new float[2] { 2f, 1f };
		PenDash[5].DashPattern = new float[2] { 2f, 3f };
		PenDash[6].DashPattern = new float[2] { 2f, 5f };
		PenDash[7].DashPattern = new float[2] { 5f, 1f };
		PenDash[8].DashPattern = new float[2] { 5f, 3f };
		PenDash[9].DashPattern = new float[2] { 5f, 5f };
		PenDash[10].DashPattern = new float[2] { 10f, 1f };
		PenDash[11].DashPattern = new float[2] { 10f, 3f };
		PenDash[12].DashPattern = new float[2] { 10f, 5f };
		PenDash[13].DashPattern = new float[4] { 5f, 1f, 1f, 1f };
		PenDash[14].DashPattern = new float[4] { 5f, 3f, 1f, 3f };
		PenDash[15].DashPattern = new float[4] { 5f, 10f, 1f, 10f };
		PenDash[16].DashPattern = new float[6] { 5f, 1f, 1f, 1f, 1f, 1f };
		PenDash[17].DashPattern = new float[6] { 5f, 3f, 1f, 1f, 1f, 3f };
		PenDash[18].DashPattern = new float[6] { 5f, 5f, 1f, 3f, 1f, 5f };
		PenDash[19].DashPattern = new float[4] { 5f, 5f, 10f, 5f };
		PenDash[20].DashPattern = new float[4] { 1f, 5f, 1f, 10f };
		PenDash[21].DashPattern = new float[6] { 1f, 5f, 1f, 10f, 1f, 10f };
		PenDash[22].DashPattern = new float[6] { 4f, 5f, 4f, 5f, 1f, 5f };
		PenDash[23].DashPattern = new float[8] { 1f, 5f, 1f, 5f, 4f, 5f, 4f, 5f };
		PenDash[24].DashPattern = new float[8] { 1f, 5f, 4f, 5f, 4f, 5f, 4f, 5f };
		PenDash[25].DashPattern = new float[10] { 1f, 5f, 1f, 5f, 1f, 5f, 4f, 5f, 4f, 5f };
		PenDash[26].DashPattern = new float[10] { 4f, 5f, 4f, 5f, 4f, 5f, 1f, 5f, 1f, 5f };
		PenDash[27].DashPattern = new float[10] { 1f, 5f, 4f, 5f, 1f, 5f, 4f, 5f, 1f, 5f };
	}

	public static void RecalcBLDm()
	{
		checked
		{
			RST.Koef = (float)((double)RST.Len / Math.Sqrt(Math.Pow(RST.X2pic - RST.X1pic, 2.0) + Math.Pow(RST.Y2pic - RST.Y1pic, 2.0)));
			BLDm = new PlineM[BLD.GetUpperBound(0) + 1][];
			int upperBound = BLD.GetUpperBound(0);
			for (int i = 1; i <= upperBound; i++)
			{
				BLDm[i] = new PlineM[BLD[i].GetUpperBound(0) + 1];
				int upperBound2 = BLD[i].GetUpperBound(0);
				for (int j = 1; j <= upperBound2; j++)
				{
					BLDm[i][j].X = (float)(BLD[i][j].X - RST.Xpic) * RST.Koef;
					BLDm[i][j].Y = (float)(-(BLD[i][j].Y - RST.Ypic)) * RST.Koef;
					BLDm[i][j].H = BLD[i][j].H;
				}
			}
		}
	}
}
