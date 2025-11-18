using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000456 RID: 1110
	internal sealed class Int16TypeInfo : TraceLoggingTypeInfo<short>
	{
		// Token: 0x06003672 RID: 13938 RVA: 0x000D30E3 File Offset: 0x000D12E3
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format16(format, TraceLoggingDataType.Int16));
		}

		// Token: 0x06003673 RID: 13939 RVA: 0x000D30F3 File Offset: 0x000D12F3
		public override void WriteData(TraceLoggingDataCollector collector, ref short value)
		{
			collector.AddScalar(value);
		}

		// Token: 0x06003674 RID: 13940 RVA: 0x000D30FD File Offset: 0x000D12FD
		public Int16TypeInfo()
		{
		}
	}
}
