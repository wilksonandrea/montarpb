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

// Token: 0x02000002 RID: 2
public class Program
{
	// Token: 0x06000001 RID: 1
	[DllImport("Kernel32")]
	private static extern bool SetConsoleCtrlHandler(Program.Delegate0 delegate0_0, bool bool_0);

	// Token: 0x06000002 RID: 2 RVA: 0x00002084 File Offset: 0x00000284
	private static bool smethod_0(Program.Enum0 enum0_0)
	{
		switch (enum0_0)
		{
		case Program.Enum0.CTRL_CLOSE_EVENT:
		case Program.Enum0.CTRL_LOGOFF_EVENT:
		case Program.Enum0.CTRL_SHUTDOWN_EVENT:
			GClass3.smethod_2(Program.fileInfo_0.FullName);
			return true;
		}
		return false;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x000029DC File Offset: 0x00000BDC
	[STAThread]
	public static void Main(string[] string_3)
	{
		AppDomain.CurrentDomain.UnhandledException += Program.smethod_16;
		Plugin.Core.Colorful.Console.CancelKeyPress += Program.smethod_15;
		try
		{
			Plugin.Core.Colorful.Console.SetWindowSize(160, 40);
			Plugin.Core.Colorful.Console.CursorVisible = false;
			Plugin.Core.Colorful.Console.TreatControlCAsInput = false;
			GClass11.smethod_2();
			string fileVersion = FileVersionInfo.GetVersionInfo(Program.fileInfo_0.Name).FileVersion;
			Plugin.Core.Colorful.Console.Title = string.Concat(new string[]
			{
				"Point Blank (",
				Program.string_1,
				"-",
				Program.string_2,
				") Server ",
				fileVersion
			});
			Plugin.Core.Colorful.Console.Clear();
			foreach (Process process in Process.GetProcessesByName("PointBlank"))
			{
				process.Kill();
			}
			bool flag;
			Program.mutex_0 = new Mutex(true, Program.fileInfo_0.Name, out flag);
			if (!flag)
			{
				CLogger.Print("The server is already running! Exiting...", LoggerType.Warning, null);
				MessageBox.Show("The server is already running!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
				Thread.Sleep(1000);
				GClass11.smethod_3(Program.int_0);
			}
			else
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
				Formatter[] array = new Formatter[]
				{
					new Formatter(":", ColorUtil.Yellow),
					new Formatter("Pavel, Monester, Fusion, zOne62, Garry", ColorUtil.Cyan),
					new Formatter("Not For Sale!", ColorUtil.Red),
					new Formatter("PBServer Suite Dev Team", ColorUtil.Green)
				};
				Plugin.Core.Colorful.Console.WriteLineFormatted(string.Format("{0}", stringBuilder), ColorUtil.White, array);
				bool flag2 = Program.smethod_13(string_3).Equals("-supc");
				if (flag2)
				{
					new Thread(new ThreadStart(Program.Class0.<>9.method_0)).Start();
				}
				Program.smethod_3((flag2 > false) ? 1 : 0, fileVersion);
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
		finally
		{
			Program.mutex_0.ReleaseMutex();
			Process.GetCurrentProcess().WaitForExit();
		}
	}

	// Token: 0x06000004 RID: 4 RVA: 0x00002CF4 File Offset: 0x00000EF4
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
		return string.Format("{0}", stringBuilder);
	}

	// Token: 0x06000005 RID: 5 RVA: 0x00002D4C File Offset: 0x00000F4C
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

	// Token: 0x06000006 RID: 6 RVA: 0x00002F84 File Offset: 0x00001184
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
		Plugin.Core.Colorful.Console.WriteLine((string_4.Equals("Ended") ? string.Format("{0}\n", stringBuilder) : string.Format("\n{0}", stringBuilder)) ?? "");
	}

	// Token: 0x06000007 RID: 7 RVA: 0x000020BC File Offset: 0x000002BC
	private static bool smethod_5()
	{
		return ComDiv.ValidateAllPlayersAccount() && Program.smethod_6();
	}

	// Token: 0x06000008 RID: 8 RVA: 0x0000302C File Offset: 0x0000122C
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
			foreach (SChannelModel schannelModel in SChannelXML.Servers)
			{
				if (schannelModel.Id >= 1 && (int)schannelModel.Port >= ConfigLoader.DEFAULT_PORT[1])
				{
					GameXender.GetPlugin(schannelModel.Id, ConfigLoader.HOST[0], (int)schannelModel.Port);
				}
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
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			flag = false;
		}
		return flag;
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00003184 File Offset: 0x00001384
	private static void smethod_7(bool bool_0, int int_1, string string_3)
	{
		GClass9.string_0 = string_3;
		GClass9.string_1 = (bool_0 ? "SERVER ONLINE" : "SERVER OFFLINE");
		if (bool_0)
		{
			Program.smethod_4("Server Status", "Begin");
			CLogger.Print("Startup Successful, Server Runtime " + DateTimeUtil.Now("yyyy"), LoggerType.Info, null);
			Program.smethod_4("Server Status", "Ended");
			Plugin.Core.Colorful.Console.WriteLine("");
			try
			{
				Program.smethod_8(int_1, string_3);
				return;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				return;
			}
		}
		Program.smethod_4("Server Status", "Begin");
		CLogger.Print("Startup Unsuccessful, Server Runtime " + DateTimeUtil.Now("yyyy"), LoggerType.Warning, null);
		Program.smethod_4("Server Status", "Ended");
		Plugin.Core.Colorful.Console.WriteLine("");
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00003260 File Offset: 0x00001460
	private static void smethod_8(int int_1, string string_3)
	{
		Program.Struct0 @struct;
		@struct.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
		@struct.int_1 = int_1;
		@struct.string_0 = string_3;
		@struct.int_0 = -1;
		@struct.asyncVoidMethodBuilder_0.Start<Program.Struct0>(ref @struct);
	}

	// Token: 0x0600000B RID: 11 RVA: 0x000032A0 File Offset: 0x000014A0
	private static bool smethod_9()
	{
		bool flag;
		try
		{
			if (DateTimeUtil.Now("HHmmss").Equals("000000"))
			{
				int num = ComDiv.CountDB("SELECT COUNT(*) FROM player_stat_dailies");
				if (num > 0)
				{
					ComDiv.UpdateDB("player_stat_dailies", new string[] { "matches", "match_wins", "match_loses", "match_draws", "kills_count", "deaths_count", "headshots_count", "exp_gained", "point_gained", "playtime" }, new object[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 });
				}
				int num2 = ComDiv.CountDB("SELECT COUNT(*) FROM player_reports");
				if (num2 > 0)
				{
					ComDiv.UpdateDB("player_reports", new string[] { "ticket_count" }, new object[] { 3 });
				}
				flag = true;
			}
			else
			{
				flag = false;
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			flag = false;
		}
		return flag;
	}

	// Token: 0x0600000C RID: 12 RVA: 0x0000340C File Offset: 0x0000160C
	private static void smethod_10()
	{
		Application.EnableVisualStyles();
		Application.SetCompatibleTextRenderingDefault(false);
		MainForm mainForm = new MainForm(Program.int_0, new DirectoryInfo(string.Format("{0}/Logs", Program.fileInfo_0.Directory)))
		{
			TopMost = true
		};
		Application.Run(mainForm);
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00003458 File Offset: 0x00001658
	private static string smethod_11()
	{
		string text = "";
		try
		{
			IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
			foreach (IPAddress ipaddress in hostEntry.AddressList)
			{
				if (ipaddress.AddressFamily == AddressFamily.InterNetwork)
				{
					text = ipaddress.ToString();
				}
			}
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		return text;
	}

	// Token: 0x0600000E RID: 14 RVA: 0x000034D0 File Offset: 0x000016D0
	private static string smethod_12()
	{
		foreach (ClientLocale clientLocale in ConfigLoader.GameLocales)
		{
			if (clientLocale == ClientLocale.Russia)
			{
				return clientLocale.ToString();
			}
		}
		return "Outside!";
	}

	// Token: 0x0600000F RID: 15 RVA: 0x00003538 File Offset: 0x00001738
	private static string smethod_13(string[] string_3)
	{
		int num = 0;
		if (num >= string_3.Length)
		{
			return "";
		}
		return string_3[num];
	}

	// Token: 0x06000010 RID: 16 RVA: 0x0000355C File Offset: 0x0000175C
	private static void smethod_14()
	{
		GClass9.string_2 = string.Format("{0}", Convert.ToInt32(GClass6.smethod_2()));
		GClass9.string_3 = string.Format("{0:0.0}%", GClass6.smethod_3());
		GClass9.string_4 = string.Format("{0:N2}MB", (double)GClass6.smethod_4(new DirectoryInfo(string.Format("{0}/Logs", Program.fileInfo_0.Directory)), true) / 1048576.0);
		GClass9.string_5 = string.Format("{0}", ComDiv.CountDB("SELECT COUNT(*) FROM accounts"));
		GClass9.string_6 = string.Format("{0}", ComDiv.CountDB(string.Format("SELECT COUNT(*) FROM accounts WHERE online = '{0}'", true)));
		GClass9.string_7 = string.Format("{0}", ComDiv.CountDB("SELECT COUNT(*) FROM system_clan"));
		GClass9.string_8 = string.Format("{0}", ComDiv.CountDB("SELECT COUNT(*) FROM accounts WHERE pc_cafe = '2' OR pc_cafe = '1'"));
		GClass9.string_9 = string.Format("{0}", ComDiv.CountDB("SELECT COUNT(*) FROM accounts WHERE nickname = ''"));
		GClass9.string_10 = string.Format("{0}", ComDiv.CountDB("SELECT COUNT(*) FROM base_auto_ban"));
		GClass9.string_11 = string.Format("{0}", ComDiv.CountDB("SELECT COUNT(*) FROM system_shop") + ComDiv.CountDB("SELECT COUNT(*) FROM system_shop_effects") + ComDiv.CountDB("SELECT COUNT(*) FROM system_shop_sets"));
		GClass9.string_12 = string.Format("{0}", ComDiv.CountDB(string.Format("SELECT COUNT(*) FROM system_shop WHERE item_visible = '{0}'", false)) + ComDiv.CountDB(string.Format("SELECT COUNT(*) FROM system_shop_effects WHERE coupon_visible = '{0}'", false)));
		GClass9.string_13 = string.Format("{0}", ComDiv.CountDB("SELECT COUNT(*) FROM system_shop_repair"));
		GClass9.string_14 = "V" + ServerConfigJSON.GetConfig(ConfigLoader.ConfigId).ClientVersion;
		GClass9.string_15 = Program.smethod_12();
		GClass9.string_16 = Program.smethod_11();
		GClass9.string_17 = DateTimeUtil.Now("yyyy");
		GClass9.string_18 = string.Format("{0}", ConfigLoader.ConfigId);
		GClass9.string_19 = (ConfigLoader.TournamentRule ? "Enabled" : "Disabled");
		GClass9.string_20 = (ConfigLoader.ICafeSystem ? "Enabled" : "Disabled");
		GClass9.string_21 = (ConfigLoader.AutoAccount ? "Enabled" : "Disabled");
		GClass9.string_22 = (ConfigLoader.AutoBan ? "Enabled" : "Disabled");
	}

	// Token: 0x06000011 RID: 17 RVA: 0x000020CC File Offset: 0x000002CC
	private static void smethod_15(object sender, ConsoleCancelEventArgs e)
	{
		CLogger.Print("Additional Changes were sended to the client.", LoggerType.Info, null);
		GClass13.smethod_0("Attention! \nThe Server Will Be Restarted!");
		e.Cancel = true;
	}

	// Token: 0x06000012 RID: 18 RVA: 0x000020EB File Offset: 0x000002EB
	private static void smethod_16(object sender, UnhandledExceptionEventArgs e)
	{
		CLogger.Print(string.Format("Application Handle Exception Sender: {0} Terminating: {1} {2}", sender, e.IsTerminating, (Exception)e.ExceptionObject), LoggerType.Error, null);
	}

	// Token: 0x06000013 RID: 19 RVA: 0x000037E8 File Offset: 0x000019E8
	private static void smethod_17()
	{
		DateTime dateTime = Program.smethod_19();
		if (dateTime == default(DateTime) || long.Parse(dateTime.ToString("yyMMddHHmmss")) >= 250730235959L)
		{
			GClass11.smethod_3(Program.int_0);
			return;
		}
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00002115 File Offset: 0x00000315
	private static void smethod_18()
	{
		if (!GClass4.smethod_0().Equals("EC2D3196B814D999C060259B0F455B0F3383EB8E"))
		{
			GClass11.smethod_3(Program.int_0);
			return;
		}
	}

	// Token: 0x06000015 RID: 21 RVA: 0x00003834 File Offset: 0x00001A34
	private static DateTime smethod_19()
	{
		DateTime dateTime;
		try
		{
			using (WebResponse response = WebRequest.Create("http://www.google.com").GetResponse())
			{
				dateTime = DateTime.ParseExact(response.Headers["date"], "ddd, dd MMM yyyy HH:mm:ss 'GMT'", CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.AssumeUniversal);
				dateTime = dateTime.ToUniversalTime();
			}
		}
		catch
		{
			dateTime = default(DateTime);
		}
		return dateTime;
	}

	// Token: 0x06000016 RID: 22 RVA: 0x000038B8 File Offset: 0x00001AB8
	private static string smethod_20()
	{
		string text4;
		try
		{
			string text = "http://checkip.dyndns.org";
			WebRequest webRequest = WebRequest.Create(text);
			WebResponse response = webRequest.GetResponse();
			using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
			{
				string text2 = streamReader.ReadToEnd().Trim();
				string[] array = text2.Split(new char[] { ':' });
				string text3 = array[1].Substring(1);
				string[] array2 = text3.Split(new char[] { '<' });
				text4 = array2[0];
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			text4 = "";
		}
		return text4;
	}

	// Token: 0x06000017 RID: 23 RVA: 0x00002133 File Offset: 0x00000333
	public Program()
	{
	}

	// Token: 0x06000018 RID: 24 RVA: 0x00003970 File Offset: 0x00001B70
	// Note: this type is marked as 'beforefieldinit'.
	static Program()
	{
	}

	// Token: 0x04000001 RID: 1
	protected static string string_0 = "";

	// Token: 0x04000002 RID: 2
	protected static string string_1 = "RUS";

	// Token: 0x04000003 RID: 3
	protected static string string_2 = "V3.80";

	// Token: 0x04000004 RID: 4
	protected static Mutex mutex_0 = null;

	// Token: 0x04000005 RID: 5
	protected static readonly FileInfo fileInfo_0 = new FileInfo(Assembly.GetExecutingAssembly().Location);

	// Token: 0x04000006 RID: 6
	protected static readonly int int_0 = Process.GetCurrentProcess().Id;

	// Token: 0x02000003 RID: 3
	// (Invoke) Token: 0x0600001A RID: 26
	private delegate bool Delegate0(Program.Enum0 sig);

	// Token: 0x02000004 RID: 4
	private enum Enum0
	{
		// Token: 0x04000008 RID: 8
		CTRL_C_EVENT,
		// Token: 0x04000009 RID: 9
		CTRL_BREAK_EVENT,
		// Token: 0x0400000A RID: 10
		CTRL_CLOSE_EVENT,
		// Token: 0x0400000B RID: 11
		CTRL_LOGOFF_EVENT = 5,
		// Token: 0x0400000C RID: 12
		CTRL_SHUTDOWN_EVENT
	}

	// Token: 0x02000005 RID: 5
	[CompilerGenerated]
	[Serializable]
	private sealed class Class0
	{
		// Token: 0x0600001D RID: 29 RVA: 0x0000213B File Offset: 0x0000033B
		// Note: this type is marked as 'beforefieldinit'.
		static Class0()
		{
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002133 File Offset: 0x00000333
		public Class0()
		{
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002147 File Offset: 0x00000347
		internal void method_0()
		{
			Program.smethod_10();
		}

		// Token: 0x0400000D RID: 13
		public static readonly Program.Class0 <>9 = new Program.Class0();

		// Token: 0x0400000E RID: 14
		public static ThreadStart <>9__10_0;
	}

	// Token: 0x02000006 RID: 6
	[CompilerGenerated]
	[StructLayout(LayoutKind.Auto)]
	private struct Struct0 : IAsyncStateMachine
	{
		// Token: 0x06000020 RID: 32 RVA: 0x000039C4 File Offset: 0x00001BC4
		void IAsyncStateMachine.MoveNext()
		{
			int num = this.int_0;
			try
			{
				TaskAwaiter awaiter;
				if (num == 0)
				{
					awaiter = this.taskAwaiter_0;
					this.taskAwaiter_0 = default(TaskAwaiter);
					this.int_0 = -1;
					goto IL_163;
				}
				IL_0D:
				Program.smethod_14();
				double num2 = GClass6.smethod_2();
				double num3 = GClass6.smethod_3();
				int num4 = ComDiv.CountDB("SELECT COUNT(*) FROM accounts");
				int num5 = ComDiv.CountDB(string.Format("SELECT COUNT(*) FROM accounts WHERE online = {0}", true));
				Plugin.Core.Colorful.Console.Title = string.Concat(new string[]
				{
					"Point Blank (",
					Program.string_1,
					"-",
					Program.string_2,
					") Server ",
					string_3,
					" </> ",
					(int_1 == 1) ? string.Format("RAM Usages: {0:0.0} MB)", num2) : string.Format("Users: {0}; Online: {1}; RAM Usages: {2:0.0} MB ({3:0.0}%)", new object[] { num4, num5, num2, num3 }),
					" -",
					Program.string_0,
					" </> Timeline: ",
					DateTimeUtil.Now("dddd, MMMM dd, yyyy - HH:mm:ss")
				});
				Program.smethod_9();
				awaiter = Task.Delay(1000).GetAwaiter();
				if (!awaiter.IsCompleted)
				{
					this.int_0 = 0;
					this.taskAwaiter_0 = awaiter;
					this.asyncVoidMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter, Program.Struct0>(ref awaiter, ref this);
					return;
				}
				IL_163:
				awaiter.GetResult();
				goto IL_0D;
			}
			catch (Exception ex)
			{
				this.int_0 = -2;
				this.asyncVoidMethodBuilder_0.SetException(ex);
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000214E File Offset: 0x0000034E
		[DebuggerHidden]
		void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
		{
			this.asyncVoidMethodBuilder_0.SetStateMachine(stateMachine);
		}

		// Token: 0x0400000F RID: 15
		public int int_0;

		// Token: 0x04000010 RID: 16
		public AsyncVoidMethodBuilder asyncVoidMethodBuilder_0;

		// Token: 0x04000011 RID: 17
		public string string_0;

		// Token: 0x04000012 RID: 18
		public int int_1;

		// Token: 0x04000013 RID: 19
		private TaskAwaiter taskAwaiter_0;
	}
}
