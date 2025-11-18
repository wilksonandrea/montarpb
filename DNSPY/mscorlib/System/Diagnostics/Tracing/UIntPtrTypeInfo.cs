using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200045D RID: 1117
	internal sealed class UIntPtrTypeInfo : TraceLoggingTypeInfo<UIntPtr>
	{
		// Token: 0x06003687 RID: 13959 RVA: 0x000D31D7 File Offset: 0x000D13D7
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.FormatPtr(format, Statics.UIntPtrType));
		}

		// Token: 0x06003688 RID: 13960 RVA: 0x000D31EB File Offset: 0x000D13EB
		public override void WriteData(TraceLoggingDataCollector collector, ref UIntPtr value)
		{
			collector.AddScalar(value);
		}

		// Token: 0x06003689 RID: 13961 RVA: 0x000D31F5 File Offset: 0x000D13F5
		public UIntPtrTypeInfo()
		{
		}
	}
}
