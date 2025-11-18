using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000476 RID: 1142
	internal sealed class EnumUInt64TypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x060036D9 RID: 14041 RVA: 0x000D3605 File Offset: 0x000D1805
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format64(format, TraceLoggingDataType.UInt64));
		}

		// Token: 0x060036DA RID: 14042 RVA: 0x000D3616 File Offset: 0x000D1816
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<ulong>.Cast<EnumType>(value));
		}

		// Token: 0x060036DB RID: 14043 RVA: 0x000D3629 File Offset: 0x000D1829
		public override object GetData(object value)
		{
			return value;
		}

		// Token: 0x060036DC RID: 14044 RVA: 0x000D362C File Offset: 0x000D182C
		public EnumUInt64TypeInfo()
		{
		}
	}
}
