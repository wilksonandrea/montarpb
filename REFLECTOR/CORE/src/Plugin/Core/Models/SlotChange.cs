namespace Plugin.Core.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class SlotChange
    {
        public SlotChange(SlotModel slotModel_2, SlotModel slotModel_3)
        {
            this.OldSlot = slotModel_2;
            this.NewSlot = slotModel_3;
        }

        public SlotModel OldSlot { get; set; }

        public SlotModel NewSlot { get; set; }
    }
}

