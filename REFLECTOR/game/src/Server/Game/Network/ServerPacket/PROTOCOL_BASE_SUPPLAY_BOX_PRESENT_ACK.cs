namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Enums;
    using Plugin.Core.Models;
    using Plugin.Core.Utility;
    using Server.Game.Data.Models;
    using Server.Game.Data.Sync.Server;
    using Server.Game.Network;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_BASE_SUPPLAY_BOX_PRESENT_ACK : GameServerPacket
    {
        private readonly uint uint_0;
        private readonly List<ItemsModel> list_0 = new List<ItemsModel>();
        private readonly List<ItemsModel> list_1 = new List<ItemsModel>();
        private readonly List<ItemsModel> list_2 = new List<ItemsModel>();

        public PROTOCOL_BASE_SUPPLAY_BOX_PRESENT_ACK(uint uint_1, ItemsModel itemsModel_0, Account account_0)
        {
            this.uint_0 = uint_1;
            ItemsModel model = new ItemsModel(itemsModel_0);
            if (model != null)
            {
                ComDiv.TryCreateItem(model, account_0.Inventory, account_0.PlayerId);
                SendItemInfo.LoadItem(account_0, model);
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
            }
        }

        public override void Write()
        {
            base.WriteH((short) 0x96b);
            base.WriteD(this.list_0.Count);
            base.WriteD(this.list_1.Count);
            base.WriteD(this.list_2.Count);
            base.WriteD(0);
            foreach (ItemsModel model in this.list_0)
            {
                base.WriteQ(model.ObjectId);
                base.WriteD(model.Id);
                base.WriteC((byte) model.Equip);
                base.WriteD(model.Count);
            }
            foreach (ItemsModel model2 in this.list_1)
            {
                base.WriteQ(model2.ObjectId);
                base.WriteD(model2.Id);
                base.WriteC((byte) model2.Equip);
                base.WriteD(model2.Count);
            }
            foreach (ItemsModel model3 in this.list_2)
            {
                base.WriteQ(model3.ObjectId);
                base.WriteD(model3.Id);
                base.WriteC((byte) model3.Equip);
                base.WriteD(model3.Count);
            }
        }
    }
}

