using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000ED RID: 237
	public class PROTOCOL_ROOM_CHECK_MAIN_ACK : GameServerPacket
	{
		// Token: 0x06000243 RID: 579 RVA: 0x000045EF File Offset: 0x000027EF
		public PROTOCOL_ROOM_CHECK_MAIN_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000045FE File Offset: 0x000027FE
		public override void Write()
		{
			base.WriteH(3618);
			base.WriteD(this.uint_0);
		}

		// Token: 0x040001B5 RID: 437
		private readonly uint uint_0;
	}
}
