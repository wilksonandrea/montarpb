namespace Plugin.Core.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class ShopData
    {
        public byte[] Buffer { get; set; }

        public int ItemsCount { get; set; }

        public int Offset { get; set; }
    }
}

