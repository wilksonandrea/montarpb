namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Models;
    using Plugin.Core.SQL;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK : GameServerPacket
    {
        private readonly uint uint_0;
        private readonly ClanModel clanModel_0;
        private readonly Account account_0;
        private readonly int int_0;

        public PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK(uint uint_1)
        {
            this.uint_0 = uint_1;
        }

        public PROTOCOL_CLAN_WAR_MATCH_TEAM_INFO_ACK(uint uint_1, ClanModel clanModel_1)
        {
            this.uint_0 = uint_1;
            this.clanModel_0 = clanModel_1;
            if (this.clanModel_0 != null)
            {
                this.int_0 = DaoManagerSQL.GetClanPlayers(clanModel_1.Id);
                this.account_0 = AccountManager.GetAccount(clanModel_1.OwnerId, 0x1f);
                if (this.account_0 == null)
                {
                    this.uint_0 = 0x80000000;
                }
            }
        }

        public override void Write()
        {
            base.WriteH((short) 0x622);
            base.WriteD(this.uint_0);
            if (this.uint_0 == 0)
            {
                base.WriteD(this.clanModel_0.Id);
                base.WriteS(this.clanModel_0.Name, 0x11);
                base.WriteC((byte) this.clanModel_0.Rank);
                base.WriteC((byte) this.int_0);
                base.WriteC((byte) this.clanModel_0.MaxPlayers);
                base.WriteD(this.clanModel_0.CreationDate);
                base.WriteD(this.clanModel_0.Logo);
                base.WriteC((byte) this.clanModel_0.NameColor);
                base.WriteC((byte) this.clanModel_0.GetClanUnit());
                base.WriteD(this.clanModel_0.Exp);
                base.WriteD(0);
                base.WriteQ(this.clanModel_0.OwnerId);
                base.WriteS(this.account_0.Nickname, 0x21);
                base.WriteC((byte) this.account_0.Rank);
                base.WriteS("", 0xff);
            }
        }
    }
}

