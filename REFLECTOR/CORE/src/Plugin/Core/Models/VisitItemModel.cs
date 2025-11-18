namespace Plugin.Core.Models
{
    using System;
    using System.Runtime.CompilerServices;

    public class VisitItemModel
    {
        public void SetGoodId(int GoodId)
        {
            this.GoodId = GoodId;
            if (GoodId > 0)
            {
                this.IsReward = true;
            }
        }

        public int GoodId { get; set; }

        public bool IsReward { get; set; }
    }
}

