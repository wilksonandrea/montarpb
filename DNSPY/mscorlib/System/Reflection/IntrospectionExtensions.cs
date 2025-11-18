using System;

namespace System.Reflection
{
	// Token: 0x020005EE RID: 1518
	[__DynamicallyInvokable]
	public static class IntrospectionExtensions
	{
		// Token: 0x06004659 RID: 18009 RVA: 0x00102318 File Offset: 0x00100518
		[__DynamicallyInvokable]
		public static TypeInfo GetTypeInfo(this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			IReflectableType reflectableType = (IReflectableType)type;
			if (reflectableType == null)
			{
				return null;
			}
			return reflectableType.GetTypeInfo();
		}
	}
}
