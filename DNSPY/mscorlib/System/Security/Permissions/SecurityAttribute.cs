using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002EF RID: 751
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public abstract class SecurityAttribute : Attribute
	{
		// Token: 0x0600265B RID: 9819 RVA: 0x0008C170 File Offset: 0x0008A370
		protected SecurityAttribute(SecurityAction action)
		{
			this.m_action = action;
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x0600265C RID: 9820 RVA: 0x0008C17F File Offset: 0x0008A37F
		// (set) Token: 0x0600265D RID: 9821 RVA: 0x0008C187 File Offset: 0x0008A387
		public SecurityAction Action
		{
			get
			{
				return this.m_action;
			}
			set
			{
				this.m_action = value;
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x0600265E RID: 9822 RVA: 0x0008C190 File Offset: 0x0008A390
		// (set) Token: 0x0600265F RID: 9823 RVA: 0x0008C198 File Offset: 0x0008A398
		public bool Unrestricted
		{
			get
			{
				return this.m_unrestricted;
			}
			set
			{
				this.m_unrestricted = value;
			}
		}

		// Token: 0x06002660 RID: 9824
		public abstract IPermission CreatePermission();

		// Token: 0x06002661 RID: 9825 RVA: 0x0008C1A4 File Offset: 0x0008A3A4
		[SecurityCritical]
		internal static IntPtr FindSecurityAttributeTypeHandle(string typeName)
		{
			PermissionSet.s_fullTrust.Assert();
			Type type = Type.GetType(typeName, false, false);
			if (type == null)
			{
				return IntPtr.Zero;
			}
			return type.TypeHandle.Value;
		}

		// Token: 0x04000EE7 RID: 3815
		internal SecurityAction m_action;

		// Token: 0x04000EE8 RID: 3816
		internal bool m_unrestricted;
	}
}
