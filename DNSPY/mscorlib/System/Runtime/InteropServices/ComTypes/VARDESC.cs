using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A48 RID: 2632
	[__DynamicallyInvokable]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct VARDESC
	{
		// Token: 0x04002DC4 RID: 11716
		[__DynamicallyInvokable]
		public int memid;

		// Token: 0x04002DC5 RID: 11717
		[__DynamicallyInvokable]
		public string lpstrSchema;

		// Token: 0x04002DC6 RID: 11718
		[__DynamicallyInvokable]
		public VARDESC.DESCUNION desc;

		// Token: 0x04002DC7 RID: 11719
		[__DynamicallyInvokable]
		public ELEMDESC elemdescVar;

		// Token: 0x04002DC8 RID: 11720
		[__DynamicallyInvokable]
		public short wVarFlags;

		// Token: 0x04002DC9 RID: 11721
		[__DynamicallyInvokable]
		public VARKIND varkind;

		// Token: 0x02000CAD RID: 3245
		[__DynamicallyInvokable]
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		public struct DESCUNION
		{
			// Token: 0x04003896 RID: 14486
			[__DynamicallyInvokable]
			[FieldOffset(0)]
			public int oInst;

			// Token: 0x04003897 RID: 14487
			[FieldOffset(0)]
			public IntPtr lpvarValue;
		}
	}
}
