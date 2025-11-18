using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000469 RID: 1129
	internal sealed class UInt64ArrayTypeInfo : TraceLoggingTypeInfo<ulong[]>
	{
		// Token: 0x060036AB RID: 13995 RVA: 0x000D33E7 File Offset: 0x000D15E7
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format64(format, TraceLoggingDataType.UInt64));
		}

		// Token: 0x060036AC RID: 13996 RVA: 0x000D33F8 File Offset: 0x000D15F8
		public override void WriteData(TraceLoggingDataCollector collector, ref ulong[] value)
		{
			collector.AddArray(value);
		}

		// Token: 0x060036AD RID: 13997 RVA: 0x000D3402 File Offset: 0x000D1602
		public UInt64ArrayTypeInfo()
		{
		}
	}
}
