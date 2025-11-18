namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_CS_COMMISSION_STAFF_RESULT_ACK : GameServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0x346);
        }
    }
}

