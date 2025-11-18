using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000226 RID: 550
	public class AuditRule<T> : AuditRule where T : struct
	{
		// Token: 0x06001FC5 RID: 8133 RVA: 0x0006EB5F File Offset: 0x0006CD5F
		public AuditRule(IdentityReference identity, T rights, AuditFlags flags)
			: this(identity, rights, InheritanceFlags.None, PropagationFlags.None, flags)
		{
		}

		// Token: 0x06001FC6 RID: 8134 RVA: 0x0006EB6C File Offset: 0x0006CD6C
		public AuditRule(IdentityReference identity, T rights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
			: this(identity, (int)((object)rights), false, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x0006EB86 File Offset: 0x0006CD86
		public AuditRule(string identity, T rights, AuditFlags flags)
			: this(new NTAccount(identity), rights, InheritanceFlags.None, PropagationFlags.None, flags)
		{
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x0006EB98 File Offset: 0x0006CD98
		public AuditRule(string identity, T rights, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
			: this(new NTAccount(identity), (int)((object)rights), false, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x0006EBB7 File Offset: 0x0006CDB7
		internal AuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
			: base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06001FCA RID: 8138 RVA: 0x0006EBC8 File Offset: 0x0006CDC8
		public T Rights
		{
			get
			{
				return (T)((object)base.AccessMask);
			}
		}
	}
}
