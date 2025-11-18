using Plugin.Core.Models;
using Plugin.Core.Network;
using Server.Game.Data.Models;
using Server.Game.Network;
using System;

namespace Server.Game.Network.ServerPacket
{
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
			this.uint_0 = uint_1;
			this.account_0 = account_1;
			if (account_1 != null)
			{
				this.playerInventory_0 = account_1.Inventory;
				this.playerEquipment_0 = account_1.Equipment;
			}
			this.characterModel_0 = characterModel_1;
			this.byte_0 = byte_1;
		}

		public override void Write()
		{
			base.WriteH(6146);
			base.WriteH(0);
			base.WriteC(0);
			base.WriteD(this.uint_0);
			if (this.uint_0 == 0)
			{
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponPrimary));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponSecondary));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponMelee));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponExplosive));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponSpecial));
				base.WriteB(this.playerInventory_0.EquipmentData(this.characterModel_0.Id));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartHead));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartFace));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartJacket));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartPocket));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartGlove));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartBelt));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartHolster));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.PartSkin));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.BeretItem));
				base.WriteD(this.account_0.Cash);
				base.WriteD(this.account_0.Gold);
				base.WriteC(this.byte_0);
				base.WriteC(20);
				base.WriteC((byte)this.characterModel_0.Slot);
				base.WriteC(1);
			}
		}
	}
}