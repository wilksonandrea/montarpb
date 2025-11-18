using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000216 RID: 534
	public sealed class EventWaitHandleAuditRule : AuditRule
	{
		// Token: 0x06001F1E RID: 7966 RVA: 0x0006D12E File Offset: 0x0006B32E
		public EventWaitHandleAuditRule(IdentityReference identity, EventWaitHandleRights eventRights, AuditFlags flags)
			: this(identity, (int)eventRights, false, InheritanceFlags.None, PropagationFlags.None, flags)
		{
		}

		// Token: 0x06001F1F RID: 7967 RVA: 0x0006D13C File Offset: 0x0006B33C
		internal EventWaitHandleAuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
			: base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06001F20 RID: 7968 RVA: 0x0006D14D File Offset: 0x0006B34D
		public EventWaitHandleRights EventWaitHandleRights
		{
			get
			{
				return (EventWaitHandleRights)base.AccessMask;
			}
		}
	}
}
