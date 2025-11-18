using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000462 RID: 1122
	internal sealed class ByteArrayTypeInfo : TraceLoggingTypeInfo<byte[]>
	{
		// Token: 0x06003696 RID: 13974 RVA: 0x000D3290 File Offset: 0x000D1490
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			switch (format)
			{
			case EventFieldFormat.String:
				collector.AddBinary(name, TraceLoggingDataType.CountedMbcsString);
				return;
			case EventFieldFormat.Boolean:
				collector.AddArray(name, TraceLoggingDataType.Boolean8);
				return;
			case EventFieldFormat.Hexadecimal:
				collector.AddArray(name, TraceLoggingDataType.HexInt8);
				return;
			default:
				if (format == EventFieldFormat.Xml)
				{
					collector.AddBinary(name, TraceLoggingDataType.CountedMbcsXml);
					return;
				}
				if (format != EventFieldFormat.Json)
				{
					collector.AddBinary(name, Statics.MakeDataType(TraceLoggingDataType.Binary, format));
					return;
				}
				collector.AddBinary(name, TraceLoggingDataType.CountedMbcsJson);
				return;
			}
		}

		// Token: 0x06003697 RID: 13975 RVA: 0x000D3308 File Offset: 0x000D1508
		public override void WriteData(TraceLoggingDataCollector collector, ref byte[] value)
		{
			collector.AddBinary(value);
		}

		// Token: 0x06003698 RID: 13976 RVA: 0x000D3312 File Offset: 0x000D1512
		public ByteArrayTypeInfo()
		{
		}
	}
}
