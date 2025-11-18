using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000044 RID: 68
	public class PROTOCOL_AUTH_SHOP_JACKPOT_ACK : GameServerPacket
	{
		// Token: 0x060000C9 RID: 201 RVA: 0x00002EC3 File Offset: 0x000010C3
		public PROTOCOL_AUTH_SHOP_JACKPOT_ACK(string string_1, int int_2, int int_3)
		{
			this.string_0 = string_1;
			this.int_0 = int_2;
			this.int_1 = int_3;
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000B020 File Offset: 0x00009220
		public override void Write()
		{
			base.WriteH(1068);
			base.WriteH(0);
			base.WriteC((byte)this.int_1);
			base.WriteD(this.int_0);
			base.WriteC((byte)this.string_0.Length);
			base.WriteN(this.string_0, this.string_0.Length, "UTF-16LE");
		}

		// Token: 0x04000088 RID: 136
		private readonly string string_0;

		// Token: 0x04000089 RID: 137
		private readonly int int_0;

		// Token: 0x0400008A RID: 138
		private readonly int int_1;
	}
}
