using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	// Token: 0x020002FF RID: 767
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public abstract class IsolatedStoragePermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x060026FB RID: 9979 RVA: 0x0008CDEE File Offset: 0x0008AFEE
		protected IsolatedStoragePermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x060026FD RID: 9981 RVA: 0x0008CE00 File Offset: 0x0008B000
		// (set) Token: 0x060026FC RID: 9980 RVA: 0x0008CDF7 File Offset: 0x0008AFF7
		public long UserQuota
		{
			get
			{
				return this.m_userQuota;
			}
			set
			{
				this.m_userQuota = value;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x060026FF RID: 9983 RVA: 0x0008CE11 File Offset: 0x0008B011
		// (set) Token: 0x060026FE RID: 9982 RVA: 0x0008CE08 File Offset: 0x0008B008
		public IsolatedStorageContainment UsageAllowed
		{
			get
			{
				return this.m_allowed;
			}
			set
			{
				this.m_allowed = value;
			}
		}

		// Token: 0x04000F0F RID: 3855
		internal long m_userQuota;

		// Token: 0x04000F10 RID: 3856
		internal IsolatedStorageContainment m_allowed;
	}
}
