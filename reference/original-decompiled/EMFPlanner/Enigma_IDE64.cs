using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualBasic.CompilerServices;

namespace EMFPlanner;

public class Enigma_IDE64
{
	public struct TKeyCountries
	{
		public string Name;

		public int Code;

		public TKeyCountries(string _Name, int _Code)
		{
			this = default;
			Name = _Name;
			Code = _Code;
		}
	}

	public struct TWMContent
	{
		public int WMType;

		public string Name;

		public int NameLen;

		public string Text;

		public int TextLen;

		public string FileName;

		public int FileNameLen;

		public string AFile;

		public int AFileLen;
	}

	public struct TKeyInformation
	{
		public bool Stolen;

		public int CreationYear;

		public int CreationMonth;

		public int CreationDay;

		public bool UseKeyExpiration;

		public int ExpirationYear;

		public int ExpirationMonth;

		public int ExpirationDay;

		public bool UseHardwareLocking;

		public bool UseExecutionsLimit;

		public int ExecutionsCount;

		public bool UseDaysLimit;

		public int DaysCount;

		public bool UseRunTimeLimit;

		public int RunTimeMinutes;

		public bool UseGlobalTimeLimit;

		public int GlobalTimeMinutes;

		public bool UseCountyLimit;

		public int CountryCode;

		public bool UseRegisterAfter;

		public int RegisterAfterYear;

		public int RegisterAfterMonth;

		public int RegisterAfterDay;

		public bool UseRegisterBefore;

		public int RegisterBeforeYear;

		public int RegisterBeforeMonth;

		public int RegisterBeforeDay;

		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
		public bool[] EncryptedSections;
	}

	public const int WM_PUBLIC = 1;

	public const int WM_PRIVATE = 2;

	public const int NUMBER_OF_CRYPTED_SECTIONS = 16;

	public const int HASH_XOR32 = 0;

	public const int HASH_MD2 = 1;

	public const int HASH_MD5 = 2;

	public const int HASH_RipeMD160 = 3;

	public const int HASH_SH1 = 4;

	public const int HASH_SHA224 = 5;

	public const int HASH_SHA256 = 6;

	public const int HASH_SHA384 = 7;

	public const int HASH_SHA512 = 8;

	public const int LOADKEY_SUCCEEDED = 0;

	public const int LOADKEY_REGINFONOTFOUND = 1;

	public const int LOADKEY_NAMEBUFFERTOOSMALL = 2;

	public const int LOADKEY_KEYBUFFERTOOSMALL = 3;

	public const int KEY_STATUS_DOESNOTEXIST = 0;

	public const int KEY_STATUS_VALID = 1;

	public const int KEY_STATUS_INVALID = 2;

	public const int KEY_STATUS_STOLEN = 3;

	public const int KEY_STATUS_DATEEXPIRED = 4;

	public const int KEY_STATUS_WITHOUTHARDWARELOCK = 5;

	public const int KEY_STATUS_WITHOUTEXPIRATIONDATE = 6;

	public const int KEY_STATUS_WITHOUTREGISTERAFTERDATE = 7;

	public const int KEY_STATUS_WITHOUTREGISTERBEFOREDATE = 8;

	public const int KEY_STATUS_WITHOUTEXECUTIONSLIMIT = 9;

	public const int KEY_STATUS_WITHOUTDAYSLIMIT = 10;

	public const int KEY_STATUS_WITHOUTRUNTIMELIMIT = 11;

	public const int KEY_STATUS_WITHOUTGLOBALTIMELIMIT = 12;

	public const int KEY_STATUS_WITHOUTCOUNTRYLOCK = 13;

	public const int KEY_STATUS_COUNTRYINVALID = 14;

	public const int KEY_STATUS_REGISTERAFTERFAILED = 15;

	public const int KEY_STATUS_REGISTERBEFOREFAILED = 16;

	public const int KEY_STATUS_EXECUTIONSEXPIRED = 17;

