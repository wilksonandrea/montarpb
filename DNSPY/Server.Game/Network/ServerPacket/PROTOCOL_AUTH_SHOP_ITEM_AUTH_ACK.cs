using System;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.SQL;
using Plugin.Core.Utility;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x02000042 RID: 66
	public class PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK : GameServerPacket
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x0000AE70 File Offset: 0x00009070
		public PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(uint uint_1, ItemsModel itemsModel_1 = null, Account account_0 = null)
		{
			this.itemsModel_0 = itemsModel_1;
			if (itemsModel_1 != null && uint_1 == 1U)
			{
				int idStatics = ComDiv.GetIdStatics(itemsModel_1.Id, 1);
				if (idStatics != 17 && idStatics != 18 && idStatics != 20)
				{
					if (idStatics != 37)
					{
						itemsModel_1.Equip = ItemEquipType.Temporary;
						goto IL_DA;
					}
				}
				if (itemsModel_1.Count > 1U && itemsModel_1.Equip == ItemEquipType.Durable)
				{
					string text = "player_items";
					string text2 = "count";
					uint num = itemsModel_1.Count - 1U;
					itemsModel_1.Count = num;
					ComDiv.UpdateDB(text, text2, (long)((ulong)num), "object_id", itemsModel_1.ObjectId, "owner_id", account_0.PlayerId);
				}
				else
				{
					DaoManagerSQL.DeletePlayerInventoryItem(itemsModel_1.ObjectId, account_0.PlayerId);
					account_0.Inventory.RemoveItem(itemsModel_1);
					itemsModel_1.Id = 0;
					itemsModel_1.Count = 0U;
				}
			}
			else
			{
				uint_1 = 2147483648U;
			}
			IL_DA:
			this.uint_0 = uint_1;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x0000AF60 File Offset: 0x00009160
		public override void Write()
		{
			base.WriteH(1048);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 1U)
			{
				base.WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
				base.WriteD((uint)this.itemsModel_0.ObjectId);
				if (this.itemsModel_0.Category == ItemCategory.Coupon && this.itemsModel_0.Equip == ItemEquipType.Temporary)
				{
					base.WriteD(0);
					base.WriteC(1);
					base.WriteD(0);
					return;
				}
				base.WriteD(this.itemsModel_0.Id);
				base.WriteC((byte)this.itemsModel_0.Equip);
				base.WriteD(this.itemsModel_0.Count);
			}
		}

		// Token: 0x04000085 RID: 133
		private readonly uint uint_0;

		// Token: 0x04000086 RID: 134
		private readonly ItemsModel itemsModel_0;
	}
}
