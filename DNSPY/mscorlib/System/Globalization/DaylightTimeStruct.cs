using System;
using System.Runtime.CompilerServices;

namespace System.Globalization
{
	// Token: 0x020003B5 RID: 949
	internal struct DaylightTimeStruct
	{
		// Token: 0x06002F49 RID: 12105 RVA: 0x000B5761 File Offset: 0x000B3961
		public DaylightTimeStruct(DateTime start, DateTime end, TimeSpan delta)
		{
			this.Start = start;
			this.End = end;
			this.Delta = delta;
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06002F4A RID: 12106 RVA: 0x000B5778 File Offset: 0x000B3978
		public DateTime Start
		{
			[CompilerGenerated]
			get
			{
				return this.<Start>k__BackingField;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06002F4B RID: 12107 RVA: 0x000B5780 File Offset: 0x000B3980
		public DateTime End
		{
			[CompilerGenerated]
			get
			{
				return this.<End>k__BackingField;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06002F4C RID: 12108 RVA: 0x000B5788 File Offset: 0x000B3988
		public TimeSpan Delta
		{
			[CompilerGenerated]
			get
			{
				return this.<Delta>k__BackingField;
			}
		}

		// Token: 0x04001409 RID: 5129
		[CompilerGenerated]
		private readonly DateTime <Start>k__BackingField;

		// Token: 0x0400140A RID: 5130
		[CompilerGenerated]
		private readonly DateTime <End>k__BackingField;

		// Token: 0x0400140B RID: 5131
		[CompilerGenerated]
		private readonly TimeSpan <Delta>k__BackingField;
	}
}
