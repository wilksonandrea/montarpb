using Plugin.Core.Managers;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_AUTH_SHOP_MATCHINGLIST_ACK : GameServerPacket
	{
		private readonly int int_0;

		private readonly ShopData shopData_0;

		public PROTOCOL_AUTH_SHOP_MATCHINGLIST_ACK(ShopData shopData_1, int int_1)
		{
			this.shopData_0 = shopData_1;
			this.int_0 = int_1;
		}

		public override void Write()
		{
			base.WriteH(1040);
			base.WriteD(this.int_0);
			base.WriteD(this.shopData_0.ItemsCount);
			base.WriteD(this.shopData_0.Offset);
			base.WriteB(this.shopData_0.Buffer);
			base.WriteB(ShopManager.ShopTagData);
		}
	}
}