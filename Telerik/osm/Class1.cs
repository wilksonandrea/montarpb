using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;

internal static class Class1
{
	public static bool Boolean_0
	{
		get
		{
			return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
		}
	}

	public static bool Boolean_1
	{
		get
		{
			return RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
		}
	}

	public static bool Boolean_2
	{
		get
		{
			return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
		}
	}

	public static bool Boolean_3
	{
		get
		{
			return Environment.Is64BitOperatingSystem;
		}
	}

	public static bool Boolean_4
	{
		get
		{
			return LicenseManager.UsageMode == LicenseUsageMode.Designtime;
		}
	}

	public static string String_0
	{
		get
		{
			return Path.GetPathRoot(Environment.SystemDirectory);
		}
	}

	public static string String_1
	{
		get
		{
			if (!Class1.Boolean_3)
			{
				return "32";
			}
			return "64";
		}
	}
}