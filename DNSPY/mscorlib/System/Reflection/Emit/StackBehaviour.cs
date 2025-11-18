using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200065A RID: 1626
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum StackBehaviour
	{
		// Token: 0x04002158 RID: 8536
		[__DynamicallyInvokable]
		Pop0,
		// Token: 0x04002159 RID: 8537
		[__DynamicallyInvokable]
		Pop1,
		// Token: 0x0400215A RID: 8538
		[__DynamicallyInvokable]
		Pop1_pop1,
		// Token: 0x0400215B RID: 8539
		[__DynamicallyInvokable]
		Popi,
		// Token: 0x0400215C RID: 8540
		[__DynamicallyInvokable]
		Popi_pop1,
		// Token: 0x0400215D RID: 8541
		[__DynamicallyInvokable]
		Popi_popi,
		// Token: 0x0400215E RID: 8542
		[__DynamicallyInvokable]
		Popi_popi8,
		// Token: 0x0400215F RID: 8543
		[__DynamicallyInvokable]
		Popi_popi_popi,
		// Token: 0x04002160 RID: 8544
		[__DynamicallyInvokable]
		Popi_popr4,
		// Token: 0x04002161 RID: 8545
		[__DynamicallyInvokable]
		Popi_popr8,
		// Token: 0x04002162 RID: 8546
		[__DynamicallyInvokable]
		Popref,
		// Token: 0x04002163 RID: 8547
		[__DynamicallyInvokable]
		Popref_pop1,
		// Token: 0x04002164 RID: 8548
		[__DynamicallyInvokable]
		Popref_popi,
		// Token: 0x04002165 RID: 8549
		[__DynamicallyInvokable]
		Popref_popi_popi,
		// Token: 0x04002166 RID: 8550
		[__DynamicallyInvokable]
		Popref_popi_popi8,
		// Token: 0x04002167 RID: 8551
		[__DynamicallyInvokable]
		Popref_popi_popr4,
		// Token: 0x04002168 RID: 8552
		[__DynamicallyInvokable]
		Popref_popi_popr8,
		// Token: 0x04002169 RID: 8553
		[__DynamicallyInvokable]
		Popref_popi_popref,
		// Token: 0x0400216A RID: 8554
		[__DynamicallyInvokable]
		Push0,
		// Token: 0x0400216B RID: 8555
		[__DynamicallyInvokable]
		Push1,
		// Token: 0x0400216C RID: 8556
		[__DynamicallyInvokable]
		Push1_push1,
		// Token: 0x0400216D RID: 8557
		[__DynamicallyInvokable]
		Pushi,
		// Token: 0x0400216E RID: 8558
		[__DynamicallyInvokable]
		Pushi8,
		// Token: 0x0400216F RID: 8559
		[__DynamicallyInvokable]
		Pushr4,
		// Token: 0x04002170 RID: 8560
		[__DynamicallyInvokable]
		Pushr8,
		// Token: 0x04002171 RID: 8561
		[__DynamicallyInvokable]
		Pushref,
		// Token: 0x04002172 RID: 8562
		[__DynamicallyInvokable]
		Varpop,
		// Token: 0x04002173 RID: 8563
		[__DynamicallyInvokable]
		Varpush,
		// Token: 0x04002174 RID: 8564
		[__DynamicallyInvokable]
		Popref_popi_pop1
	}
}
