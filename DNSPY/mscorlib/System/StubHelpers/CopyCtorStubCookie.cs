using System;

namespace System.StubHelpers
{
	// Token: 0x020005AA RID: 1450
	internal struct CopyCtorStubCookie
	{
		// Token: 0x06004331 RID: 17201 RVA: 0x000FA0F5 File Offset: 0x000F82F5
		public void SetData(IntPtr srcInstancePtr, uint dstStackOffset, IntPtr ctorPtr, IntPtr dtorPtr)
		{
			this.m_srcInstancePtr = srcInstancePtr;
			this.m_dstStackOffset = dstStackOffset;
			this.m_ctorPtr = ctorPtr;
			this.m_dtorPtr = dtorPtr;
		}

		// Token: 0x06004332 RID: 17202 RVA: 0x000FA114 File Offset: 0x000F8314
		public void SetNext(IntPtr pNext)
		{
			this.m_pNext = pNext;
		}

		// Token: 0x04001BEB RID: 7147
		public IntPtr m_srcInstancePtr;

		// Token: 0x04001BEC RID: 7148
		public uint m_dstStackOffset;

		// Token: 0x04001BED RID: 7149
		public IntPtr m_ctorPtr;

		// Token: 0x04001BEE RID: 7150
		public IntPtr m_dtorPtr;

		// Token: 0x04001BEF RID: 7151
		public IntPtr m_pNext;
	}
}
