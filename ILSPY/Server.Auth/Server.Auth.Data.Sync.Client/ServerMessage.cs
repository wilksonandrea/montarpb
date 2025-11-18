using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Data.Sync.Client;

public static class ServerMessage
{
	public static void Load(SyncClientPacket C)
	{
		byte b = C.ReadC();
		string text = C.ReadS(b);
		if (!string.IsNullOrEmpty(text) && b <= 60)
		{
			int num = 0;
			using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK packet = new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(text))
			{
				num = AuthXender.Client.SendPacketToAllClients(packet);
			}
			CLogger.Print($"Message sent to '{num}' Players", LoggerType.Command);
		}
	}
}
