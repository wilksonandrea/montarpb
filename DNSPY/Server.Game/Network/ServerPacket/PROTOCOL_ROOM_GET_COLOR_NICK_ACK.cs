using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000F0 RID: 240
	public class PROTOCOL_ROOM_GET_COLOR_NICK_ACK : GameServerPacket
	{
		// Token: 0x06000249 RID: 585 RVA: 0x00004669 File Offset: 0x00002869
		public PROTOCOL_ROOM_GET_COLOR_NICK_ACK(int int_2, int int_3)
		{
			this.int_0 = int_2;
			this.int_1 = int_3;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000467F File Offset: 0x0000287F
		public override void Write()
		{
			base.WriteH(3628);
			base.WriteD(this.int_0);
			base.WriteC((byte)this.int_1);
		}

		// Token: 0x040001BA RID: 442
		private readonly int int_0;

		// Token: 0x040001BB RID: 443
		private readonly int int_1;
	}
}
