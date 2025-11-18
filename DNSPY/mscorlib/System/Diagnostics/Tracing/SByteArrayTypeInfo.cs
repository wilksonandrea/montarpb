using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000463 RID: 1123
	internal sealed class SByteArrayTypeInfo : TraceLoggingTypeInfo<sbyte[]>
	{
		// Token: 0x06003699 RID: 13977 RVA: 0x000D331A File Offset: 0x000D151A
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format8(format, TraceLoggingDataType.Int8));
		}

		// Token: 0x0600369A RID: 13978 RVA: 0x000D332A File Offset: 0x000D152A
		public override void WriteData(TraceLoggingDataCollector collector, ref sbyte[] value)
		{
			collector.AddArray(value);
		}

		// Token: 0x0600369B RID: 13979 RVA: 0x000D3334 File Offset: 0x000D1534
		public SByteArrayTypeInfo()
		{
		}
	}
}
