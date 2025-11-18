using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002F6 RID: 758
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class ReflectionPermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x0600269D RID: 9885 RVA: 0x0008C637 File Offset: 0x0008A837
		public ReflectionPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x0600269E RID: 9886 RVA: 0x0008C640 File Offset: 0x0008A840
		// (set) Token: 0x0600269F RID: 9887 RVA: 0x0008C648 File Offset: 0x0008A848
		public ReflectionPermissionFlag Flags
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

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x060026A0 RID: 9888 RVA: 0x0008C651 File Offset: 0x0008A851
		// (set) Token: 0x060026A1 RID: 9889 RVA: 0x0008C65E File Offset: 0x0008A85E
		[Obsolete("This API has been deprecated. http://go.microsoft.com/fwlink/?linkid=14202")]
		public bool TypeInformation
		{
			get
			{
				return (this.m_flag & ReflectionPermissionFlag.TypeInformation) > ReflectionPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | ReflectionPermissionFlag.TypeInformation) : (this.m_flag & ~ReflectionPermissionFlag.TypeInformation));
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x060026A2 RID: 9890 RVA: 0x0008C67C File Offset: 0x0008A87C
		// (set) Token: 0x060026A3 RID: 9891 RVA: 0x0008C689 File Offset: 0x0008A889
		public bool MemberAccess
		{
			get
			{
				return (this.m_flag & ReflectionPermissionFlag.MemberAccess) > ReflectionPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | ReflectionPermissionFlag.MemberAccess) : (this.m_flag & ~ReflectionPermissionFlag.MemberAccess));
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x060026A4 RID: 9892 RVA: 0x0008C6A7 File Offset: 0x0008A8A7
		// (set) Token: 0x060026A5 RID: 9893 RVA: 0x0008C6B4 File Offset: 0x0008A8B4
		[Obsolete("This permission is no longer used by the CLR.")]
		public bool ReflectionEmit
		{
			get
			{
				return (this.m_flag & ReflectionPermissionFlag.ReflectionEmit) > ReflectionPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | ReflectionPermissionFlag.ReflectionEmit) : (this.m_flag & ~ReflectionPermissionFlag.ReflectionEmit));
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x060026A6 RID: 9894 RVA: 0x0008C6D2 File Offset: 0x0008A8D2
		// (set) Token: 0x060026A7 RID: 9895 RVA: 0x0008C6DF File Offset: 0x0008A8DF
		public bool RestrictedMemberAccess
		{
			get
			{
				return (this.m_flag & ReflectionPermissionFlag.RestrictedMemberAccess) > ReflectionPermissionFlag.NoFlags;
			}
			set
			{
				this.m_flag = (value ? (this.m_flag | ReflectionPermissionFlag.RestrictedMemberAccess) : (this.m_flag & ~ReflectionPermissionFlag.RestrictedMemberAccess));
			}
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x0008C6FD File Offset: 0x0008A8FD
		public override IPermission CreatePermission()
		{
			if (this.m_unrestricted)
			{
				return new ReflectionPermission(PermissionState.Unrestricted);
			}
			return new ReflectionPermission(this.m_flag);
		}

		// Token: 0x04000EFD RID: 3837
		private ReflectionPermissionFlag m_flag;
	}
}
