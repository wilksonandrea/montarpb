using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x0200005D RID: 93
	public class PassBoxModel
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060003BC RID: 956 RVA: 0x00004143 File Offset: 0x00002343
		// (set) Token: 0x060003BD RID: 957 RVA: 0x0000414B File Offset: 0x0000234B
		public PassItemModel Normal
		{
			[CompilerGenerated]
			get
			{
				return this.passItemModel_0;
			}
			[CompilerGenerated]
			set
			{
				this.passItemModel_0 = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060003BE RID: 958 RVA: 0x00004154 File Offset: 0x00002354
		// (set) Token: 0x060003BF RID: 959 RVA: 0x0000415C File Offset: 0x0000235C
		public PassItemModel PremiumA
		{
			[CompilerGenerated]
			get
			{
				return this.passItemModel_1;
			}
			[CompilerGenerated]
			set
			{
				this.passItemModel_1 = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060003C0 RID: 960 RVA: 0x00004165 File Offset: 0x00002365
		// (set) Token: 0x060003C1 RID: 961 RVA: 0x0000416D File Offset: 0x0000236D
		public PassItemModel PremiumB
		{
			[CompilerGenerated]
			get
			{
				return this.passItemModel_2;
			}
			[CompilerGenerated]
			set
			{
				this.passItemModel_2 = value;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060003C2 RID: 962 RVA: 0x00004176 File Offset: 0x00002376
		// (set) Token: 0x060003C3 RID: 963 RVA: 0x0000417E File Offset: 0x0000237E
		public int RequiredPoints
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

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x00004187 File Offset: 0x00002387
		// (set) Token: 0x060003C5 RID: 965 RVA: 0x0000418F File Offset: 0x0000238F
		public int RewardCount
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

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x00004198 File Offset: 0x00002398
		// (set) Token: 0x060003C7 RID: 967 RVA: 0x000041A0 File Offset: 0x000023A0
		public int Card
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

		// Token: 0x060003C8 RID: 968 RVA: 0x000041A9 File Offset: 0x000023A9
		public PassBoxModel()
		{
			this.Normal = new PassItemModel();
			this.PremiumA = new PassItemModel();
			this.PremiumB = new PassItemModel();
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0001AD4C File Offset: 0x00018F4C
		public void SetCount()
		{
			if (this.Normal != null && this.Normal.GoodId > 0)
			{
				this.RewardCount++;
			}
			if (this.PremiumA != null && this.PremiumA.GoodId > 0)
			{
				this.RewardCount++;
			}
			if (this.PremiumB != null && this.PremiumB.GoodId > 0)
			{
				this.RewardCount++;
			}
		}

		// Token: 0x0400016C RID: 364
		[CompilerGenerated]
		private PassItemModel passItemModel_0;

		// Token: 0x0400016D RID: 365
		[CompilerGenerated]
		private PassItemModel passItemModel_1;

		// Token: 0x0400016E RID: 366
		[CompilerGenerated]
		private PassItemModel passItemModel_2;

		// Token: 0x0400016F RID: 367
		[CompilerGenerated]
		private int int_0;

		// Token: 0x04000170 RID: 368
		[CompilerGenerated]
		private int int_1;

		// Token: 0x04000171 RID: 369
		[CompilerGenerated]
		private int int_2;
	}
}
