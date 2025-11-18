using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Plugin.Core.Utility
{
	// Token: 0x0200002C RID: 44
	public static class CryptoSyncer
	{
		// Token: 0x0600018C RID: 396 RVA: 0x00002A1F File Offset: 0x00000C1F
		static CryptoSyncer()
		{
		}

		// Token: 0x0600018D RID: 397 RVA: 0x000161AC File Offset: 0x000143AC
		private static byte[] smethod_0(byte[] byte_2)
		{
			byte[] array5;
			using (Aes aes = Aes.Create())
			{
				aes.Mode = CipherMode.ECB;
				aes.Padding = PaddingMode.None;
				aes.Key = CryptoSyncer.byte_0;
				using (ICryptoTransform cryptoTransform = aes.CreateEncryptor())
				{
					byte[] array = (byte[])CryptoSyncer.byte_1.Clone();
					byte[] array2 = new byte[byte_2.Length];
					for (int i = 0; i < byte_2.Length; i += 16)
					{
						byte[] array3 = cryptoTransform.TransformFinalBlock(array, 0, 16);
						int num = Math.Min(16, byte_2.Length - i);
						for (int j = 0; j < num; j++)
						{
							array2[i + j] = byte_2[i + j] ^ array3[j];
						}
						for (int k = 15; k >= 0; k--)
						{
							byte[] array4 = array;
							int num2 = k;
							byte b = array4[num2] + 1;
							array4[num2] = b;
							if (b != 0)
							{
								break;
							}
						}
					}
					array5 = array2;
				}
			}
			return array5;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00002A3F File Offset: 0x00000C3F
		public static byte[] Encrypt(byte[] Plain)
		{
			return CryptoSyncer.smethod_0(Plain);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00002A3F File Offset: 0x00000C3F
		public static byte[] Decrypt(byte[] Cipher)
		{
			return CryptoSyncer.smethod_0(Cipher);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000162AC File Offset: 0x000144AC
		public static void MD5File(string dist)
		{
			try
			{
				using (MD5 md = MD5.Create())
				{
					using (FileStream fileStream = File.OpenRead(dist))
					{
						string text = BitConverter.ToString(md.ComputeHash(fileStream)).Replace("-", "").ToLower();
						Console.WriteLine("MD5: " + text);
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0001633C File Offset: 0x0001453C
		public static char[] HexCodes(byte[] text)
		{
			char[] array = new char[text.Length * 2];
			for (int i = 0; i < text.Length; i++)
			{
				text[i].ToString("x2").CopyTo(0, array, i * 2, 2);
			}
			return array;
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00016380 File Offset: 0x00014580
		public static string SHA1File(string msg)
		{
			string text;
			try
			{
				using (SHA1 sha = SHA1.Create())
				{
					byte[] array = sha.ComputeHash(Encoding.UTF8.GetBytes(msg));
					StringBuilder stringBuilder = new StringBuilder();
					for (int i = 0; i < array.Length; i++)
					{
						stringBuilder.Append(array[i].ToString("x2"));
					}
					text = stringBuilder.ToString();
				}
			}
			catch (Exception)
			{
				text = "";
			}
			return text;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00016410 File Offset: 0x00014610
		public static int SHA1_Int32(string msg)
		{
			int num;
			try
			{
				num = int.Parse(CryptoSyncer.SHA1File(msg), NumberStyles.HexNumber);
			}
			catch (Exception)
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00016448 File Offset: 0x00014648
		public static string GetHWID()
		{
			string text2;
			try
			{
				string text = Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER") + Environment.GetEnvironmentVariable("COMPUTERNAME") + Environment.UserName.Trim();
				using (MD5 md = MD5.Create())
				{
					byte[] array = md.ComputeHash(Encoding.UTF8.GetBytes(text));
					StringBuilder stringBuilder = new StringBuilder();
					for (int i = 0; i < array.Length; i++)
					{
						stringBuilder.Append(array[i].ToString("x3").Substring(0, 3));
						if (i != array.Length - 1)
						{
							stringBuilder.Append("-");
						}
					}
					text2 = stringBuilder.ToString();
				}
			}
			catch (Exception)
			{
				text2 = "";
			}
			return text2;
		}

		// Token: 0x0400008C RID: 140
		private static readonly byte[] byte_0 = Bitwise.HexStringToByteArray("BC 34 88 F9 C3 00 B1 64 37 B0 6C B3 EE F3 33 3A");

		// Token: 0x0400008D RID: 141
		private static readonly byte[] byte_1 = Bitwise.HexStringToByteArray("71 F0 D9 9E 15 47 9A BA 72 C3 4F F8 04 27 D8 0A");
	}
}
