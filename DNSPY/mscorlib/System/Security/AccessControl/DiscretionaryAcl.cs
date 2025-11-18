using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x0200020F RID: 527
	public sealed class DiscretionaryAcl : CommonAcl
	{
		// Token: 0x06001EE6 RID: 7910 RVA: 0x0006CACB File Offset: 0x0006ACCB
		public DiscretionaryAcl(bool isContainer, bool isDS, int capacity)
			: this(isContainer, isDS, isDS ? GenericAcl.AclRevisionDS : GenericAcl.AclRevision, capacity)
		{
		}

		// Token: 0x06001EE7 RID: 7911 RVA: 0x0006CAE5 File Offset: 0x0006ACE5
		public DiscretionaryAcl(bool isContainer, bool isDS, byte revision, int capacity)
			: base(isContainer, isDS, revision, capacity)
		{
		}

		// Token: 0x06001EE8 RID: 7912 RVA: 0x0006CAF2 File Offset: 0x0006ACF2
		public DiscretionaryAcl(bool isContainer, bool isDS, RawAcl rawAcl)
			: this(isContainer, isDS, rawAcl, false)
		{
		}

		// Token: 0x06001EE9 RID: 7913 RVA: 0x0006CAFE File Offset: 0x0006ACFE
		internal DiscretionaryAcl(bool isContainer, bool isDS, RawAcl rawAcl, bool trusted)
			: base(isContainer, isDS, (rawAcl == null) ? new RawAcl(isDS ? GenericAcl.AclRevisionDS : GenericAcl.AclRevision, 0) : rawAcl, trusted, true)
		{
		}

		// Token: 0x06001EEA RID: 7914 RVA: 0x0006CB26 File Offset: 0x0006AD26
		public void AddAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			base.CheckAccessType(accessType);
			base.CheckFlags(inheritanceFlags, propagationFlags);
			this.everyOneFullAccessForNullDacl = false;
			base.AddQualifiedAce(sid, (accessType == AccessControlType.Allow) ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), ObjectAceFlags.None, Guid.Empty, Guid.Empty);
		}

		// Token: 0x06001EEB RID: 7915 RVA: 0x0006CB63 File Offset: 0x0006AD63
		public void SetAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			base.CheckAccessType(accessType);
			base.CheckFlags(inheritanceFlags, propagationFlags);
			this.everyOneFullAccessForNullDacl = false;
			base.SetQualifiedAce(sid, (accessType == AccessControlType.Allow) ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), ObjectAceFlags.None, Guid.Empty, Guid.Empty);
		}

		// Token: 0x06001EEC RID: 7916 RVA: 0x0006CBA0 File Offset: 0x0006ADA0
		public bool RemoveAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			base.CheckAccessType(accessType);
			this.everyOneFullAccessForNullDacl = false;
			return base.RemoveQualifiedAces(sid, (accessType == AccessControlType.Allow) ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), false, ObjectAceFlags.None, Guid.Empty, Guid.Empty);
		}

		// Token: 0x06001EED RID: 7917 RVA: 0x0006CBDF File Offset: 0x0006ADDF
		public void RemoveAccessSpecific(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags)
		{
			base.CheckAccessType(accessType);
			this.everyOneFullAccessForNullDacl = false;
			base.RemoveQualifiedAcesSpecific(sid, (accessType == AccessControlType.Allow) ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), ObjectAceFlags.None, Guid.Empty, Guid.Empty);
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x0006CC14 File Offset: 0x0006AE14
		public void AddAccess(AccessControlType accessType, SecurityIdentifier sid, ObjectAccessRule rule)
		{
			this.AddAccess(accessType, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
		}

		// Token: 0x06001EEF RID: 7919 RVA: 0x0006CC50 File Offset: 0x0006AE50
		public void AddAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			if (!base.IsDS)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OnlyValidForDS"));
			}
			base.CheckAccessType(accessType);
			base.CheckFlags(inheritanceFlags, propagationFlags);
			this.everyOneFullAccessForNullDacl = false;
			base.AddQualifiedAce(sid, (accessType == AccessControlType.Allow) ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), objectFlags, objectType, inheritedObjectType);
		}

		// Token: 0x06001EF0 RID: 7920 RVA: 0x0006CCAC File Offset: 0x0006AEAC
		public void SetAccess(AccessControlType accessType, SecurityIdentifier sid, ObjectAccessRule rule)
		{
			this.SetAccess(accessType, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
		}

		// Token: 0x06001EF1 RID: 7921 RVA: 0x0006CCE8 File Offset: 0x0006AEE8
		public void SetAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			if (!base.IsDS)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OnlyValidForDS"));
			}
			base.CheckAccessType(accessType);
			base.CheckFlags(inheritanceFlags, propagationFlags);
			this.everyOneFullAccessForNullDacl = false;
			base.SetQualifiedAce(sid, (accessType == AccessControlType.Allow) ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), objectFlags, objectType, inheritedObjectType);
		}

		// Token: 0x06001EF2 RID: 7922 RVA: 0x0006CD44 File Offset: 0x0006AF44
		public bool RemoveAccess(AccessControlType accessType, SecurityIdentifier sid, ObjectAccessRule rule)
		{
			return this.RemoveAccess(accessType, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x0006CD80 File Offset: 0x0006AF80
		public bool RemoveAccess(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			if (!base.IsDS)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OnlyValidForDS"));
			}
			base.CheckAccessType(accessType);
			this.everyOneFullAccessForNullDacl = false;
			return base.RemoveQualifiedAces(sid, (accessType == AccessControlType.Allow) ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), false, objectFlags, objectType, inheritedObjectType);
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x0006CDD4 File Offset: 0x0006AFD4
		public void RemoveAccessSpecific(AccessControlType accessType, SecurityIdentifier sid, ObjectAccessRule rule)
		{
			this.RemoveAccessSpecific(accessType, sid, rule.AccessMask, rule.InheritanceFlags, rule.PropagationFlags, rule.ObjectFlags, rule.ObjectType, rule.InheritedObjectType);
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x0006CE10 File Offset: 0x0006B010
		public void RemoveAccessSpecific(AccessControlType accessType, SecurityIdentifier sid, int accessMask, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, ObjectAceFlags objectFlags, Guid objectType, Guid inheritedObjectType)
		{
			if (!base.IsDS)
			{
				throw new InvalidOperationException(Environment.GetResourceString("InvalidOperation_OnlyValidForDS"));
			}
			base.CheckAccessType(accessType);
			this.everyOneFullAccessForNullDacl = false;
			base.RemoveQualifiedAcesSpecific(sid, (accessType == AccessControlType.Allow) ? AceQualifier.AccessAllowed : AceQualifier.AccessDenied, accessMask, GenericAce.AceFlagsFromInheritanceFlags(inheritanceFlags, propagationFlags), objectFlags, objectType, inheritedObjectType);
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06001EF6 RID: 7926 RVA: 0x0006CE61 File Offset: 0x0006B061
		// (set) Token: 0x06001EF7 RID: 7927 RVA: 0x0006CE69 File Offset: 0x0006B069
		internal bool EveryOneFullAccessForNullDacl
		{
			get
			{
				return this.everyOneFullAccessForNullDacl;
			}
			set
			{
				this.everyOneFullAccessForNullDacl = value;
			}
		}

		// Token: 0x06001EF8 RID: 7928 RVA: 0x0006CE72 File Offset: 0x0006B072
		internal override void OnAclModificationTried()
		{
			this.everyOneFullAccessForNullDacl = false;
		}

		// Token: 0x06001EF9 RID: 7929 RVA: 0x0006CE7C File Offset: 0x0006B07C
		internal static DiscretionaryAcl CreateAllowEveryoneFullAccess(bool isDS, bool isContainer)
		{
			DiscretionaryAcl discretionaryAcl = new DiscretionaryAcl(isContainer, isDS, 1);
			discretionaryAcl.AddAccess(AccessControlType.Allow, DiscretionaryAcl._sidEveryone, -1, isContainer ? (InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit) : InheritanceFlags.None, PropagationFlags.None);
			discretionaryAcl.everyOneFullAccessForNullDacl = true;
			return discretionaryAcl;
		}

		// Token: 0x06001EFA RID: 7930 RVA: 0x0006CEAF File Offset: 0x0006B0AF
		// Note: this type is marked as 'beforefieldinit'.
		static DiscretionaryAcl()
		{
		}

		// Token: 0x04000B13 RID: 2835
		private static SecurityIdentifier _sidEveryone = new SecurityIdentifier(WellKnownSidType.WorldSid, null);

		// Token: 0x04000B14 RID: 2836
		private bool everyOneFullAccessForNullDacl;
	}
}
