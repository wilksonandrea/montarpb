using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200044F RID: 1103
	internal class ClassPropertyWriter<ContainerType, ValueType> : PropertyAccessor<ContainerType>
	{
		// Token: 0x0600365E RID: 13918 RVA: 0x000D2F2A File Offset: 0x000D112A
		public ClassPropertyWriter(PropertyAnalysis property)
		{
			this.valueTypeInfo = (TraceLoggingTypeInfo<ValueType>)property.typeInfo;
			this.getter = (ClassPropertyWriter<ContainerType, ValueType>.Getter)Statics.CreateDelegate(typeof(ClassPropertyWriter<ContainerType, ValueType>.Getter), property.getterInfo);
		}

		// Token: 0x0600365F RID: 13919 RVA: 0x000D2F64 File Offset: 0x000D1164
		public override void Write(TraceLoggingDataCollector collector, ref ContainerType container)
		{
			ValueType valueType = ((container == null) ? default(ValueType) : this.getter(container));
			this.valueTypeInfo.WriteData(collector, ref valueType);
		}

		// Token: 0x06003660 RID: 13920 RVA: 0x000D2FAC File Offset: 0x000D11AC
		public override object GetData(ContainerType container)
		{
			return (container == null) ? default(ValueType) : this.getter(container);
		}

		// Token: 0x04001855 RID: 6229
		private readonly TraceLoggingTypeInfo<ValueType> valueTypeInfo;

		// Token: 0x04001856 RID: 6230
		private readonly ClassPropertyWriter<ContainerType, ValueType>.Getter getter;

		// Token: 0x02000BA1 RID: 2977
		// (Invoke) Token: 0x06006CAB RID: 27819
		private delegate ValueType Getter(ContainerType container);
	}
}
