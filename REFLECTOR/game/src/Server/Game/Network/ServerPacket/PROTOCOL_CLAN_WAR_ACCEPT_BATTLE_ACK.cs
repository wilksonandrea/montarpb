namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_CLAN_WAR_ACCEPT_BATTLE_ACK : GameServerPacket
    {
        private readonly uint uint_0;

        public PROTOCOL_CLAN_WAR_ACCEPT_BATTLE_ACK(uint uint_1)
        {
            this.uint_0 = uint_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x617);
            base.WriteD(this.uint_0);
        }
    }
}

