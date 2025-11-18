using System;
using System.Security.Principal;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000891 RID: 2193
	[Serializable]
	internal class CallContextSecurityData : ICloneable
	{
		// Token: 0x17000FF7 RID: 4087
		// (get) Token: 0x06005CE2 RID: 23778 RVA: 0x00145753 File Offset: 0x00143953
		// (set) Token: 0x06005CE3 RID: 23779 RVA: 0x0014575B File Offset: 0x0014395B
		internal IPrincipal Principal
		{
			get
			{
				return this._principal;
			}
			set
			{
				this._principal = value;
			}
		}

		// Token: 0x17000FF8 RID: 4088
		// (get) Token: 0x06005CE4 RID: 23780 RVA: 0x00145764 File Offset: 0x00143964
		internal bool HasInfo
		{
			get
			{
				return this._principal != null;
			}
		}

		// Token: 0x06005CE5 RID: 23781 RVA: 0x00145770 File Offset: 0x00143970
		public object Clone()
		{
			return new CallContextSecurityData
			{
				_principal = this._principal
			};
		}

		// Token: 0x06005CE6 RID: 23782 RVA: 0x00145790 File Offset: 0x00143990
		public CallContextSecurityData()
		{
		}

		// Token: 0x040029EA RID: 10730
		private IPrincipal _principal;
	}
}
