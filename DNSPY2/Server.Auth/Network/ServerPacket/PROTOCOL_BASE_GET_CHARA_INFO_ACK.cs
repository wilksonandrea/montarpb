using System;
using System.Collections.Generic;
using Plugin.Core.Models;
using Server.Auth.Data.Models;

namespace Server.Auth.Network.ServerPacket
{
	// Token: 0x0200001D RID: 29
	public class PROTOCOL_BASE_GET_CHARA_INFO_ACK : AuthServerPacket
	{
		// Token: 0x0600006E RID: 110 RVA: 0x00002748 File Offset: 0x00000948
		public PROTOCOL_BASE_GET_CHARA_INFO_ACK(Account account_0)
		{
			this.playerInventory_0 = account_0.Inventory;
			this.playerEquipment_0 = account_0.Equipment;
			this.list_0 = account_0.Character.Characters;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00004BB8 File Offset: 0x00002DB8
		public override void Write()
		{
			base.WriteH(2453);
			base.WriteH(0);
			base.WriteC((byte)this.list_0.Count);
			foreach (CharacterModel characterModel in this.list_0)
			{
				base.WriteC((byte)characterModel.Slot);
				base.WriteC(20);
				base.WriteB(this.playerInventory_0.EquipmentData(characterModel.Id));
			}
			foreach (CharacterModel characterModel2 in this.list_0)
			{
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponPrimary));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponSecondary));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponMelee));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponExplosive));
				base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponSpecial));
				base.WriteB(this.playerInventory_0.EquipmentData(characterModel2.Id));
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

		// Token: 0x04000039 RID: 57
		private readonly PlayerInventory playerInventory_0;

		// Token: 0x0400003A RID: 58
		private readonly PlayerEquipment playerEquipment_0;

		// Token: 0x0400003B RID: 59
		private readonly List<CharacterModel> list_0;
	}
}
