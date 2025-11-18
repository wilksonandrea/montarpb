using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000455 RID: 1109
	internal sealed class SByteTypeInfo : TraceLoggingTypeInfo<sbyte>
	{
		// Token: 0x0600366F RID: 13935 RVA: 0x000D30C1 File Offset: 0x000D12C1
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format8(format, TraceLoggingDataType.Int8));
		}

		// Token: 0x06003670 RID: 13936 RVA: 0x000D30D1 File Offset: 0x000D12D1
		public override void WriteData(TraceLoggingDataCollector collector, ref sbyte value)
		{
			collector.AddScalar(value);
		}

		// Token: 0x06003671 RID: 13937 RVA: 0x000D30DB File Offset: 0x000D12DB
		public SByteTypeInfo()
		{
		}
	}
}
