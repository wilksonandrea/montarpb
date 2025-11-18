using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using Plugin.Core;
using Plugin.Core.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace Plugin.Core.Utility
{
	public static class Bitwise
	{
		private readonly static string string_0;

		private readonly static string string_1;

		private readonly static char[] char_0;

		private readonly static string[] string_2;

		private readonly static string[] string_3;

		private readonly static string[] string_4;

		public readonly static int[] CRYPTO;

		static Bitwise()
		{
			Bitwise.string_0 = string.Format("\n", new object[0]);
			Bitwise.string_1 = string.Format("", new object[0]);
			Bitwise.char_0 = new char[256];
			Bitwise.string_2 = new string[256];
			Bitwise.string_3 = new string[16];
			Bitwise.string_4 = new string[16];
			Bitwise.CRYPTO = new int[] { 29890, 32759, 1360 };
			for (int i = 0; i < 10; i++)
			{
				StringBuilder stringBuilder = new StringBuilder(3);
				stringBuilder.Append(" 0");
				stringBuilder.Append(i);
				Bitwise.string_2[i] = stringBuilder.ToString().ToUpper();
			}
			for (int j = 10; j < 16; j++)
			{
				StringBuilder stringBuilder1 = new StringBuilder(3);
				stringBuilder1.Append(" 0");
				stringBuilder1.Append((char)(97 + j - 10));
				Bitwise.string_2[j] = stringBuilder1.ToString().ToUpper();
			}
			for (int k = 16; k < (int)Bitwise.string_2.Length; k++)
			{
				StringBuilder stringBuilder2 = new StringBuilder(3);
				stringBuilder2.Append(' ');
				stringBuilder2.Append(k.ToString("X"));
				Bitwise.string_2[k] = stringBuilder2.ToString().ToUpper();
			}
			for (int l = 0; l < (int)Bitwise.string_3.Length; l++)
			{
				int length = (int)Bitwise.string_3.Length - l;
				StringBuilder stringBuilder3 = new StringBuilder(length * 3);
				for (int m = 0; m < length; m++)
				{
					stringBuilder3.Append("   ");
				}
				Bitwise.string_3[l] = stringBuilder3.ToString().ToUpper();
			}
			for (int n = 0; n < (int)Bitwise.string_4.Length; n++)
			{
				int ınt32 = (int)Bitwise.string_4.Length - n;
				StringBuilder stringBuilder4 = new StringBuilder(ınt32);
				for (int o = 0; o < ınt32; o++)
				{
					stringBuilder4.Append(' ');
				}
				Bitwise.string_4[n] = stringBuilder4.ToString().ToUpper();
			}
			for (int p = 0; p < (int)Bitwise.char_0.Length; p++)
			{
				if (p <= 31 || p >= 127)
				{
					Bitwise.char_0[p] = '.';
				}
				else
				{
					Bitwise.char_0[p] = (char)p;
				}
			}
		}

		public static byte[] ArrayRandomize(byte[] Source)
		{
			if (Source == null || (int)Source.Length < 2)
			{
				return Source;
			}
			byte[] numArray = new byte[(int)Source.Length];
			Array.Copy(Source, numArray, (int)Source.Length);
			Random random = new Random();
			for (int i = (int)numArray.Length - 1; i > 0; i--)
			{
				int ınt32 = random.Next(i + 1);
				ref byte numPointer = ref numArray[i];
				byte num = numArray[ınt32];
				byte num1 = numArray[i];
				numPointer = num;
				numArray[ınt32] = num1;
			}
			return numArray;
		}

		public static int BytesToInt(byte Byte1, byte Byte2, byte Byte3, byte Byte4)
		{
			return Byte4 << 24 | Byte3 << 16 | Byte2 << 8 | Byte1;
		}

		public static byte[] Decrypt(byte[] Data, int Shift)
		{
			byte[] shift = new byte[(int)Data.Length];
			Array.Copy(Data, 0, shift, 0, (int)shift.Length);
			byte num = shift[(int)shift.Length - 1];
			for (int i = (int)shift.Length - 1; i > 0; i--)
			{
				shift[i] = (byte)((shift[i - 1] & 255) << (8 - Shift & 31) | (shift[i] & 255) >> (Shift & 31));
			}
			shift[0] = (byte)(num << (8 - Shift & 31) | (shift[0] & 255) >> (Shift & 31));
			return shift;
		}

		public static byte[] Encrypt(byte[] Data, int Shift)
		{
			byte[] shift = new byte[(int)Data.Length];
			Array.Copy(Data, 0, shift, 0, (int)shift.Length);
			byte num = shift[0];
			for (int i = 0; i < (int)shift.Length - 1; i++)
			{
				shift[i] = (byte)((shift[i + 1] & 255) >> (8 - Shift & 31) | (shift[i] & 255) << (Shift & 31));
			}
			shift[(int)shift.Length - 1] = (byte)(num >> (8 - Shift & 31) | (shift[(int)shift.Length - 1] & 255) << (Shift & 31));
			return shift;
		}

		public static string GenerateRandomPassword(string AllowerChars, int Length, string Salt)
		{
			string str;
			using (RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider())
			{
				byte[] numArray = new byte[Length];
				rNGCryptoServiceProvider.GetBytes(numArray);
				char[] allowerChars = new char[Length];
				for (int i = 0; i < Length; i++)
				{
					allowerChars[i] = AllowerChars[numArray[i] % AllowerChars.Length];
				}
				str = Bitwise.HashString(new string(allowerChars), Salt, Length);
			}
			return str;
		}

		public static uint GenerateRandomUInt()
		{
			uint uInt32;
			using (RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider())
			{
				byte[] numArray = new byte[4];
				rNGCryptoServiceProvider.GetBytes(numArray);
				uInt32 = BitConverter.ToUInt32(numArray, 0);
			}
			return uInt32;
		}

		public static ushort GenerateRandomUShort()
		{
			ushort uInt16;
			using (RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider())
			{
				byte[] numArray = new byte[2];
				rNGCryptoServiceProvider.GetBytes(numArray);
				uInt16 = BitConverter.ToUInt16(numArray, 0);
			}
			return uInt16;
		}

		public static List<byte[]> GenerateRSAKeyPair(int SessionId, int SecurityKey, int SeedLength)
		{
			List<byte[]> numArrays = new List<byte[]>();
			RsaKeyPairGenerator rsaKeyPairGenerator = new RsaKeyPairGenerator();
			rsaKeyPairGenerator.Init(new KeyGenerationParameters(new SecureRandom(new CryptoApiRandomGenerator()), SeedLength));
			RsaKeyParameters @public = (RsaKeyParameters)rsaKeyPairGenerator.GenerateKeyPair().get_Public();
			numArrays.Add(@public.get_Modulus().ToByteArrayUnsigned());
			numArrays.Add(@public.get_Exponent().ToByteArrayUnsigned());
			byte[] bytes = BitConverter.GetBytes(SessionId + SecurityKey);
			Array.Copy(bytes, 0, numArrays[0], 0, Math.Min((int)bytes.Length, (int)numArrays[0].Length));
			return numArrays;
		}

		public static string HashString(string Text, string Salt, int Length = 32)
		{
			string str;
			using (HMACMD5 hMACMD5 = new HMACMD5(Encoding.UTF8.GetBytes(Salt)))
			{
				str = Bitwise.smethod_0(hMACMD5.ComputeHash(Encoding.UTF8.GetBytes(Text))).Substring(0, Length);
			}
			return str;
		}

		public static string HexArrayToString(byte[] Buffer, int Length)
		{
			string str = "";
			try
			{
				str = Encoding.Unicode.GetString(Buffer, 0, Length);
				int ınt32 = str.IndexOf('\0');
				if (ınt32 != -1)
				{
					str = str.Substring(0, ınt32);
				}
			}
			catch (Exception exception1)
			{
				Exception exception = exception1;
				CLogger.Print(exception.Message, LoggerType.Error, exception);
			}
			return str;
		}

		public static byte[] HexStringToByteArray(string HexString)
		{
			string str = HexString.Replace(":", "").Replace("-", "").Replace(" ", "");
			byte[] numArray = new byte[str.Length / 2];
			for (int i = 0; i < str.Length; i += 2)
			{
				numArray[i / 2] = (byte)(Bitwise.smethod_2(str.ElementAt<char>(i)) << 4 | Bitwise.smethod_2(str.ElementAt<char>(i + 1)));
			}
			return numArray;
		}

		public static bool ProcessPacket(byte[] PacketData, int BytesToSkip, int BytesToKeepAtEnd, ushort[] Opcodes)
		{
			ushort uInt16 = BitConverter.ToUInt16(PacketData, 2);
			if (Opcodes.FirstOrDefault<ushort>((ushort ushort_1) => ushort_1 == uInt16) != 0)
			{
				return false;
			}
			int length = (int)PacketData.Length - BytesToSkip - BytesToKeepAtEnd;
			if (length < 0)
			{
				length = 0;
			}
			if ((int)PacketData.Length < BytesToSkip + BytesToKeepAtEnd)
			{
				CLogger.Print("PacketData is too short to apply encryption logic.", LoggerType.Warning, null);
				return false;
			}
			byte[] numArray = CryptoSyncer.Encrypt(PacketData.Skip<byte>(BytesToSkip).Take<byte>(length).ToArray<byte>());
			if ((int)numArray.Length != length)
			{
				CLogger.Print("Encrypted data length mismatch! Encryption function changed data size.", LoggerType.Warning, null);
				return false;
			}
			Array.Copy(numArray, 0, PacketData, BytesToSkip, (int)numArray.Length);
			return true;
		}

		public static string ReadFile(string Path)
		{
			string str = "";
			using (MD5 mD5 = MD5.Create())
			{
				using (FileStream fileStream = (new FileInfo(Path)).Open(FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
				{
					str = BitConverter.ToString(mD5.ComputeHash(fileStream)).Replace("-", string.Empty);
					fileStream.Close();
				}
			}
			return str;
		}

		private static string smethod_0(byte[] byte_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			byte[] byte0 = byte_0;
			for (int i = 0; i < (int)byte0.Length; i++)
			{
				int ınt32 = byte0[i];
				stringBuilder.Append(ınt32.ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		private static byte[] smethod_1(string string_5)
		{
			byte[] numArray;
			using (SHA256 sHA256 = SHA256.Create())
			{
				numArray = sHA256.ComputeHash(Encoding.UTF8.GetBytes(string_5));
			}
			return numArray;
		}

		private static int smethod_2(char char_1)
		{
			if (char_1 >= '0' && char_1 <= '9')
			{
				return char_1 - 48;
			}
			if (char_1 >= 'A' && char_1 <= 'F')
			{
				return char_1 - 65 + 10;
			}
			if (char_1 < 'a' || char_1 > 'f')
			{
				return 0;
			}
			return char_1 - 97 + 10;
		}

		public static string ToByteString(byte[] Result)
		{
			string str = "";
			string[] strArrays = BitConverter.ToString(Result).Split(new char[] { '-', ',', '.', ':', '\t' });
			for (int i = 0; i < (int)strArrays.Length; i++)
			{
				str = string.Concat(str, " ", strArrays[i]);
			}
			return str;
		}

		public static string ToHexData(string EventName, byte[] BuffData)
		{
			int i;
			int length = (int)BuffData.Length;
			int ınt32 = 0;
			int length1 = (int)BuffData.Length;
			StringBuilder stringBuilder = new StringBuilder((length / 16 + (length % 15 != 0) + 4) * 80 + EventName.Length + 16);
			stringBuilder.Append(string.Concat(Bitwise.string_1, "+--------+-------------------------------------------------+----------------+"));
			stringBuilder.Append(string.Format("{0}[!] {1}; Length: [{2} Bytes] </>", Bitwise.string_0, EventName, (int)BuffData.Length));
			stringBuilder.Append(string.Concat(Bitwise.string_0, "         +-------------------------------------------------+"));
			stringBuilder.Append(string.Concat(Bitwise.string_0, "         |  0  1  2  3  4  5  6  7  8  9  A  B  C  D  E  F |"));
			stringBuilder.Append(string.Concat(Bitwise.string_0, "+--------+-------------------------------------------------+----------------+"));
			for (i = 0; i < length1; i++)
			{
				int ınt321 = i - ınt32;
				int ınt322 = ınt321 & 15;
				if (ınt322 == 0)
				{
					stringBuilder.Append(Bitwise.string_0);
					long ınt64 = (long)ınt321 & 4294967295L | 4294967296L;
					stringBuilder.Append(ınt64.ToString("X"));
					stringBuilder[stringBuilder.Length - 9] = '|';
					stringBuilder.Append('|');
				}
				stringBuilder.Append(Bitwise.string_2[BuffData[i]]);
				if (ınt322 == 15)
				{
					stringBuilder.Append(" |");
					for (int j = i - 15; j <= i; j++)
					{
						stringBuilder.Append(Bitwise.char_0[BuffData[j]]);
					}
					stringBuilder.Append('|');
				}
			}
			if ((i - ınt32 & 15) != 0)
			{
				int ınt323 = length & 15;
				stringBuilder.Append(Bitwise.string_3[ınt323]);
				stringBuilder.Append(" |");
				for (int k = i - ınt323; k < i; k++)
				{
					stringBuilder.Append(Bitwise.char_0[BuffData[k]]);
				}
				stringBuilder.Append(Bitwise.string_4[ınt323]);
				stringBuilder.Append('|');
			}
			stringBuilder.Append(string.Concat(Bitwise.string_0, "+--------+-------------------------------------------------+----------------+"));
			return stringBuilder.ToString();
		}
	}
}