using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000466 RID: 1126
	internal sealed class Int32ArrayTypeInfo : TraceLoggingTypeInfo<int[]>
	{
		// Token: 0x060036A2 RID: 13986 RVA: 0x000D3380 File Offset: 0x000D1580
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format32(format, TraceLoggingDataType.Int32));
		}

		// Token: 0x060036A3 RID: 13987 RVA: 0x000D3390 File Offset: 0x000D1590
		public override void WriteData(TraceLoggingDataCollector collector, ref int[] value)
		{
			collector.AddArray(value);
		}

		// Token: 0x060036A4 RID: 13988 RVA: 0x000D339A File Offset: 0x000D159A
		public Int32ArrayTypeInfo()
		{
		}
	}
}
