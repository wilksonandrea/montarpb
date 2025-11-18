namespace Plugin.Core.Models
{
    using Plugin.Core.Enums;
    using System;
    using System.Runtime.CompilerServices;

    public class RHistoryModel
    {
        public long ObjectId { get; set; }

        public long OwnerId { get; set; }

        public long SenderId { get; set; }

        public uint Date { get; set; }

        public string OwnerNick { get; set; }

        public string SenderNick { get; set; }

        public string Message { get; set; }

        public ReportType Type { get; set; }
    }
}

