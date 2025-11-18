namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_CS_ROOM_INVITED_ACK : GameServerPacket
    {
        private readonly int int_0;

        public PROTOCOL_CS_ROOM_INVITED_ACK(int int_1)
        {
            this.int_0 = int_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x76e);
            base.WriteD(this.int_0);
        }
    }
}

