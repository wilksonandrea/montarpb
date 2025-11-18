using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000470 RID: 1136
	internal sealed class EnumSByteTypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x060036C1 RID: 14017 RVA: 0x000D34F0 File Offset: 0x000D16F0
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format8(format, TraceLoggingDataType.Int8));
		}

		// Token: 0x060036C2 RID: 14018 RVA: 0x000D3500 File Offset: 0x000D1700
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<sbyte>.Cast<EnumType>(value));
		}

		// Token: 0x060036C3 RID: 14019 RVA: 0x000D3513 File Offset: 0x000D1713
		public override object GetData(object value)
		{
			return value;
		}

		// Token: 0x060036C4 RID: 14020 RVA: 0x000D3516 File Offset: 0x000D1716
		public EnumSByteTypeInfo()
		{
		}
	}
}
