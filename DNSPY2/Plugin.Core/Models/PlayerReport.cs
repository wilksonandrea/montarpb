using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x02000067 RID: 103
	public class PlayerReport
	{
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x000046E4 File Offset: 0x000028E4
		// (set) Token: 0x06000453 RID: 1107 RVA: 0x000046EC File Offset: 0x000028EC
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

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x000046F5 File Offset: 0x000028F5
		// (set) Token: 0x06000455 RID: 1109 RVA: 0x000046FD File Offset: 0x000028FD
		public int TicketCount
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

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x00004706 File Offset: 0x00002906
		// (set) Token: 0x06000457 RID: 1111 RVA: 0x0000470E File Offset: 0x0000290E
		public int ReportedCount
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

		// Token: 0x06000458 RID: 1112 RVA: 0x00002116 File Offset: 0x00000316
		public PlayerReport()
		{
		}

		// Token: 0x040001AB RID: 427
		[CompilerGenerated]
		private long long_0;

		// Token: 0x040001AC RID: 428
		[CompilerGenerated]
		private int int_0;

		// Token: 0x040001AD RID: 429
		[CompilerGenerated]
		private int int_1;
	}
}
