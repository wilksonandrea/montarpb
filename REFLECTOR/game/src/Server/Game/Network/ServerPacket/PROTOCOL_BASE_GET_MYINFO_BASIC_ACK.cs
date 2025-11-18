namespace Server.Game.Network.ServerPacket
{
    using Plugin.Core.Models;
    using Server.Game.Data.Managers;
    using Server.Game.Data.Models;
    using Server.Game.Network;
    using System;

    public class PROTOCOL_BASE_GET_MYINFO_BASIC_ACK : GameServerPacket
    {
        private readonly Account account_0;
        private readonly ClanModel clanModel_0;

        public PROTOCOL_BASE_GET_MYINFO_BASIC_ACK(Account account_1)
        {
            this.account_0 = account_1;
            if (account_1 != null)
            {
                this.clanModel_0 = ClanManager.GetClan(account_1.ClanId);
            }
        }

        public override void Write()
        {
            base.WriteH((short) 0x943);
            base.WriteU(this.account_0.Nickname, 0x42);
            base.WriteD(this.account_0.GetRank());
            base.WriteD(this.account_0.GetRank());
            base.WriteD(this.account_0.Gold);
            base.WriteD(this.account_0.Exp);
            base.WriteD(0);
            base.WriteC(0);
            base.WriteQ((long) 0L);
            base.WriteC((byte) this.account_0.AuthLevel());
            base.WriteC(0);
            base.WriteD(this.account_0.Tags);
            base.WriteD(this.account_0.Cash);
            base.WriteD(this.clanModel_0.Id);
            base.WriteD(this.account_0.ClanAccess);
            base.WriteQ(this.account_0.StatusId());
            base.WriteC((byte) this.account_0.CafePC);
            base.WriteC((byte) this.account_0.Country);
            base.WriteU(this.clanModel_0.Name, 0x22);
            base.WriteC((byte) this.clanModel_0.Rank);
            base.WriteC((byte) this.clanModel_0.GetClanUnit());
            base.WriteD(this.clanModel_0.Logo);
            base.WriteC((byte) this.clanModel_0.NameColor);
            base.WriteC((byte) this.clanModel_0.Effect);
            base.WriteD(this.account_0.Statistic.Season.Matches);
            base.WriteD(this.account_0.Statistic.Season.MatchWins);
            base.WriteD(this.account_0.Statistic.Season.MatchLoses);
            base.WriteD(this.account_0.Statistic.Season.MatchDraws);
            base.WriteD(this.account_0.Statistic.Season.KillsCount);
            base.WriteD(this.account_0.Statistic.Season.HeadshotsCount);
            base.WriteD(this.account_0.Statistic.Season.DeathsCount);
            base.WriteD(this.account_0.Statistic.Season.TotalMatchesCount);
            base.WriteD(this.account_0.Statistic.Season.TotalKillsCount);
            base.WriteD(this.account_0.Statistic.Season.EscapesCount);
            base.WriteD(this.account_0.Statistic.Season.AssistsCount);
            base.WriteD(this.account_0.Statistic.Season.MvpCount);
            base.WriteD(this.account_0.Statistic.Basic.Matches);
            base.WriteD(this.account_0.Statistic.Basic.MatchWins);
            base.WriteD(this.account_0.Statistic.Basic.MatchLoses);
            base.WriteD(this.account_0.Statistic.Basic.MatchDraws);
            base.WriteD(this.account_0.Statistic.Basic.KillsCount);
            base.WriteD(this.account_0.Statistic.Basic.HeadshotsCount);
            base.WriteD(this.account_0.Statistic.Basic.DeathsCount);
            base.WriteD(this.account_0.Statistic.Basic.TotalMatchesCount);
            base.WriteD(this.account_0.Statistic.Basic.TotalKillsCount);
            base.WriteD(this.account_0.Statistic.Basic.EscapesCount);
            base.WriteD(this.account_0.Statistic.Basic.AssistsCount);
            base.WriteD(this.account_0.Statistic.Basic.MvpCount);
        }
    }
}

