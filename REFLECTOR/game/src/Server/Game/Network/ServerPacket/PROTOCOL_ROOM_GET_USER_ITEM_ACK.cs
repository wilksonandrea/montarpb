namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;
    using System.Collections.Generic;

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
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                SlotModel model2;
                RoomModel room = account_1.Room;
                if ((room != null) && room.GetSlot(account_1.SlotId, out model2))
                {
                    int num = (room.ValidateTeam(model2.Team, model2.CostumeTeam) == TeamEnum.FR_TEAM) ? playerEquipment_1.CharaRedId : playerEquipment_1.CharaBlueId;
                    packet.WriteB(this.playerInventory_0.EquipmentData(num));
                }
                return packet.ToArray();
            }
        }

        private byte[] method_1(PlayerEquipment playerEquipment_1)
        {
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                List<int> list1 = new List<int>();
                list1.Add(playerEquipment_1.DinoItem);
                list1.Add(playerEquipment_1.SprayId);
                list1.Add(playerEquipment_1.NameCardId);
                List<int> list = list1;
                if (list.Count > 0)
                {
                    packet.WriteC((byte) list.Count);
                    foreach (int num in list)
                    {
                        packet.WriteB(this.playerInventory_0.EquipmentData(num));
                    }
                }
                return packet.ToArray();
            }
        }

        private byte[] method_2()
        {
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                List<ItemsModel> list = this.playerInventory_0.GetItemsByType(ItemCategory.Coupon);
                if (list.Count > 0)
                {
                    packet.WriteH((short) list.Count);
                    foreach (ItemsModel model in list)
                    {
                        packet.WriteD(model.Id);
                    }
                }
                return packet.ToArray();
            }
        }

        public override void Write()
        {
            base.WriteH((short) 0xe3e);
            base.WriteH((short) 0);
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

