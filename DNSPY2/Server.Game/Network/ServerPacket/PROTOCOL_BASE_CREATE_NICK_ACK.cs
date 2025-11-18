using System;
using Plugin.Core.Models;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x0200004D RID: 77
	public class PROTOCOL_BASE_CREATE_NICK_ACK : GameServerPacket
	{
		// Token: 0x060000DB RID: 219 RVA: 0x00002F9A File Offset: 0x0000119A
		public PROTOCOL_BASE_CREATE_NICK_ACK(uint uint_1, Account account_1)
		{
			this.uint_0 = uint_1;
			this.account_0 = account_1;
			if (account_1 != null)
			{
				this.playerInventory_0 = account_1.Inventory;
				this.playerEquipment_0 = account_1.Equipment;
			}
		}

		// Token: 0x060000DC RID: 220 RVA: 0x0000B470 File Offset: 0x00009670
		public override void Write()
		{
			base.WriteH(2327);
			base.WriteH(0);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0U)
			{
				base.WriteC(1);
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.DinoItem));
				base.WriteC((byte)(this.account_0.Nickname.Length * 2));
				base.WriteU(this.account_0.Nickname, this.account_0.Nickname.Length * 2);
			}
		}

		// Token: 0x0400009C RID: 156
		private readonly uint uint_0;

		// Token: 0x0400009D RID: 157
		private readonly Account account_0;

		// Token: 0x0400009E RID: 158
		private readonly PlayerInventory playerInventory_0;

		// Token: 0x0400009F RID: 159
		private readonly PlayerEquipment playerEquipment_0;
	}
}
