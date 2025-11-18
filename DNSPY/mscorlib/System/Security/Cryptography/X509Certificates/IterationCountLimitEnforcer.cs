using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using Microsoft.Win32;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002B1 RID: 689
	internal static class IterationCountLimitEnforcer
	{
		// Token: 0x06002476 RID: 9334 RVA: 0x00083224 File Offset: 0x00081424
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal static void EnforceIterationCountLimit(byte[] pkcs12, bool readingFromFile, bool passwordProvided)
		{
			IterationCountLimitEnforcer.Impl.EnforceIterationCountLimit(pkcs12, readingFromFile, passwordProvided);
		}

		// Token: 0x02000B4D RID: 2893
		private static class Impl
		{
			// Token: 0x06006BA4 RID: 27556 RVA: 0x001745DC File Offset: 0x001727DC
			[SecuritySafeCritical]
			[EnvironmentPermission(SecurityAction.Assert, Unrestricted = true)]
			private static long ReadSecuritySwitch()
			{
				long num = 0L;
				string environmentVariable = Environment.GetEnvironmentVariable("COMPlus_Pkcs12UnspecifiedPasswordIterationLimit");
				if (environmentVariable != null && long.TryParse(environmentVariable, out num))
				{
					return num;
				}
				if (IterationCountLimitEnforcer.Impl.ReadSettingsFromRegistry(Registry.CurrentUser, ref num))
				{
					return num;
				}
				if (IterationCountLimitEnforcer.Impl.ReadSettingsFromRegistry(Registry.LocalMachine, ref num))
				{
					return num;
				}
				return 600000L;
			}

			// Token: 0x06006BA5 RID: 27557 RVA: 0x0017462C File Offset: 0x0017282C
			[SecuritySafeCritical]
			[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
			[RegistryPermission(SecurityAction.Assert, Unrestricted = true)]
			private static bool ReadSettingsFromRegistry(RegistryKey regKey, ref long value)
			{
				try
				{
					using (RegistryKey registryKey = regKey.OpenSubKey("SOFTWARE\\Microsoft\\.NETFramework", false))
					{
						if (registryKey != null)
						{
							object value2 = registryKey.GetValue("Pkcs12UnspecifiedPasswordIterationLimit");
							if (value2 != null)
							{
								value = Convert.ToInt64(value2, CultureInfo.InvariantCulture);
								return true;
							}
						}
					}
				}
				catch
				{
				}
				return false;
			}

			// Token: 0x06006BA6 RID: 27558 RVA: 0x0017469C File Offset: 0x0017289C
			internal static void EnforceIterationCountLimit(byte[] pkcs12, bool readingFromFile, bool passwordProvided)
			{
				if (readingFromFile || passwordProvided)
				{
					return;
				}
				long num = IterationCountLimitEnforcer.Impl.s_pkcs12UnspecifiedPasswordIterationLimit;
				if (num == -1L)
				{
					return;
				}
				if (num < 0L)
				{
					num = 600000L;
				}
				checked
				{
					try
					{
						try
						{
							KdfWorkLimiter.SetIterationLimit((ulong)num);
							ulong iterationCount = IterationCountLimitEnforcer.Impl.GetIterationCount(pkcs12);
							if (iterationCount > (ulong)num || KdfWorkLimiter.WasWorkLimitExceeded())
							{
								throw new CryptographicException();
							}
						}
						finally
						{
							KdfWorkLimiter.ResetIterationLimit();
						}
					}
					catch (Exception ex)
					{
						throw new CryptographicException(Environment.GetResourceString("Cryptography_X509_PfxWithoutPassword"), ex);
					}
				}
			}

			// Token: 0x06006BA7 RID: 27559 RVA: 0x00174720 File Offset: 0x00172920
			private static ulong GetIterationCount(byte[] pkcs12)
			{
				ReadOnlyMemory<byte> readOnlyMemory = new ReadOnlyMemory<byte>(pkcs12);
				AsnValueReader asnValueReader = new AsnValueReader(pkcs12, AsnEncodingRules.BER);
				PfxAsn pfxAsn;
				PfxAsn.Decode(ref asnValueReader, readOnlyMemory, out pfxAsn);
				return pfxAsn.CountTotalIterations();
			}

			// Token: 0x06006BA8 RID: 27560 RVA: 0x00174754 File Offset: 0x00172954
			// Note: this type is marked as 'beforefieldinit'.
			static Impl()
			{
			}

			// Token: 0x040033D4 RID: 13268
			private const long DefaultPkcs12UnspecifiedPasswordIterationLimit = 600000L;

			// Token: 0x040033D5 RID: 13269
			private static long s_pkcs12UnspecifiedPasswordIterationLimit = IterationCountLimitEnforcer.Impl.ReadSecuritySwitch();
		}
	}
}
