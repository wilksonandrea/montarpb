namespace Plugin.Core.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class PlayerVip
    {
        public long OwnerId { get; set; }

        public string Address { get; set; }

        public string Benefit { get; set; }

        public uint Expirate { get; set; }
    }
}

