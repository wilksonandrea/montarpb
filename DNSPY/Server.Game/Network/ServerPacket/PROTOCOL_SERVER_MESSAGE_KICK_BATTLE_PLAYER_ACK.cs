using System;
using Plugin.Core.Enums;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000112 RID: 274
	public class PROTOCOL_SERVER_MESSAGE_KICK_BATTLE_PLAYER_ACK : GameServerPacket
	{
		// Token: 0x06000299 RID: 665 RVA: 0x00004A8F File Offset: 0x00002C8F
		public PROTOCOL_SERVER_MESSAGE_KICK_BATTLE_PLAYER_ACK(EventErrorEnum eventErrorEnum_1)
		{
			this.eventErrorEnum_0 = eventErrorEnum_1;
		}

		// Token: 0x0600029A RID: 666 RVA: 0x00004A9E File Offset: 0x00002C9E
		public override void Write()
		{
			base.WriteH(3076);
			base.WriteD((uint)this.eventErrorEnum_0);
		}

		// Token: 0x040001FD RID: 509
		private readonly EventErrorEnum eventErrorEnum_0;
	}
}
