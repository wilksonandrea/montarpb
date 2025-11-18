namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Models;
    using Server.Game.Network;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_AUTH_SHOP_CAPSULE_ACK : GameServerPacket
    {
        private readonly List<ItemsModel> list_0;
        private readonly int int_0;
        private readonly int int_1;

        public PROTOCOL_AUTH_SHOP_CAPSULE_ACK(ItemsModel itemsModel_0, int int_2, int int_3)
        {
            this.int_0 = int_2;
            this.int_1 = int_3;
            this.list_0 = new List<ItemsModel>();
            ItemsModel item = new ItemsModel(itemsModel_0);
            if (item != null)
            {
                this.list_0.Add(item);
            }
        }

        public PROTOCOL_AUTH_SHOP_CAPSULE_ACK(List<ItemsModel> list_1, int int_2, int int_3)
        {
            this.int_0 = int_2;
            this.int_1 = int_3;
            this.list_0 = new List<ItemsModel>();
            using (List<ItemsModel>.Enumerator enumerator = list_1.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    ItemsModel item = new ItemsModel(enumerator.Current);
                    if (item != null)
                    {
                        this.list_0.Add(item);
                    }
                }
            }
        }

        public override void Write()
        {
            base.WriteH((short) 0x428);
            base.WriteH((short) 0);
            base.WriteC(1);
            base.WriteC((byte) this.int_1);
            base.WriteC((byte) this.list_0.Count);
            foreach (ItemsModel model in this.list_0)
            {
                base.WriteD(model.Id);
            }
        }
    }
}

