using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	// Token: 0x020003EA RID: 1002
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, Inherited = false)]
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public sealed class DebuggerNonUserCodeAttribute : Attribute
	{
		// Token: 0x0600330E RID: 13070 RVA: 0x000C49E8 File Offset: 0x000C2BE8
		[__DynamicallyInvokable]
		public DebuggerNonUserCodeAttribute()
		{
		}
	}
}
