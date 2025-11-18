using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A45 RID: 2629
	[__DynamicallyInvokable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPEDESC
	{
		// Token: 0x04002DBB RID: 11707
		public IntPtr lpValue;

		// Token: 0x04002DBC RID: 11708
		[__DynamicallyInvokable]
		public short vt;
	}
}
