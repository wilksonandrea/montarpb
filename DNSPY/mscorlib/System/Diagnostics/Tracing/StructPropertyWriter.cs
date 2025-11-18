using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200044E RID: 1102
	internal class StructPropertyWriter<ContainerType, ValueType> : PropertyAccessor<ContainerType>
	{
		// Token: 0x0600365B RID: 13915 RVA: 0x000D2E7C File Offset: 0x000D107C
		public StructPropertyWriter(PropertyAnalysis property)
		{
			this.valueTypeInfo = (TraceLoggingTypeInfo<ValueType>)property.typeInfo;
			this.getter = (StructPropertyWriter<ContainerType, ValueType>.Getter)Statics.CreateDelegate(typeof(StructPropertyWriter<ContainerType, ValueType>.Getter), property.getterInfo);
		}

		// Token: 0x0600365C RID: 13916 RVA: 0x000D2EB8 File Offset: 0x000D10B8
		public override void Write(TraceLoggingDataCollector collector, ref ContainerType container)
		{
			ValueType valueType = ((container == null) ? default(ValueType) : this.getter(ref container));
			this.valueTypeInfo.WriteData(collector, ref valueType);
		}

		// Token: 0x0600365D RID: 13917 RVA: 0x000D2EF8 File Offset: 0x000D10F8
		public override object GetData(ContainerType container)
		{
			return (container == null) ? default(ValueType) : this.getter(ref container);
		}

		// Token: 0x04001853 RID: 6227
		private readonly TraceLoggingTypeInfo<ValueType> valueTypeInfo;

		// Token: 0x04001854 RID: 6228
		private readonly StructPropertyWriter<ContainerType, ValueType>.Getter getter;

		// Token: 0x02000BA0 RID: 2976
		// (Invoke) Token: 0x06006CA7 RID: 27815
		private delegate ValueType Getter(ref ContainerType container);
	}
}
