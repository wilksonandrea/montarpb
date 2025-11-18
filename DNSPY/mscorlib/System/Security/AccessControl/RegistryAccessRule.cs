using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x0200022D RID: 557
	public sealed class RegistryAccessRule : AccessRule
	{
		// Token: 0x0600201B RID: 8219 RVA: 0x00070D5F File Offset: 0x0006EF5F
		public RegistryAccessRule(IdentityReference identity, RegistryRights registryRights, AccessControlType type)
			: this(identity, (int)registryRights, false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x0600201C RID: 8220 RVA: 0x00070D6D File Offset: 0x0006EF6D
		public RegistryAccessRule(string identity, RegistryRights registryRights, AccessControlType type)
			: this(new NTAccount(identity), (int)registryRights, false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x0600201D RID: 8221 RVA: 0x00070D80 File Offset: 0x0006EF80
		public RegistryAccessRule(IdentityReference identity, RegistryRights registryRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
			: this(identity, (int)registryRights, false, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x0600201E RID: 8222 RVA: 0x00070D90 File Offset: 0x0006EF90
		public RegistryAccessRule(string identity, RegistryRights registryRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
			: this(new NTAccount(identity), (int)registryRights, false, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x0600201F RID: 8223 RVA: 0x00070DA5 File Offset: 0x0006EFA5
		internal RegistryAccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
			: base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06002020 RID: 8224 RVA: 0x00070DB6 File Offset: 0x0006EFB6
		public RegistryRights RegistryRights
		{
			get
			{
				return (RegistryRights)base.AccessMask;
			}
		}
	}
}
