using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Security.Policy
{
	// Token: 0x0200036A RID: 874
	[ComVisible(true)]
	[Serializable]
	public sealed class Site : EvidenceBase, IIdentityPermissionFactory
	{
		// Token: 0x06002B37 RID: 11063 RVA: 0x000A10E7 File Offset: 0x0009F2E7
		public Site(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.m_name = new SiteString(name);
		}

		// Token: 0x06002B38 RID: 11064 RVA: 0x000A1109 File Offset: 0x0009F309
		private Site(SiteString name)
		{
			this.m_name = name;
		}

		// Token: 0x06002B39 RID: 11065 RVA: 0x000A1118 File Offset: 0x0009F318
		public static Site CreateFromUrl(string url)
		{
			return new Site(Site.ParseSiteFromUrl(url));
		}

		// Token: 0x06002B3A RID: 11066 RVA: 0x000A1128 File Offset: 0x0009F328
		private static SiteString ParseSiteFromUrl(string name)
		{
			URLString urlstring = new URLString(name);
			if (string.Compare(urlstring.Scheme, "file", StringComparison.OrdinalIgnoreCase) == 0)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidSite"));
			}
			return new SiteString(new URLString(name).Host);
		}

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06002B3B RID: 11067 RVA: 0x000A116F File Offset: 0x0009F36F
		public string Name
		{
			get
			{
				return this.m_name.ToString();
			}
		}

		// Token: 0x06002B3C RID: 11068 RVA: 0x000A117C File Offset: 0x0009F37C
		internal SiteString GetSiteString()
		{
			return this.m_name;
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x000A1184 File Offset: 0x0009F384
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new SiteIdentityPermission(this.Name);
		}

		// Token: 0x06002B3E RID: 11070 RVA: 0x000A1194 File Offset: 0x0009F394
		public override bool Equals(object o)
		{
			Site site = o as Site;
			return site != null && string.Equals(this.Name, site.Name, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06002B3F RID: 11071 RVA: 0x000A11BF File Offset: 0x0009F3BF
		public override int GetHashCode()
		{
			return this.Name.GetHashCode();
		}

		// Token: 0x06002B40 RID: 11072 RVA: 0x000A11CC File Offset: 0x0009F3CC
		public override EvidenceBase Clone()
		{
			return new Site(this.m_name);
		}

		// Token: 0x06002B41 RID: 11073 RVA: 0x000A11D9 File Offset: 0x0009F3D9
		public object Copy()
		{
			return this.Clone();
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x000A11E4 File Offset: 0x0009F3E4
		internal SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.Site");
			securityElement.AddAttribute("version", "1");
			if (this.m_name != null)
			{
				securityElement.AddChild(new SecurityElement("Name", this.m_name.ToString()));
			}
			return securityElement;
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x000A1230 File Offset: 0x0009F430
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		// Token: 0x06002B44 RID: 11076 RVA: 0x000A123D File Offset: 0x0009F43D
		internal object Normalize()
		{
			return this.m_name.ToString().ToUpper(CultureInfo.InvariantCulture);
		}

		// Token: 0x04001199 RID: 4505
		private SiteString m_name;
	}
}
