using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.Security.AccessControl
{
	// Token: 0x02000221 RID: 545
	public sealed class MutexSecurity : NativeObjectSecurity
	{
		// Token: 0x06001F69 RID: 8041 RVA: 0x0006D9C9 File Offset: 0x0006BBC9
		public MutexSecurity()
			: base(true, ResourceType.KernelObject)
		{
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x0006D9D3 File Offset: 0x0006BBD3
		[SecuritySafeCritical]
		public MutexSecurity(string name, AccessControlSections includeSections)
			: base(true, ResourceType.KernelObject, name, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(MutexSecurity._HandleErrorCode), null)
		{
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x0006D9EC File Offset: 0x0006BBEC
		[SecurityCritical]
		internal MutexSecurity(SafeWaitHandle handle, AccessControlSections includeSections)
			: base(true, ResourceType.KernelObject, handle, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(MutexSecurity._HandleErrorCode), null)
		{
		}

		// Token: 0x06001F6C RID: 8044 RVA: 0x0006DA08 File Offset: 0x0006BC08
		[SecurityCritical]
		private static Exception _HandleErrorCode(int errorCode, string name, SafeHandle handle, object context)
		{
			Exception ex = null;
			if (errorCode == 2 || errorCode == 6 || errorCode == 123)
			{
				if (name != null && name.Length != 0)
				{
					ex = new WaitHandleCannotBeOpenedException(Environment.GetResourceString("Threading.WaitHandleCannotBeOpenedException_InvalidHandle", new object[] { name }));
				}
				else
				{
					ex = new WaitHandleCannotBeOpenedException();
				}
			}
			return ex;
		}

		// Token: 0x06001F6D RID: 8045 RVA: 0x0006DA52 File Offset: 0x0006BC52
		public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new MutexAccessRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, type);
		}

		// Token: 0x06001F6E RID: 8046 RVA: 0x0006DA62 File Offset: 0x0006BC62
		public override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new MutexAuditRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, flags);
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x0006DA74 File Offset: 0x0006BC74
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

		// Token: 0x06001F70 RID: 8048 RVA: 0x0006DAB4 File Offset: 0x0006BCB4
		[SecurityCritical]
		internal void Persist(SafeWaitHandle handle)
		{
			base.WriteLock();
			try
			{
				AccessControlSections accessControlSectionsFromChanges = this.GetAccessControlSectionsFromChanges();
				if (accessControlSectionsFromChanges != AccessControlSections.None)
				{
					base.Persist(handle, accessControlSectionsFromChanges);
					base.OwnerModified = (base.GroupModified = (base.AuditRulesModified = (base.AccessRulesModified = false)));
				}
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		// Token: 0x06001F71 RID: 8049 RVA: 0x0006DB18 File Offset: 0x0006BD18
		public void AddAccessRule(MutexAccessRule rule)
		{
			base.AddAccessRule(rule);
		}

		// Token: 0x06001F72 RID: 8050 RVA: 0x0006DB21 File Offset: 0x0006BD21
		public void SetAccessRule(MutexAccessRule rule)
		{
			base.SetAccessRule(rule);
		}

		// Token: 0x06001F73 RID: 8051 RVA: 0x0006DB2A File Offset: 0x0006BD2A
		public void ResetAccessRule(MutexAccessRule rule)
		{
			base.ResetAccessRule(rule);
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x0006DB33 File Offset: 0x0006BD33
		public bool RemoveAccessRule(MutexAccessRule rule)
		{
			return base.RemoveAccessRule(rule);
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x0006DB3C File Offset: 0x0006BD3C
		public void RemoveAccessRuleAll(MutexAccessRule rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		// Token: 0x06001F76 RID: 8054 RVA: 0x0006DB45 File Offset: 0x0006BD45
		public void RemoveAccessRuleSpecific(MutexAccessRule rule)
		{
			base.RemoveAccessRuleSpecific(rule);
		}

		// Token: 0x06001F77 RID: 8055 RVA: 0x0006DB4E File Offset: 0x0006BD4E
		public void AddAuditRule(MutexAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		// Token: 0x06001F78 RID: 8056 RVA: 0x0006DB57 File Offset: 0x0006BD57
		public void SetAuditRule(MutexAuditRule rule)
		{
			base.SetAuditRule(rule);
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x0006DB60 File Offset: 0x0006BD60
		public bool RemoveAuditRule(MutexAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x0006DB69 File Offset: 0x0006BD69
		public void RemoveAuditRuleAll(MutexAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		// Token: 0x06001F7B RID: 8059 RVA: 0x0006DB72 File Offset: 0x0006BD72
		public void RemoveAuditRuleSpecific(MutexAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06001F7C RID: 8060 RVA: 0x0006DB7B File Offset: 0x0006BD7B
		public override Type AccessRightType
		{
			get
			{
				return typeof(MutexRights);
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06001F7D RID: 8061 RVA: 0x0006DB87 File Offset: 0x0006BD87
		public override Type AccessRuleType
		{
			get
			{
				return typeof(MutexAccessRule);
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06001F7E RID: 8062 RVA: 0x0006DB93 File Offset: 0x0006BD93
		public override Type AuditRuleType
		{
			get
			{
				return typeof(MutexAuditRule);
			}
		}
	}
}
