namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_ROOM_CHATTING_ACK : GameServerPacket
    {
        private readonly string string_0;
        private readonly int int_0;
        private readonly int int_1;
        private readonly bool bool_0;

        public PROTOCOL_ROOM_CHATTING_ACK(int int_2, int int_3, bool bool_1, string string_1)
        {
            this.int_0 = int_2;
            this.int_1 = int_3;
            this.bool_0 = bool_1;
            this.string_0 = string_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0xe16);
            base.WriteH((short) this.int_0);
            base.WriteD(this.int_1);
            base.WriteC((byte) this.bool_0);
            base.WriteD((int) (this.string_0.Length + 1));
            base.WriteN(this.string_0, this.string_0.Length + 2, "UTF-16LE");
        }
    }
}

