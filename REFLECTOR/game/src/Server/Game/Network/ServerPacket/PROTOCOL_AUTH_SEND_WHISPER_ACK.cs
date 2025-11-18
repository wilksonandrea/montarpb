namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_AUTH_SEND_WHISPER_ACK : GameServerPacket
    {
        private readonly string string_0;
        private readonly string string_1;
        private readonly uint uint_0;
        private readonly int int_0;
        private readonly int int_1;

        public PROTOCOL_AUTH_SEND_WHISPER_ACK(string string_2, string string_3, uint uint_1)
        {
            this.string_0 = string_2;
            this.string_1 = string_3;
            this.uint_0 = uint_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x723);
            base.WriteD(this.uint_0);
            base.WriteC((byte) this.int_0);
            base.WriteU(this.string_0, 0x42);
            if (this.uint_0 == 0)
            {
                base.WriteH((ushort) (this.string_1.Length + 1));
                base.WriteN(this.string_1, this.string_1.Length + 2, "UTF-16LE");
            }
        }
    }
}