	public const int KEY_STATUS_DAYSEXPIRED = 18;

	public const int KEY_STATUS_RUNTIMEEXPIRED = 19;

	public const int KEY_STATUS_GLOBALTIMEEXPIRED = 20;

	public const int KEY_STATUS_HARDWARECHANGESEXCEEDED_VOLUMESERIAL = 21;

	public const int KEY_STATUS_HARDWARECHANGESEXCEEDED_VOLUMENAME = 22;

	public const int KEY_STATUS_HARDWARECHANGESEXCEEDED_COMPUTERNAME = 23;

	public const int KEY_STATUS_HARDWARECHANGESEXCEEDED_CPU = 24;

	public const int KEY_STATUS_HARDWARECHANGESEXCEEDED_MOTHERBOARD = 25;

	public const int KEY_STATUS_HARDWARECHANGESEXCEEDED_WINDOWSKEY = 26;

	public const int KEY_STATUS_HARDWARECHANGESEXCEEDED_HDDSERIAL = 27;

	public const int KEY_STATUS_HARDWARECHANGESEXCEEDED_USERNAME = 28;

	public const int KEY_STATUS_HARDWAREINVALID = 29;

	public const int CN_AFGHANISTAN = 114;

	public const int CN_ALBANIA = 1;

	public const int CN_ALGERIA = 2;

	public const int CN_ARGENTINA = 3;

	public const int CN_ARMENIA = 4;

	public const int CN_AUSTRALIA = 5;

	public const int CN_AUSTRIA = 6;

	public const int CN_AZERBAIJAN = 7;

	public const int CN_BAHRAIN = 8;

	public const int CN_BANGLADESH = 115;

	public const int CN_BELARUS = 9;

	public const int CN_BELGIUM = 10;

	public const int CN_BELIZE = 11;

	public const int CN_BOLIVIA = 116;

	public const int CN_BOSNIA = 117;

	public const int CN_BRAZIL = 13;

	public const int CN_BRUNEI = 14;

	public const int CN_BULGARIA = 15;

	public const int CN_CAMBODIA = 16;

	public const int CN_CANADA = 17;

	public const int CN_CARRIBEAN = 118;

	public const int CN_CHILE = 20;

	public const int CN_CHINA = 21;

	public const int CN_COLOMBIA = 22;

	public const int CN_COSTARICA = 23;

	public const int CN_CROATIA = 24;

	public const int CN_CZECH = 25;

	public const int CN_DENMARK = 26;

	public const int CN_DOMINICAN = 27;

	public const int CN_ECUADOR = 28;

	public const int CN_EGYPT = 29;

	public const int CN_ELSALVADOR = 30;

	public const int CN_ESTONIA = 31;

	public const int CN_ETHIOPIA = 119;

	public const int CN_FAROE = 32;

	public const int CN_FINLAND = 33;

	public const int CN_FRANCE = 34;

	public const int CN_GEORGIA = 35;

	public const int CN_GERMANY = 36;

	public const int CN_GREECE = 37;

	public const int CN_GREENLAND = 120;

	public const int CN_GUATEMALA = 38;

	public const int CN_HONDURAS = 39;

	public const int CN_HONGKONG = 40;

	public const int CN_HUNGARU = 41;

	public const int CN_ICELAND = 42;

	public const int CN_INDIA = 43;

	public const int CN_INDONESIA = 44;

	public const int CN_IRAN = 45;

	public const int CN_IRAQ = 46;

	public const int CN_IRELAND = 47;

	public const int CN_ISRAEL = 48;

	public const int CN_ITALY = 49;

	public const int CN_JAMAICA = 50;

	public const int CN_JAPAN = 51;

	public const int CN_JORDAN = 52;

	public const int CN_KAZAKHSTAN = 53;

	public const int CN_KENYA = 54;

	public const int CN_KOREA = 56;

	public const int CN_KUWAIT = 57;

	public const int CN_KYRGYZSTAN = 58;

