namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_AUTH_RECV_WHISPER_ACK : GameServerPacket
    {
        private readonly string string_0;
        private readonly string string_1;
        private readonly bool bool_0;

        public PROTOCOL_AUTH_RECV_WHISPER_ACK(string string_2, string string_3, bool bool_1)
        {
            this.string_0 = string_2;
            this.string_1 = string_3;
            this.bool_0 = bool_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x726);
            base.WriteU(this.string_0, 0x42);
            base.WriteC((byte) this.bool_0);
            base.WriteC(0);
            base.WriteH((ushort) (this.string_1.Length + 1));
            base.WriteN(this.string_1, this.string_1.Length + 2, "UTF-16LE");
            base.WriteC(0);
        }
    }
}

