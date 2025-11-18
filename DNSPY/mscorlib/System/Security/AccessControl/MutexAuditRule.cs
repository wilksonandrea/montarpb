using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000220 RID: 544
	public sealed class MutexAuditRule : AuditRule
	{
		// Token: 0x06001F66 RID: 8038 RVA: 0x0006D9A2 File Offset: 0x0006BBA2
		public MutexAuditRule(IdentityReference identity, MutexRights eventRights, AuditFlags flags)
			: this(identity, (int)eventRights, false, InheritanceFlags.None, PropagationFlags.None, flags)
		{
		}

		// Token: 0x06001F67 RID: 8039 RVA: 0x0006D9B0 File Offset: 0x0006BBB0
		internal MutexAuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
			: base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06001F68 RID: 8040 RVA: 0x0006D9C1 File Offset: 0x0006BBC1
		public MutexRights MutexRights
		{
			get
			{
				return (MutexRights)base.AccessMask;
			}
		}
	}
}
