using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Plugin.Core.Utility
{
	public static class CryptoSyncer
	{
		private readonly static byte[] byte_0;

		private readonly static byte[] byte_1;

		static CryptoSyncer()
		{
			CryptoSyncer.byte_0 = Bitwise.HexStringToByteArray("BC 34 88 F9 C3 00 B1 64 37 B0 6C B3 EE F3 33 3A");
			CryptoSyncer.byte_1 = Bitwise.HexStringToByteArray("71 F0 D9 9E 15 47 9A BA 72 C3 4F F8 04 27 D8 0A");
		}

		public static byte[] Decrypt(byte[] Cipher)
		{
			return CryptoSyncer.smethod_0(Cipher);
		}

		public static byte[] Encrypt(byte[] Plain)
		{
			return CryptoSyncer.smethod_0(Plain);
		}

		public static string GetHWID()
		{
			string str;
			try
			{
				string str1 = string.Concat(Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER"), Environment.GetEnvironmentVariable("COMPUTERNAME"), Environment.UserName.Trim());
				using (MD5 mD5 = MD5.Create())
				{
					byte[] numArray = mD5.ComputeHash(Encoding.UTF8.GetBytes(str1));
					StringBuilder stringBuilder = new StringBuilder();
					for (int i = 0; i < (int)numArray.Length; i++)
					{
						stringBuilder.Append(numArray[i].ToString("x3").Substring(0, 3));
						if (i != (int)numArray.Length - 1)
						{
							stringBuilder.Append("-");
						}
					}
					str = stringBuilder.ToString();
				}
			}
			catch (Exception exception)
			{
				str = "";
			}
			return str;
		}

		public static char[] HexCodes(byte[] text)
		{
			char[] chrArray = new char[(int)text.Length * 2];
			for (int i = 0; i < (int)text.Length; i++)
			{
				text[i].ToString("x2").CopyTo(0, chrArray, i * 2, 2);
			}
			return chrArray;
		}

		public static void MD5File(string dist)
		{
			try
			{
				using (MD5 mD5 = MD5.Create())
				{
					using (FileStream fileStream = File.OpenRead(dist))
					{
						string lower = BitConverter.ToString(mD5.ComputeHash(fileStream)).Replace("-", "").ToLower();
						Console.WriteLine(string.Concat("MD5: ", lower));
					}
				}
			}
			catch (Exception exception)
			{
			}
		}

		public static int SHA1_Int32(string msg)
		{
			int ınt32;
			try
			{
				ınt32 = int.Parse(CryptoSyncer.SHA1File(msg), NumberStyles.HexNumber);
			}
			catch (Exception exception)
			{
				ınt32 = 0;
			}
			return ınt32;
		}

		public static string SHA1File(string msg)
		{
			string str;
			try
			{
				using (SHA1 sHA1 = SHA1.Create())
				{
					byte[] numArray = sHA1.ComputeHash(Encoding.UTF8.GetBytes(msg));
					StringBuilder stringBuilder = new StringBuilder();
					for (int i = 0; i < (int)numArray.Length; i++)
					{
						stringBuilder.Append(numArray[i].ToString("x2"));
					}
					str = stringBuilder.ToString();
				}
			}
			catch (Exception exception)
			{
				str = "";
			}
			return str;
		}

		private static byte[] smethod_0(byte[] byte_2)
		{
			byte[] numArray;
			using (Aes byte0 = Aes.Create())
			{
				byte0.Mode = CipherMode.ECB;
				byte0.Padding = PaddingMode.None;
				byte0.Key = CryptoSyncer.byte_0;
				using (ICryptoTransform cryptoTransform = byte0.CreateEncryptor())
				{
					byte[] numArray1 = (byte[])CryptoSyncer.byte_1.Clone();
					byte[] byte2 = new byte[(int)byte_2.Length];
					for (int i = 0; i < (int)byte_2.Length; i += 16)
					{
						byte[] numArray2 = cryptoTransform.TransformFinalBlock(numArray1, 0, 16);
						int ınt32 = Math.Min(16, (int)byte_2.Length - i);
						for (int j = 0; j < ınt32; j++)
						{
							byte2[i + j] = (byte)(byte_2[i + j] ^ numArray2[j]);
						}
						for (int k = 15; k >= 0; k--)
						{
							ref byte numPointer = ref numArray1[k];
							byte num = (byte)(numPointer + 1);
							numPointer = num;
							if (num != 0)
							{
								break;
							}
						}
					}
					numArray = byte2;
				}
			}
			return numArray;
		}
	}
}