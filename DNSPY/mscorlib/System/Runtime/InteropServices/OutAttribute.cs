using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000932 RID: 2354
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class OutAttribute : Attribute
	{
		// Token: 0x06006038 RID: 24632 RVA: 0x0014B922 File Offset: 0x00149B22
		internal static Attribute GetCustomAttribute(RuntimeParameterInfo parameter)
		{
			if (!parameter.IsOut)
			{
				return null;
			}
			return new OutAttribute();
		}

		// Token: 0x06006039 RID: 24633 RVA: 0x0014B933 File Offset: 0x00149B33
		internal static bool IsDefined(RuntimeParameterInfo parameter)
		{
			return parameter.IsOut;
		}

		// Token: 0x0600603A RID: 24634 RVA: 0x0014B93B File Offset: 0x00149B3B
		[__DynamicallyInvokable]
		public OutAttribute()
		{
		}
	}
}
