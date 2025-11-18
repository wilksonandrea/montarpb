using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000426 RID: 1062
	[AttributeUsage(AttributeTargets.Class)]
	[__DynamicallyInvokable]
	public sealed class EventSourceAttribute : Attribute
	{
		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x0600352C RID: 13612 RVA: 0x000CE469 File Offset: 0x000CC669
		// (set) Token: 0x0600352D RID: 13613 RVA: 0x000CE471 File Offset: 0x000CC671
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

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x0600352E RID: 13614 RVA: 0x000CE47A File Offset: 0x000CC67A
		// (set) Token: 0x0600352F RID: 13615 RVA: 0x000CE482 File Offset: 0x000CC682
		[__DynamicallyInvokable]
		public string Guid
		{
			[CompilerGenerated]
			[__DynamicallyInvokable]
			get
			{
				return this.<Guid>k__BackingField;
			}
			[CompilerGenerated]
			[__DynamicallyInvokable]
			set
			{
				this.<Guid>k__BackingField = value;
			}
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x06003530 RID: 13616 RVA: 0x000CE48B File Offset: 0x000CC68B
		// (set) Token: 0x06003531 RID: 13617 RVA: 0x000CE493 File Offset: 0x000CC693
		[__DynamicallyInvokable]
		public string LocalizationResources
		{
			[CompilerGenerated]
			[__DynamicallyInvokable]
			get
			{
				return this.<LocalizationResources>k__BackingField;
			}
			[CompilerGenerated]
			[__DynamicallyInvokable]
			set
			{
				this.<LocalizationResources>k__BackingField = value;
			}
		}

		// Token: 0x06003532 RID: 13618 RVA: 0x000CE49C File Offset: 0x000CC69C
		[__DynamicallyInvokable]
		public EventSourceAttribute()
		{
		}

		// Token: 0x04001797 RID: 6039
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x04001798 RID: 6040
		[CompilerGenerated]
		private string <Guid>k__BackingField;

		// Token: 0x04001799 RID: 6041
		[CompilerGenerated]
		private string <LocalizationResources>k__BackingField;
	}
}
