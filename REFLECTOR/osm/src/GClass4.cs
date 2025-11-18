using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

public static class GClass4
{
    public static string smethod_0()
    {
        string[] strArray = new string[] { smethod_5(Enum2.Cpuid), smethod_5(Enum2.Motherboard) };
        return smethod_1(string.Join("\n", strArray));
    }

    private static string smethod_1(string string_0)
    {
        string str;
        using (SHA1Managed managed = new SHA1Managed())
        {
            byte[] buffer = managed.ComputeHash(Encoding.UTF8.GetBytes(string_0));
            StringBuilder builder = new StringBuilder(buffer.Length * 2);
            byte[] buffer2 = buffer;
            int index = 0;
            while (true)
            {
                if (index >= buffer2.Length)
                {
                    str = builder.ToString();
                    break;
                }
                builder.Append(buffer2[index].ToString("X2"));
                index++;
            }
        }
        return str;
    }

    private static string smethod_2(string string_0, string string_1)
    {
        string str = "";
        foreach (ManagementBaseObject obj2 in new ManagementClass(string_0).GetInstances())
        {
            ManagementObject obj3 = obj2 as ManagementObject;
            if (str == "")
            {
                try
                {
                    str = obj3[string_1].ToString();
                    break;
                }
                catch
                {
                }
            }
        }
        return str;
    }

    private static string smethod_3(string string_0, string string_1)
    {
        Class8 class2 = new Class8 {
            string_0 = string_1
        };
        Class3 class1 = new Class3();
        class1.ProcessWindowStyle_0 = ProcessWindowStyle.Hidden;
        class1.Boolean_1 = true;
        class1.Boolean_2 = true;
        class1.Boolean_0 = false;
        Class4 class4 = new Class2().method_2("/usr/bin/sudo", " " + string_0, class1, true);
        class2.string_0 = class2.string_0.EndsWith(":") ? class2.string_0 : (class2.string_0 + ":");
        string[] separator = new string[] { Environment.NewLine };
        string[] selector = class4.String_3.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        if (Class7.<>9__3_0 == null)
        {
            string[] local1 = class4.String_3.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            selector = (string[]) (Class7.<>9__3_0 = new Func<string, string>(Class7.<>9.method_0));
        }
        string str = ((IEnumerable<string>) Class7.<>9__3_0).Select<string, string>(selector).First<string>(new Func<string, bool>(class2.method_0));
        char[] trimChars = new char[] { ' ', '\t' };
        return str.Substring(str.IndexOf(class2.string_0, StringComparison.Ordinal) + class2.string_0.Length).Trim(trimChars);
    }

    private static string smethod_4(string string_0)
    {
        Class9 class2 = new Class9();
        Process process = new Process();
        ProcessStartInfo info1 = new ProcessStartInfo();
        info1.FileName = "/bin/sh";
        ProcessStartInfo info = info1;
        string str = "/usr/sbin/ioreg -rd1 -c IOPlatformExpertDevice | awk -F'\\\"' '/" + string_0 + "/{ print $(NF-1) }'";
        info.Arguments = "-c \"" + str + "\"";
        info.WindowStyle = ProcessWindowStyle.Hidden;
        info.RedirectStandardOutput = true;
        info.UseShellExecute = false;
        class2.string_0 = null;
        process.StartInfo = info;
        process.OutputDataReceived += new DataReceivedEventHandler(class2.method_0);
        process.Start();
        process.BeginOutputReadLine();
        process.WaitForExit();
        return class2.string_0;
    }

    private static string smethod_5(Enum2 enum2_0)
    {
        Enum2 enum2 = enum2_0;
        if (enum2 == Enum2.Motherboard)
        {
            if (Class1.Boolean_2)
            {
                return smethod_3("dmidecode -t 2", "Manufacturer");
            }
            if (Class1.Boolean_0)
            {
                return smethod_2("Win32_BaseBoard", "Manufacturer");
            }
            if (Class1.Boolean_1)
            {
                return smethod_4("IOPlatformSerialNumber");
            }
        }
        else if (enum2 == Enum2.Cpuid)
        {
            if (Class1.Boolean_2)
            {
                char[] separator = new char[] { ' ' };
                IEnumerable<string> values = smethod_3("dmidecode -t 4", "ID").Split(separator).Reverse<string>();
                return string.Join("", values);
            }
            if (Class1.Boolean_0)
            {
                string str6 = GClass1.smethod_0();
                return (((str6 == null) || (str6.Length <= 2)) ? smethod_2("Win32_Processor", "ProcessorId") : str6);
            }
            if (Class1.Boolean_1)
            {
                return smethod_4("IOPlatformUUID");
            }
        }
        throw new InvalidEnumArgumentException();
    }

    [Serializable, CompilerGenerated]
    private sealed class Class7
    {
        public static readonly GClass4.Class7 <>9 = new GClass4.Class7();
        public static Func<string, string> <>9__3_0;

        internal string method_0(string string_0)
        {
            char[] trimChars = new char[] { ' ', '\t' };
            return string_0.Trim(trimChars);
        }
    }

    [CompilerGenerated]
    private sealed class Class8
    {
        public string string_0;

        internal bool method_0(string string_1) => 
            string_1.StartsWith(this.string_0);
    }

    [CompilerGenerated]
    private sealed class Class9
    {
        public string string_0;

        internal void method_0(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                this.string_0 = e.Data;
            }
        }
    }

    private enum Enum2
    {
        Motherboard,
        Cpuid
    }
}

