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
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

public class Program
{
    protected static string string_0 = "";
    protected static string string_1 = "RUS";
    protected static string string_2 = "V3.80";
    protected static Mutex mutex_0 = null;
    protected static readonly FileInfo fileInfo_0 = new FileInfo(Assembly.GetExecutingAssembly().Location);
    protected static readonly int int_0 = Process.GetCurrentProcess().Id;

    [STAThread]
    public static void Main(string[] string_3)
    {
        AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.smethod_16);
        Plugin.Core.Colorful.Console.CancelKeyPress += new ConsoleCancelEventHandler(Program.smethod_15);
        try
        {
            Plugin.Core.Colorful.Console.SetWindowSize(160, 40);
            Plugin.Core.Colorful.Console.CursorVisible = false;
            Plugin.Core.Colorful.Console.TreatControlCAsInput = false;
            GClass11.smethod_2();
            string fileVersion = FileVersionInfo.GetVersionInfo(fileInfo_0.Name).FileVersion;
            string[] textArray1 = new string[] { "Point Blank (", string_1, "-", string_2, ") Server ", fileVersion };
            Plugin.Core.Colorful.Console.Title = string.Concat(textArray1);
            Plugin.Core.Colorful.Console.Clear();
            Process[] processesByName = Process.GetProcessesByName("PointBlank");
            int index = 0;
            while (true)
            {
                if (index >= processesByName.Length)
                {
                    bool flag;
                    mutex_0 = new Mutex(true, fileInfo_0.Name, out flag);
                    if (!flag)
                    {
                        CLogger.Print("The server is already running! Exiting...", LoggerType.Warning, null);
                        MessageBox.Show("The server is already running!", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
                        Thread.Sleep(0x3e8);
                        GClass11.smethod_3(int_0);
                    }
                    else
                    {
                        string_0 = GClass6.smethod_0() ? "ADMINISTRATOR" : "SUPERUSER";
                        DateTime lastWriteTime = fileInfo_0.LastWriteTime;
                        StringBuilder builder = new StringBuilder();
                        builder.AppendLine("__________                 ______      _____");
                        builder.AppendLine("___  ____/______ _______  ____  /_____ __  /______________");
                        builder.AppendLine(@"__  __/  __  __ `__ \  / / /_  /_  __ `/  __/  __ \_  ___/");
                        builder.AppendLine("_  /___  _  / / / / / /_/ /_  / / /_/ // /_ / /_/ /  /");
                        builder.AppendLine(@"/_____/  /_/ /_/ /_/\__,_/ /_/  \__,_/ \__/ \____//_/");
                        builder.AppendLine("Contributors{0} {1}");
                        builder.AppendLine("These Project is Made by Love. {2}");
                        builder.AppendLine("Regards{0} {3}");
                        builder.AppendLine("");
                        builder.AppendLine(smethod_2("MoMz Games"));
                        builder.AppendLine(smethod_2($"{string_2} +Build: {lastWriteTime:yyMMddHHmmss}"));
                        builder.AppendLine(smethod_2("Copyright 2021 Zepetto Co. All right reserved"));
                        Plugin.Core.Colorful.Formatter[] args = new Plugin.Core.Colorful.Formatter[] { new Plugin.Core.Colorful.Formatter(":", ColorUtil.Yellow), new Plugin.Core.Colorful.Formatter("Pavel, Monester, Fusion, zOne62, Garry", ColorUtil.Cyan), new Plugin.Core.Colorful.Formatter("Not For Sale!", ColorUtil.Red), new Plugin.Core.Colorful.Formatter("PBServer Suite Dev Team", ColorUtil.Green) };
                        Plugin.Core.Colorful.Console.WriteLineFormatted($"{builder}", ColorUtil.White, args);
                        bool flag2 = smethod_13(string_3).Equals("-supc");
                        if (flag2)
                        {
                            new Thread(Class0.<>9__10_0 ??= new ThreadStart(Class0.<>9.method_0)).Start();
                        }
                        smethod_3((int) flag2, fileVersion);
                    }
                    break;
                }
                processesByName[index].Kill();
                index++;
            }
        }
        catch (Exception exception)
        {
            CLogger.Print(exception.Message, LoggerType.Error, exception);
        }
        finally
        {
            mutex_0.ReleaseMutex();
            Process.GetCurrentProcess().WaitForExit();
        }
    }

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
        }
        return false;
    }

    private static void smethod_10()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        MainForm mainForm = new MainForm(int_0, new DirectoryInfo($"{fileInfo_0.Directory}/Logs"));
        mainForm.TopMost = true;
        Application.Run(mainForm);
    }

    private static string smethod_11()
    {
        string str = "";
        try
        {
            foreach (IPAddress address in Dns.GetHostEntry(Dns.GetHostName()).AddressList)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    str = address.ToString();
                }
            }
        }
        catch (Exception exception1)
        {
            MessageBox.Show(exception1.Message, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        return str;
    }

    private static string smethod_12()
    {
        string str;
        using (List<ClientLocale>.Enumerator enumerator = ConfigLoader.GameLocales.GetEnumerator())
        {
            while (true)
            {
                if (enumerator.MoveNext())
                {
                    ClientLocale current = enumerator.Current;
                    if (current != ClientLocale.Russia)
                    {
                        continue;
                    }
                    str = current.ToString();
                }
                else
                {
                    return "Outside!";
                }
                break;
            }
        }
        return str;
    }

    private static string smethod_13(string[] string_3)
    {
        string[] strArray = string_3;
        int index = 0;
        return ((index < strArray.Length) ? strArray[index] : "");
    }

    private static void smethod_14()
    {
        GClass9.string_2 = $"{Convert.ToInt32(GClass6.smethod_2())}";
        GClass9.string_3 = $"{GClass6.smethod_3():0.0}%";
        GClass9.string_4 = $"{((double) GClass6.smethod_4(new DirectoryInfo($"{fileInfo_0.Directory}/Logs"), true)) / 1048576.0:N2}MB";
        GClass9.string_5 = $"{ComDiv.CountDB("SELECT COUNT(*) FROM accounts")}";
        GClass9.string_6 = $"{ComDiv.CountDB($"SELECT COUNT(*) FROM accounts WHERE online = '{true}'")}";
        GClass9.string_7 = $"{ComDiv.CountDB("SELECT COUNT(*) FROM system_clan")}";
        GClass9.string_8 = $"{ComDiv.CountDB("SELECT COUNT(*) FROM accounts WHERE pc_cafe = '2' OR pc_cafe = '1'")}";
        GClass9.string_9 = $"{ComDiv.CountDB("SELECT COUNT(*) FROM accounts WHERE nickname = ''")}";
        GClass9.string_10 = $"{ComDiv.CountDB("SELECT COUNT(*) FROM base_auto_ban")}";
        GClass9.string_11 = $"{(ComDiv.CountDB("SELECT COUNT(*) FROM system_shop") + ComDiv.CountDB("SELECT COUNT(*) FROM system_shop_effects")) + ComDiv.CountDB("SELECT COUNT(*) FROM system_shop_sets")}";
        GClass9.string_12 = $"{ComDiv.CountDB($"SELECT COUNT(*) FROM system_shop WHERE item_visible = '{false}'") + ComDiv.CountDB($"SELECT COUNT(*) FROM system_shop_effects WHERE coupon_visible = '{false}'")}";
        GClass9.string_13 = $"{ComDiv.CountDB("SELECT COUNT(*) FROM system_shop_repair")}";
        GClass9.string_14 = "V" + ServerConfigJSON.GetConfig(ConfigLoader.ConfigId).ClientVersion;
        GClass9.string_15 = smethod_12();
        GClass9.string_16 = smethod_11();
        GClass9.string_17 = DateTimeUtil.Now("yyyy");
        GClass9.string_18 = $"{ConfigLoader.ConfigId}";
        GClass9.string_19 = ConfigLoader.TournamentRule ? "Enabled" : "Disabled";
        GClass9.string_20 = ConfigLoader.ICafeSystem ? "Enabled" : "Disabled";
        GClass9.string_21 = ConfigLoader.AutoAccount ? "Enabled" : "Disabled";
        GClass9.string_22 = ConfigLoader.AutoBan ? "Enabled" : "Disabled";
    }

    private static void smethod_15(object sender, ConsoleCancelEventArgs e)
    {
        CLogger.Print("Additional Changes were sended to the client.", LoggerType.Info, null);
        GClass13.smethod_0("Attention! \nThe Server Will Be Restarted!");
        e.Cancel = true;
    }

    private static void smethod_16(object sender, UnhandledExceptionEventArgs e)
    {
        CLogger.Print($"Application Handle Exception Sender: {sender} Terminating: {e.IsTerminating} {(Exception) e.ExceptionObject}", LoggerType.Error, null);
    }

    private static void smethod_17()
    {
        DateTime time = smethod_19();
        DateTime time2 = new DateTime();
        if ((time == time2) || (long.Parse(time.ToString("yyMMddHHmmss")) >= 0x3a60afc837L))
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
        DateTime time;
        try
        {
            using (WebResponse response = WebRequest.Create("http://www.google.com").GetResponse())
            {
                time = DateTime.ParseExact(response.Headers["date"], "ddd, dd MMM yyyy HH:mm:ss 'GMT'", CultureInfo.InvariantCulture.DateTimeFormat, DateTimeStyles.AssumeUniversal).ToUniversalTime();
            }
        }
        catch
        {
            time = new DateTime();
        }
        return time;
    }

    private static string smethod_2(string string_3)
    {
        StringBuilder builder = new StringBuilder(80);
        if (string_3 != null)
        {
            builder.Append(" ");
        }
        int num = 0x4f - string_3.Length;
        while (builder.Length != num)
        {
            builder.Append(" ");
        }
        builder.Append(string_3);
        return $"{builder}";
    }

    private static string smethod_20()
    {
        string str4;
        try
        {
            using (StreamReader reader = new StreamReader(WebRequest.Create("http://checkip.dyndns.org").GetResponse().GetResponseStream()))
            {
                char[] separator = new char[] { ':' };
                char[] chArray2 = new char[] { '<' };
                str4 = reader.ReadToEnd().Trim().Split(separator)[1].Substring(1).Split(chArray2)[0];
            }
        }
        catch (Exception exception)
        {
            CLogger.Print(exception.Message, LoggerType.Error, exception);
            str4 = "";
        }
        return str4;
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
        GClass12.smethod_0(new IPEndPoint(IPAddress.Parse(ConfigLoader.HOST[0]), 0x775));
        CLogger.Print("All Server Plugins Successfully Loaded", LoggerType.Info, null);
        smethod_4("Plugin Status", "Ended");
        smethod_7(smethod_5(), int_1, string_3);
    }

    private static void smethod_4(string string_3, string string_4)
    {
        StringBuilder builder = new StringBuilder(80);
        if (string_3 != null)
        {
            builder.Append("---[").Append(string_3).Append(']');
        }
        string str = (string_4 == null) ? "" : ("[" + string_4 + "]---");
        int num = 0x4f - str.Length;
        while (builder.Length != num)
        {
            builder.Append('-');
        }
        builder.Append(str);
        string local1 = string_4.Equals("Ended") ? $"{builder}
" : $"
{builder}";
        string text1 = local1;
        if (local1 == null)
        {
            string local2 = local1;
            text1 = "";
        }
        Plugin.Core.Colorful.Console.WriteLine(text1);
    }

    private static bool smethod_5() => 
        ComDiv.ValidateAllPlayersAccount() && smethod_6();

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
            foreach (SChannelModel model in SChannelXML.Servers)
            {
                if ((model.Id >= 1) && (model.Port >= ConfigLoader.DEFAULT_PORT[1]))
                {
                    GameXender.GetPlugin(model.Id, ConfigLoader.HOST[0], model.Port);
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
        catch (Exception exception)
        {
            CLogger.Print(exception.Message, LoggerType.Error, exception);
            return false;
        }
    }

    private static void smethod_7(bool bool_0, int int_1, string string_3)
    {
        GClass9.string_0 = string_3;
        GClass9.string_1 = bool_0 ? "SERVER ONLINE" : "SERVER OFFLINE";
        if (!bool_0)
        {
            smethod_4("Server Status", "Begin");
            CLogger.Print("Startup Unsuccessful, Server Runtime " + DateTimeUtil.Now("yyyy"), LoggerType.Warning, null);
            smethod_4("Server Status", "Ended");
            Plugin.Core.Colorful.Console.WriteLine("");
        }
        else
        {
            smethod_4("Server Status", "Begin");
            CLogger.Print("Startup Successful, Server Runtime " + DateTimeUtil.Now("yyyy"), LoggerType.Info, null);
            smethod_4("Server Status", "Ended");
            Plugin.Core.Colorful.Console.WriteLine("");
            try
            {
                smethod_8(int_1, string_3);
            }
            catch (Exception exception)
            {
                CLogger.Print(exception.Message, LoggerType.Error, exception);
            }
        }
    }

    [AsyncStateMachine(typeof(Struct0))]
    private static void smethod_8(int int_1, string string_3)
    {
        Struct0 struct2;
        struct2.asyncVoidMethodBuilder_0 = AsyncVoidMethodBuilder.Create();
        struct2.int_1 = int_1;
        struct2.string_0 = string_3;
        struct2.int_0 = -1;
        struct2.asyncVoidMethodBuilder_0.Start<Struct0>(ref struct2);
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
                    string[] cOLUMNS = new string[10];
                    cOLUMNS[0] = "matches";
                    cOLUMNS[1] = "match_wins";
                    cOLUMNS[2] = "match_loses";
                    cOLUMNS[3] = "match_draws";
                    cOLUMNS[4] = "kills_count";
                    cOLUMNS[5] = "deaths_count";
                    cOLUMNS[6] = "headshots_count";
                    cOLUMNS[7] = "exp_gained";
                    cOLUMNS[8] = "point_gained";
                    cOLUMNS[9] = "playtime";
                    object[] vALUES = new object[10];
                    vALUES[0] = 0;
                    vALUES[1] = 0;
                    vALUES[2] = 0;
                    vALUES[3] = 0;
                    vALUES[4] = 0;
                    vALUES[5] = 0;
                    vALUES[6] = 0;
                    vALUES[7] = 0;
                    vALUES[8] = 0;
                    vALUES[9] = 0;
                    ComDiv.UpdateDB("player_stat_dailies", cOLUMNS, vALUES);
                }
                if (ComDiv.CountDB("SELECT COUNT(*) FROM player_reports") > 0)
                {
                    string[] cOLUMNS = new string[] { "ticket_count" };
                    object[] vALUES = new object[] { 3 };
                    ComDiv.UpdateDB("player_reports", cOLUMNS, vALUES);
                }
                flag = true;
            }
        }
        catch (Exception exception)
        {
            CLogger.Print(exception.Message, LoggerType.Error, exception);
            flag = false;
        }
        return flag;
    }

    [Serializable, CompilerGenerated]
    private sealed class Class0
    {
        public static readonly Program.Class0 <>9 = new Program.Class0();
        public static ThreadStart <>9__10_0;

        internal void method_0()
        {
            Program.smethod_10();
        }
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

    [CompilerGenerated]
    private struct Struct0 : IAsyncStateMachine
    {
        public int int_0;
        public AsyncVoidMethodBuilder asyncVoidMethodBuilder_0;
        public string string_0;
        public int int_1;
        private TaskAwaiter taskAwaiter_0;

        private void MoveNext()
        {
            int num = this.int_0;
            try
            {
                TaskAwaiter awaiter;
                if (num == 0)
                {
                    awaiter = this.taskAwaiter_0;
                    this.taskAwaiter_0 = new TaskAwaiter();
                    this.int_0 = num = -1;
                }
                else
                {
                    goto TR_000A;
                }
            TR_0003:
                awaiter.GetResult();
            TR_000A:
                while (true)
                {
                    string text1;
                    Program.smethod_14();
                    double num2 = GClass6.smethod_2();
                    double num3 = GClass6.smethod_3();
                    int num4 = ComDiv.CountDB("SELECT COUNT(*) FROM accounts");
                    int num5 = ComDiv.CountDB($"SELECT COUNT(*) FROM accounts WHERE online = {true}");
                    string[] textArray1 = new string[12];
                    textArray1[0] = "Point Blank (";
                    textArray1[1] = Program.string_1;
                    textArray1[2] = "-";
                    textArray1[3] = Program.string_2;
                    textArray1[4] = ") Server ";
                    textArray1[5] = this.string_0;
                    textArray1[6] = " </> ";
                    string[] textArray2 = textArray1;
                    if (this.int_1 == 1)
                    {
                        text1 = $"RAM Usages: {num2:0.0} MB)";
                    }
                    else
                    {
                        text1 = $"Users: {num4}; Online: {num5}; RAM Usages: {num2:0.0} MB ({num3:0.0}%)";
                    }
                    textArray1[7] = text1;
                    string[] local1 = textArray1;
                    local1[8] = " -";
                    local1[9] = Program.string_0;
                    local1[10] = " </> Timeline: ";
                    local1[11] = DateTimeUtil.Now("dddd, MMMM dd, yyyy - HH:mm:ss");
                    Plugin.Core.Colorful.Console.Title = string.Concat(local1);
                    Program.smethod_9();
                    awaiter = Task.Delay(0x3e8).GetAwaiter();
                    if (awaiter.IsCompleted)
                    {
                        break;
                    }
                    this.int_0 = num = 0;
                    this.taskAwaiter_0 = awaiter;
                    this.asyncVoidMethodBuilder_0.AwaitUnsafeOnCompleted<TaskAwaiter, Program.Struct0>(ref awaiter, ref this);
                    return;
                }
                goto TR_0003;
            }
            catch (Exception exception)
            {
                this.int_0 = -2;
                this.asyncVoidMethodBuilder_0.SetException(exception);
            }
        }

        [DebuggerHidden]
        private void SetStateMachine(IAsyncStateMachine stateMachine)
        {
            this.asyncVoidMethodBuilder_0.SetStateMachine(stateMachine);
        }
    }
}