	public const int CN_LAOS = 121;

	public const int CN_LATVIA = 59;

	public const int CN_LEBANON = 60;

	public const int CN_LIBYAN = 122;

	public const int CN_LIECHTENSTEIN = 62;

	public const int CN_LITHUANIA = 63;

	public const int CN_LUXEMBOURG = 64;

	public const int CN_MACAO = 65;

	public const int CN_MACEDONIA = 66;

	public const int CN_MALAYSIA = 67;

	public const int CN_MALDIVES = 123;

	public const int CN_MALTA = 124;

	public const int CN_MEXOCI = 68;

	public const int CN_MONACO = 70;

	public const int CN_MONGOLIA = 71;

	public const int CN_MONTENEGRO = 125;

	public const int CN_MOROCCO = 72;

	public const int CN_NEPAL = 126;

	public const int CN_NETHERLANDS = 73;

	public const int CN_NEWZEALAND = 74;

	public const int CN_NICARAGUA = 75;

	public const int CN_NIGERIA = 127;

	public const int CN_NORWAY = 76;

	public const int CN_OMAN = 77;

	public const int CN_PAKISTAN = 78;

	public const int CN_PANAMA = 79;

	public const int CN_PARAGUAY = 80;

	public const int CN_PERY = 81;

	public const int CN_PHILIPPINES = 82;

	public const int CN_POLAND = 83;

	public const int CN_PORTUGAL = 84;

	public const int CN_PUERTORICO = 85;

	public const int CN_QATAR = 86;

	public const int CN_ROMANIA = 87;

	public const int CN_RUSSIA = 88;

	public const int CN_RWANDA = 128;

	public const int CN_SAUDIARABIA = 89;

	public const int CN_SENEGAL = 129;

	public const int CN_SERBIA = 130;

	public const int CN_SERBIAMONTENEGRO = 90;

	public const int CN_SINGAROPE = 91;

	public const int CN_SLOVAKIA = 92;

	public const int CN_SLOVENIA = 93;

	public const int CN_SOUTHAFRICA = 94;

	public const int CN_SPAIN = 95;

	public const int CN_SRILANKA = 131;

	public const int CN_SWEDEN = 96;

	public const int CN_SWITZERLAND = 97;

	public const int CN_SYRIAN = 132;

	public const int CN_TAIWAN = 98;

	public const int CN_TAJIKISTAN = 99;

	public const int CN_THAILAND = 100;

	public const int CN_TRINIDADTOBAGO = 101;

	public const int CN_TUNISIA = 102;

	public const int CN_TURKEY = 103;

	public const int CN_TURKMENISTAN = 133;

	public const int CN_UKRAINE = 104;

	public const int CN_UAE = 105;

	public const int CN_UNITEDKINGDOM = 106;

	public const int CN_USA = 107;

	public const int CN_URUGUAY = 108;

	public const int CN_UZBEKISTAN = 109;

	public const int CN_VENEZUELA = 110;

	public const int CN_VIETNAM = 111;

	public const int CN_YEMEN = 112;

	public const int CN_ZIMBABWE = 113;

