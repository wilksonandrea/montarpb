namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK : GameServerPacket
    {
        private readonly PlayerInventory playerInventory_0;
        private readonly PlayerCharacters playerCharacters_0;
        private readonly PlayerEquipment playerEquipment_0;
        private readonly PlayerEquipment playerEquipment_1;
        private readonly List<ItemsModel> list_0;
        private readonly List<int> list_1 = new List<int>();
        private readonly List<int> list_2 = new List<int>();
        private readonly int int_0;

        public PROTOCOL_SERVER_MESSAGE_CHANGE_INVENTORY_ACK(Account account_0, SlotModel slotModel_0)
        {
            if (account_0 != null)
            {
                this.playerEquipment_1 = account_0.Equipment;
                this.playerInventory_0 = account_0.Inventory;
                this.playerCharacters_0 = account_0.Character;
                this.list_0 = account_0.Inventory.GetItemsByType(ItemCategory.Coupon);
                RoomModel room = account_0.Room;
                if ((room != null) && (slotModel_0 != null))
                {
                    this.playerEquipment_0 = slotModel_0.Equipment;
                    if (this.playerEquipment_0.CharaRedId != this.playerEquipment_1.CharaRedId)
                    {
                        this.list_2.Add(this.playerCharacters_0.GetCharacter(this.playerEquipment_1.CharaRedId).Slot);
                    }
                    if (this.playerEquipment_0.CharaBlueId != this.playerEquipment_1.CharaBlueId)
                    {
                        this.list_2.Add(this.playerCharacters_0.GetCharacter(this.playerEquipment_1.CharaBlueId).Slot);
                    }
                    this.int_0 = (room.ValidateTeam(slotModel_0.Team, slotModel_0.CostumeTeam) == TeamEnum.FR_TEAM) ? this.playerEquipment_1.CharaRedId : this.playerEquipment_1.CharaBlueId;
                    if (this.playerEquipment_0.DinoItem != this.playerEquipment_1.DinoItem)
                    {
                        this.list_1.Add(this.playerEquipment_1.DinoItem);
                    }
                    if (this.playerEquipment_0.SprayId != this.playerEquipment_1.SprayId)
                    {
                        this.list_1.Add(this.playerEquipment_1.SprayId);
                    }
                    if (this.playerEquipment_0.NameCardId != this.playerEquipment_1.NameCardId)
                    {
                        this.list_1.Add(this.playerEquipment_1.NameCardId);
                    }
                }
            }
        }

        public override void Write()
        {
            base.WriteH((short) 0xc0a);
            base.WriteH((short) 0);
            base.WriteC((byte) this.list_2.Count);
            foreach (int num in this.list_2)
            {
                base.WriteC((byte) num);
                base.WriteC(0);
                base.WriteC(0);
                base.WriteC(0);
            }
            base.WriteC(0);
            base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_1.AccessoryId));
            base.WriteC((byte) this.list_0.Count);
            foreach (ItemsModel model in this.list_0)
            {
                base.WriteD(model.Id);
            }
            base.WriteC(0);
            base.WriteC(0);
            base.WriteC(0);
            base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_1.WeaponPrimary));
            base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_1.WeaponSecondary));
            base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_1.WeaponMelee));
            base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_1.WeaponExplosive));
            base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_1.WeaponSpecial));
            base.WriteB(this.playerInventory_0.EquipmentData(this.int_0));
            base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_1.PartHead));
            base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_1.PartFace));
            base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_1.PartJacket));
            base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_1.PartPocket));
            base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_1.PartGlove));
            base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_1.PartBelt));
            base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_1.PartHolster));
            base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_1.PartSkin));
            base.WriteB(this.playerInventory_0.EquipmentData(this.playerEquipment_1.BeretItem));
            base.WriteC((byte) this.list_1.Count);
            foreach (int num2 in this.list_1)
            {
                base.WriteB(this.playerInventory_0.EquipmentData(num2));
            }
        }
    }
}

