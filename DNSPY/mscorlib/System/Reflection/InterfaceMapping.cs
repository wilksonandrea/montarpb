using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x020005F0 RID: 1520
	[ComVisible(true)]
	[__DynamicallyInvokable]
	public struct InterfaceMapping
	{
		// Token: 0x04001CD7 RID: 7383
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public Type TargetType;

		// Token: 0x04001CD8 RID: 7384
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public Type InterfaceType;

		// Token: 0x04001CD9 RID: 7385
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public MethodInfo[] TargetMethods;

		// Token: 0x04001CDA RID: 7386
		[ComVisible(true)]
		[__DynamicallyInvokable]
		public MethodInfo[] InterfaceMethods;
	}
}
