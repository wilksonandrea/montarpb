using Plugin.Core.Utility;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	public class EventRankUpModel
	{
		public uint BeginDate
		{
			get;
			set;
		}

		public string Description
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

		public string Name
		{
			get;
			set;
		}

		public bool Period
		{
			get;
			set;
		}

		public bool Priority
		{
			get;
			set;
		}

		public List<int[]> Ranks
		{
			get;
			set;
		}

		public EventRankUpModel()
		{
			this.Name = "";
			this.Description = "";
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

		public int[] GetBonuses(int RankId)
		{
			int[] ınt32Array;
			lock (this.Ranks)
			{
				foreach (int[] rank in this.Ranks)
				{
					if (rank[0] != RankId)
					{
						continue;
					}
					ınt32Array = new int[] { rank[1], rank[2], rank[3] };
					return ınt32Array;
				}
				ınt32Array = new int[3];
			}
			return ınt32Array;
		}
	}
}