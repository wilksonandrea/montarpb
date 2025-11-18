namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_CS_ROOM_INVITED_RESULT_ACK : GameServerPacket
    {
        private readonly long long_0;

        public PROTOCOL_CS_ROOM_INVITED_RESULT_ACK(long long_1)
        {
            this.long_0 = long_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x76f);
            base.WriteQ(this.long_0);
        }
    }
}

