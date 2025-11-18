using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x02000091 RID: 145
	public class PlayerTopup
	{
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060006D4 RID: 1748 RVA: 0x00005D90 File Offset: 0x00003F90
		// (set) Token: 0x060006D5 RID: 1749 RVA: 0x00005D98 File Offset: 0x00003F98
		public long ObjectId
		{
			[CompilerGenerated]
			get
			{
				return this.long_0;
			}
			[CompilerGenerated]
			set
			{
				this.long_0 = value;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x00005DA1 File Offset: 0x00003FA1
		// (set) Token: 0x060006D7 RID: 1751 RVA: 0x00005DA9 File Offset: 0x00003FA9
		public long PlayerId
		{
			[CompilerGenerated]
			get
			{
				return this.long_1;
			}
			[CompilerGenerated]
			set
			{
				this.long_1 = value;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060006D8 RID: 1752 RVA: 0x00005DB2 File Offset: 0x00003FB2
		// (set) Token: 0x060006D9 RID: 1753 RVA: 0x00005DBA File Offset: 0x00003FBA
		public int GoodsId
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

		// Token: 0x060006DA RID: 1754 RVA: 0x00002116 File Offset: 0x00000316
		public PlayerTopup()
		{
		}

		// Token: 0x040002D1 RID: 721
		[CompilerGenerated]
		private long long_0;

		// Token: 0x040002D2 RID: 722
		[CompilerGenerated]
		private long long_1;

		// Token: 0x040002D3 RID: 723
		[CompilerGenerated]
		private int int_0;
	}
}
