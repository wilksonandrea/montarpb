using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x02000068 RID: 104
	public class PlayerTitles
	{
		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000459 RID: 1113 RVA: 0x00004717 File Offset: 0x00002917
		// (set) Token: 0x0600045A RID: 1114 RVA: 0x0000471F File Offset: 0x0000291F
		public long OwnerId
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

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600045B RID: 1115 RVA: 0x00004728 File Offset: 0x00002928
		// (set) Token: 0x0600045C RID: 1116 RVA: 0x00004730 File Offset: 0x00002930
		public long Flags
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

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600045D RID: 1117 RVA: 0x00004739 File Offset: 0x00002939
		// (set) Token: 0x0600045E RID: 1118 RVA: 0x00004741 File Offset: 0x00002941
		public int Equiped1
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

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0000474A File Offset: 0x0000294A
		// (set) Token: 0x06000460 RID: 1120 RVA: 0x00004752 File Offset: 0x00002952
		public int Equiped2
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

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0000475B File Offset: 0x0000295B
		// (set) Token: 0x06000462 RID: 1122 RVA: 0x00004763 File Offset: 0x00002963
		public int Equiped3
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

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0000476C File Offset: 0x0000296C
		// (set) Token: 0x06000464 RID: 1124 RVA: 0x00004774 File Offset: 0x00002974
		public int Slots
		{
			[CompilerGenerated]
			get
			{
				return this.int_3;
			}
			[CompilerGenerated]
			set
			{
				this.int_3 = value;
			}
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000477D File Offset: 0x0000297D
		public PlayerTitles()
		{
			this.Slots = 1;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x0000478C File Offset: 0x0000298C
		public long Add(long flag)
		{
			this.Flags |= flag;
			return this.Flags;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x000047A2 File Offset: 0x000029A2
		public bool Contains(long flag)
		{
			return (this.Flags & flag) == flag || flag == 0L;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x000047BD File Offset: 0x000029BD
		public void SetEquip(int index, int value)
		{
			if (index == 0)
			{
				this.Equiped1 = value;
				return;
			}
			if (index == 1)
			{
				this.Equiped2 = value;
				return;
			}
			if (index == 2)
			{
				this.Equiped3 = value;
			}
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x000047E1 File Offset: 0x000029E1
		public int GetEquip(int index)
		{
			if (index == 0)
			{
				return this.Equiped1;
			}
			if (index == 1)
			{
				return this.Equiped2;
			}
			if (index == 2)
			{
				return this.Equiped3;
			}
			return 0;
		}

		// Token: 0x040001AE RID: 430
		[CompilerGenerated]
		private long long_0;

		// Token: 0x040001AF RID: 431
		[CompilerGenerated]
		private long long_1;

		// Token: 0x040001B0 RID: 432
		[CompilerGenerated]
		private int int_0;

		// Token: 0x040001B1 RID: 433
		[CompilerGenerated]
		private int int_1;

		// Token: 0x040001B2 RID: 434
		[CompilerGenerated]
		private int int_2;

		// Token: 0x040001B3 RID: 435
		[CompilerGenerated]
		private int int_3;
	}
}
