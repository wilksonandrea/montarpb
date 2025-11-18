namespace Plugin.Core.Models
{
    using Plugin.Core.XML;
    using System;
    using System.Runtime.CompilerServices;

    public class PlayerCompetitive
    {
        public CompetitiveRank Rank() => 
            CompetitiveXML.GetRank(this.Level) ?? new CompetitiveRank();

        public long OwnerId { get; set; }

        public int Level { get; set; }

        public int Points { get; set; }
    }
}

