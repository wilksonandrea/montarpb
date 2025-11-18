using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000A7 RID: 167
	public class PROTOCOL_CS_CLOSE_CLAN_ACK : GameServerPacket
	{
		// Token: 0x060001AB RID: 427 RVA: 0x00003BC5 File Offset: 0x00001DC5
		public PROTOCOL_CS_CLOSE_CLAN_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00003BD4 File Offset: 0x00001DD4
		public override void Write()
		{
			base.WriteH(809);
			base.WriteD(this.uint_0);
		}

		// Token: 0x0400013F RID: 319
		private readonly uint uint_0;
	}
}
