namespace Server.Match.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class MapModel
    {
        public BombPosition GetBomb(int BombId)
        {
            try
            {
                return this.Bombs[BombId];
            }
            catch
            {
                return null;
            }
        }

        public int Id { get; set; }

        public List<ObjectModel> Objects { get; set; }

        public List<BombPosition> Bombs { get; set; }
    }
}

