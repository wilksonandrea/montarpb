using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000B4 RID: 180
	public class PROTOCOL_CS_INVITE_ACCEPT_ACK : GameServerPacket
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x00003D67 File Offset: 0x00001F67
		public PROTOCOL_CS_INVITE_ACCEPT_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00003D76 File Offset: 0x00001F76
		public override void Write()
		{
			base.WriteH(891);
			base.WriteD(this.uint_0);
		}

		// Token: 0x0400014C RID: 332
		private readonly uint uint_0;
	}
}
