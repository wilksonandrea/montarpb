using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Plugin.Core;
using Plugin.Core.Colorful;
using Plugin.Core.Enums;
using Plugin.Core.Filters;
using Plugin.Core.JSON;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.RAW;
using Plugin.Core.Utility;
using Plugin.Core.XML;
using Server.Auth;
using Server.Auth.Data.XML;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.XML;
using Server.Match;
using Server.Match.Data.XML;

public class Program
{
	private delegate bool Delegate0(Enum0 sig);

	private enum Enum0
	{
		CTRL_C_EVENT = 0,
		CTRL_BREAK_EVENT = 1,
		CTRL_CLOSE_EVENT = 2,
		CTRL_LOGOFF_EVENT = 5,
		CTRL_SHUTDOWN_EVENT = 6
	}

	[Serializable]
	[CompilerGenerated]
	private sealed class Class0
	{
		public static readonly Class0 _003C_003E9 = new Class0();

		public static ThreadStart _003C_003E9__10_0;

		internal void method_0()
		{
			smethod_10();
		}
	}

	protected static string string_0 = "";

	protected static string string_1 = "RUS";

	protected static string string_2 = "V3.80";

	protected static Mutex mutex_0 = null;

	protected static readonly FileInfo fileInfo_0 = new FileInfo(Assembly.GetExecutingAssembly().Location);

	protected static readonly int int_0 = Process.GetCurrentProcess().Id;

	[DllImport("Kernel32")]
	private static extern bool SetConsoleCtrlHandler(Delegate0 delegate0_0, bool bool_0);

	private static bool smethod_0(Enum0 enum0_0)
	{
		switch (enum0_0)
		{
		case Enum0.CTRL_CLOSE_EVENT:
		case Enum0.CTRL_LOGOFF_EVENT:
		case Enum0.CTRL_SHUTDOWN_EVENT:
			GClass3.smethod_2(fileInfo_0.FullName);
			return true;
		default:
			return false;
		}
	}

