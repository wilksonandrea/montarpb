using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000B5 RID: 181
	public class PROTOCOL_CS_INVITE_ACK : GameServerPacket
	{
		// Token: 0x060001C8 RID: 456 RVA: 0x00003D8F File Offset: 0x00001F8F
		public PROTOCOL_CS_INVITE_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00003D9E File Offset: 0x00001F9E
		public override void Write()
		{
			base.WriteH(889);
			base.WriteD(this.uint_0);
		}

		// Token: 0x0400014D RID: 333
		private readonly uint uint_0;
	}
}
