namespace Plugin.Core.Models
{
    using Plugin.Core.Enums;
    using Plugin.Core.SQL;
    using System;

    public class ClanModel
    {
        public int Id;
        public int Matches;
        public int MatchWins;
        public int MatchLoses;
        public int TotalKills;
        public int TotalHeadshots;
        public int TotalDeaths;
        public int TotalAssists;
        public int TotalEscapes;
        public int Authority;
        public int RankLimit;
        public int MinAgeLimit;
        public int MaxAgeLimit;
        public int Exp;
        public int Rank;
        public int NameColor;
        public int MaxPlayers = 50;
        public int Effect;
        public string Name = "";
        public string Info = "";
        public string News = "";
        public long OwnerId;
        public uint Logo = uint.MaxValue;
        public uint CreationDate;
        public float Points = 1000f;
        public JoinClanType JoinType;
        public ClanBestPlayers BestPlayers = new ClanBestPlayers();

        public int GetClanUnit() => 
            this.GetClanUnit(DaoManagerSQL.GetClanPlayers(this.Id));

        public int GetClanUnit(int Count) => 
            (Count < 250) ? ((Count < 200) ? ((Count < 150) ? ((Count < 100) ? ((Count < 50) ? ((Count < 30) ? ((Count < 10) ? 0 : 1) : 2) : 3) : 4) : 5) : 6) : 7;
    }
}

