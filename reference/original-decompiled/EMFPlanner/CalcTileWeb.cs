using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using EMFPlanner.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[StandardModule]
internal sealed class CalcTileWeb
{
	public struct TileItem
	{
		public string UrlName;

		public string FileName;

		public string FolderName;

		public int X;

		public int Y;

		public int Z;
	}

	private static Random RND = new Random();

	public static TileItem[] ListTiles;

	private static object CountLock = RuntimeHelpers.GetObjectValue(new object());

	public static void LoadTileServer(string NameTileServer, ref int PROJECTION, ref string NameInCacheTileServer, ref string ExtentionTileServer, ref string UrlTileServer)
	{
		if (Operators.CompareString(NameTileServer, "Calibrated map image", TextCompare: false) == 0)
		{
			NameInCacheTileServer = "SITPLAN";
			PROJECTION = 1;
			ExtentionTileServer = ".png";
			UrlTileServer = "";
			return;
		}
		StreamReader streamReader = new StreamReader(Public2.AppDataFolder + "\\CustomBaseMaps.set", Encoding.GetEncoding(1251));
		checked
		{
			while (!streamReader.EndOfStream)
			{
				string text = streamReader.ReadLine();
				if (Strings.Len(Strings.Trim(text)) <= 10)
				{
					continue;
				}
				short num = (short)Strings.InStr(text, "\t");
				string expression = text.Substring(0, num);
				string expression2 = text.Substring(num);
				expression = Strings.Replace(expression, "\t", "");
				expression2 = Strings.Replace(expression2, "\t", "");
				short num2 = (short)expression2.Length;
				if ((Operators.CompareString(expression, NameTileServer, TextCompare: false) == 0) & (num2 > 7))
				{
					NameInCacheTileServer = expression;
					PROJECTION = 1;
					if (expression2.Contains("yandex"))
					{
						PROJECTION = 2;
					}
					UrlTileServer = expression2;
					string text2 = expression2.Substring(num2 - 4);
					if ((Operators.CompareString(text2, ".png", TextCompare: false) == 0) | (Operators.CompareString(text2, ".jpg", TextCompare: false) == 0))
					{
						ExtentionTileServer = text2;
					}
					else
					{
						ExtentionTileServer = "";
					}
					return;
				}
			}
			streamReader.Close();
			if (Information.IsNothing(NameInCacheTileServer))
			{
				NameTileServer = "MAPNIK (OpenStreetMap)";
				NameInCacheTileServer = "MAPNIK (OpenStreetMap)";
				PROJECTION = 1;
				ExtentionTileServer = "";
				UrlTileServer = "http://a.tile.openstreetmap.org/[Z]/[X]/[Y].png";
			}
		}
	}

