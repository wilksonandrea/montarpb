namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_ROOM_PERSONAL_TEAM_CHANGE_ACK : GameServerPacket
    {
        public override void Write()
        {
            base.WriteH((short) 0xe1a);
            base.WriteC(2);
            base.WriteC(0);
            base.WriteC(0);
            base.WriteC(0);
            base.WriteC(0);
            base.WriteC(2);
            base.WriteC(0);
            base.WriteC(8);
        }
    }
}

