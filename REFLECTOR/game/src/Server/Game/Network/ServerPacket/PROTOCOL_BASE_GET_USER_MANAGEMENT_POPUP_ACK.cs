namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BASE_GET_USER_MANAGEMENT_POPUP_ACK : GameServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0x1a02);
            base.WriteH((short) 0);
        }
    }
}

