namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_SEASON_CHALLENGE_SEND_REWARD_ACK : GameServerPacket
    {
        private readonly uint uint_0;
        private readonly int[] int_0;

        public PROTOCOL_SEASON_CHALLENGE_SEND_REWARD_ACK(uint uint_1, int[] int_1)
        {
            this.uint_0 = uint_1;
            this.int_0 = int_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x2105);
            base.WriteH((short) 0);
            base.WriteD(this.uint_0);
            if (this.uint_0 == 0)
            {
                base.WriteC(2);
                base.WriteD(this.int_0[1]);
                base.WriteD(this.int_0[2]);
                base.WriteD(this.int_0[0]);
                base.WriteC(1);
                base.WriteC(1);
                base.WriteC(1);
            }
        }
    }
}

