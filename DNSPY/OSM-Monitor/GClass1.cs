using System;
using System.Runtime.InteropServices;

// Token: 0x02000008 RID: 8
public static class GClass1
{
	// Token: 0x06000029 RID: 41
	[DllImport("user32", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
	private static extern IntPtr CallWindowProcW([In] byte[] byte_0, IntPtr intptr_0, int int_1, [In] [Out] byte[] byte_1, IntPtr intptr_1);

	// Token: 0x0600002A RID: 42
	[DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool VirtualProtect([In] byte[] byte_0, IntPtr intptr_0, int int_1, out int int_2);

	// Token: 0x0600002B RID: 43 RVA: 0x00003B78 File Offset: 0x00001D78
	public static string smethod_0()
	{
		byte[] array = new byte[8];
		if (GClass1.smethod_1(ref array))
		{
			return string.Format("{0:X8}{1:X8}", BitConverter.ToUInt32(array, 4), BitConverter.ToUInt32(array, 0));
		}
		return "ND";
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00003BC0 File Offset: 0x00001DC0
	private static bool smethod_1(ref byte[] byte_0)
	{
		byte[] array = new byte[]
		{
			85, 137, 229, 87, 139, 125, 16, 106, 1, 88,
			83, 15, 162, 137, 7, 137, 87, 4, 91, 95,
			137, 236, 93, 194, 16, 0
		};
		byte[] array2 = new byte[]
		{
			83, 72, 199, 192, 1, 0, 0, 0, 15, 162,
			65, 137, 0, 65, 137, 80, 4, 91, 195
		};
		byte[] array3 = (GClass1.smethod_2() ? array2 : array);
		IntPtr intPtr = new IntPtr(array3.Length);
		int num;
		if (!GClass1.VirtualProtect(array3, intPtr, 64, out num))
		{
			Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
		}
		intPtr = new IntPtr(byte_0.Length);
		return GClass1.CallWindowProcW(array3, IntPtr.Zero, 0, byte_0, intPtr) != IntPtr.Zero;
	}

	// Token: 0x0600002D RID: 45 RVA: 0x000021B1 File Offset: 0x000003B1
	private static bool smethod_2()
	{
		return IntPtr.Size == 8;
	}

	// Token: 0x04000014 RID: 20
	private const int int_0 = 64;
}
