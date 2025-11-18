using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200047B RID: 1147
	internal sealed class DateTimeOffsetTypeInfo : TraceLoggingTypeInfo<DateTimeOffset>
	{
		// Token: 0x060036EA RID: 14058 RVA: 0x000D3714 File Offset: 0x000D1914
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			TraceLoggingMetadataCollector traceLoggingMetadataCollector = collector.AddGroup(name);
			traceLoggingMetadataCollector.AddScalar("Ticks", Statics.MakeDataType(TraceLoggingDataType.FileTime, format));
			traceLoggingMetadataCollector.AddScalar("Offset", TraceLoggingDataType.Int64);
		}

		// Token: 0x060036EB RID: 14059 RVA: 0x000D374C File Offset: 0x000D194C
		public override void WriteData(TraceLoggingDataCollector collector, ref DateTimeOffset value)
		{
			long ticks = value.Ticks;
			collector.AddScalar((ticks < 504911232000000000L) ? 0L : (ticks - 504911232000000000L));
			collector.AddScalar(value.Offset.Ticks);
		}

		// Token: 0x060036EC RID: 14060 RVA: 0x000D3795 File Offset: 0x000D1995
		public DateTimeOffsetTypeInfo()
		{
		}
	}
}
