using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000233 RID: 563
	public abstract class ObjectAccessRule : AccessRule
	{
		// Token: 0x06002042 RID: 8258 RVA: 0x000711DC File Offset: 0x0006F3DC
		protected ObjectAccessRule(IdentityReference identity, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, Guid objectType, Guid inheritedObjectType, AccessControlType type)
			: base(identity, accessMask, isInherited, inheritanceFlags, propagationFlags, type)
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

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06002043 RID: 8259 RVA: 0x00071268 File Offset: 0x0006F468
		public Guid ObjectType
		{
			get
			{
				return this._objectType;
			}
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06002044 RID: 8260 RVA: 0x00071270 File Offset: 0x0006F470
		public Guid InheritedObjectType
		{
			get
			{
				return this._inheritedObjectType;
			}
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06002045 RID: 8261 RVA: 0x00071278 File Offset: 0x0006F478
		public ObjectAceFlags ObjectFlags
		{
			get
			{
				return this._objectFlags;
			}
		}

		// Token: 0x04000BA9 RID: 2985
		private readonly Guid _objectType;

		// Token: 0x04000BAA RID: 2986
		private readonly Guid _inheritedObjectType;

		// Token: 0x04000BAB RID: 2987
		private readonly ObjectAceFlags _objectFlags;
	}
}
