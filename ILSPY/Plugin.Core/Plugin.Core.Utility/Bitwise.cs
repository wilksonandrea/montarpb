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

namespace Plugin.Core.Utility;

public static class Bitwise
{
	[CompilerGenerated]
	private sealed class Class4
	{
		public ushort ushort_0;

		internal bool method_0(ushort ushort_1)
		{
			return ushort_1 == ushort_0;
		}
	}

	private static readonly string string_0;

	private static readonly string string_1;

	private static readonly char[] char_0;

	private static readonly string[] string_2;

	private static readonly string[] string_3;

	private static readonly string[] string_4;

	public static readonly int[] CRYPTO;

	static Bitwise()
	{
		string_0 = $"\n";
		string_1 = string.Format("");
		char_0 = new char[256];
		string_2 = new string[256];
		string_3 = new string[16];
		string_4 = new string[16];
		CRYPTO = new int[3] { 29890, 32759, 1360 };
		for (int i = 0; i < 10; i++)
		{
			StringBuilder stringBuilder = new StringBuilder(3);
			stringBuilder.Append(" 0");
			stringBuilder.Append(i);
			string_2[i] = stringBuilder.ToString().ToUpper();
		}
		for (int j = 10; j < 16; j++)
		{
			StringBuilder stringBuilder2 = new StringBuilder(3);
			stringBuilder2.Append(" 0");
			stringBuilder2.Append((char)(97 + j - 10));
			string_2[j] = stringBuilder2.ToString().ToUpper();
		}
		for (int k = 16; k < string_2.Length; k++)
		{
			StringBuilder stringBuilder3 = new StringBuilder(3);
			stringBuilder3.Append(' ');
			stringBuilder3.Append(k.ToString("X"));
			string_2[k] = stringBuilder3.ToString().ToUpper();
		}
		for (int l = 0; l < string_3.Length; l++)
		{
			int num = string_3.Length - l;
			StringBuilder stringBuilder4 = new StringBuilder(num * 3);
			for (int m = 0; m < num; m++)
			{
				stringBuilder4.Append("   ");
			}
			string_3[l] = stringBuilder4.ToString().ToUpper();
		}
		for (int n = 0; n < string_4.Length; n++)
		{
			int num2 = string_4.Length - n;
			StringBuilder stringBuilder5 = new StringBuilder(num2);
			for (int num3 = 0; num3 < num2; num3++)
			{
				stringBuilder5.Append(' ');
			}
			string_4[n] = stringBuilder5.ToString().ToUpper();
		}
		for (int num4 = 0; num4 < char_0.Length; num4++)
		{
			if (num4 > 31 && num4 < 127)
			{
				char_0[num4] = (char)num4;
			}
			else
			{
				char_0[num4] = '.';
			}
		}
	}

	public static byte[] Decrypt(byte[] Data, int Shift)
	{
		byte[] array = new byte[Data.Length];
		Array.Copy(Data, 0, array, 0, array.Length);
		byte b = array[array.Length - 1];
		for (int num = array.Length - 1; num > 0; num--)
		{
			array[num] = (byte)(((array[num - 1] & 0xFF) << 8 - Shift) | ((array[num] & 0xFF) >> Shift));
		}
		array[0] = (byte)((b << 8 - Shift) | ((array[0] & 0xFF) >> Shift));
		return array;
	}

	public static byte[] Encrypt(byte[] Data, int Shift)
	{
		byte[] array = new byte[Data.Length];
		Array.Copy(Data, 0, array, 0, array.Length);
		byte b = array[0];
		for (int i = 0; i < array.Length - 1; i++)
		{
			array[i] = (byte)(((array[i + 1] & 0xFF) >> 8 - Shift) | ((array[i] & 0xFF) << Shift));
		}
		array[array.Length - 1] = (byte)((b >> 8 - Shift) | ((array[array.Length - 1] & 0xFF) << Shift));
		return array;
	}

