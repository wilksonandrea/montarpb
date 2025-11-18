using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x0200005B RID: 91
	public class MissionStore
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x000040AA File Offset: 0x000022AA
		// (set) Token: 0x060003A9 RID: 937 RVA: 0x000040B2 File Offset: 0x000022B2
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

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060003AA RID: 938 RVA: 0x000040BB File Offset: 0x000022BB
		// (set) Token: 0x060003AB RID: 939 RVA: 0x000040C3 File Offset: 0x000022C3
		public int ItemId
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

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060003AC RID: 940 RVA: 0x000040CC File Offset: 0x000022CC
		// (set) Token: 0x060003AD RID: 941 RVA: 0x000040D4 File Offset: 0x000022D4
		public bool Enable
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

		// Token: 0x060003AE RID: 942 RVA: 0x00002116 File Offset: 0x00000316
		public MissionStore()
		{
		}

		// Token: 0x04000163 RID: 355
		[CompilerGenerated]
		private int int_0;

		// Token: 0x04000164 RID: 356
		[CompilerGenerated]
		private int int_1;

		// Token: 0x04000165 RID: 357
		[CompilerGenerated]
		private bool bool_0;
	}
}
