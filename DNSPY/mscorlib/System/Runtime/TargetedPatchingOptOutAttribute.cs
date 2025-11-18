using System;

namespace System.Runtime
{
	// Token: 0x0200071B RID: 1819
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public sealed class TargetedPatchingOptOutAttribute : Attribute
	{
		// Token: 0x06005138 RID: 20792 RVA: 0x0011E45E File Offset: 0x0011C65E
		public TargetedPatchingOptOutAttribute(string reason)
		{
			this.m_reason = reason;
		}

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06005139 RID: 20793 RVA: 0x0011E46D File Offset: 0x0011C66D
		public string Reason
		{
			get
			{
				return this.m_reason;
			}
		}

		// Token: 0x0600513A RID: 20794 RVA: 0x0011E475 File Offset: 0x0011C675
		private TargetedPatchingOptOutAttribute()
		{
		}

		// Token: 0x04002404 RID: 9220
		private string m_reason;
	}
}
