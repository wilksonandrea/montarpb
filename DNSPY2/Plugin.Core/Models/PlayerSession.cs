using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x02000071 RID: 113
	public class PlayerSession
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060004E0 RID: 1248 RVA: 0x00004B84 File Offset: 0x00002D84
		// (set) Token: 0x060004E1 RID: 1249 RVA: 0x00004B8C File Offset: 0x00002D8C
		public int SessionId
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

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060004E2 RID: 1250 RVA: 0x00004B95 File Offset: 0x00002D95
		// (set) Token: 0x060004E3 RID: 1251 RVA: 0x00004B9D File Offset: 0x00002D9D
		public long PlayerId
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

		// Token: 0x060004E4 RID: 1252 RVA: 0x00002116 File Offset: 0x00000316
		public PlayerSession()
		{
		}

		// Token: 0x040001E8 RID: 488
		[CompilerGenerated]
		private int int_0;

		// Token: 0x040001E9 RID: 489
		[CompilerGenerated]
		private long long_0;
	}
}
