using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Security;
using Plugin.Core.Enums;

namespace Plugin.Core.Utility
{
	// Token: 0x02000029 RID: 41
	public static class Bitwise
	{
		// Token: 0x06000175 RID: 373 RVA: 0x0001555C File Offset: 0x0001375C
		static Bitwise()
		{
			for (int i = 0; i < 10; i++)
			{
				StringBuilder stringBuilder = new StringBuilder(3);
				stringBuilder.Append(" 0");
				stringBuilder.Append(i);
				Bitwise.string_2[i] = stringBuilder.ToString().ToUpper();
			}
			for (int j = 10; j < 16; j++)
			{
				StringBuilder stringBuilder2 = new StringBuilder(3);
				stringBuilder2.Append(" 0");
				stringBuilder2.Append((char)(97 + j - 10));
				Bitwise.string_2[j] = stringBuilder2.ToString().ToUpper();
			}
			for (int k = 16; k < Bitwise.string_2.Length; k++)
			{
				StringBuilder stringBuilder3 = new StringBuilder(3);
				stringBuilder3.Append(' ');
				stringBuilder3.Append(k.ToString("X"));
				Bitwise.string_2[k] = stringBuilder3.ToString().ToUpper();
			}
			for (int l = 0; l < Bitwise.string_3.Length; l++)
			{
				int num = Bitwise.string_3.Length - l;
				StringBuilder stringBuilder4 = new StringBuilder(num * 3);
				for (int m = 0; m < num; m++)
				{
					stringBuilder4.Append("   ");
				}
				Bitwise.string_3[l] = stringBuilder4.ToString().ToUpper();
			}
			for (int n = 0; n < Bitwise.string_4.Length; n++)
			{
				int num2 = Bitwise.string_4.Length - n;
				StringBuilder stringBuilder5 = new StringBuilder(num2);
				for (int num3 = 0; num3 < num2; num3++)
				{
					stringBuilder5.Append(' ');
				}
				Bitwise.string_4[n] = stringBuilder5.ToString().ToUpper();
			}
			for (int num4 = 0; num4 < Bitwise.char_0.Length; num4++)
			{
				if (num4 > 31 && num4 < 127)
				{
					Bitwise.char_0[num4] = (char)num4;
				}
				else
				{
					Bitwise.char_0[num4] = '.';
				}
			}
		}

