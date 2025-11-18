using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x02000077 RID: 119
	public class StatisticClan
	{
		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x00004DC9 File Offset: 0x00002FC9
		// (set) Token: 0x06000527 RID: 1319 RVA: 0x00004DD1 File Offset: 0x00002FD1
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

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x00004DDA File Offset: 0x00002FDA
		// (set) Token: 0x06000529 RID: 1321 RVA: 0x00004DE2 File Offset: 0x00002FE2
		public int Matches
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

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x00004DEB File Offset: 0x00002FEB
		// (set) Token: 0x0600052B RID: 1323 RVA: 0x00004DF3 File Offset: 0x00002FF3
		public int MatchWins
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

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x00004DFC File Offset: 0x00002FFC
		// (set) Token: 0x0600052D RID: 1325 RVA: 0x00004E04 File Offset: 0x00003004
		public int MatchLoses
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

		// Token: 0x0600052E RID: 1326 RVA: 0x00002116 File Offset: 0x00000316
		public StatisticClan()
		{
		}

		// Token: 0x04000208 RID: 520
		[CompilerGenerated]
		private long long_0;

		// Token: 0x04000209 RID: 521
		[CompilerGenerated]
		private int int_0;

		// Token: 0x0400020A RID: 522
		[CompilerGenerated]
		private int int_1;

		// Token: 0x0400020B RID: 523
		[CompilerGenerated]
		private int int_2;
	}
}
