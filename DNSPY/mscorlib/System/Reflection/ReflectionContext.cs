using System;

namespace System.Reflection
{
	// Token: 0x0200061F RID: 1567
	[__DynamicallyInvokable]
	public abstract class ReflectionContext
	{
		// Token: 0x060048A4 RID: 18596 RVA: 0x001072EA File Offset: 0x001054EA
		[__DynamicallyInvokable]
		protected ReflectionContext()
		{
		}

		// Token: 0x060048A5 RID: 18597
		[__DynamicallyInvokable]
		public abstract Assembly MapAssembly(Assembly assembly);

		// Token: 0x060048A6 RID: 18598
		[__DynamicallyInvokable]
		public abstract TypeInfo MapType(TypeInfo type);

		// Token: 0x060048A7 RID: 18599 RVA: 0x001072F2 File Offset: 0x001054F2
		[__DynamicallyInvokable]
		public virtual TypeInfo GetTypeForObject(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return this.MapType(value.GetType().GetTypeInfo());
		}
	}
}
