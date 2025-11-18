namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_GMCHAT_END_CHAT_ACK : GameServerPacket
    {
        private readonly Account account_0;
        private readonly uint uint_0;

        public PROTOCOL_GMCHAT_END_CHAT_ACK(uint uint_1, Account account_1)
        {
            this.uint_0 = uint_1;
            this.account_0 = account_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x1a06);
            base.WriteH((short) 0);
            base.WriteD(this.uint_0);
        }
    }
}