	public static TKeyCountries[] KEY_COUNTRIES = new TKeyCountries[127]
	{
		new TKeyCountries("Afghanistan", 114),
		new TKeyCountries("Albania", 1),
		new TKeyCountries("Algeria", 2),
		new TKeyCountries("Argentina", 3),
		new TKeyCountries("Armenia", 4),
		new TKeyCountries("Australia", 5),
		new TKeyCountries("Austria", 6),
		new TKeyCountries("Azerbaijan", 7),
		new TKeyCountries("Bahrain", 8),
		new TKeyCountries("Bangladesh", 115),
		new TKeyCountries("Belarus", 9),
		new TKeyCountries("Belgium", 10),
		new TKeyCountries("Belize", 11),
		new TKeyCountries("Bolivia", 116),
		new TKeyCountries("Bosnia and Herzegovina", 117),
		new TKeyCountries("Brazil", 13),
		new TKeyCountries("Brunei Darussalam", 14),
		new TKeyCountries("Bulgaria", 15),
		new TKeyCountries("Cambodia", 16),
		new TKeyCountries("Canada", 17),
		new TKeyCountries("Caribbean", 118),
		new TKeyCountries("Chile", 20),
		new TKeyCountries("China", 21),
		new TKeyCountries("Colombia", 22),
		new TKeyCountries("Costa Rica", 23),
		new TKeyCountries("Croatia", 24),
		new TKeyCountries("Czech Republic", 25),
		new TKeyCountries("Denmark", 26),
		new TKeyCountries("Dominican Republic", 27),
		new TKeyCountries("Ecuador", 28),
		new TKeyCountries("Egypt", 29),
		new TKeyCountries("El Salvador", 30),
		new TKeyCountries("Estonia", 31),
		new TKeyCountries("Ethiopia", 119),
		new TKeyCountries("Faroe Islands", 32),
		new TKeyCountries("Finland", 33),
		new TKeyCountries("France", 34),
		new TKeyCountries("Georgia", 35),
		new TKeyCountries("Germany", 36),
		new TKeyCountries("Greece", 37),
		new TKeyCountries("Greenland", 120),
		new TKeyCountries("Guatemala", 38),
		new TKeyCountries("Honduras", 39),
		new TKeyCountries("Hong Kong", 40),
		new TKeyCountries("Hungary", 41),
		new TKeyCountries("Iceland", 42),
		new TKeyCountries("India", 43),
		new TKeyCountries("Indonesia", 44),
		new TKeyCountries("Iran", 45),
		new TKeyCountries("Iraq", 46),
		new TKeyCountries("Ireland", 47),
		new TKeyCountries("Israel", 48),
		new TKeyCountries("Italy", 49),
		new TKeyCountries("Jamaica", 50),
		new TKeyCountries("Japan", 51),
		new TKeyCountries("Jordan", 52),
		new TKeyCountries("Kazakhstan", 53),
		new TKeyCountries("Kenya", 54),
		new TKeyCountries("Korea", 56),
		new TKeyCountries("Kuwait", 57),
		new TKeyCountries("Kyrgyzstan", 58),
		new TKeyCountries("Laos", 121),
		new TKeyCountries("Latvia", 59),
		new TKeyCountries("Lebanon", 60),
		new TKeyCountries("Libyan", 122),
		new TKeyCountries("Liechtenstein", 62),
		new TKeyCountries("Lithuania", 63),
		new TKeyCountries("Luxembourg", 64),
		new TKeyCountries("Macao", 65),
		new TKeyCountries("Macedonia", 66),
		new TKeyCountries("Malaysia", 67),
		new TKeyCountries("Maldives", 123),
		new TKeyCountries("Malta", 124),
		new TKeyCountries("Mexico", 68),
		new TKeyCountries("Monaco", 70),
		new TKeyCountries("Mongolia", 71),
		new TKeyCountries("Montenegro", 125),
		new TKeyCountries("Morocco", 72),
		new TKeyCountries("Nepal", 126),
		new TKeyCountries("Netherlands", 73),
		new TKeyCountries("New Zealand", 74),
		new TKeyCountries("Nicaragua", 75),
		new TKeyCountries("Nigeria", 127),
		new TKeyCountries("Norway", 76),
		new TKeyCountries("Oman", 77),
		new TKeyCountries("Pakistan", 78),
		new TKeyCountries("Panama", 79),
		new TKeyCountries("Paraguay", 80),
		new TKeyCountries("Peru", 81),
		new TKeyCountries("Philippines", 82),
		new TKeyCountries("Poland", 83),
		new TKeyCountries("Portugal", 84),
		new TKeyCountries("Puerto Rico", 85),
		new TKeyCountries("Qatar", 86),
		new TKeyCountries("Romania", 87),
		new TKeyCountries("Russia", 88),
		new TKeyCountries("Rwanda", 128),
		new TKeyCountries("Saudi Arabia", 89),
		new TKeyCountries("Senegal", 129),
		new TKeyCountries("Serbia", 130),
		new TKeyCountries("Serbia and Montenegro", 90),
		new TKeyCountries("Singapore", 91),
		new TKeyCountries("Slovakia", 92),
		new TKeyCountries("Slovenia", 93),
		new TKeyCountries("South Africa", 94),
		new TKeyCountries("Spain", 95),
		new TKeyCountries("Sri Lanka", 131),
		new TKeyCountries("Sweden", 96),
		new TKeyCountries("Switzerland", 97),
		new TKeyCountries("Syrian", 132),
		new TKeyCountries("Taiwan", 98),
		new TKeyCountries("Tajikistan", 99),
		new TKeyCountries("Thailand", 100),
		new TKeyCountries("Trinidad and Tobago", 101),
		new TKeyCountries("Tunisia", 102),
		new TKeyCountries("Turkey", 103),
		new TKeyCountries("Turkmenistan", 133),
		new TKeyCountries("Ukraine", 104),
		new TKeyCountries("United Arab Emirates", 105),
		new TKeyCountries("United Kingdom", 106),
		new TKeyCountries("United States", 107),
		new TKeyCountries("Uruguay", 108),
		new TKeyCountries("Uzbekistan", 109),
		new TKeyCountries("Venezuela", 110),
		new TKeyCountries("Viet Nam", 111),
		new TKeyCountries("Yemen", 112),
		new TKeyCountries("Zimbabwe", 113)
	};

