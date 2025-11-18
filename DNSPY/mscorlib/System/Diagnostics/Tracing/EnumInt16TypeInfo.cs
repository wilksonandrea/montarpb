using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000471 RID: 1137
	internal sealed class EnumInt16TypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x060036C5 RID: 14021 RVA: 0x000D351E File Offset: 0x000D171E
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format16(format, TraceLoggingDataType.Int16));
		}

		// Token: 0x060036C6 RID: 14022 RVA: 0x000D352E File Offset: 0x000D172E
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<short>.Cast<EnumType>(value));
		}

		// Token: 0x060036C7 RID: 14023 RVA: 0x000D3541 File Offset: 0x000D1741
		public override object GetData(object value)
		{
			return value;
		}

		// Token: 0x060036C8 RID: 14024 RVA: 0x000D3544 File Offset: 0x000D1744
		public EnumInt16TypeInfo()
		{
		}
	}
}
