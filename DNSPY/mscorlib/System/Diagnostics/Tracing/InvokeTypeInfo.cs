using System;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200044A RID: 1098
	internal sealed class InvokeTypeInfo<ContainerType> : TraceLoggingTypeInfo<ContainerType>
	{
		// Token: 0x06003649 RID: 13897 RVA: 0x000D2A7C File Offset: 0x000D0C7C
		public InvokeTypeInfo(TypeAnalysis typeAnalysis)
			: base(typeAnalysis.name, typeAnalysis.level, typeAnalysis.opcode, typeAnalysis.keywords, typeAnalysis.tags)
		{
			if (typeAnalysis.properties.Length != 0)
			{
				this.properties = typeAnalysis.properties;
				this.accessors = new PropertyAccessor<ContainerType>[this.properties.Length];
				for (int i = 0; i < this.accessors.Length; i++)
				{
					this.accessors[i] = PropertyAccessor<ContainerType>.Create(this.properties[i]);
				}
			}
		}

		// Token: 0x0600364A RID: 13898 RVA: 0x000D2B00 File Offset: 0x000D0D00
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			TraceLoggingMetadataCollector traceLoggingMetadataCollector = collector.AddGroup(name);
			if (this.properties != null)
			{
				foreach (PropertyAnalysis propertyAnalysis in this.properties)
				{
					EventFieldFormat eventFieldFormat = EventFieldFormat.Default;
					EventFieldAttribute fieldAttribute = propertyAnalysis.fieldAttribute;
					if (fieldAttribute != null)
					{
						traceLoggingMetadataCollector.Tags = fieldAttribute.Tags;
						eventFieldFormat = fieldAttribute.Format;
					}
					propertyAnalysis.typeInfo.WriteMetadata(traceLoggingMetadataCollector, propertyAnalysis.name, eventFieldFormat);
				}
			}
		}

		// Token: 0x0600364B RID: 13899 RVA: 0x000D2B70 File Offset: 0x000D0D70
		public override void WriteData(TraceLoggingDataCollector collector, ref ContainerType value)
		{
			if (this.accessors != null)
			{
				foreach (PropertyAccessor<ContainerType> propertyAccessor in this.accessors)
				{
					propertyAccessor.Write(collector, ref value);
				}
			}
		}

		// Token: 0x0600364C RID: 13900 RVA: 0x000D2BA8 File Offset: 0x000D0DA8
		public override object GetData(object value)
		{
			if (this.properties != null)
			{
				List<string> list = new List<string>();
				List<object> list2 = new List<object>();
				for (int i = 0; i < this.properties.Length; i++)
				{
					object data = this.accessors[i].GetData((ContainerType)((object)value));
					list.Add(this.properties[i].name);
					list2.Add(this.properties[i].typeInfo.GetData(data));
				}
				return new EventPayload(list, list2);
			}
			return null;
		}

		// Token: 0x0600364D RID: 13901 RVA: 0x000D2C28 File Offset: 0x000D0E28
		public override void WriteObjectData(TraceLoggingDataCollector collector, object valueObj)
		{
			if (this.accessors != null)
			{
				ContainerType containerType = ((valueObj == null) ? default(ContainerType) : ((ContainerType)((object)valueObj)));
				this.WriteData(collector, ref containerType);
			}
		}

		// Token: 0x0400184A RID: 6218
		private readonly PropertyAnalysis[] properties;

		// Token: 0x0400184B RID: 6219
		private readonly PropertyAccessor<ContainerType>[] accessors;
	}
}
