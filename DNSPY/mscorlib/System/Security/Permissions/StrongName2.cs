using System;
using System.Security.Policy;

namespace System.Security.Permissions
{
	// Token: 0x02000309 RID: 777
	[Serializable]
	internal sealed class StrongName2
	{
		// Token: 0x06002759 RID: 10073 RVA: 0x0008E8E9 File Offset: 0x0008CAE9
		public StrongName2(StrongNamePublicKeyBlob publicKeyBlob, string name, Version version)
		{
			this.m_publicKeyBlob = publicKeyBlob;
			this.m_name = name;
			this.m_version = version;
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x0008E906 File Offset: 0x0008CB06
		public StrongName2 Copy()
		{
			return new StrongName2(this.m_publicKeyBlob, this.m_name, this.m_version);
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x0008E920 File Offset: 0x0008CB20
		public bool IsSubsetOf(StrongName2 target)
		{
			return this.m_publicKeyBlob == null || (this.m_publicKeyBlob.Equals(target.m_publicKeyBlob) && (this.m_name == null || (target.m_name != null && StrongName.CompareNames(target.m_name, this.m_name))) && (this.m_version == null || (target.m_version != null && target.m_version.CompareTo(this.m_version) == 0)));
		}

		// Token: 0x0600275C RID: 10076 RVA: 0x0008E997 File Offset: 0x0008CB97
		public StrongName2 Intersect(StrongName2 target)
		{
			if (target.IsSubsetOf(this))
			{
				return target.Copy();
			}
			if (this.IsSubsetOf(target))
			{
				return this.Copy();
			}
			return null;
		}

		// Token: 0x0600275D RID: 10077 RVA: 0x0008E9BA File Offset: 0x0008CBBA
		public bool Equals(StrongName2 target)
		{
			return target.IsSubsetOf(this) && this.IsSubsetOf(target);
		}

		// Token: 0x04000F45 RID: 3909
		public StrongNamePublicKeyBlob m_publicKeyBlob;

		// Token: 0x04000F46 RID: 3910
		public string m_name;

		// Token: 0x04000F47 RID: 3911
		public Version m_version;
	}
}
