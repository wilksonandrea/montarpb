using System;
using Plugin.Core.Managers;
using Plugin.Core.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000045 RID: 69
	public class PROTOCOL_AUTH_SHOP_MATCHINGLIST_ACK : GameServerPacket
	{
		// Token: 0x060000CB RID: 203 RVA: 0x00002EE0 File Offset: 0x000010E0
		public PROTOCOL_AUTH_SHOP_MATCHINGLIST_ACK(ShopData shopData_1, int int_1)
		{
			this.shopData_0 = shopData_1;
			this.int_0 = int_1;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000B088 File Offset: 0x00009288
		public override void Write()
		{
			base.WriteH(1040);
			base.WriteD(this.int_0);
			base.WriteD(this.shopData_0.ItemsCount);
			base.WriteD(this.shopData_0.Offset);
			base.WriteB(this.shopData_0.Buffer);
			base.WriteB(ShopManager.ShopTagData);
		}

		// Token: 0x0400008B RID: 139
		private readonly int int_0;

		// Token: 0x0400008C RID: 140
		private readonly ShopData shopData_0;
	}
}
