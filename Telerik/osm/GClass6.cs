using Microsoft.VisualBasic.Devices;
using Plugin.Core;
using Plugin.Core.Enums;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Principal;

public class GClass6
{
	public GClass6()
	{
	}

	[DllImport("shell32.dll", CharSet=CharSet.Auto, ExactSpelling=false)]
	private static extern int ShellExecute(IntPtr intptr_0, string string_0, string string_1, string string_2, string string_3, int int_0);

	public static bool smethod_0()
	{
		bool flag;
		using (WindowsIdentity current = WindowsIdentity.GetCurrent())
		{
			flag = (new WindowsPrincipal(current)).IsInRole(WindowsBuiltInRole.Administrator);
		}
		return flag;
	}

	public static int smethod_1()
	{
		int ınt32 = 0;
		try
		{
			ulong totalPhysicalMemory = (new ComputerInfo()).TotalPhysicalMemory;
			ulong uInt64 = ulong.Parse(totalPhysicalMemory.ToString());
			ınt32 = Convert.ToInt32((double)((float)uInt64) / 1048576);
		}
		catch (Exception exception1)
		{
			Exception exception = exception1;
			CLogger.Print(exception.Message, LoggerType.Error, exception);
		}
		return ınt32;
	}

	public static double smethod_2()
	{
		double privateMemorySize64 = 0;
		try
		{
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				currentProcess.Refresh();
				privateMemorySize64 = (double)currentProcess.PrivateMemorySize64 / 1048576;
				currentProcess.Dispose();
			}
		}
		catch (Exception exception1)
		{
			Exception exception = exception1;
			CLogger.Print(exception.Message, LoggerType.Error, exception);
		}
		return privateMemorySize64;
	}

	public static double smethod_3()
	{
		double num = 0;
		try
		{
			num = GClass6.smethod_2() * 100 / (double)GClass6.smethod_1();
		}
		catch (Exception exception1)
		{
			Exception exception = exception1;
			CLogger.Print(exception.Message, LoggerType.Error, exception);
		}
		return num;
	}

	public static long smethod_4(DirectoryInfo directoryInfo_0, bool bool_0)
	{
		long ınt64 = directoryInfo_0.EnumerateFiles().Sum<FileInfo>((FileInfo fileInfo_0) => fileInfo_0.Length);
		if (bool_0)
		{
			ınt64 = ınt64 + directoryInfo_0.EnumerateDirectories().Sum<DirectoryInfo>((DirectoryInfo argument0) => GClass6.smethod_4(argument0, true));
		}
		return ınt64;
	}

	public static bool smethod_5(DirectoryInfo directoryInfo_0)
	{
		bool flag;
		try
		{
			DirectoryInfo[] directories = directoryInfo_0.GetDirectories();
			for (int i = 0; i < (int)directories.Length; i++)
			{
				DirectoryInfo directoryInfo = directories[i];
				FileInfo[] files = directoryInfo_0.GetFiles();
				for (int j = 0; j < (int)files.Length; j++)
				{
					FileInfo fileInfo = files[j];
					fileInfo.IsReadOnly = false;
					fileInfo.Delete();
				}
				directoryInfo.Delete(true);
			}
			flag = true;
		}
		catch (Exception exception1)
		{
			Exception exception = exception1;
			CLogger.Print(exception.Message, LoggerType.Error, exception);
			flag = false;
		}
		return flag;
	}

	public static void smethod_6(string string_0, string string_1, string string_2)
	{
		try
		{
			GClass6.ShellExecute(IntPtr.Zero, string_2, string_1, string_0, null, 1);
		}
		catch (Exception exception1)
		{
			Exception exception = exception1;
			CLogger.Print(exception.Message, LoggerType.Error, exception);
		}
	}

	public static string smethod_7()
	{
		string upper;
		try
		{
			upper = Guid.NewGuid().ToString().ToUpper();
		}
		catch (Exception exception1)
		{
			Exception exception = exception1;
			CLogger.Print(exception.Message, LoggerType.Error, exception);
			upper = "";
		}
		return upper;
	}

	public static string smethod_8()
	{
		string upper;
		try
		{
			ManagementObjectCollection managementObjectCollections = (new ManagementObjectSearcher("Select ProcessorId From Win32_processor")).Get();
			string str = "";
			using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = managementObjectCollections.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					str = ((ManagementObject)enumerator.Current)["ProcessorId"].ToString();
				}
			}
			upper = str.ToUpper();
		}
		catch (Exception exception1)
		{
			Exception exception = exception1;
			CLogger.Print(exception.Message, LoggerType.Error, exception);
			upper = "";
		}
		return upper;
	}
}