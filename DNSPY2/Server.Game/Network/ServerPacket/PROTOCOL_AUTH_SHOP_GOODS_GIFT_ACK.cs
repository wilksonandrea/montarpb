using System;
using System.Collections.Generic;
using Plugin.Core.Models;
using Plugin.Core.Utility;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000038 RID: 56
	public class PROTOCOL_AUTH_SHOP_GOODS_GIFT_ACK : GameServerPacket
	{
		// Token: 0x060000AE RID: 174 RVA: 0x00002D71 File Offset: 0x00000F71
		public PROTOCOL_AUTH_SHOP_GOODS_GIFT_ACK(uint uint_1, List<GoodsItem> list_1, Account account_1)
		{
			this.uint_0 = uint_1;
			this.list_0 = list_1;
			this.account_0 = account_1;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00002D8E File Offset: 0x00000F8E
		public PROTOCOL_AUTH_SHOP_GOODS_GIFT_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x0000A6C0 File Offset: 0x000088C0
		public override void Write()
		{
			base.WriteH(1046);
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

		// Token: 0x04000068 RID: 104
		private readonly Account account_0;

		// Token: 0x04000069 RID: 105
		private readonly List<GoodsItem> list_0;

		// Token: 0x0400006A RID: 106
		private readonly uint uint_0;
	}
}
