namespace Plugin.Core.Models
{
    using Plugin.Core;
    using Plugin.Core.Enums;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class BattleBoxModel
    {
        public T GetItemWithProbabilities<T>(List<T> Items, List<double> Probabilities)
        {
            if ((Items == null) || ((Items.Count == 0) || ((Probabilities == null) || ((Probabilities.Count == 0) || (Items.Count != Probabilities.Count)))))
            {
                CLogger.Print("Battle Box Item List Is Not Valid!", LoggerType.Warning, null);
            }
            double num = new Random().NextDouble();
            double num2 = 0.0;
            for (int i = 0; i < Items.Count; i++)
            {
                num2 += Probabilities[i] / 100.0;
                if (num < num2)
                {
                    return Items[i];
                }
            }
            return Items[Items.Count - 1];
        }

        public void InitItemPercentages()
        {
            this.Goods = new List<int>();
            this.Probabilities = new List<double>();
            foreach (BattleBoxItem item in this.Items)
            {
                this.Goods.Add(item.GoodsId);
                this.Probabilities.Add((double) item.Percent);
            }
        }

        public int CouponId { get; set; }

        public int RequireTags { get; set; }

        public List<BattleBoxItem> Items { get; set; }

        public List<int> Goods { get; set; }

        public List<double> Probabilities { get; set; }
    }
}

