using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[StandardModule]
internal sealed class Public2
{
	[Serializable]
	public struct GlobalSetup
	{
		public string FolderCach;

		public string AntennaFolder;

		public string NameTileServer;

		public byte Projection;

		public int ZOOM;

		public int TileX;

		public int TileY;

		public int PointX;

		public int PointY;

		public bool FlagProxy;

		public string ProxyIP;

		public int ProxyPort;

		public bool FlagProxyLogin;

		public string ProxyLogin;

		public string ProxyPassword;

		public byte CoordinatesFormat;

		public bool DontCheckNewVersionEveryAppStart;

		public string[] OwnerNameList;

		public Color[] OwnerColorList;

		public short[] MaxHorSize;

		public string FolderProject;

		public string FolderAntennaPattren;

		public byte[] PrtoOrder;

		public CoordWebMerc OriginCoordWM;

		public CoordWGS OriginCoordWGS;

		public CoordWGS PRTOCoordWGS;

		public CoordWebMerc[] HorAreaCoordWM;

		public CoordMeters[] HorAreaCoordM;

		public List<LimitPoint> LMT;

		public string[] LimitDescription;

		public Color[] LimitColor;

		public bool[] LimitAlpha24;

		public bool[] LimitFtdc;

		public bool[] LimitFpr;

		public bool UseGainForEnvelop;
	}

	[Serializable]
	public struct PrjSetup
	{
		public long GUID;

		public string NameTileServer;

		public byte Projection;

		public int ZOOM;

		public int TileX;

		public int TileY;

		public int PointX;

		public int PointY;

		public bool ShowMap;

		public byte MapBrightness;

		public float KoefOslabl;

		public bool ConsiderRelief;

		public CoordWebMerc OriginCoordWM;

		public CoordWGS OriginCoordWGS;

		public CoordWGS PRTOCoordWGS;

		public CoordWebMerc[] HorAreaCoordWM;

		public CoordMeters[] HorAreaCoordM;

		public short[] MaxHorSize;

		public bool CombineHorPlanes;

		public bool CombineHorPlanesByColor;

		public short HorSize;

		public byte LengthAttractAntennas;

		public bool ShowAntennas;

		public bool ShowAntennaLabels;

		public bool ShowAntennaAzimuth;

		public bool ShowLongAzimuth;

		public bool ShowControlPoints;

		public bool ShowControlPointGroups;

		public bool ShowControlPointHeight;

		public string ControlPointPrefix;

		public bool ShowOrigin;

		public bool ShowHorizPlaneScale;

		public bool ShowConturZO;

		public byte TransparFillZO;

		public byte LineWidthZO;

		public bool ShowVertPlaneLines;

		public bool ShowLengthZO;

		public bool ShowHgtBottomZO;

		public bool ShowHgtMaxZO;

		public bool ShowHgtTopZO;

		public bool ShowManualPointsZO;

		public bool ShowMinDistToZO;

		public bool ShowKPonVert;

		public bool ShowPlus2m;

		public float LineHightAboveTerrain;

		public byte TransparFillVertCut;

		public byte LineWidthVertCut;

		public bool ShowAllBuildings;

		public bool ShowBuildingText;

		public bool ShowBuildingHeight;

		public bool ShowBuildingFoundation;

		public byte BuildingLineWidth;

		public byte BuildingTextSize;

		public Color BuildingColor;

		public bool ShowRelief;

		public bool ShowRoads;

		public bool ShowFences;

		public bool ShowText;

		public bool ShowCircles;

		public float CircleStep;

		public float Tabl1LengthMax;

		public float Tabl1StepL;

		public float Tabl1HeightMax;

		public float Tabl1StepH;

		public float Tabl2LengthMax;

		public float Tabl2StepL;

		public float Tabl2Height;

		public float Tabl3LengthMax;

		public float Tabl3StepL;

		public float[] Tabl3Height;

		public float TablRadial1LengthMax;

		public float TablRadial1StepL;

		public float TablRadial1Angle;

		public float[] TablRadial1Height;

		public float Graph1LengthMax;

