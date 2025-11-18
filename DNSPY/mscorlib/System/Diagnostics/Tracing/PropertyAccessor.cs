using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200044C RID: 1100
	internal abstract class PropertyAccessor<ContainerType>
	{
		// Token: 0x06003654 RID: 13908
		public abstract void Write(TraceLoggingDataCollector collector, ref ContainerType value);

		// Token: 0x06003655 RID: 13909
		public abstract object GetData(ContainerType value);

		// Token: 0x06003656 RID: 13910 RVA: 0x000D2D78 File Offset: 0x000D0F78
		public static PropertyAccessor<ContainerType> Create(PropertyAnalysis property)
		{
			Type returnType = property.getterInfo.ReturnType;
			if (!Statics.IsValueType(typeof(ContainerType)))
			{
				if (returnType == typeof(int))
				{
					return new ClassPropertyWriter<ContainerType, int>(property);
				}
				if (returnType == typeof(long))
				{
					return new ClassPropertyWriter<ContainerType, long>(property);
				}
				if (returnType == typeof(string))
				{
					return new ClassPropertyWriter<ContainerType, string>(property);
				}
			}
			return new NonGenericProperytWriter<ContainerType>(property);
		}

		// Token: 0x06003657 RID: 13911 RVA: 0x000D2DF3 File Offset: 0x000D0FF3
		protected PropertyAccessor()
		{
		}
	}
}
