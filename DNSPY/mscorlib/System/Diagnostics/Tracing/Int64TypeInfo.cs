using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200045A RID: 1114
	internal sealed class Int64TypeInfo : TraceLoggingTypeInfo<long>
	{
		// Token: 0x0600367E RID: 13950 RVA: 0x000D316B File Offset: 0x000D136B
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format64(format, TraceLoggingDataType.Int64));
		}

		// Token: 0x0600367F RID: 13951 RVA: 0x000D317C File Offset: 0x000D137C
		public override void WriteData(TraceLoggingDataCollector collector, ref long value)
		{
			collector.AddScalar(value);
		}

		// Token: 0x06003680 RID: 13952 RVA: 0x000D3186 File Offset: 0x000D1386
		public Int64TypeInfo()
		{
		}
	}
}
