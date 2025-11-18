using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A3A RID: 2618
	[__DynamicallyInvokable]
	[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
	public struct BINDPTR
	{
		// Token: 0x04002D67 RID: 11623
		[FieldOffset(0)]
		public IntPtr lpfuncdesc;

		// Token: 0x04002D68 RID: 11624
		[FieldOffset(0)]
		public IntPtr lpvardesc;

		// Token: 0x04002D69 RID: 11625
		[FieldOffset(0)]
		public IntPtr lptcomp;
	}
}
