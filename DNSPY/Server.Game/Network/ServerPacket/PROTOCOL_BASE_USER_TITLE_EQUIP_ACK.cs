using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000025 RID: 37
	public class PROTOCOL_BASE_USER_TITLE_EQUIP_ACK : GameServerPacket
	{
		// Token: 0x06000086 RID: 134 RVA: 0x00002A1A File Offset: 0x00000C1A
		public PROTOCOL_BASE_USER_TITLE_EQUIP_ACK(uint uint_1, int int_2, int int_3)
		{
			this.uint_0 = uint_1;
			this.int_0 = int_2;
			this.int_1 = int_3;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00002A37 File Offset: 0x00000C37
		public override void Write()
		{
			base.WriteH(2379);
			base.WriteD(this.uint_0);
			base.WriteC((byte)this.int_0);
			base.WriteC((byte)this.int_1);
		}

		// Token: 0x04000049 RID: 73
		private readonly uint uint_0;

		// Token: 0x0400004A RID: 74
		private readonly int int_0;

		// Token: 0x0400004B RID: 75
		private readonly int int_1;
	}
}
