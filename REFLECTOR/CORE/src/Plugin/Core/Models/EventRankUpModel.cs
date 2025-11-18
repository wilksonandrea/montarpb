namespace Plugin.Core.Models
{
    using Plugin.Core.Utility;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class EventRankUpModel
    {
        public EventRankUpModel()
        {
            this.Name = "";
            this.Description = "";
        }

        public bool EventIsEnabled()
        {
            uint num = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
            return ((this.BeginDate <= num) && (num < this.EndedDate));
        }

        public int[] GetBonuses(int RankId)
        {
            List<int[]> ranks = this.Ranks;
            lock (ranks)
            {
                using (List<int[]>.Enumerator enumerator = this.Ranks.GetEnumerator())
                {
                    while (true)
                    {
                        if (!enumerator.MoveNext())
                        {
                            break;
                        }
                        int[] current = enumerator.Current;
                        if (current[0] == RankId)
                        {
                            return new int[] { current[1], current[2], current[3] };
                        }
                    }
                }
                return new int[3];
            }
        }

        public int Id { get; set; }

        public uint BeginDate { get; set; }

        public uint EndedDate { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool Period { get; set; }

        public bool Priority { get; set; }

        public List<int[]> Ranks { get; set; }
    }
}

