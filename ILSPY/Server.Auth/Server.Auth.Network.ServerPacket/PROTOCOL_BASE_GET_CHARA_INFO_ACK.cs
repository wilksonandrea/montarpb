using System.Collections.Generic;
using Plugin.Core.Models;
using Server.Auth.Data.Models;

namespace Server.Auth.Network.ServerPacket;

public class PROTOCOL_BASE_GET_CHARA_INFO_ACK : AuthServerPacket
{
	private readonly PlayerInventory playerInventory_0;

	private readonly PlayerEquipment playerEquipment_0;

	private readonly List<CharacterModel> list_0;

	public PROTOCOL_BASE_GET_CHARA_INFO_ACK(Account account_0)
	{
		playerInventory_0 = account_0.Inventory;
		playerEquipment_0 = account_0.Equipment;
		list_0 = account_0.Character.Characters;
	}

	public override void Write()
	{
		WriteH(2453);
		WriteH(0);
		WriteC((byte)list_0.Count);
		foreach (CharacterModel item in list_0)
		{
			WriteC((byte)item.Slot);
			WriteC(20);
			WriteB(playerInventory_0.EquipmentData(item.Id));
		}
		foreach (CharacterModel item2 in list_0)
		{
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.WeaponPrimary));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.WeaponSecondary));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.WeaponMelee));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.WeaponExplosive));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.WeaponSpecial));
			WriteB(playerInventory_0.EquipmentData(item2.Id));
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
	}
}
