using System;
using System.Security;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200080C RID: 2060
	[Serializable]
	internal class CallBackHelper
	{
		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x060058BD RID: 22717 RVA: 0x00138BF1 File Offset: 0x00136DF1
		// (set) Token: 0x060058BE RID: 22718 RVA: 0x00138BFE File Offset: 0x00136DFE
		internal bool IsEERequested
		{
			get
			{
				return (this._flags & 1) == 1;
			}
			set
			{
				if (value)
				{
					this._flags |= 1;
				}
			}
		}

		// Token: 0x17000EB5 RID: 3765
		// (set) Token: 0x060058BF RID: 22719 RVA: 0x00138C11 File Offset: 0x00136E11
		internal bool IsCrossDomain
		{
			set
			{
				if (value)
				{
					this._flags |= 256;
				}
			}
		}

		// Token: 0x060058C0 RID: 22720 RVA: 0x00138C28 File Offset: 0x00136E28
		internal CallBackHelper(IntPtr privateData, bool bFromEE, int targetDomainID)
		{
			this.IsEERequested = bFromEE;
			this.IsCrossDomain = targetDomainID != 0;
			this._privateData = privateData;
		}

		// Token: 0x060058C1 RID: 22721 RVA: 0x00138C48 File Offset: 0x00136E48
		[SecurityCritical]
		internal void Func()
		{
			if (this.IsEERequested)
			{
				Context.ExecuteCallBackInEE(this._privateData);
			}
		}

		// Token: 0x04002876 RID: 10358
		internal const int RequestedFromEE = 1;

		// Token: 0x04002877 RID: 10359
		internal const int XDomainTransition = 256;

		// Token: 0x04002878 RID: 10360
		private int _flags;

		// Token: 0x04002879 RID: 10361
		private IntPtr _privateData;
	}
}
