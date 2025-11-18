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

namespace Server.Game.Data.Sync.Client;

public class ServerWarning
{
	public static void LoadGMWarning(SyncClientPacket C)
	{
		string text = C.ReadS(C.ReadC());
		string text2 = C.ReadS(C.ReadC());
		string text3 = C.ReadS(C.ReadH());
		Account accountDB = AccountManager.GetAccountDB(text, 0, 31);
		if (accountDB != null && accountDB.Password == text2 && accountDB.Access >= AccessLevel.GAMEMASTER)
		{
			int num = 0;
			using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK packet = new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(text3))
			{
				num = GameXender.Client.SendPacketToAllClients(packet);
			}
			CLogger.Print($"Message sent to '{num}' Players: '{text3}'; by Username: '{text}'", LoggerType.Command);
		}
	}

	public static void LoadShopRestart(SyncClientPacket C)
	{
		int num = C.ReadC();
		ShopManager.Reset();
		ShopManager.Load(num);
		CLogger.Print($"Shop restarted. (Type: {num})", LoggerType.Command);
	}

	public static void LoadServerUpdate(SyncClientPacket C)
	{
		int num = C.ReadC();
		SChannelXML.UpdateServer(num);
		CLogger.Print($"Server updated. (Id: {num})", LoggerType.Command);
	}

	public static void LoadShutdown(SyncClientPacket C)
	{
		string text = C.ReadS(C.ReadC());
		string text2 = C.ReadS(C.ReadC());
		Account accountDB = AccountManager.GetAccountDB(text, 0, 31);
		if (accountDB == null || !(accountDB.Password == text2) || accountDB.Access < AccessLevel.GAMEMASTER)
		{
			return;
		}
		int num = 0;
		foreach (GameClient value in GameXender.SocketSessions.Values)
		{
			value.Client.Shutdown(SocketShutdown.Both);
			value.Client.Close(10000);
			num++;
		}
		CLogger.Print($"Disconnected Players: {num} (By: {text})", LoggerType.Warning);
		GameXender.Client.ServerIsClosed = true;
		GameXender.Client.MainSocket.Close(5000);
		CLogger.Print("1/2 Step", LoggerType.Warning);
		Thread.Sleep(5000);
		GameXender.Sync.Close();
		CLogger.Print("2/2 Step", LoggerType.Warning);
		foreach (GameClient value2 in GameXender.SocketSessions.Values)
		{
			value2.Close(0, DestroyConnection: true);
		}
		CLogger.Print($"Shutdowned Server: {num} players disconnected; by Login: '" + text + ";", LoggerType.Command);
	}
}
