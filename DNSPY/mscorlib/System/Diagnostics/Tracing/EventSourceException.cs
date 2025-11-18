using System;
using System.Runtime.Serialization;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000433 RID: 1075
	[__DynamicallyInvokable]
	[Serializable]
	public class EventSourceException : Exception
	{
		// Token: 0x06003590 RID: 13712 RVA: 0x000D0F44 File Offset: 0x000CF144
		[__DynamicallyInvokable]
		public EventSourceException()
			: base(Environment.GetResourceString("EventSource_ListenerWriteFailure"))
		{
		}

		// Token: 0x06003591 RID: 13713 RVA: 0x000D0F56 File Offset: 0x000CF156
		[__DynamicallyInvokable]
		public EventSourceException(string message)
			: base(message)
		{
		}

		// Token: 0x06003592 RID: 13714 RVA: 0x000D0F5F File Offset: 0x000CF15F
		[__DynamicallyInvokable]
		public EventSourceException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06003593 RID: 13715 RVA: 0x000D0F69 File Offset: 0x000CF169
		protected EventSourceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06003594 RID: 13716 RVA: 0x000D0F73 File Offset: 0x000CF173
		internal EventSourceException(Exception innerException)
			: base(Environment.GetResourceString("EventSource_ListenerWriteFailure"), innerException)
		{
		}
	}
}
