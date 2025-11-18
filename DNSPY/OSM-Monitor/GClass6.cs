using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Principal;
using Microsoft.VisualBasic.Devices;
using Plugin.Core;
using Plugin.Core.Enums;

// Token: 0x0200001A RID: 26
public class GClass6
{
	// Token: 0x06000081 RID: 129
	[DllImport("shell32.dll", CharSet = CharSet.Auto)]
	private static extern int ShellExecute(IntPtr intptr_0, string string_0, string string_1, string string_2, string string_3, int int_0);

	// Token: 0x06000082 RID: 130 RVA: 0x00004B88 File Offset: 0x00002D88
	public static bool smethod_0()
	{
		bool flag;
		using (WindowsIdentity current = WindowsIdentity.GetCurrent())
		{
			flag = new WindowsPrincipal(current).IsInRole(WindowsBuiltInRole.Administrator);
		}
		return flag;
	}

	// Token: 0x06000083 RID: 131 RVA: 0x00004BCC File Offset: 0x00002DCC
	public static int smethod_1()
	{
		int num = 0;
		try
		{
			ComputerInfo computerInfo = new ComputerInfo();
			ulong num2 = ulong.Parse(computerInfo.TotalPhysicalMemory.ToString());
			num = Convert.ToInt32(num2 / 1048576.0);
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
		return num;
	}

	// Token: 0x06000084 RID: 132 RVA: 0x00004C30 File Offset: 0x00002E30
	public static double smethod_2()
	{
		double num = 0.0;
		try
		{
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				currentProcess.Refresh();
				num = (double)currentProcess.PrivateMemorySize64 / 1048576.0;
				currentProcess.Dispose();
			}
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
		return num;
	}

	// Token: 0x06000085 RID: 133 RVA: 0x00004CA8 File Offset: 0x00002EA8
	public static double smethod_3()
	{
		double num = 0.0;
		try
		{
			num = GClass6.smethod_2() * 100.0 / (double)GClass6.smethod_1();
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
		return num;
	}

	// Token: 0x06000086 RID: 134 RVA: 0x00004CFC File Offset: 0x00002EFC
	public static long smethod_4(DirectoryInfo directoryInfo_0, bool bool_0)
	{
		long num = directoryInfo_0.EnumerateFiles().Sum(new Func<FileInfo, long>(GClass6.Class10.<>9.method_0));
		if (bool_0)
		{
			num += directoryInfo_0.EnumerateDirectories().Sum(new Func<DirectoryInfo, long>(GClass6.Class10.<>9.method_1));
		}
		return num;
	}

	// Token: 0x06000087 RID: 135 RVA: 0x00004D68 File Offset: 0x00002F68
	public static bool smethod_5(DirectoryInfo directoryInfo_0)
	{
		bool flag;
		try
		{
			foreach (DirectoryInfo directoryInfo in directoryInfo_0.GetDirectories())
			{
				FileInfo[] files = directoryInfo_0.GetFiles();
				foreach (FileInfo fileInfo in files)
				{
					fileInfo.IsReadOnly = false;
					fileInfo.Delete();
				}
				directoryInfo.Delete(true);
			}
			flag = true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			flag = false;
		}
		return flag;
	}

	// Token: 0x06000088 RID: 136 RVA: 0x00004DF8 File Offset: 0x00002FF8
	public static void smethod_6(string string_0, string string_1, string string_2)
	{
		try
		{
			GClass6.ShellExecute(IntPtr.Zero, string_2, string_1, string_0, null, 1);
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	// Token: 0x06000089 RID: 137 RVA: 0x00004E38 File Offset: 0x00003038
	public static string smethod_7()
	{
		string text2;
		try
		{
			string text = Guid.NewGuid().ToString();
			text2 = text.ToUpper();
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			text2 = "";
		}
		return text2;
	}

	// Token: 0x0600008A RID: 138 RVA: 0x00004E8C File Offset: 0x0000308C
	public static string smethod_8()
	{
		string text2;
		try
		{
			ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("Select ProcessorId From Win32_processor");
			ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get();
			string text = "";
			using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectCollection.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					ManagementObject managementObject = (ManagementObject)enumerator.Current;
					text = managementObject["ProcessorId"].ToString();
				}
			}
			text2 = text.ToUpper();
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			text2 = "";
		}
		return text2;
	}

	// Token: 0x0600008B RID: 139 RVA: 0x00002133 File Offset: 0x00000333
	public GClass6()
	{
	}

	// Token: 0x0200001B RID: 27
	[CompilerGenerated]
	[Serializable]
	private sealed class Class10
	{
		// Token: 0x0600008C RID: 140 RVA: 0x00002408 File Offset: 0x00000608
		// Note: this type is marked as 'beforefieldinit'.
		static Class10()
		{
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00002133 File Offset: 0x00000333
		public Class10()
		{
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00002414 File Offset: 0x00000614
		internal long method_0(FileInfo fileInfo_0)
		{
			return fileInfo_0.Length;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x0000241C File Offset: 0x0000061C
		internal long method_1(DirectoryInfo directoryInfo_0)
		{
			return GClass6.smethod_4(directoryInfo_0, true);
		}

		// Token: 0x0400004B RID: 75
		public static readonly GClass6.Class10 <>9 = new GClass6.Class10();

		// Token: 0x0400004C RID: 76
		public static Func<FileInfo, long> <>9__5_0;

		// Token: 0x0400004D RID: 77
		public static Func<DirectoryInfo, long> <>9__5_1;
	}
}
