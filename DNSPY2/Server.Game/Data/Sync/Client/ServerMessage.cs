using System;
using Plugin.Core;
using Plugin.Core.Enums;
using Plugin.Core.Network;
using Server.Game.Network.ServerPacket;

namespace Server.Game.Data.Sync.Client
{
	// Token: 0x020001FF RID: 511
	public static class ServerMessage
	{
		// Token: 0x060005F3 RID: 1523 RVA: 0x00030F34 File Offset: 0x0002F134
		public static void Load(SyncClientPacket C)
		{
			byte b = C.ReadC();
			string text = C.ReadS((int)b);
			if (!string.IsNullOrEmpty(text) && b <= 60)
			{
				int num = 0;
				using (PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK protocol_SERVER_MESSAGE_ANNOUNCE_ACK = new PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(text))
				{
					num = GameXender.Client.SendPacketToAllClients(protocol_SERVER_MESSAGE_ANNOUNCE_ACK);
				}
				CLogger.Print(string.Format("Message sent to '{0}' Players", num), LoggerType.Command, null);
				return;
			}
		}
	}
}
