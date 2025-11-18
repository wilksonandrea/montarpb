using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32;

namespace System.StubHelpers
{
	// Token: 0x02000591 RID: 1425
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class VBByValStrMarshaler
	{
		// Token: 0x060042D3 RID: 17107 RVA: 0x000F9374 File Offset: 0x000F7574
		[SecurityCritical]
		internal unsafe static IntPtr ConvertToNative(string strManaged, bool fBestFit, bool fThrowOnUnmappableChar, ref int cch)
		{
			if (strManaged == null)
			{
				return IntPtr.Zero;
			}
			cch = strManaged.Length;
			StubHelpers.CheckStringLength(cch);
			int num = 4 + (cch + 1) * Marshal.SystemMaxDBCSCharSize;
			byte* ptr = (byte*)(void*)Marshal.AllocCoTaskMem(num);
			int* ptr2 = (int*)ptr;
			ptr += 4;
			if (cch == 0)
			{
				*ptr = 0;
				*ptr2 = 0;
			}
			else
			{
				int num2;
				byte[] array = AnsiCharMarshaler.DoAnsiConversion(strManaged, fBestFit, fThrowOnUnmappableChar, out num2);
				Buffer.Memcpy(ptr, 0, array, 0, num2);
				ptr[num2] = 0;
				*ptr2 = num2;
			}
			return new IntPtr((void*)ptr);
		}

		// Token: 0x060042D4 RID: 17108 RVA: 0x000F93E9 File Offset: 0x000F75E9
		[SecurityCritical]
		internal unsafe static string ConvertToManaged(IntPtr pNative, int cch)
		{
			if (IntPtr.Zero == pNative)
			{
				return null;
			}
			return new string((sbyte*)(void*)pNative, 0, cch);
		}

		// Token: 0x060042D5 RID: 17109 RVA: 0x000F9407 File Offset: 0x000F7607
		[SecurityCritical]
		internal static void ClearNative(IntPtr pNative)
		{
			if (IntPtr.Zero != pNative)
			{
				Win32Native.CoTaskMemFree((IntPtr)((long)pNative - 4L));
			}
		}
	}
}
