namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Utility;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_SHOP_ENTER_ACK : GameServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0x402);
            base.WriteD(0);
            base.WriteC(0);
            base.WriteD(uint.Parse(DateTimeUtil.Now("yyMMddHHmm")));
        }
    }
}

