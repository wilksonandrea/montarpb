using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000475 RID: 1141
	internal sealed class EnumInt64TypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x060036D5 RID: 14037 RVA: 0x000D35D6 File Offset: 0x000D17D6
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format64(format, TraceLoggingDataType.Int64));
		}

		// Token: 0x060036D6 RID: 14038 RVA: 0x000D35E7 File Offset: 0x000D17E7
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<long>.Cast<EnumType>(value));
		}

		// Token: 0x060036D7 RID: 14039 RVA: 0x000D35FA File Offset: 0x000D17FA
		public override object GetData(object value)
		{
			return value;
		}

		// Token: 0x060036D8 RID: 14040 RVA: 0x000D35FD File Offset: 0x000D17FD
		public EnumInt64TypeInfo()
		{
		}
	}
}
