namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Utility;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK : GameServerPacket
    {
        private readonly uint uint_0;
        private readonly List<ItemsModel> list_0 = new List<ItemsModel>();
        private readonly List<ItemsModel> list_1 = new List<ItemsModel>();
        private readonly List<ItemsModel> list_2 = new List<ItemsModel>();
        private readonly List<ItemsModel> list_3 = new List<ItemsModel>();

        public PROTOCOL_AUTH_SHOP_AUTH_GIFT_ACK(uint uint_1, ItemsModel itemsModel_0 = null, Account account_0 = null)
        {
            this.uint_0 = uint_1;
            ItemsModel model = new ItemsModel(itemsModel_0);
            if (model != null)
            {
                ComDiv.TryCreateItem(model, account_0.Inventory, account_0.PlayerId);
                if (model.Category == ItemCategory.Weapon)
                {
                    this.list_1.Add(model);
                }
                else if (model.Category == ItemCategory.Character)
                {
                    this.list_0.Add(model);
                }
                else if (model.Category == ItemCategory.Coupon)
                {
                    this.list_2.Add(model);
                }
                else if (model.Category == ItemCategory.NewItem)
                {
                    this.list_3.Add(model);
                }
            }
        }

        public override void Write()
        {
            base.WriteH((short) 0x41e);
            base.WriteD(this.uint_0);
            if (this.uint_0 == 1)
            {
                base.WriteH((short) 0);
                base.WriteH((ushort) (((this.list_0.Count + this.list_1.Count) + this.list_2.Count) + this.list_3.Count));
                foreach (ItemsModel model in this.list_0)
                {
                    base.WriteD((uint) model.ObjectId);
                    base.WriteD(model.Id);
                    base.WriteC((byte) model.Equip);
                    base.WriteD(model.Count);
                }
                foreach (ItemsModel model2 in this.list_1)
                {
                    base.WriteD((uint) model2.ObjectId);
                    base.WriteD(model2.Id);
                    base.WriteC((byte) model2.Equip);
                    base.WriteD(model2.Count);
                }
                foreach (ItemsModel model3 in this.list_2)
                {
                    base.WriteD((uint) model3.ObjectId);
                    base.WriteD(model3.Id);
                    base.WriteC((byte) model3.Equip);
                    base.WriteD(model3.Count);
                }
                foreach (ItemsModel model4 in this.list_3)
                {
                    base.WriteD((uint) model4.ObjectId);
                    base.WriteD(model4.Id);
                    base.WriteC((byte) model4.Equip);
                    base.WriteD(model4.Count);
                }
            }
        }
    }
}

