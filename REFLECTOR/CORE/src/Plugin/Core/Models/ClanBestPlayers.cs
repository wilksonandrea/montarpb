namespace Plugin.Core.Models
{
    using Plugin.Core.Utility;
    using System;
    using System.Runtime.CompilerServices;

    public class ClanBestPlayers
    {
        public long GetPlayerId(string[] Split)
        {
            try
            {
                return long.Parse(Split[0]);
            }
            catch
            {
                return 0L;
            }
        }

        public int GetPlayerValue(string[] Split)
        {
            try
            {
                return int.Parse(Split[1]);
            }
            catch
            {
                return 0;
            }
        }

        public void SetBestExp(SlotModel Slot)
        {
            if (Slot.Exp > this.Exp.RecordValue)
            {
                this.Exp.PlayerId = Slot.PlayerId;
                this.Exp.RecordValue = Slot.Exp;
            }
        }

        public void SetBestHeadshot(SlotModel Slot)
        {
            if (Slot.AllHeadshots > this.Headshots.RecordValue)
            {
                this.Headshots.PlayerId = Slot.PlayerId;
                this.Headshots.RecordValue = Slot.AllHeadshots;
            }
        }

        public void SetBestKills(SlotModel Slot)
        {
            if (Slot.AllKills > this.Kills.RecordValue)
            {
                this.Kills.PlayerId = Slot.PlayerId;
                this.Kills.RecordValue = Slot.AllKills;
            }
        }

        public void SetBestParticipation(PlayerStatistic Stat, SlotModel Slot)
        {
            StatisticClan clan = Stat.Clan;
            int vALUE = clan.Matches + 1;
            clan.Matches = vALUE;
            ComDiv.UpdateDB("player_stat_clans", "clan_matches", vALUE, "owner_id", Slot.PlayerId);
            if (Stat.Clan.Matches > this.Participation.RecordValue)
            {
                this.Participation.PlayerId = Slot.PlayerId;
                this.Participation.RecordValue = Stat.Clan.Matches;
            }
        }

        public void SetBestWins(PlayerStatistic Stat, SlotModel Slot, bool WonTheMatch)
        {
            if (WonTheMatch)
            {
                StatisticClan clan = Stat.Clan;
                int vALUE = clan.MatchWins + 1;
                clan.MatchWins = vALUE;
                ComDiv.UpdateDB("player_stat_clans", "clan_match_wins", vALUE, "owner_id", Slot.PlayerId);
                if (Stat.Clan.MatchWins > this.Wins.RecordValue)
                {
                    this.Wins.PlayerId = Slot.PlayerId;
                    this.Wins.RecordValue = Stat.Clan.MatchWins;
                }
            }
        }

        public void SetDefault()
        {
            string[] strArray = new string[] { "0", "0" };
            this.Exp = new RecordInfo(strArray);
            this.Participation = new RecordInfo(strArray);
            this.Wins = new RecordInfo(strArray);
            this.Kills = new RecordInfo(strArray);
            this.Headshots = new RecordInfo(strArray);
        }

        public void SetPlayers(string Exp, string Participation, string Wins, string Kills, string Headshots)
        {
            char[] separator = new char[] { '-' };
            this.Exp = new RecordInfo(Exp.Split(separator));
            char[] chArray2 = new char[] { '-' };
            this.Participation = new RecordInfo(Participation.Split(chArray2));
            char[] chArray3 = new char[] { '-' };
            this.Wins = new RecordInfo(Wins.Split(chArray3));
            char[] chArray4 = new char[] { '-' };
            this.Kills = new RecordInfo(Kills.Split(chArray4));
            char[] chArray5 = new char[] { '-' };
            this.Headshots = new RecordInfo(Headshots.Split(chArray5));
        }

        public RecordInfo Exp { get; set; }

        public RecordInfo Participation { get; set; }

        public RecordInfo Wins { get; set; }

        public RecordInfo Kills { get; set; }

        public RecordInfo Headshots { get; set; }
    }
}

