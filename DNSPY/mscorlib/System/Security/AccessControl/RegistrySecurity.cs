using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;
using Microsoft.Win32.SafeHandles;

namespace System.Security.AccessControl
{
	// Token: 0x0200022F RID: 559
	public sealed class RegistrySecurity : NativeObjectSecurity
	{
		// Token: 0x06002025 RID: 8229 RVA: 0x00070DFC File Offset: 0x0006EFFC
		public RegistrySecurity()
			: base(true, ResourceType.RegistryKey)
		{
		}

		// Token: 0x06002026 RID: 8230 RVA: 0x00070E06 File Offset: 0x0006F006
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		internal RegistrySecurity(SafeRegistryHandle hKey, string name, AccessControlSections includeSections)
			: base(true, ResourceType.RegistryKey, hKey, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(RegistrySecurity._HandleErrorCode), null)
		{
			new RegistryPermission(RegistryPermissionAccess.NoAccess, AccessControlActions.View, name).Demand();
		}

		// Token: 0x06002027 RID: 8231 RVA: 0x00070E2C File Offset: 0x0006F02C
		[SecurityCritical]
		private static Exception _HandleErrorCode(int errorCode, string name, SafeHandle handle, object context)
		{
			Exception ex = null;
			if (errorCode != 2)
			{
				if (errorCode != 6)
				{
					if (errorCode == 123)
					{
						ex = new ArgumentException(Environment.GetResourceString("Arg_RegInvalidKeyName", new object[] { "name" }));
					}
				}
				else
				{
					ex = new ArgumentException(Environment.GetResourceString("AccessControl_InvalidHandle"));
				}
			}
			else
			{
				ex = new IOException(Environment.GetResourceString("Arg_RegKeyNotFound", new object[] { errorCode }));
			}
			return ex;
		}

		// Token: 0x06002028 RID: 8232 RVA: 0x00070E9C File Offset: 0x0006F09C
		public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new RegistryAccessRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, type);
		}

		// Token: 0x06002029 RID: 8233 RVA: 0x00070EAC File Offset: 0x0006F0AC
		public override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new RegistryAuditRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, flags);
		}

		// Token: 0x0600202A RID: 8234 RVA: 0x00070EBC File Offset: 0x0006F0BC
		internal AccessControlSections GetAccessControlSectionsFromChanges()
		{
			AccessControlSections accessControlSections = AccessControlSections.None;
			if (base.AccessRulesModified)
			{
				accessControlSections = AccessControlSections.Access;
			}
			if (base.AuditRulesModified)
			{
				accessControlSections |= AccessControlSections.Audit;
			}
			if (base.OwnerModified)
			{
				accessControlSections |= AccessControlSections.Owner;
			}
			if (base.GroupModified)
			{
				accessControlSections |= AccessControlSections.Group;
			}
			return accessControlSections;
		}

		// Token: 0x0600202B RID: 8235 RVA: 0x00070EFC File Offset: 0x0006F0FC
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		internal void Persist(SafeRegistryHandle hKey, string keyName)
		{
			new RegistryPermission(RegistryPermissionAccess.NoAccess, AccessControlActions.Change, keyName).Demand();
			base.WriteLock();
			try
			{
				AccessControlSections accessControlSectionsFromChanges = this.GetAccessControlSectionsFromChanges();
				if (accessControlSectionsFromChanges != AccessControlSections.None)
				{
					base.Persist(hKey, accessControlSectionsFromChanges);
					base.OwnerModified = (base.GroupModified = (base.AuditRulesModified = (base.AccessRulesModified = false)));
				}
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		// Token: 0x0600202C RID: 8236 RVA: 0x00070F6C File Offset: 0x0006F16C
		public void AddAccessRule(RegistryAccessRule rule)
		{
			base.AddAccessRule(rule);
		}

		// Token: 0x0600202D RID: 8237 RVA: 0x00070F75 File Offset: 0x0006F175
		public void SetAccessRule(RegistryAccessRule rule)
		{
			base.SetAccessRule(rule);
		}

		// Token: 0x0600202E RID: 8238 RVA: 0x00070F7E File Offset: 0x0006F17E
		public void ResetAccessRule(RegistryAccessRule rule)
		{
			base.ResetAccessRule(rule);
		}

		// Token: 0x0600202F RID: 8239 RVA: 0x00070F87 File Offset: 0x0006F187
		public bool RemoveAccessRule(RegistryAccessRule rule)
		{
			return base.RemoveAccessRule(rule);
		}

		// Token: 0x06002030 RID: 8240 RVA: 0x00070F90 File Offset: 0x0006F190
		public void RemoveAccessRuleAll(RegistryAccessRule rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		// Token: 0x06002031 RID: 8241 RVA: 0x00070F99 File Offset: 0x0006F199
		public void RemoveAccessRuleSpecific(RegistryAccessRule rule)
		{
			base.RemoveAccessRuleSpecific(rule);
		}

		// Token: 0x06002032 RID: 8242 RVA: 0x00070FA2 File Offset: 0x0006F1A2
		public void AddAuditRule(RegistryAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		// Token: 0x06002033 RID: 8243 RVA: 0x00070FAB File Offset: 0x0006F1AB
		public void SetAuditRule(RegistryAuditRule rule)
		{
			base.SetAuditRule(rule);
		}

		// Token: 0x06002034 RID: 8244 RVA: 0x00070FB4 File Offset: 0x0006F1B4
		public bool RemoveAuditRule(RegistryAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		// Token: 0x06002035 RID: 8245 RVA: 0x00070FBD File Offset: 0x0006F1BD
		public void RemoveAuditRuleAll(RegistryAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		// Token: 0x06002036 RID: 8246 RVA: 0x00070FC6 File Offset: 0x0006F1C6
		public void RemoveAuditRuleSpecific(RegistryAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06002037 RID: 8247 RVA: 0x00070FCF File Offset: 0x0006F1CF
		public override Type AccessRightType
		{
			get
			{
				return typeof(RegistryRights);
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06002038 RID: 8248 RVA: 0x00070FDB File Offset: 0x0006F1DB
		public override Type AccessRuleType
		{
			get
			{
				return typeof(RegistryAccessRule);
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06002039 RID: 8249 RVA: 0x00070FE7 File Offset: 0x0006F1E7
		public override Type AuditRuleType
		{
			get
			{
				return typeof(RegistryAuditRule);
			}
		}
	}
}
