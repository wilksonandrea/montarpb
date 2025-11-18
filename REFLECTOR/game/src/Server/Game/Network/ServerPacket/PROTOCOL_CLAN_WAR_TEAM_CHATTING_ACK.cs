namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Network;
    using System;

    public class PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK : GameServerPacket
    {
        private readonly int int_0;
        private readonly int int_1;
        private readonly string string_0;
        private readonly string string_1;

        public PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK(int int_2, int int_3)
        {
            this.int_0 = int_2;
            this.int_1 = int_3;
        }

        public PROTOCOL_CLAN_WAR_TEAM_CHATTING_ACK(string string_2, string string_3)
        {
            this.string_1 = string_2;
            this.string_0 = string_3;
        }

        public override void Write()
        {
            base.WriteH((short) 0x1b11);
            base.WriteC((byte) this.int_0);
            if (this.int_0 != 0)
            {
                base.WriteD(this.int_1);
            }
            else
            {
                base.WriteC((byte) (this.string_1.Length + 1));
                base.WriteN(this.string_1, this.string_1.Length + 2, "UTF-16LE");
                base.WriteC((byte) (this.string_0.Length + 1));
                base.WriteN(this.string_0, this.string_0.Length + 2, "UTF-16LE");
            }
        }
    }
}