		public float Graph1Height;

		public float TablHoriz1Angle;

		public string RepFileName;

		public string[,] ALS;

		public List<LimitPoint> LMT;

		public string[] LimitDescription;

		public Color[] LimitColor;

		public bool[] LimitAlpha24;

		public bool[] LimitFtdc;

		public bool[] LimitFpr;

		public byte TableGraphLimitIndex;

		public bool[] VertCutLimits;

		public bool[] HorizCutLimits;

		public bool FlagLastRezManyLimits;

		public byte ControlPointLimitIndex;

		public float ControlPointMesureFreq;

		public byte UseBuildings;

		public float BuildAttenSubband1;

		public float BuildAttenSubband2;

		public float BuildFreqBorder;
	}

	[Serializable]
	public struct TxPRTO
	{
		public bool Active;

		public string Name;

		public string TypeOfService;

		public float F;

		public float F2;

		public float P;

		public int N;

		public float Loss;

		public string AntType;

		public float Az;

		public float G;

		public float H;

		public float D;

		public float Ug;

		public float ElectUg;

		public string UgH;

		public string UgV;

		public string Location;

		public string Exist;

		public string Owner;

		public float[] DnH;

		public float[] DnV;

		public string Sector;

		public float[] Alpha24;

		public float[] Ftdc;

		public float[] Fpr;

		public string Cable;

		public float LengthCable;

		public string Jumper;

		public string LengthJumper;

		public bool ChkLossSocket;

		public float LossSocketOne;

		public float ExtraLoss;

		public Color Color;

		public CoordWebMerc CoordWM;

		public CoordMeters CoordM;

		public bool Selected;

		public string PTOT()
		{
			return Module1.SuperFormat((float)((double)(P * (float)N) * Math.Pow(10.0, (0f - Loss) / 10f)), 6);
		}
	}

	[Serializable]
	public struct LimitPoint
	{
		public float Freq;

		public float[] E;

		public float[] A;

		public LimitPoint(float _F, float E0, float A0, float E1, float A1, float E2, float A2, float E3, float A3)
		{
			this = default;
			Freq = _F;
			E = new float[4] { E0, E1, E2, E3 };
			A = new float[4] { A0, A1, A2, A3 };
		}
	}

	[Serializable]
	public struct ControlPointGroup
	{
		public bool Active;

		public string Name;

		public ControlPoint[] CP;
	}

	[Serializable]
	public struct ControlPoint
	{
		public CoordWebMerc CoordWM;

		public CoordMeters CoordM;

		public string Name;

		public bool Selected;

		public string Location;

		public ControlPointHeight[] H;
	}

	[Serializable]
	public struct ControlPointHeight
	{
		public float H;

		public float L;

		public float MeasuredE;

		public string Name;
	}

	[Serializable]
	public struct StructBuilding
	{
		public bool Show;

		public float H;

		public float Fundation;

		public string Text;

		public CoordWebMerc[] CoordWM;

		public CoordMeters[] CoordM;

		public bool UseAttenuation;
	}

	[Serializable]
	public struct StructRoad
	{
		public CoordWebMerc[] CoordWM;

		public byte WidthM;

		public byte WidthBorder;
	}

	[Serializable]
	public struct StructFence
	{
		public CoordWebMerc[] CoordWM;

		public byte Type;

		public byte Width;
	}

	[Serializable]
	public struct StructText
	{
		public CoordWebMerc CoordWM;

		public string Text;

		public string FontName;

		public byte Size;

		public short Angle;

		public bool Point;

		public Bitmap Icon;
	}

	[Serializable]
	public struct StructElevLine
	{
		public CoordWebMerc[] CoordWM;

		public float H;

		public bool CloseLine;
	}

	[Serializable]
	public struct StructElevPoint
	{
		public CoordWebMerc CoordWM;

		public float H;
	}

	[Serializable]
	public struct CoordWebMerc
	{
		public double Xwm;

		public double Ywm;
	}

	[Serializable]
	public struct CoordMeters
	{
		public double Xm;

		public double Ym;
	}

	[Serializable]
	public struct CoordWGS
	{
		public double Lat;

