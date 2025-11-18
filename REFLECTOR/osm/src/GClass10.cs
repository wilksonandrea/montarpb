using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

public class GClass10
{
    public const int int_0 = 0x200;
    public const int int_1 = 0x201;
    public const int int_2 = 0x202;
    public const int int_3 = 0x204;
    public const int int_4 = 0x203;
    public const int int_5 = 0x2a3;
    public const int int_6 = 15;
    public const int int_7 = 20;
    public const int int_8 = 0x317;
    public const int int_9 = 0x114;
    public const int int_10 = 0x115;
    public const int int_11 = 0xb0;
    public const int int_12 = 0xbb;
    public const int int_13 = 0xc9;
    public const int int_14 = 0xd6;
    public const int int_15 = 0x318;
    public const long long_0 = 1L;
    public const long long_1 = 2L;
    public const long long_2 = 4L;
    public const long long_3 = 8L;
    public const long long_4 = 0x10L;
    public const long long_5 = 0x20L;

    [DllImport("USER32.DLL")]
    public static extern uint GetCaretBlinkTime();
    [DllImport("USER32.DLL")]
    public static extern bool PostMessage(IntPtr intptr_0, uint uint_0, IntPtr intptr_1, IntPtr intptr_2);
    [DllImport("USER32.DLL")]
    public static extern int SendMessage(IntPtr intptr_0, int int_16, IntPtr intptr_1, IntPtr intptr_2);
    public static bool smethod_0(Control control_0, ref Bitmap bitmap_0)
    {
        Graphics graphics = Graphics.FromImage(bitmap_0);
        IntPtr ptr = new IntPtr(12);
        IntPtr hdc = graphics.GetHdc();
        SendMessage(control_0.Handle, 0x317, hdc, ptr);
        graphics.ReleaseHdc(hdc);
        graphics.Dispose();
        return true;
    }

    private enum Enum3 : long
    {
        PRF_CHECKVISIBLE = 1L,
        PRF_NONCLIENT = 2L,
        PRF_CLIENT = 4L,
        PRF_ERASEBKGND = 8L,
        PRF_CHILDREN = 0x10L,
        PRF_OWNED = 0x20L
    }
}

