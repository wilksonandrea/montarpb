namespace Plugin.Core.Models
{
    using Plugin.Core.Enums;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TicketModel
    {
        public string Token { get; set; }

        public TicketType Type { get; set; }

        public int GoldReward { get; set; }

        public int CashReward { get; set; }

        public int TagsReward { get; set; }

        public uint TicketCount { get; set; }

        public uint PlayerRation { get; set; }

        public List<int> Rewards { get; set; }
    }
}

