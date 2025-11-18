namespace Plugin.Core.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class PassBoxModel
    {
        public PassBoxModel()
        {
            this.Normal = new PassItemModel();
            this.PremiumA = new PassItemModel();
            this.PremiumB = new PassItemModel();
        }

        public void SetCount()
        {
            if ((this.Normal != null) && (this.Normal.GoodId > 0))
            {
                this.RewardCount++;
            }
            if ((this.PremiumA != null) && (this.PremiumA.GoodId > 0))
            {
                this.RewardCount++;
            }
            if ((this.PremiumB != null) && (this.PremiumB.GoodId > 0))
            {
                this.RewardCount++;
            }
        }

        public PassItemModel Normal { get; set; }

        public PassItemModel PremiumA { get; set; }

        public PassItemModel PremiumB { get; set; }

        public int RequiredPoints { get; set; }

        public int RewardCount { get; set; }

        public int Card { get; set; }
    }
}

