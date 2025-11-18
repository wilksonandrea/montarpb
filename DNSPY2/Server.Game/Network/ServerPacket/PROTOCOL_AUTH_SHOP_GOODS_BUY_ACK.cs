using System;
using System.Collections.Generic;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000040 RID: 64
	public class PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK : GameServerPacket
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x00002E59 File Offset: 0x00001059
		public PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(uint uint_1, List<GoodsItem> list_1, Account account_1)
		{
			this.uint_0 = uint_1;
			this.account_0 = account_1;
			this.list_0 = list_1;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00002E76 File Offset: 0x00001076
		public PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000AD30 File Offset: 0x00008F30
		public override void Write()
		{
			base.WriteH(1044);
			base.WriteH(0);
			if (this.uint_0 == 1U)
			{
				base.WriteC((byte)this.list_0.Count);
				foreach (GoodsItem goodsItem in this.list_0)
				{
					base.WriteD(0);
					base.WriteD(goodsItem.Id);
					base.WriteC(0);
				}
				base.WriteD(this.account_0.Cash);
				base.WriteD(this.account_0.Gold);
				base.WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
				return;
			}
			base.WriteD(this.uint_0);
		}

		// Token: 0x04000080 RID: 128
		private readonly List<GoodsItem> list_0;

		// Token: 0x04000081 RID: 129
		private readonly Account account_0;

		// Token: 0x04000082 RID: 130
		private readonly uint uint_0;
	}
}
