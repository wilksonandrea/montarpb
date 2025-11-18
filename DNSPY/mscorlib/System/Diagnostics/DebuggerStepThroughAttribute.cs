using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	// Token: 0x020003E7 RID: 999
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class DebuggerStepThroughAttribute : Attribute
	{
		// Token: 0x0600330B RID: 13067 RVA: 0x000C49D0 File Offset: 0x000C2BD0
		[__DynamicallyInvokable]
		public DebuggerStepThroughAttribute()
		{
		}
	}
}
