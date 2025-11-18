using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200045E RID: 1118
	internal sealed class DoubleTypeInfo : TraceLoggingTypeInfo<double>
	{
		// Token: 0x0600368A RID: 13962 RVA: 0x000D31FD File Offset: 0x000D13FD
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format64(format, TraceLoggingDataType.Double));
		}

		// Token: 0x0600368B RID: 13963 RVA: 0x000D320E File Offset: 0x000D140E
		public override void WriteData(TraceLoggingDataCollector collector, ref double value)
		{
			collector.AddScalar(value);
		}

		// Token: 0x0600368C RID: 13964 RVA: 0x000D3218 File Offset: 0x000D1418
		public DoubleTypeInfo()
		{
		}
	}
}
