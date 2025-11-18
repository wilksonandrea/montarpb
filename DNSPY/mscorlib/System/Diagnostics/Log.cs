using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Diagnostics
{
	// Token: 0x020003F4 RID: 1012
	internal static class Log
	{
		// Token: 0x0600333D RID: 13117 RVA: 0x000C4D09 File Offset: 0x000C2F09
		static Log()
		{
			Log.GlobalSwitch.MinimumLevel = LoggingLevels.ErrorLevel;
		}

		// Token: 0x0600333E RID: 13118 RVA: 0x000C4D48 File Offset: 0x000C2F48
		public static void AddOnLogMessage(LogMessageEventHandler handler)
		{
			object obj = Log.locker;
			lock (obj)
			{
				Log._LogMessageEventHandler = (LogMessageEventHandler)Delegate.Combine(Log._LogMessageEventHandler, handler);
			}
		}

		// Token: 0x0600333F RID: 13119 RVA: 0x000C4D98 File Offset: 0x000C2F98
		public static void RemoveOnLogMessage(LogMessageEventHandler handler)
		{
			object obj = Log.locker;
			lock (obj)
			{
				Log._LogMessageEventHandler = (LogMessageEventHandler)Delegate.Remove(Log._LogMessageEventHandler, handler);
			}
		}

		// Token: 0x06003340 RID: 13120 RVA: 0x000C4DE8 File Offset: 0x000C2FE8
		public static void AddOnLogSwitchLevel(LogSwitchLevelHandler handler)
		{
			object obj = Log.locker;
			lock (obj)
			{
				Log._LogSwitchLevelHandler = (LogSwitchLevelHandler)Delegate.Combine(Log._LogSwitchLevelHandler, handler);
			}
		}

		// Token: 0x06003341 RID: 13121 RVA: 0x000C4E3C File Offset: 0x000C303C
		public static void RemoveOnLogSwitchLevel(LogSwitchLevelHandler handler)
		{
			object obj = Log.locker;
			lock (obj)
			{
				Log._LogSwitchLevelHandler = (LogSwitchLevelHandler)Delegate.Remove(Log._LogSwitchLevelHandler, handler);
			}
		}

		// Token: 0x06003342 RID: 13122 RVA: 0x000C4E90 File Offset: 0x000C3090
		internal static void InvokeLogSwitchLevelHandlers(LogSwitch ls, LoggingLevels newLevel)
		{
			LogSwitchLevelHandler logSwitchLevelHandler = Log._LogSwitchLevelHandler;
			if (logSwitchLevelHandler != null)
			{
				logSwitchLevelHandler(ls, newLevel);
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06003343 RID: 13123 RVA: 0x000C4EB0 File Offset: 0x000C30B0
		// (set) Token: 0x06003344 RID: 13124 RVA: 0x000C4EB9 File Offset: 0x000C30B9
		public static bool IsConsoleEnabled
		{
			get
			{
				return Log.m_fConsoleDeviceEnabled;
			}
			set
			{
				Log.m_fConsoleDeviceEnabled = value;
			}
		}

		// Token: 0x06003345 RID: 13125 RVA: 0x000C4EC3 File Offset: 0x000C30C3
		public static void LogMessage(LoggingLevels level, string message)
		{
			Log.LogMessage(level, Log.GlobalSwitch, message);
		}

		// Token: 0x06003346 RID: 13126 RVA: 0x000C4ED4 File Offset: 0x000C30D4
		public static void LogMessage(LoggingLevels level, LogSwitch logswitch, string message)
		{
			if (logswitch == null)
			{
				throw new ArgumentNullException("LogSwitch");
			}
			if (level < LoggingLevels.TraceLevel0)
			{
				throw new ArgumentOutOfRangeException("level", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			if (logswitch.CheckLevel(level))
			{
				Debugger.Log((int)level, logswitch.strName, message);
				if (Log.m_fConsoleDeviceEnabled)
				{
					Console.Write(message);
				}
			}
		}

		// Token: 0x06003347 RID: 13127 RVA: 0x000C4F2D File Offset: 0x000C312D
		public static void Trace(LogSwitch logswitch, string message)
		{
			Log.LogMessage(LoggingLevels.TraceLevel0, logswitch, message);
		}

		// Token: 0x06003348 RID: 13128 RVA: 0x000C4F38 File Offset: 0x000C3138
		public static void Trace(string switchname, string message)
		{
			LogSwitch @switch = LogSwitch.GetSwitch(switchname);
			Log.LogMessage(LoggingLevels.TraceLevel0, @switch, message);
		}

		// Token: 0x06003349 RID: 13129 RVA: 0x000C4F54 File Offset: 0x000C3154
		public static void Trace(string message)
		{
			Log.LogMessage(LoggingLevels.TraceLevel0, Log.GlobalSwitch, message);
		}

		// Token: 0x0600334A RID: 13130 RVA: 0x000C4F62 File Offset: 0x000C3162
		public static void Status(LogSwitch logswitch, string message)
		{
			Log.LogMessage(LoggingLevels.StatusLevel0, logswitch, message);
		}

		// Token: 0x0600334B RID: 13131 RVA: 0x000C4F70 File Offset: 0x000C3170
		public static void Status(string switchname, string message)
		{
			LogSwitch @switch = LogSwitch.GetSwitch(switchname);
			Log.LogMessage(LoggingLevels.StatusLevel0, @switch, message);
		}

		// Token: 0x0600334C RID: 13132 RVA: 0x000C4F8D File Offset: 0x000C318D
		public static void Status(string message)
		{
			Log.LogMessage(LoggingLevels.StatusLevel0, Log.GlobalSwitch, message);
		}

		// Token: 0x0600334D RID: 13133 RVA: 0x000C4F9C File Offset: 0x000C319C
		public static void Warning(LogSwitch logswitch, string message)
		{
			Log.LogMessage(LoggingLevels.WarningLevel, logswitch, message);
		}

		// Token: 0x0600334E RID: 13134 RVA: 0x000C4FA8 File Offset: 0x000C31A8
		public static void Warning(string switchname, string message)
		{
			LogSwitch @switch = LogSwitch.GetSwitch(switchname);
			Log.LogMessage(LoggingLevels.WarningLevel, @switch, message);
		}

		// Token: 0x0600334F RID: 13135 RVA: 0x000C4FC5 File Offset: 0x000C31C5
		public static void Warning(string message)
		{
			Log.LogMessage(LoggingLevels.WarningLevel, Log.GlobalSwitch, message);
		}

		// Token: 0x06003350 RID: 13136 RVA: 0x000C4FD4 File Offset: 0x000C31D4
		public static void Error(LogSwitch logswitch, string message)
		{
			Log.LogMessage(LoggingLevels.ErrorLevel, logswitch, message);
		}

		// Token: 0x06003351 RID: 13137 RVA: 0x000C4FE0 File Offset: 0x000C31E0
		public static void Error(string switchname, string message)
		{
			LogSwitch @switch = LogSwitch.GetSwitch(switchname);
			Log.LogMessage(LoggingLevels.ErrorLevel, @switch, message);
		}

		// Token: 0x06003352 RID: 13138 RVA: 0x000C4FFD File Offset: 0x000C31FD
		public static void Error(string message)
		{
			Log.LogMessage(LoggingLevels.ErrorLevel, Log.GlobalSwitch, message);
		}

		// Token: 0x06003353 RID: 13139 RVA: 0x000C500C File Offset: 0x000C320C
		public static void Panic(string message)
		{
			Log.LogMessage(LoggingLevels.PanicLevel, Log.GlobalSwitch, message);
		}

		// Token: 0x06003354 RID: 13140
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void AddLogSwitch(LogSwitch logSwitch);

		// Token: 0x06003355 RID: 13141
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ModifyLogSwitch(int iNewLevel, string strSwitchName, string strParentName);

		// Token: 0x040016B6 RID: 5814
		internal static Hashtable m_Hashtable = new Hashtable();

		// Token: 0x040016B7 RID: 5815
		private static volatile bool m_fConsoleDeviceEnabled = false;

		// Token: 0x040016B8 RID: 5816
		private static LogMessageEventHandler _LogMessageEventHandler;

		// Token: 0x040016B9 RID: 5817
		private static volatile LogSwitchLevelHandler _LogSwitchLevelHandler;

		// Token: 0x040016BA RID: 5818
		private static object locker = new object();

		// Token: 0x040016BB RID: 5819
		public static readonly LogSwitch GlobalSwitch = new LogSwitch("Global", "Global Switch for this log");
	}
}
