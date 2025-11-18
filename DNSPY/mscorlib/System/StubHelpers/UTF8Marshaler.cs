using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using Microsoft.Win32;

namespace System.StubHelpers
{
	// Token: 0x0200058E RID: 1422
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class UTF8Marshaler
	{
		// Token: 0x060042CB RID: 17099 RVA: 0x000F90C4 File Offset: 0x000F72C4
		[SecurityCritical]
		internal unsafe static IntPtr ConvertToNative(int flags, string strManaged, IntPtr pNativeBuffer)
		{
			if (strManaged == null)
			{
				return IntPtr.Zero;
			}
			StubHelpers.CheckStringLength(strManaged.Length);
			byte* ptr = (byte*)(void*)pNativeBuffer;
			int num;
			if (ptr != null)
			{
				num = (strManaged.Length + 1) * 3;
				num = strManaged.GetBytesFromEncoding(ptr, num, Encoding.UTF8);
			}
			else
			{
				num = Encoding.UTF8.GetByteCount(strManaged);
				ptr = (byte*)(void*)Marshal.AllocCoTaskMem(num + 1);
				strManaged.GetBytesFromEncoding(ptr, num, Encoding.UTF8);
			}
			ptr[num] = 0;
			return (IntPtr)((void*)ptr);
		}

		// Token: 0x060042CC RID: 17100 RVA: 0x000F9140 File Offset: 0x000F7340
		[SecurityCritical]
		internal unsafe static string ConvertToManaged(IntPtr cstr)
		{
			if (IntPtr.Zero == cstr)
			{
				return null;
			}
			int num = StubHelpers.strlen((sbyte*)(void*)cstr);
			return string.CreateStringFromEncoding((byte*)(void*)cstr, num, Encoding.UTF8);
		}

		// Token: 0x060042CD RID: 17101 RVA: 0x000F9179 File Offset: 0x000F7379
		[SecurityCritical]
		internal static void ClearNative(IntPtr pNative)
		{
			if (pNative != IntPtr.Zero)
			{
				Win32Native.CoTaskMemFree(pNative);
			}
		}

		// Token: 0x04001BD7 RID: 7127
		private const int MAX_UTF8_CHAR_SIZE = 3;
	}
}
