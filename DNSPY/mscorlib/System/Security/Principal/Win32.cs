using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Principal
{
	// Token: 0x0200033D RID: 829
	internal static class Win32
	{
		// Token: 0x0600294B RID: 10571 RVA: 0x00098818 File Offset: 0x00096A18
		[SecuritySafeCritical]
		static Win32()
		{
			Win32Native.OSVERSIONINFO osversioninfo = new Win32Native.OSVERSIONINFO();
			if (!Environment.GetVersion(osversioninfo))
			{
				throw new SystemException(Environment.GetResourceString("InvalidOperation_GetVersion"));
			}
			if (osversioninfo.MajorVersion > 5 || osversioninfo.MinorVersion > 0)
			{
				Win32._LsaLookupNames2Supported = true;
				Win32._WellKnownSidApisSupported = true;
				return;
			}
			Win32._LsaLookupNames2Supported = false;
			Win32Native.OSVERSIONINFOEX osversioninfoex = new Win32Native.OSVERSIONINFOEX();
			if (!Environment.GetVersionEx(osversioninfoex))
			{
				throw new SystemException(Environment.GetResourceString("InvalidOperation_GetVersion"));
			}
			if (osversioninfoex.ServicePackMajor < 3)
			{
				Win32._WellKnownSidApisSupported = false;
				return;
			}
			Win32._WellKnownSidApisSupported = true;
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x0600294C RID: 10572 RVA: 0x000988A0 File Offset: 0x00096AA0
		internal static bool LsaLookupNames2Supported
		{
			get
			{
				return Win32._LsaLookupNames2Supported;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x0600294D RID: 10573 RVA: 0x000988A7 File Offset: 0x00096AA7
		internal static bool WellKnownSidApisSupported
		{
			get
			{
				return Win32._WellKnownSidApisSupported;
			}
		}

		// Token: 0x0600294E RID: 10574 RVA: 0x000988B0 File Offset: 0x00096AB0
		[SecurityCritical]
		internal static SafeLsaPolicyHandle LsaOpenPolicy(string systemName, PolicyRights rights)
		{
			Win32Native.LSA_OBJECT_ATTRIBUTES lsa_OBJECT_ATTRIBUTES;
			lsa_OBJECT_ATTRIBUTES.Length = Marshal.SizeOf(typeof(Win32Native.LSA_OBJECT_ATTRIBUTES));
			lsa_OBJECT_ATTRIBUTES.RootDirectory = IntPtr.Zero;
			lsa_OBJECT_ATTRIBUTES.ObjectName = IntPtr.Zero;
			lsa_OBJECT_ATTRIBUTES.Attributes = 0;
			lsa_OBJECT_ATTRIBUTES.SecurityDescriptor = IntPtr.Zero;
			lsa_OBJECT_ATTRIBUTES.SecurityQualityOfService = IntPtr.Zero;
			SafeLsaPolicyHandle safeLsaPolicyHandle;
			uint num;
			if ((num = Win32Native.LsaOpenPolicy(systemName, ref lsa_OBJECT_ATTRIBUTES, (int)rights, out safeLsaPolicyHandle)) == 0U)
			{
				return safeLsaPolicyHandle;
			}
			if (num == 3221225506U)
			{
				throw new UnauthorizedAccessException();
			}
			if (num == 3221225626U || num == 3221225495U)
			{
				throw new OutOfMemoryException();
			}
			int num2 = Win32Native.LsaNtStatusToWinError((int)num);
			throw new SystemException(Win32Native.GetMessage(num2));
		}

		// Token: 0x0600294F RID: 10575 RVA: 0x00098954 File Offset: 0x00096B54
		[SecurityCritical]
		internal static byte[] ConvertIntPtrSidToByteArraySid(IntPtr binaryForm)
		{
			byte b = Marshal.ReadByte(binaryForm, 0);
			if (b != SecurityIdentifier.Revision)
			{
				throw new ArgumentException(Environment.GetResourceString("IdentityReference_InvalidSidRevision"), "binaryForm");
			}
			byte b2 = Marshal.ReadByte(binaryForm, 1);
			if (b2 < 0 || b2 > SecurityIdentifier.MaxSubAuthorities)
			{
				throw new ArgumentException(Environment.GetResourceString("IdentityReference_InvalidNumberOfSubauthorities", new object[] { SecurityIdentifier.MaxSubAuthorities }), "binaryForm");
			}
			int num = (int)(8 + b2 * 4);
			byte[] array = new byte[num];
			Marshal.Copy(binaryForm, array, 0, num);
			return array;
		}

		// Token: 0x06002950 RID: 10576 RVA: 0x000989DC File Offset: 0x00096BDC
		[SecurityCritical]
		internal static int CreateSidFromString(string stringSid, out byte[] resultSid)
		{
			IntPtr zero = IntPtr.Zero;
			int lastWin32Error;
			try
			{
				if (1 != Win32Native.ConvertStringSidToSid(stringSid, out zero))
				{
					lastWin32Error = Marshal.GetLastWin32Error();
					goto IL_2D;
				}
				resultSid = Win32.ConvertIntPtrSidToByteArraySid(zero);
			}
			finally
			{
				Win32Native.LocalFree(zero);
			}
			return 0;
			IL_2D:
			resultSid = null;
			return lastWin32Error;
		}

		// Token: 0x06002951 RID: 10577 RVA: 0x00098A2C File Offset: 0x00096C2C
		[SecurityCritical]
		internal static int CreateWellKnownSid(WellKnownSidType sidType, SecurityIdentifier domainSid, out byte[] resultSid)
		{
			if (!Win32.WellKnownSidApisSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_RequiresW2kSP3"));
			}
			uint maxBinaryLength = (uint)SecurityIdentifier.MaxBinaryLength;
			resultSid = new byte[maxBinaryLength];
			if (Win32Native.CreateWellKnownSid((int)sidType, (domainSid == null) ? null : domainSid.BinaryForm, resultSid, ref maxBinaryLength) != 0)
			{
				return 0;
			}
			resultSid = null;
			return Marshal.GetLastWin32Error();
		}

		// Token: 0x06002952 RID: 10578 RVA: 0x00098A88 File Offset: 0x00096C88
		[SecurityCritical]
		internal static bool IsEqualDomainSid(SecurityIdentifier sid1, SecurityIdentifier sid2)
		{
			if (!Win32.WellKnownSidApisSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_RequiresW2kSP3"));
			}
			if (sid1 == null || sid2 == null)
			{
				return false;
			}
			byte[] array = new byte[sid1.BinaryLength];
			sid1.GetBinaryForm(array, 0);
			byte[] array2 = new byte[sid2.BinaryLength];
			sid2.GetBinaryForm(array2, 0);
			bool flag;
			return Win32Native.IsEqualDomainSid(array, array2, out flag) != 0 && flag;
		}

		// Token: 0x06002953 RID: 10579 RVA: 0x00098AF8 File Offset: 0x00096CF8
		[SecurityCritical]
		internal unsafe static void InitializeReferencedDomainsPointer(SafeLsaMemoryHandle referencedDomains)
		{
			referencedDomains.Initialize((ulong)Marshal.SizeOf(typeof(Win32Native.LSA_REFERENCED_DOMAIN_LIST)));
			Win32Native.LSA_REFERENCED_DOMAIN_LIST lsa_REFERENCED_DOMAIN_LIST = referencedDomains.Read<Win32Native.LSA_REFERENCED_DOMAIN_LIST>(0UL);
			byte* ptr = null;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
				referencedDomains.AcquirePointer(ref ptr);
				if (!lsa_REFERENCED_DOMAIN_LIST.Domains.IsNull())
				{
					Win32Native.LSA_TRUST_INFORMATION* ptr2 = (Win32Native.LSA_TRUST_INFORMATION*)(void*)lsa_REFERENCED_DOMAIN_LIST.Domains;
					ptr2 += lsa_REFERENCED_DOMAIN_LIST.Entries;
					long num = (long)((byte*)ptr2 - (byte*)ptr);
					referencedDomains.Initialize((ulong)num);
				}
			}
			finally
			{
				if (ptr != null)
				{
					referencedDomains.ReleasePointer();
				}
			}
		}

		// Token: 0x06002954 RID: 10580 RVA: 0x00098B8C File Offset: 0x00096D8C
		[SecurityCritical]
		internal static int GetWindowsAccountDomainSid(SecurityIdentifier sid, out SecurityIdentifier resultSid)
		{
			if (!Win32.WellKnownSidApisSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_RequiresW2kSP3"));
			}
			byte[] array = new byte[sid.BinaryLength];
			sid.GetBinaryForm(array, 0);
			uint maxBinaryLength = (uint)SecurityIdentifier.MaxBinaryLength;
			byte[] array2 = new byte[maxBinaryLength];
			if (Win32Native.GetWindowsAccountDomainSid(array, array2, ref maxBinaryLength) != 0)
			{
				resultSid = new SecurityIdentifier(array2, 0);
				return 0;
			}
			resultSid = null;
			return Marshal.GetLastWin32Error();
		}

		// Token: 0x06002955 RID: 10581 RVA: 0x00098BF0 File Offset: 0x00096DF0
		[SecurityCritical]
		internal static bool IsWellKnownSid(SecurityIdentifier sid, WellKnownSidType type)
		{
			if (!Win32.WellKnownSidApisSupported)
			{
				throw new PlatformNotSupportedException(Environment.GetResourceString("PlatformNotSupported_RequiresW2kSP3"));
			}
			byte[] array = new byte[sid.BinaryLength];
			sid.GetBinaryForm(array, 0);
			return Win32Native.IsWellKnownSid(array, (int)type) != 0;
		}

		// Token: 0x06002956 RID: 10582
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern int ImpersonateLoggedOnUser(SafeAccessTokenHandle hToken);

		// Token: 0x06002957 RID: 10583
		[SecurityCritical]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int OpenThreadToken(TokenAccessLevels dwDesiredAccess, WinSecurityContext OpenAs, out SafeAccessTokenHandle phThreadToken);

		// Token: 0x06002958 RID: 10584
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern int RevertToSelf();

		// Token: 0x06002959 RID: 10585
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern int SetThreadToken(SafeAccessTokenHandle hToken);

		// Token: 0x04001103 RID: 4355
		internal const int FALSE = 0;

		// Token: 0x04001104 RID: 4356
		internal const int TRUE = 1;

		// Token: 0x04001105 RID: 4357
		private static bool _LsaLookupNames2Supported;

		// Token: 0x04001106 RID: 4358
		private static bool _WellKnownSidApisSupported;
	}
}
