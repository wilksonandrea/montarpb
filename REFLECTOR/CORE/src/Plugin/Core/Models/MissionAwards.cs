namespace Plugin.Core.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class MissionAwards
    {
        public MissionAwards(int int_4, int int_5, int int_6, int int_7)
        {
            this.Id = int_4;
            this.MasterMedal = int_5;
            this.Exp = int_6;
            this.Gold = int_7;
        }

        public int Id { get; set; }

        public int MasterMedal { get; set; }

        public int Exp { get; set; }

        public int Gold { get; set; }
    }
}

