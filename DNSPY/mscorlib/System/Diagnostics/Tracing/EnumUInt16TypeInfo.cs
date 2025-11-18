using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000472 RID: 1138
	internal sealed class EnumUInt16TypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x060036C9 RID: 14025 RVA: 0x000D354C File Offset: 0x000D174C
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format16(format, TraceLoggingDataType.UInt16));
		}

		// Token: 0x060036CA RID: 14026 RVA: 0x000D355C File Offset: 0x000D175C
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<ushort>.Cast<EnumType>(value));
		}

		// Token: 0x060036CB RID: 14027 RVA: 0x000D356F File Offset: 0x000D176F
		public override object GetData(object value)
		{
			return value;
		}

		// Token: 0x060036CC RID: 14028 RVA: 0x000D3572 File Offset: 0x000D1772
		public EnumUInt16TypeInfo()
		{
		}
	}
}
