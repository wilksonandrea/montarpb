namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Utility;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_CS_REPLACE_MEMBER_BASIC_RESULT_ACK : GameServerPacket
    {
        private readonly Account account_0;
        private readonly ulong ulong_0;

        public PROTOCOL_CS_REPLACE_MEMBER_BASIC_RESULT_ACK(Account account_1)
        {
            this.account_0 = account_1;
            this.ulong_0 = ComDiv.GetClanStatus(account_1.Status, account_1.IsOnline);
        }

        public override void Write()
        {
            base.WriteH((short) 0x36c);
            base.WriteQ(this.account_0.PlayerId);
            base.WriteU(this.account_0.Nickname, 0x42);
            base.WriteC((byte) this.account_0.Rank);
            base.WriteC((byte) this.account_0.ClanAccess);
            base.WriteQ(this.ulong_0);
            base.WriteD(this.account_0.ClanDate);
            base.WriteC((byte) this.account_0.NickColor);
            base.WriteD(0);
            base.WriteD(0);
        }
    }
}

