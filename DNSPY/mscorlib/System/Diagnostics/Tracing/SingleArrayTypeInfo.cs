using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200046E RID: 1134
	internal sealed class SingleArrayTypeInfo : TraceLoggingTypeInfo<float[]>
	{
		// Token: 0x060036BA RID: 14010 RVA: 0x000D349F File Offset: 0x000D169F
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format32(format, TraceLoggingDataType.Float));
		}

		// Token: 0x060036BB RID: 14011 RVA: 0x000D34B0 File Offset: 0x000D16B0
		public override void WriteData(TraceLoggingDataCollector collector, ref float[] value)
		{
			collector.AddArray(value);
		}

		// Token: 0x060036BC RID: 14012 RVA: 0x000D34BA File Offset: 0x000D16BA
		public SingleArrayTypeInfo()
		{
		}
	}
}
