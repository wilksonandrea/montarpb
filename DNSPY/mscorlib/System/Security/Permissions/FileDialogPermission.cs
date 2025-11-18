using System;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x020002E0 RID: 736
	[ComVisible(true)]
	[Serializable]
	public sealed class FileDialogPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		// Token: 0x060025CE RID: 9678 RVA: 0x00089A55 File Offset: 0x00087C55
		public FileDialogPermission(PermissionState state)
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

		// Token: 0x060025CF RID: 9679 RVA: 0x00089A89 File Offset: 0x00087C89
		public FileDialogPermission(FileDialogPermissionAccess access)
		{
			FileDialogPermission.VerifyAccess(access);
			this.access = access;
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060025D0 RID: 9680 RVA: 0x00089A9E File Offset: 0x00087C9E
		// (set) Token: 0x060025D1 RID: 9681 RVA: 0x00089AA6 File Offset: 0x00087CA6
		public FileDialogPermissionAccess Access
		{
			get
			{
				return this.access;
			}
			set
			{
				FileDialogPermission.VerifyAccess(value);
				this.access = value;
			}
		}

		// Token: 0x060025D2 RID: 9682 RVA: 0x00089AB5 File Offset: 0x00087CB5
		public override IPermission Copy()
		{
			return new FileDialogPermission(this.access);
		}

		// Token: 0x060025D3 RID: 9683 RVA: 0x00089AC4 File Offset: 0x00087CC4
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.ValidateElement(esd, this);
			if (XMLUtil.IsUnrestricted(esd))
			{
				this.SetUnrestricted(true);
				return;
			}
			this.access = FileDialogPermissionAccess.None;
			string text = esd.Attribute("Access");
			if (text != null)
			{
				this.access = (FileDialogPermissionAccess)Enum.Parse(typeof(FileDialogPermissionAccess), text);
			}
		}

		// Token: 0x060025D4 RID: 9684 RVA: 0x00089B19 File Offset: 0x00087D19
		int IBuiltInPermission.GetTokenIndex()
		{
			return FileDialogPermission.GetTokenIndex();
		}

		// Token: 0x060025D5 RID: 9685 RVA: 0x00089B20 File Offset: 0x00087D20
		internal static int GetTokenIndex()
		{
			return 1;
		}

		// Token: 0x060025D6 RID: 9686 RVA: 0x00089B24 File Offset: 0x00087D24
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
			FileDialogPermission fileDialogPermission = (FileDialogPermission)target;
			FileDialogPermissionAccess fileDialogPermissionAccess = this.access & fileDialogPermission.Access;
			if (fileDialogPermissionAccess == FileDialogPermissionAccess.None)
			{
				return null;
			}
			return new FileDialogPermission(fileDialogPermissionAccess);
		}

		// Token: 0x060025D7 RID: 9687 RVA: 0x00089B84 File Offset: 0x00087D84
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.access == FileDialogPermissionAccess.None;
			}
			bool flag;
			try
			{
				FileDialogPermission fileDialogPermission = (FileDialogPermission)target;
				if (fileDialogPermission.IsUnrestricted())
				{
					flag = true;
				}
				else if (this.IsUnrestricted())
				{
					flag = false;
				}
				else
				{
					int num = (int)(this.access & FileDialogPermissionAccess.Open);
					int num2 = (int)(this.access & FileDialogPermissionAccess.Save);
					int num3 = (int)(fileDialogPermission.Access & FileDialogPermissionAccess.Open);
					int num4 = (int)(fileDialogPermission.Access & FileDialogPermissionAccess.Save);
					flag = num <= num3 && num2 <= num4;
				}
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			return flag;
		}

		// Token: 0x060025D8 RID: 9688 RVA: 0x00089C30 File Offset: 0x00087E30
		public bool IsUnrestricted()
		{
			return this.access == FileDialogPermissionAccess.OpenSave;
		}

		// Token: 0x060025D9 RID: 9689 RVA: 0x00089C3B File Offset: 0x00087E3B
		private void Reset()
		{
			this.access = FileDialogPermissionAccess.None;
		}

		// Token: 0x060025DA RID: 9690 RVA: 0x00089C44 File Offset: 0x00087E44
		private void SetUnrestricted(bool unrestricted)
		{
			if (unrestricted)
			{
				this.access = FileDialogPermissionAccess.OpenSave;
			}
		}

		// Token: 0x060025DB RID: 9691 RVA: 0x00089C50 File Offset: 0x00087E50
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.FileDialogPermission");
			if (!this.IsUnrestricted())
			{
				if (this.access != FileDialogPermissionAccess.None)
				{
					securityElement.AddAttribute("Access", Enum.GetName(typeof(FileDialogPermissionAccess), this.access));
				}
			}
			else
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		// Token: 0x060025DC RID: 9692 RVA: 0x00089CB4 File Offset: 0x00087EB4
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
			FileDialogPermission fileDialogPermission = (FileDialogPermission)target;
			return new FileDialogPermission(this.access | fileDialogPermission.Access);
		}

		// Token: 0x060025DD RID: 9693 RVA: 0x00089D11 File Offset: 0x00087F11
		private static void VerifyAccess(FileDialogPermissionAccess access)
		{
			if ((access & ~(FileDialogPermissionAccess.Open | FileDialogPermissionAccess.Save)) != FileDialogPermissionAccess.None)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { (int)access }));
			}
		}

		// Token: 0x04000E7C RID: 3708
		private FileDialogPermissionAccess access;
	}
}
