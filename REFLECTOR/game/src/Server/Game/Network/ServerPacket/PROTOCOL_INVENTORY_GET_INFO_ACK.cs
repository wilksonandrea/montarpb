namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Models;
    using Plugin.Core.Utility;
    using Server.Game.Data.Models;
    using Server.Game.Data.Sync.Server;
    using Server.Game.Network;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_INVENTORY_GET_INFO_ACK : GameServerPacket
    {
        private readonly int int_0;
        private readonly PlayerInventory playerInventory_0;
        private readonly List<ItemsModel> list_0;

        public PROTOCOL_INVENTORY_GET_INFO_ACK(int int_1, Account account_0, ItemsModel itemsModel_0)
        {
            this.int_0 = int_1;
            if (account_0 != null)
            {
                this.playerInventory_0 = account_0.Inventory;
                this.list_0 = new List<ItemsModel>();
            }
            ItemsModel model = new ItemsModel(itemsModel_0);
            if (model != null)
            {
                if (int_1 == 0)
                {
                    ComDiv.TryCreateItem(model, account_0.Inventory, account_0.PlayerId);
                }
                SendItemInfo.LoadItem(account_0, model);
                this.list_0.Add(model);
            }
        }

        public PROTOCOL_INVENTORY_GET_INFO_ACK(int int_1, Account account_0, List<GoodsItem> list_1)
        {
            this.int_0 = int_1;
            if (account_0 != null)
            {
                this.playerInventory_0 = account_0.Inventory;
                this.list_0 = new List<ItemsModel>();
            }
            using (List<GoodsItem>.Enumerator enumerator = list_1.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    ItemsModel model = new ItemsModel(enumerator.Current.Item);
                    if (model != null)
                    {
                        if (int_1 == 0)
                        {
                            ComDiv.TryCreateItem(model, account_0.Inventory, account_0.PlayerId);
                        }
                        SendItemInfo.LoadItem(account_0, model);
                        this.list_0.Add(model);
                    }
                }
            }
        }

        public PROTOCOL_INVENTORY_GET_INFO_ACK(int int_1, Account account_0, List<ItemsModel> list_1)
        {
            this.int_0 = int_1;
            if (account_0 != null)
            {
                this.playerInventory_0 = account_0.Inventory;
                this.list_0 = new List<ItemsModel>();
            }
            using (List<ItemsModel>.Enumerator enumerator = list_1.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    ItemsModel model = new ItemsModel(enumerator.Current);
                    if (model != null)
                    {
                        if (int_1 == 0)
                        {
                            ComDiv.TryCreateItem(model, account_0.Inventory, account_0.PlayerId);
                        }
                        SendItemInfo.LoadItem(account_0, model);
                        this.list_0.Add(model);
                    }
                }
            }
        }

        public override void Write()
        {
            base.WriteH((short) 0xd06);
            base.WriteH((short) 0);
            base.WriteH((ushort) this.playerInventory_0.Items.Count);
            base.WriteH((ushort) this.list_0.Count);
            foreach (ItemsModel model in this.list_0)
            {
                base.WriteD((uint) model.ObjectId);
                base.WriteD(model.Id);
                base.WriteC((byte) model.Equip);
                base.WriteD(model.Count);
            }
            base.WriteC((byte) this.int_0);
        }
    }
}

