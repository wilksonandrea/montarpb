using System;
using System.Collections.Generic;

namespace Plugin.Core.Models
{
	// Token: 0x02000093 RID: 147
	public class RankModel
	{
		// Token: 0x060006F0 RID: 1776 RVA: 0x00005EB8 File Offset: 0x000040B8
		public RankModel(int int_0)
		{
			this.Id = int_0;
		}

		// Token: 0x040002DB RID: 731
		public int Id;

		// Token: 0x040002DC RID: 732
		public string Title;

		// Token: 0x040002DD RID: 733
		public int OnNextLevel;

		// Token: 0x040002DE RID: 734
		public int OnGoldUp;

		// Token: 0x040002DF RID: 735
		public int OnAllExp;

		// Token: 0x040002E0 RID: 736
		public SortedList<int, List<int>> Rewards;
	}
}
