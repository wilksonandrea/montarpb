namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_CLAN_WAR_LEAVE_TEAM_ACK : GameServerPacket
    {
        private readonly uint uint_0;

        public PROTOCOL_CLAN_WAR_LEAVE_TEAM_ACK(uint uint_1)
        {
            this.uint_0 = uint_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x1b0b);
            base.WriteD(this.uint_0);
        }
    }
}

