using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000477 RID: 1143
	internal sealed class StringTypeInfo : TraceLoggingTypeInfo<string>
	{
		// Token: 0x060036DD RID: 14045 RVA: 0x000D3634 File Offset: 0x000D1834
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddBinary(name, Statics.MakeDataType(TraceLoggingDataType.CountedUtf16String, format));
		}

		// Token: 0x060036DE RID: 14046 RVA: 0x000D3645 File Offset: 0x000D1845
		public override void WriteData(TraceLoggingDataCollector collector, ref string value)
		{
			collector.AddBinary(value);
		}

		// Token: 0x060036DF RID: 14047 RVA: 0x000D3650 File Offset: 0x000D1850
		public override object GetData(object value)
		{
			object obj = base.GetData(value);
			if (obj == null)
			{
				obj = "";
			}
			return obj;
		}

		// Token: 0x060036E0 RID: 14048 RVA: 0x000D366F File Offset: 0x000D186F
		public StringTypeInfo()
		{
		}
	}
}
