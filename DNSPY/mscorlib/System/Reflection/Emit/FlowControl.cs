using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200065C RID: 1628
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum FlowControl
	{
		// Token: 0x04002189 RID: 8585
		[__DynamicallyInvokable]
		Branch,
		// Token: 0x0400218A RID: 8586
		[__DynamicallyInvokable]
		Break,
		// Token: 0x0400218B RID: 8587
		[__DynamicallyInvokable]
		Call,
		// Token: 0x0400218C RID: 8588
		[__DynamicallyInvokable]
		Cond_Branch,
		// Token: 0x0400218D RID: 8589
		[__DynamicallyInvokable]
		Meta,
		// Token: 0x0400218E RID: 8590
		[__DynamicallyInvokable]
		Next,
		// Token: 0x0400218F RID: 8591
		[Obsolete("This API has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
		Phi,
		// Token: 0x04002190 RID: 8592
		[__DynamicallyInvokable]
		Return,
		// Token: 0x04002191 RID: 8593
		[__DynamicallyInvokable]
		Throw
	}
}
