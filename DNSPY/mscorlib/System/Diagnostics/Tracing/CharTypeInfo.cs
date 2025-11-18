using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000460 RID: 1120
	internal sealed class CharTypeInfo : TraceLoggingTypeInfo<char>
	{
		// Token: 0x06003690 RID: 13968 RVA: 0x000D3243 File Offset: 0x000D1443
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format16(format, TraceLoggingDataType.Char16));
		}

		// Token: 0x06003691 RID: 13969 RVA: 0x000D3257 File Offset: 0x000D1457
		public override void WriteData(TraceLoggingDataCollector collector, ref char value)
		{
			collector.AddScalar(value);
		}

		// Token: 0x06003692 RID: 13970 RVA: 0x000D3261 File Offset: 0x000D1461
		public CharTypeInfo()
		{
		}
	}
}
