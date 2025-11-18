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
		return GClass4.smethod_1(string.Join("\n", new string[] { GClass4.smethod_5(GClass4.Enum2.Cpuid), GClass4.smethod_5(GClass4.Enum2.Motherboard) }));
	}

	private static string smethod_1(string string_0)
	{
		string str;
		using (SHA1Managed sHA1Managed = new SHA1Managed())
		{
			byte[] numArray = sHA1Managed.ComputeHash(Encoding.UTF8.GetBytes(string_0));
			StringBuilder stringBuilder = new StringBuilder((int)numArray.Length * 2);
			byte[] numArray1 = numArray;
			for (int i = 0; i < (int)numArray1.Length; i++)
			{
				byte num = numArray1[i];
				stringBuilder.Append(num.ToString("X2"));
			}
			str = stringBuilder.ToString();
		}
		return str;
	}

	private static string smethod_2(string string_0, string string_1)
	{
		string str = "";
		foreach (ManagementBaseObject ınstance in (new ManagementClass(string_0)).GetInstances())
		{
			ManagementObject managementObject = ınstance as ManagementObject;
			if (str != "")
			{
				continue;
			}
			try
			{
				str = managementObject[string_1].ToString();
				return str;
			}
			catch
			{
			}
		}
		return str;
	}

	private static string smethod_3(string string_0, string string_1)
	{
		string string1 = string_1;
		Class2 class2 = new Class2();
		Class4 class4 = class2.method_2("/usr/bin/sudo", string.Concat(" ", string_0), new Class3()
		{
			ProcessWindowStyle_0 = ProcessWindowStyle.Hidden,
			Boolean_1 = true,
			Boolean_2 = true,
			Boolean_0 = false
		}, true);
		string1 = (string1.EndsWith(":") ? string1 : string.Concat(string1, ":"));
		IEnumerable<string> strs = 
			from string_0 in class4.String_3.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
			select string_0.Trim(new char[] { ' ', '\t' });
		string str = strs.First<string>((string argument0) => argument0.StartsWith(string1));
		return str.Substring(str.IndexOf(string1, StringComparison.Ordinal) + string1.Length).Trim(new char[] { ' ', '\t' });
	}

	private static string smethod_4(string string_0)
	{
		Process process = new Process();
		ProcessStartInfo processStartInfo = new ProcessStartInfo()
		{
			FileName = "/bin/sh"
		};
		string str = string.Concat("/usr/sbin/ioreg -rd1 -c IOPlatformExpertDevice | awk -F'\\\"' '/", string_0, "/{ print $(NF-1) }'");
		processStartInfo.Arguments = string.Concat("-c \"", str, "\"");
		processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
		processStartInfo.RedirectStandardOutput = true;
		processStartInfo.UseShellExecute = false;
		string data = null;
		process.StartInfo = processStartInfo;
		process.OutputDataReceived += new DataReceivedEventHandler((object sender, DataReceivedEventArgs e) => {
			if (!string.IsNullOrEmpty(e.Data))
			{
				data = e.Data;
			}
		});
		process.Start();
		process.BeginOutputReadLine();
		process.WaitForExit();
		return data;
	}

	private static string smethod_5(GClass4.Enum2 enum2_0)
	{
		GClass4.Enum2 enum20 = enum2_0;
		if (enum20 == GClass4.Enum2.Motherboard)
		{
			if (Class1.Boolean_2)
			{
				return GClass4.smethod_3("dmidecode -t 2", "Manufacturer");
			}
			if (Class1.Boolean_0)
			{
				return GClass4.smethod_2("Win32_BaseBoard", "Manufacturer");
			}
			if (Class1.Boolean_1)
			{
				return GClass4.smethod_4("IOPlatformSerialNumber");
			}
		}
		else if (enum20 == GClass4.Enum2.Cpuid)
		{
			if (Class1.Boolean_2)
			{
				string str = GClass4.smethod_3("dmidecode -t 4", "ID");
				IEnumerable<string> strs = str.Split(new char[] { ' ' }).Reverse<string>();
				return string.Join("", strs);
			}
			if (Class1.Boolean_0)
			{
				string str1 = GClass1.smethod_0();
				if (str1 != null && str1.Length > 2)
				{
					return str1;
				}
				return GClass4.smethod_2("Win32_Processor", "ProcessorId");
			}
			if (Class1.Boolean_1)
			{
				return GClass4.smethod_4("IOPlatformUUID");
			}
		}
		throw new InvalidEnumArgumentException();
	}

	private enum Enum2
	{
		Motherboard,
		Cpuid
	}
}