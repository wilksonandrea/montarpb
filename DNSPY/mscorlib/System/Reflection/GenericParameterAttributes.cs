using System;

namespace System.Reflection
{
	// Token: 0x020005EB RID: 1515
	[Flags]
	[__DynamicallyInvokable]
	public enum GenericParameterAttributes
	{
		// Token: 0x04001CCE RID: 7374
		[__DynamicallyInvokable]
		None = 0,
		// Token: 0x04001CCF RID: 7375
		[__DynamicallyInvokable]
		VarianceMask = 3,
		// Token: 0x04001CD0 RID: 7376
		[__DynamicallyInvokable]
		Covariant = 1,
		// Token: 0x04001CD1 RID: 7377
		[__DynamicallyInvokable]
		Contravariant = 2,
		// Token: 0x04001CD2 RID: 7378
		[__DynamicallyInvokable]
		SpecialConstraintMask = 28,
		// Token: 0x04001CD3 RID: 7379
		[__DynamicallyInvokable]
		ReferenceTypeConstraint = 4,
		// Token: 0x04001CD4 RID: 7380
		[__DynamicallyInvokable]
		NotNullableValueTypeConstraint = 8,
		// Token: 0x04001CD5 RID: 7381
		[__DynamicallyInvokable]
		DefaultConstructorConstraint = 16
	}
}
