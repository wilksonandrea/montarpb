using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000429 RID: 1065
	[AttributeUsage(AttributeTargets.Field)]
	internal class EventChannelAttribute : Attribute
	{
		// Token: 0x170007F0 RID: 2032
		// (get) Token: 0x0600354A RID: 13642 RVA: 0x000CE582 File Offset: 0x000CC782
		// (set) Token: 0x0600354B RID: 13643 RVA: 0x000CE58A File Offset: 0x000CC78A
		public bool Enabled
		{
			[CompilerGenerated]
			get
			{
				return this.<Enabled>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Enabled>k__BackingField = value;
			}
		}

		// Token: 0x170007F1 RID: 2033
		// (get) Token: 0x0600354C RID: 13644 RVA: 0x000CE593 File Offset: 0x000CC793
		// (set) Token: 0x0600354D RID: 13645 RVA: 0x000CE59B File Offset: 0x000CC79B
		public EventChannelType EventChannelType
		{
			[CompilerGenerated]
			get
			{
				return this.<EventChannelType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<EventChannelType>k__BackingField = value;
			}
		}

		// Token: 0x0600354E RID: 13646 RVA: 0x000CE5A4 File Offset: 0x000CC7A4
		public EventChannelAttribute()
		{
		}

		// Token: 0x040017A5 RID: 6053
		[CompilerGenerated]
		private bool <Enabled>k__BackingField;

		// Token: 0x040017A6 RID: 6054
		[CompilerGenerated]
		private EventChannelType <EventChannelType>k__BackingField;
	}
}
