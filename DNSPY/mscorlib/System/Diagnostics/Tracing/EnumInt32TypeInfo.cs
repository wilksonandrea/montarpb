using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000473 RID: 1139
	internal sealed class EnumInt32TypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x060036CD RID: 14029 RVA: 0x000D357A File Offset: 0x000D177A
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format32(format, TraceLoggingDataType.Int32));
		}

		// Token: 0x060036CE RID: 14030 RVA: 0x000D358A File Offset: 0x000D178A
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<int>.Cast<EnumType>(value));
		}

		// Token: 0x060036CF RID: 14031 RVA: 0x000D359D File Offset: 0x000D179D
		public override object GetData(object value)
		{
			return value;
		}

		// Token: 0x060036D0 RID: 14032 RVA: 0x000D35A0 File Offset: 0x000D17A0
		public EnumInt32TypeInfo()
		{
		}
	}
}
