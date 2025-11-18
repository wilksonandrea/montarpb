using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002E5 RID: 741
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class HostProtectionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06002620 RID: 9760 RVA: 0x0008B647 File Offset: 0x00089847
		public HostProtectionAttribute()
			: base(SecurityAction.LinkDemand)
		{
		}

		// Token: 0x06002621 RID: 9761 RVA: 0x0008B650 File Offset: 0x00089850
		public HostProtectionAttribute(SecurityAction action)
			: base(action)
		{
			if (action != SecurityAction.LinkDemand)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidFlag"));
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x06002622 RID: 9762 RVA: 0x0008B66D File Offset: 0x0008986D
		// (set) Token: 0x06002623 RID: 9763 RVA: 0x0008B675 File Offset: 0x00089875
		public HostProtectionResource Resources
		{
			get
			{
				return this.m_resources;
			}
			set
			{
				this.m_resources = value;
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06002624 RID: 9764 RVA: 0x0008B67E File Offset: 0x0008987E
		// (set) Token: 0x06002625 RID: 9765 RVA: 0x0008B68B File Offset: 0x0008988B
		public bool Synchronization
		{
			get
			{
				return (this.m_resources & HostProtectionResource.Synchronization) > HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.Synchronization) : (this.m_resources & ~HostProtectionResource.Synchronization));
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06002626 RID: 9766 RVA: 0x0008B6A9 File Offset: 0x000898A9
		// (set) Token: 0x06002627 RID: 9767 RVA: 0x0008B6B6 File Offset: 0x000898B6
		public bool SharedState
		{
			get
			{
				return (this.m_resources & HostProtectionResource.SharedState) > HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.SharedState) : (this.m_resources & ~HostProtectionResource.SharedState));
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06002628 RID: 9768 RVA: 0x0008B6D4 File Offset: 0x000898D4
		// (set) Token: 0x06002629 RID: 9769 RVA: 0x0008B6E1 File Offset: 0x000898E1
		public bool ExternalProcessMgmt
		{
			get
			{
				return (this.m_resources & HostProtectionResource.ExternalProcessMgmt) > HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.ExternalProcessMgmt) : (this.m_resources & ~HostProtectionResource.ExternalProcessMgmt));
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x0600262A RID: 9770 RVA: 0x0008B6FF File Offset: 0x000898FF
		// (set) Token: 0x0600262B RID: 9771 RVA: 0x0008B70C File Offset: 0x0008990C
		public bool SelfAffectingProcessMgmt
		{
			get
			{
				return (this.m_resources & HostProtectionResource.SelfAffectingProcessMgmt) > HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.SelfAffectingProcessMgmt) : (this.m_resources & ~HostProtectionResource.SelfAffectingProcessMgmt));
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x0600262C RID: 9772 RVA: 0x0008B72A File Offset: 0x0008992A
		// (set) Token: 0x0600262D RID: 9773 RVA: 0x0008B738 File Offset: 0x00089938
		public bool ExternalThreading
		{
			get
			{
				return (this.m_resources & HostProtectionResource.ExternalThreading) > HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.ExternalThreading) : (this.m_resources & ~HostProtectionResource.ExternalThreading));
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x0600262E RID: 9774 RVA: 0x0008B757 File Offset: 0x00089957
		// (set) Token: 0x0600262F RID: 9775 RVA: 0x0008B765 File Offset: 0x00089965
		public bool SelfAffectingThreading
		{
			get
			{
				return (this.m_resources & HostProtectionResource.SelfAffectingThreading) > HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.SelfAffectingThreading) : (this.m_resources & ~HostProtectionResource.SelfAffectingThreading));
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06002630 RID: 9776 RVA: 0x0008B784 File Offset: 0x00089984
		// (set) Token: 0x06002631 RID: 9777 RVA: 0x0008B792 File Offset: 0x00089992
		[ComVisible(true)]
		public bool SecurityInfrastructure
		{
			get
			{
				return (this.m_resources & HostProtectionResource.SecurityInfrastructure) > HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.SecurityInfrastructure) : (this.m_resources & ~HostProtectionResource.SecurityInfrastructure));
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06002632 RID: 9778 RVA: 0x0008B7B1 File Offset: 0x000899B1
		// (set) Token: 0x06002633 RID: 9779 RVA: 0x0008B7C2 File Offset: 0x000899C2
		public bool UI
		{
			get
			{
				return (this.m_resources & HostProtectionResource.UI) > HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.UI) : (this.m_resources & ~HostProtectionResource.UI));
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06002634 RID: 9780 RVA: 0x0008B7E7 File Offset: 0x000899E7
		// (set) Token: 0x06002635 RID: 9781 RVA: 0x0008B7F8 File Offset: 0x000899F8
		public bool MayLeakOnAbort
		{
			get
			{
				return (this.m_resources & HostProtectionResource.MayLeakOnAbort) > HostProtectionResource.None;
			}
			set
			{
				this.m_resources = (value ? (this.m_resources | HostProtectionResource.MayLeakOnAbort) : (this.m_resources & ~HostProtectionResource.MayLeakOnAbort));
			}
		}

		// Token: 0x06002636 RID: 9782 RVA: 0x0008B81D File Offset: 0x00089A1D
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new HostProtectionPermission(PermissionState.Unrestricted);
			}
			return new HostProtectionPermission(this.m_resources);
		}

		// Token: 0x04000E9E RID: 3742
		private HostProtectionResource m_resources;
	}
}
