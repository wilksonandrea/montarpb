using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200046A RID: 1130
	internal sealed class IntPtrArrayTypeInfo : TraceLoggingTypeInfo<IntPtr[]>
	{
		// Token: 0x060036AE RID: 13998 RVA: 0x000D340A File Offset: 0x000D160A
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.FormatPtr(format, Statics.IntPtrType));
		}

		// Token: 0x060036AF RID: 13999 RVA: 0x000D341E File Offset: 0x000D161E
		public override void WriteData(TraceLoggingDataCollector collector, ref IntPtr[] value)
		{
			collector.AddArray(value);
		}

		// Token: 0x060036B0 RID: 14000 RVA: 0x000D3428 File Offset: 0x000D1628
		public IntPtrArrayTypeInfo()
		{
		}
	}
}
