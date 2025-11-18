using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000215 RID: 533
	public sealed class EventWaitHandleAccessRule : AccessRule
	{
		// Token: 0x06001F1A RID: 7962 RVA: 0x0006D0F4 File Offset: 0x0006B2F4
		public EventWaitHandleAccessRule(IdentityReference identity, EventWaitHandleRights eventRights, AccessControlType type)
			: this(identity, (int)eventRights, false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x06001F1B RID: 7963 RVA: 0x0006D102 File Offset: 0x0006B302
		public EventWaitHandleAccessRule(string identity, EventWaitHandleRights eventRights, AccessControlType type)
			: this(new NTAccount(identity), (int)eventRights, false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x06001F1C RID: 7964 RVA: 0x0006D115 File Offset: 0x0006B315
		internal EventWaitHandleAccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
			: base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06001F1D RID: 7965 RVA: 0x0006D126 File Offset: 0x0006B326
		public EventWaitHandleRights EventWaitHandleRights
		{
			get
			{
				return (EventWaitHandleRights)base.AccessMask;
			}
		}
	}
}
