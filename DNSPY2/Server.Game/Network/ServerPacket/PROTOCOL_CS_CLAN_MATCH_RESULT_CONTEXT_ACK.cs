using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000A2 RID: 162
	public class PROTOCOL_CS_CLAN_MATCH_RESULT_CONTEXT_ACK : GameServerPacket
	{
		// Token: 0x060001A1 RID: 417 RVA: 0x00003B1A File Offset: 0x00001D1A
		public PROTOCOL_CS_CLAN_MATCH_RESULT_CONTEXT_ACK(int int_1)
		{
			this.int_0 = int_1;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00003B29 File Offset: 0x00001D29
		public override void Write()
		{
			base.WriteH(1955);
			base.WriteC((byte)this.int_0);
			base.WriteC(13);
			base.WriteC((byte)Math.Ceiling((double)this.int_0 / 13.0));
		}

		// Token: 0x04000137 RID: 311
		private readonly int int_0;
	}
}
