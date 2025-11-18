using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000931 RID: 2353
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class InAttribute : Attribute
	{
		// Token: 0x06006035 RID: 24629 RVA: 0x0014B901 File Offset: 0x00149B01
		internal static Attribute GetCustomAttribute(RuntimeParameterInfo parameter)
		{
			if (!parameter.IsIn)
			{
				return null;
			}
			return new InAttribute();
		}

		// Token: 0x06006036 RID: 24630 RVA: 0x0014B912 File Offset: 0x00149B12
		internal static bool IsDefined(RuntimeParameterInfo parameter)
		{
			return parameter.IsIn;
		}

		// Token: 0x06006037 RID: 24631 RVA: 0x0014B91A File Offset: 0x00149B1A
		[__DynamicallyInvokable]
		public InAttribute()
		{
		}
	}
}
