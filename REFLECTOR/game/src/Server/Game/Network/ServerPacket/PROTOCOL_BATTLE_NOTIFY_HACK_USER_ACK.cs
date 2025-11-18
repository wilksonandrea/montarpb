namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BATTLE_NOTIFY_HACK_USER_ACK : GameServerPacket
    {
        private readonly int int_0;

        public PROTOCOL_BATTLE_NOTIFY_HACK_USER_ACK(int int_1)
        {
            this.int_0 = int_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0xd55);
            base.WriteC((byte) this.int_0);
            base.WriteC(1);
            base.WriteD(1);
        }
    }
}

