namespace Server.Match.Data.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class ItemsStatistic
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Damage { get; set; }

        public int HelmetPenetrate { get; set; }

        public int BulletLoaded { get; set; }

        public int BulletTotal { get; set; }

        public float FireDelay { get; set; }

        public float Range { get; set; }
    }
}

