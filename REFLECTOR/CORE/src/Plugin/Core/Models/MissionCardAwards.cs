namespace Plugin.Core.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class MissionCardAwards
    {
        public bool Unusable() => 
            (this.Ensign == 0) && ((this.Medal == 0) && ((this.Ribbon == 0) && ((this.Exp == 0) && (this.Gold == 0))));

        public int Id { get; set; }

        public int Card { get; set; }

        public int Ensign { get; set; }

        public int Medal { get; set; }

        public int Ribbon { get; set; }

        public int Exp { get; set; }

        public int Gold { get; set; }
    }
}

