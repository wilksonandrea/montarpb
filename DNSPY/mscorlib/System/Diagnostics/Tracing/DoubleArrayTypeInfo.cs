using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200046D RID: 1133
	internal sealed class DoubleArrayTypeInfo : TraceLoggingTypeInfo<double[]>
	{
		// Token: 0x060036B7 RID: 14007 RVA: 0x000D347C File Offset: 0x000D167C
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddArray(name, Statics.Format64(format, TraceLoggingDataType.Double));
		}

		// Token: 0x060036B8 RID: 14008 RVA: 0x000D348D File Offset: 0x000D168D
		public override void WriteData(TraceLoggingDataCollector collector, ref double[] value)
		{
			collector.AddArray(value);
		}

		// Token: 0x060036B9 RID: 14009 RVA: 0x000D3497 File Offset: 0x000D1697
		public DoubleArrayTypeInfo()
		{
		}
	}
}
