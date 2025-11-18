namespace Plugin.Core.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class StatisticClan
    {
        public long OwnerId { get; set; }

        public int Matches { get; set; }

        public int MatchWins { get; set; }

        public int MatchLoses { get; set; }
    }
}

