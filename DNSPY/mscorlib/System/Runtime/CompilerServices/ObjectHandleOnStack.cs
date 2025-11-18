using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020008D6 RID: 2262
	internal struct ObjectHandleOnStack
	{
		// Token: 0x06005DCB RID: 24011 RVA: 0x00149726 File Offset: 0x00147926
		internal ObjectHandleOnStack(IntPtr pObject)
		{
			this.m_ptr = pObject;
		}

		// Token: 0x04002A3D RID: 10813
		private IntPtr m_ptr;
	}
}
