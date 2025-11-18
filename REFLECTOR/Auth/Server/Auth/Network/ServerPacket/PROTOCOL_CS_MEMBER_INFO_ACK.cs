namespace Server.Auth.Network.ServerPacket
{
    using Plugin.Core.Utility;
    using Server.Auth.Data.Models;
    using Server.Auth.Network;
    using System;
    using System.Collections.Generic;

    public class PROTOCOL_CS_MEMBER_INFO_ACK : AuthServerPacket
    {
        private readonly List<Account> list_0;

        public PROTOCOL_CS_MEMBER_INFO_ACK(List<Account> list_1)
        {
            this.list_0 = list_1;
        }

        public override void Write()
        {
            base.WriteH((short) 0x34d);
            base.WriteC((byte) this.list_0.Count);
            foreach (Account account in this.list_0)
            {
                base.WriteC((byte) (account.Nickname.Length + 1));
                base.WriteN(account.Nickname, account.Nickname.Length + 2, "UTF-16LE");
                base.WriteQ(account.PlayerId);
                base.WriteQ(ComDiv.GetClanStatus(account.Status, account.IsOnline));
                base.WriteC((byte) account.Rank);
                base.WriteD(account.Statistic.Clan.MatchWins);
                base.WriteD(account.Statistic.Clan.MatchLoses);
                base.WriteD(account.Equipment.NameCardId);
                base.WriteC((byte) account.Bonus.NickBorderColor);
                base.WriteD(0);
                base.WriteD(0);
                base.WriteD(0);
            }
        }
    }
}