		// Token: 0x06000176 RID: 374 RVA: 0x000157A0 File Offset: 0x000139A0
		public static byte[] Decrypt(byte[] Data, int Shift)
		{
			byte[] array = new byte[Data.Length];
			Array.Copy(Data, 0, array, 0, array.Length);
			byte b = array[array.Length - 1];
			for (int i = array.Length - 1; i > 0; i--)
			{
				array[i] = (byte)(((int)(array[i - 1] & byte.MaxValue) << 8 - Shift) | ((array[i] & byte.MaxValue) >> Shift));
			}
			array[0] = (byte)(((int)b << 8 - Shift) | ((array[0] & byte.MaxValue) >> Shift));
			return array;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x0001581C File Offset: 0x00013A1C
		public static byte[] Encrypt(byte[] Data, int Shift)
		{
			byte[] array = new byte[Data.Length];
			Array.Copy(Data, 0, array, 0, array.Length);
			byte b = array[0];
			for (int i = 0; i < array.Length - 1; i++)
			{
				array[i] = (byte)(((array[i + 1] & byte.MaxValue) >> 8 - Shift) | ((int)(array[i] & byte.MaxValue) << Shift));
			}
			array[array.Length - 1] = (byte)((b >> 8 - Shift) | ((int)(array[array.Length - 1] & byte.MaxValue) << Shift));
			return array;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x0001589C File Offset: 0x00013A9C
		public static string ToHexData(string EventName, byte[] BuffData)
		{
			int num = BuffData.Length;
			int num2 = 0;
			int num3 = BuffData.Length;
			StringBuilder stringBuilder = new StringBuilder((num / 16 + ((num % 15 != 0) ? 1 : 0) + 4) * 80 + EventName.Length + 16);
			stringBuilder.Append(Bitwise.string_1 + "+--------+-------------------------------------------------+----------------+");
			stringBuilder.Append(string.Format("{0}[!] {1}; Length: [{2} Bytes] </>", Bitwise.string_0, EventName, BuffData.Length));
			stringBuilder.Append(Bitwise.string_0 + "         +-------------------------------------------------+");
			stringBuilder.Append(Bitwise.string_0 + "         |  0  1  2  3  4  5  6  7  8  9  A  B  C  D  E  F |");
			stringBuilder.Append(Bitwise.string_0 + "+--------+-------------------------------------------------+----------------+");
			int i;
			for (i = 0; i < num3; i++)
			{
				int num4 = i - num2;
				int num5 = num4 & 15;
				if (num5 == 0)
				{
					stringBuilder.Append(Bitwise.string_0);
					stringBuilder.Append((((long)num4 & 4294967295L) | 4294967296L).ToString("X"));
					stringBuilder[stringBuilder.Length - 9] = '|';
					stringBuilder.Append('|');
				}
				stringBuilder.Append(Bitwise.string_2[(int)BuffData[i]]);
				if (num5 == 15)
				{
					stringBuilder.Append(" |");
					for (int j = i - 15; j <= i; j++)
					{
						stringBuilder.Append(Bitwise.char_0[(int)BuffData[j]]);
					}
					stringBuilder.Append('|');
				}
			}
			if (((i - num2) & 15) != 0)
			{
				int num6 = num & 15;
				stringBuilder.Append(Bitwise.string_3[num6]);
				stringBuilder.Append(" |");
				for (int k = i - num6; k < i; k++)
				{
					stringBuilder.Append(Bitwise.char_0[(int)BuffData[k]]);
				}
				stringBuilder.Append(Bitwise.string_4[num6]);
				stringBuilder.Append('|');
			}
			stringBuilder.Append(Bitwise.string_0 + "+--------+-------------------------------------------------+----------------+");
			return stringBuilder.ToString();
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00015A98 File Offset: 0x00013C98
		public static string HexArrayToString(byte[] Buffer, int Length)
		{
			string text = "";
			try
			{
				text = Encoding.Unicode.GetString(Buffer, 0, Length);
				int num = text.IndexOf('\0');
				if (num != -1)
				{
					text = text.Substring(0, num);
				}
			}
			catch (Exception ex)
			{
				CLogger.Print(ex.Message, LoggerType.Error, ex);
			}
			return text;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00015AF4 File Offset: 0x00013CF4
		public static byte[] HexStringToByteArray(string HexString)
		{
			string text = HexString.Replace(":", "").Replace("-", "").Replace(" ", "");
			byte[] array = new byte[text.Length / 2];
			for (int i = 0; i < text.Length; i += 2)
			{
				array[i / 2] = (byte)((Bitwise.smethod_2(text.ElementAt(i)) << 4) | Bitwise.smethod_2(text.ElementAt(i + 1)));
			}
			return array;
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00015B74 File Offset: 0x00013D74
		private static string smethod_0(byte[] byte_0)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (int num in byte_0)
			{
				stringBuilder.Append(num.ToString("x2"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00015BB4 File Offset: 0x00013DB4
		private static byte[] smethod_1(string string_5)
		{
			byte[] array;
			using (SHA256 sha = SHA256.Create())
			{
				array = sha.ComputeHash(Encoding.UTF8.GetBytes(string_5));
			}
			return array;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000029CD File Offset: 0x00000BCD
		private static int smethod_2(char char_1)
		{
			if (char_1 >= '0' && char_1 <= '9')
			{
				return (int)(char_1 - '0');
			}
			if (char_1 >= 'A' && char_1 <= 'F')
			{
				return (int)(char_1 - 'A' + '\n');
			}
			if (char_1 >= 'a' && char_1 <= 'f')
			{
				return (int)(char_1 - 'a' + '\n');
			}
			return 0;
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00015BF8 File Offset: 0x00013DF8
		public static string ToByteString(byte[] Result)
		{
			string text = "";
			foreach (string text2 in BitConverter.ToString(Result).Split(new char[] { '-', ',', '.', ':', '\t' }))
			{
				text = text + " " + text2;
			}
			return text;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00015C48 File Offset: 0x00013E48
		public static string GenerateRandomPassword(string AllowerChars, int Length, string Salt)
		{
			string text;
			using (RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider())
			{
				byte[] array = new byte[Length];
				rngcryptoServiceProvider.GetBytes(array);
				char[] array2 = new char[Length];
				for (int i = 0; i < Length; i++)
				{
					array2[i] = AllowerChars[(int)array[i] % AllowerChars.Length];
				}
				text = Bitwise.HashString(new string(array2), Salt, Length);
			}
			return text;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00015CBC File Offset: 0x00013EBC
		public static string HashString(string Text, string Salt, int Length = 32)
		{
			string text;
			using (HMACMD5 hmacmd = new HMACMD5(Encoding.UTF8.GetBytes(Salt)))
			{
				text = Bitwise.smethod_0(hmacmd.ComputeHash(Encoding.UTF8.GetBytes(Text))).Substring(0, Length);
			}
			return text;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00015D18 File Offset: 0x00013F18
		public static List<byte[]> GenerateRSAKeyPair(int SessionId, int SecurityKey, int SeedLength)
		{
			List<byte[]> list = new List<byte[]>();
			RsaKeyPairGenerator rsaKeyPairGenerator = new RsaKeyPairGenerator();
			rsaKeyPairGenerator.Init(new KeyGenerationParameters(new SecureRandom(new CryptoApiRandomGenerator()), SeedLength));
			RsaKeyParameters rsaKeyParameters = (RsaKeyParameters)rsaKeyPairGenerator.GenerateKeyPair().Public;
			list.Add(rsaKeyParameters.Modulus.ToByteArrayUnsigned());
			list.Add(rsaKeyParameters.Exponent.ToByteArrayUnsigned());
			byte[] bytes = BitConverter.GetBytes(SessionId + SecurityKey);
			Array.Copy(bytes, 0, list[0], 0, Math.Min(bytes.Length, list[0].Length));
			return list;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00015DA4 File Offset: 0x00013FA4
		public static ushort GenerateRandomUShort()
		{
			ushort num;
			using (RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider())
			{
				byte[] array = new byte[2];
				rngcryptoServiceProvider.GetBytes(array);
				num = BitConverter.ToUInt16(array, 0);
			}
			return num;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00015DEC File Offset: 0x00013FEC
		public static uint GenerateRandomUInt()
		{
			uint num;
			using (RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider())
			{
				byte[] array = new byte[4];
				rngcryptoServiceProvider.GetBytes(array);
				num = BitConverter.ToUInt32(array, 0);
			}
			return num;
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00015E34 File Offset: 0x00014034
		public static string ReadFile(string Path)
		{
			string text = "";
			using (MD5 md = MD5.Create())
			{
				using (FileStream fileStream = new FileInfo(Path).Open(FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read))
				{
					text = BitConverter.ToString(md.ComputeHash(fileStream)).Replace("-", string.Empty);
					fileStream.Close();
				}
			}
			return text;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00015EB4 File Offset: 0x000140B4
		public static byte[] ArrayRandomize(byte[] Source)
		{
			if (Source != null && Source.Length >= 2)
			{
				byte[] array = new byte[Source.Length];
				Array.Copy(Source, array, Source.Length);
				Random random = new Random();
				for (int i = array.Length - 1; i > 0; i--)
				{
					int num = random.Next(i + 1);
					ref byte ptr = ref array[i];
					byte[] array2 = array;
					int num2 = num;
					byte b = array[num];
					byte b2 = array[i];
					ptr = b;
					array2[num2] = b2;
				}
				return array;
			}
			return Source;
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00002A03 File Offset: 0x00000C03
		public static int BytesToInt(byte Byte1, byte Byte2, byte Byte3, byte Byte4)
		{
			return ((int)Byte4 << 24) | ((int)Byte3 << 16) | ((int)Byte2 << 8) | (int)Byte1;
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00015F24 File Offset: 0x00014124
		public static bool ProcessPacket(byte[] PacketData, int BytesToSkip, int BytesToKeepAtEnd, ushort[] Opcodes)
		{
			Bitwise.Class4 @class = new Bitwise.Class4();
			@class.ushort_0 = BitConverter.ToUInt16(PacketData, 2);
			if (Opcodes.FirstOrDefault(new Func<ushort, bool>(@class.method_0)) != 0)
			{
				return false;
			}
			int num = PacketData.Length - BytesToSkip - BytesToKeepAtEnd;
			if (num < 0)
			{
				num = 0;
			}
			int num2 = BytesToSkip + BytesToKeepAtEnd;
			if (PacketData.Length < num2)
			{
				CLogger.Print("PacketData is too short to apply encryption logic.", LoggerType.Warning, null);
				return false;
			}
			byte[] array = CryptoSyncer.Encrypt(PacketData.Skip(BytesToSkip).Take(num).ToArray<byte>());
			if (array.Length != num)
			{
				CLogger.Print("Encrypted data length mismatch! Encryption function changed data size.", LoggerType.Warning, null);
				return false;
			}
			Array.Copy(array, 0, PacketData, BytesToSkip, array.Length);
			return true;
		}

		// Token: 0x04000082 RID: 130
		private static readonly string string_0 = string.Format("\n", new object[0]);

		// Token: 0x04000083 RID: 131
		private static readonly string string_1 = string.Format("", new object[0]);

		// Token: 0x04000084 RID: 132
		private static readonly char[] char_0 = new char[256];

		// Token: 0x04000085 RID: 133
		private static readonly string[] string_2 = new string[256];

		// Token: 0x04000086 RID: 134
		private static readonly string[] string_3 = new string[16];

		// Token: 0x04000087 RID: 135
		private static readonly string[] string_4 = new string[16];

		// Token: 0x04000088 RID: 136
		public static readonly int[] CRYPTO = new int[] { 29890, 32759, 1360 };

		// Token: 0x0200002A RID: 42
		[CompilerGenerated]
		private sealed class Class4
		{
			// Token: 0x06000188 RID: 392 RVA: 0x00002116 File Offset: 0x00000316
			public Class4()
			{
			}

			// Token: 0x06000189 RID: 393 RVA: 0x00002A14 File Offset: 0x00000C14
			internal bool method_0(ushort ushort_1)
			{
				return ushort_1 == this.ushort_0;
			}

			// Token: 0x04000089 RID: 137
			public ushort ushort_0;
		}
	}
}
