using System;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Plugin.Core.Utility;

public static class CryptoSyncer
{
	private static readonly byte[] byte_0;

	private static readonly byte[] byte_1;

	static CryptoSyncer()
	{
		byte_0 = Bitwise.HexStringToByteArray("BC 34 88 F9 C3 00 B1 64 37 B0 6C B3 EE F3 33 3A");
		byte_1 = Bitwise.HexStringToByteArray("71 F0 D9 9E 15 47 9A BA 72 C3 4F F8 04 27 D8 0A");
	}

	private static byte[] smethod_0(byte[] byte_2)
	{
		using Aes aes = Aes.Create();
		aes.Mode = CipherMode.ECB;
		aes.Padding = PaddingMode.None;
		aes.Key = byte_0;
		using ICryptoTransform cryptoTransform = aes.CreateEncryptor();
		byte[] array = (byte[])byte_1.Clone();
		byte[] array2 = new byte[byte_2.Length];
		for (int i = 0; i < byte_2.Length; i += 16)
		{
			byte[] array3 = cryptoTransform.TransformFinalBlock(array, 0, 16);
			int num = Math.Min(16, byte_2.Length - i);
			for (int j = 0; j < num; j++)
			{
				array2[i + j] = (byte)(byte_2[i + j] ^ array3[j]);
			}
			int num2 = 15;
			while (num2 >= 0 && ++array[num2] == 0)
			{
				num2--;
			}
		}
		return array2;
	}

	public static byte[] Encrypt(byte[] Plain)
	{
		return smethod_0(Plain);
	}

	public static byte[] Decrypt(byte[] Cipher)
	{
		return smethod_0(Cipher);
	}

	public static void MD5File(string dist)
	{
		try
		{
			using MD5 mD = MD5.Create();
			using FileStream inputStream = File.OpenRead(dist);
			string text = BitConverter.ToString(mD.ComputeHash(inputStream)).Replace("-", "").ToLower();
			Console.WriteLine("MD5: " + text);
		}
		catch (Exception)
		{
		}
	}

	public static char[] HexCodes(byte[] text)
	{
		char[] array = new char[text.Length * 2];
		for (int i = 0; i < text.Length; i++)
		{
			text[i].ToString("x2").CopyTo(0, array, i * 2, 2);
		}
		return array;
	}

	public static string SHA1File(string msg)
	{
		try
		{
			using SHA1 sHA = SHA1.Create();
			byte[] array = sHA.ComputeHash(Encoding.UTF8.GetBytes(msg));
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("x2"));
			}
			return stringBuilder.ToString();
		}
		catch (Exception)
		{
			return "";
		}
	}

	public static int SHA1_Int32(string msg)
	{
		try
		{
			return int.Parse(SHA1File(msg), NumberStyles.HexNumber);
		}
		catch (Exception)
		{
			return 0;
		}
	}

	public static string GetHWID()
	{
		try
		{
			string s = Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER") + Environment.GetEnvironmentVariable("COMPUTERNAME") + Environment.UserName.Trim();
			using MD5 mD = MD5.Create();
			byte[] array = mD.ComputeHash(Encoding.UTF8.GetBytes(s));
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				stringBuilder.Append(array[i].ToString("x3").Substring(0, 3));
				if (i != array.Length - 1)
				{
					stringBuilder.Append("-");
				}
			}
			return stringBuilder.ToString();
		}
		catch (Exception)
		{
			return "";
		}
	}
}
