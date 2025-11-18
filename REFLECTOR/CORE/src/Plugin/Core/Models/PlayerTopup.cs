namespace Plugin.Core.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class PlayerTopup
    {
        public long ObjectId { get; set; }

        public long PlayerId { get; set; }

        public int GoodsId { get; set; }
    }
}

