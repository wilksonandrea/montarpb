using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000235 RID: 565
	public abstract class ObjectAuditRule : AuditRule
	{
		// Token: 0x06002048 RID: 8264 RVA: 0x000712E0 File Offset: 0x0006F4E0
		protected ObjectAuditRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, Guid objectType, Guid inheritedObjectType, AuditFlags auditFlags)
			: base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, auditFlags)
		{
			if (!objectType.Equals(Guid.Empty) && (accessMask & ObjectAce.AccessMaskWithObjectType) != 0)
			{
				this._objectType = objectType;
				this._objectFlags |= ObjectAceFlags.ObjectAceTypePresent;
			}
			else
			{
				this._objectType = Guid.Empty;
			}
			if (!inheritedObjectType.Equals(Guid.Empty) && (inheritanceFlags & InheritanceFlags.ContainerInherit) != InheritanceFlags.None)
			{
				this._inheritedObjectType = inheritedObjectType;
				this._objectFlags |= ObjectAceFlags.InheritedObjectAceTypePresent;
				return;
			}
			this._inheritedObjectType = Guid.Empty;
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06002049 RID: 8265 RVA: 0x0007136C File Offset: 0x0006F56C
		public Guid ObjectType
		{
			get
			{
				return this._objectType;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x0600204A RID: 8266 RVA: 0x00071374 File Offset: 0x0006F574
		public Guid InheritedObjectType
		{
			get
			{
				return this._inheritedObjectType;
			}
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x0600204B RID: 8267 RVA: 0x0007137C File Offset: 0x0006F57C
		public ObjectAceFlags ObjectFlags
		{
			get
			{
				return this._objectFlags;
			}
		}

		// Token: 0x04000BAD RID: 2989
		private readonly Guid _objectType;

		// Token: 0x04000BAE RID: 2990
		private readonly Guid _inheritedObjectType;

		// Token: 0x04000BAF RID: 2991
		private readonly ObjectAceFlags _objectFlags;
	}
}
