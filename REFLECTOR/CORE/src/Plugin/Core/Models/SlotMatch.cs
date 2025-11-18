namespace Plugin.Core.Models
{
    using Plugin.Core.Enums;
    using System;
    using System.Runtime.CompilerServices;

    public class SlotMatch
    {
        public SlotMatch(int int_1)
        {
            this.Id = int_1;
        }

        public int Id { get; set; }

        public long PlayerId { get; set; }

        public SlotMatchState State { get; set; }
    }
}

