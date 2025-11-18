using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000459 RID: 1113
	internal sealed class UInt32TypeInfo : TraceLoggingTypeInfo<uint>
	{
		// Token: 0x0600367B RID: 13947 RVA: 0x000D3149 File Offset: 0x000D1349
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format32(format, TraceLoggingDataType.UInt32));
		}

		// Token: 0x0600367C RID: 13948 RVA: 0x000D3159 File Offset: 0x000D1359
		public override void WriteData(TraceLoggingDataCollector collector, ref uint value)
		{
			collector.AddScalar(value);
		}

		// Token: 0x0600367D RID: 13949 RVA: 0x000D3163 File Offset: 0x000D1363
		public UInt32TypeInfo()
		{
		}
	}
}
