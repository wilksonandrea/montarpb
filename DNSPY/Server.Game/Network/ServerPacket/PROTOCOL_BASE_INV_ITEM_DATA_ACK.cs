using System;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000056 RID: 86
	public class PROTOCOL_BASE_INV_ITEM_DATA_ACK : GameServerPacket
	{
		// Token: 0x060000ED RID: 237 RVA: 0x000030E5 File Offset: 0x000012E5
		public PROTOCOL_BASE_INV_ITEM_DATA_ACK(int int_1, Account account_1)
		{
			this.int_0 = int_1;
			this.account_0 = account_1;
		}

		// Token: 0x060000EE RID: 238 RVA: 0x0000BF24 File Offset: 0x0000A124
		public override void Write()
		{
			base.WriteH(2395);
			base.WriteC((byte)this.int_0);
			base.WriteC((byte)this.account_0.NickColor);
			base.WriteD(this.account_0.Bonus.FakeRank);
			base.WriteD(this.account_0.Bonus.FakeRank);
			base.WriteU(this.account_0.Bonus.FakeNick, 66);
			base.WriteH((short)this.account_0.Bonus.CrosshairColor);
			base.WriteH((short)this.account_0.Bonus.MuzzleColor);
			base.WriteC((byte)this.account_0.Bonus.NickBorderColor);
		}

		// Token: 0x040000AC RID: 172
		private readonly int int_0;

		// Token: 0x040000AD RID: 173
		private readonly Account account_0;
	}
}
