using System;
using System.Runtime.InteropServices;

public static class GClass1
{
	private const int int_0 = 64;

	[DllImport("user32", CharSet=CharSet.Unicode, ExactSpelling=true, SetLastError=true)]
	private static extern IntPtr CallWindowProcW([In] byte[] byte_0, IntPtr intptr_0, int int_1, [In][Out] byte[] byte_1, IntPtr intptr_1);

	public static string smethod_0()
	{
		byte[] numArray = new byte[8];
		if (!GClass1.smethod_1(ref numArray))
		{
			return "ND";
		}
		return string.Format("{0:X8}{1:X8}", BitConverter.ToUInt32(numArray, 4), BitConverter.ToUInt32(numArray, 0));
	}

	private static bool smethod_1(ref byte[] byte_0)
	{
		int ınt32;
		byte[] numArray = new byte[] { 85, 137, 229, 87, 139, 125, 16, 106, 1, 88, 83, 15, 162, 137, 7, 137, 87, 4, 91, 95, 137, 236, 93, 194, 16, 0 };
		byte[] numArray1 = new byte[] { 83, 72, 199, 192, 1, 0, 0, 0, 15, 162, 65, 137, 0, 65, 137, 80, 4, 91, 195 };
		byte[] numArray2 = (GClass1.smethod_2() ? numArray1 : numArray);
		IntPtr ıntPtr = new IntPtr((int)numArray2.Length);
		if (!GClass1.VirtualProtect(numArray2, ıntPtr, 64, out ınt32))
		{
			Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
		}
		ıntPtr = new IntPtr((int)byte_0.Length);
		return GClass1.CallWindowProcW(numArray2, IntPtr.Zero, 0, byte_0, ıntPtr) != IntPtr.Zero;
	}

	private static bool smethod_2()
	{
		return IntPtr.Size == 8;
	}

	[DllImport("kernel32", CharSet=CharSet.Unicode, ExactSpelling=false, SetLastError=true)]
	public static extern bool VirtualProtect([In] byte[] byte_0, IntPtr intptr_0, int int_1, out int int_2);
}