	[STAThread]
	public static void Main(string[] string_3)
	{
		AppDomain.CurrentDomain.UnhandledException += smethod_16;
		Plugin.Core.Colorful.Console.CancelKeyPress += smethod_15;
		try
		{
			Plugin.Core.Colorful.Console.SetWindowSize(160, 40);
			Plugin.Core.Colorful.Console.CursorVisible = false;
			Plugin.Core.Colorful.Console.TreatControlCAsInput = false;
			GClass11.smethod_2();
			string fileVersion = FileVersionInfo.GetVersionInfo(fileInfo_0.Name).FileVersion;
			Plugin.Core.Colorful.Console.Title = "Point Blank (" + string_1 + "-" + string_2 + ") Server " + fileVersion;
			Plugin.Core.Colorful.Console.Clear();
			Process[] processesByName = Process.GetProcessesByName("PointBlank");
			foreach (Process process in processesByName)
			{
				process.Kill();
			}
			mutex_0 = new Mutex(initiallyOwned: true, fileInfo_0.Name, out var createdNew);
			if (!createdNew)
			{
				CLogger.Print("The server is already running! Exiting...", LoggerType.Warning);
				MessageBox.Show("The server is already running!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
				Thread.Sleep(1000);
				GClass11.smethod_3(int_0);
				return;
			}
			string_0 = (GClass6.smethod_0() ? "ADMINISTRATOR" : "SUPERUSER");
			DateTime lastWriteTime = fileInfo_0.LastWriteTime;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine("__________                 ______      _____");
			stringBuilder.AppendLine("___  ____/______ _______  ____  /_____ __  /______________");
			stringBuilder.AppendLine("__  __/  __  __ `__ \\  / / /_  /_  __ `/  __/  __ \\_  ___/");
			stringBuilder.AppendLine("_  /___  _  / / / / / /_/ /_  / / /_/ // /_ / /_/ /  /");
			stringBuilder.AppendLine("/_____/  /_/ /_/ /_/\\__,_/ /_/  \\__,_/ \\__/ \\____//_/");
			stringBuilder.AppendLine("Contributors{0} {1}");
			stringBuilder.AppendLine("These Project is Made by Love. {2}");
			stringBuilder.AppendLine("Regards{0} {3}");
			stringBuilder.AppendLine("");
			stringBuilder.AppendLine(smethod_2("MoMz Games"));
			stringBuilder.AppendLine(smethod_2($"{string_2} +Build: {lastWriteTime:yyMMddHHmmss}"));
			stringBuilder.AppendLine(smethod_2("Copyright 2021 Zepetto Co. All right reserved"));
			Formatter[] args = new Formatter[4]
			{
				new Formatter(":", ColorUtil.Yellow),
				new Formatter("Pavel, Monester, Fusion, zOne62, Garry", ColorUtil.Cyan),
				new Formatter("Not For Sale!", ColorUtil.Red),
				new Formatter("PBServer Suite Dev Team", ColorUtil.Green)
			};
			Plugin.Core.Colorful.Console.WriteLineFormatted($"{stringBuilder}", ColorUtil.White, args);
			bool flag = smethod_13(string_3).Equals("-supc");
			if (flag)
			{
				new Thread((ThreadStart)delegate
				{
					smethod_10();
				}).Start();
			}
			smethod_3(flag ? 1 : 0, fileVersion);
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
		finally
		{
			mutex_0.ReleaseMutex();
			Process.GetCurrentProcess().WaitForExit();
		}
	}

	private static string smethod_2(string string_3)
	{
		StringBuilder stringBuilder = new StringBuilder(80);
		if (string_3 != null)
		{
			stringBuilder.Append(" ");
		}
		int num = 79 - string_3.Length;
		while (stringBuilder.Length != num)
		{
			stringBuilder.Append(" ");
		}
		stringBuilder.Append(string_3);
		return $"{stringBuilder}";
	}

	private static void smethod_3(int int_1, string string_3)
	{
		smethod_4("Config", "Begin");
		ServerConfigJSON.Load();
		CommandHelperJSON.Load();
		ResolutionJSON.Load();
		smethod_4("Config", "Ended");
		smethod_4("Events Data", "Begin");
		EventLoginXML.Load();
		EventBoostXML.Load();
		EventPlaytimeXML.Load();
		EventQuestXML.Load();
		EventRankUpXML.Load();
		EventVisitXML.Load();
		EventXmasXML.Load();
		smethod_4("Events Data", "Ended");
		smethod_4("Event Portal", "Begin");
		PortalManager.Load();
		smethod_4("Event Portal", "Ended");
		smethod_4("Shop Data", "Begin");
		ShopManager.Load(1);
		ShopManager.Load(2);
		smethod_4("Shop Data", "Ended");
		smethod_4("Mission Cards", "Begin");
		MissionCardRAW.LoadBasicCards(1);
		MissionCardRAW.LoadBasicCards(2);
		smethod_4("Mission Cards", "Ended");
		smethod_4("Server Data", "Begin");
		TemplatePackXML.Load();
		TitleSystemXML.Load();
		TitleAwardXML.Load();
		MissionAwardXML.Load();
		MissionConfigXML.Load();
		SChannelXML.Load();
		SynchronizeXML.Load();
		SystemMapXML.Load();
		ClanRankXML.Load();
		PlayerRankXML.Load();
		CouponEffectXML.Load();
		PermissionXML.Load();
		RandomBoxXML.Load();
		BattleBoxXML.Load();
		DirectLibraryXML.Load();
		InternetCafeXML.Load();
		RedeemCodeXML.Load();
		BattleRewardXML.Load();
		NickFilter.Load();
		smethod_4("Server Data", "Ended");
		smethod_4("Classic Mode", "Begin");
		GameRuleXML.Load();
		smethod_4("Classic Mode", "Ended");
		smethod_4("Battle Pass", "Begin");
		SeasonChallengeXML.Load();
		smethod_4("Battle Pass", "Ended");
		smethod_4("Competitive", "Begin");
		CompetitiveXML.Load();
		smethod_4("Competitive", "Ended");
		Thread.Sleep(250);
		smethod_4("Plugin Status", "Begin");
		GClass12.smethod_0(new IPEndPoint(IPAddress.Parse(ConfigLoader.HOST[0]), 1909));
		CLogger.Print("All Server Plugins Successfully Loaded", LoggerType.Info);
		smethod_4("Plugin Status", "Ended");
		smethod_7(smethod_5(), int_1, string_3);
	}

	private static void smethod_4(string string_3, string string_4)
	{
		StringBuilder stringBuilder = new StringBuilder(80);
		if (string_3 != null)
		{
			stringBuilder.Append("---[").Append(string_3).Append(']');
		}
		string text = ((string_4 == null) ? "" : ("[" + string_4 + "]---"));
		int num = 79 - text.Length;
		while (stringBuilder.Length != num)
		{
			stringBuilder.Append('-');
		}
		stringBuilder.Append(text);
		Plugin.Core.Colorful.Console.WriteLine((string_4.Equals("Ended") ? $"{stringBuilder}\n" : $"\n{stringBuilder}") ?? "");
	}

	private static bool smethod_5()
	{
		if (ComDiv.ValidateAllPlayersAccount())
		{
			return smethod_6();
		}
		return false;
	}

	private static bool smethod_6()
	{
		try
		{
			smethod_4("Auth Server", "Begin");
			Server.Auth.Data.XML.ChannelsXML.Load();
			AuthXender.GetPlugin(ConfigLoader.HOST[0], ConfigLoader.DEFAULT_PORT[0]);
			smethod_4("Auth Server", "Ended");
			smethod_4("Game Server", "Begin");
			Server.Game.Data.XML.ChannelsXML.Load();
			ClanManager.Load();
			foreach (SChannelModel server in SChannelXML.Servers)
			{
				if (server.Id >= 1 && server.Port >= ConfigLoader.DEFAULT_PORT[1])
				{
					GameXender.GetPlugin(server.Id, ConfigLoader.HOST[0], server.Port);
				}
			}
			smethod_4("Game Server", "Ended");
			smethod_4("Battle Server", "Begin");
			MapStructureXML.Load();
			CharaStructureXML.Load();
			ItemStatisticXML.Load();
			MatchXender.GetPlugin(ConfigLoader.HOST[0], ConfigLoader.DEFAULT_PORT[2]);
			smethod_4("Battle Server", "Ended");
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	private static void smethod_7(bool bool_0, int int_1, string string_3)
	{
		GClass9.string_0 = string_3;
		GClass9.string_1 = (bool_0 ? "SERVER ONLINE" : "SERVER OFFLINE");
		if (bool_0)
		{
			smethod_4("Server Status", "Begin");
			CLogger.Print("Startup Successful, Server Runtime " + DateTimeUtil.Now("yyyy"), LoggerType.Info);
			smethod_4("Server Status", "Ended");
			Plugin.Core.Colorful.Console.WriteLine("");
			try
			{
				smethod_8(int_1, string_3);
				return;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				return;
			}
		}
		smethod_4("Server Status", "Begin");
		CLogger.Print("Startup Unsuccessful, Server Runtime " + DateTimeUtil.Now("yyyy"), LoggerType.Warning);
		smethod_4("Server Status", "Ended");
		Plugin.Core.Colorful.Console.WriteLine("");
	}

	private static async void smethod_8(int int_1, string string_3)
	{
		while (true)
		{
			smethod_14();
			double num = GClass6.smethod_2();
			double num2 = GClass6.smethod_3();
			int num3 = ComDiv.CountDB("SELECT COUNT(*) FROM accounts");
			int num4 = ComDiv.CountDB($"SELECT COUNT(*) FROM accounts WHERE online = {true}");
			Plugin.Core.Colorful.Console.Title = "Point Blank (" + string_1 + "-" + string_2 + ") Server " + string_3 + " </> " + ((int_1 == 1) ? $"RAM Usages: {num:0.0} MB)" : $"Users: {num3}; Online: {num4}; RAM Usages: {num:0.0} MB ({num2:0.0}%)") + " -" + string_0 + " </> Timeline: " + DateTimeUtil.Now("dddd, MMMM dd, yyyy - HH:mm:ss");
			smethod_9();
			await Task.Delay(1000);
		}
	}

	private static bool smethod_9()
	{
		try
		{
			if (DateTimeUtil.Now("HHmmss").Equals("000000"))
			{
				int num = ComDiv.CountDB("SELECT COUNT(*) FROM player_stat_dailies");
				if (num > 0)
				{
					ComDiv.UpdateDB("player_stat_dailies", new string[10] { "matches", "match_wins", "match_loses", "match_draws", "kills_count", "deaths_count", "headshots_count", "exp_gained", "point_gained", "playtime" }, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
				}
				int num2 = ComDiv.CountDB("SELECT COUNT(*) FROM player_reports");
				if (num2 > 0)
				{
					ComDiv.UpdateDB("player_reports", new string[1] { "ticket_count" }, 3);
				}
				return true;
			}
			return false;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	private static void smethod_10()
	{
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(defaultValue: false);
		MainForm mainForm = new MainForm(int_0, new DirectoryInfo($"{fileInfo_0.Directory}/Logs"))
		{
			TopMost = true
		};
		Application.Run(mainForm);
	}

	private static string smethod_11()
	{
		string result = "";
		try
		{
			IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
			IPAddress[] addressList = hostEntry.AddressList;
			foreach (IPAddress ıPAddress in addressList)
			{
				if (ıPAddress.AddressFamily == AddressFamily.InterNetwork)
				{
					result = ıPAddress.ToString();
				}
			}
			return result;
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			return result;
		}
	}

	private static string smethod_12()
	{
		foreach (ClientLocale gameLocale in ConfigLoader.GameLocales)
		{
			if (gameLocale == ClientLocale.Russia)
			{
				return gameLocale.ToString();
			}
		}
		return "Outside!";
	}

	private static string smethod_13(string[] string_3)
	{
		int num = 0;
		if (num < string_3.Length)
		{
			return string_3[num];
		}
		return "";
	}

	private static void smethod_14()
	{
		GClass9.string_2 = $"{Convert.ToInt32(GClass6.smethod_2())}";
		GClass9.string_3 = $"{GClass6.smethod_3():0.0}%";
		GClass9.string_4 = $"{(double)GClass6.smethod_4(new DirectoryInfo($"{fileInfo_0.Directory}/Logs"), bool_0: true) / 1048576.0:N2}MB";
		GClass9.string_5 = string.Format("{0}", ComDiv.CountDB("SELECT COUNT(*) FROM accounts"));
		GClass9.string_6 = $"{ComDiv.CountDB($"SELECT COUNT(*) FROM accounts WHERE online = '{true}'")}";
		GClass9.string_7 = string.Format("{0}", ComDiv.CountDB("SELECT COUNT(*) FROM system_clan"));
		GClass9.string_8 = string.Format("{0}", ComDiv.CountDB("SELECT COUNT(*) FROM accounts WHERE pc_cafe = '2' OR pc_cafe = '1'"));
		GClass9.string_9 = string.Format("{0}", ComDiv.CountDB("SELECT COUNT(*) FROM accounts WHERE nickname = ''"));
		GClass9.string_10 = string.Format("{0}", ComDiv.CountDB("SELECT COUNT(*) FROM base_auto_ban"));
		GClass9.string_11 = string.Format("{0}", ComDiv.CountDB("SELECT COUNT(*) FROM system_shop") + ComDiv.CountDB("SELECT COUNT(*) FROM system_shop_effects") + ComDiv.CountDB("SELECT COUNT(*) FROM system_shop_sets"));
		GClass9.string_12 = $"{ComDiv.CountDB($"SELECT COUNT(*) FROM system_shop WHERE item_visible = '{false}'") + ComDiv.CountDB($"SELECT COUNT(*) FROM system_shop_effects WHERE coupon_visible = '{false}'")}";
		GClass9.string_13 = string.Format("{0}", ComDiv.CountDB("SELECT COUNT(*) FROM system_shop_repair"));
		GClass9.string_14 = "V" + ServerConfigJSON.GetConfig(ConfigLoader.ConfigId).ClientVersion;
		GClass9.string_15 = smethod_12();
		GClass9.string_16 = smethod_11();
		GClass9.string_17 = DateTimeUtil.Now("yyyy");
		GClass9.string_18 = $"{ConfigLoader.ConfigId}";
		GClass9.string_19 = (ConfigLoader.TournamentRule ? "Enabled" : "Disabled");
		GClass9.string_20 = (ConfigLoader.ICafeSystem ? "Enabled" : "Disabled");
		GClass9.string_21 = (ConfigLoader.AutoAccount ? "Enabled" : "Disabled");
		GClass9.string_22 = (ConfigLoader.AutoBan ? "Enabled" : "Disabled");
	}

	private static void smethod_15(object sender, ConsoleCancelEventArgs e)
	{
		CLogger.Print("Additional Changes were sended to the client.", LoggerType.Info);
		GClass13.smethod_0("Attention! \nThe Server Will Be Restarted!");
		e.Cancel = true;
	}

	private static void smethod_16(object sender, UnhandledExceptionEventArgs e)
	{
		CLogger.Print($"Application Handle Exception Sender: {sender} Terminating: {e.IsTerminating} {(Exception)e.ExceptionObject}", LoggerType.Error);
	}

	private static void smethod_17()
	{
		DateTime dateTime = smethod_19();
		if (dateTime == default(DateTime) || long.Parse(dateTime.ToString("yyMMddHHmmss")) >= 250730235959L)
		{
			GClass11.smethod_3(int_0);
		}
	}

	private static void smethod_18()
	{
		if (!GClass4.smethod_0().Equals("EC2D3196B814D999C060259B0F455B0F3383EB8E"))
		{
			GClass11.smethod_3(int_0);
		}
	}

	private static DateTime smethod_19()
	{
		try
		{
			using WebResponse webResponse = WebRequest.Create("http://www.google.com").GetResponse();
			return DateTime.ParseExact(webResponse.Headers["date"], "ddd, dd MMM yyyy HH:mm:ss 'GMT'", CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.AssumeUniversal).ToUniversalTime();
		}
		catch
		{
			return default(DateTime);
		}
	}

	private static string smethod_20()
	{
		try
		{
			string requestUriString = "http://checkip.dyndns.org";
			WebRequest webRequest = WebRequest.Create(requestUriString);
			WebResponse response = webRequest.GetResponse();
			using StreamReader streamReader = new StreamReader(response.GetResponseStream());
			string text = streamReader.ReadToEnd().Trim();
			string[] array = text.Split(':');
			string text2 = array[1].Substring(1);
			string[] array2 = text2.Split('<');
			return array2[0];
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return "";
		}
	}
}
