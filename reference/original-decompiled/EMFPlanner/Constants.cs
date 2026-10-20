using System;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

[StandardModule]
internal sealed class Constants
{
	public const double Pi2 = Math.PI * 2.0;

	public const double Pi_2 = Math.PI / 2.0;

	public const double PI180 = 180.0 / Math.PI;

	public static int SecInYear = 31536000;

	public static int SecInMonth = checked((int)Math.Round((double)SecInYear / 12.0));

	public static double MainDivCableLoss = 1024.0;

	public static double PI_180 = Math.PI / 180.0;

	public static string UrlROOT = "http://data1.mlinkplanner.com";
}
