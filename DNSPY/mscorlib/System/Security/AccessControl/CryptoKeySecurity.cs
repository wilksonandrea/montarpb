using System;
using System.Runtime.CompilerServices;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000213 RID: 531
	public sealed class CryptoKeySecurity : NativeObjectSecurity
	{
		// Token: 0x06001F07 RID: 7943 RVA: 0x0006CFB9 File Offset: 0x0006B1B9
		public CryptoKeySecurity()
			: base(false, ResourceType.FileObject)
		{
		}

		// Token: 0x06001F08 RID: 7944 RVA: 0x0006CFC3 File Offset: 0x0006B1C3
		[SecuritySafeCritical]
		public CryptoKeySecurity(CommonSecurityDescriptor securityDescriptor)
			: base(ResourceType.FileObject, securityDescriptor)
		{
		}

		// Token: 0x06001F09 RID: 7945 RVA: 0x0006CFCD File Offset: 0x0006B1CD
		public sealed override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new CryptoKeyAccessRule(identityReference, CryptoKeyAccessRule.RightsFromAccessMask(accessMask), type);
		}

		// Token: 0x06001F0A RID: 7946 RVA: 0x0006CFDD File Offset: 0x0006B1DD
		public sealed override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new CryptoKeyAuditRule(identityReference, CryptoKeyAuditRule.RightsFromAccessMask(accessMask), flags);
		}

		// Token: 0x06001F0B RID: 7947 RVA: 0x0006CFED File Offset: 0x0006B1ED
		public void AddAccessRule(CryptoKeyAccessRule rule)
		{
			base.AddAccessRule(rule);
		}

		// Token: 0x06001F0C RID: 7948 RVA: 0x0006CFF6 File Offset: 0x0006B1F6
		public void SetAccessRule(CryptoKeyAccessRule rule)
		{
			base.SetAccessRule(rule);
		}

		// Token: 0x06001F0D RID: 7949 RVA: 0x0006CFFF File Offset: 0x0006B1FF
		public void ResetAccessRule(CryptoKeyAccessRule rule)
		{
			base.ResetAccessRule(rule);
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x0006D008 File Offset: 0x0006B208
		public bool RemoveAccessRule(CryptoKeyAccessRule rule)
		{
			return base.RemoveAccessRule(rule);
		}

		// Token: 0x06001F0F RID: 7951 RVA: 0x0006D011 File Offset: 0x0006B211
		public void RemoveAccessRuleAll(CryptoKeyAccessRule rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		// Token: 0x06001F10 RID: 7952 RVA: 0x0006D01A File Offset: 0x0006B21A
		public void RemoveAccessRuleSpecific(CryptoKeyAccessRule rule)
		{
			base.RemoveAccessRuleSpecific(rule);
		}

		// Token: 0x06001F11 RID: 7953 RVA: 0x0006D023 File Offset: 0x0006B223
		public void AddAuditRule(CryptoKeyAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		// Token: 0x06001F12 RID: 7954 RVA: 0x0006D02C File Offset: 0x0006B22C
		public void SetAuditRule(CryptoKeyAuditRule rule)
		{
			base.SetAuditRule(rule);
		}

		// Token: 0x06001F13 RID: 7955 RVA: 0x0006D035 File Offset: 0x0006B235
		public bool RemoveAuditRule(CryptoKeyAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		// Token: 0x06001F14 RID: 7956 RVA: 0x0006D03E File Offset: 0x0006B23E
		public void RemoveAuditRuleAll(CryptoKeyAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		// Token: 0x06001F15 RID: 7957 RVA: 0x0006D047 File Offset: 0x0006B247
		public void RemoveAuditRuleSpecific(CryptoKeyAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06001F16 RID: 7958 RVA: 0x0006D050 File Offset: 0x0006B250
		public override Type AccessRightType
		{
			get
			{
				return typeof(CryptoKeyRights);
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06001F17 RID: 7959 RVA: 0x0006D05C File Offset: 0x0006B25C
		public override Type AccessRuleType
		{
			get
			{
				return typeof(CryptoKeyAccessRule);
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06001F18 RID: 7960 RVA: 0x0006D068 File Offset: 0x0006B268
		public override Type AuditRuleType
		{
			get
			{
				return typeof(CryptoKeyAuditRule);
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06001F19 RID: 7961 RVA: 0x0006D074 File Offset: 0x0006B274
		internal AccessControlSections ChangedAccessControlSections
		{
			[SecurityCritical]
			get
			{
				AccessControlSections accessControlSections = AccessControlSections.None;
				bool flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
					}
					finally
					{
						base.ReadLock();
						flag = true;
					}
					if (base.AccessRulesModified)
					{
						accessControlSections |= AccessControlSections.Access;
					}
					if (base.AuditRulesModified)
					{
						accessControlSections |= AccessControlSections.Audit;
					}
					if (base.GroupModified)
					{
						accessControlSections |= AccessControlSections.Group;
					}
					if (base.OwnerModified)
					{
						accessControlSections |= AccessControlSections.Owner;
					}
				}
				finally
				{
					if (flag)
					{
						base.ReadUnlock();
					}
				}
				return accessControlSections;
			}
		}

		// Token: 0x04000B26 RID: 2854
		private const ResourceType s_ResourceType = ResourceType.FileObject;
	}
}
