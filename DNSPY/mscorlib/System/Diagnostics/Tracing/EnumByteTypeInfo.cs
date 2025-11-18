using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200046F RID: 1135
	internal sealed class EnumByteTypeInfo<EnumType> : TraceLoggingTypeInfo<EnumType>
	{
		// Token: 0x060036BD RID: 14013 RVA: 0x000D34C2 File Offset: 0x000D16C2
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.Format8(format, TraceLoggingDataType.UInt8));
		}

		// Token: 0x060036BE RID: 14014 RVA: 0x000D34D2 File Offset: 0x000D16D2
		public override void WriteData(TraceLoggingDataCollector collector, ref EnumType value)
		{
			collector.AddScalar(EnumHelper<byte>.Cast<EnumType>(value));
		}

		// Token: 0x060036BF RID: 14015 RVA: 0x000D34E5 File Offset: 0x000D16E5
		public override object GetData(object value)
		{
			return value;
		}

		// Token: 0x060036C0 RID: 14016 RVA: 0x000D34E8 File Offset: 0x000D16E8
		public EnumByteTypeInfo()
		{
		}
	}
}
