namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_CS_CHATTING_ACK : GameServerPacket
    {
        private readonly string string_0;
        private readonly Account account_0;
        private readonly int int_0;
        private readonly int int_1;

        public PROTOCOL_CS_CHATTING_ACK(int int_2, int int_3)
        {
            this.int_0 = int_2;
            this.int_1 = int_3;
        }

        public PROTOCOL_CS_CHATTING_ACK(string string_1, Account account_1)
        {
            this.string_0 = string_1;
            this.account_0 = account_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x357);
            if (this.int_0 != 0)
            {
                base.WriteD(this.int_1);
            }
            else
            {
                base.WriteC((byte) (this.account_0.Nickname.Length + 1));
                base.WriteN(this.account_0.Nickname, this.account_0.Nickname.Length + 2, "UTF-16LE");
                base.WriteC((byte) this.account_0.UseChatGM());
                base.WriteC((byte) this.account_0.NickColor);
                base.WriteC((byte) (this.string_0.Length + 1));
                base.WriteN(this.string_0, this.string_0.Length + 2, "UTF-16LE");
            }
        }
    }
}

