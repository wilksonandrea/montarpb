namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_SEASON_CHALLENGE_PLUS_SEASON_EXP_ACK : GameServerPacket
    {
        private readonly uint uint_0;

        public PROTOCOL_SEASON_CHALLENGE_PLUS_SEASON_EXP_ACK(uint uint_1)
        {
            this.uint_0 = uint_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x2108);
            base.WriteH((short) 0);
            base.WriteD(this.uint_0);
            base.WriteC(1);
            base.WriteC(6);
            base.WriteD(0xa14);
            base.WriteC(5);
            base.WriteC(5);
            base.WriteC(1);
        }
    }
}

