namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_ROOM_INFO_LEAVE_ACK : GameServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0xe5a);
            base.WriteD(0);
            base.WriteC(0x44);
        }
    }
}

