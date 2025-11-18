using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Plugin.Core.Enums;

namespace Plugin.Core.Models
{
	// Token: 0x0200005F RID: 95
	public class PCCafeModel
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x00004208 File Offset: 0x00002408
		// (set) Token: 0x060003D1 RID: 977 RVA: 0x00004210 File Offset: 0x00002410
		public CafeEnum Type
		{
			[CompilerGenerated]
			get
			{
				return this.cafeEnum_0;
			}
			[CompilerGenerated]
			set
			{
				this.cafeEnum_0 = value;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060003D2 RID: 978 RVA: 0x00004219 File Offset: 0x00002419
		// (set) Token: 0x060003D3 RID: 979 RVA: 0x00004221 File Offset: 0x00002421
		public int PointUp
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

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060003D4 RID: 980 RVA: 0x0000422A File Offset: 0x0000242A
		// (set) Token: 0x060003D5 RID: 981 RVA: 0x00004232 File Offset: 0x00002432
		public int ExpUp
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

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060003D6 RID: 982 RVA: 0x0000423B File Offset: 0x0000243B
		// (set) Token: 0x060003D7 RID: 983 RVA: 0x00004243 File Offset: 0x00002443
		public SortedList<CafeEnum, List<ItemsModel>> Rewards
		{
			[CompilerGenerated]
			get
			{
				return this.sortedList_0;
			}
			[CompilerGenerated]
			set
			{
				this.sortedList_0 = value;
			}
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x0000424C File Offset: 0x0000244C
		public PCCafeModel(CafeEnum cafeEnum_1)
		{
			this.Type = cafeEnum_1;
			this.PointUp = 0;
			this.ExpUp = 0;
		}

		// Token: 0x04000174 RID: 372
		[CompilerGenerated]
		private CafeEnum cafeEnum_0;

		// Token: 0x04000175 RID: 373
		[CompilerGenerated]
		private int int_0;

		// Token: 0x04000176 RID: 374
		[CompilerGenerated]
		private int int_1;

		// Token: 0x04000177 RID: 375
		[CompilerGenerated]
		private SortedList<CafeEnum, List<ItemsModel>> sortedList_0;
	}
}
