using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A42 RID: 2626
	[__DynamicallyInvokable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct IDLDESC
	{
		// Token: 0x04002DAE RID: 11694
		public IntPtr dwReserved;

		// Token: 0x04002DAF RID: 11695
		[__DynamicallyInvokable]
		public IDLFLAG wIDLFlags;
	}
}
