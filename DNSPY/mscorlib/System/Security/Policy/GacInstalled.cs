using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Security.Policy
{
	// Token: 0x02000373 RID: 883
	[ComVisible(true)]
	[Serializable]
	public sealed class GacInstalled : EvidenceBase, IIdentityPermissionFactory
	{
		// Token: 0x06002BC1 RID: 11201 RVA: 0x000A2D23 File Offset: 0x000A0F23
		public GacInstalled()
		{
		}

		// Token: 0x06002BC2 RID: 11202 RVA: 0x000A2D2B File Offset: 0x000A0F2B
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new GacIdentityPermission();
		}

		// Token: 0x06002BC3 RID: 11203 RVA: 0x000A2D32 File Offset: 0x000A0F32
		public override bool Equals(object o)
		{
			return o is GacInstalled;
		}

		// Token: 0x06002BC4 RID: 11204 RVA: 0x000A2D3D File Offset: 0x000A0F3D
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06002BC5 RID: 11205 RVA: 0x000A2D40 File Offset: 0x000A0F40
		public override EvidenceBase Clone()
		{
			return new GacInstalled();
		}

		// Token: 0x06002BC6 RID: 11206 RVA: 0x000A2D47 File Offset: 0x000A0F47
		public object Copy()
		{
			return this.Clone();
		}

		// Token: 0x06002BC7 RID: 11207 RVA: 0x000A2D50 File Offset: 0x000A0F50
		internal SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement(base.GetType().FullName);
			securityElement.AddAttribute("version", "1");
			return securityElement;
		}

		// Token: 0x06002BC8 RID: 11208 RVA: 0x000A2D7F File Offset: 0x000A0F7F
		public override string ToString()
		{
			return this.ToXml().ToString();
		}
	}
}
