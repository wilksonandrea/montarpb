using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000457 RID: 1111
	internal sealed class UInt16TypeInfo : TraceLoggingTypeInfo<ushort>
	{
		// Token: 0x06003675 RID: 13941 RVA: 0x000D3105 File Offset: 0x000D1305
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format16(format, TraceLoggingDataType.UInt16));
		}

		// Token: 0x06003676 RID: 13942 RVA: 0x000D3115 File Offset: 0x000D1315
		public override void WriteData(TraceLoggingDataCollector collector, ref ushort value)
		{
			collector.AddScalar(value);
		}

		// Token: 0x06003677 RID: 13943 RVA: 0x000D311F File Offset: 0x000D131F
		public UInt16TypeInfo()
		{
		}
	}
}
