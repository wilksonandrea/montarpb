using System;
using System.Runtime.CompilerServices;
using Plugin.Core.Enums;

namespace Plugin.Core.Models
{
	// Token: 0x0200004C RID: 76
	public class BattleRewardModel
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x000038C0 File Offset: 0x00001AC0
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x000038C8 File Offset: 0x00001AC8
		public BattleRewardType Type
		{
			[CompilerGenerated]
			get
			{
				return this.battleRewardType_0;
			}
			[CompilerGenerated]
			set
			{
				this.battleRewardType_0 = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x000038D1 File Offset: 0x00001AD1
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x000038D9 File Offset: 0x00001AD9
		public int Percentage
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

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x000038E2 File Offset: 0x00001AE2
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x000038EA File Offset: 0x00001AEA
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

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060002CA RID: 714 RVA: 0x000038F3 File Offset: 0x00001AF3
		// (set) Token: 0x060002CB RID: 715 RVA: 0x000038FB File Offset: 0x00001AFB
		public int[] Rewards
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

		// Token: 0x060002CC RID: 716 RVA: 0x00002116 File Offset: 0x00000316
		public BattleRewardModel()
		{
		}

		// Token: 0x040000FD RID: 253
		[CompilerGenerated]
		private BattleRewardType battleRewardType_0;

		// Token: 0x040000FE RID: 254
		[CompilerGenerated]
		private int int_0;

		// Token: 0x040000FF RID: 255
		[CompilerGenerated]
		private bool bool_0;

		// Token: 0x04000100 RID: 256
		[CompilerGenerated]
		private int[] int_1;
	}
}
