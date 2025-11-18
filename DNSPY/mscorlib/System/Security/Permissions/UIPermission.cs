using System;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Permissions
{
	// Token: 0x0200030E RID: 782
	[ComVisible(true)]
	[Serializable]
	public sealed class UIPermission : CodeAccessPermission, IUnrestrictedPermission, IBuiltInPermission
	{
		// Token: 0x06002777 RID: 10103 RVA: 0x0008F45E File Offset: 0x0008D65E
		public UIPermission(PermissionState state)
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

		// Token: 0x06002778 RID: 10104 RVA: 0x0008F492 File Offset: 0x0008D692
		public UIPermission(UIPermissionWindow windowFlag, UIPermissionClipboard clipboardFlag)
		{
			UIPermission.VerifyWindowFlag(windowFlag);
			UIPermission.VerifyClipboardFlag(clipboardFlag);
			this.m_windowFlag = windowFlag;
			this.m_clipboardFlag = clipboardFlag;
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x0008F4B4 File Offset: 0x0008D6B4
		public UIPermission(UIPermissionWindow windowFlag)
		{
			UIPermission.VerifyWindowFlag(windowFlag);
			this.m_windowFlag = windowFlag;
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x0008F4C9 File Offset: 0x0008D6C9
		public UIPermission(UIPermissionClipboard clipboardFlag)
		{
			UIPermission.VerifyClipboardFlag(clipboardFlag);
			this.m_clipboardFlag = clipboardFlag;
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x0600277C RID: 10108 RVA: 0x0008F4ED File Offset: 0x0008D6ED
		// (set) Token: 0x0600277B RID: 10107 RVA: 0x0008F4DE File Offset: 0x0008D6DE
		public UIPermissionWindow Window
		{
			get
			{
				return this.m_windowFlag;
			}
			set
			{
				UIPermission.VerifyWindowFlag(value);
				this.m_windowFlag = value;
			}
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x0600277E RID: 10110 RVA: 0x0008F504 File Offset: 0x0008D704
		// (set) Token: 0x0600277D RID: 10109 RVA: 0x0008F4F5 File Offset: 0x0008D6F5
		public UIPermissionClipboard Clipboard
		{
			get
			{
				return this.m_clipboardFlag;
			}
			set
			{
				UIPermission.VerifyClipboardFlag(value);
				this.m_clipboardFlag = value;
			}
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x0008F50C File Offset: 0x0008D70C
		private static void VerifyWindowFlag(UIPermissionWindow flag)
		{
			if (flag < UIPermissionWindow.NoWindows || flag > UIPermissionWindow.AllWindows)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { (int)flag }));
			}
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x0008F535 File Offset: 0x0008D735
		private static void VerifyClipboardFlag(UIPermissionClipboard flag)
		{
			if (flag < UIPermissionClipboard.NoClipboard || flag > UIPermissionClipboard.AllClipboard)
			{
				throw new ArgumentException(Environment.GetResourceString("Arg_EnumIllegalVal", new object[] { (int)flag }));
			}
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x0008F55E File Offset: 0x0008D75E
		private void Reset()
		{
			this.m_windowFlag = UIPermissionWindow.NoWindows;
			this.m_clipboardFlag = UIPermissionClipboard.NoClipboard;
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x0008F56E File Offset: 0x0008D76E
		private void SetUnrestricted(bool unrestricted)
		{
			if (unrestricted)
			{
				this.m_windowFlag = UIPermissionWindow.AllWindows;
				this.m_clipboardFlag = UIPermissionClipboard.AllClipboard;
			}
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x0008F581 File Offset: 0x0008D781
		public bool IsUnrestricted()
		{
			return this.m_windowFlag == UIPermissionWindow.AllWindows && this.m_clipboardFlag == UIPermissionClipboard.AllClipboard;
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x0008F598 File Offset: 0x0008D798
		public override bool IsSubsetOf(IPermission target)
		{
			if (target == null)
			{
				return this.m_windowFlag == UIPermissionWindow.NoWindows && this.m_clipboardFlag == UIPermissionClipboard.NoClipboard;
			}
			bool flag;
			try
			{
				UIPermission uipermission = (UIPermission)target;
				if (uipermission.IsUnrestricted())
				{
					flag = true;
				}
				else if (this.IsUnrestricted())
				{
					flag = false;
				}
				else
				{
					flag = this.m_windowFlag <= uipermission.m_windowFlag && this.m_clipboardFlag <= uipermission.m_clipboardFlag;
				}
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_WrongType", new object[] { base.GetType().FullName }));
			}
			return flag;
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x0008F638 File Offset: 0x0008D838
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
			UIPermission uipermission = (UIPermission)target;
			UIPermissionWindow uipermissionWindow = ((this.m_windowFlag < uipermission.m_windowFlag) ? this.m_windowFlag : uipermission.m_windowFlag);
			UIPermissionClipboard uipermissionClipboard = ((this.m_clipboardFlag < uipermission.m_clipboardFlag) ? this.m_clipboardFlag : uipermission.m_clipboardFlag);
			if (uipermissionWindow == UIPermissionWindow.NoWindows && uipermissionClipboard == UIPermissionClipboard.NoClipboard)
			{
				return null;
			}
			return new UIPermission(uipermissionWindow, uipermissionClipboard);
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x0008F6C8 File Offset: 0x0008D8C8
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
			UIPermission uipermission = (UIPermission)target;
			UIPermissionWindow uipermissionWindow = ((this.m_windowFlag > uipermission.m_windowFlag) ? this.m_windowFlag : uipermission.m_windowFlag);
			UIPermissionClipboard uipermissionClipboard = ((this.m_clipboardFlag > uipermission.m_clipboardFlag) ? this.m_clipboardFlag : uipermission.m_clipboardFlag);
			if (uipermissionWindow == UIPermissionWindow.NoWindows && uipermissionClipboard == UIPermissionClipboard.NoClipboard)
			{
				return null;
			}
			return new UIPermission(uipermissionWindow, uipermissionClipboard);
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x0008F75C File Offset: 0x0008D95C
		public override IPermission Copy()
		{
			return new UIPermission(this.m_windowFlag, this.m_clipboardFlag);
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x0008F770 File Offset: 0x0008D970
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = CodeAccessPermission.CreatePermissionElement(this, "System.Security.Permissions.UIPermission");
			if (!this.IsUnrestricted())
			{
				if (this.m_windowFlag != UIPermissionWindow.NoWindows)
				{
					securityElement.AddAttribute("Window", Enum.GetName(typeof(UIPermissionWindow), this.m_windowFlag));
				}
				if (this.m_clipboardFlag != UIPermissionClipboard.NoClipboard)
				{
					securityElement.AddAttribute("Clipboard", Enum.GetName(typeof(UIPermissionClipboard), this.m_clipboardFlag));
				}
			}
			else
			{
				securityElement.AddAttribute("Unrestricted", "true");
			}
			return securityElement;
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x0008F800 File Offset: 0x0008DA00
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.ValidateElement(esd, this);
			if (XMLUtil.IsUnrestricted(esd))
			{
				this.SetUnrestricted(true);
				return;
			}
			this.m_windowFlag = UIPermissionWindow.NoWindows;
			this.m_clipboardFlag = UIPermissionClipboard.NoClipboard;
			string text = esd.Attribute("Window");
			if (text != null)
			{
				this.m_windowFlag = (UIPermissionWindow)Enum.Parse(typeof(UIPermissionWindow), text);
			}
			string text2 = esd.Attribute("Clipboard");
			if (text2 != null)
			{
				this.m_clipboardFlag = (UIPermissionClipboard)Enum.Parse(typeof(UIPermissionClipboard), text2);
			}
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x0008F886 File Offset: 0x0008DA86
		int IBuiltInPermission.GetTokenIndex()
		{
			return UIPermission.GetTokenIndex();
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x0008F88D File Offset: 0x0008DA8D
		internal static int GetTokenIndex()
		{
			return 7;
		}

		// Token: 0x04000F54 RID: 3924
		private UIPermissionWindow m_windowFlag;

		// Token: 0x04000F55 RID: 3925
		private UIPermissionClipboard m_clipboardFlag;
	}
}
