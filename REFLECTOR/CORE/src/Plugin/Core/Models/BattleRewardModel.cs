namespace Plugin.Core.Models
{
    using Plugin.Core.Enums;
    using System;
    using System.Runtime.CompilerServices;

    public class BattleRewardModel
    {
        public BattleRewardType Type { get; set; }

        public int Percentage { get; set; }

        public bool Enable { get; set; }

        public int[] Rewards { get; set; }
    }
}

