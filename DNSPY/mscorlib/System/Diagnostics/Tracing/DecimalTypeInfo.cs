using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200047D RID: 1149
	internal sealed class DecimalTypeInfo : TraceLoggingTypeInfo<decimal>
	{
		// Token: 0x060036F0 RID: 14064 RVA: 0x000D37C4 File Offset: 0x000D19C4
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.MakeDataType(TraceLoggingDataType.Double, format));
		}

		// Token: 0x060036F1 RID: 14065 RVA: 0x000D37D5 File Offset: 0x000D19D5
		public override void WriteData(TraceLoggingDataCollector collector, ref decimal value)
		{
			collector.AddScalar((double)value);
		}

		// Token: 0x060036F2 RID: 14066 RVA: 0x000D37E9 File Offset: 0x000D19E9
		public DecimalTypeInfo()
		{
		}
	}
}
