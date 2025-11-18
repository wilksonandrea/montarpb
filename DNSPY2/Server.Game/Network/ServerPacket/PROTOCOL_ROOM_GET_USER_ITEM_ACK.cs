using System;
using System.Collections.Generic;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket
{
	// Token: 0x020000FA RID: 250
	public class PROTOCOL_ROOM_GET_USER_ITEM_ACK : GameServerPacket
	{
		// Token: 0x06000261 RID: 609 RVA: 0x00004808 File Offset: 0x00002A08
		public PROTOCOL_ROOM_GET_USER_ITEM_ACK(Account account_1)
		{
			this.account_0 = account_1;
			if (account_1 != null)
			{
				this.playerInventory_0 = account_1.Inventory;
				this.playerEquipment_0 = account_1.Equipment;
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x000130CC File Offset: 0x000112CC
		public override void Write()
		{
			base.WriteH(3646);
			base.WriteH(0);
			base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.AccessoryId));
			base.WriteB(this.method_2());
			base.WriteB(this.method_1(this.playerEquipment_0));
			base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponPrimary));
			base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponSecondary));
			base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponMelee));
			base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponExplosive));
			base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponSpecial));
			base.WriteB(this.method_0(this.account_0, this.playerEquipment_0));
			base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartHead));
			base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartFace));
			base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartJacket));
			base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartPocket));
			base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartGlove));
			base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartBelt));
			base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartHolster));
			base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartSkin));
			base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.BeretItem));
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000132C8 File Offset: 0x000114C8
		private byte[] method_0(Account account_1, PlayerEquipment playerEquipment_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				RoomModel room = account_1.Room;
				SlotModel slotModel;
				if (room != null && room.GetSlot(account_1.SlotId, out slotModel))
				{
					int num = ((room.ValidateTeam(slotModel.Team, slotModel.CostumeTeam) == TeamEnum.FR_TEAM) ? playerEquipment_1.CharaRedId : playerEquipment_1.CharaBlueId);
					syncServerPacket.WriteB(this.playerInventory_0.EquipmentData(num));
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00013350 File Offset: 0x00011550
		private byte[] method_1(PlayerEquipment playerEquipment_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				List<int> list = new List<int> { playerEquipment_1.DinoItem, playerEquipment_1.SprayId, playerEquipment_1.NameCardId };
				if (list.Count > 0)
				{
					syncServerPacket.WriteC((byte)list.Count);
					foreach (int num in list)
					{
						syncServerPacket.WriteB(this.playerInventory_0.EquipmentData(num));
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00013414 File Offset: 0x00011614
		private byte[] method_2()
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				List<ItemsModel> itemsByType = this.playerInventory_0.GetItemsByType(ItemCategory.Coupon);
				if (itemsByType.Count > 0)
				{
					syncServerPacket.WriteH((short)itemsByType.Count);
					foreach (ItemsModel itemsModel in itemsByType)
					{
						syncServerPacket.WriteD(itemsModel.Id);
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		// Token: 0x040001D4 RID: 468
		private readonly Account account_0;

		// Token: 0x040001D5 RID: 469
		private readonly PlayerInventory playerInventory_0;

		// Token: 0x040001D6 RID: 470
		private readonly PlayerEquipment playerEquipment_0;
	}
}
