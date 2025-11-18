using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A49 RID: 2633
	[__DynamicallyInvokable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct DISPPARAMS
	{
		// Token: 0x04002DCA RID: 11722
		[__DynamicallyInvokable]
		public IntPtr rgvarg;

		// Token: 0x04002DCB RID: 11723
		[__DynamicallyInvokable]
		public IntPtr rgdispidNamedArgs;

		// Token: 0x04002DCC RID: 11724
		[__DynamicallyInvokable]
		public int cArgs;

		// Token: 0x04002DCD RID: 11725
		[__DynamicallyInvokable]
		public int cNamedArgs;
	}
}
