using System;

namespace System.Diagnostics.Contracts
{
	// Token: 0x0200040A RID: 1034
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Parameter | AttributeTargets.Delegate, AllowMultiple = false, Inherited = true)]
	[__DynamicallyInvokable]
	public sealed class PureAttribute : Attribute
	{
		// Token: 0x060033F1 RID: 13297 RVA: 0x000C6517 File Offset: 0x000C4717
		[__DynamicallyInvokable]
		public PureAttribute()
		{
		}
	}
}
