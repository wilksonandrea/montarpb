using System;
using System.Runtime.ConstrainedExecution;
using System.Security;
using Microsoft.Win32;

namespace System.StubHelpers
{
	// Token: 0x02000592 RID: 1426
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class AnsiBSTRMarshaler
	{
		// Token: 0x060042D6 RID: 17110 RVA: 0x000F942C File Offset: 0x000F762C
		[SecurityCritical]
		internal static IntPtr ConvertToNative(int flags, string strManaged)
		{
			if (strManaged == null)
			{
				return IntPtr.Zero;
			}
			int length = strManaged.Length;
			StubHelpers.CheckStringLength(length);
			byte[] array = null;
			int num = 0;
			if (length > 0)
			{
				array = AnsiCharMarshaler.DoAnsiConversion(strManaged, (flags & 255) != 0, flags >> 8 != 0, out num);
			}
			return Win32Native.SysAllocStringByteLen(array, (uint)num);
		}

		// Token: 0x060042D7 RID: 17111 RVA: 0x000F9477 File Offset: 0x000F7677
		[SecurityCritical]
		internal unsafe static string ConvertToManaged(IntPtr bstr)
		{
			if (IntPtr.Zero == bstr)
			{
				return null;
			}
			return new string((sbyte*)(void*)bstr);
		}

		// Token: 0x060042D8 RID: 17112 RVA: 0x000F9493 File Offset: 0x000F7693
		[SecurityCritical]
		internal static void ClearNative(IntPtr pNative)
		{
			if (IntPtr.Zero != pNative)
			{
				Win32Native.SysFreeString(pNative);
			}
		}
	}
}
