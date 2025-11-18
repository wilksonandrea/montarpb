namespace Plugin.Core.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class StatisticAcemode
    {
        public long OwnerId { get; set; }

        public int Matches { get; set; }

        public int MatchWins { get; set; }

        public int MatchLoses { get; set; }

        public int Kills { get; set; }

        public int Deaths { get; set; }

        public int Headshots { get; set; }

        public int Assists { get; set; }

        public int Escapes { get; set; }

        public int Winstreaks { get; set; }
    }
}

