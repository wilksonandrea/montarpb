namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_ROOM_UNREADY_4VS4_ACK : GameServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0xe28);
            base.WriteD(0);
        }
    }
}

