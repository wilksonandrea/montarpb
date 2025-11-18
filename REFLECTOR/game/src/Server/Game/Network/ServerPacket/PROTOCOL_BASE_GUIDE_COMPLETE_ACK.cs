namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BASE_GUIDE_COMPLETE_ACK : GameServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0x925);
            base.WriteD(0);
            base.WriteD(0);
            base.WriteD(0);
            base.WriteD(0);
            base.WriteD(0);
            base.WriteD(0);
        }
    }
}

