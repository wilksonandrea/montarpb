using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000425 RID: 1061
	[__DynamicallyInvokable]
	public class EventWrittenEventArgs : EventArgs
	{
		// Token: 0x170007D3 RID: 2003
		// (get) Token: 0x06003515 RID: 13589 RVA: 0x000CE1A9 File Offset: 0x000CC3A9
		// (set) Token: 0x06003516 RID: 13590 RVA: 0x000CE1E0 File Offset: 0x000CC3E0
		[__DynamicallyInvokable]
		public string EventName
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_eventName != null || this.EventId < 0)
				{
					return this.m_eventName;
				}
				return this.m_eventSource.m_eventData[this.EventId].Name;
			}
			internal set
			{
				this.m_eventName = value;
			}
		}

		// Token: 0x170007D4 RID: 2004
		// (get) Token: 0x06003517 RID: 13591 RVA: 0x000CE1E9 File Offset: 0x000CC3E9
		// (set) Token: 0x06003518 RID: 13592 RVA: 0x000CE1F1 File Offset: 0x000CC3F1
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
			internal set
			{
				this.<EventId>k__BackingField = value;
			}
		}

		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x06003519 RID: 13593 RVA: 0x000CE1FC File Offset: 0x000CC3FC
		// (set) Token: 0x0600351A RID: 13594 RVA: 0x000CE224 File Offset: 0x000CC424
		[__DynamicallyInvokable]
		public Guid ActivityId
		{
			[SecurityCritical]
			[__DynamicallyInvokable]
			get
			{
				Guid guid = this.m_activityId;
				if (guid == Guid.Empty)
				{
					guid = EventSource.CurrentThreadActivityId;
				}
				return guid;
			}
			internal set
			{
				this.m_activityId = value;
			}
		}

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x0600351B RID: 13595 RVA: 0x000CE22D File Offset: 0x000CC42D
		// (set) Token: 0x0600351C RID: 13596 RVA: 0x000CE235 File Offset: 0x000CC435
		[__DynamicallyInvokable]
		public Guid RelatedActivityId
		{
			[CompilerGenerated]
			[SecurityCritical]
			[__DynamicallyInvokable]
			get
			{
				return this.<RelatedActivityId>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<RelatedActivityId>k__BackingField = value;
			}
		}

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x0600351D RID: 13597 RVA: 0x000CE23E File Offset: 0x000CC43E
		// (set) Token: 0x0600351E RID: 13598 RVA: 0x000CE246 File Offset: 0x000CC446
		[__DynamicallyInvokable]
		public ReadOnlyCollection<object> Payload
		{
			[CompilerGenerated]
			[__DynamicallyInvokable]
			get
			{
				return this.<Payload>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Payload>k__BackingField = value;
			}
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x0600351F RID: 13599 RVA: 0x000CE250 File Offset: 0x000CC450
		// (set) Token: 0x06003520 RID: 13600 RVA: 0x000CE2B9 File Offset: 0x000CC4B9
		[__DynamicallyInvokable]
		public ReadOnlyCollection<string> PayloadNames
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.m_payloadNames == null)
				{
					List<string> list = new List<string>();
					foreach (ParameterInfo parameterInfo in this.m_eventSource.m_eventData[this.EventId].Parameters)
					{
						list.Add(parameterInfo.Name);
					}
					this.m_payloadNames = new ReadOnlyCollection<string>(list);
				}
				return this.m_payloadNames;
			}
			internal set
			{
				this.m_payloadNames = value;
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06003521 RID: 13601 RVA: 0x000CE2C2 File Offset: 0x000CC4C2
		[__DynamicallyInvokable]
		public EventSource EventSource
		{
			[__DynamicallyInvokable]
			get
			{
				return this.m_eventSource;
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x06003522 RID: 13602 RVA: 0x000CE2CA File Offset: 0x000CC4CA
		[__DynamicallyInvokable]
		public EventKeywords Keywords
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.EventId < 0)
				{
					return this.m_keywords;
				}
				return (EventKeywords)this.m_eventSource.m_eventData[this.EventId].Descriptor.Keywords;
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06003523 RID: 13603 RVA: 0x000CE2FE File Offset: 0x000CC4FE
		[__DynamicallyInvokable]
		public EventOpcode Opcode
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.EventId < 0)
				{
					return this.m_opcode;
				}
				return (EventOpcode)this.m_eventSource.m_eventData[this.EventId].Descriptor.Opcode;
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x06003524 RID: 13604 RVA: 0x000CE332 File Offset: 0x000CC532
		[__DynamicallyInvokable]
		public EventTask Task
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.EventId < 0)
				{
					return EventTask.None;
				}
				return (EventTask)this.m_eventSource.m_eventData[this.EventId].Descriptor.Task;
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06003525 RID: 13605 RVA: 0x000CE361 File Offset: 0x000CC561
		[__DynamicallyInvokable]
		public EventTags Tags
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.EventId < 0)
				{
					return this.m_tags;
				}
				return this.m_eventSource.m_eventData[this.EventId].Tags;
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06003526 RID: 13606 RVA: 0x000CE390 File Offset: 0x000CC590
		// (set) Token: 0x06003527 RID: 13607 RVA: 0x000CE3BF File Offset: 0x000CC5BF
		[__DynamicallyInvokable]
		public string Message
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.EventId < 0)
				{
					return this.m_message;
				}
				return this.m_eventSource.m_eventData[this.EventId].Message;
			}
			internal set
			{
				this.m_message = value;
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06003528 RID: 13608 RVA: 0x000CE3C8 File Offset: 0x000CC5C8
		[__DynamicallyInvokable]
		public EventChannel Channel
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.EventId < 0)
				{
					return EventChannel.None;
				}
				return (EventChannel)this.m_eventSource.m_eventData[this.EventId].Descriptor.Channel;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06003529 RID: 13609 RVA: 0x000CE3F7 File Offset: 0x000CC5F7
		[__DynamicallyInvokable]
		public byte Version
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.EventId < 0)
				{
					return 0;
				}
				return this.m_eventSource.m_eventData[this.EventId].Descriptor.Version;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x0600352A RID: 13610 RVA: 0x000CE426 File Offset: 0x000CC626
		[__DynamicallyInvokable]
		public EventLevel Level
		{
			[__DynamicallyInvokable]
			get
			{
				if (this.EventId < 0)
				{
					return this.m_level;
				}
				return (EventLevel)this.m_eventSource.m_eventData[this.EventId].Descriptor.Level;
			}
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x000CE45A File Offset: 0x000CC65A
		internal EventWrittenEventArgs(EventSource eventSource)
		{
			this.m_eventSource = eventSource;
		}

		// Token: 0x0400178B RID: 6027
		[CompilerGenerated]
		private int <EventId>k__BackingField;

		// Token: 0x0400178C RID: 6028
		[CompilerGenerated]
		private Guid <RelatedActivityId>k__BackingField;

		// Token: 0x0400178D RID: 6029
		[CompilerGenerated]
		private ReadOnlyCollection<object> <Payload>k__BackingField;

		// Token: 0x0400178E RID: 6030
		private string m_message;

		// Token: 0x0400178F RID: 6031
		private string m_eventName;

		// Token: 0x04001790 RID: 6032
		private EventSource m_eventSource;

		// Token: 0x04001791 RID: 6033
		private ReadOnlyCollection<string> m_payloadNames;

		// Token: 0x04001792 RID: 6034
		private Guid m_activityId;

		// Token: 0x04001793 RID: 6035
		internal EventTags m_tags;

		// Token: 0x04001794 RID: 6036
		internal EventOpcode m_opcode;

		// Token: 0x04001795 RID: 6037
		internal EventKeywords m_keywords;

		// Token: 0x04001796 RID: 6038
		internal EventLevel m_level;
	}
}
