using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A37 RID: 2615
	[__DynamicallyInvokable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct STATSTG
	{
		// Token: 0x04002D55 RID: 11605
		[__DynamicallyInvokable]
		public string pwcsName;

		// Token: 0x04002D56 RID: 11606
		[__DynamicallyInvokable]
		public int type;

		// Token: 0x04002D57 RID: 11607
		[__DynamicallyInvokable]
		public long cbSize;

		// Token: 0x04002D58 RID: 11608
		[__DynamicallyInvokable]
		public FILETIME mtime;

		// Token: 0x04002D59 RID: 11609
		[__DynamicallyInvokable]
		public FILETIME ctime;

		// Token: 0x04002D5A RID: 11610
		[__DynamicallyInvokable]
		public FILETIME atime;

		// Token: 0x04002D5B RID: 11611
		[__DynamicallyInvokable]
		public int grfMode;

		// Token: 0x04002D5C RID: 11612
		[__DynamicallyInvokable]
		public int grfLocksSupported;

		// Token: 0x04002D5D RID: 11613
		[__DynamicallyInvokable]
		public Guid clsid;

		// Token: 0x04002D5E RID: 11614
		[__DynamicallyInvokable]
		public int grfStateBits;

		// Token: 0x04002D5F RID: 11615
		[__DynamicallyInvokable]
		public int reserved;
	}
}