	[DllImport("enigma_ide64.dll")]
	public static extern bool EP_RegSaveKey(string Name, string Key);

	[DllImport("enigma_ide64.dll", CharSet = CharSet.Ansi)]
	public static extern bool EP_RegSaveKeyA(string Name, string Key);

	[DllImport("enigma_ide64.dll", CharSet = CharSet.Unicode)]
	public static extern bool EP_RegSaveKeyW(string Name, string Key);

	[DllImport("enigma_ide64.dll")]
	public static extern bool EP_RegLoadKey(ref string Name, ref string Key);

	[DllImport("enigma_ide64.dll", CharSet = CharSet.Ansi)]
	public static extern bool EP_RegLoadKeyA(ref string Name, ref string Key);

	[DllImport("enigma_ide64.dll", CharSet = CharSet.Unicode)]
	public static extern bool EP_RegLoadKeyW(ref string Name, ref string Key);

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_RegLoadKeyEx(IntPtr Name, ref int NameLen, IntPtr Key, ref int KeyLen);

	[DllImport("enigma_ide64.dll")]
	public static extern bool EP_RegCheckKey(string Name, string Key);

	[DllImport("enigma_ide64.dll", CharSet = CharSet.Ansi)]
	public static extern bool EP_RegCheckKeyA(string Name, string Key);

	[DllImport("enigma_ide64.dll", CharSet = CharSet.Unicode)]
	public static extern bool EP_RegCheckKeyW(string Name, string Key);

	[DllImport("enigma_ide64.dll")]
	public static extern bool EP_RegDeleteKey();

	[DllImport("enigma_ide64.dll")]
	public static extern bool EP_RegLoadAndCheckKey();

	[DllImport("enigma_ide64.dll")]
	public static extern bool EP_RegCheckAndSaveKey(string Name, string Key);

	[DllImport("enigma_ide64.dll", CharSet = CharSet.Ansi)]
	public static extern bool EP_RegCheckAndSaveKeyA(string Name, string Key);

	[DllImport("enigma_ide64.dll", CharSet = CharSet.Unicode)]
	public static extern bool EP_RegCheckAndSaveKeyW(string Name, string Key);

	[DllImport("enigma_ide64.dll")]
	public static extern IntPtr EP_RegHardwareID();

