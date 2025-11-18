using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x0200023A RID: 570
	public sealed class CommonSecurityDescriptor : GenericSecurityDescriptor
	{
		// Token: 0x06002073 RID: 8307 RVA: 0x00071A08 File Offset: 0x0006FC08
		private void CreateFromParts(bool isContainer, bool isDS, ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, SystemAcl systemAcl, DiscretionaryAcl discretionaryAcl)
		{
			if (systemAcl != null && systemAcl.IsContainer != isContainer)
			{
				throw new ArgumentException(Environment.GetResourceString(isContainer ? "AccessControl_MustSpecifyContainerAcl" : "AccessControl_MustSpecifyLeafObjectAcl"), "systemAcl");
			}
			if (discretionaryAcl != null && discretionaryAcl.IsContainer != isContainer)
			{
				throw new ArgumentException(Environment.GetResourceString(isContainer ? "AccessControl_MustSpecifyContainerAcl" : "AccessControl_MustSpecifyLeafObjectAcl"), "discretionaryAcl");
			}
			this._isContainer = isContainer;
			if (systemAcl != null && systemAcl.IsDS != isDS)
			{
				throw new ArgumentException(Environment.GetResourceString(isDS ? "AccessControl_MustSpecifyDirectoryObjectAcl" : "AccessControl_MustSpecifyNonDirectoryObjectAcl"), "systemAcl");
			}
			if (discretionaryAcl != null && discretionaryAcl.IsDS != isDS)
			{
				throw new ArgumentException(Environment.GetResourceString(isDS ? "AccessControl_MustSpecifyDirectoryObjectAcl" : "AccessControl_MustSpecifyNonDirectoryObjectAcl"), "discretionaryAcl");
			}
			this._isDS = isDS;
			this._sacl = systemAcl;
			if (discretionaryAcl == null)
			{
				discretionaryAcl = DiscretionaryAcl.CreateAllowEveryoneFullAccess(this._isDS, this._isContainer);
			}
			this._dacl = discretionaryAcl;
			ControlFlags controlFlags = flags | ControlFlags.DiscretionaryAclPresent;
			if (systemAcl == null)
			{
				controlFlags &= ~ControlFlags.SystemAclPresent;
			}
			else
			{
				controlFlags |= ControlFlags.SystemAclPresent;
			}
			this._rawSd = new RawSecurityDescriptor(controlFlags, owner, group, (systemAcl == null) ? null : systemAcl.RawAcl, discretionaryAcl.RawAcl);
		}

		// Token: 0x06002074 RID: 8308 RVA: 0x00071B37 File Offset: 0x0006FD37
		public CommonSecurityDescriptor(bool isContainer, bool isDS, ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, SystemAcl systemAcl, DiscretionaryAcl discretionaryAcl)
		{
			this.CreateFromParts(isContainer, isDS, flags, owner, group, systemAcl, discretionaryAcl);
		}

		// Token: 0x06002075 RID: 8309 RVA: 0x00071B50 File Offset: 0x0006FD50
		private CommonSecurityDescriptor(bool isContainer, bool isDS, ControlFlags flags, SecurityIdentifier owner, SecurityIdentifier group, RawAcl systemAcl, RawAcl discretionaryAcl)
			: this(isContainer, isDS, flags, owner, group, (systemAcl == null) ? null : new SystemAcl(isContainer, isDS, systemAcl), (discretionaryAcl == null) ? null : new DiscretionaryAcl(isContainer, isDS, discretionaryAcl))
		{
		}

		// Token: 0x06002076 RID: 8310 RVA: 0x00071B8A File Offset: 0x0006FD8A
		public CommonSecurityDescriptor(bool isContainer, bool isDS, RawSecurityDescriptor rawSecurityDescriptor)
			: this(isContainer, isDS, rawSecurityDescriptor, false)
		{
		}

		// Token: 0x06002077 RID: 8311 RVA: 0x00071B98 File Offset: 0x0006FD98
		internal CommonSecurityDescriptor(bool isContainer, bool isDS, RawSecurityDescriptor rawSecurityDescriptor, bool trusted)
		{
			if (rawSecurityDescriptor == null)
			{
				throw new ArgumentNullException("rawSecurityDescriptor");
			}
			this.CreateFromParts(isContainer, isDS, rawSecurityDescriptor.ControlFlags, rawSecurityDescriptor.Owner, rawSecurityDescriptor.Group, (rawSecurityDescriptor.SystemAcl == null) ? null : new SystemAcl(isContainer, isDS, rawSecurityDescriptor.SystemAcl, trusted), (rawSecurityDescriptor.DiscretionaryAcl == null) ? null : new DiscretionaryAcl(isContainer, isDS, rawSecurityDescriptor.DiscretionaryAcl, trusted));
		}

		// Token: 0x06002078 RID: 8312 RVA: 0x00071C07 File Offset: 0x0006FE07
		public CommonSecurityDescriptor(bool isContainer, bool isDS, string sddlForm)
			: this(isContainer, isDS, new RawSecurityDescriptor(sddlForm), true)
		{
		}

		// Token: 0x06002079 RID: 8313 RVA: 0x00071C18 File Offset: 0x0006FE18
		public CommonSecurityDescriptor(bool isContainer, bool isDS, byte[] binaryForm, int offset)
			: this(isContainer, isDS, new RawSecurityDescriptor(binaryForm, offset), true)
		{
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x0600207A RID: 8314 RVA: 0x00071C2B File Offset: 0x0006FE2B
		internal sealed override GenericAcl GenericSacl
		{
			get
			{
				return this._sacl;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x0600207B RID: 8315 RVA: 0x00071C33 File Offset: 0x0006FE33
		internal sealed override GenericAcl GenericDacl
		{
			get
			{
				return this._dacl;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x0600207C RID: 8316 RVA: 0x00071C3B File Offset: 0x0006FE3B
		public bool IsContainer
		{
			get
			{
				return this._isContainer;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x0600207D RID: 8317 RVA: 0x00071C43 File Offset: 0x0006FE43
		public bool IsDS
		{
			get
			{
				return this._isDS;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x0600207E RID: 8318 RVA: 0x00071C4B File Offset: 0x0006FE4B
		public override ControlFlags ControlFlags
		{
			get
			{
				return this._rawSd.ControlFlags;
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x0600207F RID: 8319 RVA: 0x00071C58 File Offset: 0x0006FE58
		// (set) Token: 0x06002080 RID: 8320 RVA: 0x00071C65 File Offset: 0x0006FE65
		public override SecurityIdentifier Owner
		{
			get
			{
				return this._rawSd.Owner;
			}
			set
			{
				this._rawSd.Owner = value;
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06002081 RID: 8321 RVA: 0x00071C73 File Offset: 0x0006FE73
		// (set) Token: 0x06002082 RID: 8322 RVA: 0x00071C80 File Offset: 0x0006FE80
		public override SecurityIdentifier Group
		{
			get
			{
				return this._rawSd.Group;
			}
			set
			{
				this._rawSd.Group = value;
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06002083 RID: 8323 RVA: 0x00071C8E File Offset: 0x0006FE8E
		// (set) Token: 0x06002084 RID: 8324 RVA: 0x00071C98 File Offset: 0x0006FE98
		public SystemAcl SystemAcl
		{
			get
			{
				return this._sacl;
			}
			set
			{
				if (value != null)
				{
					if (value.IsContainer != this.IsContainer)
					{
						throw new ArgumentException(Environment.GetResourceString(this.IsContainer ? "AccessControl_MustSpecifyContainerAcl" : "AccessControl_MustSpecifyLeafObjectAcl"), "value");
					}
					if (value.IsDS != this.IsDS)
					{
						throw new ArgumentException(Environment.GetResourceString(this.IsDS ? "AccessControl_MustSpecifyDirectoryObjectAcl" : "AccessControl_MustSpecifyNonDirectoryObjectAcl"), "value");
					}
				}
				this._sacl = value;
				if (this._sacl != null)
				{
					this._rawSd.SystemAcl = this._sacl.RawAcl;
					this.AddControlFlags(ControlFlags.SystemAclPresent);
					return;
				}
				this._rawSd.SystemAcl = null;
				this.RemoveControlFlags(ControlFlags.SystemAclPresent);
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06002085 RID: 8325 RVA: 0x00071D4E File Offset: 0x0006FF4E
		// (set) Token: 0x06002086 RID: 8326 RVA: 0x00071D58 File Offset: 0x0006FF58
		public DiscretionaryAcl DiscretionaryAcl
		{
			get
			{
				return this._dacl;
			}
			set
			{
				if (value != null)
				{
					if (value.IsContainer != this.IsContainer)
					{
						throw new ArgumentException(Environment.GetResourceString(this.IsContainer ? "AccessControl_MustSpecifyContainerAcl" : "AccessControl_MustSpecifyLeafObjectAcl"), "value");
					}
					if (value.IsDS != this.IsDS)
					{
						throw new ArgumentException(Environment.GetResourceString(this.IsDS ? "AccessControl_MustSpecifyDirectoryObjectAcl" : "AccessControl_MustSpecifyNonDirectoryObjectAcl"), "value");
					}
				}
				if (value == null)
				{
					this._dacl = DiscretionaryAcl.CreateAllowEveryoneFullAccess(this.IsDS, this.IsContainer);
				}
				else
				{
					this._dacl = value;
				}
				this._rawSd.DiscretionaryAcl = this._dacl.RawAcl;
				this.AddControlFlags(ControlFlags.DiscretionaryAclPresent);
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06002087 RID: 8327 RVA: 0x00071E0C File Offset: 0x0007000C
		public bool IsSystemAclCanonical
		{
			get
			{
				return this.SystemAcl == null || this.SystemAcl.IsCanonical;
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06002088 RID: 8328 RVA: 0x00071E23 File Offset: 0x00070023
		public bool IsDiscretionaryAclCanonical
		{
			get
			{
				return this.DiscretionaryAcl == null || this.DiscretionaryAcl.IsCanonical;
			}
		}

		// Token: 0x06002089 RID: 8329 RVA: 0x00071E3A File Offset: 0x0007003A
		public void SetSystemAclProtection(bool isProtected, bool preserveInheritance)
		{
			if (!isProtected)
			{
				this.RemoveControlFlags(ControlFlags.SystemAclProtected);
				return;
			}
			if (!preserveInheritance && this.SystemAcl != null)
			{
				this.SystemAcl.RemoveInheritedAces();
			}
			this.AddControlFlags(ControlFlags.SystemAclProtected);
		}

		// Token: 0x0600208A RID: 8330 RVA: 0x00071E6C File Offset: 0x0007006C
		public void SetDiscretionaryAclProtection(bool isProtected, bool preserveInheritance)
		{
			if (!isProtected)
			{
				this.RemoveControlFlags(ControlFlags.DiscretionaryAclProtected);
			}
			else
			{
				if (!preserveInheritance && this.DiscretionaryAcl != null)
				{
					this.DiscretionaryAcl.RemoveInheritedAces();
				}
				this.AddControlFlags(ControlFlags.DiscretionaryAclProtected);
			}
			if (this.DiscretionaryAcl != null && this.DiscretionaryAcl.EveryOneFullAccessForNullDacl)
			{
				this.DiscretionaryAcl.EveryOneFullAccessForNullDacl = false;
			}
		}

		// Token: 0x0600208B RID: 8331 RVA: 0x00071ECB File Offset: 0x000700CB
		public void PurgeAccessControl(SecurityIdentifier sid)
		{
			if (sid == null)
			{
				throw new ArgumentNullException("sid");
			}
			if (this.DiscretionaryAcl != null)
			{
				this.DiscretionaryAcl.Purge(sid);
			}
		}

		// Token: 0x0600208C RID: 8332 RVA: 0x00071EF5 File Offset: 0x000700F5
		public void PurgeAudit(SecurityIdentifier sid)
		{
			if (sid == null)
			{
				throw new ArgumentNullException("sid");
			}
			if (this.SystemAcl != null)
			{
				this.SystemAcl.Purge(sid);
			}
		}

		// Token: 0x0600208D RID: 8333 RVA: 0x00071F1F File Offset: 0x0007011F
		public void AddDiscretionaryAcl(byte revision, int trusted)
		{
			this.DiscretionaryAcl = new DiscretionaryAcl(this.IsContainer, this.IsDS, revision, trusted);
			this.AddControlFlags(ControlFlags.DiscretionaryAclPresent);
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x00071F41 File Offset: 0x00070141
		public void AddSystemAcl(byte revision, int trusted)
		{
			this.SystemAcl = new SystemAcl(this.IsContainer, this.IsDS, revision, trusted);
			this.AddControlFlags(ControlFlags.SystemAclPresent);
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x00071F64 File Offset: 0x00070164
		internal void UpdateControlFlags(ControlFlags flagsToUpdate, ControlFlags newFlags)
		{
			ControlFlags controlFlags = newFlags | (this._rawSd.ControlFlags & ~flagsToUpdate);
			this._rawSd.SetFlags(controlFlags);
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x00071F8E File Offset: 0x0007018E
		internal void AddControlFlags(ControlFlags flags)
		{
			this._rawSd.SetFlags(this._rawSd.ControlFlags | flags);
		}

		// Token: 0x06002091 RID: 8337 RVA: 0x00071FA8 File Offset: 0x000701A8
		internal void RemoveControlFlags(ControlFlags flags)
		{
			this._rawSd.SetFlags(this._rawSd.ControlFlags & ~flags);
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06002092 RID: 8338 RVA: 0x00071FC3 File Offset: 0x000701C3
		internal bool IsSystemAclPresent
		{
			get
			{
				return (this._rawSd.ControlFlags & ControlFlags.SystemAclPresent) > ControlFlags.None;
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06002093 RID: 8339 RVA: 0x00071FD6 File Offset: 0x000701D6
		internal bool IsDiscretionaryAclPresent
		{
			get
			{
				return (this._rawSd.ControlFlags & ControlFlags.DiscretionaryAclPresent) > ControlFlags.None;
			}
		}

		// Token: 0x04000BCD RID: 3021
		private bool _isContainer;

		// Token: 0x04000BCE RID: 3022
		private bool _isDS;

		// Token: 0x04000BCF RID: 3023
		private RawSecurityDescriptor _rawSd;

		// Token: 0x04000BD0 RID: 3024
		private SystemAcl _sacl;

		// Token: 0x04000BD1 RID: 3025
		private DiscretionaryAcl _dacl;
	}
}
