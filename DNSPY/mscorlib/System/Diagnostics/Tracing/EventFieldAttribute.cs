using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000443 RID: 1091
	[AttributeUsage(AttributeTargets.Property)]
	[__DynamicallyInvokable]
	public class EventFieldAttribute : Attribute
	{
		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x06003609 RID: 13833 RVA: 0x000D21F2 File Offset: 0x000D03F2
		// (set) Token: 0x0600360A RID: 13834 RVA: 0x000D21FA File Offset: 0x000D03FA
		[__DynamicallyInvokable]
		public EventFieldTags Tags
		{
			[CompilerGenerated]
			[__DynamicallyInvokable]
			get
			{
				return this.<Tags>k__BackingField;
			}
			[CompilerGenerated]
			[__DynamicallyInvokable]
			set
			{
				this.<Tags>k__BackingField = value;
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x0600360B RID: 13835 RVA: 0x000D2203 File Offset: 0x000D0403
		// (set) Token: 0x0600360C RID: 13836 RVA: 0x000D220B File Offset: 0x000D040B
		internal string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x0600360D RID: 13837 RVA: 0x000D2214 File Offset: 0x000D0414
		// (set) Token: 0x0600360E RID: 13838 RVA: 0x000D221C File Offset: 0x000D041C
		[__DynamicallyInvokable]
		public EventFieldFormat Format
		{
			[CompilerGenerated]
			[__DynamicallyInvokable]
			get
			{
				return this.<Format>k__BackingField;
			}
			[CompilerGenerated]
			[__DynamicallyInvokable]
			set
			{
				this.<Format>k__BackingField = value;
			}
		}

		// Token: 0x0600360F RID: 13839 RVA: 0x000D2225 File Offset: 0x000D0425
		[__DynamicallyInvokable]
		public EventFieldAttribute()
		{
		}

		// Token: 0x04001825 RID: 6181
		[CompilerGenerated]
		private EventFieldTags <Tags>k__BackingField;

		// Token: 0x04001826 RID: 6182
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x04001827 RID: 6183
		[CompilerGenerated]
		private EventFieldFormat <Format>k__BackingField;
	}
}