	public static void CalcNameTile(int X, int Y, string ZOOM, string NameInCacheTileServer, string ExtentionTileServer, string UrlTileServer, ref string NameTileCACH, ref string NameTileURL, ref string NameTileDir)
	{
		checked
		{
			int num = (int)Conversion.Int((double)X / 1024.0);
			int num2 = (int)Conversion.Int((double)Y / 1024.0);
			if (Operators.CompareString(NameInCacheTileServer, "SITPLAN", TextCompare: false) == 0)
			{
				NameTileURL = "SITPLAN";
				NameTileCACH = Public2.AppSetup.FolderCach + "\\SitPlan\\" + Conversions.ToString(Public2.PRJ.SETT.GUID) + "\\z" + Conversions.ToString(Conversions.ToDouble(ZOOM) + 1.0) + "\\" + Conversions.ToString(num) + "\\x" + Conversions.ToString(X) + "\\" + Conversions.ToString(num2) + "\\y" + Conversions.ToString(Y) + ExtentionTileServer;
				NameTileDir = Public2.AppSetup.FolderCach + "\\SitPlan\\" + Conversions.ToString(Public2.PRJ.SETT.GUID) + "\\z" + Conversions.ToString(Conversions.ToDouble(ZOOM) + 1.0) + "\\" + Conversions.ToString(num) + "\\x" + Conversions.ToString(X) + "\\" + Conversions.ToString(num2);
				return;
			}
			NameTileCACH = Public2.AppSetup.FolderCach + "\\" + NameInCacheTileServer + "\\z" + Conversions.ToString(Conversions.ToDouble(ZOOM) + 1.0) + "\\" + Conversions.ToString(num) + "\\x" + Conversions.ToString(X) + "\\" + Conversions.ToString(num2) + "\\y" + Conversions.ToString(Y) + ExtentionTileServer;
			NameTileDir = Public2.AppSetup.FolderCach + "\\" + NameInCacheTileServer + "\\z" + Conversions.ToString(Conversions.ToDouble(ZOOM) + 1.0) + "\\" + Conversions.ToString(num) + "\\x" + Conversions.ToString(X) + "\\" + Conversions.ToString(num2);
			NameTileURL = UrlTileServer.Replace("[X]", Conversions.ToString(X));
			NameTileURL = NameTileURL.Replace("[Y]", Conversions.ToString(Y));
			NameTileURL = NameTileURL.Replace("[Z]", ZOOM);
			NameTileURL = NameTileURL.Replace("[17-Z]", Conversions.ToString(17.0 - Conversions.ToDouble(ZOOM)));
			NameTileURL = NameTileURL.Replace("[Z+1]", Conversions.ToString(Conversions.ToDouble(ZOOM) + 1.0));
			NameTileURL = NameTileURL.Replace("[FX]", Conversions.ToString(num));
			NameTileURL = NameTileURL.Replace("[FY]", Conversions.ToString(num2));
			NameTileURL = NameTileURL.Replace("[Gagarin.Substring(0,RND(0,8))]", "Gagarin".Substring(0, RND.Next(0, 8)));
			if (Strings.InStr(NameTileURL, "[RES]") > 0)
			{
				string text = "";
				long num3 = (long)Math.Floor(Math.Pow(2.0, Conversions.ToDouble(ZOOM)) / 2.0);
				long num4 = (long)Math.Floor(Math.Pow(2.0, Conversions.ToDouble(ZOOM)) / 2.0);
				long num5 = num3;
				long num6 = num4;
				int num7 = Conversions.ToInteger(ZOOM);
				for (int i = 1; i <= num7; i++)
				{
					num5 = (long)Math.Floor((double)num5 / 2.0);
					num6 = (long)Math.Floor((double)num6 / 2.0);
					if (X < num3)
					{
						num3 -= num5;
						if (Y < num4)
						{
							num4 -= num6;
							text += "0";
						}
						else
						{
							num4 += num6;
							text += "2";
						}
					}
					else
					{
						num3 += num5;
						if (Y < num4)
						{
							num4 -= num6;
							text += "1";
						}
						else
						{
							num4 += num6;
							text += "3";
						}
					}
				}
				NameTileURL = UrlTileServer.Replace("[RES]", text);
			}
			if (Strings.InStr(NameTileURL, "[RND(a,b,c)]") > 0)
			{
				int num8 = RND.Next(1, 4);
				NameTileURL = NameTileURL.Replace("[RND(a,b,c)]", num8 switch
				{
					1 => "a", 
					2 => "b", 
					_ => "c", 
				});
			}
		}
	}

	public static void CoordToTile(byte PROJECTION, double SHIR, double DOLG, int ZOOM, ref int TileX, ref int TileY, ref int PicX, ref int PicY)
	{
		float num = (float)(SHIR * Constants.PI_180);
		checked
		{
			long num2 = (long)Math.Round(Math.Pow(2.0, ZOOM));
			double num3 = (double)num2 * (DOLG + 180.0) / 360.0;
			double num6;
			if (PROJECTION == 2)
			{
				double num4 = 6378137.0;
				double num5 = 0.0818191908426;
				double d = Math.Tan(Math.PI / 4.0 + (double)(num / 2f)) / Math.Pow(Math.Tan(Math.PI / 4.0 + Math.Asin(num5 * Math.Sin(num)) / 2.0), num5);
				num6 = (20037508.342789 - num4 * Math.Log(d)) * 53.5865938 / Math.Pow(2.0, 23 - ZOOM) / 256.0;
			}
			else
			{
				num6 = 0.5 * (double)num2 * (1.0 - Math.Log(Math.Tan(num) + 1.0 / Math.Cos(num)) / Math.PI);
			}
			TileX = (int)Conversion.Int(num3);
			TileY = (int)Conversion.Int(num6);
			PicX = (int)Math.Round((num3 - (double)TileX) * 256.0);
			PicY = (int)Math.Round((num6 - (double)TileY) * 256.0);
		}
	}

