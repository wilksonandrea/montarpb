using Plugin.Core.Utility;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class EventVisitModel
	{
		public uint BeginDate
		{
			get;
			set;
		}

		public List<VisitBoxModel> Boxes
		{
			get;
			set;
		}

		public int Checks
		{
			get;
			set;
		}

		public uint EndedDate
		{
			get;
			set;
		}

		public int Id
		{
			get;
			set;
		}

		public string Title
		{
			get;
			set;
		}

		public EventVisitModel()
		{
			this.Checks = 31;
			this.Title = "";
		}

		public bool EventIsEnabled()
		{
			uint uInt32 = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
			if (this.BeginDate > uInt32)
			{
				return false;
			}
			return uInt32 < this.EndedDate;
		}

		public List<VisitItemModel> GetReward(int Day, int Type)
		{
			List<VisitItemModel> visitItemModels = new List<VisitItemModel>();
			if (Type == 0)
			{
				visitItemModels.Add(this.Boxes[Day].Reward1);
			}
			else if (Type != 1)
			{
				visitItemModels.Add(this.Boxes[Day].Reward1);
				visitItemModels.Add(this.Boxes[Day].Reward2);
			}
			else
			{
				visitItemModels.Add(this.Boxes[Day].Reward2);
			}
			return visitItemModels;
		}

		public void SetBoxCounts()
		{
			for (int i = 0; i < 31; i++)
			{
				this.Boxes[i].SetCount();
			}
		}
	}
}