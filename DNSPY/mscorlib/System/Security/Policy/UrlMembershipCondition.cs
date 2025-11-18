using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
	// Token: 0x02000370 RID: 880
	[ComVisible(true)]
	[Serializable]
	public sealed class UrlMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition, IReportMatchMembershipCondition
	{
		// Token: 0x06002B92 RID: 11154 RVA: 0x000A2412 File Offset: 0x000A0612
		internal UrlMembershipCondition()
		{
			this.m_url = null;
		}

		// Token: 0x06002B93 RID: 11155 RVA: 0x000A2424 File Offset: 0x000A0624
		public UrlMembershipCondition(string url)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			this.m_url = new URLString(url, false, true);
			if (this.m_url.IsRelativeFileUrl)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_RelativeUrlMembershipCondition"), "url");
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06002B95 RID: 11157 RVA: 0x000A24BE File Offset: 0x000A06BE
		// (set) Token: 0x06002B94 RID: 11156 RVA: 0x000A2478 File Offset: 0x000A0678
		public string Url
		{
			get
			{
				if (this.m_url == null && this.m_element != null)
				{
					this.ParseURL();
				}
				return this.m_url.ToString();
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				URLString urlstring = new URLString(value);
				if (urlstring.IsRelativeFileUrl)
				{
					throw new ArgumentException(Environment.GetResourceString("Argument_RelativeUrlMembershipCondition"), "value");
				}
				this.m_url = urlstring;
			}
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x000A24E4 File Offset: 0x000A06E4
		public bool Check(Evidence evidence)
		{
			object obj = null;
			return ((IReportMatchMembershipCondition)this).Check(evidence, out obj);
		}

		// Token: 0x06002B97 RID: 11159 RVA: 0x000A24FC File Offset: 0x000A06FC
		bool IReportMatchMembershipCondition.Check(Evidence evidence, out object usedEvidence)
		{
			usedEvidence = null;
			if (evidence == null)
			{
				return false;
			}
			Url hostEvidence = evidence.GetHostEvidence<Url>();
			if (hostEvidence != null)
			{
				if (this.m_url == null && this.m_element != null)
				{
					this.ParseURL();
				}
				if (hostEvidence.GetURLString().IsSubsetOf(this.m_url))
				{
					usedEvidence = hostEvidence;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002B98 RID: 11160 RVA: 0x000A254C File Offset: 0x000A074C
		public IMembershipCondition Copy()
		{
			if (this.m_url == null && this.m_element != null)
			{
				this.ParseURL();
			}
			return new UrlMembershipCondition
			{
				m_url = new URLString(this.m_url.ToString())
			};
		}

		// Token: 0x06002B99 RID: 11161 RVA: 0x000A258C File Offset: 0x000A078C
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		// Token: 0x06002B9A RID: 11162 RVA: 0x000A2595 File Offset: 0x000A0795
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x000A25A0 File Offset: 0x000A07A0
		public SecurityElement ToXml(PolicyLevel level)
		{
			if (this.m_url == null && this.m_element != null)
			{
				this.ParseURL();
			}
			SecurityElement securityElement = new SecurityElement("IMembershipCondition");
			XMLUtil.AddClassAttribute(securityElement, base.GetType(), "System.Security.Policy.UrlMembershipCondition");
			securityElement.AddAttribute("version", "1");
			if (this.m_url != null)
			{
				securityElement.AddAttribute("Url", this.m_url.ToString());
			}
			return securityElement;
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x000A2610 File Offset: 0x000A0810
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
				this.m_element = e;
				this.m_url = null;
			}
		}

		// Token: 0x06002B9D RID: 11165 RVA: 0x000A2684 File Offset: 0x000A0884
		private void ParseURL()
		{
			lock (this)
			{
				if (this.m_element != null)
				{
					string text = this.m_element.Attribute("Url");
					if (text == null)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_UrlCannotBeNull"));
					}
					URLString urlstring = new URLString(text);
					if (urlstring.IsRelativeFileUrl)
					{
						throw new ArgumentException(Environment.GetResourceString("Argument_RelativeUrlMembershipCondition"));
					}
					this.m_url = urlstring;
					this.m_element = null;
				}
			}
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x000A2714 File Offset: 0x000A0914
		public override bool Equals(object o)
		{
			UrlMembershipCondition urlMembershipCondition = o as UrlMembershipCondition;
			if (urlMembershipCondition != null)
			{
				if (this.m_url == null && this.m_element != null)
				{
					this.ParseURL();
				}
				if (urlMembershipCondition.m_url == null && urlMembershipCondition.m_element != null)
				{
					urlMembershipCondition.ParseURL();
				}
				if (object.Equals(this.m_url, urlMembershipCondition.m_url))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x000A276D File Offset: 0x000A096D
		public override int GetHashCode()
		{
			if (this.m_url == null && this.m_element != null)
			{
				this.ParseURL();
			}
			if (this.m_url != null)
			{
				return this.m_url.GetHashCode();
			}
			return typeof(UrlMembershipCondition).GetHashCode();
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x000A27A8 File Offset: 0x000A09A8
		public override string ToString()
		{
			if (this.m_url == null && this.m_element != null)
			{
				this.ParseURL();
			}
			if (this.m_url != null)
			{
				return string.Format(CultureInfo.CurrentCulture, Environment.GetResourceString("Url_ToStringArg"), this.m_url.ToString());
			}
			return Environment.GetResourceString("Url_ToString");
		}

		// Token: 0x040011A9 RID: 4521
		private URLString m_url;

		// Token: 0x040011AA RID: 4522
		private SecurityElement m_element;
	}
}