	[DllImport("enigma_ide64.dll", CharSet = CharSet.Ansi)]
	public static extern IntPtr EP_RegHardwareIDA();

	[DllImport("enigma_ide64.dll", CharSet = CharSet.Unicode)]
	public static extern IntPtr EP_RegHardwareIDW();

	[DllImport("enigma_ide64.dll")]
	public static extern bool EP_RegKeyCreationDate(ref int Year, ref int Month, ref int Day);

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_RegKeyCreationDateEx();

	[DllImport("enigma_ide64.dll")]
	public static extern bool EP_RegKeyExpirationDate(ref int Year, ref int Month, ref int Day);

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_RegKeyExpirationDateEx();

	[DllImport("enigma_ide64.dll")]
	public static extern bool EP_RegKeyExecutions(ref int Total, ref int Left);

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_RegKeyExecutionsTotal();

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_RegKeyExecutionsLeft();

	[DllImport("enigma_ide64.dll")]
	public static extern bool EP_RegKeyDays(ref int Total, ref int Left);

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_RegKeyDaysTotal();

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_RegKeyDaysLeft();

	[DllImport("enigma_ide64.dll")]
	public static extern bool EP_RegKeyRuntime(ref int Total, ref int Left);

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_RegKeyRuntimeTotal();

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_RegKeyRuntimeLeft();

	[DllImport("enigma_ide64.dll")]
	public static extern bool EP_RegKeyGlobalTime(ref int Total, ref int Left);

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_RegKeyGlobalTimeTotal();

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_RegKeyGlobalTimeLeft();

	[DllImport("enigma_ide64.dll")]
	public static extern bool EP_RegKeyRegisterAfterDate(ref int Year, ref int Month, ref int Day);

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_RegKeyRegisterAfterDateEx();

	[DllImport("enigma_ide64.dll")]
	public static extern bool EP_RegKeyRegisterBeforeDate(ref int Year, ref int Month, ref int Day);

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_RegKeyRegisterBeforeDateEx();

	[DllImport("enigma_ide64.dll", CharSet = CharSet.Ansi)]
	public static extern bool EP_RegKeyInformation(string AName, string AKey, ref TKeyInformation AKeyInfo);

	[DllImport("enigma_ide64.dll", CharSet = CharSet.Ansi)]
	public static extern bool EP_RegKeyInformationA(string AName, string AKey, ref TKeyInformation AKeyInfo);

	[DllImport("enigma_ide64.dll", CharSet = CharSet.Unicode)]
	public static extern bool EP_RegKeyInformationW(string AName, string AKey, ref TKeyInformation AKeyInfo);

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_RegKeyStatus();

	[DllImport("enigma_ide64.dll")]
	public static extern void EP_RegShowDialog();

	[DllImport("enigma_ide64.dll")]
	public static extern void EP_ActivationShowDialog();

	[DllImport("enigma_ide64.dll")]
	public static extern bool EP_ActivationActivate();

	[DllImport("enigma_ide64.dll", CharSet = CharSet.Ansi)]
	public static extern bool EP_ActivationActivateWithId(string ActivationId);

	[DllImport("enigma_ide64.dll", CharSet = CharSet.Ansi)]
	public static extern bool EP_ActivationActivateWithIdA(string ActivationId);

	[DllImport("enigma_ide64.dll", CharSet = CharSet.Unicode)]
	public static extern bool EP_ActivationActivateWithIdW(string ActivationId);

	[DllImport("enigma_ide64.dll")]
	public static extern bool EP_TrialExecutions(ref int Total, ref int Left);

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_TrialExecutionsTotal();

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_TrialExecutionsLeft();

	[DllImport("enigma_ide64.dll")]
	public static extern bool EP_TrialDays(ref int Total, ref int Left);

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_TrialDaysTotal();

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_TrialDaysLeft();

	[DllImport("enigma_ide64.dll")]
	public static extern bool EP_TrialExpirationDate(ref int Year, ref int Month, ref int Day);

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_TrialExpirationDateEx();

