namespace Plugin.Core.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class VisitBoxModel
    {
        public VisitBoxModel()
        {
            this.Reward1 = new VisitItemModel();
            this.Reward2 = new VisitItemModel();
        }

        public void SetCount()
        {
            int rewardCount;
            if ((this.Reward1 != null) && (this.Reward1.GoodId > 0))
            {
                rewardCount = this.RewardCount;
                this.RewardCount = rewardCount + 1;
            }
            if ((this.Reward2 != null) && (this.Reward2.GoodId > 0))
            {
                rewardCount = this.RewardCount;
                this.RewardCount = rewardCount + 1;
            }
        }

        public VisitItemModel Reward1 { get; set; }

        public VisitItemModel Reward2 { get; set; }

        public int RewardCount { get; set; }

        public bool IsBothReward { get; set; }
    }
}

