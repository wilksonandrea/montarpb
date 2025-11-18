using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
	// Token: 0x0200036B RID: 875
	[ComVisible(true)]
	[Serializable]
	public sealed class SiteMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IReportMatchMembershipCondition
	{
		// Token: 0x06002B45 RID: 11077 RVA: 0x000A1254 File Offset: 0x0009F454
		internal SiteMembershipCondition()
		{
			this.m_site = null;
		}

		// Token: 0x06002B46 RID: 11078 RVA: 0x000A1263 File Offset: 0x0009F463
		public SiteMembershipCondition(string site)
		{
			if (site == null)
			{
				throw new ArgumentNullException("site");
			}
			this.m_site = new SiteString(site);
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06002B48 RID: 11080 RVA: 0x000A12A1 File Offset: 0x0009F4A1
		// (set) Token: 0x06002B47 RID: 11079 RVA: 0x000A1285 File Offset: 0x0009F485
		public string Site
		{
			get
			{
				if (this.m_site == null && this.m_element != null)
				{
					this.ParseSite();
				}
				if (this.m_site != null)
				{
					return this.m_site.ToString();
				}
				return "";
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.m_site = new SiteString(value);
			}
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x000A12D4 File Offset: 0x0009F4D4
		public bool Check(Evidence evidence)
		{
			object obj = null;
			return ((IReportMatchMembershipCondition)this).Check(evidence, out obj);
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x000A12EC File Offset: 0x0009F4EC
		bool IReportMatchMembershipCondition.Check(Evidence evidence, out object usedEvidence)
		{
			usedEvidence = null;
			if (evidence == null)
			{
				return false;
			}
			Site hostEvidence = evidence.GetHostEvidence<Site>();
			if (hostEvidence != null)
			{
				if (this.m_site == null && this.m_element != null)
				{
					this.ParseSite();
				}
				if (hostEvidence.GetSiteString().IsSubsetOf(this.m_site))
				{
					usedEvidence = hostEvidence;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x000A133A File Offset: 0x0009F53A
		public IMembershipCondition Copy()
		{
			if (this.m_site == null && this.m_element != null)
			{
				this.ParseSite();
			}
			return new SiteMembershipCondition(this.m_site.ToString());
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x000A1362 File Offset: 0x0009F562
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x000A136B File Offset: 0x0009F56B
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		// Token: 0x06002B4E RID: 11086 RVA: 0x000A1378 File Offset: 0x0009F578
		public SecurityElement ToXml(PolicyLevel level)
		{
			if (this.m_site == null && this.m_element != null)
			{
				this.ParseSite();
			}
			SecurityElement securityElement = new SecurityElement("IMembershipCondition");
			XMLUtil.AddClassAttribute(securityElement, base.GetType(), "System.Security.Policy.SiteMembershipCondition");
			securityElement.AddAttribute("version", "1");
			if (this.m_site != null)
			{
				securityElement.AddAttribute("Site", this.m_site.ToString());
			}
			return securityElement;
		}

		// Token: 0x06002B4F RID: 11087 RVA: 0x000A13E8 File Offset: 0x0009F5E8
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
			lock (this)
			{
				this.m_site = null;
				this.m_element = e;
			}
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x000A145C File Offset: 0x0009F65C
		private void ParseSite()
		{
			lock (this)
			{
				if (this.m_element != null)
				{
					string text = this.m_element.Attribute("Site");
					if (text == null)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_SiteCannotBeNull"));
					}
					this.m_site = new SiteString(text);
					this.m_element = null;
				}
			}
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x000A14D4 File Offset: 0x0009F6D4
		public override bool Equals(object o)
		{
			SiteMembershipCondition siteMembershipCondition = o as SiteMembershipCondition;
			if (siteMembershipCondition != null)
			{
				if (this.m_site == null && this.m_element != null)
				{
					this.ParseSite();
				}
				if (siteMembershipCondition.m_site == null && siteMembershipCondition.m_element != null)
				{
					siteMembershipCondition.ParseSite();
				}
				if (object.Equals(this.m_site, siteMembershipCondition.m_site))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002B52 RID: 11090 RVA: 0x000A152D File Offset: 0x0009F72D
		public override int GetHashCode()
		{
			if (this.m_site == null && this.m_element != null)
			{
				this.ParseSite();
			}
			if (this.m_site != null)
			{
				return this.m_site.GetHashCode();
			}
			return typeof(SiteMembershipCondition).GetHashCode();
		}

		// Token: 0x06002B53 RID: 11091 RVA: 0x000A1568 File Offset: 0x0009F768
		public override string ToString()
		{
			if (this.m_site == null && this.m_element != null)
			{
				this.ParseSite();
			}
			if (this.m_site != null)
			{
				return string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Site_ToStringArg"), this.m_site);
			}
			return Environment.GetResourceString("Site_ToString");
		}

		// Token: 0x0400119A RID: 4506
		private SiteString m_site;

		// Token: 0x0400119B RID: 4507
		private SecurityElement m_element;
	}
}
