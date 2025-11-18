using Plugin.Core.Models;
using Server.Game.Data.Models;

namespace Server.Game.Network.ServerPacket;

public class PROTOCOL_CHAR_CREATE_CHARA_ACK : GameServerPacket
{
	private readonly uint uint_0;

	private readonly Account account_0;

	private readonly CharacterModel characterModel_0;

	private readonly PlayerInventory playerInventory_0;

	private readonly PlayerEquipment playerEquipment_0;

	private readonly byte byte_0;

	public PROTOCOL_CHAR_CREATE_CHARA_ACK(uint uint_1, byte byte_1, CharacterModel characterModel_1, Account account_1)
	{
		uint_0 = uint_1;
		account_0 = account_1;
		if (account_1 != null)
		{
			playerInventory_0 = account_1.Inventory;
			playerEquipment_0 = account_1.Equipment;
		}
		characterModel_0 = characterModel_1;
		byte_0 = byte_1;
	}

	public override void Write()
	{
		WriteH(6146);
		WriteH(0);
		WriteC(0);
		WriteD(uint_0);
		if (uint_0 == 0)
		{
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.WeaponPrimary));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.WeaponSecondary));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.WeaponMelee));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.WeaponExplosive));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.WeaponSpecial));
			WriteB(playerInventory_0.EquipmentData(characterModel_0.Id));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.PartHead));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.PartFace));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.PartJacket));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.PartPocket));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.PartGlove));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.PartBelt));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.PartHolster));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.PartSkin));
			WriteB(playerInventory_0.EquipmentData(playerEquipment_0.BeretItem));
			WriteD(account_0.Cash);
			WriteD(account_0.Gold);
			WriteC(byte_0);
			WriteC(20);
			WriteC((byte)characterModel_0.Slot);
			WriteC(1);
		}
	}
}
