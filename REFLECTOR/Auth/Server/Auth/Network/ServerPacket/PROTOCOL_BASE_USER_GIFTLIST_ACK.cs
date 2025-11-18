namespace Server.Auth.Network.ServerPacket
{
    using Plugin.Core.Utility;
    using Server.Auth.Network;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_BASE_USER_GIFTLIST_ACK : AuthServerPacket
    {
        private readonly int int_0;
        private readonly List<MessageModel> list_0;

        public PROTOCOL_BASE_USER_GIFTLIST_ACK(int int_1, List<MessageModel> list_1)
        {
            this.int_0 = int_1;
            this.list_0 = list_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x412);
            base.WriteC(0);
            base.WriteC((byte) this.list_0.Count);
            for (int i = 0; i < this.list_0.Count; i++)
            {
                MessageModel local1 = this.list_0[i];
            }
            base.WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
        }
    }
}

