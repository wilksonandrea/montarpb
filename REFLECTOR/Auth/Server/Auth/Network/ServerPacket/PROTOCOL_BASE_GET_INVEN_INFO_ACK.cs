namespace Server.Auth.Network.ServerPacket
{
    using Plugin.Core.Models;
    using Server.Auth.Network;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_BASE_GET_INVEN_INFO_ACK : AuthServerPacket
    {
        private readonly uint uint_0;
        private readonly int int_0;
        private readonly List<ItemsModel> list_0;

        public PROTOCOL_BASE_GET_INVEN_INFO_ACK(uint uint_1, List<ItemsModel> list_1, int int_1)
        {
            this.uint_0 = uint_1;
            this.list_0 = list_1;
            this.int_0 = int_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x90f);
            base.WriteH((short) 0);
            base.WriteD(this.uint_0);
            if (this.uint_0 == 0)
            {
                base.WriteH((ushort) this.list_0.Count);
                foreach (ItemsModel model in this.list_0)
                {
                    base.WriteD((uint) model.ObjectId);
                    base.WriteD(model.Id);
                    base.WriteC((byte) model.Equip);
                    base.WriteD(model.Count);
                }
                base.WriteH((ushort) this.int_0);
                base.WriteH((ushort) this.list_0.Count);
                base.WriteH((ushort) this.list_0.Count);
                base.WriteH((ushort) this.list_0.Count);
                base.WriteH((short) 0);
            }
        }
    }
}

