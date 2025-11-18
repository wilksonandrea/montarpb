using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000464 RID: 1124
	internal sealed class Int16ArrayTypeInfo : TraceLoggingTypeInfo<short[]>
	{
		// Token: 0x0600369C RID: 13980 RVA: 0x000D333C File Offset: 0x000D153C
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format16(format, TraceLoggingDataType.Int16));
		}

		// Token: 0x0600369D RID: 13981 RVA: 0x000D334C File Offset: 0x000D154C
		public override void WriteData(TraceLoggingDataCollector collector, ref short[] value)
		{
			collector.AddArray(value);
		}

		// Token: 0x0600369E RID: 13982 RVA: 0x000D3356 File Offset: 0x000D1556
		public Int16ArrayTypeInfo()
		{
		}
	}
}
