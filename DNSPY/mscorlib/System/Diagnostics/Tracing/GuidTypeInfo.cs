using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000478 RID: 1144
	internal sealed class GuidTypeInfo : TraceLoggingTypeInfo<Guid>
	{
		// Token: 0x060036E1 RID: 14049 RVA: 0x000D3677 File Offset: 0x000D1877
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.MakeDataType(TraceLoggingDataType.Guid, format));
		}

		// Token: 0x060036E2 RID: 14050 RVA: 0x000D3688 File Offset: 0x000D1888
		public override void WriteData(TraceLoggingDataCollector collector, ref Guid value)
		{
			collector.AddScalar(value);
		}

		// Token: 0x060036E3 RID: 14051 RVA: 0x000D3696 File Offset: 0x000D1896
		public GuidTypeInfo()
		{
		}
	}
}
