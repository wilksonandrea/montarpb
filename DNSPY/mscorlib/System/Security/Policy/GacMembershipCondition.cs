using System;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
	// Token: 0x02000374 RID: 884
	[ComVisible(true)]
	[Serializable]
	public sealed class GacMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IReportMatchMembershipCondition
	{
		// Token: 0x06002BC9 RID: 11209 RVA: 0x000A2D8C File Offset: 0x000A0F8C
		public GacMembershipCondition()
		{
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x000A2D94 File Offset: 0x000A0F94
		public bool Check(Evidence evidence)
		{
			object obj = null;
			return ((IReportMatchMembershipCondition)this).Check(evidence, out obj);
		}

		// Token: 0x06002BCB RID: 11211 RVA: 0x000A2DAC File Offset: 0x000A0FAC
		bool IReportMatchMembershipCondition.Check(Evidence evidence, out object usedEvidence)
		{
			usedEvidence = null;
			return evidence != null && evidence.GetHostEvidence<GacInstalled>() != null;
		}

		// Token: 0x06002BCC RID: 11212 RVA: 0x000A2DBF File Offset: 0x000A0FBF
		public IMembershipCondition Copy()
		{
			return new GacMembershipCondition();
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x000A2DC6 File Offset: 0x000A0FC6
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x000A2DCF File Offset: 0x000A0FCF
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x000A2DDC File Offset: 0x000A0FDC
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = new SecurityElement("IMembershipCondition");
			XMLUtil.AddClassAttribute(securityElement, base.GetType(), base.GetType().FullName);
			securityElement.AddAttribute("version", "1");
			return securityElement;
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x000A2E1C File Offset: 0x000A101C
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

		// Token: 0x06002BD1 RID: 11217 RVA: 0x000A2E50 File Offset: 0x000A1050
		public override bool Equals(object o)
		{
			return o is GacMembershipCondition;
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x000A2E6A File Offset: 0x000A106A
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x000A2E6D File Offset: 0x000A106D
		public override string ToString()
		{
			return Environment.GetResourceString("GAC_ToString");
		}
	}
}
