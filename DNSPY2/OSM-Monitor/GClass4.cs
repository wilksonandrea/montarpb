using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

// Token: 0x02000014 RID: 20
public static class GClass4
{
	// Token: 0x06000070 RID: 112 RVA: 0x00004700 File Offset: 0x00002900
	public static string smethod_0()
	{
		string[] array = new string[]
		{
			GClass4.smethod_5(GClass4.Enum2.Cpuid),
			GClass4.smethod_5(GClass4.Enum2.Motherboard)
		};
		string text = string.Join("\n", array);
		return GClass4.smethod_1(text);
	}

	// Token: 0x06000071 RID: 113 RVA: 0x0000473C File Offset: 0x0000293C
	private static string smethod_1(string string_0)
	{
		string text;
		using (SHA1Managed sha1Managed = new SHA1Managed())
		{
			byte[] array = sha1Managed.ComputeHash(Encoding.UTF8.GetBytes(string_0));
			StringBuilder stringBuilder = new StringBuilder(array.Length * 2);
			foreach (byte b in array)
			{
				stringBuilder.Append(b.ToString("X2"));
			}
			text = stringBuilder.ToString();
		}
		return text;
	}

	// Token: 0x06000072 RID: 114 RVA: 0x000047C0 File Offset: 0x000029C0
	private static string smethod_2(string string_0, string string_1)
	{
		string text = "";
		ManagementClass managementClass = new ManagementClass(string_0);
		ManagementObjectCollection instances = managementClass.GetInstances();
		foreach (ManagementBaseObject managementBaseObject in instances)
		{
			ManagementObject managementObject = managementBaseObject as ManagementObject;
			if (!(text != ""))
			{
				try
				{
					text = managementObject[string_1].ToString();
					break;
				}
				catch
				{
				}
			}
		}
		return text;
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00004850 File Offset: 0x00002A50
	private static string smethod_3(string string_0, string string_1)
	{
		GClass4.Class8 @class = new GClass4.Class8();
		@class.string_0 = string_1;
		Class2 class2 = new Class2();
		Class4 class3 = class2.method_2("/usr/bin/sudo", " " + string_0, new Class3
		{
			ProcessWindowStyle_0 = ProcessWindowStyle.Hidden,
			Boolean_1 = true,
			Boolean_2 = true,
			Boolean_0 = false
		}, true);
		@class.string_0 = (@class.string_0.EndsWith(":") ? @class.string_0 : (@class.string_0 + ":"));
		IEnumerable<string> enumerable = class3.String_3.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).Select(new Func<string, string>(GClass4.Class7.<>9.method_0));
		string text = enumerable.First(new Func<string, bool>(@class.method_0));
		return text.Substring(text.IndexOf(@class.string_0, StringComparison.Ordinal) + @class.string_0.Length).Trim(new char[] { ' ', '\t' });
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00004968 File Offset: 0x00002B68
	private static string smethod_4(string string_0)
	{
		GClass4.Class9 @class = new GClass4.Class9();
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
		@class.string_0 = null;
		process.StartInfo = processStartInfo;
		process.OutputDataReceived += @class.method_0;
		process.Start();
		process.BeginOutputReadLine();
		process.WaitForExit();
		return @class.string_0;
	}

	// Token: 0x06000075 RID: 117 RVA: 0x00004A08 File Offset: 0x00002C08
	private static string smethod_5(GClass4.Enum2 enum2_0)
	{
		if (enum2_0 != GClass4.Enum2.Motherboard)
		{
			if (enum2_0 == GClass4.Enum2.Cpuid)
			{
				if (Class1.Boolean_2)
				{
					string text = GClass4.smethod_3("dmidecode -t 4", "ID");
					IEnumerable<string> enumerable = text.Split(new char[] { ' ' }).Reverse<string>();
					return string.Join("", enumerable);
				}
				if (Class1.Boolean_0)
				{
					string text2 = GClass1.smethod_0();
					if (text2 == null || text2.Length <= 2)
					{
						return GClass4.smethod_2("Win32_Processor", "ProcessorId");
					}
					return text2;
				}
				else if (Class1.Boolean_1)
				{
					return GClass4.smethod_4("IOPlatformUUID");
				}
			}
		}
		else
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
		throw new InvalidEnumArgumentException();
	}

	// Token: 0x02000015 RID: 21
	private enum Enum2
	{
		// Token: 0x04000044 RID: 68
		Motherboard,
		// Token: 0x04000045 RID: 69
		Cpuid
	}

	// Token: 0x02000016 RID: 22
	[CompilerGenerated]
	[Serializable]
	private sealed class Class7
	{
		// Token: 0x06000076 RID: 118 RVA: 0x000023A8 File Offset: 0x000005A8
		// Note: this type is marked as 'beforefieldinit'.
		static Class7()
		{
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002133 File Offset: 0x00000333
		public Class7()
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000023B4 File Offset: 0x000005B4
		internal string method_0(string string_0)
		{
			return string_0.Trim(new char[] { ' ', '\t' });
		}

		// Token: 0x04000046 RID: 70
		public static readonly GClass4.Class7 <>9 = new GClass4.Class7();

		// Token: 0x04000047 RID: 71
		public static Func<string, string> <>9__3_0;
	}

	// Token: 0x02000017 RID: 23
	[CompilerGenerated]
	private sealed class Class8
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00002133 File Offset: 0x00000333
		public Class8()
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000023CC File Offset: 0x000005CC
		internal bool method_0(string string_1)
		{
			return string_1.StartsWith(this.string_0);
		}

		// Token: 0x04000048 RID: 72
		public string string_0;
	}

	// Token: 0x02000018 RID: 24
	[CompilerGenerated]
	private sealed class Class9
	{
		// Token: 0x0600007B RID: 123 RVA: 0x00002133 File Offset: 0x00000333
		public Class9()
		{
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000023DA File Offset: 0x000005DA
		internal void method_0(object sender, DataReceivedEventArgs e)
		{
			if (!string.IsNullOrEmpty(e.Data))
			{
				this.string_0 = e.Data;
			}
		}

		// Token: 0x04000049 RID: 73
		public string string_0;
	}
}
