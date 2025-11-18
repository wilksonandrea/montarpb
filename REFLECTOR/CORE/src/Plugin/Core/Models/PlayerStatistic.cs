namespace Plugin.Core.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class PlayerStatistic
    {
        public int GetBCKDRatio() => 
            ((this.Battlecup.HeadshotsCount > 0) || (this.Battlecup.KillsCount > 0)) ? ((int) Math.Floor((double) (((this.Battlecup.KillsCount * 100) + 0.5) / ((double) (this.Battlecup.KillsCount + this.Battlecup.DeathsCount))))) : 0;

        public int GetBCWinRatio() => 
            ((this.Battlecup.MatchWins > 0) || (this.Battlecup.Matches > 0)) ? ((int) Math.Floor((double) (((this.Battlecup.MatchWins * 100) + 0.5) / ((double) (this.Battlecup.MatchWins + this.Battlecup.MatchLoses))))) : 0;

        public int GetHSRatio() => 
            (this.Basic.KillsCount > 0) ? ((int) Math.Floor((double) (((double) (this.Basic.HeadshotsCount * 100)) / (this.Basic.KillsCount + 0.5)))) : 0;

        public int GetKDRatio() => 
            ((this.Basic.HeadshotsCount > 0) || (this.Basic.KillsCount > 0)) ? ((int) Math.Floor((double) (((this.Basic.KillsCount * 100) + 0.5) / ((double) (this.Basic.KillsCount + this.Basic.DeathsCount))))) : 0;

        public int GetSeasonHSRatio() => 
            (this.Season.KillsCount > 0) ? ((int) Math.Floor((double) (((double) (this.Season.HeadshotsCount * 100)) / (this.Season.KillsCount + 0.5)))) : 0;

        public int GetSeasonKDRatio() => 
            ((this.Season.HeadshotsCount > 0) || (this.Season.KillsCount > 0)) ? ((int) Math.Floor((double) (((this.Season.KillsCount * 100) + 0.5) / ((double) (this.Season.KillsCount + this.Season.DeathsCount))))) : 0;

        public StatisticTotal Basic { get; set; }

        public StatisticSeason Season { get; set; }

        public StatisticDaily Daily { get; set; }

        public StatisticClan Clan { get; set; }

        public StatisticWeapon Weapon { get; set; }

        public StatisticAcemode Acemode { get; set; }

        public StatisticBattlecup Battlecup { get; set; }
    }
}

