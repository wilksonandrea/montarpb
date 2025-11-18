using System;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x02000307 RID: 775
	[ComVisible(true)]
	[Serializable]
	public sealed class SecurityPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		// Token: 0x0600273A RID: 10042 RVA: 0x0008DF01 File Offset: 0x0008C101
		public SecurityPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.SetUnrestricted(true);
				return;
			}
			if (state == PermissionState.None)
			{
				this.SetUnrestricted(false);
				this.Reset();
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
		}

		// Token: 0x0600273B RID: 10043 RVA: 0x0008DF35 File Offset: 0x0008C135
		public SecurityPermission(SecurityPermissionFlag flag)
		{
			this.VerifyAccess(flag);
			this.SetUnrestricted(false);
			this.m_flags = flag;
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x0008DF52 File Offset: 0x0008C152
		private void SetUnrestricted(bool unrestricted)
		{
			if (unrestricted)
			{
				this.m_flags = SecurityPermissionFlag.AllFlags;
			}
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x0008DF62 File Offset: 0x0008C162
		private void Reset()
		{
			this.m_flags = SecurityPermissionFlag.NoFlags;
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x0600273F RID: 10047 RVA: 0x0008DF7B File Offset: 0x0008C17B
		// (set) Token: 0x0600273E RID: 10046 RVA: 0x0008DF6B File Offset: 0x0008C16B
		public SecurityPermissionFlag Flags
		{
			get
			{
				return this.m_flags;
			}
			set
			{
				this.VerifyAccess(value);
				this.m_flags = value;
			}
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x0008DF84 File Offset: 0x0008C184
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.m_flags == SecurityPermissionFlag.NoFlags;
			}
			SecurityPermission securityPermission = target as SecurityPermission;
			if (securityPermission != null)
			{
				return (this.m_flags & ~securityPermission.m_flags) == SecurityPermissionFlag.NoFlags;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
		}

		// Token: 0x06002741 RID: 10049 RVA: 0x0008DFE0 File Offset: 0x0008C1E0
		public override IPermission Union(IPermission target)
		{
			if (target == null)
			{
				return this.Copy();
			}
			if (!base.VerifyType(target))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			SecurityPermission securityPermission = (SecurityPermission)target;
			if (securityPermission.IsUnrestricted() || this.IsUnrestricted())
			{
				return new SecurityPermission(PermissionState.Unrestricted);
			}
			SecurityPermissionFlag securityPermissionFlag = this.m_flags | securityPermission.m_flags;
			return new SecurityPermission(securityPermissionFlag);
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x0008E058 File Offset: 0x0008C258
		public override IPermission Intersect(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			if (!base.VerifyType(target))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			SecurityPermission securityPermission = (SecurityPermission)target;
			SecurityPermissionFlag securityPermissionFlag;
			if (securityPermission.IsUnrestricted())
			{
				if (this.IsUnrestricted())
				{
					return new SecurityPermission(PermissionState.Unrestricted);
				}
				securityPermissionFlag = this.m_flags;
			}
			else if (this.IsUnrestricted())
			{
				securityPermissionFlag = securityPermission.m_flags;
			}
			else
			{
				securityPermissionFlag = this.m_flags & securityPermission.m_flags;
			}
			if (securityPermissionFlag == SecurityPermissionFlag.NoFlags)
			{
				return null;
			}
			return new SecurityPermission(securityPermissionFlag);
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x0008E0EA File Offset: 0x0008C2EA
		public override IPermission Copy()
		{
			if (this.IsUnrestricted())
			{
				return new SecurityPermission(PermissionState.Unrestricted);
			}
			return new SecurityPermission(this.m_flags);
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x0008E106 File Offset: 0x0008C306
		public bool IsUnrestricted()
		{
			return this.m_flags == SecurityPermissionFlag.AllFlags;
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x0008E115 File Offset: 0x0008C315
		private void VerifyAccess(SecurityPermissionFlag type)
		{
			if ((type & ~(SecurityPermissionFlag.Assertion | SecurityPermissionFlag.UnmanagedCode | SecurityPermissionFlag.SkipVerification | SecurityPermissionFlag.Execution | SecurityPermissionFlag.ControlThread | SecurityPermissionFlag.ControlEvidence | SecurityPermissionFlag.ControlPolicy | SecurityPermissionFlag.SerializationFormatter | SecurityPermissionFlag.ControlDomainPolicy | SecurityPermissionFlag.ControlPrincipal | SecurityPermissionFlag.ControlAppDomain | SecurityPermissionFlag.RemotingConfiguration | SecurityPermissionFlag.Infrastructure | SecurityPermissionFlag.BindingRedirects)) != SecurityPermissionFlag.NoFlags)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { (int)type }));
			}
		}

		// Token: 0x06002746 RID: 10054 RVA: 0x0008E140 File Offset: 0x0008C340
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.SecurityPermission");
			if (!this.IsUnrestricted())
			{
				securityElement.AddAttribute("Flags", XMLUtil.BitFieldEnumToString(typeof(SecurityPermissionFlag), this.m_flags));
			}
			else
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x0008E19C File Offset: 0x0008C39C
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.ValidateElement(esd, this);
			if (XMLUtil.IsUnrestricted(esd))
			{
				this.m_flags = SecurityPermissionFlag.AllFlags;
				return;
			}
			this.Reset();
			this.SetUnrestricted(false);
			string text = esd.Attribute("Flags");
			if (text != null)
			{
				this.m_flags = (SecurityPermissionFlag)Enum.Parse(typeof(SecurityPermissionFlag), text);
			}
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x0008E1FB File Offset: 0x0008C3FB
		int IBuiltInPermission.GetTokenIndex()
		{
			return SecurityPermission.GetTokenIndex();
		}

		// Token: 0x06002749 RID: 10057 RVA: 0x0008E202 File Offset: 0x0008C402
		internal static int GetTokenIndex()
		{
			return 6;
		}

		// Token: 0x04000F35 RID: 3893
		private SecurityPermissionFlag m_flags;

		// Token: 0x04000F36 RID: 3894
		private const string _strHeaderAssertion = "Assertion";

		// Token: 0x04000F37 RID: 3895
		private const string _strHeaderUnmanagedCode = "UnmanagedCode";

		// Token: 0x04000F38 RID: 3896
		private const string _strHeaderExecution = "Execution";

		// Token: 0x04000F39 RID: 3897
		private const string _strHeaderSkipVerification = "SkipVerification";

		// Token: 0x04000F3A RID: 3898
		private const string _strHeaderControlThread = "ControlThread";

		// Token: 0x04000F3B RID: 3899
		private const string _strHeaderControlEvidence = "ControlEvidence";

		// Token: 0x04000F3C RID: 3900
		private const string _strHeaderControlPolicy = "ControlPolicy";

		// Token: 0x04000F3D RID: 3901
		private const string _strHeaderSerializationFormatter = "SerializationFormatter";

		// Token: 0x04000F3E RID: 3902
		private const string _strHeaderControlDomainPolicy = "ControlDomainPolicy";

		// Token: 0x04000F3F RID: 3903
		private const string _strHeaderControlPrincipal = "ControlPrincipal";

		// Token: 0x04000F40 RID: 3904
		private const string _strHeaderControlAppDomain = "ControlAppDomain";
	}
}
