using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000453 RID: 1107
	internal sealed class BooleanTypeInfo : TraceLoggingTypeInfo<bool>
	{
		// Token: 0x06003669 RID: 13929 RVA: 0x000D3079 File Offset: 0x000D1279
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format8(format, TraceLoggingDataType.Boolean8));
		}

		// Token: 0x0600366A RID: 13930 RVA: 0x000D308D File Offset: 0x000D128D
		public override void WriteData(TraceLoggingDataCollector collector, ref bool value)
		{
			collector.AddScalar(value);
		}

		// Token: 0x0600366B RID: 13931 RVA: 0x000D3097 File Offset: 0x000D1297
		public BooleanTypeInfo()
		{
		}
	}
}
