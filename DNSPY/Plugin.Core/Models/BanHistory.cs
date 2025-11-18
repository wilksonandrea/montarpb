using System;
using System.Runtime.CompilerServices;
using Plugin.Core.Utility;

namespace Plugin.Core.Models
{
	// Token: 0x02000081 RID: 129
	public class BanHistory
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x000052F3 File Offset: 0x000034F3
		// (set) Token: 0x060005C5 RID: 1477 RVA: 0x000052FB File Offset: 0x000034FB
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

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x00005304 File Offset: 0x00003504
		// (set) Token: 0x060005C7 RID: 1479 RVA: 0x0000530C File Offset: 0x0000350C
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

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060005C8 RID: 1480 RVA: 0x00005315 File Offset: 0x00003515
		// (set) Token: 0x060005C9 RID: 1481 RVA: 0x0000531D File Offset: 0x0000351D
		public string Type
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

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x00005326 File Offset: 0x00003526
		// (set) Token: 0x060005CB RID: 1483 RVA: 0x0000532E File Offset: 0x0000352E
		public string Value
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

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x00005337 File Offset: 0x00003537
		// (set) Token: 0x060005CD RID: 1485 RVA: 0x0000533F File Offset: 0x0000353F
		public string Reason
		{
			[CompilerGenerated]
			get
			{
				return this.string_2;
			}
			[CompilerGenerated]
			set
			{
				this.string_2 = value;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x00005348 File Offset: 0x00003548
		// (set) Token: 0x060005CF RID: 1487 RVA: 0x00005350 File Offset: 0x00003550
		public DateTime StartDate
		{
			[CompilerGenerated]
			get
			{
				return this.dateTime_0;
			}
			[CompilerGenerated]
			set
			{
				this.dateTime_0 = value;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x00005359 File Offset: 0x00003559
		// (set) Token: 0x060005D1 RID: 1489 RVA: 0x00005361 File Offset: 0x00003561
		public DateTime EndDate
		{
			[CompilerGenerated]
			get
			{
				return this.dateTime_1;
			}
			[CompilerGenerated]
			set
			{
				this.dateTime_1 = value;
			}
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0000536A File Offset: 0x0000356A
		public BanHistory()
		{
			this.StartDate = DateTimeUtil.Now();
			this.Type = "";
			this.Value = "";
			this.Reason = "";
		}

		// Token: 0x04000251 RID: 593
		[CompilerGenerated]
		private long long_0;

		// Token: 0x04000252 RID: 594
		[CompilerGenerated]
		private long long_1;

		// Token: 0x04000253 RID: 595
		[CompilerGenerated]
		private string string_0;

		// Token: 0x04000254 RID: 596
		[CompilerGenerated]
		private string string_1;

		// Token: 0x04000255 RID: 597
		[CompilerGenerated]
		private string string_2;

		// Token: 0x04000256 RID: 598
		[CompilerGenerated]
		private DateTime dateTime_0;

		// Token: 0x04000257 RID: 599
		[CompilerGenerated]
		private DateTime dateTime_1;
	}
}
