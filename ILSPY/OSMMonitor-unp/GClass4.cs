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
	private enum Enum2
	{
		Motherboard,
		Cpuid
	}

	[Serializable]
	[CompilerGenerated]
	private sealed class Class7
	{
		public static readonly Class7 _003C_003E9 = new Class7();

		public static Func<string, string> _003C_003E9__3_0;

		internal string method_0(string string_0)
		{
			return string_0.Trim(' ', '\t');
		}
	}

	[CompilerGenerated]
	private sealed class Class8
	{
		public string string_0;

		internal bool method_0(string string_1)
		{
			return string_1.StartsWith(string_0);
		}
	}

	[CompilerGenerated]
	private sealed class Class9
	{
		public string string_0;

		internal void method_0(object sender, DataReceivedEventArgs e)
		{
			if (!string.IsNullOrEmpty(e.Data))
			{
				string_0 = e.Data;
			}
		}
	}

	public static string smethod_0()
	{
		string[] value = new string[2]
		{
			smethod_5(Enum2.Cpuid),
			smethod_5(Enum2.Motherboard)
		};
		string string_ = string.Join("\n", value);
		return smethod_1(string_);
	}

	private static string smethod_1(string string_0)
	{
		using SHA1Managed sHA1Managed = new SHA1Managed();
		byte[] array = sHA1Managed.ComputeHash(Encoding.UTF8.GetBytes(string_0));
		StringBuilder stringBuilder = new StringBuilder(array.Length * 2);
		byte[] array2 = array;
		foreach (byte b in array2)
		{
			stringBuilder.Append(b.ToString("X2"));
		}
		return stringBuilder.ToString();
	}

	private static string smethod_2(string string_0, string string_1)
	{
		string text = "";
		ManagementClass managementClass = new ManagementClass(string_0);
		ManagementObjectCollection ınstances = managementClass.GetInstances();
		foreach (ManagementBaseObject item in ınstances)
		{
			ManagementObject managementObject = item as ManagementObject;
			if (!(text != ""))
			{
				try
				{
					text = managementObject[string_1].ToString();
					return text;
				}
				catch
				{
				}
			}
		}
		return text;
	}

	private static string smethod_3(string string_0, string string_1)
	{
		global::Class2 @class = new global::Class2();
		global::Class4 class2 = @class.method_2("/usr/bin/sudo", " " + string_0, new global::Class3
		{
			ProcessWindowStyle_0 = ProcessWindowStyle.Hidden,
			Boolean_1 = true,
			Boolean_2 = true,
			Boolean_0 = false
		}, bool_0: true);
		string_1 = (string_1.EndsWith(":") ? string_1 : (string_1 + ":"));
		IEnumerable<string> source = from string_0 in class2.String_3.Split(new string[1] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
			select string_0.Trim(' ', '\t');
		string text = source.First((string string_1) => string_1.StartsWith(string_1));
		return text.Substring(text.IndexOf(string_1, StringComparison.Ordinal) + string_1.Length).Trim(' ', '\t');
	}

	private static string smethod_4(string string_0)
	{
		Process process = new Process();
		ProcessStartInfo processStartInfo = new ProcessStartInfo
		{
			FileName = "/bin/sh"
		};
		string text = "/usr/sbin/ioreg -rd1 -c IOPlatformExpertDevice | awk -F'\\\"' '/" + string_0 + "/{ print $(NF-1) }'";
		processStartInfo.Arguments = "-c \"" + text + "\"";
		processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
		processStartInfo.RedirectStandardOutput = true;
		processStartInfo.UseShellExecute = false;
		string string_ = null;
		process.StartInfo = processStartInfo;
		process.OutputDataReceived += delegate(object sender, DataReceivedEventArgs e)
		{
			if (!string.IsNullOrEmpty(e.Data))
			{
				string_ = e.Data;
			}
		};
		process.Start();
		process.BeginOutputReadLine();
		process.WaitForExit();
		return string_;
	}

	private static string smethod_5(Enum2 enum2_0)
	{
		switch (enum2_0)
		{
		case Enum2.Motherboard:
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
			break;
		case Enum2.Cpuid:
			if (Class1.Boolean_2)
			{
				string text = smethod_3("dmidecode -t 4", "ID");
				IEnumerable<string> values = text.Split(' ').Reverse();
				return string.Join("", values);
			}
			if (Class1.Boolean_0)
			{
				string text2 = GClass1.smethod_0();
				if (text2 == null || text2.Length <= 2)
				{
					return smethod_2("Win32_Processor", "ProcessorId");
				}
				return text2;
			}
			if (Class1.Boolean_1)
			{
				return smethod_4("IOPlatformUUID");
			}
			break;
		}
		throw new InvalidEnumArgumentException();
	}
}
