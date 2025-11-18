using System;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000930 RID: 2352
	[AttributeUsage(AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public sealed class PreserveSigAttribute : Attribute
	{
		// Token: 0x06006032 RID: 24626 RVA: 0x0014B8D1 File Offset: 0x00149AD1
		internal static Attribute GetCustomAttribute(RuntimeMethodInfo method)
		{
			if ((method.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) == MethodImplAttributes.IL)
			{
				return null;
			}
			return new PreserveSigAttribute();
		}

		// Token: 0x06006033 RID: 24627 RVA: 0x0014B8E8 File Offset: 0x00149AE8
		internal static bool IsDefined(RuntimeMethodInfo method)
		{
			return (method.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) > MethodImplAttributes.IL;
		}

		// Token: 0x06006034 RID: 24628 RVA: 0x0014B8F9 File Offset: 0x00149AF9
		[__DynamicallyInvokable]
		public PreserveSigAttribute()
		{
		}
	}
}
