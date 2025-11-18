using Plugin.Core.Models;
using Plugin.Core.Network;
using Plugin.Core.Utility;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK : GameServerPacket
	{
		private readonly List<GoodsItem> list_0;

		private readonly Account account_0;

		private readonly uint uint_0;

		public PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(uint uint_1, List<GoodsItem> list_1, Account account_1)
		{
			this.uint_0 = uint_1;
			this.account_0 = account_1;
			this.list_0 = list_1;
		}

		public PROTOCOL_AUTH_SHOP_GOODS_BUY_ACK(uint uint_1)
		{
			this.uint_0 = uint_1;
		}

		public override void Write()
		{
			base.WriteH(1044);
			base.WriteH(0);
			if (this.uint_0 != 1)
			{
				base.WriteD(this.uint_0);
				return;
			}
			base.WriteC((byte)this.list_0.Count);
			foreach (GoodsItem list0 in this.list_0)
			{
				base.WriteD(0);
				base.WriteD(list0.Id);
				base.WriteC(0);
			}
			base.WriteD(this.account_0.Cash);
			base.WriteD(this.account_0.Gold);
			base.WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
		}
	}
}