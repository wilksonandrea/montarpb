using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000458 RID: 1112
	internal sealed class Int32TypeInfo : TraceLoggingTypeInfo<int>
	{
		// Token: 0x06003678 RID: 13944 RVA: 0x000D3127 File Offset: 0x000D1327
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format32(format, TraceLoggingDataType.Int32));
		}

		// Token: 0x06003679 RID: 13945 RVA: 0x000D3137 File Offset: 0x000D1337
		public override void WriteData(TraceLoggingDataCollector collector, ref int value)
		{
			collector.AddScalar(value);
		}

		// Token: 0x0600367A RID: 13946 RVA: 0x000D3141 File Offset: 0x000D1341
		public Int32TypeInfo()
		{
		}
	}
}
