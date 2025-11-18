namespace Plugin.Core.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class InternetCafe
    {
        public InternetCafe(int int_5)
        {
            this.ConfigId = int_5;
        }

        public int ConfigId { get; set; }

        public int BasicExp { get; set; }

        public int BasicGold { get; set; }

        public int PremiumExp { get; set; }

        public int PremiumGold { get; set; }
    }
}

