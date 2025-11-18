using System;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200010B RID: 267
	public class PROTOCOL_SEASON_CHALLENGE_SEND_REWARD_ACK : GameServerPacket
	{
		// Token: 0x0600028B RID: 651 RVA: 0x000049EE File Offset: 0x00002BEE
		public PROTOCOL_SEASON_CHALLENGE_SEND_REWARD_ACK(uint uint_1, int[] int_1)
		{
			this.uint_0 = uint_1;
			this.int_0 = int_1;
		}

		// Token: 0x0600028C RID: 652 RVA: 0x00013F2C File Offset: 0x0001212C
		public override void Write()
		{
			base.WriteH(8453);
			base.WriteH(0);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0U)
			{
				base.WriteC(2);
				base.WriteD(this.int_0[1]);
				base.WriteD(this.int_0[2]);
				base.WriteD(this.int_0[0]);
				base.WriteC(1);
				base.WriteC(1);
				base.WriteC(1);
			}
		}

		// Token: 0x040001EC RID: 492
		private readonly uint uint_0;

		// Token: 0x040001ED RID: 493
		private readonly int[] int_0;
	}
}
