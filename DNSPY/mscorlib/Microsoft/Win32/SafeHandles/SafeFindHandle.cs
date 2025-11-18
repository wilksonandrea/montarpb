using System;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200001C RID: 28
	[SecurityCritical]
	internal sealed class SafeFindHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600015F RID: 351 RVA: 0x00004753 File Offset: 0x00002953
		[SecurityCritical]
		internal SafeFindHandle()
			: base(true)
		{
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000475C File Offset: 0x0000295C
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return Win32Native.FindClose(this.handle);
		}
	}
}
