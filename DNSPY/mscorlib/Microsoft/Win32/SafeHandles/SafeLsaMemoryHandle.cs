using System;
using System.Runtime.InteropServices;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000028 RID: 40
	[SecurityCritical]
	internal sealed class SafeLsaMemoryHandle : SafeBuffer
	{
		// Token: 0x06000183 RID: 387 RVA: 0x0000497E File Offset: 0x00002B7E
		private SafeLsaMemoryHandle()
			: base(true)
		{
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00004987 File Offset: 0x00002B87
		internal SafeLsaMemoryHandle(IntPtr handle)
			: base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00004997 File Offset: 0x00002B97
		internal static SafeLsaMemoryHandle InvalidHandle
		{
			get
			{
				return new SafeLsaMemoryHandle(IntPtr.Zero);
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x000049A3 File Offset: 0x00002BA3
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return Win32Native.LsaFreeMemory(this.handle) == 0;
		}
	}
}
