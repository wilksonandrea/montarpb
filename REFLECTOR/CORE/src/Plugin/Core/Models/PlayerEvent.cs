namespace Plugin.Core.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class PlayerEvent
    {
        public long OwnerId { get; set; }

        public int LastQuestFinish { get; set; }

        public int LastPlaytimeFinish { get; set; }

        public int LastVisitSeqType { get; set; }

        public int LastVisitCheckDay { get; set; }

        public long LastPlaytimeValue { get; set; }

        public uint LastVisitDate { get; set; }

        public uint LastPlaytimeDate { get; set; }

        public uint LastLoginDate { get; set; }

        public uint LastXmasDate { get; set; }

        public uint LastQuestDate { get; set; }
    }
}

