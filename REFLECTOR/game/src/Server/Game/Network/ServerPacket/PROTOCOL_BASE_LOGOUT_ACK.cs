namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BASE_LOGOUT_ACK : GameServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0x904);
            base.WriteH((short) 0);
        }
    }
}

