using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x02000099 RID: 153
	public class RandomBoxModel
	{
		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600072B RID: 1835 RVA: 0x000060BD File Offset: 0x000042BD
		// (set) Token: 0x0600072C RID: 1836 RVA: 0x000060C5 File Offset: 0x000042C5
		public int ItemsCount
		{
			[CompilerGenerated]
			get
			{
				return this.int_0;
			}
			[CompilerGenerated]
			set
			{
				this.int_0 = value;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600072D RID: 1837 RVA: 0x000060CE File Offset: 0x000042CE
		// (set) Token: 0x0600072E RID: 1838 RVA: 0x000060D6 File Offset: 0x000042D6
		public int MinPercent
		{
			[CompilerGenerated]
			get
			{
				return this.int_1;
			}
			[CompilerGenerated]
			set
			{
				this.int_1 = value;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600072F RID: 1839 RVA: 0x000060DF File Offset: 0x000042DF
		// (set) Token: 0x06000730 RID: 1840 RVA: 0x000060E7 File Offset: 0x000042E7
		public int MaxPercent
		{
			[CompilerGenerated]
			get
			{
				return this.int_2;
			}
			[CompilerGenerated]
			set
			{
				this.int_2 = value;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000731 RID: 1841 RVA: 0x000060F0 File Offset: 0x000042F0
		// (set) Token: 0x06000732 RID: 1842 RVA: 0x000060F8 File Offset: 0x000042F8
		public List<RandomBoxItem> Items
		{
			[CompilerGenerated]
			get
			{
				return this.list_0;
			}
			[CompilerGenerated]
			set
			{
				this.list_0 = value;
			}
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0001C8A0 File Offset: 0x0001AAA0
		public List<RandomBoxItem> GetRewardList(List<RandomBoxItem> SortedLists, int RandomId)
		{
			List<RandomBoxItem> list = new List<RandomBoxItem>();
			if (SortedLists.Count > 0)
			{
				int index = SortedLists[RandomId].Index;
				foreach (RandomBoxItem randomBoxItem in SortedLists)
				{
					if (randomBoxItem.Index == index)
					{
						list.Add(randomBoxItem);
					}
				}
			}
			return list;
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0001C918 File Offset: 0x0001AB18
		public List<RandomBoxItem> GetSortedList(int Percent)
		{
			if (Percent < this.MinPercent)
			{
				Percent = this.MinPercent;
			}
			List<RandomBoxItem> list = new List<RandomBoxItem>();
			foreach (RandomBoxItem randomBoxItem in this.Items)
			{
				if (Percent <= randomBoxItem.Percent)
				{
					list.Add(randomBoxItem);
				}
			}
			return list;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0001C990 File Offset: 0x0001AB90
		public void SetTopPercent()
		{
			int num = 100;
			int num2 = 0;
			foreach (RandomBoxItem randomBoxItem in this.Items)
			{
				if (randomBoxItem.Percent < num)
				{
					num = randomBoxItem.Percent;
				}
				if (randomBoxItem.Percent > num2)
				{
					num2 = randomBoxItem.Percent;
				}
			}
			this.MinPercent = num;
			this.MaxPercent = num2;
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x00002116 File Offset: 0x00000316
		public RandomBoxModel()
		{
		}

		// Token: 0x040002FB RID: 763
		[CompilerGenerated]
		private int int_0;

		// Token: 0x040002FC RID: 764
		[CompilerGenerated]
		private int int_1;

		// Token: 0x040002FD RID: 765
		[CompilerGenerated]
		private int int_2;

		// Token: 0x040002FE RID: 766
		[CompilerGenerated]
		private List<RandomBoxItem> list_0;
	}
}
