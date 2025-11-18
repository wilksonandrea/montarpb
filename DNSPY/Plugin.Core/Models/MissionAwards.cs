using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x02000057 RID: 87
	public class MissionAwards
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000375 RID: 885 RVA: 0x00003EA1 File Offset: 0x000020A1
		// (set) Token: 0x06000376 RID: 886 RVA: 0x00003EA9 File Offset: 0x000020A9
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

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000377 RID: 887 RVA: 0x00003EB2 File Offset: 0x000020B2
		// (set) Token: 0x06000378 RID: 888 RVA: 0x00003EBA File Offset: 0x000020BA
		public int MasterMedal
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

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000379 RID: 889 RVA: 0x00003EC3 File Offset: 0x000020C3
		// (set) Token: 0x0600037A RID: 890 RVA: 0x00003ECB File Offset: 0x000020CB
		public int Exp
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

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600037B RID: 891 RVA: 0x00003ED4 File Offset: 0x000020D4
		// (set) Token: 0x0600037C RID: 892 RVA: 0x00003EDC File Offset: 0x000020DC
		public int Gold
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

		// Token: 0x0600037D RID: 893 RVA: 0x00003EE5 File Offset: 0x000020E5
		public MissionAwards(int int_4, int int_5, int int_6, int int_7)
		{
			this.Id = int_4;
			this.MasterMedal = int_5;
			this.Exp = int_6;
			this.Gold = int_7;
		}

		// Token: 0x0400014C RID: 332
		[CompilerGenerated]
		private int int_0;

		// Token: 0x0400014D RID: 333
		[CompilerGenerated]
		private int int_1;

		// Token: 0x0400014E RID: 334
		[CompilerGenerated]
		private int int_2;

		// Token: 0x0400014F RID: 335
		[CompilerGenerated]
		private int int_3;
	}
}
