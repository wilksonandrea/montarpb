using System;
using System.Drawing;
using System.Runtime.InteropServices;

// Token: 0x0200001C RID: 28
public class GClass7
{
	// Token: 0x06000090 RID: 144
	[DllImport("user32.dll")]
	public static extern bool EnumDisplaySettings(string string_0, int int_0, ref GClass7.GStruct0 gstruct0_0);

	// Token: 0x06000091 RID: 145 RVA: 0x00004F34 File Offset: 0x00003134
	public static int smethod_0()
	{
		GClass7.GStruct0 gstruct = default(GClass7.GStruct0);
		gstruct.short_2 = (short)Marshal.SizeOf<GClass7.GStruct0>(gstruct);
		gstruct.short_3 = 0;
		if (GClass7.EnumDisplaySettings(null, -1, ref gstruct))
		{
			return gstruct.int_7;
		}
		return -1;
	}

	// Token: 0x06000092 RID: 146 RVA: 0x00002133 File Offset: 0x00000333
	public GClass7()
	{
	}

	// Token: 0x0200001D RID: 29
	public struct GStruct0
	{
		// Token: 0x0400004E RID: 78
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string string_0;

		// Token: 0x0400004F RID: 79
		public short short_0;

		// Token: 0x04000050 RID: 80
		public short short_1;

		// Token: 0x04000051 RID: 81
		public short short_2;

		// Token: 0x04000052 RID: 82
		public short short_3;

		// Token: 0x04000053 RID: 83
		public int int_0;

		// Token: 0x04000054 RID: 84
		public Point point_0;

		// Token: 0x04000055 RID: 85
		public int int_1;

		// Token: 0x04000056 RID: 86
		public int int_2;

		// Token: 0x04000057 RID: 87
		public short short_4;

		// Token: 0x04000058 RID: 88
		public short short_5;

		// Token: 0x04000059 RID: 89
		public short short_6;

		// Token: 0x0400005A RID: 90
		public short short_7;

		// Token: 0x0400005B RID: 91
		public short short_8;

		// Token: 0x0400005C RID: 92
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
		public string string_1;

		// Token: 0x0400005D RID: 93
		public short short_9;

		// Token: 0x0400005E RID: 94
		public int int_3;

		// Token: 0x0400005F RID: 95
		public int int_4;

		// Token: 0x04000060 RID: 96
		public int int_5;

		// Token: 0x04000061 RID: 97
		public int int_6;

		// Token: 0x04000062 RID: 98
		public int int_7;

		// Token: 0x04000063 RID: 99
		public int int_8;

		// Token: 0x04000064 RID: 100
		public int int_9;

		// Token: 0x04000065 RID: 101
		public int int_10;

		// Token: 0x04000066 RID: 102
		public int int_11;

		// Token: 0x04000067 RID: 103
		public int int_12;

		// Token: 0x04000068 RID: 104
		public int int_13;

		// Token: 0x04000069 RID: 105
		public int int_14;

		// Token: 0x0400006A RID: 106
		public int int_15;
	}
}
