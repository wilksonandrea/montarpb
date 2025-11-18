namespace Plugin.Core.Models
{
    using Plugin.Core.Utility;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class EventVisitModel
    {
        public EventVisitModel()
        {
            this.Checks = 0x1f;
            this.Title = "";
        }

        public bool EventIsEnabled()
        {
            uint num = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
            return ((this.BeginDate <= num) && (num < this.EndedDate));
        }

        public List<VisitItemModel> GetReward(int Day, int Type)
        {
            List<VisitItemModel> list = new List<VisitItemModel>();
            if (Type == 0)
            {
                list.Add(this.Boxes[Day].Reward1);
            }
            else if (Type == 1)
            {
                list.Add(this.Boxes[Day].Reward2);
            }
            else
            {
                list.Add(this.Boxes[Day].Reward1);
                list.Add(this.Boxes[Day].Reward2);
            }
            return list;
        }

        public void SetBoxCounts()
        {
            for (int i = 0; i < 0x1f; i++)
            {
                this.Boxes[i].SetCount();
            }
        }

        public int Id { get; set; }

        public uint BeginDate { get; set; }

        public uint EndedDate { get; set; }

        public int Checks { get; set; }

        public string Title { get; set; }

        public List<VisitBoxModel> Boxes { get; set; }
    }
}

