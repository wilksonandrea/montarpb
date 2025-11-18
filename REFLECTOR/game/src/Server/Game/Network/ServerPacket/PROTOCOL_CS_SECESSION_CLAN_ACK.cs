namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_CS_SECESSION_CLAN_ACK : GameServerPacket
    {
        private readonly uint uint_0;

        public PROTOCOL_CS_SECESSION_CLAN_ACK(uint uint_1)
        {
            this.uint_0 = uint_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x33d);
            base.WriteD(this.uint_0);
        }
    }
}

