using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002F8 RID: 760
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class SecurityPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x060026B9 RID: 9913 RVA: 0x0008C855 File Offset: 0x0008AA55
		public SecurityPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x060026BA RID: 9914 RVA: 0x0008C85E File Offset: 0x0008AA5E
		// (set) Token: 0x060026BB RID: 9915 RVA: 0x0008C866 File Offset: 0x0008AA66
		public SecurityPermissionFlag Flags
		{
			get
			{
				return this.m_flag;
			}
			set
			{
				this.m_flag = value;
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x060026BC RID: 9916 RVA: 0x0008C86F File Offset: 0x0008AA6F
		// (set) Token: 0x060026BD RID: 9917 RVA: 0x0008C87C File Offset: 0x0008AA7C
		public bool Assertion
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.Assertion) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.Assertion) : (this.m_flag & ~SecurityPermissionFlag.Assertion));
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x060026BE RID: 9918 RVA: 0x0008C89A File Offset: 0x0008AA9A
		// (set) Token: 0x060026BF RID: 9919 RVA: 0x0008C8A7 File Offset: 0x0008AAA7
		public bool UnmanagedCode
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.UnmanagedCode) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.UnmanagedCode) : (this.m_flag & ~SecurityPermissionFlag.UnmanagedCode));
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x060026C0 RID: 9920 RVA: 0x0008C8C5 File Offset: 0x0008AAC5
		// (set) Token: 0x060026C1 RID: 9921 RVA: 0x0008C8D2 File Offset: 0x0008AAD2
		public bool SkipVerification
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.SkipVerification) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.SkipVerification) : (this.m_flag & ~SecurityPermissionFlag.SkipVerification));
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x060026C2 RID: 9922 RVA: 0x0008C8F0 File Offset: 0x0008AAF0
		// (set) Token: 0x060026C3 RID: 9923 RVA: 0x0008C8FD File Offset: 0x0008AAFD
		public bool Execution
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.Execution) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.Execution) : (this.m_flag & ~SecurityPermissionFlag.Execution));
			}
		}

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x060026C4 RID: 9924 RVA: 0x0008C91B File Offset: 0x0008AB1B
		// (set) Token: 0x060026C5 RID: 9925 RVA: 0x0008C929 File Offset: 0x0008AB29
		public bool ControlThread
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.ControlThread) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.ControlThread) : (this.m_flag & ~SecurityPermissionFlag.ControlThread));
			}
		}

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x060026C6 RID: 9926 RVA: 0x0008C948 File Offset: 0x0008AB48
		// (set) Token: 0x060026C7 RID: 9927 RVA: 0x0008C956 File Offset: 0x0008AB56
		public bool ControlEvidence
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.ControlEvidence) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.ControlEvidence) : (this.m_flag & ~SecurityPermissionFlag.ControlEvidence));
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x060026C8 RID: 9928 RVA: 0x0008C975 File Offset: 0x0008AB75
		// (set) Token: 0x060026C9 RID: 9929 RVA: 0x0008C983 File Offset: 0x0008AB83
		public bool ControlPolicy
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.ControlPolicy) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.ControlPolicy) : (this.m_flag & ~SecurityPermissionFlag.ControlPolicy));
			}
		}

		// Token: 0x170004EC RID: 1260
		// (get) Token: 0x060026CA RID: 9930 RVA: 0x0008C9A2 File Offset: 0x0008ABA2
		// (set) Token: 0x060026CB RID: 9931 RVA: 0x0008C9B3 File Offset: 0x0008ABB3
		public bool SerializationFormatter
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.SerializationFormatter) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.SerializationFormatter) : (this.m_flag & ~SecurityPermissionFlag.SerializationFormatter));
			}
		}

		// Token: 0x170004ED RID: 1261
		// (get) Token: 0x060026CC RID: 9932 RVA: 0x0008C9D8 File Offset: 0x0008ABD8
		// (set) Token: 0x060026CD RID: 9933 RVA: 0x0008C9E9 File Offset: 0x0008ABE9
		public bool ControlDomainPolicy
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.ControlDomainPolicy) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.ControlDomainPolicy) : (this.m_flag & ~SecurityPermissionFlag.ControlDomainPolicy));
			}
		}

		// Token: 0x170004EE RID: 1262
		// (get) Token: 0x060026CE RID: 9934 RVA: 0x0008CA0E File Offset: 0x0008AC0E
		// (set) Token: 0x060026CF RID: 9935 RVA: 0x0008CA1F File Offset: 0x0008AC1F
		public bool ControlPrincipal
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.ControlPrincipal) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.ControlPrincipal) : (this.m_flag & ~SecurityPermissionFlag.ControlPrincipal));
			}
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x060026D0 RID: 9936 RVA: 0x0008CA44 File Offset: 0x0008AC44
		// (set) Token: 0x060026D1 RID: 9937 RVA: 0x0008CA55 File Offset: 0x0008AC55
		public bool ControlAppDomain
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.ControlAppDomain) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.ControlAppDomain) : (this.m_flag & ~SecurityPermissionFlag.ControlAppDomain));
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x060026D2 RID: 9938 RVA: 0x0008CA7A File Offset: 0x0008AC7A
		// (set) Token: 0x060026D3 RID: 9939 RVA: 0x0008CA8B File Offset: 0x0008AC8B
		public bool RemotingConfiguration
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.RemotingConfiguration) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.RemotingConfiguration) : (this.m_flag & ~SecurityPermissionFlag.RemotingConfiguration));
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x060026D4 RID: 9940 RVA: 0x0008CAB0 File Offset: 0x0008ACB0
		// (set) Token: 0x060026D5 RID: 9941 RVA: 0x0008CAC1 File Offset: 0x0008ACC1
		[ComVisible(true)]
		public bool Infrastructure
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.Infrastructure) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.Infrastructure) : (this.m_flag & ~SecurityPermissionFlag.Infrastructure));
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x060026D6 RID: 9942 RVA: 0x0008CAE6 File Offset: 0x0008ACE6
		// (set) Token: 0x060026D7 RID: 9943 RVA: 0x0008CAF7 File Offset: 0x0008ACF7
		public bool BindingRedirects
		{
			get
			{
				return (this.m_flag & SecurityPermissionFlag.BindingRedirects) > SecurityPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | SecurityPermissionFlag.BindingRedirects) : (this.m_flag & ~SecurityPermissionFlag.BindingRedirects));
			}
		}

		// Token: 0x060026D8 RID: 9944 RVA: 0x0008CB1C File Offset: 0x0008AD1C
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new SecurityPermission(PermissionState.Unrestricted);
			}
			return new SecurityPermission(this.m_flag);
		}

		// Token: 0x04000F03 RID: 3843
		private SecurityPermissionFlag m_flag;
	}
}
