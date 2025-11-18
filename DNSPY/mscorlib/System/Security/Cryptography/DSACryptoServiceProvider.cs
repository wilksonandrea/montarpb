using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Security.Cryptography
{
	// Token: 0x0200025F RID: 607
	[ComVisible(true)]
	public sealed class DSACryptoServiceProvider : DSA, ICspAsymmetricAlgorithm
	{
		// Token: 0x06002183 RID: 8579 RVA: 0x00076848 File Offset: 0x00074A48
		public DSACryptoServiceProvider()
			: this(0, new CspParameters(13, null, null, DSACryptoServiceProvider.s_UseMachineKeyStore))
		{
		}

		// Token: 0x06002184 RID: 8580 RVA: 0x00076861 File Offset: 0x00074A61
		public DSACryptoServiceProvider(int dwKeySize)
			: this(dwKeySize, new CspParameters(13, null, null, DSACryptoServiceProvider.s_UseMachineKeyStore))
		{
		}

		// Token: 0x06002185 RID: 8581 RVA: 0x0007687A File Offset: 0x00074A7A
		public DSACryptoServiceProvider(CspParameters parameters)
			: this(0, parameters)
		{
		}

		// Token: 0x06002186 RID: 8582 RVA: 0x00076884 File Offset: 0x00074A84
		[SecuritySafeCritical]
		public DSACryptoServiceProvider(int dwKeySize, CspParameters parameters)
		{
			if (dwKeySize < 0)
			{
				throw new ArgumentOutOfRangeException("dwKeySize", Environment.GetResourceString("ArgumentOutOfRange_NeedNonNegNum"));
			}
			this._parameters = Utils.SaveCspParameters(CspAlgorithmType.Dss, parameters, DSACryptoServiceProvider.s_UseMachineKeyStore, ref this._randomKeyContainer);
			this.LegalKeySizesValue = new KeySizes[]
			{
				new KeySizes(512, 1024, 64)
			};
			this._dwKeySize = dwKeySize;
			this._sha1 = new SHA1CryptoServiceProvider();
			if (!this._randomKeyContainer || Environment.GetCompatibilityFlag(CompatibilityFlag.EagerlyGenerateRandomAsymmKeys))
			{
				this.GetKeyPair();
			}
		}

		// Token: 0x06002187 RID: 8583 RVA: 0x00076914 File Offset: 0x00074B14
		[SecurityCritical]
		private void GetKeyPair()
		{
			if (this._safeKeyHandle == null)
			{
				lock (this)
				{
					if (this._safeKeyHandle == null)
					{
						Utils.GetKeyPairHelper(CspAlgorithmType.Dss, this._parameters, this._randomKeyContainer, this._dwKeySize, ref this._safeProvHandle, ref this._safeKeyHandle);
					}
				}
			}
		}

		// Token: 0x06002188 RID: 8584 RVA: 0x00076980 File Offset: 0x00074B80
		[SecuritySafeCritical]
		protected override void Dispose(bool disposing)
		{
			base.Dispose(disposing);
			if (this._safeKeyHandle != null && !this._safeKeyHandle.IsClosed)
			{
				this._safeKeyHandle.Dispose();
			}
			if (this._safeProvHandle != null && !this._safeProvHandle.IsClosed)
			{
				this._safeProvHandle.Dispose();
			}
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x06002189 RID: 8585 RVA: 0x000769D4 File Offset: 0x00074BD4
		[ComVisible(false)]
		public bool PublicOnly
		{
			[SecuritySafeCritical]
			get
			{
				this.GetKeyPair();
				byte[] array = Utils._GetKeyParameter(this._safeKeyHandle, 2U);
				return array[0] == 1;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x0600218A RID: 8586 RVA: 0x000769FA File Offset: 0x00074BFA
		[ComVisible(false)]
		public CspKeyContainerInfo CspKeyContainerInfo
		{
			[SecuritySafeCritical]
			get
			{
				this.GetKeyPair();
				return new CspKeyContainerInfo(this._parameters, this._randomKeyContainer);
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x0600218B RID: 8587 RVA: 0x00076A14 File Offset: 0x00074C14
		public override int KeySize
		{
			[SecuritySafeCritical]
			get
			{
				this.GetKeyPair();
				byte[] array = Utils._GetKeyParameter(this._safeKeyHandle, 1U);
				this._dwKeySize = (int)array[0] | ((int)array[1] << 8) | ((int)array[2] << 16) | ((int)array[3] << 24);
				return this._dwKeySize;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x0600218C RID: 8588 RVA: 0x00076A57 File Offset: 0x00074C57
		public override string KeyExchangeAlgorithm
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x0600218D RID: 8589 RVA: 0x00076A5A File Offset: 0x00074C5A
		public override string SignatureAlgorithm
		{
			get
			{
				return "http://www.w3.org/2000/09/xmldsig#dsa-sha1";
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x0600218E RID: 8590 RVA: 0x00076A61 File Offset: 0x00074C61
		// (set) Token: 0x0600218F RID: 8591 RVA: 0x00076A6D File Offset: 0x00074C6D
		public static bool UseMachineKeyStore
		{
			get
			{
				return DSACryptoServiceProvider.s_UseMachineKeyStore == CspProviderFlags.UseMachineKeyStore;
			}
			set
			{
				DSACryptoServiceProvider.s_UseMachineKeyStore = (value ? CspProviderFlags.UseMachineKeyStore : CspProviderFlags.NoFlags);
			}
		}

		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06002190 RID: 8592 RVA: 0x00076A80 File Offset: 0x00074C80
		// (set) Token: 0x06002191 RID: 8593 RVA: 0x00076AE8 File Offset: 0x00074CE8
		public bool PersistKeyInCsp
		{
			[SecuritySafeCritical]
			get
			{
				if (this._safeProvHandle == null)
				{
					lock (this)
					{
						if (this._safeProvHandle == null)
						{
							this._safeProvHandle = Utils.CreateProvHandle(this._parameters, this._randomKeyContainer);
						}
					}
				}
				return Utils.GetPersistKeyInCsp(this._safeProvHandle);
			}
			[SecuritySafeCritical]
			set
			{
				bool persistKeyInCsp = this.PersistKeyInCsp;
				if (value == persistKeyInCsp)
				{
					return;
				}
				KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
				if (!value)
				{
					KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Delete);
					keyContainerPermission.AccessEntries.Add(keyContainerPermissionAccessEntry);
				}
				else
				{
					KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry2 = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Create);
					keyContainerPermission.AccessEntries.Add(keyContainerPermissionAccessEntry2);
				}
				keyContainerPermission.Demand();
				Utils.SetPersistKeyInCsp(this._safeProvHandle, value);
			}
		}

		// Token: 0x06002192 RID: 8594 RVA: 0x00076B54 File Offset: 0x00074D54
		[SecuritySafeCritical]
		public override DSAParameters ExportParameters(bool includePrivateParameters)
		{
			this.GetKeyPair();
			if (includePrivateParameters)
			{
				KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
				KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Export);
				keyContainerPermission.AccessEntries.Add(keyContainerPermissionAccessEntry);
				keyContainerPermission.Demand();
			}
			DSACspObject dsacspObject = new DSACspObject();
			int num = (includePrivateParameters ? 7 : 6);
			Utils._ExportKey(this._safeKeyHandle, num, dsacspObject);
			return DSACryptoServiceProvider.DSAObjectToStruct(dsacspObject);
		}

		// Token: 0x06002193 RID: 8595 RVA: 0x00076BB3 File Offset: 0x00074DB3
		[SecuritySafeCritical]
		[ComVisible(false)]
		public byte[] ExportCspBlob(bool includePrivateParameters)
		{
			this.GetKeyPair();
			return Utils.ExportCspBlobHelper(includePrivateParameters, this._parameters, this._safeKeyHandle);
		}

		// Token: 0x06002194 RID: 8596 RVA: 0x00076BD0 File Offset: 0x00074DD0
		[SecuritySafeCritical]
		public override void ImportParameters(DSAParameters parameters)
		{
			DSACspObject dsacspObject = DSACryptoServiceProvider.DSAStructToObject(parameters);
			if (this._safeKeyHandle != null && !this._safeKeyHandle.IsClosed)
			{
				this._safeKeyHandle.Dispose();
			}
			this._safeKeyHandle = SafeKeyHandle.InvalidHandle;
			if (DSACryptoServiceProvider.IsPublic(parameters))
			{
				Utils._ImportKey(Utils.StaticDssProvHandle, 8704, CspProviderFlags.NoFlags, dsacspObject, ref this._safeKeyHandle);
				return;
			}
			KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
			KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Import);
			keyContainerPermission.AccessEntries.Add(keyContainerPermissionAccessEntry);
			keyContainerPermission.Demand();
			if (this._safeProvHandle == null)
			{
				this._safeProvHandle = Utils.CreateProvHandle(this._parameters, this._randomKeyContainer);
			}
			Utils._ImportKey(this._safeProvHandle, 8704, this._parameters.Flags, dsacspObject, ref this._safeKeyHandle);
		}

		// Token: 0x06002195 RID: 8597 RVA: 0x00076C98 File Offset: 0x00074E98
		[SecuritySafeCritical]
		[ComVisible(false)]
		public void ImportCspBlob(byte[] keyBlob)
		{
			Utils.ImportCspBlobHelper(CspAlgorithmType.Dss, keyBlob, DSACryptoServiceProvider.IsPublic(keyBlob), ref this._parameters, this._randomKeyContainer, ref this._safeProvHandle, ref this._safeKeyHandle);
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x00076CC0 File Offset: 0x00074EC0
		public byte[] SignData(Stream inputStream)
		{
			byte[] array = this._sha1.ComputeHash(inputStream);
			return this.SignHash(array, null);
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x00076CE4 File Offset: 0x00074EE4
		public byte[] SignData(byte[] buffer)
		{
			byte[] array = this._sha1.ComputeHash(buffer);
			return this.SignHash(array, null);
		}

		// Token: 0x06002198 RID: 8600 RVA: 0x00076D08 File Offset: 0x00074F08
		public byte[] SignData(byte[] buffer, int offset, int count)
		{
			byte[] array = this._sha1.ComputeHash(buffer, offset, count);
			return this.SignHash(array, null);
		}

		// Token: 0x06002199 RID: 8601 RVA: 0x00076D2C File Offset: 0x00074F2C
		public bool VerifyData(byte[] rgbData, byte[] rgbSignature)
		{
			byte[] array = this._sha1.ComputeHash(rgbData);
			return this.VerifyHash(array, null, rgbSignature);
		}

		// Token: 0x0600219A RID: 8602 RVA: 0x00076D4F File Offset: 0x00074F4F
		public override byte[] CreateSignature(byte[] rgbHash)
		{
			return this.SignHash(rgbHash, null);
		}

		// Token: 0x0600219B RID: 8603 RVA: 0x00076D59 File Offset: 0x00074F59
		public override bool VerifySignature(byte[] rgbHash, byte[] rgbSignature)
		{
			return this.VerifyHash(rgbHash, null, rgbSignature);
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x00076D64 File Offset: 0x00074F64
		protected override byte[] HashData(byte[] data, int offset, int count, HashAlgorithmName hashAlgorithm)
		{
			if (hashAlgorithm != HashAlgorithmName.SHA1)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_UnknownHashAlgorithm", new object[] { hashAlgorithm.Name }));
			}
			return this._sha1.ComputeHash(data, offset, count);
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x00076DA2 File Offset: 0x00074FA2
		protected override byte[] HashData(Stream data, HashAlgorithmName hashAlgorithm)
		{
			if (hashAlgorithm != HashAlgorithmName.SHA1)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_UnknownHashAlgorithm", new object[] { hashAlgorithm.Name }));
			}
			return this._sha1.ComputeHash(data);
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x00076DE0 File Offset: 0x00074FE0
		[SecuritySafeCritical]
		public byte[] SignHash(byte[] rgbHash, string str)
		{
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			if (this.PublicOnly)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_CSP_NoPrivateKey"));
			}
			int num = X509Utils.NameOrOidToAlgId(str, OidGroup.HashAlgorithm);
			if (rgbHash.Length != this._sha1.HashSize / 8)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidHashSize", new object[]
				{
					"SHA1",
					this._sha1.HashSize / 8
				}));
			}
			this.GetKeyPair();
			if (!this.CspKeyContainerInfo.RandomlyGenerated)
			{
				KeyContainerPermission keyContainerPermission = new KeyContainerPermission(KeyContainerPermissionFlags.NoFlags);
				KeyContainerPermissionAccessEntry keyContainerPermissionAccessEntry = new KeyContainerPermissionAccessEntry(this._parameters, KeyContainerPermissionFlags.Sign);
				keyContainerPermission.AccessEntries.Add(keyContainerPermissionAccessEntry);
				keyContainerPermission.Demand();
			}
			return Utils.SignValue(this._safeKeyHandle, this._parameters.KeyNumber, 8704, num, rgbHash);
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x00076EBC File Offset: 0x000750BC
		[SecuritySafeCritical]
		public bool VerifyHash(byte[] rgbHash, string str, byte[] rgbSignature)
		{
			if (rgbHash == null)
			{
				throw new ArgumentNullException("rgbHash");
			}
			if (rgbSignature == null)
			{
				throw new ArgumentNullException("rgbSignature");
			}
			int num = X509Utils.NameOrOidToAlgId(str, OidGroup.HashAlgorithm);
			if (rgbHash.Length != this._sha1.HashSize / 8)
			{
				throw new CryptographicException(Environment.GetResourceString("Cryptography_InvalidHashSize", new object[]
				{
					"SHA1",
					this._sha1.HashSize / 8
				}));
			}
			this.GetKeyPair();
			return Utils.VerifySign(this._safeKeyHandle, 8704, num, rgbHash, rgbSignature);
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x00076F4C File Offset: 0x0007514C
		private static DSAParameters DSAObjectToStruct(DSACspObject dsaCspObject)
		{
			return new DSAParameters
			{
				P = dsaCspObject.P,
				Q = dsaCspObject.Q,
				G = dsaCspObject.G,
				Y = dsaCspObject.Y,
				J = dsaCspObject.J,
				X = dsaCspObject.X,
				Seed = dsaCspObject.Seed,
				Counter = dsaCspObject.Counter
			};
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x00076FCC File Offset: 0x000751CC
		private static DSACspObject DSAStructToObject(DSAParameters dsaParams)
		{
			return new DSACspObject
			{
				P = dsaParams.P,
				Q = dsaParams.Q,
				G = dsaParams.G,
				Y = dsaParams.Y,
				J = dsaParams.J,
				X = dsaParams.X,
				Seed = dsaParams.Seed,
				Counter = dsaParams.Counter
			};
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x00077040 File Offset: 0x00075240
		private static bool IsPublic(DSAParameters dsaParams)
		{
			return dsaParams.X == null;
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x0007704C File Offset: 0x0007524C
		private static bool IsPublic(byte[] keyBlob)
		{
			if (keyBlob == null)
			{
				throw new ArgumentNullException("keyBlob");
			}
			return keyBlob[0] == 6 && (keyBlob[11] == 49 || keyBlob[11] == 51) && keyBlob[10] == 83 && keyBlob[9] == 83 && keyBlob[8] == 68;
		}

		// Token: 0x04000C40 RID: 3136
		private int _dwKeySize;

		// Token: 0x04000C41 RID: 3137
		private CspParameters _parameters;

		// Token: 0x04000C42 RID: 3138
		private bool _randomKeyContainer;

		// Token: 0x04000C43 RID: 3139
		[SecurityCritical]
		private SafeProvHandle _safeProvHandle;

		// Token: 0x04000C44 RID: 3140
		[SecurityCritical]
		private SafeKeyHandle _safeKeyHandle;

		// Token: 0x04000C45 RID: 3141
		private SHA1CryptoServiceProvider _sha1;

		// Token: 0x04000C46 RID: 3142
		private static volatile CspProviderFlags s_UseMachineKeyStore;
	}
}
