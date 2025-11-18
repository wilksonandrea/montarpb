using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000225 RID: 549
	public class AccessRule<T> : AccessRule where T : struct
	{
		// Token: 0x06001FBF RID: 8127 RVA: 0x0006EACE File Offset: 0x0006CCCE
		public AccessRule(IdentityReference identity, T rights, AccessControlType type)
			: this(identity, (int)((object)rights), false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x0006EAE6 File Offset: 0x0006CCE6
		public AccessRule(string identity, T rights, AccessControlType type)
			: this(new NTAccount(identity), (int)((object)rights), false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x0006EB03 File Offset: 0x0006CD03
		public AccessRule(IdentityReference identity, T rights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
			: this(identity, (int)((object)rights), false, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x0006EB1D File Offset: 0x0006CD1D
		public AccessRule(string identity, T rights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
			: this(new NTAccount(identity), (int)((object)rights), false, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x06001FC3 RID: 8131 RVA: 0x0006EB3C File Offset: 0x0006CD3C
		internal AccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
			: base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06001FC4 RID: 8132 RVA: 0x0006EB4D File Offset: 0x0006CD4D
		public T Rights
		{
			get
			{
				return (T)((object)base.AccessMask);
			}
		}
	}
}
