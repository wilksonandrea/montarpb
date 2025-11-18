using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;

internal static class Class1
{
	public static bool Boolean_0 => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

	public static bool Boolean_1 => RuntimeInformation.IsOSPlatform(OSPlatform.OSX);

	public static bool Boolean_2 => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);

	public static string String_0 => Path.GetPathRoot(Environment.SystemDirectory);

	public static bool Boolean_3 => Environment.Is64BitOperatingSystem;

	public static string String_1
	{
		get
		{
			if (!Boolean_3)
			{
				return "32";
			}
			return "64";
		}
	}

	public static bool Boolean_4 => LicenseManager.UsageMode == LicenseUsageMode.Designtime;
}
