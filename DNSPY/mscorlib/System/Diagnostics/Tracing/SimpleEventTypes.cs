using System;
using System.Threading;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000451 RID: 1105
	internal class SimpleEventTypes<T> : TraceLoggingEventTypes
	{
		// Token: 0x06003662 RID: 13922 RVA: 0x000D3002 File Offset: 0x000D1202
		private SimpleEventTypes(TraceLoggingTypeInfo<T> typeInfo)
			: base(typeInfo.Name, typeInfo.Tags, new TraceLoggingTypeInfo[] { typeInfo })
		{
			this.typeInfo = typeInfo;
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06003663 RID: 13923 RVA: 0x000D3027 File Offset: 0x000D1227
		public static SimpleEventTypes<T> Instance
		{
			get
			{
				return SimpleEventTypes<T>.instance ?? SimpleEventTypes<T>.InitInstance();
			}
		}

		// Token: 0x06003664 RID: 13924 RVA: 0x000D3038 File Offset: 0x000D1238
		private static SimpleEventTypes<T> InitInstance()
		{
			SimpleEventTypes<T> simpleEventTypes = new SimpleEventTypes<T>(TraceLoggingTypeInfo<T>.Instance);
			Interlocked.CompareExchange<SimpleEventTypes<T>>(ref SimpleEventTypes<T>.instance, simpleEventTypes, null);
			return SimpleEventTypes<T>.instance;
		}

		// Token: 0x0400185B RID: 6235
		private static SimpleEventTypes<T> instance;

		// Token: 0x0400185C RID: 6236
		internal readonly TraceLoggingTypeInfo<T> typeInfo;
	}
}
