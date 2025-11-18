namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Utility;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_SHOP_GET_SAILLIST_ACK : GameServerPacket
    {
        private readonly bool bool_0;

        public PROTOCOL_SHOP_GET_SAILLIST_ACK(bool bool_1)
        {
            this.bool_0 = bool_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x406);
            base.WriteC((byte) this.bool_0);
            base.WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
        }
    }
}

