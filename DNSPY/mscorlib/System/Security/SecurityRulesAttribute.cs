using System;

namespace System.Security
{
	// Token: 0x020001CC RID: 460
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	public sealed class SecurityRulesAttribute : Attribute
	{
		// Token: 0x06001C28 RID: 7208 RVA: 0x00060C31 File Offset: 0x0005EE31
		public SecurityRulesAttribute(SecurityRuleSet ruleSet)
		{
			this.m_ruleSet = ruleSet;
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06001C29 RID: 7209 RVA: 0x00060C40 File Offset: 0x0005EE40
		// (set) Token: 0x06001C2A RID: 7210 RVA: 0x00060C48 File Offset: 0x0005EE48
		public bool SkipVerificationInFullTrust
		{
			get
			{
				return this.m_skipVerificationInFullTrust;
			}
			set
			{
				this.m_skipVerificationInFullTrust = value;
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06001C2B RID: 7211 RVA: 0x00060C51 File Offset: 0x0005EE51
		public SecurityRuleSet RuleSet
		{
			get
			{
				return this.m_ruleSet;
			}
		}

		// Token: 0x040009CB RID: 2507
		private SecurityRuleSet m_ruleSet;

		// Token: 0x040009CC RID: 2508
		private bool m_skipVerificationInFullTrust;
	}
}
