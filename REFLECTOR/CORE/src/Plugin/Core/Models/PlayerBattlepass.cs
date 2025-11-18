namespace Plugin.Core.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class PlayerBattlepass
    {
        public int Id { get; set; }

        public int Level { get; set; }

        public bool IsPremium { get; set; }

        public int TotalPoints { get; set; }

        public int DailyPoints { get; set; }

        public uint LastRecord { get; set; }
    }
}

