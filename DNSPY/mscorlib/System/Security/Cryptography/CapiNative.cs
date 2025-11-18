using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	// Token: 0x0200023C RID: 572
	internal static class CapiNative
	{
		// Token: 0x06002097 RID: 8343 RVA: 0x00072370 File Offset: 0x00070570
		[SecurityCritical]
		internal static SafeCspHandle AcquireCsp(string keyContainer, string providerName, CapiNative.ProviderType providerType, CapiNative.CryptAcquireContextFlags flags)
		{
			if ((flags & CapiNative.CryptAcquireContextFlags.VerifyContext) == CapiNative.CryptAcquireContextFlags.VerifyContext && (flags & CapiNative.CryptAcquireContextFlags.MachineKeyset) == CapiNative.CryptAcquireContextFlags.MachineKeyset)
			{
				flags &= ~CapiNative.CryptAcquireContextFlags.MachineKeyset;
			}
			SafeCspHandle safeCspHandle = null;
			if (!CapiNative.UnsafeNativeMethods.CryptAcquireContext(out safeCspHandle, keyContainer, providerName, providerType, flags))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			return safeCspHandle;
		}

		// Token: 0x06002098 RID: 8344 RVA: 0x000723B4 File Offset: 0x000705B4
		[SecurityCritical]
		internal static SafeCspHashHandle CreateHashAlgorithm(SafeCspHandle cspHandle, CapiNative.AlgorithmID algorithm)
		{
			SafeCspHashHandle safeCspHashHandle = null;
			if (!CapiNative.UnsafeNativeMethods.CryptCreateHash(cspHandle, algorithm, IntPtr.Zero, 0, out safeCspHashHandle))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			return safeCspHashHandle;
		}

		// Token: 0x06002099 RID: 8345 RVA: 0x000723E0 File Offset: 0x000705E0
		[SecurityCritical]
		internal static void GenerateRandomBytes(SafeCspHandle cspHandle, byte[] buffer)
		{
			if (!CapiNative.UnsafeNativeMethods.CryptGenRandom(cspHandle, buffer.Length, buffer))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x000723FC File Offset: 0x000705FC
		[SecurityCritical]
		internal unsafe static void GenerateRandomBytes(SafeCspHandle cspHandle, byte[] buffer, int offset, int count)
		{
			fixed (byte* ptr = &buffer[offset])
			{
				byte* ptr2 = ptr;
				if (!CapiNative.UnsafeNativeMethods.CryptGenRandom(cspHandle, count, ptr2))
				{
					throw new CryptographicException(Marshal.GetLastWin32Error());
				}
			}
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x0007242C File Offset: 0x0007062C
		[SecurityCritical]
		internal static int GetHashPropertyInt32(SafeCspHashHandle hashHandle, CapiNative.HashProperty property)
		{
			byte[] hashProperty = CapiNative.GetHashProperty(hashHandle, property);
			if (hashProperty.Length != 4)
			{
				return 0;
			}
			return BitConverter.ToInt32(hashProperty, 0);
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x00072450 File Offset: 0x00070650
		[SecurityCritical]
		internal static byte[] GetHashProperty(SafeCspHashHandle hashHandle, CapiNative.HashProperty property)
		{
			int num = 0;
			byte[] array = null;
			if (!CapiNative.UnsafeNativeMethods.CryptGetHashParam(hashHandle, property, array, ref num, 0))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error != 234)
				{
					throw new CryptographicException(lastWin32Error);
				}
			}
			array = new byte[num];
			if (!CapiNative.UnsafeNativeMethods.CryptGetHashParam(hashHandle, property, array, ref num, 0))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			return array;
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x000724A4 File Offset: 0x000706A4
		[SecurityCritical]
		internal static int GetKeyPropertyInt32(SafeCspKeyHandle keyHandle, CapiNative.KeyProperty property)
		{
			byte[] keyProperty = CapiNative.GetKeyProperty(keyHandle, property);
			if (keyProperty.Length != 4)
			{
				return 0;
			}
			return BitConverter.ToInt32(keyProperty, 0);
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x000724C8 File Offset: 0x000706C8
		[SecurityCritical]
		internal static byte[] GetKeyProperty(SafeCspKeyHandle keyHandle, CapiNative.KeyProperty property)
		{
			int num = 0;
			byte[] array = null;
			if (!CapiNative.UnsafeNativeMethods.CryptGetKeyParam(keyHandle, property, array, ref num, 0))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error != 234)
				{
					throw new CryptographicException(lastWin32Error);
				}
			}
			array = new byte[num];
			if (!CapiNative.UnsafeNativeMethods.CryptGetKeyParam(keyHandle, property, array, ref num, 0))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
			return array;
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x0007251B File Offset: 0x0007071B
		[SecurityCritical]
		internal static void SetHashProperty(SafeCspHashHandle hashHandle, CapiNative.HashProperty property, byte[] value)
		{
			if (!CapiNative.UnsafeNativeMethods.CryptSetHashParam(hashHandle, property, value, 0))
			{
				throw new CryptographicException(Marshal.GetLastWin32Error());
			}
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x00072534 File Offset: 0x00070734
		[SecurityCritical]
		internal static bool VerifySignature(SafeCspHandle cspHandle, SafeCspKeyHandle keyHandle, CapiNative.AlgorithmID signatureAlgorithm, CapiNative.AlgorithmID hashAlgorithm, byte[] hashValue, byte[] signature)
		{
			byte[] array = new byte[signature.Length];
			Array.Copy(signature, array, array.Length);
			Array.Reverse(array);
			bool flag;
			using (SafeCspHashHandle safeCspHashHandle = CapiNative.CreateHashAlgorithm(cspHandle, hashAlgorithm))
			{
				if (hashValue.Length != CapiNative.GetHashPropertyInt32(safeCspHashHandle, CapiNative.HashProperty.HashSize))
				{
					throw new CryptographicException(-2146893822);
				}
				CapiNative.SetHashProperty(safeCspHashHandle, CapiNative.HashProperty.HashValue, hashValue);
				if (CapiNative.UnsafeNativeMethods.CryptVerifySignature(safeCspHashHandle, array, array.Length, keyHandle, null, 0))
				{
					flag = true;
				}
				else
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error != -2146893818)
					{
						throw new CryptographicException(lastWin32Error);
					}
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x02000B36 RID: 2870
		internal enum AlgorithmClass
		{
			// Token: 0x04003364 RID: 13156
			Any,
			// Token: 0x04003365 RID: 13157
			Signature = 8192,
			// Token: 0x04003366 RID: 13158
			DataEncrypt = 24576,
			// Token: 0x04003367 RID: 13159
			Hash = 32768,
			// Token: 0x04003368 RID: 13160
			KeyExchange = 40960
		}

		// Token: 0x02000B37 RID: 2871
		internal enum AlgorithmType
		{
			// Token: 0x0400336A RID: 13162
			Any,
			// Token: 0x0400336B RID: 13163
			Rsa = 1024,
			// Token: 0x0400336C RID: 13164
			Block = 1536
		}

		// Token: 0x02000B38 RID: 2872
		internal enum AlgorithmSubId
		{
			// Token: 0x0400336E RID: 13166
			Any,
			// Token: 0x0400336F RID: 13167
			RsaAny = 0,
			// Token: 0x04003370 RID: 13168
			Rc2 = 2,
			// Token: 0x04003371 RID: 13169
			Md5,
			// Token: 0x04003372 RID: 13170
			Sha1,
			// Token: 0x04003373 RID: 13171
			Sha256 = 12,
			// Token: 0x04003374 RID: 13172
			Sha384,
			// Token: 0x04003375 RID: 13173
			Sha512,
			// Token: 0x04003376 RID: 13174
			Hmac = 9
		}

		// Token: 0x02000B39 RID: 2873
		internal enum AlgorithmID
		{
			// Token: 0x04003378 RID: 13176
			None,
			// Token: 0x04003379 RID: 13177
			RsaSign = 9216,
			// Token: 0x0400337A RID: 13178
			RsaKeyExchange = 41984,
			// Token: 0x0400337B RID: 13179
			Rc2 = 26114,
			// Token: 0x0400337C RID: 13180
			Md5 = 32771,
			// Token: 0x0400337D RID: 13181
			Sha1,
			// Token: 0x0400337E RID: 13182
			Sha256 = 32780,
			// Token: 0x0400337F RID: 13183
			Sha384,
			// Token: 0x04003380 RID: 13184
			Sha512,
			// Token: 0x04003381 RID: 13185
			Hmac = 32777
		}

		// Token: 0x02000B3A RID: 2874
		[Flags]
		internal enum CryptAcquireContextFlags
		{
			// Token: 0x04003383 RID: 13187
			None = 0,
			// Token: 0x04003384 RID: 13188
			NewKeyset = 8,
			// Token: 0x04003385 RID: 13189
			DeleteKeyset = 16,
			// Token: 0x04003386 RID: 13190
			MachineKeyset = 32,
			// Token: 0x04003387 RID: 13191
			Silent = 64,
			// Token: 0x04003388 RID: 13192
			VerifyContext = -268435456
		}

		// Token: 0x02000B3B RID: 2875
		internal enum ErrorCode
		{
			// Token: 0x0400338A RID: 13194
			Ok,
			// Token: 0x0400338B RID: 13195
			MoreData = 234,
			// Token: 0x0400338C RID: 13196
			BadHash = -2146893822,
			// Token: 0x0400338D RID: 13197
			BadData = -2146893819,
			// Token: 0x0400338E RID: 13198
			BadSignature,
			// Token: 0x0400338F RID: 13199
			NoKey = -2146893811
		}

		// Token: 0x02000B3C RID: 2876
		internal enum HashProperty
		{
			// Token: 0x04003391 RID: 13201
			None,
			// Token: 0x04003392 RID: 13202
			HashValue = 2,
			// Token: 0x04003393 RID: 13203
			HashSize = 4,
			// Token: 0x04003394 RID: 13204
			HmacInfo
		}

		// Token: 0x02000B3D RID: 2877
		[Flags]
		internal enum KeyGenerationFlags
		{
			// Token: 0x04003396 RID: 13206
			None = 0,
			// Token: 0x04003397 RID: 13207
			Exportable = 1,
			// Token: 0x04003398 RID: 13208
			UserProtected = 2,
			// Token: 0x04003399 RID: 13209
			Archivable = 16384
		}

		// Token: 0x02000B3E RID: 2878
		internal enum KeyProperty
		{
			// Token: 0x0400339B RID: 13211
			None,
			// Token: 0x0400339C RID: 13212
			AlgorithmID = 7,
			// Token: 0x0400339D RID: 13213
			KeyLength = 9
		}

		// Token: 0x02000B3F RID: 2879
		internal enum KeySpec
		{
			// Token: 0x0400339F RID: 13215
			KeyExchange = 1,
			// Token: 0x040033A0 RID: 13216
			Signature
		}

		// Token: 0x02000B40 RID: 2880
		internal static class ProviderNames
		{
			// Token: 0x040033A1 RID: 13217
			internal const string MicrosoftEnhanced = "Microsoft Enhanced Cryptographic Provider v1.0";
		}

		// Token: 0x02000B41 RID: 2881
		internal enum ProviderType
		{
			// Token: 0x040033A3 RID: 13219
			RsaFull = 1,
			// Token: 0x040033A4 RID: 13220
			RsaAes = 24
		}

		// Token: 0x02000B42 RID: 2882
		[SecurityCritical]
		[SuppressUnmanagedCodeSecurity]
		internal static class SafeNativeMethods
		{
			// Token: 0x06006B7E RID: 27518
			[DllImport("advapi32", CharSet = CharSet.Unicode, SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptAcquireContext(out SafeCspHandle phProv, IntPtr pszContainer, IntPtr pszProvider, CapiNative.ProviderType dwProvType, CapiNative.CryptAcquireContextFlags dwFlags);

			// Token: 0x06006B7F RID: 27519
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptCreateHash(SafeCspHandle hProv, CapiNative.AlgorithmID Algid, IntPtr hKey, int dwFlags, out SafeCspHashHandle phHash);

			// Token: 0x06006B80 RID: 27520
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptGetHashParam(SafeCspHashHandle hHash, CapiNative.HashProperty dwParam, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] byte[] pbData, [In] [Out] ref int pdwDataLen, int dwFlags);

			// Token: 0x06006B81 RID: 27521
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptHashData(SafeCspHashHandle hHash, IntPtr pbData, int dwDataLen, int dwFlags);

			// Token: 0x06006B82 RID: 27522
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptImportKey(SafeCspHandle hProv, IntPtr pbData, int dwDataLen, IntPtr hPubKey, CapiNative.KeyGenerationFlags dwFlags, out SafeCspKeyHandle phKey);

			// Token: 0x06006B83 RID: 27523
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptSetHashParam(SafeCspHashHandle hHash, CapiNative.HashProperty dwParam, IntPtr pbData, int dwFlags);

			// Token: 0x06006B84 RID: 27524 RVA: 0x00173811 File Offset: 0x00171A11
			// Note: this type is marked as 'beforefieldinit'.
			static SafeNativeMethods()
			{
			}

			// Token: 0x040033A5 RID: 13221
			internal static readonly Lazy<SafeCspHandle> DefaultProvider = new Lazy<SafeCspHandle>(delegate
			{
				SafeCspHandle safeCspHandle;
				if (!CapiNative.SafeNativeMethods.CryptAcquireContext(out safeCspHandle, IntPtr.Zero, IntPtr.Zero, CapiNative.ProviderType.RsaAes, CapiNative.CryptAcquireContextFlags.VerifyContext))
				{
					Exception ex = new CryptographicException(Marshal.GetLastWin32Error());
					safeCspHandle.Dispose();
					throw ex;
				}
				return safeCspHandle;
			});

			// Token: 0x02000D02 RID: 3330
			[CompilerGenerated]
			[Serializable]
			private sealed class <>c
			{
				// Token: 0x060071F5 RID: 29173 RVA: 0x0018874F File Offset: 0x0018694F
				// Note: this type is marked as 'beforefieldinit'.
				static <>c()
				{
				}

				// Token: 0x060071F6 RID: 29174 RVA: 0x0018875B File Offset: 0x0018695B
				public <>c()
				{
				}

				// Token: 0x060071F7 RID: 29175 RVA: 0x00188764 File Offset: 0x00186964
				internal SafeCspHandle <.cctor>b__7_0()
				{
					SafeCspHandle safeCspHandle;
					if (!CapiNative.SafeNativeMethods.CryptAcquireContext(out safeCspHandle, IntPtr.Zero, IntPtr.Zero, CapiNative.ProviderType.RsaAes, CapiNative.CryptAcquireContextFlags.VerifyContext))
					{
						Exception ex = new CryptographicException(Marshal.GetLastWin32Error());
						safeCspHandle.Dispose();
						throw ex;
					}
					return safeCspHandle;
				}

				// Token: 0x04003934 RID: 14644
				public static readonly CapiNative.SafeNativeMethods.<>c <>9 = new CapiNative.SafeNativeMethods.<>c();
			}
		}

		// Token: 0x02000B43 RID: 2883
		[SecurityCritical]
		internal static class UnsafeNativeMethods
		{
			// Token: 0x06006B85 RID: 27525
			[DllImport("advapi32", CharSet = CharSet.Unicode, SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptAcquireContext(out SafeCspHandle phProv, string pszContainer, string pszProvider, CapiNative.ProviderType dwProvType, CapiNative.CryptAcquireContextFlags dwFlags);

			// Token: 0x06006B86 RID: 27526
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptCreateHash(SafeCspHandle hProv, CapiNative.AlgorithmID Algid, IntPtr hKey, int dwFlags, out SafeCspHashHandle phHash);

			// Token: 0x06006B87 RID: 27527
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptGenKey(SafeCspHandle hProv, int Algid, uint dwFlags, out SafeCspKeyHandle phKey);

			// Token: 0x06006B88 RID: 27528
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptGenRandom(SafeCspHandle hProv, int dwLen, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] byte[] pbBuffer);

			// Token: 0x06006B89 RID: 27529
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal unsafe static extern bool CryptGenRandom(SafeCspHandle hProv, int dwLen, byte* pbBuffer);

			// Token: 0x06006B8A RID: 27530
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptGetHashParam(SafeCspHashHandle hHash, CapiNative.HashProperty dwParam, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] byte[] pbData, [In] [Out] ref int pdwDataLen, int dwFlags);

			// Token: 0x06006B8B RID: 27531
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptGetKeyParam(SafeCspKeyHandle hKey, CapiNative.KeyProperty dwParam, [MarshalAs(UnmanagedType.LPArray)] [In] [Out] byte[] pbData, [In] [Out] ref int pdwDataLen, int dwFlags);

			// Token: 0x06006B8C RID: 27532
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptImportKey(SafeCspHandle hProv, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbData, int pdwDataLen, IntPtr hPubKey, CapiNative.KeyGenerationFlags dwFlags, out SafeCspKeyHandle phKey);

			// Token: 0x06006B8D RID: 27533
			[DllImport("advapi32", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptSetHashParam(SafeCspHashHandle hHash, CapiNative.HashProperty dwParam, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbData, int dwFlags);

			// Token: 0x06006B8E RID: 27534
			[DllImport("advapi32", CharSet = CharSet.Unicode, SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			internal static extern bool CryptVerifySignature(SafeCspHashHandle hHash, [MarshalAs(UnmanagedType.LPArray)] [In] byte[] pbSignature, int dwSigLen, SafeCspKeyHandle hPubKey, string sDescription, int dwFlags);
		}
	}
}
