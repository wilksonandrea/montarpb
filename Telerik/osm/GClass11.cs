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

	public GClass11()
	{
	}

	[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false, SetLastError=true)]
	private static extern IntPtr FindWindow(string string_0, string string_1);

	[DllImport("User32.dll", CharSet=CharSet.Auto, ExactSpelling=true)]
	private static extern int GetSystemMetrics(int int_0);

	[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false)]
	private static extern bool GetWindowRect(HandleRef handleRef_0, out GClass11.Struct5 struct5_0);

	[DllImport("user32.dll", CharSet=CharSet.None, ExactSpelling=false, SetLastError=true)]
	private static extern bool SetWindowPos(IntPtr intptr_0, IntPtr intptr_1, int int_0, int int_1, int int_2, int int_3, uint uint_2);

	private static GClass11.Struct4 smethod_0()
	{
		return new GClass11.Struct4(GClass11.GetSystemMetrics(0), GClass11.GetSystemMetrics(1));
	}

	private static GClass11.Struct4 smethod_1(IntPtr intptr_0)
	{
		GClass11.Struct5 struct5;
		if (!GClass11.GetWindowRect(new HandleRef(null, intptr_0), out struct5))
		{
			CLogger.Print("Unable to get window rect!", LoggerType.Warning, null);
		}
		int int2 = struct5.int_2 - struct5.int_0;
		return new GClass11.Struct4(int2, struct5.int_3 - struct5.int_1);
	}

	public static void smethod_2()
	{
		IntPtr mainWindowHandle = Process.GetCurrentProcess().MainWindowHandle;
		if (mainWindowHandle == IntPtr.Zero)
		{
			CLogger.Print("Couldn't find a window to center!", LoggerType.Warning, null);
		}
		GClass11.Struct4 struct4 = GClass11.smethod_0();
		GClass11.Struct4 struct41 = GClass11.smethod_1(mainWindowHandle);
		int ınt320 = (struct4.Int32_0 - struct41.Int32_0) / 2;
		int ınt321 = (struct4.Int32_1 - struct41.Int32_1) / 2;
		GClass11.SetWindowPos(mainWindowHandle, IntPtr.Zero, ınt320, ınt321, 0, 0, 5);
	}

	public static void smethod_3(int int_0)
	{
		if (int_0 == 0)
		{
			return;
		}
		using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(string.Format("Select * From Win32_Process Where ParentProcessID={0}", int_0)))
		{
			foreach (ManagementObject managementObject in managementObjectSearcher.Get())
			{
				GClass11.smethod_3(Convert.ToInt32(managementObject["ProcessID"]));
			}
			try
			{
				Process.GetProcessById(int_0).Kill();
			}
			catch (ArgumentException argumentException)
			{
			}
		}
	}

	private struct Struct4
	{
		public int Int32_0
		{
			get;
			set;
		}

		public int Int32_1
		{
			get;
			set;
		}

		public Struct4(int int_2, int int_3)
		{
			this.Int32_0 = int_2;
			this.Int32_1 = int_3;
		}
	}

	private struct Struct5
	{
		public int int_0;

		public int int_1;

		public int int_2;

		public int int_3;
	}
}