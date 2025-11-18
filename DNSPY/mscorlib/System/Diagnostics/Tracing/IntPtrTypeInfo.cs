using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200045C RID: 1116
	internal sealed class IntPtrTypeInfo : TraceLoggingTypeInfo<IntPtr>
	{
		// Token: 0x06003684 RID: 13956 RVA: 0x000D31B1 File Offset: 0x000D13B1
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.FormatPtr(format, Statics.IntPtrType));
		}

		// Token: 0x06003685 RID: 13957 RVA: 0x000D31C5 File Offset: 0x000D13C5
		public override void WriteData(TraceLoggingDataCollector collector, ref IntPtr value)
		{
			collector.AddScalar(value);
		}

		// Token: 0x06003686 RID: 13958 RVA: 0x000D31CF File Offset: 0x000D13CF
		public IntPtrTypeInfo()
		{
		}
	}
}
