using System;
using System.Diagnostics;
using System.Management;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Plugin.Core;
using Plugin.Core.Enums;

// Token: 0x02000022 RID: 34
public class GClass11
{
	// Token: 0x060000A0 RID: 160
	[DllImport("user32.dll", SetLastError = true)]
	private static extern IntPtr FindWindow(string string_0, string string_1);

	// Token: 0x060000A1 RID: 161
	[DllImport("user32.dll", SetLastError = true)]
	private static extern bool SetWindowPos(IntPtr intptr_0, IntPtr intptr_1, int int_0, int int_1, int int_2, int int_3, uint uint_2);

	// Token: 0x060000A2 RID: 162 RVA: 0x00002425 File Offset: 0x00000625
	private static GClass11.Struct4 smethod_0()
	{
		return new GClass11.Struct4(GClass11.GetSystemMetrics(0), GClass11.GetSystemMetrics(1));
	}

	// Token: 0x060000A3 RID: 163
	[DllImport("User32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
	private static extern int GetSystemMetrics(int int_0);

	// Token: 0x060000A4 RID: 164
	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool GetWindowRect(HandleRef handleRef_0, out GClass11.Struct5 struct5_0);

	// Token: 0x060000A5 RID: 165 RVA: 0x00005270 File Offset: 0x00003470
	private static GClass11.Struct4 smethod_1(IntPtr intptr_0)
	{
		GClass11.Struct5 @struct;
		if (!GClass11.GetWindowRect(new HandleRef(null, intptr_0), out @struct))
		{
			CLogger.Print("Unable to get window rect!", LoggerType.Warning, null);
		}
		int num = @struct.int_2 - @struct.int_0;
		int num2 = @struct.int_3 - @struct.int_1;
		return new GClass11.Struct4(num, num2);
	}

	// Token: 0x060000A6 RID: 166 RVA: 0x000052BC File Offset: 0x000034BC
	public static void smethod_2()
	{
		IntPtr mainWindowHandle = Process.GetCurrentProcess().MainWindowHandle;
		if (mainWindowHandle == IntPtr.Zero)
		{
			CLogger.Print("Couldn't find a window to center!", LoggerType.Warning, null);
		}
		GClass11.Struct4 @struct = GClass11.smethod_0();
		GClass11.Struct4 struct2 = GClass11.smethod_1(mainWindowHandle);
		int num = (@struct.Int32_0 - struct2.Int32_0) / 2;
		int num2 = (@struct.Int32_1 - struct2.Int32_1) / 2;
		GClass11.SetWindowPos(mainWindowHandle, IntPtr.Zero, num, num2, 0, 0, 5U);
	}

	// Token: 0x060000A7 RID: 167 RVA: 0x00005334 File Offset: 0x00003534
	public static void smethod_3(int int_0)
	{
		if (int_0 == 0)
		{
			return;
		}
		using (ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(string.Format("Select * From Win32_Process Where ParentProcessID={0}", int_0)))
		{
			ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get();
			foreach (ManagementBaseObject managementBaseObject in managementObjectCollection)
			{
				ManagementObject managementObject = (ManagementObject)managementBaseObject;
				GClass11.smethod_3(Convert.ToInt32(managementObject["ProcessID"]));
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

	// Token: 0x060000A8 RID: 168 RVA: 0x00002133 File Offset: 0x00000333
	public GClass11()
	{
	}

	// Token: 0x0400009F RID: 159
	private const uint uint_0 = 1U;

	// Token: 0x040000A0 RID: 160
	private const uint uint_1 = 4U;

	// Token: 0x02000023 RID: 35
	private struct Struct4
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00002438 File Offset: 0x00000638
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00002440 File Offset: 0x00000640
		public int Int32_0
		{
			[CompilerGenerated]
			get
			{
				return this.int_0;
			}
			[CompilerGenerated]
			set
			{
				this.int_0 = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000AB RID: 171 RVA: 0x00002449 File Offset: 0x00000649
		// (set) Token: 0x060000AC RID: 172 RVA: 0x00002451 File Offset: 0x00000651
		public int Int32_1
		{
			[CompilerGenerated]
			get
			{
				return this.int_1;
			}
			[CompilerGenerated]
			set
			{
				this.int_1 = value;
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000245A File Offset: 0x0000065A
		public Struct4(int int_2, int int_3)
		{
			this.Int32_0 = int_2;
			this.Int32_1 = int_3;
		}

		// Token: 0x040000A1 RID: 161
		[CompilerGenerated]
		private int int_0;

		// Token: 0x040000A2 RID: 162
		[CompilerGenerated]
		private int int_1;
	}

	// Token: 0x02000024 RID: 36
	private struct Struct5
	{
		// Token: 0x040000A3 RID: 163
		public int int_0;

		// Token: 0x040000A4 RID: 164
		public int int_1;

		// Token: 0x040000A5 RID: 165
		public int int_2;

		// Token: 0x040000A6 RID: 166
		public int int_3;
	}
}
