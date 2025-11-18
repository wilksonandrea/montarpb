using System;
using System.Runtime.CompilerServices;
using Plugin.Core.Enums;

namespace Plugin.Core.Models
{
	// Token: 0x02000074 RID: 116
	public class SlotMatch
	{
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00004C11 File Offset: 0x00002E11
		// (set) Token: 0x060004F2 RID: 1266 RVA: 0x00004C19 File Offset: 0x00002E19
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

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x00004C22 File Offset: 0x00002E22
		// (set) Token: 0x060004F4 RID: 1268 RVA: 0x00004C2A File Offset: 0x00002E2A
		public long PlayerId
		{
			[CompilerGenerated]
			get
			{
				return this.long_0;
			}
			[CompilerGenerated]
			set
			{
				this.long_0 = value;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00004C33 File Offset: 0x00002E33
		// (set) Token: 0x060004F6 RID: 1270 RVA: 0x00004C3B File Offset: 0x00002E3B
		public SlotMatchState State
		{
			[CompilerGenerated]
			get
			{
				return this.slotMatchState_0;
			}
			[CompilerGenerated]
			set
			{
				this.slotMatchState_0 = value;
			}
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x00004C44 File Offset: 0x00002E44
		public SlotMatch(int int_1)
		{
			this.Id = int_1;
		}

		// Token: 0x040001EF RID: 495
		[CompilerGenerated]
		private int int_0;

		// Token: 0x040001F0 RID: 496
		[CompilerGenerated]
		private long long_0;

		// Token: 0x040001F1 RID: 497
		[CompilerGenerated]
		private SlotMatchState slotMatchState_0;
	}
}