	public static void CoordToTileNew(byte PROJECTION, double SHIR, double DOLG, int ZOOM, ref double TileX, ref double TileY)
	{
		double num = SHIR * Constants.PI_180;
		checked
		{
			long num2 = (long)Math.Round(Math.Pow(2.0, ZOOM));
			TileX = (double)num2 * (DOLG + 180.0) / 360.0;
			if (PROJECTION == 2)
			{
				double num3 = 6378137.0;
				double num4 = 0.0818191908426;
				double d = Math.Tan(Math.PI / 4.0 + num / 2.0) / Math.Pow(Math.Tan(Math.PI / 4.0 + Math.Asin(num4 * Math.Sin(num)) / 2.0), num4);
				TileY = (20037508.342789 - num3 * Math.Log(d)) * 53.5865938 / Math.Pow(2.0, 23 - ZOOM) / 256.0;
			}
			else
			{
				TileY = 0.5 * (double)num2 * (1.0 - Math.Log(Math.Tan(num) + 1.0 / Math.Cos(num)) / Math.PI);
			}
		}
	}

	public static void TileToCoord(byte PROJECTION, int TileX, int TileY, int ZOOM, ref double ShirDeg, ref double DolgDeg, ref double ShirShag, ref double DolgShag)
	{
		checked
		{
			long num = (long)Math.Round(Math.Pow(2.0, ZOOM));
			if (PROJECTION == 2)
			{
				double num2 = TileY * 256;
				double num3 = 6378137.0;
				double num4 = 0.00335655146887969;
				double num5 = 6.57187271079536E-06;
				double num6 = 1.764564338702E-08;
				double num7 = 5.328478445E-11;
				double num8 = 20037508.342789 - num2 * Math.Pow(2.0, 23 - ZOOM) / 53.5865938;
				double num9 = Math.PI / 2.0 - 2.0 * Math.Atan(1.0 / Math.Exp(num8 / num3));
				double num10 = num9 + num4 * Math.Sin(2.0 * num9) + num5 * Math.Sin(4.0 * num9) + num6 * Math.Sin(6.0 * num9) + num7 * Math.Sin(8.0 * num9);
				ShirDeg = num10 * (180.0 / Math.PI);
			}
			else
			{
				ShirDeg = Math.Atan(Math.Sinh(Math.PI * (1.0 - (double)(2 * TileY) / (double)num))) * (180.0 / Math.PI);
			}
			DolgDeg = (double)TileX / (double)num * 360.0 - 180.0;
			DolgShag = 360.0 / (double)num / 256.0;
			ShirShag = (Math.Atan(Math.Sinh(Math.PI * (1.0 - (double)(2 * (TileY + 1)) / (double)num))) * (180.0 / Math.PI) - Math.Atan(Math.Sinh(Math.PI * (1.0 - (double)(2 * TileY) / (double)num))) * (180.0 / Math.PI)) / 256.0;
		}
	}

	public static void TileToCoordNEW(byte PROJECTION, double TileX, double TileY, int ZOOM, ref double ShirDeg, ref double DolgDeg)
	{
		checked
		{
			long num = (long)Math.Round(Math.Pow(2.0, ZOOM));
			if (PROJECTION == 2)
			{
				double num2 = TileY * 256.0;
				double num3 = 6378137.0;
				double num4 = 0.00335655146887969;
				double num5 = 6.57187271079536E-06;
				double num6 = 1.764564338702E-08;
				double num7 = 5.328478445E-11;
				double num8 = 20037508.342789 - num2 * Math.Pow(2.0, 23 - ZOOM) / 53.5865938;
				double num9 = Math.PI / 2.0 - 2.0 * Math.Atan(1.0 / Math.Exp(num8 / num3));
				double num10 = num9 + num4 * Math.Sin(2.0 * num9) + num5 * Math.Sin(4.0 * num9) + num6 * Math.Sin(6.0 * num9) + num7 * Math.Sin(8.0 * num9);
				ShirDeg = num10 * (180.0 / Math.PI);
			}
			else
			{
				ShirDeg = Math.Atan(Math.Sinh(Math.PI * (1.0 - 2.0 * TileY / (double)num))) * (180.0 / Math.PI);
			}
			DolgDeg = TileX / (double)num * 360.0 - 180.0;
		}
	}

