using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models.Map
{
	// Token: 0x020000A2 RID: 162
	public class MapRule
	{
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x00006575 File Offset: 0x00004775
		// (set) Token: 0x060007BF RID: 1983 RVA: 0x0000657D File Offset: 0x0000477D
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

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x00006586 File Offset: 0x00004786
		// (set) Token: 0x060007C1 RID: 1985 RVA: 0x0000658E File Offset: 0x0000478E
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

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x00006597 File Offset: 0x00004797
		// (set) Token: 0x060007C3 RID: 1987 RVA: 0x0000659F File Offset: 0x0000479F
		public int Rule
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

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x000065A8 File Offset: 0x000047A8
		// (set) Token: 0x060007C5 RID: 1989 RVA: 0x000065B0 File Offset: 0x000047B0
		public int StageOptions
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

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x000065B9 File Offset: 0x000047B9
		// (set) Token: 0x060007C7 RID: 1991 RVA: 0x000065C1 File Offset: 0x000047C1
		public int Conditions
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

		// Token: 0x060007C8 RID: 1992 RVA: 0x000065CA File Offset: 0x000047CA
		public MapRule()
		{
			this.Name = "";
		}

		// Token: 0x04000378 RID: 888
		[CompilerGenerated]
		private string string_0;

		// Token: 0x04000379 RID: 889
		[CompilerGenerated]
		private int int_0;

		// Token: 0x0400037A RID: 890
		[CompilerGenerated]
		private int int_1;

		// Token: 0x0400037B RID: 891
		[CompilerGenerated]
		private int int_2;

		// Token: 0x0400037C RID: 892
		[CompilerGenerated]
		private int int_3;
	}
}
