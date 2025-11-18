using System;

namespace System.Diagnostics.Contracts
{
	// Token: 0x02000413 RID: 1043
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	[Conditional("CONTRACTS_FULL")]
	[__DynamicallyInvokable]
	public sealed class ContractAbbreviatorAttribute : Attribute
	{
		// Token: 0x060033FE RID: 13310 RVA: 0x000C659B File Offset: 0x000C479B
		[__DynamicallyInvokable]
		public ContractAbbreviatorAttribute()
		{
		}
	}
}
