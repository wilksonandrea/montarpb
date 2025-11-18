using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Auth.Network.ServerPacket;

namespace Server.Auth.Data.Sync.Client
{
	// Token: 0x02000058 RID: 88
	public static class ServerMessage
	{
		// Token: 0x0600013C RID: 316 RVA: 0x0000B8F4 File Offset: 0x00009AF4
		public static void Load(SyncClientPacket C)
		{
			byte b = C.ReadC();
			string text = C.ReadS((int)b);
			if (!string.IsNullOrEmpty(text) && b <= 60)
			{
				int num = 0;
				using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK protocol_SERVER_MESSAGE_ANNOUNCE_ACK = new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(text))
				{
					num = AuthXender.Client.SendPacketToAllClients(protocol_SERVER_MESSAGE_ANNOUNCE_ACK);
				}
				CLogger.Print(string.Format("Message sent to '{0}' Players", num), LoggerType.Command, null);
				return;
			}
		}
	}
}
