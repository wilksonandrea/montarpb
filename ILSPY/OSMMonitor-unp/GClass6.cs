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

public class GClass6
{
	[Serializable]
	[CompilerGenerated]
	private sealed class Class10
	{
		public static readonly Class10 _003C_003E9 = new Class10();

		public static Func<FileInfo, long> _003C_003E9__5_0;

		public static Func<DirectoryInfo, long> _003C_003E9__5_1;

		internal long method_0(FileInfo fileInfo_0)
		{
			return fileInfo_0.Length;
		}

		internal long method_1(DirectoryInfo directoryInfo_0)
		{
			return smethod_4(directoryInfo_0, bool_0: true);
		}
	}

	[DllImport("shell32.dll", CharSet = CharSet.Auto)]
	private static extern int ShellExecute(IntPtr intptr_0, string string_0, string string_1, string string_2, string string_3, int int_0);

	public static bool smethod_0()
	{
		using WindowsIdentity ntIdentity = WindowsIdentity.GetCurrent();
		return new WindowsPrincipal(ntIdentity).IsInRole(WindowsBuiltInRole.Administrator);
	}

	public static int smethod_1()
	{
		int result = 0;
		try
		{
			ComputerInfo computerInfo = new ComputerInfo();
			ulong num = ulong.Parse(computerInfo.TotalPhysicalMemory.ToString());
			result = Convert.ToInt32((double)num / 1048576.0);
			return result;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return result;
		}
	}

	public static double smethod_2()
	{
		double result = 0.0;
		try
		{
			using Process process = Process.GetCurrentProcess();
			process.Refresh();
			result = (double)process.PrivateMemorySize64 / 1048576.0;
			process.Dispose();
			return result;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return result;
		}
	}

	public static double smethod_3()
	{
		double result = 0.0;
		try
		{
			result = smethod_2() * 100.0 / (double)smethod_1();
			return result;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return result;
		}
	}

	public static long smethod_4(DirectoryInfo directoryInfo_0, bool bool_0)
	{
		long num = directoryInfo_0.EnumerateFiles().Sum((FileInfo fileInfo_0) => fileInfo_0.Length);
		if (bool_0)
		{
			num += directoryInfo_0.EnumerateDirectories().Sum((DirectoryInfo directoryInfo_0) => smethod_4(directoryInfo_0, bool_0: true));
		}
		return num;
	}

	public static bool smethod_5(DirectoryInfo directoryInfo_0)
	{
		try
		{
			DirectoryInfo[] directories = directoryInfo_0.GetDirectories();
			foreach (DirectoryInfo directoryInfo in directories)
			{
				FileInfo[] files = directoryInfo_0.GetFiles();
				FileInfo[] array = files;
				foreach (FileInfo fileInfo in array)
				{
					fileInfo.IsReadOnly = false;
					fileInfo.Delete();
				}
				directoryInfo.Delete(recursive: true);
			}
			return true;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return false;
		}
	}

	public static void smethod_6(string string_0, string string_1, string string_2)
	{
		try
		{
			ShellExecute(IntPtr.Zero, string_2, string_1, string_0, null, 1);
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
		}
	}

	public static string smethod_7()
	{
		try
		{
			string text = Guid.NewGuid().ToString();
			return text.ToUpper();
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return "";
		}
	}

	public static string smethod_8()
	{
		try
		{
			ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher("Select ProcessorId From Win32_processor");
			ManagementObjectCollection managementObjectCollection = managementObjectSearcher.Get();
			string text = "";
			using (ManagementObjectCollection.ManagementObjectEnumerator managementObjectEnumerator = managementObjectCollection.GetEnumerator())
			{
				if (managementObjectEnumerator.MoveNext())
				{
					ManagementObject managementObject = (ManagementObject)managementObjectEnumerator.Current;
					text = managementObject["ProcessorId"].ToString();
				}
			}
			return text.ToUpper();
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return "";
		}
	}
}
