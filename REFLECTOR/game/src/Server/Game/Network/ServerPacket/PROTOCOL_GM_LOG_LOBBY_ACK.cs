namespace Server.Game.Network.ServerPacket
{
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_GM_LOG_LOBBY_ACK : GameServerPacket
    {
        private readonly Account account_0;
        private readonly uint uint_0;

        public PROTOCOL_GM_LOG_LOBBY_ACK(uint uint_1, long long_0)
        {
            this.uint_0 = uint_1;
            this.account_0 = AccountManager.GetAccount(long_0, true);
        }

        public override void Write()
        {
            base.WriteH((short) 0xa7d);
            base.WriteD(this.uint_0);
            base.WriteQ(this.account_0.PlayerId);
        }
    }
}

