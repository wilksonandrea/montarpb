using System;
using System.Collections.Concurrent;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.XML;
using Server.Match.Data.Sync;

namespace Server.Match
{
	// Token: 0x02000003 RID: 3
	public class MatchXender
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002066 File Offset: 0x00000266
		// (set) Token: 0x06000010 RID: 16 RVA: 0x0000206D File Offset: 0x0000026D
		public static MatchSync Sync
		{
			[CompilerGenerated]
			get
			{
				return MatchXender.matchSync_0;
			}
			[CompilerGenerated]
			set
			{
				MatchXender.matchSync_0 = value;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002075 File Offset: 0x00000275
		// (set) Token: 0x06000012 RID: 18 RVA: 0x0000207C File Offset: 0x0000027C
		public static MatchManager Client
		{
			[CompilerGenerated]
			get
			{
				return MatchXender.matchManager_0;
			}
			[CompilerGenerated]
			set
			{
				MatchXender.matchManager_0 = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002084 File Offset: 0x00000284
		// (set) Token: 0x06000014 RID: 20 RVA: 0x0000208B File Offset: 0x0000028B
		public static ConcurrentDictionary<string, int> SpamConnections
		{
			[CompilerGenerated]
			get
			{
				return MatchXender.concurrentDictionary_0;
			}
			[CompilerGenerated]
			set
			{
				MatchXender.concurrentDictionary_0 = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002093 File Offset: 0x00000293
		// (set) Token: 0x06000016 RID: 22 RVA: 0x0000209A File Offset: 0x0000029A
		public static ConcurrentDictionary<IPEndPoint, Socket> UdpClients
		{
			[CompilerGenerated]
			get
			{
				return MatchXender.concurrentDictionary_1;
			}
			[CompilerGenerated]
			set
			{
				MatchXender.concurrentDictionary_1 = value;
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00005D20 File Offset: 0x00003F20
		public static bool GetPlugin(string Host, int Port)
		{
			bool flag;
			try
			{
				MatchXender.SpamConnections = new ConcurrentDictionary<string, int>();
				MatchXender.UdpClients = new ConcurrentDictionary<IPEndPoint, Socket>();
				MatchXender.Sync = new MatchSync(SynchronizeXML.GetServer(Port).Connection);
				MatchXender.Client = new MatchManager(Host, Port);
				MatchXender.Sync.Start();
				MatchXender.Client.Start();
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000020A2 File Offset: 0x000002A2
		public MatchXender()
		{
		}

		// Token: 0x04000003 RID: 3
		[CompilerGenerated]
		private static MatchSync matchSync_0;

		// Token: 0x04000004 RID: 4
		[CompilerGenerated]
		private static MatchManager matchManager_0;

		// Token: 0x04000005 RID: 5
		[CompilerGenerated]
		private static ConcurrentDictionary<string, int> concurrentDictionary_0;

		// Token: 0x04000006 RID: 6
		[CompilerGenerated]
		private static ConcurrentDictionary<IPEndPoint, Socket> concurrentDictionary_1;
	}
}
