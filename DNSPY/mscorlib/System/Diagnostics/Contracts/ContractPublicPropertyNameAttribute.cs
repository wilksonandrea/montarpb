using System;

namespace System.Diagnostics.Contracts
{
	// Token: 0x02000411 RID: 1041
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Field)]
	[__DynamicallyInvokable]
	public sealed class ContractPublicPropertyNameAttribute : Attribute
	{
		// Token: 0x060033FB RID: 13307 RVA: 0x000C657C File Offset: 0x000C477C
		[__DynamicallyInvokable]
		public ContractPublicPropertyNameAttribute(string name)
		{
			this._publicName = name;
		}

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x060033FC RID: 13308 RVA: 0x000C658B File Offset: 0x000C478B
		[__DynamicallyInvokable]
		public string Name
		{
			[__DynamicallyInvokable]
			get
			{
				return this._publicName;
			}
		}

		// Token: 0x0400170B RID: 5899
		private string _publicName;
	}
}
