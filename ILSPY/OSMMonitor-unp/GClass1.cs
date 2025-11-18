using System;
using System.Runtime.InteropServices;

public static class GClass1
{
	private const int int_0 = 64;

	[DllImport("user32", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
	private static extern IntPtr CallWindowProcW([In] byte[] byte_0, IntPtr intptr_0, int int_1, [In][Out] byte[] byte_1, IntPtr intptr_1);

	[DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool VirtualProtect([In] byte[] byte_0, IntPtr intptr_0, int int_1, out int int_2);

	public static string smethod_0()
	{
		byte[] byte_ = new byte[8];
		if (smethod_1(ref byte_))
		{
			return $"{BitConverter.ToUInt32(byte_, 4):X8}{BitConverter.ToUInt32(byte_, 0):X8}";
		}
		return "ND";
	}

	private static bool smethod_1(ref byte[] byte_0)
	{
		byte[] array = new byte[26]
		{
			85, 137, 229, 87, 139, 125, 16, 106, 1, 88,
			83, 15, 162, 137, 7, 137, 87, 4, 91, 95,
			137, 236, 93, 194, 16, 0
		};
		byte[] array2 = new byte[19]
		{
			83, 72, 199, 192, 1, 0, 0, 0, 15, 162,
			65, 137, 0, 65, 137, 80, 4, 91, 195
		};
		byte[] array3 = (smethod_2() ? array2 : array);
		IntPtr intptr_ = new IntPtr(array3.Length);
		if (!VirtualProtect(array3, intptr_, 64, out var _))
		{
			Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
		}
		return CallWindowProcW(intptr_1: new IntPtr(byte_0.Length), byte_0: array3, intptr_0: IntPtr.Zero, int_1: 0, byte_1: byte_0) != IntPtr.Zero;
	}

	private static bool smethod_2()
	{
		return IntPtr.Size == 8;
	}
}
