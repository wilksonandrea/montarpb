using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x02000371 RID: 881
	[ComVisible(true)]
	[Serializable]
	public sealed class Zone : EvidenceBase, IIdentityPermissionFactory
	{
		// Token: 0x06002BA1 RID: 11169 RVA: 0x000A27FD File Offset: 0x000A09FD
		public Zone(SecurityZone zone)
		{
			if (zone < SecurityZone.NoZone || zone > SecurityZone.Untrusted)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_IllegalZone"));
			}
			this.m_zone = zone;
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x000A2824 File Offset: 0x000A0A24
		private Zone(Zone zone)
		{
			this.m_url = zone.m_url;
			this.m_zone = zone.m_zone;
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x000A2844 File Offset: 0x000A0A44
		private Zone(string url)
		{
			this.m_url = url;
			this.m_zone = SecurityZone.NoZone;
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x000A285A File Offset: 0x000A0A5A
		public static Zone CreateFromUrl(string url)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			return new Zone(url);
		}

		// Token: 0x06002BA5 RID: 11173
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern SecurityZone _CreateFromUrl(string url);

		// Token: 0x06002BA6 RID: 11174 RVA: 0x000A2870 File Offset: 0x000A0A70
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new ZoneIdentityPermission(this.SecurityZone);
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06002BA7 RID: 11175 RVA: 0x000A287D File Offset: 0x000A0A7D
		public SecurityZone SecurityZone
		{
			[SecuritySafeCritical]
			get
			{
				if (this.m_url != null)
				{
					this.m_zone = Zone._CreateFromUrl(this.m_url);
				}
				return this.m_zone;
			}
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x000A28A0 File Offset: 0x000A0AA0
		public override bool Equals(object o)
		{
			Zone zone = o as Zone;
			return zone != null && this.SecurityZone == zone.SecurityZone;
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x000A28C7 File Offset: 0x000A0AC7
		public override int GetHashCode()
		{
			return (int)this.SecurityZone;
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x000A28CF File Offset: 0x000A0ACF
		public override EvidenceBase Clone()
		{
			return new Zone(this);
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x000A28D7 File Offset: 0x000A0AD7
		public object Copy()
		{
			return this.Clone();
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x000A28E0 File Offset: 0x000A0AE0
		internal SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.Zone");
			securityElement.AddAttribute("version", "1");
			if (this.SecurityZone != SecurityZone.NoZone)
			{
				securityElement.AddChild(new SecurityElement("Zone", Zone.s_names[(int)this.SecurityZone]));
			}
			else
			{
				securityElement.AddChild(new SecurityElement("Zone", Zone.s_names[Zone.s_names.Length - 1]));
			}
			return securityElement;
		}

		// Token: 0x06002BAD RID: 11181 RVA: 0x000A294F File Offset: 0x000A0B4F
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		// Token: 0x06002BAE RID: 11182 RVA: 0x000A295C File Offset: 0x000A0B5C
		internal object Normalize()
		{
			return Zone.s_names[(int)this.SecurityZone];
		}

		// Token: 0x06002BAF RID: 11183 RVA: 0x000A296A File Offset: 0x000A0B6A
		// Note: this type is marked as 'beforefieldinit'.
		static Zone()
		{
		}

		// Token: 0x040011AB RID: 4523
		[OptionalField(VersionAdded = 2)]
		private string m_url;

		// Token: 0x040011AC RID: 4524
		private SecurityZone m_zone;

		// Token: 0x040011AD RID: 4525
		private static readonly string[] s_names = new string[] { "MyComputer", "Intranet", "Trusted", "Internet", "Untrusted", "NoZone" };
	}
}
