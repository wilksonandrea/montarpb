using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Auth;
using Server.Auth.Network.ServerPacket;
using System;

namespace Server.Auth.Data.Sync.Client
{
	public static class ServerMessage
	{
		public static void Load(SyncClientPacket C)
		{
			byte num = C.ReadC();
			string str = C.ReadS((int)num);
			if (string.IsNullOrEmpty(str) || num > 60)
			{
				return;
			}
			int allClients = 0;
			using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK pROTOCOLSERVERMESSAGEANNOUNCEACK = new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(str))
			{
				allClients = AuthXender.Client.SendPacketToAllClients(pROTOCOLSERVERMESSAGEANNOUNCEACK);
			}
			CLogger.Print(string.Format("Message sent to '{0}' Players", allClients), LoggerType.Command, null);
		}
	}
}