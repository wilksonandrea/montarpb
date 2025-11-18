using System;

namespace System.Diagnostics.Contracts
{
	// Token: 0x0200040D RID: 1037
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[__DynamicallyInvokable]
	public sealed class ContractInvariantMethodAttribute : Attribute
	{
		// Token: 0x060033F6 RID: 13302 RVA: 0x000C654D File Offset: 0x000C474D
		[__DynamicallyInvokable]
		public ContractInvariantMethodAttribute()
		{
		}
	}
}