	public static float CalcShirMAP(byte PROJECTION, int TileX, int TileY, int ZOOM)
	{
		checked
		{
			long num = (long)Math.Round(Math.Pow(2.0, ZOOM));
			if (PROJECTION == 2)
			{
				double num2 = TileY * 256;
				double num3 = 6378137.0;
				double num4 = 0.00335655146887969;
				double num5 = 6.57187271079536E-06;
				double num6 = 1.764564338702E-08;
				double num7 = 5.328478445E-11;
				double num8 = 20037508.342789 - num2 * Math.Pow(2.0, 23 - ZOOM) / 53.5865938;
				double num9 = Math.PI / 2.0 - 2.0 * Math.Atan(1.0 / Math.Exp(num8 / num3));
				return (float)((num9 + num4 * Math.Sin(2.0 * num9) + num5 * Math.Sin(4.0 * num9) + num6 * Math.Sin(6.0 * num9) + num7 * Math.Sin(8.0 * num9)) * 180.0 / Math.PI);
			}
			return (float)(Math.Atan(Math.Sinh(Math.PI * (1.0 - (double)(2 * TileY) / (double)num))) * 180.0 / Math.PI);
		}
	}

	public static void CalcYandexToGoogleTileY(short Z, int Y2, ref int Y1tile, ref double Y1pix)
	{
		double ShirDeg = default;
		double DolgDeg = default;
		TileToCoordNEW(2, 0.0, Y2, Z, ref ShirDeg, ref DolgDeg);
		double TileX = default;
		double TileY = default;
		CoordToTileNew(1, ShirDeg, 0.0, Z, ref TileX, ref TileY);
		Y1tile = checked((int)Math.Floor(TileY));
		Y1pix = (TileY - (double)Y1tile) * 256.0;
	}

	public static float KoefYandex(short Z, int Y1)
	{
		double ShirDeg = default;
		double DolgDeg = default;
		TileToCoordNEW(1, 0.0, Y1, Z, ref ShirDeg, ref DolgDeg);
		double ShirDeg2 = default;
		double DolgDeg2 = default;
		TileToCoordNEW(1, 0.0, checked(Y1 + 1), Z, ref ShirDeg2, ref DolgDeg2);
		double sHIR = ShirDeg;
		double dOLG = DolgDeg;
		short zOOM = Z;
		float num = default;
		double TileX = num;
		float num2 = default;
		double TileY = num2;
		CoordToTileNew(2, sHIR, dOLG, zOOM, ref TileX, ref TileY);
		num2 = (float)TileY;
		num = (float)TileX;
		double sHIR2 = ShirDeg2;
		double dOLG2 = DolgDeg2;
		short zOOM2 = Z;
		TileY = num;
		float num3 = default;
		TileX = num3;
		CoordToTileNew(2, sHIR2, dOLG2, zOOM2, ref TileY, ref TileX);
		num3 = (float)TileX;
		num = (float)TileY;
		return (num3 - num2) * 256f;
	}

	public static void CalcYandexToGoogleCursor(short Z, double Y2, ref double Y1)
	{
		double ShirDeg = default;
		double DolgDeg = default;
		TileToCoordNEW(2, 0.0, Y2, Z, ref ShirDeg, ref DolgDeg);
		double TileX = default;
		CoordToTileNew(1, ShirDeg, 0.0, Z, ref TileX, ref Y1);
	}

