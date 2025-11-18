using System;
using System.Net.Sockets;
using System.Threading;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Network;
using Plugin.Core.XML;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Data.Sync.Client
{
	// Token: 0x02000200 RID: 512
	public class ServerWarning
	{
		// Token: 0x060005F4 RID: 1524 RVA: 0x00030FA8 File Offset: 0x0002F1A8
		public static void LoadGMWarning(SyncClientPacket C)
		{
			string text = C.ReadS((int)C.ReadC());
			string text2 = C.ReadS((int)C.ReadC());
			string text3 = C.ReadS((int)C.ReadH());
			Account accountDB = AccountManager.GetAccountDB(text, 0, 31);
			if (accountDB != null && accountDB.Password == text2 && accountDB.Access >= AccessLevel.GAMEMASTER)
			{
				int num = 0;
				using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK protocol_SERVER_MESSAGE_ANNOUNCE_ACK = new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(text3))
				{
					num = GameXender.Client.SendPacketToAllClients(protocol_SERVER_MESSAGE_ANNOUNCE_ACK);
				}
				CLogger.Print(string.Format("Message sent to '{0}' Players: '{1}'; by Username: '{2}'", num, text3, text), LoggerType.Command, null);
			}
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00031054 File Offset: 0x0002F254
		public static void LoadShopRestart(SyncClientPacket C)
		{
			int num = (int)C.ReadC();
			ShopManager.Reset();
			ShopManager.Load(num);
			CLogger.Print(string.Format("Shop restarted. (Type: {0})", num), LoggerType.Command, null);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x0003108C File Offset: 0x0002F28C
		public static void LoadServerUpdate(SyncClientPacket C)
		{
			int num = (int)C.ReadC();
			SChannelXML.UpdateServer(num);
			CLogger.Print(string.Format("Server updated. (Id: {0})", num), LoggerType.Command, null);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x000310C0 File Offset: 0x0002F2C0
		public static void LoadShutdown(SyncClientPacket C)
		{
			string text = C.ReadS((int)C.ReadC());
			string text2 = C.ReadS((int)C.ReadC());
			Account accountDB = AccountManager.GetAccountDB(text, 0, 31);
			if (accountDB != null && accountDB.Password == text2 && accountDB.Access >= AccessLevel.GAMEMASTER)
			{
				int num = 0;
				foreach (GameClient gameClient in GameXender.SocketSessions.Values)
				{
					gameClient.Client.Shutdown(SocketShutdown.Both);
					gameClient.Client.Close(10000);
					num++;
				}
				CLogger.Print(string.Format("Disconnected Players: {0} (By: {1})", num, text), LoggerType.Warning, null);
				GameXender.Client.ServerIsClosed = true;
				GameXender.Client.MainSocket.Close(5000);
				CLogger.Print("1/2 Step", LoggerType.Warning, null);
				Thread.Sleep(5000);
				GameXender.Sync.Close();
				CLogger.Print("2/2 Step", LoggerType.Warning, null);
				foreach (GameClient gameClient2 in GameXender.SocketSessions.Values)
				{
					gameClient2.Close(0, true, false);
				}
				CLogger.Print(string.Format("Shutdowned Server: {0} players disconnected; by Login: '", num) + text + ";", LoggerType.Command, null);
			}
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x000025DF File Offset: 0x000007DF
		public ServerWarning()
		{
		}
	}
}
