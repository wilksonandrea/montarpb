using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A2C RID: 2604
	[__DynamicallyInvokable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct CONNECTDATA
	{
		// Token: 0x04002D51 RID: 11601
		[__DynamicallyInvokable]
		[MarshalAs(UnmanagedType.Interface)]
		public object pUnk;

		// Token: 0x04002D52 RID: 11602
		[__DynamicallyInvokable]
		public int dwCookie;
	}
}
