using Microsoft.VisualBasic.Devices;
using Plugin.Core;
using Plugin.Core.Enums;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Principal;

public class GClass6
{
    [DllImport("shell32.dll", CharSet=CharSet.Auto)]
    private static extern int ShellExecute(IntPtr intptr_0, string string_0, string string_1, string string_2, string string_3, int int_0);
    public static bool smethod_0()
    {
        using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
        {
            return new WindowsPrincipal(identity).IsInRole(WindowsBuiltInRole.Administrator);
        }
    }

    public static int smethod_1()
    {
        int num = 0;
        try
        {
            num = Convert.ToInt32((double) (((double) ulong.Parse(new ComputerInfo().TotalPhysicalMemory.ToString())) / 1048576.0));
        }
        catch (Exception exception)
        {
            CLogger.Print(exception.Message, LoggerType.Error, exception);
        }
        return num;
    }

    public static double smethod_2()
    {
        double num = 0.0;
        try
        {
            using (Process process = Process.GetCurrentProcess())
            {
                process.Refresh();
                num = ((double) process.PrivateMemorySize64) / 1048576.0;
                process.Dispose();
            }
        }
        catch (Exception exception)
        {
            CLogger.Print(exception.Message, LoggerType.Error, exception);
        }
        return num;
    }

    public static double smethod_3()
    {
        double num = 0.0;
        try
        {
            num = (smethod_2() * 100.0) / ((double) smethod_1());
        }
        catch (Exception exception)
        {
            CLogger.Print(exception.Message, LoggerType.Error, exception);
        }
        return num;
    }

    public static long smethod_4(DirectoryInfo directoryInfo_0, bool bool_0)
    {
        Func<FileInfo, long> selector = Class10.<>9__5_0;
        if (Class10.<>9__5_0 == null)
        {
            Func<FileInfo, long> local1 = Class10.<>9__5_0;
            selector = Class10.<>9__5_0 = new Func<FileInfo, long>(Class10.<>9.method_0);
        }
        long num = directoryInfo_0.EnumerateFiles().Sum<FileInfo>(selector);
        if (bool_0)
        {
            Func<DirectoryInfo, long> func2 = Class10.<>9__5_1;
            if (Class10.<>9__5_1 == null)
            {
                Func<DirectoryInfo, long> local2 = Class10.<>9__5_1;
                func2 = Class10.<>9__5_1 = new Func<DirectoryInfo, long>(Class10.<>9.method_1);
            }
            num += directoryInfo_0.EnumerateDirectories().Sum<DirectoryInfo>(func2);
        }
        return num;
    }

    public static bool smethod_5(DirectoryInfo directoryInfo_0)
    {
        bool flag;
        try
        {
            DirectoryInfo[] directories = directoryInfo_0.GetDirectories();
            int index = 0;
            while (true)
            {
                if (index >= directories.Length)
                {
                    flag = true;
                    break;
                }
                DirectoryInfo info = directories[index];
                FileInfo[] infoArray3 = directoryInfo_0.GetFiles();
                int num2 = 0;
                while (true)
                {
                    if (num2 >= infoArray3.Length)
                    {
                        info.Delete(true);
                        index++;
                        break;
                    }
                    FileInfo info2 = infoArray3[num2];
                    info2.IsReadOnly = false;
                    info2.Delete();
                    num2++;
                }
            }
        }
        catch (Exception exception)
        {
            CLogger.Print(exception.Message, LoggerType.Error, exception);
            flag = false;
        }
        return flag;
    }

    public static void smethod_6(string string_0, string string_1, string string_2)
    {
        try
        {
            ShellExecute(IntPtr.Zero, string_2, string_1, string_0, null, 1);
        }
        catch (Exception exception)
        {
            CLogger.Print(exception.Message, LoggerType.Error, exception);
        }
    }

    public static string smethod_7()
    {
        try
        {
            return Guid.NewGuid().ToString().ToUpper();
        }
        catch (Exception exception)
        {
            CLogger.Print(exception.Message, LoggerType.Error, exception);
            return "";
        }
    }

    public static string smethod_8()
    {
        try
        {
            string str = "";
            using (ManagementObjectCollection.ManagementObjectEnumerator enumerator = new ManagementObjectSearcher("Select ProcessorId From Win32_processor").Get().GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    str = ((ManagementObject) enumerator.Current)["ProcessorId"].ToString();
                }
            }
            return str.ToUpper();
        }
        catch (Exception exception)
        {
            CLogger.Print(exception.Message, LoggerType.Error, exception);
            return "";
        }
    }

    [Serializable, CompilerGenerated]
    private sealed class Class10
    {
        public static readonly GClass6.Class10 <>9 = new GClass6.Class10();
        public static Func<FileInfo, long> <>9__5_0;
        public static Func<DirectoryInfo, long> <>9__5_1;

        internal long method_0(FileInfo fileInfo_0) => 
            fileInfo_0.Length;

        internal long method_1(DirectoryInfo directoryInfo_0) => 
            GClass6.smethod_4(directoryInfo_0, true);
    }
}

