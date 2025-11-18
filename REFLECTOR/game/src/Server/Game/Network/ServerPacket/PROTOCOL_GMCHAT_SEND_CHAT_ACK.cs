namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_GMCHAT_SEND_CHAT_ACK : GameServerPacket
    {
        private readonly Account account_0;
        private readonly string string_0;
        private readonly string string_1;
        private readonly string string_2;

        public PROTOCOL_GMCHAT_SEND_CHAT_ACK(string string_3, string string_4, string string_5, Account account_1)
        {
            this.string_0 = string_3;
            this.string_2 = string_4;
            this.string_1 = string_5;
            this.account_0 = account_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x1a04);
            base.WriteH((short) 0);
            base.WriteD(0);
            base.WriteH((short) ((byte) this.string_2.Length));
            base.WriteU(this.string_2, this.string_2.Length * 2);
            base.WriteC((byte) this.string_1.Length);
            base.WriteU(this.string_1, this.string_1.Length * 2);
        }
    }
}

