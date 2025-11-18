using System.Collections.Generic;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_ROOM_GET_USER_ITEM_ACK : GameServerPacket
{
	private readonly Account account_0;

	private readonly PlayerInventory playerInventory_0;

	private readonly PlayerEquipment playerEquipment_0;

	public PROTOCOL_ROOM_GET_USER_ITEM_ACK(Account account_1)
	{
		account_0 = account_1;
		if (account_1 != null)
		{
			playerInventory_0 = account_1.Inventory;
			playerEquipment_0 = account_1.Equipment;
		}
	}

	public override void Write()
	{
		WriteH(3646);
		WriteH(0);
		WriteB(playerInventory_0.EquipmentData(playerEquipment_0.AccessoryId));
		WriteB(method_2());
		WriteB(method_1(playerEquipment_0));
		WriteB(playerInventory_0.EquipmentData(playerEquipment_0.WeaponPrimary));
		WriteB(playerInventory_0.EquipmentData(playerEquipment_0.WeaponSecondary));
		WriteB(playerInventory_0.EquipmentData(playerEquipment_0.WeaponMelee));
		WriteB(playerInventory_0.EquipmentData(playerEquipment_0.WeaponExplosive));
		WriteB(playerInventory_0.EquipmentData(playerEquipment_0.WeaponSpecial));
		WriteB(method_0(account_0, playerEquipment_0));
		WriteB(playerInventory_0.EquipmentData(playerEquipment_0.PartHead));
		WriteB(playerInventory_0.EquipmentData(playerEquipment_0.PartFace));
		WriteB(playerInventory_0.EquipmentData(playerEquipment_0.PartJacket));
		WriteB(playerInventory_0.EquipmentData(playerEquipment_0.PartPocket));
		WriteB(playerInventory_0.EquipmentData(playerEquipment_0.PartGlove));
		WriteB(playerInventory_0.EquipmentData(playerEquipment_0.PartBelt));
		WriteB(playerInventory_0.EquipmentData(playerEquipment_0.PartHolster));
		WriteB(playerInventory_0.EquipmentData(playerEquipment_0.PartSkin));
		WriteB(playerInventory_0.EquipmentData(playerEquipment_0.BeretItem));
	}

	private byte[] method_0(Account account_1, PlayerEquipment playerEquipment_1)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		RoomModel room = account_1.Room;
		if (room != null && room.GetSlot(account_1.SlotId, out var Slot))
		{
			int ıtemId = ((room.ValidateTeam(Slot.Team, Slot.CostumeTeam) == TeamEnum.FR_TEAM) ? playerEquipment_1.CharaRedId : playerEquipment_1.CharaBlueId);
			syncServerPacket.WriteB(playerInventory_0.EquipmentData(ıtemId));
		}
		return syncServerPacket.ToArray();
	}

	private byte[] method_1(PlayerEquipment playerEquipment_1)
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		List<int> list = new List<int> { playerEquipment_1.DinoItem, playerEquipment_1.SprayId, playerEquipment_1.NameCardId };
		if (list.Count > 0)
		{
			syncServerPacket.WriteC((byte)list.Count);
			foreach (int item in list)
			{
				syncServerPacket.WriteB(playerInventory_0.EquipmentData(item));
			}
		}
		return syncServerPacket.ToArray();
	}

	private byte[] method_2()
	{
		using SyncServerPacket syncServerPacket = new SyncServerPacket();
		List<ItemsModel> ıtemsByType = playerInventory_0.GetItemsByType(ItemCategory.Coupon);
		if (ıtemsByType.Count > 0)
		{
			syncServerPacket.WriteH((short)ıtemsByType.Count);
			foreach (ItemsModel item in ıtemsByType)
			{
				syncServerPacket.WriteD(item.Id);
			}
		}
		return syncServerPacket.ToArray();
	}
}
