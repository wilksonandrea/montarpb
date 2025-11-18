using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200094C RID: 2380
	[ComVisible(true)]
	public struct HandleRef
	{
		// Token: 0x060060AC RID: 24748 RVA: 0x0014C881 File Offset: 0x0014AA81
		public HandleRef(object wrapper, IntPtr handle)
		{
			this.m_wrapper = wrapper;
			this.m_handle = handle;
		}

		// Token: 0x170010FB RID: 4347
		// (get) Token: 0x060060AD RID: 24749 RVA: 0x0014C891 File Offset: 0x0014AA91
		public object Wrapper
		{
			get
			{
				return this.m_wrapper;
			}
		}

		// Token: 0x170010FC RID: 4348
		// (get) Token: 0x060060AE RID: 24750 RVA: 0x0014C899 File Offset: 0x0014AA99
		public IntPtr Handle
		{
			get
			{
				return this.m_handle;
			}
		}

		// Token: 0x060060AF RID: 24751 RVA: 0x0014C8A1 File Offset: 0x0014AAA1
		public static explicit operator IntPtr(HandleRef value)
		{
			return value.m_handle;
		}

		// Token: 0x060060B0 RID: 24752 RVA: 0x0014C8A9 File Offset: 0x0014AAA9
		public static IntPtr ToIntPtr(HandleRef value)
		{
			return value.m_handle;
		}

		// Token: 0x04002B5A RID: 11098
		internal object m_wrapper;

		// Token: 0x04002B5B RID: 11099
		internal IntPtr m_handle;
	}
}
