using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000427 RID: 1063
	[AttributeUsage(AttributeTargets.Method)]
	[__DynamicallyInvokable]
	public sealed class EventAttribute : Attribute
	{
		// Token: 0x06003533 RID: 13619 RVA: 0x000CE4A4 File Offset: 0x000CC6A4
		[__DynamicallyInvokable]
		public EventAttribute(int eventId)
		{
			this.EventId = eventId;
			this.Level = EventLevel.Informational;
			this.m_opcodeSet = false;
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06003534 RID: 13620 RVA: 0x000CE4C1 File Offset: 0x000CC6C1
		// (set) Token: 0x06003535 RID: 13621 RVA: 0x000CE4C9 File Offset: 0x000CC6C9
		[__DynamicallyInvokable]
		public int EventId
		{
			[CompilerGenerated]
			[__DynamicallyInvokable]
			get
			{
				return this.<EventId>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<EventId>k__BackingField = value;
			}
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06003536 RID: 13622 RVA: 0x000CE4D2 File Offset: 0x000CC6D2
		// (set) Token: 0x06003537 RID: 13623 RVA: 0x000CE4DA File Offset: 0x000CC6DA
		[__DynamicallyInvokable]
		public EventLevel Level
		{
			[CompilerGenerated]
			[__DynamicallyInvokable]
			get
			{
				return this.<Level>k__BackingField;
			}
			[CompilerGenerated]
			[__DynamicallyInvokable]
			set
			{
				this.<Level>k__BackingField = value;
			}
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x06003538 RID: 13624 RVA: 0x000CE4E3 File Offset: 0x000CC6E3
		// (set) Token: 0x06003539 RID: 13625 RVA: 0x000CE4EB File Offset: 0x000CC6EB
		[__DynamicallyInvokable]
		public EventKeywords Keywords
		{
			[CompilerGenerated]
			[__DynamicallyInvokable]
			get
			{
				return this.<Keywords>k__BackingField;
			}
			[CompilerGenerated]
			[__DynamicallyInvokable]
			set
			{
				this.<Keywords>k__BackingField = value;
			}
		}

		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x0600353A RID: 13626 RVA: 0x000CE4F4 File Offset: 0x000CC6F4
		// (set) Token: 0x0600353B RID: 13627 RVA: 0x000CE4FC File Offset: 0x000CC6FC
		[__DynamicallyInvokable]
		public EventOpcode Opcode
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_opcode;
			}
			[__DynamicallyInvokable]
			set
			{
				this.m_opcode = value;
				this.m_opcodeSet = true;
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x0600353C RID: 13628 RVA: 0x000CE50C File Offset: 0x000CC70C
		internal bool IsOpcodeSet
		{
			get
			{
				return this.m_opcodeSet;
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x0600353D RID: 13629 RVA: 0x000CE514 File Offset: 0x000CC714
		// (set) Token: 0x0600353E RID: 13630 RVA: 0x000CE51C File Offset: 0x000CC71C
		[__DynamicallyInvokable]
		public EventTask Task
		{
			[CompilerGenerated]
			[__DynamicallyInvokable]
			get
			{
				return this.<Task>k__BackingField;
			}
			[CompilerGenerated]
			[__DynamicallyInvokable]
			set
			{
				this.<Task>k__BackingField = value;
			}
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x0600353F RID: 13631 RVA: 0x000CE525 File Offset: 0x000CC725
		// (set) Token: 0x06003540 RID: 13632 RVA: 0x000CE52D File Offset: 0x000CC72D
		[__DynamicallyInvokable]
		public EventChannel Channel
		{
			[CompilerGenerated]
			[__DynamicallyInvokable]
			get
			{
				return this.<Channel>k__BackingField;
			}
			[CompilerGenerated]
			[__DynamicallyInvokable]
			set
			{
				this.<Channel>k__BackingField = value;
			}
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06003541 RID: 13633 RVA: 0x000CE536 File Offset: 0x000CC736
		// (set) Token: 0x06003542 RID: 13634 RVA: 0x000CE53E File Offset: 0x000CC73E
		[__DynamicallyInvokable]
		public byte Version
		{
			[CompilerGenerated]
			[__DynamicallyInvokable]
			get
			{
				return this.<Version>k__BackingField;
			}
			[CompilerGenerated]
			[__DynamicallyInvokable]
			set
			{
				this.<Version>k__BackingField = value;
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x06003543 RID: 13635 RVA: 0x000CE547 File Offset: 0x000CC747
		// (set) Token: 0x06003544 RID: 13636 RVA: 0x000CE54F File Offset: 0x000CC74F
		[__DynamicallyInvokable]
		public string Message
		{
			[CompilerGenerated]
			[__DynamicallyInvokable]
			get
			{
				return this.<Message>k__BackingField;
			}
			[CompilerGenerated]
			[__DynamicallyInvokable]
			set
			{
				this.<Message>k__BackingField = value;
			}
		}

		// Token: 0x170007EE RID: 2030
		// (get) Token: 0x06003545 RID: 13637 RVA: 0x000CE558 File Offset: 0x000CC758
		// (set) Token: 0x06003546 RID: 13638 RVA: 0x000CE560 File Offset: 0x000CC760
		[__DynamicallyInvokable]
		public EventTags Tags
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

		// Token: 0x170007EF RID: 2031
		// (get) Token: 0x06003547 RID: 13639 RVA: 0x000CE569 File Offset: 0x000CC769
		// (set) Token: 0x06003548 RID: 13640 RVA: 0x000CE571 File Offset: 0x000CC771
		[__DynamicallyInvokable]
		public EventActivityOptions ActivityOptions
		{
			[CompilerGenerated]
			[__DynamicallyInvokable]
			get
			{
				return this.<ActivityOptions>k__BackingField;
			}
			[CompilerGenerated]
			[__DynamicallyInvokable]
			set
			{
				this.<ActivityOptions>k__BackingField = value;
			}
		}

		// Token: 0x0400179A RID: 6042
		[CompilerGenerated]
		private int <EventId>k__BackingField;

		// Token: 0x0400179B RID: 6043
		[CompilerGenerated]
		private EventLevel <Level>k__BackingField;

		// Token: 0x0400179C RID: 6044
		[CompilerGenerated]
		private EventKeywords <Keywords>k__BackingField;

		// Token: 0x0400179D RID: 6045
		[CompilerGenerated]
		private EventTask <Task>k__BackingField;

		// Token: 0x0400179E RID: 6046
		[CompilerGenerated]
		private EventChannel <Channel>k__BackingField;

		// Token: 0x0400179F RID: 6047
		[CompilerGenerated]
		private byte <Version>k__BackingField;

		// Token: 0x040017A0 RID: 6048
		[CompilerGenerated]
		private string <Message>k__BackingField;

		// Token: 0x040017A1 RID: 6049
		[CompilerGenerated]
		private EventTags <Tags>k__BackingField;

		// Token: 0x040017A2 RID: 6050
		[CompilerGenerated]
		private EventActivityOptions <ActivityOptions>k__BackingField;

		// Token: 0x040017A3 RID: 6051
		private EventOpcode m_opcode;

		// Token: 0x040017A4 RID: 6052
		private bool m_opcodeSet;
	}
}
