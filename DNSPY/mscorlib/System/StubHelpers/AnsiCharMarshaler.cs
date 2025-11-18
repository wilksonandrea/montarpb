using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace System.StubHelpers
{
	// Token: 0x0200058C RID: 1420
	[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
	internal static class AnsiCharMarshaler
	{
		// Token: 0x060042C5 RID: 17093 RVA: 0x000F8F40 File Offset: 0x000F7140
		[SecurityCritical]
		internal unsafe static byte[] DoAnsiConversion(string str, bool fBestFit, bool fThrowOnUnmappableChar, out int cbLength)
		{
			byte[] array = new byte[(str.Length + 1) * Marshal.SystemMaxDBCSCharSize];
			byte[] array2;
			byte* ptr;
			if ((array2 = array) == null || array2.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array2[0];
			}
			cbLength = str.ConvertToAnsi(ptr, array.Length, fBestFit, fThrowOnUnmappableChar);
			array2 = null;
			return array;
		}

		// Token: 0x060042C6 RID: 17094 RVA: 0x000F8F8C File Offset: 0x000F718C
		[SecurityCritical]
		internal unsafe static byte ConvertToNative(char managedChar, bool fBestFit, bool fThrowOnUnmappableChar)
		{
			int num = 2 * Marshal.SystemMaxDBCSCharSize;
			byte* ptr = stackalloc byte[(UIntPtr)num];
			int num2 = managedChar.ToString().ConvertToAnsi(ptr, num, fBestFit, fThrowOnUnmappableChar);
			return *ptr;
		}

		// Token: 0x060042C7 RID: 17095 RVA: 0x000F8FBC File Offset: 0x000F71BC
		internal static char ConvertToManaged(byte nativeChar)
		{
			byte[] array = new byte[] { nativeChar };
			string @string = Encoding.Default.GetString(array);
			return @string[0];
		}
	}
}
