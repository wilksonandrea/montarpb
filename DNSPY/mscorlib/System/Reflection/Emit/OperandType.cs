using System;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x0200065B RID: 1627
	[ComVisible(true)]
	[__DynamicallyInvokable]
	[Serializable]
	public enum OperandType
	{
		// Token: 0x04002176 RID: 8566
		[__DynamicallyInvokable]
		InlineBrTarget,
		// Token: 0x04002177 RID: 8567
		[__DynamicallyInvokable]
		InlineField,
		// Token: 0x04002178 RID: 8568
		[__DynamicallyInvokable]
		InlineI,
		// Token: 0x04002179 RID: 8569
		[__DynamicallyInvokable]
		InlineI8,
		// Token: 0x0400217A RID: 8570
		[__DynamicallyInvokable]
		InlineMethod,
		// Token: 0x0400217B RID: 8571
		[__DynamicallyInvokable]
		InlineNone,
		// Token: 0x0400217C RID: 8572
		[Obsolete("This API has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
		InlinePhi,
		// Token: 0x0400217D RID: 8573
		[__DynamicallyInvokable]
		InlineR,
		// Token: 0x0400217E RID: 8574
		[__DynamicallyInvokable]
		InlineSig = 9,
		// Token: 0x0400217F RID: 8575
		[__DynamicallyInvokable]
		InlineString,
		// Token: 0x04002180 RID: 8576
		[__DynamicallyInvokable]
		InlineSwitch,
		// Token: 0x04002181 RID: 8577
		[__DynamicallyInvokable]
		InlineTok,
		// Token: 0x04002182 RID: 8578
		[__DynamicallyInvokable]
		InlineType,
		// Token: 0x04002183 RID: 8579
		[__DynamicallyInvokable]
		InlineVar,
		// Token: 0x04002184 RID: 8580
		[__DynamicallyInvokable]
		ShortInlineBrTarget,
		// Token: 0x04002185 RID: 8581
		[__DynamicallyInvokable]
		ShortInlineI,
		// Token: 0x04002186 RID: 8582
		[__DynamicallyInvokable]
		ShortInlineR,
		// Token: 0x04002187 RID: 8583
		[__DynamicallyInvokable]
		ShortInlineVar
	}
}
