using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;
using System.Collections.Generic;

namespace Server.Game.Network.ServerPacket
{
	public class PROTOCOL_ROOM_GET_USER_ITEM_ACK : GameServerPacket
	{
		private readonly Account account_0;

		private readonly PlayerInventory playerInventory_0;

		private readonly PlayerEquipment playerEquipment_0;

		public PROTOCOL_ROOM_GET_USER_ITEM_ACK(Account account_1)
		{
			this.account_0 = account_1;
			if (account_1 != null)
			{
				this.playerInventory_0 = account_1.Inventory;
				this.playerEquipment_0 = account_1.Equipment;
			}
		}

		private byte[] method_0(Account account_1, PlayerEquipment playerEquipment_1)
		{
			SlotModel slotModel;
			byte[] array;
			int ınt32;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				RoomModel room = account_1.Room;
				if (room != null && room.GetSlot(account_1.SlotId, out slotModel))
				{
					ınt32 = (room.ValidateTeam(slotModel.Team, slotModel.CostumeTeam) == TeamEnum.FR_TEAM ? playerEquipment_1.CharaRedId : playerEquipment_1.CharaBlueId);
					syncServerPacket.WriteB(this.playerInventory_0.EquipmentData(ınt32));
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		private byte[] method_1(PlayerEquipment playerEquipment_1)
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				List<int> ınt32s = new List<int>()
				{
					playerEquipment_1.DinoItem,
					playerEquipment_1.SprayId,
					playerEquipment_1.NameCardId
				};
				if (ınt32s.Count > 0)
				{
					syncServerPacket.WriteC((byte)ınt32s.Count);
					foreach (int ınt32 in ınt32s)
					{
						syncServerPacket.WriteB(this.playerInventory_0.EquipmentData(ınt32));
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

		private byte[] method_2()
		{
			byte[] array;
			using (SyncServerPacket syncServerPacket = new SyncServerPacket())
			{
				List<ItemsModel> ıtemsByType = this.playerInventory_0.GetItemsByType(ItemCategory.Coupon);
				if (ıtemsByType.Count > 0)
				{
					syncServerPacket.WriteH((short)ıtemsByType.Count);
					foreach (ItemsModel ıtemsModel in ıtemsByType)
					{
						syncServerPacket.WriteD(ıtemsModel.Id);
					}
				}
				array = syncServerPacket.ToArray();
			}
			return array;
		}

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
	}
}