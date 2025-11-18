namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BASE_OPTION_SAVE_ACK : GameServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0x913);
            base.WriteD(0);
        }
    }
}

