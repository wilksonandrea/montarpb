namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Plugin.Core.Utility;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;
    using System.Runtime.InteropServices;

    public class PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK : GameServerPacket
    {
        private readonly uint uint_0;
        private readonly ItemsModel itemsModel_0;

        public PROTOCOL_AUTH_SHOP_ITEM_AUTH_ACK(uint uint_1, ItemsModel itemsModel_1 = null, Account account_0 = null)
        {
            this.itemsModel_0 = itemsModel_1;
            if ((itemsModel_1 == null) || (uint_1 != 1))
            {
                uint_1 = 0x80000000;
            }
            else
            {
                int num = ComDiv.GetIdStatics(itemsModel_1.Id, 1);
                if ((num != 0x11) && ((num != 0x12) && ((num != 20) && (num != 0x25))))
                {
                    itemsModel_1.Equip = ItemEquipType.Temporary;
                }
                else if ((itemsModel_1.Count > 1) && (itemsModel_1.Equip == ItemEquipType.Durable))
                {
                    uint num2 = itemsModel_1.Count - 1;
                    itemsModel_1.Count = num2;
                    ComDiv.UpdateDB("player_items", "count", (long) num2, "object_id", itemsModel_1.ObjectId, "owner_id", account_0.PlayerId);
                }
                else
                {
                    DaoManagerSQL.DeletePlayerInventoryItem(itemsModel_1.ObjectId, account_0.PlayerId);
                    account_0.Inventory.RemoveItem(itemsModel_1);
                    itemsModel_1.Id = 0;
                    itemsModel_1.Count = 0;
                }
            }
            this.uint_0 = uint_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x418);
            base.WriteD(this.uint_0);
            if (this.uint_0 == 1)
            {
                base.WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
                base.WriteD((uint) this.itemsModel_0.ObjectId);
                if ((this.itemsModel_0.Category == ItemCategory.Coupon) && (this.itemsModel_0.Equip == ItemEquipType.Temporary))
                {
                    base.WriteD(0);
                    base.WriteC(1);
                    base.WriteD(0);
                }
                else
                {
                    base.WriteD(this.itemsModel_0.Id);
                    base.WriteC((byte) this.itemsModel_0.Equip);
                    base.WriteD(this.itemsModel_0.Count);
                }
            }
        }
    }
}

