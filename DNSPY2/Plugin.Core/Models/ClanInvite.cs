using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x02000086 RID: 134
	public class ClanInvite
	{
		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000604 RID: 1540 RVA: 0x0000562A File Offset: 0x0000382A
		// (set) Token: 0x06000605 RID: 1541 RVA: 0x00005632 File Offset: 0x00003832
		public int Id
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

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x0000563B File Offset: 0x0000383B
		// (set) Token: 0x06000607 RID: 1543 RVA: 0x00005643 File Offset: 0x00003843
		public uint InviteDate
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

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x0000564C File Offset: 0x0000384C
		// (set) Token: 0x06000609 RID: 1545 RVA: 0x00005654 File Offset: 0x00003854
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

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x0000565D File Offset: 0x0000385D
		// (set) Token: 0x0600060B RID: 1547 RVA: 0x00005665 File Offset: 0x00003865
		public string Text
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

		// Token: 0x0600060C RID: 1548 RVA: 0x00002116 File Offset: 0x00000316
		public ClanInvite()
		{
		}

		// Token: 0x04000280 RID: 640
		[CompilerGenerated]
		private int int_0;

		// Token: 0x04000281 RID: 641
		[CompilerGenerated]
		private uint uint_0;

		// Token: 0x04000282 RID: 642
		[CompilerGenerated]
		private long long_0;

		// Token: 0x04000283 RID: 643
		[CompilerGenerated]
		private string string_0;
	}
}
