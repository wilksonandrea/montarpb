using System;
using System.Net.Sockets;
using System.Threading;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Network;
using Plugin.Core.XML;
using Server.Auth.Data.Managers;
using Server.Auth.Data.Models;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Data.Sync.Client
{
	// Token: 0x0200005D RID: 93
	public static class ServerWarning
	{
		// Token: 0x06000144 RID: 324 RVA: 0x0000BAC0 File Offset: 0x00009CC0
		public static void LoadGMWarning(SyncClientPacket C)
		{
			string text = C.ReadS((int)C.ReadC());
			string text2 = C.ReadS((int)C.ReadC());
			string text3 = C.ReadS((int)C.ReadH());
			Account accountDB = AccountManager.GetAccountDB(text, text2, 2, 31);
			if (accountDB != null && accountDB.Password == text2 && accountDB.Access >= AccessLevel.GAMEMASTER)
			{
				int num = 0;
				using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK protocol_SERVER_MESSAGE_ANNOUNCE_ACK = new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(text3))
				{
					num = AuthXender.Client.SendPacketToAllClients(protocol_SERVER_MESSAGE_ANNOUNCE_ACK);
				}
				CLogger.Print(string.Format("Message sent to '{0}' Players: '{1}'; by Username: '{2}'", num, text3, text), LoggerType.Command, null);
			}
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000BB6C File Offset: 0x00009D6C
		public static void LoadShopRestart(SyncClientPacket C)
		{
			int num = (int)C.ReadC();
			ShopManager.Reset();
			ShopManager.Load(num);
			CLogger.Print(string.Format("Shop restarted. (Type: {0})", num), LoggerType.Command, null);
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000BBA4 File Offset: 0x00009DA4
		public static void LoadServerUpdate(SyncClientPacket C)
		{
			int num = (int)C.ReadC();
			SChannelXML.UpdateServer(num);
			CLogger.Print(string.Format("Server updated. (Id: {0})", num), LoggerType.Command, null);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000BBD8 File Offset: 0x00009DD8
		public static void LoadShutdown(SyncClientPacket C)
		{
			string text = C.ReadS((int)C.ReadC());
			string text2 = C.ReadS((int)C.ReadC());
			Account accountDB = AccountManager.GetAccountDB(text, text2, 2, 31);
			if (accountDB != null && accountDB.Password == text2 && accountDB.Access >= AccessLevel.GAMEMASTER)
			{
				int num = 0;
				foreach (AuthClient authClient in AuthXender.SocketSessions.Values)
				{
					authClient.Client.Shutdown(SocketShutdown.Both);
					authClient.Client.Close(10000);
					num++;
				}
				CLogger.Print(string.Format("Disconnected Players: {0} (By: {1})", num, text), LoggerType.Warning, null);
				AuthXender.Client.ServerIsClosed = true;
				AuthXender.Client.MainSocket.Close(5000);
				CLogger.Print("1/2 Step", LoggerType.Warning, null);
				Thread.Sleep(5000);
				AuthXender.Sync.Close();
				CLogger.Print("2/2 Step", LoggerType.Warning, null);
				foreach (AuthClient authClient2 in AuthXender.SocketSessions.Values)
				{
					authClient2.Close(0, true);
				}
				CLogger.Print(string.Format("Shutdowned Server: {0} players disconnected; by Username: '{1};", num, text), LoggerType.Command, null);
			}
		}
	}
}
