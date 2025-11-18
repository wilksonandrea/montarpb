using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Plugin.Core.Enums;

namespace Plugin.Core.Models
{
	// Token: 0x0200004A RID: 74
	public class BattleBoxModel
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x00003849 File Offset: 0x00001A49
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x00003851 File Offset: 0x00001A51
		public int CouponId
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

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000385A File Offset: 0x00001A5A
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x00003862 File Offset: 0x00001A62
		public int RequireTags
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

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000386B File Offset: 0x00001A6B
		// (set) Token: 0x060002B7 RID: 695 RVA: 0x00003873 File Offset: 0x00001A73
		public List<BattleBoxItem> Items
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

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x0000387C File Offset: 0x00001A7C
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x00003884 File Offset: 0x00001A84
		public List<int> Goods
		{
			[CompilerGenerated]
			get
			{
				return this.list_1;
			}
			[CompilerGenerated]
			set
			{
				this.list_1 = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060002BA RID: 698 RVA: 0x0000388D File Offset: 0x00001A8D
		// (set) Token: 0x060002BB RID: 699 RVA: 0x00003895 File Offset: 0x00001A95
		public List<double> Probabilities
		{
			[CompilerGenerated]
			get
			{
				return this.list_2;
			}
			[CompilerGenerated]
			set
			{
				this.list_2 = value;
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0001A9E0 File Offset: 0x00018BE0
		public T GetItemWithProbabilities<T>(List<T> Items, List<double> Probabilities)
		{
			if (Items == null || Items.Count == 0 || Probabilities == null || Probabilities.Count == 0 || Items.Count != Probabilities.Count)
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

		// Token: 0x060002BD RID: 701 RVA: 0x0001AA74 File Offset: 0x00018C74
		public void InitItemPercentages()
		{
			this.Goods = new List<int>();
			this.Probabilities = new List<double>();
			foreach (BattleBoxItem battleBoxItem in this.Items)
			{
				this.Goods.Add(battleBoxItem.GoodsId);
				this.Probabilities.Add((double)battleBoxItem.Percent);
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00002116 File Offset: 0x00000316
		public BattleBoxModel()
		{
		}

		// Token: 0x040000F6 RID: 246
		[CompilerGenerated]
		private int int_0;

		// Token: 0x040000F7 RID: 247
		[CompilerGenerated]
		private int int_1;

		// Token: 0x040000F8 RID: 248
		[CompilerGenerated]
		private List<BattleBoxItem> list_0;

		// Token: 0x040000F9 RID: 249
		[CompilerGenerated]
		private List<int> list_1;

		// Token: 0x040000FA RID: 250
		[CompilerGenerated]
		private List<double> list_2;
	}
}
