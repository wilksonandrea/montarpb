// Decompiled with JetBrains decompiler
// Type: Program
// Assembly: OSM-Monitor, Version=1.1.2510.3, Culture=neutral, PublicKeyToken=null
// MVID: 7AB73F6A-01A7-48C3-AEBF-9703F61790CD
// Assembly location: C:\Users\Administrator\Desktop\unpack\OSMMonitor-unp.exe

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
using Server.Game;
using Server.Game.Data.Managers;
using Server.Match;
using Server.Match.Data.XML;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

#nullable disable
public class Program
{
  protected static string string_0 = "";
  protected static string string_1 = "RUS";
  protected static string string_2 = "V3.80";
  protected static Mutex mutex_0 = (Mutex) null;
  protected static readonly FileInfo fileInfo_0 = new FileInfo(Assembly.GetExecutingAssembly().Location);
  protected static readonly int int_0 = Process.GetCurrentProcess().Id;

  [DllImport("Kernel32")]
  private static extern bool SetConsoleCtrlHandler(Program.Delegate0 delegate0_0, bool bool_0);

  private static bool smethod_0(Program.Enum0 enum0_0)
  {
    switch (enum0_0)
    {
      case Program.Enum0.CTRL_CLOSE_EVENT:
      case Program.Enum0.CTRL_LOGOFF_EVENT:
      case Program.Enum0.CTRL_SHUTDOWN_EVENT:
        GClass3.smethod_2(Program.fileInfo_0.FullName);
        return true;
      default:
        return false;
    }
  }

