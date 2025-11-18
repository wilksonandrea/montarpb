using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;

// Token: 0x02000007 RID: 7
internal static class Class1
{
	// Token: 0x17000001 RID: 1
	// (get) Token: 0x06000022 RID: 34 RVA: 0x0000215C File Offset: 0x0000035C
	public static bool Boolean_0
	{
		get
		{
			return RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
		}
	}

	// Token: 0x17000002 RID: 2
	// (get) Token: 0x06000023 RID: 35 RVA: 0x00002168 File Offset: 0x00000368
	public static bool Boolean_1
	{
		get
		{
			return RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
		}
	}

	// Token: 0x17000003 RID: 3
	// (get) Token: 0x06000024 RID: 36 RVA: 0x00002174 File Offset: 0x00000374
	public static bool Boolean_2
	{
		get
		{
			return RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
		}
	}

	// Token: 0x17000004 RID: 4
	// (get) Token: 0x06000025 RID: 37 RVA: 0x00002180 File Offset: 0x00000380
	public static string String_0
	{
		get
		{
			return Path.GetPathRoot(Environment.SystemDirectory);
		}
	}

	// Token: 0x17000005 RID: 5
	// (get) Token: 0x06000026 RID: 38 RVA: 0x0000218C File Offset: 0x0000038C
	public static bool Boolean_3
	{
		get
		{
			return Environment.Is64BitOperatingSystem;
		}
	}

	// Token: 0x17000006 RID: 6
	// (get) Token: 0x06000027 RID: 39 RVA: 0x00002193 File Offset: 0x00000393
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

	// Token: 0x17000007 RID: 7
	// (get) Token: 0x06000028 RID: 40 RVA: 0x000021A7 File Offset: 0x000003A7
	public static bool Boolean_4
	{
		get
		{
			return LicenseManager.UsageMode == LicenseUsageMode.Designtime;
		}
	}
}
