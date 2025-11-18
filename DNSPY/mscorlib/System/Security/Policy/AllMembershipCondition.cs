using System;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
	// Token: 0x0200033F RID: 831
	[ComVisible(true)]
	[Serializable]
	public sealed class AllMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IReportMatchMembershipCondition
	{
		// Token: 0x06002961 RID: 10593 RVA: 0x00098C98 File Offset: 0x00096E98
		public AllMembershipCondition()
		{
		}

		// Token: 0x06002962 RID: 10594 RVA: 0x00098CA0 File Offset: 0x00096EA0
		public bool Check(Evidence evidence)
		{
			object obj = null;
			return ((IReportMatchMembershipCondition)this).Check(evidence, out obj);
		}

		// Token: 0x06002963 RID: 10595 RVA: 0x00098CB8 File Offset: 0x00096EB8
		bool IReportMatchMembershipCondition.Check(Evidence evidence, out object usedEvidence)
		{
			usedEvidence = null;
			return true;
		}

		// Token: 0x06002964 RID: 10596 RVA: 0x00098CBE File Offset: 0x00096EBE
		public IMembershipCondition Copy()
		{
			return new AllMembershipCondition();
		}

		// Token: 0x06002965 RID: 10597 RVA: 0x00098CC5 File Offset: 0x00096EC5
		public override string ToString()
		{
			return Environment.GetResourceString("All_ToString");
		}

		// Token: 0x06002966 RID: 10598 RVA: 0x00098CD1 File Offset: 0x00096ED1
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x06002967 RID: 10599 RVA: 0x00098CDA File Offset: 0x00096EDA
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		// Token: 0x06002968 RID: 10600 RVA: 0x00098CE4 File Offset: 0x00096EE4
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = new SecurityElement("IMembershipCondition");
			XMLUtil.AddClassAttribute(securityElement, base.GetType(), "System.Security.Policy.AllMembershipCondition");
			securityElement.AddAttribute("version", "1");
			return securityElement;
		}

		// Token: 0x06002969 RID: 10601 RVA: 0x00098D1E File Offset: 0x00096F1E
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (!e.Tag.Equals("IMembershipCondition"))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_MembershipConditionElement"));
			}
		}

		// Token: 0x0600296A RID: 10602 RVA: 0x00098D50 File Offset: 0x00096F50
		public override bool Equals(object o)
		{
			return o is AllMembershipCondition;
		}

		// Token: 0x0600296B RID: 10603 RVA: 0x00098D5B File Offset: 0x00096F5B
		public override int GetHashCode()
		{
			return typeof(AllMembershipCondition).GetHashCode();
		}
	}
}
