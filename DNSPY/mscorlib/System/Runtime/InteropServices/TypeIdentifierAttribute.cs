using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000912 RID: 2322
	[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	[ComVisible(false)]
	[__DynamicallyInvokable]
	public sealed class TypeIdentifierAttribute : Attribute
	{
		// Token: 0x06005FF6 RID: 24566 RVA: 0x0014B439 File Offset: 0x00149639
		[__DynamicallyInvokable]
		public TypeIdentifierAttribute()
		{
		}

		// Token: 0x06005FF7 RID: 24567 RVA: 0x0014B441 File Offset: 0x00149641
		[__DynamicallyInvokable]
		public TypeIdentifierAttribute(string scope, string identifier)
		{
			this.Scope_ = scope;
			this.Identifier_ = identifier;
		}

		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x06005FF8 RID: 24568 RVA: 0x0014B457 File Offset: 0x00149657
		[__DynamicallyInvokable]
		public string Scope
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Scope_;
			}
		}

		// Token: 0x170010D0 RID: 4304
		// (get) Token: 0x06005FF9 RID: 24569 RVA: 0x0014B45F File Offset: 0x0014965F
		[__DynamicallyInvokable]
		public string Identifier
		{
			[__DynamicallyInvokable]
			get
			{
				return this.Identifier_;
			}
		}

		// Token: 0x04002A6A RID: 10858
		internal string Scope_;

		// Token: 0x04002A6B RID: 10859
		internal string Identifier_;
	}
}
