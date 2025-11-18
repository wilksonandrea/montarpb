namespace Plugin.Core.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class ItemsRepair
    {
        public int Id { get; set; }

        public int Point { get; set; }

        public int Cash { get; set; }

        public uint Quantity { get; set; }

        public bool Enable { get; set; }
    }
}