	public static void CalcY_YandexToGoogle(short Z, double Y2, ref double Y1)
	{
		checked
		{
			int num = (int)Math.Round(Math.Pow(2.0, Z));
			double num2 = 20037508.342789 - Y2 * 256.0 * Math.Pow(2.0, 23 - Z) / 53.5865938;
			double num3 = Math.PI / 2.0 - 2.0 * Math.Atan(1.0 / Math.Exp(num2 / 6378137.0));
			double num4 = num3 + 0.00335655146887969 * Math.Sin(2.0 * num3) + 6.57187271079536E-06 * Math.Sin(4.0 * num3) + 1.764564338702E-08 * Math.Sin(6.0 * num3) + 5.328478445E-11 * Math.Sin(8.0 * num3);
			Y1 = 0.5 * (double)num * (1.0 - Math.Log(Math.Tan(num4) + 1.0 / Math.Cos(num4)) / Math.PI);
		}
	}

	public static void CalcY_GoogleToYandex(short Z, double Y1, ref double Y2)
	{
		checked
		{
			int num = (int)Math.Round(Math.Pow(2.0, Z));
			double num2 = Math.Atan(Math.Sinh(Math.PI * (1.0 - 2.0 * Y1 / (double)num)));
			double d = Math.Tan(Math.PI / 4.0 + num2 / 2.0) / Math.Pow(Math.Tan(Math.PI / 4.0 + Math.Asin(0.0818191908426 * Math.Sin(num2)) / 2.0), 0.0818191908426);
			Y2 = (20037508.342789 - 6378137.0 * Math.Log(d)) * 53.5865938 / Math.Pow(2.0, 23 - Z) / 256.0;
		}
	}

	public static void DownloadOneTile(int item)
	{
		if (MyProject.Computer.FileSystem.FileExists(ListTiles[item].FileName))
		{
			return;
		}
		if (!MyProject.Computer.FileSystem.DirectoryExists(ListTiles[item].FolderName))
		{
			Directory.CreateDirectory(ListTiles[item].FolderName);
		}
		if (Operators.CompareString(ListTiles[item].UrlName, "SITPLAN", TextCompare: false) != 0)
		{
			MyWebClient myWebClient = new MyWebClient();
			myWebClient.timeout = 60000;
			myWebClient.UseDefaultCredentials = true;
			WebProxy webProxy = new WebProxy();
			if (Public2.AppSetup.FlagProxy)
			{
				if (Public2.AppSetup.FlagProxyLogin)
				{
					webProxy.UseDefaultCredentials = false;
					webProxy.Credentials = new NetworkCredential(Public2.AppSetup.ProxyLogin, Public2.AppSetup.ProxyPassword);
				}
				else
				{
					webProxy.UseDefaultCredentials = true;
				}
				Uri address = new Uri("http://" + Public2.AppSetup.ProxyIP + ":" + Conversions.ToString(Public2.AppSetup.ProxyPort));
				webProxy.Address = address;
				myWebClient.Proxy = webProxy;
			}
			else
			{
				myWebClient.Proxy = WebRequest.GetSystemWebProxy();
			}
			myWebClient.Headers.Add("User-Agent", "Referer: openstreetmap");
			try
			{
				myWebClient.DownloadFile(ListTiles[item].UrlName, ListTiles[item].FileName);
			}
			catch (Exception projectError)
			{
				ProjectData.SetProjectError(projectError);
				myWebClient.Dispose();
				try
				{
					File.Delete(ListTiles[item].FileName);
				}
				catch (Exception projectError2)
				{
					ProjectData.SetProjectError(projectError2);
					ProjectData.ClearProjectError();
				}
				ProjectData.ClearProjectError();
				return;
			}
			myWebClient.Dispose();
		}
		else
		{
			CropeOneTileFromRaster(ListTiles[item].X, ListTiles[item].Y, ListTiles[item].Z);
		}
	}

