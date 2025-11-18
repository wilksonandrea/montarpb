using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200046C RID: 1132
	internal sealed class CharArrayTypeInfo : TraceLoggingTypeInfo<char[]>
	{
		// Token: 0x060036B4 RID: 14004 RVA: 0x000D3456 File Offset: 0x000D1656
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format16(format, TraceLoggingDataType.Char16));
		}

		// Token: 0x060036B5 RID: 14005 RVA: 0x000D346A File Offset: 0x000D166A
		public override void WriteData(TraceLoggingDataCollector collector, ref char[] value)
		{
			collector.AddArray(value);
		}

		// Token: 0x060036B6 RID: 14006 RVA: 0x000D3474 File Offset: 0x000D1674
		public CharArrayTypeInfo()
		{
		}
	}
}
