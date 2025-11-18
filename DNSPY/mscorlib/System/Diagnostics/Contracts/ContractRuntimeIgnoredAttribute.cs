using System;

namespace System.Diagnostics.Contracts
{
	// Token: 0x0200040F RID: 1039
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	[__DynamicallyInvokable]
	public sealed class ContractRuntimeIgnoredAttribute : Attribute
	{
		// Token: 0x060033F8 RID: 13304 RVA: 0x000C655D File Offset: 0x000C475D
		[__DynamicallyInvokable]
		public ContractRuntimeIgnoredAttribute()
		{
		}
	}
}
