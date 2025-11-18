using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.Security.AccessControl
{
	// Token: 0x02000217 RID: 535
	public sealed class EventWaitHandleSecurity : NativeObjectSecurity
	{
		// Token: 0x06001F21 RID: 7969 RVA: 0x0006D155 File Offset: 0x0006B355
		public EventWaitHandleSecurity()
			: base(true, ResourceType.KernelObject)
		{
		}

		// Token: 0x06001F22 RID: 7970 RVA: 0x0006D15F File Offset: 0x0006B35F
		[SecurityCritical]
		internal EventWaitHandleSecurity(string name, AccessControlSections includeSections)
			: base(true, ResourceType.KernelObject, name, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(EventWaitHandleSecurity._HandleErrorCode), null)
		{
		}

		// Token: 0x06001F23 RID: 7971 RVA: 0x0006D178 File Offset: 0x0006B378
		[SecurityCritical]
		internal EventWaitHandleSecurity(SafeWaitHandle handle, AccessControlSections includeSections)
			: base(true, ResourceType.KernelObject, handle, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(EventWaitHandleSecurity._HandleErrorCode), null)
		{
		}

		// Token: 0x06001F24 RID: 7972 RVA: 0x0006D194 File Offset: 0x0006B394
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

		// Token: 0x06001F25 RID: 7973 RVA: 0x0006D1DE File Offset: 0x0006B3DE
		public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new EventWaitHandleAccessRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, type);
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x0006D1EE File Offset: 0x0006B3EE
		public override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new EventWaitHandleAuditRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, flags);
		}

		// Token: 0x06001F27 RID: 7975 RVA: 0x0006D200 File Offset: 0x0006B400
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

		// Token: 0x06001F28 RID: 7976 RVA: 0x0006D240 File Offset: 0x0006B440
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

		// Token: 0x06001F29 RID: 7977 RVA: 0x0006D2A4 File Offset: 0x0006B4A4
		public void AddAccessRule(EventWaitHandleAccessRule rule)
		{
			base.AddAccessRule(rule);
		}

		// Token: 0x06001F2A RID: 7978 RVA: 0x0006D2AD File Offset: 0x0006B4AD
		public void SetAccessRule(EventWaitHandleAccessRule rule)
		{
			base.SetAccessRule(rule);
		}

		// Token: 0x06001F2B RID: 7979 RVA: 0x0006D2B6 File Offset: 0x0006B4B6
		public void ResetAccessRule(EventWaitHandleAccessRule rule)
		{
			base.ResetAccessRule(rule);
		}

		// Token: 0x06001F2C RID: 7980 RVA: 0x0006D2BF File Offset: 0x0006B4BF
		public bool RemoveAccessRule(EventWaitHandleAccessRule rule)
		{
			return base.RemoveAccessRule(rule);
		}

		// Token: 0x06001F2D RID: 7981 RVA: 0x0006D2C8 File Offset: 0x0006B4C8
		public void RemoveAccessRuleAll(EventWaitHandleAccessRule rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		// Token: 0x06001F2E RID: 7982 RVA: 0x0006D2D1 File Offset: 0x0006B4D1
		public void RemoveAccessRuleSpecific(EventWaitHandleAccessRule rule)
		{
			base.RemoveAccessRuleSpecific(rule);
		}

		// Token: 0x06001F2F RID: 7983 RVA: 0x0006D2DA File Offset: 0x0006B4DA
		public void AddAuditRule(EventWaitHandleAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		// Token: 0x06001F30 RID: 7984 RVA: 0x0006D2E3 File Offset: 0x0006B4E3
		public void SetAuditRule(EventWaitHandleAuditRule rule)
		{
			base.SetAuditRule(rule);
		}

		// Token: 0x06001F31 RID: 7985 RVA: 0x0006D2EC File Offset: 0x0006B4EC
		public bool RemoveAuditRule(EventWaitHandleAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		// Token: 0x06001F32 RID: 7986 RVA: 0x0006D2F5 File Offset: 0x0006B4F5
		public void RemoveAuditRuleAll(EventWaitHandleAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		// Token: 0x06001F33 RID: 7987 RVA: 0x0006D2FE File Offset: 0x0006B4FE
		public void RemoveAuditRuleSpecific(EventWaitHandleAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001F34 RID: 7988 RVA: 0x0006D307 File Offset: 0x0006B507
		public override Type AccessRightType
		{
			get
			{
				return typeof(EventWaitHandleRights);
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06001F35 RID: 7989 RVA: 0x0006D313 File Offset: 0x0006B513
		public override Type AccessRuleType
		{
			get
			{
				return typeof(EventWaitHandleAccessRule);
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001F36 RID: 7990 RVA: 0x0006D31F File Offset: 0x0006B51F
		public override Type AuditRuleType
		{
			get
			{
				return typeof(EventWaitHandleAuditRule);
			}
		}
	}
}
