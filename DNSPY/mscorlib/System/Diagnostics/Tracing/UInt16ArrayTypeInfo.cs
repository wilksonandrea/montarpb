using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000465 RID: 1125
	internal sealed class UInt16ArrayTypeInfo : TraceLoggingTypeInfo<ushort[]>
	{
		// Token: 0x0600369F RID: 13983 RVA: 0x000D335E File Offset: 0x000D155E
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format16(format, TraceLoggingDataType.UInt16));
		}

		// Token: 0x060036A0 RID: 13984 RVA: 0x000D336E File Offset: 0x000D156E
		public override void WriteData(TraceLoggingDataCollector collector, ref ushort[] value)
		{
			collector.AddArray(value);
		}

		// Token: 0x060036A1 RID: 13985 RVA: 0x000D3378 File Offset: 0x000D1578
		public UInt16ArrayTypeInfo()
		{
		}
	}
}
