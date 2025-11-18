using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class VisitBoxModel
	{
		public bool IsBothReward
		{
			get;
			set;
		}

		public VisitItemModel Reward1
		{
			get;
			set;
		}

		public VisitItemModel Reward2
		{
			get;
			set;
		}

		public int RewardCount
		{
			get;
			set;
		}

		public VisitBoxModel()
		{
			this.Reward1 = new VisitItemModel();
			this.Reward2 = new VisitItemModel();
		}

		public void SetCount()
		{
			if (this.Reward1 != null && this.Reward1.GoodId > 0)
			{
				this.RewardCount = this.RewardCount + 1;
			}
			if (this.Reward2 != null && this.Reward2.GoodId > 0)
			{
				this.RewardCount = this.RewardCount + 1;
			}
		}
	}
}