	[DllImport("enigma_ide64.dll")]
	public static extern bool EP_TrialDateTillDate(ref int StartYear, ref int StartMonth, ref int StartDay, ref int EndYear, ref int EndMonth, ref int EndDay);

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_TrialDateTillDateStartEx();

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_TrialDateTillDateEndEx();

	[DllImport("enigma_ide64.dll")]
	public static extern bool EP_TrialExecutionTime(ref int Total, ref int Left);

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_TrialExecutionTimeTotal();

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_TrialExecutionTimeLeft();

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_TrialClockReversedDays();

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_MiscGetWatermark(int ID, TWMContent WM);

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_MiscCountryCode();

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_ProtectedStringByID(int ID, IntPtr Buffer, int Len);

	[DllImport("enigma_ide64.dll", CharSet = CharSet.Ansi)]
	public static extern int EP_ProtectedStringByKey(string Key, IntPtr Buffer, int Len);

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_CryptHashBuffer(int Hash, IntPtr Buffer, int Size, IntPtr Digest);

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_CryptHashFileA(int Hash, string FileName, IntPtr Digest);

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_CryptHashFileW(int Hash, string FileName, IntPtr Digest);

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_CryptHashStringA(int Hash, string Str, IntPtr Digest);

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_CryptHashStringW(int Hash, string Str, IntPtr Digest);

	[DllImport("enigma_ide64.dll")]
	public static extern void EP_CryptEncryptBuffer(IntPtr Buffer, int Size, string Key);

	[DllImport("enigma_ide64.dll")]
	public static extern void EP_CryptEncryptBufferEx(IntPtr InBuffer, IntPtr OutBuffer, int Size, IntPtr Key, int KeySize);

	[DllImport("enigma_ide64.dll")]
	public static extern void EP_CryptDecryptBuffer(IntPtr Buffer, int Size, string Key);

	[DllImport("enigma_ide64.dll")]
	public static extern void EP_CryptDecryptBufferEx(IntPtr InBuffer, IntPtr OutBuffer, int Size, IntPtr Key, int KeySize);

	[DllImport("enigma_ide64.dll")]
	public static extern bool EP_CheckupCopies(ref int Total, ref int Current);

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_CheckupCopiesTotal();

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_CheckupCopiesCurrent();

	[DllImport("enigma_ide64.dll")]
	public static extern bool EP_CheckupIsProtected();

	[DllImport("enigma_ide64.dll")]
	public static extern bool EP_CheckupIsEnigmaOk();

	[DllImport("enigma_ide64.dll")]
	public static extern bool EP_CheckupVirtualizationTools();

	[DllImport("enigma_ide64.dll", CharSet = CharSet.Ansi)]
	public static extern bool EP_CheckupFindProcess(string FileName, string WindowText, string WindowClass);

	[DllImport("enigma_ide64.dll", CharSet = CharSet.Ansi)]
	public static extern bool EP_CheckupFindProcessA(string FileName, string WindowText, string WindowClass);

	[DllImport("enigma_ide64.dll", CharSet = CharSet.Unicode)]
	public static extern bool EP_CheckupFindProcessW(string FileName, string WindowText, string WindowClass);

	[DllImport("enigma_ide64.dll", CallingConvention = CallingConvention.StdCall)]
	public static extern bool EP_ProtectionDate(ref int Year, ref int Month, ref int Day);

	[DllImport("enigma_ide64.dll", CallingConvention = CallingConvention.StdCall)]
	public static extern int EP_ProtectionDateEx();

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_EnigmaVersion();

	[DllImport("enigma_ide64.dll")]
	public static extern int EP_SplashScreenShow();

	[DllImport("enigma_ide64.dll")]
	public static extern void EP_SplashScreenHide();

