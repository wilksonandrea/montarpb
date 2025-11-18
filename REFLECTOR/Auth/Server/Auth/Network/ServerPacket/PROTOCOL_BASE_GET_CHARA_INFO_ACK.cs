namespace Server.Auth.Network.ServerPacket
{
    using Plugin.Core.Models;
    using Server.Auth.Data.Models;
    using Server.Auth.Network;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_BASE_GET_CHARA_INFO_ACK : AuthServerPacket
    {
        private readonly PlayerInventory playerInventory_0;
        private readonly PlayerEquipment playerEquipment_0;
        private readonly List<CharacterModel> list_0;

        public PROTOCOL_BASE_GET_CHARA_INFO_ACK(Account account_0)
        {
            this.playerInventory_0 = account_0.Inventory;
            this.playerEquipment_0 = account_0.Equipment;
            this.list_0 = account_0.Character.Characters;
        }

        public override void Write()
        {
            base.WriteH((short) 0x995);
            base.WriteH((short) 0);
            base.WriteC((byte) this.list_0.Count);
            foreach (CharacterModel model in this.list_0)
            {
                base.WriteC((byte) model.Slot);
                base.WriteC(20);
                base.WriteB(this.playerInventory_0.EquipmentData(model.Id));
            }
            foreach (CharacterModel model2 in this.list_0)
            {
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponPrimary));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponSecondary));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponMelee));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponExplosive));
                base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_0.WeaponSpecial));
                base.WriteB(this.playerInventory_0.EquipmentData(model2.Id));
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
}

