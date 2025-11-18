using System;
using System.Reflection;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200044D RID: 1101
	internal class NonGenericProperytWriter<ContainerType> : PropertyAccessor<ContainerType>
	{
		// Token: 0x06003658 RID: 13912 RVA: 0x000D2DFB File Offset: 0x000D0FFB
		public NonGenericProperytWriter(PropertyAnalysis property)
		{
			this.getterInfo = property.getterInfo;
			this.typeInfo = property.typeInfo;
		}

		// Token: 0x06003659 RID: 13913 RVA: 0x000D2E1C File Offset: 0x000D101C
		public override void Write(TraceLoggingDataCollector collector, ref ContainerType container)
		{
			object obj = ((container == null) ? null : this.getterInfo.Invoke(container, null));
			this.typeInfo.WriteObjectData(collector, obj);
		}

		// Token: 0x0600365A RID: 13914 RVA: 0x000D2E5E File Offset: 0x000D105E
		public override object GetData(ContainerType container)
		{
			if (container != null)
			{
				return this.getterInfo.Invoke(container, null);
			}
			return null;
		}

		// Token: 0x04001851 RID: 6225
		private readonly TraceLoggingTypeInfo typeInfo;

		// Token: 0x04001852 RID: 6226
		private readonly MethodInfo getterInfo;
	}
}
