using System;
using System.Runtime.CompilerServices;

namespace Plugin.Core.Models
{
	// Token: 0x02000073 RID: 115
	public class SlotChange
	{
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x00004BD9 File Offset: 0x00002DD9
		// (set) Token: 0x060004ED RID: 1261 RVA: 0x00004BE1 File Offset: 0x00002DE1
		public SlotModel OldSlot
		{
			[CompilerGenerated]
			get
			{
				return this.slotModel_0;
			}
			[CompilerGenerated]
			set
			{
				this.slotModel_0 = value;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060004EE RID: 1262 RVA: 0x00004BEA File Offset: 0x00002DEA
		// (set) Token: 0x060004EF RID: 1263 RVA: 0x00004BF2 File Offset: 0x00002DF2
		public SlotModel NewSlot
		{
			[CompilerGenerated]
			get
			{
				return this.slotModel_1;
			}
			[CompilerGenerated]
			set
			{
				this.slotModel_1 = value;
			}
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x00004BFB File Offset: 0x00002DFB
		public SlotChange(SlotModel slotModel_2, SlotModel slotModel_3)
		{
			this.OldSlot = slotModel_2;
			this.NewSlot = slotModel_3;
		}

		// Token: 0x040001ED RID: 493
		[CompilerGenerated]
		private SlotModel slotModel_0;

		// Token: 0x040001EE RID: 494
		[CompilerGenerated]
		private SlotModel slotModel_1;
	}
}
