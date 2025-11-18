using System;
using System.Security;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200001B RID: 27
	[SecurityCritical]
	internal sealed class SafeFileMappingHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600015C RID: 348 RVA: 0x0000472D File Offset: 0x0000292D
		[SecurityCritical]
		internal SafeFileMappingHandle()
			: base(true)
		{
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00004736 File Offset: 0x00002936
		[SecurityCritical]
		internal SafeFileMappingHandle(IntPtr handle, bool ownsHandle)
			: base(ownsHandle)
		{
			base.SetHandle(handle);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00004746 File Offset: 0x00002946
		[SecurityCritical]
		protected override bool ReleaseHandle()
		{
			return Win32Native.CloseHandle(this.handle);
		}
	}
}
