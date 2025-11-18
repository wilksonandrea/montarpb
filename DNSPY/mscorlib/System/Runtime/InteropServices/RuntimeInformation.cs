using System;
using System.Reflection;
using System.Security;
using Microsoft.Win32;

namespace System.Runtime.InteropServices
{
	// Token: 0x020009AD RID: 2477
	public static class RuntimeInformation
	{
		// Token: 0x1700111B RID: 4379
		// (get) Token: 0x0600630E RID: 25358 RVA: 0x0015169C File Offset: 0x0014F89C
		public static string FrameworkDescription
		{
			get
			{
				if (RuntimeInformation.s_frameworkDescription == null)
				{
					AssemblyFileVersionAttribute assemblyFileVersionAttribute = (AssemblyFileVersionAttribute)typeof(object).GetTypeInfo().Assembly.GetCustomAttribute(typeof(AssemblyFileVersionAttribute));
					RuntimeInformation.s_frameworkDescription = ".NET Framework " + assemblyFileVersionAttribute.Version;
				}
				return RuntimeInformation.s_frameworkDescription;
			}
		}

		// Token: 0x0600630F RID: 25359 RVA: 0x001516F3 File Offset: 0x0014F8F3
		public static bool IsOSPlatform(OSPlatform osPlatform)
		{
			return OSPlatform.Windows == osPlatform;
		}

		// Token: 0x1700111C RID: 4380
		// (get) Token: 0x06006310 RID: 25360 RVA: 0x00151700 File Offset: 0x0014F900
		public static string OSDescription
		{
			[SecuritySafeCritical]
			get
			{
				if (RuntimeInformation.s_osDescription == null)
				{
					RuntimeInformation.s_osDescription = RuntimeInformation.RtlGetVersion();
				}
				return RuntimeInformation.s_osDescription;
			}
		}

		// Token: 0x1700111D RID: 4381
		// (get) Token: 0x06006311 RID: 25361 RVA: 0x00151718 File Offset: 0x0014F918
		public static Architecture OSArchitecture
		{
			[SecuritySafeCritical]
			get
			{
				object obj = RuntimeInformation.s_osLock;
				lock (obj)
				{
					if (RuntimeInformation.s_osArch == null)
					{
						Win32Native.SYSTEM_INFO system_INFO;
						Win32Native.GetNativeSystemInfo(out system_INFO);
						RuntimeInformation.s_osArch = new Architecture?(RuntimeInformation.GetArchitecture(system_INFO.wProcessorArchitecture));
					}
				}
				return RuntimeInformation.s_osArch.Value;
			}
		}

		// Token: 0x1700111E RID: 4382
		// (get) Token: 0x06006312 RID: 25362 RVA: 0x00151784 File Offset: 0x0014F984
		public static Architecture ProcessArchitecture
		{
			[SecuritySafeCritical]
			get
			{
				object obj = RuntimeInformation.s_processLock;
				lock (obj)
				{
					if (RuntimeInformation.s_processArch == null)
					{
						Win32Native.SYSTEM_INFO system_INFO = default(Win32Native.SYSTEM_INFO);
						Win32Native.GetSystemInfo(ref system_INFO);
						RuntimeInformation.s_processArch = new Architecture?(RuntimeInformation.GetArchitecture(system_INFO.wProcessorArchitecture));
					}
				}
				return RuntimeInformation.s_processArch.Value;
			}
		}

		// Token: 0x06006313 RID: 25363 RVA: 0x001517F8 File Offset: 0x0014F9F8
		private static Architecture GetArchitecture(ushort wProcessorArchitecture)
		{
			Architecture architecture = Architecture.X86;
			if (wProcessorArchitecture <= 5)
			{
				if (wProcessorArchitecture != 0)
				{
					if (wProcessorArchitecture == 5)
					{
						architecture = Architecture.Arm;
					}
				}
				else
				{
					architecture = Architecture.X86;
				}
			}
			else if (wProcessorArchitecture != 9)
			{
				if (wProcessorArchitecture == 12)
				{
					architecture = Architecture.Arm64;
				}
			}
			else
			{
				architecture = Architecture.X64;
			}
			return architecture;
		}

		// Token: 0x06006314 RID: 25364 RVA: 0x00151830 File Offset: 0x0014FA30
		[SecuritySafeCritical]
		private static string RtlGetVersion()
		{
			Win32Native.RTL_OSVERSIONINFOEX rtl_OSVERSIONINFOEX = default(Win32Native.RTL_OSVERSIONINFOEX);
			rtl_OSVERSIONINFOEX.dwOSVersionInfoSize = (uint)Marshal.SizeOf<Win32Native.RTL_OSVERSIONINFOEX>(rtl_OSVERSIONINFOEX);
			if (Win32Native.RtlGetVersion(out rtl_OSVERSIONINFOEX) == 0)
			{
				return string.Format("{0} {1}.{2}.{3} {4}", new object[] { "Microsoft Windows", rtl_OSVERSIONINFOEX.dwMajorVersion, rtl_OSVERSIONINFOEX.dwMinorVersion, rtl_OSVERSIONINFOEX.dwBuildNumber, rtl_OSVERSIONINFOEX.szCSDVersion });
			}
			return "Microsoft Windows";
		}

		// Token: 0x06006315 RID: 25365 RVA: 0x001518AC File Offset: 0x0014FAAC
		// Note: this type is marked as 'beforefieldinit'.
		static RuntimeInformation()
		{
		}

		// Token: 0x04002CB6 RID: 11446
		private const string FrameworkName = ".NET Framework";

		// Token: 0x04002CB7 RID: 11447
		private static string s_frameworkDescription;

		// Token: 0x04002CB8 RID: 11448
		private static string s_osDescription = null;

		// Token: 0x04002CB9 RID: 11449
		private static object s_osLock = new object();

		// Token: 0x04002CBA RID: 11450
		private static object s_processLock = new object();

		// Token: 0x04002CBB RID: 11451
		private static Architecture? s_osArch = null;

		// Token: 0x04002CBC RID: 11452
		private static Architecture? s_processArch = null;
	}
}
