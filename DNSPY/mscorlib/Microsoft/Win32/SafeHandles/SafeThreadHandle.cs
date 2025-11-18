using System;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200002C RID: 44
	[SecurityCritical]
	internal sealed class SafeThreadHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x06000193 RID: 403 RVA: 0x00004A52 File Offset: 0x00002C52
		private SafeThreadHandle()
			: base(true)
		{
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00004A5B File Offset: 0x00002C5B
		internal SafeThreadHandle(IntPtr handle)
			: base(true)
		{
			base.SetHandle(handle);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00004A6B File Offset: 0x00002C6B
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return Win32Native.CloseHandle(this.handle);
		}
	}
}
