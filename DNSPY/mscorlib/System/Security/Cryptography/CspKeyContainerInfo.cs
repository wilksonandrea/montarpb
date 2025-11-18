using System;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x0200026C RID: 620
	[ComVisible(true)]
	public sealed class CspKeyContainerInfo
	{
		// Token: 0x060021F9 RID: 8697 RVA: 0x000780B6 File Offset: 0x000762B6
		private CspKeyContainerInfo()
		{
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x000780C0 File Offset: 0x000762C0
		[SecurityCritical]
		internal CspKeyContainerInfo(CspParameters parameters, bool randomKeyContainer)
		{
			if (!CompatibilitySwitches.IsAppEarlierThanWindowsPhone8)
			{
				KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
				KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry = new KeyContainerPermissionAccessEntry(parameters, KeyContainerPermissionFlags.Open);
				keyContainerPermission.AccessEntries.Add(keyContainerPermissionAccessEntry);
				keyContainerPermission.Demand();
			}
			this.m_parameters = new CspParameters(parameters);
			if (this.m_parameters.KeyNumber == -1)
			{
				if (this.m_parameters.ProviderType == 1 || this.m_parameters.ProviderType == 24)
				{
					this.m_parameters.KeyNumber = 1;
				}
				else if (this.m_parameters.ProviderType == 13)
				{
					this.m_parameters.KeyNumber = 2;
				}
			}
			this.m_randomKeyContainer = randomKeyContainer;
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x00078163 File Offset: 0x00076363
		[SecuritySafeCritical]
		public CspKeyContainerInfo(CspParameters parameters)
			: this(parameters, false)
		{
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x060021FC RID: 8700 RVA: 0x0007816D File Offset: 0x0007636D
		public bool MachineKeyStore
		{
			get
			{
				return (this.m_parameters.Flags & CspProviderFlags.UseMachineKeyStore) == CspProviderFlags.UseMachineKeyStore;
			}
		}

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x060021FD RID: 8701 RVA: 0x00078182 File Offset: 0x00076382
		public string ProviderName
		{
			get
			{
				return this.m_parameters.ProviderName;
			}
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x060021FE RID: 8702 RVA: 0x0007818F File Offset: 0x0007638F
		public int ProviderType
		{
			get
			{
				return this.m_parameters.ProviderType;
			}
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x060021FF RID: 8703 RVA: 0x0007819C File Offset: 0x0007639C
		public string KeyContainerName
		{
			get
			{
				return this.m_parameters.KeyContainerName;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06002200 RID: 8704 RVA: 0x000781AC File Offset: 0x000763AC
		public string UniqueKeyContainerName
		{
			[SecuritySafeCritical]
			get
			{
				SafeProvHandle invalidHandle = SafeProvHandle.InvalidHandle;
				int num = Utils._OpenCSP(this.m_parameters, 64U, ref invalidHandle);
				if (num != 0)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_NotFound"));
				}
				string text = (string)Utils._GetProviderParameter(invalidHandle, this.m_parameters.KeyNumber, 8U);
				invalidHandle.Dispose();
				return text;
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06002201 RID: 8705 RVA: 0x00078201 File Offset: 0x00076401
		public KeyNumber KeyNumber
		{
			get
			{
				return (KeyNumber)this.m_parameters.KeyNumber;
			}
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x06002202 RID: 8706 RVA: 0x00078210 File Offset: 0x00076410
		public bool Exportable
		{
			[SecuritySafeCritical]
			get
			{
				if (this.HardwareDevice)
				{
					return false;
				}
				SafeProvHandle invalidHandle = SafeProvHandle.InvalidHandle;
				int num = Utils._OpenCSP(this.m_parameters, 64U, ref invalidHandle);
				if (num != 0)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_NotFound"));
				}
				byte[] array = (byte[])Utils._GetProviderParameter(invalidHandle, this.m_parameters.KeyNumber, 3U);
				invalidHandle.Dispose();
				return array[0] == 1;
			}
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x06002203 RID: 8707 RVA: 0x00078274 File Offset: 0x00076474
		public bool HardwareDevice
		{
			[SecuritySafeCritical]
			get
			{
				SafeProvHandle invalidHandle = SafeProvHandle.InvalidHandle;
				CspParameters cspParameters = new CspParameters(this.m_parameters);
				cspParameters.KeyContainerName = null;
				cspParameters.Flags = (((cspParameters.Flags & CspProviderFlags.UseMachineKeyStore) != CspProviderFlags.NoFlags) ? CspProviderFlags.UseMachineKeyStore : CspProviderFlags.NoFlags);
				uint num = 4026531840U;
				int num2 = Utils._OpenCSP(cspParameters, num, ref invalidHandle);
				if (num2 != 0)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_NotFound"));
				}
				byte[] array = (byte[])Utils._GetProviderParameter(invalidHandle, cspParameters.KeyNumber, 5U);
				invalidHandle.Dispose();
				return array[0] == 1;
			}
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06002204 RID: 8708 RVA: 0x000782F4 File Offset: 0x000764F4
		public bool Removable
		{
			[SecuritySafeCritical]
			get
			{
				SafeProvHandle invalidHandle = SafeProvHandle.InvalidHandle;
				CspParameters cspParameters = new CspParameters(this.m_parameters);
				cspParameters.KeyContainerName = null;
				cspParameters.Flags = (((cspParameters.Flags & CspProviderFlags.UseMachineKeyStore) != CspProviderFlags.NoFlags) ? CspProviderFlags.UseMachineKeyStore : CspProviderFlags.NoFlags);
				uint num = 4026531840U;
				int num2 = Utils._OpenCSP(cspParameters, num, ref invalidHandle);
				if (num2 != 0)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_NotFound"));
				}
				byte[] array = (byte[])Utils._GetProviderParameter(invalidHandle, cspParameters.KeyNumber, 4U);
				invalidHandle.Dispose();
				return array[0] == 1;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06002205 RID: 8709 RVA: 0x00078374 File Offset: 0x00076574
		public bool Accessible
		{
			[SecuritySafeCritical]
			get
			{
				SafeProvHandle invalidHandle = SafeProvHandle.InvalidHandle;
				int num = Utils._OpenCSP(this.m_parameters, 64U, ref invalidHandle);
				if (num != 0)
				{
					return false;
				}
				byte[] array = (byte[])Utils._GetProviderParameter(invalidHandle, this.m_parameters.KeyNumber, 6U);
				invalidHandle.Dispose();
				return array[0] == 1;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06002206 RID: 8710 RVA: 0x000783C0 File Offset: 0x000765C0
		public bool Protected
		{
			[SecuritySafeCritical]
			get
			{
				if (this.HardwareDevice)
				{
					return true;
				}
				SafeProvHandle invalidHandle = SafeProvHandle.InvalidHandle;
				int num = Utils._OpenCSP(this.m_parameters, 64U, ref invalidHandle);
				if (num != 0)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_NotFound"));
				}
				byte[] array = (byte[])Utils._GetProviderParameter(invalidHandle, this.m_parameters.KeyNumber, 7U);
				invalidHandle.Dispose();
				return array[0] == 1;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06002207 RID: 8711 RVA: 0x00078424 File Offset: 0x00076624
		public CryptoKeySecurity CryptoKeySecurity
		{
			[SecuritySafeCritical]
			get
			{
				KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
				KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry = new KeyContainerPermissionAccessEntry(this.m_parameters, KeyContainerPermissionFlags.ViewAcl | KeyContainerPermissionFlags.ChangeAcl);
				keyContainerPermission.AccessEntries.Add(keyContainerPermissionAccessEntry);
				keyContainerPermission.Demand();
				SafeProvHandle invalidHandle = SafeProvHandle.InvalidHandle;
				int num = Utils._OpenCSP(this.m_parameters, 64U, ref invalidHandle);
				if (num != 0)
				{
					throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_NotFound"));
				}
				CryptoKeySecurity keySetSecurityInfo;
				using (invalidHandle)
				{
					keySetSecurityInfo = Utils.GetKeySetSecurityInfo(invalidHandle, AccessControlSections.All);
				}
				return keySetSecurityInfo;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06002208 RID: 8712 RVA: 0x000784B4 File Offset: 0x000766B4
		public bool RandomlyGenerated
		{
			get
			{
				return this.m_randomKeyContainer;
			}
		}

		// Token: 0x04000C5D RID: 3165
		private CspParameters m_parameters;

		// Token: 0x04000C5E RID: 3166
		private bool m_randomKeyContainer;
	}
}
