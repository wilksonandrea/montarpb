using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x02000094 RID: 148
	public class VisitBoxModel
	{
		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060006F1 RID: 1777 RVA: 0x00005EC7 File Offset: 0x000040C7
		// (set) Token: 0x060006F2 RID: 1778 RVA: 0x00005ECF File Offset: 0x000040CF
		public VisitItemModel Reward1
		{
			[CompilerGenerated]
			get
			{
				return this.visitItemModel_0;
			}
			[CompilerGenerated]
			set
			{
				this.visitItemModel_0 = value;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x00005ED8 File Offset: 0x000040D8
		// (set) Token: 0x060006F4 RID: 1780 RVA: 0x00005EE0 File Offset: 0x000040E0
		public VisitItemModel Reward2
		{
			[CompilerGenerated]
			get
			{
				return this.visitItemModel_1;
			}
			[CompilerGenerated]
			set
			{
				this.visitItemModel_1 = value;
			}
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x00005EE9 File Offset: 0x000040E9
		// (set) Token: 0x060006F6 RID: 1782 RVA: 0x00005EF1 File Offset: 0x000040F1
		public int RewardCount
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

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060006F7 RID: 1783 RVA: 0x00005EFA File Offset: 0x000040FA
		// (set) Token: 0x060006F8 RID: 1784 RVA: 0x00005F02 File Offset: 0x00004102
		public bool IsBothReward
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

		// Token: 0x060006F9 RID: 1785 RVA: 0x00005F0B File Offset: 0x0000410B
		public VisitBoxModel()
		{
			this.Reward1 = new VisitItemModel();
			this.Reward2 = new VisitItemModel();
		}

		// Token: 0x060006FA RID: 1786 RVA: 0x0001C844 File Offset: 0x0001AA44
		public void SetCount()
		{
			if (this.Reward1 != null && this.Reward1.GoodId > 0)
			{
				int num = this.RewardCount;
				this.RewardCount = num + 1;
			}
			if (this.Reward2 != null && this.Reward2.GoodId > 0)
			{
				int num = this.RewardCount;
				this.RewardCount = num + 1;
			}
		}

		// Token: 0x040002E1 RID: 737
		[CompilerGenerated]
		private VisitItemModel visitItemModel_0;

		// Token: 0x040002E2 RID: 738
		[CompilerGenerated]
		private VisitItemModel visitItemModel_1;

		// Token: 0x040002E3 RID: 739
		[CompilerGenerated]
		private int int_0;

		// Token: 0x040002E4 RID: 740
		[CompilerGenerated]
		private bool bool_0;
	}
}
