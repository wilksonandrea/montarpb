using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000933 RID: 2355
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class OptionalAttribute : Attribute
	{
		// Token: 0x0600603B RID: 24635 RVA: 0x0014B943 File Offset: 0x00149B43
		internal static Attribute GetCustomAttribute(RuntimeParameterInfo parameter)
		{
			if (!parameter.IsOptional)
			{
				return null;
			}
			return new OptionalAttribute();
		}

		// Token: 0x0600603C RID: 24636 RVA: 0x0014B954 File Offset: 0x00149B54
		internal static bool IsDefined(RuntimeParameterInfo parameter)
		{
			return parameter.IsOptional;
		}

		// Token: 0x0600603D RID: 24637 RVA: 0x0014B95C File Offset: 0x00149B5C
		[__DynamicallyInvokable]
		public OptionalAttribute()
		{
		}
	}
}
