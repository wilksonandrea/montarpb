using System;
using System.Runtime.CompilerServices;
using Plugin.Core.Enums;

namespace Plugin.Core.Models
{
	// Token: 0x02000082 RID: 130
	public class CouponFlag
	{
		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x0000539E File Offset: 0x0000359E
		// (set) Token: 0x060005D4 RID: 1492 RVA: 0x000053A6 File Offset: 0x000035A6
		public int ItemId
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

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x000053AF File Offset: 0x000035AF
		// (set) Token: 0x060005D6 RID: 1494 RVA: 0x000053B7 File Offset: 0x000035B7
		public CouponEffects EffectFlag
		{
			[CompilerGenerated]
			get
			{
				return this.couponEffects_0;
			}
			[CompilerGenerated]
			set
			{
				this.couponEffects_0 = value;
			}
		}

		// Token: 0x060005D7 RID: 1495 RVA: 0x00002116 File Offset: 0x00000316
		public CouponFlag()
		{
		}

		// Token: 0x04000258 RID: 600
		[CompilerGenerated]
		private int int_0;

		// Token: 0x04000259 RID: 601
		[CompilerGenerated]
		private CouponEffects couponEffects_0;
	}
}
