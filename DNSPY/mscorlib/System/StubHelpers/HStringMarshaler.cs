using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;

namespace System.StubHelpers
{
	// Token: 0x02000596 RID: 1430
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class HStringMarshaler
	{
		// Token: 0x060042DE RID: 17118 RVA: 0x000F9518 File Offset: 0x000F7718
		[SecurityCritical]
		internal unsafe static IntPtr ConvertToNative(string managed)
		{
			if (!Environment.IsWinRTSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
			}
			if (managed == null)
			{
				throw new ArgumentNullException();
			}
			IntPtr intPtr;
			int num = UnsafeNativeMethods.WindowsCreateString(managed, managed.Length, &intPtr);
			Marshal.ThrowExceptionForHR(num, new IntPtr(-1));
			return intPtr;
		}

		// Token: 0x060042DF RID: 17119 RVA: 0x000F9564 File Offset: 0x000F7764
		[SecurityCritical]
		internal unsafe static IntPtr ConvertToNativeReference(string managed, [Out] HSTRING_HEADER* hstringHeader)
		{
			if (!Environment.IsWinRTSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
			}
			if (managed == null)
			{
				throw new ArgumentNullException();
			}
			char* ptr = managed;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			IntPtr intPtr;
			int num = UnsafeNativeMethods.WindowsCreateStringReference(ptr, managed.Length, hstringHeader, &intPtr);
			Marshal.ThrowExceptionForHR(num, new IntPtr(-1));
			return intPtr;
		}

		// Token: 0x060042E0 RID: 17120 RVA: 0x000F95BF File Offset: 0x000F77BF
		[SecurityCritical]
		internal static string ConvertToManaged(IntPtr hstring)
		{
			if (!Environment.IsWinRTSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_WinRT"));
			}
			return WindowsRuntimeMarshal.HStringToString(hstring);
		}

		// Token: 0x060042E1 RID: 17121 RVA: 0x000F95DE File Offset: 0x000F77DE
		[SecurityCritical]
		internal static void ClearNative(IntPtr hstring)
		{
			if (hstring != IntPtr.Zero)
			{
				UnsafeNativeMethods.WindowsDeleteString(hstring);
			}
		}
	}
}