	public static string ToHexData(string EventName, byte[] BuffData)
	{
		int num = BuffData.Length;
		int num2 = 0;
		int num3 = BuffData.Length;
		StringBuilder stringBuilder = new StringBuilder((num / 16 + ((num % 15 != 0) ? 1 : 0) + 4) * 80 + EventName.Length + 16);
		stringBuilder.Append(string_1 + "+--------+-------------------------------------------------+----------------+");
		stringBuilder.Append($"{string_0}[!] {EventName}; Length: [{BuffData.Length} Bytes] </>");
		stringBuilder.Append(string_0 + "         +-------------------------------------------------+");
		stringBuilder.Append(string_0 + "         |  0  1  2  3  4  5  6  7  8  9  A  B  C  D  E  F |");
		stringBuilder.Append(string_0 + "+--------+-------------------------------------------------+----------------+");
		int i;
		for (i = 0; i < num3; i++)
		{
			int num4 = i - num2;
			int num5 = num4 & 0xF;
			if (num5 == 0)
			{
				stringBuilder.Append(string_0);
				stringBuilder.Append(((num4 & 0xFFFFFFFFL) | 0x100000000L).ToString("X"));
				stringBuilder[stringBuilder.Length - 9] = '|';
				stringBuilder.Append('|');
			}
			stringBuilder.Append(string_2[BuffData[i]]);
			if (num5 == 15)
			{
				stringBuilder.Append(" |");
				for (int j = i - 15; j <= i; j++)
				{
					stringBuilder.Append(char_0[BuffData[j]]);
				}
				stringBuilder.Append('|');
			}
		}
		if (((uint)(i - num2) & 0xFu) != 0)
		{
			int num6 = num & 0xF;
			stringBuilder.Append(string_3[num6]);
			stringBuilder.Append(" |");
			for (int k = i - num6; k < i; k++)
			{
				stringBuilder.Append(char_0[BuffData[k]]);
			}
			stringBuilder.Append(string_4[num6]);
			stringBuilder.Append('|');
		}
		stringBuilder.Append(string_0 + "+--------+-------------------------------------------------+----------------+");
		return stringBuilder.ToString();
	}

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
				return text;
			}
			return text;
		}
		catch (Exception ex)
		{
			CLogger.Print(ex.Message, LoggerType.Error, ex);
			return text;
		}
	}

	public static byte[] HexStringToByteArray(string HexString)
	{
		string text = HexString.Replace(":", "").Replace("-", "").Replace(" ", "");
		byte[] array = new byte[text.Length / 2];
		for (int i = 0; i < text.Length; i += 2)
		{
			array[i / 2] = (byte)((smethod_2(text.ElementAt(i)) << 4) | smethod_2(text.ElementAt(i + 1)));
		}
		return array;
	}

	private static string smethod_0(byte[] byte_0)
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (int num in byte_0)
		{
			stringBuilder.Append(num.ToString("x2"));
		}
		return stringBuilder.ToString();
	}

	private static byte[] smethod_1(string string_5)
	{
		using SHA256 sHA = SHA256.Create();
		return sHA.ComputeHash(Encoding.UTF8.GetBytes(string_5));
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
		if (char_1 >= 'a' && char_1 <= 'f')
		{
			return char_1 - 97 + 10;
		}
		return 0;
	}

	public static string ToByteString(byte[] Result)
	{
		string text = "";
		string[] array = BitConverter.ToString(Result).Split('-', ',', '.', ':', '\t');
		foreach (string text2 in array)
		{
			text = text + " " + text2;
		}
		return text;
	}

	public static string GenerateRandomPassword(string AllowerChars, int Length, string Salt)
	{
		using RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider();
		byte[] array = new byte[Length];
		rNGCryptoServiceProvider.GetBytes(array);
		char[] array2 = new char[Length];
		for (int i = 0; i < Length; i++)
		{
			array2[i] = AllowerChars[(int)array[i] % AllowerChars.Length];
		}
		return HashString(new string(array2), Salt, Length);
	}

	public static string HashString(string Text, string Salt, int Length = 32)
	{
		using HMACMD5 hMACMD = new HMACMD5(Encoding.UTF8.GetBytes(Salt));
		return smethod_0(hMACMD.ComputeHash(Encoding.UTF8.GetBytes(Text))).Substring(0, Length);
	}

	public static List<byte[]> GenerateRSAKeyPair(int SessionId, int SecurityKey, int SeedLength)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Expected O, but got Unknown
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Expected O, but got Unknown
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected O, but got Unknown
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		List<byte[]> list = new List<byte[]>();
		RsaKeyPairGenerator val = new RsaKeyPairGenerator();
		val.Init(new KeyGenerationParameters(new SecureRandom((IRandomGenerator)new CryptoApiRandomGenerator()), SeedLength));
		RsaKeyParameters val2 = (RsaKeyParameters)val.GenerateKeyPair().Public;
		list.Add(val2.Modulus.ToByteArrayUnsigned());
		list.Add(val2.Exponent.ToByteArrayUnsigned());
		byte[] bytes = BitConverter.GetBytes(SessionId + SecurityKey);
		Array.Copy(bytes, 0, list[0], 0, Math.Min(bytes.Length, list[0].Length));
		return list;
	}

	public static ushort GenerateRandomUShort()
	{
		using RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider();
		byte[] array = new byte[2];
		rNGCryptoServiceProvider.GetBytes(array);
		return BitConverter.ToUInt16(array, 0);
	}

	public static uint GenerateRandomUInt()
	{
		using RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider();
		byte[] array = new byte[4];
		rNGCryptoServiceProvider.GetBytes(array);
		return BitConverter.ToUInt32(array, 0);
	}

	public static string ReadFile(string Path)
	{
		string text = "";
		using MD5 mD = MD5.Create();
		using FileStream fileStream = new FileInfo(Path).Open(FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);
		text = BitConverter.ToString(mD.ComputeHash(fileStream)).Replace("-", string.Empty);
		fileStream.Close();
		return text;
	}

	public static byte[] ArrayRandomize(byte[] Source)
	{
		if (Source != null && Source.Length >= 2)
		{
			byte[] array = new byte[Source.Length];
			Array.Copy(Source, array, Source.Length);
			Random random = new Random();
			for (int num = array.Length - 1; num > 0; num--)
			{
				int num2 = random.Next(num + 1);
				ref byte reference = ref array[num];
				ref byte reference2 = ref array[num2];
				byte b = array[num2];
				byte b2 = array[num];
				reference = b;
				reference2 = b2;
			}
			return array;
		}
		return Source;
	}

	public static int BytesToInt(byte Byte1, byte Byte2, byte Byte3, byte Byte4)
	{
		return (Byte4 << 24) | (Byte3 << 16) | (Byte2 << 8) | Byte1;
	}

	public static bool ProcessPacket(byte[] PacketData, int BytesToSkip, int BytesToKeepAtEnd, ushort[] Opcodes)
	{
		ushort ushort_2 = BitConverter.ToUInt16(PacketData, 2);
		if (Opcodes.FirstOrDefault((ushort ushort_1) => ushort_1 == ushort_2) != 0)
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
			CLogger.Print("PacketData is too short to apply encryption logic.", LoggerType.Warning);
			return false;
		}
		byte[] array = CryptoSyncer.Encrypt(PacketData.Skip(BytesToSkip).Take(num).ToArray());
		if (array.Length != num)
		{
			CLogger.Print("Encrypted data length mismatch! Encryption function changed data size.", LoggerType.Warning);
			return false;
		}
		Array.Copy(array, 0, PacketData, BytesToSkip, array.Length);
		return true;
	}
}
