namespace Plugin.Core.Colorful
{
    using System;
    using System.Drawing;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct COLORREF
    {
        private uint uint_0;
        internal COLORREF(Color color_0)
        {
            this.uint_0 = (uint) ((color_0.R + (color_0.G << 8)) + (color_0.B << 0x10));
        }

        internal COLORREF(uint uint_1, uint uint_2, uint uint_3)
        {
            this.uint_0 = (uint_1 + (uint_2 << 8)) + (uint_3 << 0x10);
        }

        public override string ToString() => 
            this.uint_0.ToString();
    }
}

