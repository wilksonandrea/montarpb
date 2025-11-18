using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000468 RID: 1128
	internal sealed class Int64ArrayTypeInfo : TraceLoggingTypeInfo<long[]>
	{
		// Token: 0x060036A8 RID: 13992 RVA: 0x000D33C4 File Offset: 0x000D15C4
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format64(format, TraceLoggingDataType.Int64));
		}

		// Token: 0x060036A9 RID: 13993 RVA: 0x000D33D5 File Offset: 0x000D15D5
		public override void WriteData(TraceLoggingDataCollector collector, ref long[] value)
		{
			collector.AddArray(value);
		}

		// Token: 0x060036AA RID: 13994 RVA: 0x000D33DF File Offset: 0x000D15DF
		public Int64ArrayTypeInfo()
		{
		}
	}
}
