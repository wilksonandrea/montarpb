using System;
using System.Collections;

namespace System.Security.AccessControl
{
	// Token: 0x02000236 RID: 566
	public sealed class AuthorizationRuleCollection : ReadOnlyCollectionBase
	{
		// Token: 0x0600204C RID: 8268 RVA: 0x00071384 File Offset: 0x0006F584
		public AuthorizationRuleCollection()
		{
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x0007138C File Offset: 0x0006F58C
		public void AddRule(AuthorizationRule rule)
		{
			base.InnerList.Add(rule);
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x0007139B File Offset: 0x0006F59B
		public void CopyTo(AuthorizationRule[] rules, int index)
		{
			((ICollection)this).CopyTo(rules, index);
		}

		// Token: 0x170003CA RID: 970
		public AuthorizationRule this[int index]
		{
			get
			{
				return base.InnerList[index] as AuthorizationRule;
			}
		}
	}
}