  [STAThread]
  public static void Main(string[] string_3)
  {
    AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.smethod_16);
    Plugin.Core.Colorful.Console.CancelKeyPress += new ConsoleCancelEventHandler(Program.smethod_15);
    try
    {
      Plugin.Core.Colorful.Console.SetWindowSize(160 /*0xA0*/, 40);
      Plugin.Core.Colorful.Console.CursorVisible = false;
      Plugin.Core.Colorful.Console.TreatControlCAsInput = false;
      GClass11.smethod_2();
      string fileVersion = FileVersionInfo.GetVersionInfo(Program.fileInfo_0.Name).FileVersion;
      Plugin.Core.Colorful.Console.Title = $"Point Blank ({Program.string_1}-{Program.string_2}) Server {fileVersion}";
      Plugin.Core.Colorful.Console.Clear();
      foreach (Process process in Process.GetProcessesByName("PointBlank"))
        process.Kill();
      bool createdNew;
      Program.mutex_0 = new Mutex(true, Program.fileInfo_0.Name, out createdNew);
      if (!createdNew)
      {
        CLogger.Print("The server is already running! Exiting...", LoggerType.Warning, (Exception) null);
        int num = (int) MessageBox.Show("The server is already running!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
        Thread.Sleep(1000);
        GClass11.smethod_3(Program.int_0);
      }
      else
      {
        Program.string_0 = GClass6.smethod_0() ? "ADMINISTRATOR" : "SUPERUSER";
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
        stringBuilder.AppendLine(Program.smethod_2($"{Program.string_2} +Build: {lastWriteTime:yyMMddHHmmss}"));
        stringBuilder.AppendLine(Program.smethod_2("Copyright 2021 Zepetto Co. All right reserved"));
        Formatter[] formatterArray = new Formatter[4]
        {
          new Formatter((object) ":", ColorUtil.Yellow),
          new Formatter((object) "Pavel, Monester, Fusion, zOne62, Garry", ColorUtil.Cyan),
          new Formatter((object) "Not For Sale!", ColorUtil.Red),
          new Formatter((object) "PBServer Suite Dev Team", ColorUtil.Green)
        };
        Plugin.Core.Colorful.Console.WriteLineFormatted($"{stringBuilder}", ColorUtil.White, formatterArray);
        bool flag = Program.smethod_13(string_3).Equals("-supc");
        if (flag)
          new Thread((ThreadStart) (() => Program.smethod_10())).Start();
        Program.smethod_3(flag ? 1 : 0, fileVersion);
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

  private static string smethod_2(string string_3)
  {
    StringBuilder stringBuilder = new StringBuilder(80 /*0x50*/);
    if (string_3 != null)
      stringBuilder.Append(" ");
    int num = 79 - string_3.Length;
    while (stringBuilder.Length != num)
      stringBuilder.Append(" ");
    stringBuilder.Append(string_3);
    return $"{stringBuilder}";
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
    CLogger.Print("All Server Plugins Successfully Loaded", LoggerType.Info, (Exception) null);
    Program.smethod_4("Plugin Status", "Ended");
    Program.smethod_7(Program.smethod_5(), int_1, string_3);
  }

  private static void smethod_4(string string_3, string string_4)
  {
    StringBuilder stringBuilder = new StringBuilder(80 /*0x50*/);
    if (string_3 != null)
      stringBuilder.Append("---[").Append(string_3).Append(']');
    string str = string_4 == null ? "" : $"[{string_4}]---";
    int num = 79 - str.Length;
    while (stringBuilder.Length != num)
      stringBuilder.Append('-');
    stringBuilder.Append(str);
    Plugin.Core.Colorful.Console.WriteLine((string_4.Equals("Ended") ? $"{stringBuilder}\n" : $"\n{stringBuilder}") ?? "");
  }

  private static bool smethod_5() => ComDiv.ValidateAllPlayersAccount() && Program.smethod_6();

  private static bool smethod_6()
  {
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
        if (server.Id >= 1 && (int) server.Port >= ConfigLoader.DEFAULT_PORT[1])
          GameXender.GetPlugin(server.Id, ConfigLoader.HOST[0], (int) server.Port);
      }
      Program.smethod_4("Game Server", "Ended");
      Program.smethod_4("Battle Server", "Begin");
      MapStructureXML.Load();
      CharaStructureXML.Load();
      ItemStatisticXML.Load();
      MatchXender.GetPlugin(ConfigLoader.HOST[0], ConfigLoader.DEFAULT_PORT[2]);
      Program.smethod_4("Battle Server", "Ended");
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
    GClass9.string_1 = bool_0 ? "SERVER ONLINE" : "SERVER OFFLINE";
    if (bool_0)
    {
      Program.smethod_4("Server Status", "Begin");
      CLogger.Print("Startup Successful, Server Runtime " + DateTimeUtil.Now("yyyy"), LoggerType.Info, (Exception) null);
      Program.smethod_4("Server Status", "Ended");
      Plugin.Core.Colorful.Console.WriteLine("");
      try
      {
        Program.smethod_8(int_1, string_3);
      }
      catch (Exception ex)
      {
        CLogger.Print(ex.Message, LoggerType.Error, ex);
      }
    }
    else
    {
      Program.smethod_4("Server Status", "Begin");
      CLogger.Print("Startup Unsuccessful, Server Runtime " + DateTimeUtil.Now("yyyy"), LoggerType.Warning, (Exception) null);
      Program.smethod_4("Server Status", "Ended");
      Plugin.Core.Colorful.Console.WriteLine("");
    }
  }

  private static async void smethod_8(int int_1, string string_3)
  {
    while (true)
    {
      Program.smethod_14();
      double num1 = GClass6.smethod_2();
      double num2 = GClass6.smethod_3();
      int num3 = ComDiv.CountDB("SELECT COUNT(*) FROM accounts");
      int num4 = ComDiv.CountDB($"SELECT COUNT(*) FROM accounts WHERE online = {(ValueType) true}");
      string[] strArray = new string[12]
      {
        "Point Blank (",
        Program.string_1,
        "-",
        Program.string_2,
        ") Server ",
        string_3,
        " </> ",
        null,
        null,
        null,
        null,
        null
      };
      string str;
      if (int_1 != 1)
        str = $"Users: {num3}; Online: {num4}; RAM Usages: {num1:0.0} MB ({num2:0.0}%)";
      else
        str = $"RAM Usages: {num1:0.0} MB)";
      strArray[7] = str;
      strArray[8] = " -";
      strArray[9] = Program.string_0;
      strArray[10] = " </> Timeline: ";
      strArray[11] = DateTimeUtil.Now("dddd, MMMM dd, yyyy - HH:mm:ss");
      Plugin.Core.Colorful.Console.Title = string.Concat(strArray);
      Program.smethod_9();
      await Task.Delay(1000);
    }
  }

  private static bool smethod_9()
  {
    try
    {
      if (!DateTimeUtil.Now("HHmmss").Equals("000000"))
        return false;
      if (ComDiv.CountDB("SELECT COUNT(*) FROM player_stat_dailies") > 0)
        ComDiv.UpdateDB("player_stat_dailies", new string[10]
        {
          "matches",
          "match_wins",
          "match_loses",
          "match_draws",
          "kills_count",
          "deaths_count",
          "headshots_count",
          "exp_gained",
          "point_gained",
          "playtime"
        }, new object[10]
        {
          (object) 0,
          (object) 0,
          (object) 0,
          (object) 0,
          (object) 0,
          (object) 0,
          (object) 0,
          (object) 0,
          (object) 0,
          (object) 0
        });
      if (ComDiv.CountDB("SELECT COUNT(*) FROM player_reports") > 0)
        ComDiv.UpdateDB("player_reports", new string[1]
        {
          "ticket_count"
        }, new object[1]{ (object) 3 });
      return true;
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
    Application.SetCompatibleTextRenderingDefault(false);
    MainForm mainForm = new MainForm(Program.int_0, new DirectoryInfo($"{Program.fileInfo_0.Directory}/Logs"));
    mainForm.TopMost = true;
    Application.Run((Form) mainForm);
  }

  private static string smethod_11()
  {
    string str = "";
    try
    {
      foreach (IPAddress address in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
      {
        if (address.AddressFamily == AddressFamily.InterNetwork)
          str = address.ToString();
      }
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
    }
    return str;
  }

  private static string smethod_12()
  {
    foreach (ClientLocale gameLocale in ConfigLoader.GameLocales)
    {
      if (gameLocale == ClientLocale.Russia)
        return gameLocale.ToString();
    }
    return "Outside!";
  }

  private static string smethod_13(string[] string_3)
  {
    string[] strArray = string_3;
    int index = 0;
    return index < strArray.Length ? strArray[index] : "";
  }

  private static void smethod_14()
  {
    GClass9.string_2 = $"{Convert.ToInt32(GClass6.smethod_2())}";
    GClass9.string_3 = $"{GClass6.smethod_3():0.0}%";
    GClass9.string_4 = $"{(double) GClass6.smethod_4(new DirectoryInfo($"{Program.fileInfo_0.Directory}/Logs"), true) / 1048576.0:N2}MB";
    GClass9.string_5 = $"{ComDiv.CountDB("SELECT COUNT(*) FROM accounts")}";
    GClass9.string_6 = $"{ComDiv.CountDB($"SELECT COUNT(*) FROM accounts WHERE online = '{(ValueType) true}'")}";
    GClass9.string_7 = $"{ComDiv.CountDB("SELECT COUNT(*) FROM system_clan")}";
    GClass9.string_8 = $"{ComDiv.CountDB("SELECT COUNT(*) FROM accounts WHERE pc_cafe = '2' OR pc_cafe = '1'")}";
    GClass9.string_9 = $"{ComDiv.CountDB("SELECT COUNT(*) FROM accounts WHERE nickname = ''")}";
    GClass9.string_10 = $"{ComDiv.CountDB("SELECT COUNT(*) FROM base_auto_ban")}";
    GClass9.string_11 = $"{ComDiv.CountDB("SELECT COUNT(*) FROM system_shop") + ComDiv.CountDB("SELECT COUNT(*) FROM system_shop_effects") + ComDiv.CountDB("SELECT COUNT(*) FROM system_shop_sets")}";
    GClass9.string_12 = $"{ComDiv.CountDB($"SELECT COUNT(*) FROM system_shop WHERE item_visible = '{(ValueType) false}'") + ComDiv.CountDB($"SELECT COUNT(*) FROM system_shop_effects WHERE coupon_visible = '{(ValueType) false}'")}";
    GClass9.string_13 = $"{ComDiv.CountDB("SELECT COUNT(*) FROM system_shop_repair")}";
    GClass9.string_14 = "V" + ServerConfigJSON.GetConfig(ConfigLoader.ConfigId).ClientVersion;
    GClass9.string_15 = Program.smethod_12();
    GClass9.string_16 = Program.smethod_11();
    GClass9.string_17 = DateTimeUtil.Now("yyyy");
    GClass9.string_18 = $"{ConfigLoader.ConfigId}";
    GClass9.string_19 = ConfigLoader.TournamentRule ? "Enabled" : "Disabled";
    GClass9.string_20 = ConfigLoader.ICafeSystem ? "Enabled" : "Disabled";
    GClass9.string_21 = ConfigLoader.AutoAccount ? "Enabled" : "Disabled";
    GClass9.string_22 = ConfigLoader.AutoBan ? "Enabled" : "Disabled";
  }

  private static void smethod_15(object sender, ConsoleCancelEventArgs e)
  {
    CLogger.Print("Additional Changes were sended to the client.", LoggerType.Info, (Exception) null);
    GClass13.smethod_0("Attention! \nThe Server Will Be Restarted!");
    e.Cancel = true;
  }

  private static void smethod_16(object sender, UnhandledExceptionEventArgs e)
  {
    CLogger.Print($"Application Handle Exception Sender: {sender} Terminating: {e.IsTerminating} {(Exception) e.ExceptionObject}", LoggerType.Error, (Exception) null);
  }

  private static void smethod_17()
  {
    DateTime dateTime = Program.smethod_19();
    if (!(dateTime == new DateTime()) && long.Parse(dateTime.ToString("yyMMddHHmmss")) < 250730235959L)
      return;
    GClass11.smethod_3(Program.int_0);
  }

  private static void smethod_18()
  {
    if (GClass4.smethod_0().Equals("EC2D3196B814D999C060259B0F455B0F3383EB8E"))
      return;
    GClass11.smethod_3(Program.int_0);
  }

  private static DateTime smethod_19()
  {
    try
    {
      using (WebResponse response = WebRequest.Create("http://www.google.com").GetResponse())
      {
        DateTime dateTime = DateTime.ParseExact(response.Headers["date"], "ddd, dd MMM yyyy HH:mm:ss 'GMT'", (IFormatProvider) CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.AssumeUniversal);
        dateTime = dateTime.ToUniversalTime();
        return dateTime;
      }
    }
    catch
    {
      return new DateTime();
    }
  }

  private static string smethod_20()
  {
    try
    {
      using (StreamReader streamReader = new StreamReader(WebRequest.Create("http://checkip.dyndns.org").GetResponse().GetResponseStream()))
        return streamReader.ReadToEnd().Trim().Split(':')[1].Substring(1).Split('<')[0];
    }
    catch (Exception ex)
    {
      CLogger.Print(ex.Message, LoggerType.Error, ex);
      return "";
    }
  }

  private delegate bool Delegate0(Program.Enum0 sig);

  private enum Enum0
  {
    CTRL_C_EVENT = 0,
    CTRL_BREAK_EVENT = 1,
    CTRL_CLOSE_EVENT = 2,
    CTRL_LOGOFF_EVENT = 5,
    CTRL_SHUTDOWN_EVENT = 6,
  }
}
