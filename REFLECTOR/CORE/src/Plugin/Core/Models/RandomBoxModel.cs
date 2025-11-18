namespace Plugin.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class RandomBoxModel
    {
        public List<RandomBoxItem> GetRewardList(List<RandomBoxItem> SortedLists, int RandomId)
        {
            List<RandomBoxItem> list = new List<RandomBoxItem>();
            if (SortedLists.Count > 0)
            {
                int num = SortedLists[RandomId].Index;
                foreach (RandomBoxItem item in SortedLists)
                {
                    if (item.Index == num)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        public List<RandomBoxItem> GetSortedList(int Percent)
        {
            if (Percent < this.MinPercent)
            {
                Percent = this.MinPercent;
            }
            List<RandomBoxItem> list = new List<RandomBoxItem>();
            foreach (RandomBoxItem item in this.Items)
            {
                if (Percent <= item.Percent)
                {
                    list.Add(item);
                }
            }
            return list;
        }

        public void SetTopPercent()
        {
            int percent = 100;
            int percent = 0;
            foreach (RandomBoxItem item in this.Items)
            {
                if (item.Percent < percent)
                {
                    percent = item.Percent;
                }
                if (item.Percent > percent)
                {
                    percent = item.Percent;
                }
            }
            this.MinPercent = percent;
            this.MaxPercent = percent;
        }

        public int ItemsCount { get; set; }

        public int MinPercent { get; set; }

        public int MaxPercent { get; set; }

        public List<RandomBoxItem> Items { get; set; }
    }
}

