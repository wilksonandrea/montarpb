namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BATTLE_READYBATTLE_ACK : GameServerPacket
    {
        private readonly uint uint_0;

        public PROTOCOL_BATTLE_READYBATTLE_ACK(uint uint_1)
        {
            this.uint_0 = uint_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x1404);
            base.WriteC(0);
            base.WriteH((short) 0);
            base.WriteD(this.uint_0);
        }
    }
}

