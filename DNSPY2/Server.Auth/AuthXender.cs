using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.XML;
using Server.Auth.Data.Sync;

namespace Server.Auth
{
	// Token: 0x02000006 RID: 6
	public class AuthXender
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000242E File Offset: 0x0000062E
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002435 File Offset: 0x00000635
		public static AuthSync Sync
		{
			[CompilerGenerated]
			get
			{
				return AuthXender.authSync_0;
			}
			[CompilerGenerated]
			set
			{
				AuthXender.authSync_0 = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000243D File Offset: 0x0000063D
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00002444 File Offset: 0x00000644
		public static AuthManager Client
		{
			[CompilerGenerated]
			get
			{
				return AuthXender.authManager_0;
			}
			[CompilerGenerated]
			set
			{
				AuthXender.authManager_0 = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000244C File Offset: 0x0000064C
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002453 File Offset: 0x00000653
		public static ConcurrentDictionary<int, AuthClient> SocketSessions
		{
			[CompilerGenerated]
			get
			{
				return AuthXender.concurrentDictionary_0;
			}
			[CompilerGenerated]
			set
			{
				AuthXender.concurrentDictionary_0 = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000034 RID: 52 RVA: 0x0000245B File Offset: 0x0000065B
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002462 File Offset: 0x00000662
		public static ConcurrentDictionary<string, int> SocketConnections
		{
			[CompilerGenerated]
			get
			{
				return AuthXender.concurrentDictionary_1;
			}
			[CompilerGenerated]
			set
			{
				AuthXender.concurrentDictionary_1 = value;
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003E20 File Offset: 0x00002020
		public static bool GetPlugin(string Host, int Port)
		{
			bool flag;
			try
			{
				AuthXender.SocketSessions = new ConcurrentDictionary<int, AuthClient>();
				AuthXender.SocketConnections = new ConcurrentDictionary<string, int>();
				AuthXender.Sync = new AuthSync(SynchronizeXML.GetServer(Port).Connection);
				AuthXender.Client = new AuthManager(0, Host, ConfigLoader.DEFAULT_PORT[0]);
				AuthXender.Sync.Start();
				AuthXender.Client.Start();
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002409 File Offset: 0x00000609
		public AuthXender()
		{
		}

		// Token: 0x04000018 RID: 24
		[CompilerGenerated]
		private static AuthSync authSync_0;

		// Token: 0x04000019 RID: 25
		[CompilerGenerated]
		private static AuthManager authManager_0;

		// Token: 0x0400001A RID: 26
		[CompilerGenerated]
		private static ConcurrentDictionary<int, AuthClient> concurrentDictionary_0;

		// Token: 0x0400001B RID: 27
		[CompilerGenerated]
		private static ConcurrentDictionary<string, int> concurrentDictionary_1;
	}
}
