using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Plugin.Core.Utility;

namespace Plugin.Core.Models
{
	// Token: 0x02000054 RID: 84
	public class EventRankUpModel
	{
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600034B RID: 843 RVA: 0x00003D47 File Offset: 0x00001F47
		// (set) Token: 0x0600034C RID: 844 RVA: 0x00003D4F File Offset: 0x00001F4F
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

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00003D58 File Offset: 0x00001F58
		// (set) Token: 0x0600034E RID: 846 RVA: 0x00003D60 File Offset: 0x00001F60
		public uint BeginDate
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

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00003D69 File Offset: 0x00001F69
		// (set) Token: 0x06000350 RID: 848 RVA: 0x00003D71 File Offset: 0x00001F71
		public uint EndedDate
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

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00003D7A File Offset: 0x00001F7A
		// (set) Token: 0x06000352 RID: 850 RVA: 0x00003D82 File Offset: 0x00001F82
		public string Name
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

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00003D8B File Offset: 0x00001F8B
		// (set) Token: 0x06000354 RID: 852 RVA: 0x00003D93 File Offset: 0x00001F93
		public string Description
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

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00003D9C File Offset: 0x00001F9C
		// (set) Token: 0x06000356 RID: 854 RVA: 0x00003DA4 File Offset: 0x00001FA4
		public bool Period
		{
			[CompilerGenerated]
			get
			{
				return this.bool_0;
			}
			[CompilerGenerated]
			set
			{
				this.bool_0 = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00003DAD File Offset: 0x00001FAD
		// (set) Token: 0x06000358 RID: 856 RVA: 0x00003DB5 File Offset: 0x00001FB5
		public bool Priority
		{
			[CompilerGenerated]
			get
			{
				return this.bool_1;
			}
			[CompilerGenerated]
			set
			{
				this.bool_1 = value;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00003DBE File Offset: 0x00001FBE
		// (set) Token: 0x0600035A RID: 858 RVA: 0x00003DC6 File Offset: 0x00001FC6
		public List<int[]> Ranks
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

		// Token: 0x0600035B RID: 859 RVA: 0x00003DCF File Offset: 0x00001FCF
		public EventRankUpModel()
		{
			this.Name = "";
			this.Description = "";
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0001AB98 File Offset: 0x00018D98
		public bool EventIsEnabled()
		{
			uint num = uint.Parse(DateTimeUtil.Now("yyMMddHHmm"));
			return this.BeginDate <= num && num < this.EndedDate;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0001ABCC File Offset: 0x00018DCC
		public int[] GetBonuses(int RankId)
		{
			List<int[]> ranks = this.Ranks;
			int[] array2;
			lock (ranks)
			{
				foreach (int[] array in this.Ranks)
				{
					if (array[0] == RankId)
					{
						return new int[]
						{
							array[1],
							array[2],
							array[3]
						};
					}
				}
				array2 = new int[3];
			}
			return array2;
		}

		// Token: 0x0400013B RID: 315
		[CompilerGenerated]
		private int int_0;

		// Token: 0x0400013C RID: 316
		[CompilerGenerated]
		private uint uint_0;

		// Token: 0x0400013D RID: 317
		[CompilerGenerated]
		private uint uint_1;

		// Token: 0x0400013E RID: 318
		[CompilerGenerated]
		private string string_0;

		// Token: 0x0400013F RID: 319
		[CompilerGenerated]
		private string string_1;

		// Token: 0x04000140 RID: 320
		[CompilerGenerated]
		private bool bool_0;

		// Token: 0x04000141 RID: 321
		[CompilerGenerated]
		private bool bool_1;

		// Token: 0x04000142 RID: 322
		[CompilerGenerated]
		private List<int[]> list_0;
	}
}
