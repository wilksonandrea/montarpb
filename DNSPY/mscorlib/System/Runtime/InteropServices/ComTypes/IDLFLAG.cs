using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x02000A41 RID: 2625
	[Flags]
	[__DynamicallyInvokable]
	[Serializable]
	public enum IDLFLAG : short
	{
		// Token: 0x04002DA9 RID: 11689
		[__DynamicallyInvokable]
		IDLFLAG_NONE = 0,
		// Token: 0x04002DAA RID: 11690
		[__DynamicallyInvokable]
		IDLFLAG_FIN = 1,
		// Token: 0x04002DAB RID: 11691
		[__DynamicallyInvokable]
		IDLFLAG_FOUT = 2,
		// Token: 0x04002DAC RID: 11692
		[__DynamicallyInvokable]
		IDLFLAG_FLCID = 4,
		// Token: 0x04002DAD RID: 11693
		[__DynamicallyInvokable]
		IDLFLAG_FRETVAL = 8
	}
}
