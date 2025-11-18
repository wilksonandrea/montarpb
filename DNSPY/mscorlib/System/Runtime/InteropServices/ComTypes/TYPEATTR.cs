using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A3F RID: 2623
	[__DynamicallyInvokable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPEATTR
	{
		// Token: 0x04002D89 RID: 11657
		[__DynamicallyInvokable]
		public const int MEMBER_ID_NIL = -1;

		// Token: 0x04002D8A RID: 11658
		[__DynamicallyInvokable]
		public Guid guid;

		// Token: 0x04002D8B RID: 11659
		[__DynamicallyInvokable]
		public int lcid;

		// Token: 0x04002D8C RID: 11660
		[__DynamicallyInvokable]
		public int dwReserved;

		// Token: 0x04002D8D RID: 11661
		[__DynamicallyInvokable]
		public int memidConstructor;

		// Token: 0x04002D8E RID: 11662
		[__DynamicallyInvokable]
		public int memidDestructor;

		// Token: 0x04002D8F RID: 11663
		public IntPtr lpstrSchema;

		// Token: 0x04002D90 RID: 11664
		[__DynamicallyInvokable]
		public int cbSizeInstance;

		// Token: 0x04002D91 RID: 11665
		[__DynamicallyInvokable]
		public TYPEKIND typekind;

		// Token: 0x04002D92 RID: 11666
		[__DynamicallyInvokable]
		public short cFuncs;

		// Token: 0x04002D93 RID: 11667
		[__DynamicallyInvokable]
		public short cVars;

		// Token: 0x04002D94 RID: 11668
		[__DynamicallyInvokable]
		public short cImplTypes;

		// Token: 0x04002D95 RID: 11669
		[__DynamicallyInvokable]
		public short cbSizeVft;

		// Token: 0x04002D96 RID: 11670
		[__DynamicallyInvokable]
		public short cbAlignment;

		// Token: 0x04002D97 RID: 11671
		[__DynamicallyInvokable]
		public TYPEFLAGS wTypeFlags;

		// Token: 0x04002D98 RID: 11672
		[__DynamicallyInvokable]
		public short wMajorVerNum;

		// Token: 0x04002D99 RID: 11673
		[__DynamicallyInvokable]
		public short wMinorVerNum;

		// Token: 0x04002D9A RID: 11674
		[__DynamicallyInvokable]
		public TYPEDESC tdescAlias;

		// Token: 0x04002D9B RID: 11675
		[__DynamicallyInvokable]
		public IDLDESC idldescType;
	}
}
