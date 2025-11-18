using Plugin.Core;
using Plugin.Core.Enums;
using System;
using System.Diagnostics;
using System.Management;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

public class GClass11
{
    private const uint uint_0 = 1;
    private const uint uint_1 = 4;

    [DllImport("user32.dll", SetLastError=true)]
    private static extern IntPtr FindWindow(string string_0, string string_1);
    [DllImport("User32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
    private static extern int GetSystemMetrics(int int_0);
    [return: MarshalAs(UnmanagedType.Bool)]
    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(HandleRef handleRef_0, out Struct5 struct5_0);
    [DllImport("user32.dll", SetLastError=true)]
    private static extern bool SetWindowPos(IntPtr intptr_0, IntPtr intptr_1, int int_0, int int_1, int int_2, int int_3, uint uint_2);
    private static Struct4 smethod_0() => 
        new Struct4(GetSystemMetrics(0), GetSystemMetrics(1));

    private static Struct4 smethod_1(IntPtr intptr_0)
    {
        Struct5 struct2;
        if (!GetWindowRect(new HandleRef(null, intptr_0), out struct2))
        {
            CLogger.Print("Unable to get window rect!", LoggerType.Warning, null);
        }
        return new Struct4(struct2.int_2 - struct2.int_0, struct2.int_3 - struct2.int_1);
    }

    public static void smethod_2()
    {
        IntPtr mainWindowHandle = Process.GetCurrentProcess().MainWindowHandle;
        if (mainWindowHandle == IntPtr.Zero)
        {
            CLogger.Print("Couldn't find a window to center!", LoggerType.Warning, null);
        }
        Struct4 struct2 = smethod_0();
        Struct4 struct3 = smethod_1(mainWindowHandle);
        int num = (struct2.Int32_0 - struct3.Int32_0) / 2;
        SetWindowPos(mainWindowHandle, IntPtr.Zero, num, (struct2.Int32_1 - struct3.Int32_1) / 2, 0, 0, 5);
    }

    public static void smethod_3(int int_0)
    {
        if (int_0 != 0)
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher($"Select * From Win32_Process Where ParentProcessID={int_0}"))
            {
                foreach (ManagementObject obj2 in searcher.Get())
                {
                    smethod_3(Convert.ToInt32(obj2["ProcessID"]));
                }
                try
                {
                    Process.GetProcessById(int_0).Kill();
                }
                catch (ArgumentException)
                {
                }
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct Struct4
    {
        public int Int32_0 { get; set; }
        public int Int32_1 { get; set; }
        public Struct4(int int_2, int int_3)
        {
            this.Int32_0 = int_2;
            this.Int32_1 = int_3;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct Struct5
    {
        public int int_0;
        public int int_1;
        public int int_2;
        public int int_3;
    }
}

