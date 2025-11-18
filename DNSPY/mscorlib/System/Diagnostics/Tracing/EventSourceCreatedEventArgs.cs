using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000424 RID: 1060
	public class EventSourceCreatedEventArgs : EventArgs
	{
		// Token: 0x170007D2 RID: 2002
		// (get) Token: 0x06003512 RID: 13586 RVA: 0x000CE190 File Offset: 0x000CC390
		// (set) Token: 0x06003513 RID: 13587 RVA: 0x000CE198 File Offset: 0x000CC398
		public EventSource EventSource
		{
			[CompilerGenerated]
			get
			{
				return this.<EventSource>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<EventSource>k__BackingField = value;
			}
		}

		// Token: 0x06003514 RID: 13588 RVA: 0x000CE1A1 File Offset: 0x000CC3A1
		public EventSourceCreatedEventArgs()
		{
		}

		// Token: 0x0400178A RID: 6026
		[CompilerGenerated]
		private EventSource <EventSource>k__BackingField;
	}
}
