namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Utility;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_SHOP_LIMITED_SALE_SYNC_ACK : GameServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0x44a);
            base.WriteC(1);
            base.WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
        }
    }
}

