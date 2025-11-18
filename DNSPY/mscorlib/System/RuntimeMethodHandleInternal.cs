using System;
using System.Security;

namespace System
{
	// Token: 0x02000131 RID: 305
	internal struct RuntimeMethodHandleInternal
	{
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06001231 RID: 4657 RVA: 0x0003743C File Offset: 0x0003563C
		internal static RuntimeMethodHandleInternal EmptyHandle
		{
			get
			{
				return default(RuntimeMethodHandleInternal);
			}
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x00037452 File Offset: 0x00035652
		internal bool IsNullHandle()
		{
			return this.m_handle.IsNull();
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x0003745F File Offset: 0x0003565F
		internal IntPtr Value
		{
			[SecurityCritical]
			get
			{
				return this.m_handle;
			}
		}

		// Token: 0x06001234 RID: 4660 RVA: 0x00037467 File Offset: 0x00035667
		[SecurityCritical]
		internal RuntimeMethodHandleInternal(IntPtr value)
		{
			this.m_handle = value;
		}

		// Token: 0x04000660 RID: 1632
		internal IntPtr m_handle;
	}
}
