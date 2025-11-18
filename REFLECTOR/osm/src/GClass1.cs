using System;
using System.Runtime.InteropServices;

public static class GClass1
{
    private const int int_0 = 0x40;

    [DllImport("user32", CharSet=CharSet.Unicode, SetLastError=true, ExactSpelling=true)]
    private static extern IntPtr CallWindowProcW([In] byte[] byte_0, IntPtr intptr_0, int int_1, [In, Out] byte[] byte_1, IntPtr intptr_1);
    public static string smethod_0()
    {
        byte[] buffer = new byte[8];
        return (!smethod_1(ref buffer) ? "ND" : $"{BitConverter.ToUInt32(buffer, 4):X8}{BitConverter.ToUInt32(buffer, 0):X8}");
    }

    private static bool smethod_1(ref byte[] byte_0)
    {
        int num;
        byte[] buffer = new byte[] { 
            0x55, 0x89, 0xe5, 0x57, 0x8b, 0x7d, 0x10, 0x6a, 1, 0x58, 0x53, 15, 0xa2, 0x89, 7, 0x89,
            0x57, 4, 0x5b, 0x5f, 0x89, 0xec, 0x5d, 0xc2, 0x10, 0
        };
        byte[] buffer2 = new byte[] { 
            0x53, 0x48, 0xc7, 0xc0, 1, 0, 0, 0, 15, 0xa2, 0x41, 0x89, 0, 0x41, 0x89, 80,
            4, 0x5b, 0xc3
        };
        byte[] buffer3 = smethod_2() ? buffer2 : buffer;
        IntPtr ptr = new IntPtr(buffer3.Length);
        if (!VirtualProtect(buffer3, ptr, 0x40, out num))
        {
            Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
        }
        ptr = new IntPtr(byte_0.Length);
        return (CallWindowProcW(buffer3, IntPtr.Zero, 0, byte_0, ptr) != IntPtr.Zero);
    }

    private static bool smethod_2() => 
        IntPtr.Size == 8;

    [return: MarshalAs(UnmanagedType.Bool)]
    [DllImport("kernel32", CharSet=CharSet.Unicode, SetLastError=true)]
    public static extern bool VirtualProtect([In] byte[] byte_0, IntPtr intptr_0, int int_1, out int int_2);
}

