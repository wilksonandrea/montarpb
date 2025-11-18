using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200045F RID: 1119
	internal sealed class SingleTypeInfo : TraceLoggingTypeInfo<float>
	{
		// Token: 0x0600368D RID: 13965 RVA: 0x000D3220 File Offset: 0x000D1420
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format32(format, TraceLoggingDataType.Float));
		}

		// Token: 0x0600368E RID: 13966 RVA: 0x000D3231 File Offset: 0x000D1431
		public override void WriteData(TraceLoggingDataCollector collector, ref float value)
		{
			collector.AddScalar(value);
		}

		// Token: 0x0600368F RID: 13967 RVA: 0x000D323B File Offset: 0x000D143B
		public SingleTypeInfo()
		{
		}
	}
}
