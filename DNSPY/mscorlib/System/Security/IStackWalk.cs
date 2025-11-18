using System;
using System.Runtime.InteropServices;

namespace System.Security
{
	// Token: 0x020001D5 RID: 469
	[ComVisible(true)]
	public interface IStackWalk
	{
		// Token: 0x06001C7B RID: 7291
		void Assert();

		// Token: 0x06001C7C RID: 7292
		void Demand();

		// Token: 0x06001C7D RID: 7293
		void Deny();

		// Token: 0x06001C7E RID: 7294
		void PermitOnly();
	}
}
