using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	// Token: 0x020003E8 RID: 1000
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class DebuggerStepperBoundaryAttribute : Attribute
	{
		// Token: 0x0600330C RID: 13068 RVA: 0x000C49D8 File Offset: 0x000C2BD8
		public DebuggerStepperBoundaryAttribute()
		{
		}
	}
}
