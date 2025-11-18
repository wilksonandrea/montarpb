using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x0200022E RID: 558
	public sealed class RegistryAuditRule : AuditRule
	{
		// Token: 0x06002021 RID: 8225 RVA: 0x00070DBE File Offset: 0x0006EFBE
		public RegistryAuditRule(IdentityReference identity, RegistryRights registryRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
			: this(identity, (int)registryRights, false, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x06002022 RID: 8226 RVA: 0x00070DCE File Offset: 0x0006EFCE
		public RegistryAuditRule(string identity, RegistryRights registryRights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
			: this(new NTAccount(identity), (int)registryRights, false, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x06002023 RID: 8227 RVA: 0x00070DE3 File Offset: 0x0006EFE3
		internal RegistryAuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
			: base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06002024 RID: 8228 RVA: 0x00070DF4 File Offset: 0x0006EFF4
		public RegistryRights RegistryRights
		{
			get
			{
				return (RegistryRights)base.AccessMask;
			}
		}
	}
}