		public double Lon;
	}

	[Serializable]
	public struct CoordPix
	{
		public short Xpix;

		public short Ypix;
	}

	[Serializable]
	public struct RasterPlan
	{
		public Bitmap BTM;

		public short Xpb;

		public short Ypb;

		public short RasterScale;

		public bool UseScalingLine;

		public float Length;

		public CoordPix[] PointRaster;

		public CoordWebMerc[] PointMap;
	}

	[Serializable]
	public struct StructHorizPlane
	{
		public bool Activ;

		public float Height;

		public Color Color;

		public byte LineType;

		public CoordWebMerc[][] PLNwm;

		public bool FlagCombine;
	}

	[Serializable]
	public struct StructBuildingProf
	{
		public float L1;

		public float L2;

		public float H;

		public float Fundation;

		public string Text;

		public bool UseAttenuation;
	}

	[Serializable]
	public struct StructMesurePoint
	{
		public float R;

		public float H;
	}

	[Serializable]
	public struct StructDimension
	{
		public float R;

		public float H;

		public float Hz;

		public string Text;
	}

	[Serializable]
	public struct CoordSNG
	{
		public float X;

		public float Y;
	}

	[Serializable]
	public struct StructVertPlane
	{
		public bool Activ;

		public float Azimuth;

		public CoordWebMerc CoordWM;

		public CoordMeters CoordM;

		public float H1;

		public float H2;

		public float L1;

		public float L2;

		public string Name;

		public CoordMeters Coord1M;

		public CoordMeters Coord2M;

		public short Size;

		public float LastH1;

		public float LastH2;

		public float LastL1;

		public float LastL2;

		public CoordSNG[][][] PLNs;

		public float[] Elev;

		public StructBuildingProf[] Bld;

		public float Elev0;

		public float LenghtZO;

		public float HeightBottomZO;

		public float HeightMaxZO;

		public float HeightTopZO;

		public StructMesurePoint[] ManualPointZO;

		public StructDimension[] DimensionZO;

		public StructDimension DimensionLowerestZO;
	}

	[Serializable]
	public struct Project
	{
		public PrjSetup SETT;

		public TxPRTO[] TX;

		public ControlPointGroup[] CPG;

		public RasterPlan RPL;

		public StructBuilding[] BLD;

		public StructRoad[] ROAD;

		public StructFence[] FENCE;

		public StructText[] TXT;

		public StructElevLine[] ElLN;

		public StructElevPoint[] ElPT;

		public StructHorizPlane[] HP;

		public StructVertPlane[] VP;

		public CoordWebMerc[][][] HP_PLNwm;
	}

	public static double BufSize;

	public static string ProjectFileName;

	public static string AppDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\EMFPlanner";

	public static string AppStartupPath = Application.StartupPath;

	public static GlobalSetup AppSetup;

	public static Project PRJ;

