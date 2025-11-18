using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000441 RID: 1089
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
	[__DynamicallyInvokable]
	public class EventDataAttribute : Attribute
	{
		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x060035FE RID: 13822 RVA: 0x000D2187 File Offset: 0x000D0387
		// (set) Token: 0x060035FF RID: 13823 RVA: 0x000D218F File Offset: 0x000D038F
		[__DynamicallyInvokable]
		public string Name
		{
			[CompilerGenerated]
			[__DynamicallyInvokable]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			[__DynamicallyInvokable]
			set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06003600 RID: 13824 RVA: 0x000D2198 File Offset: 0x000D0398
		// (set) Token: 0x06003601 RID: 13825 RVA: 0x000D21A0 File Offset: 0x000D03A0
		internal EventLevel Level
		{
			get
			{
				return this.level;
			}
			set
			{
				this.level = value;
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x06003602 RID: 13826 RVA: 0x000D21A9 File Offset: 0x000D03A9
		// (set) Token: 0x06003603 RID: 13827 RVA: 0x000D21B1 File Offset: 0x000D03B1
		internal EventOpcode Opcode
		{
			get
			{
				return this.opcode;
			}
			set
			{
				this.opcode = value;
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x06003604 RID: 13828 RVA: 0x000D21BA File Offset: 0x000D03BA
		// (set) Token: 0x06003605 RID: 13829 RVA: 0x000D21C2 File Offset: 0x000D03C2
		internal EventKeywords Keywords
		{
			[CompilerGenerated]
			get
			{
				return this.<Keywords>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Keywords>k__BackingField = value;
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x06003606 RID: 13830 RVA: 0x000D21CB File Offset: 0x000D03CB
		// (set) Token: 0x06003607 RID: 13831 RVA: 0x000D21D3 File Offset: 0x000D03D3
		internal EventTags Tags
		{
			[CompilerGenerated]
			get
			{
				return this.<Tags>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Tags>k__BackingField = value;
			}
		}

		// Token: 0x06003608 RID: 13832 RVA: 0x000D21DC File Offset: 0x000D03DC
		[__DynamicallyInvokable]
		public EventDataAttribute()
		{
		}

		// Token: 0x0400181E RID: 6174
		private EventLevel level = (EventLevel)(-1);

		// Token: 0x0400181F RID: 6175
		private EventOpcode opcode = (EventOpcode)(-1);

		// Token: 0x04001820 RID: 6176
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x04001821 RID: 6177
		[CompilerGenerated]
		private EventKeywords <Keywords>k__BackingField;

		// Token: 0x04001822 RID: 6178
		[CompilerGenerated]
		private EventTags <Tags>k__BackingField;
	}
}
