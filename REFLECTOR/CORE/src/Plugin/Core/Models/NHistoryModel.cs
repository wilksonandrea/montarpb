namespace Plugin.Core.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class NHistoryModel
    {
        public string OldNick { get; set; }

        public string NewNick { get; set; }

        public string Motive { get; set; }

        public long ObjectId { get; set; }

        public long OwnerId { get; set; }

        public uint ChangeDate { get; set; }
    }
}

