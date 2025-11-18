using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x02000070 RID: 112
	public class ClanTeam
	{
		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060004D9 RID: 1241 RVA: 0x00004B51 File Offset: 0x00002D51
		// (set) Token: 0x060004DA RID: 1242 RVA: 0x00004B59 File Offset: 0x00002D59
		public int ClanId
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

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x00004B62 File Offset: 0x00002D62
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x00004B6A File Offset: 0x00002D6A
		public int PlayersFR
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

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x00004B73 File Offset: 0x00002D73
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x00004B7B File Offset: 0x00002D7B
		public int PlayersCT
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

		// Token: 0x060004DF RID: 1247 RVA: 0x00002116 File Offset: 0x00000316
		public ClanTeam()
		{
		}

		// Token: 0x040001E5 RID: 485
		[CompilerGenerated]
		private int int_0;

		// Token: 0x040001E6 RID: 486
		[CompilerGenerated]
		private int int_1;

		// Token: 0x040001E7 RID: 487
		[CompilerGenerated]
		private int int_2;
	}
}
