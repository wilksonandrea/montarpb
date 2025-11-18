using System;

namespace System.Threading
{
	// Token: 0x02000546 RID: 1350
	internal struct CancellationCallbackCoreWorkArguments
	{
		// Token: 0x06003F6C RID: 16236 RVA: 0x000EBE9C File Offset: 0x000EA09C
		public CancellationCallbackCoreWorkArguments(SparselyPopulatedArrayFragment<CancellationCallbackInfo> currArrayFragment, int currArrayIndex)
		{
			this.m_currArrayFragment = currArrayFragment;
			this.m_currArrayIndex = currArrayIndex;
		}

		// Token: 0x04001AAD RID: 6829
		internal SparselyPopulatedArrayFragment<CancellationCallbackInfo> m_currArrayFragment;

		// Token: 0x04001AAE RID: 6830
		internal int m_currArrayIndex;
	}
}
