using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Util;

namespace System.Security.Policy
{
	// Token: 0x0200036F RID: 879
	[ComVisible(true)]
	[Serializable]
	public sealed class Url : EvidenceBase, IIdentityPermissionFactory
	{
		// Token: 0x06002B85 RID: 11141 RVA: 0x000A22E6 File Offset: 0x000A04E6
		internal Url(string name, bool parsed)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.m_url = new URLString(name, parsed);
		}

		// Token: 0x06002B86 RID: 11142 RVA: 0x000A2309 File Offset: 0x000A0509
		public Url(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.m_url = new URLString(name);
		}

		// Token: 0x06002B87 RID: 11143 RVA: 0x000A232B File Offset: 0x000A052B
		private Url(Url url)
		{
			this.m_url = url.m_url;
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06002B88 RID: 11144 RVA: 0x000A233F File Offset: 0x000A053F
		public string Value
		{
			get
			{
				return this.m_url.ToString();
			}
		}

		// Token: 0x06002B89 RID: 11145 RVA: 0x000A234C File Offset: 0x000A054C
		internal URLString GetURLString()
		{
			return this.m_url;
		}

		// Token: 0x06002B8A RID: 11146 RVA: 0x000A2354 File Offset: 0x000A0554
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new UrlIdentityPermission(this.m_url);
		}

		// Token: 0x06002B8B RID: 11147 RVA: 0x000A2364 File Offset: 0x000A0564
		public override bool Equals(object o)
		{
			Url url = o as Url;
			return url != null && url.m_url.Equals(this.m_url);
		}

		// Token: 0x06002B8C RID: 11148 RVA: 0x000A238E File Offset: 0x000A058E
		public override int GetHashCode()
		{
			return this.m_url.GetHashCode();
		}

		// Token: 0x06002B8D RID: 11149 RVA: 0x000A239B File Offset: 0x000A059B
		public override EvidenceBase Clone()
		{
			return new Url(this);
		}

		// Token: 0x06002B8E RID: 11150 RVA: 0x000A23A3 File Offset: 0x000A05A3
		public object Copy()
		{
			return this.Clone();
		}

		// Token: 0x06002B8F RID: 11151 RVA: 0x000A23AC File Offset: 0x000A05AC
		internal SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.Url");
			securityElement.AddAttribute("version", "1");
			if (this.m_url != null)
			{
				securityElement.AddChild(new SecurityElement("Url", this.m_url.ToString()));
			}
			return securityElement;
		}

		// Token: 0x06002B90 RID: 11152 RVA: 0x000A23F8 File Offset: 0x000A05F8
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		// Token: 0x06002B91 RID: 11153 RVA: 0x000A2405 File Offset: 0x000A0605
		internal object Normalize()
		{
			return this.m_url.NormalizeUrl();
		}

		// Token: 0x040011A8 RID: 4520
		private URLString m_url;
	}
}
