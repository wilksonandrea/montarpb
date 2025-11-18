using System;

namespace System.Reflection.Emit
{
	// Token: 0x02000637 RID: 1591
	internal sealed class GenericMethodInfo
	{
		// Token: 0x06004A34 RID: 18996 RVA: 0x0010C4C5 File Offset: 0x0010A6C5
		internal GenericMethodInfo(RuntimeMethodHandle methodHandle, RuntimeTypeHandle context)
		{
			this.m_methodHandle = methodHandle;
			this.m_context = context;
		}

		// Token: 0x04001E9E RID: 7838
		internal RuntimeMethodHandle m_methodHandle;

		// Token: 0x04001E9F RID: 7839
		internal RuntimeTypeHandle m_context;
	}
}
