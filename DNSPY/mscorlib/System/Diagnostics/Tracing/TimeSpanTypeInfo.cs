using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200047C RID: 1148
	internal sealed class TimeSpanTypeInfo : TraceLoggingTypeInfo<TimeSpan>
	{
		// Token: 0x060036ED RID: 14061 RVA: 0x000D379D File Offset: 0x000D199D
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.MakeDataType(TraceLoggingDataType.Int64, format));
		}

		// Token: 0x060036EE RID: 14062 RVA: 0x000D37AE File Offset: 0x000D19AE
		public override void WriteData(TraceLoggingDataCollector collector, ref TimeSpan value)
		{
			collector.AddScalar(value.Ticks);
		}

		// Token: 0x060036EF RID: 14063 RVA: 0x000D37BC File Offset: 0x000D19BC
		public TimeSpanTypeInfo()
		{
		}
	}
}
