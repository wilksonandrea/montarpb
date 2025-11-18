using System;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x020005A7 RID: 1447
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class HResultExceptionMarshaler
	{
		// Token: 0x0600432C RID: 17196 RVA: 0x000FA036 File Offset: 0x000F8236
		internal static int ConvertToNative(Exception ex)
		{
			if (!Environment.IsWinRTSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
			}
			if (ex == null)
			{
				return 0;
			}
			return ex._HResult;
		}

		// Token: 0x0600432D RID: 17197 RVA: 0x000FA05C File Offset: 0x000F825C
		[SecuritySafeCritical]
		internal static Exception ConvertToManaged(int hr)
		{
			if (!Environment.IsWinRTSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
			}
			Exception ex = null;
			if (hr < 0)
			{
				ex = StubHelpers.InternalGetCOMHRExceptionObject(hr, IntPtr.Zero, null, true);
			}
			return ex;
		}
	}
}
