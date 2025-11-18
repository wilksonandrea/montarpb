using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200001E RID: 30
	public class PROTOCOL_BASE_SUPPLAY_BOX_ANNOUNCE_ACK : GameServerPacket
	{
		// Token: 0x06000078 RID: 120 RVA: 0x0000295B File Offset: 0x00000B5B
		public PROTOCOL_BASE_SUPPLAY_BOX_ANNOUNCE_ACK(string string_1)
		{
			this.string_0 = string_1;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x0000296A File Offset: 0x00000B6A
		public override void Write()
		{
			base.WriteH(2409);
			base.WriteD(0);
		}

		// Token: 0x0400003E RID: 62
		private readonly string string_0;
	}
}
