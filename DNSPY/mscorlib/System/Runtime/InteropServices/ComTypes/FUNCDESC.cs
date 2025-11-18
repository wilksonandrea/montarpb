using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A40 RID: 2624
	[__DynamicallyInvokable]
	public struct FUNCDESC
	{
		// Token: 0x04002D9C RID: 11676
		[__DynamicallyInvokable]
		public int memid;

		// Token: 0x04002D9D RID: 11677
		public IntPtr lprgscode;

		// Token: 0x04002D9E RID: 11678
		public IntPtr lprgelemdescParam;

		// Token: 0x04002D9F RID: 11679
		[__DynamicallyInvokable]
		public FUNCKIND funckind;

		// Token: 0x04002DA0 RID: 11680
		[__DynamicallyInvokable]
		public INVOKEKIND invkind;

		// Token: 0x04002DA1 RID: 11681
		[__DynamicallyInvokable]
		public CALLCONV callconv;

		// Token: 0x04002DA2 RID: 11682
		[__DynamicallyInvokable]
		public short cParams;

		// Token: 0x04002DA3 RID: 11683
		[__DynamicallyInvokable]
		public short cParamsOpt;

		// Token: 0x04002DA4 RID: 11684
		[__DynamicallyInvokable]
		public short oVft;

		// Token: 0x04002DA5 RID: 11685
		[__DynamicallyInvokable]
		public short cScodes;

		// Token: 0x04002DA6 RID: 11686
		[__DynamicallyInvokable]
		public ELEMDESC elemdescFunc;

		// Token: 0x04002DA7 RID: 11687
		[__DynamicallyInvokable]
		public short wFuncFlags;
	}
}
