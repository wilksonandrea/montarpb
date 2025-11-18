using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000F6 RID: 246
	public class PROTOCOL_ROOM_GET_RANK_ACK : GameServerPacket
	{
		// Token: 0x06000256 RID: 598 RVA: 0x0000472B File Offset: 0x0000292B
		public PROTOCOL_ROOM_GET_RANK_ACK(int int_2, int int_3)
		{
			this.int_0 = int_2;
			this.int_1 = int_3;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00004741 File Offset: 0x00002941
		public override void Write()
		{
			base.WriteH(3634);
			base.WriteD(this.int_0);
			base.WriteD(this.int_1);
		}

		// Token: 0x040001CA RID: 458
		private readonly int int_0;

		// Token: 0x040001CB RID: 459
		private readonly int int_1;
	}
}
