using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000454 RID: 1108
	internal sealed class ByteTypeInfo : TraceLoggingTypeInfo<byte>
	{
		// Token: 0x0600366C RID: 13932 RVA: 0x000D309F File Offset: 0x000D129F
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format8(format, TraceLoggingDataType.UInt8));
		}

		// Token: 0x0600366D RID: 13933 RVA: 0x000D30AF File Offset: 0x000D12AF
		public override void WriteData(TraceLoggingDataCollector collector, ref byte value)
		{
			collector.AddScalar(value);
		}

		// Token: 0x0600366E RID: 13934 RVA: 0x000D30B9 File Offset: 0x000D12B9
		public ByteTypeInfo()
		{
		}
	}
}
