using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000479 RID: 1145
	internal sealed class GuidArrayTypeInfo : TraceLoggingTypeInfo<Guid[]>
	{
		// Token: 0x060036E4 RID: 14052 RVA: 0x000D369E File Offset: 0x000D189E
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.MakeDataType(TraceLoggingDataType.Guid, format));
		}

		// Token: 0x060036E5 RID: 14053 RVA: 0x000D36AF File Offset: 0x000D18AF
		public override void WriteData(TraceLoggingDataCollector collector, ref Guid[] value)
		{
			collector.AddArray(value);
		}

		// Token: 0x060036E6 RID: 14054 RVA: 0x000D36B9 File Offset: 0x000D18B9
		public GuidArrayTypeInfo()
		{
		}
	}
}
