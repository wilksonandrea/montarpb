namespace Plugin.Core.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class PlayerReport
    {
        public long OwnerId { get; set; }

        public int TicketCount { get; set; }

        public int ReportedCount { get; set; }
    }
}

