namespace Plugin.Core.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class TitleModel
    {
        public TitleModel()
        {
        }

        public TitleModel(int int_10)
        {
            this.Id = int_10;
            this.Flag = 1L << (int_10 & 0x3f);
        }

        public int Id { get; set; }

        public int ClassId { get; set; }

        public int Medal { get; set; }

        public int Ribbon { get; set; }

        public int MasterMedal { get; set; }

        public int Ensign { get; set; }

        public int Rank { get; set; }

        public int Slot { get; set; }

        public int Req1 { get; set; }

        public int Req2 { get; set; }

        public long Flag { get; set; }
    }
}

