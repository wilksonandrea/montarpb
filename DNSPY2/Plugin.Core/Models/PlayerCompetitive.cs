using System;
using System.Runtime.CompilerServices;
using Plugin.Core.XML;

namespace Plugin.Core.Models
{
	// Token: 0x02000063 RID: 99
	public class PlayerCompetitive
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00004315 File Offset: 0x00002515
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x0000431D File Offset: 0x0000251D
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

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00004326 File Offset: 0x00002526
		// (set) Token: 0x060003F7 RID: 1015 RVA: 0x0000432E File Offset: 0x0000252E
		public int Level
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

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060003F8 RID: 1016 RVA: 0x00004337 File Offset: 0x00002537
		// (set) Token: 0x060003F9 RID: 1017 RVA: 0x0000433F File Offset: 0x0000253F
		public int Points
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

		// Token: 0x060003FA RID: 1018 RVA: 0x00004348 File Offset: 0x00002548
		public CompetitiveRank Rank()
		{
			return CompetitiveXML.GetRank(this.Level) ?? new CompetitiveRank();
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00002116 File Offset: 0x00000316
		public PlayerCompetitive()
		{
		}

		// Token: 0x04000182 RID: 386
		[CompilerGenerated]
		private long long_0;

		// Token: 0x04000183 RID: 387
		[CompilerGenerated]
		private int int_0;

		// Token: 0x04000184 RID: 388
		[CompilerGenerated]
		private int int_1;
	}
}
