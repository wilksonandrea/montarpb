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
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

public class Program
{
	protected static string string_0;

	protected static string string_1;

	protected static string string_2;

	protected static Mutex mutex_0;

	protected readonly static FileInfo fileInfo_0;

	protected readonly static int int_0;

	static Program()
	{
		Program.string_0 = "";
		Program.string_1 = "RUS";
		Program.string_2 = "V3.80";
		Program.mutex_0 = null;
		Program.fileInfo_0 = new FileInfo(Assembly.GetExecutingAssembly().Location);
		Program.int_0 = Process.GetCurrentProcess().Id;
	}

	public Program()
	{
	}

	[STAThread]
	public static void Main(string[] string_3)
	{
		bool flag = false;
		AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.smethod_16);
		Plugin.Core.Colorful.Console.CancelKeyPress += new ConsoleCancelEventHandler(Program.smethod_15);
		try
		{
			try
			{
				Plugin.Core.Colorful.Console.SetWindowSize(160, 40);
				Plugin.Core.Colorful.Console.CursorVisible = false;
				Plugin.Core.Colorful.Console.TreatControlCAsInput = false;
				GClass11.smethod_2();
				string fileVersion = FileVersionInfo.GetVersionInfo(Program.fileInfo_0.Name).FileVersion;
				Plugin.Core.Colorful.Console.Title = string.Concat(new string[] { "Point Blank (", Program.string_1, "-", Program.string_2, ") Server ", fileVersion });
				Plugin.Core.Colorful.Console.Clear();
				Process[] processesByName = Process.GetProcessesByName("PointBlank");
				for (int i = 0; i < (int)processesByName.Length; i++)
				{
					processesByName[i].Kill();
				}
				Program.mutex_0 = new Mutex(true, Program.fileInfo_0.Name, out flag);
				if (flag)
				{
					Program.string_0 = (GClass6.smethod_0() ? "ADMINISTRATOR" : "SUPERUSER");
					DateTime lastWriteTime = Program.fileInfo_0.LastWriteTime;
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
					stringBuilder.AppendLine(Program.smethod_2("MoMz Games"));
					stringBuilder.AppendLine(Program.smethod_2(string.Format("{0} +Build: {1:yyMMddHHmmss}", Program.string_2, lastWriteTime)));
					stringBuilder.AppendLine(Program.smethod_2("Copyright 2021 Zepetto Co. All right reserved"));
					Plugin.Core.Colorful.Formatter[] formatter = new Plugin.Core.Colorful.Formatter[] { new Plugin.Core.Colorful.Formatter(":", ColorUtil.Yellow), new Plugin.Core.Colorful.Formatter("Pavel, Monester, Fusion, zOne62, Garry", ColorUtil.Cyan), new Plugin.Core.Colorful.Formatter("Not For Sale!", ColorUtil.Red), new Plugin.Core.Colorful.Formatter("PBServer Suite Dev Team", ColorUtil.Green) };
					Plugin.Core.Colorful.Console.WriteLineFormatted(string.Format("{0}", stringBuilder), ColorUtil.White, formatter);
					bool flag1 = Program.smethod_13(string_3).Equals("-supc");
					if (flag1)
					{
						(new Thread(() => Program.smethod_10())).Start();
					}
					Program.smethod_3(flag1, fileVersion);
				}
				else
				{
					CLogger.Print("The server is already running! Exiting...", LoggerType.Warning, null);
					MessageBox.Show("The server is already running!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
					Thread.Sleep(1000);
					GClass11.smethod_3(Program.int_0);
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}
		finally
		{
			Program.mutex_0.ReleaseMutex();
			Process.GetCurrentProcess().WaitForExit();
		}
	}

	[DllImport("Kernel32", CharSet=CharSet.None, ExactSpelling=false)]
	private static extern bool SetConsoleCtrlHandler(Program.Delegate0 delegate0_0, bool bool_0);

	private static bool smethod_0(Program.Enum0 enum0_0)
	{
		switch (enum0_0)
		{
			case Program.Enum0.CTRL_C_EVENT:
			case Program.Enum0.CTRL_BREAK_EVENT:
			case Program.Enum0.CTRL_BREAK_EVENT | Program.Enum0.CTRL_CLOSE_EVENT:
			case 4:
			{
				return false;
			}
			case Program.Enum0.CTRL_CLOSE_EVENT:
			case Program.Enum0.CTRL_LOGOFF_EVENT:
			case Program.Enum0.CTRL_SHUTDOWN_EVENT:
			{
				GClass3.smethod_2(Program.fileInfo_0.FullName);
				return true;
			}
			default:
			{
				return false;
			}
		}
	}

	private static void smethod_10()
	{
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);
		Application.Run(new MainForm(Program.int_0, new DirectoryInfo(string.Format("{0}/Logs", Program.fileInfo_0.Directory)))
		{
			TopMost = true
		});
	}

	private static string smethod_11()
	{
		string str = "";
		try
		{
			IPAddress[] addressList = Dns.GetHostEntry(Dns.GetHostName()).AddressList;
			for (int i = 0; i < (int)addressList.Length; i++)
			{
				IPAddress pAddress = addressList[i];
				if (pAddress.AddressFamily == AddressFamily.InterNetwork)
				{
					str = pAddress.ToString();
				}
			}
		}
		catch (Exception exception1)
		{
			Exception exception = exception1;
			MessageBox.Show(exception.Message, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		return str;
	}

	private static string smethod_12()
	{
		string str;
		List<ClientLocale>.Enumerator enumerator = ConfigLoader.GameLocales.GetEnumerator();
		try
		{
			while (enumerator.MoveNext())
			{
				ClientLocale current = enumerator.Current;
				if (current != ClientLocale.Russia)
				{
					continue;
				}
				str = current.ToString();
				return str;
			}
			return "Outside!";
		}
		finally
		{
			((IDisposable)enumerator).Dispose();
		}
		return str;
	}

	private static string smethod_13(string[] string_3)
	{
		string[] string3 = string_3;
		int ınt32 = 0;
		if (ınt32 >= (int)string3.Length)
		{
			return "";
		}
		return string3[ınt32];
	}

	private static void smethod_14()
	{
		GClass9.string_2 = string.Format("{0}", Convert.ToInt32(GClass6.smethod_2()));
		GClass9.string_3 = string.Format("{0:0.0}%", GClass6.smethod_3());
		GClass9.string_4 = string.Format("{0:N2}MB", (double)GClass6.smethod_4(new DirectoryInfo(string.Format("{0}/Logs", Program.fileInfo_0.Directory)), true) / 1048576);
		GClass9.string_5 = string.Format("{0}", ComDiv.CountDB("SELECT COUNT(*) FROM accounts"));
		GClass9.string_6 = string.Format("{0}", ComDiv.CountDB(string.Format("SELECT COUNT(*) FROM accounts WHERE online = '{0}'", true)));
		GClass9.string_7 = string.Format("{0}", ComDiv.CountDB("SELECT COUNT(*) FROM system_clan"));
		GClass9.string_8 = string.Format("{0}", ComDiv.CountDB("SELECT COUNT(*) FROM accounts WHERE pc_cafe = '2' OR pc_cafe = '1'"));
		GClass9.string_9 = string.Format("{0}", ComDiv.CountDB("SELECT COUNT(*) FROM accounts WHERE nickname = ''"));
		GClass9.string_10 = string.Format("{0}", ComDiv.CountDB("SELECT COUNT(*) FROM base_auto_ban"));
		GClass9.string_11 = string.Format("{0}", ComDiv.CountDB("SELECT COUNT(*) FROM system_shop") + ComDiv.CountDB("SELECT COUNT(*) FROM system_shop_effects") + ComDiv.CountDB("SELECT COUNT(*) FROM system_shop_sets"));
		GClass9.string_12 = string.Format("{0}", ComDiv.CountDB(string.Format("SELECT COUNT(*) FROM system_shop WHERE item_visible = '{0}'", false)) + ComDiv.CountDB(string.Format("SELECT COUNT(*) FROM system_shop_effects WHERE coupon_visible = '{0}'", false)));
		GClass9.string_13 = string.Format("{0}", ComDiv.CountDB("SELECT COUNT(*) FROM system_shop_repair"));
		GClass9.string_14 = string.Concat("V", ServerConfigJSON.GetConfig(ConfigLoader.ConfigId).ClientVersion);
		GClass9.string_15 = Program.smethod_12();
		GClass9.string_16 = Program.smethod_11();
		GClass9.string_17 = DateTimeUtil.Now("yyyy");
		GClass9.string_18 = string.Format("{0}", ConfigLoader.ConfigId);
		GClass9.string_19 = (ConfigLoader.TournamentRule ? "Enabled" : "Disabled");
		GClass9.string_20 = (ConfigLoader.ICafeSystem ? "Enabled" : "Disabled");
		GClass9.string_21 = (ConfigLoader.AutoAccount ? "Enabled" : "Disabled");
		GClass9.string_22 = (ConfigLoader.AutoBan ? "Enabled" : "Disabled");
	}

	private static void smethod_15(object sender, ConsoleCancelEventArgs e)
	{
		CLogger.Print("Additional Changes were sended to the client.", LoggerType.Info, null);
		GClass13.smethod_0("Attention! \nThe Server Will Be Restarted!");
		e.Cancel = true;
	}

	private static void smethod_16(object sender, UnhandledExceptionEventArgs e)
	{
		CLogger.Print(string.Format("Application Handle Exception Sender: {0} Terminating: {1} {2}", sender, e.IsTerminating, (Exception)e.ExceptionObject), LoggerType.Error, null);
	}

	private static void smethod_17()
	{
		DateTime dateTime = Program.smethod_19();
		DateTime dateTime1 = new DateTime();
		if (!(dateTime == dateTime1) && long.Parse(dateTime.ToString("yyMMddHHmmss")) < 250730235959L)
		{
			return;
		}
		GClass11.smethod_3(Program.int_0);
	}

	private static void smethod_18()
	{
		if (GClass4.smethod_0().Equals("EC2D3196B814D999C060259B0F455B0F3383EB8E"))
		{
			return;
		}
		GClass11.smethod_3(Program.int_0);
	}

	private static DateTime smethod_19()
	{
		DateTime universalTime;
		try
		{
			using (WebResponse response = WebRequest.Create("http://www.google.com").GetResponse())
			{
				universalTime = DateTime.ParseExact(response.Headers["date"], "ddd, dd MMM yyyy HH:mm:ss 'GMT'", CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.AssumeUniversal);
				universalTime = universalTime.ToUniversalTime();
			}
		}
		catch
		{
			universalTime = new DateTime();
		}
		return universalTime;
	}

	private static string smethod_2(string string_3)
	{
		StringBuilder stringBuilder = new StringBuilder(80);
		if (string_3 != null)
		{
			stringBuilder.Append(" ");
		}
		int length = 79 - string_3.Length;
		while (stringBuilder.Length != length)
		{
			stringBuilder.Append(" ");
		}
		stringBuilder.Append(string_3);
		return string.Format("{0}", stringBuilder);
	}

	private static string smethod_20()
	{
		string str;
		try
		{
			WebRequest webRequest = WebRequest.Create("http://checkip.dyndns.org");
			using (StreamReader streamReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
			{
				string str1 = streamReader.ReadToEnd().Trim();
				string[] strArrays = str1.Split(new char[] { ':' });
				string str2 = strArrays[1].Substring(1);
				str = str2.Split(new char[] { '<' })[0];
			}
		}
		catch (Exception exception1)
		{
			Exception exception = exception1;
			CLogger.Print(exception.Message, LoggerType.Error, exception);
			str = "";
		}
		return str;
	}

	private static void smethod_3(int int_1, string string_3)
	{
		Program.smethod_4("Config", "Begin");
		ServerConfigJSON.Load();
		CommandHelperJSON.Load();
		ResolutionJSON.Load();
		Program.smethod_4("Config", "Ended");
		Program.smethod_4("Events Data", "Begin");
		EventLoginXML.Load();
		EventBoostXML.Load();
		EventPlaytimeXML.Load();
		EventQuestXML.Load();
		EventRankUpXML.Load();
		EventVisitXML.Load();
		EventXmasXML.Load();
		Program.smethod_4("Events Data", "Ended");
		Program.smethod_4("Event Portal", "Begin");
		PortalManager.Load();
		Program.smethod_4("Event Portal", "Ended");
		Program.smethod_4("Shop Data", "Begin");
		ShopManager.Load(1);
		ShopManager.Load(2);
		Program.smethod_4("Shop Data", "Ended");
		Program.smethod_4("Mission Cards", "Begin");
		MissionCardRAW.LoadBasicCards(1);
		MissionCardRAW.LoadBasicCards(2);
		Program.smethod_4("Mission Cards", "Ended");
		Program.smethod_4("Server Data", "Begin");
		TemplatePackXML.Load();
		TitleSystemXML.Load();
		TitleAwardXML.Load();
		MissionAwardXML.Load();
		MissionConfigXML.Load();
		SChannelXML.Load(false);
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
		Program.smethod_4("Server Data", "Ended");
		Program.smethod_4("Classic Mode", "Begin");
		GameRuleXML.Load();
		Program.smethod_4("Classic Mode", "Ended");
		Program.smethod_4("Battle Pass", "Begin");
		SeasonChallengeXML.Load();
		Program.smethod_4("Battle Pass", "Ended");
		Program.smethod_4("Competitive", "Begin");
		CompetitiveXML.Load();
		Program.smethod_4("Competitive", "Ended");
		Thread.Sleep(250);
		Program.smethod_4("Plugin Status", "Begin");
		GClass12.smethod_0(new IPEndPoint(IPAddress.Parse(ConfigLoader.HOST[0]), 1909));
		CLogger.Print("All Server Plugins Successfully Loaded", LoggerType.Info, null);
		Program.smethod_4("Plugin Status", "Ended");
		Program.smethod_7(Program.smethod_5(), int_1, string_3);
	}

	private static void smethod_4(string string_3, string string_4)
	{
		StringBuilder stringBuilder = new StringBuilder(80);
		if (string_3 != null)
		{
			stringBuilder.Append("---[").Append(string_3).Append(']');
		}
		string str = (string_4 == null ? "" : string.Concat("[", string_4, "]---"));
		int length = 79 - str.Length;
		while (stringBuilder.Length != length)
		{
			stringBuilder.Append('-');
		}
		stringBuilder.Append(str);
		Plugin.Core.Colorful.Console.WriteLine((string_4.Equals("Ended") ? string.Format("{0}\n", stringBuilder) : string.Format("\n{0}", stringBuilder)) ?? "");
	}

	private static bool smethod_5()
	{
		if (ComDiv.ValidateAllPlayersAccount())
		{
			return Program.smethod_6();
		}
		return false;
	}

	private static bool smethod_6()
	{
		bool flag;
		try
		{
			Program.smethod_4("Auth Server", "Begin");
			Server.Auth.Data.XML.ChannelsXML.Load();
			AuthXender.GetPlugin(ConfigLoader.HOST[0], ConfigLoader.DEFAULT_PORT[0]);
			Program.smethod_4("Auth Server", "Ended");
			Program.smethod_4("Game Server", "Begin");
			Server.Game.Data.XML.ChannelsXML.Load();
			ClanManager.Load();
			foreach (SChannelModel server in SChannelXML.Servers)
			{
				if (server.Id < 1 || server.Port < ConfigLoader.DEFAULT_PORT[1])
				{
					continue;
				}
				GameXender.GetPlugin(server.Id, ConfigLoader.HOST[0], (int)server.Port);
			}
			Program.smethod_4("Game Server", "Ended");
			Program.smethod_4("Battle Server", "Begin");
			MapStructureXML.Load();
			CharaStructureXML.Load();
			ItemStatisticXML.Load();
			MatchXender.GetPlugin(ConfigLoader.HOST[0], ConfigLoader.DEFAULT_PORT[2]);
			Program.smethod_4("Battle Server", "Ended");
			flag = true;
		}
		catch (Exception exception1)
		{
			Exception exception = exception1;
			CLogger.Print(exception.Message, LoggerType.Error, exception);
			flag = false;
		}
		return flag;
	}

	private static void smethod_7(bool bool_0, int int_1, string string_3)
	{
		GClass9.string_0 = string_3;
		GClass9.string_1 = (bool_0 ? "SERVER ONLINE" : "SERVER OFFLINE");
		if (!bool_0)
		{
			Program.smethod_4("Server Status", "Begin");
			CLogger.Print(string.Concat("Startup Unsuccessful, Server Runtime ", DateTimeUtil.Now("yyyy")), LoggerType.Warning, null);
			Program.smethod_4("Server Status", "Ended");
			Plugin.Core.Colorful.Console.WriteLine("");
		}
		else
		{
			Program.smethod_4("Server Status", "Begin");
			CLogger.Print(string.Concat("Startup Successful, Server Runtime ", DateTimeUtil.Now("yyyy")), LoggerType.Info, null);
			Program.smethod_4("Server Status", "Ended");
			Plugin.Core.Colorful.Console.WriteLine("");
			try
			{
				Program.smethod_8(int_1, string_3);
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
		}
	}

	private static async void smethod_8(int int_1, string string_3)
	{
		Program.Struct0 int1 = new Program.Struct0();
		int1.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
		int1.int_1 = int_1;
		int1.string_0 = string_3;
		int1.int_0 = -1;
		int1.asyncVoidMethodBuilder_0.Start<Program.Struct0>(ref int1);
	}

	private static bool smethod_9()
	{
		bool flag;
		try
		{
			if (!DateTimeUtil.Now("HHmmss").Equals("000000"))
			{
				flag = false;
			}
			else
			{
				if (ComDiv.CountDB("SELECT COUNT(*) FROM player_stat_dailies") > 0)
				{
					ComDiv.UpdateDB("player_stat_dailies", new string[] { "matches", "match_wins", "match_loses", "match_draws", "kills_count", "deaths_count", "headshots_count", "exp_gained", "point_gained", "playtime" }, new object[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
				}
				if (ComDiv.CountDB("SELECT COUNT(*) FROM player_reports") > 0)
				{
					ComDiv.UpdateDB("player_reports", new string[] { "ticket_count" }, new object[] { 3 });
				}
				flag = true;
			}
		}
		catch (Exception exception1)
		{
			Exception exception = exception1;
			CLogger.Print(exception.Message, LoggerType.Error, exception);
			flag = false;
		}
		return flag;
	}

	private delegate bool Delegate0(Program.Enum0 sig);

	private enum Enum0
	{
		CTRL_C_EVENT = 0,
		CTRL_BREAK_EVENT = 1,
		CTRL_CLOSE_EVENT = 2,
		CTRL_LOGOFF_EVENT = 5,
		CTRL_SHUTDOWN_EVENT = 6
	}
}