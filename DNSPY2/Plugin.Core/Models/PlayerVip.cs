using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x02000069 RID: 105
	public class PlayerVip
	{
		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x00004804 File Offset: 0x00002A04
		// (set) Token: 0x0600046B RID: 1131 RVA: 0x0000480C File Offset: 0x00002A0C
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

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x00004815 File Offset: 0x00002A15
		// (set) Token: 0x0600046D RID: 1133 RVA: 0x0000481D File Offset: 0x00002A1D
		public string Address
		{
			[CompilerGenerated]
			get
			{
				return this.string_0;
			}
			[CompilerGenerated]
			set
			{
				this.string_0 = value;
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x00004826 File Offset: 0x00002A26
		// (set) Token: 0x0600046F RID: 1135 RVA: 0x0000482E File Offset: 0x00002A2E
		public string Benefit
		{
			[CompilerGenerated]
			get
			{
				return this.string_1;
			}
			[CompilerGenerated]
			set
			{
				this.string_1 = value;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x00004837 File Offset: 0x00002A37
		// (set) Token: 0x06000471 RID: 1137 RVA: 0x0000483F File Offset: 0x00002A3F
		public uint Expirate
		{
			[CompilerGenerated]
			get
			{
				return this.uint_0;
			}
			[CompilerGenerated]
			set
			{
				this.uint_0 = value;
			}
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00002116 File Offset: 0x00000316
		public PlayerVip()
		{
		}

		// Token: 0x040001B4 RID: 436
		[CompilerGenerated]
		private long long_0;

		// Token: 0x040001B5 RID: 437
		[CompilerGenerated]
		private string string_0;

		// Token: 0x040001B6 RID: 438
		[CompilerGenerated]
		private string string_1;

		// Token: 0x040001B7 RID: 439
		[CompilerGenerated]
		private uint uint_0;
	}
}
