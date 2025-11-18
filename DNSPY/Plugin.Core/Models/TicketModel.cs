using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Plugin.Core.Enums;

namespace Plugin.Core.Models
{
	// Token: 0x02000095 RID: 149
	public class TicketModel
	{
		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060006FB RID: 1787 RVA: 0x00005F29 File Offset: 0x00004129
		// (set) Token: 0x060006FC RID: 1788 RVA: 0x00005F31 File Offset: 0x00004131
		public string Token
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

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060006FD RID: 1789 RVA: 0x00005F3A File Offset: 0x0000413A
		// (set) Token: 0x060006FE RID: 1790 RVA: 0x00005F42 File Offset: 0x00004142
		public TicketType Type
		{
			[CompilerGenerated]
			get
			{
				return this.ticketType_0;
			}
			[CompilerGenerated]
			set
			{
				this.ticketType_0 = value;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x060006FF RID: 1791 RVA: 0x00005F4B File Offset: 0x0000414B
		// (set) Token: 0x06000700 RID: 1792 RVA: 0x00005F53 File Offset: 0x00004153
		public int GoldReward
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

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000701 RID: 1793 RVA: 0x00005F5C File Offset: 0x0000415C
		// (set) Token: 0x06000702 RID: 1794 RVA: 0x00005F64 File Offset: 0x00004164
		public int CashReward
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

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x00005F6D File Offset: 0x0000416D
		// (set) Token: 0x06000704 RID: 1796 RVA: 0x00005F75 File Offset: 0x00004175
		public int TagsReward
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

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x00005F7E File Offset: 0x0000417E
		// (set) Token: 0x06000706 RID: 1798 RVA: 0x00005F86 File Offset: 0x00004186
		public uint TicketCount
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

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x00005F8F File Offset: 0x0000418F
		// (set) Token: 0x06000708 RID: 1800 RVA: 0x00005F97 File Offset: 0x00004197
		public uint PlayerRation
		{
			[CompilerGenerated]
			get
			{
				return this.uint_1;
			}
			[CompilerGenerated]
			set
			{
				this.uint_1 = value;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x00005FA0 File Offset: 0x000041A0
		// (set) Token: 0x0600070A RID: 1802 RVA: 0x00005FA8 File Offset: 0x000041A8
		public List<int> Rewards
		{
			[CompilerGenerated]
			get
			{
				return this.list_0;
			}
			[CompilerGenerated]
			set
			{
				this.list_0 = value;
			}
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00002116 File Offset: 0x00000316
		public TicketModel()
		{
		}

		// Token: 0x040002E5 RID: 741
		[CompilerGenerated]
		private string string_0;

		// Token: 0x040002E6 RID: 742
		[CompilerGenerated]
		private TicketType ticketType_0;

		// Token: 0x040002E7 RID: 743
		[CompilerGenerated]
		private int int_0;

		// Token: 0x040002E8 RID: 744
		[CompilerGenerated]
		private int int_1;

		// Token: 0x040002E9 RID: 745
		[CompilerGenerated]
		private int int_2;

		// Token: 0x040002EA RID: 746
		[CompilerGenerated]
		private uint uint_0;

		// Token: 0x040002EB RID: 747
		[CompilerGenerated]
		private uint uint_1;

		// Token: 0x040002EC RID: 748
		[CompilerGenerated]
		private List<int> list_0;
	}
}
