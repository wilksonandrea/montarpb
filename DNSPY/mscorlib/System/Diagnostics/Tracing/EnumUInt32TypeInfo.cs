using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000474 RID: 1140
	internal sealed class EnumUInt32TypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x060036D1 RID: 14033 RVA: 0x000D35A8 File Offset: 0x000D17A8
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format32(format, TraceLoggingDataType.UInt32));
		}

		// Token: 0x060036D2 RID: 14034 RVA: 0x000D35B8 File Offset: 0x000D17B8
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<uint>.Cast<EnumType>(value));
		}

		// Token: 0x060036D3 RID: 14035 RVA: 0x000D35CB File Offset: 0x000D17CB
		public override object GetData(object value)
		{
			return value;
		}

		// Token: 0x060036D4 RID: 14036 RVA: 0x000D35CE File Offset: 0x000D17CE
		public EnumUInt32TypeInfo()
		{
		}
	}
}
