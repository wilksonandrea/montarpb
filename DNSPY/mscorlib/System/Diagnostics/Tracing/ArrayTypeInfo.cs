using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200043A RID: 1082
	internal sealed class ArrayTypeInfo<ElementType> : TraceLoggingTypeInfo<ElementType[]>
	{
		// Token: 0x060035DD RID: 13789 RVA: 0x000D19DF File Offset: 0x000CFBDF
		public ArrayTypeInfo(TraceLoggingTypeInfo<ElementType> elementInfo)
		{
			this.elementInfo = elementInfo;
		}

		// Token: 0x060035DE RID: 13790 RVA: 0x000D19EE File Offset: 0x000CFBEE
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.BeginBufferedArray();
			this.elementInfo.WriteMetadata(collector, name, format);
			collector.EndBufferedArray();
		}

		// Token: 0x060035DF RID: 13791 RVA: 0x000D1A0C File Offset: 0x000CFC0C
		public override void WriteData(TraceLoggingDataCollector collector, ref ElementType[] value)
		{
			int num = collector.BeginBufferedArray();
			int num2 = 0;
			if (value != null)
			{
				num2 = value.Length;
				for (int i = 0; i < value.Length; i++)
				{
					this.elementInfo.WriteData(collector, ref value[i]);
				}
			}
			collector.EndBufferedArray(num, num2);
		}

		// Token: 0x060035E0 RID: 13792 RVA: 0x000D1A58 File Offset: 0x000CFC58
		public override object GetData(object value)
		{
			ElementType[] array = (ElementType[])value;
			object[] array2 = new object[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = this.elementInfo.GetData(array[i]);
			}
			return array2;
		}

		// Token: 0x0400180E RID: 6158
		private readonly TraceLoggingTypeInfo<ElementType> elementInfo;
	}
}
