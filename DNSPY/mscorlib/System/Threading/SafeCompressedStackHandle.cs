using System;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Threading
{
	// Token: 0x020004F0 RID: 1264
	[SecurityCritical]
	internal class SafeCompressedStackHandle : SafeHandle
	{
		// Token: 0x06003BAE RID: 15278 RVA: 0x000E26CD File Offset: 0x000E08CD
		public SafeCompressedStackHandle()
			: base(IntPtr.Zero, true)
		{
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06003BAF RID: 15279 RVA: 0x000E26DB File Offset: 0x000E08DB
		public override bool IsInvalid
		{
			[SecurityCritical]
			get
			{
				return this.handle == IntPtr.Zero;
			}
		}

		// Token: 0x06003BB0 RID: 15280 RVA: 0x000E26ED File Offset: 0x000E08ED
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			CompressedStack.DestroyDelayedCompressedStack(this.handle);
			this.handle = IntPtr.Zero;
			return true;
		}
	}
}
