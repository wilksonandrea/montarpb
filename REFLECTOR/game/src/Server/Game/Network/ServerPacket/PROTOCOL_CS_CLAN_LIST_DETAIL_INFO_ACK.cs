namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Models;
    using Plugin.Core.Network;
    using Plugin.Core.SQL;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_ACK : GameServerPacket
    {
        private readonly ClanModel clanModel_0;
        private readonly int int_0;
        private readonly Account account_0;
        private readonly int int_1;

        public PROTOCOL_CS_CLAN_LIST_DETAIL_INFO_ACK(int int_2, ClanModel clanModel_1)
        {
            this.int_0 = int_2;
            this.clanModel_0 = clanModel_1;
            if (clanModel_1 != null)
            {
                this.account_0 = AccountManager.GetAccount(clanModel_1.OwnerId, 0x1f);
                this.int_1 = DaoManagerSQL.GetClanPlayers(clanModel_1.Id);
            }
        }

        private byte[] method_0(Account account_1)
        {
            using (SyncServerPacket packet = new SyncServerPacket())
            {
                if (account_1 == null)
                {
                    packet.WriteC(0);
                }
                else
                {
                    packet.WriteC((byte) (account_1.Nickname.Length + 1));
                    packet.WriteN(account_1.Nickname, account_1.Nickname.Length + 2, "UTF-16LE");
                }
                return packet.ToArray();
            }
        }

        public override void Write()
        {
            base.WriteH((short) 0x3e8);
            base.WriteH((short) 0);
            base.WriteD(0);
            base.WriteD((int) this.clanModel_0.Points);
            base.WriteC(0);
            base.WriteD(0);
            base.WriteD(this.clanModel_0.MatchLoses);
            base.WriteD(this.clanModel_0.MatchWins);
            base.WriteD(this.clanModel_0.Matches);
            base.WriteC((byte) this.clanModel_0.MaxPlayers);
            base.WriteC((byte) this.int_1);
            base.WriteC((byte) this.clanModel_0.GetClanUnit());
            base.WriteB(this.method_0(this.account_0));
            base.WriteD(this.clanModel_0.Exp);
            base.WriteC((byte) this.clanModel_0.Rank);
            base.WriteQ((long) 0L);
            base.WriteC(0);
        }
    }
}

