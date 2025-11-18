using System;
using System.Drawing;
using System.Runtime.InteropServices;

public class GClass7
{
	public GClass7()
	{
	}

	[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
	public static extern bool EnumDisplaySettings(string string_0, int int_0, ref GClass7.GStruct0 gstruct0_0);

	public static int smethod_0()
	{
		GClass7.GStruct0 gStruct0 = new GClass7.GStruct0()
		{
			short_2 = (short)Marshal.SizeOf<GClass7.GStruct0>(gStruct0),
			short_3 = 0
		};
		if (!GClass7.EnumDisplaySettings(null, -1, ref gStruct0))
		{
			return -1;
		}
		return gStruct0.int_7;
	}

	public struct GStruct0
	{
		public string string_0;

		public short short_0;

		public short short_1;

		public short short_2;

		public short short_3;

		public int int_0;

		public Point point_0;

		public int int_1;

		public int int_2;

		public short short_4;

		public short short_5;

		public short short_6;

		public short short_7;

		public short short_8;

		public string string_1;

		public short short_9;

		public int int_3;

		public int int_4;

		public int int_5;

		public int int_6;

		public int int_7;

		public int int_8;

		public int int_9;

		public int int_10;

		public int int_11;

		public int int_12;

		public int int_13;

		public int int_14;

		public int int_15;
	}
}