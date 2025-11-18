using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000227 RID: 551
	public abstract class ObjectSecurity<T> : NativeObjectSecurity where T : struct
	{
		// Token: 0x06001FCB RID: 8139 RVA: 0x0006EBDA File Offset: 0x0006CDDA
		protected ObjectSecurity(bool isContainer, ResourceType resourceType)
			: base(isContainer, resourceType, null, null)
		{
		}

		// Token: 0x06001FCC RID: 8140 RVA: 0x0006EBE6 File Offset: 0x0006CDE6
		protected ObjectSecurity(bool isContainer, ResourceType resourceType, string name, AccessControlSections includeSections)
			: base(isContainer, resourceType, name, includeSections, null, null)
		{
		}

		// Token: 0x06001FCD RID: 8141 RVA: 0x0006EBF5 File Offset: 0x0006CDF5
		protected ObjectSecurity(bool isContainer, ResourceType resourceType, string name, AccessControlSections includeSections, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext)
			: base(isContainer, resourceType, name, includeSections, exceptionFromErrorCode, exceptionContext)
		{
		}

		// Token: 0x06001FCE RID: 8142 RVA: 0x0006EC06 File Offset: 0x0006CE06
		[SecuritySafeCritical]
		protected ObjectSecurity(bool isContainer, ResourceType resourceType, SafeHandle safeHandle, AccessControlSections includeSections)
			: base(isContainer, resourceType, safeHandle, includeSections, null, null)
		{
		}

		// Token: 0x06001FCF RID: 8143 RVA: 0x0006EC15 File Offset: 0x0006CE15
		[SecuritySafeCritical]
		protected ObjectSecurity(bool isContainer, ResourceType resourceType, SafeHandle safeHandle, AccessControlSections includeSections, NativeObjectSecurity.ExceptionFromErrorCode exceptionFromErrorCode, object exceptionContext)
			: base(isContainer, resourceType, safeHandle, includeSections, exceptionFromErrorCode, exceptionContext)
		{
		}

		// Token: 0x06001FD0 RID: 8144 RVA: 0x0006EC26 File Offset: 0x0006CE26
		public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new AccessRule<T>(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, type);
		}

		// Token: 0x06001FD1 RID: 8145 RVA: 0x0006EC36 File Offset: 0x0006CE36
		public override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new AuditRule<T>(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, flags);
		}

		// Token: 0x06001FD2 RID: 8146 RVA: 0x0006EC48 File Offset: 0x0006CE48
		private AccessControlSections GetAccessControlSectionsFromChanges()
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

		// Token: 0x06001FD3 RID: 8147 RVA: 0x0006EC88 File Offset: 0x0006CE88
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		protected internal void Persist(SafeHandle handle)
		{
			base.WriteLock();
			try
			{
				AccessControlSections accessControlSectionsFromChanges = this.GetAccessControlSectionsFromChanges();
				base.Persist(handle, accessControlSectionsFromChanges);
				base.OwnerModified = (base.GroupModified = (base.AuditRulesModified = (base.AccessRulesModified = false)));
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		// Token: 0x06001FD4 RID: 8148 RVA: 0x0006ECE8 File Offset: 0x0006CEE8
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		protected internal void Persist(string name)
		{
			base.WriteLock();
			try
			{
				AccessControlSections accessControlSectionsFromChanges = this.GetAccessControlSectionsFromChanges();
				base.Persist(name, accessControlSectionsFromChanges);
				base.OwnerModified = (base.GroupModified = (base.AuditRulesModified = (base.AccessRulesModified = false)));
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		// Token: 0x06001FD5 RID: 8149 RVA: 0x0006ED48 File Offset: 0x0006CF48
		public virtual void AddAccessRule(AccessRule<T> rule)
		{
			base.AddAccessRule(rule);
		}

		// Token: 0x06001FD6 RID: 8150 RVA: 0x0006ED51 File Offset: 0x0006CF51
		public virtual void SetAccessRule(AccessRule<T> rule)
		{
			base.SetAccessRule(rule);
		}

		// Token: 0x06001FD7 RID: 8151 RVA: 0x0006ED5A File Offset: 0x0006CF5A
		public virtual void ResetAccessRule(AccessRule<T> rule)
		{
			base.ResetAccessRule(rule);
		}

		// Token: 0x06001FD8 RID: 8152 RVA: 0x0006ED63 File Offset: 0x0006CF63
		public virtual bool RemoveAccessRule(AccessRule<T> rule)
		{
			return base.RemoveAccessRule(rule);
		}

		// Token: 0x06001FD9 RID: 8153 RVA: 0x0006ED6C File Offset: 0x0006CF6C
		public virtual void RemoveAccessRuleAll(AccessRule<T> rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x0006ED75 File Offset: 0x0006CF75
		public virtual void RemoveAccessRuleSpecific(AccessRule<T> rule)
		{
			base.RemoveAccessRuleSpecific(rule);
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x0006ED7E File Offset: 0x0006CF7E
		public virtual void AddAuditRule(AuditRule<T> rule)
		{
			base.AddAuditRule(rule);
		}

		// Token: 0x06001FDC RID: 8156 RVA: 0x0006ED87 File Offset: 0x0006CF87
		public virtual void SetAuditRule(AuditRule<T> rule)
		{
			base.SetAuditRule(rule);
		}

		// Token: 0x06001FDD RID: 8157 RVA: 0x0006ED90 File Offset: 0x0006CF90
		public virtual bool RemoveAuditRule(AuditRule<T> rule)
		{
			return base.RemoveAuditRule(rule);
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x0006ED99 File Offset: 0x0006CF99
		public virtual void RemoveAuditRuleAll(AuditRule<T> rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x0006EDA2 File Offset: 0x0006CFA2
		public virtual void RemoveAuditRuleSpecific(AuditRule<T> rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001FE0 RID: 8160 RVA: 0x0006EDAB File Offset: 0x0006CFAB
		public override Type AccessRightType
		{
			get
			{
				return typeof(T);
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06001FE1 RID: 8161 RVA: 0x0006EDB7 File Offset: 0x0006CFB7
		public override Type AccessRuleType
		{
			get
			{
				return typeof(AccessRule<T>);
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06001FE2 RID: 8162 RVA: 0x0006EDC3 File Offset: 0x0006CFC3
		public override Type AuditRuleType
		{
			get
			{
				return typeof(AuditRule<T>);
			}
		}
	}
}