	public static void NewPrj()
	{
		PRJ.SETT.GUID = DateAndTime.Now.Ticks;
		PRJ.SETT.NameTileServer = AppSetup.NameTileServer;
		PRJ.SETT.Projection = AppSetup.Projection;
		PRJ.SETT.ZOOM = AppSetup.ZOOM;
		PRJ.SETT.TileX = AppSetup.TileX;
		PRJ.SETT.TileY = AppSetup.TileY;
		PRJ.SETT.PointX = AppSetup.PointX;
		PRJ.SETT.PointY = AppSetup.PointY;
		PRJ.SETT.ShowMap = true;
		PRJ.SETT.MapBrightness = 0;
		PRJ.SETT.KoefOslabl = 1.3f;
		PRJ.SETT.ConsiderRelief = true;
		PRJ.SETT.OriginCoordWM.Xwm = 1608744.0;
		PRJ.SETT.OriginCoordWM.Ywm = 6462657.0;
		CalcCoords.ConvWebMercMetersToWGS84(PRJ.SETT.OriginCoordWM, ref PRJ.SETT.OriginCoordWGS.Lat, ref PRJ.SETT.OriginCoordWGS.Lon);
		PRJ.SETT.PRTOCoordWGS = PRJ.SETT.OriginCoordWGS;
		PRJ.SETT.HorAreaCoordWM = new CoordWebMerc[2];
		PRJ.SETT.HorAreaCoordWM[0].Xwm = PRJ.SETT.OriginCoordWM.Xwm - 100.0;
		PRJ.SETT.HorAreaCoordWM[0].Ywm = PRJ.SETT.OriginCoordWM.Ywm - 100.0;
		PRJ.SETT.HorAreaCoordWM[1].Xwm = PRJ.SETT.OriginCoordWM.Xwm + 100.0;
		PRJ.SETT.HorAreaCoordWM[1].Ywm = PRJ.SETT.OriginCoordWM.Ywm + 100.0;
		PRJ.SETT.HorAreaCoordM = new CoordMeters[2];
		PRJ.SETT.HorAreaCoordM[0] = CalcCoords.ConvWebMercMetersToMeters(PRJ.SETT.HorAreaCoordWM[0]);
		PRJ.SETT.HorAreaCoordM[1] = CalcCoords.ConvWebMercMetersToMeters(PRJ.SETT.HorAreaCoordWM[1]);
		PRJ.SETT.MaxHorSize = new short[2] { 600, 200 };
		PRJ.SETT.CombineHorPlanes = true;
		PRJ.SETT.CombineHorPlanesByColor = true;
		PRJ.SETT.HorSize = 0;
		PRJ.HP = null;
		if (!Information.IsNothing(AppSetup.OriginCoordWM) & !Information.IsNothing(AppSetup.OriginCoordWGS) & !Information.IsNothing(AppSetup.PRTOCoordWGS) & !Information.IsNothing(AppSetup.HorAreaCoordWM) & !Information.IsNothing(AppSetup.HorAreaCoordM))
		{
			PRJ.SETT.OriginCoordWM = Module1.DeepClone(AppSetup.OriginCoordWM);
			PRJ.SETT.OriginCoordWGS = Module1.DeepClone(AppSetup.OriginCoordWGS);
			PRJ.SETT.PRTOCoordWGS = Module1.DeepClone(AppSetup.PRTOCoordWGS);
			PRJ.SETT.HorAreaCoordWM = Module1.DeepClone(AppSetup.HorAreaCoordWM);
			PRJ.SETT.HorAreaCoordM = Module1.DeepClone(AppSetup.HorAreaCoordM);
		}
		PRJ.SETT.LengthAttractAntennas = 15;
		PRJ.SETT.ShowAntennas = true;
		PRJ.SETT.ShowAntennaLabels = true;
		PRJ.SETT.ShowAntennaAzimuth = true;
		PRJ.SETT.ShowLongAzimuth = false;
		PRJ.SETT.ShowControlPoints = true;
		PRJ.SETT.ShowControlPointGroups = true;
		PRJ.SETT.ShowControlPointHeight = true;
		PRJ.SETT.ControlPointPrefix = "";
		PRJ.SETT.ShowOrigin = true;
		PRJ.SETT.ShowHorizPlaneScale = true;
		PRJ.SETT.TransparFillZO = 1;
		PRJ.SETT.ShowLengthZO = true;
		PRJ.SETT.ShowHgtBottomZO = true;
		PRJ.SETT.ShowHgtMaxZO = true;
		PRJ.SETT.ShowHgtTopZO = false;
		PRJ.SETT.ShowMinDistToZO = true;
		PRJ.SETT.ShowManualPointsZO = true;
		PRJ.SETT.ShowKPonVert = true;
		PRJ.SETT.ShowPlus2m = false;
		PRJ.SETT.TransparFillVertCut = 0;
		PRJ.SETT.LineWidthVertCut = 3;
		PRJ.SETT.LineHightAboveTerrain = 2f;
		PRJ.SETT.LineWidthZO = 2;
		PRJ.SETT.ShowAllBuildings = true;
		PRJ.SETT.ShowBuildingText = true;
		PRJ.SETT.ShowBuildingHeight = false;
		PRJ.SETT.ShowBuildingFoundation = false;
		PRJ.SETT.ShowRelief = true;
		PRJ.SETT.BuildingLineWidth = 2;
		PRJ.SETT.BuildingTextSize = 9;
		PRJ.SETT.BuildingColor = Color.Black;
		PRJ.SETT.ShowRoads = true;
		PRJ.SETT.ShowFences = true;
		PRJ.SETT.ShowText = true;
		PRJ.SETT.ShowConturZO = true;
		PRJ.SETT.ShowVertPlaneLines = false;
		PRJ.SETT.ShowCircles = false;
		PRJ.SETT.CircleStep = 50f;
		PRJ.SETT.Tabl1LengthMax = 200f;
		PRJ.SETT.Tabl1StepL = 5f;
		PRJ.SETT.Tabl1HeightMax = 50f;
		PRJ.SETT.Tabl1StepH = 2f;
		PRJ.SETT.Tabl2LengthMax = 200f;
		PRJ.SETT.Tabl2StepL = 5f;
		PRJ.SETT.Tabl2Height = 50f;
		PRJ.SETT.Tabl3LengthMax = 200f;
		PRJ.SETT.Tabl3StepL = 5f;
		PRJ.SETT.Tabl3Height = new float[3] { 2f, 10f, 20f };
		PRJ.SETT.TablRadial1LengthMax = 200f;
		PRJ.SETT.TablRadial1StepL = 5f;
		PRJ.SETT.TablRadial1Angle = 10f;
		PRJ.SETT.TablRadial1Height = new float[2] { 2f, 10f };
		PRJ.SETT.Graph1LengthMax = 200f;
		PRJ.SETT.Graph1Height = 50f;
		PRJ.SETT.TablHoriz1Angle = 5f;
		PRJ.SETT.RepFileName = "";
		NewAliases();
		PRJ.TX = null;
		PRJ.CPG = new ControlPointGroup[1];
		PRJ.CPG[0].Active = true;
		PRJ.CPG[0].Name = Localization.LocMSG(44);
		PRJ.RPL.BTM = null;
		PRJ.RPL.Xpb = 0;
		PRJ.RPL.Ypb = 0;
		PRJ.RPL.RasterScale = 1;
		PRJ.RPL.UseScalingLine = false;
		PRJ.RPL.Length = 50f;
		PRJ.RPL.PointRaster = new CoordPix[3];
		PRJ.RPL.PointRaster[0].Xpix = 300;
		PRJ.RPL.PointRaster[0].Ypix = 300;
		PRJ.RPL.PointRaster[1].Xpix = 250;
		PRJ.RPL.PointRaster[1].Ypix = 250;
		PRJ.RPL.PointRaster[2].Xpix = 350;
		PRJ.RPL.PointRaster[2].Ypix = 350;
		PRJ.RPL.PointMap = new CoordWebMerc[3];
		PRJ.RPL.PointMap[0].Xwm = PRJ.SETT.OriginCoordWM.Xwm;
		PRJ.RPL.PointMap[0].Ywm = PRJ.SETT.OriginCoordWM.Ywm;
		PRJ.RPL.PointMap[1].Xwm = PRJ.SETT.OriginCoordWM.Xwm - 25.0;
		PRJ.RPL.PointMap[1].Ywm = PRJ.SETT.OriginCoordWM.Ywm - 25.0;
		PRJ.RPL.PointMap[2].Xwm = PRJ.SETT.OriginCoordWM.Xwm + 25.0;
		PRJ.RPL.PointMap[2].Ywm = PRJ.SETT.OriginCoordWM.Ywm + 25.0;
		PRJ.BLD = null;
		PRJ.ROAD = null;
		PRJ.FENCE = null;
		PRJ.TXT = null;
		PRJ.ElLN = null;
		PRJ.ElPT = null;
		PRJ.HP = null;
		PRJ.VP = null;
		Form1_Public.VPSelRow = 0;
		Form1_Public.CPGSelRow = 0;
		Triangul.TRG = null;
		PRJ.SETT.LMT = Module1.DeepClone(AppSetup.LMT);
		PRJ.SETT.LimitDescription = Module1.DeepClone(AppSetup.LimitDescription);
		PRJ.SETT.LimitColor = Module1.DeepClone(AppSetup.LimitColor);
		PRJ.SETT.LimitAlpha24 = Module1.DeepClone(AppSetup.LimitAlpha24);
		PRJ.SETT.LimitFtdc = Module1.DeepClone(AppSetup.LimitFtdc);
		PRJ.SETT.LimitFpr = Module1.DeepClone(AppSetup.LimitFpr);
		if (PRJ.SETT.LimitAlpha24 == null)
		{
			AppSetup.LimitAlpha24 = new bool[4] { true, true, true, true };
			AppSetup.LimitFtdc = new bool[4] { true, true, true, true };
			AppSetup.LimitFpr = new bool[4] { true, true, true, true };
			PRJ.SETT.LimitAlpha24 = Module1.DeepClone(AppSetup.LimitAlpha24);
			PRJ.SETT.LimitFtdc = Module1.DeepClone(AppSetup.LimitFtdc);
			PRJ.SETT.LimitFpr = Module1.DeepClone(AppSetup.LimitFpr);
		}
		PRJ.SETT.TableGraphLimitIndex = 0;
		PRJ.SETT.VertCutLimits = new bool[4];
		PRJ.SETT.VertCutLimits[0] = true;
		PRJ.SETT.HorizCutLimits = new bool[4];
		PRJ.SETT.HorizCutLimits[0] = true;
		PRJ.SETT.ControlPointLimitIndex = 0;
		PRJ.SETT.ControlPointMesureFreq = 1000f;
		PRJ.SETT.UseBuildings = 0;
		PRJ.SETT.BuildAttenSubband1 = 3f;
		PRJ.SETT.BuildAttenSubband2 = 6f;
		PRJ.SETT.BuildFreqBorder = 400f;
	}

