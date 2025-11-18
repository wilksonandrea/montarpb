using System;
using System.Security;

namespace System
{
	// Token: 0x02000135 RID: 309
	internal struct RuntimeFieldHandleInternal
	{
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06001281 RID: 4737 RVA: 0x000378B0 File Offset: 0x00035AB0
		internal static RuntimeFieldHandleInternal EmptyHandle
		{
			get
			{
				return default(RuntimeFieldHandleInternal);
			}
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x000378C6 File Offset: 0x00035AC6
		internal bool IsNullHandle()
		{
			return this.m_handle.IsNull();
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06001283 RID: 4739 RVA: 0x000378D3 File Offset: 0x00035AD3
		internal IntPtr Value
		{
			[SecurityCritical]
			get
			{
				return this.m_handle;
			}
		}

		// Token: 0x06001284 RID: 4740 RVA: 0x000378DB File Offset: 0x00035ADB
		[SecurityCritical]
		internal RuntimeFieldHandleInternal(IntPtr value)
		{
			this.m_handle = value;
		}

		// Token: 0x0400066C RID: 1644
		internal IntPtr m_handle;
	}
}
