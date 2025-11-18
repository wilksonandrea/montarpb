namespace Plugin.Core.Models
{
    using Plugin.Core.Utility;
    using System;
    using System.Runtime.CompilerServices;

    public class BanHistory
    {
        public BanHistory()
        {
            this.StartDate = DateTimeUtil.Now();
            this.Type = "";
            this.Value = "";
            this.Reason = "";
        }

        public long ObjectId { get; set; }

        public long PlayerId { get; set; }

        public string Type { get; set; }

        public string Value { get; set; }

        public string Reason { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}

