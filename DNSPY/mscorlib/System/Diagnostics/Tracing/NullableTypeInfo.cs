using System;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200047F RID: 1151
	internal sealed class NullableTypeInfo<T> : TraceLoggingTypeInfo<T?> where T : struct
	{
		// Token: 0x060036F7 RID: 14071 RVA: 0x000D38E7 File Offset: 0x000D1AE7
		public NullableTypeInfo(List<Type> recursionCheck)
		{
			this.valueInfo = TraceLoggingTypeInfo<T>.GetInstance(recursionCheck);
		}

		// Token: 0x060036F8 RID: 14072 RVA: 0x000D38FC File Offset: 0x000D1AFC
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			TraceLoggingMetadataCollector traceLoggingMetadataCollector = collector.AddGroup(name);
			traceLoggingMetadataCollector.AddScalar("HasValue", TraceLoggingDataType.Boolean8);
			this.valueInfo.WriteMetadata(traceLoggingMetadataCollector, "Value", format);
		}

		// Token: 0x060036F9 RID: 14073 RVA: 0x000D3934 File Offset: 0x000D1B34
		public override void WriteData(TraceLoggingDataCollector collector, ref T? value)
		{
			bool flag = value != null;
			collector.AddScalar(flag);
			T t = (flag ? value.Value : default(T));
			this.valueInfo.WriteData(collector, ref t);
		}

		// Token: 0x0400185F RID: 6239
		private readonly TraceLoggingTypeInfo<T> valueInfo;
	}
}
