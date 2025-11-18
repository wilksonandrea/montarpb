namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK : GameServerPacket
    {
        private readonly string string_0;

        public PROTOCOL_SERVER_MESSAGE_ANNOUNCE_ACK(string string_1)
        {
            this.string_0 = string_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0xc07);
            base.WriteH((short) 0);
            base.WriteD(0);
            base.WriteH((ushort) this.string_0.Length);
            base.WriteN(this.string_0, this.string_0.Length, "UTF-16LE");
            base.WriteD(2);
        }
    }
}

