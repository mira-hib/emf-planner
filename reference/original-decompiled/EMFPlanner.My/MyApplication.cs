using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner.My;

[GeneratedCode("MyTemplate", "11.0.0.0")]
[EditorBrowsable(EditorBrowsableState.Never)]
internal class MyApplication : WindowsFormsApplicationBase
{
	[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
	[STAThread]
	[DebuggerHidden]
	[EditorBrowsable(EditorBrowsableState.Advanced)]
	internal static void Main(string[] Args)
	{
		Application.SetCompatibleTextRenderingDefault(WindowsFormsApplicationBase.UseCompatibleTextRendering);
		MyProject.Application.Run(Args);
	}

	private void MyApplication_Startup(object sender, StartupEventArgs e)
	{
		ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
		ServicePointManager.ServerCertificateValidationCallback = [SpecialName] (object se, X509Certificate cert, X509Chain chain, SslPolicyErrors sslerror) => true;
		if (!MyProject.Computer.FileSystem.DirectoryExists(Public2.AppDataFolder))
		{
			Directory.CreateDirectory(Public2.AppDataFolder);
			Directory.CreateDirectory(Public2.AppDataFolder + "\\cache");
		}
		if (!MyProject.Computer.FileSystem.FileExists(Public2.AppDataFolder + "\\CustomBaseMaps.set"))
		{
			File.Copy(Public2.AppStartupPath + "\\CustomBaseMaps.set", Public2.AppDataFolder + "\\CustomBaseMaps.set", overwrite: false);
		}
		Public2.AppSetup.FolderCach = Public2.AppDataFolder + "\\cache";
		Public2.AppSetup.NameTileServer = "OpenStreetMap";
		Public2.AppSetup.Projection = 1;
		Public2.AppSetup.ZOOM = 18;
		Public2.AppSetup.TileX = 141593;
		Public2.AppSetup.TileY = 88796;
		Public2.AppSetup.PointX = 0;
		Public2.AppSetup.PointY = 0;
		Public2.AppSetup.FlagProxy = false;
		Public2.AppSetup.ProxyIP = "80.255.145.41";
		Public2.AppSetup.ProxyPort = 3128;
		Public2.AppSetup.FlagProxyLogin = false;
		Public2.AppSetup.ProxyLogin = "";
		Public2.AppSetup.ProxyPassword = "";
		Public2.AppSetup.OwnerNameList = new string[5] { "Vodafone", "Orange", "Deutsche Telekom", "Swisscom", "Telecom Italia" };
		Public2.AppSetup.OwnerColorList = new Color[5]
		{
			Color.LightCoral,
			Color.LightGreen,
			Color.Yellow,
			Color.LightSeaGreen,
			Color.LightSkyBlue
		};
		Public2.SetPrtoOrder();
		Public2.SetLimitsAPP();
		try
		{
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			using Stream serializationStream = File.Open(Public2.AppDataFolder + "\\setup.tmp", FileMode.Open);
			object obj = binaryFormatter.Deserialize(serializationStream);
			Public2.AppSetup = ((obj != null) ? ((Public2.GlobalSetup)obj) : default(Public2.GlobalSetup));
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
		if (Information.IsNothing(Public2.AppSetup.FolderCach) | (Operators.CompareString(Public2.AppSetup.FolderCach, "", TextCompare: false) == 0) | (Public2.AppSetup.ZOOM == 0))
		{
			Public2.AppSetup.FolderCach = Public2.AppDataFolder + "\\cache";
			Public2.AppSetup.NameTileServer = "OpenStreetMap";
			Public2.AppSetup.Projection = 1;
			Public2.AppSetup.ZOOM = 18;
			Public2.AppSetup.TileX = 141593;
			Public2.AppSetup.TileY = 88796;
			Public2.AppSetup.PointX = 0;
			Public2.AppSetup.PointY = 0;
			Public2.AppSetup.FlagProxy = false;
			Public2.AppSetup.ProxyIP = "80.255.145.41";
			Public2.AppSetup.ProxyPort = 3128;
			Public2.AppSetup.FlagProxyLogin = false;
			Public2.AppSetup.ProxyLogin = "";
			Public2.AppSetup.ProxyPassword = "";
			Public2.AppSetup.OwnerNameList = new string[5] { "Vodafone", "Orange", "Deutsche Telekom", "Swisscom", "Telecom Italia" };
			Public2.AppSetup.OwnerColorList = new Color[5]
			{
				Color.LightCoral,
				Color.LightGreen,
				Color.Yellow,
				Color.LightSeaGreen,
				Color.LightSkyBlue
			};
			Public2.SetPrtoOrder();
			Public2.SetLimitsAPP();
		}
		bool createdNew = false;
		Module1.TestMutex = new Mutex(initiallyOwned: true, "EMFPlannerMutex", out createdNew);
		if (createdNew)
		{
			try
			{
				Directory.Delete(Public2.AppDataFolder + "\\TMP", recursive: true);
			}
			catch (Exception projectError2)
			{
				ProjectData.SetProjectError(projectError2);
				ProjectData.ClearProjectError();
			}
		}
		if (!MyProject.Computer.FileSystem.DirectoryExists(Public2.AppDataFolder + "\\TMP"))
		{
			Directory.CreateDirectory(Public2.AppDataFolder + "\\TMP");
		}
		Module1.FolderTMP = Public2.AppDataFolder + "\\TMP\\" + Conversions.ToString(DateAndTime.Now.Ticks);
		Directory.CreateDirectory(Module1.FolderTMP);
		Public2.NewPrj();
		Public1.InitialPenDash();
		Form1_Public.FillFontList();
		checked
		{
			try
			{
				StreamReader streamReader = new StreamReader(Public2.AppStartupPath + "\\feeders.txt", Encoding.GetEncoding(1251));
				short num = -1;
				short num2 = 0;
				Public1.FID = new Public1.FIDER[1];
				while (!streamReader.EndOfStream)
				{
					string text = streamReader.ReadLine();
					if (Operators.CompareString(text, "-", TextCompare: false) == 0)
					{
						num++;
						Public1.FID = (Public1.FIDER[])Utils.CopyArray(Public1.FID, new Public1.FIDER[num + 1]);
						Public1.FID[num].Name = streamReader.ReadLine();
						num2 = 0;
					}
					else
					{
						ref float[,] loss = ref Public1.FID[num].Loss;
						loss = (float[,])Utils.CopyArray(loss, new float[2, num2 + 1]);
						Public1.FID[num].Loss[0, num2] = Conversions.ToSingle(Module1.TchkToZpt(text.Substring(0, Strings.InStr(text, "\t"))));
						Public1.FID[num].Loss[1, num2] = Conversions.ToSingle(Module1.TchkToZpt(text.Substring(Strings.InStr(text, "\t"))));
						num2++;
					}
				}
				streamReader.Close();
			}
			catch (Exception ex)
			{
				ProjectData.SetProjectError(ex);
				Exception ex2 = ex;
				Interaction.MsgBox(Localization.LocMSG(102));
				ProjectData.ClearProjectError();
			}
			try
			{
				Interaction.MsgBox("You have " + Conversions.ToString(Enigma_IDE64.EP_TrialDaysLeft()) + " days left in your free trial!");
			}
			catch (Exception projectError3)
			{
				ProjectData.SetProjectError(projectError3);
				ProjectData.ClearProjectError();
			}
		}
	}

	private void MyApplication_Shutdown(object sender, EventArgs e)
	{
		Public2.AppSetup.NameTileServer = Public2.PRJ.SETT.NameTileServer;
		Public2.AppSetup.ZOOM = Public2.PRJ.SETT.ZOOM;
		Public2.AppSetup.Projection = Public2.PRJ.SETT.Projection;
		Public2.AppSetup.OriginCoordWM = Module1.DeepClone(Public2.PRJ.SETT.OriginCoordWM);
		Public2.AppSetup.OriginCoordWGS = Module1.DeepClone(Public2.PRJ.SETT.OriginCoordWGS);
		Public2.AppSetup.PRTOCoordWGS = Module1.DeepClone(Public2.PRJ.SETT.PRTOCoordWGS);
		Public2.AppSetup.HorAreaCoordWM = Module1.DeepClone(Public2.PRJ.SETT.HorAreaCoordWM);
		Public2.AppSetup.HorAreaCoordM = Module1.DeepClone(Public2.PRJ.SETT.HorAreaCoordM);
		Public2.AppSetup.LMT = Module1.DeepClone(Public2.PRJ.SETT.LMT);
		Public2.AppSetup.LimitDescription = Module1.DeepClone(Public2.PRJ.SETT.LimitDescription);
		Public2.AppSetup.LimitColor = Module1.DeepClone(Public2.PRJ.SETT.LimitColor);
		Public2.AppSetup.LimitAlpha24 = Module1.DeepClone(Public2.PRJ.SETT.LimitAlpha24);
		Public2.AppSetup.LimitFtdc = Module1.DeepClone(Public2.PRJ.SETT.LimitFtdc);
		Public2.AppSetup.LimitFpr = Module1.DeepClone(Public2.PRJ.SETT.LimitFpr);
		BinaryFormatter binaryFormatter = new BinaryFormatter();
		using Stream serializationStream = File.Create(Public2.AppDataFolder + "\\setup.tmp");
		binaryFormatter.Serialize(serializationStream, Public2.AppSetup);
	}

	[DebuggerStepThrough]
	public MyApplication()
		: base(AuthenticationMode.Windows)
	{
		Startup += MyApplication_Startup;
		Shutdown += MyApplication_Shutdown;
		IsSingleInstance = false;
		EnableVisualStyles = true;
		SaveMySettingsOnExit = true;
		ShutdownStyle = ShutdownMode.AfterMainFormCloses;
	}

	[DebuggerStepThrough]
	protected override void OnCreateMainForm()
	{
		MainForm = MyProject.Forms.Form1;
	}
}
