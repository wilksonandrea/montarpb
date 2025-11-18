using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200009C RID: 156
	public class PROTOCOL_CS_CHANGE_CLAN_EXP_RANK_ACK : GameServerPacket
	{
		// Token: 0x06000193 RID: 403 RVA: 0x00003A10 File Offset: 0x00001C10
		public PROTOCOL_CS_CHANGE_CLAN_EXP_RANK_ACK(int int_1)
		{
			this.int_0 = int_1;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00003A1F File Offset: 0x00001C1F
		public override void Write()
		{
			base.WriteH(880);
			base.WriteC((byte)this.int_0);
		}

		// Token: 0x0400012B RID: 299
		private readonly int int_0;
	}
}
