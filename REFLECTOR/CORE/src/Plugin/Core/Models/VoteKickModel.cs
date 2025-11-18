namespace Plugin.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class VoteKickModel
    {
        public VoteKickModel(int int_7, int int_8)
        {
            this.Accept = 1;
            this.Denie = 1;
            this.CreatorIdx = int_7;
            this.VictimIdx = int_8;
            this.Votes = new List<int>();
            this.Votes.Add(int_7);
            this.Votes.Add(int_8);
            this.TotalArray = new bool[0x12];
        }

        public int GetInGamePlayers()
        {
            int num = 0;
            for (int i = 0; i < 0x12; i++)
            {
                if (this.TotalArray[i])
                {
                    num++;
                }
            }
            return num;
        }

        public int CreatorIdx { get; set; }

        public int VictimIdx { get; set; }

        public int Motive { get; set; }

        public int Accept { get; set; }

        public int Denie { get; set; }

        public int Allies { get; set; }

        public int Enemies { get; set; }

        public List<int> Votes { get; set; }

        public bool[] TotalArray { get; set; }
    }
}

