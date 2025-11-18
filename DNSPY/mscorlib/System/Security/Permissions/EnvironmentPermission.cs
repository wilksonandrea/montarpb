using System;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x020002DE RID: 734
	[ComVisible(true)]
	[Serializable]
	public sealed class EnvironmentPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		// Token: 0x060025BC RID: 9660 RVA: 0x000894B2 File Offset: 0x000876B2
		public EnvironmentPermission(PermissionState state)
		{
			if (state == PermissionState.Unrestricted)
			{
				this.m_unrestricted = true;
				return;
			}
			if (state == PermissionState.None)
			{
				this.m_unrestricted = false;
				return;
			}
			throw new ArgumentException(Environment.GetResourceString("Argument_InvalidPermissionState"));
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x000894E0 File Offset: 0x000876E0
		public EnvironmentPermission(EnvironmentPermissionAccess flag, string pathList)
		{
			this.SetPathList(flag, pathList);
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x000894F0 File Offset: 0x000876F0
		public void SetPathList(EnvironmentPermissionAccess flag, string pathList)
		{
			this.VerifyFlag(flag);
			this.m_unrestricted = false;
			if ((flag & EnvironmentPermissionAccess.Read) != EnvironmentPermissionAccess.NoAccess)
			{
				this.m_read = null;
			}
			if ((flag & EnvironmentPermissionAccess.Write) != EnvironmentPermissionAccess.NoAccess)
			{
				this.m_write = null;
			}
			this.AddPathList(flag, pathList);
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x00089520 File Offset: 0x00087720
		[SecuritySafeCritical]
		public void AddPathList(EnvironmentPermissionAccess flag, string pathList)
		{
			this.VerifyFlag(flag);
			if (this.FlagIsSet(flag, EnvironmentPermissionAccess.Read))
			{
				if (this.m_read == null)
				{
					this.m_read = new EnvironmentStringExpressionSet();
				}
				this.m_read.AddExpressions(pathList);
			}
			if (this.FlagIsSet(flag, EnvironmentPermissionAccess.Write))
			{
				if (this.m_write == null)
				{
					this.m_write = new EnvironmentStringExpressionSet();
				}
				this.m_write.AddExpressions(pathList);
			}
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x00089588 File Offset: 0x00087788
		public string GetPathList(EnvironmentPermissionAccess flag)
		{
			this.VerifyFlag(flag);
			this.ExclusiveFlag(flag);
			if (this.FlagIsSet(flag, EnvironmentPermissionAccess.Read))
			{
				if (this.m_read == null)
				{
					return "";
				}
				return this.m_read.ToString();
			}
			else
			{
				if (!this.FlagIsSet(flag, EnvironmentPermissionAccess.Write))
				{
					return "";
				}
				if (this.m_write == null)
				{
					return "";
				}
				return this.m_write.ToString();
			}
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x000895F0 File Offset: 0x000877F0
		private void VerifyFlag(EnvironmentPermissionAccess flag)
		{
			if ((flag & ~(EnvironmentPermissionAccess.Read | EnvironmentPermissionAccess.Write)) != EnvironmentPermissionAccess.NoAccess)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { (int)flag }));
			}
		}

		// Token: 0x060025C2 RID: 9666 RVA: 0x00089617 File Offset: 0x00087817
		private void ExclusiveFlag(EnvironmentPermissionAccess flag)
		{
			if (flag == EnvironmentPermissionAccess.NoAccess)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumNotSingleFlag"));
			}
			if ((flag & (flag - 1)) != EnvironmentPermissionAccess.NoAccess)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumNotSingleFlag"));
			}
		}

		// Token: 0x060025C3 RID: 9667 RVA: 0x00089643 File Offset: 0x00087843
		private bool FlagIsSet(EnvironmentPermissionAccess flag, EnvironmentPermissionAccess question)
		{
			return (flag & question) > EnvironmentPermissionAccess.NoAccess;
		}

		// Token: 0x060025C4 RID: 9668 RVA: 0x0008964B File Offset: 0x0008784B
		private bool IsEmpty()
		{
			return !this.m_unrestricted && (this.m_read == null || this.m_read.IsEmpty()) && (this.m_write == null || this.m_write.IsEmpty());
		}

		// Token: 0x060025C5 RID: 9669 RVA: 0x00089681 File Offset: 0x00087881
		public bool IsUnrestricted()
		{
			return this.m_unrestricted;
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x0008968C File Offset: 0x0008788C
		[SecuritySafeCritical]
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.IsEmpty();
			}
			bool flag;
			try
			{
				EnvironmentPermission environmentPermission = (EnvironmentPermission)target;
				if (environmentPermission.IsUnrestricted())
				{
					flag = true;
				}
				else if (this.IsUnrestricted())
				{
					flag = false;
				}
				else
				{
					flag = (this.m_read == null || this.m_read.IsSubsetOf(environmentPermission.m_read)) && (this.m_write == null || this.m_write.IsSubsetOf(environmentPermission.m_write));
				}
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			return flag;
		}

		// Token: 0x060025C7 RID: 9671 RVA: 0x00089738 File Offset: 0x00087938
		[SecuritySafeCritical]
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
			if (this.IsUnrestricted())
			{
				return target.Copy();
			}
			EnvironmentPermission environmentPermission = (EnvironmentPermission)target;
			if (environmentPermission.IsUnrestricted())
			{
				return this.Copy();
			}
			StringExpressionSet stringExpressionSet = ((this.m_read == null) ? null : this.m_read.Intersect(environmentPermission.m_read));
			StringExpressionSet stringExpressionSet2 = ((this.m_write == null) ? null : this.m_write.Intersect(environmentPermission.m_write));
			if ((stringExpressionSet == null || stringExpressionSet.IsEmpty()) && (stringExpressionSet2 == null || stringExpressionSet2.IsEmpty()))
			{
				return null;
			}
			return new EnvironmentPermission(PermissionState.None)
			{
				m_unrestricted = false,
				m_read = stringExpressionSet,
				m_write = stringExpressionSet2
			};
		}

		// Token: 0x060025C8 RID: 9672 RVA: 0x0008980C File Offset: 0x00087A0C
		[SecuritySafeCritical]
		public override IPermission Union(IPermission other)
		{
			if (other == null)
			{
				return this.Copy();
			}
			if (!base.VerifyType(other))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			EnvironmentPermission environmentPermission = (EnvironmentPermission)other;
			if (this.IsUnrestricted() || environmentPermission.IsUnrestricted())
			{
				return new EnvironmentPermission(PermissionState.Unrestricted);
			}
			StringExpressionSet stringExpressionSet = ((this.m_read == null) ? environmentPermission.m_read : this.m_read.Union(environmentPermission.m_read));
			StringExpressionSet stringExpressionSet2 = ((this.m_write == null) ? environmentPermission.m_write : this.m_write.Union(environmentPermission.m_write));
			if ((stringExpressionSet == null || stringExpressionSet.IsEmpty()) && (stringExpressionSet2 == null || stringExpressionSet2.IsEmpty()))
			{
				return null;
			}
			return new EnvironmentPermission(PermissionState.None)
			{
				m_unrestricted = false,
				m_read = stringExpressionSet,
				m_write = stringExpressionSet2
			};
		}

		// Token: 0x060025C9 RID: 9673 RVA: 0x000898E8 File Offset: 0x00087AE8
		public override IPermission Copy()
		{
			EnvironmentPermission environmentPermission = new EnvironmentPermission(PermissionState.None);
			if (this.m_unrestricted)
			{
				environmentPermission.m_unrestricted = true;
			}
			else
			{
				environmentPermission.m_unrestricted = false;
				if (this.m_read != null)
				{
					environmentPermission.m_read = this.m_read.Copy();
				}
				if (this.m_write != null)
				{
					environmentPermission.m_write = this.m_write.Copy();
				}
			}
			return environmentPermission;
		}

		// Token: 0x060025CA RID: 9674 RVA: 0x00089948 File Offset: 0x00087B48
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.EnvironmentPermission");
			if (!this.IsUnrestricted())
			{
				if (this.m_read != null && !this.m_read.IsEmpty())
				{
					securityElement.AddAttribute("Read", SecurityElement.Escape(this.m_read.ToString()));
				}
				if (this.m_write != null && !this.m_write.IsEmpty())
				{
					securityElement.AddAttribute("Write", SecurityElement.Escape(this.m_write.ToString()));
				}
			}
			else
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		// Token: 0x060025CB RID: 9675 RVA: 0x000899DC File Offset: 0x00087BDC
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.ValidateElement(esd, this);
			if (XMLUtil.IsUnrestricted(esd))
			{
				this.m_unrestricted = true;
				return;
			}
			this.m_unrestricted = false;
			this.m_read = null;
			this.m_write = null;
			string text = esd.Attribute("Read");
			if (text != null)
			{
				this.m_read = new EnvironmentStringExpressionSet(text);
			}
			text = esd.Attribute("Write");
			if (text != null)
			{
				this.m_write = new EnvironmentStringExpressionSet(text);
			}
		}

		// Token: 0x060025CC RID: 9676 RVA: 0x00089A4B File Offset: 0x00087C4B
		int IBuiltInPermission.GetTokenIndex()
		{
			return EnvironmentPermission.GetTokenIndex();
		}

		// Token: 0x060025CD RID: 9677 RVA: 0x00089A52 File Offset: 0x00087C52
		internal static int GetTokenIndex()
		{
			return 0;
		}

		// Token: 0x04000E74 RID: 3700
		private StringExpressionSet m_read;

		// Token: 0x04000E75 RID: 3701
		private StringExpressionSet m_write;

		// Token: 0x04000E76 RID: 3702
		private bool m_unrestricted;
	}
}
