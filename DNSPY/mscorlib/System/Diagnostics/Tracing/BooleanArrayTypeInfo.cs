using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000461 RID: 1121
	internal sealed class BooleanArrayTypeInfo : TraceLoggingTypeInfo<bool[]>
	{
		// Token: 0x06003693 RID: 13971 RVA: 0x000D3269 File Offset: 0x000D1469
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format8(format, TraceLoggingDataType.Boolean8));
		}

		// Token: 0x06003694 RID: 13972 RVA: 0x000D327D File Offset: 0x000D147D
		public override void WriteData(TraceLoggingDataCollector collector, ref bool[] value)
		{
			collector.AddArray(value);
		}

		// Token: 0x06003695 RID: 13973 RVA: 0x000D3287 File Offset: 0x000D1487
		public BooleanArrayTypeInfo()
		{
		}
	}
}
