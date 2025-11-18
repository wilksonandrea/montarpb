using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class VisitItemModel
	{
		public int GoodId
		{
			get;
			set;
		}

		public bool IsReward
		{
			get;
			set;
		}

		public VisitItemModel()
		{
		}

		public void SetGoodId(int GoodId)
		{
			this.GoodId = GoodId;
			if (GoodId > 0)
			{
				this.IsReward = true;
			}
		}
	}
}