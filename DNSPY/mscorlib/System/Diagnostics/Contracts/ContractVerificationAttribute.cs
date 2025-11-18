using System;

namespace System.Diagnostics.Contracts
{
	// Token: 0x02000410 RID: 1040
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property)]
	[__DynamicallyInvokable]
	public sealed class ContractVerificationAttribute : Attribute
	{
		// Token: 0x060033F9 RID: 13305 RVA: 0x000C6565 File Offset: 0x000C4765
		[__DynamicallyInvokable]
		public ContractVerificationAttribute(bool value)
		{
			this._value = value;
		}

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x060033FA RID: 13306 RVA: 0x000C6574 File Offset: 0x000C4774
		[__DynamicallyInvokable]
		public bool Value
		{
			[__DynamicallyInvokable]
			get
			{
				return this._value;
			}
		}

		// Token: 0x0400170A RID: 5898
		private bool _value;
	}
}
