using System;

namespace System.Security.Policy
{
	// Token: 0x02000357 RID: 855
	internal interface IDelayEvaluatedEvidence
	{
		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x06002A74 RID: 10868
		bool IsVerified
		{
			[SecurityCritical]
			get;
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06002A75 RID: 10869
		bool WasUsed { get; }

		// Token: 0x06002A76 RID: 10870
		void MarkUsed();
	}
}
