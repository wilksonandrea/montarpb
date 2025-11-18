using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200045B RID: 1115
	internal sealed class UInt64TypeInfo : TraceLoggingTypeInfo<ulong>
	{
		// Token: 0x06003681 RID: 13953 RVA: 0x000D318E File Offset: 0x000D138E
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format64(format, TraceLoggingDataType.UInt64));
		}

		// Token: 0x06003682 RID: 13954 RVA: 0x000D319F File Offset: 0x000D139F
		public override void WriteData(TraceLoggingDataCollector collector, ref ulong value)
		{
			collector.AddScalar(value);
		}

		// Token: 0x06003683 RID: 13955 RVA: 0x000D31A9 File Offset: 0x000D13A9
		public UInt64TypeInfo()
		{
		}
	}
}