	public static bool EP_RegistrationLoadKeyA(ref string Name, ref string Key)
	{
		Name = Conversions.ToString(Operators.CompareString(Key, string.Empty, TextCompare: false) == 0);
		int NameLen = 0;
		int KeyLen = 0;
		checked
		{
			if (EP_RegLoadKeyEx(IntPtr.Zero, ref NameLen, IntPtr.Zero, ref KeyLen) != 1)
			{
				byte[] array = new byte[NameLen + 1];
				byte[] array2 = new byte[KeyLen + 1];
				if (EP_RegLoadKeyEx(Marshal.UnsafeAddrOfPinnedArrayElement(array, 0), ref NameLen, Marshal.UnsafeAddrOfPinnedArrayElement(array2, 0), ref KeyLen) == 0)
				{
					Name = Encoding.ASCII.GetString(array);
					Key = Encoding.ASCII.GetString(array2);
					return true;
				}
			}
			return false;
		}
	}

	public static bool EP_RegistrationLoadKeyW(ref string Name, ref string Key)
	{
		Name = Conversions.ToString(Operators.CompareString(Key, string.Empty, TextCompare: false) == 0);
		int NameLen = 0;
		int KeyLen = 0;
		checked
		{
			if (EP_RegLoadKeyEx(IntPtr.Zero, ref NameLen, IntPtr.Zero, ref KeyLen) != 1)
			{
				byte[] array = new byte[NameLen + 1];
				byte[] array2 = new byte[KeyLen + 1];
				if (EP_RegLoadKeyEx(Marshal.UnsafeAddrOfPinnedArrayElement(array, 0), ref NameLen, Marshal.UnsafeAddrOfPinnedArrayElement(array2, 0), ref KeyLen) == 0)
				{
					Name = Encoding.Unicode.GetString(array);
					Key = Encoding.Unicode.GetString(array2);
					return true;
				}
			}
			return false;
		}
	}

	public static string EP_RegistrationHardwareID()
	{
		return Marshal.PtrToStringAnsi(EP_RegHardwareID());
	}

	public static string EP_ProtectedStringByIDAsAnsiString(int ID)
	{
		int num = EP_ProtectedStringByID(ID, IntPtr.Zero, 0);
		if (num > 0)
		{
			byte[] array = new byte[checked(num + 1)];
			if (EP_ProtectedStringByID(ID, Marshal.UnsafeAddrOfPinnedArrayElement(array, 0), num) > 0)
			{
				return Encoding.ASCII.GetString(array);
			}
		}
		return string.Empty;
	}

	public static string EP_ProtectedStringByIDAsWideString(int ID)
	{
		int num = EP_ProtectedStringByID(ID, IntPtr.Zero, 0);
		if (num > 0)
		{
			byte[] array = new byte[checked(num + 1)];
			if (EP_ProtectedStringByID(ID, Marshal.UnsafeAddrOfPinnedArrayElement(array, 0), num) > 0)
			{
				return Encoding.Unicode.GetString(array);
			}
		}
		return string.Empty;
	}

	public static string EP_ProtectedStringByKeyAsAnsiString(string Key)
	{
		int num = EP_ProtectedStringByKey(Key, IntPtr.Zero, 0);
		if (num > 0)
		{
			byte[] array = new byte[checked(num + 1)];
			if (EP_ProtectedStringByKey(Key, Marshal.UnsafeAddrOfPinnedArrayElement(array, 0), num) > 0)
			{
				return Encoding.ASCII.GetString(array);
			}
		}
		return string.Empty;
	}

	public static string EP_ProtectedStringByKeyAsWideString(string Key)
	{
		int num = EP_ProtectedStringByKey(Key, IntPtr.Zero, 0);
		if (num > 0)
		{
			byte[] array = new byte[checked(num + 1)];
			if (EP_ProtectedStringByKey(Key, Marshal.UnsafeAddrOfPinnedArrayElement(array, 0), num) > 0)
			{
				return Encoding.Unicode.GetString(array);
			}
		}
		return string.Empty;
	}
}