	public static void CropeOneTileFromRaster(int X, int Y, int Z)
	{
		if (Information.IsNothing(Public2.PRJ.RPL.BTM))
		{
			return;
		}
		Public2.CoordPix coordPix = default;
		Public2.CoordPix coordPix2 = default;
		if (Public2.PRJ.RPL.PointRaster[1].Xpix < Public2.PRJ.RPL.PointRaster[2].Xpix)
		{
			coordPix = Public2.PRJ.RPL.PointRaster[1];
			coordPix2 = Public2.PRJ.RPL.PointRaster[2];
		}
		else if (Public2.PRJ.RPL.PointRaster[1].Xpix > Public2.PRJ.RPL.PointRaster[2].Xpix)
		{
			coordPix = Public2.PRJ.RPL.PointRaster[2];
			coordPix2 = Public2.PRJ.RPL.PointRaster[1];
		}
		checked
		{
			if (Public2.PRJ.RPL.UseScalingLine)
			{
				Public2.CoordWebMerc coordWM = default;
				Public2.CoordWebMerc coordWM2 = default;
				if (Public2.PRJ.RPL.PointMap[1].Xwm < Public2.PRJ.RPL.PointMap[2].Xwm)
				{
					coordWM = Public2.PRJ.RPL.PointMap[1];
					coordWM2 = Public2.PRJ.RPL.PointMap[2];
				}
				else if (Public2.PRJ.RPL.PointMap[2].Xwm < Public2.PRJ.RPL.PointMap[1].Xwm)
				{
					coordWM = Public2.PRJ.RPL.PointMap[2];
					coordWM2 = Public2.PRJ.RPL.PointMap[1];
				}
				double num = Math.Sqrt(Math.Pow((short)unchecked(coordPix2.Xpix - coordPix.Xpix), 2.0) + Math.Pow((short)unchecked(coordPix2.Ypix - coordPix.Ypix), 2.0));
				double Xt = default;
				double Yt = default;
				CalcCoords.ConvWebMercMetersToTile(coordWM, (byte)Z, ref Xt, ref Yt);
				double Xt2 = default;
				double Yt2 = default;
				CalcCoords.ConvWebMercMetersToTile(coordWM2, (byte)Z, ref Xt2, ref Yt2);
				double num2 = Math.Sqrt(Math.Pow((Xt2 - Xt) * 256.0, 2.0) + Math.Pow((Yt2 - Yt) * 256.0, 2.0)) / num;
				int num3 = (int)Math.Floor(Xt - (double)coordPix.Xpix * num2 / 256.0);
				int num4 = (int)Math.Floor(Yt - (double)coordPix.Ypix * num2 / 256.0);
				int num5 = (int)Math.Round(((double)num3 - (Xt - (double)coordPix.Xpix * num2 / 256.0)) * 256.0 / num2);
				int num6 = (int)Math.Round(((double)num4 - (Yt - (double)coordPix.Ypix * num2 / 256.0)) * 256.0 / num2);
				int num7 = (int)Math.Round(256.0 / num2);
				int num8 = (int)Math.Round((double)num5 + (double)((X - num3) * 256) / num2);
				int num9 = (int)Math.Round((double)num6 + (double)((Y - num4) * 256) / num2);
				Bitmap bitmap = new Bitmap(256, 256);
				object countLock = CountLock;
				ObjectFlowControl.CheckForSyncLockOnValueType(countLock);
				bool lockTaken = false;
				try
				{
					Monitor.Enter(countLock, ref lockTaken);
					if (!((num8 + num7 < 0) | (num9 + num7 < 0) | (num8 > Public2.PRJ.RPL.BTM.Width) | (num9 > Public2.PRJ.RPL.BTM.Height)))
					{
						Graphics.FromImage(bitmap).DrawImage(srcRect: new RectangleF(num8, num9, num7, num7), destRect: new RectangleF(0f, 0f, 256f, 256f), image: Public2.PRJ.RPL.BTM, srcUnit: GraphicsUnit.Pixel);
					}
				}
				finally
				{
					if (lockTaken)
					{
						Monitor.Exit(countLock);
					}
				}
				int num10 = (int)Conversion.Int((double)X / 1024.0);
				int num11 = (int)Conversion.Int((double)Y / 1024.0);
				string filename = Public2.AppSetup.FolderCach + "\\SitPlan\\" + Conversions.ToString(Public2.PRJ.SETT.GUID) + "\\z" + Conversions.ToString(Z + 1) + "\\" + Conversions.ToString(num10) + "\\x" + Conversions.ToString(X) + "\\" + Conversions.ToString(num11) + "\\y" + Conversions.ToString(Y) + ".png";
				string text = Public2.AppSetup.FolderCach + "\\SitPlan\\" + Conversions.ToString(Public2.PRJ.SETT.GUID) + "\\z" + Conversions.ToString(Z + 1) + "\\" + Conversions.ToString(num10) + "\\x" + Conversions.ToString(X) + "\\" + Conversions.ToString(num11);
				if (!MyProject.Computer.FileSystem.DirectoryExists(text))
				{
					Directory.CreateDirectory(text);
				}
				bitmap.Save(filename, ImageFormat.Png);
			}
			else
			{
				double num12 = Math.Sqrt(Math.Pow((short)unchecked(coordPix2.Xpix - coordPix.Xpix), 2.0) + Math.Pow((short)unchecked(coordPix2.Ypix - coordPix.Ypix), 2.0));
				double Xt3 = default;
				double Yt3 = default;
				CalcCoords.ConvWebMercMetersToTile(Public2.PRJ.RPL.PointMap[0], (byte)Z, ref Xt3, ref Yt3);
				double num13 = (double)Public2.PRJ.RPL.Length / num12;
				double num14 = CalcCoords.CalcMashtMeterstoPixel(Public2.PRJ.RPL.PointMap[0]);
				double num15 = num13 * num14;
				int num16 = (int)Math.Floor(Xt3 - (double)Public2.PRJ.RPL.PointRaster[0].Xpix * num15 / 256.0);
				int num17 = (int)Math.Floor(Yt3 - (double)Public2.PRJ.RPL.PointRaster[0].Ypix * num15 / 256.0);
				int num18 = (int)Math.Round(((double)num16 - (Xt3 - (double)Public2.PRJ.RPL.PointRaster[0].Xpix * num15 / 256.0)) * 256.0 / num15);
				int num19 = (int)Math.Round(((double)num17 - (Yt3 - (double)Public2.PRJ.RPL.PointRaster[0].Ypix * num15 / 256.0)) * 256.0 / num15);
				int num20 = (int)Math.Round(256.0 / num15);
				int num21 = (int)Math.Round((double)num18 + (double)((X - num16) * 256) / num15);
				int num22 = (int)Math.Round((double)num19 + (double)((Y - num17) * 256) / num15);
				Bitmap bitmap2 = new Bitmap(256, 256);
				object countLock2 = CountLock;
				ObjectFlowControl.CheckForSyncLockOnValueType(countLock2);
				bool lockTaken2 = false;
				try
				{
					Monitor.Enter(countLock2, ref lockTaken2);
					if (!((num21 + num20 < 0) | (num22 + num20 < 0) | (num21 > Public2.PRJ.RPL.BTM.Width) | (num22 > Public2.PRJ.RPL.BTM.Height)))
					{
						Graphics.FromImage(bitmap2).DrawImage(srcRect: new RectangleF(num21, num22, num20, num20), destRect: new RectangleF(0f, 0f, 256f, 256f), image: Public2.PRJ.RPL.BTM, srcUnit: GraphicsUnit.Pixel);
					}
				}
				finally
				{
					if (lockTaken2)
					{
						Monitor.Exit(countLock2);
					}
				}
				int num23 = (int)Conversion.Int((double)X / 1024.0);
				int num24 = (int)Conversion.Int((double)Y / 1024.0);
				string filename2 = Public2.AppSetup.FolderCach + "\\SitPlan\\" + Conversions.ToString(Public2.PRJ.SETT.GUID) + "\\z" + Conversions.ToString(Z + 1) + "\\" + Conversions.ToString(num23) + "\\x" + Conversions.ToString(X) + "\\" + Conversions.ToString(num24) + "\\y" + Conversions.ToString(Y) + ".png";
				string text2 = Public2.AppSetup.FolderCach + "\\SitPlan\\" + Conversions.ToString(Public2.PRJ.SETT.GUID) + "\\z" + Conversions.ToString(Z + 1) + "\\" + Conversions.ToString(num23) + "\\x" + Conversions.ToString(X) + "\\" + Conversions.ToString(num24);
				if (!MyProject.Computer.FileSystem.DirectoryExists(text2))
				{
					Directory.CreateDirectory(text2);
				}
				bitmap2.Save(filename2, ImageFormat.Png);
			}
			GC.Collect();
		}
	}
}
