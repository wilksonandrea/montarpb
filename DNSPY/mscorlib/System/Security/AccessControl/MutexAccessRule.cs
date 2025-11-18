using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x0200021F RID: 543
	public sealed class MutexAccessRule : AccessRule
	{
		// Token: 0x06001F62 RID: 8034 RVA: 0x0006D968 File Offset: 0x0006BB68
		public MutexAccessRule(IdentityReference identity, MutexRights eventRights, AccessControlType type)
			: this(identity, (int)eventRights, false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x0006D976 File Offset: 0x0006BB76
		public MutexAccessRule(string identity, MutexRights eventRights, AccessControlType type)
			: this(new NTAccount(identity), (int)eventRights, false, InheritanceFlags.None, PropagationFlags.None, type)
		{
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x0006D989 File Offset: 0x0006BB89
		internal MutexAccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
			: base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
		{
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06001F65 RID: 8037 RVA: 0x0006D99A File Offset: 0x0006BB9A
		public MutexRights MutexRights
		{
			get
			{
				return (MutexRights)base.AccessMask;
			}
		}
	}
}
