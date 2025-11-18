using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200005B RID: 91
	public class PROTOCOL_BASE_SELECT_CHANNEL_ACK : GameServerPacket
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x000031A2 File Offset: 0x000013A2
		public PROTOCOL_BASE_SELECT_CHANNEL_ACK(uint uint_1, int int_1, int int_2)
		{
			this.uint_0 = uint_1;
			this.int_0 = int_1;
			this.ushort_0 = (ushort)int_2;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000031C0 File Offset: 0x000013C0
		public override void Write()
		{
			base.WriteH(2335);
			base.WriteD(0);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0U)
			{
				base.WriteD(this.int_0);
				base.WriteH(this.ushort_0);
			}
		}

		// Token: 0x040000B3 RID: 179
		private readonly int int_0;

		// Token: 0x040000B4 RID: 180
		private readonly ushort ushort_0;

		// Token: 0x040000B5 RID: 181
		private readonly uint uint_0;
	}
}
