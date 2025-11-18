using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A4A RID: 2634
	[__DynamicallyInvokable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct EXCEPINFO
	{
		// Token: 0x04002DCE RID: 11726
		[__DynamicallyInvokable]
		public short wCode;

		// Token: 0x04002DCF RID: 11727
		[__DynamicallyInvokable]
		public short wReserved;

		// Token: 0x04002DD0 RID: 11728
		[__DynamicallyInvokable]
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrSource;

		// Token: 0x04002DD1 RID: 11729
		[__DynamicallyInvokable]
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrDescription;

		// Token: 0x04002DD2 RID: 11730
		[__DynamicallyInvokable]
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrHelpFile;

		// Token: 0x04002DD3 RID: 11731
		[__DynamicallyInvokable]
		public int dwHelpContext;

		// Token: 0x04002DD4 RID: 11732
		public IntPtr pvReserved;

		// Token: 0x04002DD5 RID: 11733
		public IntPtr pfnDeferredFillIn;

		// Token: 0x04002DD6 RID: 11734
		[__DynamicallyInvokable]
		public int scode;
	}
}
