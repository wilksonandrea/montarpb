using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000212 RID: 530
	public sealed class CryptoKeyAuditRule : AuditRule
	{
		// Token: 0x06001F01 RID: 7937 RVA: 0x0006CF6A File Offset: 0x0006B16A
		public CryptoKeyAuditRule(IdentityReference identity, CryptoKeyRights cryptoKeyRights, AuditFlags flags)
			: this(identity, CryptoKeyAuditRule.AccessMaskFromRights(cryptoKeyRights), false, InheritanceFlags.None, PropagationFlags.None, flags)
		{
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x0006CF7D File Offset: 0x0006B17D
		public CryptoKeyAuditRule(string identity, CryptoKeyRights cryptoKeyRights, AuditFlags flags)
			: this(new NTAccount(identity), CryptoKeyAuditRule.AccessMaskFromRights(cryptoKeyRights), false, InheritanceFlags.None, PropagationFlags.None, flags)
		{
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x0006CF95 File Offset: 0x0006B195
		private CryptoKeyAuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
			: base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, flags)
		{
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06001F04 RID: 7940 RVA: 0x0006CFA6 File Offset: 0x0006B1A6
		public CryptoKeyRights CryptoKeyRights
		{
			get
			{
				return CryptoKeyAuditRule.RightsFromAccessMask(base.AccessMask);
			}
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x0006CFB3 File Offset: 0x0006B1B3
		private static int AccessMaskFromRights(CryptoKeyRights cryptoKeyRights)
		{
			return (int)cryptoKeyRights;
		}

		// Token: 0x06001F06 RID: 7942 RVA: 0x0006CFB6 File Offset: 0x0006B1B6
		internal static CryptoKeyRights RightsFromAccessMask(int accessMask)
		{
			return (CryptoKeyRights)accessMask;
		}
	}
}
