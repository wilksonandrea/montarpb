using System;
using System.Runtime.InteropServices;

namespace System.Security.Principal
{
	// Token: 0x02000334 RID: 820
	[ComVisible(false)]
	public abstract class IdentityReference
	{
		// Token: 0x060028FB RID: 10491 RVA: 0x00096EB7 File Offset: 0x000950B7
		internal IdentityReference()
		{
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x060028FC RID: 10492
		public abstract string Value { get; }

		// Token: 0x060028FD RID: 10493
		public abstract bool IsValidTargetType(Type targetType);

		// Token: 0x060028FE RID: 10494
		public abstract IdentityReference Translate(Type targetType);

		// Token: 0x060028FF RID: 10495
		public abstract override bool Equals(object o);

		// Token: 0x06002900 RID: 10496
		public abstract override int GetHashCode();

		// Token: 0x06002901 RID: 10497
		public abstract override string ToString();

		// Token: 0x06002902 RID: 10498 RVA: 0x00096EC0 File Offset: 0x000950C0
		public static bool operator ==(IdentityReference left, IdentityReference right)
		{
			return (left == null && right == null) || (left != null && right != null && left.Equals(right));
		}

		// Token: 0x06002903 RID: 10499 RVA: 0x00096EE8 File Offset: 0x000950E8
		public static bool operator !=(IdentityReference left, IdentityReference right)
		{
			return !(left == right);
		}
	}
}
