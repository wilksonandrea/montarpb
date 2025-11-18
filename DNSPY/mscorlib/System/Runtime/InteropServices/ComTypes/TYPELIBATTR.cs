using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A53 RID: 2643
	[__DynamicallyInvokable]
	[Serializable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPELIBATTR
	{
		// Token: 0x04002E13 RID: 11795
		[__DynamicallyInvokable]
		public Guid guid;

		// Token: 0x04002E14 RID: 11796
		[__DynamicallyInvokable]
		public int lcid;

		// Token: 0x04002E15 RID: 11797
		[__DynamicallyInvokable]
		public SYSKIND syskind;

		// Token: 0x04002E16 RID: 11798
		[__DynamicallyInvokable]
		public short wMajorVerNum;

		// Token: 0x04002E17 RID: 11799
		[__DynamicallyInvokable]
		public short wMinorVerNum;

		// Token: 0x04002E18 RID: 11800
		[__DynamicallyInvokable]
		public LIBFLAGS wLibFlags;
	}
}
