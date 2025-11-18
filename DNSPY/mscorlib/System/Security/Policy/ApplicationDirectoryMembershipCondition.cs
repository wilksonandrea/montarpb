using System;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
	// Token: 0x02000342 RID: 834
	[ComVisible(true)]
	[Serializable]
	public sealed class ApplicationDirectoryMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IReportMatchMembershipCondition
	{
		// Token: 0x06002979 RID: 10617 RVA: 0x00098EF5 File Offset: 0x000970F5
		public ApplicationDirectoryMembershipCondition()
		{
		}

		// Token: 0x0600297A RID: 10618 RVA: 0x00098F00 File Offset: 0x00097100
		public bool Check(Evidence evidence)
		{
			object obj = null;
			return ((IReportMatchMembershipCondition)this).Check(evidence, out obj);
		}

		// Token: 0x0600297B RID: 10619 RVA: 0x00098F18 File Offset: 0x00097118
		bool IReportMatchMembershipCondition.Check(Evidence evidence, out object usedEvidence)
		{
			usedEvidence = null;
			if (evidence == null)
			{
				return false;
			}
			ApplicationDirectory hostEvidence = evidence.GetHostEvidence<ApplicationDirectory>();
			Url hostEvidence2 = evidence.GetHostEvidence<Url>();
			if (hostEvidence != null && hostEvidence2 != null)
			{
				string text = hostEvidence.Directory;
				if (text != null && text.Length > 1)
				{
					if (text[text.Length - 1] == '/')
					{
						text += "*";
					}
					else
					{
						text += "/*";
					}
					URLString urlstring = new URLString(text);
					if (hostEvidence2.GetURLString().IsSubsetOf(urlstring))
					{
						usedEvidence = hostEvidence;
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600297C RID: 10620 RVA: 0x00098F9B File Offset: 0x0009719B
		public IMembershipCondition Copy()
		{
			return new ApplicationDirectoryMembershipCondition();
		}

		// Token: 0x0600297D RID: 10621 RVA: 0x00098FA2 File Offset: 0x000971A2
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x0600297E RID: 10622 RVA: 0x00098FAB File Offset: 0x000971AB
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		// Token: 0x0600297F RID: 10623 RVA: 0x00098FB8 File Offset: 0x000971B8
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = new SecurityElement("IMembershipCondition");
			XMLUtil.AddClassAttribute(securityElement, base.GetType(), "System.Security.Policy.ApplicationDirectoryMembershipCondition");
			securityElement.AddAttribute("version", "1");
			return securityElement;
		}

		// Token: 0x06002980 RID: 10624 RVA: 0x00098FF2 File Offset: 0x000971F2
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

		// Token: 0x06002981 RID: 10625 RVA: 0x00099024 File Offset: 0x00097224
		public override bool Equals(object o)
		{
			return o is ApplicationDirectoryMembershipCondition;
		}

		// Token: 0x06002982 RID: 10626 RVA: 0x0009902F File Offset: 0x0009722F
		public override int GetHashCode()
		{
			return typeof(ApplicationDirectoryMembershipCondition).GetHashCode();
		}

		// Token: 0x06002983 RID: 10627 RVA: 0x00099040 File Offset: 0x00097240
		public override string ToString()
		{
			return Environment.GetResourceString("ApplicationDirectory_ToString");
		}
	}
}
