using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Managers;
using Plugin.Core.Network;
using Plugin.Core.XML;
using Server.Game;
using Server.Game.Data.Managers;
using Server.Game.Data.Models;
using Server.Game.Data.Sync;
using Server.Game.Network.ServerPacket;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;

namespace Server.Game.Data.Sync.Client
{
	public class ServerWarning
	{
		public ServerWarning()
		{
		}

		public static void LoadGMWarning(SyncClientPacket C)
		{
			string str = C.ReadS((int)C.ReadC());
			string str1 = C.ReadS((int)C.ReadC());
			string str2 = C.ReadS(C.ReadH());
			Account accountDB = AccountManager.GetAccountDB(str, 0, 31);
			if (accountDB != null && accountDB.Password == str1 && accountDB.Access >= AccessLevel.GAMEMASTER)
			{
				int allClients = 0;
				using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK pROTOCOLSERVERMESSAGEANNOUNCEACK = new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(str2))
				{
					allClients = GameXender.Client.SendPacketToAllClients(pROTOCOLSERVERMESSAGEANNOUNCEACK);
				}
				CLogger.Print(string.Format("Message sent to '{0}' Players: '{1}'; by Username: '{2}'", allClients, str2, str), LoggerType.Command, null);
			}
		}

		public static void LoadServerUpdate(SyncClientPacket C)
		{
			int ınt32 = C.ReadC();
			SChannelXML.UpdateServer(ınt32);
			CLogger.Print(string.Format("Server updated. (Id: {0})", ınt32), LoggerType.Command, null);
		}

		public static void LoadShopRestart(SyncClientPacket C)
		{
			int ınt32 = C.ReadC();
			ShopManager.Reset();
			ShopManager.Load(ınt32);
			CLogger.Print(string.Format("Shop restarted. (Type: {0})", ınt32), LoggerType.Command, null);
		}

		public static void LoadShutdown(SyncClientPacket C)
		{
			string str = C.ReadS((int)C.ReadC());
			string str1 = C.ReadS((int)C.ReadC());
			Account accountDB = AccountManager.GetAccountDB(str, 0, 31);
			if (accountDB != null && accountDB.Password == str1 && accountDB.Access >= AccessLevel.GAMEMASTER)
			{
				int ınt32 = 0;
				foreach (GameClient value in GameXender.SocketSessions.Values)
				{
					value.Client.Shutdown(SocketShutdown.Both);
					value.Client.Close(10000);
					ınt32++;
				}
				CLogger.Print(string.Format("Disconnected Players: {0} (By: {1})", ınt32, str), LoggerType.Warning, null);
				GameXender.Client.ServerIsClosed = true;
				GameXender.Client.MainSocket.Close(5000);
				CLogger.Print("1/2 Step", LoggerType.Warning, null);
				Thread.Sleep(5000);
				GameXender.Sync.Close();
				CLogger.Print("2/2 Step", LoggerType.Warning, null);
				foreach (GameClient gameClient in GameXender.SocketSessions.Values)
				{
					gameClient.Close(0, true, false);
				}
				CLogger.Print(string.Concat(string.Format("Shutdowned Server: {0} players disconnected; by Login: '", ınt32), str, ";"), LoggerType.Command, null);
			}
		}
	}
}