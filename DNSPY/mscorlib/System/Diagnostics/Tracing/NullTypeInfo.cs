using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000452 RID: 1106
	internal sealed class NullTypeInfo<DataType> : TraceLoggingTypeInfo<DataType>
	{
		// Token: 0x06003665 RID: 13925 RVA: 0x000D3062 File Offset: 0x000D1262
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddGroup(name);
		}

		// Token: 0x06003666 RID: 13926 RVA: 0x000D306C File Offset: 0x000D126C
		public override void WriteData(TraceLoggingDataCollector collector, ref DataType value)
		{
		}

		// Token: 0x06003667 RID: 13927 RVA: 0x000D306E File Offset: 0x000D126E
		public override object GetData(object value)
		{
			return null;
		}

		// Token: 0x06003668 RID: 13928 RVA: 0x000D3071 File Offset: 0x000D1271
		public NullTypeInfo()
		{
		}
	}
}
