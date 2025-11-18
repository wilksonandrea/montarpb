namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Utility;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BASE_UNKNOWN_PACKET_1803_ACK : GameServerPacket
    {
        private readonly string string_0;
        private readonly string string_1;

        public PROTOCOL_BASE_UNKNOWN_PACKET_1803_ACK(string string_2, string string_3)
        {
            this.string_0 = string_2;
            this.string_1 = string_3;
        }

        public override void Write()
        {
            base.WriteH((short) 0x70b);
            base.WriteD(0x1722f);
            base.WriteD(0x18a56);
            base.WriteD(0);
            base.WriteD(0xf0103);
            base.WriteC(0);
            base.WriteC(0);
            base.WriteC(0);
            base.WriteC(3);
            base.WriteC(8);
            base.WriteB(Bitwise.HexStringToByteArray("47 00 4D 00 00 00 45 00 56 00 45 00 4E 00 54 00 5F 00 38 00 00 00"));
            base.WriteD(0x38);
            base.WriteC(1);
            base.WriteD(0xabddca8);
            base.WriteB(Bitwise.HexStringToByteArray("81 E0 D0 03 09 04 15 00 80 22"));
        }
    }
}

