using System;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200043F RID: 1087
	internal sealed class EnumerableTypeInfo<IterableType, ElementType> : TraceLoggingTypeInfo<IterableType> where IterableType : IEnumerable<ElementType>
	{
		// Token: 0x060035F7 RID: 13815 RVA: 0x000D2041 File Offset: 0x000D0241
		public EnumerableTypeInfo(TraceLoggingTypeInfo<ElementType> elementInfo)
		{
			this.elementInfo = elementInfo;
		}

		// Token: 0x060035F8 RID: 13816 RVA: 0x000D2050 File Offset: 0x000D0250
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.BeginBufferedArray();
			this.elementInfo.WriteMetadata(collector, name, format);
			collector.EndBufferedArray();
		}

		// Token: 0x060035F9 RID: 13817 RVA: 0x000D206C File Offset: 0x000D026C
		public override void WriteData(TraceLoggingDataCollector collector, ref IterableType value)
		{
			int num = collector.BeginBufferedArray();
			int num2 = 0;
			if (value != null)
			{
				foreach (ElementType elementType in value)
				{
					ElementType elementType2 = elementType;
					this.elementInfo.WriteData(collector, ref elementType2);
					num2++;
				}
			}
			collector.EndBufferedArray(num, num2);
		}

		// Token: 0x060035FA RID: 13818 RVA: 0x000D20E8 File Offset: 0x000D02E8
		public override object GetData(object value)
		{
			IterableType iterableType = (IterableType)((object)value);
			List<object> list = new List<object>();
			foreach (ElementType elementType in iterableType)
			{
				list.Add(this.elementInfo.GetData(elementType));
			}
			return list.ToArray();
		}

		// Token: 0x0400181C RID: 6172
		private readonly TraceLoggingTypeInfo<ElementType> elementInfo;
	}
}
