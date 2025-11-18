using System;
using System.Diagnostics;
using System.Management;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Plugin.Core;
using Plugin.Core.Enums;

public class GClass11
{
	private struct Struct4
	{
		[CompilerGenerated]
		private int int_0;

		[CompilerGenerated]
		private int int_1;

		public int Int32_0
		{
			[CompilerGenerated]
			get
			{
				return int_0;
			}
			[CompilerGenerated]
			set
			{
				int_0 = value;
			}
		}

		public int Int32_1
		{
			[CompilerGenerated]
			get
			{
				return int_1;
			}
			[CompilerGenerated]
			set
			{
				int_1 = value;
			}
		}

		public Struct4(int int_2, int int_3)
		{
			Int32_0 = int_2;
			Int32_1 = int_3;
		}
	}

	private struct Struct5
	{
		public int int_0;

		public int int_1;

		public int int_2;

		public int int_3;
	}

	private const uint uint_0 = 1u;

	private const uint uint_1 = 4u;

	[DllImport("user32.dll", SetLastError = true)]
	private static extern IntPtr FindWindow(string string_0, string string_1);

	[DllImport("user32.dll", SetLastError = true)]
	private static extern bool SetWindowPos(IntPtr intptr_0, IntPtr intptr_1, int int_0, int int_1, int int_2, int int_3, uint uint_2);

	private static Struct4 smethod_0()
	{
		return new Struct4(GetSystemMetrics(0), GetSystemMetrics(1));
	}

	[DllImport("User32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
	private static extern int GetSystemMetrics(int int_0);

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool GetWindowRect(HandleRef handleRef_0, out Struct5 struct5_0);

	private static Struct4 smethod_1(IntPtr intptr_0)
	{
		if (!GetWindowRect(new HandleRef(null, intptr_0), out var struct5_))
		{
			CLogger.Print("Unable to get window rect!", LoggerType.Warning);
		}
		int int_ = struct5_.int_2 - struct5_.int_0;
		int int_2 = struct5_.int_3 - struct5_.int_1;
		return new Struct4(int_, int_2);
	}

	public static void smethod_2()
	{
		IntPtr mainWindowHandle = Process.GetCurrentProcess().MainWindowHandle;
		if (mainWindowHandle == IntPtr.Zero)
		{
			CLogger.Print("Couldn't find a window to center!", LoggerType.Warning);
		}
		Struct4 @struct = smethod_0();
		Struct4 struct2 = smethod_1(mainWindowHandle);
		int int_ = (@struct.Int32_0 - struct2.Int32_0) / 2;
		int int_2 = (@struct.Int32_1 - struct2.Int32_1) / 2;
		SetWindowPos(mainWindowHandle, IntPtr.Zero, int_, int_2, 0, 0, 5u);
	}

	public static void smethod_3(int int_0)
	{
		if (int_0 == 0)
		{
			return;
		}
		using ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher($"Select * From Win32_Process Where ParentProcessID={int_0}");
		ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get();
		foreach (ManagementObject item in managementObjectCollection)
		{
			smethod_3(Convert.ToInt32(item["ProcessID"]));
		}
		try
		{
			Process processById = Process.GetProcessById(int_0);
			processById.Kill();
		}
		catch (ArgumentException)
		{
		}
	}
}
