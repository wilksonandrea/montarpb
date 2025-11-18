using System;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.XML;
using Server.Game.Data.Sync;
using Server.Game.Network.ServerPacket;

namespace Server.Game
{
	// Token: 0x02000006 RID: 6
	public class GameXender
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000034 RID: 52 RVA: 0x00002604 File Offset: 0x00000804
		// (set) Token: 0x06000035 RID: 53 RVA: 0x0000260B File Offset: 0x0000080B
		public static GameSync Sync
		{
			[CompilerGenerated]
			get
			{
				return GameXender.gameSync_0;
			}
			[CompilerGenerated]
			set
			{
				GameXender.gameSync_0 = value;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002613 File Offset: 0x00000813
		// (set) Token: 0x06000037 RID: 55 RVA: 0x0000261A File Offset: 0x0000081A
		public static GameManager Client
		{
			[CompilerGenerated]
			get
			{
				return GameXender.gameManager_0;
			}
			[CompilerGenerated]
			set
			{
				GameXender.gameManager_0 = value;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002622 File Offset: 0x00000822
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002629 File Offset: 0x00000829
		public static ConcurrentDictionary<int, GameClient> SocketSessions
		{
			[CompilerGenerated]
			get
			{
				return GameXender.concurrentDictionary_0;
			}
			[CompilerGenerated]
			set
			{
				GameXender.concurrentDictionary_0 = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002631 File Offset: 0x00000831
		// (set) Token: 0x0600003B RID: 59 RVA: 0x00002638 File Offset: 0x00000838
		public static ConcurrentDictionary<string, int> SocketConnections
		{
			[CompilerGenerated]
			get
			{
				return GameXender.concurrentDictionary_1;
			}
			[CompilerGenerated]
			set
			{
				GameXender.concurrentDictionary_1 = value;
			}
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000087E0 File Offset: 0x000069E0
		public static bool GetPlugin(int ServerId, string Host, int Port)
		{
			bool flag;
			try
			{
				GameXender.SocketSessions = new ConcurrentDictionary<int, GameClient>();
				GameXender.SocketConnections = new ConcurrentDictionary<string, int>();
				GameXender.Sync = new GameSync(SynchronizeXML.GetServer(Port).Connection);
				int[] default_PORT = ConfigLoader.DEFAULT_PORT;
				int num = 1;
				int num2 = default_PORT[num];
				default_PORT[num] = num2 + 1;
				GameXender.Client = new GameManager(ServerId, Host, num2);
				GameXender.Sync.Start();
				GameXender.Client.Start();
				flag = true;
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00008874 File Offset: 0x00006A74
		public static void UpdateEvents()
		{
			foreach (GameClient gameClient in GameXender.SocketSessions.Values)
			{
				if (gameClient != null)
				{
					gameClient.SendPacket(new PROTOCOL_BASE_EVENT_PORTAL_ACK(true));
				}
			}
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000088D0 File Offset: 0x00006AD0
		public static void UpdateShop()
		{
			foreach (GameClient gameClient in GameXender.SocketSessions.Values)
			{
				if (gameClient != null)
				{
					foreach (ShopData shopData in ShopManager.ShopDataItems)
					{
						gameClient.SendPacket(new PROTOCOL_AUTH_SHOP_ITEMLIST_ACK(shopData, ShopManager.TotalItems));
					}
					foreach (ShopData shopData2 in ShopManager.ShopDataGoods)
					{
						gameClient.SendPacket(new PROTOCOL_AUTH_SHOP_GOODSLIST_ACK(shopData2, ShopManager.TotalGoods));
					}
					foreach (ShopData shopData3 in ShopManager.ShopDataItemRepairs)
					{
						gameClient.SendPacket(new PROTOCOL_AUTH_SHOP_REPAIRLIST_ACK(shopData3, ShopManager.TotalRepairs));
					}
					foreach (ShopData shopData4 in BattleBoxXML.ShopDataBattleBoxes)
					{
						gameClient.SendPacket(new PROTOCOL_BATTLEBOX_GET_LIST_ACK(shopData4, BattleBoxXML.TotalBoxes));
					}
					gameClient.SendPacket(new PROTOCOL_SHOP_TAG_INFO_ACK());
					gameClient.SendPacket(new PROTOCOL_SHOP_GET_SAILLIST_ACK(true));
				}
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000025DF File Offset: 0x000007DF
		public GameXender()
		{
		}

		// Token: 0x04000019 RID: 25
		[CompilerGenerated]
		private static GameSync gameSync_0;

		// Token: 0x0400001A RID: 26
		[CompilerGenerated]
		private static GameManager gameManager_0;

		// Token: 0x0400001B RID: 27
		[CompilerGenerated]
		private static ConcurrentDictionary<int, GameClient> concurrentDictionary_0;

		// Token: 0x0400001C RID: 28
		[CompilerGenerated]
		private static ConcurrentDictionary<string, int> concurrentDictionary_1;
	}
}
