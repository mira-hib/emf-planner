using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using EMFPlanner.My;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[StandardModule]
internal sealed class UpdateMessage
{
	public static string AppCurrentVersion;

	public static string AppAvailableVersion;

	public static string AppLastShownVersion;

	public static void LoadUpdateMessage1()
	{
		try
		{
			WebClient webClient = new WebClient();
			webClient.UseDefaultCredentials = true;
			new WebProxy();
			webClient.Proxy = WebRequest.GetSystemWebProxy();
			webClient.DownloadFile("http://data1.mlinkplanner.com/UPDATE/EMF.txt", Public2.AppDataFolder + "\\AvailableVersion.txt");
			StreamReader streamReader = new StreamReader(Public2.AppDataFolder + "\\AvailableVersion.txt", Encoding.GetEncoding(1251));
			AppAvailableVersion = Strings.Trim(streamReader.ReadLine());
			streamReader.Close();
			StreamReader streamReader2 = new StreamReader(Application.StartupPath + "\\CurrentVersion.txt", Encoding.GetEncoding(1251));
			AppCurrentVersion = Strings.Trim(streamReader2.ReadLine());
			streamReader2.Close();
			if (Operators.CompareString(AppAvailableVersion, AppCurrentVersion, TextCompare: false) <= 0)
			{
				Interaction.MsgBox(Localization.LocMSG(106) + " " + AppCurrentVersion, MsgBoxStyle.Information);
				return;
			}
			webClient.DownloadFile("http://data1.mlinkplanner.com/UPDATE/EMFHistory.rtf", Public2.AppDataFolder + "\\History.rtf");
			MyProject.Forms.frmUpdateMessage.ShowDialog();
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			Interaction.MsgBox(Localization.LocMSG(107), MsgBoxStyle.Exclamation);
			ProjectData.ClearProjectError();
		}
	}

	public static void StartUpdateMessage()
	{
		new Thread(LoadUpdateMessage2).Start();
	}

	public static void LoadUpdateMessage2()
	{
		try
		{
			WebClient webClient = new WebClient();
			webClient.UseDefaultCredentials = true;
			new WebProxy();
			webClient.Proxy = WebRequest.GetSystemWebProxy();
			webClient.DownloadFile("http://data1.mlinkplanner.com/UPDATE/EMF.txt", Public2.AppDataFolder + "\\AvailableVersion.txt");
			StreamReader streamReader = new StreamReader(Public2.AppDataFolder + "\\AvailableVersion.txt", Encoding.GetEncoding(1251));
			AppAvailableVersion = Strings.Trim(streamReader.ReadLine());
			streamReader.Close();
			StreamReader streamReader2 = new StreamReader(Application.StartupPath + "\\CurrentVersion.txt", Encoding.GetEncoding(1251));
			AppCurrentVersion = Strings.Trim(streamReader2.ReadLine());
			streamReader2.Close();
			if (MyProject.Computer.FileSystem.FileExists(Public2.AppDataFolder + "\\LastShownVersion.txt"))
			{
				StreamReader streamReader3 = new StreamReader(Public2.AppDataFolder + "\\LastShownVersion.txt", Encoding.GetEncoding(1251));
				AppLastShownVersion = streamReader3.ReadLine();
				streamReader3.Close();
				if ((Operators.CompareString(AppAvailableVersion, AppCurrentVersion, TextCompare: false) > 0) & (Operators.CompareString(AppAvailableVersion, AppLastShownVersion, TextCompare: false) > 0))
				{
					webClient.DownloadFile("http://data1.mlinkplanner.com/UPDATE/EMFHistory.rtf", Public2.AppDataFolder + "\\History.rtf");
					MyProject.Forms.frmUpdateMessage.ShowDialog(MyProject.Forms.Form1);
					StreamWriter streamWriter = new StreamWriter(Public2.AppDataFolder + "\\LastShownVersion.txt", append: false, Encoding.GetEncoding(1251));
					streamWriter.WriteLine(AppAvailableVersion);
					streamWriter.Close();
				}
			}
			else
			{
				if (Operators.CompareString(AppAvailableVersion, AppCurrentVersion, TextCompare: false) > 0)
				{
					webClient.DownloadFile("http://data1.mlinkplanner.com/UPDATE/EMFHistory.rtf", Public2.AppDataFolder + "\\History.rtf");
					MyProject.Forms.frmUpdateMessage.ShowDialog(MyProject.Forms.Form1);
				}
				StreamWriter streamWriter2 = new StreamWriter(Public2.AppDataFolder + "\\LastShownVersion.txt", append: false, Encoding.GetEncoding(1251));
				streamWriter2.WriteLine(AppAvailableVersion);
				streamWriter2.Close();
			}
		}
		catch (Exception projectError)
		{
			ProjectData.SetProjectError(projectError);
			ProjectData.ClearProjectError();
		}
	}
}
