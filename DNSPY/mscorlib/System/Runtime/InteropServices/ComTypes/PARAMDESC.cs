using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A44 RID: 2628
	[__DynamicallyInvokable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct PARAMDESC
	{
		// Token: 0x04002DB9 RID: 11705
		public IntPtr lpVarValue;

		// Token: 0x04002DBA RID: 11706
		[__DynamicallyInvokable]
		public PARAMFLAG wParamFlags;
	}
}
