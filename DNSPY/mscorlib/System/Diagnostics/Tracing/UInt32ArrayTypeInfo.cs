using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000467 RID: 1127
	internal sealed class UInt32ArrayTypeInfo : TraceLoggingTypeInfo<uint[]>
	{
		// Token: 0x060036A5 RID: 13989 RVA: 0x000D33A2 File Offset: 0x000D15A2
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format32(format, TraceLoggingDataType.UInt32));
		}

		// Token: 0x060036A6 RID: 13990 RVA: 0x000D33B2 File Offset: 0x000D15B2
		public override void WriteData(TraceLoggingDataCollector collector, ref uint[] value)
		{
			collector.AddArray(value);
		}

		// Token: 0x060036A7 RID: 13991 RVA: 0x000D33BC File Offset: 0x000D15BC
		public UInt32ArrayTypeInfo()
		{
		}
	}
}
