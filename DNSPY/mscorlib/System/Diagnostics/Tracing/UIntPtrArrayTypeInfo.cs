using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200046B RID: 1131
	internal sealed class UIntPtrArrayTypeInfo : TraceLoggingTypeInfo<UIntPtr[]>
	{
		// Token: 0x060036B1 RID: 14001 RVA: 0x000D3430 File Offset: 0x000D1630
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.FormatPtr(format, Statics.UIntPtrType));
		}

		// Token: 0x060036B2 RID: 14002 RVA: 0x000D3444 File Offset: 0x000D1644
		public override void WriteData(TraceLoggingDataCollector collector, ref UIntPtr[] value)
		{
			collector.AddArray(value);
		}

		// Token: 0x060036B3 RID: 14003 RVA: 0x000D344E File Offset: 0x000D164E
		public UIntPtrArrayTypeInfo()
		{
		}
	}
}
