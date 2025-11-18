namespace Plugin.Core.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class PlayerTitles
    {
        public PlayerTitles()
        {
            this.Slots = 1;
        }

        public long Add(long flag)
        {
            this.Flags |= flag;
            return this.Flags;
        }

        public bool Contains(long flag) => 
            ((this.Flags & flag) == flag) || (flag == 0L);

        public int GetEquip(int index) => 
            (index != 0) ? ((index != 1) ? ((index != 2) ? 0 : this.Equiped3) : this.Equiped2) : this.Equiped1;

        public void SetEquip(int index, int value)
        {
            if (index == 0)
            {
                this.Equiped1 = value;
            }
            else if (index == 1)
            {
                this.Equiped2 = value;
            }
            else if (index == 2)
            {
                this.Equiped3 = value;
            }
        }

        public long OwnerId { get; set; }

        public long Flags { get; set; }

        public int Equiped1 { get; set; }

        public int Equiped2 { get; set; }

        public int Equiped3 { get; set; }

        public int Slots { get; set; }
    }
}

