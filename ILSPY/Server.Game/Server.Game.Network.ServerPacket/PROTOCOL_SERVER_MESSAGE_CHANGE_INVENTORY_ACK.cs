using System.Collections.Generic;
using Plugin.Core.Enums;
using Plugin.Core.Models;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK : GameServerPacket
{
	private readonly PlayerInventory playerInventory_0;

	private readonly PlayerCharacters playerCharacters_0;

	private readonly PlayerEquipment playerEquipment_0;

	private readonly PlayerEquipment playerEquipment_1;

	private readonly List<ItemsModel> list_0;

	private readonly List<int> list_1;

	private readonly List<int> list_2;

	private readonly int int_0;

	public PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK(Account account_0, SlotModel slotModel_0)
	{
		list_2 = new List<int>();
		list_1 = new List<int>();
		if (account_0 == null)
		{
			return;
		}
		playerEquipment_1 = account_0.Equipment;
		playerInventory_0 = account_0.Inventory;
		playerCharacters_0 = account_0.Character;
		list_0 = account_0.Inventory.GetItemsByType(ItemCategory.Coupon);
		RoomModel room = account_0.Room;
		if (room != null && slotModel_0 != null)
		{
			playerEquipment_0 = slotModel_0.Equipment;
			if (playerEquipment_0.CharaRedId != playerEquipment_1.CharaRedId)
			{
				list_2.Add(playerCharacters_0.GetCharacter(playerEquipment_1.CharaRedId).Slot);
			}
			if (playerEquipment_0.CharaBlueId != playerEquipment_1.CharaBlueId)
			{
				list_2.Add(playerCharacters_0.GetCharacter(playerEquipment_1.CharaBlueId).Slot);
			}
			int_0 = ((room.ValidateTeam(slotModel_0.Team, slotModel_0.CostumeTeam) == TeamEnum.FR_TEAM) ? playerEquipment_1.CharaRedId : playerEquipment_1.CharaBlueId);
			if (playerEquipment_0.DinoItem != playerEquipment_1.DinoItem)
			{
				list_1.Add(playerEquipment_1.DinoItem);
			}
			if (playerEquipment_0.SprayId != playerEquipment_1.SprayId)
			{
				list_1.Add(playerEquipment_1.SprayId);
			}
			if (playerEquipment_0.NameCardId != playerEquipment_1.NameCardId)
			{
				list_1.Add(playerEquipment_1.NameCardId);
			}
		}
	}

	public override void Write()
	{
		WriteH(3082);
		WriteH(0);
		WriteC((byte)list_2.Count);
		foreach (int item in list_2)
		{
			WriteC((byte)item);
			WriteC(0);
			WriteC(0);
			WriteC(0);
		}
		WriteC(0);
		WriteB(playerInventory_0.EquipmentData(playerEquipment_1.AccessoryId));
		WriteC((byte)list_0.Count);
		foreach (ItemsModel item2 in list_0)
		{
			WriteD(item2.Id);
		}
		WriteC(0);
		WriteC(0);
		WriteC(0);
		WriteB(playerInventory_0.EquipmentData(playerEquipment_1.WeaponPrimary));
		WriteB(playerInventory_0.EquipmentData(playerEquipment_1.WeaponSecondary));
		WriteB(playerInventory_0.EquipmentData(playerEquipment_1.WeaponMelee));
		WriteB(playerInventory_0.EquipmentData(playerEquipment_1.WeaponExplosive));
		WriteB(playerInventory_0.EquipmentData(playerEquipment_1.WeaponSpecial));
		WriteB(playerInventory_0.EquipmentData(int_0));
		WriteB(playerInventory_0.EquipmentData(playerEquipment_1.PartHead));
		WriteB(playerInventory_0.EquipmentData(playerEquipment_1.PartFace));
		WriteB(playerInventory_0.EquipmentData(playerEquipment_1.PartJacket));
		WriteB(playerInventory_0.EquipmentData(playerEquipment_1.PartPocket));
		WriteB(playerInventory_0.EquipmentData(playerEquipment_1.PartGlove));
		WriteB(playerInventory_0.EquipmentData(playerEquipment_1.PartBelt));
		WriteB(playerInventory_0.EquipmentData(playerEquipment_1.PartHolster));
		WriteB(playerInventory_0.EquipmentData(playerEquipment_1.PartSkin));
		WriteB(playerInventory_0.EquipmentData(playerEquipment_1.BeretItem));
		WriteC((byte)list_1.Count);
		foreach (int item3 in list_1)
		{
			WriteB(playerInventory_0.EquipmentData(item3));
		}
	}
}
