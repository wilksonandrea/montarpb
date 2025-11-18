namespace Plugin.Core.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class MapMatch
    {
        public MapMatch(int int_4)
        {
            this.Mode = int_4;
        }

        public int Mode { get; set; }

        public int Id { get; set; }

        public int Limit { get; set; }

        public int Tag { get; set; }

        public string Name { get; set; }
    }
}