	public static void SetLimitsPRJ(string CountryIndex)
	{
		PRJ.SETT.LMT = new List<LimitPoint>();
		if ((Operators.CompareString(CountryIndex, "ICNIRP", TextCompare: false) == 0) | (Operators.CompareString(CountryIndex, "EU", TextCompare: false) == 0))
		{
			PRJ.SETT.LMT.Add(new LimitPoint(400f, 28f, 0f, 61f, 0f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LMT.Add(new LimitPoint(2000f, 1.375f, 0.5f, 3f, 0.5f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LMT.Add(new LimitPoint(1000000f, 61f, 0f, 137f, 0f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LimitDescription = new string[4] { "Public exposure limits", "Occupational exposure limits", "", "" };
			return;
		}
		switch (CountryIndex)
		{
		case "Belgium":
			PRJ.SETT.LMT.Add(new LimitPoint(400f, 14f, 0f, 61f, 0f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LMT.Add(new LimitPoint(2000f, 0.688f, 0.5f, 3f, 0.5f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LMT.Add(new LimitPoint(1000000f, 30.5f, 0f, 137f, 0f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LimitDescription = new string[4] { "Public exposure limits", "Occupational exposure limits", "", "" };
			break;
		case "Bulgaria":
			PRJ.SETT.LMT.Add(new LimitPoint(400f, 6f, 0f, 61f, 0f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LMT.Add(new LimitPoint(2000f, 6f, 0f, 3f, 0.5f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LMT.Add(new LimitPoint(1000000f, 6f, 0f, 137f, 0f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LimitDescription = new string[4] { "Public exposure limits", "Occupational exposure limits", "", "" };
			break;
		case "China":
			PRJ.SETT.LMT.Add(new LimitPoint(100000f, 12f, 0f, 137f, 0f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LimitDescription = new string[4] { "Public exposure limits", "Occupational exposure limits", "", "" };
			break;
		case "Croatia":
			PRJ.SETT.LMT.Add(new LimitPoint(400f, 11.2f, 0f, 61f, 0f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LMT.Add(new LimitPoint(2000f, 0.55f, 0.5f, 3f, 0.5f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LMT.Add(new LimitPoint(1000000f, 24.4f, 0f, 137f, 0f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LimitDescription = new string[4] { "Public exposure limits", "Occupational exposure limits", "", "" };
			break;
		case "Greece":
			PRJ.SETT.LMT.Add(new LimitPoint(400f, 21.56f, 0f, 61f, 0f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LMT.Add(new LimitPoint(2000f, 1.059f, 0.5f, 3f, 0.5f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LMT.Add(new LimitPoint(1000000f, 46.97f, 0f, 137f, 0f, -999f, -999f, -999f, -999f));
			break;
		case "India":
			PRJ.SETT.LMT.Add(new LimitPoint(400f, 8.854f, 0f, 61f, 0f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LMT.Add(new LimitPoint(2000f, 0.435f, 0.5f, 3f, 0.5f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LMT.Add(new LimitPoint(1000000f, 19.29f, 0f, 137f, 0f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LimitDescription = new string[4] { "Public exposure limits", "Occupational exposure limits", "", "" };
			break;
		case "Italy":
			PRJ.SETT.LMT.Add(new LimitPoint(100000f, 6f, 0f, 20f, 0f, 4.5f, 0f, 3f, 0f));
			PRJ.SETT.LimitDescription = new string[4] { "Inside buildings and/or every space with people might stay continuously more than 4 hours (garden, terrace, balcony, etc)", "Every space with people usually stay continuously less than 4 hours (streets, open fields, etc)", "75% of EMF Limits 2", "Full safety value" };
			break;
		case "Lithuania":
			PRJ.SETT.LMT.Add(new LimitPoint(400f, 8.854f, 0f, 61f, 0f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LMT.Add(new LimitPoint(2000f, 0.435f, 0.5f, 3f, 0.5f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LMT.Add(new LimitPoint(1000000f, 19.29f, 0f, 137f, 0f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LimitDescription = new string[4] { "Public exposure limits", "Occupational exposure limits", "", "" };
			break;
		case "Poland":
			PRJ.SETT.LMT.Add(new LimitPoint(100000f, 7f, 0f, 60f, 0f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LimitDescription = new string[4] { "Public exposure limits", "Occupational exposure limits", "", "" };
			break;
		case "Russia":
			PRJ.SETT.LMT.Add(new LimitPoint(300f, 3f, 0f, -999f, -999f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LMT.Add(new LimitPoint(100000f, 6.1f, 0f, -999f, -999f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LimitDescription = new string[4] { "Public exposure limits", "", "", "" };
			break;
		case "Slovenia":
			PRJ.SETT.LMT.Add(new LimitPoint(400f, 8.854f, 0f, 61f, 0f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LMT.Add(new LimitPoint(2000f, 0.435f, 0.5f, 3f, 0.5f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LMT.Add(new LimitPoint(1000000f, 19.29f, 0f, 137f, 0f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LimitDescription = new string[4] { "Public exposure limits", "Occupational exposure limits", "", "" };
			break;
		case "Switzerland":
			PRJ.SETT.LMT.Add(new LimitPoint(400f, 5f, 0f, 61f, 0f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LMT.Add(new LimitPoint(2000f, 5f, 0f, 3f, 0.5f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LMT.Add(new LimitPoint(1000000f, 5f, 0f, 137f, 0f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LimitDescription = new string[4] { "Public exposure limits", "Occupational exposure limits", "", "" };
			break;
		case "UK":
			PRJ.SETT.LMT.Add(new LimitPoint(400f, 28f, 0f, 61f, 0f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LMT.Add(new LimitPoint(2000f, 1.375f, 0.5f, 3f, 0.5f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LMT.Add(new LimitPoint(1000000f, 61f, 0f, 137f, 0f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LimitDescription = new string[4] { "Public exposure limits", "Occupational exposure limits", "", "" };
			break;
		case "USA":
			PRJ.SETT.LMT.Add(new LimitPoint(30f, 824f, -1f, 1842f, -1f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LMT.Add(new LimitPoint(300f, 27.5f, 0f, 61.4f, 0f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LMT.Add(new LimitPoint(1500f, 1.585f, 0.5f, 3.545f, 0.5f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LMT.Add(new LimitPoint(1000000f, 61.4f, 0f, 137.3f, 0f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LimitDescription = new string[4] { "Limits for General Population/ Uncontrolled Exposure", "Limits for Occupational/ Controlled Exposure", "", "" };
			break;
		default:
			PRJ.SETT.LMT.Add(new LimitPoint(1000000f, 3f, 0f, 10f, 0f, -999f, -999f, -999f, -999f));
			PRJ.SETT.LimitDescription = new string[4] { "Limits 1", "Limits 2", "", "" };
			break;
		}
	}

	public static void SetLimitsAPP()
	{
		AppSetup.LMT = new List<LimitPoint>();
		AppSetup.LMT.Add(new LimitPoint(400f, 28f, 0f, 61f, 0f, -999f, -999f, -999f, -999f));
		AppSetup.LMT.Add(new LimitPoint(2000f, 1.375f, 0.5f, 3f, 0.5f, -999f, -999f, -999f, -999f));
		AppSetup.LMT.Add(new LimitPoint(1000000f, 61f, 0f, 137f, 0f, -999f, -999f, -999f, -999f));
		AppSetup.LimitDescription = new string[4] { "Public exposure limits", "Occupational exposure limits", "", "" };
		AppSetup.LimitColor = new Color[4]
		{
			Color.FromArgb(255, 0, 0),
			Color.FromArgb(0, 0, 255),
			Color.FromArgb(0, 255, 0),
			Color.FromArgb(0, 255, 255)
		};
		AppSetup.LimitAlpha24 = new bool[4] { true, true, true, true };
		AppSetup.LimitFtdc = new bool[4] { true, true, true, true };
		AppSetup.LimitFpr = new bool[4] { true, true, true, true };
	}

	public static void NewAliases()
	{
		PRJ.SETT.ALS = new string[2, 6];
		PRJ.SETT.ALS[0, 0] = "{ProjectCode}";
		PRJ.SETT.ALS[1, 0] = "ProjectCode";
		PRJ.SETT.ALS[0, 1] = "{ProjectName}";
		PRJ.SETT.ALS[1, 1] = "ProjectName";
		PRJ.SETT.ALS[0, 2] = "{SiteAddress}";
		PRJ.SETT.ALS[1, 2] = "SiteAddress";
		PRJ.SETT.ALS[0, 3] = "{Customer}";
		PRJ.SETT.ALS[1, 3] = "Customer";
		PRJ.SETT.ALS[0, 4] = "{Designer}";
		PRJ.SETT.ALS[1, 4] = "Designer";
		PRJ.SETT.ALS[0, 5] = "{Date}";
		PRJ.SETT.ALS[1, 5] = DateTime.Now.ToString("dd/MM/yyyy");
	}

	public static void SetPrtoOrder()
	{
		AppSetup.PrtoOrder = new byte[21];
		AppSetup.PrtoOrder[0] = 0;
		AppSetup.PrtoOrder[1] = 1;
		AppSetup.PrtoOrder[2] = 3;
		AppSetup.PrtoOrder[3] = 4;
		AppSetup.PrtoOrder[4] = 5;
		AppSetup.PrtoOrder[5] = 6;
		AppSetup.PrtoOrder[6] = 7;
		AppSetup.PrtoOrder[7] = 9;
		AppSetup.PrtoOrder[8] = 10;
		AppSetup.PrtoOrder[9] = 11;
		AppSetup.PrtoOrder[10] = 12;
		AppSetup.PrtoOrder[11] = 13;
		AppSetup.PrtoOrder[12] = 14;
		AppSetup.PrtoOrder[13] = 15;
		AppSetup.PrtoOrder[14] = 16;
		AppSetup.PrtoOrder[15] = 17;
		AppSetup.PrtoOrder[16] = 18;
		AppSetup.PrtoOrder[17] = 19;
		AppSetup.PrtoOrder[18] = 20;
		AppSetup.PrtoOrder[19] = 8;
		AppSetup.PrtoOrder[20] = 2;
	}
}
