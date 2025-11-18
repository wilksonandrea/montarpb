using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using Microsoft.Win32;

namespace System.StubHelpers
{
	// Token: 0x02000590 RID: 1424
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class BSTRMarshaler
	{
		// Token: 0x060042D0 RID: 17104 RVA: 0x000F9250 File Offset: 0x000F7450
		[SecurityCritical]
		internal unsafe static IntPtr ConvertToNative(string strManaged, IntPtr pNativeBuffer)
		{
			if (strManaged == null)
			{
				return IntPtr.Zero;
			}
			StubHelpers.CheckStringLength(strManaged.Length);
			byte b;
			bool flag = strManaged.TryGetTrailByte(out b);
			uint num = (uint)(strManaged.Length * 2);
			if (flag)
			{
				num += 1U;
			}
			byte* ptr;
			if (pNativeBuffer != IntPtr.Zero)
			{
				*(int*)pNativeBuffer.ToPointer() = (int)num;
				ptr = (byte*)pNativeBuffer.ToPointer() + 4;
			}
			else
			{
				ptr = (byte*)Win32Native.SysAllocStringByteLen(null, num).ToPointer();
			}
			fixed (string text = strManaged)
			{
				char* ptr2 = text;
				if (ptr2 != null)
				{
					ptr2 += RuntimeHelpers.OffsetToStringData / 2;
				}
				Buffer.Memcpy(ptr, (byte*)ptr2, (strManaged.Length + 1) * 2);
			}
			if (flag)
			{
				ptr[num - 1U] = b;
			}
			return (IntPtr)((void*)ptr);
		}

		// Token: 0x060042D1 RID: 17105 RVA: 0x000F92FC File Offset: 0x000F74FC
		[SecurityCritical]
		internal unsafe static string ConvertToManaged(IntPtr bstr)
		{
			if (IntPtr.Zero == bstr)
			{
				return null;
			}
			uint num = Win32Native.SysStringByteLen(bstr);
			StubHelpers.CheckStringLength(num);
			string text;
			if (num == 1U)
			{
				text = string.FastAllocateString(0);
			}
			else
			{
				text = new string((char*)(void*)bstr, 0, (int)(num / 2U));
			}
			if ((num & 1U) == 1U)
			{
				text.SetTrailByte(((byte*)bstr.ToPointer())[num - 1U]);
			}
			return text;
		}

		// Token: 0x060042D2 RID: 17106 RVA: 0x000F935C File Offset: 0x000F755C
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
