using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200047A RID: 1146
	internal sealed class DateTimeTypeInfo : TraceLoggingTypeInfo<DateTime>
	{
		// Token: 0x060036E7 RID: 14055 RVA: 0x000D36C1 File Offset: 0x000D18C1
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.MakeDataType(TraceLoggingDataType.FileTime, format));
		}

		// Token: 0x060036E8 RID: 14056 RVA: 0x000D36D4 File Offset: 0x000D18D4
		public override void WriteData(TraceLoggingDataCollector collector, ref DateTime value)
		{
			long ticks = value.Ticks;
			collector.AddScalar((ticks < 504911232000000000L) ? 0L : (ticks - 504911232000000000L));
		}

		// Token: 0x060036E9 RID: 14057 RVA: 0x000D3709 File Offset: 0x000D1909
		public DateTimeTypeInfo()
		{
		}
	}
}
